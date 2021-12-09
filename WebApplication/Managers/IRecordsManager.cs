using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Models;

namespace WebApplication.Managers
{
    public interface IRecordsManager
    {
        Record Add(Record record);
        Record GetById(int id);
        IEnumerable<Record> GetByDevice(string device);
        IEnumerable<Record> GetAll(); 
        List<object> GetAvgTemperature();
        List<object> GetAvgHumidity();
        Record GetLastRecord(string device);
    }
}