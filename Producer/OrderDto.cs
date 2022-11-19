namespace Producer
{
    public record OrderDto
    {
        public string ProductName { get; init; } = null!;

        public decimal Price { get; init; }

        public int Quantity { get; init; }
    }
}
