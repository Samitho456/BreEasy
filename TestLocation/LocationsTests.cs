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

        [TestMethod]
        public void TestHumidity()
        {
            var location = new Location();
            location.Humidity = 55.5;
            Assert.AreEqual(55.5, location.Humidity);
            Assert.AreNotEqual(60.0, location.Humidity);
            Assert.IsTrue(location.Humidity >= 0 && location.Humidity <= 100);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => location.Humidity = -10);
        }
    }
}
