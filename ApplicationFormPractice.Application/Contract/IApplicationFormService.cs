using ApplicationFormPractice.Domain.DTOs.Request;
using ApplicationFormPractice.Domain.DTOs.Response;
using ApplicationFormPractice.SharedKernel.GenericModels;

namespace ApplicationFormPractice.Application.Contract;

public interface IApplicationFormService
{
    Task<ResponseWrapper<string>> AddNewQuestion(AddQuestionRequest request);
    Task<ResponseWrapper<string>> DeleteCustomQuestion(string questionId);
    Task<ResponseWrapper<string>> EditCustomQuestion(EditCustomQuestionRequest request);
    Task<ResponseWrapper<List<CustomQuestionDTO>>> GetCustomQuestions();
    ResponseWrapper<List<string>> GetQuestionTypes();
    Task<ResponseWrapper<string>> SubmitApplication(SubmitApplicationRequest request);
}
