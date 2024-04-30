using WebApi.Models.DTOs;

namespace WebApi.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    async public Task<int> AddProductToWarehouseAsync(WarehouseEntryRequestDto request)
    {
        return 1;
    }

    async public Task<bool> WareHouseExists(WarehouseEntryRequestDto request)
    {
        return true;
    }
}