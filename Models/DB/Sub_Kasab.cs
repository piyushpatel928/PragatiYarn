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
    
    public partial class Sub_Kasab
    {
        public Sub_Kasab()
        {
            this.AllMaterials = new HashSet<AllMaterial>();
            this.Stocks = new HashSet<Stock>();
        }
    
        public int Sub_KasabId { get; set; }
        public string Name { get; set; }
        public string Length { get; set; }
        public Nullable<float> Price { get; set; }
        public Nullable<int> MaterialTypeId { get; set; }
    
        public virtual ICollection<AllMaterial> AllMaterials { get; set; }
        public virtual MaterialType MaterialType { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
