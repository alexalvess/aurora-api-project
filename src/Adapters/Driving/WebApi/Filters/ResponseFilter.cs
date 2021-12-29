using Application.Envelop;
using Application.Extensions;
using Application.Ports.NotificationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
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
    private readonly Application.Envelop.Queryable _queryable;

    public ResponseFilter(INotificationContext notificationContext, Application.Envelop.Queryable queryable)
        => (_notificationContext, _queryable) = (notificationContext, queryable);

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
        else
        {
            //var result = _queryable.SelectFields(((ObjectResult)context.Result).Value);
            //if (result != default)
            //    context.Result = new ObjectResult(result);

            if(_queryable.Wrap)
                context.Result = new ObjectResult(new Response(((ObjectResult)context.Result).Value));
        }
    }
}