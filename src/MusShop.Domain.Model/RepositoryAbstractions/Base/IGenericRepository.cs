using System.Linq.Expressions;
using MusShop.Domain.Model.Entities.Base;

namespace MusShop.Domain.Model.RepositoryAbstractions.Base;

public interface IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetById(Guid id);

    Task<IEnumerable<TEntity>> GetAll();

    IQueryable<TEntity> FindQueryable(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);

    Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>>? expression,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default);

    Task<List<TEntity>> FindAllAsync(CancellationToken cancellationToken);

    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, string includeProperties);

    Task<TEntity> Add(TEntity entity);

    void Update(TEntity entity);

    void UpdateRange(IEnumerable<TEntity> entities);

    void Delete(TEntity entity);
}