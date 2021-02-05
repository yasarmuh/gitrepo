using MYMA.CommonBase.Contracts;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace MYMA.MigratorApp.Plugin
{
    public class MigratorPluginManager
    {
        public MigratorPluginManager()
        {
            InitializeDataMigrationManager();
        }

        [ImportMany]
        public IEnumerable<Lazy<IDataMigrationService, DataMigrationAttribute>> DataMigrationServices { get; set; }
        private void InitializeDataMigrationManager()
        {
            try
            {
                var executableLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var assemblies = Directory
                    .GetFiles(executableLocation, "MYMA.*.DAL.dll", SearchOption.TopDirectoryOnly)
                    .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath).ToList();


                //var conv = new ConventionBuilder();
                //conv.ForTypesDerivedFrom<IDataMigrationService>()
                //    .Export<IDataMigrationService>()
                //    .Shared();


                //var configuration = new ContainerConfiguration().WithAssemblies(assemblies, conv);
                var configuration = new ContainerConfiguration().WithAssemblies(assemblies);
                using (var container = configuration.CreateContainer())
                {
                    DataMigrationServices = container.GetExports<Lazy<IDataMigrationService, DataMigrationAttribute>>();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Migrate(DbConnection connection)
        {
            try
            {
                foreach (var dataMigrationServiceReference in DataMigrationServices.OrderBy(s => s.Metadata.DataMigrationBussinessSpace))
                    dataMigrationServiceReference?.Value.Migrate(connection);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
