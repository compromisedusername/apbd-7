using System.Net;

namespace WebApi.Exceptions;

public class OrderNotExistsException : ApiException
{

    public OrderNotExistsException( ) : base("Product with given ID and amount does not exists in Orders, or date of creating order is later than date of request!", HttpStatusCode.NotFound)
    {
    }
}