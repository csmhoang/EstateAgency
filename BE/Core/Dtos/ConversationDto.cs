namespace Core;

public record ConversationDto
{
    public string? Id { get; set; }

    public ICollection<ParticipantDto>? Participants { get; set; }
    public ICollection<MessageDto>? Messages { get; set; }
}
