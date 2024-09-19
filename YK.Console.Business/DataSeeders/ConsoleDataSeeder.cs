using Microsoft.EntityFrameworkCore;
using System.Reflection;
using YK.Console.Core.Consts;
using YK.Console.Core.DbContext;
using YK.Core.Commons.Tools;
using YK.ORM.Attributes;

namespace YK.Console.Business.DataSeeders;

/// <summary>
/// Console种子数据初始化
/// </summary>
public class ConsoleDataSeeder(ILogger<ConsoleDataSeeder> _logger, ISerializerService _serializable, ConsoleDbContext _consoledb) : IDbDataSeeder
{
    public async Task SeederAsync(CancellationToken cancellationToken)
    {
        var entitytypes = _consoledb.Model.GetEntityTypes()
            .Where(e => e.BaseType is null && e.ClrType.GetInterface(typeof(IEntity).Name) is not null)
            .Select(e => new Tuple<Type, int>(e.ClrType, e.ClrType.GetCustomAttribute<DataSeederOrderAttribute>()?.Order ?? 0))
            .OrderBy(x=>x.Item2).ToList();

        var set = typeof(ConsoleDataSeeder).GetMethod(nameof(UploadDataFromJson), BindingFlags.Instance | BindingFlags.NonPublic);

        foreach (var entitytype in entitytypes)
        {
            set?.MakeGenericMethod(entitytype.Item1).Invoke(this, null);
        }

        await InitDefaultDataDict();

        _logger.LogInformation("{seeder} succeed finished.", GetType().Name);
    }

    private void UploadDataFromJson<TEntity>()
        where TEntity : BaseEntity
    {
        //忽略软删除
        _consoledb.IgnoreDeleteFilter = true;
        //忽略租户
        _consoledb.IgnoreTenantFilter = true;

        var context = _consoledb.Set<TEntity>();

        string filePath = Path.Combine(AppContext.BaseDirectory, ConsoleAppConsts.SeederDataRootPath, $"{typeof(TEntity).Name}.json");
        if (!File.Exists(filePath)) return;

        using var streamReader = new StreamReader(filePath);
        var jsonContent = streamReader.ReadToEnd();

        if (jsonContent.IsNullOrEmpty()) return;

        var datas = _serializable.Deserialize<List<TEntity>>(jsonContent);

        if (!datas.Any()) return;

        var dataIds = datas
            .Select(x => x.Id)
            .ToList();

        var existIds = context
            .AsNoTracking()
            .Where(x => dataIds.Contains(x.Id))
            .Select(x => x.Id)
            .ToList();

        dataIds = dataIds.Except(existIds).ToList();

        datas = datas.Where(x => dataIds.Contains(x.Id)).ToList();

        if (!datas.Any()) return;

        context.AddRange(datas);

        _consoledb.SaveChanges();

        _logger.LogInformation("{entity} seeder succeed.", typeof(TEntity).Name);
    }

    /// <summary>
    /// 初始化默认字典
    /// </summary>
    /// <returns></returns>
    private async Task InitDefaultDataDict()
    {
        var enumType = typeof(DefaultDataDictEnum);

        var summaryList = EnumOperation.GetEnumSummaryList(enumType);

        var dicts = Enum.GetValues(enumType).Cast<Enum>().Select(x => new EnumsOutput.Models.Options
        {
            Name = x.ToString(),
            Desc = x.Description() ?? (summaryList.TryGetValue(x.ToString(), out var comment) ? comment : ""),
            Value = x.Value(),
            Theme = x.GetTheme() ?? string.Empty
        }).ToList();

        var defaultDics = dicts.Select(x => new DataDictInfo
        {
            Code = x.Name,
            Name = x.Desc,
            Enabled = EnabledStatusEnum.Enabled,
            Remark = x.Desc,
            ThemeStyle = x.Theme,
            Sort = x.Value,
        }).ToList();

        if (!defaultDics.IsNullOrEmpty())
        {
            var codes = defaultDics.Select(x => x.Code).ToList();

            var existCodes = await _consoledb.Set<DataDictInfo>().AsNoTracking()
                .Where(x => codes.Contains(x.Code))
                .Select(x => x.Code).ToListAsync();

            codes = codes.Except(existCodes).ToList();

            if (!codes.IsNullOrEmpty())
            {
                defaultDics = defaultDics.Where(x => codes.Contains(x.Code)).ToList();

                _consoledb.Set<DataDictInfo>().AddRange(defaultDics);

                await _consoledb.SaveChangesAsync();
            }
        }

        _logger.LogInformation("default datadict seeder succeed.");
    }
}
