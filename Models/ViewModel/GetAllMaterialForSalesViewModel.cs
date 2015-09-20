using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.ViewModel
{
    public class GetAllMaterialForSalesViewModel
    {
        public int MaterialId
        {
            get;
            set;
        }
        public string MaterialCode
        {
            get;
            set;
        }
        public string MaterialImage
        {
            get;
            set;
        }
        public float stock
        {
            get;
            set;
        }
    }
}