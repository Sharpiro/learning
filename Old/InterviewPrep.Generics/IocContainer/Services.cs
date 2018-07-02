using InterviewPrep.Generics.Entities;

namespace InterviewPrep.Generics.IocContainer
{
    public interface ILogger
    {

    }

    public class SqlLogger : ILogger
    {

    }

    public interface IRepository<T>
    {

    }

    public class SqlRepository<T> : IRepository<T>
    {
        public SqlRepository(ILogger logger)
        {

        }
    }

    public class InvoiceService
    {
        public InvoiceService(IRepository<BetterEmployee> repository, ILogger logger)
        {

        }
    }
}
