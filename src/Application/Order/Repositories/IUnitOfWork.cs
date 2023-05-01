namespace Application.Order.Repositories;
public interface IUnitOfWork
{
    Task CommitAsync();
}
