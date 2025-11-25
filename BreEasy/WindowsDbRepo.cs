using BreEasy.EFDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreEasy
{
    public class WindowsDbRepo
    {
        private int _nextId = 0;
        private readonly WindowDbContext _context;

        public WindowsDbRepo(WindowDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Window obj)
        {
            obj.Id = _nextId++;
            _context.Windows.Add(obj);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Window>> GetAll()
        {
            // ToListAsync() returns List<Window>, which implements IEnumerable<Window>
            return await _context.Windows.ToListAsync();
        }

        public async Task<Window> GetById(int id)
        {
            return await _context.Windows.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Window> GetByLocation(int id)
        {
            return await _context.Windows.FirstOrDefaultAsync(w => w.LocationId == id);
        }

        public async Task<Window> Remove(int id)
        {
            Window window = await _context.Windows.FirstOrDefaultAsync(w => w.Id == id);
            if (window != null)
            {
                _context.Windows.Remove(window);
                await _context.SaveChangesAsync();
            }
            return window;
        }

        public async Task<Window> Update(int id, Window obj)
        {
            Window window = await _context.Windows.FirstOrDefaultAsync(w => w.Id == id);
            if (window != null)
            {
                window.WindowName = obj.WindowName;
                window.LocationId = obj.LocationId;
                window.TimeLastOpened = obj.TimeLastOpened;
                window.IsOpen = obj.IsOpen;
                await _context.SaveChangesAsync();
            }
            return window;
        }
    }
}
