using System.ComponentModel.DataAnnotations;

namespace Destiny.Models.Enums;

public enum DestinyRouteParam
{
    [Display(Name = "")]
    None,

    [Display(Name = "{id}")]
    BungieNetMembershipID,

    [Display(Name = "{membershipId}")]
    PlatformMembershipID,

    [Display(Name = "{membershipType}")]
    PlatformMembershipType,

    [Display(Name = "{mType}")]
    MType,

    [Display(Name = "{crType}")]
    CredentialType,

    [Display(Name = "{credential}")]
    Credential,

    [Display(Name = "{page}")]
    Page,

    [Display(Name = "{type}")]
    ContentType,

    [Display(Name = "{locale}")]
    Locale,

    [Display(Name = "{searchtext}")]
    SearchText,

    [Display(Name = "{tag}")]
    Tag,

    [Display(Name = "{size}")]
    Size,

    [Display(Name = "{pageToken}")]
    PageToken,

    [Display(Name = "{pageSize}")]
    PageSize,

    [Display(Name = "{group}")]
    Group,

    [Display(Name = "{sort}")]
    Sort,

    [Display(Name = "{quickDate}")]
    QuickDate,

    [Display(Name = "{categoryFilter}")]
    CategoryFilter,

    [Display(Name = "{replySize}")]
    ReplySize,

    [Display(Name = "{getParentPost}")]
    GetParentPost,

    [Display(Name = "{rootThreadMode}")]
    RootThreadMode,

    [Display(Name = "{sortMode}")]
    SortMode,

    [Display(Name = "{childPostId}")]
    ChildPostID,

    [Display(Name = "{contentID}")]
    ContentID,

    [Display(Name = "{topicId}")]
    TopicID,

    [Display(Name = "{createDateRange}")]
    CreateDateRange,

    [Display(Name = "{conversationId}")]
    ConversationId,

    [Display(Name = "{memberType}")]
    MemberType,

    [Display(Name = "{founderIdNew}")]
    FounderIDNew,

    [Display(Name = "{filter}")]
    Filter,

    [Display(Name = "{groupType}")]
    GroupType,

    [Display(Name = "{groupId}")]
    GroupID,

    [Display(Name = "{characterId}")]
    Character,

    [Display(Name = "{activityId}")]
    Activity
}