using YK.Console.Business.PermissionGroupApis;

namespace YK.Console.Business.PermissionGroups;

public class PermissionGroupOutput: BaseOutput
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 数权范围
    /// </summary>
    public DataPermissionScope Scope { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public EnabledStatusEnum Enabled { get; set; }

    /// <summary>
    /// 父级
    /// </summary>
    public Guid? ParentId { get; set; }
}

/// <summary>
/// 权限组接口
/// </summary>
public class PermissionGroupWithApiOutput:PermissionGroupOutput
{
    /// <summary>
    /// 权限组接口列表
    /// </summary>
    public virtual List<PermissionGroupApiOutput> PermissionGroupApis { get; set; }
}