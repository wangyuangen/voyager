using YK.Core.Enums;
using YK.Core.Exceptions;
using YK.Core.Extensions;

namespace YK.Core.Contract;

/// <summary>
/// 结果输出
/// </summary>
public class ResultOutput<T>
{
    /// <summary>
    /// 是否成功标记
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public string? Msg { get; set; }

    /// <summary>
    /// 数据
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// 成功
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="msg">消息</param>
    public ResultOutput<T> Ok(T? data, string? msg = null)
    {
        Success = true;
        Data = data;
        Msg = msg;
        Code = HttpStatusCode.Status200Ok.Value();
        return this;
    }

    /// <summary>
    /// 失败
    /// </summary>
    /// <param name="msg">消息</param>
    /// <param name="data">数据</param>
    /// <returns></returns>
    public ResultOutput<T> NotOk(string? msg = null, T? data = default)
    {
        Success = false;
        Msg = msg;
        Data = data;
        Code = HttpStatusCode.Status0NotOk.Value();
        return this;
    }
}

/// <summary>
/// 静态结果输出
/// </summary>
public static class ResultOutput
{
    /// <summary>
    /// 成功
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="msg">消息</param>
    /// <returns></returns>
    public static ResultOutput<T> Ok<T>(T? data = default(T), string? msg = null)
    {
        return new ResultOutput<T>().Ok(data, msg);
    }

    /// <summary>
    /// 成功
    /// </summary>
    /// <returns></returns>
    public static ResultOutput<string> Ok()
    {
        return Ok<string>();
    }

    /// <summary>
    /// 失败
    /// </summary>
    /// <param name="msg">消息</param>
    /// <param name="data">数据</param>
    /// <returns></returns>
    public static ResultOutput<T> NotOk<T>(string? msg = null, T? data = default)
    {
        return new ResultOutput<T>().NotOk(msg, data);
    }

    /// <summary>
    /// 失败
    /// </summary>
    /// <param name="msg">消息</param>
    /// <returns></returns>
    public static ResultOutput<string> NotOk(string? msg = null)
    {
        return new ResultOutput<string>().NotOk(msg);
    }

    /// <summary>
    /// 系统异常
    /// </summary>
    /// <param name="msg">消息</param>
    /// <param name="appCode">编码</param>
    /// <param name="statusCode">状态编码</param>
    /// <returns></returns>
    public static AppException Exception(string? msg = null, int? appCode = null, int statusCode = (int)HttpStatusCode.Status500InternalServerError)
    {
        return new AppException(msg, appCode, statusCode);
    }

    /// <summary>
    /// 根据布尔值返回结果
    /// </summary>
    /// <param name="success"></param>
    /// <returns></returns>
    public static ResultOutput<T> Result<T>(bool success)
    {
        return success ? Ok<T>() : NotOk<T>();
    }

    /// <summary>
    /// 根据布尔值返回结果
    /// </summary>
    /// <param name="success"></param>
    /// <returns></returns>
    public static ResultOutput<string> Result(bool success)
    {
        return success ? Ok() : NotOk();
    }
}
