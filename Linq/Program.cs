using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Addresses;
using ConsoleApp5;

namespace SqlConnectionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "data source=Amrit;initial catalog=AdventureWorksLT;persist security info=True; Integrated Security=SSPI;";

            // Retrieve addresses
            IEnumerable<Address> addresses = GetAddresses(connectionString);
            // Retrieve products
            IEnumerable<Product> products = GetProducts(connectionString);

            var fil = products.Where(p => p.ListPrice > 1000 & p.ListPrice < 1500).OrderByDescending(s => s.ListPrice).ThenBy(d => d.Name);

            var qsx = products.GroupBy(p => p.Color).Distinct();



            var sel1 = products.Where(s => s.SellEndDate == null).Select(p => new
            {

                Name = p.Name,
                p.ProductID
            });

            foreach (var c in sel1)
            {

                Console.WriteLine(c.ProductID);
            }


            var csting = products.Select(s => {
                if (int.TryParse(s.Size, out int result))
                {
                    return (int?)result;

                }
                return null;

            });

            foreach (var p in csting)
            {

                // Console.WriteLine(p.Value.ToString());

            }


            // LINQ Query: Filter addresses by City
            var filteredAddresses = addresses
                    .Where(a => a.CountryRegion == "Canada")
                    .OrderBy(a => a.AddressLine1)
                    .Select(a => new
                    {
                        a.AddressID,
                        col1 = a.AddressLine1 + a.AddressLine2 + a.CountryRegion + a.StateProvince,
                        a.City
                    });



            //// Display the filtered addresses
            foreach (var address in filteredAddresses)
            {
                //  Console.WriteLine($"ID: {address.AddressID}, AddressLine1: {address.col1}, City: {address.City}");
            }

        }

        static IEnumerable<Address> GetAddresses(string connectionString)
        {
            var addresses = new List<Address>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string address = "SELECT * FROM SalesLT.Address";
                string product = "Select * from SalesLT.Product";

                using (SqlCommand command = new SqlCommand(address, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            addresses.Add(new Address
                            {
                                AddressID = reader.GetInt32(0),
                                AddressLine1 = reader.GetString(1),
                                AddressLine2 = reader.IsDBNull(2) ? null : reader.GetString(2),
                                City = reader.GetString(3),
                                StateProvince = reader.GetString(4),
                                CountryRegion = reader.GetString(5),
                                PostalCode = reader.GetString(6)
                            });
                        }
                    }
                }

                connection.Close();
            }

            return addresses;
        }

        static IEnumerable<Product> GetProducts(string connectionString)
        {
            var products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                string product = "Select * from SalesLT.Product";
                using (SqlCommand command = new SqlCommand(product, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProductID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ProductNumber = reader.GetString(2),
                                Color = reader.IsDBNull(3) ? null : reader.GetString(3),
                                StandardCost = reader.GetDecimal(4),
                                ListPrice = reader.GetDecimal(5),
                                Size = reader.IsDBNull(6) ? null : reader.GetString(6),
                                Weight = reader.IsDBNull(7) ? null : reader.GetDecimal(7),
                                ProductCategoryID = reader.GetInt32(8),
                                ProductModelID = reader.GetInt32(9),
                                SellStartDate = reader.GetDateTime(10),
                                SellEndDate = reader.IsDBNull(11) ? null : reader.GetDateTime(11),

                            });
                        }
                    }
                }


                connection.Close();
            }

            return products;
        }

        static IEnumerable<SalesOrderDetail> GetSalesOrderDetails(string connectionString)
        {
            var salesOrderDetails = new List<SalesOrderDetail>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT SalesOrderID, SalesOrderDetailID, OrderQty, ProductID, UnitPrice, UnitPriceDiscount, LineTotal, Rowguid, ModifiedDate FROM SalesLT.SalesOrderDetail";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salesOrderDetails.Add(new SalesOrderDetail
                            {
                                SalesOrderID = reader.GetInt32(0),
                                SalesOrderDetailID = reader.GetInt32(1),
                                OrderQty = reader.GetInt16(2),
                                ProductID = reader.GetInt32(3),
                                UnitPrice = reader.GetDecimal(4),
                                UnitPriceDiscount = reader.GetDecimal(5),
                                LineTotal = reader.GetDecimal(6), // Computed column
                                Rowguid = reader.GetGuid(7),
                                ModifiedDate = reader.GetDateTime(8)
                            });
                        }
                    }
                }
            }

            return salesOrderDetails;
        }


        static IEnumerable<Customer> GetCustomers(string connectionString)
        {
            var customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CustomerID, NameStyle, Title, FirstName, MiddleName, LastName, Suffix, CompanyName, SalesPerson, EmailAddress, Phone, PasswordHash, PasswordSalt, Rowguid, ModifiedDate FROM SalesLT.Customer";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer
                            {
                                CustomerID = reader.GetInt32(0),
                                NameStyle = reader.GetBoolean(1),
                                Title = reader.IsDBNull(2) ? null : reader.GetString(2),
                                FirstName = reader.GetString(3),
                                MiddleName = reader.IsDBNull(4) ? null : reader.GetString(4),
                                LastName = reader.GetString(5),
                                Suffix = reader.IsDBNull(6) ? null : reader.GetString(6),
                                CompanyName = reader.IsDBNull(7) ? null : reader.GetString(7),
                                SalesPerson = reader.IsDBNull(8) ? null : reader.GetString(8),
                                EmailAddress = reader.IsDBNull(9) ? null : reader.GetString(9),
                                Phone = reader.IsDBNull(10) ? null : reader.GetString(10),
                                PasswordHash = reader.GetString(11),
                                PasswordSalt = reader.GetString(12),
                                Rowguid = reader.GetGuid(13),
                                ModifiedDate = reader.GetDateTime(14)
                            });
                        }
                    }
                }
            }

            return customers;
        }

        static IEnumerable<SalesOrderHeader> GetSalesOrderHeaders(string connectionString)
        {
            var salesOrderHeaders = new List<SalesOrderHeader>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                SELECT 
                    SalesOrderID, RevisionNumber, OrderDate, DueDate, ShipDate, Status, 
                    OnlineOrderFlag, SalesOrderNumber, PurchaseOrderNumber, AccountNumber, 
                    CustomerID, ShipToAddressID, BillToAddressID, ShipMethod, 
                    CreditCardApprovalCode, SubTotal, TaxAmt, Freight, TotalDue, 
                    Comment, Rowguid, ModifiedDate 
                FROM SalesLT.SalesOrderHeader";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salesOrderHeaders.Add(new SalesOrderHeader
                            {
                                SalesOrderID = reader.GetInt32(0),
                                RevisionNumber = reader.GetByte(1),
                                OrderDate = reader.GetDateTime(2),
                                DueDate = reader.GetDateTime(3),
                                ShipDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                                Status = reader.GetByte(5),
                                OnlineOrderFlag = reader.GetBoolean(6),
                                SalesOrderNumber = reader.GetString(7),
                                PurchaseOrderNumber = reader.IsDBNull(8) ? null : reader.GetString(8),
                                AccountNumber = reader.IsDBNull(9) ? null : reader.GetString(9),
                                CustomerID = reader.GetInt32(10),
                                ShipToAddressID = reader.IsDBNull(11) ? (int?)null : reader.GetInt32(11),
                                BillToAddressID = reader.IsDBNull(12) ? (int?)null : reader.GetInt32(12),
                                ShipMethod = reader.GetString(13),
                                CreditCardApprovalCode = reader.IsDBNull(14) ? null : reader.GetString(14),
                                SubTotal = reader.GetDecimal(15),
                                TaxAmt = reader.GetDecimal(16),
                                Freight = reader.GetDecimal(17),
                                TotalDue = reader.GetDecimal(18), // Computed column
                                Comment = reader.IsDBNull(19) ? null : reader.GetString(19),
                                Rowguid = reader.GetGuid(20),
                                ModifiedDate = reader.GetDateTime(21)
                            });
                        }
                    }
                }
            }

            return salesOrderHeaders;
        }

    }




}
