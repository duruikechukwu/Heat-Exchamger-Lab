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
using System.IO;

namespace Heat_Exchamger_Lab
{
    /// <summary>
    /// Interaction logic for Units.xaml
    /// </summary>
    public partial class UnitManager : Window
    {
      
        UnitSystem unitsystem;
        public UnitManager()
        {
            InitializeComponent();
            UnitSystem.UnitSetupPath = AppDomain.CurrentDomain.BaseDirectory + "SETUPFILE001";
            this.Loaded += UnitManager_Loaded;
            this.Closed += UnitManager_Closed;
        }

        void UnitManager_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSavedUnits();
        }

        private void LoadSavedUnits()
        {
            if (File.Exists(UnitSystem.UnitSetupPath))
            {
                
                unitsystem = BinarySerialization.ReadFromBinaryFile<UnitSystem>(UnitSystem.UnitSetupPath);
                cmbUnitOfArea.SelectedIndex = unitsystem.UnitofArea;
                cmbUnitOfBaffle.SelectedIndex = unitsystem.UnitofBaffleSpace;
                cmbUnitOfClearance.SelectedIndex = unitsystem.UnitofClearance;
                cmbUnitOfCp.SelectedIndex = unitsystem.UnitofCp;
                cmbUnitOfDensity.SelectedIndex = unitsystem.UnitofDensity;
                cmbUnitOfDiameter.SelectedIndex = unitsystem.UnitofDiameter;
                cmbUnitOfK.SelectedIndex = unitsystem.UnitofK;
                cmbUnitOfLength.SelectedIndex = unitsystem.UnitofLenght;
                cmbUnitOfMassFlow.SelectedIndex = unitsystem.UnitofMassflow;
                cmbUnitOfPitch.SelectedIndex = unitsystem.UnitofPitch;
                cmbUnitOfPressure.SelectedIndex = unitsystem.UnitofPressure;
                cmbUnitOfTemp.SelectedIndex = unitsystem.UnitofTemperature;
                //cmbUnitOfTime.SelectedIndex = unitsystem.UnitofTime;
                cmbUnitOfU.SelectedIndex = unitsystem.UnitofU;
                cmbUnitOfViscosity.SelectedIndex = unitsystem.UnitofViscosity;
                cmbUnitOfVolumetricFlow.SelectedIndex = unitsystem.UnitofVolumetricFlowrate;
                cmbUnitOfVolume.SelectedIndex = unitsystem.UnitofVolume;
                cmbUnitOfHeatTransfer.SelectedIndex = unitsystem.UnitofHeatTransfer;
             
            }
        }

        void UnitManager_Closed(object sender, EventArgs e)
        {
            if (!File.Exists(UnitSystem.UnitSetupPath))
            {
               AssignAndSaveUnits();
            }
        }     

        private void ok_Click_1(object sender, RoutedEventArgs e)
        {        
          AssignAndSaveUnits();
          this.Close();
        }

        private void AssignAndSaveUnits()
        {
            unitsystem = new UnitSystem();
            unitsystem.UnitofArea = cmbUnitOfArea.SelectedIndex;
            unitsystem.UnitofBaffleSpace = cmbUnitOfBaffle.SelectedIndex;
            unitsystem.UnitofClearance = cmbUnitOfClearance.SelectedIndex;
            unitsystem.UnitofCp = cmbUnitOfCp.SelectedIndex;
            unitsystem.UnitofDensity = cmbUnitOfDensity.SelectedIndex;
            unitsystem.UnitofDiameter = cmbUnitOfDiameter.SelectedIndex;
            unitsystem.UnitofK = cmbUnitOfK.SelectedIndex;
            unitsystem.UnitofLenght = cmbUnitOfLength.SelectedIndex;
            unitsystem.UnitofMassflow = cmbUnitOfMassFlow.SelectedIndex;
            unitsystem.UnitofPitch = cmbUnitOfPitch.SelectedIndex;
            unitsystem.UnitofPressure = cmbUnitOfPressure.SelectedIndex;
            unitsystem.UnitofTemperature = cmbUnitOfTemp.SelectedIndex;
            //unitsystem.UnitofTime = cmbUnitOfTime.SelectedIndex;
            unitsystem.UnitofU = cmbUnitOfU.SelectedIndex;
            unitsystem.UnitofViscosity = cmbUnitOfViscosity.SelectedIndex;
            unitsystem.UnitofVolumetricFlowrate = cmbUnitOfVolumetricFlow.SelectedIndex;
            unitsystem.UnitofVolume = cmbUnitOfVolume.SelectedIndex;
            unitsystem.UnitofHeatTransfer = cmbUnitOfHeatTransfer.SelectedIndex;

            BinarySerialization.WriteToBinaryFile<UnitSystem>(UnitSystem.UnitSetupPath, unitsystem);
        }
        
    }
}
