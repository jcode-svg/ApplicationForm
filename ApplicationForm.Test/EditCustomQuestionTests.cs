using Moq;
using ApplicationFormPractice.Application.Implementation;
using ApplicationFormPractice.Domain.DTOs.Request;
using ApplicationFormPractice.Domain.Entities;
using ApplicationFormPractice.Domain.RepositoryContracts;

namespace ApplicationForm.Test;

public class EditCustomQuestionTests
{
    [Fact]
    public async Task EditCustomQuestion_ValidQuestionId_Successfully()
    {
        // Arrange
        var request = new EditCustomQuestionRequest
        {
            QuestionId = "aece1747-33ef-42e8-b663-6f6fc7c95b78",
            QuestionType = "Paragraph",
            Question = "What is your name?",
            Choices = null,
            MaxChoicesAllowed = 0
        };

        var mockRepository = new Mock<IApplicationFormRepository>();
        var fakeCustomQuestion = new CustomQuestion(); 
        mockRepository.Setup(repo => repo.GetQuestionByIdAsync(It.IsAny<Guid>())).ReturnsAsync(fakeCustomQuestion);
        var service = new ApplicationFormService(mockRepository.Object);

        // Act
        var result = await service.EditCustomQuestion(request);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal("Successful", result.Message);
        mockRepository.Verify(repo => repo.SaveChangesAsync(fakeCustomQuestion, true), Times.Once);
    }

    [Fact]
    public async Task EditCustomQuestion_InvalidQuestionId_ReturnsError()
    {
        // Arrange
        var request = new EditCustomQuestionRequest
        {
            QuestionId = "invalid_guid_string",
            QuestionType = "Paragraph",
            Question = "What is your name?",
            Choices = null,
            MaxChoicesAllowed = 0
        };

        var mockRepository = new Mock<IApplicationFormRepository>();
        mockRepository.Setup(repo => repo.GetQuestionByIdAsync(It.IsAny<Guid>())).ReturnsAsync((CustomQuestion)null);
        var service = new ApplicationFormService(mockRepository.Object);

        // Act
        var result = await service.EditCustomQuestion(request);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Invalid Question Id", result.Message);
        mockRepository.Verify(repo => repo.SaveChangesAsync(It.IsAny<CustomQuestion>(), It.IsAny<bool>()), Times.Never);
    }

    [Fact]
    public async Task EditCustomQuestion_CustomQuestionIsNull_ReturnsError()
    {
        // Arrange
        var request = new EditCustomQuestionRequest
        {
            QuestionId = "aece1747-33ef-42e8-b663-6f6fc7c95b78",
            QuestionType = "Paragraph",
            Question = "What is your name?",
            Choices = null,
            MaxChoicesAllowed = 0
        };

        var mockRepository = new Mock<IApplicationFormRepository>();
        mockRepository.Setup(repo => repo.GetQuestionByIdAsync(It.IsAny<Guid>())).ReturnsAsync((CustomQuestion)null);
        var service = new ApplicationFormService(mockRepository.Object);

        // Act
        var result = await service.EditCustomQuestion(request);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Invalid Question Id", result.Message);
        mockRepository.Verify(repo => repo.SaveChangesAsync(It.IsAny<CustomQuestion>(), It.IsAny<bool>()), Times.Never);
    }
}
