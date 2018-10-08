using Queries.Persistence.Repositories;
using userDemo.Data.Core;
using userDemo.Data.Core.Repositories;

namespace Queries.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDbContext _context;

        public IUserRepository Users { get; private set; }

        public UnitOfWork(UserDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}