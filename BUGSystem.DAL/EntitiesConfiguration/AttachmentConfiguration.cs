using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BUGSystem.DAL.EntitiesConfiguration
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(a => a.AttachmentId);

            builder.Property(a => a.FileName)
                .HasMaxLength(255);
            builder.Property(a => a.FileType)
                .HasMaxLength(50);
            builder.Property(a => a.FilePath)
                .HasMaxLength(500);

            builder.HasOne(a => a.Bugs)
                .WithMany(b => b.Attachments)
                .HasForeignKey(a => a.BugId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
