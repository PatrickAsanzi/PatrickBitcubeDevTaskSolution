using Domain.Entities;

namespace Application.ProductManagement.DTOs
{
    public static class ViewProductDtoMapper
    {

        public static ViewCreateProductDto ToViewProductDto(Product product) 
        {
            return new ViewCreateProductDto()
            {
                Price = product.Price,
                ProductId = product.ProductId,
                Name = product.Name,
                Quantity = product.Quantity,
                UserId = product.UserId,
            }; 
        }


    }
}
