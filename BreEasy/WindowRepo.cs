namespace BreEasy
{
    public class WindowRepo : IRepo<Window>
    {
        // Local instances that gonna be used throughout the class
        private int _idCounter = 1;
        private List<Window> _windowDb = new List<Window>();

        // Constructor
        public WindowRepo() { }

        // Add method implementation
        public void Add(Window obj)
        {
            // Assign a unique Id to the new Window and add it to the list
            obj.Id = _idCounter++;
            _windowDb.Add(obj);
        }

        // GetAll method implementation
        public IEnumerable<Window> GetAll()
        {
            // Return a copy of the list to prevent external modification
            IEnumerable<Window> windows = new List<Window>(_windowDb);
            return windows;
        }

        // GetById method implementation
        public Window GetById(int id)
        {
            // Find and return the Window with the specified Id
            return _windowDb.FirstOrDefault(w => w.Id == id);
        }

        // GetByLocation method implementation
        public Window GetByLocation(int id)
        {
            // Find and return the Window with the specified LocationId
            return _windowDb.FirstOrDefault(w => w.LocationId == id);
        }

        // Remove method implementation
        public Window Remove(int id)
        {
            // Find the Window to remove
            Window windowToRemove = GetById(id);
            // If found, remove it from the list
            if (windowToRemove != null)
            {
                _windowDb.Remove(windowToRemove);
            }
            // Return the removed Window (or null if not found)
            return windowToRemove;
        }

        // Update method implementation
        public Window Update(int id, Window obj)
        {
            // Find the Window to update
            Window windowToUpdate = GetById(id);
            // If found, update its properties
            if (windowToUpdate != null)
            {
                windowToUpdate.WindowName = obj.WindowName;
                windowToUpdate.LocationId = obj.LocationId;
                windowToUpdate.TimeLastOpened = obj.TimeLastOpened;
                windowToUpdate.IsOpen = obj.IsOpen;
            }
            // Return the updated Window (or null if not found)
            return windowToUpdate;
        }
    }
}
