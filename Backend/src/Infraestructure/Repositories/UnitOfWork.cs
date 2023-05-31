using System.Collections;
using Ecommerce.Application.Persistence;
using Infraestructure.Persistence;

namespace Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable? _repositories; 
        private readonly EcommerceDbContext _context;
        public UnitOfWork(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                
                throw new Exception("ERROR IN TRANSACTION", e);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories is null)
            {
                _repositories = new Hashtable();
            }

            string type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                Type repositoryType = typeof(RepositoryBase<>);
                object respositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context)!;
                _repositories.Add(type, respositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type]!;
        }
    }
}