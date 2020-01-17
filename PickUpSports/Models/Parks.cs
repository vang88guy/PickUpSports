using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickUpSports.Models
{
    public class Parks
    { 
        [Key]        
        public int Id { get; set; }
        [Required(ErrorMessage = "Park Name is required")]
        public string ParkName { get; set; }
        [Required(ErrorMessage = "Zip Code is required")]
        public string ZipCode { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
       
    }


}