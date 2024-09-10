using MusShop.Domain.Model.Entities.Base;

namespace MusShop.Domain.Model.RepositoryAbstractions.Base;

public interface IUnitOfWork : IDisposable
{
    public IGenericRepository<TEntity> GetRepository<TEntity>()
        where TEntity : BaseEntity;

    Task<int> CommitAsync(CancellationToken cancellationToken);
}