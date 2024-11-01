using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; private set; }
        [Required]
        public string LastName { get; private set; }
        [Required]
        public string ApiKey { get; private set; }

        private readonly List<Product> _products = new List<Product>();
        public IReadOnlyCollection<Product> Products => _products;

        public ApplicationUser(string firstName, string lastName, string? userName, string? email)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Email = email ?? throw new ArgumentNullException(nameof(email));

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
