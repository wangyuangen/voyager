using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 岗位
/// </summary>
[DataSeederOrder(10)]
public class PostInfo : BaseTenantSoftDeleteEntity
{
    /// <summary>
    /// 岗位名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual List<UserStaffInfo> UserStaffInfos { get; set; }

    public PostInfo Update(string? name= null, string? remark = null)
    {
        if (name is not null && Name?.Equals(name) is not true)
            Name = name;
        if (remark is not null && Remark?.Equals(remark) is not true)
            Remark = remark;
        return this;
    }
}
