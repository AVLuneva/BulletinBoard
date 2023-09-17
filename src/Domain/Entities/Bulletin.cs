namespace BulletinBoard.Domain.Entities;
public class Bulletin : BaseAuditableEntity
{
    public int BoardId { get; set; }

    public string? Title { get; set; }

    public string? Text { get; set; }
}
