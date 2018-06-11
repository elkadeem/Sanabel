using BusinessSolutions.Common.Core.Entities;
using BusinessSolutions.Common.Infra.Validation;
using CommonSettings.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanable.Cases.Domain.Model
{
    public class Case : Entity<Guid>
    {

        public Case()
        {
            Id = Guid.NewGuid();
            CaseResearchs = new HashSet<CaseResearch>();
            CaseActions = new HashSet<CaseAction>();
            CaseAids = new HashSet<Aid>();
        }

        public string Name { get; set; }

        public int CityId { get; set; }

        public int? DistrictId { get; set; }

        public string Phone { get; set; }

        public Genders Gender { get; set; }

        public CaseTypes CaseType { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public virtual ICollection<CaseResearch> CaseResearchs { get; private set; }

        public City City { get; set; }

        public District District { get; set; }

        public CaseStatus CaseStatus { get; set; }

        public DateTime? CaseSuspensionDate { get; set; }

        public virtual ICollection<CaseAction> CaseActions { get; set; }

        public virtual ICollection<Aid> CaseAids { get; private set; }

        public void AddAid(AidTypes aidType, string description
            , DateTime aidDate, double amount, string notes)
        {
            Guard.StringIsNull<ArgumentNullException>(description, nameof(description));
            Guard.LessThanZero(amount, nameof(amount));

            if (aidType == AidTypes.Finacial)
                Guard.LessThanOrEqualZero(amount, nameof(amount));

            var aid = new Aid
            {
                AidAmount = amount,
                AidDate = aidDate,
                AidDescription = description,
                AidType = aidType,
                Id = Guid.NewGuid(),
                CaseId = Id,
                CreatedDate = DateTime.Now,
            };

            CaseAids.Add(aid);
        }

        public void UpdateAid(Guid aidId, string description, DateTime aidDate
            , double amount, string notes)
        {
            Guard.GuidIsEmpty<ArgumentNullException>(aidId, nameof(aidId));
            Guard.StringIsNull<ArgumentNullException>(description, nameof(description));
            
            var aid = CaseAids.FirstOrDefault(c => c.Id == aidId);
            if (aid == null)
                throw new ArgumentException("Aid is not exist.", nameof(aidId));

            if (aid.AidType == AidTypes.Finacial)
                Guard.LessThanOrEqualZero(amount, nameof(amount));

            aid.AidAmount = amount;
            aid.AidDate = aidDate;
            aid.AidDescription = description;            
            aid.UpdatedDate = DateTime.Now;
        }

        public void DeleteAid(Guid aidId)
        {
            Guard.GuidIsEmpty<ArgumentNullException>(aidId, nameof(aidId));
            
            var aid = CaseAids.FirstOrDefault(c => c.Id == aidId);
            if (aid == null)
                throw new ArgumentException("Aid is not exist.", nameof(aidId));

            CaseAids.Remove(aid);
        }
    }
}
