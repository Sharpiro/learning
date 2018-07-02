using Autofac;

namespace InterviewPrep.ServicePattern.PresentationLayer
{
    public class FakeService
    {
        private readonly ILifetimeScope _lifetimeScope;

        public FakeService(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public int Poll()
        {
            using (var processScope = _lifetimeScope.BeginLifetimeScope())
            {
                var worker = processScope.Resolve<FakeWorker>();
                return worker.Work();
            }
        }
    }
}
