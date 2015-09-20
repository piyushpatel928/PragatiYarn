using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.ViewModel
{
    public class RateViewModel
    {
        public int TypeId
        {
            get;
            set;
        }
        public int SubMaterialId
        {
            get;
            set;
        }
        public float NewRate
        {
            get;
            set;
        }
    }
}