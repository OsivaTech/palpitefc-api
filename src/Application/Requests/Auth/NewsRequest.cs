﻿namespace PalpiteFC.Api.Application.Requests.Auth;

public class NewsRequest
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int UserId { get; set; }
}