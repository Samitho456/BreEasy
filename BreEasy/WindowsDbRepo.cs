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
    

        // DbContext for Entity Framework
        private readonly WindowDbContext _context;

        // Constructor that accepts a DbContext
        public WindowsDbRepo(WindowDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Adds a new <see cref="Window"/> object to the context and assigns it a unique identifier.
        /// </summary>
        /// <remarks>The method assigns a unique identifier to the <paramref name="obj"/> before adding it
        /// to the context. Changes are persisted to the database immediately.</remarks>
        /// <param name="obj">The <see cref="Window"/> object to add. The object's <c>Id</c> property will be set automatically.</param>
        public void Add(Window obj)
        {
            _context.Windows.Add(obj);
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves all windows from the data source.
        /// </summary>
        /// <remarks>This method asynchronously retrieves all windows from the underlying data source. 
        /// The result is an enumerable collection of Window objects.  Ensure that the data source is properly
        /// configured and accessible before calling this method.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains an  IEnumerable{T} of Window
        /// objects representing all windows in the data source.</returns>
        public async Task<IEnumerable<Window>> GetAll()
        {
            // ToListAsync() returns List<Window>, which implements IEnumerable<Window>
            return await _context.Windows.ToListAsync();
        }

        /// <summary>
        /// Retrieves a window entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the window to retrieve.</param>
        /// <returns>A <see cref="Window"/> object representing the window with the specified identifier,  or <see
        /// langword="null"/> if no matching window is found.</returns>
        public async Task<Window> GetById(int id)
        {
            return await _context.Windows.FirstOrDefaultAsync(w => w.Id == id);
        }

        /// <summary>
        /// Retrieves the first window associated with the specified location identifier.
        /// </summary>
        /// <remarks>This method performs an asynchronous database query to find the first window with a
        /// matching location identifier. Ensure that the database context is properly configured and accessible before
        /// calling this method.</remarks>
        /// <param name="id">The unique identifier of the location to search for.</param>
        /// <returns>A <see cref="Window"/> object representing the first window found for the specified location, or <see
        /// langword="null"/> if no matching window is found.</returns>
        public async Task<Window> GetByLocation(int id)
        {
            return await _context.Windows.FirstOrDefaultAsync(w => w.LocationId == id);
        }

        /// <summary>
        /// Removes the window with the specified identifier from the database.
        /// </summary>
        /// <remarks>If a window with the specified identifier exists, it is removed from the database,
        /// and the changes are saved asynchronously.</remarks>
        /// <param name="id">The unique identifier of the window to remove.</param>
        /// <returns>The <see cref="Window"/> object that was removed, or <see langword="null"/> if no window with the specified
        /// identifier was found.</returns>
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

        /// <summary>
        /// Updates the properties of an existing window with the specified ID using the provided values.
        /// </summary>
        /// <remarks>If a window with the specified ID is found, its properties are updated to match the
        /// values in the provided <paramref name="obj"/>. Changes are persisted to the database. If no window with the
        /// specified ID exists, no changes are made, and <see langword="null"/> is returned.</remarks>
        /// <param name="id">The unique identifier of the window to update.</param>
        /// <param name="obj">An object containing the updated values for the window's properties.</param>
        /// <returns>The updated <see cref="Window"/> object if the window with the specified ID exists; otherwise, <see
        /// langword="null"/>.</returns>
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
