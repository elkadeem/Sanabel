using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanable.Cases.Domain.Model
{
    public enum Genders : byte
    {
        Male = 1,
        Female = 2,
    }

    public enum CaseTypes : byte
    {
        Single = 1,
        Family = 2,
        widow = 3,
        Divorced = 4,
        Orphans = 5,
    }

    public enum ResearchTypes : byte
    {
        Prisoner = 1,
        widow = 2,
        Others = 3,
    }

    public enum JobTypes : byte
    {
        FullTime = 1,
        PartialTime = 2,
        Jobless = 3,
    }

    public enum HealthStatuses : byte
    {
        Sound = 1,
        Sick = 2,
        Disabled = 3,
        SemiDisabled = 4,
    }    
}
