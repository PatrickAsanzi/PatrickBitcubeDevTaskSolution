using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; private set; }
        public string? Name { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public virtual string UserId { get; private set; }
        public virtual ApplicationUser? User { get; private set; }  // Navigation property to User
    }
}
