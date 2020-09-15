using System;
using LearningTerraform.BusinessLogic.DataAccess.Abstract;
using LearningTerraform.DataAccess.MsSql.Database;
using Microsoft.Extensions.DependencyInjection;

namespace LearningTerraform.DataAccess.MsSql.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();

            services
                .AddSingleton<Func<DataContext>>(services => () => services.GetRequiredService<DataContext>());

            return services;
        }
    }
}
