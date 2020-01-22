using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PickUpSports.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        [Display(Name = "Event Name")]
        public string EventName { get; set; }
        [Display(Name = "Sport")]
        public string SportsName { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public int ZipCode { get; set; }
        [Display(Name = "Skill Level")]
        public int SkillLevel { get; set; }
        [Display(Name = "Player Rating")]
        public int PlayerRating { get; set; }
        [Display(Name = "Date")]
        public string DateOfEvent { get; set; }
        [Display(Name = "Time")]
        public string TimeOfEvent { get; set; }
        [Display(Name = "Maximum Players")]
        public int MaximumPlayers { get; set; }
        [Display(Name ="Current Players")]
        public int CurrentPlayers { get; set; }
        [Display(Name = "Event Is Full")]
        public bool IsFull { get; set; }
       
        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        

    }

}