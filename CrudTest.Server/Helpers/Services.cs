using CrudTest.Data.Context;
using CrudTest.Data.Interfaces;
using CrudTest.Data.Repositories;
using CrudTest.Data.UoW;
using CrudTest.Services.Interfaces;
using CrudTest.Services.Profiles;
using CrudTest.Services.Services;

namespace CrudTest.Server.Helpers;

public static class Services
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddDbContext<TestContext>();
        
        services.AddAutoMapper(typeof(Profiles));
        services.AddSwaggerGen();
        
        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        // UoW
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Services
        services.AddScoped<IUserService, UserService>();
    }
}
