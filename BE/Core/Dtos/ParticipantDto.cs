using System.ComponentModel.DataAnnotations;

namespace Core;

public record ParticipantDto
{
    public string? Id { get; set; }
    [Required(ErrorMessage = UserConst.ErrorEmptyId)]
    public string UserId { get; set; } = null!;
    [Required(ErrorMessage = ConversationConst.ErrorEmptyId)]
    public string ConversationId { get; set; } = null!;

    public UserDto? User { get; set; }
    public ConversationDto? Conversation { get; set; }
}
