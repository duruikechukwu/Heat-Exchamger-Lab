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
    /// Interaction logic for FluidTank.xaml
    /// </summary>
    public partial class FluidTank : UserControl
    {
            
        private TankTypes _Type;

        public TankTypes Type
        {
            get { return _Type; }
            set 
            { 
                _Type = value;
            }
        }

        public enum TankTypes {none, hot, cold };

        public FluidTank()
        {
            InitializeComponent();      
        }

      

        public void On() 
        {
            if (_Type==TankTypes.hot)
            {
                canmeter.Background = Brushes.Red;
            }
            else if (_Type==TankTypes.cold)
            {
                canmeter.Background = Brushes.Green;
            }
        }
        public void Off()
        {
            if (_Type == TankTypes.hot)
            {
                canmeter.Background = Brushes.DarkRed;
            }
            else if (_Type == TankTypes.cold)
            {
                canmeter.Background = Brushes.DarkGreen;
            }
        }

     

    }
}
