namespace Application.Exceptions
{
    public class ValidatorException : ApplicationException
    {
        public ValidatorException(string? message) : base(message)
        {
        }
    }
}
