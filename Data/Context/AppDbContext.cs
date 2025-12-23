using Microsoft.EntityFrameworkCore;
using Projexor.Features.Groups.Models;
using Projexor.Features.GroupUsers.Models;
using Projexor.Features.Projects.Models;
using Projexor.Features.Users.Models;

namespace Projexor.Data.Context;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<GroupUser> GroupUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}