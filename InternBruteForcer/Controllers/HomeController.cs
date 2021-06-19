using DDOS_Saldırı.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace DDOS_Saldırı.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        public IWebHostEnvironment _environment { get; }

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _environment = env;

        }
        public class JsonStringResult : ContentResult
        {
            //public JsonStringResult(string json)
            //{
            //    Content = json;
            //    ContentType = "application/json";
            //}
        }
        // GET: Student
        [Route("Post")]
        [HttpPost]
        public JsonResult Login([FromBody]StudentViewModel data)
        {
            Startup.a++; // Bu metot her çalıştığında a değerini 1 arttırıyoruz.
            if (Startup.a > 3) // Eğer a değeri 10'dan fazla olursa.
            {


                return new JsonResult(JsonConvert.SerializeObject("Limit asildi")); // Limit aşıldı yazısını yazdırıyoruz.
            }
            else
                return Json(data.UserName, data.Password); // Front-end tarafından gönderilen değerlerin tutulmasını sağladığımız kod




        }

        [Route("api/GenerateRequests")]
        [HttpPost]
        public JsonResult RequestGenerator([FromBody] RequestViewModel data)
        {
            string[] userNames = System.IO.File.ReadAllLines(System.IO.Path.Combine(_environment.WebRootPath, "username.txt"));
            string[] passwords = System.IO.File.ReadAllLines(System.IO.Path.Combine(_environment.WebRootPath, "password.txt"));
            string[] emails = System.IO.File.ReadAllLines(System.IO.Path.Combine(_environment.WebRootPath, "email.txt"));

            string userInputAreaId = data.formUsernameAreaId;
            string userPasswordAreaId = data.formPasswordAreaId;
            string loginButtonId = data.formLoginButtonId;
            string url = data.formUrl;


            int numberOfRequest = 1;

            Int32.TryParse(data.formRequestsNumber, out numberOfRequest);
            


            var chromeOptions = new ChromeOptions()
            {
                BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe"
            };

            IWebDriver driver = new ChromeDriver(chromeOptions);
            driver.Url = url;
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector($"#{userInputAreaId}")).SendKeys(emails[0]);
            driver.FindElement(By.CssSelector($"#{userPasswordAreaId}")).SendKeys(passwords[0]);
            driver.FindElement(By.CssSelector($"#{userPasswordAreaId}")).SendKeys(passwords[0]);
            
            //driver.FindElement(By.CssSelector($"#{userInputAreaId}")).Click();
            

            


            return Json("OK"); 



        }



        public IActionResult Index()
        {

            //Startup.a++;

            //if (Startup.a == 3)
            //{
            //    //return new JsonResult (JsonConvert.SerializeObject("Limit aşıldı")) ; 


            //    return Content("Limit aşıldı");
            //}
            return View();
        }


        public IActionResult Options()
        {
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
