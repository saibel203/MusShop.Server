using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MusShop.Domain.Model.Entities.Base;
using MusShop.Domain.Model.RepositoryAbstractions.Base;

namespace MusShop.Persistence.Repositories.Base;

public class Repository : IRepository
{
    private readonly MusShopDataDbContext _context;

    public Repository(MusShopDataDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetById<T>(Guid id)
        where T : BaseEntity<Guid>
    {
        return await _context.Set<T>().FindAsync(id);
    }
    
    public async Task<IEnumerable<T>> GetAll<T>()
        where T : BaseEntity<Guid>
    {
        return await _context.Set<T>().ToListAsync();
    }

    public IQueryable<T> FindQueryable<T>(Expression<Func<T, bool>> expression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        where T : BaseEntity<Guid>
    {
        IQueryable<T> query = _context.Set<T>().Where(expression);
        return orderBy != null ? orderBy(query) : query;
    }

    public Task<List<T>> FindListAsync<T>(Expression<Func<T, bool>>? expression, Func<IQueryable<T>,
        IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default)
        where T : class
    {
        IQueryable<T> query = expression != null ? _context.Set<T>().Where(expression) : _context.Set<T>();
        return orderBy != null
            ? orderBy(query).ToListAsync(cancellationToken)
            : query.ToListAsync(cancellationToken);
    }

    public Task<List<T>> FindAllAsync<T>(CancellationToken cancellationToken)
        where T : BaseEntity<Guid>
    {
        return _context.Set<T>().ToListAsync(cancellationToken);
    }

    public Task<T?> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression, string includeProperties)
        where T : BaseEntity<Guid>
    {
        IQueryable<T> query = _context.Set<T>().AsQueryable();

        query = includeProperties.Split(new char[] { ',' },
            StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty)
            => current.Include(includeProperty));

        return query.SingleOrDefaultAsync(expression);
    }

    public T Add<T>(T entity)
        where T : BaseEntity<Guid>
    {
        return _context.Set<T>().Add(entity).Entity;
    }

    public void Update<T>(T entity)
        where T : BaseEntity<Guid>
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void UpdateRange<T>(IEnumerable<T> entities)
        where T : BaseEntity<Guid>
    {
        _context.Set<T>().UpdateRange(entities);
    }

    public void Delete<T>(T entity)
        where T : BaseEntity<Guid>
    {
        _context.Set<T>().Remove(entity);
    }
}