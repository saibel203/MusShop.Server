using System.Linq.Expressions;
using MusShop.Domain.Model.Entities.Base;

namespace MusShop.Domain.Model.RepositoryAbstractions.Base;

public interface IRepository
{
    Task<T?> GetById<T>(Guid id) where T : BaseEntity<Guid>;

    Task<IEnumerable<T>> GetAll<T>()
        where T : BaseEntity<Guid>;

    IQueryable<T> FindQueryable<T>(Expression<Func<T, bool>> expression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        where T : BaseEntity<Guid>;

    Task<List<T>> FindListAsync<T>(Expression<Func<T, bool>>? expression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default)
        where T : class;

    Task<List<T>> FindAllAsync<T>(CancellationToken cancellationToken)
        where T : BaseEntity<Guid>;

    Task<T?> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression, string includeProperties)
        where T : BaseEntity<Guid>;

    T Add<T>(T entity)
        where T : BaseEntity<Guid>;

    void Update<T>(T entity)
        where T : BaseEntity<Guid>;

    void UpdateRange<T>(IEnumerable<T> entities)
        where T : BaseEntity<Guid>;

    void Delete<T>(T entity)
        where T : BaseEntity<Guid>;
}