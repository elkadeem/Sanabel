using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSettings.DAL
{
    [Table("Places", Schema = "Common")]
    public class Place
    {
        public Place()
        {
            ChildPlaces = new HashSet<Place>();
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

        [Required]
        public byte PlaceTypeId { get; set; }

        [Required]
        public int CountryId { get; set; }
        
        public int? ParentPlaceId { get; set; }

        [ForeignKey("ParentPlaceId")]
        public Place ParentPlace { get; set; }

        [ForeignKey("ParentPlaceId")]
        public ICollection<Place> ChildPlaces { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }
    }
}
