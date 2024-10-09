using DentalClinic.Constants;
using DentalClinic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DentalClinic.Repositories
{
    public class BaseRepository<T>(ApplicationDbContext context) : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context = context;

        public IEnumerable<T> GetAll()
        {
            return [.. _context.Set<T>()];
        }
        public IQueryable<T> GetAllDeferred()
        {
            return _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);

        }
        public T? GetById(string id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T? Find(Expression<Func<T, bool>> predicate, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }


            }
            return query.SingleOrDefault(predicate);
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.SingleOrDefaultAsync(predicate);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, int? skip, int? take, Expression<Func<T, object>>? orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy is not null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            return [.. query];

        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, int? skip, int? take, Expression<Func<T, object>>? orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy is not null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }


            }
            return await query.Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, int skip, int take)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);

            if (skip != 0)
                query = query.Skip(skip);
            if (take != 0)
                query = query.Take(take);


            return await query.Where(predicate).ToListAsync();

        }



        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes is not null)
            {
                foreach (var include in includes)
                    query = query.Include(include);

            }
            return [.. query.Where(predicate)];

        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public async Task<T> AddAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
            return item;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> items)
        {
            _context.Set<T>().AddRange(items);
            return items;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> items)
        {
            _context.Set<T>().RemoveRange(items);
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public void AttachRange(IEnumerable<T> items)
        {
            _context.Set<T>().AttachRange(items);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Count(expression);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().CountAsync(predicate);
        }


        public IEnumerable<T> FindAll(Expression<Func<T, bool>> expression, int take, int skip)
        {
            return [.. _context.Set<T>().Where(expression).Skip(skip).Take(take)];
        }

        /*        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T,bool>> predicate,int skip,int take)
                {
                    return await _context.Set<T>().Where(predicate).Skip(skip).Take(take).ToListAsync();
                }*/
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }
    }
}
