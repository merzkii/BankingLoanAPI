namespace Application.DTO.ErrorResponse
{
    public record ApiErrorResponseDTO(int StatusCode,
        string Message,
        string? Details = null);
    
}
