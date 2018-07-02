using InterviewPrep.Concepts.ChangeTracking.Entities;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data;

namespace InterviewPrep.Concepts.ChangeTracking
{
    public class AutoContext : DbContext
    {
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Car> Cars { get; set; }

        public AutoContext()
        {
            Database.SetInitializer<AutoContext>(null);
            Database.CommandTimeout = 1;
            //Database.Connection.ConnectionString = null;
        }
    }

    public class FakeDbProviderFactory : DbProviderFactory
    {
    }

    public class FakeConnection : DbConnection
    {
        protected override DbProviderFactory DbProviderFactory
        {
            get
            {
                return new FakeDbProviderFactory();
                //return base.DbProviderFactory;
            }
        }
        public override string ConnectionString
        {
            get
            {
                return string.Empty;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override string Database
        {
            get
            {
                return string.Empty;
            }
        }

        public override string DataSource
        {
            get
            {
                return string.Empty;
            }
        }

        public override string ServerVersion
        {
            get
            {
                return string.Empty;
            }
        }

        public override ConnectionState State
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Open()
        {
            throw new NotImplementedException();
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        protected override DbCommand CreateDbCommand()
        {
            throw new NotImplementedException();
        }
    }

    public class FileConnectionFactory : IDbConnectionFactory
    {
        public FileConnectionFactory(object whatever)
        {
            
        }
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            return new FakeConnection();
        }
    }

    public class FileProvider : DbProviderServices
    {
        private static FileProvider _instance;
        public static FileProvider Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FileProvider();
                return _instance;
            }
        }

        protected override DbCommandDefinition CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree)
        {
            var temp = new LocalDbConnectionFactory(null, null);
            throw new NotImplementedException();
        }

        protected override DbProviderManifest GetDbProviderManifest(string manifestToken)
        {
            throw new NotImplementedException();
        }

        protected override string GetDbProviderManifestToken(DbConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}
