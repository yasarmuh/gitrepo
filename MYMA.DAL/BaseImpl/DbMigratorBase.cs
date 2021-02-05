using EntityFramework.Firebird;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;

namespace MYMA.Students.DAL.BaseImpl
{
    public class DbMigratorBase<TContext, TMigrationsConfiguration> : DbMigrator
   where TContext : CommonDbContextBase
   where TMigrationsConfiguration : DataMigrationConfigurationBase<TContext>, new()
    {

        public DbMigratorBase(DbConnection connection)
       : base(new TMigrationsConfiguration()
       {
           TargetDatabase = new DbConnectionInfo(connection.ConnectionString, FbProviderServices.ProviderInvariantName)
       })
        {
        }
    }
}
