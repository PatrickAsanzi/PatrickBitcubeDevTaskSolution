using Domain.Entities;

namespace Domain.Tests.Entities
{
    public class ProductTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProductWithValidData()
        {
            // Arrange
            string name = "Test Product";
            decimal price = 100.0m;
            int quantity = 10;
            string userId = "userId";

            // Act
            var product = new Product(name, price, quantity, userId);

            // Assert
            Assert.Equal(name, product.Name);
            Assert.Equal(price, product.Price);
            Assert.Equal(quantity, product.Quantity);
            Assert.Equal(userId, product.UserId);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenNameIsNull()
        {
            // Arrange
            string userId = "userId";

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Product(null, 100.0m, 10, userId));
            Assert.Equal("name", exception.ParamName);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenUserIdIsNull()
        {
            // Arrange
            string name = "Test Product";

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Product(name, 100.0m, 10, null));
            Assert.Equal("userId", exception.ParamName);
        }

        [Fact]
        public void Update_ShouldUpdateProductSuccessfully()
        {
            // Arrange
            var product = new Product("Old Product", 50.0m, 5, "userId");
            string newName = "Updated Product";
            decimal newPrice = 75.0m;
            int newQuantity = 20;

            // Act
            product.Update(newName, newPrice, newQuantity);

            // Assert
            Assert.Equal(newName, product.Name);
            Assert.Equal(newPrice, product.Price);
            Assert.Equal(newQuantity, product.Quantity);
        }

        [Fact]
        public void Update_ShouldThrowArgumentNullException_WhenNameIsNull()
        {
            // Arrange
            var product = new Product("Product", 50.0m, 5, "userId");

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => product.Update(null, 75.0m, 20));
            Assert.Equal("name", exception.ParamName);
        }

        [Fact]
        public void DeductQuantity_ShouldReduceQuantitySuccessfully()
        {
            // Arrange
            var product = new Product("Product", 50.0m, 10, "userId");
            int deductAmount = 3;

            // Act
            product.DeductQuantity(deductAmount);

            // Assert
            Assert.Equal(7, product.Quantity);
        }

        [Fact]
        public void DeductQuantity_ShouldThrowArgumentException_WhenQuantityIsZeroOrNegative()
        {
            // Arrange
            var product = new Product("Product", 50.0m, 10, "userId");

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => product.DeductQuantity(0));
            Assert.Equal("quantity", exception.ParamName);
            Assert.Equal("Quantity must be greater than zero. (Parameter 'quantity')", exception.Message);
        }

        [Fact]
        public void DeductQuantity_ShouldThrowInvalidOperationException_WhenInsufficientQuantity()
        {
            // Arrange
            var product = new Product("Product", 50.0m, 5, "userId");
            int deductAmount = 10;

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => product.DeductQuantity(deductAmount));
            Assert.Equal("Insufficient quantity. Available: 5, Requested: 10.", exception.Message);
        }
    }
}
