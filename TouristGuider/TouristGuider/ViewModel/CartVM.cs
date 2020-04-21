using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuider.Models
{
    public class CartVM
    {
        public IEnumerable<FoodOrder> _fdodr { get; set; }
        public IEnumerable<FoodOrderDetail> _fdodrdtls { get; set; }
    }
}