using System;
using System.Collections.Generic;
using WebApplication.Managers;
using WebApplication.Models;

namespace WebApiUnitTest
{
    public class RecordsManagerFake : IRecordsManager
    {
        private readonly List<Record> _records;
        private static int _nextId = 1;
        
        public RecordsManagerFake()
        {
            _records = new List<Record>()
            {
                new Record {Id = _nextId++, Temperature = 20, Humidity = 100, Device = "Device-01", CreatedAt = new DateTime(2021, 12, 05, 12, 00, 00)},
                new Record {Id = _nextId++, Temperature = 22, Humidity = 110, Device = "Device-01", CreatedAt = new DateTime(2021, 12, 05, 13, 00, 00)},
                new Record {Id = _nextId++, Temperature = 24, Humidity = 120, Device = "Device-01", CreatedAt = new DateTime(2021, 12, 05, 14, 00, 00)},
                new Record {Id = _nextId++, Temperature = 26, Humidity = 130, Device = "Device-01", CreatedAt = new DateTime(2021, 12, 05, 15, 00, 00)},
            };
        }
        
        public IEnumerable<Record> GetAll()
        {
            return _records;
        }
        
        public Record Add(Record record)
        {
            record.Id = _nextId++;
            _records.Add(record);
            
            return record;
        }
        
        public Record GetById(int id)
        {
            return _records.Find(item => item.Id == id);
        }

        public IEnumerable<Record> GetByDevice(string device)
        {
            var result = _records.FindAll(item => item.Device.Contains(device, StringComparison.OrdinalIgnoreCase));

            return result;
        }
    }
}