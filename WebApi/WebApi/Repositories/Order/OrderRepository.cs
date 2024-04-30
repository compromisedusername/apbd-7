using Microsoft.Data.SqlClient;
using WebApi.Exceptions;
using WebApi.Models.DTOs;

namespace WebApi.Repositories.Order;

public class OrderRepository : BaseRepository, IOrderRepository
{

    async public Task<OrderDTO> AmountAndProductExists(WarehouseEntryRequestDto request)
    {
        var query = @"SELECT IdOrder, IdProduct, Amount, CreatedAt, FulfilledAt FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt > @Date";
        
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("IdProduct",request.ProductId);
        command.Parameters.AddWithValue("Amount",request.Amount);
        command.Parameters.AddWithValue("Date",request.RequestDate);

        await connection.OpenAsync();

        var reader = await command.ExecuteReaderAsync();

        if (!reader.HasRows) throw new OrderNotExistsException();

        int idOrderOrdinal = reader.GetOrdinal("IdOrder");
        var idProductOrdinal = reader.GetOrdinal("IdProduct");
        var amount = reader.GetOrdinal("Amount");
        var createdAt = reader.GetOrdinal("CreatedAt");
        var fulfilledAt = reader.GetOrdinal("FulfilledAt");

        await reader.ReadAsync();
        var order = new OrderDTO();
        order.Amount = reader.GetInt32(amount);
        order.IdOrder = reader.GetInt32(idOrderOrdinal);
        order.CreatedAt = reader.GetDateTime(createdAt);
        order.FulfilledAt = reader.GetDateTime(fulfilledAt);
        order.IdOrder = reader.GetInt32(idProductOrdinal);
    
        return order;
    }
    async public Task UpdateOrderDate(OrderDTO orderDto)
    {
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await connection.OpenAsync();

        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        command.CommandText = @"UPDATE [Order] SET FulfilledAt = GETDATE() WHERE IdOrder = @IdOrder";

        command.Parameters.AddWithValue("IdOrder",orderDto.IdOrder);

        await command.ExecuteNonQueryAsync();
    }

    public OrderRepository(IConfiguration configuration) : base(configuration)
    {
    }
}