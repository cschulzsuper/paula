using System.ComponentModel.DataAnnotations;

namespace Super.Paula.Validation
{
    public class KebabCaseAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }

            return KebabCaseValidator.IsValid(value);
        }
    }
}
