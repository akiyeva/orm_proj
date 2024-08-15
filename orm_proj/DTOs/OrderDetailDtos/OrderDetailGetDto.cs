namespace orm_proj.DTOs
{
    public class OrderDetailGetDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; } //Sifariş edilmiş məhsulun sayı.
        public decimal PricePerItem { get; set; } //Məhsulun hər biri üçün qiymət.
        public override string ToString()
        {
            return $"Order id: {OrderId}, Product id: {ProductId}, Quantity: {Quantity}, Price per item: {PricePerItem}";
        }
    }
}
