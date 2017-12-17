using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.DataAccessLayer
{
    [Table("UserLogins", Schema = "Security")]
    public class UserLogin
    {
        [Key, ForeignKey("User")]
        [Column(Order =1)]
        public Guid UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required]
        [StringLength(100)]
        public string LoginProvider { get; set; }
                
        [Required]
        [StringLength(50)]        
        public string ProviderKey { get; set; }
        
        public virtual User User { get; set; }
    }
}
