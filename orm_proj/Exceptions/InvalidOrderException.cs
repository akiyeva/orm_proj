namespace orm_proj.Exceptions
{
    public class InvalidOrderException : Exception
    {
        public InvalidOrderException() { }
        public InvalidOrderException(string message) : base(message) { }
    }
}
