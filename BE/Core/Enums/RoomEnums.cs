using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public record RoomEnums
    {
        public enum ConditionRoom
        {
            [EnumMember(Value = "Available")]
            Available = 0,
            [EnumMember(Value = "Occupied")]
            Occupied = 1
        }
        public enum Interior
        {
            [EnumMember(Value = "Empty")]
            Empty = 0,
            [EnumMember(Value = "Full")]
            Full = 1
        }
    }
}
