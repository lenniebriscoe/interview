using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interview
{
    class Program
    {
        /// <summary>
        /// Ficticious program for adding items into a cart. The console app could be a webApi or a Windows form and is not important.
        /// The brief is simple. Improve this code in any way you see fit. You can pull in any nuget packages, change project structure, naming. Anything.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var cart = new Cart();

            cart.AddCartItem(args[0], decimal.Parse(args[1]));
        }
    }

    public class Cart
    {
        public void AddCartItem(string name, decimal price)
        {
            string connectionString = "Data Source=192.168.123.45;Initial Catalog=MyDatabase;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO CartItem (Name, price) VALUES (" + name + "," + price.ToString() + ")", connection))
                {
                    connection.Open();
                    string result = (string)command.ExecuteScalar();

                    using (StreamWriter w = File.AppendText("log.txt"))
                    {
                        w.WriteLine("{0} {1} Cart Item {2} added successfully!!", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), result);
                    }
                }
            }
        }
    }
}
