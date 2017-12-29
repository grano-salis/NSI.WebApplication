using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.Extensions.Options;

namespace NSI.WebApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly string _restURL;
        private readonly string _webAppURL;
        public LoginController(IOptions<RestConfig> restConfig)
        {
            _restURL = restConfig.Value.RestURL;
            _webAppURL = "~/wwwroot/dist/index.html";
        }

        public IActionResult Index([FromForm]string username, [FromForm]string password)
        {
        
            if (Request.Cookies.ContainsKey("JWT.Token"))
            {
                var jwtToken = Request.Cookies["JWT.Token"];

                var cl = new HttpClient();
                cl.DefaultRequestHeaders.Accept.Clear();
                cl.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                cl.DefaultRequestHeaders.Add("JWTToken", jwtToken);
                cl.DefaultRequestHeaders.Add("SelectedBranch", "0");

                try
                {
                    var checkStr = cl.GetStringAsync(string.Format("{0}/api/authorization/checkToken", _restURL)).Result;
                    if (string.IsNullOrWhiteSpace(checkStr) || checkStr.Trim('"') != "OK")
                    {
                        Response.Cookies.Delete("JWT.Token");
                        return View("~/Views/Login.cshtml");
                    }
                }
                catch
                {
                    Response.Cookies.Delete("JWT.Token");
                    return View("~/Views/Login.cshtml");
                }

                ViewBag.JWTToken = jwtToken;
                return View(_webAppURL);
            }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return View("~/Views/Login.cshtml");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var stringTask = client.GetStringAsync(string.Format("{0}/api/Authorization/login?username={1}&passCode={2}", _restURL, username, password)).Result;
            if (string.IsNullOrWhiteSpace(stringTask))
                return View("~/Views/Login.cshtml");

            var loginResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponse>(stringTask);
            if (loginResponse == null || string.IsNullOrWhiteSpace(loginResponse.AccessToken))
                return View("~/Views/Login.cshtml");

            ViewBag.JWTToken = loginResponse.AccessToken;
            Response.Cookies.Append("JWT.Token", loginResponse.AccessToken);
            return View(_webAppURL);
        }

        private class LoginResponse
        {
            public string AccessToken { get; set; }
            public string Status { get; set; }
        }

        [HttpPost]
        public IActionResult Login([FromForm]string username, [FromForm]string password)
        {
            if (Request.Cookies.ContainsKey("JWT.Token"))
            {
                ViewBag.JWTToken = Request.Cookies["JWT.Token"];
                return View(_webAppURL);
            }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return View("~/Views/Login.cshtml");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var stringTask = client.GetStringAsync(string.Format("{0}/api/authorization/login?username={1}&passCode={2}", _restURL, username, password)).Result;
            if (string.IsNullOrWhiteSpace(stringTask))
                return View("~/Views/Login.cshtml");

            var loginResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(stringTask);
            if (loginResponse == null || loginResponse.Status != "Active" || string.IsNullOrWhiteSpace(loginResponse.accessToken))
                return View("~/Views/Login.cshtml");

            ViewBag["JWT.Token"] = loginResponse.accessToken;
            Response.Cookies.Append("JWT.Token", loginResponse.accessToken);
            return View(_webAppURL);
        }
    }
}