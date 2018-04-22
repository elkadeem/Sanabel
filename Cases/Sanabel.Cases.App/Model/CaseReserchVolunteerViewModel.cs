using Sanabel.Cases.App.Resources;
using System.ComponentModel.DataAnnotations;

namespace Sanabel.Cases.App.Model
{
    public class CaseReserchVolunteerViewModel
    {        
        public int VolunteerId { get; set; }

        [Display(Name = "Volunteer", ResourceType = typeof(CasesResource))]
        public string VolunteerName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(CasesResource))]
        public string Phone { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(CasesResource))]
        public string Email { get; set; }
    }
}