using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Windows
{
    public static class ObjectValidator
    {
        public static IEnumerable<ValidationResult> Validate ( object value)
        {
            var context = new ValidationContext(value);
            var errors = new Collection<ValidationResult>();
            Validator.TryValidateObject(value, context, true);

            return errors;

        }
    }
}
