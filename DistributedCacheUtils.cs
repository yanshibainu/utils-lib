using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace utils_lib
{
    public static class DistributedCacheUtils
    {
        public static string GetKey(HttpContext httpContext)
        {
            return new StringBuilder()
                .Append(httpContext.Request.Scheme)
                .Append("://")
                .Append(httpContext.Request.Host)
                .Append(httpContext.Request.PathBase)
                .Append(httpContext.Request.Path)
                .Append(httpContext.Request.QueryString)
                .ToString();
        }

        public static byte[] ObjectToByteArray(object obj)
        {
            var binaryFormatter = new BinaryFormatter();
            using var memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, obj);
            return memoryStream.ToArray();
        }

        public static T ByteArrayToObject<T>(byte[] bytes)
        {
            using var memoryStream = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            memoryStream.Write(bytes, 0, bytes.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var obj = binaryFormatter.Deserialize(memoryStream);
            return (T)obj;
        }
    }
}
