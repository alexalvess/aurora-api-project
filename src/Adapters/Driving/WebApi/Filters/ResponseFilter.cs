using Application.Envelop;
using Application.Ports.NotificationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace WebApi.Filters;

public class ResponseFilter : IResultFilter
{
    private readonly INotificationContext _notificationContext;

    public ResponseFilter(INotificationContext notificationContext)
        => _notificationContext = notificationContext;

    public void OnResultExecuted(ResultExecutedContext context)
    {
        // Do nothing
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (_notificationContext.HasNotifications)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = "application/json; charset=utf-8";

            var contentResponse = new Response(_notificationContext.Notifications.Select(notification => notification.ErrorMessage).ToList());
            var serializeOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            context.Result = new JsonResult(contentResponse, serializeOptions);
        }
    }
}