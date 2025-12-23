using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projexor.Features.Projects.Models;

namespace Projexor.Data.Mapping;

public sealed class ProjectMapping : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("Status")
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.CompleteDate)
            .HasColumnName("CompleteDate")
            .HasColumnType("timestamptz");

        builder.Property(x => x.UserId)
            .HasColumnName("UserId")
            .HasColumnType("uuid");

        builder.Property(x => x.GroupId)
            .HasColumnName("GroupId")
            .HasColumnType("uuid");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("timestamptz")
            .IsRequired();

    }
}