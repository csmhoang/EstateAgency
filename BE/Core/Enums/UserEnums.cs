using System.Runtime.Serialization;

namespace Core.Enums
{
    public record UserEnums
    {
        public enum Gender
        {
            [EnumMember(Value = "Male")]
            Male = 0,
            [EnumMember(Value = "Female")]
            Female = 1,
            [EnumMember(Value = "Other")]
            Other = 2
        }
    }
}
