using WebApi.Models.DTOs;

namespace WebApi.Repositories;

public interface IWarehouseRepository
{ 
    Task<int> AddProductToWarehouse(WarehouseEntryRequestDto request, OrderDTO orderDto,
        decimal price);
    Task <bool> WarehouseExists(WarehouseEntryRequestDto request);
    Task <bool> IsOrderCompleted(OrderDTO orderDto);
    Task<ICollection<WarehouseDTO>> GetProductsFromWarehouse();
    Task<int> DeleteProductsFromWarehouse();
    Task<int> AddProductToWarehouseProcedure(WarehouseEntryRequestDto request);

}