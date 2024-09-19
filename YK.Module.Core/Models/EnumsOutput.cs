namespace YK.Module.Core.Models;

/// <summary>
/// 枚举输出
/// </summary>
public class EnumsOutput
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Desc { get; set; }

    /// <summary>
    /// 选项列表
    /// </summary>
    public List<Models.Options> Options { get; set; }

    public static class Models
    {
        /// <summary>
        /// 选项
        /// </summary>
        public class Options
        {
            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 描述
            /// </summary>
            public string Desc { get; set; }

            /// <summary>
            /// 值
            /// </summary>
            public int Value { get; set; }

            /// <summary>
            /// 主题样式
            /// </summary>
            public string Theme { get; set; }
        }
    }
}
