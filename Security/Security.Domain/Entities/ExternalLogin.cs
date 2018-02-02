using System;

namespace Sanabel.Security.Domain
{
    public class ExternalLogin
    {
        public Guid UserId { get; set; }

        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }

        public User User { get; set; }
    }
}