using System.Data.Common;
using Microsoft.Data.SqlClient;
using WebApi.Exceptions;
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

    async public Task<int> AddProductToWarehouseProcedure(WarehouseEntryRequestDto request)
    {
        var queryFindIdOrder = @"SELECT TOP 1 o.IdOrder FROM [Order] o
                        JOIN Product_Warehouse pw ON o.IdOrder = pw.IdOrder WHERE o.IdProduct = @IdProduct AND o.Amount = @Amount AND DATEDIFF(day,@Date,CreatedAt) <= 0  ";
        var queryIsProductInDb = @"SELECT 1 FROM Product WHERE IdProduct = @IdProduct";

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;

        await connection.OpenAsync();


        DbTransaction transaction = await connection.BeginTransactionAsync();
        command.Transaction = (SqlTransaction)transaction;

        string errorMessage = "";
        try
        {

            //Exec of 1st command
            command.Parameters.Clear();
            command.CommandText = queryFindIdOrder;
            command.Parameters.AddWithValue("IdProduct", request.ProductId);
            command.Parameters.AddWithValue("Date", request.RequestDate);
            command.Parameters.AddWithValue("Amount", request.Amount);
            var idOrder = await command.ExecuteScalarAsync();
            if ( idOrder is null) throw new OrderNotExistsException();
            //Exec of 2nd command
            command.Parameters.Clear();
            command.CommandText = queryIsProductInDb;
            command.Parameters.AddWithValue("IdProduct", request.ProductId);
            if (await command.ExecuteScalarAsync() is null) throw new ProductNotExistsException();
            //Exec of 3rd command
            
            //Exec of 4th command

        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }

        throw new BadHttpRequestException("Transaction failed! (" + errorMessage +")");
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