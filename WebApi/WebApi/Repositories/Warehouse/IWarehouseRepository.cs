using WebApi.Models.DTOs;

namespace WebApi.Repositories;

public interface IWarehouseRepository
{ 
    Task<int> AddProductToWarehouse(WarehouseEntryRequestDto request, OrderDTO orderDto,
        int price);
    Task <bool> WarehouseExists(WarehouseEntryRequestDto request);
    Task <bool> IsOrderCompleted(OrderDTO orderDto);
}