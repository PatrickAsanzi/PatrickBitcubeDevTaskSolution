using Application.ProductManagement;
using Application.ProductManagement.DTOs;
using Domain;
using Domain.Entities;
using Moq;

namespace Application.Tests.ProductManagement
{
    public class ProductManagementServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ProductManagementService _productManagementService;

        public ProductManagementServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _productManagementService = new ProductManagementService(_unitOfWorkMock.Object);
        }


        [Fact]
        public async Task CreateProductAsync_ShouldThrowUserNotFoundException()
        {
            // Arrange
            var createProductDto = new CreateProductDto { Name = "New Product", Price = 10.0m, Quantity = 100 };
            var userId = "invalidUserId";

            _unitOfWorkMock.Setup(u => u.UserRepository.GetByIdAsync(userId)).ReturnsAsync((ApplicationUser)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _productManagementService.CreateProductAsync(createProductDto, userId));
            Assert.Equal("User not found.", exception.Message);
        }

        [Fact]
        public void GetAllUserProducts_ShouldReturnUserProducts()
        {
            // Arrange
            var userId = "userId";
            var products = new List<Product>
            {
                new Product("Product 1", 10.0m, 50, userId),
                new Product("Product 2", 15.0m, 20, userId)
            };

            _unitOfWorkMock.Setup(u => u.ProductRepository.GetProductsByUserIdAsync(userId)).ReturnsAsync(products);

            // Act
            var result = _productManagementService.GetAllUserProducts(userId).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Product 1", result[0].Name);
            Assert.Equal("Product 2", result[1].Name);
        }

        [Fact]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product("Product 1", 10.0m, 50, "userId"),
                new Product("Product 2", 15.0m, 20, "userId")
            };

            _unitOfWorkMock.Setup(u => u.ProductRepository.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = _productManagementService.GetAllProducts().ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Product 1", result[0].ProductName);
            Assert.Equal("Product 2", result[1].ProductName);
        }


        [Fact]
        public async Task UpdateProductAsync_ShouldThrowProductNotFoundException()
        {
            // Arrange
            var productId = "invalidProductId";
            var createProductDto = new CreateProductDto { Name = "Updated Product", Price = 20.0m, Quantity = 10 };
            var userId = "userId";

            _unitOfWorkMock.Setup(u => u.ProductRepository.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _productManagementService.UpdateProductAsync(productId, createProductDto, userId));
            Assert.Equal("Product not found.", exception.Message);
        }


        [Fact]
        public async Task DeleteProductAsync_ShouldThrowProductNotFoundException()
        {
            // Arrange
            var productId = "invalidProductId";
            var userId = "userId";

            _unitOfWorkMock.Setup(u => u.ProductRepository.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _productManagementService.DeleteProductAsync(productId, userId));
            Assert.Equal("Product not found.", exception.Message);
        }

    }
}
