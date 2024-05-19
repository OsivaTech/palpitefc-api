﻿using PalpiteFC.Api.Application.Enums;

namespace PalpiteFC.Api.Application.Contracts;

public sealed class Address
{
    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? Complement { get; set; }
    public string? Neighborhood { get; set; }
    public string? City { get; set; }
    public States? State { get; set; }
    public Countries? Country { get; set; }
    public string? PostalCode { get; set; }
}