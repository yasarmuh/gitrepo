using EntityFramework.Firebird;
using System.Data.Common;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.History;

namespace MYMA.Students.DAL.BaseImpl
{
    public partial class DataMigrationConfigurationBase<TDataContext> :
        DbMigrationsConfiguration<TDataContext>
        where TDataContext : CommonDbContextBase
    {
        public DataMigrationConfigurationBase()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            SetHistoryContextFactory(
                FbProviderServices.ProviderInvariantName,
                (connection, defaultSchema) => new MigrationsDataContext(connection)
                );
            SetSqlGenerator(
                FbProviderServices.ProviderInvariantName,
                new FbMigrationSqlGenerator()
                );
        }

        public class MigrationsDataContext : HistoryContext
        {
            public MigrationsDataContext(DbConnection existingConnection)
                : base(new FbConnectionFactory().CreateConnection(existingConnection.ConnectionString), "dbo")
            {
            }
        }
    }
}
