using System.Reflection;

namespace Destiny.Extensions;

public static class EnumExtensions
{
    public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
    {
        return value.GetType().GetMember(value.ToString()).First()
            .GetCustomAttribute<TAttribute>();
    }
}