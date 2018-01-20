using BusinessSolutions.Common.Infra.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanabel.Cases.App;
using Sanabel.Cases.App.Resources;

namespace Sanabel.Cases.App.Model
{
    public enum Genders : byte
    {      
        [LocalizedDescription("Male", typeof(CasesResource))]
        Male = 1,
        [LocalizedDescription("Female", typeof(CasesResource))]
        Female = 2,
    }

    public enum CaseTypes : byte
    {
        [LocalizedDescription("Single", typeof(CasesResource))]
        Single = 1,
        [LocalizedDescription("Family", typeof(CasesResource))]
        Family = 2,
        [LocalizedDescription("Widow", typeof(CasesResource))]
        Widow = 3,
        [LocalizedDescription("Divorced", typeof(CasesResource))]
        Divorced = 4,
        [LocalizedDescription("Orphans", typeof(CasesResource))]
        Orphans = 5,
    }
}
