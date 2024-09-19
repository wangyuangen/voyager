using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using YK.Console.Core.Consts;
using YK.Console.Core.DbContext;
using YK.Core.Attributes;
using YK.Core.Commons.Tools;
using YK.Core.Extensions;
using YK.Core.Options;
using YK.Module.Core.Models;

namespace YK.Console;


#if DEBUG
/// <summary>
/// Console工具服务
/// </summary>
[DynamicApi(Area = ConsoleAppConsts.ApiGroup), Order(100)]
public class ToolService(ConsoleDbContext _consoledb, ISerializerService _serializer) : IDynamicApi
{
    /// <summary>
    /// 导出种子数据
    /// </summary>
    /// <param name="entityType"></param>
    [AllowEveryUser]
    public void ExportSeederData([Required] string entityType)
    {
        var set = typeof(ToolService).GetMethod(nameof(ExportDataToJson), BindingFlags.Instance | BindingFlags.NonPublic);

        var type = _consoledb.Model.FindEntityType(entityType);
        if (type == null) throw ResultOutput.Exception($"实体{entityType}不存在");

        var data = set?.MakeGenericMethod(type.ClrType).Invoke(this, null);
    }

    /// <summary>
    /// 获取当前数据库实体类型
    /// </summary>
    /// <returns></returns>
    [AllowEveryUser]
    public IEnumerable<string?> CurrentDbEntityTypes()
    {
        return _consoledb.Model.GetEntityTypes()
            .Where(e => e.BaseType is null && e.ClrType.GetInterface(typeof(IEntity).Name) is not null)
            .Select(e => e.ClrType.FullName);
    }

    /// <summary>
    /// 获得枚举列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public List<EnumsOutput> AllEnums()
    {
        var enums = new List<EnumsOutput>();

        var appOptions = AppCore.GetConfig<AppOptions>();
        var assemblyNames = appOptions.Modules;
        if (!(assemblyNames?.Length > 0))
        {
            return enums;
        }

        foreach (var assemblyName in assemblyNames)
        {
            var assembly = Assembly.Load(assemblyName);
            var enumTypes = assembly.GetTypes().Where(m => m.IsEnum);
            foreach (var enumType in enumTypes)
            {
                var summaryList = EnumOperation.GetEnumSummaryList(enumType);

                var enumDescriptor = new EnumsOutput
                {
                    Name = enumType.Name,
                    Desc = Reflections.GetDescription(enumType) ?? (summaryList.TryGetValue("", out var comment) ? comment : ""),
                    Options = Enum.GetValues(enumType).Cast<Enum>().Select(x => new EnumsOutput.Models.Options
                    {
                        Name = x.ToString(),
                        Desc = x.Description() ?? (summaryList.TryGetValue(x.ToString(), out var comment) ? comment : ""),
                        Value = x.Value(),
                        Theme = x.GetTheme() ?? string.Empty
                    }).ToList()
                };

                enums.Add(enumDescriptor);
            }
        }

        return enums;
    }



    [NonAction]
    private void ExportDataToJson<TEntity>()
        where TEntity : class, IEntity
    {
        _consoledb.IgnoreTenantFilter = true;

        var datas = _consoledb.Set<TEntity>().ToList();

        string directory = Path.Combine(Directory.GetCurrentDirectory(), ConsoleAppConsts.SeederDataRootPath);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        string filePath = Path.Combine(directory, $"{typeof(TEntity).Name}.json");
        string jsonContent = _serializer.Serialize(datas);

        if (File.Exists(filePath)) File.Delete(filePath);

        File.WriteAllText(filePath, jsonContent);
    }
}

#endif