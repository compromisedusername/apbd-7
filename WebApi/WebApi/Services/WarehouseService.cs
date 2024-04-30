using WebApi.Exceptions;
using WebApi.Models;
using WebApi.Models.DTOs;
using WebApi.Repositories;
using WebApi.Repositories.Order;

namespace WebApi.Services;

public class WarehouseService : IWarehouseService
{
    
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository, IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _warehouseRepository = warehouseRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public async Task<int> AddProductToWarehouse(WarehouseEntryRequestDto request)
    {
        if (!await _productRepository.ProductExists(request))
        {
            throw  new ProductNotExistsException();
        }
        if (!await _warehouseRepository.WarehouseExists(request))
        {
            throw new WarehouseNotExistsException();
        }

        OrderDTO order;
        if ( (order = await _orderRepository.AmountAndProductExists(request)) != null)
        {
            throw new OrderNotExistsException();
        };
        if (await _warehouseRepository.IsOrderCompleted(order))
        {
            throw new OrderAlreadyCompletedException();
        }

        await _orderRepository.UpdateOrderDate(order);
        int productPrice = await _productRepository.GetPrice(order.IdProduct);
        int entryId = await _warehouseRepository.AddProductToWarehouse(request, order, productPrice);
        return entryId;
    }

  
}