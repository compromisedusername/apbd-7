using System.Net;

namespace WebApi.Exceptions;

public class ProductNotExistsException : ApiException
{

    public ProductNotExistsException() : base("Product with given ID  does not exists!", HttpStatusCode.NotFound)
    {
    }
}