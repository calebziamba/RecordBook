using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Recordbook_WebAPI.Models;
using Recordbook_WebAPI.Services;

namespace Recordbook_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("ReactPolicy")]
    public class RecordsController : Controller
    {
        private readonly ICosmosDbService _cosmosDbService;

        public RecordsController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        // GET: records
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_cosmosDbService.GetRecordsAsync("Select * from c").Result);
        }

        // GET: records/5
        [HttpGet("{id}")]
        public ActionResult GetRecord(string id)
        {
            Record RequestedRecord = _cosmosDbService.GetRecordAsync(id).Result;
            return Ok(RequestedRecord);
        }

        // POST: Records
        [HttpPost]
        public ActionResult Create([FromBody] Record message)
        {
            _cosmosDbService.AddRecordAsync(message).Wait();

            foreach (var tag in message.Tags)
                System.Console.WriteLine(tag.Label);

            return CreatedAtAction(nameof(GetRecord), new { id = message.Id }, message);
        }

        // PUT: Records/5
        [HttpPut("{id}")]
        public ActionResult PutRecord(string id, [FromBody] Record message)
        {
            if (id != message.Id)
            {
                return BadRequest(new { message = "Record.id must match request id" });
            }

            try
            {
                _cosmosDbService.UpdateRecordAsync(id, message).Wait();
                return Ok(message);
            }
            catch
            {
                if (_cosmosDbService.GetRecordAsync(id).Result.Equals(null))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // DELETE: Records/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            System.Console.WriteLine(id);
            _cosmosDbService.DeleteRecordAsync(id).Wait();

            return Ok();
        }
    }
}
