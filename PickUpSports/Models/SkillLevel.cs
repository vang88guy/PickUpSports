using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace PickUpSports.Models
{
    public class SkillLevel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Skill Level")]
        public string Level { get; set; }
    }
}