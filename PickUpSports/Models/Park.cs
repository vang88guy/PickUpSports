﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickUpSports.Models
{
    public class Park
    { 
        [Key]        
        public int Id { get; set; }
        public string ParkName { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
       
    }


}