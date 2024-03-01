namespace DevQA.Library.DataAccess;
public interface IStatusData
{
    IMemoryCache memoryCache { get; }

    Task<StatusModel> CreateStatusAsync(StatusModel status);
    Task DeleteStatusAsync(string id);
    Task<List<StatusModel>> GetAllStatusesAsync();
    Task<StatusModel> GetStatusByIdAsync(string id);
    Task<StatusModel> GetStatusByNameAsync(string name);
    Task UpdateStatusAsync(string id, StatusModel status);
}