using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.DataAccessLayer
{
    [Table("UserClaims", Schema = "Security")]
    public class UserClaim
    {
        [Key]
        public int ClaimId { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string ClaimType { get; set; }

        [StringLength(200)]
        public string ClaimValue { get; set; }
        
        public virtual User User { get; set; }
    }
}
