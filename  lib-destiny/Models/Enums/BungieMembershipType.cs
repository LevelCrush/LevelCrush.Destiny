using System.Runtime.Serialization;

namespace Destiny.Models.Enums;

[DataContract]
public enum BungieMembershipType
{
    [EnumMember]
    None = 0,

    [EnumMember]
    Xbox = 1,

    [EnumMember]
    Psn = 2,

    [EnumMember]
    Steam = 3,

    [EnumMember]
    Blizzard = 4,

    [EnumMember]
    Stadia = 5,

    [EnumMember]
    Egs = 6,

    [EnumMember]
    Demon = 10,

    [EnumMember]
    BungieNext = 254,

    [EnumMember]
    All = -1
}