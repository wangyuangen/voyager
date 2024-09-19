using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using YK.Core.Extensions;

namespace YK.Core.Commons.Tools
{
    public class IPAddressTool
    {
        /// <summary>
        /// 获取请求ip地址
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tryUseXForwardHeader"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetRequestIP(HttpContext context, bool tryUseXForwardHeader = true)
        {
            string? ip = null;

            if (tryUseXForwardHeader)
                ip = GetHeaderValueAs<string>(context, "X-Forwarded-For")?.SplitCsv()?.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(ip))
                ip = GetHeaderValueAs<string>(context, "REMOTE_ADDR");

            if (string.IsNullOrWhiteSpace(ip) && context?.Connection?.RemoteIpAddress != null)
            {
                var remoteIp = context.Connection.RemoteIpAddress;
                if (remoteIp.IsIPv4MappedToIPv6)
                {
                    ip = remoteIp.MapToIPv4().ToString();
                }
                if (string.IsNullOrEmpty(ip) && System.Net.IPAddress.IsLoopback(remoteIp))
                {
                    return "127.0.0.1";
                }
            }

            if (string.IsNullOrWhiteSpace(ip))
                throw new Exception("Unable to determine caller's IP.");

            return ip;
        }

        public static T? GetHeaderValueAs<T>(HttpContext context, string headerName)
        {
            StringValues values;

            if (context.Request?.Headers?.TryGetValue(headerName, out values) ?? false)
            {
                string rawValues = values.ToString();   // writes out as Csv when there are multiple.

                if (!string.IsNullOrEmpty(rawValues))
                    return (T)Convert.ChangeType(values.ToString(), typeof(T));
            }
            return default;
        }
    }
}
