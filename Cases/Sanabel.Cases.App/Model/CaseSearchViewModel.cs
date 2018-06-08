using BusinessSolutions.MVCCommon.Common;
using Sanabel.Cases.App.Resources;
using System.ComponentModel.DataAnnotations;

namespace Sanabel.Cases.App.Model
{
    public class SearchCaseViewModel : BaseSearchViewModel<CaseViewModel>
    {
        public SearchCaseViewModel() : base()
        {
        }

        public SearchCaseViewModel(int pageSize) : base(pageSize)
        {
        }

        
        [Display(Name = "CaseName", ResourceType = typeof(CasesResource))]
        public string CaseName { get; set; }

        [Display(Name = "Country", ResourceType = typeof(CasesResource))]
        public int CountryId { get; set; }

        [Display(Name = "Region", ResourceType = typeof(CasesResource))]
        public int RegionId { get; set; }

        [Display(Name = "City", ResourceType = typeof(CasesResource))]
        public int CityId { get; set; }

        [Display(Name = "District", ResourceType = typeof(CasesResource))]
        public int DistrictId { get; set; }
        
        [Display(Name = "Phone", ResourceType = typeof(CasesResource))]
        public string Phone { get; set; }
        
        [Display(Name = "Gender", ResourceType = typeof(CasesResource))]
        public Genders Gender { get; set; }
        
        [Display(Name = "CaseType", ResourceType = typeof(CasesResource))]
        public CaseTypes CaseType { get; set; }

        [Display(Name = "Address", ResourceType = typeof(CasesResource))]
        public string Address { get; set; }

        [Display(Name = "Description", ResourceType = typeof(CasesResource))]
        public string Description { get; set; }

        [Display(Name = "Decision", ResourceType = typeof(CasesResource))]
        public string Decision { get; set; }

        [Display(Name = "CaseStatus", ResourceType = typeof(CasesResource))]
        public CaseStatus CaseStatus { get; set; }
    }
}
