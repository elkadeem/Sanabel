namespace Sanable.Cases.Domain.Model
{
    public enum Genders : byte
    {
        Male = 1,
        Female = 2,
    }

    public enum CaseStatus : byte
    {
        New = 1,
        Approved=2,
        Rejected = 3,
        Suspended = 4,
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
