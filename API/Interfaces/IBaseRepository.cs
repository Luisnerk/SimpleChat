
using API.Entities;

namespace API.Interfaces;
public interface IBaseRepository<T> where T : BaseEntity
{
    void Add(T entity);
    Task<IEnumerable<T>> GetList(int size, int pageNumber);

    Task<int> SaveAsync();
}