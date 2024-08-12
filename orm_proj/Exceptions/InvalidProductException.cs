namespace orm_proj.Exceptions
{
    public class InvalidProductException : Exception
    {
        public InvalidProductException() { }
        public InvalidProductException(string message) : base(message) { }
    }
}
