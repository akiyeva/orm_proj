namespace orm_proj.Exceptions
{
    public class OrderAlreadyCancelledException : Exception
    {
        public OrderAlreadyCancelledException() { }
        public OrderAlreadyCancelledException(string message) : base(message) { }
    }
}
