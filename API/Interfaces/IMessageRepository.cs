using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Repository;

namespace API.Interfaces;
public interface IMessageRepository : IBaseRepository<Message>
{
    public Task<IEnumerable<Message>> GetMessagesAsync(int chatId);
}