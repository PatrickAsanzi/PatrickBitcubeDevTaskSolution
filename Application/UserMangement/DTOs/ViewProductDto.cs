namespace Application.UserMangement.DTOs
{
    public class ViewProductDto
    {
        public string ProductId { get; init; }
        public string? Name { get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; init; }
    }
}
