using Microsoft.EntityFrameworkCore;
using System.Reflection;
using YK.Console.Core.Consts;
using YK.Console.Core.DbContext;
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
}
