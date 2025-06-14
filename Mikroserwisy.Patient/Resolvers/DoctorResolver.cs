using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Mikroserwisy.PatientApi.Resolvers
{
    public class DoctorResolver
    {
        public async Task<string> ResolverFor(int doctorId)
        {
            return await ResolveFromExternalDictionary(doctorId);
        }
        private async Task<string> ResolveFromExternalDictionary(int doctorId)
        {
            string apiUrl = "http://localhost:5056/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));

                HttpResponseMessage response = await client.GetAsync("doctors");

                string responseData = await response.Content.ReadAsStringAsync();
                List<DoctorDto>? doctors = JsonConvert.DeserializeObject<List<DoctorDto>>(responseData);
                return doctors.FirstOrDefault(x => x.Id == doctorId).DoctorSpecialization;
            }
        }

    }
    public class DoctorDto
    {
        public int Id { get; set; }
        public string? DoctorSpecialization { get; set; }
    }
}

