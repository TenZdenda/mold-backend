using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Managers;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordsManager _manager;

        public RecordsController(RecordContext context)
        {
            _manager = new RecordsManagerDb(context);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<Record>> Get()
        {
            IEnumerable<Record> result = _manager.GetAll();

            if (!result.Any()) return NoContent();

            return Ok(result);
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Record> Get(int id)
        {
            Record result = _manager.GetById(id);
            if (result == null) return NotFound("No such record, id: " + id);

            return Ok(result);
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("/device/{device}")]
        public ActionResult<Record> Get(string device)
        {
            IEnumerable<Record> result = _manager.GetByDevice(device);
            if (!result.Any()) return NoContent();

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public ActionResult Post([FromBody] Record newRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            Record result = _manager.Add(newRecord);
            return CreatedAtAction("Get", new { id = result.Id }, result);
        }
    }
}