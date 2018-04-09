using BusinessSolutions.MVCCommon.Common;
using Sanabel.Cases.App.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sanabel.Cases.App.Model
{
    public class SearchCaseReserchViewModel : BaseSearchViewModel<CaseResearchViewModel>
    {
        public SearchCaseReserchViewModel()
        {
        }

        public SearchCaseReserchViewModel(int pageSize) : base(pageSize)
        {
        }

        [Display(Name = "CaseName", ResourceType = typeof(CasesResource))]
        public string CaseName { get; set; }

        [Display(Name = "FromDate", ResourceType = typeof(CasesResource))]
        public DateTime FromDate { get; set; }

        [Display(Name = "ToDate", ResourceType = typeof(CasesResource))]
        public DateTime ToDate { get; set; }

        [Display(Name = "Volunteer", ResourceType = typeof(CasesResource))]
        public int VolunteerId { get; set; }

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

    }
}
