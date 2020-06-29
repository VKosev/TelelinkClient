using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser applicationUser, string password)
        {
            var options = new JsonSerializerOptions{ WriteIndented = true };

            // JsonObject is used to perform the serialization.
            var jsonObject = new
            {
                UserName = applicationUser.UserName,
                Password = applicationUser.Password,
            };

            String jsonString = System.Text.Json.JsonSerializer.Serialize(jsonObject, options);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44393/api/Account/Login");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "TelelinkClient");
            request.Content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                // JwtToken anonymus object is used to extract only the token from the response.
                var JwtToken = new { token = new { result = ""}};

                var jsonExtract = JsonConvert.DeserializeAnonymousType(responseData, JwtToken);

                HttpContext.Response.Cookies.Append(
                     "Token", jsonExtract.token.result,
                     new CookieOptions()
                     {
                         SameSite = SameSiteMode.Lax,
                         HttpOnly = true,
                         Secure = true,
                         Expires = DateTime.Now.AddDays(5)

                     });
                HttpContext.Response.Cookies.Append("Username", applicationUser.UserName);
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                ViewBag.ResponseMessage = responseMessage;
            }

            return View("Welcome");
        }

        [HttpGet]
        public IActionResult Register()
        {
          
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(ApplicationUser appUser, String ownerName)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            var jsonObject = new
            {
                UserName = appUser.UserName,
                Password = appUser.Password,
                Email = appUser.Email,
                Owner = new { Name = ownerName },
            };

            String jsonString = System.Text.Json.JsonSerializer.Serialize(jsonObject, options);

            var request = new HttpRequestMessage(HttpMethod.Post,"https://localhost:44393/api/Account/Register");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "TelelinkClient");
            request.Content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

            var client = _clientFactory.CreateClient();
      
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
        
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("Token");
            HttpContext.Response.Cookies.Delete("Username");

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> GenerateFirstAdmin()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://localhost:44393/api/Account/GenerateFirstAdmin");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "TelelinkClient");

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                JObject o = JObject.Parse(responseData);

                ViewBag.SomeData = o.ToString();
            }
            else
            {
                string responseData = await response.Content.ReadAsStringAsync();

                ViewBag.SomeData = responseData;
            }
             return View();
        }
    }
}
