
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.DAL.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //        builder.Property(u => u.Username)
            //            .HasMaxLength(100)
            //            .IsRequired();

            //        builder.Property(u => u.PasswordHash)
            //            .IsRequired();

            //        builder.Property(u => u.Email)
            //           .HasMaxLength(150)
            //           .IsRequired();

            //        builder
            //            .Property(u => u.Roles)
            //            .HasConversion(
            //                roles => string.Join(',', roles),
            //                str => str.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            //            );
               }
            }
        }