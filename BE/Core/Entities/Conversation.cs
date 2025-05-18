namespace Core.Entities;

public partial class Conversation: BaseEntity
{
    public Conversation()
    {
        Participants = new HashSet<Participant>();
        Messages = new HashSet<Message>();
    }
    public virtual ICollection<Participant> Participants { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
}
