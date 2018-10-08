using System;
using userDemo.Data.Core.Repositories;

namespace userDemo.Data.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        int Complete();
    }
}