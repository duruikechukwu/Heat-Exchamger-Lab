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
    /// Interaction logic for ExchangerViewer.xaml
    /// </summary>
    public partial class ExchangerViewer : Window
    {
        UnitSystem unitsystem;
        public ExchangerViewer()
        {
            InitializeComponent();
        }
        public void ViewExchanger(HeatExchanger HE)
        {
            unitsystem = BinarySerialization.ReadFromBinaryFile<UnitSystem>(UnitSystem.UnitSetupPath);

            txtA.Text = UnitConverter.ToUnitArea(HE.A, unitsystem.UnitofArea).ToString();
            txtAUnit.Text = UnitConverter.ShowUnitArea(unitsystem.UnitofArea);
            txtBc.Text = HE.Bc.ToString();


            txtB.Text = UnitConverter.ToUnitLength(HE.B, unitsystem.UnitofBaffleSpace).ToString();
            txtBUnit.Text = UnitConverter.ShowUnitLength(unitsystem.UnitofBaffleSpace);

            txtBin.Text = UnitConverter.ToUnitLength(HE.Bin, unitsystem.UnitofBaffleSpace).ToString();
            txtBinUnit.Text = UnitConverter.ShowUnitLength(unitsystem.UnitofBaffleSpace);

            txtBout.Text = UnitConverter.ToUnitLength(HE.Bout, unitsystem.UnitofBaffleSpace).ToString();
            txtBoutUnit.Text = UnitConverter.ShowUnitLength(unitsystem.UnitofBaffleSpace);

            txtDctl.Text = UnitConverter.ToUnitLength(HE.Dctl, unitsystem.UnitofDiameter).ToString();
            txtDctlUnit.Text = UnitConverter.ShowUnitLength(unitsystem.UnitofDiameter);

            txtDi.Text = UnitConverter.ToUnitLength(HE.Di, unitsystem.UnitofDiameter).ToString();
            txtDiUnit.Text = UnitConverter.ShowUnitLength(unitsystem.UnitofDiameter);

            txtDo.Text = UnitConverter.ToUnitLength(HE.Do, unitsystem.UnitofDiameter).ToString();
            txtDoUnit.Text = UnitConverter.ShowUnitLength(unitsystem.UnitofDiameter);

            txtDotl.Text = UnitConverter.ToUnitLength(HE.Dotl, unitsystem.UnitofDiameter).ToString();
            txtDotlUnit.Text = UnitConverter.ShowUnitLength(unitsystem.UnitofDiameter);


            txtDs.Text = UnitConverter.ToUnitLength(HE.Ds, unitsystem.UnitofDiameter).ToString();
            txtDsUnit.Text = UnitConverter.ShowUnitLength(unitsystem.UnitofDiameter);

            txtDs_Dotl.Text = UnitConverter.ToUnitLength(HE.Ds_Dotl, unitsystem.UnitofClearance).ToString();
            txtDs_DotlUnit.Text = UnitConverter.ShowUnitLength(unitsystem.UnitofClearance);

            txtDsb.Text = UnitConverter.ToUnitLength(HE.Dsb, unitsystem.UnitofClearance).ToString();
            txtDsbUnit.Text = UnitConverter.ShowUnitLength(unitsystem.UnitofClearance);

            txtDtb.Text = UnitConverter.ToUnitLength(HE.Dtb, unitsystem.UnitofClearance).ToString();
            txtDtbUnit.Text = UnitConverter.ShowUnitLength(unitsystem.UnitofClearance);

            txtFc.Text = HE.Fc.ToString();
            txtFw.Text = HE.Fw.ToString();



            txtL.Text = UnitConverter.ToUnitLength(HE.L, unitsystem.UnitofLenght).ToString();
            txtLUnit.Text = UnitConverter.ShowUnitLength(unitsystem.UnitofLenght);

            txtIdentifier.Text = HE.Identifier;
            txtMaterial.Text = HE.Material;
            txtNc.Text = HE.Nc.ToString();
            txtNcw.Text = HE.Ncw.ToString();


            txtNp.Text = HE.np.ToString();
            txtNss.Text = HE.Nss.ToString();
            txtrss.Text = HE.rss.ToString();
            txtNt.Text = HE.nt.ToString();
            txtNumberofBaffles.Text = HE.nb.ToString();


            txtPT.Text = UnitConverter.ToUnitLength(HE.Pt, unitsystem.UnitofPitch).ToString();
            txtPTUnit.Text = UnitConverter.ShowUnitLength(unitsystem.UnitofPitch);



            txtSb.Text = UnitConverter.ToUnitArea(HE.Sb, unitsystem.UnitofArea).ToString();
            txtSbUnit.Text = UnitConverter.ShowUnitArea(unitsystem.UnitofArea);

            txtSm.Text = UnitConverter.ToUnitArea(HE.Sm, unitsystem.UnitofArea).ToString();
            txtSmUnit.Text = UnitConverter.ShowUnitArea(unitsystem.UnitofArea);

            txtSsb.Text = UnitConverter.ToUnitArea(HE.Ssb, unitsystem.UnitofArea).ToString();
            txtSsbUnit.Text = UnitConverter.ShowUnitArea(unitsystem.UnitofArea);

            txtStb.Text = UnitConverter.ToUnitArea(HE.Stb, unitsystem.UnitofArea).ToString();
            txtStbUnit.Text = UnitConverter.ShowUnitArea(unitsystem.UnitofArea);

            txtSw.Text = UnitConverter.ToUnitArea(HE.Sw, unitsystem.UnitofArea).ToString();
            txtSwUnit.Text = UnitConverter.ShowUnitArea(unitsystem.UnitofArea);

            txtTemaType.Text = HE.TEMATypeCaption;
            txtTubeLayout.Text = HE.TubelayoutCaption;
        }
    }
}
