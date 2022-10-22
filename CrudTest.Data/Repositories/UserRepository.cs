using CrudTest.Data.Context;
using CrudTest.Data.Interfaces;
using CrudTest.Domain.Models;

namespace CrudTest.Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(TestContext context) : base(context)
    {
        
    }
}
