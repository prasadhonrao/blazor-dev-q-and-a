namespace DevQA.Library.DataAccess;
public class DbConnection : IDbConnection
{
    private readonly IMongoDatabase db;
    private readonly IConfiguration config;
    private readonly string connectionId = "MongoDb";
    public string DbName { get; private set; } = "";
    public MongoClient Client { get; private set; }

    public DbConnection(IConfiguration configuration)
    {
        config = configuration;
        Client = new MongoClient(config.GetConnectionString(connectionId));
        DbName = config["DatabaseName"]!.ToString();
        db = Client.GetDatabase(DbName);
    }
    public IMongoCollection<UserModel> Users => db.GetCollection<UserModel>("users");
    public IMongoCollection<QuestionModel> Questions => db.GetCollection<QuestionModel>("questions");
    public IMongoCollection<TagModel> Tags => db.GetCollection<TagModel>("tags");
    public IMongoCollection<StatusModel> Statuses => db.GetCollection<StatusModel>("statuses");
}
