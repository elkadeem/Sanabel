using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSolutions.Common.Core.Entities
{
    public class Entity<Key> : IEntity<Key>, IEquatable<Entity<Key>>
    {
        public virtual Key Id { get; set; }

        public override bool Equals(object obj)
        {
            var entity = obj as Entity<Key>;
            if(entity != null)
            {
                return this.Equals(entity);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public virtual bool Equals(Entity<Key> other)
        {
            if (other == null)
                return false;

            return this.Id.Equals(other.Id);
        }


    }
}
