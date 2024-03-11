namespace PalpiteApi.Domain.Entities.Database;

public class UserVote : BaseEntity
{
    public int OptionId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
