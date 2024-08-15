using orm_proj.Enums;

namespace orm_proj.DTOs
{
    public class OrderPostDto
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }  //Sifarişin ümumi məbləği
        public OrderStatus Status { get; set; }
        public List<OrderDetail> Details { get; set; }
    }
}
