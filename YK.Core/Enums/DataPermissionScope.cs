using System.ComponentModel;

namespace YK.Core.Enums;

/// <summary>
/// 数权范围
/// </summary>
[Description("数权范围")]
public enum DataPermissionScope
{
    /// <summary>
    /// 全部
    /// </summary>
    [Description("全部")]
    All = 1,

    /// <summary>
    /// 本部门和下级部门以及附属部门
    /// </summary>
    [Description("本部门加下级部门加附属部门")]
    CurrentWithChildAndAffiliateDept = 2,

    /// <summary>
    /// 本部门和附属部门
    /// </summary>
    [Description("本部门加附属部门")]
    CurrentWithAffiliateDept = 3,

    /// <summary>
    /// 本部门和下级部门
    /// </summary>
    [Description("本部门加下级部门")]
    CurrentWithChildDept = 4,

    /// <summary>
    /// 当前部门
    /// </summary>
    [Description("本部门")]
    CurrentDept = 5,

    /// <summary>
    /// 当前员工
    /// </summary>
    [Description("当前员工")]
    CurrentUserStaff = 6
}
