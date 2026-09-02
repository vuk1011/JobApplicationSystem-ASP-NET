namespace JobApplicationAPI.DTOs
{
    public record ApiResponse(string message);
    public record ApiResponse<T>(string message, T? data);
}
