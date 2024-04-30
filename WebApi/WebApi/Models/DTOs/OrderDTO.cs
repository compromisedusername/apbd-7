using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.DTOs;

public class OrderDTO
{
    public int IdOrder { get; set; }
    public int IdProduct { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? FulfilledAt { get; set; }
    
}