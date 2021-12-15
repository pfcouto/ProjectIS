using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOne.Models
{
    public class VCardUserPassword
    {
        public string Phone_number { get; set; }
        public string User_password { get; set; }
        public int User_id { get; set; }
        public int External_entity_id { get; set; }
        public decimal Max_debit { get; set; }
        public decimal Earning_percentage { get; set; }
    }
}