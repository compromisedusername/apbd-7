namespace WebApi.Repositories;

public abstract class BaseRepository
{
    protected readonly IConfiguration _configuration;

    public BaseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}