using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuider.Models
{
    public class CreateVM
    {

 
        public User _user { get; set; }
        public Credential _credential { get; set; }
        public Restaurant _restaurant { get; set; }
        public Hotel _hotel { get; set; }
        public RentCar _rentcar { get; set; }



    }
}
