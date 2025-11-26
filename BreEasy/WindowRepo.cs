namespace BreEasy
{
    public class WindowRepo : IRepo<Window>
    {
        // counter to auto-incrementing primary key
        private int _idCounter = 1;
        // in-memory list to act as a database
        private List<Window> _windowDb = new List<Window>();

        // constructor
        public WindowRepo() { }

        /// <summary>
        /// Adds a new <see cref="Window"/> object to the database and assigns it a unique identifier.
        /// </summary>
        /// <remarks>The <paramref name="obj"/> parameter must not be null. The method assigns a unique
        /// identifier to the <c>Id</c> property of the object before adding it to the database.</remarks>
        /// <param name="obj">The <see cref="Window"/> object to add. The object's <c>Id</c> property will be set to a unique value.</param>
        public void Add(Window obj)
        {
            // Assign a unique Id to the new Window and add it to the list
            obj.Id = _idCounter++;
            _windowDb.Add(obj);
        }

        /// <summary>
        /// Retrieves all windows from the database.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all <see cref="Window"/> objects in the database.</returns>
        public IEnumerable<Window> GetAll()
        {
            // Return a copy of the list to prevent external modification
            IEnumerable<Window> windows = new List<Window>(_windowDb);
            return windows;
        }

        /// <summary>
        /// Retrieves a <see cref="Window"/> object with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the <see cref="Window"/> to retrieve.</param>
        /// <returns>The <see cref="Window"/> object with the specified identifier, or <see langword="null"/> if no such object
        /// exists.</returns>
        public Window GetById(int id)
        {
            // Find and return the Window with the specified Id
            return _windowDb.FirstOrDefault(w => w.Id == id);
        }

        /// <summary>
        /// Retrieves a window associated with the specified location identifier.
        /// </summary>
        /// <remarks>This method searches the underlying database for the first window that matches the
        /// given location identifier.</remarks>
        /// <param name="id">The unique identifier of the location to search for.</param>
        /// <returns>The <see cref="Window"/> object associated with the specified location identifier,  or <see
        /// langword="null"/> if no matching window is found.</returns>
        public Window GetByLocation(int id)
        {
            // Find and return the Window with the specified LocationId
            return _windowDb.FirstOrDefault(w => w.LocationId == id);
        }
        
        /// <summary>
        /// Removes the window with the specified identifier from the collection.
        /// </summary>
        /// <remarks>If a window with the specified identifier does not exist, the method returns <see
        /// langword="null"/> and no changes are made to the collection.</remarks>
        /// <param name="id">The unique identifier of the window to remove.</param>
        /// <returns>The <see cref="Window"/> object that was removed if it exists; otherwise, <see langword="null"/>.</returns>
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

        /// <summary>
        /// Updates the properties of an existing window with the specified values.
        /// </summary>
        /// <remarks>The method updates the properties of the window identified by <paramref name="id"/>
        /// to match the values provided in <paramref name="obj"/>. If no window with the specified <paramref
        /// name="id"/> is found, the method returns <see langword="null"/>.</remarks>
        /// <param name="id">The unique identifier of the window to update.</param>
        /// <param name="obj">An object containing the updated property values for the window.</param>
        /// <returns>The updated <see cref="Window"/> object if the window with the specified <paramref name="id"/> exists;
        /// otherwise, <see langword="null"/>.</returns>
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
