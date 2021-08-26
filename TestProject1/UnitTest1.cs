using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Abstraction;
using WebApplication1.Controllers;
using WebApplication1.Model;

namespace TestProject1
{
    public class Tests
    {
        private FlattenController controller;
        private Mock<IFlatten> _flattenService;
        private Mock<ILogger<FlattenController>> logMock;
        private List<RouteClass> items;
        private List<StopClass> stops;
        [SetUp]
        public void Setup()
        {
            _flattenService = new Mock<IFlatten>();
            logMock = new Mock<ILogger<FlattenController>>();
            controller = new FlattenController(logMock.Object, _flattenService.Object);
        }

        [Test]
        public void Read_Success_Test()
        {
            stops = new List<StopClass>() {
            new StopClass{ routename= "Route 1",stopname= "Stop 1", objecttype= "tank", objectname= "MT ACE UNIT 3H WATER TANK" },
            new StopClass{ routename= "Route 1",stopname= "Stop 1", objecttype= "meter", objectname= "MT ACE UNIT 3H WATER METER"},
            new StopClass{ routename= "Route 1",stopname= "Stop 1", objecttype= "well", objectname= "MT ACE UNIT 3H WELL" },
            };
            items = new List<RouteClass>()
          {
              new RouteClass()
              {
                  routename="Route 1",
                  stops=new List<StopArrayClass>(){
                      new StopArrayClass {
                          stopname= "Stop 1",
                          objects=new List<ObjectClass>() {
                          new ObjectClass { objectname= "MT ACE UNIT 3H WATER TANK", objecttype="tank" },
                          new ObjectClass { objectname= "MT ACE UNIT 3H WATER METER", objecttype="meter" },
                          new ObjectClass { objectname="MT ACE UNIT 3H WELL", objecttype="well" }
                      }
                      }
                  }
              }
          };
            _flattenService.Setup(x => x.FlatObject(It.IsAny<List<RouteClass>>())).Returns(stops.AsEnumerable());
            var result = controller.Post(items);
            _flattenService.Verify(s => s.FlatObject(items), Times.Once());
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void Read_Null_Test()
        {
            stops = new List<StopClass>() {
            new StopClass{ routename= "Route 1",stopname= "Stop 1", objecttype= "tank", objectname= "MT ACE UNIT 3H WATER TANK" },
            new StopClass{ routename= "Route 1",stopname= "Stop 1", objecttype= "meter", objectname= "MT ACE UNIT 3H WATER METER"},
            new StopClass{ routename= "Route 1",stopname= "Stop 1", objecttype= "well", objectname= "MT ACE UNIT 3H WELL" },
            };
            items = new List<RouteClass>();
            _flattenService.Setup(x => x.FlatObject(It.IsAny<List<RouteClass>>())).Returns(stops.AsEnumerable());
            var result = controller.Post(items);
            _flattenService.Verify(s => s.FlatObject(items), Times.Never());
            var okResult = result as BadRequestResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }
    }
}