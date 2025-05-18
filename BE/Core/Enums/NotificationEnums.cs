using System.Runtime.Serialization;

namespace Core;

public class NotificationEnums
{
    public enum StatusNotification
    {
        [EnumMember(Value = "Unread")]
        Unread = 0,
        [EnumMember(Value = "Readed")]
        Readed = 1,
    }
}
