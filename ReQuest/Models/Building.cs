using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReQuest.Models
{
    public class Building
    {
        [Key]
        public int BuildingId { get; set; }

        [Required]
        public string BuildingLetter { get; set; }

        [Required]
        [MaxLength(255)]
        public string BuildingDepartment { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}