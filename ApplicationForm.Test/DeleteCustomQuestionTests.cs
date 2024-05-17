using Moq;
using ApplicationFormPractice.Application.Implementation;
using ApplicationFormPractice.Domain.RepositoryContracts;

namespace ApplicationForm.Test;

public class DeleteCustomQuestionTests
{
    [Fact]
    public async Task DeleteCustomQuestion_ValidQuestionId_Successfully()
    {
        // Arrange
        var questionId = "aece1747-33ef-42e8-b663-6f6fc7c95b78";
        var mockRepository = new Mock<IApplicationFormRepository>();
        mockRepository.Setup(repo => repo.DeleteCustomQuestionByIdAsync(It.IsAny<Guid>())).ReturnsAsync(true);
        var service = new ApplicationFormService(mockRepository.Object);

        // Act
        var result = await service.DeleteCustomQuestion(questionId);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal("Successful", result.Message);
        mockRepository.Verify(repo => repo.DeleteCustomQuestionByIdAsync(It.IsAny<Guid>()), Times.Once);
        mockRepository.Verify(repo => repo.SaveChangesAsync(null, false), Times.Once);
    }

    [Fact]
    public async Task DeleteCustomQuestion_InvalidQuestionId_ReturnsError()
    {
        // Arrange
        var questionId = "invalid_guid_string";
        var mockRepository = new Mock<IApplicationFormRepository>();
        mockRepository.Setup(repo => repo.DeleteCustomQuestionByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);
        var service = new ApplicationFormService(mockRepository.Object);

        // Act
        var result = await service.DeleteCustomQuestion(questionId);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Invalid Question Id", result.Message);
        mockRepository.Verify(repo => repo.DeleteCustomQuestionByIdAsync(It.IsAny<Guid>()), Times.Never);
        mockRepository.Verify(repo => repo.SaveChangesAsync(null, false), Times.Never); 
    }

    [Fact]
    public async Task DeleteCustomQuestion_OperationFailed_ReturnsError()
    {
        // Arrange
        var questionId = "aece1747-33ef-42e8-b663-6f6fc7c95b78";
        var mockRepository = new Mock<IApplicationFormRepository>();
        mockRepository.Setup(repo => repo.DeleteCustomQuestionByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);
        var service = new ApplicationFormService(mockRepository.Object);

        // Act
        var result = await service.DeleteCustomQuestion(questionId);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Invalid Question Id", result.Message);
        mockRepository.Verify(repo => repo.DeleteCustomQuestionByIdAsync(It.IsAny<Guid>()), Times.Once);
        mockRepository.Verify(repo => repo.SaveChangesAsync(null, false), Times.Never); 
    }
}
