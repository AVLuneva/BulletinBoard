using BulletinBoard.Application.Common.Exceptions;
using BulletinBoard.Application.Common.Interfaces;
using BulletinBoard.Domain.Entities;
using MediatR;

namespace BulletinBoard.Application.Bulletins.Commands.UpdateBulletin;
public record UpdateBulletinCommand : IRequest
{
    public int Id { get; init; }
    public int BoardId { get; init; }
    public string? Title { get; init; }
    public string? Text { get; init; }
}

public class UpdateBulletinCommandHandler : IRequestHandler<UpdateBulletinCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBulletinCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateBulletinCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Bulletins.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Bulletin), request.Id);
        }

        entity.BoardId = request.BoardId;
        entity.Title = request.Title;
        entity.Text = request.Text;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
