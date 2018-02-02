using BusinessSolutions.Common.Infra.Validation;
using System;

namespace Sanabel.Security.Domain
{
    public class Claim
    {
        private Claim()
        {
        }

        public Claim(Guid userId, string claimType, string claimValue)
        {
            Guard.StringIsNull<ArgumentNullException>(claimType, nameof(claimType));
            this.UserId = userId;
            this.ClaimType = claimType;
            this.ClaimValue = claimValue;
        }

        public int ClaimId { get; private set; }

        public Guid UserId { get; private set; }

        public string ClaimType { get; private set; }

        public string ClaimValue { get; private  set; }
    }
}