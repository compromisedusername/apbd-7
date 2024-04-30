using WebApi.Models.DTOs;

namespace WebApi.Repositories.Order;

public interface IOrderRepository
{
    Task<OrderDTO> AmountAndProductExists(WarehouseEntryRequestDto request);
    Task UpdateOrderDate(OrderDTO orderDto);

}