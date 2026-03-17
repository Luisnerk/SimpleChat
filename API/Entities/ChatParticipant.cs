
namespace API.Entities;
public class ChatParticipant : BaseEntity
{
    public int ChatId { get; set; }
    public int AppUserId { get; set; }

    public virtual Chat Chat { get; set; } = null!;
    public virtual AppUser AppUser { get; set; } = null!;
}