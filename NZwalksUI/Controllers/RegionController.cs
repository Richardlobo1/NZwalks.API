using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalksUI.Models;
using NZwalksUI.Models.DTO;
using System.Net.Http.Json;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;

namespace NZwalksUI.Controllers
{
    public class RegionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
     

        public RegionController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> regions = new List<RegionDto>();
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponcemessage = await client.GetAsync("https://localhost:7019/api/Regions");
                httpResponcemessage.EnsureSuccessStatusCode();
                regions.AddRange(await httpResponcemessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
                //ViewBag.Response=stringResponceBody;

                

            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                // Optionally, you can return an error view or message to the user
                return View("Error");
            }
            return View(regions);
            
             
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Add(AddRegionViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMeassage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7019/api/Regions"),
                Content = new StringContent(JsonSerializer.Serialize(model),Encoding.UTF8,"application/json")
            };

            var httpResponceMesage=await client.SendAsync(httpRequestMeassage);
            httpResponceMesage.EnsureSuccessStatusCode();
            var responce=await httpResponceMesage.Content.ReadFromJsonAsync<RegionDto>();  
            if(responce is not null)
            {
                return RedirectToAction("Index", "Region");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult>Edit(Guid Id)
        {
            ViewBag.Id = Id;
            var client = _httpClientFactory.CreateClient();
            var Responce= await client.GetFromJsonAsync<RegionDto>($"https://localhost:7019/api/Regions/{Id.ToString()}");
            if(Responce is not null)
            {
                return View(Responce);
            }
            return View(null);
        }
        [HttpPost]
        public  async Task<IActionResult> Edit(RegionDto  regionDto)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7019/api/Regions/{regionDto.Id.ToString()}"),
                Content = new StringContent(JsonSerializer.Serialize(regionDto), Encoding.UTF8, "application/json")
            };
           var httpsResponceMessage=   await client.SendAsync(httpRequest);
            httpsResponceMessage.EnsureSuccessStatusCode();

            var responce = await httpsResponceMessage.Content.ReadFromJsonAsync<RegionDto>();
            if(responce is not null)
            {
                return RedirectToAction("Index", "Region");
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpsResponceMessage = await client.DeleteAsync($"https://localhost:7019/api/Regions/{request.Id.ToString()}");
                httpsResponceMessage.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Region");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
               
            }

            return View("Edit");

        }
    }
}
