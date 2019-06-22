using System;

namespace Advantage.API.Models
{
    public class Order{
        public int ID { get; set; }
        public Customer Customer { get; set; }
        public decimal Total { get; set; }
        public DateTime Placed { get; set; }
        public DateTime? Completed { get; set; }
    }
}