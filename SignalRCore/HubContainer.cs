using Microsoft.AspNet.SignalR.Client;
using Quartz;
using Quartz.Impl.Matchers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Topshelf.Logging;
using static SignalRCore.Commands;

namespace SignalRCore
{
    public class HubContainer
    {
        private readonly IScheduler _scheduler;
        private readonly dynamic _config;
        private HubConnection _hubConnection;
        private readonly string _hubUrl;
        private readonly LogWriter _logger;

        public ConnectionState State => _hubConnection.State;
        public Action<Exception> OnError { set { _hubConnection.Error += value; } }
        public IJobDetail PollingUpdateJob { get; set; }

        public HubContainer(dynamic config, string hubUrl,
            IScheduler scheduler, LogWriter logger)
        {
            if (scheduler == null)
                throw new ArgumentNullException(nameof(scheduler));
            if (config == null)
                throw new ArgumentNullException(nameof(config));
            if (string.IsNullOrEmpty(hubUrl))
                throw new ArgumentNullException(nameof(hubUrl));
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            _scheduler = scheduler;
            _config = config;
            _hubUrl = hubUrl;
            _logger = logger;
        }

        public void Stop()
        {
            _logger.Info("Stopping SignalR...");
            _hubConnection?.Stop();
        }

        public async Task Start()
        {
            try
            {
                _logger.Info("Starting SignalR...");
                _hubConnection?.Stop();
                _hubConnection = new HubConnection(_hubUrl);
                _hubConnection.Reconnecting += async () => await Restart();

                var hubProxy = _hubConnection.CreateHubProxy(Hub);
                RegisterOnRunJob(hubProxy);
                RegisterOnUpdatePolling(hubProxy);

                await _hubConnection.Start();
                await hubProxy.Invoke(HubCommands.JoinGroup, _config.AppGuid.ToString().ToUpper());
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred trying to setup SignalR connection", ex);
            }
        }

        public async Task Restart()
        {
            _logger.Warn("Reconnecting to SignalR...");
            await Start();
        }

        private void RegisterOnRunJob(IHubProxy hubProxy)
        {
            if (hubProxy == null)
                throw new ArgumentNullException(nameof(hubProxy));

            hubProxy.On(HubCommands.RunJob, () =>
            {
                _scheduler.GetJobKeys(GroupMatcher<JobKey>
                    .GroupEquals("Default")).ToList()
                    .ForEach(jk => _scheduler.TriggerJob(jk));
            });
        }

        private void RegisterOnUpdatePolling(IHubProxy hubProxy)
        {
            if (PollingUpdateJob == null) return;
            if (hubProxy == null)
                throw new ArgumentNullException(nameof(hubProxy));

            _scheduler.AddJob(PollingUpdateJob, true);
            hubProxy.On(HubCommands.UpdatePolling, () =>
            {
                _scheduler.TriggerJob(PollingUpdateJob.Key);
            });
        }
    }
}