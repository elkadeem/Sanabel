using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Infra.Attributes
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private ResourceManager _resourceManager;
        private string _resourceName;
        public LocalizedDescriptionAttribute(string resourceName, Type resourceType)
        {
            if (string.IsNullOrEmpty(resourceName))
                throw new ArgumentNullException("resourceName");
            if (resourceType == null)
                throw new ArgumentNullException("resourceType");
            _resourceManager = new ResourceManager(resourceType);
            _resourceName = resourceName;
        }

        public override string Description
        {
            get
            {
                string description = _resourceManager.GetString(_resourceName);
                return string.IsNullOrEmpty(description) ? _resourceName : description;  
            }
        }

    }
}
