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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Heat_Exchamger_Lab
{
    /// <summary>
    /// Interaction logic for FluidData.xaml
    /// </summary>
    public partial class FluidData : UserControl
    {
        public FluidData()
        {
            InitializeComponent();
        }

        public bool CheckandValidate()
        {
            bool result = false;
            try
            {
                double a = double.Parse(txtCp.Text);
                double b = double.Parse(txtK.Text);
                double c = double.Parse(txtMiu.Text);
                double d = double.Parse(txtRho.Text);
                double e = double.Parse(txtTemp.Text);
                result = true;
            }
            catch (Exception)
            {
                
               
            }
            return result;
        }
    }
}
