using WebApi.Models.DTOs;

namespace WebApi.Repositories;

public interface IWarehouseRepository
{ 
    Task<int> AddProductToWarehouseAsync(WarehouseEntryRequestDto request);
    Task <bool> WareHouseExists(WarehouseEntryRequestDto request);
}