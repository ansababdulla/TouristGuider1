//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TouristGuider.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FoodOrder
    {
        public long FdOdrID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> isPaid { get; set; }
        public Nullable<long> UserID { get; set; }
        public string Ttl { get; set; }
    
        public virtual User User { get; set; }
    }
}
