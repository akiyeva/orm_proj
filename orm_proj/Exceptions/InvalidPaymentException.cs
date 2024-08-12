namespace orm_proj.Exceptions
{
    public class InvalidPaymentException : Exception
    {
        public InvalidPaymentException() { }
        public InvalidPaymentException(string message) : base(message) { }
    }
}
