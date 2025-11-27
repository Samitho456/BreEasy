using Microsoft.VisualStudio.TestTools.UnitTesting;
using BreEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreEasy.Tests
{
    [TestClass]
    public class WindowRepoTests
    {
        private WindowRepo repo;

        [TestInitialize]
        public void Setup()
        {
            repo = new WindowRepo();
            repo.Add(new Window { Id = 1, WindowName = "Living Room Window", LocationId = 101, TimeLastOpened = DateTime.Now.AddHours(-2), IsOpen = false });
            repo.Add(new Window { Id = 2, WindowName = "Bedroom Window", LocationId = 102, TimeLastOpened = DateTime.Now.AddHours(-5), IsOpen = true });
        }

        [TestMethod]
        public void WindowConstructorTest()
        {
            // Valid window creation
            var window = new Window(1, "Test Window", 100, DateTime.Now, false);
            Assert.AreEqual(1, window.Id);
            Assert.AreEqual("Test Window", window.WindowName);
            Assert.AreEqual(100, window.LocationId);
            Assert.IsFalse(window.IsOpen);

            // Invalid window name (too short)
            Assert.ThrowsException<ArgumentException>(() => new Window(2, "A", 101, DateTime.Now, true));
            // Invalid window name (null)
            Assert.ThrowsException<ArgumentException>(() => new Window(3, null, 102, DateTime.Now, false));
        }

        [TestMethod()]
        public void AddTest()
        {
            // Creating a new window to add
            var newWindow = new Window { WindowName = "Kitchen Window", LocationId = 103, TimeLastOpened = DateTime.Now, IsOpen = true };
            // Add the new window to the repo
            repo.Add(newWindow);

            // Retrieve the window by its assigned ID
            var retrievedWindow = repo.GetById(3); // Assuming this is the third window added
            // Verify that the window was added correctly
            Assert.IsNotNull(retrievedWindow);
            // Check that the properties match
            Assert.AreEqual("Kitchen Window", retrievedWindow.WindowName);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Retrieve all windows from the repo
            var allWindows = repo.GetAll().ToList();
            // Verify that the correct number of windows is returned
            Assert.AreEqual(2, allWindows.Count); // Initially added 2 windows in Setup

        }

        [TestMethod()]
        public void GetByIdTest()
        {
            // Retrieve a window by its ID
            var window = repo.GetById(1);
            // Verify that it doesn't return null.
            Assert.IsNotNull(window);
            // Check that the name properties match
            Assert.AreEqual("Living Room Window", window.WindowName);
        }

        [TestMethod()]
        public void GetByLocationTest()
        {
            // Retrieve a window by its LocationId
            var window = repo.GetByLocation(102);
            // Verify that it doesn't return null.
            Assert.IsNotNull(window);
            // Check that the name properties match
            Assert.AreEqual("Bedroom Window", window.WindowName);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            // Rreate a new window to remove
            var newWindow = new Window { WindowName = "Office Window", LocationId = 104, TimeLastOpened = DateTime.Now, IsOpen = false };
            // Add the new window to the repo
            repo.Add(newWindow);
            // Remove the window by its assigned ID
            var removedWindow = repo.Remove(3);
            // Verify that the removed window is empty
            Assert.AreEqual(2, repo.GetAll().Count());
        }

            [TestMethod()]
        public void UpdateTest()
        {
            // Creates a new window with updated properties
            var updatedWindow = new Window { WindowName = "Updated Living Room Window", LocationId = 101, TimeLastOpened = DateTime.Now, IsOpen = true };
            // Update the existing window in the repo
            repo.Update(1, updatedWindow);

            // Retrieve the updated window by its ID
            var retrievedWindow = repo.GetById(1);
            Assert.IsNotNull(retrievedWindow);
            // Verify that the properties were updated correctly
            Assert.AreEqual("Updated Living Room Window", retrievedWindow.WindowName);
            Assert.AreEqual(101, retrievedWindow.LocationId);
            Assert.IsTrue(retrievedWindow.IsOpen);

            // Test updating a non-existing window
            var nonExistingUpdate = repo.Update(999, updatedWindow);
            Assert.IsNull(nonExistingUpdate);
        }
    }
}