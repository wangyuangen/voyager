using Microsoft.AspNetCore.Mvc;

namespace YK.Host.Controlers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns></returns>
        public string HealthCheck()
        {
            return "Success";
        }
    }
}
