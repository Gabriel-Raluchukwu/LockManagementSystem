using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LockManagementSystem.Application.Attributes;

[AttributeUsage(validOn: AttributeTargets.Method)]
public class LogEventAuthAttribute : Attribute, IAsyncActionFilter
{
    private const string ApiKeyName = "ApiKey";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyName, out var headerValue) || string.IsNullOrWhiteSpace(headerValue))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

        var apiKey = appSettings.GetValue<string>(ApiKeyName);

        if (!apiKey.Equals(headerValue, StringComparison.Ordinal))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        await next();
    }
}