using System.Linq.Expressions;
using Ecommerce.Application.Persistence;
using Ecommerce.Application.Specifications;
using Infraestructure.Persistence;
using Infraestructure.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : class
    {
        private readonly EcommerceDbContext _context;

        public RepositoryBase(EcommerceDbContext context)
        {
            this._context = context;
        }

        // insert new record in database from entity T
        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // insert new record in memory server from entity T
        public void AddEntity(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        // insert massive records in memory server from entities T
        public void AddRange(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }
        //Delete record in database from entity T
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        // Delete record in memory server from entity T
        public void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        // delete massive records in memory server from entities T
        public void DeleteRange(IReadOnlyList<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
        // Get all records from database 
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        // get records when the client send a predicate specific
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate, 
            Func<IQueryable<T>, 
            IOrderedQueryable<T>>? orderBy, 
            string? includeString, 
            bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if(disableTracking) query = query.AsNoTracking();
            if(!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);
            if(predicate != null) return (await orderBy(query).ToListAsync());
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate, 
            Func<IQueryable<T>, 
            IOrderedQueryable<T>>? orderBy = null, 
            List<Expression<Func<T, object>>>? includes = null, 
            bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if(disableTracking) query = query.AsNoTracking();
            if(includes!=null) query = includes.Aggregate(query, (current, include) => current.Include(include));
            if(predicate != null) query = query.Where(predicate);
            if(orderBy != null) return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }
         // get record from database by id 
        public async Task<T> GetByIdAsync(int id)
        {
            return (await _context.Set<T>().FindAsync(id))!;
        }

        public async Task<T> GetEntityAsync(Expression<Func<T, bool>>? predicate, 
            List<Expression<Func<T, object>>>? includes = null, 
            bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if(disableTracking) query = query.AsNoTracking();
            if(includes!=null) query = includes.Aggregate(query, (current, include) => current.Include(include));
            if(predicate != null) query = query.Where(predicate);
            return (await query.FirstOrDefaultAsync())!;
        }

        // update record from database in entity T
        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        // update record from memory server in entity T
        public void UpdateEntity(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }



        public async Task<T> GetByIdWithSpec(ISpecification<T> spec)
        {
            return (await ApplySpecification(spec).FirstOrDefaultAsync())!;
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }


        public IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}