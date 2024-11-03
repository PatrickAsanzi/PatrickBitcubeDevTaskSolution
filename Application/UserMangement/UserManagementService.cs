using Application.UserMangement.DTOs;
using Domain;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Identity;

namespace Application.UserMangement
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserManagementService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }
        public async Task<ViewProductDto> AddProductAsync(CreateProductDto createProductDto, string userId)
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
            return new ViewProductDto() { ProductId = product.ProductId, Name = product.Name, Price = product.Price, Quantity = product.Quantity }; // clean this up static method 
        }

        public async Task<ViewProductDto> UpdateProductAsync(string productId, CreateProductDto createProductDto, string userId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

            if (product == null)
                throw new ArgumentException("Product not found.");

            if (product.UserId != userId)
                throw new UnauthorizedAccessException("You do not have permission to update this product.");

            if (_unitOfWork.ProductRepository.ProductNameExistsAsync(createProductDto.Name).Result)
                throw new ArgumentException("Product name already exists");

            product.Update(
                createProductDto.Name,
                createProductDto.Price,
                createProductDto.Quantity
            );

            await _unitOfWork.CommitAsync();

            return new ViewProductDto
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };
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

        public async Task<CreateUserResultDto> Create(UserCreateDto userCreateDto)
        {
            var user = new ApplicationUser(userCreateDto.FirstName, userCreateDto.LastName, userCreateDto?.UserName, userCreateDto?.Email);

            var result = await _userManager.CreateAsync(user, userCreateDto.Password);
            await _unitOfWork.CommitAsync();

            if (result.Succeeded)
            {
                return new CreateUserResultDto
                {
                    Succeeded = true,
                    ApiKey = user.ApiKey,
                    UserName = user.UserName,
                    Email = user.Email
                };
            }
            return new CreateUserResultDto
            {
                Succeeded = false,
                Errors = result.Errors
            };

        }
    }
}
