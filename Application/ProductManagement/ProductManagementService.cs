using Application.ProductManagement.DTOs;
using Domain;
using Domain.Entities;

namespace Application.ProductManagement
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductManagementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

      
        public async Task<ViewCreateProductDto> CreateProductAsync(CreateProductDto createProductDto, string userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user == null)
                throw new ArgumentException("User not found.");

            if (_unitOfWork.ProductRepository.ProductNameExistsAsync(createProductDto.Name).Result)
                throw new ArgumentException("Product name already exists");

            var product = new Product(
                createProductDto.Name,
                createProductDto.Price,
                createProductDto.Quantity,
                user.Id);
            user.AddProduct(product);

            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.CommitAsync();
            return ViewProductDtoMapper.ToViewProductDto(product);
        }
        public IEnumerable<ViewCreateProductDto> GetAllUserProducts(string userId)
        {
            return _unitOfWork.ProductRepository.GetProductsByUserIdAsync(userId).Result
                                                .Select(x => new ViewCreateProductDto() 
                                                { Price = x.Price, 
                                                  ProductId = x.ProductId, 
                                                  Name = x.Name, 
                                                  Quantity = x.Quantity, 
                                                  UserId = x.UserId });

        }
        public IEnumerable<ViewProductDto> GetAllProducts()
        {
            return _unitOfWork.ProductRepository.GetAllAsync().Result
                                                .Select(x => new ViewProductDto() 
                                                { Price = x.Price, 
                                                  ProductId = x.ProductId, 
                                                  ProductName = x.Name}).ToList();
           
        }
        public async Task<ViewCreateProductDto> UpdateProductAsync(string productId, CreateProductDto createProductDto, string userId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

            if (product == null)
                throw new ArgumentException("Product not found.");

            if (product.UserId != userId)
                throw new UnauthorizedAccessException("You do not have permission to update this product.");

            product.Update(
                createProductDto.Name,
                createProductDto.Price,
                createProductDto.Quantity
            );

            await _unitOfWork.CommitAsync();

            return ViewProductDtoMapper.ToViewProductDto(product);
        }
        public async Task DeleteProductAsync(string productId, string userId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

            if (product == null)
                throw new ArgumentException("Product not found.");

            if (product.UserId != userId)
                throw new UnauthorizedAccessException("You do not have permission to delete this product.");

            _unitOfWork.ProductRepository.Remove(product);
            await _unitOfWork.CommitAsync();
        }
    }
}
