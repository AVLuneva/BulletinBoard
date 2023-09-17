using BulletinBoard.Domain.Entities;
using BulletinBoard.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BulletinBoard.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole("Administrator");

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }

        // Default data
        // Seed, if necessary
        if (!_context.Boards.Any())
        {
            _context.Boards.AddRange(
                new Board
                {
                    Title = "Тема 1",
                    Bulletins =
                    {
                        new Bulletin { Title = "Пост 1.1", Text = "Текст поста 1.1" },
                        new Bulletin { Title = "Пост 1.2", Text = "Текст поста 1.2" },
                        new Bulletin { Title = "Пост 1.3", Text = "Текст поста 1.3" },
                    }
                },
                new Board
                {
                    Title = "Тема 2",
                    Bulletins =
                    {
                        new Bulletin { Title = "Пост 2.1", Text = "Текст поста 2.1" },
                        new Bulletin { Title = "Пост 2.2", Text = "Текст поста 2.2" },
                        new Bulletin { Title = "Пост 2.3", Text = "Текст поста 2.3" },
                    }
                },
                new Board
                {
                    Title = "Тема 3",
                    Bulletins =
                    {
                        new Bulletin { Title = "Пост 3.1", Text = "Текст поста 3.1" },
                        new Bulletin { Title = "Пост 3.2", Text = "Текст поста 3.2" },
                        new Bulletin { Title = "Пост 3.3", Text = "Текст поста 3.3" },
                    }
                });

            await _context.SaveChangesAsync();
        }
    }
}
