using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ReQuest.Models;

namespace ReQuest.Models
{
    public class Request
    {
        [Key, Index(Order = 1)]
        public int RequestId { get; set; }
        
        public string RequestUserID { get; set; }

        public string RequestStatus { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Activity Name")]
        public string RequestName { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Description")]
        public string RequestDescription { get; set; }

        [Display(Name = "Start Datetime")]
        public DateTime RequestStartTime { get; set; }

        [Display(Name = "End Datetime")]
        public DateTime RequestEndTime { get; set; }

        [Display(Name = "Room")]
        public virtual string RoomNumber { get; set; }
        public virtual Room Rooms { get; set; }

        public virtual Calendar Calendar { get; set; }
    }
}