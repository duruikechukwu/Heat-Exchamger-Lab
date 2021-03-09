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

namespace Heat_Exchamger_Lab
{
    /// <summary>
    /// Interaction logic for FluidDefiner.xaml
    /// </summary>
    public partial class FluidDefiner : Window
    {
        UnitSystem unitsystem;
        double? mp;
        double T;
        bool Go1;
        bool Go2;
        public FluidDefiner()
        {
            InitializeComponent();
            unitsystem = BinarySerialization.ReadFromBinaryFile<UnitSystem>(UnitSystem.UnitSetupPath);   
            this.Loaded += FluidDefiner_Loaded;
        }

        void FluidDefiner_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUnits();
        }
        private void LoadUnits()
        {
            cmbUnitOfBP.SelectedIndex = unitsystem.UnitofTemperature;
            cmbUnitOfCp.SelectedIndex = unitsystem.UnitofCp;
            cmbUnitOfDensity.SelectedIndex = unitsystem.UnitofDensity;
            cmbUnitOfK.SelectedIndex = unitsystem.UnitofK;
            cmbUnitOfMP.SelectedIndex = unitsystem.UnitofTemperature;
            cmbUnitOfTemp.SelectedIndex = unitsystem.UnitofTemperature;
            cmbUnitOfViscosity.SelectedIndex = unitsystem.UnitofViscosity;
        }
        private void btnOk_Click_1(object sender, RoutedEventArgs e)
        {
            if (lstData.Items.Count>=2)
            {
                double count = 0;            
                double check1 = 0;
                double check2 = 0;
                double itemnumber = 1;
                Go1 = false;
                Go2 = false;
                T=0;
                try
                {
                    double a = double.Parse(txtBP.Text);
                    check1 = 1;  
                    double b = double.Parse(txtMP.Text);
                    check2 = 1;
                               
                }
                catch (Exception)
                {
                    
                   
                }
                foreach (FluidData item in lstData.Items)
                {
                   
                    if (item.CheckandValidate())
                    {
                        count++;
                        if (check1==1&&check2==1&&double.Parse(item.txtTemp.Text)>= double.Parse(txtMP.Text)&&double.Parse(item.txtTemp.Text)<=double.Parse(txtBP.Text)&&cmbPhase.SelectedIndex==0)
                        {
                            T++;
                        }
                        else if (check1==1&&double.Parse(item.txtTemp.Text)>= double.Parse(txtBP.Text)&&cmbPhase.SelectedIndex==1)
                        {
                            T++;
                        }
                        else
                        {
                            T = 0;
                        }

                        if (itemnumber==1)
                        {
                            if (item.txtTemp.Text==txtMP.Text&&cmbPhase.SelectedIndex==0)
                            {
                                Go1 = true;
                            }
                            else if (item.txtTemp.Text==txtBP.Text&&cmbPhase.SelectedIndex==1)
                            {
                                Go1 = true;
                                Go2=true;
                            }
                            
                        }

                        itemnumber++;
                        if (itemnumber== lstData.Items.Count+1)
                        {
                            if (item.txtTemp.Text == txtBP.Text && cmbPhase.SelectedIndex == 0)
                            {
                                Go2 = true;
                            }
                            
                        }
                    }
                    else
                    {
                        count = 0;
                    }

                }
                //
                if (count == lstData.Items.Count && check1 == 1 && check2 == 1 && !string.IsNullOrWhiteSpace(txtFluidName.Text)&&cmbPhase.SelectedIndex==0)
                {
                    mp = Math.Round(UnitConverter.ToStandardTemperature(double.Parse(txtMP.Text), cmbUnitOfMP.SelectedIndex),4);
                    AddFluid();
         
                }
                else if (count == lstData.Items.Count && check1 == 1 && !string.IsNullOrWhiteSpace(txtFluidName.Text)&&cmbPhase.SelectedIndex==1)
                {
                    mp=null;
                    AddFluid();
                }

                else
                {
                    MessageBox.Show("Incorrect or Incomplete Data Entry", "UNABLE TO COMPLETE TASK");
                }
              //
            } 
            else
            {
                MessageBox.Show("At Least 2 sets of Data Required", "UNABLE TO COMPLETE TASK");
            }
        }

        private void btnDelete_Click_1(object sender, RoutedEventArgs e)
        {
            if (lstData.SelectedItem!=null)
            {
                lstData.Items.RemoveAt(lstData.SelectedIndex);
            }
        }

        private void btnAdd_Click_1(object sender, RoutedEventArgs e)
        {
            if (lstData.Items.Count<15)
            {
                  lstData.Items.Add(new FluidData());
            }
            else
            {
                MessageBox.Show("Maximum of 15 set of Data Required", "UNABLE TO ADD");
            }
        }

        private void AddFluid()
        {
            if (T==lstData.Items.Count)
            {
                if (Go1&&Go2)
                {
                    bool ok = false;
                    double bp = Math.Round(UnitConverter.ToStandardTemperature(double.Parse(txtBP.Text), cmbUnitOfBP.SelectedIndex), 4);
                    string name = txtFluidName.Text;
                    Store.CreateConnection();
                    Store.command = Store.connect.CreateCommand();
                    Store.connect.Open();
                    string checktext = "INSERT INTO FLUIDNAMES VALUES('" + name + "','" + bp + "','" + mp + "')";
                    string creattext = "CREATE TABLE " + name + " (TEMP NCHAR (10) NOT NULL,CP NCHAR (10) NOT NULL,MIU NCHAR (10) NOT NULL, RHO NCHAR (10) NOT NULL, K NCHAR (10) NOT NULL)";
                 
                   

                    try
                    {

                        Store.command.CommandText = creattext;
                        Store.command.ExecuteNonQuery();
                        Store.command.CommandText = checktext;
                        Store.command.ExecuteNonQuery();
                        ok = true;
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Fluid with similar name already exists or wrong input", "CANNOT COMPLETE TASK");
                    }

                    if (ok)
                    {
                        foreach (FluidData item in lstData.Items)
                        {
                            double temp = Math.Round(UnitConverter.ToStandardTemperature(double.Parse(item.txtTemp.Text), cmbUnitOfTemp.SelectedIndex), 4);
                            double cp = Math.Round(UnitConverter.ToStandardSpecificHeat(double.Parse(item.txtCp.Text), cmbUnitOfCp.SelectedIndex), 4);
                            double miu = Math.Round(UnitConverter.ToStandardViscosity(double.Parse(item.txtMiu.Text), cmbUnitOfViscosity.SelectedIndex), 4);
                            double rho = Math.Round(UnitConverter.ToStandardDensity(double.Parse(item.txtRho.Text), cmbUnitOfDensity.SelectedIndex), 4);
                            double k = Math.Round(UnitConverter.ToStandardThermalConductivity(double.Parse(item.txtK.Text), cmbUnitOfK.SelectedIndex), 4);
                            string inserttext = "INSERT INTO " + name + " VALUES('" + temp + "','" + cp + "','" + miu + "','" + rho + "','" + k + "')";
                            Store.command.CommandText = inserttext;
                            Store.command.ExecuteNonQuery();
                        }
                        MessageBox.Show("Added");
                    }
                    Store.connect.Close();
               
                }
                else
                {
                    if (cmbPhase.SelectedIndex==0)
                    {
                        MessageBox.Show("Temperature Data of Liquid Must Start and End With Melting and Boiling Point Respectively ", "UNABLE TO COMPLETE TASK"); 
                    }
                    else
                    {
                        MessageBox.Show("Temperature Data of Gas Must Start With Boiling Point ", "UNABLE TO COMPLETE TASK");
                    }
                }
            }
            else
            {
                MessageBox.Show("Data must be restricted to single phase region","UNABLE TO COMPLETE TASK");
            }
        }

        private void cmbPhase_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPhase.SelectedIndex==0)
            {
                txtMP.Visibility = Visibility.Visible;
            }
            else
            {
                txtMP.Clear();
                txtMP.Visibility = Visibility.Hidden;
            }
        }

        private void btnClearAll_Click_1(object sender, RoutedEventArgs e)
        {
            if (lstData.HasItems)
            {
                foreach (FluidData item in lstData.Items)
                {
                    item.txtCp.Clear();
                    item.txtK.Clear();
                    item.txtMiu.Clear();
                    item.txtRho.Clear();
                    item.txtTemp.Clear();
                }
            }
        }
    }
}
