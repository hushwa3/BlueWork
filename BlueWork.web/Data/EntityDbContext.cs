using BlueWork.web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlueWork.web.Data
{
    public class EntityDbContext : IdentityDbContext
    {
           public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options)
        {
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
