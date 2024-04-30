using WebApi.Models.DTOs;

namespace WebApi.Repositories.Order;

public interface IOrderRepository
{
    Task<bool> ProductAndAmountExists(WarehouseEntryRequestDto request);
}