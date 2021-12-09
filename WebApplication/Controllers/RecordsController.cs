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
        [HttpGet("device/{device}")]
        public ActionResult<Record> GetDevice(string device)
        {
            IEnumerable<Record> result = _manager.GetByDevice(device);
            if (!result.Any()) return NoContent();

            return Ok(result);
        }
    
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("avgTemp")]
        public ActionResult<List<double>> GetAvgTemperature()
        {
            var result = _manager.GetAvgTemperature();

            return Ok(result);
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("avgHumi")]
        public ActionResult<List<double>> GetAvgHumidity()
        {
            var result = _manager.GetAvgHumidity();

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult Post([FromBody] Record newRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            Record result = _manager.Add(newRecord);
            return Ok(result);
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("last/{device}")]
        public ActionResult<Record> GetLast(string device)
        {
            Record result = _manager.GetLastRecord(device);
            
            return Ok(result);
        }
    }
}