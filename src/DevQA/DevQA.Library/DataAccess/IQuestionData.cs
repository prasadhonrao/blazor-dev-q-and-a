namespace DevQA.Library.DataAccess;
public interface IQuestionData
{
    Task<QuestionModel> CreateQuestionAsync(QuestionModel question);
    Task<QuestionModel> CreateQuestionWithoutTransactionAsync(QuestionModel question);
    Task DeleteQuestionAsync(string id);
    Task<List<QuestionModel>> GetAllApprovedQuestionsAsync();
    Task<List<QuestionModel>> GetAllQuestionsAsync();
    Task<QuestionModel> GetQuestionByIdAsync(string id);
    Task<List<QuestionModel>> GetQuestionsWaitingForApprovalAsync();
    Task UpdateQuestionAsync(string id, QuestionModel question);
    Task UpvoteQuestion(string questionId, string userId);
}