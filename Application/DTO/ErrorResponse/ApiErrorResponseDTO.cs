namespace Application.DTO.ErrorResponse
{
    public record ApiErrorResponseDto(int StatusCode,
        string Message,
        string? Details = null
        );
}
