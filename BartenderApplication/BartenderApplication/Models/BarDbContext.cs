using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BartenderApplication.Models
{
    public class BarDbContext : DbContext
    {
        public BarDbContext(DbContextOptions<BarDbContext> options) : base(options) { }

        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
