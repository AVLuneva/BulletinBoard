using BulletinBoard.Application.Common.Interfaces;
using BulletinBoard.Domain.Entities;
using BulletinBoard.Domain.Events;
using MediatR;

namespace BulletinBoard.Application.Bulletins.Commands.CreateBulletin;
public record CreateBulletinCommand : IRequest<int>
{
    public int BoardId { get; init; }
    public string? Title { get; init; }
    public string? Text { get; init; }
}

public class CreateBulletinCommandHandler : IRequestHandler<CreateBulletinCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateBulletinCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateBulletinCommand request, CancellationToken cancellationToken)
    {
        var entity = new Bulletin
        {
            BoardId = request.BoardId,
            Title = request.Title,
            Text = request.Text,
        };

        entity.AddDomainEvent(new BulletinCreatedEvent(entity));

        _context.Bulletins.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
