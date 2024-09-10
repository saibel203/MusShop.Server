using MusShop.Domain.Model.Entities.Base;
using MusShop.Domain.Model.RepositoryAbstractions.Base;

namespace MusShop.Persistence.Repositories.Base;

public class UnitOfWork : IUnitOfWork
{
    private readonly MusShopDataDbContext _context;
    private Dictionary<Type, object> _repositories;
    private bool _disposed;

    public UnitOfWork(MusShopDataDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public IGenericRepository<TEntity> GetRepository<TEntity>()
        where TEntity : BaseEntity
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (IGenericRepository<TEntity>)_repositories[typeof(TEntity)];
        }

        var repository = new GenericRepository<TEntity>(_context);
        _repositories.Add(typeof(TEntity), repository);
        return repository;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _context.Dispose();
        _disposed = true;
    }
}