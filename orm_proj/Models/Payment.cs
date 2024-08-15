using orm_proj.Models.Common;

namespace orm_proj.Models
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public decimal Amount { get; set; }  //Ödəniş məbləği
        public DateTime PaymentDate { get; set; }
    }
}
