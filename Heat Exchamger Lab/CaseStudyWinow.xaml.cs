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
    /// Interaction logic for CaseStudyWinow.xaml
    /// </summary>
    public partial class CaseStudyWinow : Window
    {
        double i;
        PerformanceCalculator studycalculator;

        bool clicked;
        UnitSystem unitsystem;
        CaseStudySystem study;
        PointCollection studypoints;
        Lab myLab;
    
        double? end = null;
        double? start = null;
        double? step = null;
        public CaseStudyWinow(CaseStudySystem _study, Lab lab)
        {
            InitializeComponent();
            this.myLab = lab;
            this.study = _study;
            clicked = false;
     
            unitsystem = BinarySerialization.ReadFromBinaryFile<UnitSystem>(UnitSystem.UnitSetupPath);
            this.Loaded += CaseStudyWinow_Loaded;
        }

        void CaseStudyWinow_Loaded(object sender, RoutedEventArgs e)
        {
            switch (study.dependentPlotVar)
            {
                case 0:
                    txtdependent.Text = "Overall Heat Transfer Coefficient";
                    txtdepunit.Text = UnitConverter.ShowUnitHeatTransferCoefficientI(unitsystem.UnitofU);
                    break;
                case 1:
                    txtdependent.Text = "Shell-side Heat Transfer Coefficient";
                    txtdepunit.Text = UnitConverter.ShowUnitHeatTransferCoefficientI(unitsystem.UnitofU);
                    break;
                case 2:
                    txtdependent.Text = "Tube-side Heat Transfer Coefficient";
                    txtdepunit.Text = UnitConverter.ShowUnitHeatTransferCoefficientI(unitsystem.UnitofU);
                    break;
                case 3:
                    txtdependent.Text = "Shell-side Reynolds Number";
                    txtdepunit.Text = "(Dimensionless)";
                    break;
                case 4:
                    txtdependent.Text = "Tube-side Reynolds Number";
                    txtdepunit.Text = "(Dimensionless)";
                    break;
                case 5:
                    txtdependent.Text = "Shell-side Pressure Drop";
                    txtdepunit.Text = UnitConverter.ShowUnitPressure(unitsystem.UnitofPressure);
                    break;
                case 6:
                    txtdependent.Text = "Tube-side Pressure Drop";
                    txtdepunit.Text = UnitConverter.ShowUnitPressure(unitsystem.UnitofPressure);
                    break;
                case 7:
                    txtdependent.Text = "Shell-side Mass Flow Rate";
                    txtdepunit.Text = UnitConverter.ShowUnitMassFlow(unitsystem.UnitofMassflow);
                    break;
                case 8:
                    txtdependent.Text = "Tube-side Mass Flow Rate";
                    txtdepunit.Text = UnitConverter.ShowUnitMassFlow(unitsystem.UnitofMassflow);
                    break;
                case 9:
                    txtdependent.Text = "Shell-side Outlet Temperature";
                    txtdepunit.Text = UnitConverter.ShowUnitTemperature(unitsystem.UnitofTemperature);
                    break;
                case 10:
                    txtdependent.Text = "Tube-side Outlet Temperature";
                    txtdepunit.Text = UnitConverter.ShowUnitTemperature(unitsystem.UnitofTemperature);
                    break;
                case 11:
                    txtdependent.Text = "Rate of heat transfer";
                    txtdepunit.Text = UnitConverter.ShowUnitEnergyFlow(unitsystem.UnitofHeatTransfer);
                    break;
                default:
                    break;
            }
            switch (study.IndependependentPlotVardent)
            {
                case 0:
                    txtindependent.Text = "Shell-side Volumetric Flow";
                    txtindepunit.Text = UnitConverter.ShowUnitVolumeFlow(unitsystem.UnitofVolumetricFlowrate);
          
                    break;
                case 1:
                    txtindependent.Text = "Tube-side Volumetric Flow";
                    txtindepunit.Text = UnitConverter.ShowUnitVolumeFlow(unitsystem.UnitofVolumetricFlowrate);
                    break;
                case 2:
                    txtindependent.Text = "Shell-side Inlet Temperature";
                    txtindepunit.Text = UnitConverter.ShowUnitTemperature(unitsystem.UnitofTemperature);

                    break;
                case 3:
                    txtindependent.Text = "Tube-side Inlet Temperature";
                    txtindepunit.Text = UnitConverter.ShowUnitTemperature(unitsystem.UnitofTemperature);
                
                    break;
                case 4:
                    txtindependent.Text = "Shell-side Fouling Factor";
                    txtindepunit.Text = "(Dimensionless)";
                 
                    break;
                case 5:
                    txtindependent.Text = "Tube-side Fouling Factor";
                    txtindepunit.Text = "(Dimensionless)";
             
                    break;
                default:
                    break;
            }
            if (myLab.runs.Count!=0)
            {
                foreach (Run item in myLab.runs)
                {
                    cmbrun.Items.Add(item.runNo);
                }
                cmbrun.SelectedIndex = 0;
            }
        }

        private void btnGo_Click_1(object sender, RoutedEventArgs e)
        {
            if (cmbrun.SelectedItem!=null&&clicked==false)
            {
              
                try
                {
                    end = double.Parse(txtend.Text);
                    start = double.Parse(txtstart.Text);
                    step = double.Parse(txtstep.Text);             
                    if (step>0)
                    {
                        StudyCase();
                    }
                    else
                    {
                        datalog.Items.Clear();
                    }
                }
                catch (Exception)
                {
                    end = null;
                    start = null;
                    step = null;
                    MessageBox.Show("Sorry! Could Not Complete Operation Under The Specified Operating Condition. Please recheck values and try again ","INVALID OPERATION");
                }
          
            }
        }

        private async void StudyCase()
        {
          
            datalog.Items.Clear();
            if (myLab.runs.Count != 0 && cmbrun.SelectedItem != null && end != null && start != null && step != null)
            {
                 studypoints = new PointCollection();
                IEnumerable<Run> studyrun = from run in myLab.runs where run.runNo == (int)cmbrun.SelectedItem select run;
                foreach (Run item in studyrun)
                {
                     studycalculator = new PerformanceCalculator()
                    {
                        ExchangerName = item.EXchanger,
                        ShellSideFluid = item.SF,
                        TubeSideFluid = item.TF,
                        SVolFlow = item.SVF,
                        tVolFlow = item.TVF,
                        Tsi=item.Tsi,
                        Tti=item.Tti,
                        shellff=item.SFF,
                        tubeff=item.TFF
                    };
                     double count = 0;
                    for (i = (double)start; i <=(double)end; i += (double)step)
                    {
                        count++;
                        clicked = true;
                        txtstatus.Text = "Processing...       (" + count + ")";
                        txtstatus.Background = Brushes.Yellow;
                        Point studyp = new Point();
                        CheckStudyIndependentVar(ref studycalculator, ref studyp, i);
                        studycalculator.Calculate(myLab.shellNozzleDiameter, myLab.tubeNozzleDiameter, studycalculator.shellff, studycalculator.tubeff);
                        CheckStudyDependentVar(ref studycalculator, ref studyp);
                        studyp.X = Math.Round(studyp.X, 4);
                        studyp.Y = Math.Round(studyp.Y, 4);
                        studypoints.Add(studyp);
                        await Task.Delay(50);
                        clicked = false;
                 
                      
                    }
                    txtstatus.Text = "Ready";
                    txtstatus.Background = Brushes.LightGray;
                    foreach (Point p in studypoints)
                    {
                        datalog.Items.Add(p);
                    }

                }
            }
        }
    

        private void CheckStudyIndependentVar(ref PerformanceCalculator studycalcu, ref Point point, double I)
        {
            switch (study.IndependependentPlotVardent)
            {
                case 0:
          
                    studycalcu.SVolFlow = UnitConverter.ToStandardVolumeFlow(I, unitsystem.UnitofVolumetricFlowrate);
     
                    break;
                case 1:
             
                    studycalcu.tVolFlow = UnitConverter.ToStandardVolumeFlow(I, unitsystem.UnitofVolumetricFlowrate);
               
                    break;
                case 2:
       
                    studycalcu.Tsi = UnitConverter.ToStandardTemperature(I, unitsystem.UnitofTemperature);
                  
                    break;
                case 3:
      
                    studycalcu.Tti = UnitConverter.ToStandardTemperature(I, unitsystem.UnitofTemperature);
                  
                    break;
                case 4:
 
                    studycalcu.shellff = I;
             
                    break;
                case 5:
       
                    studycalcu.tubeff = I;
                   
                    break;
                default:
                    break;
            }
            point.X = I;
        
        }
        private void CheckStudyDependentVar(ref PerformanceCalculator studycalcu, ref Point point)
        {
            switch (study.dependentPlotVar)
            {
                case 0:
                    txtdependent.Text = "Overall Heat Transfer Coefficient";
                    point.Y = UnitConverter.ToUnitHeatTransferCoefficient(studycalcu.U, unitsystem.UnitofU);
                    break;
                case 1:
                    txtdependent.Text = "Shell-side Heat Transfer Coefficient";
                    point.Y = UnitConverter.ToUnitHeatTransferCoefficient(studycalcu.ho, unitsystem.UnitofU);
                    break;
                case 2:
                    txtdependent.Text = "Tube-side Heat Transfer Coefficient";
                    point.Y = UnitConverter.ToUnitHeatTransferCoefficient(studycalcu.hi, unitsystem.UnitofU);
                    break;
                case 3:
                    txtdependent.Text = "Shell-side Reynolds Number";
                    point.Y = studycalcu.REo;
                    break;
                case 4:
                    txtdependent.Text = "Tube-side Reynolds Number";
                    point.Y = studycalcu.REi;
                    break;
                case 5:
                    txtdependent.Text = "Shell-side Pressure Drop";
                    point.Y = UnitConverter.ToUnitPressure(studycalcu.DPo, unitsystem.UnitofPressure);
                    break;
                case 6:
                    txtdependent.Text = "Tube-side Pressure Drop";
                    point.Y = UnitConverter.ToUnitPressure(studycalcu.DPi, unitsystem.UnitofPressure);
                    break;
                case 7:
                    txtdependent.Text = "Shell-side Mass Flow Rate";
                    point.Y = UnitConverter.ToUnitMassFlow(studycalcu.sMassFlow, unitsystem.UnitofMassflow);
                    break;
                case 8:
                    txtdependent.Text = "Tube-side Mass Flow Rate";
                    point.Y = UnitConverter.ToUnitMassFlow(studycalcu.tMassFlow, unitsystem.UnitofMassflow);
                    break;
                case 9:
                    txtdependent.Text = "Shell-side Outlet Temperature";
                    point.Y = UnitConverter.ToUnitTemperature(studycalcu.Tso, unitsystem.UnitofTemperature);
                    break;
                case 10:
                    txtdependent.Text = "Tube-side Outlet Temperature";
                    point.Y = UnitConverter.ToUnitTemperature(studycalcu.Tto, unitsystem.UnitofTemperature);
                    break;
                case 11:
                    txtdependent.Text = "Rate of heat transfer";
                    point.Y = UnitConverter.ToUnitEnergyFlow(studycalcu.Q, unitsystem.UnitofHeatTransfer);
                    break;
                default:
                    break;
            }
        }

        private void btnView_Click_1(object sender, RoutedEventArgs e)
        {
            if (datalog.Items.Count!=0)
            {
                    LineGraph line = new LineGraph();
                    line.Plot(ref studypoints);
                    line.ShowVariables(txtindependent.Text, txtindepunit.Text, txtdependent.Text, txtdepunit.Text);
                    line.Owner = this;                  
                   line.ShowDialog();
            }
        }

        private void txtstart_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            try
            {
                    double a = double.Parse(txtend.Text);
                    double b = double.Parse(txtstart.Text);
                    double c = double.Parse(txtstep.Text);
                    double nopoints = Math.Floor((double)((a - b) / c));
                    txtstatus.Text = nopoints + 1 + " points will be generated";
             
            }
            catch (Exception)
            {

                txtstatus.Text = "Ready";
            }
        }


      
    }
}
