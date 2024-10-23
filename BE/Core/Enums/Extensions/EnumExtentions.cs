using System.Reflection;
using System.Runtime.Serialization;

namespace Core.Enums.Extensions
{
    public static class EnumExtentions
    {
        public static string Value(this Enum member)
        {
            FieldInfo? field = member.GetType().GetField(member.ToString());

            if (field == null)
            {
                return member.ToString();
            }
            EnumMemberAttribute? attribute = field.GetCustomAttribute<EnumMemberAttribute>();

            return attribute?.Value ?? member.ToString();
        }
    }
}
