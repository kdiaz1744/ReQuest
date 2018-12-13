using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReQuest.Models
{
    public class Material
    {
        [Key, Index(Order = 1)]
        public int MaterialId { get; set; }

        [Required]
        [MaxLength(255)]
        public string MaterialName { get; set; }

        [Required]
        [MaxLength(255)]
        public string MaterialDescription { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}