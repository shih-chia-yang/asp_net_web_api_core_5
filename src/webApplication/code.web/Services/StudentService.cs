using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using code.web.Infrastructure;
using code.web.Services.Dto;
using code.web.ViewModels;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace code.web.Services
{
    public class StudentService:IStudentService
    {
        // private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _apiClient;

        private readonly string _studentByPassUrl;
        
        public StudentService(HttpClient httpClient)
        {
            _apiClient = httpClient;
            _studentByPassUrl = "https://localhost:5101/api";
        }

        public async Task<StudentDto> AddAsync(StudentDto student)
        {
            var uri = API.Student.BaseUri(_studentByPassUrl);
            var studentContent = new StringContent(JsonConvert.SerializeObject(student), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, studentContent);
            response.EnsureSuccessStatusCode();
            return student;
        }

        public async Task DeleteAsync(int id)
        {
            var uri = API.Student.BaseUriWithParam(_studentByPassUrl,id.ToString());
            var response = await _apiClient.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Student> FindAsync(string StudentId)
        {
            var uri = API.Student.BaseUriWithParam(_studentByPassUrl,StudentId);
            var response = await _apiClient.GetAsync(uri);
            var responseString = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Student>(responseString);
        }

        public async Task<IEnumerable<Student>> GetAllAsync(SortOrder sort)
        {
            var uri = API.Student.BaseUri(_studentByPassUrl);
            if(sort !=null)
            {
                var query = new Dictionary<string, string>
                {
                    ["SortBy"] = sort.SortBy,
                    ["IsAscending"] = sort.IsAscending.ToString(),
                };
                uri = QueryHelpers.AddQueryString(uri, query);
            }
            var response = await _apiClient.GetAsync(uri);
            var responseString = response.Content.ReadAsStringAsync().Result;
            return string.IsNullOrEmpty(responseString) ?
                Enumerable.Empty<Student>() :
                JsonConvert.DeserializeObject<IEnumerable<Student>>(responseString);
        }

        public async Task<Student> UpdateAsync(Student student)
        {
            var uri = API.Student.BaseUri(_studentByPassUrl);
            var updateContent = new StringContent(JsonConvert.SerializeObject(student), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PutAsync(uri, updateContent);
            response.EnsureSuccessStatusCode();
            return student;
        } 
    }
}