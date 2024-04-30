using WebApi.Models.DTOs;

namespace WebApi.Repositories.Order;

public class OrderRepository : BaseRepository, IOrderRepository
{

    async public Task<bool> ProductAndAmountExists(WarehouseEntryRequestDto request)
    {
        throw new NotImplementedException();
    }

    public OrderRepository(IConfiguration configuration) : base(configuration)
    {
    }
}