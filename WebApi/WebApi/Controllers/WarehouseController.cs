using Microsoft.AspNetCore.Mvc;
using WebApi.Exceptions;
using WebApi.Models.DTOs;
using WebApi.Services;

namespace WebApi.Controllers;

[Route("api/warehouse")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpPost]
    [Route("add-product")]
    public async Task<IActionResult> AddProductToWarehouse(WarehouseEntryRequestDto request)
    {
        try
        {
            var entryId = await _warehouseService.AddProductToWarehouse(request);
            return Ok(new WarehouseEntryResponseDto{ WarehouseEntryId = entryId});

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("add-product-procedure")]
    public async Task<IActionResult> AddProdcutToWarehouseProcedure(WarehouseEntryRequestDto request)
    {
        try
        {
            return Ok(await _warehouseService.DeleteProductsFromWarehouse());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest(e.Message);
        }
    }
    
    /*[HttpGet]
    [Route("get-products")]
    public async Task<IActionResult> GetProductsFromWarehouse()
    {
        throw new NotImplementedException();

    }*/
    
    [HttpDelete]
    [Route("delete-products")]
    public async Task<IActionResult> DeleteProductsFromWarehouse()
    {
        return Ok("Dropped rows:  "+ await _warehouseService.DeleteProductsFromWarehouse());
    }
    
    
}