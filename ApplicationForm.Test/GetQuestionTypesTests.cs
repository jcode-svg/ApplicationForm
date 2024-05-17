using ApplicationFormPractice.Application.Implementation;
using ApplicationFormPractice.Domain.Entities;
using ApplicationFormPractice.Domain.RepositoryContracts;
using Moq;

namespace ApplicationForm.Test;

public class GetQuestionTypesTests
{
    [Fact]
    public void GetQuestionTypes_ReturnsAllQuestionTypes()
    {
        // Arrange
        var mockRepository = new Mock<IApplicationFormRepository>();
        mockRepository.Setup(repo => repo.AddNewCustomQuestionAsync(It.IsAny<CustomQuestion>()))
                      .ReturnsAsync(new CustomQuestion());
        var service = new ApplicationFormService(mockRepository.Object);

        // Act
        var result = service.GetQuestionTypes();

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(6, result.ResponseObject.Count);
        Assert.Contains("Paragraph", result.ResponseObject);
        Assert.Contains("YesOrNo", result.ResponseObject);
        Assert.Contains("Dropdown", result.ResponseObject);
        Assert.Contains("MultipleChoice", result.ResponseObject);
        Assert.Contains("Date", result.ResponseObject);
        Assert.Contains("Number", result.ResponseObject);
    }
}
