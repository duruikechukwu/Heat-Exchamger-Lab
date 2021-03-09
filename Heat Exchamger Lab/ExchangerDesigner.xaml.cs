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
    /// Interaction logic for ExchangerDesigner.xaml
    /// </summary>
    public partial class ExchangerDesigner : Window
    {
        UnitSystem unitsystem;
        HeatExchanger HE;
        bool ready = false;
        public ExchangerDesigner()
        {
            InitializeComponent();
            this.Loaded += ExchangerDesigner_Loaded;
        }

        void ExchangerDesigner_Loaded(object sender, RoutedEventArgs e)
        {

            LoadUnits();
        }

        private void LoadUnits()
        {
            unitsystem = BinarySerialization.ReadFromBinaryFile<UnitSystem>(UnitSystem.UnitSetupPath);
            cmbUnitofB.SelectedIndex = unitsystem.UnitofBaffleSpace;
            cmbUnitofBin.SelectedIndex = unitsystem.UnitofBaffleSpace;
            cmbUnitofBout.SelectedIndex = unitsystem.UnitofBaffleSpace;
            cmbUnitofDi.SelectedIndex = unitsystem.UnitofDiameter;
            cmbUnitofDo.SelectedIndex = unitsystem.UnitofDiameter;
            cmbUnitofDs.SelectedIndex = unitsystem.UnitofDiameter;
            cmbUnitofL.SelectedIndex = unitsystem.UnitofLenght;
            cmbUnitofPT.SelectedIndex = unitsystem.UnitofPitch;
        }

        private void btnSave_Click_1(object sender, RoutedEventArgs e)
        {
            CheckAndBuild();
            if (ready)
            {
                SaveExchanger();
            }
        }

        private void SaveExchanger()
        {

          List<HeatExchanger> exchangers = BinarySerialization.ReadFromBinaryFile<List<HeatExchanger>>(Store.exlocation); 
            
            double count = 0;
            foreach (HeatExchanger item in exchangers)
            {
                if (HE.Identifier==item.Identifier)
                {
                    count++;
                }
            }
            if (count==0)
            {
                exchangers.Add(HE);
                BinarySerialization.WriteToBinaryFile<List<HeatExchanger>>(Store.exlocation, exchangers);
                MessageBox.Show("Saved");
            }
            else
            {
                MessageBox.Show("Exchanger with similar Identifier already exists","CANNOT COMPLETE");
            }
        }

        private void CheckAndBuild()
        {
            DoubleCollection aa = new DoubleCollection();
            foreach (TextBox textbox in stkValues.Children)
            {
                try
                {
                    double a = double.Parse(textbox.Text);
                    aa.Add(a);
                }
                catch (Exception)
                {

                }
            }
            if (aa.Count == 13)
            {
                if (!string.IsNullOrWhiteSpace(txtIdentifier.Text))
                {
                    BuildExchanger();
                    ready = true;
                }
                else
                {
                    MessageBox.Show("Heat exchanger must have a unique identidier", "CANNOT COMPLETE");
                }
            }
            else
            {
                ready = false;
                int b = 13 - aa.Count;
                MessageBox.Show(string.Format("{0} Parameter(s) not properly defined", b), "CANNOT COMPLETE TASK");
            }
        }

        private void BuildExchanger()
        {
                HE = new HeatExchanger() 
            {
                Identifier=txtIdentifier.Text,

                Ds=UnitConverter.ToStandardLength(double.Parse(txtDs.Text),cmbUnitofDs.SelectedIndex),
                Do=UnitConverter.ToStandardLength(double.Parse(txtDo.Text),cmbUnitofDo.SelectedIndex),
                Di=UnitConverter.ToStandardLength(double.Parse(txtDi.Text),cmbUnitofDi.SelectedIndex),
                L=UnitConverter.ToStandardLength(double.Parse(txtL.Text),cmbUnitofL.SelectedIndex),
                Pt=UnitConverter.ToStandardLength(double.Parse(txtPt.Text),cmbUnitofPT.SelectedIndex),
                B=UnitConverter.ToStandardLength(double.Parse(txtB.Text),cmbUnitofB.SelectedIndex),
                Bin=UnitConverter.ToStandardLength(double.Parse(txtBin.Text),cmbUnitofBin.SelectedIndex),
                Bout=UnitConverter.ToStandardLength(double.Parse(txtBout.Text),cmbUnitofBout.SelectedIndex),
                nt=double.Parse(txtNt.Text),
                np=double.Parse(txtnp.Text),
                Bc=double.Parse(txtBc.Text),
                nb=double.Parse(txtnb.Text),              
            };
            if (cmbTubeLayout.SelectedIndex==0)
            {
                HE.TubeLayout = 30;
                HE.TubelayoutCaption="Triangular";
            }
            else if (cmbTubeLayout.SelectedIndex==1)
            {
                HE.TubeLayout = 90;
                HE.TubelayoutCaption = "Square";
            }
            else if (cmbTubeLayout.SelectedIndex == 2)
            {
                HE.TubeLayout = 45;
                HE.TubelayoutCaption = "Rotated Square";
            }
            if (string.IsNullOrWhiteSpace(txtNss.Text))
            {
                HE.rss = double.Parse(txtrss.Text);
                HE.Nss = -1;
            }
            else
            {
                HE.Nss = double.Parse(txtNss.Text);
                HE.rss = -1;
            }
            HE.TEMAType = cmbTemaType.SelectedIndex;
            HE.TEMATypeCaption = cmbTemaType.SelectionBoxItem.ToString();
            HE.Material = cmbMaterial.SelectionBoxItem.ToString();
            HE.IncludeSafetyFactor = (bool)chkIncludeSafety.IsChecked;
            HE.calculate();
            
        }

        private void txtNss_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            txtrss.Clear();
        }

        private void txtrss_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            txtNss.Clear();
        }

        private void btnView_Click_1(object sender, RoutedEventArgs e)
        {
            CheckAndBuild();
            if (ready)
            {
                     ViewExchanger();
            }
        }

        private void ViewExchanger()
        {
            ExchangerViewer viewer = new ExchangerViewer();
            viewer.ViewExchanger(this.HE);
            viewer.Owner = this;
            viewer.ShowDialog();

        }

        
     

        
    }
}
