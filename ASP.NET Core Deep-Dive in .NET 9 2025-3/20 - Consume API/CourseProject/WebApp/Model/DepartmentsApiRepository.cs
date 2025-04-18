using System.Net.Http;
using System.Text.Json;
using System.Xml.Linq;

namespace WebApp.Models
{
    public class DepartmentsApiRepository : IDepartmentsApiRepository
    {
        private readonly IHttpClientFactory httpClientFactory;

        public DepartmentsApiRepository(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            var client = httpClientFactory.CreateClient("ApiEndpoints");

            var response = await client.GetAsync($"departments/{id}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Department>(
                responseString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

    }
}
