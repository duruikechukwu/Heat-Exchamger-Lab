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

using System.Data.SqlClient;
using System.Data.SQLite;
namespace Heat_Exchamger_Lab
{
    /// <summary>
    /// Interaction logic for Store.xaml
    /// </summary>
    public partial class Store : Window
    {
     
       
        UnitSystem unitsystem;
        List<HeatExchanger> exchangers;
        public static string exlocation = "res/ES101FILE";

        //public static string connectionstring = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\USER\Documents\Visual Studio 2012\Projects\Heat Exchamger Lab\Heat Exchamger Lab\res\FLUI101.mdf;Integrated Security=True";
        //public static SqlConnection connect = new SqlConnection(connectionstring);
        //public static SqlCommand command;
        //public static SqlDataReader reader;


        static string connectionstring = @"Data Source= ./res/FLUI101.db";
        public static SQLiteConnection connect;
        public static SQLiteCommand command;
        public static SQLiteDataReader reader;

        public Store(bool truecall)
        {
            InitializeComponent();
            loadFluid();
            this.Loaded += Store_Loaded;
            if (truecall==false)
            {
                this.Close();
            }
        }

        void Store_Loaded(object sender, RoutedEventArgs e)
        {
            unitsystem = BinarySerialization.ReadFromBinaryFile<UnitSystem>(UnitSystem.UnitSetupPath);  
            LoadExchangers();
        }
        public static void CreateConnection()
        {
            connect = new SQLiteConnection(connectionstring);
            //connect = new SqlConnection(connectionstring);
        }

        private void loadFluid()
        {
            CreateConnection();
            connect.Open();
            string text = @"SELECT FLUID FROM FLUIDNAMES";
           command = connect.CreateCommand();
            command.CommandText = text;
         
            reader= command.ExecuteReader();
            while (reader.Read())
            {
                lstFluid.Items.Add(reader.GetValue(0)); 
            }
            connect.Close();
        }
        private void LoadExchangers()
        {         
            exchangers = BinarySerialization.ReadFromBinaryFile<List<HeatExchanger>>(exlocation);
            foreach (HeatExchanger item in exchangers)
            {
                lstExchanger.Items.Add(item.Identifier);
            }
        }
        public void LoadLab(Lab lab)
        {
            txtEX.Text = lab.ExchangerInUse;
            txtCF.Text = lab.ColdFluid;
            txtHF.Text = lab.HotFluid;
        }

        private void btnViewExchanger_Click(object sender, RoutedEventArgs e)
        {
            if (lstExchanger.SelectedItem!=null)
            {
                ExchangerViewer viewer = new ExchangerViewer();
                IEnumerable<HeatExchanger> HES = from exchanger in exchangers where exchanger.Identifier == lstExchanger.SelectedItem.ToString() select exchanger;
                foreach (HeatExchanger item in HES)
                {
                    viewer.ViewExchanger(item);
                }
                viewer.Owner = this;
                viewer.ShowDialog();
            }

        }

        private void btnViewFluid_Click_1(object sender, RoutedEventArgs e)
        {
            ViewFluid();
        }

        private void ViewFluid()
        {
            if (lstFluid.SelectedItem!=null)
            {
                FluidViewer viewer = new FluidViewer();
                viewer.Owner = this;

                viewer.colCp.Header += UnitConverter.ShowUnitSpecificHeat(unitsystem.UnitofCp) + ")";
                viewer.colT.Header += UnitConverter.ShowUnitTemperature(unitsystem.UnitofTemperature) + ")";
                viewer.colMiu.Header += UnitConverter.ShowUnitViscosity(unitsystem.UnitofViscosity) + ")";
                viewer.colRho.Header += UnitConverter.ShowUnitDensity(unitsystem.UnitofDensity) + ")";
                viewer.colK.Header += UnitConverter.ShowUnitThermalConductivity(unitsystem.UnitofK)+")";
                string name = lstFluid.SelectedItem.ToString();
                viewer.txtFluidName.Text = name;

                CreateConnection();
                connect.Open();
                
                command = connect.CreateCommand();
                string text = "SELECT * FROM FLUIDNAMES WHERE FLUID = '"+name+"'";
                command.CommandText = text;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    viewer.txtBp.Text= Math.Round((UnitConverter.ToUnitTemperature((double.Parse(reader.GetValue(1).ToString())),unitsystem.UnitofTemperature)),4).ToString();
                    viewer.txtMp.Text = Math.Round((UnitConverter.ToUnitTemperature((double.Parse(reader.GetValue(2).ToString())), unitsystem.UnitofTemperature)), 4).ToString();
                    viewer.lblBpunit.Content = UnitConverter.ShowUnitTemperature(unitsystem.UnitofTemperature);
                    viewer.lblMpunit.Content = UnitConverter.ShowUnitTemperature(unitsystem.UnitofTemperature);
                }
                connect.Close();
                connect.Open();
                text = @"SELECT * FROM "+name;
                command = connect.CreateCommand();
                command.CommandText = text;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    double temp = Math.Round(UnitConverter.ToUnitTemperature(double.Parse(reader.GetValue(0).ToString()), unitsystem.UnitofTemperature),4);
                    double cp = Math.Round(UnitConverter.ToUnitSpecificHeat(double.Parse(reader.GetValue(1).ToString()), unitsystem.UnitofCp),4);
                    double miu = Math.Round(UnitConverter.ToUnitViscosity(double.Parse(reader.GetValue(2).ToString()), unitsystem.UnitofViscosity),4);
                    double rho = Math.Round(UnitConverter.ToUnitDensity(double.Parse(reader.GetValue(3).ToString()), unitsystem.UnitofDensity),4);
                    double k = Math.Round(UnitConverter.ToUnitThermalConductivity(double.Parse(reader.GetValue(4).ToString()), unitsystem.UnitofK),4);
                    var aa = new {Temp=temp,Cp=cp,MIU=miu,RHO=rho,K =k};
                    viewer.datagrid.Items.Add(aa);
                }
                connect.Close();
                viewer.ShowDialog();
           
            }

        }

        private void btnAddExchanger_Click_1(object sender, RoutedEventArgs e)
        {
            if (lstExchanger.SelectedItem!=null)
            {
                txtEX.Text = lstExchanger.SelectedItem.ToString();
            }
        }

        private void btnAddToHot_Click_1(object sender, RoutedEventArgs e)
        {
            if (lstFluid.SelectedItem != null)
            {
              txtHF.Text = lstFluid.SelectedItem.ToString();
            }
        }

        private void btnAddToCold_Click_1(object sender, RoutedEventArgs e)
        {
            if (lstFluid.SelectedItem != null)
            {
                txtCF.Text = lstFluid.SelectedItem.ToString();
            }
        }

        private void btnDeleteFluid_Click_1(object sender, RoutedEventArgs e)
        {
           
                 
            string name = lstFluid.SelectedItem.ToString();
            if (!string.Equals(name,txtHF.Text)&&!string.Equals(name,txtCF.Text))
            {
                connect.Open();     
                string text = "DROP TABLE " + name + ";";
                 command = connect.CreateCommand();
                command.CommandText = text;
                command.ExecuteNonQuery();
                text = "DELETE FROM FLUIDNAMES WHERE FLUID = '" + name + "'";
                command.CommandText = text;
                command.ExecuteNonQuery();
                lstFluid.Items.RemoveAt(lstFluid.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Cannot delete fluid currently in use");
            }

            
        }

        private void btnDeleteExchanger_Click_1(object sender, RoutedEventArgs e)
        {
            if (lstExchanger.SelectedItem!=null)
            {
                if (!string.Equals(lstExchanger.SelectedItem.ToString(),txtEX.Text))
                {
                    List<HeatExchanger> HES = new List<HeatExchanger>();
                    foreach (HeatExchanger item in exchangers)
                    {
                        if (!string.Equals(lstExchanger.SelectedItem.ToString(), item.Identifier))
                        {
                            HES.Add(item);
                        }
                    }
                    lstExchanger.Items.Clear();
                    exchangers.Clear();
                    foreach (HeatExchanger item in HES)
                    {
                        exchangers.Add(item);
                    }
                    foreach (HeatExchanger item in exchangers)
                    {
                        lstExchanger.Items.Add(item.Identifier);
                    }
                    BinarySerialization.WriteToBinaryFile<List<HeatExchanger>>(exlocation, exchangers);
                }
                else
                {
                    MessageBox.Show("Cannot delete exchanger currently in use");
                }
            }
        }
    }
}
