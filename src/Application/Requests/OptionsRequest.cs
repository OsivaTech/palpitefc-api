﻿namespace PalpiteApi.Application.Requests;

public class OptionsRequest
{
    public int Id { get; set; }
    public int VoteId { get; set; }
    public string? Title { get; set; }
    public int Count { get; set; }
}
