namespace Classify.Service.Commons.Exceptions;

public class CustomerException : Exception
{
    public int Code { get; set; }
    public CustomerException(int code = 500, string message = "Somthing went wrong")
        : base(message)
    {
        Code = code;
    }
}
