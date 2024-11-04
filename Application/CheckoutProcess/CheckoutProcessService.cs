using Application.CheckoutProcess.DTOs;
using Domain;
using Domain.Entities;

namespace Application.CheckoutProcess
{
    public class CheckoutProcessService : ICheckoutProcessService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CheckoutProcessService(IUnitOfWork unitOfWork)
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
                throw new ArgumentException("Product not found.", nameof(productId));

            if (quantity > product.Quantity)
            {
                throw new InvalidOperationException($"Insufficient quantity for product '{product.Name}'. Available: {product.Quantity}, Requested: {quantity}.");
            }

            checkoutItem.AddProduct(product, quantity);

            await _unitOfWork.CommitAsync();
        }

        public async Task<CheckoutItemDto> GetCurrentBasketAsync(string apiKey)
        {
            var checkoutItem = await _unitOfWork.ProductRepository.GetByApiKeyAsync(apiKey, false);

            if (checkoutItem == null)
                throw new ArgumentException("Checkout item not found.");

            // Map the checkout item to a DTO
            var checkoutItemDto = new CheckoutItemDto
            {
                IsComplete = checkoutItem.IsComplete,
                TotalCost = checkoutItem.CheckoutItemProducts
                                        .Sum(checkoutProduct => checkoutProduct.Product.Price * checkoutProduct.Quantity),
                Products = checkoutItem.CheckoutItemProducts.Select(x => new CheckoutProductDto()
                {
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    Quantity = x.Quantity
                }).ToList()
            };
            return checkoutItemDto;
        }
        public async Task<CheckoutItemDto> CompleteCheckoutAsync(string apiKey)
        {
            var checkoutItem = await _unitOfWork.ProductRepository.GetByApiKeyAsync(apiKey, false);

            if (checkoutItem == null)
                throw new ArgumentException("Checkout item not found.");

            decimal totalAmount = checkoutItem.CheckoutItemProducts
                                              .Sum(checkoutProduct => checkoutProduct.Product.Price * checkoutProduct.Quantity);


            foreach (var checkoutProduct in checkoutItem.CheckoutItemProducts)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(checkoutProduct.ProductId);
                if (product != null)
                {
                    product.DeductQuantity(checkoutProduct.Quantity);
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

            var productToRemove = checkoutItem.CheckoutItemProducts.Select(x => x.Product)
                                              .FirstOrDefault(x => x.ProductId == productId);

            if (productToRemove == null)
                throw new ArgumentException("Product not found in the checkout basket.");

            checkoutItem.RemoveProduct(productToRemove, quantity);

            await _unitOfWork.CommitAsync();
        }
    }
}
