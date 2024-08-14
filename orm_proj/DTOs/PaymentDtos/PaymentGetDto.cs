namespace orm_proj.DTOs
{
    public class PaymentGetDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public decimal Amount { get; set; }  //Ödəniş məbləği
        public DateTime PaymentDate { get; set; }
    }
}
