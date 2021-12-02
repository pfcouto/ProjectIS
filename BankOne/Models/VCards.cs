using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOne.Models
{
    public class VCards
    {
        public int Phone_number { get; set; }
        public decimal Balance { get; set; }
        public decimal Max_debit { get; set; }
        public decimal Earning_percentage { get; set; }
    }
}