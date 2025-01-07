using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using Addresses;
using ConsoleApp5;

namespace SqlConnectionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "data source=LIN-PF1BULQT\\SQLEXPRESS;initial catalog=AdventureWorksLT4;persist security info=True; Integrated Security=SSPI;";

            try
            {
                // Retrieve addresses
                IEnumerable<Address> addresses = GetAddresses(connectionString);

                // LINQ Query: Filter addresses by City
                var filteredAddresses = addresses
                    .Where(a => a.CountryRegion == "Canada")
                    .OrderBy(a => a.AddressLine1)
                    .Select(a => new
                    {
                        a.AddressID,
                        a.AddressLine1,
                        a.City
                    });

                // Display the filtered addresses
                foreach (var address in filteredAddresses)
                {
                    //Console.WriteLine($"ID: {address.AddressID}, AddressLine1: {address.AddressLine1}, City: {address.City}");
                }
                IEnumerable<Product> products = GetProducts(connectionString);
                var sel1 = products.Select(p => new
                {
                    Name=p.Name,
                    Nsize=int.TryParse(p.Size,out int size) ? size : 0
                });
                foreach (var C in sel1)
                {
                    Console.WriteLine(C.Name,C.Nsize);
                }
   
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static IEnumerable<Address> GetAddresses(string connectionString)
        {
            var addresses = new List<Address>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string address = "SELECT * FROM SalesLT.Address";
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
    }
}