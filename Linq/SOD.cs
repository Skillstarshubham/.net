using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;
 
namespace ConsoleApp5

{

    internal class SalesOrderHeader

    {

        public int SalesOrderID { get; set; } // Primary Key

        public byte RevisionNumber { get; set; } // tinyint

        public DateTime OrderDate { get; set; } // datetime, Not Nullable

        public DateTime DueDate { get; set; } // datetime, Not Nullable

        public DateTime? ShipDate { get; set; } // datetime, Nullable

        public byte Status { get; set; } // tinyint, Not Nullable

        public bool OnlineOrderFlag { get; set; } // Assuming dbo.Flag maps to boolean

        public string SalesOrderNumber { get; set; } // Computed column

        public string? PurchaseOrderNumber { get; set; } // Nullable, dbo.OrderNumber

        public string? AccountNumber { get; set; } // Nullable, dbo.AccountNumber

        public int CustomerID { get; set; } // Not Nullable

        public int? ShipToAddressID { get; set; } // Nullable

        public int? BillToAddressID { get; set; } // Nullable

        public string ShipMethod { get; set; } // nvarchar(50), Not Nullable

        public string? CreditCardApprovalCode { get; set; } // Nullable, varchar(15)

        public decimal SubTotal { get; set; } // money, Not Nullable

        public decimal TaxAmt { get; set; } // money, Not Nullable

        public decimal Freight { get; set; } // money, Not Nullable

        public decimal TotalDue { get; set; } // Computed column

        public string? Comment { get; set; } // nvarchar(max), Nullable

        public Guid Rowguid { get; set; } // uniqueidentifier, Not Nullable

        public DateTime ModifiedDate { get; set; } // datetime, Not Nullable

    }

}
