using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PickUpSports.Models
{
    public class PlayerEvent
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}