namespace Application.ProductManagement.DTOs
{
    public class CheckoutItemDto
    {
        public List<CheckoutProductDto> Products { get; set; }
        public decimal TotalCost { get; set; }
        public bool IsComplete { get; set; }
    }
}
