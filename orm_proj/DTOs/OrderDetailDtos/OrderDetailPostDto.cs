namespace orm_proj.DTOs.OrderDetailDtos
{
    public class OrderDetailPostDto
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; } //Sifariş edilmiş məhsulun sayı.
        public decimal PricePerItem { get; set; } //Məhsulun hər biri üçün qiymət.
    }
}
