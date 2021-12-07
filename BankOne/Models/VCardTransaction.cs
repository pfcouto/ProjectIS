using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOne.Models
{
    public class VCardTransaction {
        public VCardTransaction()
        {
            Category_id = 0;
        }

        public int Id { get; set; }
        public string VCard { get; set; }
        public DateTime Date { get; set; }
        public char Type { get; set; }
        public decimal Value { get; set; }
        public int Category_id { get; set; }
        public string Description { get; set; }
    }
}