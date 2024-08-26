using System;

namespace Carl_Assignment.Entity
{
    public class Product
    {
        
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public int Stock  { get; set; }
        public string Description { get; set; }
        public DateTime? Createdon { get; set; }
    }
}
