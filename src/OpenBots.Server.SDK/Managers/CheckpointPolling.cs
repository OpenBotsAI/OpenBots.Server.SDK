using OpenBots.Server.SDK.Managers.Hubs;

namespace OpenBots.Server.SDK.Managers
{
    public class CheckpointPolling
    {
        public bool checkpointReceived = false;
        public string checkpointId;
        private HubManager _hub;
        private string _checkpointName;
        private string _serverUrl;

        public CheckpointPolling(string checkpointName, string serverUrl)
        {
            _checkpointName = checkpointName;
            _serverUrl = serverUrl;
        }

        public void StartCheckpointPolling()
        {
            StartHubManager();
        }

        public void StopCheckpointPolling()
        {
            StopHubManager();
        }


        private void StartHubManager()
        {
            if (_hub == null)
                _hub = new HubManager(_serverUrl);

            _hub.JobCheckpointNotificationReceived += OnCheckpointReceived;
            _hub.Connect();
        }

        private void StopHubManager()
        {
            if (_hub != null)
            {
                _hub.Disconnect();
                _hub.JobCheckpointNotificationReceived -= OnCheckpointReceived;
            }
        }

        private void OnCheckpointReceived(string checkpointName, string checkpointId)
        {
            if (_checkpointName == checkpointName)
            {
                checkpointReceived = true;
                this.checkpointId = checkpointId;
            }
        }
    }

}
