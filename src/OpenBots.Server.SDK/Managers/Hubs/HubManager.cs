using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace OpenBots.Server.SDK.Managers.Hubs
{
    public class HubManager
    {
        public readonly HubConnection _connection;
        public event Action<string, string> JobCheckpointNotificationReceived;

        public HubManager(string serverUrl)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl($"{serverUrl}/notification")
                .WithAutomaticReconnect()
                .Build();
            _connection.On<string, string>("NewCheckpointRegistered", (checkpointName, checkpointId) => JobCheckpointNotificationReceived?.Invoke(checkpointName, checkpointId));
        }

        public void Connect()
        {
            _connection.StartAsync();
        }

        public void Disconnect()
        {
            _connection.StopAsync();
        }
    }
}
