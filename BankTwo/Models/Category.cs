using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankTwo.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public char Type { get; set; }
        public int User_id { get; set; }
    }
}