using CrudTest.Data.Context;
using CrudTest.Data.Interfaces;

namespace CrudTest.Data.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly TestContext Context;

    public UnitOfWork(TestContext context) => Context = context;

    public async Task<bool> Complete() => await Context.SaveChangesAsync() > 0;

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}
