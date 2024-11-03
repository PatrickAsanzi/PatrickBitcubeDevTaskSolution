using Microsoft.EntityFrameworkCore.Update.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductId { get; private set; }
        public string? Name { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public virtual string UserId { get; private set; }
        public virtual ApplicationUser? User { get; private set; }

        private readonly List<CheckoutItemProduct> _checkoutItemProducts = new List<CheckoutItemProduct>();
        public IReadOnlyCollection<CheckoutItemProduct> CheckoutItemProducts => _checkoutItemProducts;

        public Product(string name, decimal price, int quantity, string userId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price;
            Quantity = quantity;
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
        }

        public void Update(string name, decimal price, int quantity)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price;
            Quantity = quantity;
        }

        public void DeductQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            if (Quantity < quantity)
                throw new InvalidOperationException($"Insufficient quantity. Available: {Quantity}, Requested: {quantity}.");

            Quantity -= quantity;
        }
    }
}
