using System.Net;

namespace WebApi.Exceptions;

public class DateException : ApiException
{

    public DateException() : base("Date of creating order is later than date of query!", HttpStatusCode.BadRequest)
    {
    }
}