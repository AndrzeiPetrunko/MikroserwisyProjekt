using Mikroserwisy.Doctor.Services;

namespace Mikroserwisy.Doctor.Extensions
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
