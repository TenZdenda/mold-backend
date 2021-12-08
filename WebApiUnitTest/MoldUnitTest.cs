using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Record = WebApplication.Models.Record;

namespace WebApiUnitTest
{
    public class MoldUnitTest
    {
        private readonly RecordsControllerFake _controller;
        private readonly RecordsManagerFake _service;
        
        public MoldUnitTest()
        {
            _service = new RecordsManagerFake();
            _controller = new RecordsControllerFake(_service);
        }

        [Fact]
        public void Test_Get_All_Records_Returns_Ok_Result()
        {
            var result = _controller.Get();
            
            Assert.IsType<OkObjectResult>(result.Result);
        }
        
        [Fact]
        public void Test_Get_All_Records_Returns_All_Records()
        {
            var result = _controller.Get();
            
            var items = result.Result as OkObjectResult;
            var list = items.Value as List<Record>;

            Assert.IsType<List<Record>>(items.Value);
            Assert.Equal(4, list.Count);
        }
        
        [Fact]
        public void Test_Get_Specific_Record_Unknown_Id_Returns_Not_Found_Result()
        {
            // Act
            var result = _controller.Get(1000);
            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
        
        /*[Fact]
        public void Test_Get_Specific_Record_Returns_Record()
        {
            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<Record>(result.Value);
            
            //var record = result.Value;
            //Assert.Equal(1, record.Id);
            //Assert.Equal(20, record.Temperature);
        }

        [Fact]
        public void Test_Create_New_Record()
        {
            var testRecord = new Record()
            {
                Temperature = 25,
                Humidity = 100,
                Device = "Device-01",
                CreatedAt = new DateTime(2021, 12, 08, 12, 00, 00)
            };
            
            var createdResponse = _controller.Post(testRecord);
           
            Assert.IsType<OkObjectResult>(createdResponse);
        }

        [Fact]
        public void Test_Create_New_Record_With_Wrong_Record_Model_Returns_Bad_Request()
        {
            var humidityMissingRecord = new Record()
            {
                Temperature = 20,
                Device = "Device-02",
                CreatedAt = new DateTime(2021, 12, 08, 12, 00, 00)
            };
            
            _controller.ModelState.AddModelError("Humidity", "Required");
            var badResponse = _controller.Post(humidityMissingRecord);
            
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }*/
    }
}