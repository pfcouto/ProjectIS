using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOne.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Photo_url { get; set; }
        public string Confirmation_code { get; set; }
        public string Phone_number { get; set; }
    }
}