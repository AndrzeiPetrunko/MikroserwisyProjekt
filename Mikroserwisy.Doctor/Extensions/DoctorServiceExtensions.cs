using Mikroserwisy.DoctorApi;
using Mikroserwisy.DoctorApi.Services;

namespace Mikroserwisy.DoctorApi.Extensions
{
    public static class DoctorServiceExtensions
    {
        public static IServiceCollection AddDoctorServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<DoctorContextDb, DoctorContextDb>();
            serviceCollection.AddTransient<DoctorService>();
            return serviceCollection;
        }
    }
}
