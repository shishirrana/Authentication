using Microsoft.AspNetCore.Mvc.Filters;

namespace reviseAuthentication.Filter
{
    public class MyActionFilter : IActionFilter
    {
        /// <summary>
        /// before
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        /// <summary>
        /// after
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
