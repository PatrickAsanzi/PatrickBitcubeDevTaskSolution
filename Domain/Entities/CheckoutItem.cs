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

            var existingProduct = _checkoutItemProducts
                .FirstOrDefault(cp => cp.ProductId == product.ProductId);

            if (existingProduct != null)
            {
                existingProduct.IncreaseQuantity(quantity);
            }
            else
            {
                var checkoutItemProduct = new CheckoutItemProduct(product, quantity);
                checkoutItemProduct.CheckoutItem = this;
                _checkoutItemProducts.Add(checkoutItemProduct);
            }
        }

        public void RemoveProduct(Product product, int quantity)
        {
            if (IsComplete)
                throw new InvalidOperationException("Cannot remove products from a completed checkout.");

            var checkoutItemProduct = _checkoutItemProducts
                .FirstOrDefault(cp => cp.ProductId == product.ProductId);

            if (checkoutItemProduct == null)
                throw new InvalidOperationException("Product not found in the checkout.");

            if (checkoutItemProduct.Quantity > quantity)
            {
                checkoutItemProduct.DecreaseQuantity(quantity);
            }
            else
            {
                _checkoutItemProducts.Remove(checkoutItemProduct);
            }
        }

        public void CompleteCheckout()
        {
            IsComplete = true;
        }
    }
}

