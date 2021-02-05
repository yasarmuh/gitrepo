using MYMA.MigratorApp.Plugin;
using MYMA.MigratorApp.VMs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MYMA.MigratorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);           

            //DbConfiguration.SetConfiguration(new CommonBase.CommonDbContextBase.FirebirdConfiguration());

            MigratorPluginManager pluginManager = new MigratorPluginManager();

            if (pluginManager.DataMigrationServices != null)
                new MainWindow()
                {
                    DataContext = new MainWindowVM(pluginManager)
                }.Show();
        }
    }
}
