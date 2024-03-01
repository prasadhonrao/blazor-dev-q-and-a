namespace DevQA.Library.DataAccess;
public class MongoUserData : IUserData
{
    private readonly IMongoCollection<UserModel> users;
    public MongoUserData(IDbConnection dbConnection)
    {
        users = dbConnection.Users;
    }

    public async Task<List<UserModel>> GetAllUsersAsync()
    {
        return await users.Find(user => true).ToListAsync();
    }

    public async Task<UserModel> GetUserByIdAsync(string id)
    {
        return await users.Find(user => user.Id == id).FirstOrDefaultAsync();
    }

    public async Task<UserModel> GetUserByObjectIdentifierAsync(string objectIdentifier)
    {
        return await users.Find(user => user.ObjectIdentifier == objectIdentifier).FirstOrDefaultAsync();
    }

    public async Task<UserModel> CreateUserAsync(UserModel user)
    {
        await users.InsertOneAsync(user);
        return user;
    }

    public async Task UpdateUserAsync(string id, UserModel user)
    {
        await users.ReplaceOneAsync(user => user.Id == id, user, new ReplaceOptions { IsUpsert = true });
    }

    public async Task DeleteUserAsync(string id)
    {
        await users.DeleteOneAsync(user => user.Id == id);
    }
}
