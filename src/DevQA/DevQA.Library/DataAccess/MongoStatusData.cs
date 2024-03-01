namespace DevQA.Library.DataAccess;
public class MongoStatusData : IStatusData
{
    private readonly IMongoCollection<StatusModel> statuses;
    public IMemoryCache memoryCache { get; }
    private const string cacheKey = "Statuses";

    public MongoStatusData(IDbConnection dbConnection, IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache;
        statuses = dbConnection.Statuses;
    }

    public async Task<List<StatusModel>> GetAllStatusesAsync()
    {
        var output = memoryCache.Get<List<StatusModel>>(cacheKey);
        if (output is null)
        {
            output = await statuses.Find(status => true).ToListAsync();
            memoryCache.Set(cacheKey, output, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });
        }
        return output;
    }

    public async Task<StatusModel> GetStatusByIdAsync(string id)
    {
        var output = memoryCache.Get<StatusModel>(id);
        if (output is null)
        {
            output = await statuses.Find(status => status.Id == id).FirstOrDefaultAsync();
            memoryCache.Set(id, output, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });
        }
        return output;
    }

    public async Task<StatusModel> GetStatusByNameAsync(string name)
    {
        var output = memoryCache.Get<StatusModel>(name);
        if (output is null)
        {
            output = await statuses.Find(status => status.Name == name).FirstOrDefaultAsync();
            memoryCache.Set(name, output, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });
        }
        return output;
    }

    public async Task<StatusModel> CreateStatusAsync(StatusModel status)
    {
        await statuses.InsertOneAsync(status);
        return status;
    }

    public async Task UpdateStatusAsync(string id, StatusModel status)
    {
        await statuses.ReplaceOneAsync(status => status.Id == id, status, new ReplaceOptions { IsUpsert = true });
    }

    public async Task DeleteStatusAsync(string id)
    {
        await statuses.DeleteOneAsync(status => status.Id == id);
    }

}
