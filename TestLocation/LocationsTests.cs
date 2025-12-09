using BreEasy;
using BreEasy.EFDbContext;
using Microsoft.EntityFrameworkCore;

namespace TestLocation
{
    [TestClass]
    public class LocaionsTests
    {
        [TestMethod]
        public void TestLocationName()
        {
            var location = new Location();
            location.LocationName = "TestLocation";
            Assert.AreEqual("TestLocation", location.LocationName);
            Assert.AreNotEqual("WrongLocation", location.LocationName);
            Assert.IsNotNull(location.LocationName);
            Assert.ThrowsException<ArgumentNullException>(() => location.LocationName = null);
        }

        [DataRow(0)]
        [DataRow(100)]
        [DataRow(1)]
        [DataRow(99)]
        [TestMethod]
        public void TestHumidity(double humidity)
        {

            var location = new Location();

            location.Humidity = humidity;
            
            Assert.AreEqual(humidity, location.Humidity);
            Assert.IsTrue(location.Humidity >= 0 && location.Humidity <= 100);
        }

        [TestMethod]
        public void TestHumidityFail()
        {
            var location = new Location();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => location.Humidity = -1);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => location.Humidity = 101);
        }
    }
}
