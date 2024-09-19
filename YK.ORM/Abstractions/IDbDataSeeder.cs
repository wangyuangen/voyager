namespace YK.ORM.Abstractions;

/// <summary>
/// 初始化数据
/// </summary>
public interface IDbDataSeeder:IScopedService
{
    Task SeederAsync(CancellationToken cancellationToken);
}
