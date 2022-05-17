using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotNETRequestTest.Models;

namespace dotNETRequestTest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        IEnumerable<TestModel> testModels = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:7058/api");

            var responseTest = client.GetAsync("/test");
            responseTest.Wait();

            var result = responseTest.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTest = result.Content.ReadFromJsonAsync<List<TestModel>>();
                readTest.Wait();

                testModels = readTest.Result;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error");
            }
        }
        return View(testModels);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Hello()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
