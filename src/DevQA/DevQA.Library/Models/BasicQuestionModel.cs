using MongoDB.Bson.Serialization.Attributes;

namespace DevQA.Library.Models;
public class BasicQuestionModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";

    public BasicQuestionModel()
    {
    }
    public BasicQuestionModel(QuestionModel model)
    {
        Id = model.Id;
        Title = model.Title;
    }
}
