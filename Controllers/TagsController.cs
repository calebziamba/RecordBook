using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Recordbook_WebAPI.Services;
using Recordbook_WebAPI.Models;

namespace Recordbook_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("ReactPolicy")]
    public class TagsController : Controller
    {
        private readonly ICosmosDbService _cosmosDbService;

        public TagsController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_cosmosDbService.GetTagsAsync("Select DISTINCT VALUE t FROM t IN c.tags").Result);
        }

        [HttpGet("{value}")]
        public ActionResult Get(string value)
        {
            return Ok(_cosmosDbService.GetTagAsync(value));
        }
    }
}
