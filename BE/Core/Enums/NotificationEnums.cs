using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
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
}
