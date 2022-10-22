using System.Linq.Expressions;
using CrudTest.Data.Context;
using CrudTest.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrudTest.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly TestContext Db;
    protected readonly DbSet<T> DbSet;
    
    protected Repository(TestContext context)
    {
        Db = context;
        DbSet = Db.Set<T>();
    }
    
    public void Update(T entity) => DbSet.Update(entity);
    public void Add(T entity) => DbSet.Add(entity);
    public void AddRange(IEnumerable<T> entity) => DbSet.AddRange(entity);
    public IQueryable<T> Find(Expression<Func<T, bool>> expression) => DbSet.Where(expression);
    public int Count(Expression<Func<T, bool>>? expression = null) => DbSet.Count(expression ?? (_ => true));
    public T? GetById(int id) => DbSet.Find(id);
    public void Remove(T entity) => DbSet.Remove(entity);

    public void Dispose()
    {
        Db.Dispose();
        GC.SuppressFinalize(this);
    }
}
