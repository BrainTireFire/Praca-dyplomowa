namespace pracadyplomowa.Errors;

public class ApiResponse
{
    public int StatusCode { get; private set; }
    public string? Message { get; private set; }
    
    public ApiResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }

    private string? GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "You have made a bad request",
            401 => "You are not authorized",
            404 => "Resource was not found",
            500 => "An internal server error occurred",
            _ => null
        };
    }
}