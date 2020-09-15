using LearningTerraform.BusinessLogic.Operations.Commands.CreateOwner;
using LearningTerraform.BusinessLogic.Operations.Queries.GetOwnerById;
using Microsoft.Extensions.DependencyInjection;

namespace LearningTerraform.BusinessLogic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            AddCommandHandlers(services);
            AddQueryHandlers(services);

            return services;
        }

        private static void AddCommandHandlers(IServiceCollection services)
        {
            services.AddSingleton<ICreateOwnerCommandHandler, CreateOwnerCommandHandler>();
        }

        private static void AddQueryHandlers(IServiceCollection services)
        {
            services.AddSingleton<IGetOwnerByIdQueryHandler, GetOwnerByIdQueryHandler>();
        }
    }
}
