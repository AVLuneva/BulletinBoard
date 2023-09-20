using BulletinBoard.Application.Common.Exceptions;
using BulletinBoard.Application.Common.Interfaces;
using BulletinBoard.Domain.Entities;
using BulletinBoard.Domain.Events;
using MediatR;

namespace BulletinBoard.Application.Bulletins.Commands.DeleteBulletin;
public record DeleteBulletinCommand(int Id) : IRequest;
public class DeleteBulletinCommandHandler : IRequestHandler<DeleteBulletinCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteBulletinCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBulletinCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Bulletins.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Bulletin), request.Id);
        }

        _context.Bulletins.Remove(entity);

        entity.AddDomainEvent(new BulletinDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}