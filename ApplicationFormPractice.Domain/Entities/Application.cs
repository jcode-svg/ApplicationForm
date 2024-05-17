using ApplicationFormPractice.Domain.DTOs.Request;
using ApplicationFormPractice.Domain.DTOs.Response;

namespace ApplicationFormPractice.Domain.Entities;

public class Application : Entity<Guid>
{
    public Application() : base(Guid.NewGuid())
    {
        
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Nationality { get; set; }
    public string CurrentResidence { get; set; }
    public string IDNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public List<CustomQuestionAnswerDTO> CustomQuestionAnswers { get; set; }

    public static Application CreateNewApplication(SubmitApplicationRequest request)
    {
        return new Application
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Nationality = request.Nationality,
            CurrentResidence = request.CurrentResidence,
            IDNumber = request.IDNumber,
            Gender = request.Gender,
            DateOfBirth = request.DateOfBirth,
            CustomQuestionAnswers = request.CustomQuestionAnswers
        };
    }
}
