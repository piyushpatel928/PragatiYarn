using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.ViewModel
{
    public class AddMaterialViewModel
    {
        public int MaterialId
        {
            get;
            set;
        }
        public int SubMaterialId
        {
            get;
            set;
        }
        public string MaterialCode
        {
            get;
            set;
        }
        public float Price
        {
            get;
            set;
        }
        public string MaterialImage
        {
            get;
            set;
        }
        public string MaterialImagePath
        {
            get;
            set;
        }
    }
}