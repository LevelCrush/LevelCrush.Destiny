using System.Runtime.Serialization;

namespace Destiny.Models.Enums;

[DataContract]
public enum PlatformErrorCode
{
    [EnumMember]
    None = 0,

    [EnumMember]
    Success = 1,

    [EnumMember]
    TransportException = 2,

    [EnumMember]
    UnhandledException = 3,

    [EnumMember]
    NotImplemented = 4,

    [EnumMember]
    SystemDisabled = 5,

    [EnumMember]
    FailedToLoadAvailableLocalesConfiguration = 6,

    [EnumMember]
    ParameterParseFailure = 7,

    [EnumMember]
    ParameterInvalidRange = 8,

    [EnumMember]
    BadRequest = 9,

    [EnumMember]
    AuthenticationInvalid = 10,

    [EnumMember]
    DataNotFound = 11,

    [EnumMember]
    InsufficientPrivileges = 12,

    [EnumMember]
    Duplicate = 13,

    [EnumMember]
    UnknownSqlResult = 14,

    [EnumMember]
    ValidationError = 15,

    [EnumMember]
    ValidationMissingFieldError = 16,

    [EnumMember]
    ValidationInvalidInputError = 17,

    [EnumMember]
    InvalidParameters = 18,

    [EnumMember]
    ParameterNotFound = 19,

    [EnumMember]
    UnhandledHttpException = 20,

    [EnumMember]
    NotFound = 21,

    [EnumMember]
    WebAuthModuleAsyncFailed = 22,

    [EnumMember]
    InvalidReturnValue = 23,

    [EnumMember]
    UserBanned = 24,

    [EnumMember]
    InvalidPostBody = 25,

    [EnumMember]
    MissingPostBody = 26,

    [EnumMember]
    ExternalServiceTimeout = 27,

    [EnumMember]
    ValidationLengthError = 28,

    [EnumMember]
    ValidationRangeError = 29,

    [EnumMember]
    JsonDeserializationError = 30,

    [EnumMember]
    ThrottleLimitExceeded = 31,

    [EnumMember]
    ValidationTagError = 32,

    [EnumMember]
    ValidationProfanityError = 33,

    [EnumMember]
    ValidationUrlFormatError = 34,

    [EnumMember]
    ThrottleLimitExceededMinutes = 35,

    [EnumMember]
    ThrottleLimitExceededMomentarily = 36,

    [EnumMember]
    ThrottleLimitExceededSeconds = 37,

    [EnumMember]
    ExternalServiceUnknown = 38,

    [EnumMember]
    ValidationWordLengthError = 39,

    [EnumMember]
    ValidationInvisibleUnicode = 40,

    [EnumMember]
    ValidationBadNames = 41,

    [EnumMember]
    ExternalServiceFailed = 42,

    [EnumMember]
    ServiceRetired = 43,

    [EnumMember]
    UnknownSqlException = 44,

    [EnumMember]
    UnsupportedLocale = 45,

    [EnumMember]
    InvalidPageNumber = 46,

    [EnumMember]
    MaximumPageSizeExceeded = 47,

    [EnumMember]
    ServiceUnsupported = 48,

    [EnumMember]
    ValidationMaximumUnicodeCombiningCharacters = 49,

    [EnumMember]
    ValidationMaximumSequentialCarriageReturns = 50,

    [EnumMember]
    PerEndpointRequestThrottleExceeded = 51,

    [EnumMember]
    AuthContextCacheAssertion = 52,

    [EnumMember]
    ExPlatformStringValidationError = 53,

    [EnumMember]
    PerApplicationThrottleExceeded = 54,

    [EnumMember]
    PerApplicationAnonymousThrottleExceeded = 55,

    [EnumMember]
    PerApplicationAuthenticatedThrottleExceeded = 56,

    [EnumMember]
    PerUserThrottleExceeded = 57,

    [EnumMember]
    PayloadSignatureVerificationFailure = 58,

    [EnumMember]
    InvalidServiceAuthContext = 59,

    [EnumMember]
    ObsoleteCredentialType = 89,

    [EnumMember]
    UnableToUnPairMobileApp = 90,

    [EnumMember]
    UnableToPairMobileApp = 91,

    [EnumMember]
    CannotUseMobileAuthWithNonMobileProvider = 92,

    [EnumMember]
    MissingDeviceCookie = 93,

    [EnumMember]
    FacebookTokenExpired = 94,

    [EnumMember]
    AuthTicketRequired = 95,

    [EnumMember]
    CookieContextRequired = 96,

    [EnumMember]
    UnknownAuthenticationError = 97,

    [EnumMember]
    BungieNetAccountCreationRequired = 98,

    [EnumMember]
    WebAuthRequired = 99,

    [EnumMember]
    ContentUnknownSqlResult = 100,

    [EnumMember]
    ContentNeedUniquePath = 101,

    [EnumMember]
    ContentSqlException = 102,

    [EnumMember]
    ContentNotFound = 103,

    [EnumMember]
    ContentSuccessWithTagAddFail = 104,

    [EnumMember]
    ContentSearchMissingParameters = 105,

    [EnumMember]
    ContentInvalidId = 106,

    [EnumMember]
    ContentPhysicalFileDeletionError = 107,

    [EnumMember]
    ContentPhysicalFileCreationError = 108,

    [EnumMember]
    ContentPerforceSubmissionError = 109,

    [EnumMember]
    ContentPerforceInitializationError = 110,

    [EnumMember]
    ContentDeploymentPackageNotReadyError = 111,

    [EnumMember]
    ContentUploadFailed = 112,

    [EnumMember]
    ContentTooManyResults = 113,

    [EnumMember]
    ContentInvalidState = 115,

    [EnumMember]
    ContentNavigationParentNotFound = 116,

    [EnumMember]
    ContentNavigationParentUpdateError = 117,

    [EnumMember]
    DeploymentPackageNotEditable = 118,

    [EnumMember]
    ContentValidationError = 119,

    [EnumMember]
    ContentPropertiesValidationError = 120,

    [EnumMember]
    ContentTypeNotFound = 121,

    [EnumMember]
    DeploymentPackageNotFound = 122,

    [EnumMember]
    ContentSearchInvalidParameters = 123,

    [EnumMember]
    ContentItemPropertyAggregationError = 124,

    [EnumMember]
    DeploymentPackageFileNotFound = 125,

    [EnumMember]
    ContentPerforceFileHistoryNotFound = 126,

    [EnumMember]
    ContentAssetZipCreationFailure = 127,

    [EnumMember]
    ContentAssetZipCreationBusy = 128,

    [EnumMember]
    ContentProjectNotFound = 129,

    [EnumMember]
    ContentFolderNotFound = 130,

    [EnumMember]
    ContentPackagesInconsistent = 131,

    [EnumMember]
    ContentPackagesInvalidState = 132,

    [EnumMember]
    ContentPackagesInconsistentType = 133,

    [EnumMember]
    ContentCannotDeletePackage = 134,

    [EnumMember]
    ContentLockedForChanges = 135,

    [EnumMember]
    ContentFileUploadFailed = 136,

    [EnumMember]
    ContentNotReviewed = 137,

    [EnumMember]
    ContentPermissionDenied = 138,

    [EnumMember]
    ContentInvalidExternalUrl = 139,

    [EnumMember]
    ContentExternalFileCannotBeImportedLocally = 140,

    [EnumMember]
    ContentTagSaveFailure = 141,

    [EnumMember]
    ContentPerforceUnmatchedFileError = 142,

    [EnumMember]
    ContentPerforceChangelistResultNotFound = 143,

    [EnumMember]
    ContentPerforceChangelistFileItemsNotFound = 144,

    [EnumMember]
    ContentPerforceInvalidRevisionError = 145,

    [EnumMember]
    ContentUnloadedSaveResult = 146,

    [EnumMember]
    ContentPropertyInvalidNumber = 147,

    [EnumMember]
    ContentPropertyInvalidUrl = 148,

    [EnumMember]
    ContentPropertyInvalidDate = 149,

    [EnumMember]
    ContentPropertyInvalidSet = 150,

    [EnumMember]
    ContentPropertyCannotDeserialize = 151,

    [EnumMember]
    ContentRegexValidationFailOnProperty = 152,

    [EnumMember]
    ContentMaxLengthFailOnProperty = 153,

    [EnumMember]
    ContentPropertyUnexpectedDeserializationError = 154,

    [EnumMember]
    ContentPropertyRequired = 155,

    [EnumMember]
    ContentCannotCreateFile = 156,

    [EnumMember]
    ContentInvalidMigrationFile = 157,

    [EnumMember]
    ContentMigrationAlteringProcessedItem = 158,

    [EnumMember]
    ContentPropertyDefinitionNotFound = 159,

    [EnumMember]
    ContentReviewDataChanged = 160,

    [EnumMember]
    ContentRollbackRevisionNotInPackage = 161,

    [EnumMember]
    ContentItemNotBasedOnLatestRevision = 162,

    [EnumMember]
    ContentUnauthorized = 163,

    [EnumMember]
    ContentCannotCreateDeploymentPackage = 164,

    [EnumMember]
    ContentUserNotFound = 165,

    [EnumMember]
    ContentLocalePermissionDenied = 166,

    [EnumMember]
    ContentInvalidLinkToInternalEnvironment = 167,

    [EnumMember]
    ContentInvalidBlacklistedContent = 168,

    [EnumMember]
    ContentMacroMalformedNoContentId = 169,

    [EnumMember]
    ContentMacroMalformedNoTemplateType = 170,

    [EnumMember]
    ContentIllegalBNetMembershipId = 171,

    [EnumMember]
    ContentLocaleDidNotMatchExpected = 172,

    [EnumMember]
    ContentBabelCallFailed = 173,

    [EnumMember]
    ContentEnglishPostLiveForbidden = 174,

    [EnumMember]
    ContentLocaleEditPermissionDenied = 175,

    [EnumMember]
    ContentStackUnknownError = 176,

    [EnumMember]
    ContentStackNotFound = 177,

    [EnumMember]
    ContentStackRateLimited = 178,

    [EnumMember]
    ContentStackTimeout = 179,

    [EnumMember]
    ContentStackServiceError = 180,

    [EnumMember]
    ContentStackDeserializationFailure = 181,

    [EnumMember]
    UserNonUniqueName = 200,

    [EnumMember]
    UserManualLinkingStepRequired = 201,

    [EnumMember]
    UserCreateUnknownSqlResult = 202,

    [EnumMember]
    UserCreateUnknownSqlException = 203,

    [EnumMember]
    UserMalformedMembershipId = 204,

    [EnumMember]
    UserCannotFindRequestedUser = 205,

    [EnumMember]
    UserCannotLoadAccountCredentialLinkInfo = 206,

    [EnumMember]
    UserInvalidMobileAppType = 207,

    [EnumMember]
    UserMissingMobilePairingInfo = 208,

    [EnumMember]
    UserCannotGenerateMobileKeyWhileUsingMobileCredential = 209,

    [EnumMember]
    UserGenerateMobileKeyExistingSlotCollision = 210,

    [EnumMember]
    UserDisplayNameMissingOrInvalid = 211,

    [EnumMember]
    UserCannotLoadAccountProfileData = 212,

    [EnumMember]
    UserCannotSaveUserProfileData = 213,

    [EnumMember]
    UserEmailMissingOrInvalid = 214,

    [EnumMember]
    UserTermsOfUseRequired = 215,

    [EnumMember]
    UserCannotCreateNewAccountWhileLoggedIn = 216,

    [EnumMember]
    UserCannotResolveCentralAccount = 217,

    [EnumMember]
    UserInvalidAvatar = 218,

    [EnumMember]
    UserMissingCreatedUserResult = 219,

    [EnumMember]
    UserCannotChangeUniqueNameYet = 220,

    [EnumMember]
    UserCannotChangeDisplayNameYet = 221,

    [EnumMember]
    UserCannotChangeEmail = 222,

    [EnumMember]
    UserUniqueNameMustStartWithLetter = 223,

    [EnumMember]
    UserNoLinkedAccountsSupportFriendListings = 224,

    [EnumMember]
    UserAcknowledgmentTableFull = 225,

    [EnumMember]
    UserCreationDestinyMembershipRequired = 226,

    [EnumMember]
    UserFriendsTokenNeedsRefresh = 227,

    [EnumMember]
    UserEmailValidationUnknown = 228,

    [EnumMember]
    UserEmailValidationLimit = 229,

    [EnumMember]
    TransactionEmailSendFailure = 230,

    [EnumMember]
    MailHookPermissionFailure = 231,

    [EnumMember]
    MailServiceRateLimit = 232,

    [EnumMember]
    UserEmailMustBeVerified = 233,

    [EnumMember]
    UserMustAllowCustomerServiceEmails = 234,

    [EnumMember]
    NonTransactionalEmailSendFailure = 235,

    [EnumMember]
    UnknownErrorSettingGlobalDisplayName = 236,

    [EnumMember]
    DuplicateGlobalDisplayName = 237,

    [EnumMember]
    ErrorRunningNameValidationChecks = 238,

    [EnumMember]
    ErrorDatabaseGlobalName = 239,

    [EnumMember]
    ErrorNoAvailableNameChanges = 240,

    [EnumMember]
    ErrorNameAlreadySetToInput = 241,

    [EnumMember]
    UserDisplayNameLessThanMinLength = 242,

    [EnumMember]
    UserDisplayNameGreaterThanMaxLength = 243,

    [EnumMember]
    UserDisplayNameContainsUnacceptableOrInvalidContent = 244,

    [EnumMember]
    MessagingUnknownError = 300,

    [EnumMember]
    MessagingSelfError = 301,

    [EnumMember]
    MessagingSendThrottle = 302,

    [EnumMember]
    MessagingNoBody = 303,

    [EnumMember]
    MessagingTooManyUsers = 304,

    [EnumMember]
    MessagingCanNotLeaveConversation = 305,

    [EnumMember]
    MessagingUnableToSend = 306,

    [EnumMember]
    MessagingDeletedUserForbidden = 307,

    [EnumMember]
    MessagingCannotDeleteExternalConversation = 308,

    [EnumMember]
    MessagingGroupChatDisabled = 309,

    [EnumMember]
    MessagingMustIncludeSelfInPrivateMessage = 310,

    [EnumMember]
    MessagingSenderIsBanned = 311,

    [EnumMember]
    MessagingGroupOptionalChatExceededMaximum = 312,

    [EnumMember]
    PrivateMessagingRequiresDestinyMembership = 313,

    [EnumMember]
    AddSurveyAnswersUnknownSqlException = 400,

    [EnumMember]
    ForumBodyCannotBeEmpty = 500,

    [EnumMember]
    ForumSubjectCannotBeEmptyOnTopicPost = 501,

    [EnumMember]
    ForumCannotLocateParentPost = 502,

    [EnumMember]
    ForumThreadLockedForReplies = 503,

    [EnumMember]
    ForumUnknownSqlResultDuringCreatePost = 504,

    [EnumMember]
    ForumUnknownTagCreationError = 505,

    [EnumMember]
    ForumUnknownSqlResultDuringTagItem = 506,

    [EnumMember]
    ForumUnknownExceptionCreatePost = 507,

    [EnumMember]
    ForumQuestionMustBeTopicPost = 508,

    [EnumMember]
    ForumExceptionDuringTagSearch = 509,

    [EnumMember]
    ForumExceptionDuringTopicRetrieval = 510,

    [EnumMember]
    ForumAliasedTagError = 511,

    [EnumMember]
    ForumCannotLocateThread = 512,

    [EnumMember]
    ForumUnknownExceptionEditPost = 513,

    [EnumMember]
    ForumCannotLocatePost = 514,

    [EnumMember]
    ForumUnknownExceptionGetOrCreateTags = 515,

    [EnumMember]
    ForumEditPermissionDenied = 516,

    [EnumMember]
    ForumUnknownSqlResultDuringTagIdRetrieval = 517,

    [EnumMember]
    ForumCannotGetRating = 518,

    [EnumMember]
    ForumUnknownExceptionGetRating = 519,

    [EnumMember]
    ForumRatingsAccessError = 520,

    [EnumMember]
    ForumRelatedPostAccessError = 521,

    [EnumMember]
    ForumLatestReplyAccessError = 522,

    [EnumMember]
    ForumUserStatusAccessError = 523,

    [EnumMember]
    ForumAuthorAccessError = 524,

    [EnumMember]
    ForumGroupAccessError = 525,

    [EnumMember]
    ForumUrlExpectedButMissing = 526,

    [EnumMember]
    ForumRepliesCannotBeEmpty = 527,

    [EnumMember]
    ForumRepliesCannotBeInDifferentGroups = 528,

    [EnumMember]
    ForumSubTopicCannotBeCreatedAtThisThreadLevel = 529,

    [EnumMember]
    ForumCannotCreateContentTopic = 530,

    [EnumMember]
    ForumTopicDoesNotExist = 531,

    [EnumMember]
    ForumContentCommentsNotAllowed = 532,

    [EnumMember]
    ForumUnknownSqlResultDuringEditPost = 533,

    [EnumMember]
    ForumUnknownSqlResultDuringGetPost = 534,

    [EnumMember]
    ForumPostValidationBadUrl = 535,

    [EnumMember]
    ForumBodyTooLong = 536,

    [EnumMember]
    ForumSubjectTooLong = 537,

    [EnumMember]
    ForumAnnouncementNotAllowed = 538,

    [EnumMember]
    ForumCannotShareOwnPost = 539,

    [EnumMember]
    ForumEditNoOp = 540,

    [EnumMember]
    ForumUnknownDatabaseErrorDuringGetPost = 541,

    [EnumMember]
    ForumExceeedMaximumRowLimit = 542,

    [EnumMember]
    ForumCannotSharePrivatePost = 543,

    [EnumMember]
    ForumCannotCrossPostBetweenGroups = 544,

    [EnumMember]
    ForumIncompatibleCategories = 555,

    [EnumMember]
    ForumCannotUseTheseCategoriesOnNonTopicPost = 556,

    [EnumMember]
    ForumCanOnlyDeleteTopics = 557,

    [EnumMember]
    ForumDeleteSqlException = 558,

    [EnumMember]
    ForumDeleteSqlUnknownResult = 559,

    [EnumMember]
    ForumTooManyTags = 560,

    [EnumMember]
    ForumCanOnlyRateTopics = 561,

    [EnumMember]
    ForumBannedPostsCannotBeEdited = 562,

    [EnumMember]
    ForumThreadRootIsBanned = 563,

    [EnumMember]
    ForumCannotUseOfficialTagCategoryAsTag = 564,

    [EnumMember]
    ForumAnswerCannotBeMadeOnCreatePost = 565,

    [EnumMember]
    ForumAnswerCannotBeMadeOnEditPost = 566,

    [EnumMember]
    ForumAnswerPostIdIsNotADirectReplyOfQuestion = 567,

    [EnumMember]
    ForumAnswerTopicIdIsNotAQuestion = 568,

    [EnumMember]
    ForumUnknownExceptionDuringMarkAnswer = 569,

    [EnumMember]
    ForumUnknownSqlResultDuringMarkAnswer = 570,

    [EnumMember]
    ForumCannotRateYourOwnPosts = 571,

    [EnumMember]
    ForumPollsMustBeTheFirstPostInTopic = 572,

    [EnumMember]
    ForumInvalidPollInput = 573,

    [EnumMember]
    ForumGroupAdminEditNonMember = 574,

    [EnumMember]
    ForumCannotEditModeratorEditedPost = 575,

    [EnumMember]
    ForumRequiresDestinyMembership = 576,

    [EnumMember]
    ForumUnexpectedError = 577,

    [EnumMember]
    ForumAgeLock = 578,

    [EnumMember]
    ForumMaxPages = 579,

    [EnumMember]
    ForumMaxPagesOldestFirst = 580,

    [EnumMember]
    ForumCannotApplyForumIdWithoutTags = 581,

    [EnumMember]
    ForumCannotApplyForumIdToNonTopics = 582,

    [EnumMember]
    ForumCannotDownvoteCommunityCreations = 583,

    [EnumMember]
    ForumTopicsMustHaveOfficialCategory = 584,

    [EnumMember]
    ForumRecruitmentTopicMalformed = 585,

    [EnumMember]
    ForumRecruitmentTopicNotFound = 586,

    [EnumMember]
    ForumRecruitmentTopicNoSlotsRemaining = 587,

    [EnumMember]
    ForumRecruitmentTopicKickBan = 588,

    [EnumMember]
    ForumRecruitmentTopicRequirementsNotMet = 589,

    [EnumMember]
    ForumRecruitmentTopicNoPlayers = 590,

    [EnumMember]
    ForumRecruitmentApproveFailMessageBan = 591,

    [EnumMember]
    ForumRecruitmentGlobalBan = 592,

    [EnumMember]
    ForumUserBannedFromThisTopic = 593,

    [EnumMember]
    ForumRecruitmentFireteamMembersOnly = 594,

    [EnumMember]
    ForumRequiresDestiny2Progress = 595,

    [EnumMember]
    ForumRequiresDestiny2EntitlementPurchase = 596,

    [EnumMember]
    GroupMembershipApplicationAlreadyResolved = 601,

    [EnumMember]
    GroupMembershipAlreadyApplied = 602,

    [EnumMember]
    GroupMembershipInsufficientPrivileges = 603,

    [EnumMember]
    GroupIdNotReturnedFromCreation = 604,

    [EnumMember]
    GroupSearchInvalidParameters = 605,

    [EnumMember]
    GroupMembershipPendingApplicationNotFound = 606,

    [EnumMember]
    GroupInvalidId = 607,

    [EnumMember]
    GroupInvalidMembershipId = 608,

    [EnumMember]
    GroupInvalidMembershipType = 609,

    [EnumMember]
    GroupMissingTags = 610,

    [EnumMember]
    GroupMembershipNotFound = 611,

    [EnumMember]
    GroupInvalidRating = 612,

    [EnumMember]
    GroupUserFollowingAccessError = 613,

    [EnumMember]
    GroupUserMembershipAccessError = 614,

    [EnumMember]
    GroupCreatorAccessError = 615,

    [EnumMember]
    GroupAdminAccessError = 616,

    [EnumMember]
    GroupPrivatePostNotViewable = 617,

    [EnumMember]
    GroupMembershipNotLoggedIn = 618,

    [EnumMember]
    GroupNotDeleted = 619,

    [EnumMember]
    GroupUnknownErrorUndeletingGroup = 620,

    [EnumMember]
    GroupDeleted = 621,

    [EnumMember]
    GroupNotFound = 622,

    [EnumMember]
    GroupMemberBanned = 623,

    [EnumMember]
    GroupMembershipClosed = 624,

    [EnumMember]
    GroupPrivatePostOverrideError = 625,

    [EnumMember]
    GroupNameTaken = 626,

    [EnumMember]
    GroupDeletionGracePeriodExpired = 627,

    [EnumMember]
    GroupCannotCheckBanStatus = 628,

    [EnumMember]
    GroupMaximumMembershipCountReached = 629,

    [EnumMember]
    NoDestinyAccountForClanPlatform = 630,

    [EnumMember]
    AlreadyRequestingMembershipForClanPlatform = 631,

    [EnumMember]
    AlreadyClanMemberOnPlatform = 632,

    [EnumMember]
    GroupJoinedCannotSetClanName = 633,

    [EnumMember]
    GroupLeftCannotClearClanName = 634,

    [EnumMember]
    GroupRelationshipRequestPending = 635,

    [EnumMember]
    GroupRelationshipRequestBlocked = 636,

    [EnumMember]
    GroupRelationshipRequestNotFound = 637,

    [EnumMember]
    GroupRelationshipBlockNotFound = 638,

    [EnumMember]
    GroupRelationshipNotFound = 639,

    [EnumMember]
    GroupAlreadyAllied = 641,

    [EnumMember]
    GroupAlreadyMember = 642,

    [EnumMember]
    GroupRelationshipAlreadyExists = 643,

    [EnumMember]
    InvalidGroupTypesForRelationshipRequest = 644,

    [EnumMember]
    GroupAtMaximumAlliances = 646,

    [EnumMember]
    GroupCannotSetClanOnlySettings = 647,

    [EnumMember]
    ClanCannotSetTwoDefaultPostTypes = 648,

    [EnumMember]
    GroupMemberInvalidMemberType = 649,

    [EnumMember]
    GroupInvalidPlatformType = 650,

    [EnumMember]
    GroupMemberInvalidSort = 651,

    [EnumMember]
    GroupInvalidResolveState = 652,

    [EnumMember]
    ClanAlreadyEnabledForPlatform = 653,

    [EnumMember]
    ClanNotEnabledForPlatform = 654,

    [EnumMember]
    ClanEnabledButCouldNotJoinNoAccount = 655,

    [EnumMember]
    ClanEnabledButCouldNotJoinAlreadyMember = 656,

    [EnumMember]
    ClanCannotJoinNoCredential = 657,

    [EnumMember]
    NoClanMembershipForPlatform = 658,

    [EnumMember]
    GroupToGroupFollowLimitReached = 659,

    [EnumMember]
    ChildGroupAlreadyInAlliance = 660,

    [EnumMember]
    OwnerGroupAlreadyInAlliance = 661,

    [EnumMember]
    AllianceOwnerCannotJoinAlliance = 662,

    [EnumMember]
    GroupNotInAlliance = 663,

    [EnumMember]
    ChildGroupCannotInviteToAlliance = 664,

    [EnumMember]
    GroupToGroupAlreadyFollowed = 665,

    [EnumMember]
    GroupToGroupNotFollowing = 666,

    [EnumMember]
    ClanMaximumMembershipReached = 667,

    [EnumMember]
    ClanNameNotValid = 668,

    [EnumMember]
    ClanNameNotValidError = 669,

    [EnumMember]
    AllianceOwnerNotDefined = 670,

    [EnumMember]
    AllianceChildNotDefined = 671,

    [EnumMember]
    ClanCultureIllegalCharacters = 672,

    [EnumMember]
    ClanTagIllegalCharacters = 673,

    [EnumMember]
    ClanRequiresInvitation = 674,

    [EnumMember]
    ClanMembershipClosed = 675,

    [EnumMember]
    ClanInviteAlreadyMember = 676,

    [EnumMember]
    GroupInviteAlreadyMember = 677,

    [EnumMember]
    GroupJoinApprovalRequired = 678,

    [EnumMember]
    ClanTagRequired = 679,

    [EnumMember]
    GroupNameCannotStartOrEndWithWhiteSpace = 680,

    [EnumMember]
    ClanCallsignCannotStartOrEndWithWhiteSpace = 681,

    [EnumMember]
    ClanMigrationFailed = 682,

    [EnumMember]
    ClanNotEnabledAlreadyMemberOfAnotherClan = 683,

    [EnumMember]
    GroupModerationNotPermittedOnNonMembers = 684,

    [EnumMember]
    ClanCreationInWorldServerFailed = 685,

    [EnumMember]
    ClanNotFound = 686,

    [EnumMember]
    ClanMembershipLevelDoesNotPermitThatAction = 687,

    [EnumMember]
    ClanMemberNotFound = 688,

    [EnumMember]
    ClanMissingMembershipApprovers = 689,

    [EnumMember]
    ClanInWrongStateForRequestedAction = 690,

    [EnumMember]
    ClanNameAlreadyUsed = 691,

    [EnumMember]
    ClanTooFewMembers = 692,

    [EnumMember]
    ClanInfoCannotBeWhitespace = 693,

    [EnumMember]
    GroupCultureThrottle = 694,

    [EnumMember]
    ClanTargetDisallowsInvites = 695,

    [EnumMember]
    ClanInvalidOperation = 696,

    [EnumMember]
    ClanFounderCannotLeaveWithoutAbdication = 697,

    [EnumMember]
    ClanNameReserved = 698,

    [EnumMember]
    ClanApplicantInClanSoNowInvited = 699,

    [EnumMember]
    ActivitiesUnknownException = 701,

    [EnumMember]
    ActivitiesParameterNull = 702,

    [EnumMember]
    ActivityCountsDiabled = 703,

    [EnumMember]
    ActivitySearchInvalidParameters = 704,

    [EnumMember]
    ActivityPermissionDenied = 705,

    [EnumMember]
    ShareAlreadyShared = 706,

    [EnumMember]
    ActivityLoggingDisabled = 707,

    [EnumMember]
    ClanRequiresExistingDestinyAccount = 750,

    [EnumMember]
    ClanNameRestricted = 751,

    [EnumMember]
    ClanCreationBan = 752,

    [EnumMember]
    ClanCreationTenureRequirementsNotMet = 753,

    [EnumMember]
    ClanFieldContainsReservedTerms = 754,

    [EnumMember]
    ClanFieldContainsInappropriateContent = 755,

    [EnumMember]
    ItemAlreadyFollowed = 801,

    [EnumMember]
    ItemNotFollowed = 802,

    [EnumMember]
    CannotFollowSelf = 803,

    [EnumMember]
    GroupFollowLimitExceeded = 804,

    [EnumMember]
    TagFollowLimitExceeded = 805,

    [EnumMember]
    UserFollowLimitExceeded = 806,

    [EnumMember]
    FollowUnsupportedEntityType = 807,

    [EnumMember]
    NoValidTagsInList = 900,

    [EnumMember]
    BelowMinimumSuggestionLength = 901,

    [EnumMember]
    CannotGetSuggestionsOnMultipleTagsSimultaneously = 902,

    [EnumMember]
    NotAValidPartialTag = 903,

    [EnumMember]
    TagSuggestionsUnknownSqlResult = 904,

    [EnumMember]
    TagsUnableToLoadPopularTagsFromDatabase = 905,

    [EnumMember]
    TagInvalid = 906,

    [EnumMember]
    TagNotFound = 907,

    [EnumMember]
    SingleTagExpected = 908,

    [EnumMember]
    TagsExceededMaximumPerItem = 909,

    [EnumMember]
    IgnoreInvalidParameters = 1000,

    [EnumMember]
    IgnoreSqlException = 1001,

    [EnumMember]
    IgnoreErrorRetrievingGroupPermissions = 1002,

    [EnumMember]
    IgnoreErrorInsufficientPermission = 1003,

    [EnumMember]
    IgnoreErrorRetrievingItem = 1004,

    [EnumMember]
    IgnoreCannotIgnoreSelf = 1005,

    [EnumMember]
    IgnoreIllegalType = 1006,

    [EnumMember]
    IgnoreNotFound = 1007,

    [EnumMember]
    IgnoreUserGloballyIgnored = 1008,

    [EnumMember]
    IgnoreUserIgnored = 1009,

    [EnumMember]
    TargetUserIgnored = 1010,

    [EnumMember]
    NotificationSettingInvalid = 1100,

    [EnumMember]
    PsnApiExpiredAccessToken = 1204,

    [EnumMember]
    PSNExForbidden = 1205,

    [EnumMember]
    PSNExSystemDisabled = 1218,

    [EnumMember]
    PsnApiErrorCodeUnknown = 1223,

    [EnumMember]
    PsnApiErrorWebException = 1224,

    [EnumMember]
    PsnApiBadRequest = 1225,

    [EnumMember]
    PsnApiAccessTokenRequired = 1226,

    [EnumMember]
    PsnApiInvalidAccessToken = 1227,

    [EnumMember]
    PsnApiBannedUser = 1229,

    [EnumMember]
    PsnApiAccountUpgradeRequired = 1230,

    [EnumMember]
    PsnApiServiceTemporarilyUnavailable = 1231,

    [EnumMember]
    PsnApiServerBusy = 1232,

    [EnumMember]
    PsnApiUnderMaintenance = 1233,

    [EnumMember]
    PsnApiProfileUserNotFound = 1234,

    [EnumMember]
    PsnApiProfilePrivacyRestriction = 1235,

    [EnumMember]
    PsnApiProfileUnderMaintenance = 1236,

    [EnumMember]
    PsnApiAccountAttributeMissing = 1237,

    [EnumMember]
    PsnApiNoPermission = 1238,

    [EnumMember]
    PsnApiTargetUserBlocked = 1239,

    [EnumMember]
    PsnApiJwksMissing = 1240,

    [EnumMember]
    PsnApiJwtMalformedHeader = 1241,

    [EnumMember]
    PsnApiJwtMalformedPayload = 1242,

    [EnumMember]
    XblExSystemDisabled = 1300,

    [EnumMember]
    XblExUnknownError = 1301,

    [EnumMember]
    XblApiErrorWebException = 1302,

    [EnumMember]
    XblStsTokenInvalid = 1303,

    [EnumMember]
    XblStsMissingToken = 1304,

    [EnumMember]
    XblStsExpiredToken = 1305,

    [EnumMember]
    XblAccessToTheSandboxDenied = 1306,

    [EnumMember]
    XblMsaResponseMissing = 1307,

    [EnumMember]
    XblMsaAccessTokenExpired = 1308,

    [EnumMember]
    XblMsaInvalidRequest = 1309,

    [EnumMember]
    XblMsaFriendsRequireSignIn = 1310,

    [EnumMember]
    XblUserActionRequired = 1311,

    [EnumMember]
    XblParentalControls = 1312,

    [EnumMember]
    XblDeveloperAccount = 1313,

    [EnumMember]
    XblUserTokenExpired = 1314,

    [EnumMember]
    XblUserTokenInvalid = 1315,

    [EnumMember]
    XblOffline = 1316,

    [EnumMember]
    XblUnknownErrorCode = 1317,

    [EnumMember]
    XblMsaInvalidGrant = 1318,

    [EnumMember]
    ReportNotYetResolved = 1400,

    [EnumMember]
    ReportOverturnDoesNotChangeDecision = 1401,

    [EnumMember]
    ReportNotFound = 1402,

    [EnumMember]
    ReportAlreadyReported = 1403,

    [EnumMember]
    ReportInvalidResolution = 1404,

    [EnumMember]
    ReportNotAssignedToYou = 1405,

    [EnumMember]
    LegacyGameStatsSystemDisabled = 1500,

    [EnumMember]
    LegacyGameStatsUnknownError = 1501,

    [EnumMember]
    LegacyGameStatsMalformedSneakerNetCode = 1502,

    [EnumMember]
    DestinyAccountAcquisitionFailure = 1600,

    [EnumMember]
    DestinyAccountNotFound = 1601,

    [EnumMember]
    DestinyBuildStatsDatabaseError = 1602,

    [EnumMember]
    DestinyCharacterStatsDatabaseError = 1603,

    [EnumMember]
    DestinyPvPStatsDatabaseError = 1604,

    [EnumMember]
    DestinyPvEStatsDatabaseError = 1605,

    [EnumMember]
    DestinyGrimoireStatsDatabaseError = 1606,

    [EnumMember]
    DestinyStatsParameterMembershipTypeParseError = 1607,

    [EnumMember]
    DestinyStatsParameterMembershipIdParseError = 1608,

    [EnumMember]
    DestinyStatsParameterRangeParseError = 1609,

    [EnumMember]
    DestinyStringItemHashNotFound = 1610,

    [EnumMember]
    DestinyStringSetNotFound = 1611,

    [EnumMember]
    DestinyContentLookupNotFoundForKey = 1612,

    [EnumMember]
    DestinyContentItemNotFound = 1613,

    [EnumMember]
    DestinyContentSectionNotFound = 1614,

    [EnumMember]
    DestinyContentPropertyNotFound = 1615,

    [EnumMember]
    DestinyContentConfigNotFound = 1616,

    [EnumMember]
    DestinyContentPropertyBucketValueNotFound = 1617,

    [EnumMember]
    DestinyUnexpectedError = 1618,

    [EnumMember]
    DestinyInvalidAction = 1619,

    [EnumMember]
    DestinyCharacterNotFound = 1620,

    [EnumMember]
    DestinyInvalidFlag = 1621,

    [EnumMember]
    DestinyInvalidRequest = 1622,

    [EnumMember]
    DestinyItemNotFound = 1623,

    [EnumMember]
    DestinyInvalidCustomizationChoices = 1624,

    [EnumMember]
    DestinyVendorItemNotFound = 1625,

    [EnumMember]
    DestinyInternalError = 1626,

    [EnumMember]
    DestinyVendorNotFound = 1627,

    [EnumMember]
    DestinyRecentActivitiesDatabaseError = 1628,

    [EnumMember]
    DestinyItemBucketNotFound = 1629,

    [EnumMember]
    DestinyInvalidMembershipType = 1630,

    [EnumMember]
    DestinyVersionIncompatibility = 1631,

    [EnumMember]
    DestinyItemAlreadyInInventory = 1632,

    [EnumMember]
    DestinyBucketNotFound = 1633,

    [EnumMember]
    DestinyCharacterNotInTower = 1634,

    [EnumMember]
    DestinyCharacterNotLoggedIn = 1635,

    [EnumMember]
    DestinyDefinitionsNotLoaded = 1636,

    [EnumMember]
    DestinyInventoryFull = 1637,

    [EnumMember]
    DestinyItemFailedLevelCheck = 1638,

    [EnumMember]
    DestinyItemFailedUnlockCheck = 1639,

    [EnumMember]
    DestinyItemUnequippable = 1640,

    [EnumMember]
    DestinyItemUniqueEquipRestricted = 1641,

    [EnumMember]
    DestinyNoRoomInDestination = 1642,

    [EnumMember]
    DestinyServiceFailure = 1643,

    [EnumMember]
    DestinyServiceRetired = 1644,

    [EnumMember]
    DestinyTransferFailed = 1645,

    [EnumMember]
    DestinyTransferNotFoundForSourceBucket = 1646,

    [EnumMember]
    DestinyUnexpectedResultInVendorTransferCheck = 1647,

    [EnumMember]
    DestinyUniquenessViolation = 1648,

    [EnumMember]
    DestinyErrorDeserializationFailure = 1649,

    [EnumMember]
    DestinyValidAccountTicketRequired = 1650,

    [EnumMember]
    DestinyShardRelayClientTimeout = 1651,

    [EnumMember]
    DestinyShardRelayProxyTimeout = 1652,

    [EnumMember]
    DestinyPGCRNotFound = 1653,

    [EnumMember]
    DestinyAccountMustBeOffline = 1654,

    [EnumMember]
    DestinyCanOnlyEquipInGame = 1655,

    [EnumMember]
    DestinyCannotPerformActionOnEquippedItem = 1656,

    [EnumMember]
    DestinyQuestAlreadyCompleted = 1657,

    [EnumMember]
    DestinyQuestAlreadyTracked = 1658,

    [EnumMember]
    DestinyTrackableQuestsFull = 1659,

    [EnumMember]
    DestinyItemNotTransferrable = 1660,

    [EnumMember]
    DestinyVendorPurchaseNotAllowed = 1661,

    [EnumMember]
    DestinyContentVersionMismatch = 1662,

    [EnumMember]
    DestinyItemActionForbidden = 1663,

    [EnumMember]
    DestinyRefundInvalid = 1664,

    [EnumMember]
    DestinyPrivacyRestriction = 1665,

    [EnumMember]
    DestinyActionInsufficientPrivileges = 1666,

    [EnumMember]
    DestinyInvalidClaimException = 1667,

    [EnumMember]
    DestinyLegacyPlatformRestricted = 1668,

    [EnumMember]
    DestinyLegacyPlatformInUse = 1669,

    [EnumMember]
    DestinyLegacyPlatformInaccessible = 1670,

    [EnumMember]
    DestinyCannotPerformActionAtThisLocation = 1671,

    [EnumMember]
    DestinyThrottledByGameServer = 1672,

    [EnumMember]
    DestinyItemNotTransferrableHasSideEffects = 1673,

    [EnumMember]
    DestinyItemLocked = 1674,

    [EnumMember]
    DestinyCannotAffordMaterialRequirements = 1675,

    [EnumMember]
    DestinyFailedPlugInsertionRules = 1676,

    [EnumMember]
    DestinySocketNotFound = 1677,

    [EnumMember]
    DestinySocketActionNotAllowed = 1678,

    [EnumMember]
    DestinySocketAlreadyHasPlug = 1679,

    [EnumMember]
    DestinyPlugItemNotAvailable = 1680,

    [EnumMember]
    DestinyCharacterLoggedInNotAllowed = 1681,

    [EnumMember]
    DestinyPublicAccountNotAccessible = 1682,

    [EnumMember]
    DestinyClaimsItemAlreadyClaimed = 1683,

    [EnumMember]
    DestinyClaimsNoInventorySpace = 1684,

    [EnumMember]
    DestinyClaimsRequiredLevelNotMet = 1685,

    [EnumMember]
    DestinyClaimsInvalidState = 1686,

    [EnumMember]
    DestinyNotEnoughRoomForMultipleRewards = 1687,

    [EnumMember]
    DestinyDirectBabelClientTimeout = 1688,

    [EnumMember]
    FbInvalidRequest = 1800,

    [EnumMember]
    FbRedirectMismatch = 1801,

    [EnumMember]
    FbAccessDenied = 1802,

    [EnumMember]
    FbUnsupportedResponseType = 1803,

    [EnumMember]
    FbInvalidScope = 1804,

    [EnumMember]
    FbUnsupportedGrantType = 1805,

    [EnumMember]
    FbInvalidGrant = 1806,

    [EnumMember]
    InvitationExpired = 1900,

    [EnumMember]
    InvitationUnknownType = 1901,

    [EnumMember]
    InvitationInvalidResponseStatus = 1902,

    [EnumMember]
    InvitationInvalidType = 1903,

    [EnumMember]
    InvitationAlreadyPending = 1904,

    [EnumMember]
    InvitationInsufficientPermission = 1905,

    [EnumMember]
    InvitationInvalidCode = 1906,

    [EnumMember]
    InvitationInvalidTargetState = 1907,

    [EnumMember]
    InvitationCannotBeReactivated = 1908,

    [EnumMember]
    InvitationNoRecipients = 1910,

    [EnumMember]
    InvitationGroupCannotSendToSelf = 1911,

    [EnumMember]
    InvitationTooManyRecipients = 1912,

    [EnumMember]
    InvitationInvalid = 1913,

    [EnumMember]
    InvitationNotFound = 1914,

    [EnumMember]
    TokenInvalid = 2000,

    [EnumMember]
    TokenBadFormat = 2001,

    [EnumMember]
    TokenAlreadyClaimed = 2002,

    [EnumMember]
    TokenAlreadyClaimedSelf = 2003,

    [EnumMember]
    TokenThrottling = 2004,

    [EnumMember]
    TokenUnknownRedemptionFailure = 2005,

    [EnumMember]
    TokenPurchaseClaimFailedAfterTokenClaimed = 2006,

    [EnumMember]
    TokenUserAlreadyOwnsOffer = 2007,

    [EnumMember]
    TokenInvalidOfferKey = 2008,

    [EnumMember]
    TokenEmailNotValidated = 2009,

    [EnumMember]
    TokenProvisioningBadVendorOrOffer = 2010,

    [EnumMember]
    TokenPurchaseHistoryUnknownError = 2011,

    [EnumMember]
    TokenThrottleStateUnknownError = 2012,

    [EnumMember]
    TokenUserAgeNotVerified = 2013,

    [EnumMember]
    TokenExceededOfferMaximum = 2014,

    [EnumMember]
    TokenNoAvailableUnlocks = 2015,

    [EnumMember]
    TokenMarketplaceInvalidPlatform = 2016,

    [EnumMember]
    TokenNoMarketplaceCodesFound = 2017,

    [EnumMember]
    TokenOfferNotAvailableForRedemption = 2018,

    [EnumMember]
    TokenUnlockPartialFailure = 2019,

    [EnumMember]
    TokenMarketplaceInvalidRegion = 2020,

    [EnumMember]
    TokenOfferExpired = 2021,

    [EnumMember]
    RAFExceededMaximumReferrals = 2022,

    [EnumMember]
    RAFDuplicateBond = 2023,

    [EnumMember]
    RAFNoValidVeteranDestinyMembershipsFound = 2024,

    [EnumMember]
    RAFNotAValidVeteranUser = 2025,

    [EnumMember]
    RAFCodeAlreadyClaimedOrNotFound = 2026,

    [EnumMember]
    RAFMismatchedDestinyMembershipType = 2027,

    [EnumMember]
    RAFUnableToAccessPurchaseHistory = 2028,

    [EnumMember]
    RAFUnableToCreateBond = 2029,

    [EnumMember]
    RAFUnableToFindBond = 2030,

    [EnumMember]
    RAFUnableToRemoveBond = 2031,

    [EnumMember]
    RAFCannotBondToSelf = 2032,

    [EnumMember]
    RAFInvalidPlatform = 2033,

    [EnumMember]
    RAFGenerateThrottled = 2034,

    [EnumMember]
    RAFUnableToCreateBondVersionMismatch = 2035,

    [EnumMember]
    RAFUnableToRemoveBondVersionMismatch = 2036,

    [EnumMember]
    RAFRedeemThrottled = 2037,

    [EnumMember]
    NoAvailableDiscountCode = 2038,

    [EnumMember]
    DiscountAlreadyClaimed = 2039,

    [EnumMember]
    DiscountClaimFailure = 2040,

    [EnumMember]
    DiscountConfigurationFailure = 2041,

    [EnumMember]
    DiscountGenerationFailure = 2042,

    [EnumMember]
    DiscountAlreadyExists = 2043,

    [EnumMember]
    TokenRequiresCredentialXuid = 2044,

    [EnumMember]
    TokenRequiresCredentialPsnid = 2045,

    [EnumMember]
    OfferRequired = 2046,

    [EnumMember]
    UnknownEververseHistoryError = 2047,

    [EnumMember]
    MissingEververseHistoryError = 2048,

    [EnumMember]
    BungieRewardEmailStateInvalid = 2049,

    [EnumMember]
    BungieRewardNotYetClaimable = 2050,

    [EnumMember]
    MissingOfferConfig = 2051,

    [EnumMember]
    RAFQuestEntitlementRequiresBnet = 2052,

    [EnumMember]
    RAFQuestEntitlementTransportFailure = 2053,

    [EnumMember]
    RAFQuestEntitlementUnknownFailure = 2054,

    [EnumMember]
    RAFVeteranRewardUnknownFailure = 2055,

    [EnumMember]
    RAFTooEarlyToCancelBond = 2056,

    [EnumMember]
    LoyaltyRewardAlreadyRedeemed = 2057,

    [EnumMember]
    UnclaimedLoyaltyRewardEntryNotFound = 2058,

    [EnumMember]
    PartnerOfferPartialFailure = 2059,

    [EnumMember]
    PartnerOfferAlreadyClaimed = 2060,

    [EnumMember]
    PartnerOfferSkuNotFound = 2061,

    [EnumMember]
    PartnerOfferSkuExpired = 2062,

    [EnumMember]
    PartnerOfferPermissionFailure = 2063,

    [EnumMember]
    PartnerOfferNoDestinyAccount = 2064,

    [EnumMember]
    PartnerOfferApplyDataNotFound = 2065,

    [EnumMember]
    ApiExceededMaxKeys = 2100,

    [EnumMember]
    ApiInvalidOrExpiredKey = 2101,

    [EnumMember]
    ApiKeyMissingFromRequest = 2102,

    [EnumMember]
    ApplicationDisabled = 2103,

    [EnumMember]
    ApplicationExceededMax = 2104,

    [EnumMember]
    ApplicationDisallowedByScope = 2105,

    [EnumMember]
    AuthorizationCodeInvalid = 2106,

    [EnumMember]
    OriginHeaderDoesNotMatchKey = 2107,

    [EnumMember]
    AccessNotPermittedByApplicationScope = 2108,

    [EnumMember]
    ApplicationNameIsTaken = 2109,

    [EnumMember]
    RefreshTokenNotYetValid = 2110,

    [EnumMember]
    AccessTokenHasExpired = 2111,

    [EnumMember]
    ApplicationTokenFormatNotValid = 2112,

    [EnumMember]
    ApplicationNotConfiguredForBungieAuth = 2113,

    [EnumMember]
    ApplicationNotConfiguredForOAuth = 2114,

    [EnumMember]
    OAuthAccessTokenExpired = 2115,

    [EnumMember]
    ApplicationTokenKeyIdDoesNotExist = 2116,

    [EnumMember]
    ProvidedTokenNotValidRefreshToken = 2117,

    [EnumMember]
    RefreshTokenExpired = 2118,

    [EnumMember]
    AuthorizationRecordInvalid = 2119,

    [EnumMember]
    TokenPreviouslyRevoked = 2120,

    [EnumMember]
    TokenInvalidMembership = 2121,

    [EnumMember]
    AuthorizationCodeStale = 2122,

    [EnumMember]
    AuthorizationRecordExpired = 2123,

    [EnumMember]
    AuthorizationRecordRevoked = 2124,

    [EnumMember]
    AuthorizationRecordInactiveApiKey = 2125,

    [EnumMember]
    AuthorizationRecordApiKeyMatching = 2126,

    [EnumMember]
    PartnershipInvalidType = 2200,

    [EnumMember]
    PartnershipValidationError = 2201,

    [EnumMember]
    PartnershipValidationTimeout = 2202,

    [EnumMember]
    PartnershipAccessFailure = 2203,

    [EnumMember]
    PartnershipAccountInvalid = 2204,

    [EnumMember]
    PartnershipGetAccountInfoFailure = 2205,

    [EnumMember]
    PartnershipDisabled = 2206,

    [EnumMember]
    PartnershipAlreadyExists = 2207,

    [EnumMember]
    CommunityStreamingUnavailable = 2300,

    [EnumMember]
    TwitchNotLinked = 2500,

    [EnumMember]
    TwitchAccountNotFound = 2501,

    [EnumMember]
    TwitchCouldNotLoadDestinyInfo = 2502,

    [EnumMember]
    TwitchCouldNotRegisterUser = 2503,

    [EnumMember]
    TwitchCouldNotUnregisterUser = 2504,

    [EnumMember]
    TwitchRequiresRelinking = 2505,

    [EnumMember]
    TwitchNoPlatformChosen = 2506,

    [EnumMember]
    TwitchDropHistoryPermissionFailure = 2507,

    [EnumMember]
    TwitchDropsRepairPartialFailure = 2508,

    [EnumMember]
    TwitchNotAuthorized = 2509,

    [EnumMember]
    TwitchUnknownAuthorizationFailure = 2510,

    [EnumMember]
    TrendingCategoryNotFound = 2600,

    [EnumMember]
    TrendingEntryTypeNotSupported = 2601,

    [EnumMember]
    ReportOffenderNotInPgcr = 2700,

    [EnumMember]
    ReportRequestorNotInPgcr = 2701,

    [EnumMember]
    ReportSubmissionFailed = 2702,

    [EnumMember]
    ReportCannotReportSelf = 2703,

    [EnumMember]
    AwaTypeDisabled = 2800,

    [EnumMember]
    AwaTooManyPendingRequests = 2801,

    [EnumMember]
    AwaTheFeatureRequiresARegisteredDevice = 2802,

    [EnumMember]
    AwaRequestWasUnansweredForTooLong = 2803,

    [EnumMember]
    AwaWriteRequestMissingOrInvalidToken = 2804,

    [EnumMember]
    AwaWriteRequestTokenExpired = 2805,

    [EnumMember]
    AwaWriteRequestTokenUsageLimitReached = 2806,

    [EnumMember]
    SteamWebApiError = 2900,

    [EnumMember]
    SteamWebNullResponseError = 2901,

    [EnumMember]
    SteamAccountRequired = 2902,

    [EnumMember]
    SteamNotAuthorized = 2903,

    [EnumMember]
    ClanFireteamNotFound = 3000,

    [EnumMember]
    ClanFireteamAddNoAlternatesForImmediate = 3001,

    [EnumMember]
    ClanFireteamFull = 3002,

    [EnumMember]
    ClanFireteamAltFull = 3003,

    [EnumMember]
    ClanFireteamBlocked = 3004,

    [EnumMember]
    ClanFireteamPlayerEntryNotFound = 3005,

    [EnumMember]
    ClanFireteamPermissions = 3006,

    [EnumMember]
    ClanFireteamInvalidPlatform = 3007,

    [EnumMember]
    ClanFireteamCannotAdjustSlotCount = 3008,

    [EnumMember]
    ClanFireteamInvalidPlayerPlatform = 3009,

    [EnumMember]
    ClanFireteamNotReadyForInvitesNotEnoughPlayers = 3010,

    [EnumMember]
    ClanFireteamGameInvitesNotSupportForPlatform = 3011,

    [EnumMember]
    ClanFireteamPlatformInvitePreqFailure = 3012,

    [EnumMember]
    ClanFireteamInvalidAuthContext = 3013,

    [EnumMember]
    ClanFireteamInvalidAuthProviderPsn = 3014,

    [EnumMember]
    ClanFireteamPs4SessionFull = 3015,

    [EnumMember]
    ClanFireteamInvalidAuthToken = 3016,

    [EnumMember]
    ClanFireteamScheduledFireteamsDisabled = 3017,

    [EnumMember]
    ClanFireteamNotReadyForInvitesNotScheduledYet = 3018,

    [EnumMember]
    ClanFireteamNotReadyForInvitesClosed = 3019,

    [EnumMember]
    ClanFireteamScheduledFireteamsRequireAdminPermissions = 3020,

    [EnumMember]
    ClanFireteamNonPublicMustHaveClan = 3021,

    [EnumMember]
    ClanFireteamPublicCreationRestriction = 3022,

    [EnumMember]
    ClanFireteamAlreadyJoined = 3023,

    [EnumMember]
    ClanFireteamScheduledFireteamsRange = 3024,

    [EnumMember]
    ClanFireteamPublicCreationRestrictionExtended = 3025,

    [EnumMember]
    ClanFireteamExpired = 3026,

    [EnumMember]
    ClanFireteamInvalidAuthProvider = 3027,

    [EnumMember]
    ClanFireteamInvalidAuthProviderXuid = 3028,

    [EnumMember]
    ClanFireteamThrottle = 3029,

    [EnumMember]
    ClanFireteamTooManyOpenScheduledFireteams = 3030,

    [EnumMember]
    ClanFireteamCannotReopenScheduledFireteams = 3031,

    [EnumMember]
    ClanFireteamJoinNoAccountSpecified = 3032,

    [EnumMember]
    ClanFireteamMinDestiny2ProgressForCreation = 3033,

    [EnumMember]
    ClanFireteamMinDestiny2ProgressForJoin = 3034,

    [EnumMember]
    ClanFireteamSMSOrPurchaseRequiredCreate = 3035,

    [EnumMember]
    ClanFireteamPurchaseRequiredCreate = 3036,

    [EnumMember]
    ClanFireteamSMSOrPurchaseRequiredJoin = 3037,

    [EnumMember]
    ClanFireteamPurchaseRequiredJoin = 3038,

    [EnumMember]
    CrossSaveOverriddenAccountNotFound = 3200,

    [EnumMember]
    CrossSaveTooManyOverriddenPlatforms = 3201,

    [EnumMember]
    CrossSaveNoOverriddenPlatforms = 3202,

    [EnumMember]
    CrossSavePrimaryAccountNotFound = 3203,

    [EnumMember]
    CrossSaveRequestInvalid = 3204,

    [EnumMember]
    CrossSaveBungieAccountValidationFailure = 3206,

    [EnumMember]
    CrossSaveOverriddenPlatformNotAllowed = 3207,

    [EnumMember]
    CrossSaveThresholdExceeded = 3208,

    [EnumMember]
    CrossSaveIncompatibleMembershipType = 3209,

    [EnumMember]
    CrossSaveCouldNotFindLinkedAccountForMembershipType = 3210,

    [EnumMember]
    CrossSaveCouldNotCreateDestinyProfileForMembershipType = 3211,

    [EnumMember]
    CrossSaveErrorCreatingDestinyProfileForMembershipType = 3212,

    [EnumMember]
    CrossSaveCannotOverrideSelf = 3213,

    [EnumMember]
    CrossSaveRecentSilverPurchase = 3214,

    [EnumMember]
    CrossSaveSilverBalanceNegative = 3215,

    [EnumMember]
    CrossSaveAccountNotAuthenticated = 3216,

    [EnumMember]
    ErrorOneAccountAlreadyActive = 3217,

    [EnumMember]
    ErrorOneAccountDestinyRestriction = 3218,

    [EnumMember]
    CrossSaveMustMigrateToSteam = 3219,

    [EnumMember]
    CrossSaveSteamAlreadyPaired = 3220,

    [EnumMember]
    CrossSaveCannotPairJustSteamAndBlizzard = 3221,

    [EnumMember]
    CrossSaveCannotPairSteamAloneBeforeShadowkeep = 3222,

    [EnumMember]
    AuthVerificationNotLinkedToAccount = 3300,

    [EnumMember]
    PCMigrationMissingBlizzard = 3400,

    [EnumMember]
    PCMigrationMissingSteam = 3401,

    [EnumMember]
    PCMigrationInvalidBlizzard = 3402,

    [EnumMember]
    PCMigrationInvalidSteam = 3403,

    [EnumMember]
    PCMigrationUnknownFailure = 3404,

    [EnumMember]
    PCMigrationUnknownException = 3405,

    [EnumMember]
    PCMigrationNotLinked = 3406,

    [EnumMember]
    PCMigrationAccountsAlreadyUsed = 3407,

    [EnumMember]
    PCMigrationStepFailed = 3408,

    [EnumMember]
    PCMigrationInvalidBlizzardCrossSaveState = 3409,

    [EnumMember]
    PCMigrationDestinationBanned = 3410,

    [EnumMember]
    PCMigrationDestinyFailure = 3411,

    [EnumMember]
    PCMigrationSilverTransferFailed = 3412,

    [EnumMember]
    PCMigrationEntitlementTransferFailed = 3413,

    [EnumMember]
    PCMigrationCannotStompClanFounder = 3414,

    [EnumMember]
    UnsupportedBrowser = 3500,

    [EnumMember]
    StadiaAccountRequired = 3600,

    [EnumMember]
    ErrorPhoneValidationTooManyUses = 3702,

    [EnumMember]
    ErrorPhoneValidationNoAssociatedPhone = 3703,

    [EnumMember]
    ErrorPhoneValidationCodeInvalid = 3705,

    [EnumMember]
    ErrorPhoneValidationBanned = 3706,

    [EnumMember]
    ErrorPhoneValidationCodeTooRecentlySent = 3707,

    [EnumMember]
    ErrorPhoneValidationCodeExpired = 3708,

    [EnumMember]
    ErrorPhoneValidationInvalidNumberType = 3709,

    [EnumMember]
    ErrorPhoneValidationCodeTooRecentlyChecked = 3710,

    [EnumMember]
    ApplePushErrorUnknown = 3800,

    [EnumMember]
    ApplePushErrorNull = 3801,

    [EnumMember]
    ApplePushErrorTimeout = 3802,

    [EnumMember]
    ApplePushBadRequest = 3803,

    [EnumMember]
    ApplePushFailedAuth = 3804,

    [EnumMember]
    ApplePushThrottled = 3805,

    [EnumMember]
    ApplePushServiceUnavailable = 3806,

    [EnumMember]
    NotAnImageOrVideo = 3807,

    [EnumMember]
    ErrorBungieFriendsBlockFailed = 3900,

    [EnumMember]
    ErrorBungieFriendsAutoReject = 3901,

    [EnumMember]
    ErrorBungieFriendsNoRequestFound = 3902,

    [EnumMember]
    ErrorBungieFriendsAlreadyFriends = 3903,

    [EnumMember]
    ErrorBungieFriendsUnableToRemoveRequest = 3904,

    [EnumMember]
    ErrorBungieFriendsUnableToRemove = 3905,

    [EnumMember]
    ErrorBungieFriendsIdenticalSourceTarget = 3906,

    [EnumMember]
    ErrorBungieFriendsSelf = 3907,

    [EnumMember]
    ErrorBungieBlockSelf = 3908,

    [EnumMember]
    ErrorBungieFriendsListFull = 3910,

    [EnumMember]
    ErrorBungieBlockListFull = 3911,

    [EnumMember]
    ErrorEgsUnknown = 4000,

    [EnumMember]
    ErrorEgsBadRequest = 4001,

    [EnumMember]
    ErrorEgsNotAuthorized = 4002,

    [EnumMember]
    ErrorEgsForbidden = 4003,

    [EnumMember]
    ErrorEgsAccountNotFound = 4004,

    [EnumMember]
    ErrorEgsWebException = 4005,

    [EnumMember]
    ErrorEgsUnavailable = 4006,

    [EnumMember]
    ErrorEgsJwksMissing = 4007,

    [EnumMember]
    ErrorEgsJwtMalformedHeader = 4008,

    [EnumMember]
    ErrorEgsJwtMalformedPayload = 4009
}