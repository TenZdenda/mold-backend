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

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public ActionResult<Record> Post([FromBody] Record record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var item = _manager.Add(record);
            return Ok(item);
        }
    }
}