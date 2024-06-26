using WebApi.Models.DTOs;

namespace WebApi.Repositories;

public interface IProductRepository
{
    Task<bool> ProductExists(WarehouseEntryRequestDto request);
    Task<decimal> GetPrice(int productId);
}