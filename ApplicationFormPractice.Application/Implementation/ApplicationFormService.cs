using ApplicationFormPractice.Application.Contract;
using ApplicationFormPractice.Domain.DTOs.Request;
using ApplicationFormPractice.Domain.DTOs.Response;
using ApplicationFormPractice.Domain.Entities;
using ApplicationFormPractice.Domain.RepositoryContracts;
using ApplicationFormPractice.SharedKernel.GenericModels;
using static ApplicationFormPractice.SharedKernel.Enums.Enumerations;
using static ApplicationFormPractice.SharedKernel.Enums.EnumHelper;
using ApplicationRecord = ApplicationFormPractice.Domain.Entities.Application;


namespace ApplicationFormPractice.Application.Implementation;

public class ApplicationFormService : IApplicationFormService
{
    private readonly IApplicationFormRepository _applicationFormRepository;

    public ApplicationFormService(IApplicationFormRepository applicationFormRepository)
    {
        _applicationFormRepository = applicationFormRepository;
    }

    public ResponseWrapper<List<string>> GetQuestionTypes()
    {
        var response = GetEnumMembers<QuestionType>();

        return ResponseWrapper<List<string>>.Success(response);
    }

    public async Task<ResponseWrapper<string>> AddNewQuestion(AddQuestionRequest request)
    {
        var newQuestion = CustomQuestion.CreateNewQuestionRecord(request);
        var newRecord = await _applicationFormRepository.AddNewCustomQuestionAsync(newQuestion);
        await _applicationFormRepository.SaveChangesAsync();

        return ResponseWrapper<string>.Success("Saved Question Successfully");
    }

    public async Task<ResponseWrapper<List<CustomQuestionDTO>>> GetCustomQuestions()
    {
        var customQuestions = await _applicationFormRepository.GetAllCustomQuestionsAsync();
        if (!customQuestions.Any())
        {
            return ResponseWrapper<List<CustomQuestionDTO>>.Error("There is no custom question");
        }

        var customQuestionsResponse = customQuestions.Select(record => new CustomQuestionDTO
        {
            QuestionId = record.Id.ToString(),
            QuestionType = record.QuestionType,
            Question = record.Question,
            Choices = string.IsNullOrEmpty(record.Choices) ? new List<string>() 
            : record.Choices.Trim().Split(",").ToList(),
            MaxChoicesAllowed = record.MaxChoicesAllowed
        }).ToList();

        return ResponseWrapper<List<CustomQuestionDTO>>.Success(customQuestionsResponse);
    }

    public async Task<ResponseWrapper<string>> EditCustomQuestion(EditCustomQuestionRequest request)
    {
        var parsed = Guid.TryParse(request.QuestionId, out Guid parsedQuestionId);
        if (!parsed)
        {
            return ResponseWrapper<string>.Error("Invalid Question Id");
        }

        var customQuestion = await _applicationFormRepository.GetQuestionByIdAsync(parsedQuestionId);
        if (customQuestion == null)
        {
            return ResponseWrapper<string>.Error("Invalid Question Id");
        }

        customQuestion.UpdateCustomQuestion(request);
        await _applicationFormRepository.SaveChangesAsync(customQuestion, true);

        return ResponseWrapper<string>.Success("Update Successful");
    }

    public async Task<ResponseWrapper<string>> DeleteCustomQuestion(string questionId)
    {
        var parsed = Guid.TryParse(questionId, out Guid parsedQuestionId);
        if (!parsed)
        {
            return ResponseWrapper<string>.Error("Invalid Question Id");
        }

        var deleteSuccesful = await _applicationFormRepository.DeleteCustomQuestionByIdAsync(parsedQuestionId);
        if (!deleteSuccesful)
        {
            return ResponseWrapper<string>.Error("Invalid Question Id");
        }
        await _applicationFormRepository.SaveChangesAsync();

        return ResponseWrapper<string>.Success("Deleted Successfully");
    }

    public async Task<ResponseWrapper<string>> SubmitApplication(SubmitApplicationRequest request)
    {
        var newApplication = ApplicationRecord.CreateNewApplication(request);
        var newApplicationRecord = await _applicationFormRepository.AddNewApplicationAsync(newApplication);
        await _applicationFormRepository.SaveChangesAsync();

        return ResponseWrapper<string>.Success("Application Submitted Successfully");
    }
}
