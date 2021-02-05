using EntityFramework.Firebird;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data.Common;
using System.Data.Entity;

namespace MYMA.Students.DAL
{
    public abstract class CommonDbContextBase : DbContext
    {
        public CommonDbContextBase() : base(connection, true)
        {
            DefaultInit();
        }
        private static FbConnection connection
        {
            get
            {
                FbConnectionStringBuilder b = new FbConnectionStringBuilder
                {
                    ServerType = FbServerType.Default,
                    UserID = "sysdba",
                    Password = "masterkey",
                    DataSource = "localhost",
                    Database = @"C:\Workspace\DataStores\AlRehmanSchool.FDB",
                    ClientLibrary = "fbclient.dll",
                    Dialect = 3

                };

                return new FbConnection(b.ToString());
            }
        }
        private void DefaultInit()
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.ValidateOnSaveEnabled = true;
        }

        public class FirebirdConfiguration : DbConfiguration
        {
            public FirebirdConfiguration()
            {
                SetDefaultConnectionFactory(new FbConnectionFactory());
                SetProviderFactory(FbProviderServices.ProviderInvariantName, FirebirdClientFactory.Instance);
                SetProviderServices(FbProviderServices.ProviderInvariantName, FbProviderServices.Instance);
                DbProviderFactories.RegisterFactory(FbProviderServices.ProviderInvariantName, FirebirdClientFactory.Instance);

            }
        }
    }
}
