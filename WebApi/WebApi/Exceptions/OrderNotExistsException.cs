using System.Net;

namespace WebApi.Exceptions;

public class OrderNotExistsException : ApiException
{

    public OrderNotExistsException( ) : base("Product with given ID and amount does not exists in Orders!", HttpStatusCode.NotFound)
    {
    }
}