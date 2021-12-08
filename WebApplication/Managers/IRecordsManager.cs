using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Managers
{
    public interface IRecordsManager
    {
        Record Add(Record record);
        Record GetById(int id);
        IEnumerable<Record> GetByDevice(string device);
        IEnumerable<Record> GetAll();
    }
}