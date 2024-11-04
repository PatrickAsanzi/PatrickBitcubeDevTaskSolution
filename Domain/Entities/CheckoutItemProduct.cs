namespace Domain.Entities
{
    public class CheckoutItemProduct
    {
        
        public int CheckoutItemId { get; set; }
        public CheckoutItem CheckoutItem { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        private CheckoutItemProduct() { }

        // Custom constructor for use in business logic
        public CheckoutItemProduct(Product product, int quantity)
        {
            Product = product;
            ProductId = product.ProductId;
            UpdateQuantity(quantity);
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity < 0)
                throw new ArgumentException("Quantity cannot be negative.", nameof(newQuantity));

            Quantity = newQuantity;
        }

        public void IncreaseQuantity(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Increase amount must be positive.", nameof(amount));

            Quantity += amount;
        }

        public void DecreaseQuantity(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Decrease amount must be positive.", nameof(amount));

            if (amount > Quantity)
                throw new InvalidOperationException("Cannot decrease quantity below zero.");

            Quantity -= amount;
        }
    }
}