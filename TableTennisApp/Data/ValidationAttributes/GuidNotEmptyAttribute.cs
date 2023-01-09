using System.ComponentModel.DataAnnotations;

namespace TableTennisApp.Data.ValidationAttributes
{
    public class GuidNotEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is Guid guid)
            {
                if (guid != Guid.Empty)
                    return true;
            }
            return false;
        }
    }
}
