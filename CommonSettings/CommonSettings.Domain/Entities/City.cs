﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSettings.Domain.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string NameEn { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        public int RegionId { get; set; }

        public Region Region { get; set; }

        public List<District> Districts { get; set; }
    }
}
