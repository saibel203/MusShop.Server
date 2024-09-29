using Microsoft.EntityFrameworkCore;
using MusShop.Contracts.RepositoryAbstractions.Base;
using MusShop.Contracts.Responses;
using MusShop.Domain.Model.Entities.Base;

namespace MusShop.Persistence.Repositories.Base;

public class UnitOfWork : IUnitOfWork
{
    private readonly MusShopDataDbContext _context;
    private readonly Dictionary<Type, object> _repositories;
    private bool _disposed;

    public UnitOfWork(MusShopDataDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public IGenericRepository<TEntity, TFilter> GetRepository<TEntity, TFilter>()
        where TEntity : BaseEntity
        where TFilter : BaseFilter
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (IGenericRepository<TEntity, TFilter>)_repositories[typeof(TEntity)];
        }

        var repository = new GenericRepository<TEntity, TFilter>(_context);
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
        var entries = _context.ChangeTracker
            .Entries()
            .Where(e => e is
                { Entity: BaseEntity, State: EntityState.Added or EntityState.Modified });

        foreach (var entityEntry in entries)
        {
            ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.UtcNow;
            }
        }

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