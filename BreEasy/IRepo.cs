namespace BreEasy
{
    public interface IRepo<T>
    {
        void Add(T obj);
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Remove(int id);
        T GetByLocation(int id);
        T Update(int id, T obj);
    }
}
