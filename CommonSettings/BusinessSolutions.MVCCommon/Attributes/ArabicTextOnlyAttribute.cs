using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace BusinessSolutions.MVCCommon.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ArabicTextOnlyAttribute : RegularExpressionAttribute
    {
        private new  const string Pattern = @"^[\u0600-\u06FF\0-9s]*$";

        static ArabicTextOnlyAttribute()
        {
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(ArabicTextOnlyAttribute)
                , typeof(RegularExpressionAttributeAdapter));
        }

        public ArabicTextOnlyAttribute() : base(Pattern)
        {
        }
    }
}
