using orm_proj.Enums;
using orm_proj.Models.Common;

namespace orm_proj.Models
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }  //Sifarişin ümumi məbləği
        public OrderStatus Status { get; set; }
        public List<OrderDetail> Details { get; set; }

      
    }
}
    