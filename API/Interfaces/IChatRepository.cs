using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace API.Interfaces;
public interface IChatRepository : IBaseRepository<Chat>
{
    public Task<IEnumerable<Chat>> GetUserChats(int userId);
}