namespace InventorySystem.Core.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll(int size = 20, int pageNumber = 1, string includes = "");
        T? GetByID(int id, string include = "");
        void Add(T obj);
        void Update(T obj);
        void Delete(int id);
        void SaveChanges();
    }
}
