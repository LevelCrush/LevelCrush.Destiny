using System.Runtime.Serialization;

namespace Destiny.Models.Requests;

[DataContract]
public class UserExactSearchRequest
{
    [DataMember(Name = "displayName")]
    public string DisplayName { get; set; }

    [DataMember(Name = "displayNameCode")]
    public short Code { get; set; }
}