using Microsoft.AspNetCore.Http;

namespace Classify.Service.Commons.Helper.HttpContextHelper;

public class HttpContextHelper
{

    public static IHttpContextAccessor Accessor { get; set; }
    public static HttpContext HttpContext => Accessor?.HttpContext;
    public static IHeaderDictionary ResponseHeaders => HttpContext?.Response?.Headers;
    public static long? Id => long.Parse(HttpContext?.User?.FindFirst("Id").Value);
    public static string Role => HttpContext.User.FindFirst("Role").Value;


}
