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
    
    public partial class MaterialType
    {
        public MaterialType()
        {
            this.AllMaterials = new HashSet<AllMaterial>();
            this.Stocks = new HashSet<Stock>();
            this.Sub_Accralic = new HashSet<Sub_Accralic>();
            this.Sub_FizingPaper = new HashSet<Sub_FizingPaper>();
            this.Sub_Kasab = new HashSet<Sub_Kasab>();
            this.SubMaterialTypes = new HashSet<SubMaterialType>();
        }
    
        public int MaterialTypeId { get; set; }
        public string MaterialName { get; set; }
    
        public virtual ICollection<AllMaterial> AllMaterials { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<Sub_Accralic> Sub_Accralic { get; set; }
        public virtual ICollection<Sub_FizingPaper> Sub_FizingPaper { get; set; }
        public virtual ICollection<Sub_Kasab> Sub_Kasab { get; set; }
        public virtual ICollection<SubMaterialType> SubMaterialTypes { get; set; }
    }
}