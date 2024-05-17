using ApplicationFormPractice.Domain.Entities;
using ApplicationFormPractice.Domain.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace ApplicationFormPractice.Domain.DTOs.Request;

public class AddQuestionRequest
{
    [Required(ErrorMessage = "Question Type is required.")]
    public string QuestionType { get; set; }

    [Required(ErrorMessage = "Question is required.")]
    [StringLength(500, ErrorMessage = "Question cannot exceed 500 characters.")]
    public string Question { get; set; }

    [ConditionalChoices("QuestionType", new[] { "Dropdown", "MultipleChoice" }, ErrorMessage = "Choices can only be provided if QuestionType is Dropdown or MultipleChoice.")]
    public List<string> Choices { get; set; }

    [ConditionalMaxChoicesAllowed("QuestionType", "MultipleChoice", ErrorMessage = "MaxChoicesAllowed can only be provided if QuestionType is MultipleChoice.")]
    public int MaxChoicesAllowed { get; set; }


}
