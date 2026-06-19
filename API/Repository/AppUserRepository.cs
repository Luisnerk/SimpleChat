using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;
public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
{
    public AppUserRepository(DataContext context) : base(context) {}

    public async Task<IEnumerable<AppUser>> SearchList(string nameParam)
    {
        return await _context.Set<AppUser>().Where(usr => usr.Name.ToLower().Contains(nameParam)).Take(5).ToListAsync();
    }

    public async Task<AppUser?> GetUser(string userName)
    {
        var user =  await _context.Set<AppUser>().FirstOrDefaultAsync(usr => usr.Username == userName);
        return user;
    }
    
}