using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReQuest.Models
{
    public class Calendar
    {
        
        [Key, Index(Order = 1)]
        public int ActivityId { get; set; }

        public string ActivityUserID { get; set; }

        public string ActivityStatus { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Activity Name")]
        public string ActivityName { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Description")]
        public string ActivityDescription { get; set; }

        [Display(Name = "Start Datetime")]
        public DateTime ActivityStartTime { get; set; }

        [Display(Name = "End Datetime")]
        public DateTime ActivityEndTime { get; set; }

        [Display(Name = "Room")]
        public virtual string RoomNumber { get; set; }
        
    }
}