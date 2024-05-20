using Microsoft.EntityFrameworkCore;
using Relacionamentos.Models;

namespace Relacionamentos.Data
{
    public class RelacionamentosContext : DbContext
    {
        public RelacionamentosContext(DbContextOptions<RelacionamentosContext> options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
    }
}
