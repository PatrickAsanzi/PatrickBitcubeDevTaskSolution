using Application.CheckoutProcess;
using Domain;
using Domain.Entities;
using Moq;

namespace Tests.Services
{
    public class CheckoutProcessServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CheckoutProcessService _checkoutProcessService;

        public CheckoutProcessServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _checkoutProcessService = new CheckoutProcessService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddProductToCheckoutAsync_ShouldAddProductSuccessfully()
        {
            // Arrange
            var apiKey = "sampleApiKey";
            var productId = "sampleProductId";
            int quantity = 5;

            var product = new Product("Sample Product", 10.0m, 100, "userId");
            var checkoutItem = new CheckoutItem(apiKey);

            _unitOfWorkMock.Setup(u => u.ProductRepository.GetByIdAsync(productId)).ReturnsAsync(product);
            _unitOfWorkMock.Setup(u => u.ProductRepository.GetByApiKeyAsync(apiKey, false)).ReturnsAsync(checkoutItem);
            _unitOfWorkMock.Setup(u => u.CommitAsync()).ReturnsAsync(1);

            // Act
            await _checkoutProcessService.AddProductToCheckoutAsync(apiKey, productId, quantity);

            // Assert
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
            Assert.Contains(checkoutItem.CheckoutItemProducts, p => p.Product == product && p.Quantity == quantity);
        }

        [Fact]
        public async Task AddProductToCheckoutAsync_ShouldThrowException_WhenProductNotFound()
        {
            // Arrange
            var apiKey = "sampleApiKey";
            var productId = "invalidProductId";
            int quantity = 5;

            _unitOfWorkMock.Setup(u => u.ProductRepository.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _checkoutProcessService.AddProductToCheckoutAsync(apiKey, productId, quantity));
        }

        [Fact]
        public async Task AddProductToCheckoutAsync_ShouldThrowException_WhenInsufficientQuantity()
        {
            // Arrange
            var apiKey = "sampleApiKey";
            var productId = "sampleProductId";
            int quantity = 50;

            var product = new Product("Sample Product", 10.0m, 20, "userId");
            var checkoutItem = new CheckoutItem(apiKey);

            _unitOfWorkMock.Setup(u => u.ProductRepository.GetByIdAsync(productId)).ReturnsAsync(product);
            _unitOfWorkMock.Setup(u => u.ProductRepository.GetByApiKeyAsync(apiKey, false)).ReturnsAsync(checkoutItem);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _checkoutProcessService.AddProductToCheckoutAsync(apiKey, productId, quantity));
        }

        [Fact]
        public async Task GetCurrentBasketAsync_ShouldReturnCheckoutItemDto()
        {
            // Arrange
            var apiKey = "sampleApiKey";
            var checkoutItem = new CheckoutItem(apiKey);
            checkoutItem.AddProduct(new Product("Sample Product", 10.0m, 100, "userId"), 2);

            _unitOfWorkMock.Setup(u => u.ProductRepository.GetByApiKeyAsync(apiKey, false)).ReturnsAsync(checkoutItem);

            // Act
            var result = await _checkoutProcessService.GetCurrentBasketAsync(apiKey);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(apiKey, checkoutItem.ApiKey);
            Assert.Single(result.Products);
        }

        [Fact]
        public async Task CompleteCheckoutAsync_ShouldDeductQuantitiesAndMarkCheckoutComplete()
        {
            // Arrange
            var apiKey = "sampleApiKey";
            var productId = "sampleProductId";  // Mock product ID
            var productQuantity = 20;
            var quantityToDeduct = 5;

            var product = new Product("Sample Product", 10.0m, productQuantity, "userId");
            var checkoutItem = new CheckoutItem(apiKey);
            checkoutItem.AddProduct(product, quantityToDeduct);

            // Mock the repository to return the checkout item by API key
            _unitOfWorkMock.Setup(u => u.ProductRepository.GetByApiKeyAsync(apiKey, false))
                .ReturnsAsync(checkoutItem);

            // Mock the repository to return the product by ID
            _unitOfWorkMock.Setup(u => u.ProductRepository.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(product);

            _unitOfWorkMock.Setup(u => u.CommitAsync()).ReturnsAsync(1);

            // Act
            var result = await _checkoutProcessService.CompleteCheckoutAsync(apiKey);

            // Assert
            Assert.True(result.IsComplete);
            Assert.Equal(productQuantity - quantityToDeduct, product.Quantity);  // Verify quantity after deduction
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task RemoveProductFromCheckoutAsync_ShouldThrowException_WhenProductNotInCheckout()
        {
            // Arrange
            var apiKey = "sampleApiKey";
            var productId = "sampleProductId";
            int quantity = 1;

            var checkoutItem = new CheckoutItem(apiKey);
            _unitOfWorkMock.Setup(u => u.ProductRepository.GetByApiKeyAsync(apiKey, false)).ReturnsAsync(checkoutItem);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _checkoutProcessService.RemoveProductFromCheckoutAsync(apiKey, productId, quantity));
        }
    }
}
