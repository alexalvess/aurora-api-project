using Application.Envelop;
using Application.Ports.NotificationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;

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
        object contentResponse = default;

        if (_notificationContext.HasNotifications)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = "application/json; charset=utf-8";

            contentResponse = _notificationContext.Notifications.Select(notification => new 
            {
                code = notification.ErrorCode,
                field = notification.PropertyName,
                message = notification.ErrorMessage
            }).ToList();
        }

        if (Convert.ToBoolean(context.HttpContext.Request.Query["wrap"]))
            context.Result = new JsonResult(new Response(((ObjectResult)context.Result).Value));
        else
            context.Result = new JsonResult(contentResponse);
    }
}