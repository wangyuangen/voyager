using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 权限组接口
/// </summary>
[DataSeederOrder(20)]
public class PermissionGroupApi: BaseEntity
{
    /// <summary>
    /// 接口Id
    /// </summary>
    public Guid ApiId { get; set; }

    /// <summary>
    /// 接口
    /// </summary>
    public virtual ApiInfo Api { get; set; }

    /// <summary>
    /// 权限组Id
    /// </summary>
    public Guid PermissionGroupId { get; set; }

    /// <summary>
    /// 权限组
    /// </summary>
    public virtual PermissionGroup PermissionGroup { get; set; }
}
