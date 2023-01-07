using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace TableTennisApp.Data.ValidationAttributes
{
    public class CollectionNotEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is ICollection collection)
            {
                if (collection.Count > 0)
                    return true;
            }
            return false;
        }
    }
}
