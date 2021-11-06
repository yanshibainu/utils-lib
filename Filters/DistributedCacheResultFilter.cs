using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;

namespace utils_lib.Filters
{
    public class DistributedCacheResultFilter : Attribute, IResultFilter
    {
        private readonly IDistributedCache _distributedCache;

        public DistributedCacheResultFilter(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.HttpContext.Request.Method.ToLower() != "get")return;
            if (!(context.Result is OkObjectResult result)) return;

            var cacheKey = DistributedCacheUtils.GetKey(context.HttpContext);

            if (_distributedCache.Get(cacheKey) == null)
                _distributedCache.Set(cacheKey, DistributedCacheUtils.ObjectToByteArray(result.Value));
        }
    }
}