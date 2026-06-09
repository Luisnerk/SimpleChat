using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Repository;

namespace API.Interfaces;
public interface IChatParticipantRepository : IBaseRepository<ChatParticipant>
{
    public Task<ChatParticipant> GetChatParticipant(int chatId, int userId);
}