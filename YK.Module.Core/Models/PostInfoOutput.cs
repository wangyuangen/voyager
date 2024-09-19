using YK.Module.Core.Contracts;

namespace YK.Module.Core.Models;

/// <summary>
/// 岗位输出
/// </summary>
public class PostInfoOutput : AuditableOutput
{
    /// <summary>
    /// 岗位名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Remark { get; set; }
}
