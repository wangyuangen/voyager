using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using YK.ORM.Contract;
using YK.ORM.Enums;

namespace YK.ORM
{
    public static class SpecificationBuilderExtensions
    {
        public static ISpecificationBuilder<T> SearchBy<T>(this ISpecificationBuilder<T> query, BaseFilter filter) =>
            query
                .SearchByKeyword(filter.Keyword)
                .AdvancedSearch(filter.Search)
                .AdvancedFilter(filter.Filter);

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static ISpecificationBuilder<T> PaginateBy<T>(this ISpecificationBuilder<T> query, PaginationFilter filter)
        {
            if (filter.PageNumber <= 0)
            {
                filter.PageNumber = 1;
            }

            if (filter.PageSize <= 0)
            {
                filter.PageSize = 10;
            }

            if (filter.PageNumber > 1)
            {
                query = query.Skip((filter.PageNumber - 1) * filter.PageSize);
            }

            return query
                .Take(filter.PageSize)
                .OrderBy(filter.OrderBy);
        }

        /// <summary>
        /// 关键字模糊查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="specificationBuilder"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static IOrderedSpecificationBuilder<T> SearchByKeyword<T>(
            this ISpecificationBuilder<T> specificationBuilder,
            string? keyword) =>
            specificationBuilder.AdvancedSearch(new AdvancedSearch { Keyword = keyword });

        /// <summary>
        /// 指定字段模糊查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="specificationBuilder"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static IOrderedSpecificationBuilder<T> AdvancedSearch<T>(
            this ISpecificationBuilder<T> specificationBuilder,
            AdvancedSearch? search)
        {
            if (!string.IsNullOrEmpty(search?.Keyword))
            {
                if (search.Fields?.Any() is true)
                {
                    // search seleted fields (can contain deeper nested fields)
                    foreach (string field in search.Fields)
                    {
                        var paramExpr = Expression.Parameter(typeof(T));
                        MemberExpression propertyExpr = GetPropertyExpression(field, paramExpr);

                        specificationBuilder.AddSearchPropertyByKeyword(propertyExpr, paramExpr, search.Keyword);
                    }
                }
                else
                {
                    // search all fields (only first level)
                    foreach (var property in typeof(T).GetProperties()
                        .Where(prop => (Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType) is { } propertyType
                            && !propertyType.IsEnum
                            && Type.GetTypeCode(propertyType) != TypeCode.Object))
                    {
                        var paramExpr = Expression.Parameter(typeof(T));
                        var propertyExpr = Expression.Property(paramExpr, property);

                        specificationBuilder.AddSearchPropertyByKeyword(propertyExpr, paramExpr, search.Keyword);
                    }
                }
            }

            return new OrderedSpecificationBuilder<T>(specificationBuilder.Specification);
        }

        private static void AddSearchPropertyByKeyword<T>(
            this ISpecificationBuilder<T> specificationBuilder,
            Expression propertyExpr,
            ParameterExpression paramExpr,
            string keyword,
            FilterOperatorEnum operatorSearch = FilterOperatorEnum.Contains)
        {
            if (propertyExpr is not MemberExpression memberExpr || memberExpr.Member is not PropertyInfo property)
            {
                throw new ArgumentException("propertyExpr must be a property expression.", nameof(propertyExpr));
            }

            string searchTerm = operatorSearch switch
            {
                FilterOperatorEnum.StartsWith => $"{keyword}%",
                FilterOperatorEnum.EndsWith => $"%{keyword}",
                FilterOperatorEnum.Contains => $"%{keyword}%",
                _ => throw new ArgumentException("operatorSearch is not valid.", nameof(operatorSearch))
            };

            // Generate lambda [ x => x.Property ] for string properties
            // or [ x => ((object)x.Property) == null ? null : x.Property.ToString() ] for other properties
            Expression selectorExpr =
                property.PropertyType == typeof(string)
                    ? propertyExpr
                    : Expression.Condition(
                        Expression.Equal(Expression.Convert(propertyExpr, typeof(object)), Expression.Constant(null, typeof(object))),
                        Expression.Constant(null, typeof(string)),
                        Expression.Call(propertyExpr, "ToString", null, null));

            var selector = Expression.Lambda<Func<T, string>>(selectorExpr, paramExpr);

            ((List<SearchExpressionInfo<T>>)specificationBuilder.Specification.SearchCriterias)
                .Add(new SearchExpressionInfo<T>(selector, searchTerm, 1));
        }

        /// <summary>
        /// 条件过滤
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="specificationBuilder"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IOrderedSpecificationBuilder<T> AdvancedFilter<T>(
            this ISpecificationBuilder<T> specificationBuilder,
            AdvancedFilter? filter)
        {
            if (filter is not null)
            {
                var parameter = Expression.Parameter(typeof(T));

                Expression binaryExpresioFilter;

                if (!filter.Logic.IsNull())
                {
                    if (filter.Filters is null) throw new ArgumentException("The Filters attribute is required when declaring a logic");
                    binaryExpresioFilter = CreateFilterExpression(filter?.Logic, filter?.Filters, parameter);
                }
                else
                {
                    var filterValid = GetValidFilter(filter);
                    binaryExpresioFilter = CreateFilterExpression(filterValid.Field!, filterValid.Operator!, filterValid.Value, parameter);
                }

                ((List<WhereExpressionInfo<T>>)specificationBuilder.Specification.WhereExpressions)
                    .Add(new WhereExpressionInfo<T>(Expression.Lambda<Func<T, bool>>(binaryExpresioFilter, parameter)));
            }

            return new OrderedSpecificationBuilder<T>(specificationBuilder.Specification);
        }

        private static Expression CreateFilterExpression(
            FilterLogicEnum? logic,
            IEnumerable<AdvancedFilter>? filters,
            ParameterExpression parameter)
        {
            Expression filterExpression = default!;

            if (filters is not null)
            {
                foreach (var filter in filters)
                {
                    Expression bExpresionFilter;

                    if (!filter.Logic.IsNull())
                    {
                        if (filter.Filters is null) throw new ArgumentException("The Filters attribute is required when declaring a logic");
                        bExpresionFilter = CreateFilterExpression(filter.Logic, filter.Filters, parameter);
                    }
                    else
                    {
                        var filterValid = GetValidFilter(filter);
                        bExpresionFilter = CreateFilterExpression(filterValid.Field!, filterValid?.Operator, filterValid?.Value, parameter);
                    }

                    filterExpression = filterExpression is null ? bExpresionFilter : CombineFilter(logic, filterExpression, bExpresionFilter);
                }
            }

            return filterExpression;
        }

        private static Expression CreateFilterExpression(
            string field,
            FilterOperatorEnum? filterOperator,
            object? value,
            ParameterExpression parameter)
        {
            var propertyExpresion = GetPropertyExpression(field, parameter);
            var valueExpresion = GeValuetExpression(field, value, propertyExpresion.Type);
            return CreateFilterExpression(propertyExpresion, valueExpresion, filterOperator);
        }

        private static Expression CreateFilterExpression(
            MemberExpression memberExpression,
            ConstantExpression constantExpression,
            FilterOperatorEnum? filterOperator)
        {
            return filterOperator switch
            {
                FilterOperatorEnum.EQ => Expression.Equal(memberExpression, constantExpression),
                FilterOperatorEnum.NEQ => Expression.NotEqual(memberExpression, constantExpression),
                FilterOperatorEnum.LT => Expression.LessThan(memberExpression, constantExpression),
                FilterOperatorEnum.LTE => Expression.LessThanOrEqual(memberExpression, constantExpression),
                FilterOperatorEnum.GT => Expression.GreaterThan(memberExpression, constantExpression),
                FilterOperatorEnum.GTE => Expression.GreaterThanOrEqual(memberExpression, constantExpression),
                FilterOperatorEnum.Contains => Expression.Call(memberExpression, nameof(FilterOperatorEnum.Contains), null, constantExpression),
                FilterOperatorEnum.StartsWith => Expression.Call(memberExpression, nameof(FilterOperatorEnum.StartsWith), null, constantExpression),
                FilterOperatorEnum.EndsWith => Expression.Call(memberExpression, nameof(FilterOperatorEnum.EndsWith), null, constantExpression),
                _ => throw new ArgumentException("Filter Operator is not valid."),
            };
        }

        private static Expression CombineFilter(
            FilterLogicEnum? filterLogic,
            Expression bExpresionBase,
            Expression bExpresion)
        {
            return filterLogic switch
            {
                FilterLogicEnum.And => Expression.And(bExpresionBase, bExpresion),
                FilterLogicEnum.Or => Expression.Or(bExpresionBase, bExpresion),
                FilterLogicEnum.Xor => Expression.ExclusiveOr(bExpresionBase, bExpresion),
                _ => throw new ArgumentException("FilterLogic is not valid.", nameof(filterLogic)),
            };
        }

        private static MemberExpression GetPropertyExpression(
            string propertyName,
            ParameterExpression parameter)
        {
            Expression propertyExpression = parameter;
            foreach (string member in propertyName.Split('.'))
            {
                propertyExpression = Expression.PropertyOrField(propertyExpression, member);
            }

            return (MemberExpression)propertyExpression;
        }

        private static string GetStringFromJsonElement(object value)
        {
            if (value is JsonElement) return ((JsonElement)value).GetString()!;
            if (value is string) return (string)value;
            return value?.ToString() ?? string.Empty;
        }

        private static ConstantExpression GeValuetExpression(
            string field,
            object? value,
            Type propertyType)
        {
            if (value == null) return Expression.Constant(null, propertyType);

            if (propertyType.IsEnum)
            {
                string? stringEnum = GetStringFromJsonElement(value);

                if (!Enum.TryParse(propertyType, stringEnum, true, out object? valueparsed)) throw new ArgumentException(string.Format("Value {0} is not valid for {1}", value, field));

                return Expression.Constant(valueparsed, propertyType);
            }

            if (propertyType == typeof(Guid))
            {
                string? stringGuid = GetStringFromJsonElement(value);

                if (!Guid.TryParse(stringGuid, out Guid valueparsed)) throw new ArgumentException(string.Format("Value {0} is not valid for {1}", value, field));

                return Expression.Constant(valueparsed, propertyType);
            }

            if (propertyType == typeof(string))
            {
                string? text = GetStringFromJsonElement(value);

                return Expression.Constant(text, propertyType);
            }

            if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                string? text = GetStringFromJsonElement(value);
                return Expression.Constant(ChangeType(text, propertyType), propertyType);
            }

            return Expression.Constant(ChangeType(value, propertyType), propertyType);
        }

        public static dynamic? ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return Convert.ChangeType(value, t!);
        }

        private static AdvancedFilter GetValidFilter(AdvancedFilter filter)
        {
            if (string.IsNullOrEmpty(filter.Field)) throw new ArgumentException("The field attribute is required when declaring a filter");
            if (filter.Operator.IsNull()) throw new ArgumentException("The Operator attribute is required when declaring a filter");
            return filter;
        }

        public static IOrderedSpecificationBuilder<T> OrderBy<T>(
            this ISpecificationBuilder<T> specificationBuilder,
            string[]? orderByFields)
        {
            if (orderByFields is not null)
            {
                foreach (var field in ParseOrderBy(orderByFields))
                {
                    var paramExpr = Expression.Parameter(typeof(T));

                    Expression propertyExpr = paramExpr;
                    foreach (string member in field.Key.Split('.'))
                    {
                        propertyExpr = Expression.PropertyOrField(propertyExpr, member);
                    }

                    var keySelector = Expression.Lambda<Func<T, object?>>(
                        Expression.Convert(propertyExpr, typeof(object)),
                        paramExpr);

                    ((List<OrderExpressionInfo<T>>)specificationBuilder.Specification.OrderExpressions)
                        .Add(new OrderExpressionInfo<T>(keySelector, field.Value));
                }
            }

            return new OrderedSpecificationBuilder<T>(specificationBuilder.Specification);
        }

        private static Dictionary<string, OrderTypeEnum> ParseOrderBy(string[] orderByFields) =>
            new(orderByFields.Select((orderByfield, index) =>
            {
                string[] fieldParts = orderByfield.Split(' ');
                string field = fieldParts[0];
                bool descending = fieldParts.Length > 1 && fieldParts[1].StartsWith("Desc", StringComparison.OrdinalIgnoreCase);
                var orderBy = index == 0
                    ? descending ? OrderTypeEnum.OrderByDescending
                                    : OrderTypeEnum.OrderBy
                    : descending ? OrderTypeEnum.ThenByDescending
                                    : OrderTypeEnum.ThenBy;

                return new KeyValuePair<string, OrderTypeEnum>(field, orderBy);
            }));
    }
}
