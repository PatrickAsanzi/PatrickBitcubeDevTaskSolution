using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class CheckoutItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        public string ApiKey { get; private set; }

        private readonly List<CheckoutItemProduct> _checkoutItemProducts = new List<CheckoutItemProduct>();
        public IReadOnlyCollection<CheckoutItemProduct> CheckoutItemProducts => _checkoutItemProducts;
        public bool IsComplete { get; private set; }

        public CheckoutItem(string apiKey)
        {
            ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            IsComplete = false;
        }

        public void AddProduct(Product product, int quantity)
        {
            if (IsComplete)
                throw new InvalidOperationException("Cannot add products to a completed checkout.");

            // Check if the product already exists in the checkout
            var existingProduct = _checkoutItemProducts
                .FirstOrDefault(cp => cp.ProductId == product.ProductId);

            if (existingProduct != null)
            {
                // Increase quantity of existing product
                existingProduct.Quantity += quantity;
            }
            else
            {
                // Add new product with specified quantity
                var checkoutItemProduct = new CheckoutItemProduct
                {
                    CheckoutItem = this,
                    Product = product,
                    Quantity = quantity
                };
                _checkoutItemProducts.Add(checkoutItemProduct);
            }
        }
        public void RemoveProduct(Product product, int quantity)
        {
            if (IsComplete)
                throw new InvalidOperationException("Cannot remove products from a completed checkout.");

            // Find the product in the checkout
            var existingProduct = _checkoutItemProducts
                .FirstOrDefault(cp => cp.ProductId == product.ProductId);

            if (existingProduct == null)
                throw new InvalidOperationException("Product not found in the checkout.");

            // Decrease quantity or remove product if quantity becomes zero or less
            if (existingProduct.Quantity > quantity)
            {
                existingProduct.Quantity -= quantity;
            }
            else
            {
                _checkoutItemProducts.Remove(existingProduct);
            }
        }

        public void CompleteCheckout()
        {
            IsComplete = true;
        }
    }
}

