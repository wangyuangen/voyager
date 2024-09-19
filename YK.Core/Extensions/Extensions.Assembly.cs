using System.Reflection;
using System.Runtime.CompilerServices;
using YK.Core.Commons;

namespace YK.Core.Extensions;

public partial class Extensions
{
    /// <summary>
    /// 获取当前程序集与依赖的程序集
    /// </summary>
    /// <param name="currentAssembly"></param>
    /// <param name="assemblyNames"></param>
    /// <returns></returns>
    public static Assembly[] GetCurrentWithReferenceAssembly(this Assembly currentAssembly, string[]? assemblyNames)
    {
        var allAssemblies = Reflections.GetAssemblyList(assemblyNames);

        var assemblies = currentAssembly
            .GetReferencedAssemblies()
            .Select(x => Assembly.Load(x))
            .Where(x => allAssemblies.Contains(x))
            .Append(currentAssembly);

        return assemblies.ToArray();
    }
   
    public static Type GetReturnType(this MethodInfo method)
    {
        var isAsync = method.IsAsync();
        var returnType = method.ReturnType;
        return isAsync ? (returnType.GenericTypeArguments.FirstOrDefault() ?? typeof(void)) : returnType;
    }

    /// <summary>
    /// 获取当前成员唯一特性
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="memberInfo"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TAttribute? GetSingleAttributeOrNull<TAttribute>(this MemberInfo memberInfo, bool inherit = true)
        where TAttribute : Attribute
    {
        if (memberInfo == null)
        {
            throw new ArgumentNullException(nameof(memberInfo));
        }

        var attrs = memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).ToArray();
        if (attrs.Length > 0)
        {
            return (TAttribute)attrs[0];
        }

        return default;
    }

    public static bool IsNotAbstractClass(this Type type, bool publicOnly)
    {
        if (type.IsSpecialName)
            return false;

        if (type.IsClass && !type.IsAbstract)
        {
            if (type.HasAttribute<CompilerGeneratedAttribute>())
                return false;

            if (publicOnly)
                return type.IsPublic || type.IsNestedPublic;

            return true;
        }
        return false;
    }

    /// <summary>
    /// 是否为异步方法
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public static bool IsAsync(this MethodInfo method)
        => method.ReturnType == typeof(Task) || (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>));

    /// <summary>
    /// 类型是否包含某特性
    /// </summary>
    /// <param name="type"></param>
    /// <param name="attributeType"></param>
    /// <returns></returns>
    public static bool HasAttribute(this Type type, Type attributeType) 
        => type.IsDefined(attributeType, inherit: true);

    /// <summary>
    /// 类型是否包含某特性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool HasAttribute<T>(this Type type) 
        where T : Attribute
        => type.HasAttribute(typeof(T));

    /// <summary>
    /// 类型是否包含某特性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool HasAttribute<T>(this Type type, Func<T, bool> predicate)
        where T : Attribute 
        => type.GetCustomAttributes<T>(inherit: true).Any(predicate);

    /// <summary>
    /// 获取类型中包含某特性的属性
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<PropertyInfo> GetPropertiesByAttribures<TAttribute>(this Type type)
        where TAttribute : Attribute
        => type.GetProperties().Where(x => x.CustomAttributes.Any(x => x.AttributeType == typeof(TAttribute))).ToList();
}
