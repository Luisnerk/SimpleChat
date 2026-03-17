using API.Data;
using API.Entities;
using API.Interfaces;

namespace API.Repository;
public class ChatParticipantRepository : BaseRepository<ChatParticipant>, IChatParticipantRepository
{
    public ChatParticipantRepository(DataContext context) : base(context) {}
}