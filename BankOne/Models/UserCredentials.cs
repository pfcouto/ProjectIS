using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOne.Models
{
    public class UserCredentials
    {
        public string OldPassword { get; set; } 
        public string Password { get; set; } 
        public string ConfirmationCode { get; set; } 

    }
}