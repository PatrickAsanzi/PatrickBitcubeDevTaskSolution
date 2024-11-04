namespace Application.ProductManagement.DTOs
{
    public class ViewCreateProductDto
    {
        public string ProductId { get; init; }
        public string? Name { get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; init; }
        public string UserId { get; init; }
    }
}
