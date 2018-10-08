using userDemo.Data.Core.Domain;
using userDemo.Data.Core.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Queries.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(UserDbContext context) : base(context)
        {
        }
    }
}