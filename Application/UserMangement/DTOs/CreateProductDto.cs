namespace Application.UserMangement.DTOs
{
    public class CreateProductDto
    {
        public string? Name { get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; init; }
    }

}
