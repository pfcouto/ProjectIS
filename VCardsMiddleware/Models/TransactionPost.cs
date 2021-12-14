using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VCardsMiddleware.Models
{
    public class TransactionPost
    {
        public TransactionPost()
        {
            Category_id = 0;
        }

        public string VCard { get; set; }
        public char Type { get; set; }
        public decimal Value { get; set; }
        public int Category_id { get; set; }
        public string Description { get; set; }
        public string Payment_reference { get; set; }
        public string Confirmation_code { get; set; }
    }
}