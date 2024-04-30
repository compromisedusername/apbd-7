using System.Net;

namespace WebApi.Exceptions;

public class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public ApiException(string message, HttpStatusCode statusCode) 
        : base(message)
    {
        StatusCode = statusCode;
    }
}