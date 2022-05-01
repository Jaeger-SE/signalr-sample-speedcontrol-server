# SignalR for Dotnet
## Setup
- Requires Dotnet 5.0
## Scope
- No Authentication and Authorzation (If needed: it simply requires the regular integration for auth/auth of aspnet core)
- No security at hub level (Here a Cop can register himself as whatever radar he wants)
## Structure of the projects
### Contracts
Contains all the interfaces and DTO used by the HUB's and its clients.
### Hub
Contains an *ASPNET Core application* with all the required settings to make a signalr app working. Contains 2 Hubs used by the example clients in dotnet and Angular.
### Clients
Contains 3 *Console Applications* that represents 3 different kinds of clients:
- A Pusher: ... .Clients.Radar that only sends message to the hub.
- A Consumer: ... .Clients.Cops that only register itself as a client for a specific set of messages. It also sends a registration message that allows it to receive the correct message.
- A Pusher-and-Consumer: ... .Clients.PoliceOffice that register itself as a consumer for radar messages and sends message to the notifications' hub.
### Helpers
Contains the wrapper-library around SignalR-client in dotnet for strongly-typed interactions with the Hub.
Use the Zwedze.Framework.SignalR.Client.RealtimeClientFactory to create a new IRealtimeClient.
## Variation
### Redis backplane
A branch feature/redis can be found on this repo. It contains all the required setup for a redis backplane in a signalr app.
To run a redis locally using Docker:
``` cmd
docker run --name radar-realtime-redis -p 6379:6379 -d redis
```
## Feedback & usage
If anything is not clear or missing, feel free to contact me  
📧 esteban.goffaux@satellit.be  
📜 Or directly on MS Teams

❤️ Feel also free to take whatever part of code you want for your personnal or professional project.