using System.Net;

namespace WebApi.Exceptions;

public class OrderAlreadyCompletedException : ApiException
{
    public OrderAlreadyCompletedException() : base("Order has already completed!", HttpStatusCode.BadRequest)
    {
    }
}