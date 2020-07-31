using Aurora.Domain.Interfaces;
using Aurora.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Infra.CrossCutting.InversionOfControl
{
    public static class MySqlRepositoryDependency
    {
        public static void AddMySqlRepositoryDependency(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWorker, WorkerRepository>();
        }
    }
}
