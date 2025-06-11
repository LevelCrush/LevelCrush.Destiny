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

## Contributing

We welcome all contributions. Feel free to open a [Github Discussion Post](./discussions) or raise any [Github Issues](./issues).


