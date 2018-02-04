using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Security.Application
{
    public class ViewUserViewModel
    {
        public Guid UserId { get; set; }

        [Display(Name = "UserName", ResourceType = typeof(Localization.SecurityResource))]
        public string UserName { get; set; }

        [Display(Name = "FullName", ResourceType = typeof(Localization.SecurityResource))]
        public string FullName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Localization.SecurityResource))]
        public string Email { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(Localization.SecurityResource))]
        public string Phone { get; set; }

        [Display(Name = "IsLockOut", ResourceType = typeof(Localization.SecurityResource))]
        public bool IsLockOut { get; internal set; }

        [Display(Name = "Roles", ResourceType = typeof(Localization.SecurityResource))]
        public Dictionary<Guid, string> Roles { get; set; }
    }
}
