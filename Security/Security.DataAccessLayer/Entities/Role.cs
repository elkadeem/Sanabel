using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.DataAccessLayer
{
    [Table("Roles", Schema ="Security")]
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }

        [Required]
        [StringLength(100)]
        public string RoleName { get; set; }

        [Required]
        [StringLength(100)]
        public string RoleNameAr { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
    }
}
