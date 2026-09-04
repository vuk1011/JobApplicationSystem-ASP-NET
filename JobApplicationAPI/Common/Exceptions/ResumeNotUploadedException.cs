namespace JobApplicationAPI.Common.Exceptions
{
    public class ResumeNotUploadedException : Exception
    {
        public ResumeNotUploadedException(string message) : base(message) { }
    }
}
