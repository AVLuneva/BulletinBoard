namespace BulletinBoard.Domain.Entities;
public class Board : BaseAuditableEntity
{
    public string? Title { get; set; }

    public IList<Bulletin> Bulletins { get; private set; } = new List<Bulletin>();
}
