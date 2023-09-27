using Microsoft.EntityFrameworkCore;
using WeatherApi.Model;

namespace WeatherApi.Context;

public class WeatherApiContext : DbContext
{
    public DbSet<City> Cities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=WeatherApi;User Id=sa;Password=xXx_FreshNuts420_xXx;TrustServerCertificate=True;");
    }
}