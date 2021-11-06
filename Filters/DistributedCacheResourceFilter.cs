using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;

namespace utils_lib.Filters
{
    public class DistributedCacheResourceFilter : System.Attribute, IResourceFilter
    {
        private readonly IDistributedCache _distributedCache;

        public DistributedCacheResourceFilter(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (context.HttpContext.Request.Method.ToLower() != "get")return;

            var cacheKey = DistributedCacheUtils.GetKey(context.HttpContext);

            var bytes = _distributedCache.Get(cacheKey);

            if (bytes == null) return;
            var byteArrayToObject = DistributedCacheUtils.ByteArrayToObject<object>(bytes);

            context.Result = new JsonResult(byteArrayToObject);
        }
    }
}