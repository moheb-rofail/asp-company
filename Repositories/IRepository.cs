namespace MyMvcProject.Repositories;

public interface IRepository<T> where T : class
{
    public List<T> GetAll();
    T GetById(int id);

    bool Add(T entity);

    bool Update(int id, T entity);

    bool Remove(int id);

    void Save();
}