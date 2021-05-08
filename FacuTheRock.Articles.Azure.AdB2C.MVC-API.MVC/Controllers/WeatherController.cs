using FacuTheRock.Articles.Azure.AdB2C.MVC_API.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FacuTheRock.Articles.Azure.AdB2C.MVC_API.MVC.Controllers
{
    public class WeatherController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenAcquisition _tokenAcquisition;

        public WeatherController(
            ITokenAcquisition tokenAcquisition,
            HttpClient httpClient)
        {
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
        }

        public async Task<IActionResult> Index()
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { "https://linkedinazureadb2c.onmicrosoft.com/posts/Lector" });
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.GetAsync("https://localhost:5002/weatherforecast");
            var content = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<IEnumerable<WeatherForecastViewModel>>(content);

            return View(values);
        }
    }
}
