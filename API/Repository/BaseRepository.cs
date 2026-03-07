
using API.Data;
using API.Entities;
using API.Interfaces;

namespace API.Repository;
public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly DataContext _context;

    public BaseRepository(DataContext context)
    {
        _context = context;
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}