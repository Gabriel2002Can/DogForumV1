using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DogForum.Models;

namespace DogForum.Data
{
    public class DogForumContext : DbContext
    {
        public DogForumContext (DbContextOptions<DogForumContext> options)
            : base(options)
        {
        }

        public DbSet<DogForum.Models.Discussions> Discussions { get; set; } = default!;
        public DbSet<DogForum.Models.Comments> Comments { get; set; } = default!;
    }
}
