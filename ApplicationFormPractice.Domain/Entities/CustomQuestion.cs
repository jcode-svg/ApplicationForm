using ApplicationFormPractice.Domain.DTOs.Request;
using static ApplicationFormPractice.SharedKernel.HelperMethods;

namespace ApplicationFormPractice.Domain.Entities;

public class CustomQuestion : Entity<Guid>
{
    public CustomQuestion() : base(Guid.NewGuid())
    {
        
    }

    public string QuestionType { get;  private set; }
    public string Question { get;  private set; }
    public string Choices { get;  private set; }
    public int MaxChoicesAllowed { get;  private set; }
    public DateTime UpdatedAt { get; private set; }

    public static CustomQuestion CreateNewQuestionRecord(AddQuestionRequest request)
    {
        return new CustomQuestion
        {
            QuestionType = request.QuestionType,
            Question = request.Question,
            Choices = request.Choices.ListToString(),
            MaxChoicesAllowed = request.MaxChoicesAllowed,
            UpdatedAt = DateTime.Now,
        };
    }

    public void UpdateCustomQuestion(EditCustomQuestionRequest request)
    {
        QuestionType = request.QuestionType;
        Question = request.Question;
        Choices = request.Choices.ListToString();
    }

    public static CustomQuestion MockCustomQuestionData(string questionType, string question, string choices, int maxChoicesAllowed)
    {
        return new CustomQuestion
        {
           QuestionType = questionType,
           Question = question,
           Choices = choices,
           MaxChoicesAllowed = maxChoicesAllowed
        };
    }
}
