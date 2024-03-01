﻿namespace DevQA.Library.DataAccess;
public class MongoQuestionData : IQuestionData
{
    private readonly IUserData userData;
    private readonly IMemoryCache memoryCache;
    private readonly IMongoCollection<QuestionModel> questions;
    private const string cacheKey = "Questions";

    public MongoQuestionData(IDbConnection dbConnection, IUserData userData, IMemoryCache memoryCache)
    {
        this.userData = userData;
        this.memoryCache = memoryCache;
        questions = dbConnection.Questions;
    }

    public async Task<List<QuestionModel>> GetAllQuestionsAsync()
    {
        var output = memoryCache.Get<List<QuestionModel>>(cacheKey);
        if (output is null)
        {
            output = await questions.Find(question => question.Archived == false).ToListAsync();
            memoryCache.Set(cacheKey, output, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });
        }
        return output;
    }

    public async Task<List<QuestionModel>> GetAllApprovedQuestionsAsync()
    {
        var output = memoryCache.Get<List<QuestionModel>>(cacheKey);
        if (output is null)
        {
            output = await questions.Find(question => question.Archived == false && question.ApprovedForRelease == true).ToListAsync();
            memoryCache.Set(cacheKey, output, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });
        }
        return output;
    }

    public async Task<QuestionModel> GetQuestionByIdAsync(string id)
    {
        var output = memoryCache.Get<QuestionModel>(id);
        if (output is null)
        {
            output = await questions.Find(question => question.Id == id).FirstOrDefaultAsync();
            memoryCache.Set(id, output, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });
        }
        return output;
    }

    public async Task<List<QuestionModel>> GetQuestionsWaitingForApprovalAsync()
    {
        var output = memoryCache.Get<List<QuestionModel>>(cacheKey);
        if (output is null)
        {
            output = await questions.Find(question => question.Archived == false &&
                                                      question.Rejected == false &&
                                                      question.ApprovedForRelease == false).ToListAsync();
            memoryCache.Set(cacheKey, output, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });
        }
        return output;
    }

    public async Task UpvoteQuestion(string questionId, string userId)
    {
        var question = await GetQuestionByIdAsync(questionId);
        if (question is null)
        {
            throw new Exception("Question not found");
        }
        bool isUpvote = question.UserVotes.Add(userId);
        if (isUpvote == false)
        {
            question.UserVotes.Remove(userId);
        }
        await UpdateQuestionAsync(questionId, question);

        var user = await userData.GetUserByIdAsync(question.User.Id);
        if (isUpvote)
        {
            user.VotedQuestions.Add(new BasicQuestionModel(question));
        }
        else
        {
            var questionToRemove = user.VotedQuestions.Where(q => q.Id == question.Id).First();
            user.VotedQuestions.Remove(questionToRemove);
        }
        await userData.UpdateUserAsync(user.Id, user);

        memoryCache.Remove(cacheKey);
    }

    public async Task<QuestionModel> CreateQuestionAsync(QuestionModel question)
    {
        await questions.InsertOneAsync(question);

        var user = await userData.GetUserByIdAsync(question.User.Id);
        user.Questions.Add(new BasicQuestionModel(question));
        await userData.UpdateUserAsync(user.Id, user);

        return question;
    }

    public async Task UpdateQuestionAsync(string id, QuestionModel question)
    {
        await questions.ReplaceOneAsync(question => question.Id == id, question, new ReplaceOptions { IsUpsert = true });
        memoryCache.Remove(cacheKey);
    }

    public async Task DeleteQuestionAsync(string id)
    {
        await questions.DeleteOneAsync(question => question.Id == id);
        memoryCache.Remove(cacheKey);
    }

}
