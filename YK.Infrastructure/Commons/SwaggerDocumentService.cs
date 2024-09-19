using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using YK.Core.Commons.Models;

namespace YK.Infrastructure.Commons;

/// <summary>
/// swagger接口文档服务
/// </summary>
public class SwaggerDocumentService(
    SwaggerGeneratorOptions swaggerOption,
    SwaggerGenerator swaggerGenerator
    ) : ISwaggerDocumentService
{
    private readonly SwaggerGeneratorOptions _swaggerOption = swaggerOption;
    private readonly SwaggerGenerator _swaggerGenerator = swaggerGenerator;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<List<SwaggerApiInfoDto>> GetAllApiInfoAsync()
    {
        SwaggerApiInfoDto? group, category, api;
        List<SwaggerApiInfoDto> allApis = new();
        var docs = _swaggerOption.SwaggerDocs;

        if (docs.Any())
        {
            string categoryPath;
            OpenApiDocument document;
            foreach (var doc in docs)
            {
                group = new SwaggerApiInfoDto
                {
                    Name = doc.Value.Title,
                    Path = doc.Key,
                    Remark = doc.Value.Description,
                };
                group.Code = GetCode(group.Path, group.HttpMethod);
                allApis.Add(group);

                document = await _swaggerGenerator.GetSwaggerAsync(doc.Key);

                if (document.Paths.Any())
                {
                    document.Paths.ForEachItem(path =>
                    {
                        if (path.Value.Operations.Any())
                        {
                            path.Value.Operations.ForEachItem(operation =>
                            {
                                categoryPath = operation.Value.Tags.First()?.Name ?? "";
                                category = allApis.FirstOrDefault(x => x.Path == categoryPath);
                                if (category == null)
                                {
                                    category = new SwaggerApiInfoDto
                                    {
                                        Name = categoryPath,
                                        Path = categoryPath,
                                        ParentPath = group.Path
                                    };
                                    category.Code = GetCode(category.Path, category.HttpMethod);
                                    allApis.Add(category);
                                }
                                api = new SwaggerApiInfoDto
                                {
                                    HttpMethod = operation.Key.ToString(),
                                    Name = operation.Value.Summary,
                                    Path = path.Key,
                                    Remark = string.IsNullOrEmpty(operation.Value.Description) ? operation.Value.Summary : operation.Value.Description,
                                    ParentPath = category.Path,
                                };
                                api.Code = GetCode(api.Path, api.HttpMethod);
                                allApis.Add(api);
                            });
                        }
                    });
                }
            }
        }
        return allApis;
    }

    private string GetCode(string? path, string? method) 
        => string.Join(":", path?.Replace("/", ":"), method?.ToLower()).Trim(':');
}
