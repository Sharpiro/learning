using System.Data.Entity;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Runtime.Remoting.Messaging;
using System;

namespace InterviewPrep.Concepts.ChangeTracking
{
    public class CodeConfiguration : DbConfiguration
    {
        private static readonly string _providerInvariantName = ConfigurationManager.AppSettings["ProviderInvariantName"];
        private static readonly string _baseConnectionString = ConfigurationManager.AppSettings["BaseConnectionString"];

        public CodeConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => SuspendExecutionStrategy
              ? (IDbExecutionStrategy)new DefaultExecutionStrategy()
              : new SqlAzureExecutionStrategy());

            //switch (_providerInvariantName)
            //{
            //    case "System.Data.SqlClient":
            SetDefaultConnectionFactory(new FileConnectionFactory(_baseConnectionString));
            //        break;

            //    default:
            //        throw new InvalidOperationException("Unknown ProviderInvariantName specified in App.config: " + _providerInvariantName);
            //}

            AddDependencyResolver(MutableResolver.Instance);
        }

        public static bool SuspendExecutionStrategy
        {
            get
            {
                return (bool?)CallContext.LogicalGetData("SuspendExecutionStrategy") ?? false;
            }
            set
            {
                CallContext.LogicalSetData("SuspendExecutionStrategy", value);
            }
        }
    }
}
