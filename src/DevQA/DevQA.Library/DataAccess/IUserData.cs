namespace DevQA.Library.DataAccess;
public interface IUserData
{
    Task<UserModel> CreateUserAsync(UserModel user);
    Task DeleteUserAsync(string id);
    Task<List<UserModel>> GetAllUsersAsync();
    Task<UserModel> GetUserByIdAsync(string id);
    Task<UserModel> GetUserByObjectIdentifierAsync(string objectIdentifier);
    Task UpdateUserAsync(string id, UserModel user);
}