using Microsoft.EntityFrameworkCore;
using ApiBalta.Models;

namespace ApiBalta.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        { }

        public DbSet<Todo> todos { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        //     optionsBuilder.UseSqlServer(
        //         "Initial Catalog=ApiBalta; Data Source=localhost,1433;Persist"
        //         + " Security Info=true; User ID=sa; Password=1q2w3e4r@#$"
        //     );

    }
}