using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using API.Migrations;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class ChatRepository : BaseRepository<Chat>, IChatRepository
{
    public ChatRepository(DataContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Chat>> GetUserChats(int userId)
    {
        return await _context.Set<Chat>().Where(chat => chat.Participants.Any(p => p.AppUserId == userId)).ToListAsync();
    }
}