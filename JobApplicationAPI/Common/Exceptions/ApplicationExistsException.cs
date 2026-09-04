namespace JobApplicationAPI.Common.Exceptions
{
    public class ApplicationExistsException : Exception
    {
        public ApplicationExistsException(string message) : base(message) { }
    }
}
