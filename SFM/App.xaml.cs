using System.Windows;
using SFM.View;
using SFM.ViewModel.Helper;
using SFM.ViewModel.Interfaces;
using SFM.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace SFM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            services.AddSingleton<IFileHelper, FileHelper>();
            services.AddSingleton<MainViewModel>();
            var provider = services.BuildServiceProvider();

            var mainWindow = new MainWindow(provider.GetRequiredService<MainViewModel>());
            mainWindow.Show();

        }
    }
}
