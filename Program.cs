using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace interview
{
    class Program
    {
        static void Main(string[] args)
        {
            var cart = new Cart();

            foreach (var cartArg in args)
            {
                var props = cartArg.Split(",");
                cart.AddCartItem(props[0], int.Parse(props[1]));
            }
            
        }
    }
    public class Cart
    {
        public void AddCartItem(string name, decimal price)
        {
            var options = new DbContextOptionsBuilder<InterviewContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;

            using (var context = new InterviewContext(options))
            {
                var cartItem = new CartItem
                {
                    Name = name,
                    price = price.ToString(),
                };

                context.Cart.Add(cartItem);
                context.SaveChanges();

                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    w.WriteLine("{0} {1} Cart Item ID:{2} Name:{3} Price:{4} added successfully!!", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), cartItem.Id, cartItem.Name, cartItem.price);
                }
            }
        }
    }

    public class InterviewContext : DbContext
    {
        public InterviewContext(DbContextOptions<InterviewContext> options)
            : base(options)
        { }
        public DbSet<CartItem> Cart { get; set; }
    }

    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string price { get; set; }
    }
}
