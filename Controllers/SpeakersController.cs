using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Recordbook_WebAPI.Models;
using Recordbook_WebAPI.Services;

namespace Recordbook_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("ReactPolicy")]
    public class SpeakersController : Controller
    {
        private readonly ICosmosDbService _cosmosDbService;

        public SpeakersController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        // GET: speakers
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_cosmosDbService.GetSpeakersAsync("Select * from c").Result);
        }

        // Get: speakers/3
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            return Ok(_cosmosDbService.GetSpeakerAsync(id));
        }

    }
}
