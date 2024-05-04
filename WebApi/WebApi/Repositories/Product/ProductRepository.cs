using Azure.Core;
using Microsoft.Data.SqlClient;
using WebApi.Models.DTOs;

namespace WebApi.Repositories;

public class ProductRepository :BaseRepository, IProductRepository
{
    
    async public Task<bool> ProductExists(WarehouseEntryRequestDto request)
    {
        var query = @"SELECT 1 FROM Product WHERE IdProduct = @IdProduct";
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("IdProduct",request.ProductId);

        await connection.OpenAsync();
        var res = await command.ExecuteScalarAsync();
        return res is not null;
    }

    async public Task<int> GetPrice(int productId)
    {
        var query = @"SELECT Price FROM Product WHERE IdProduct = @IdProduct";
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("IdProduct", productId);

        var reader = await command.ExecuteReaderAsync();

        if (!reader.HasRows) throw new Exception("No rows in GetPrice!");

        try
        {

            var price = reader.GetInt32(0);
            return price;
        }
        catch (Exception e)
        {
            Console.WriteLine("Reader in ProductRepository error!");
        }
        return -1;

    }


    public ProductRepository(IConfiguration configuration) : base(configuration)
    {
    }
}