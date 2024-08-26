using Carl_Assignment.Controllers;
using Carl_Assignment.Entity;
using Carl_Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Carl_Assignment.Test
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductController _controller;

        public ProductControllerTest()
        {
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductController(_mockProductService.Object);
        }

        [Fact]
        public void GetProduct_ReturnsOkResult_WithProductList()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Test Product 1", Category = "Fruits", Stock = 100, Description = "New Fruits", Createdon = DateTime.Now},
                new Product { ProductId = 2, ProductName = "Test Product 2", Category = "Clothes", Stock = 50, Description = "New clothes", Createdon = DateTime.Now }
            };

            var error = new ErrorDto { error_message = string.Empty, error_code = 200 };

            _mockProductService.Setup(service => service.GetProduct())
                .Returns(Task.FromResult(Tuple.Create(products, error)));

            // Act
            var result = _controller.GetProduct().Result;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetProduct_ReturnsBadRequest_WithProductList()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Test Product 1", Category = "Fruits", Stock = 100, Description = "New Fruits", Createdon = DateTime.Now},
                new Product { ProductId = 2, ProductName = "Test Product 2", Category = "Clothes", Stock = 50, Description = "New clothes", Createdon = DateTime.Now }
            };

            var error = new ErrorDto { error_message = "Error occured", error_code = 400 };

            _mockProductService.Setup(service => service.GetProduct())
                .Returns(Task.FromResult(Tuple.Create(products, error)));

            // Act
            var result = _controller.GetProduct().Result;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetProduct_ReturnsNoContent_WithProductList()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Test Product 1", Category = "Fruits", Stock = 100, Description = "New Fruits", Createdon = DateTime.Now},
                new Product { ProductId = 2, ProductName = "Test Product 2", Category = "Clothes", Stock = 50, Description = "New clothes", Createdon = DateTime.Now }
            };

            var error = new ErrorDto { error_message = "No Content", error_code = 204 };

            _mockProductService.Setup(service => service.GetProduct())
                .Returns(Task.FromResult(Tuple.Create(products, error)));

            // Act
            var result = _controller.GetProduct().Result;

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void StockDecrement_ReturnsOkResult_WhenDecrementIsSuccessful()
        {
            // Arrange
            int productId = 1;
            int quantity = 5;
            var product = new Product { ProductId = productId, Stock = 30 }; // Initial stock of 10
            var resultProduct = new Product { ProductId = productId, Stock = 25 }; // Expected stock after decrement
            var error = new ErrorDto{ error_message = string.Empty };

            _mockProductService.Setup(service => service.StockDecrement(productId, quantity))
                .Returns(Task.FromResult(Tuple.Create(resultProduct, error)));

            // Act
            var result = _controller.StockDecrement(productId, quantity).Result;

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(resultProduct, okResult.Value);
        }

        [Fact]
        public void StockDecrement_ReturnsBadRequest_WhenErrorOccurs()
        {
            // Arrange
            int productId = 1;
            int quantity = 5;
            var product = new Product { ProductId = productId, Stock = 30 }; // Initial stock of 10
            var resultProduct = new Product { ProductId = productId, Stock = 25 }; // Expected stock after decrement
            var error = new ErrorDto{ error_message = "Stock decrement failed", error_code=400 };

            _mockProductService.Setup(service => service.StockDecrement(productId, quantity))
                .Returns(Task.FromResult(Tuple.Create(resultProduct, error)));

            // Act
            var result = _controller.StockDecrement(productId, quantity).Result;

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(error, badRequestResult.Value);
        }

        [Fact]
        public void StockDecrement_ReturnsNoContent_WhenErrorOccurs()
        {
            // Arrange
            int productId = 1;
            int quantity = 5;
            var product = new Product { ProductId = productId, Stock = 30 }; // Initial stock of 10
            var resultProduct = new Product { ProductId = productId, Stock = 25 }; // Expected stock after decrement
            var error = new ErrorDto { error_message = "Stock decrement failed", error_code = 404 };

            _mockProductService.Setup(service => service.StockDecrement(productId, quantity))
                .Returns(Task.FromResult(Tuple.Create(resultProduct, error)));

            // Act
            var result = _controller.StockDecrement(productId, quantity).Result;

            // Assert
             Assert.IsType<NotFoundResult>(result);
            
        }


        [Fact]
        public void UpdateProduct_ReturnsCreatedResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            int productId = 1;
            var productDto = new ProductDto {ProductName = "Test Product 1", Category = "Fruits", Stock = 100, Description = "New Fruits", Createdon = DateTime.Now };

            var updatedProduct = new Product
            { ProductId = productId, ProductName = "Test Product 1", Category = "Fruits", Stock = 100, Description = "New Fruits", Createdon = DateTime.Now };
            var error = new ErrorDto { error_message = string.Empty, error_code = 201};

            _mockProductService.Setup(service => service.UpdateProduct(productId, productDto))
                .Returns(Task.FromResult(Tuple.Create(updatedProduct, error)));

            // Act
            var result = _controller.UpdateProduct(productId, productDto).Result;

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public void UpdateProduct_ReturnsBadRequest_WhenServiceReturnsError()
        {
            // Arrange
            int productId = 1;
            var productDto = new ProductDto { ProductName = "Test Product 1", Category = "Fruits", Stock = 100, Description = "New Fruits", Createdon = DateTime.Now };

            var updatedProduct = new Product
            { ProductId = productId, ProductName = "Test Product 1", Category = "Fruits", Stock = 100, Description = "New Fruits", Createdon = DateTime.Now };
            var error = new ErrorDto { error_message = string.Empty, error_code = 400 };

            _mockProductService.Setup(service => service.UpdateProduct(productId, productDto))
                .Returns(Task.FromResult(Tuple.Create(updatedProduct, error)));

            // Act
            var result = _controller.UpdateProduct(productId, productDto).Result;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateProduct_ReturnsNoContent_WhenServiceReturnsNotFound()
        {
            // Arrange
            int productId = 1;
            var productDto = new ProductDto { ProductName = "Test Product 1", Category = "Fruits", Stock = 100, Description = "New Fruits", Createdon = DateTime.Now };

            var updatedProduct = new Product
            { ProductId = productId, ProductName = "Test Product 1", Category = "Fruits", Stock = 100, Description = "New Fruits", Createdon = DateTime.Now };
            var error = new ErrorDto { error_message = string.Empty, error_code = 404 };

            _mockProductService.Setup(service => service.UpdateProduct(productId, productDto))
                .Returns(Task.FromResult(Tuple.Create(updatedProduct, error)));

            // Act
            var result = _controller.UpdateProduct(productId, productDto).Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void StockIncrement_ReturnsOkResult_WhenIncrementIsSuccessful()
        {
            // Arrange
            int productId = 1;
            int quantity = 20;
            var product = new Product { ProductId = productId, Stock = 30 }; // Initial stock of 30
            var resultProduct = new Product { ProductId = productId, Stock = 50 }; // Expected stock after increment
            var error = new ErrorDto { error_message = string.Empty };

            _mockProductService.Setup(service => service.StockIncrement(productId, quantity))
                .Returns(Task.FromResult(Tuple.Create(resultProduct, error)));

            // Act
            var result = _controller.StockIncrement(productId, quantity).Result;

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(resultProduct, okResult.Value);
        }

        [Fact]
        public void StockIncrement_ReturnsBadRequest_WhenErrorOccurs()
        {
            // Arrange
            int productId = 1;
            int quantity = 20;
            var product = new Product { ProductId = productId, Stock = 30 }; // Initial stock of 30
            var resultProduct = new Product { ProductId = productId, Stock = 50 }; // Expected stock after increment
            var error = new ErrorDto { error_message = "Stock increment failed", error_code = 400 };

            _mockProductService.Setup(service => service.StockIncrement(productId, quantity))
                .Returns(Task.FromResult(Tuple.Create(resultProduct, error)));

            // Act
            var result = _controller.StockIncrement(productId, quantity).Result;

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(error, badRequestResult.Value);
        }

        [Fact]
        public void StockIncrement_ReturnsNoContent_WhenErrorOccurs()
        {
            // Arrange
            int productId = 1;
            int quantity = 20;
            var product = new Product { ProductId = productId, Stock = 30 }; // Initial stock of 30
            var resultProduct = new Product { ProductId = productId, Stock = 50 }; // Expected stock after increment
            var error = new ErrorDto { error_message = "Stock increment failed", error_code = 404 };

            _mockProductService.Setup(service => service.StockIncrement(productId, quantity))
                .Returns(Task.FromResult(Tuple.Create(resultProduct, error)));

            // Act
            var result = _controller.StockIncrement(productId, quantity).Result;

            // Assert
             Assert.IsType<NotFoundResult>(result);
            
        }

        [Fact]
        public void AddProducts_ReturnsOkResult_WhenAddProductsIsSuccessful()
        {
            // Arrange
            
            var products = new List<ProductDto>
            {
                new ProductDto {  ProductName = "Test Product 1", Category = "Fruits", Stock = 100, Description = "New Fruits", Createdon = DateTime.Now},
                new ProductDto {  ProductName = "Test Product 2", Category = "Clothes", Stock = 50, Description = "New clothes", Createdon = DateTime.Now }
            };

            var resultProduct = new List<Product> 
            {
                new Product { ProductId = 100001, ProductName = "Test Product 1", Category = "Fruits", Stock = 100, Description = "New Fruits", Createdon = DateTime.Now},
                new Product { ProductId = 100002, ProductName = "Test Product 2", Category = "Clothes", Stock = 50, Description = "New clothes", Createdon = DateTime.Now } 
            }; 

            var error = new ErrorDto { error_message = string.Empty };

            _mockProductService.Setup(service => service.AddProducts(products))
                .Returns(Task.FromResult(Tuple.Create(resultProduct, error)));

            // Act
            var result = _controller.AddProducts(products).Result;

            // Assert
            var okResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal(resultProduct, okResult.Value);
        }

        [Fact]
        public void AddProducts_ReturnsBadRequest_WhenErrorOccurs()
        {
            // Arrange
            var products = new List<ProductDto>
            {
                new ProductDto {  ProductName = "Test Product 1", Category = "Fruits", Stock = 100, Description = "New Fruits", Createdon = DateTime.Now},
                new ProductDto {  ProductName = "Test Product 2", Category = "Clothes", Stock = 50, Description = "New clothes", Createdon = DateTime.Now }
            };

            var resultProduct = new List<Product>
            {
                new Product { ProductId = 100001, ProductName = "Test Product 1", Category = "Fruits", Stock = 100, Description = "New Fruits", Createdon = DateTime.Now},
                new Product { ProductId = 100002, ProductName = "Test Product 2", Category = "Clothes", Stock = 50, Description = "New clothes", Createdon = DateTime.Now }
            };

            var error = new ErrorDto { error_message = "Failed to add products", error_code = 400 };

            _mockProductService.Setup(service => service.AddProducts(products))
                .Returns(Task.FromResult(Tuple.Create(resultProduct, error)));

            // Act
            var result = _controller.AddProducts(products).Result;

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(error, badRequestResult.Value);
        }


    }
}
