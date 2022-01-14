using Application.Envelop;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class QueryableFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var temp = context.ActionArguments.TryGetValue("id", out var id);

            base.OnActionExecuting(context);
        }
    }
}
