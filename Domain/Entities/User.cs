using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; private set; }
        [Required]
        public string FirstName { get; private set; }
        [Required]
        public string LastName { get; private set; }
        public string? Username { get; private set; }
        [Required]
        public string ApiKey { get; private set; }

        private readonly List<Product> _products = new List<Product>();
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        public User(string firstName, string lastName, string username)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Username = username;
            ApiKey = GenerateApiKey();
        }

        // Private method to generate a new API key
        private string GenerateApiKey()
        {
            return Guid.NewGuid().ToString();
        }

        // Business logic methods
        public void AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _products.Add(product);
        }
    }
}
