using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOne.Models
{
    public class VCardTransactionDetails
    {
        public VCardTransactionDetails()
        {
            Category_id = -1;
            IsChanged = false;
        }

        private bool IsChanged;
        private int Category_id_p;
        public string Confirmation_code { get; set; }
        public int Category_id { get { return Category_id_p; } set { IsChanged = true; Category_id_p = value; } }
        public string Description { get; set; }
        public bool GetIsChanged()
        {
            return IsChanged;
        }
    }
}