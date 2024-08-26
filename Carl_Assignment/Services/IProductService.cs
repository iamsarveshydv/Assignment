using Carl_Assignment.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carl_Assignment.Services
{
    public interface IProductService
    {
        Task<Tuple<List<Product>, ErrorDto>> AddProducts(List<ProductDto> productdto);

        Task<Tuple<List<Product>, ErrorDto>> GetProduct();

        Task<Tuple<List<Product>, ErrorDto>> GetProductById(int id);

        Tuple<bool, ErrorDto> DeleteProductById(int id);

        Task<Tuple<Product, ErrorDto>> UpdateProduct(int id, ProductDto productdto);

        Task<Tuple<Product, ErrorDto>> StockDecrement(int id, int quantity);

        Task<Tuple<Product, ErrorDto>> StockIncrement(int id, int quantity);
    }
}
