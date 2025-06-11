# Changelog

All notable changes to the Rasputin - Destiny 2 Data Tracking System will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased] - 2025-01-16

### Added
- Comprehensive REST API implementation in Rasputin-Server
  - Member endpoints with flexible identifier lookup (MembershipId or DisplayNameGlobal)
    - `GET /api/members/{identifier}` - Get member summary
    - `GET /api/members/{identifier}/activities` - Get member activities with pagination
    - `GET /api/members/{identifier}/stats` - Get member activity statistics
    - `GET /api/members/{identifier}/titles` - Get member titles
    - `GET /api/members/{identifier}/leaderboards` - Get member leaderboard positions
  - Clan endpoints
    - `GET /api/clans` - List all clans (with optional network filter)
    - `GET /api/clans/{groupId}` - Get clan summary
    - `GET /api/clans/{groupId}/roster` - Get clan roster with pagination and sorting
    - `GET /api/clans/{groupId}/stats` - Get clan activity statistics
  - Leaderboard endpoints
    - `GET /api/leaderboards/{stat}` - Get leaderboard for specific stat
  - Activity endpoints
    - `GET /api/activities/{instanceId}` - Get specific activity details
  - Health and status endpoints
    - `GET /api/health` - Health check endpoint
    - `GET /api` - API information endpoint
- Swagger/OpenAPI documentation support
- CORS support for cross-origin requests
- Standardized API response wrapper with success/error handling
- Response model classes for type-safe API responses
  - ApiResponse.cs - Standard API response wrapper
  - MemberResponses.cs - Member-related response models
  - ClanResponses.cs - Clan-related response models  
  - LeaderboardResponses.cs - Leaderboard response models
- Pagination support for large result sets
- Date range filtering for activities and statistics
- Activity mode filtering
- Sorting options for clan rosters

### Changed
- Updated README.md with accurate project description
  - Changed from experimental language project to Destiny 2 data tracking system
  - Added comprehensive architecture overview
  - Added technology stack details
  - Added API examples section with curl commands
- Refactored existing `/member/{bungieName}/titles` endpoint to use new standardized patterns
- Improved SQL queries with proper column name mapping

### Fixed
- Fixed SQL column name case sensitivity issue in leaderboard queries
  - Changed column aliases to match C# property names exactly (e.g., 'LeaderboardType' instead of 'leaderboard_type')
- Fixed type conversion issues between database types and C# types
  - Properly handle uint to string conversions for hashes
  - Correctly convert bool to int for activity modes
  - Handle long timestamp conversions

### Technical Details
- Implemented helper function `FindMemberByIdentifier` for flexible member lookups
- Added proper error handling and logging throughout API endpoints
- Used Entity Framework Core's SqlQuery for complex SQL operations
- Implemented proper Unix timestamp conversions for date/time fields

## [Previous Versions]

_No previous versions documented yet. This is the initial comprehensive API implementation._