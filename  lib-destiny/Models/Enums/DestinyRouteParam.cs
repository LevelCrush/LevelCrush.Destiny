using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Destiny.Models.Enums;

[DataContract]
public enum DestinyRouteParam
{
    [Display(Name = "")]
    [EnumMember]
    None,

    [Display(Name = "{id}")]
    [EnumMember]
    BungieNetMembershipID,

    [Display(Name = "{membershipId}")]
    [EnumMember]
    PlatformMembershipID,

    [Display(Name = "{membershipType}")]
    [EnumMember]
    PlatformMembershipType,

    [Display(Name = "{mType}")]
    [EnumMember]
    MType,

    [Display(Name = "{crType}")]
    [EnumMember]
    CredentialType,

    [Display(Name = "{credential}")]
    [EnumMember]
    Credential,

    [Display(Name = "{page}")]
    [EnumMember]
    Page,

    [Display(Name = "{type}")]
    [EnumMember]
    ContentType,

    [Display(Name = "{locale}")]
    [EnumMember]
    Locale,

    [Display(Name = "{searchtext}")]
    [EnumMember]
    SearchText,

    [Display(Name = "{tag}")]
    [EnumMember]
    Tag,

    [Display(Name = "{size}")]
    [EnumMember]
    Size,

    [Display(Name = "{pageToken}")]
    [EnumMember]
    PageToken,

    [Display(Name = "{pageSize}")]
    [EnumMember]
    PageSize,

    [Display(Name = "{group}")]
    [EnumMember]
    Group,

    [Display(Name = "{sort}")]
    [EnumMember]
    Sort,

    [Display(Name = "{quickDate}")]
    [EnumMember]
    QuickDate,

    [Display(Name = "{categoryFilter}")]
    [EnumMember]
    CategoryFilter,

    [Display(Name = "{replySize}")]
    [EnumMember]
    ReplySize,

    [Display(Name = "{getParentPost}")]
    [EnumMember]
    GetParentPost,

    [Display(Name = "{rootThreadMode}")]
    [EnumMember]
    RootThreadMode,

    [Display(Name = "{sortMode}")]
    [EnumMember]
    SortMode,

    [Display(Name = "{childPostId}")]
    [EnumMember]
    ChildPostID,

    [Display(Name = "{contentID}")]
    [EnumMember]
    ContentID,

    [Display(Name = "{topicId}")]
    [EnumMember]
    TopicID,

    [Display(Name = "{createDateRange}")]
    [EnumMember]
    CreateDateRange,

    [Display(Name = "{conversationId}")]
    [EnumMember]
    ConversationId,

    [Display(Name = "{memberType}")]
    [EnumMember]
    MemberType,

    [Display(Name = "{founderIdNew}")]
    [EnumMember]
    FounderIDNew,

    [Display(Name = "{filter}")]
    [EnumMember]
    Filter,

    [Display(Name = "{groupType}")]
    [EnumMember]
    GroupType,

    [Display(Name = "{groupId}")]
    [EnumMember]
    GroupID,

    [Display(Name = "{characterId}")]
    [EnumMember]
    Character,

    [Display(Name = "{activityId}")]
    [EnumMember]
    Activity
}