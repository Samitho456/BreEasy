namespace BreEasy
{
    public class Window
    {
        // Friendly name for the window to show on the vue page
        private string _windowName;

        // Unique identifier for the window
        public int Id { get; set; }

        // Friendly name for the window with validation on length
        public string WindowName
        {
            get { return _windowName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Window name cannot be null or empty.");
                }
                if (value.Length <= 2)
                {
                    throw new ArgumentException("Window name must be 2 or more characters.");
                }
                _windowName = value;
            }
        }

        // Foreign key to Location table
        public int LocationId { get; set; }

        // Timestamp of when the window was last opened or closed
        public DateTime TimeLastOpened { get; set; }

        // Boolean indicating if the window is currently open or closed
        public bool IsOpen { get; set; }

        public int TimeToClose { get; set; }

        // Constructor
        public Window(int id, string windowName, int locationId , DateTime timeLastOpened, bool isOpen, int timeToClose)
        {
            Id = id;
            WindowName = windowName;
            LocationId = locationId;
            TimeLastOpened = timeLastOpened;
            IsOpen = isOpen;
            TimeToClose = timeToClose;
        }

        // Default constructor
        public Window() { }
    }
}
