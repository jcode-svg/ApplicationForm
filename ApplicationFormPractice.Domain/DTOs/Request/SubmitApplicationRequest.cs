using ApplicationFormPractice.Domain.DTOs.Response;
using System.ComponentModel.DataAnnotations;

namespace ApplicationFormPractice.Domain.DTOs.Request;

public class SubmitApplicationRequest
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(20, ErrorMessage = "First name must not exceed 20 characters")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(20, ErrorMessage = "Last name must not exceed 20 characters")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email must be a valid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [StringLength(20, ErrorMessage = "Phone number must not exceed 20 characters")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Nationality is required")]
    [StringLength(20, ErrorMessage = "Nationality must not exceed 20 characters")]
    public string Nationality { get; set; }

    [Required(ErrorMessage = "Current residence is required")]
    [StringLength(20, ErrorMessage = "Current residence must not exceed 20 characters")]
    public string CurrentResidence { get; set; }

    [Required(ErrorMessage = "ID number is required")]
    [StringLength(20, ErrorMessage = "ID number must not exceed 20 characters")]
    public string IDNumber { get; set; }

    [Required(ErrorMessage = "Date of birth is required")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    [StringLength(20, ErrorMessage = "Gender must not exceed 20 characters")]
    public string Gender { get; set; }

    public List<CustomQuestionAnswerDTO> CustomQuestionAnswers { get; set; }
}
