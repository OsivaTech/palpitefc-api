﻿namespace PalpiteApi.Infra.Persistence.Entities;

public class Championships
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
