namespace orm_proj.Exceptions
{
    public class InvalidOrderDetailException : Exception
    {
        public InvalidOrderDetailException() { }
        public InvalidOrderDetailException(string message) : base(message) { }
    }
}
