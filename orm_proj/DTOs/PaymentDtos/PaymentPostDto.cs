namespace orm_proj.DTOs
{
    public class PaymentPostDto
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }  //Ödəniş məbləği
        public DateTime PaymentDate { get; set; }
    }
}
