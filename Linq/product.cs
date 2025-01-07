using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class Product
    {
        public string Name { get; set; }
        public int ProductID { get; set; }

        public string ProductNumber { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public decimal? Weight { get; set; }

        public decimal? StandardCost { get; set; }
        public decimal? ListPrice { get; set; }

        public int ProductCategoryID { get; set; }
        public int ProductModelID { get; set; }
        public DateTime? SellStartDate { get; set; }
        public DateTime? SellEndDate { get; set; }
        public DateTime? DiscontinuedDate { get; set; }




    }
}