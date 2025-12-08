using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreEasy.DTO
{
    public class DTOWindow
    {
        public int Id { get; set; }
        public string WindowName { get; set; }
        public int LocationId { get; set; }
        public DateTime TimeLastOpened { get; set; }
        public bool IsOpen { get; set; }

        // Constructor
        public DTOWindow(int id, string windowName, int locationId, DateTime timeLastOpened, bool isOpen)
        {
            Id = id;
            WindowName = windowName;
            LocationId = locationId;
            TimeLastOpened = timeLastOpened;
            IsOpen = isOpen;
        }
    }
}
