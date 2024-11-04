using Domain.Entities;

namespace Domain.Tests.Entities
{
    public class CheckoutItemTests
    {
        [Fact]
        public void Constructor_ShouldInitializeCheckoutItemWithValidApiKey()
        {
            // Arrange
            string apiKey = "sampleApiKey";

            // Act
            var checkoutItem = new CheckoutItem(apiKey);

            // Assert
            Assert.Equal(apiKey, checkoutItem.ApiKey);
            Assert.False(checkoutItem.IsComplete);
            Assert.Empty(checkoutItem.CheckoutItemProducts);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenApiKeyIsNull()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new CheckoutItem(null));
            Assert.Equal("apiKey", exception.ParamName);
        }

        [Fact]
        public void AddProduct_ShouldAddNewProduct_WhenProductDoesNotExistInCheckout()
        {
            // Arrange
            var checkoutItem = new CheckoutItem("sampleApiKey");
            var product = new Product("Test Product", 50.0m, 10, "userId");

            // Act
            checkoutItem.AddProduct(product, 5);

            // Assert
            Assert.Single(checkoutItem.CheckoutItemProducts);
            var checkoutItemProduct = checkoutItem.CheckoutItemProducts.First();
            Assert.Equal(product.ProductId, checkoutItemProduct.Product.ProductId);
            Assert.Equal(5, checkoutItemProduct.Quantity);
        }

        [Fact]
        public void AddProduct_ShouldIncreaseQuantity_WhenProductAlreadyExistsInCheckout()
        {
            // Arrange
            var checkoutItem = new CheckoutItem("sampleApiKey");
            var product = new Product("Test Product", 50.0m, 10, "userId");

            // Add the product initially
            checkoutItem.AddProduct(product, 5);

            // Act - add the same product again
            checkoutItem.AddProduct(product, 3);

            // Assert
            var checkoutItemProduct = checkoutItem.CheckoutItemProducts.First();
            Assert.Equal(8, checkoutItemProduct.Quantity);
        }

        [Fact]
        public void AddProduct_ShouldThrowInvalidOperationException_WhenCheckoutIsComplete()
        {
            // Arrange
            var checkoutItem = new CheckoutItem("sampleApiKey");
            var product = new Product("Test Product", 50.0m, 10, "userId");

            checkoutItem.CompleteCheckout();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => checkoutItem.AddProduct(product, 5));
        }

        [Fact]
        public void RemoveProduct_ShouldReduceQuantity_WhenProductExistsInCheckout()
        {
            // Arrange
            var checkoutItem = new CheckoutItem("sampleApiKey");
            var product = new Product("Test Product", 50.0m, 10, "userId");
            checkoutItem.AddProduct(product, 8);

            // Act
            checkoutItem.RemoveProduct(product, 3);

            // Assert
            var checkoutItemProduct = checkoutItem.CheckoutItemProducts.First();
            Assert.Equal(5, checkoutItemProduct.Quantity);
        }

        [Fact]
        public void RemoveProduct_ShouldRemoveProduct_WhenQuantityIsLessThanOrEqualToZero()
        {
            // Arrange
            var checkoutItem = new CheckoutItem("sampleApiKey");
            var product = new Product("Test Product", 50.0m, 5, "userId");
            checkoutItem.AddProduct(product, 0);

            // Act
            checkoutItem.RemoveProduct(product, 5);

            // Assert
            Assert.Empty(checkoutItem.CheckoutItemProducts);
        }

        [Fact]
        public void RemoveProduct_ShouldThrowInvalidOperationException_WhenProductNotInCheckout()
        {
            // Arrange
            var checkoutItem = new CheckoutItem("sampleApiKey");
            var product = new Product("Test Product", 50.0m, 10, "userId");

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => checkoutItem.RemoveProduct(product, 5));
            Assert.Equal("Product not found in the checkout.", exception.Message);
        }

        [Fact]
        public void RemoveProduct_ShouldThrowInvalidOperationException_WhenCheckoutIsComplete()
        {
            // Arrange
            var checkoutItem = new CheckoutItem("sampleApiKey");
            var product = new Product("Test Product", 50.0m, 10, "userId");

            checkoutItem.AddProduct(product, 5);
            checkoutItem.CompleteCheckout();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => checkoutItem.RemoveProduct(product, 3));
        }

        [Fact]
        public void CompleteCheckout_ShouldSetIsCompleteToTrue()
        {
            // Arrange
            var checkoutItem = new CheckoutItem("sampleApiKey");

            // Act
            checkoutItem.CompleteCheckout();

            // Assert
            Assert.True(checkoutItem.IsComplete);
        }
    }
}
