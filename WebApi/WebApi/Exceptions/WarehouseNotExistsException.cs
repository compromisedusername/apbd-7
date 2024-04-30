using System.Net;

namespace WebApi.Exceptions;

public class WarehouseNotExistsException : ApiException
{

    public WarehouseNotExistsException() : base("Warehouse with the given ID does not exists!", HttpStatusCode.NotFound)
    {
    }
}