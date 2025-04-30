
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL.EntitiesConfiguration
{
    public class BugConfiguration : IEntityTypeConfiguration<Bug>
    {
        public void Configure(EntityTypeBuilder<Bug> builder)
        {
            builder.Property(b => b.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(b => b.Description)
                .HasMaxLength(1000);

            builder.HasOne(b => b.Project)
                .WithMany(p => p.Bugs)
                .HasForeignKey(b => b.ProjectId);
        }
    }
}
