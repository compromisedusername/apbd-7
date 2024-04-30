namespace WebApi.Models.DTOs;

public class WarehouseEntryRequestDto
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int Amount { get; set; }
    public DateTime RequestDate { get; set; }
}