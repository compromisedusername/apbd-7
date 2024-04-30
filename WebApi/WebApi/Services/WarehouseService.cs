using WebApi.Exceptions;
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

    public async Task<int> AddProductToWarehouseAsync(WarehouseEntryRequestDto request)
    {
        if (!_productRepository.ProductExists(request).Result)
        {
            throw new ProductNotExistsException();
        }
        if (!_warehouseRepository.WareHouseExists(request).Result)
        {
            throw new WarehouseNotExistsException();
        }
        if (!_orderRepository.ProductAndAmountExists(request).Result | request.Amount <= 0)
        {
            throw new OrderNotExistsException();
        };
        
        var entryId = await _warehouseRepository.AddProductToWarehouseAsync(request);
        return entryId;
    }
}