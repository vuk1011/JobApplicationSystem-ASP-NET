namespace JobApplicationAPI.Common.Exceptions
{
    public class JobPostingClosedException : Exception
    {
        public JobPostingClosedException(string message) : base(message) { }
    }
}
