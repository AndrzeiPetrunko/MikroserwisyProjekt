using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Mikroserwisy.PatientApi.Resolvers
{
    public class DoctorResolver
    {
            public async Task<string> ResolverFor(int doctorId)
            {
                var doctor = await GetDoctorById(doctorId);
                if (!doctor.IsAvailable)
                {
                    throw new InvalidOperationException($"Doctor with ID {doctorId} is not available.");
                }

                return doctor.DoctorSpecialization;
            }

            private async Task<DoctorDto> GetDoctorById(int doctorId)
            {
                string apiUrl = "http://localhost:5056/";

                using var client = new HttpClient();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("doctors");
                var responseData = await response.Content.ReadAsStringAsync();

                var doctors = JsonConvert.DeserializeObject<List<DoctorDto>>(responseData);
                var doctor = doctors.FirstOrDefault(d => d.Id == doctorId);

                if (doctor == null)
                    throw new InvalidOperationException($"Doctor with ID {doctorId} not found.");

                return doctor;
            }
        }


    }
    public class DoctorDto
    {
        public int Id { get; set; }
        public string DoctorSpecialization { get; set; }
        public bool IsAvailable { get; set; }
    }


