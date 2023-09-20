using BulletinBoard.Domain.Entities;

namespace BulletinBoard.Domain.Events;
public class BulletinDeletedEvent : BaseEvent
{
    public BulletinDeletedEvent(Bulletin bulletin)
    {
        Bulletin = bulletin;
    }

    public Bulletin Bulletin { get; }
}
