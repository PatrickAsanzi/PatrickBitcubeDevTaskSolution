using Application.ProductManagement.DTOs;
using Domain;
using Domain.Entities;
using Domain.Services;

namespace Application.ProductManagement
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductManagementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddProductToCheckoutAsync(string apiKey, string productId, int quantity)
        {
            var checkoutItem = await _unitOfWork.ProductRepository.GetByApiKeyAsync(apiKey, false);

            if (checkoutItem == null)
            {
                checkoutItem = new CheckoutItem(apiKey);
                _unitOfWork.ProductRepository.Add(checkoutItem);
            }

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

            if (product == null)
                throw new ArgumentException("Product not found.");

            checkoutItem.AddProduct(product, quantity);

            await _unitOfWork.CommitAsync();
        }

        public async Task<CheckoutItemDto> CompleteCheckoutAsync(string apiKey)
        {
            var checkoutItem = await _unitOfWork.ProductRepository.GetByApiKeyAsync(apiKey, false);

            if (checkoutItem == null)
                throw new ArgumentException("Checkout item not found.");

            decimal totalAmount = checkoutItem.CheckoutItemProducts
                                              .Sum(checkoutProduct => checkoutProduct.Product.Price * checkoutProduct.Quantity);

            // Check quantities against available stock before proceeding
            foreach (var checkoutProduct in checkoutItem.CheckoutItemProducts)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(checkoutProduct.ProductId);
                if (product == null)
                    throw new ArgumentException($"Product with ID {checkoutProduct.ProductId} not found.");

                // Check if the requested quantity exceeds available stock
                if (checkoutProduct.Quantity > product.Quantity)
                {
                    throw new InvalidOperationException($"Insufficient quantity for product '{product.Name}'. Available: {product.Quantity}, Requested: {checkoutProduct.Quantity}.");
                }
            }

            // Deduct quantities from the database for each product in the checkout
            foreach (var checkoutProduct in checkoutItem.CheckoutItemProducts)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(checkoutProduct.ProductId);
                if (product != null)
                {
                    product.DeductQuantity(checkoutProduct.Quantity); // Use the new method to deduct quantity
                }
            }

            checkoutItem.CompleteCheckout();
            await _unitOfWork.CommitAsync();

            return new CheckoutItemDto
            {
                IsComplete = true,
                TotalCost = totalAmount,
                Products = checkoutItem.CheckoutItemProducts
                                       .Select(x => new CheckoutProductDto
                                       {
                                           Name = x.Product.Name,
                                           Price = x.Product.Price,
                                           Quantity = x.Quantity
                                       }).ToList()
            };
        }

        public async Task RemoveProductFromCheckoutAsync(string apiKey, string productId, int quantity)
        {
            var checkoutItem = await _unitOfWork.ProductRepository.GetByApiKeyAsync(apiKey, false);

            if (checkoutItem == null)
                throw new ArgumentException("Checkout item not found.");

            var productToRemove = checkoutItem.CheckoutItemProducts
                                              .FirstOrDefault(x => x.ProductId == productId);

            if (productToRemove == null)
                throw new ArgumentException("Product not found in the checkout basket.");

            // Remove the product from the checkout item's product list
            checkoutItem.RemoveProduct(productToRemove.Product, quantity);

            await _unitOfWork.CommitAsync();
        }

        public async Task<CheckoutItemDto> GetCheckoutItemAsync(string apiKey)
        {
            var checkoutItem = await _unitOfWork.ProductRepository.GetByApiKeyAsync(apiKey, false);

            if (checkoutItem == null)
                throw new ArgumentException("Checkout item not found.");

            // Map the checkout item to a DTO
            var checkoutItemDto = new CheckoutItemDto
            {
                IsComplete = checkoutItem.IsComplete,
                Products = checkoutItem.CheckoutItemProducts.Select(x => new CheckoutProductDto()
                {
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    Quantity = x.Quantity
                }).ToList()
            };
            return checkoutItemDto;
        }

    }
}
