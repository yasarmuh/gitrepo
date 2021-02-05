using MYMA.CommonBase.Contracts;
using System.Composition;
using System.Data.Common;
using System.Reflection;


namespace MYMA.Students.DAL.BaseImpl
{
    [Export(typeof(IDataMigrationService))]
    public abstract class DataMigrationServiceBase<TContext, TMigrationsConfiguration> : IDataMigrationService
        where TContext : CommonDbContextBase
        where TMigrationsConfiguration : DataMigrationConfigurationBase<TContext>, new()
    {
        public virtual void Migrate(DbConnection connection)
        {
            var migrator = new DbMigratorBase<TContext, TMigrationsConfiguration>(connection);
            migrator.Configuration.MigrationsAssembly = Assembly.GetAssembly(GetType());
            migrator.Update();
        }
    }
}
