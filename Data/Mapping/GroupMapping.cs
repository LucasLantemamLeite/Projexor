using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stokify.Features.Groups.Models;

namespace Stokify.Data.Mapping;

public sealed class GroupMapping : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Groups");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.Property(x => x.Active)
            .HasColumnName("Active")
            .HasColumnType("boolean")
            .IsRequired();

        builder.HasMany(x => x.Projects)
            .WithOne()
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}