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
            return _records;
        }

        public List<double> getAvgTemperature()
        {
            var temperature = from record in _records
                group record.Temperature by record.CreatedAt.Date
                into g
                select g.Average();
            
            return temperature.ToList(); 
        }

        public List<double> getAvgHumidity()
        {
            var humidity = from record in _records
                group record.Humidity by record.CreatedAt.Date
                into g
                select g.Average();

            return humidity.ToList();
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