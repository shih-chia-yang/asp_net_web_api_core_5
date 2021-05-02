using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using code.web.Infrastructure;
using code.web.Services.Dto;
using code.web.ViewModels;
using Newtonsoft.Json;

namespace code.web.Services
{
    public class InstructorService:IInstructorService
    {
        private readonly HttpClient _apiClient;

        private readonly string _instructorByPassUrl;
        public InstructorService(HttpClient httpClient)
        {
            _apiClient = httpClient;
            _instructorByPassUrl = "https://localhost:5101/api";
        }

        public async Task<InstructorDto> AddAsync(InstructorDto addnew)
        {
            var uri = API.Instructor.InstructorCommand(_instructorByPassUrl, "Add");
            var content = new StringContent(JsonConvert.SerializeObject(addnew), Encoding.UTF8,"application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return addnew;
        }

        public async Task DeleteAsync(int id)
        {
            var uri = API.Instructor.InstructorCommand(_instructorByPassUrl,$"Delete/{id}");
            var response = await _apiClient.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Instructor> FindAsync(int id)
        {
            var uri = API.Instructor.GetInstructor(_instructorByPassUrl, id.ToString());
            var response = await _apiClient.GetAsync(uri);
            var responseString = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Instructor>(responseString);
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            var uri = API.Instructor.GetInstructorList(_instructorByPassUrl);
            var response = await _apiClient.GetAsync(uri);
            var responseString = response.Content.ReadAsStringAsync().Result;
            return string.IsNullOrEmpty(responseString) ?
                    Enumerable.Empty<Instructor>() :
                    JsonConvert.DeserializeObject<IEnumerable<Instructor>>(responseString);
        }

        public async Task<Instructor> UpdateAsync(Instructor update)
        {
            var uri = API.Instructor.InstructorCommand(_instructorByPassUrl,"Update");
            var updateContent = new StringContent(JsonConvert.SerializeObject(update), Encoding.UTF8, "application/json");
            var response = await _apiClient.PutAsync(uri, updateContent);
            response.EnsureSuccessStatusCode();
            return update;
        }
    }
}