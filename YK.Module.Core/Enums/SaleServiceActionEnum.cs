using System.ComponentModel;

namespace YK.Module.Core.Enums;

/// <summary>
/// 销售服务过程动作
/// </summary>
[Description("销售服务过程动作")]
public enum SaleServiceActionEnum
{
    /// <summary>
    /// 回访跟进
    /// </summary>
    [Description("回访跟进")]
    VisitTrack,

    /// <summary>
    /// 文件上传
    /// </summary>
    [Description("文件上传")]
    FileUpload,

    /// <summary>
    /// 参数确认
    /// </summary>
    [Description("参数确认")]
    ParamConfirm,

    /// <summary>
    /// 预算报价
    /// </summary>
    [Description("预算报价")]
    Quotation,

    /// <summary>
    /// 合同签署
    /// </summary>
    [Description("合同签署")]
    SignContract
}
