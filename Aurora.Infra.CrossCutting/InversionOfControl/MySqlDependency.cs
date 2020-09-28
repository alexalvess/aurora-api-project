using Aurora.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Infra.CrossCutting.InversionOfControl
{
    public static class MySqlDependency
    {
        public static void AddMySqlDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MySqlContext>(options =>
            {
                var server = configuration["database:mysql:server"];
                var port = configuration["database:mysql:port"];
                var database = configuration["database:mysql:database"];
                var username = configuration["database:mysql:username"];
                var password = configuration["database:mysql:password"];

                options.UseMySql($"Server={server};Port={port};Database={database};Uid={username};Pwd={password}", opt =>
                {
                    opt.CommandTimeout(180);
                    opt.EnableRetryOnFailure(5);
                });
            });
        }
    }
}
