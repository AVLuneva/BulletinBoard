using BulletinBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Board> Boards { get; }

    DbSet<Bulletin> Bulletins { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}