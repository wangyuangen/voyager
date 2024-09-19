using Microsoft.Extensions.Configuration;
using YK.Core.Contract;

namespace YK.Core.Extensions;

public static partial class Extensions
{
    /// <summary>
    /// 获取配置节点
    /// </summary>
    /// <typeparam name="IOption"></typeparam>
    /// <param name="configuration"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static IOption GetOptions<IOption>(this IConfiguration configuration, string key = "")
    {
        if (key.IsNullOrEmpty()) key = typeof(IOption).Name;
        var option = configuration.GetSection(key).Get<IOption>();
        if (option == null) throw ResultOutput.Exception($"{key} option not found");
        return option;
    }
}
