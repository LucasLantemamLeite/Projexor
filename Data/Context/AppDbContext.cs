using Microsoft.EntityFrameworkCore;
using Stokify.Features.Groups.Models;
using Stokify.Features.GroupUsers.Models;
using Stokify.Features.Projects.Models;
using Stokify.Features.Users.Models;

namespace Stokify.Data.Context;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<GroupUser> GroupUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}