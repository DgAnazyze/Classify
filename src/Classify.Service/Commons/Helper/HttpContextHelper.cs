using Microsoft.AspNetCore.Http;

namespace Classify.Service.Commons.Helper;

public class HttpContextHelper
{
    public static IHttpContextAccessor Accessor { get; set; }
    public static HttpContext HttpContext => Accessor?.HttpContext;
    public static IHeaderDictionary ResponseHeaders => HttpContext?.Response?.Headers;
    public static long? Id => long.Parse(HttpContext?.User?.FindFirst("Id").Value);
    public static string Role => HttpContext.User.FindFirst("Role").Value;
    public static string Region => HttpContext.User.FindFirst("Region").Value;
    public static string School => HttpContext.User.FindFirst("School").Value;

    // You may also add here Region when you need

    // Done :)

}
