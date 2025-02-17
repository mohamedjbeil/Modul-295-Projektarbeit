using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JetstreamAPI.Models;

namespace JetstreamAPI.Data
{
    public class JetstreamDbContext : IdentityDbContext<User>
    {
        public JetstreamDbContext(DbContextOptions<JetstreamDbContext> options) : base(options) { }


        public DbSet<Order> Orders { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}
