﻿namespace PalpiteFC.Api.Domain.Settings;

public class JwtSettings
{
    public string? SecurityKey { get; set; }
    public TimeSpan Expiration { get; set; }
}