using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankTwo.Models
{
    public class VCard
    {
        public string Phone_number { get; set; }
        public int User_id { get; set; }
        public decimal Balance { get; set; }
        public decimal Max_debit { get; set; }
        public decimal Earning_percentage { get; set; }
    }
}