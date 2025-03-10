using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DogForum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DogForum.Data
{
    public class DogForumContext : IdentityDbContext<ApplicationUser>
    {
        public DogForumContext(DbContextOptions<DogForumContext> options)
            : base(options)
        {
        }

        public DbSet<DogForum.Models.Discussions> Discussions { get; set; } = default!;
        public DbSet<DogForum.Models.Comments> Comments { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Discussions>()
                .HasOne(d => d.User)
                .WithMany() 
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comments>()
                .HasOne(c => c.User) 
                .WithMany() 
                .HasForeignKey(c => c.UserId) 
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Comments>()
                .HasOne(c => c.Discussions)
                .WithMany(d => d.Comments)
                .HasForeignKey(c => c.DiscussionsId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
