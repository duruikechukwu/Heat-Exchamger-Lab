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

namespace Heat_Exchamger_Lab
{
    /// <summary>
    /// Interaction logic for NozzleDefiner.xaml
    /// </summary>
    public partial class NozzleDefiner : Window
    {
        public double ShellNozzleD { get; private set; }
        public double TubeNozzleD { get; private set; }
        public double heaterPower { get; private set; }
        public double heatervolume { get; private set; }
        public bool madeChange { get; private set; }
        UnitSystem unitsystem;
        public NozzleDefiner()
        {
            InitializeComponent();
            madeChange = false;
            LoadUnit();
        }

        private void LoadUnit()
        {
            unitsystem = BinarySerialization.ReadFromBinaryFile<UnitSystem>(UnitSystem.UnitSetupPath);
            cmbshellNozzle.SelectedIndex = unitsystem.UnitofDiameter;
            cmbstubeNozzle.SelectedIndex = unitsystem.UnitofDiameter;
            cmbHeaterpowerunit.SelectedIndex = unitsystem.UnitofHeatTransfer;
            cmbHeaterVolume.SelectedIndex = unitsystem.UnitofVolume;
        }

        private void btnOk_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                 double a = double.Parse(txtShellNozzle.Text);
                 double b = double.Parse(txtTubeNozzle.Text);
                 double c = double.Parse(txtHeaterPower.Text);
                 double d = double.Parse(txthHeaterVolume.Text);
                 ShellNozzleD = UnitConverter.ToStandardLength(a, cmbshellNozzle.SelectedIndex);
                 TubeNozzleD = UnitConverter.ToStandardLength(b, cmbstubeNozzle.SelectedIndex);
                 heaterPower = UnitConverter.ToStandardEnergyFlow(c, cmbHeaterpowerunit.SelectedIndex);
                 heatervolume = UnitConverter.ToStandsrdVolume(d, cmbHeaterVolume.SelectedIndex);
                 madeChange = true;
                 this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("incomplete and/or incorrect input", "CANNOT COMPLETE TASK");
             
            }
        }
        
    }
}
