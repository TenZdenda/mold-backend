using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Models;

namespace WebApplication.Managers
{
    public class RecordsManagerDb : IRecordsManager
    {
        private readonly RecordContext _context;

        public RecordsManagerDb(RecordContext context)
        {
            _context = context;
        }

        public Record Add(Record newRecord)
        {
            newRecord.Id = 0;
            _context.Records.Add(newRecord);

            _context.SaveChanges();

            return newRecord;
        }

        public IEnumerable<Record> GetByDevice(string device)
        {
            IEnumerable<Record> records = from record in _context.Records where record.Device == device select record;

            return records;
        }

        public IEnumerable<Record> GetAll()
        {
            IEnumerable<Record> records = from record in _context.Records select record;

            return records.OrderByDescending(r => r.CreatedAt);
        }

        public List<object> GetAvgTemperature()
        {
            var temperature = from record in _context.Records
                group record.Temperature by record.CreatedAt.Date
                into g
                select new { Temperature = g.Average(), CreatedAt = g.Key };

            return new List<object> {temperature.OrderBy(o => o.CreatedAt).ToList()};
        }

        public List<object> GetAvgHumidity()
        {
            var humidity = from record in _context.Records
                group record.Humidity by record.CreatedAt.Date
                into g
                select new {Humidity = g.Average(), CreatedAt = g.Key};

            return new List<object> {humidity.OrderBy(o => o.CreatedAt).ToList()};
        }

        public Record GetLastRecord(string device)
        {
            return _context.Records.OrderByDescending(r => r.CreatedAt).FirstOrDefault(r => r.Device.ToLower() == device.ToLower());
        }

        public Record GetById(int id)
        {
            return _context.Records.Find(id);
        }
    }
}