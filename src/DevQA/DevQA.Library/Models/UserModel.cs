namespace DevQA.Library.Models;
public class UserModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";
    public string ObjectIdentifier { get; set; } = ""; // This is the Azure AD Object Identifier
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string Email { get; set; } = "";
    public List<BasicQuestionModel> Questions { get; set; } = new(); 
    public List<BasicQuestionModel> VotedQuestions { get; set; } = new(); 

}
