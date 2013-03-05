# SignalR server events visualization demo
## Finnish MS Techdays 2013

This project demonstrates what it takes to send events from server to all listening clients, including others than web browsers. 

Technologies used:

- [SignalR](http://signalr.net) [hubs](https://github.com/SignalR/SignalR/wiki/Hubs) for easy client server interaction
- [SignalR .NET client library](https://github.com/SignalR/SignalR/wiki/SignalR-Client-Hubs)
- HTML SVG with [D3.js](http://d3js.org/)

To run the demo, open project in Visual Studio, run it, and open multiple browsers to point to the [local site http://localhost:45888/](http://localhost:45888/). You can choose to run either only web site, or web site and command prompt client. 

-----
### What should I look for?
Meaningful code can be found from

- Visualization/VisualizationHub.cs: implements the server hub
- Visualization/VisualizationHub.js: client side connection to the hub
- Visualization/CounterChart.js: D3.js usage
- VisualizationConsole/Program.cs: demonstrates how generic .NET client can be used to connect the SignalR hub

