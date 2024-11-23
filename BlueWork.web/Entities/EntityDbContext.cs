using Microsoft.EntityFrameworkCore;

namespace BlueWork.web.Entities
{
    public class EntityDbContext : DbContext
    {
        public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options)
        {
        }
        public DbSet<BlueWorkAuth.UserAccount> UserAccounts { get; set; }
        public List<BlueWorkAuth.UserAccount> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
