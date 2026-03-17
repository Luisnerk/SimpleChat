namespace API.Entities;
public class Chat : BaseEntity
{
    public string Name { get; set; }

    public virtual List<ChatParticipant> Participants { get; set; } = new List<ChatParticipant>();
}