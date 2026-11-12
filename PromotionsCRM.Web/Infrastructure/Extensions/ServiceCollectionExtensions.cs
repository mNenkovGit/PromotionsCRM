using PromotionsCRM.Data.Repository.Common;
using PromotionsCRM.Data.Repository.Contracts.Common;
using System.Reflection;

namespace PromotionsCRM.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterRepositoriesFromAssembly(this IServiceCollection serviceCollection, Assembly repositoryAssembly)
        {
            IEnumerable<Type> repositoryImplementationTypes = repositoryAssembly
                .GetTypes()
                .Where(t => t.BaseType != null &&
                t.BaseType.IsGenericType &&
                t.IsAbstract == false &&
                (t.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<>) ))
                .ToArray();

            foreach (Type repositoryImplementationType in repositoryImplementationTypes)
            {
                Type? interfaceType = repositoryImplementationType.GetInterfaces()
                    .Where(it => !it.IsGenericType &&
                    it.GetInterfaces().Any(bit => bit.IsGenericType &&
                                                   (bit.GetGenericTypeDefinition() == typeof(IRepository<>) 
                                                   ))) 
                             .SingleOrDefault();

                if (interfaceType != null)
                {
                    serviceCollection.AddScoped(interfaceType, repositoryImplementationType);
                }
            }

            return serviceCollection;
        }
    }
}
