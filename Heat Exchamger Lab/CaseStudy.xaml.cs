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
    /// Interaction logic for CaseStudy.xaml
    /// </summary>
    public partial class CaseStudy : Window
    {
        CaseStudySystem study;
        string path;
        public CaseStudy()
        {
            InitializeComponent();
            path = AppDomain.CurrentDomain.BaseDirectory + "SETUPFILE002";
            this.Loaded += CaseStudy_Loaded;
         
        }

        void CaseStudy_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                study = BinarySerialization.ReadFromBinaryFile<CaseStudySystem>(path);
                CmbDependentVar.SelectedIndex = study.dependentPlotVar;
                CmbIndependentVar.SelectedIndex = study.IndependependentPlotVardent;
            }
            catch (Exception)
            {
                   study = new CaseStudySystem();
                   Assign();
            }
            study = new CaseStudySystem();
           
        }

        void Assign()
        {
            study.setuppath = path;
            study.dependentPlotVar = CmbDependentVar.SelectedIndex;
            study.IndependependentPlotVardent = CmbIndependentVar.SelectedIndex;
            BinarySerialization.WriteToBinaryFile<CaseStudySystem>(study.setuppath, study);
        }


   
        private void btnok_Click_1(object sender, RoutedEventArgs e)
        {
            Assign();
            this.Close();
        }

    }
}
