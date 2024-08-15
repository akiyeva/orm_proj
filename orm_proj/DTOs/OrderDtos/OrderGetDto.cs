using orm_proj.Enums;

namespace orm_proj.DTOs
{
    public class OrderGetDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal TotalAmount { get; set; }  //Sifarişin ümumi məbləği
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetail> Details { get; set; }
        public override string ToString()
        {
            return $"OrderId: {Id}, UserId: {UserId}, OrderDate: {OrderDate}, Total Amount: {TotalAmount}, Order status: {Status}";
        }
    }
}
