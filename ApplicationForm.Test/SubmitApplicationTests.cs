using Moq;
using ApplicationFormPractice.Application.Implementation;
using ApplicationFormPractice.Domain.DTOs.Request;
using ApplicationFormPractice.Domain.RepositoryContracts;
using ApplicationFormPractice.Domain.Entities;

namespace ApplicationForm.Test;

public class SubmitApplicationTests
{
    [Fact]
    public async Task SubmitApplication_Successfully()
    {
        // Arrange
        var request = new SubmitApplicationRequest
        {
            FirstName = "Nelson",
            LastName = "Akinro",
            PhoneNumber = "1234567890",
            Gender = "Male",
            Email = "nelson@gmail.com",
            Nationality = "Dutch",
            CurrentResidence = "United Kingdom",
            DateOfBirth = new DateTime(1991, 8, 2),
            IDNumber = "12345",
            CustomQuestionAnswers = null
        };

        var mockRepository = new Mock<IApplicationFormRepository>();
        mockRepository.Setup(repo => repo.AddNewApplicationAsync(It.IsAny<Application>())).ReturnsAsync(new Application());
        var service = new ApplicationFormService(mockRepository.Object);

        // Act
        var result = await service.SubmitApplication(request);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal("Successful", result.Message);
        mockRepository.Verify(repo => repo.AddNewApplicationAsync(It.IsAny<Application>()), Times.Once);
        mockRepository.Verify(repo => repo.SaveChangesAsync(null, false), Times.Once);
    }
}
