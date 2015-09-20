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
    
    public partial class Invoice
    {
        public Invoice()
        {
            this.Sales = new HashSet<Sale>();
        }
    
        public int InvoiceId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<float> TotalAmount { get; set; }
        public string DateTime { get; set; }
        public Nullable<int> UserId { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
