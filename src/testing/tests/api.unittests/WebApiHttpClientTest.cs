using System.Text;
using System.Net;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace tests.api.unittests
{
    public class WebApiHttpClientTest
    {
        private const string _testUri = "https://localhost:5001/";

        [Fact]
        public async Task TestGet()
        {
            //Given
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpStatusCode actualStatusCode;
            List<string> actualModel;
            //When
            using(HttpClient client=new HttpClient(clientHandler))
            {
                client.BaseAddress =new Uri(_testUri);
                HttpResponseMessage response = await client.GetAsync("api/Values/get");
                var result = response.Content.ReadAsStringAsync().Result;
                actualStatusCode = response.StatusCode;
                actualModel =JsonConvert.DeserializeObject<List<String>>(result);
                
            }
            //Then
            Assert.Equal(HttpStatusCode.OK, actualStatusCode);
            Assert.Equal(expected: 2, actualModel.Count);
        }

        [Fact]
        public async Task TestPost()
        {
            //Given
            string testValue = "test from xunit";
            var postValue = JsonConvert.SerializeObject(testValue);
            var content = new StringContent(postValue, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpStatusCode actualStatusCode;
            string actual = string.Empty;
            //When
            using(HttpClient client=new HttpClient(clientHandler))
            {
                client.BaseAddress =new Uri(_testUri);
                HttpResponseMessage response = await client.PostAsync("api/Values/post",content);
                var result = response.Content.ReadAsStringAsync().Result;
                actualStatusCode = response.StatusCode;
                actual =JsonConvert.DeserializeObject<String>(result);
            }
            //Then
            Assert.Equal(HttpStatusCode.Created, actualStatusCode);
            Assert.Equal(testValue, actual);
        }
    }
}
