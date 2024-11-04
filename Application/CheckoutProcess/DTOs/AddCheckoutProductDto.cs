namespace Application.CheckoutProcess.DTOs
{
    public class AddCheckoutProductDto
    {
        public string ProductId { get; init; }
        public string ApiKey { get; init; }
        public int Quantity { get; init; }
    }
}
