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
    
    public partial class RentCar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RentCar()
        {
            this.RentBookings = new HashSet<RentBooking>();
            this.Cars = new HashSet<Car>();
        }
    
        public long RtID { get; set; }
        public Nullable<long> CredID { get; set; }
        public string RtNm { get; set; }
        public string RtImg { get; set; }
    
        public virtual Credential Credential { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RentBooking> RentBookings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Car> Cars { get; set; }
    }
}
