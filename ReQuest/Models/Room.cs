using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReQuest.Models
{
    public class Room
    {
        [Key]
        [Required]
        public string RoomNumber { get; set; }

        [Required]
        public int RoomCapacity { get; set; }

        public Nullable<int> BuildingID { get; set; }
        public virtual Building Building { get; set; }

        public virtual ICollection<Material> Materials { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

    }
}