namespace DevQA.Library.Models;

public class QuestionModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public BasicUserModel User { get; set; } = new();
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public List<TagModel> Tags { get; set; } = new();
    public StatusModel Status { get; set; } = new();
    public HashSet<string> UserVotes { get; set; } = new(); 
    public string AdminNotes { get; set; } = "";
    public bool ApprovedForRelease { get; set; } = false;
    public bool Archived { get; set; } = false;
    public bool Rejected { get; set; } = false;

}
