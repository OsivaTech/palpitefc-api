﻿namespace PalpiteFC.Api.Domain.Entities.Database;

public class TeamsGame : BaseEntity
{
    public int Gol { get; set; }
    public int TeamId { get; set; }
    public int GameId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
