namespace PalpiteFC.Api.Domain.Entities.Database;

public class UserVote : BaseEntity
{
    public int OptionId { get; set; }
    public int UserId { get; set; }
}
