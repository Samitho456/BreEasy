namespace BreEasy
{
    public interface IRepo<T>
    {
        /// <summary>
        /// Adds the specified object to the collection.
        /// </summary>
        /// <param name="obj">The object to add to the collection. Cannot be <see langword="null"/>.</param>
        void Add(T obj);

        /// <summary>
        /// Retrieves all items of type <typeparamref name="T"/> from the data source.
        /// </summary>
        /// <remarks>The order of the items in the returned collection is not guaranteed.  Callers should
        /// enumerate the collection to access the items.</remarks>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all items of type <typeparamref name="T"/>.  The collection will
        /// be empty if no items are available.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Retrieves an entity of type <typeparamref name="T"/> by its unique identifier.
        /// </summary>
        /// <remarks>This method is typically used to fetch a single entity from a data source based on
        /// its identifier. Ensure that the identifier corresponds to an existing entity in the data source.</remarks>
        /// <param name="id">The unique identifier of the entity to retrieve. Must be a positive integer.</param>
        /// <returns>The entity of type <typeparamref name="T"/> if found; otherwise, <see langword="null"/>.</returns>
        T GetById(int id);

        /// <summary>
        /// Removes the item with the specified identifier from the collection.
        /// </summary>
        /// <param name="id">The unique identifier of the item to remove.</param>
        /// <returns>The removed item of type <typeparamref name="T"/> if the operation is successful; otherwise, <see
        /// langword="null"/> if no item with the specified identifier exists.</returns>
        T Remove(int id);

        /// <summary>
        /// Retrieves an item based on its location identifier.
        /// </summary>
        /// <remarks>The method assumes that the location identifier corresponds to a valid entry.  Ensure
        /// the identifier is valid and exists in the data source before calling this method.</remarks>
        /// <param name="id">The unique identifier of the location to retrieve the item for.</param>
        /// <returns>The item associated with the specified location identifier.</returns>
        T GetByLocation(int id);

        /// <summary>
        /// Updates an existing entity with the specified identifier using the provided object.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to update.</param>
        /// <param name="obj">The object containing the updated values for the entity.</param>
        /// <returns>The updated entity of type <typeparamref name="T"/>.</returns>
        T Update(int id, T obj);
    }
}
