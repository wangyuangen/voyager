namespace YK.Console.Business.PostInfos;

public class PostInfoInput
{
    /// <summary>
    /// 岗位名称
    /// </summary>
    [Required, MaxLength(64)]
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [Required, MaxLength(512)]
    public string? Remark { get; set; }
}
