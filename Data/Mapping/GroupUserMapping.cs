using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projexor.Features.GroupUsers.Models;

namespace Projexor.Data.Mapping;

public sealed class GroupUserMapping : IEntityTypeConfiguration<GroupUser>
{
    public void Configure(EntityTypeBuilder<GroupUser> builder)
    {
        builder.ToTable("GroupUsers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.GroupId)
            .HasColumnName("GroupId")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasColumnName("UserId")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.Role)
            .HasColumnName("Role")
            .HasColumnType("smallint")
            .IsRequired();

        builder.HasOne(x => x.Group)
            .WithMany(x => x.GroupUsers)
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
            .WithMany(x => x.GroupUsers)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}