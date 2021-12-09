using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Managers;
using WebApplication.Models;

namespace WebApiUnitTest
{
    public class RecordsControllerFake : ControllerBase
    {
        private readonly IRecordsManager _manager;

        public RecordsControllerFake(IRecordsManager manager)
        {
            _manager = manager;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<Record>> Get()
        {
            var results = _manager.GetAll();

            if (!results.Any()) return NoContent();

            return Ok(results);
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
            var result = _manager.getAvgTemperature();

            return Ok(result);
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("avgHumi")]
        public ActionResult<List<double>> GetAvgHumidity()
        {
            var result = _manager.getAvgHumidity();

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
            
            var result = _manager.Add(newRecord);
            return Ok(result);
        }
    }
}