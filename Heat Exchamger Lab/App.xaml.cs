using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Heat_Exchamger_Lab
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private async void Application_Startup_1(object sender, StartupEventArgs e)
        {
            try
            {
                Splash splash = new Splash();
                splash.Show();
                await Task.Delay(1000);
                MainWindow window = new MainWindow();
                if (e.Args.Length == 1)
                {
                    window.OpenFile(e.Args[0]);
                }
                window.Show();
                splash.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
