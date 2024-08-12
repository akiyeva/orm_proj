namespace orm_proj.Models
{
    public class Payment
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public decimal Amount { get; set; }  //Ödəniş məbləği
        public DateTime PaymentDate { get; set; }
    }
}
