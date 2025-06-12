using Mikroserwisy.PatientApi.Services;

namespace Mikroserwisy.PatientApi.Extensions
{
    public static class PatientCollectionExtensions
    {
        public static IServiceCollection AddPatientServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<PatientContextDb, PatientContextDb>();
            serviceCollection.AddTransient<PatientService>();
            return serviceCollection;
        }
    }
}
