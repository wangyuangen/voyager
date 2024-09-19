using YK.Console.Business.ApiInfos;

namespace YK.Console.Business.PermissionGroupApis;

/// <summary>
/// 接口数权范围
/// </summary>
public class ApiPermissionDataScope
{
    /// <summary>
    /// 数权范围
    /// </summary>
    public DataPermissionScope Scope { get; set; }

    /// <summary>
    /// Http请求方法
    /// </summary>
    public string HttpMethod { get; set; }
    
    /// <summary>
    /// 接口地址
    /// </summary>
    public string ApiPath { get; set; }

    /// <summary>
    /// 接口编码
    /// </summary>
    public string Code { get; set; }

    public ApiPermissionDataScope(DataPermissionScope scope, string httpMethod, string apiPath,string code)
    {
        Scope = scope;
        HttpMethod = httpMethod;
        ApiPath = apiPath;
        Code = code;
    }
}

/// <summary>
/// 权限组接口
/// </summary>
public class PermissionGroupApiOutput:BaseOutput
{
    /// <summary>
    /// 接口Id
    /// </summary>
    public Guid ApiId { get; set; }

    /// <summary>
    /// 接口
    /// </summary>
    public virtual ApiInfoOutput Api { get; set; }

    /// <summary>
    /// 权限组Id
    /// </summary>
    public Guid PermissionGroupId { get; set; }
}