using System.Runtime.Serialization;

namespace Destiny.Models.Schemas;

/// Contains component data as well as profile privacy and if it is disabled (if available)
/// 
/// **Source**: [Bungie Documentation](https://bungie-net.github.io/#/components/schemas/SingleComponentResponseOfDestinyProfileComponent)
[DataContract]
public class DestinyComponent<T> where T : class
{
    [DataMember(Name = "disabled", IsRequired = false)]
    public bool? Disabled { get; set; }

    [DataMember(Name = "data", IsRequired = false)]
    public T? Data { get; set; }

    [DataMember(Name = "privacy", IsRequired = false)]
    public int Privacy { get; set; }
}