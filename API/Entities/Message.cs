
namespace API.Entities;
public class Message : BaseEntity
{
    public int ChatParticipantId { get; set; }
    public string Content { get; set; } = null!;
    public DateTime DateTimeSent { get; set; } = DateTime.UtcNow;
    public virtual ChatParticipant ChatParticipant { get; set; } = null!;

}