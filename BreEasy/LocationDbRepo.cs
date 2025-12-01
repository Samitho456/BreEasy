using BreEasy.EFDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreEasy
{
    public class LocationDbRepo
    {
        // int to increment for each new Location
        private int _nextId = 0;

        // DbContext for Entity Framework
        private readonly WindowDbContext _context;

        // Constructor that accepts a DbContext
        public LocationDbRepo(WindowDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Adds a new <see cref="Location"/> object to the context and assigns it a unique identifier.
        /// </summary>
        /// <remarks>The method assigns a unique identifier to the <paramref name="obj"/> before adding it
        /// to the context. Changes are persisted to the database immediately.</remarks>
        /// <param name="obj">The <see cref="Location"/> object to add. The object's <c>Id</c> property will be set automatically.</param>
        public void Add(Location obj)
        {
            obj.Id = _nextId++;
            _context.Locations.Add(obj);
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves all windows from the data source.
        /// </summary>
        /// <remarks>This method asynchronously retrieves all windows from the underlying data source. 
        /// The result is an enumerable collection of Location objects.  Ensure that the data source is properly
        /// configured and accessible before calling this method.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains an  IEnumerable{T} of Location
        /// objects representing all windows in the data source.</returns>
        public async Task<IEnumerable<Location>> GetAll()
        {
            // ToListAsync() returns List<Location>, which implements IEnumerable<Location>
            return await _context.Locations.ToListAsync();
        }

        /// <summary>
        /// Retrieves a window entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the window to retrieve.</param>
        /// <returns>A <see cref="Location"/> object representing the window with the specified identifier,  or <see
        /// langword="null"/> if no matching window is found.</returns>
        public async Task<Location> GetById(int id)
        {
            return await _context.Locations.FirstOrDefaultAsync(w => w.Id == id);
        }

        /// <summary>
        /// Removes the window with the specified identifier from the database.
        /// </summary>
        /// <remarks>If a window with the specified identifier exists, it is removed from the database,
        /// and the changes are saved asynchronously.</remarks>
        /// <param name="id">The unique identifier of the window to remove.</param>
        /// <returns>The <see cref="Location"/> object that was removed, or <see langword="null"/> if no window with the specified
        /// identifier was found.</returns>
        public async Task<Location> Remove(int id)
        {
            Location window = await _context.Locations.FirstOrDefaultAsync(w => w.Id == id);
            if (window != null)
            {
                _context.Locations.Remove(window);
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
        /// <returns>The updated <see cref="Location"/> object if the window with the specified ID exists; otherwise, <see
        /// langword="null"/>.</returns>
        public async Task<Location> Update(int id, Location obj)
        {
            Location location = await _context.Locations.FirstOrDefaultAsync(w => w.Id == id);
            if (location != null)
            {
                location.LocationName = obj.LocationName;
                location.Id = obj.Id;
                await _context.SaveChangesAsync();
            }
            return location;
        }
    }
}
