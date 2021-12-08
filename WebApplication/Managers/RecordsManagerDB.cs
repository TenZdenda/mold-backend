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

        public Record GetById(int id)
        {
            return _context.Records.Find(id);
        }
    }
}