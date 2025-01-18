using System.Text.Json;
using System.Text.Json.Serialization;

namespace Destiny.Extensions;

internal static class CloneExtensions
{
    public static T Clone<T>(this T obj) where T: class
    {
        var clone = JsonSerializer.Serialize(obj);
        return JsonSerializer.Deserialize<T>(clone);
    }
}