
using API.Entities;

namespace API.Interfaces;
public interface IBaseRepository<T> where T : BaseEntity
{
    void Add(T entity);

    Task<int> SaveAsync();
}