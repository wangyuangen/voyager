using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.RegularExpressions;
using YK.DynamicApi;
using YK.DynamicApi.Attributes;
using YK.DynamicApi.OpenApi;
using YK.DynamicApi.Options;

namespace YK.DynamicApi;

public static class Extensions
{
    /// <summary>
    /// 注入swagger
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        var apiOptions = AppCore.GetConfig<OpenApiOptions>();

        if (apiOptions.Swagger.Enable)
        {
            services.AddSwaggerGen(options =>
            {
                apiOptions.Swagger.Projects.ForEach(project =>
                {
                    options.SwaggerDoc(project.Code.ToLower(), new OpenApiInfo
                    {
                        Title = project.Name,
                        Version = project.Version,
                        Description = project.Description
                    });
                });

                options.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    var api = controllerAction?.AttributeRouteInfo?.Template ?? string.Empty;
                    api = Regex.Replace(api, @"[\{\\\/\}]", "-") + "-" + apiDesc?.HttpMethod?.ToLower();
                    return api.Replace("--", "-");
                });

                options.ResolveConflictingActions(apiDescription => apiDescription.First());

                string DefaultSchemaIdSelector(Type modelType)
                {
                    var modelName = modelType.Name;
                    if (apiOptions.Swagger.EnableSchemaIdNamespace)
                    {
                        var nameSpaceList = apiOptions.Swagger.AssemblyNameList;
                        if (nameSpaceList.Length > 0)
                        {
                            var nameSpace = modelType.Namespace ?? string.Empty;
                            if (nameSpaceList.Any(a => nameSpace.Contains(a)))
                            {
                                modelName = modelType.FullName;
                            }
                        }
                        else
                        {
                            modelName = modelType.FullName;
                        }
                    }

                    if (modelType.IsConstructedGenericType)
                    {
                        var prefix = modelType.GetGenericArguments()
                        .Select(DefaultSchemaIdSelector)
                        .Aggregate((previous, current) => previous + current);

                        modelName = modelName?.Split('`').First() + prefix;
                    }
                    else
                    {
                        modelName = modelName?.Replace("[]", "Array");
                    }

                    if (modelType.IsDefined(typeof(SchemaIdAttribute)))
                    {
                        var swaggerSchemaIdAttribute = modelType.GetCustomAttribute<SchemaIdAttribute>(false);
                        if (swaggerSchemaIdAttribute?.SchemaId.IsNullOrEmpty() is false)
                        {
                            return swaggerSchemaIdAttribute.SchemaId;
                        }
                        else
                        {
                            return swaggerSchemaIdAttribute?.Prefix + modelName + swaggerSchemaIdAttribute?.Suffix;
                        }
                    }

                    return modelName ?? string.Empty;
                }

                options.CustomSchemaIds(modelType => DefaultSchemaIdSelector(modelType));

                //支持多分组
                options.DocInclusionPredicate((docName, apiDescription) =>
                {
                    var nonGroup = false;
                    var groupNames = new List<string>();
                    var dynamicApiAttribute = apiDescription.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is DynamicApiAttribute);
                    if (dynamicApiAttribute != null)
                    {
                        var dynamicApi = dynamicApiAttribute as DynamicApiAttribute;
                        if (dynamicApi?.GroupNames?.Length > 0)
                        {
                            groupNames.AddRange(dynamicApi.GroupNames);
                        }
                    }

                    var apiGroupAttribute = apiDescription.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is ApiGroupAttribute);
                    if (apiGroupAttribute != null)
                    {
                        var apiGroup = apiGroupAttribute as ApiGroupAttribute;
                        if (apiGroup?.GroupNames?.Length > 0)
                        {
                            groupNames.AddRange(apiGroup.GroupNames);
                        }
                        nonGroup = apiGroup?.NonGroup ?? true;
                    }

                    return docName == apiDescription.GroupName || groupNames.Any(a => a == docName) || nonGroup;
                });

                string[] xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
                if (xmlFiles.Length > 0)
                {
                    foreach (var xmlFile in xmlFiles)
                    {
                        options.IncludeXmlComments(xmlFile, true);
                    }
                }

                var server = new OpenApiServer()
                {
                    Url = apiOptions.Swagger.Url,
                    Description = ""
                };
                if (apiOptions.Swagger.Footer.Enable)
                {
                    server.Extensions.Add("extensions", new OpenApiObject
                    {
                        ["copyright"] = new OpenApiString(apiOptions.Swagger.Footer.Content)
                    });
                }
                options.AddServer(server);

                if (apiOptions.Swagger.EnableEnumSchemaFilter)
                {
                    options.SchemaFilter<EnumSchemaFilter>();
                }
                if (apiOptions.Swagger.EnableOrderTagsDocumentFilter)
                {
                    options.DocumentFilter<OrderTagsDocumentFilter>();
                }
                options.OrderActionsBy(apiDesc =>
                {
                    var order = 0;
                    var objOrderAttribute = apiDesc.CustomAttributes().FirstOrDefault(x => x is OrderAttribute);
                    if (objOrderAttribute != null)
                    {
                        var orderAttribute = objOrderAttribute as OrderAttribute;
                        order = orderAttribute?.Value ?? 0;
                    }
                    return (int.MaxValue - order).ToString().PadLeft(int.MaxValue.ToString().Length, '0');
                });

                #region 添加设置Token的按钮

                //添加Jwt验证设置
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new List<string>()
                        }
                    });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Value: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                #endregion 添加设置Token的按钮
            });

            services.AddScoped<SwaggerGenerator>();
        }
        return services;
    }

    /// <summary>
    /// 配置Swagger
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    private static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
    {
        var apiOptions = AppCore.GetConfig<OpenApiOptions>();

        if (apiOptions.Swagger.Enable)
        {
            var routePrefix = apiOptions.ApiUI.RoutePrefix;
            if (!apiOptions.ApiUI.Enable && routePrefix.IsNullOrEmpty())
            {
                routePrefix = apiOptions.Swagger.RoutePrefix;
            }
            app.UseSwagger(optoins =>
            {
                optoins.RouteTemplate = routePrefix + (optoins.RouteTemplate.StartsWith("/") ? "" : "/") + optoins.RouteTemplate;
            });
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = apiOptions.Swagger.RoutePrefix;
                apiOptions.Swagger.Projects.ForEach(project =>
                {
                    options.SwaggerEndpoint($"/{routePrefix}/swagger/{project.Code.ToLower()}/swagger.json", project.Name);
                });

                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);//折叠Api
                //options.DefaultModelsExpandDepth(-1);//不显示Models
                if (apiOptions.MiniProfiler)
                {
                    options.InjectJavascript("/swagger/mini-profiler.js?v=4.2.22+2.0");
                    options.InjectStylesheet("/swagger/mini-profiler.css?v=4.2.22+2.0");
                }
            });
        }
        return app;
    }

    /// <summary>
    /// 配置开放接口
    /// </summary>
    /// <param name="app"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    private static IApplicationBuilder UseOpenApi(this IApplicationBuilder app)
    {
        var apiOptions = AppCore.GetConfig<OpenApiOptions>();

        if (apiOptions.MiniProfiler) app.UseMiniProfiler();

        app.UseSwagger();

        return app;
    }

    /// <summary>
    /// 注册开放接口
    /// </summary>
    /// <param name="sercice"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static ICoreServiceBuider AddOpenApi(this ICoreServiceBuider serviceBuider)
    {
        var appOptions = AppCore.GetConfig<AppOptions>();
        var apiOptions = AppCore.GetConfig<OpenApiOptions>();

        serviceBuider.Services.AddSwagger();

        if (apiOptions.MiniProfiler) serviceBuider.Services.AddMiniProfiler();

        serviceBuider.Services.AddDynamicApi(options =>
        {
            var assemblies = Reflections.GetAssemblyList(appOptions.Modules);
            options.FormatResult = appOptions.FormatResult ;
            options.FormatResultType = typeof(ResultOutput<>);
            assemblies.ForEachItem(assembly =>
            {
                options.AddAssemblyOptions(assembly);
            });
        });

        serviceBuider.AppBuilder += app => app.UseOpenApi();

        return serviceBuider;
    }
}
