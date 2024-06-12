namespace BoyumIT.TodoApi.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ValidEnumAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var enumType = value.GetType();
            bool valid = Enum.IsDefined(enumType, value);
            if (!valid)
            {
                return new ValidationResult($"Invalid value for enum {enumType.Name}");
            }
            return ValidationResult.Success;
        }
    }

}
