using Microsoft.AspNetCore.Mvc;
using WebApi.Exceptions;
using WebApi.Models.DTOs;
using WebApi.Services;

namespace WebApi.Controllers;

[Route("api/[controller]")]
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
           
        var entryId = await _warehouseService.AddProductToWarehouseAsync(request);
        return Ok(new WarehouseEntryResponseDto{ WarehouseEntryId = entryId});
        
    }
    
    
}