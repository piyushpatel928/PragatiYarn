//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PragatiYarn.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerPaidHistory
    {
        public int PaidId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<float> Amount { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual User User { get; set; }
    }
}
