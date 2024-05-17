using Moq;
using ApplicationFormPractice.Application.Implementation;
using ApplicationFormPractice.Domain.Entities;
using ApplicationFormPractice.Domain.RepositoryContracts;

namespace ApplicationForm.Test;

public class GetCustomQuestionsTests
{
    [Fact]
    public async Task GetCustomQuestions_ReturnsQuestions_Successfully()
    {
        // Arrange
        var mockRepository = new Mock<IApplicationFormRepository>();
        var fakeQuestions = new List<CustomQuestion>
    {
        CustomQuestion.MockCustomQuestionData("MultipleChoice", "What is your favorite color?", "Red,Blue,Green", 1 ),
        CustomQuestion.MockCustomQuestionData( "Paragrapgh", "What is your name?", null, 0 )
    };
        mockRepository.Setup(repo => repo.GetAllCustomQuestionsAsync()).ReturnsAsync(fakeQuestions);
        var service = new ApplicationFormService(mockRepository.Object);

        // Act
        var result = await service.GetCustomQuestions();

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(2, result.ResponseObject.Count);
        Assert.Equal("MultipleChoice", result.ResponseObject[0].QuestionType);
        Assert.Equal("What is your favorite color?", result.ResponseObject[0].Question);
        Assert.Equal(new List<string> { "Red", "Blue", "Green" }, result.ResponseObject[0].Choices);
        Assert.Equal(1, result.ResponseObject[0].MaxChoicesAllowed);
        Assert.Equal("Paragrapgh", result.ResponseObject[1].QuestionType);
        Assert.Equal("What is your name?", result.ResponseObject[1].Question);
        mockRepository.Verify(repo => repo.GetAllCustomQuestionsAsync(), Times.Once);
    }

    [Fact]
    public async Task GetCustomQuestions_EmptyList()
    {
        // Arrange
        var mockRepository = new Mock<IApplicationFormRepository>();
        mockRepository.Setup(repo => repo.GetAllCustomQuestionsAsync()).ReturnsAsync(new List<CustomQuestion>());
        var service = new ApplicationFormService(mockRepository.Object);

        // Act
        var result = await service.GetCustomQuestions();

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("There is no custom question", result.Message);
        mockRepository.Verify(repo => repo.GetAllCustomQuestionsAsync(), Times.Once);
    }
}
