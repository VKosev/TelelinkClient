using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TelelinkClient.ViewModels;

namespace TelelinkClient.Controllers
{
    public class AppController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public AppController(IHttpClientFactory clientFactory)
        {
           
            this._clientFactory = clientFactory;
           
        }

        [HttpGet]
        public async Task<IActionResult> Owners()
        {
            string token = Request.Cookies["Token"];

            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://localhost:44393/api/App/Owners");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "TelelinkClient");
            
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);
           
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                //JObject o = JObject.Parse(responseData);

                ViewBag.SomeData = responseData.ToString();
            }
            else
            {
                string responseData = await response.Content.ReadAsStringAsync();

                ViewBag.SomeData = responseData.ToString();
            }
            return View("Welcome");
        }

        [HttpGet]
        public async Task<IActionResult> AllData(int id) //id param used as flag indicating what sorting to do
        {
            string token = Request.Cookies["Token"];
            string url;
            if (id != 0) { url = "https://localhost:44393/api/App/AllData?id=" + id; }
            else { url = "https://localhost:44393/api/App/AllData"; }
            Console.WriteLine(url);

             var request = new HttpRequestMessage(HttpMethod.Get, url
                 );
             request.Headers.Add("Accept", "application/json");
             request.Headers.Add("User-Agent", "TelelinkClient");

             var client = _clientFactory.CreateClient();
             client.DefaultRequestHeaders.Accept.Add(
                 new MediaTypeWithQualityHeaderValue("application/json"));
             client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", token);

             var response = await client.SendAsync(request);

             if (response.IsSuccessStatusCode)
             {
                 string responseData = await response.Content.ReadAsStringAsync();
                 List<AllUsersDataViewModel> allUsersData = JsonConvert.DeserializeObject<List<AllUsersDataViewModel>>(responseData);
                 
                 ViewBag.AllUsersData = allUsersData;
                 return View();
             }
             else
             {
                 string responseData = await response.Content.ReadAsStringAsync();

                 ViewBag.SomeData = responseData.ToString();
                 return View();
             }
            

        }
        [HttpGet]
        public async Task<IActionResult> UserData()
        {
            string token = Request.Cookies["Token"];

            var cookie= new { username = Request.Cookies["Username"] };

            string url = "https://localhost:44393/api/App/UserData?username="+ cookie.username;

            var request = new HttpRequestMessage(HttpMethod.Get,url);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "TelelinkClient");

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);
           
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                UserDataViewModel userDataViewModel = JsonConvert.DeserializeObject<UserDataViewModel>(responseData);

                ViewBag.SomeData = responseData.ToString();

                return View(userDataViewModel);
            }
            else
            {
                string responseData = await response.Content.ReadAsStringAsync();

                ViewBag.SomeData = responseData.ToString();
            }
            return View("Welcome");
        }

        [HttpGet]
       public async Task<IActionResult> AddAssignModels()
        {
            string token = Request.Cookies["Token"];

            var cookie = new { username = Request.Cookies["Username"] };

            string url = "https://localhost:44393/api/App/AddAssignModels?username=" + cookie.username;

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "TelelinkClient");

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                List<ModelAddsToOwnerCountViewModel> addsToOwnerCountViewModel = JsonConvert.DeserializeObject<List<ModelAddsToOwnerCountViewModel>>(responseData);

                ViewBag.addsToOwnerCountViewModel = addsToOwnerCountViewModel;

                return View();
            }
            else
            {
                string responseData = await response.Content.ReadAsStringAsync();

                ViewBag.SomeData = responseData.ToString();
            }
            return View("Welcome");
        }
        [HttpGet]
        public async Task<IActionResult> AddModel(string modelName)
        {
            string token = Request.Cookies["Token"];

            string url = "https://localhost:44393/api/App/AddModel?modelName=" + modelName;

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "TelelinkClient");

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("AddAssignModels", "App");
            }
            else
            {
                string addModelError = await response.Content.ReadAsStringAsync();
                return BadRequest(addModelError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> AssignModel(string description, int id)
        {
            string token = Request.Cookies["Token"];
            var cookie = new { userName = Request.Cookies["Username"] };

            string url = "https://localhost:44393/api/App/AssignModel?userName="
                + cookie.userName + "&id=" + id + "&description=" + description;

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "TelelinkClient");

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {              
                return RedirectToAction("AddAssignModels", "App");
            }
            else
            {
                string addModelError = await response.Content.ReadAsStringAsync();
                return BadRequest(addModelError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteModelFromUser(string userName, int id) // id is the id of the model;
        {
            string token = Request.Cookies["Token"];

            var cookie = new { userName = Request.Cookies["Username"] };

            string url = "https://localhost:44393/api/App/DeleteModelFromUser?userName="
                + cookie.userName + "&id=" + id;

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "TelelinkClient");

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {                
                return RedirectToAction("AddAssignModels", "App");
            }
            else
            {
                string addModelError = await response.Content.ReadAsStringAsync();
                return BadRequest(addModelError);
            }

        }
    }
}
