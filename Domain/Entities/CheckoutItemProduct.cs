namespace Domain.Entities
{
    public class CheckoutItemProduct
    {
        public int CheckoutItemId { get; set; }
        public CheckoutItem CheckoutItem { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
