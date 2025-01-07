using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class Customer
    {
        public int CustomerID { get; set; } // Primary Key
        public bool NameStyle { get; set; } // Assuming dbo.NameStyle maps to a boolean
        public string? Title { get; set; } // Nullable nvarchar(8)
        public string FirstName { get; set; } // Not nullable
        public string? MiddleName { get; set; } // Nullable
        public string LastName { get; set; } // Not nullable
        public string? Suffix { get; set; } // Nullable nvarchar(10)
        public string? CompanyName { get; set; } // Nullable nvarchar(128)
        public string? SalesPerson { get; set; } // Nullable nvarchar(256)
        public string? EmailAddress { get; set; } // Nullable nvarchar(50)
        public string? Phone { get; set; } // Nullable dbo.Phone
        public string PasswordHash { get; set; } // Not nullable
        public string PasswordSalt { get; set; } // Not nullable
        public Guid Rowguid { get; set; } // uniqueidentifier
        public DateTime ModifiedDate { get; set; } // datetime
    }
}