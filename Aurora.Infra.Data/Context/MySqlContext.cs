using Aurora.Domain.Entities;
using Aurora.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Infra.Data.Context
{
    public class MySqlContext : DbContext
    {
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseMySql("Server=db4free.net;Port=3306;Database=aurora_api_proj;Uid=aurora_api_proj;Pwd=12345678");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(new UserMap().Configure);
        }
    }
}
