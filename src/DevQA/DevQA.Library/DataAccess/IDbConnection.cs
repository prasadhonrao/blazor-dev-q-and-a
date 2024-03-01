namespace DevQA.Library.DataAccess;
public interface IDbConnection
{
    MongoClient Client { get; }
    string DbName { get; }
    IMongoCollection<QuestionModel> Questions { get; }
    IMongoCollection<StatusModel> Statuses { get; }
    IMongoCollection<TagModel> Tags { get; }
    IMongoCollection<UserModel> Users { get; }
}