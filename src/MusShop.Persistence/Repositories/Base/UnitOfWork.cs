using MusShop.Domain.Model.RepositoryAbstractions.Base;

namespace MusShop.Persistence.Repositories.Base;

public class UnitOfWork : IUnitOfWork
{
    private readonly MusShopDataDbContext _context;
    private bool _disposed;

    public UnitOfWork(MusShopDataDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IRepository Repository()
    {
        return new Repository(_context);
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