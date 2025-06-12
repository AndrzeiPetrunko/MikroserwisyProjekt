using Mikroserwisy.Patient;
using Mikroserwisy.Patient.Services;

namespace Mikroserwisy.Patient.Extensions
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
