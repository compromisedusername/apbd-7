using WebApi.Models.DTOs;
using WebApi.Repositories;

namespace WebApi.Services;

public interface IWarehouseService 
{
    Task<int> AddProductToWarehouse(WarehouseEntryRequestDto request);
    Task<ICollection<WarehouseDTO>> GetProductsFromWarehouse();
    Task<int> DeleteProductsFromWarehouse();
    
}