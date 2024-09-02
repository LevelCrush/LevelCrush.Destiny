using System.Runtime.Serialization;

namespace Destiny.Models.Enums;

[DataContract]
public enum DestinyActivityModeType
{
    [EnumMember]
    None = 0,

    [EnumMember]
    Story = 2,

    [EnumMember]
    Strike = 3,

    [EnumMember]
    Raid = 4,

    [EnumMember]
    AllPvP = 5,

    [EnumMember]
    Patrol = 6,

    [EnumMember]
    AllPvE = 7,

    [EnumMember]
    Reserved9 = 9,

    [EnumMember]
    Control = 10,

    [EnumMember]
    Reserved11 = 11,

    [EnumMember]
    Clash = 12,

    [EnumMember]
    Reserved13 = 13,

    [EnumMember]
    CrimsonDoubles = 15,

    [EnumMember]
    Nightfall = 16,

    [EnumMember]
    HeroicNightfall = 17,

    [EnumMember]
    AllStrikes = 18,

    [EnumMember]
    IronBanner = 19,

    [EnumMember]
    Reserved20 = 20,

    [EnumMember]
    Reserved21 = 21,

    [EnumMember]
    Reserved22 = 22,

    [EnumMember]
    Reserved24 = 24,

    [EnumMember]
    AllMayhem = 25,

    [EnumMember]
    Reserved26 = 26,

    [EnumMember]
    Reserved27 = 27,

    [EnumMember]
    Reserved28 = 28,

    [EnumMember]
    Reserved29 = 29,

    [EnumMember]
    Reserved30 = 30,

    [EnumMember]
    Supremacy = 31,

    [EnumMember]
    PrivateMatchesAll = 32,

    [EnumMember]
    Survival = 37,

    [EnumMember]
    Countdown = 38,

    [EnumMember]
    TrialsOfTheNine = 39,

    [EnumMember]
    Social = 40,

    [EnumMember]
    TrialsCountdown = 41,

    [EnumMember]
    TrialsSurvival = 42,

    [EnumMember]
    IronBannerControl = 43,

    [EnumMember]
    IronBannerClash = 44,

    [EnumMember]
    IronBannerSupremacy = 45,

    [EnumMember]
    ScoredNightfall = 46,

    [EnumMember]
    ScoredHeroicNightfall = 47,

    [EnumMember]
    Rumble = 48,

    [EnumMember]
    AllDoubles = 49,

    [EnumMember]
    Doubles = 50,

    [EnumMember]
    PrivateMatchesClash = 51,

    [EnumMember]
    PrivateMatchesControl = 52,

    [EnumMember]
    PrivateMatchesSupremacy = 53,

    [EnumMember]
    PrivateMatchesCountdown = 54,

    [EnumMember]
    PrivateMatchesSurvival = 55,

    [EnumMember]
    PrivateMatchesMayhem = 56,

    [EnumMember]
    PrivateMatchesRumble = 57,

    [EnumMember]
    HeroicAdventure = 58,

    [EnumMember]
    Showdown = 59,

    [EnumMember]
    Lockdown = 60,

    [EnumMember]
    Scorched = 61,

    [EnumMember]
    ScorchedTeam = 62,

    [EnumMember]
    Gambit = 63,

    [EnumMember]
    AllPvECompetitive = 64,

    [EnumMember]
    Breakthrough = 65,

    [EnumMember]
    BlackArmoryRun = 66,

    [EnumMember]
    Salvage = 67,

    [EnumMember]
    IronBannerSalvage = 68,

    [EnumMember]
    PvPCompetitive = 69,

    [EnumMember]
    PvPQuickplay = 70,

    [EnumMember]
    ClashQuickplay = 71,

    [EnumMember]
    ClashCompetitive = 72,

    [EnumMember]
    ControlQuickplay = 73,

    [EnumMember]
    ControlCompetitive = 74,

    [EnumMember]
    GambitPrime = 75,

    [EnumMember]
    Reckoning = 76,

    [EnumMember]
    Menagerie = 77,

    [EnumMember]
    VexOffensive = 78,

    [EnumMember]
    NightmareHunt = 79,

    [EnumMember]
    Elimination = 80,

    [EnumMember]
    Momentum = 81,

    [EnumMember]
    Dungeon = 82,

    [EnumMember]
    Sundial = 83,

    [EnumMember]
    TrialsOfOsiris = 84,

    [EnumMember]
    Dares = 85,

    [EnumMember]
    Offensive = 86,

    [EnumMember]
    LostSector = 87,

    [EnumMember]
    Rift = 88,

    [EnumMember]
    ZoneControl = 89,

    [EnumMember]
    IronBannerRift = 90,

    [EnumMember]
    IronBannerZoneControl = 91,

    [EnumMember]
    Relic = 92,

    [EnumMember]
    Unknown = int.MaxValue
}