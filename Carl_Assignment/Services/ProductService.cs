using AutoMapper;
using Carl_Assignment.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carl_Assignment.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Tuple<List<Product>, ErrorDto>> AddProducts(List<ProductDto> productdto)
        {
            ErrorDto error = new ErrorDto();
            List<Product> product = new List<Product>();
            try
            {
                 product = _mapper.Map<List<Product>>(productdto);
                _context.AddRange(product);
                await _context.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                error.error_code = 400;
                error.error_message = "Failed to update";
            }
            return new Tuple<List<Product>, ErrorDto>(product, error);

        }

        public async Task<Tuple<List<Product>, ErrorDto>> GetProduct()
        {
            ErrorDto error = new ErrorDto();
            List<Product> result = null; 
            try
            {
                result = await _context.Product.ToListAsync();
                if (result == null || !result.Any())
                {
                    error.error_code = 204;
                    error.error_message = "No Content";
                }
            }
            catch (Exception ex)
            {
                error.error_code = 400;
                error.error_message = "Failed to update";
            }
            return new Tuple<List<Product>, ErrorDto>(result.ToList(), error);

        }

        public async Task<Tuple<List<Product>, ErrorDto>> GetProductById(int id)
        {
            ErrorDto error = new ErrorDto();
            List<Product> result = null;
            try
            {
                 result = await _context.Product.Where(X => X.ProductId == id).ToListAsync();
                if (result == null || !result.Any())
                {
                    error.error_code = 204;
                    error.error_message = "No Content";
                }
            }
            catch (Exception ex)
            {
                error.error_code = 400;
                error.error_message = "Failed to fetch data";
            }
            return new Tuple<List<Product>, ErrorDto>(result, error);
        }

        public Tuple<bool, ErrorDto> DeleteProductById(int id)
        {
            int number = 0;
            ErrorDto error = new ErrorDto();
            try
            {
                var result = _context.Product.FirstOrDefault(s => s.ProductId.Equals(id));
                if (result == null)
                {
                    error.error_code = 404;
                    error.error_message = "Record Not Found";
                    return new Tuple<bool, ErrorDto>(false, error);
                }

                _context.Remove(result);
                number = _context.SaveChanges();

            }
            catch (Exception ex)
            {
                number = 0;
            }

            if (number > 0)
                return new Tuple<bool, ErrorDto>(true, error);
            else
                return new Tuple<bool, ErrorDto>(false, error);
        }

        public async Task<Tuple<Product, ErrorDto>> UpdateProduct(int id, ProductDto productdto)
        {
            ErrorDto error = new ErrorDto();
            Product dbProduct = null;
            try
            {
                dbProduct = await _context.Product.FirstOrDefaultAsync(s => s.ProductId.Equals(id));
                if (dbProduct == null)
                {
                    error.error_code = 404;
                    error.error_message = "Product not found.";
                    return new Tuple<Product, ErrorDto>(null, error);
                }

                var product = _mapper.Map<ProductDto>(productdto);
                dbProduct.ProductName = product.ProductName;
                dbProduct.Category = product.Category;
                dbProduct.Description = product.Description;
                dbProduct.Stock = product.Stock;
                dbProduct.Createdon = product.Createdon;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                error.error_code = 400;
                error.error_message = "Failed to update";
            }
            return new Tuple<Product, ErrorDto>(dbProduct, error);
        }

        public async Task<Tuple<Product, ErrorDto>> StockDecrement(int id, int quantity)
        {
            ErrorDto error = new ErrorDto();
            Product product = new Product();
            try
            {
                var dbProduct = await _context.Product.FirstOrDefaultAsync(s => s.ProductId.Equals(id));

                if (dbProduct == null)
                {
                    error.error_code = 404;
                    error.error_message = "Product not found.";
                    return new Tuple<Product, ErrorDto>(null, error);
                }

                dbProduct.Stock = dbProduct.Stock - quantity;

                await _context.SaveChangesAsync();

                 product = await _context.Product.FirstOrDefaultAsync(s => s.ProductId.Equals(id));
                
            }
            catch (Exception ex)
            {
                error.error_code = 400;
                error.error_message = "Failed to update";
            }
            return new Tuple<Product, ErrorDto>(product, error);
        }

        public async Task<Tuple<Product, ErrorDto>> StockIncrement(int id, int quantity)
        {
            ErrorDto error = new ErrorDto();
            Product product = new Product();
            try
            {
                var dbProduct = await _context.Product.FirstOrDefaultAsync(s => s.ProductId.Equals(id));

                if (dbProduct == null)
                {
                    error.error_code = 404;
                    error.error_message = "Product not found.";
                    return new Tuple<Product, ErrorDto>(null, error);
                }

                dbProduct.Stock = dbProduct.Stock + quantity;

                await _context.SaveChangesAsync();

                product = await _context.Product.FirstOrDefaultAsync(s => s.ProductId.Equals(id));
                
            }
            catch (Exception ex)
            {
                error.error_code = 400;
                error.error_message = "Failed to update";
            }

            return new Tuple<Product, ErrorDto>(product, error);
        }
    }

}