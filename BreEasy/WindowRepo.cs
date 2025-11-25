namespace BreEasy
{
    public class WindowRepo : IRepo<Window>
    {
        private int _idCounter = 1;
        private List<Window> _windowDb = new List<Window>();

        public WindowRepo() { }

        public void Add(Window obj)
        {
            obj.Id = _idCounter++;
            _windowDb.Add(obj);
        }

        public IEnumerable<Window> GetAll()
        {
            IEnumerable<Window> windows = new List<Window>(_windowDb);
            return windows;
        }

        public Window GetById(int id)
        {
            return _windowDb.FirstOrDefault(w => w.Id == id);
        }

        public Window GetByLocation(int id)
        {
            return _windowDb.FirstOrDefault(w => w.LocationId == id);
        }

        public Window Remove(int id)
        {
            Window windowToRemove = GetById(id);
            if (windowToRemove != null)
            {
                _windowDb.Remove(windowToRemove);
            }
            return windowToRemove;
        }

        public Window Update(int id, Window obj)
        {
            Window windowToUpdate = GetById(id);
            if (windowToUpdate != null)
            {
                windowToUpdate.WindowName = obj.WindowName;
                windowToUpdate.LocationId = obj.LocationId;
                windowToUpdate.TimeLastOpened = obj.TimeLastOpened;
                windowToUpdate.IsOpen = obj.IsOpen;
            }
            return windowToUpdate;
        }
    }
}
