using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;

namespace YK.Infrastructure.Filters;

/// <summary>
/// 输入校验过滤器
/// </summary>
public class InputValidateFilter : IAsyncActionFilter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            try
            {
                //验证失败
                var errors = context.ModelState
                    .Where(x => x.Value?.ValidationState == ModelValidationState.Invalid)
                    .Select(x =>
                    {
                        var sb = new StringBuilder();
                        sb.AppendFormat("{0}：", x.Key);
                        sb.Append(x.Value?.Errors.Select(n => n.ErrorMessage).Aggregate((x, y) => $"{x};{y}"));
                        return sb.ToString();
                    }).Aggregate((x, y) => $"{x}|{y}");
                Log.Error(errors);
                context.Result = new JsonResult(ResultOutput.NotOk(errors));
            }
            catch
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return;
        }
        await next();
    }
}
