using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickUpSports.Models
{
    public class PlayerCreateViewModel
    {
        public Player player { get; set; }

        public string SportInterestOne { get; set; }
        public string SportInterestTwo { get; set; }
        public string SportInterestThree { get; set; }
        public string SportInterestFour { get; set; }
        public string SportInterestFive { get; set; }

    }
}