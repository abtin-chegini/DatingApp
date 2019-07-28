using Microsoft.EntityFrameworkCore;
using DatingApp.API.Model;
namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {

public DataContext(DbContextOptions<DataContext> options) : base(options)
{
    
}

public DbSet<Values>  Value { get; set; }


    }
}