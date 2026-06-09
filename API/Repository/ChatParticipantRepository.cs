using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Repository;
public class ChatParticipantRepository : BaseRepository<ChatParticipant>, IChatParticipantRepository
{
    public ChatParticipantRepository(DataContext context) : base(context) {}

    public async Task<ChatParticipant> GetChatParticipant(int chatId, int userId)
    {
        var participant = await  _context.Set<ChatParticipant>().FirstOrDefaultAsync(cp => cp.AppUserId == userId && cp.ChatId == chatId);
        
        return participant;
    }
}