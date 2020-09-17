using LearningTerraform.BusinessLogic.Operations.Commands.CreateOwner;
using LearningTerraform.BusinessLogic.Operations.Commands.CreatePet;
using LearningTerraform.BusinessLogic.Operations.Queries.GetOwnerById;
using LearningTerraform.BusinessLogic.Operations.Queries.GetPetById;
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
            services.AddSingleton<ICreatePetCommandHandler, CreatePetCommandHandler>();
        }

        private static void AddQueryHandlers(IServiceCollection services)
        {
            services.AddSingleton<IGetOwnerByIdQueryHandler, GetOwnerByIdQueryHandler>();
            services.AddSingleton<IGetPetByIdQueryHandler, GetPetByIdQueryHandler>();
        }
    }
}
