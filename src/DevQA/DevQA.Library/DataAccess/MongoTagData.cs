namespace DevQA.Library.DataAccess;
public class MongoTagData : ITagData
{
    public IMemoryCache memoryCache { get; }
    private readonly IMongoCollection<TagModel> tags;
    private const string cacheKey = "Tags";
    public MongoTagData(IDbConnection dbConnection, IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache;
        tags = dbConnection.Tags;
    }

    public async Task<List<TagModel>> GetAllTagsAsync()
    {
        var output = memoryCache.Get<List<TagModel>>(cacheKey);
        if (output is null)
        {
            output = await tags.Find(tag => true).ToListAsync();
            memoryCache.Set(cacheKey, output, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });
        }
        return output;
    }

    public async Task<TagModel> GetTagByIdAsync(string id)
    {
        var output = memoryCache.Get<TagModel>(id);
        if (output is null)
        {
            output = await tags.Find(tag => tag.Id == id).FirstOrDefaultAsync();
            memoryCache.Set(id, output, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });
        }
        return output;
    }

    public async Task<TagModel> GetTagByNameAsync(string name)
    {
        var output = memoryCache.Get<TagModel>(name);
        if (output is null)
        {
            output = await tags.Find(tag => tag.Name == name).FirstOrDefaultAsync();
            memoryCache.Set(name, output, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });
        }
        return output;
    }

    public async Task<TagModel> CreateTagAsync(TagModel tag)
    {
        await tags.InsertOneAsync(tag);
        return tag;
    }

    public async Task UpdateTagAsync(string id, TagModel tag)
    {
        await tags.ReplaceOneAsync(tag => tag.Id == id, tag, new ReplaceOptions { IsUpsert = true });
    }

    public async Task DeleteTagAsync(string id)
    {
        await tags.DeleteOneAsync(tag => tag.Id == id);
    }

}
