using orm_proj.Models.Common;

namespace orm_proj.Models
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; } //Sifariş edilmiş məhsulun sayı.
        public decimal PricePerItem { get; set; } //Məhsulun hər biri üçün qiymət.
    }
}
