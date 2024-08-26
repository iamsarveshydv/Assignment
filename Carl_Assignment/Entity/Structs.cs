using System;

namespace Carl_Assignment.Entity
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public string Category { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public DateTime? Createdon { get; set; }
    }

    public class ErrorDto
    {       
            public int error_code { get; set; }
            public string error_message { get; set; }       
    }
}
