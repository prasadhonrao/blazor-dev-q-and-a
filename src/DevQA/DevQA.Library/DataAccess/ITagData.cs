namespace DevQA.Library.DataAccess;
public interface ITagData
{
    IMemoryCache memoryCache { get; }

    Task<TagModel> CreateTagAsync(TagModel tag);
    Task DeleteTagAsync(string id);
    Task<List<TagModel>> GetAllTagsAsync();
    Task<TagModel> GetTagByIdAsync(string id);
    Task<TagModel> GetTagByNameAsync(string id);
    Task UpdateTagAsync(string id, TagModel tag);
}