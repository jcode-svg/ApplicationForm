using ApplicationFormPractice.Domain.Entities;

namespace ApplicationFormPractice.Domain.RepositoryContracts;

public interface IApplicationFormRepository
{
    Task<Application> AddNewApplicationAsync(Application record);
    Task<CustomQuestion> AddNewCustomQuestionAsync(CustomQuestion record);
    Task<bool> DeleteCustomQuestionByIdAsync(Guid questionId);
    Task<IEnumerable<CustomQuestion>> GetAllCustomQuestionsAsync();
    Task<CustomQuestion> GetQuestionByIdAsync(Guid questionId);
    Task<int> SaveChangesAsync(CustomQuestion Entity = null, bool Update = false);
}
