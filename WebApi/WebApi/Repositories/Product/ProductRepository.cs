using WebApi.Models.DTOs;

namespace WebApi.Repositories;

public class ProductRepository : IProductRepository
{
    async public Task<bool> ProductExists(WarehouseEntryRequestDto request)
    {
        throw new NotImplementedException();
    }
}