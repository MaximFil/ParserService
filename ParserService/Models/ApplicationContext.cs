using Microsoft.EntityFrameworkCore;
using System;

namespace ParserService
{
    public class ParserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ParserContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Parser1111;Trusted_Connection=True;");
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UserArticle>().HasMany()


        //    modelBuilder.Entity<UserArticle>()
        //        .HasKey(t => new { t.UserId, t.ArticleId });

        //    modelBuilder.Entity<UserArticle>()
        //        .HasOne(sc => sc.User)
        //        .WithMany(s => s.UserArticle)
        //        .HasForeignKey(sc => sc.UserId);

        //    modelBuilder.Entity<UserArticle>()
        //        .HasOne(sc => sc.Article)
        //        .WithMany(c => c.UserArticle)
        //        .HasForeignKey(sc => sc.ArticleId);

        //    modelBuilder.Entity<UserSite>()
        //        .HasKey(t => new { t.UserId, t.SiteId });

        //    modelBuilder.Entity<UserSite>().HasOne(sc => sc.User)
        //        .WithMany(c => c.UserSite)
        //        .HasForeignKey(sc => sc.UserId);

        //    modelBuilder.Entity<UserSite>().HasOne(sc => sc.Site)
        //        .WithMany(c => c.UserSite)
        //        .HasForeignKey(sc => sc.SiteId);
        //}
    }
}
