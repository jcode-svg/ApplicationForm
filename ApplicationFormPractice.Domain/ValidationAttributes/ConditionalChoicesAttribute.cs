using System.ComponentModel.DataAnnotations;

namespace ApplicationFormPractice.Domain.ValidationAttributes;

public class ConditionalChoicesAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;
    private readonly string[] _allowedValues;

    public ConditionalChoicesAttribute(string comparisonProperty, string[] allowedValues)
    {
        _comparisonProperty = comparisonProperty;
        _allowedValues = allowedValues;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var comparisonPropertyValue = validationContext.ObjectType.GetProperty(_comparisonProperty)
            .GetValue(validationContext.ObjectInstance, null) as string;

        var choices = value as List<string>;

        if (_allowedValues.Contains(comparisonPropertyValue) && choices == null)
        {
            return new ValidationResult(ErrorMessage);
        }

        if (!_allowedValues.Contains(comparisonPropertyValue) && choices != null && choices.Any())
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
