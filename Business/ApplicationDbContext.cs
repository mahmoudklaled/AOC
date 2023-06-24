using Business.Accounts.Models;
using Business.Posts.Models;
using Business.Authentication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Business
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<TokenCode> TokenCodes { get; set; }
        public DbSet<ProfileAccounts> ProfileAccounts { get; set; }
        public DbSet<DefaultPhotos> DefaultPhotos { get; set; }
        //public DbSet<PostPhoto> PostPhotos { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<QuestionPost> QuestionPosts { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Vedio> Vedios { get; set; }
        public DbSet<Comment> Comments { get;set; }
        public DbSet<Reacts> Reacts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationships and foreign keys
            modelBuilder.Entity<ProfileAccounts>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<ProfileAccounts>()
                .HasMany(p => p.Posts)
                .WithOne(post => post.ProfileAccount)
                .HasForeignKey(post => post.ProfileAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProfileAccounts>()
                .HasMany(p => p.QuestionPosts)
                .WithOne(qp => qp.ProfileAccount)
                .HasForeignKey(qp => qp.ProfileAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Photos)
                .WithOne(photo => photo.Post)
                .HasForeignKey(photo => photo.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionPost>()
                .HasMany(qp => qp.Photos)
                .WithOne(photo => photo.QuestionPost)
                .HasForeignKey(photo => photo.QuestionPostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Vedios)
                .WithOne(vedio => vedio.Post)
                .HasForeignKey(vedio => vedio.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionPost>()
                .HasMany(qp => qp.Vedios)
                .WithOne(vedio => vedio.QuestionPost)
                .HasForeignKey(vedio => vedio.QuestionPostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Photo)
                .WithOne(p => p.Comment)
                .HasForeignKey<Photo>(p => p.CommentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Vedio)
                .WithOne(v => v.Comment)
                .HasForeignKey<Vedio>(v => v.CommentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ProfileAccount)
                .WithMany(pa => pa.Comments)
                .HasForeignKey(c => c.ProfileAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict); // <-- Use DeleteBehavior.Restrict for Post

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.QuestionPost)
                .WithMany(qp => qp.Comments)
                .HasForeignKey(c => c.QuestionPostId)
                .OnDelete(DeleteBehavior.Restrict); // <-- Use DeleteBehavior.Restrict for QuestionPost


             //data for reacts 
             modelBuilder.Entity<Reacts>()
                .HasOne(r => r.ProfileAccount)
                .WithMany(pa => pa.Reacts)
                .HasForeignKey(r => r.ProfileAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reacts>()
                .HasOne(r => r.Post)
                .WithMany(pa => pa.Reacts)
                .HasForeignKey(r => r.PostID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reacts>()
                .HasOne(r => r.QuestionPost)
                .WithMany(pa => pa.Reacts)
                .HasForeignKey(r => r.QuestionID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reacts>()
                .HasOne(r => r.Comment)
                .WithMany(pa => pa.Reacts)
                .HasForeignKey(r => r.CommentID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the primary key for IdentityUserLogin<string>
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(login => new { login.LoginProvider, login.ProviderKey });
        }


    }
}
