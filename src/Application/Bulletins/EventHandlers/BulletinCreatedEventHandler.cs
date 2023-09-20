using BulletinBoard.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BulletinBoard.Application.Bulletins.EventHandlers;
public class BulletinCreatedEventHandler : INotificationHandler<BulletinCreatedEvent>
{
    private readonly ILogger<BulletinCreatedEventHandler> _logger;

    public BulletinCreatedEventHandler(ILogger<BulletinCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(BulletinCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("BulletinBoard Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
