using WebApi.Models.DTOs;

namespace WebApi.Repositories;

public class WarehouseRepository : BaseRepository,  IWarehouseRepository
{
    

    async public Task<int> AddProductToWarehouse(WarehouseEntryRequestDto request)
    {
        return 1;
    }

    async public Task<bool> WarehouseExists(WarehouseEntryRequestDto request)
    {
        return true;
    }

    async public Task<bool> IsOrderCompleted(WarehouseEntryRequestDto request)
    {
        throw new NotImplementedException();
    }


    public WarehouseRepository(IConfiguration configuration) : base(configuration)
    {
    }
}