using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelelinkClient.Models;

namespace TelelinkClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public AccountController( IHttpClientFactory clientFactory)
        {
           this. _clientFactory = clientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(ApplicationUser appUser, String ownerName, String modelName)
        {
            var options = new JsonSerializerOptions
            {
              
                WriteIndented = true
            };

            ViewBag.ViewModelUser = new ApplicationUser()
            {
                UserName = appUser.UserName,
                Password = appUser.Password,
                Email = appUser.Email,
               
            };
            ViewBag.OwnerName = ownerName;
            ViewBag.ModelName = modelName;

            var jsonObject = new
            {
                UserName = appUser.UserName,
                Password = appUser.Password,
                Email = appUser.Email,
                Owner = new { Name = ownerName },
                ModelName = modelName
            };

            String jsonString = JsonSerializer.Serialize(jsonObject, options);

            ViewBag.JSONString = jsonString;
            var request = new HttpRequestMessage(HttpMethod.Post,"https://localhost:44393/api/Account/Register");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "TelelinkClient");
           

            var client = _clientFactory.CreateClient();
      
            request.Content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
           

            var response = await client.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                var responseMSG = await response.Content.ReadAsStringAsync();
                ViewBag.ResponseMessage = responseMSG;
            }
            else
            {
                var responseMSG = await response.Content.ReadAsStringAsync();
                ViewBag.ResponseMessage = responseMSG;
            }

            return View("Welcome");
        }
    }
}
