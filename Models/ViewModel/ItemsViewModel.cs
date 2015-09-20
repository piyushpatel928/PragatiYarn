using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.ViewModel
{
    public class ItemsViewModel
    {
        public int SerailNo
        {
            get;
            set;
        }
        public string ItemName
        {
            get;
            set;
        }
        public string Piece
        {
            get;
            set;
        }
        public float Rate
        {
            get;
            set;
        }
        public float Amount
        {
            get;
            set;
        }
        public string lstsubitems
        {
            get;
            set;
        }
        public float totpiece
        {
            get;
            set;
        }
        public float totAmount
        {
            get;
            set;
        }
    }
}