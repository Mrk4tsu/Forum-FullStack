using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MrKatsuWebAPI.Helper;

namespace MrKatsuWebAPI.DataAccess
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
             .Build();
            var connectionString = configuration.GetConnectionString(SystemConstant.DB_CONNECTION_STRING);
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new AppDbContext(optionBuilder.Options);
        }
    }
}
