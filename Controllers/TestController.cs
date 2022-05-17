using dotNETRequestTest.Models;

using Microsoft.AspNetCore.Mvc;

namespace dotNETRequestTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<TestModel> Create(TestModel testModel)
    {
        testModel.Name = "Tested";
        testModel.Age = 30;

        return CreatedAtAction("Success", testModel);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<TestModel> Get()
    {
        var testModel = new TestModel
        {
            Age = 23,
            Name = "Jerry",
            TestID = Guid.NewGuid().ToString()
        };

        List<TestModel> models = new List<TestModel>();
        models.Add(testModel);

        return Ok(models);
    }
}
