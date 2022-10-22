using System.Linq.Expressions;

namespace CrudTest.Data.Interfaces;

public interface IRepository<T> : IDisposable where T : class
{
    T? GetById(int id);
    IQueryable<T> Find(Expression<Func<T, bool>> expression);
    int Count(Expression<Func<T, bool>>? expression = null);
    void Update(T entity);
    void Add(T entity);
    void AddRange(IEnumerable<T> entity);
    void Remove(T entity);
}
