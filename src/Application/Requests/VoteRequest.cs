namespace PalpiteApi.Application.Requests;

public class VoteRequest
{
    public string? Title { get; set; }
    public IEnumerable<Option>? Options { get; set; }
}

public class Option
{
    public string? Title { get; set; }
}
