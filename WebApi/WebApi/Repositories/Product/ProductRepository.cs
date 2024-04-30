using WebApi.Models.DTOs;

namespace WebApi.Repositories;

public class ProductRepository :BaseRepository, IProductRepository
{
    
    async public Task<bool> ProductExists(WarehouseEntryRequestDto request)
    {
        throw new NotImplementedException();
    }

    async public Task UpdateOrderDate(WarehouseEntryRequestDto request)
    {
        throw new NotImplementedException();
    }

    public ProductRepository(IConfiguration configuration) : base(configuration)
    {
    }
}