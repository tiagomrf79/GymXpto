﻿namespace Application.Responses;

public class BaseResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string>? ValidationErrors { get; set; }

    public BaseResponse()
    {
        Success = true;
    }
}
