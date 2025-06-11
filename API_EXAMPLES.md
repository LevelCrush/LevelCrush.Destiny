# Rasputin API Examples

This document provides practical examples of using the Rasputin API endpoints with various tools and programming languages.

## Base URL

```
http://localhost:5000
```

## Authentication

Currently, the API does not require authentication. All endpoints are publicly accessible.

## Examples by Language

### cURL Examples

#### Member Endpoints

```bash
# Get member by Bungie name
curl -X GET "http://localhost:5000/api/members/ExamplePlayer%231234"

# Get member by membership ID
curl -X GET "http://localhost:5000/api/members/4611686018467431790"

# Get member activities (page 1, 20 items per page, raid mode only)
curl -X GET "http://localhost:5000/api/members/ExamplePlayer%231234/activities?page=1&pageSize=20&mode=4"

# Get member activities within date range
curl -X GET "http://localhost:5000/api/members/ExamplePlayer%231234/activities?startDate=2024-01-01&endDate=2024-12-31"

# Get member statistics for the current year
curl -X GET "http://localhost:5000/api/members/ExamplePlayer%231234/stats?startDate=2024-01-01"

# Get member titles
curl -X GET "http://localhost:5000/api/members/ExamplePlayer%231234/titles"

# Get member leaderboard positions
curl -X GET "http://localhost:5000/api/members/ExamplePlayer%231234/leaderboards"
```

#### Clan Endpoints

```bash
# List all network clans
curl -X GET "http://localhost:5000/api/clans?isNetwork=true"

# Get specific clan details
curl -X GET "http://localhost:5000/api/clans/4107840"

# Get clan roster sorted by last played date
curl -X GET "http://localhost:5000/api/clans/4107840/roster?sortBy=lastPlayed&page=1&pageSize=50"

# Get clan roster sorted by guardian rank
curl -X GET "http://localhost:5000/api/clans/4107840/roster?sortBy=rank"

# Get clan activity statistics for the last 30 days
curl -X GET "http://localhost:5000/api/clans/4107840/stats?startDate=2024-11-01&endDate=2024-12-01"
```

#### Leaderboard Endpoints

```bash
# Get weekly kills leaderboard (top 100)
curl -X GET "http://localhost:5000/api/leaderboards/kills?period=weekly&limit=100"

# Get all-time efficiency leaderboard
curl -X GET "http://localhost:5000/api/leaderboards/efficiency?period=all"

# Get monthly deaths leaderboard for network clans only
curl -X GET "http://localhost:5000/api/leaderboards/deaths?period=monthly&networkOnly=true"
```

#### Activity Endpoints

```bash
# Get specific activity details
curl -X GET "http://localhost:5000/api/activities/12345678901234567890"
```

#### Health Check

```bash
# Check API health status
curl -X GET "http://localhost:5000/api/health"

# Get API information
curl -X GET "http://localhost:5000/api"
```

### JavaScript/TypeScript Examples

```javascript
// Using fetch API
async function getMember(identifier) {
  const response = await fetch(`http://localhost:5000/api/members/${encodeURIComponent(identifier)}`);
  const data = await response.json();
  
  if (data.success) {
    console.log('Member data:', data.data);
  } else {
    console.error('Error:', data.error);
  }
}

// Get member activities with pagination
async function getMemberActivities(identifier, page = 1, pageSize = 20, mode = null) {
  const params = new URLSearchParams({
    page: page.toString(),
    pageSize: pageSize.toString()
  });
  
  if (mode !== null) {
    params.append('mode', mode.toString());
  }
  
  const response = await fetch(
    `http://localhost:5000/api/members/${encodeURIComponent(identifier)}/activities?${params}`
  );
  const data = await response.json();
  
  if (data.success) {
    console.log(`Total activities: ${data.data.totalCount}`);
    console.log(`Showing page ${data.data.page} of ${Math.ceil(data.data.totalCount / data.data.pageSize)}`);
    data.data.items.forEach(activity => {
      console.log(`Activity ${activity.instanceId} - Mode: ${activity.mode}, Completed: ${activity.completed}`);
    });
  }
}

// Get clan roster with sorting
async function getClanRoster(groupId, sortBy = 'lastPlayed') {
  const response = await fetch(
    `http://localhost:5000/api/clans/${groupId}/roster?sortBy=${sortBy}`
  );
  const data = await response.json();
  
  if (data.success) {
    data.data.items.forEach(member => {
      console.log(`${member.displayNameGlobal} - Rank: ${member.guardianRankCurrent}`);
    });
  }
}
```

### Python Examples

```python
import requests
from datetime import datetime, timedelta

# Base URL
BASE_URL = "http://localhost:5000"

def get_member(identifier):
    """Get member information by Bungie name or membership ID"""
    response = requests.get(f"{BASE_URL}/api/members/{identifier}")
    data = response.json()
    
    if data['success']:
        member = data['data']
        print(f"Member: {member['displayNameGlobal']}")
        print(f"Guardian Rank: {member['guardianRankCurrent']}")
        print(f"Characters: {len(member['characters'])}")
        for char in member['characters']:
            print(f"  - {char['className']} (Light: {char['lightLevel']})")
    else:
        print(f"Error: {data['error']}")

def get_member_stats(identifier, days_back=30):
    """Get member statistics for the last N days"""
    end_date = datetime.now()
    start_date = end_date - timedelta(days=days_back)
    
    params = {
        'startDate': start_date.strftime('%Y-%m-%d'),
        'endDate': end_date.strftime('%Y-%m-%d')
    }
    
    response = requests.get(
        f"{BASE_URL}/api/members/{identifier}/stats",
        params=params
    )
    data = response.json()
    
    if data['success']:
        stats = data['data']
        print(f"Total Activities: {stats['totalActivities']}")
        print("Activities by Mode:")
        for mode, count in stats['activitiesByMode'].items():
            print(f"  - {mode}: {count}")
        print("\nAverage Stats:")
        for stat, value in stats['averageStats'].items():
            print(f"  - {stat}: {value:.2f}")

def get_leaderboard(stat='kills', period='weekly', limit=10):
    """Get leaderboard for a specific stat"""
    params = {
        'period': period,
        'limit': limit
    }
    
    response = requests.get(
        f"{BASE_URL}/api/leaderboards/{stat}",
        params=params
    )
    data = response.json()
    
    if data['success']:
        leaderboard = data['data']
        print(f"{stat.capitalize()} Leaderboard ({period})")
        print("=" * 50)
        for entry in leaderboard['entries']:
            print(f"{entry['rank']:3d}. {entry['displayName']:20s} [{entry['clanTag']}] - {entry['value']:,.0f}")

# Example usage
if __name__ == "__main__":
    # Get member info
    get_member("ExamplePlayer#1234")
    
    # Get member stats for last 7 days
    get_member_stats("ExamplePlayer#1234", days_back=7)
    
    # Get weekly kills leaderboard
    get_leaderboard('kills', 'weekly', 10)
```

### C# Examples

```csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

public class RasputinApiClient
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "http://localhost:5000";

    public RasputinApiClient()
    {
        _httpClient = new HttpClient();
    }

    public async Task<ApiResponse<MemberSummaryResponse>> GetMemberAsync(string identifier)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/api/members/{Uri.EscapeDataString(identifier)}");
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ApiResponse<MemberSummaryResponse>>(content);
    }

    public async Task<ApiResponse<PagedResponse<ActivitySummary>>> GetMemberActivitiesAsync(
        string identifier, 
        int page = 1, 
        int pageSize = 20,
        int? mode = null,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        var query = $"?page={page}&pageSize={pageSize}";
        
        if (mode.HasValue)
            query += $"&mode={mode}";
        
        if (startDate.HasValue)
            query += $"&startDate={startDate.Value:yyyy-MM-dd}";
            
        if (endDate.HasValue)
            query += $"&endDate={endDate.Value:yyyy-MM-dd}";

        var response = await _httpClient.GetAsync(
            $"{BaseUrl}/api/members/{Uri.EscapeDataString(identifier)}/activities{query}"
        );
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ApiResponse<PagedResponse<ActivitySummary>>>(content);
    }

    public async Task PrintMemberSummary(string identifier)
    {
        var result = await GetMemberAsync(identifier);
        
        if (result.Success)
        {
            var member = result.Data;
            Console.WriteLine($"Member: {member.DisplayNameGlobal}");
            Console.WriteLine($"Platform: {member.Platform}");
            Console.WriteLine($"Guardian Rank: {member.GuardianRankCurrent}/{member.GuardianRankLifetime}");
            Console.WriteLine($"Last Played: {member.LastPlayedAt}");
            
            Console.WriteLine("\nCharacters:");
            foreach (var character in member.Characters)
            {
                Console.WriteLine($"  - {character.ClassName} (Light: {character.LightLevel})");
                Console.WriteLine($"    Play Time: {character.MinutesPlayedSession} min (session), {character.MinutesPlayedLifetime} min (lifetime)");
            }
            
            Console.WriteLine("\nClans:");
            foreach (var clan in member.Clans)
            {
                Console.WriteLine($"  - [{clan.ClanTag}] {clan.ClanName} (Role: {clan.GroupRole})");
            }
        }
        else
        {
            Console.WriteLine($"Error: {result.Error}");
        }
    }
}

// Response models (simplified)
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string Error { get; set; }
    public DateTime Timestamp { get; set; }
}

public class MemberSummaryResponse
{
    public string MembershipId { get; set; }
    public string DisplayName { get; set; }
    public string DisplayNameGlobal { get; set; }
    public int Platform { get; set; }
    public int GuardianRankCurrent { get; set; }
    public int GuardianRankLifetime { get; set; }
    public DateTime LastPlayedAt { get; set; }
    public List<CharacterSummary> Characters { get; set; }
    public List<ClanMembershipSummary> Clans { get; set; }
}

// Usage
var client = new RasputinApiClient();
await client.PrintMemberSummary("ExamplePlayer#1234");
```

## Response Format

All API responses follow a standard format:

```json
{
  "success": true,
  "data": {
    // Response data here
  },
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

## Pagination

Endpoints that return lists support pagination with the following query parameters:

- `page` - Page number (default: 1)
- `pageSize` - Items per page (default: varies by endpoint)

Paginated responses include:

```json
{
  "items": [...],
  "page": 1,
  "pageSize": 20,
  "totalCount": 150
}
```

## Date Filtering

Many endpoints support date filtering with ISO 8601 date format:

- `startDate` - Filter results after this date (inclusive)
- `endDate` - Filter results before this date (inclusive)

Example: `?startDate=2024-01-01&endDate=2024-12-31`

## Activity Modes

The `mode` parameter in activity endpoints corresponds to Destiny 2 activity types:

- 0: None
- 2: Story
- 3: Strike
- 4: Raid
- 5: AllPvP
- 6: Patrol
- 63: Gambit
- 82: Dungeon
- 84: Trials of Osiris

See the full list in the API implementation.

## Error Handling

The API returns appropriate HTTP status codes:

- 200: Success
- 404: Resource not found
- 500: Internal server error

Always check the `success` field in the response to determine if the request was successful.