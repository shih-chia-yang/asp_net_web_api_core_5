using System.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cors.client.Models;
using System.Net.Http;

namespace cors.client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IHttpClientFactory _clientFactory;

        public HomeController(ILogger<HomeController> logger,IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var EndPoint = "https://localhost:5001/WeatherForecast";
            // var client = _clientFactory.CreateClient("HttpClientWithSSLUntrusted");

            //  var response = await client.GetAsync(EndPoint);
            //  string apiResponse = response.Content.ReadAsStringAsync().Result;
            //  ViewBag.TestList = apiResponse;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var httpClient = new HttpClient(clientHandler);
            var response = await httpClient.GetAsync(EndPoint);
            try
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                ViewBag.TestList = apiResponse;
            }
            catch (Exception e)
            {

            }


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
