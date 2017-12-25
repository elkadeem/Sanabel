using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core.Entities
{
    public interface IEntity<Key>
    {
        Key Id { get; set; }
    }
}
