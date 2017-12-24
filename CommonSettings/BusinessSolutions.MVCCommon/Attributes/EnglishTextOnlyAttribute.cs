using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace BusinessSolutions.MVCCommon.Attributes
{
    public class EnglishTextOnlyAttribute : RegularExpressionAttribute
    {
        private new const string Pattern = @"^[A-Za-z0-9\s!@#$%^&*()_+=-`~\\\]\[{}|';:/.,?]*$";

        static EnglishTextOnlyAttribute()
        {            
            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(EnglishTextOnlyAttribute),
                typeof(RegularExpressionAttributeAdapter));
        }

        public EnglishTextOnlyAttribute(): base(Pattern)
        {
        }
    }
}
