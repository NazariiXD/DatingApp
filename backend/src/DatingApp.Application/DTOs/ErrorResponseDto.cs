namespace DatingApp.Application.DTOs;

public record ErrorResponseDto(int StatusCode, string Message, string? Details);