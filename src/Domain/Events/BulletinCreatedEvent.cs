using BulletinBoard.Domain.Entities;

namespace BulletinBoard.Domain.Events;
public class BulletinCreatedEvent : BaseEvent
{
    public BulletinCreatedEvent(Bulletin bulletin)
    {
        Bulletin = bulletin;
    }

    public Bulletin Bulletin { get; }
}
