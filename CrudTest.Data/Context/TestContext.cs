using CrudTest.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudTest.Data.Context;

public class TestContext : DbContext
{
    public DbSet<User> Users { get; set; }
    private string DbPath { get; }
    
    public TestContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "crudtest.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
