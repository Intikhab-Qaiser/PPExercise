using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Promotion
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Discount { get; set; } 

         public Article Article { get; set; } = null;

        public bool IsChristmas { get; set; } = false;

        public bool IsClearance { get; set; } = false;
    }
}
