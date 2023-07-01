using BDataBase.Core.Models.Accounts;
using DataBase.Core.Models.PhotoModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace DataBase.EF.DBConfiguration
{
    public class ProfileAccountConfiguration : IEntityTypeConfiguration<ProfileAccounts>
    {
        public void Configure(EntityTypeBuilder<ProfileAccounts> modelBuilder)
        {
            modelBuilder.
                HasKey(p => p.Id);


            modelBuilder.
                 HasMany(p => p.Posts)
                .WithOne(post => post.ProfileAccount)
                .HasForeignKey(post => post.ProfileAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(p => p.QuestionPosts)
                .WithOne(qp => qp.ProfileAccount)
                .HasForeignKey(qp => qp.ProfileAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasOne(p => p.ProfilePohot)
                .WithOne()
                .HasForeignKey<ProfilePhoto>(photo => photo.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasOne(p => p.CoverPhoto)
                .WithOne()
                .HasForeignKey<CoverPhoto>(c => c.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
