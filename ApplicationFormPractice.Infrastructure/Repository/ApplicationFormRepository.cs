using ApplicationFormPractice.Domain.Entities;
using ApplicationFormPractice.Domain.RepositoryContracts;
using ApplicationFormPractice.Infrastructure.Contract;
using ApplicationFormPractice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApplicationFormPractice.Infrastructure.Repository;

public class ApplicationFormRepository : RepositoryAbstract, IApplicationFormRepository
{
    private ApplicationFormDbContext _applicationFormDbContext;

    public ApplicationFormRepository(ApplicationFormDbContext dbContext) : base(dbContext)
    {
        _applicationFormDbContext = dbContext;
    }

    public async Task<CustomQuestion> AddNewCustomQuestionAsync(CustomQuestion record)
    {
        var newRecord = await _applicationFormDbContext.CustomQuestions.AddAsync(record);
        return newRecord.Entity;
    }
      
    public async Task<IEnumerable<CustomQuestion>> GetAllCustomQuestionsAsync()
    {
        return await _applicationFormDbContext.CustomQuestions.ToListAsync();
    }

    public async Task<CustomQuestion> GetQuestionByIdAsync(Guid questionId)
    {
        return await _applicationFormDbContext.CustomQuestions.FindAsync(questionId);
    }

    public async Task<bool> DeleteCustomQuestionByIdAsync(Guid questionId)
    {
        var customQuestion = await _applicationFormDbContext.CustomQuestions.FindAsync(questionId);
        if (customQuestion == null)
        {
            return false; 
        }

        _applicationFormDbContext.CustomQuestions.Remove(customQuestion);

        return true; 
    }

    public async Task<Application> AddNewApplicationAsync(Application record)
    {
        var newRecord = await _applicationFormDbContext.Applications.AddAsync(record);
        return newRecord.Entity;
    }

    public async Task<int> SaveChangesAsync(CustomQuestion Entity = null, bool Update = false)
    {
        if (Update)
            _applicationFormDbContext.Entry(Entity).State = EntityState.Modified;

        return await _applicationFormDbContext.SaveChangesAsync(new CancellationToken());
    }
}
