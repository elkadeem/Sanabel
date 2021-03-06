﻿using BusinessSolutions.Common.Infra.Attributes;
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

    public enum ResearchTypes : byte
    {
        [LocalizedDescription("Prisoner", typeof(CasesResource))]
        Prisoner = 1,
        [LocalizedDescription("widow", typeof(CasesResource))]
        widow = 2,
        [LocalizedDescription("Others", typeof(CasesResource))]
        Others = 3,
    }

    public enum JobTypes : byte
    {
        [LocalizedDescription("FullTime", typeof(CasesResource))]
        FullTime = 1,
        [LocalizedDescription("PartialTime", typeof(CasesResource))]
        PartialTime = 2,
        [LocalizedDescription("Jobless", typeof(CasesResource))]
        Jobless = 3,
    }

    public enum HealthStatuses : byte
    {
        [LocalizedDescription("Sound", typeof(CasesResource))]
        Sound = 1,
        [LocalizedDescription("Sick", typeof(CasesResource))]
        Sick = 2,
        [LocalizedDescription("Disabled", typeof(CasesResource))]
        Disabled = 3,
        [LocalizedDescription("SemiDisabled", typeof(CasesResource))]
        SemiDisabled = 4,
    }

    public enum CaseStatus : byte
    {
        [LocalizedDescription("New", typeof(CasesResource))]
        New = 1,
        [LocalizedDescription("Approved", typeof(CasesResource))]
        Approved = 2,
        [LocalizedDescription("Rejected", typeof(CasesResource))]
        Rejected = 3,
        [LocalizedDescription("Suspended", typeof(CasesResource))]
        Suspended = 4,
    }

    public enum AidTypes : byte
    {
        [LocalizedDescription("Finacial", typeof(CasesResource))]
        Finacial = 1,
        [LocalizedDescription("Medical", typeof(CasesResource))]
        Medical = 2,
        [LocalizedDescription("Legal", typeof(CasesResource))]
        Legal = 3
    }
}
