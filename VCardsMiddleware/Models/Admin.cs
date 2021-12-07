using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VCardsMiddleware.Models
{
    public class Admin
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public char Enabled { get; set; }
    }
}