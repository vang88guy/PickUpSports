using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PickUpSports.Models
{
    public class Sport
    {
        [Key]
        public int SportId { get; set; }
        [Display(Name ="Sport")]
        public string SportName { get; set; }
    }
}