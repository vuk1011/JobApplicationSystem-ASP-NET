namespace JobApplicationAPI.Common.Exceptions
{
    public class EmailInUseException : Exception
    {
        public EmailInUseException(string message) : base(message) { }
    }
}
