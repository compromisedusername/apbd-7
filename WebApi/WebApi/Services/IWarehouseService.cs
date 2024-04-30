using WebApi.Models.DTOs;
using WebApi.Repositories;

namespace WebApi.Services;

public interface IWarehouseService 
{
    Task<int> AddProductToWarehouseAsync(WarehouseEntryRequestDto request);
}