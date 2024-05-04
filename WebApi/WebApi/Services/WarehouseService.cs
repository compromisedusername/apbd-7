using System.Diagnostics;
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
            throw new ProductNotExistsException();
        }
        if (!await _warehouseRepository.WarehouseExists(request))
        {
            throw new WarehouseNotExistsException();
        }

        OrderDTO order = await _orderRepository.AmountAndProductExists(request);
        
        if ( order == null)
        {
            throw new OrderNotExistsException();
        };
        if (await _warehouseRepository.IsOrderCompleted(order))
        {
            throw new OrderAlreadyCompletedException();
        }

        await _orderRepository.UpdateOrderDate(order);
        var productPrice = await _productRepository.GetPrice(order.IdProduct);
        var entryId = await _warehouseRepository.AddProductToWarehouse(request, order, productPrice);
        return entryId;
    }

    public async Task<ICollection<WarehouseDTO>> GetProductsFromWarehouse()
    {
        throw new NotImplementedException();
    }

    public async Task<int> DeleteProductsFromWarehouse()
    {
        return await _warehouseRepository.DeleteProductsFromWarehouse();
    }

   
}