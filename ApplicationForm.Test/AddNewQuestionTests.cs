using ApplicationFormPractice.Application.Implementation;
using ApplicationFormPractice.Domain.DTOs.Request;
using ApplicationFormPractice.Domain.Entities;
using ApplicationFormPractice.Domain.RepositoryContracts;
using ApplicationFormPractice.SharedKernel.GenericModels;
using Moq;

namespace ApplicationForm.Test;

public class AddNewQuestionTests
{
    [Fact]
    public async Task AddNewQuestion_Successfully()
    {
        // Arrange
        var request = new AddQuestionRequest
        {
            Question = "Tell us about yourself",
            QuestionType = "Paragraph",
            Choices = null,
            MaxChoicesAllowed = 0
        };

        var mockRepository = new Mock<IApplicationFormRepository>();
        mockRepository.Setup(repo => repo.AddNewCustomQuestionAsync(It.IsAny<CustomQuestion>()))
                      .ReturnsAsync(new CustomQuestion());
        var service = new ApplicationFormService(mockRepository.Object);

        // Act
        var result = await service.AddNewQuestion(request);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal("Successful", result.Message);
        mockRepository.Verify(repo => repo.AddNewCustomQuestionAsync(It.IsAny<CustomQuestion>()), Times.Once);
        mockRepository.Verify(repo => repo.SaveChangesAsync(null, false), Times.Once);
    }

    [Fact]
    public async Task AddNewQuestion_Failure()
    {
        // Arrange
        var request = new AddQuestionRequest
        {
            Question = "Tell us about yourself",
            QuestionType = "Paragraph",
            Choices = null,
            MaxChoicesAllowed = 0
        };

        var mockRepository = new Mock<IApplicationFormRepository>();
        mockRepository.Setup(repo => repo.AddNewCustomQuestionAsync(It.IsAny<CustomQuestion>()))
                      .ThrowsAsync(new Exception("Failed to add question"));
        var service = new ApplicationFormService(mockRepository.Object);
        ResponseWrapper<string> result = new ResponseWrapper<string>();

        // Act
        try
        {
            result = await service.AddNewQuestion(request);
        }
        catch (Exception ex)
        {
            result.IsSuccessful = false; 
            result.Message = ex.Message;
        }

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Failed to add question", result.Message);
        mockRepository.Verify(repo => repo.AddNewCustomQuestionAsync(It.IsAny<CustomQuestion>()), Times.Once);
        mockRepository.Verify(repo => repo.SaveChangesAsync(null, false), Times.Never);
    }
}
