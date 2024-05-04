using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using WebApi.Middlewares;

namespace WebApi.Models.DTOs;

public class WarehouseEntryRequestDto
{
    [Required(ErrorMessage = "ProductId is required!")]
    public int ProductId { get; set; }
    
    [Required (ErrorMessage = "WarehouseId is required!")]
    public int WarehouseId { get; set; }
    
    [Required(ErrorMessage = "Amount is required!")]
    [Range(1,99999,ErrorMessage = "Amount value must be between 1 and 99999!")]
    public int Amount { get; set; }
    
    [Required(ErrorMessage = "RequestDate is required!")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime RequestDate { get; set; }
}