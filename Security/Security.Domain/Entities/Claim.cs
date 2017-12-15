using System;

namespace Security.Domain
{
    public class Claim
    {
        public int ClaimId { get; set; }

        public Guid UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public User User { get; set; }
    }
}