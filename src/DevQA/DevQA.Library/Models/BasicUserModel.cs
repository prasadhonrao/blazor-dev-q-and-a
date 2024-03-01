namespace DevQA.Library.Models;

public class BasicUserModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";
    public string DisplayName { get; set; } = "";

    public BasicUserModel()
    {
    }

    public BasicUserModel(UserModel model)
    {
        Id = model.Id;
        DisplayName = model.DisplayName;
    }
}