using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSettings.DAL
{
    [Table("Countries", Schema = "Common")]
    public class Country
    {
        public Country()
        {
            Places = new HashSet<Place>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string NameEn { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        public virtual ICollection<Place> Places { get; set; }
    }
}
