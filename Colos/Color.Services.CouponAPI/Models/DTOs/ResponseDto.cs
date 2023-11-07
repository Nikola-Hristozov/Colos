namespace Color.Services.CouponAPI.Models.DTOs;

public class ResponseDto
{
    public object? Result { get; set; }
    public bool IsSuccessful { get; set; } = true;
    public string Message { get; set; } = string.Empty;
}