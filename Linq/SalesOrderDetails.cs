using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class SalesOrderDetail
    {
        public int SalesOrderID { get; set; } // Part of Primary Key
        public int SalesOrderDetailID { get; set; } // Part of Primary Key (Identity)
        public short OrderQty { get; set; } // smallint, Not Nullable
        public int ProductID { get; set; } // Not Nullable
        public decimal UnitPrice { get; set; } // money, Not Nullable
        public decimal UnitPriceDiscount { get; set; } // money, Not Nullable
        public decimal LineTotal { get; set; } // Computed column
        public Guid Rowguid { get; set; } // uniqueidentifier, Not Nullable
        public DateTime ModifiedDate { get; set; } // datetime, Not Nullable
    }
}