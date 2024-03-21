﻿namespace PalpiteFC.Api.Domain.Entities.Database;

public class Teams : BaseEntity
{
    public string? Name { get; set; }
    public string? Image { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
