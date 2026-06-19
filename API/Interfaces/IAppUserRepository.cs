
using API.Entities;

namespace API.Interfaces;
public interface IAppUserRepository : IBaseRepository<AppUser>
{
    public Task<IEnumerable<AppUser>> SearchList(string nameParam);
    public Task<AppUser?> GetUser(string userName);
}