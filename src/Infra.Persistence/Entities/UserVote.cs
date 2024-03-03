namespace PalpiteApi.Infra.Persistence.Entities;
public class UserVote
{
    public int Id { get; set; }
    public int OptionId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
