namespace CrudTest.Data.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<bool> Complete();
}
