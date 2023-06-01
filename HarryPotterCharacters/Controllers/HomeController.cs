using HarryPotterCharacters.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;

namespace HarryPotterCharacters.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://legacy--api.herokuapp.com/api/v1/characters");
            var response = client.Send(request);
            response.EnsureSuccessStatusCode();
            Character[] characters = JsonConvert.DeserializeObject<Character[]>(response.Content.ReadAsStringAsync().Result) ?? new Character[] { };
            Character[] filteredCharacters = characters.Where(a => new string[] { "3", "4", "6", "10", "16", "20" }.ToList().Contains(a.Id ?? "")).ToArray();
            return View(filteredCharacters);
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Character()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://legacy--api.herokuapp.com/api/v1/characters");
            var response = client.Send(request);
            response.EnsureSuccessStatusCode();
            Character[] characters = JsonConvert.DeserializeObject<Character[]>(response.Content.ReadAsStringAsync().Result) ?? new Character[] {};
            Character[] filteredCharacters = characters.Where(a => new string[] { "3", "4", "6", "10", "16", "20" }.ToList().Contains(a.Id ?? "")).ToArray();
            return View(filteredCharacters);
        }

        public IActionResult Details(int Id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, String.Concat("https://legacy--api.herokuapp.com/api/v1/characters/",Id.ToString()));
            var response = client.Send(request);
            response.EnsureSuccessStatusCode();
            Details detail = JsonConvert.DeserializeObject<Details>(response.Content.ReadAsStringAsync().Result) ?? new Details();
            return View(detail);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}