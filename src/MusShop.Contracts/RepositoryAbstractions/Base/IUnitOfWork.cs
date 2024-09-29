using MusShop.Contracts.Responses;
using MusShop.Domain.Model.Entities.Base;

namespace MusShop.Contracts.RepositoryAbstractions.Base;

public interface IUnitOfWork : IDisposable
{
    public IGenericRepository<TEntity, TFilter> GetRepository<TEntity, TFilter>()
        where TEntity : BaseEntity
        where TFilter : BaseFilter;

    Task<int> CommitAsync(CancellationToken cancellationToken);
}