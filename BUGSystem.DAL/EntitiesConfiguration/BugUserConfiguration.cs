
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL.EntitiesConfiguration
{
    public class BugUserConfiguration : IEntityTypeConfiguration<BugUser>
    {
        public void Configure(EntityTypeBuilder<BugUser> builder)
        {
            builder.HasKey(bu => new { bu.BugId, bu.UserId });

            builder.HasOne(bu => bu.Bug)
                .WithMany(b => b.BugUsers)
                .HasForeignKey(bu => bu.BugId);

            builder.HasOne(bu => bu.User)
                .WithMany(u => u.BugUsers)
                .HasForeignKey(bu => bu.UserId);
        }
    }
}
