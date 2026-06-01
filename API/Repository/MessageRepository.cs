using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class MessageRepository : BaseRepository<Message>, IMessageRepository
{
    public MessageRepository(DataContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Message>> GetMessagesAsync(int chatId) {
        return  await _context.Set<Message>()
                                    .Include(m => m.ChatParticipant)
                                    .ThenInclude(p => p.AppUser)
                                    .Where(m => m.ChatParticipant.ChatId == chatId)
                                    .ToListAsync();
    }
}