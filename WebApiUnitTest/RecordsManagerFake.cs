using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Managers;
using WebApplication.Models;

namespace WebApiUnitTest
{
    public class RecordsManagerFake : IRecordsManager
    {
        private readonly List<Record> _records;
        
        public RecordsManagerFake()
        {
            _records = new List<Record>()
            {
                new Record {Id = 1, Temperature = 20, Humidity = 100, Device = "device-01", CreatedAt = new DateTime(2021, 12, 05, 12, 00, 00)},
                new Record {Id = 2, Temperature = 22, Humidity = 110, Device = "device-01", CreatedAt = new DateTime(2021, 12, 05, 13, 00, 00)},
                new Record {Id = 3, Temperature = 24, Humidity = 120, Device = "device-01", CreatedAt = new DateTime(2021, 12, 05, 14, 00, 00)},
                new Record {Id = 4, Temperature = 26, Humidity = 130, Device = "device-01", CreatedAt = new DateTime(2021, 12, 05, 15, 00, 00)},
            };
        }
        
        public IEnumerable<Record> GetAll()
        {
            return _records.OrderByDescending(r => r.CreatedAt);
        }

        public List<object> GetAvgTemperature()
        {
            var temperature = from record in _records
                group record.Temperature by record.CreatedAt.Date
                into g
                select new { Temperature = g.Average(), CreatedAt = g.Key };

            return new List<object> {temperature.OrderBy(o => o.CreatedAt).ToList()};
        }

        public List<object> GetAvgHumidity()
        {
            var humidity = from record in _records
                group record.Humidity by record.CreatedAt.Date
                into g
                select new {Humidity = g.Average(), CreatedAt = g.Key};

            return new List<object> {humidity.OrderBy(o => o.CreatedAt).ToList()};
        }

        public Record GetLastRecord(string device)
        {
            throw new NotImplementedException();
        }

        public Record Add(Record record)
        {
            _records.Add(record);
            
            return record;
        }
        
        public Record GetById(int id)
        {
            return _records.Find(item => item.Id == id);
        }

        public IEnumerable<Record> GetByDevice(string device)
        {
            IEnumerable<Record> records = from record in _records where record.Device == device select record;

            return records;
        }
    }
}