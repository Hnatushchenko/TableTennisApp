using System.ComponentModel.DataAnnotations;

namespace TableTennisApp.Data.ValidationAttributes
{
    public class PropertiesAreNotEqualAttribute : ValidationAttribute
    {
        private readonly string _comparisonPropertyName;

        public PropertiesAreNotEqualAttribute(string comparisonProperty)
        {
            _comparisonPropertyName = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is Guid guidValue)
            {
                var property = validationContext.ObjectType.GetProperty(_comparisonPropertyName);

                if (property == null)
                    throw new ArgumentException("Property with this name not found");

                var comparisonValue = property.GetValue(validationContext.ObjectInstance) as Guid?;

                if (guidValue == comparisonValue && guidValue != Guid.Empty)
                    return new ValidationResult("Переможець, та той хто програв повинні бути різними гравцями.");

                return null;
            }
            throw new ArgumentException("Attribute can be applied only to Guid property");
        }
    }
}
