using Microsoft.AspNetCore.Mvc;
using navidocAssignment.DataModels;
using navidocAssignment.Helpers;
using navidocAssignment.Models;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace navidocAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult readTxtFile(IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment)
        {

            file = Request.Form.Files[0];
            Stream stream = file.OpenReadStream();
            System.IO.StreamReader fileReader = new System.IO.StreamReader(stream);
            BarchartDataModel barcharDataModel = new BarchartDataModel();
            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                string[] strArray = line.Split(':');
                barcharDataModel.labels.Add(strArray[0]);
                barcharDataModel.colors.Add(strArray[1]);
                barcharDataModel.data.Add(Int32.Parse(strArray[2]));
            }
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };

            return Json(barcharDataModel,options);
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