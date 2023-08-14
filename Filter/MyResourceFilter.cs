using Microsoft.AspNetCore.Mvc.Filters;

namespace reviseAuthentication.Filter
{
    public class MyResourceFilter : IResourceFilter
    {
        /// <summary>
        /// after
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        /// <summary>
        /// before
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            
        }
    }
}
