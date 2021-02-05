using EntityFramework.Firebird;
using FirebirdSql.Data.FirebirdClient;
using MYMA.MigratorApp.Plugin;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MYMA.MigratorApp.VMs
{
    public class MainWindowVM : ReactiveObject
    {
        MigratorPluginManager pluginManager;
        public MainWindowVM(MigratorPluginManager pluginManager)
        {
            this.pluginManager = pluginManager;
            Migrate = ReactiveCommand.Create(ExecuteMigration);
            ExitApp = ReactiveCommand.Create(() => Application.Current.Shutdown());
        }

        private void ExecuteMigration()
        {
            DbProviderFactories.RegisterFactory(FbProviderServices.ProviderInvariantName, FirebirdClientFactory.Instance);
            IEnumerable<string> invariants = DbProviderFactories.GetProviderInvariantNames();
            DbProviderFactory factory = DbProviderFactories.GetFactory(invariants.FirstOrDefault());

            DbConnection connection = factory.CreateConnection();
            connection.ConnectionString =
                "server type=Default;user id=SYSDBA;password=masterkey;data source=localhost;initial catalog='C:\\Workspace\\DataStores\\AlRehmanSchool.FDB';client library=fbclient.dll;dialect=3";
            pluginManager.Migrate(connection);
            Done = true;
        }
        [Reactive]
        public bool Done { get; set; }
        public ICommand Migrate { get; set; }
        public ICommand ExitApp { get; set; }
    }
}
