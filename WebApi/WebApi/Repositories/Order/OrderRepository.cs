using WebApi.Models.DTOs;

namespace WebApi.Repositories.Order;

public class OrderRepository : IOrderRepository
{
    async public Task<bool> ProductAndAmountExists(WarehouseEntryRequestDto request)
    {
        throw new NotImplementedException();
    }
}