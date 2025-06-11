# Rasputin API Documentation

This document provides technical documentation for the Rasputin API endpoints and implementation details.

## API Overview

The Rasputin API is a RESTful service built with ASP.NET Core Minimal APIs that provides access to Destiny 2 player data, clan information, and activity statistics.

### Base URL
```
http://localhost:5000/api
```

## Authentication

Currently, the API does not require authentication. This may change in future versions.

## Response Format

All API responses use a standardized wrapper format:

```json
{
  "success": true,
  "data": { ... },
  "error": null,
  "timestamp": "2024-01-16T12:00:00Z"
}
```

Error responses:
```json
{
  "success": false,
  "data": null,
  "error": "Error message here",
  "timestamp": "2024-01-16T12:00:00Z"
}
```

## Member Endpoints

### Get Member Summary
Retrieves comprehensive information about a member including characters and clan memberships.

```
GET /api/members/{identifier}
```

**Parameters:**
- `identifier` - Can be either:
  - Numeric MembershipId (e.g., "4611686018467431790")
  - DisplayNameGlobal (e.g., "PlayerName#1234")

**Response:**
```json
{
  "membershipId": "4611686018467431790",
  "displayName": "PlayerName",
  "displayNameGlobal": "PlayerName#1234",
  "platform": 3,
  "guardianRankCurrent": 11,
  "guardianRankLifetime": 11,
  "lastPlayedAt": "2024-01-16T10:30:00Z",
  "characters": [
    {
      "characterId": "2305843009504575107",
      "classHash": "671679327",
      "className": "Hunter",
      "lightLevel": 1810,
      "minutesPlayedSession": 12560,
      "minutesPlayedLifetime": 156789,
      "lastPlayedAt": "2024-01-16T10:30:00Z",
      "emblemUrl": "https://www.bungie.net/..."
    }
  ],
  "clans": [
    {
      "groupId": "4107840",
      "clanName": "Level Crush",
      "clanTag": "LC",
      "groupRole": 3,
      "joinedAt": "2023-05-15T08:00:00Z"
    }
  ]
}
```

### Get Member Activities
Retrieves paginated activity history for a member.

```
GET /api/members/{identifier}/activities?page=1&pageSize=20&mode=4&startDate=2024-01-01&endDate=2024-01-16
```

**Parameters:**
- `identifier` - MembershipId or DisplayNameGlobal
- `page` - Page number (default: 1)
- `pageSize` - Items per page (default: 20, max: 100)
- `mode` - Activity mode filter (optional, see mode values below)
- `startDate` - Filter activities after this date (optional, ISO 8601)
- `endDate` - Filter activities before this date (optional, ISO 8601)

**Response:**
```json
{
  "items": [
    {
      "instanceId": "13456789012",
      "activityHash": "1374392663",
      "mode": 4,
      "completed": true,
      "occurredAt": "2024-01-16T09:00:00Z",
      "stats": {
        "kills": 125.0,
        "deaths": 8.0,
        "assists": 45.0,
        "score": 18750.0
      }
    }
  ],
  "page": 1,
  "pageSize": 20,
  "totalCount": 150,
  "totalPages": 8,
  "hasNext": true,
  "hasPrevious": false
}
```

### Get Member Statistics
Retrieves aggregated statistics for a member.

```
GET /api/members/{identifier}/stats?startDate=2024-01-01&endDate=2024-01-16
```

**Parameters:**
- `identifier` - MembershipId or DisplayNameGlobal
- `startDate` - Calculate stats after this date (optional)
- `endDate` - Calculate stats before this date (optional)

**Response:**
```json
{
  "membershipId": "4611686018467431790",
  "totalActivities": 342,
  "activitiesByMode": {
    "Raid": 45,
    "Strike": 120,
    "Gambit": 67,
    "Control": 110
  },
  "averageStats": {
    "kills": 95.5,
    "deaths": 12.3,
    "assists": 28.7,
    "efficiency": 2.85
  },
  "firstActivity": "2024-01-01T08:30:00Z",
  "lastActivity": "2024-01-16T10:30:00Z"
}
```

### Get Member Titles
Retrieves earned titles/seals for a member.

```
GET /api/members/{identifier}/titles
```

**Parameters:**
- `identifier` - MembershipId or DisplayNameGlobal

**Response:**
```json
[
  {
    "title": "Rivensbane",
    "amount": 1
  },
  {
    "title": "Dredgen",
    "amount": 1
  }
]
```

### Get Member Leaderboard Positions
Retrieves the member's ranking across various leaderboards.

```
GET /api/members/{identifier}/leaderboards
```

**Parameters:**
- `identifier` - MembershipId or DisplayNameGlobal

**Response:**
```json
{
  "membershipId": "4611686018467431790",
  "displayName": "PlayerName#1234",
  "positions": {
    "kills": {
      "leaderboardType": "kills",
      "rank": 42,
      "totalPlayers": 1250,
      "percentile": 96.64,
      "value": 125000.0
    }
  }
}
```

## Clan Endpoints

### Get Clan Summary
Retrieves basic information about a clan.

```
GET /api/clans/{groupId}
```

**Response:**
```json
{
  "groupId": "4107840",
  "name": "Level Crush",
  "slug": "level-crush",
  "motto": "Crushing it since 2017",
  "about": "A gaming community focused on Destiny 2",
  "callSign": "LC",
  "isNetwork": true,
  "memberCount": 85,
  "createdAt": "2017-09-15T12:00:00Z"
}
```

### Get Clan Roster
Retrieves paginated clan member list.

```
GET /api/clans/{groupId}/roster?page=1&pageSize=50&sortBy=lastPlayed
```

**Parameters:**
- `page` - Page number (default: 1)
- `pageSize` - Items per page (default: 50)
- `sortBy` - Sort order: "lastPlayed", "name", "rank", "joined" (default: "lastPlayed")

### Get Clan Statistics
Retrieves aggregated clan activity statistics.

```
GET /api/clans/{groupId}/stats?startDate=2024-01-01&endDate=2024-01-16
```

### List All Clans
Retrieves all tracked clans.

```
GET /api/clans?isNetwork=true
```

**Parameters:**
- `isNetwork` - Filter by network status (optional)

## Leaderboard Endpoints

### Get Leaderboard
Retrieves top players for a specific stat.

```
GET /api/leaderboards/{stat}?period=weekly&limit=100&networkOnly=true
```

**Parameters:**
- `stat` - Stat name (e.g., "kills", "deaths", "efficiency")
- `period` - Time period: "all", "daily", "weekly", "monthly", "season" (default: "all")
- `limit` - Number of entries to return (default: 100)
- `networkOnly` - Only include network clan members (default: true)

## Activity Endpoints

### Get Activity Details
Retrieves details about a specific activity instance.

```
GET /api/activities/{instanceId}
```

## Utility Endpoints

### Health Check
```
GET /api/health
```

**Response:**
```json
{
  "status": "Healthy",
  "database": "Connected",
  "timestamp": "2024-01-16T12:00:00Z"
}
```

### API Info
```
GET /api
```

## Activity Mode Values

| Mode | Name |
|------|------|
| 0 | None |
| 2 | Story |
| 3 | Strike |
| 4 | Raid |
| 5 | AllPvP |
| 6 | Patrol |
| 7 | AllPvE |
| 10 | Control |
| 12 | Clash |
| 16 | Nightfall |
| 18 | AllStrikes |
| 19 | IronBanner |
| 31 | Supremacy |
| 37 | Survival |
| 48 | Rumble |
| 63 | Gambit |
| 69 | PvPCompetitive |
| 70 | PvPQuickplay |
| 82 | Dungeon |
| 84 | TrialsOfOsiris |

## Error Handling

The API uses standard HTTP status codes:
- 200 OK - Request successful
- 404 Not Found - Resource not found
- 500 Internal Server Error - Server error

All errors are returned in the standard response format with the `success` field set to `false` and an error message.

## Implementation Notes

### Member Identifier Resolution
The `FindMemberByIdentifier` helper function allows flexible member lookups:
1. First attempts to parse the identifier as a numeric MembershipId
2. If successful and a member is found, returns that member
3. Otherwise, searches by DisplayNameGlobal (exact match, case-sensitive)

### Database Queries
- All timestamps in the database are stored as Unix timestamps (seconds since epoch)
- Boolean fields in some tables are stored as tinyint (0/1) and converted appropriately
- The API uses Entity Framework Core with MySQL
- Complex queries use raw SQL for performance

### Caching
The API is designed to work with Redis caching, though implementation details are handled by the Rasputin-Redis project.

### Message Queue Integration
Background data updates are handled through RabbitMQ message queues, processed by the Rasputin-MessageQueue-Consumer service.