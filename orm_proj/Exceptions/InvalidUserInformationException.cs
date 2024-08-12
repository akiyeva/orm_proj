namespace orm_proj.Exceptions
{
    public class InvalidUserInformationException : Exception
    {
        public InvalidUserInformationException() { }
        public InvalidUserInformationException(string message) : base(message) { }
    }
}
