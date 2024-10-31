using Domain.Repositories;
using Domain.Services;

namespace Application.ProductManagement
{
    public class ProductManagementService: IProductManagementService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        public ProductManagementService(IProductRepository productRepository, IUserRepository userRepository) 
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
        }
    }
}
