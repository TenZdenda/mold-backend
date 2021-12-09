using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Models;

namespace WebApplication.Managers
{
    public class RecordsManagerDb : IRecordsManager
    {
        private RecordContext _context;

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

            return records;
        }

        public List<double> getAvgTemperature()
        {
            var temperature = from record in _context.Records
                group record.Temperature by record.CreatedAt.Date
                into g
                select g.Average();

            return temperature.ToList();
        }

        public List<double> getAvgHumidity()
        {
            var humidity = from record in _context.Records
                group record.Humidity by record.CreatedAt.Date
                into g
                select g.Average();

            return humidity.ToList();
        }

        public Record GetById(int id)
        {
            return _context.Records.Find(id);
        }
    }
}