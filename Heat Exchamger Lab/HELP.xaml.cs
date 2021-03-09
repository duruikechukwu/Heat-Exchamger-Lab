using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Diagnostics;

namespace Heat_Exchamger_Lab
{
    /// <summary>
    /// Interaction logic for HELP.xaml
    /// </summary>
    public partial class HELP : Window
    {
        public HELP()
        {
            InitializeComponent();
            richTextBox.AddHandler(Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(RequestNavigateHandler));
        }
        private void RequestNavigateHandler(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.ToString());
        }
    }
}
