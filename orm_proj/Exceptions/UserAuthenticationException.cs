namespace orm_proj.Exceptions
{
    public class UserAuthenticationException : Exception
    {
        public UserAuthenticationException() { }
        public UserAuthenticationException(string message) : base(message) { }
    }
}
