using WebApi.Models.DTOs;

namespace WebApi.Repositories;

public interface IWarehouseRepository
{ 
    Task<int> AddProductToWarehouse(WarehouseEntryRequestDto request);
    Task <bool> WarehouseExists(WarehouseEntryRequestDto request);
    Task <bool> IsOrderCompleted(WarehouseEntryRequestDto request);
}