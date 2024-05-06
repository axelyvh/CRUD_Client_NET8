namespace Application.Exceptions
{
    public class UserFriendlyException : ApplicationException
    {
        public UserFriendlyException(string? message) : base(message)
        {
        }
    }
}
