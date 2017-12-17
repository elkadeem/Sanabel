using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.DataAccessLayer
{
    [Table("Users", Schema = "Security")]
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [StringLength(200)]
        public string PasswordHash { get; set; }

        [StringLength(200)]
        public string SecurityStamp { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public bool IsPhoneConfirmed { get; set; }

        public bool IsLocked { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? LockedOutDate { get; set; }

        public int AccessFailedCount { get; set; }

        public bool EnableTowFactorAuthentication { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime UpdatedDate { get; set; }
        
        public virtual ICollection<UserClaim> Claims { get; set; }
        
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        
        public virtual ICollection<Role> Roles { get; set; }
               
    }
}
