using Microsoft.Data.SqlClient;
using WebApi.Models.DTOs;

namespace WebApi.Repositories;

public class WarehouseRepository : BaseRepository,  IWarehouseRepository
{
    

    public async Task<int> AddProductToWarehouse(WarehouseEntryRequestDto request, OrderDTO orderDto, decimal price)
    {
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await connection.OpenAsync();
        
        string query = "INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)" +
                       " VALUES (@IdWarehouse,@IdProduct,@IdOrder,@Amount,@Price,GETDATE());" +
                       "SELECT SCOPE_IDENTITY();";
        await using SqlCommand command = new SqlCommand(query,connection);
       

        command.Parameters.AddWithValue("IdWarehouse",request.WarehouseId);
        command.Parameters.AddWithValue("IdProduct",request.ProductId);
        command.Parameters.AddWithValue("IdOrder",orderDto.IdOrder);
        command.Parameters.AddWithValue("Amount",request.Amount);
        command.Parameters.AddWithValue("Price",(request.Amount * price));

        var res = await command.ExecuteScalarAsync();

        
        
        if (res != null)
        {
            return Convert.ToInt32((decimal)res); 
        }
        else
        {
            throw new Exception("No data was inserted! Error in WarehouseRepository!.");
        }


    }


   

    async public Task<bool> WarehouseExists(WarehouseEntryRequestDto request)
    {
        var query = @"SELECT 1 FROM Warehouse WHERE IdWarehouse = @IdWarehouse ";
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

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
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("IdOrder", orderDto.IdOrder);

        await connection.OpenAsync();
        var res = await command.ExecuteScalarAsync();
        return res is not null;
    }

    async public Task<ICollection<WarehouseDTO>> GetProductsFromWarehouse()
    {
        throw new NotImplementedException();
    }

    public async Task<int> DeleteProductsFromWarehouse()
    {
        var query = @"DELETE FROM Product_Warehouse WHERE 1 = 1";
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        
        await connection.OpenAsync();
        return await command.ExecuteNonQueryAsync();
    }


    public  WarehouseRepository(IConfiguration configuration) : base(configuration)
    {
    }
}