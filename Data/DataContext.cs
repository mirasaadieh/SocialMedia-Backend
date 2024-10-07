using Microsoft.EntityFrameworkCore;
using SocialMedia.Entity.Views;
using SocialMedia.Models;

namespace SocialMedia.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){ 
        
        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserLogin> Users { get; set; }
        public DbSet<UsersPosts> UsersPosts { get; set; }
        public DbSet<UsersLikes> UsersLikes { get; set; }
        public DbSet<UsersFollowers> UsersFollowers { get; set; }
        public DbSet<UsersComments> UsersComments { get; set; }
        public DbSet<PostsLikes> PostsLikes { get; set; }
        public DbSet<PostsComments> PostsComments { get; set; }
        public DbSet<CommentsLikes> CommentsLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLogin>().HasData(
           new UserLogin { Id = 1, UserName = "john_doe", Email = "john@example.com" ,Password="john123"},
           new UserLogin { Id = 2, UserName = "jane_smith", Email = "jane@example.com", Password="jane123" }
       );
            modelBuilder.Entity<UsersPosts>()
            .HasNoKey() 
            .ToView("UsersPosts");

            modelBuilder.Entity<UsersLikes>()
           .HasNoKey()
           .ToView("UsersLikes");

            modelBuilder.Entity<UsersFollowers>()
           .HasNoKey()
           .ToView("UsersFollowers");

            modelBuilder.Entity<UsersComments>()
           .HasNoKey()
           .ToView("UsersComments");

            modelBuilder.Entity<PostsLikes>()
            .HasNoKey()
            .ToView("PostLikes");

            modelBuilder.Entity<PostsComments>()
            .HasNoKey()
            .ToView("PostsComments");

            modelBuilder.Entity<CommentsLikes>()
            .HasNoKey()
            .ToView("CommentsLikes");

            //modelBuilder.Entity<UsersPosts>()
            //        .HasOne(u => u.User)
            //        .WithMany(up => up.UsersPosts)
            //        .HasForeignKey(u => u.UserId);
            //modelBuilder.Entity<UsersPosts>()
            //        .HasOne(p => p.Post)
            //        .WithMany(up => up.UsersPosts)
            //        .HasForeignKey(p => p.PostId);
            //modelBuilder.Entity<UsersLikes>()
            //.HasKey(ul => ul.Id);
            //modelBuilder.Entity<UsersLikes>()
            //        .HasOne(u => u.User)
            //        .WithMany(up => up.UsersLikes)
            //        .HasForeignKey(u => u.UserId);
            //modelBuilder.Entity<UsersLikes>()
            //        .HasOne(l => l.Like)
            //        .WithMany(ul => ul.UsersLikes)
            //        .HasForeignKey(l => l.LikeId);
            //modelBuilder.Entity<UsersFollowers>()
            //    .HasKey(uf => uf.Id);
            //modelBuilder.Entity<UsersFollowers>()
            //        .HasOne(u => u.User)
            //        .WithMany(uf => uf.UsersFollowers)
            //        .HasForeignKey(u => u.UserId);
            //modelBuilder.Entity<UsersFollowers>()
            //        .HasOne(f => f.Follower)
            //        .WithMany(uf => uf.UsersFollowers)
            //        .HasForeignKey(f => f.FollowerId);
            //modelBuilder.Entity<UsersComments>()
            //    .HasKey(uc => uc.Id);
            //modelBuilder.Entity<UsersComments>()
            //        .HasOne(u => u.User)
            //        .WithMany(uc => uc.UsersComments)
            //        .HasForeignKey(u => u.UserId);
            //modelBuilder.Entity<UsersComments>()
            //        .HasOne(c => c.Comment)
            //        .WithMany(uc => uc.UsersComments)
            //        .HasForeignKey(c => c.CommentId);
            //modelBuilder.Entity<PostsLikes>()
            //   .HasKey(pl => pl.Id);
            //modelBuilder.Entity<PostsLikes>()
            //        .HasOne(p => p.Post)
            //        .WithMany(pl => pl.PostsLikes)
            //       .HasForeignKey(p => p.PostId);
            //modelBuilder.Entity<PostsLikes>()
            //        .HasOne(l => l.Like)
            //        .WithMany(pl => pl.PostsLikes)
            //        .HasForeignKey(l => l.PostId);
            //modelBuilder.Entity<PostsComments>()
            //  .HasKey(pc => pc.Id);
            //modelBuilder.Entity<PostsComments>()
            //        .HasOne(p => p.Post)
            //        .WithMany(pc => pc.PostsComments)
            //       .HasForeignKey(p => p.PostId)
            //        .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete
            //modelBuilder.Entity<PostsComments>()
            //        .HasOne(c => c.Comment)
            //        .WithMany(pc => pc.PostsComments)
            //        .HasForeignKey(c => c.CommentId)
            //        .OnDelete(DeleteBehavior.Cascade);  // Allow cascade delete if desired
            //modelBuilder.Entity<CommentsLikes>()
            // .HasKey(cl => cl.Id);
            //modelBuilder.Entity<CommentsLikes>()
            //        .HasOne(l => l.Like)
            //        .WithMany(cl => cl.CommentsLikes)
            //       .HasForeignKey(p => p.LikeId);
            //modelBuilder.Entity<CommentsLikes>()
            //        .HasOne(c => c.Comment)
            //        .WithMany(cl => cl.CommentsLikes)
            //        .HasForeignKey(c => c.CommentId);
        }
    }
}
