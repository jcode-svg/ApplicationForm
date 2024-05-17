using System.ComponentModel.DataAnnotations;

namespace ApplicationFormPractice.Domain.ValidationAttributes;

public class ConditionalMaxChoicesAllowedAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;
    private readonly string _requiredValue;

    public ConditionalMaxChoicesAllowedAttribute(string comparisonProperty, string requiredValue)
    {
        _comparisonProperty = comparisonProperty;
        _requiredValue = requiredValue;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var comparisonPropertyValue = validationContext.ObjectType.GetProperty(_comparisonProperty)
            .GetValue(validationContext.ObjectInstance, null) as string;

        var maxChoicesAllowed = value as int?;

        if (comparisonPropertyValue == _requiredValue && maxChoicesAllowed == 0)
        {
            return new ValidationResult(ErrorMessage);
        }

        if (comparisonPropertyValue != _requiredValue && maxChoicesAllowed != 0)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
