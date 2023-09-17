using BulletinBoard.Application.Common.Interfaces;

namespace BulletinBoard.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
