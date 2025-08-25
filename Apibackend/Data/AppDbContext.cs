
using Apibackend.Models;
using Microsoft.EntityFrameworkCore;

namespace Apibackend.Data
{
    /// <summary>
    /// context class for the application database, managing entities like Products and Users.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// constructor that accepts DbContextOptions to configure the context.
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        /// <summary>
        /// collection of Product entities in the database.
        /// </summary>
        public DbSet<Product> Products { get; set; }
        /// <summary>
        /// collection of User entities in the database.
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
