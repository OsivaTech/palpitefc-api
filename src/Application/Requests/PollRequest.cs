namespace PalpiteFC.Api.Application.Requests;

public class PollRequest
{
    public string? Title { get; set; }
    public IEnumerable<OptionRequest>? Options { get; set; }
}

public class OptionRequest
{
    public string? Title { get; set; }
}
