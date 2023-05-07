namespace Classify.Service.Exceptions;

public class CustomerException : Exception
{
    public int Code { get; set; }
    public CustomerException(int code = 500, string message = "Somthing went wrong") 
        : base(message)
    {
        this.Code = code;
    }
}
