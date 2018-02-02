using BusinessSolutions.Common.Infra.Validation;
using System;

namespace Sanabel.Security.Domain
{
    public class ExternalLogin
    {
        private ExternalLogin()
        {
        }

        public ExternalLogin(Guid userId, string loginProvider, string providerKey)
        {
            Guard.StringIsNull<ArgumentNullException>(loginProvider, nameof(loginProvider));
            Guard.StringIsNull<ArgumentNullException>(providerKey, nameof(providerKey));
            this.UserId = UserId;
            this.LoginProvider = loginProvider;
            this.ProviderKey = providerKey;
        }

        public Guid UserId { get; private set; }

        public string LoginProvider { get; private set; }

        public string ProviderKey { get; private set; }
        
    }
}