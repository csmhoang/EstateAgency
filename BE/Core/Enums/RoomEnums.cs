using Newtonsoft.Json.Linq;
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
            Occupied = 1,
            [EnumMember(Value = "PostingForRent")]
            PostingForRent = 2
        }
        public enum Interior
        {
            [EnumMember(Value = "Empty")]
            Empty = 0,
            [EnumMember(Value = "Full")]
            Full = 1
        }
        public enum Category
        {
            [EnumMember(Value = "RentalRoom")]
            RentalRoom = 0,
            [EnumMember(Value = "MiniApartment")]
            MiniApartment = 1,
            [EnumMember(Value = "Apartment")]
            Apartment = 2,
        }
    }
}
