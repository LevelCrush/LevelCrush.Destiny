# Rasputin - Destiny 2 Data Tracking System

A comprehensive Destiny 2 data tracking and analytics platform built for the Level Crush gaming community. This C# microservices application integrates with Bungie's API to collect, process, and analyze player statistics, clan information, and game activities.

![Discord](<https://img.shields.io/discord/303862208419594240?logo=discord&logoColor=rgb(255%2C255%2C255)&link=https%3A%2F%2Fdiscord.gg%2Flevelcrush>)

[Join us on Discord](https://discord.gg/levelcrush)

## Overview

Rasputin is a distributed system that continuously monitors and aggregates Destiny 2 game data for players and clans. Built using modern C# and microservices architecture, it provides real-time tracking of player activities, achievements, and clan-wide statistics.

### Key Features

- **Player Statistics Tracking**: Monitor individual player activities, time played, and triumph completion
- **Clan Management**: Track multiple clan rosters, member changes, and aggregate statistics
- **Activity Monitoring**: Record and analyze raids, strikes, PvP matches, and other game activities
- **Real-time Data Collection**: Background workers continuously update player and clan information
- **Performance Analytics**: Complex queries for leaderboards and performance metrics
- **Manifest Synchronization**: Automatic updates of game definitions from Bungie

## Architecture

The project follows a microservices architecture with the following components:

### Core Components

1. **lib-destiny**: Custom C# wrapper for Bungie's Destiny 2 API
   - Handles authentication and API requests
   - Provides strongly-typed models for all Destiny 2 data
   - Includes endpoints for players, clans, activities, and manifests

2. **Rasputin-Server**: ASP.NET Core API server
   - RESTful endpoints for client applications
   - Complex SQL queries for data aggregation
   - Example: `/member/{bungieName}/titles` for player achievements

3. **Rasputin-Database**: Data persistence layer
   - Entity Framework Core with MySQL
   - Stores player profiles, activities, clan data, and game manifests
   - Includes migrations for schema management

4. **Rasputin-MessageQueue**: RabbitMQ integration
   - Asynchronous task processing
   - Queue types: Member, Clan, DBSync, Actions, Instance
   - Enables scalable background processing

5. **Rasputin-MessageQueue-Consumer**: Background workers
   - Processes queued tasks
   - Crawls member data, updates clans, syncs manifests
   - Handles rate limiting and error recovery

6. **Rasputin-Redis**: Caching layer
   - Improves performance for frequent API calls
   - Reduces load on Bungie's API

7. **Ghost**: Command-line utility
   - Administrative tasks and data management
   - Currently includes triumph icon downloading

8. **Rasputin-Jobs**: Job scheduler
   - Triggers specific actions via message queue
   - Enables scheduled data updates

## Technology Stack

- **Language**: C# (.NET 8)
- **Database**: MySQL with Entity Framework Core
- **Message Queue**: RabbitMQ
- **Caching**: Redis
- **API Framework**: ASP.NET Core Minimal APIs
- **Testing**: xUnit
- **Deployment**: Docker/Nixpacks compatible

## Getting Started

### Prerequisites

- .NET 8 SDK
- MySQL Server
- RabbitMQ Server
- Redis Server
- Bungie API Key (obtain from [Bungie.net](https://www.bungie.net/en/Application))

### Configuration

Each component has its own `appsettings.json` file for configuration:

- Database connection strings
- RabbitMQ connection settings
- Redis connection settings
- Bungie API credentials
- Service-specific settings

### Building

```bash
dotnet build destiny.sln
```

### Running Tests

```bash
dotnet test
```

### Running the API Server

```bash
cd Rasputin-Server
dotnet run
```

The API will be available at `http://localhost:5000`

## API Examples

### Member Endpoints

```bash
# Get member by Bungie name
curl http://localhost:5000/api/members/PlayerName%231234

# Get member by membership ID
curl http://localhost:5000/api/members/4611686018467431790

# Get member activities with filters
curl "http://localhost:5000/api/members/PlayerName%231234/activities?page=1&pageSize=10&mode=4"

# Get member statistics
curl "http://localhost:5000/api/members/PlayerName%231234/stats?startDate=2024-01-01"

# Get member titles
curl http://localhost:5000/api/members/PlayerName%231234/titles

# Get member leaderboard positions
curl http://localhost:5000/api/members/PlayerName%231234/leaderboards
```

### Clan Endpoints

```bash
# Get clan information
curl http://localhost:5000/api/clans/4107840

# Get clan roster
curl "http://localhost:5000/api/clans/4107840/roster?sortBy=lastPlayed"

# Get clan statistics
curl http://localhost:5000/api/clans/4107840/stats

# List all network clans
curl http://localhost:5000/api/clans?isNetwork=true
```

### Leaderboard Endpoints

```bash
# Get kill leaderboard
curl "http://localhost:5000/api/leaderboards/kills?period=weekly&limit=50"

# Get efficiency leaderboard for all time
curl http://localhost:5000/api/leaderboards/efficiency
```

For detailed API documentation, see [CLAUDE.md](CLAUDE.md). For comprehensive usage examples in multiple programming languages, see [API_EXAMPLES.md](API_EXAMPLES.md).

## Project Goals

1. **Provide Value**: Create tools that enhance the Destiny 2 experience for the Level Crush community
2. **Learn and Experiment**: Use modern technologies and patterns to expand technical knowledge
3. **Iterate Quickly**: Ship working features and improve based on user feedback
4. **Open Development**: Welcome community contributions and feedback

## Contributing

We welcome all contributions! Feel free to:
- Open a [Github Issue](https://github.com/levelcrush/destiny/issues) for bugs or feature requests
- Submit Pull Requests with improvements
- Join our [Discord](https://discord.gg/levelcrush) to discuss ideas

## License

This project is part of the Level Crush community initiatives. Please see LICENSE file for details.

## Acknowledgments

- The Level Crush gaming community for continuous feedback and support
- Bungie for providing the Destiny 2 API
- All contributors who have helped improve this project


