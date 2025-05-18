using System.Runtime.Serialization;

namespace Core;

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
    public enum StatusRoom
    {
        [EnumMember(Value = "Show")]
        Show = 0,
        [EnumMember(Value = "Hide")]
        Hide = 1
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
