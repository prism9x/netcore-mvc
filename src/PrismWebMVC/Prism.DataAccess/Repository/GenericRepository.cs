using Microsoft.EntityFrameworkCore;
using Prism.DataAccess.Data;
using System.Linq.Expressions;

namespace Prism.DataAccess.Repository
{
    public class GenericRepository<T>(PrismDbContext db) where T : class
    {
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter)
        {
            IQueryable<T> query = db.Set<T>();

            if (filter is null)
            {
                return await query.AsNoTracking().ToListAsync();
            }

            return await query.AsNoTracking().Where(filter).ToListAsync();
        }

        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> filter)
        {
            return await db.Set<T>().AsNoTracking()
                                            .FirstOrDefaultAsync(filter);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await db.Set<T>().AddAsync(entity);
            return entity;
        }

        public T Update(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public void Delete(T entity)
        {
            db.Set<T>().Remove(entity);
        }
    }
}