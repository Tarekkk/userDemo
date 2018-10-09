using Microsoft.EntityFrameworkCore;
using userDemo.Data.Core.Domain;

namespace Queries.Persistence
{
    public class UserDbContext : DbContext
    {

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> User { get; set; }
    }
}
