using Destiny.Models.Enums;
using Destiny.Models.Schemas;

namespace Destiny.Helpers;

public static class RaidReport
{

    /// <summary>
    /// Generates a Raid Report URL from a membership id and passed membership type
    /// </summary>
    /// <param name="membershipId"></param>
    /// <param name="membershipType"></param>
    /// <returns>A URL String that would corrospond to the individuals raid report</returns>
    public static string GenerateUrl(string membershipId, BungieMembershipType membershipType)
    {
        string membershipTypeName = membershipType switch
        {
            BungieMembershipType.None => "none", // 0 is verified to be none
            BungieMembershipType.Xbox => "xb", // 1 is verified to be xbox
            BungieMembershipType.Psn => "ps", // 2 is verified to be playstation
            _ => "pc", // other numbers either result in epic game store, steam, battle.net, or unknown numbers. Values like -1 are not possible and value 254 is reserved and not used
        };

        return $"https://raid.report/{membershipTypeName}/{membershipId}";
    }
    
 
    
    /// <summary>
    /// Generates a Raid Report URL based off the user information supplied
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public static string GenerateUrl(UserInfoCard userInfo)
    {
        return GenerateUrl(userInfo.MembershipId, userInfo.MembershipType);
    }
    
    /// <summary>
    /// Generates a Raid Report from a users profile component
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    public static string GenerateUrl(DestinyProfileComponent profile)
    {   
        return GenerateUrl(profile.UserInfo);
    }
}