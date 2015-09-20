using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.ViewModel
{
    public class SalesItemViewModel
    {
        public int MaterialId
        {
            get;
            set;
        }
        public int Piece
        {
            get;
            set;
        }
        public float Weight
        {
            get;
            set;
        }
        public int SubMaterialTypeId
        {
            get;
            set;
        }
        public int MaterialTypeId
        {
            get;
            set;
        }
        public string MaterialCode { get; set; }
    }
}