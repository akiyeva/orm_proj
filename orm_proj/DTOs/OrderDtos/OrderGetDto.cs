using orm_proj.Enums;

namespace orm_proj.DTOs
{
    public class OrderGetDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TotalAmount { get; set; }  //Sifarişin ümumi məbləği
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetail> Details { get; set; }
    }
}
