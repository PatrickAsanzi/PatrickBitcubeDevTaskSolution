namespace Domain.Entities
{
    public class Product
    {
        public int ProductId { get; private set; }
        public int UserId { get; private set; }
        public string? Name { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public User? User { get; private set; }  // Navigation property to User
    }
}
