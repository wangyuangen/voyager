namespace YK.ORM.Abstractions;

/// <summary>
/// 数据库结构初始化
/// </summary>
public interface IDbInitializer:IScopedService
{
    Task MigrateAsync(CancellationToken cancellationToken);
}
