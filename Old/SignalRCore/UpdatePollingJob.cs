using System;
using System.Linq;
using Quartz;
using Quartz.Impl.Matchers;
using Topshelf.Logging;

namespace SignalRCore
{
    public class UpdatePollingJob : IJob
    {
        private readonly IScheduler _scheduler;
        private readonly LogWriter _logger;
        private readonly dynamic _configService;

        public UpdatePollingJob(IScheduler scheduler, LogWriter logger,
            dynamic configurationService)
        {
            if (scheduler == null)
                throw new ArgumentNullException(nameof(scheduler));
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));
            if (configurationService == null)
                throw new ArgumentNullException(nameof(configurationService));

            _scheduler = scheduler;
            _logger = logger;
            _configService = configurationService;

        }

        public void Execute(IJobExecutionContext context)
        {
            var configuration = _configService.GetConfiguration();
            var pollingTimeSpan = TimeSpan.FromMinutes(configuration.PollingIntervalMinutes).Duration();

            var jobKeys = _scheduler.GetJobKeys(GroupMatcher<JobKey>
                .GroupEquals("Default")).ToList();

            var newTrigger = TriggerBuilder.Create()
                         .WithIdentity(Guid.NewGuid().ToString(), "Interval")
                         .WithSimpleSchedule(builder => builder.WithInterval(pollingTimeSpan).RepeatForever()).Build();

            foreach (var jobKey in jobKeys)
            {
                var jobTriggers = _scheduler.GetTriggersOfJob(jobKey);

                foreach (var trigger in jobTriggers)
                {
                    if (((ISimpleTrigger)trigger).RepeatInterval == pollingTimeSpan) continue;
                    _scheduler.RescheduleJob(trigger.Key, newTrigger);
                }
            }
            _logger.Info("running poll update job...");
        }
    }
}