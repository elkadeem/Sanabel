
using Sanabel.Cases.App.Resources;
using System;

using System.ComponentModel.DataAnnotations;


namespace Sanabel.Cases.App.Model
{
    public class CaseActionViewModel
    {
        public CaseViewModel Case { get; set; }

        public Guid CaseId { get; set; }

        public string Action { get; set; }

        public string Comment { get; set; }
        
        public DateTime CaseActionDate { get; set; }

        public DateTime CaseSuspensionDate { get; set; }

        public CaseStatusTypes oldStatus { get; set; }

        public CaseStatusTypes Status { get; set; }
    
        public string CreatedBy { get; set; }
    }
}

