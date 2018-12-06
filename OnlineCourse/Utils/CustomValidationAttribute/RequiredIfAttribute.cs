using System.ComponentModel.DataAnnotations;

namespace OnlineCourse.Panel.Utils.CustomValidationAttribute
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        private string PropertyName { get; set; }
        private object DesiredValue { get; set; }
        private readonly RequiredAttribute _innerAttribute;

        public RequiredIfAttribute(string propertyName, object desiredvalue)
        {
            PropertyName = propertyName;
            DesiredValue = desiredvalue;
            _innerAttribute = new RequiredAttribute();
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var propertyInfo = context.ObjectInstance.GetType().GetProperty(PropertyName);
            if (propertyInfo == null) return ValidationResult.Success;
            var dependentValue = propertyInfo.GetValue(context.ObjectInstance, null);
            if (dependentValue.ToString() == DesiredValue.ToString())
                return ValidationResult.Success;
            return !_innerAttribute.IsValid(value) ? new ValidationResult(FormatErrorMessage(context.DisplayName), new[] { context.MemberName }) : ValidationResult.Success;
        }
    }

}
