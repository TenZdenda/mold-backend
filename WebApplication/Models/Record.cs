using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Record
    {
        public int Id { get; set; }
        
        [Required]
        public int Temperature { get; set; }
        
        [Required]
        public int Humidity { get; set; }
        
        [Required]
        public string Device { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }

        public Record()
        {
            
        }

        public Record(int id, int temperature, int humidity, string device, DateTime createdAt)
        {
            Id = id;
            Temperature = temperature;
            Humidity = humidity;
            Device = device;
            CreatedAt = createdAt;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Temperature: {Temperature}, Humidity: {Humidity}, Device: {Device}, CreatedAt: {CreatedAt}";
        }
    }
}