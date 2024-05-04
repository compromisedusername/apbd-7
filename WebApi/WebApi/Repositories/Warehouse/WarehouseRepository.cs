using Microsoft.Data.SqlClient;
using WebApi.Models.DTOs;

namespace WebApi.Repositories;

public class WarehouseRepository : BaseRepository,  IWarehouseRepository
{
    

    public async Task<int> AddProductToWarehouse(WarehouseEntryRequestDto request, OrderDTO orderDto, int price)
    {
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await connection.OpenAsync();
        string query = "INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) VALUES (@IdWarehouse,@IdProduct,@IdOrder,@Amount,@Price,GETDATE());";
        string id = @"SELECT SCOPE_IDENTITY() AS LastInsertedProductId;";

        
        await using SqlCommand command = new SqlCommand(query,connection);
        await using SqlCommand idCommand = new SqlCommand(id,connection);

        command.Parameters.AddWithValue("IdWarehouse",request.WarehouseId);
        command.Parameters.AddWithValue("IdProduct",request.ProductId);
        command.Parameters.AddWithValue("IdOrder",orderDto.IdOrder);
        command.Parameters.AddWithValue("Amount",request.Amount);
        command.Parameters.AddWithValue("Price",(request.Amount * price));

        await command.ExecuteNonQueryAsync();
        command.CommandText = id;
        
        var reader = await command.ExecuteReaderAsync();
        if (!reader.HasRows)
        {
            throw new Exception("No product added!");
        }
        await reader.ReadAsync();
        try
        {
            var idInserted = Convert.ToInt32(reader[0]);
            return idInserted;
        }
        catch (Exception e)
        {
            Console.WriteLine("Reader in Waraehouse Repository error!");
        }

        return -1;
    }


   

    async public Task<bool> WarehouseExists(WarehouseEntryRequestDto request)
    {
        var query = @"SELECT 1 FROM Warehouse WHERE IdWarehouse = @IdWarehouse ";
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("IdWarehouse", request.WarehouseId);

        await connection.OpenAsync();
        var res = await command.ExecuteScalarAsync();
        return res is not null;
    }


    async public Task<bool> IsOrderCompleted(OrderDTO orderDto)
    {
        var query = @"SELECT 1 FROM Product_Warehouse WHERE IdOrder = @IdOrder";
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("IdOrder", orderDto.IdOrder);

        await connection.OpenAsync();
        var res = await command.ExecuteScalarAsync();
        return res is not null;
    }


    public WarehouseRepository(IConfiguration configuration) : base(configuration)
    {
    }
}