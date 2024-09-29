using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MusShop.Contracts.RepositoryAbstractions.Base;
using MusShop.Contracts.Responses;
using MusShop.Domain.Model.Entities.Base;

namespace MusShop.Persistence.Repositories.Base;

public class GenericRepository<TEntity, TFilter> : IGenericRepository<TEntity, TFilter>
    where TEntity : BaseEntity
    where TFilter : BaseFilter
{
    protected MusShopDataDbContext Context;

    public GenericRepository(MusShopDataDbContext context)
    {
        Context = context;
    }

    public async Task<TEntity?> GetById(Guid id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public async Task<PaginatedList<TEntity>> GetAll(TFilter? filter = null)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable();
        int totalRecords = await query.CountAsync();

        if (filter is not null)
        {
            int pageSize = 
                filter.PageSize is <= 0 or null ? 6 : (int)filter.PageSize;
            int pageIndex = 
                filter.PageIndex is <= 0 or null ? 0 : (int)filter.PageIndex;

            query = query
                .Skip(pageSize * pageIndex)
                .Take(pageSize);
        }
        
        List<TEntity> result = await query.ToListAsync();
        PaginatedList<TEntity> pageResult = 
            new PaginatedList<TEntity>(result, filter?.PageIndex, totalRecords, filter?.PageSize);

        return pageResult;
    }

    public IQueryable<TEntity> FindQueryable(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>().Where(expression);
        return orderBy != null ? orderBy(query) : query;
    }

    public Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>>? expression,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query =
            expression != null ? Context.Set<TEntity>().Where(expression) : Context.Set<TEntity>();
        return orderBy != null
            ? orderBy(query).ToListAsync(cancellationToken)
            : query.ToListAsync(cancellationToken);
    }

    public Task<List<TEntity>> FindAllAsync(CancellationToken cancellationToken)
    {
        return Context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression,
        string includeProperties)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable();

        query = includeProperties.Split(new[] { ',' },
            StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty)
            => current.Include(includeProperty));

        return await query.SingleOrDefaultAsync(expression);
    }

    public async Task<TEntity> Add(TEntity entity)
    {
        return (await Context.Set<TEntity>().AddAsync(entity)).Entity;
    }

    public void Update(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().UpdateRange(entities);
    }

    public void Delete(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }
}