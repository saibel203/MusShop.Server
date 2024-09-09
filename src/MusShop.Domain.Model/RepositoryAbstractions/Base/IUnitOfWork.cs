namespace MusShop.Domain.Model.RepositoryAbstractions.Base;

public interface IUnitOfWork : IDisposable
{
    IRepository Repository();
    Task<int> CommitAsync(CancellationToken cancellationToken);
}