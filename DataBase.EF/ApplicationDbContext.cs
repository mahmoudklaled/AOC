using BDataBase.Core.Models.Accounts;
using DataBase.Core.Models.Authentication;
using DataBase.Core.Models.CommentModels;
using DataBase.Core.Models.PhotoModels;
using DataBase.Core.Models.Posts;
using DataBase.Core.Models.Reacts;
using DataBase.Core.Models.VedioModels;
using DataBase.EF.DBConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<TokenCode> TokenCodes { get; set; }
        public DbSet<ProfileAccounts> ProfileAccounts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<QuestionPost> QuestionPosts { get; set; }
        public DbSet<CoverPhoto> CoverPhotos { get; set; }
        public DbSet<ProfilePhoto> ProfilePhotos { get; set; }
        public DbSet<PostPhoto> PostPhotos { get; set; }
        public DbSet<QuestionPhoto> QuestionPhotos { get; set; }
        public DbSet<QuestionCommentPhoto> QuestionCommentPhotos { get; set; }
        public DbSet<PostCommentPhoto> PostCommentPhotos { get; set; }
        public DbSet<PostVedio> PostVedios { get; set; }
        public DbSet<QuestionVedio> QuestionVedios { get; set; }
        public DbSet<QuestionCommentVedio> QuestionCommentVedios { get; set; }
        public DbSet<PostCommentVedio> PostCommentVedios { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<QuestionComment> QuestionComments { get; set; }
        public DbSet<PostReact> PostReacts { get; set; }
        public DbSet<QuestionReact> QuestionReacts { get; set; }
        public DbSet<QuestionCommentReact> QuestionCommentReacts { get; set; }
        public DbSet<PostCommentReact> PostCommentReacts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProfileAccountConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new CommentPostConfiguration());


            modelBuilder.Entity<PostCommentReact>()
                .HasOne(qc => qc.ProfileAccount)
                .WithMany()
                .HasForeignKey(qc => qc.ProfileAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionCommentReact>()
                .HasOne(pc => pc.ProfileAccount)
                .WithMany()
                .HasForeignKey(pc => pc.ProfileAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostComment>()
                .HasOne(pc => pc.ProfileAccount)
                .WithMany()
                .HasForeignKey(pc => pc.ProfileAccountId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PostReact>()
                .HasOne(pr => pr.ProfileAccount)
                .WithMany()
                .HasForeignKey(pr => pr.ProfileAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionComment>()
                .HasOne(qc => qc.ProfileAccount)
                .WithMany()
                .HasForeignKey(qc => qc.ProfileAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionReact>()
                .HasOne(qr => qr.ProfileAccount)
                .WithMany()
                .HasForeignKey(qr => qr.ProfileAccountId)
                .OnDelete(DeleteBehavior.Restrict);






            // Configure the primary key for IdentityUserLogin<string>
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(login => new { login.LoginProvider, login.ProviderKey });
        }


    }
}
