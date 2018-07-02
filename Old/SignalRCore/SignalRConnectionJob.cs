using System;
using Microsoft.AspNet.SignalR.Client;
using Quartz;
using Topshelf.Logging;

namespace SignalRCore
{
    public class SignalRConnectionJob : IJob
    {
        private readonly HubContainer _hubContainer;
        private readonly LogWriter _logger;

        public SignalRConnectionJob(LogWriter logger, HubContainer hubContainer)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));
            if (hubContainer == null)
                throw new ArgumentNullException(nameof(hubContainer));

            _logger = logger;
            _hubContainer = hubContainer;
        }

        public async void Execute(IJobExecutionContext context)
        {
            var notConnected = _hubContainer.State == ConnectionState.Disconnected
                || _hubContainer.State == ConnectionState.Reconnecting;
            if (!notConnected) return;
            await _hubContainer.Restart();
            _logger.Warn("Attempting to re-connect to SignalR...");
        }
    }
}