namespace BreEasy
{
    public class Window
    {
        private string _windowName;
        public int Id { get; set; }

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

        public int LocationId { get; set; }

        public DateTime TimeLastOpened { get; set; }

        public bool IsOpen { get; set; }

        public Window(int id, string windowName, int locationId , DateTime timeLastOpened, bool isOpen)
        {
            Id = id;
            WindowName = windowName;
            LocationId = locationId;
            TimeLastOpened = timeLastOpened;
            IsOpen = isOpen;
        }
        public Window() { }
    }
}
