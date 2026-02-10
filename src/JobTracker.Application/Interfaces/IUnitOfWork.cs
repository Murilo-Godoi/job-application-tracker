namespace JobTracker.Application.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChanges(CancellationToken ct);
}
