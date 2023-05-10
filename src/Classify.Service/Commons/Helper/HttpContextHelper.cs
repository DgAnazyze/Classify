using Microsoft.AspNetCore.Http;

namespace Classify.Service.Commons.Helper;

public class HttpContextHelper
{

    public IHttpContextAccessor Accessor { get; set; }
    public HttpContext HttpContext => Accessor?.HttpContext;

    public long? Id => long.Parse(HttpContext?.User?.FindFirst("Id").Value);
    public string Role => HttpContext.User.FindFirst("Role").Value;

    // You may also add here Region when you need

}
