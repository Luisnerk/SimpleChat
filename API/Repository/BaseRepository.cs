
using System.Collections;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<T>> GetList(int size, int pageNumber)
    {
        IEnumerable<T> items = await _context.Set<T>().Skip((pageNumber-1) * size).Take(size).ToListAsync();
        return items;
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}