﻿namespace PalpiteApi.Infra.Persistence.Entities;
public class Palpitations
{
    public int Id { get; set; }
    public int FirstTeamId { get; set; }
    public int FirstTeamGol { get; set; }
    public int SecondTeamId { get; set; }
    public int SecondTeamGol { get; set; }
    public int UserId { get; set; }
    public int GameId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}