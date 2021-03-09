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
using System.IO;
using Microsoft.Win32;
using System.Data.SQLite;
using System.Data.SqlClient;


namespace Heat_Exchamger_Lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int TimerCount = 0;
     
        bool IsReady;
        bool IsNotRunning;
        Lab myLab;
        string currentfile;
        string currentfilename;
        string testfilename;
        PerformanceCalculator calculator;
        UnitSystem unitsystem;
        CaseStudySystem study;
        Store store;
       
        bool textaltered;
        bool changemade;
        bool dynamic;
        int colorindex;
        int equipmentcoloindex;
        bool canrecord;
        string settingspath;
        Settings settig;
        public MainWindow()
        {
            InitializeComponent();
            UnitSystem.UnitSetupPath = AppDomain.CurrentDomain.BaseDirectory + "SETUPFILE001";
            settingspath = AppDomain.CurrentDomain.BaseDirectory + "SETUPFILE003";
            calculator = new PerformanceCalculator();
            TankHottank.Type = FluidTank.TankTypes.hot;
            TankcoldTank.Type = FluidTank.TankTypes.cold;
            IsReady = false;
            IsNotRunning = true;
            InitializeUnit();
            Store s = new Store(false);
            TankcoldTank.Off();
            TankHottank.Off();
            SetInitialUnits();
            NewExperiment();
            this.Closing += MainWindow_Closing;
            
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            settig = new Settings() { isdynamic = dynamic, backcolor = colorindex, equipmentcolor = equipmentcoloindex };
            BinarySerialization.WriteToBinaryFile<Settings>(settingspath, settig);
            if (!Confirm())
            {
                e.Cancel=true;
            }
        }

        private void CalculatePerformance()
        {
            calculator.ExchangerName = myLab.ExchangerInUse;
            if (myLab.HotIsToShell)
            {
                calculator.ShellSideFluid = myLab.HotFluid;
                calculator.TubeSideFluid = myLab.ColdFluid;
                calculator.Tsi = myLab.HFT;
                calculator.Tti = myLab.CFT;
            }
            else
            {
                calculator.TubeSideFluid = myLab.HotFluid;
                calculator.ShellSideFluid = myLab.ColdFluid;
                calculator.Tti = myLab.HFT;
                calculator.Tsi = myLab.CFT;
            }
            calculator.SVolFlow = myLab.SVF;
            calculator.tVolFlow = myLab.TVF;
            calculator.Calculate(myLab.shellNozzleDiameter,myLab.tubeNozzleDiameter,myLab.shellFF,myLab.tubeFF);
        }
      
    

        private void Comment(string exchanger,string hotfluid, string coldfluid)
        {
            run1.Foreground = Brushes.Black;
            run2.Foreground = Brushes.Black;
            run1.Text = string.Format("Heat Exchanger In Use: {0}", exchanger);

            Store.CreateConnection();
            Store.connect.Open();
            string text = "SELECT * FROM FLUIDNAMES WHERE FLUID = '" + hotfluid + "'";
            Store.command = Store.connect.CreateCommand();
            Store.command.CommandText = text;
            Store.reader = Store.command.ExecuteReader();
            string BP="";
            string MP="";
            while (Store.reader.Read())
            {
                BP = Math.Round((UnitConverter.ToUnitTemperature((double.Parse(Store.reader.GetValue(1).ToString())), unitsystem.UnitofTemperature)), 4).ToString();
                MP = Math.Round((UnitConverter.ToUnitTemperature((double.Parse(Store.reader.GetValue(2).ToString())), unitsystem.UnitofTemperature)), 4).ToString();
            }
            run2.Text = string.Format("\nHot Fluid: {0}  BP: {1}   MP: {2}", hotfluid, BP, MP);
            Store.connect.Close();
            Store.connect.Open();
            text = "SELECT * FROM FLUIDNAMES WHERE FLUID = '" + coldfluid + "'";
            Store.command = Store.connect.CreateCommand();
            Store.command.CommandText = text;
            Store.reader = Store.command.ExecuteReader();
            while (Store.reader.Read())
            {
                BP = Math.Round((UnitConverter.ToUnitTemperature((double.Parse(Store.reader.GetValue(1).ToString())), unitsystem.UnitofTemperature)), 4).ToString();
                MP = Math.Round((UnitConverter.ToUnitTemperature((double.Parse(Store.reader.GetValue(2).ToString())), unitsystem.UnitofTemperature)), 4).ToString();
            }
            run3.Text = string.Format("\nCold Fluid: {0}  BP: {1}   MP: {2}", coldfluid, BP, MP);

            Store.connect.Close();
               
        }

        private async void Comment(string word)
        {
            run2.Text = "";
            run3.Text = "";
            run1.Foreground = Brushes.Red;
            run1.Text = word;
            await Task.Delay(1000);
            Comment(myLab.ExchangerInUse, myLab.HotFluid, myLab.ColdFluid);

        }

        private async void Comment(string word,string header)
        {
            run2.Text = word;
            run3.Text = "";
            run1.Text = header;
            run2.Foreground = Brushes.Red;
            run1.Foreground=Brushes.Red;
            await Task.Delay(5000);
            Comment(myLab.ExchangerInUse, myLab.HotFluid, myLab.ColdFluid);

        }

        private void InitializeUnit()
        {
            if (!File.Exists(UnitSystem.UnitSetupPath))
            {
               LoadUnit(); 
            }
            else
            {
                SetInitialUnits();
            }
          
            if (!File.Exists(settingspath))
            {
                settig = new Settings() { isdynamic = true, backcolor=0, equipmentcolor=0 };
                BinarySerialization.WriteToBinaryFile<Settings>(settingspath, settig);
            }
                settig = BinarySerialization.ReadFromBinaryFile<Settings>(settingspath);
                ChangeDynamics(settig.isdynamic);
                ChangeBackground(settig.backcolor);
                ChangeEquipmentColour(settig.equipmentcolor);
         
        }
        private void SetInitialUnits()
        {
            unitsystem = BinarySerialization.ReadFromBinaryFile<UnitSystem>(UnitSystem.UnitSetupPath);
            cmbUnitOfCFTemp.SelectedIndex = unitsystem.UnitofTemperature;
            cmbUnitOfHFTemp.SelectedIndex = unitsystem.UnitofTemperature;
            cmbUnitOfShellVFlow.SelectedIndex = unitsystem.UnitofVolumetricFlowrate;
            cmbUnitOfTubeVFlow.SelectedIndex = unitsystem.UnitofVolumetricFlowrate;
            //txtunitTime.Text = UnitConverter.ShowUnitime(unitsystem.UnitofTime);
            TankcoldTank.txtVolumeUnit.Text = UnitConverter.ShowUnitVolume(unitsystem.UnitofVolume);
            TankHottank.txtVolumeUnit.Text = UnitConverter.ShowUnitVolume(unitsystem.UnitofVolume);
        }

        private void LoadUnit()
        {
            UnitManager unit = new UnitManager();
           // unit.Owner = this;
            unit.Closed += unit_Closed;
           // unit.ShowDialog();
            unit.Close();
        }
        private void NewExperiment()
        {

            if (IsNotRunning)
            {
                if (Confirm())
                {
                    myLab = new Lab();

                    myLab.UnitCFT = unitsystem.UnitofTemperature;
                    myLab.UnitHFT = unitsystem.UnitofTemperature;
                    myLab.UnitSVF = unitsystem.UnitofVolumetricFlowrate;
                    myLab.UnitTVF = unitsystem.UnitofVolumetricFlowrate;
                    txtShellFF.Text = myLab.shellFF.ToString();
                    txtTubeFF.Text = myLab.tubeFF.ToString();
                    ClearFields();
                    SetInitialUnits();
                    currentfile = null;
                    currentfilename = "NO NAME";
                    txbfilename.Text = currentfilename;
                    FixTankSides();
                    Comment(myLab.ExchangerInUse, myLab.HotFluid, myLab.ColdFluid);
                   Animate1();
                }
            }
            else
            {
                MessageBox.Show("Unable to complete task while experiment is running", "STOP EXPERIMENT!!");
            }
     
        }

        private void OpenExperiment()
        {

            if (IsNotRunning)
            {
                OpenFileDialog open = new OpenFileDialog();
                open.DefaultExt = ".hexlab";
               // open.InitialDirectory = @"C:\Users\bles\Documents";
                open.RestoreDirectory = true;
                open.Filter = "HEXLAB Files(*.hexlab)|*.hexlab";
                if (open.ShowDialog() == true)
                {
                    if (Confirm())
                    {
                        if (!string.IsNullOrEmpty(open.FileName))
                        {
                            testfilename = open.SafeFileName;
                            OpenFile(open.FileName);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Unable to complete task while experiment is running", "STOP EXPERIMENT!!");
            }
          
        }


        public void OpenFile(string filename)
        {
            try
            {
                myLab = BinarySerialization.ReadFromBinaryFile<Lab>(filename);
                cmbUnitOfCFTemp.SelectedIndex = myLab.UnitCFT;
                cmbUnitOfHFTemp.SelectedIndex = myLab.UnitHFT;
                cmbUnitOfShellVFlow.SelectedIndex = myLab.UnitSVF;
                cmbUnitOfTubeVFlow.SelectedIndex = myLab.UnitTVF;
                txtShellFF.Text = myLab.shellFF.ToString();
                txtTubeFF.Text = myLab.tubeFF.ToString();
                ClearFields();
                txtCFT.Text = UnitConverter.ToUnitTemperature(myLab.CFT, cmbUnitOfCFTemp.SelectedIndex).ToString();
                txtHFT.Text = UnitConverter.ToUnitTemperature(myLab.HFT, cmbUnitOfHFTemp.SelectedIndex).ToString();
                txtShellFlow.Text = UnitConverter.ToUnitVolumeFlow(myLab.SVF, cmbUnitOfShellVFlow.SelectedIndex).ToString();
                txtTubeFlow.Text = UnitConverter.ToUnitVolumeFlow(myLab.TVF, cmbUnitOfTubeVFlow.SelectedIndex).ToString();
                currentfile = filename;
                currentfilename = testfilename;
                FixTankSides();
                changemade = false;
                textaltered = false;
                txbfilename.Text = currentfilename;
                Comment(myLab.ExchangerInUse, myLab.HotFluid, myLab.ColdFluid);
                Animate1();
       
            }
            catch (Exception)
            {

                MessageBox.Show("Unable to Open", "ERROR! SOMETHING WENT WRONG", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            txtT1.Clear();
            txtT2.Clear();
            txtT3.Clear();
            txtT4.Clear();
            txtTimer.Clear();
            TankHottank.txtVolumeChange.Clear();
            TankcoldTank.txtVolumeChange.Clear();         
            txtCFT.Clear();
            txtHFT.Clear();
            txtShellFlow.Clear();
            txtTubeFlow.Clear();
            textaltered = false;
            changemade = false;
            
        }

        private bool Confirm()
        {
            if (myLab != null&&changemade)
            {
                MessageBoxResult result = MessageBox.Show("Do you wish to save changes to existing case before exit?", "SAVE EXPERIMENT?", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    SaveExperiment();
                    return true;
                }
                else if (result == MessageBoxResult.No)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void SaveExperiment()
        {
            if (string.IsNullOrEmpty(currentfile))
            {
                SaveExperimentAs();

            }
            else
            {
                myLab.UnitCFT = cmbUnitOfCFTemp.SelectedIndex;
                myLab.UnitHFT = cmbUnitOfHFTemp.SelectedIndex;
                myLab.UnitSVF = cmbUnitOfShellVFlow.SelectedIndex;
                myLab.UnitTVF = cmbUnitOfTubeVFlow.SelectedIndex;
        
                try
                {
                    myLab.CFT = UnitConverter.ToStandardTemperature(double.Parse(txtCFT.Text), cmbUnitOfCFTemp.SelectedIndex);
                    myLab.SVF = UnitConverter.ToStandardVolumeFlow(double.Parse(txtShellFlow.Text), cmbUnitOfShellVFlow.SelectedIndex);

                    myLab.HFT = UnitConverter.ToStandardTemperature(double.Parse(txtHFT.Text), cmbUnitOfHFTemp.SelectedIndex);
                    myLab.TVF = UnitConverter.ToStandardVolumeFlow(double.Parse(txtTubeFlow.Text), cmbUnitOfTubeVFlow.SelectedIndex);
                    myLab.shellFF = double.Parse(txtShellFF.Text);
                    myLab.tubeFF = double.Parse(txtTubeFF.Text);
                }
                catch (Exception)
                {
                    Comment("Parameters Not Fully Specified","INCOMPLETE!!");
                }

                BinarySerialization.WriteToBinaryFile<Lab>(currentfile, myLab);
                textaltered = false;
                changemade = false;
                txbfilename.Text = currentfilename;
                MessageBox.Show("Saved Successfully");
            }
        }

        private void SaveExperimentAs()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".hexlab";
            save.Filter = "HEXLAB file(*.hexlab)|*.hexlab";
           // save.InitialDirectory = @"C:\Users\bles\Documents";
            save.RestoreDirectory = true;
            if (save.ShowDialog()==true)
            {
                if (!string.IsNullOrEmpty(save.FileName))
                {
                    currentfile = save.FileName;
                    currentfilename = save.SafeFileName;
                    SaveExperiment();
                }
            }
           
        }

        async void TimerTick()
        {
            while (IsNotRunning==false)
            {
                txtTimer.Text = TimerCount.ToString();
                //try
                //{
                //         ShowVolumeChange();
                //}
                //catch (Exception)
                //{
                    
                   
                //}
                
                await Task.Delay(1000);
                TimerCount += 1;
            }
        }

        async void Animate1()
        {
            double step2 = 0;
            double step1 = 0;
 
            
            cansub.Children.Clear();
         
            if (myLab.HotIsToShell) //hot is to shell
            {
                while (step1 < 280)
                {
                    Ellipse eh1 = new Ellipse() { Fill = Brushes.Yellow, Height = 5, Width = 5 };
                    Canvas.SetLeft(eh1, step1);
                    Canvas.SetTop(eh1, 15);
                    cansub.Children.Add(eh1);
                    step1 += 10;
                }
                while (step2 < 90)
                {
                    Ellipse eh2 = new Ellipse() { Fill = Brushes.Aqua, Height = 5, Width = 5 };
                    Canvas.SetLeft(eh2, step2);
                    Canvas.SetTop(eh2, 64);
                    cansub.Children.Add(eh2);
                    step2 += 10;
                }
                while (IsNotRunning == false)
                {

                    foreach (var item in cansub.Children)
                    {
                        if (item is Ellipse && Canvas.GetTop((UIElement)item) == 15)
                        {
                            if (Canvas.GetLeft((UIElement)item) < 279)
                            {
                                Canvas.SetLeft((UIElement)item, Canvas.GetLeft((UIElement)item) + 1);
                            }
                            if (Canvas.GetLeft((UIElement)item) ==279)
                            {
                                Canvas.SetLeft((UIElement)item, 0);
                            }
                        }
                        if (item is Ellipse && Canvas.GetTop((UIElement)item) == 64)
                        {
                            if (Canvas.GetLeft((UIElement)item) < 86)
                            {
                                Canvas.SetLeft((UIElement)item, Canvas.GetLeft((UIElement)item) + 1);
                            }
                            if (Canvas.GetLeft((UIElement)item) == 86)
                            {
                                Canvas.SetLeft((UIElement)item, 0);
                            }
                        }
                    }
                    await Task.Delay(10);
                }
            }
            else
            {
                while (step1 < 280)
                {
                    Ellipse eh1 = new Ellipse() { Fill = Brushes.Aqua, Height = 5, Width = 5 };
                    Canvas.SetLeft(eh1, step1);
                    Canvas.SetTop(eh1, 15);
                    cansub.Children.Add(eh1);
                    step1 += 10;
                }
                while (step2 < 90)
                {
                    Ellipse eh2 = new Ellipse() { Fill = Brushes.Yellow, Height = 5, Width = 5 };
                    Canvas.SetLeft(eh2, step2);
                    Canvas.SetTop(eh2, 64);
                    cansub.Children.Add(eh2);
                    step2 += 10;
                }
                while (IsNotRunning == false)
                {

                    foreach (var item in cansub.Children)
                    {
                        if (item is Ellipse && Canvas.GetTop((UIElement)item) == 15)
                        {
                            if (Canvas.GetLeft((UIElement)item) < 279)
                            {
                                Canvas.SetLeft((UIElement)item, Canvas.GetLeft((UIElement)item) + 1);
                            }
                            if (Canvas.GetLeft((UIElement)item) == 279)
                            {
                                Canvas.SetLeft((UIElement)item, 0);
                            }
                        }
                        if (item is Ellipse && Canvas.GetTop((UIElement)item) == 64)
                        {
                            if (Canvas.GetLeft((UIElement)item) < 86)
                            {
                                Canvas.SetLeft((UIElement)item, Canvas.GetLeft((UIElement)item) + 1);
                            }
                            if (Canvas.GetLeft((UIElement)item) == 86)
                            {
                                Canvas.SetLeft((UIElement)item, 0);
                            }
                        }
                    }
                    await Task.Delay(10);
                }
              
            }
            
           
        }
    
        async void Animate()
        {
            Animate1();
            bool notcompleted1 = true;
            bool notcompleted2 = true;
          
            canmain.Children.Clear();
            double x1 = 9; double y1 = 56;
            double x2 = 205; double y2 = 7;
            int step1 = 10;
            int step2 = 10;

            double time = 0;
            double DhotT =1;

            while (IsNotRunning == false)
            {
           

                if (myLab.HotIsToShell)
                {


                    if (step1 == 10 && notcompleted1)
                    {
                        step1 = 0;
                        Ellipse eh1 = new Ellipse() { Fill = Brushes.Aqua, Height = 5, Width = 5 };
                        Canvas.SetLeft(eh1, x1);
                        Canvas.SetTop(eh1, y1);
                        canmain.Children.Add(eh1);

                    }
                    if (step2 == 10 && notcompleted2)
                    {
                        step2 = 0;
                        Ellipse eh2 = new Ellipse() { Fill = Brushes.Yellow, Height = 5, Width = 5 };
                        Canvas.SetLeft(eh2, x2);
                        Canvas.SetTop(eh2, y2);
                        canmain.Children.Add(eh2);

                    }
                    if (notcompleted1)
                    {
                        step1++;

                    }
                    if (notcompleted2)
                    {
                        step2++;
                    }

                    foreach (var item in canmain.Children)
                    {
                        if (item is Ellipse && Canvas.GetLeft((UIElement)item) == x1)
                        {
                            if (Canvas.GetTop((UIElement)item) < 165)
                            {
                                Canvas.SetTop((UIElement)item, Canvas.GetTop((UIElement)item) + 1);
                            }
                        }
                        if (item is Ellipse && Canvas.GetTop((UIElement)item) == 165)
                        {
                            Ellipse a = item as Ellipse;
                            if (a.Fill == Brushes.Aqua)
                            {
                                Canvas.SetLeft((UIElement)item, Canvas.GetLeft((UIElement)item) + 1);

                                if (Canvas.GetLeft((UIElement)item) > 372 && Canvas.GetLeft((UIElement)item) < 890)
                                {
                                    a.Visibility = Visibility.Hidden;
                                }
                                else
                                {
                                    a.Visibility = Visibility.Visible;

                                }
                                if (Canvas.GetLeft((UIElement)item) == 890)
                                {
                                  
                                    if (dynamic)
                                    {
                                        if (DhotT < (myLab.HFT - myLab.CFT))
                                        {

                                            calculator.Tsi = myLab.CFT + DhotT;
                                            calculator.Calculate(myLab.shellNozzleDiameter, myLab.tubeNozzleDiameter, myLab.shellFF, myLab.tubeFF);
                                            ShowResult();
                                            time += 0.01;
                                            DhotT = (myLab.HeaterPower / calculator.Cs) * (1 - Math.Exp((-calculator.SVolFlow * time) / (3600 * myLab.HeaterVolume)));


                                        }
                                        else
                                        {
                                            calculator.Tsi = myLab.HFT;
                                            calculator.Calculate(myLab.shellNozzleDiameter, myLab.tubeNozzleDiameter, myLab.shellFF, myLab.tubeFF);
                                            ShowResult();

                                        }
                                    }
                                    else
                                    {
                                        ShowResult();
                                    }
                                    canrecord = true;
                                    //ShowResult();
                                }
                                if (Canvas.GetLeft((UIElement)item) == 1300)
                                {
                                    notcompleted1 = false;
                                    Canvas.SetLeft((UIElement)item, x1);
                                    Canvas.SetTop((UIElement)item, y1);
                                }
                            }
                        }
                        if (item is Ellipse && Canvas.GetLeft((UIElement)item) == x2)
                        {
                            if (Canvas.GetTop((UIElement)item) < 53)
                            {
                                Canvas.SetTop((UIElement)item, Canvas.GetTop((UIElement)item) + 1);
                            }
                        }
                        if (item is Ellipse && Canvas.GetTop((UIElement)item) == 53 && Canvas.GetLeft((UIElement)item) != x1)
                        {

                            if (Canvas.GetLeft((UIElement)item) < 397)
                            {
                                Canvas.SetLeft((UIElement)item, Canvas.GetLeft((UIElement)item) + 1);
                            }
                        }
                        if (item is Ellipse && Canvas.GetLeft((UIElement)item) == 397)
                        {
                            Ellipse a = item as Ellipse;
                            if (a.Fill == Brushes.Yellow)
                            {
                                if (Canvas.GetTop((UIElement)item) < 230)
                                {
                                    Canvas.SetTop((UIElement)item, Canvas.GetTop((UIElement)item) + 1);
                                }

                                if (Canvas.GetTop((UIElement)item) > 105)
                                {
                                    a.Visibility = Visibility.Hidden;
                                }
                            }
                        }
                        if (item is Ellipse && Canvas.GetTop((UIElement)item) == 230)
                        {
                            if (Canvas.GetLeft((UIElement)item) < 851)
                            {
                                Canvas.SetLeft((UIElement)item, Canvas.GetLeft((UIElement)item) + 1);
                            }
                        }
                        if (item is Ellipse && Canvas.GetLeft((UIElement)item) == 851 && Canvas.GetTop((UIElement)item) != 165)
                        {
                            Ellipse a = item as Ellipse;
                            a.Visibility = Visibility.Visible;
                            if (Canvas.GetTop((UIElement)item) < 294)
                            {
                                Canvas.SetTop((UIElement)item, Canvas.GetTop((UIElement)item) + 1);
                            }
                        }
                        if (item is Ellipse && Canvas.GetTop((UIElement)item) == 294)
                        {
                            Ellipse a = item as Ellipse;
                            if (Canvas.GetLeft((UIElement)item) < 1300)
                            {
                                Canvas.SetLeft((UIElement)item, Canvas.GetLeft((UIElement)item) + 1);
                            }
                            if (Canvas.GetLeft((UIElement)item) == 1300)
                            {
                                notcompleted2 = false;
                                Canvas.SetLeft((UIElement)item, x2);
                                Canvas.SetTop((UIElement)item, y2);
                            }
                        }
                    }


                }


                else
                {
                    if (step1 == 10 && notcompleted1)
                    {
                        step1 = 0;
                        Ellipse eh1 = new Ellipse() { Fill = Brushes.Yellow, Height = 5, Width = 5 };
                        Canvas.SetLeft(eh1, x1);
                        Canvas.SetTop(eh1, y1);
                        canmain.Children.Add(eh1);

                    }
                    if (step2 == 10 && notcompleted2)
                    {
                        step2 = 0;
                        Ellipse eh2 = new Ellipse() { Fill = Brushes.Aqua, Height = 5, Width = 5 };
                        Canvas.SetLeft(eh2, x2);
                        Canvas.SetTop(eh2, y2);
                        canmain.Children.Add(eh2);

                    }
                    if (notcompleted1)
                    {
                        step1++;

                    }
                    if (notcompleted2)
                    {
                        step2++;
                    }

                    foreach (var item in canmain.Children)
                    {
                        if (item is Ellipse && Canvas.GetLeft((UIElement)item) == x1)
                        {
                            if (Canvas.GetTop((UIElement)item) < 165)
                            {
                                Canvas.SetTop((UIElement)item, Canvas.GetTop((UIElement)item) + 1);
                            }
                        }
                        if (item is Ellipse && Canvas.GetTop((UIElement)item) == 165)
                        {
                            Ellipse a = item as Ellipse;
                            if (a.Fill == Brushes.Yellow)
                            {
                                Canvas.SetLeft((UIElement)item, Canvas.GetLeft((UIElement)item) + 1);

                                if (Canvas.GetLeft((UIElement)item) > 372 && Canvas.GetLeft((UIElement)item) < 890)
                                {
                                    a.Visibility = Visibility.Hidden;
                                }
                                else
                                {
                                    a.Visibility = Visibility.Visible;

                                }
                                if (Canvas.GetLeft((UIElement)item) == 890)
                                {

                                    if (dynamic)
                                    {
                                        if (DhotT < (myLab.HFT - myLab.CFT))
                                        {

                                            calculator.Tti = myLab.CFT + DhotT;
                                            calculator.Calculate(myLab.shellNozzleDiameter, myLab.tubeNozzleDiameter, myLab.shellFF, myLab.tubeFF);
                                            ShowResult();
                                            time += 0.01;
                                            DhotT = (myLab.HeaterPower / calculator.Ct) * (1 - Math.Exp((-calculator.tVolFlow * time) / (3600 * myLab.HeaterVolume)));


                                        }
                                        else
                                        {
                                            calculator.Tti = myLab.HFT;
                                            calculator.Calculate(myLab.shellNozzleDiameter, myLab.tubeNozzleDiameter, myLab.shellFF, myLab.tubeFF);
                                            ShowResult();
                                        }
                                    }
                                    else
                                    {
                                        ShowResult();
                                    }
                                    canrecord = true;
                                    //ShowResult();
                                }
                                if (Canvas.GetLeft((UIElement)item) == 1300)
                                {
                                    notcompleted1 = false;
                                    Canvas.SetLeft((UIElement)item, x1);
                                    Canvas.SetTop((UIElement)item, y1);
                                }
                            }
                        }
                        if (item is Ellipse && Canvas.GetLeft((UIElement)item) == x2)
                        {
                            if (Canvas.GetTop((UIElement)item) < 53)
                            {
                                Canvas.SetTop((UIElement)item, Canvas.GetTop((UIElement)item) + 1);
                            }
                        }
                        if (item is Ellipse && Canvas.GetTop((UIElement)item) == 53 && Canvas.GetLeft((UIElement)item) != x1)
                        {

                            if (Canvas.GetLeft((UIElement)item) < 397)
                            {
                                Canvas.SetLeft((UIElement)item, Canvas.GetLeft((UIElement)item) + 1);
                            }
                        }
                        if (item is Ellipse && Canvas.GetLeft((UIElement)item) == 397)
                        {
                            Ellipse a = item as Ellipse;
                            if (a.Fill == Brushes.Aqua)
                            {
                                if (Canvas.GetTop((UIElement)item) < 230)
                                {
                                    Canvas.SetTop((UIElement)item, Canvas.GetTop((UIElement)item) + 1);
                                }

                                if (Canvas.GetTop((UIElement)item) > 105)
                                {
                                    a.Visibility = Visibility.Hidden;
                                }
                            }
                        }
                        if (item is Ellipse && Canvas.GetTop((UIElement)item) == 230)
                        {
                            if (Canvas.GetLeft((UIElement)item) < 851)
                            {
                                Canvas.SetLeft((UIElement)item, Canvas.GetLeft((UIElement)item) + 1);
                            }
                        }
                        if (item is Ellipse && Canvas.GetLeft((UIElement)item) == 851 && Canvas.GetTop((UIElement)item) != 165)
                        {
                            Ellipse a = item as Ellipse;
                            a.Visibility = Visibility.Visible;
                            if (Canvas.GetTop((UIElement)item) < 294)
                            {
                                Canvas.SetTop((UIElement)item, Canvas.GetTop((UIElement)item) + 1);
                            }
                        }
                        if (item is Ellipse && Canvas.GetTop((UIElement)item) == 294)
                        {
                            Ellipse a = item as Ellipse;
                            if (Canvas.GetLeft((UIElement)item) < 1300)
                            {
                                Canvas.SetLeft((UIElement)item, Canvas.GetLeft((UIElement)item) + 1);
                            }
                            if (Canvas.GetLeft((UIElement)item) == 1300)
                            {
                                notcompleted2 = false;
                                Canvas.SetLeft((UIElement)item, x2);
                                Canvas.SetTop((UIElement)item, y2);
                            }
                        }
                    }
                }


                await Task.Delay(10);
            }
        }

     

    
        private void SwitchValve(int position)
        {
            if (position==1)
            {
                rectShellValve1.Fill = Brushes.Green;
                rectTubeValve1.Fill = Brushes.Green;
                rectShellValve2.Fill = Brushes.Green;
                rectTubeValve2.Fill = Brushes.Green;
            }
            else
            {
                rectShellValve1.Fill = Brushes.Maroon;
                rectTubeValve1.Fill = Brushes.Maroon;
                rectShellValve2.Fill = Brushes.Maroon;
                rectTubeValve2.Fill = Brushes.Maroon;
            }
        }

        private void btnStart_Click_1(object sender, RoutedEventArgs e)
        {
            Start();
        }
        private void Start()
        {
            IsReady = false;
         
            try
            {
                myLab.UnitCFT = cmbUnitOfCFTemp.SelectedIndex;
                myLab.UnitHFT = cmbUnitOfHFTemp.SelectedIndex;
                myLab.UnitSVF = cmbUnitOfShellVFlow.SelectedIndex;
                myLab.UnitTVF = cmbUnitOfTubeVFlow.SelectedIndex;

                myLab.shellFF = double.Parse(txtShellFF.Text);
                myLab.tubeFF = double.Parse(txtTubeFF.Text);
                if (myLab.shellFF>0.01)
                {
                    txtShellFF.Foreground = Brushes.Red;
                    Comment("Fouling Factor Too High", "ATTENTION");
                }
                else
                {
                    txtShellFF.Foreground = Brushes.Blue;
                }
                if (myLab.tubeFF > 0.01)
                {
                    txtTubeFF.Foreground = Brushes.Red;
                    Comment("Fouling Factor Too High", "ATTENTION");
                }
                else
                {
                    txtTubeFF.Foreground = Brushes.Blue;
                }
                myLab.CFT = UnitConverter.ToStandardTemperature(double.Parse(txtCFT.Text), cmbUnitOfCFTemp.SelectedIndex);
                myLab.SVF = UnitConverter.ToStandardVolumeFlow(double.Parse(txtShellFlow.Text), cmbUnitOfShellVFlow.SelectedIndex);
                
                myLab.HFT = UnitConverter.ToStandardTemperature(double.Parse(txtHFT.Text), cmbUnitOfHFTemp.SelectedIndex);
                myLab.TVF = UnitConverter.ToStandardVolumeFlow(double.Parse(txtTubeFlow.Text), cmbUnitOfTubeVFlow.SelectedIndex);

                if (myLab.CFT < myLab.HFT)
                {
                    IsReady = true;
                }
                else
                {
                    MessageBox.Show("Hot Tank Temperature must be greater than Cold Tank Temperature", "UNABLE TO COMPLETE TASK");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect/Incomplete Input                 ", "UNABLE TO COMPLETE TASK");
            }
           
            if (IsReady && IsNotRunning)
            {
              
                txtT1.Clear();
                txtT2.Clear();
                txtT3.Clear();
                txtT4.Clear();
                lblT1Unit.Content = "";
                lblT2Unit.Content = "";
                lblT3Unit.Content = "";
                lblT4Unit.Content = "";
                IsNotRunning = false;
              
                txtShellFlow.IsReadOnly = true;
                txtTubeFlow.IsReadOnly = true;
                txtCFT.IsReadOnly = true;
                txtHFT.IsReadOnly = true;
                txtShellFF.IsReadOnly = true;
                txtTubeFF.IsReadOnly = true;
                TankHottank.On();
                TankcoldTank.On();
                TimerTick();
           
                btnStart.Content = "STOP";
                SwitchValve(1);
                canrecord = false;
                 CalculatePerformance();
                if (calculator.hasvalue)
                {
                    
                    Animate();
                }
                if (textaltered)
                {
                    changemade = true;
                }
                
            }
            else 
            {
                IsNotRunning = true;
                TankcoldTank.Off();
                TankHottank.Off();
                txtShellFlow.IsReadOnly = false;
                txtTubeFlow.IsReadOnly = false;
                txtCFT.IsReadOnly = false;
                txtHFT.IsReadOnly = false;
                txtShellFF.IsReadOnly = false;
                txtTubeFF.IsReadOnly = false;
                btnStart.Content = "START";
                TimerCount = 0;
                SwitchValve(0);
            }
        }

        private void ShowResult()
        {
            if (calculator.hasvalue)
            {
                txtT1.Text = Math.Round(UnitConverter.ToUnitTemperature(calculator.Tsi, unitsystem.UnitofTemperature), 2).ToString();
                txtT2.Text = Math.Round(UnitConverter.ToUnitTemperature(calculator.Tti, unitsystem.UnitofTemperature), 2).ToString();
                txtT3.Text = Math.Round(UnitConverter.ToUnitTemperature(calculator.Tto, unitsystem.UnitofTemperature), 2).ToString();
                txtT4.Text = Math.Round(UnitConverter.ToUnitTemperature(calculator.Tso, unitsystem.UnitofTemperature), 2).ToString();

                lblT1Unit.Content = UnitConverter.ShowUnitTemperature(unitsystem.UnitofTemperature);
                lblT2Unit.Content = UnitConverter.ShowUnitTemperature(unitsystem.UnitofTemperature);
                lblT3Unit.Content = UnitConverter.ShowUnitTemperature(unitsystem.UnitofTemperature);
                lblT4Unit.Content = UnitConverter.ShowUnitTemperature(unitsystem.UnitofTemperature);
            }
            if (calculator.valueHasError)
            {
                Comment("Final Temperature Outside Single Phase Region", "RESULT LIKELY TO HAVE ERROR!!");
            }
        }

        private void InitializeExchangerDesigner()
        {
            ExchangerDesigner E_design = new ExchangerDesigner();
            E_design.Owner = this;
            E_design.ShowDialog();
        }
        private void menuDesignExchanger_Click_1(object sender, RoutedEventArgs e)
        {
            InitializeExchangerDesigner();
        }

        private void btnSwitchSides_Click_1(object sender, RoutedEventArgs e)
        {
            Switchtank();
        }
        private void Switchtank()
        {
            if (IsNotRunning)
            {
                canmain.Children.Clear();
                if (myLab.HotIsToShell)
                {
                    myLab.HotIsToShell = false;
                    canShellSideTank.Children.Clear();
                    canTubeideTank.Children.Clear();       
                    canShellSideTank.Children.Add(TankcoldTank);
                    canTubeideTank.Children.Add(TankHottank);

                   
                }
                else
                {
                    myLab.HotIsToShell = true;
                    canShellSideTank.Children.Clear();
                    canTubeideTank.Children.Clear();
                    canShellSideTank.Children.Add(TankHottank);
                    canTubeideTank.Children.Add(TankcoldTank);


              
                }
                changemade = true;
                Animate1();
            }
            else
            {
                Comment("NOT DONE!! Stop experiment to switch");
            }
        }

        private void FixTankSides()
        {
            canmain.Children.Clear();
            TankcoldTank.txtVolumeChange.Text=Math.Round((UnitConverter.ToUnitVolume(myLab.HeaterVolume, unitsystem.UnitofVolume)),4).ToString();
            TankHottank.txtVolumeChange.Text = Math.Round((UnitConverter.ToUnitVolume(myLab.HeaterVolume, unitsystem.UnitofVolume)), 4).ToString();
               if (!myLab.HotIsToShell)
                {             
                    canShellSideTank.Children.Clear();
                    canTubeideTank.Children.Clear();
                    canShellSideTank.Children.Add(TankcoldTank);
                    canTubeideTank.Children.Add(TankHottank);

                  
                }
                else
                {                  
                    canShellSideTank.Children.Clear();
                    canTubeideTank.Children.Clear();
                    canShellSideTank.Children.Add(TankHottank);
                    canTubeideTank.Children.Add(TankcoldTank);

                }
            
        }

        private void menuItemPrefferedUnit_Click_1(object sender, RoutedEventArgs e)
        {
            if (IsNotRunning)
            {
                UnitManager unit = new UnitManager();
                unit.Owner = this;
                unit.Closed += unit_Closed;
                unit.ShowDialog();
                unit.Close();
            }
            else
            {
                Comment("NOT DONE!! Stop experiment before trying to alter units");
            }
        }


        void unit_Closed(object sender, EventArgs e)
        {
            SetInitialUnits();
        }

   

        private void btnStore_Click_1(object sender, RoutedEventArgs e)
        {
            if (IsNotRunning)
            {
                store  = new Store(true);
                store.Owner = this;
                store.LoadLab(myLab);
                store.Closed += store_Closed;
                store.ShowDialog();
            }
            else
            {
                Comment("NOT DONE!! Stop experiment to access store");
            }
        }

        void store_Closed(object sender, EventArgs e) 
        {
            Store store = sender as Store;
            myLab.ColdFluid = store.txtCF.Text;
            myLab.ExchangerInUse = store.txtEX.Text;
            myLab.HotFluid = store.txtHF.Text;
            changemade = true;
            Comment(myLab.ExchangerInUse, myLab.HotFluid, myLab.ColdFluid);
        }

        private void menuDesignFluid_Click_1(object sender, RoutedEventArgs e)
        {
            DesignFluid();
        }

        private void DesignFluid()
        {
            FluidDefiner fluid = new FluidDefiner();
            fluid.Owner = this;
            fluid.ShowDialog();
        }

        private void menuFittings_Click_1(object sender, RoutedEventArgs e)
        {
            NozzleDefiner nozzle = new NozzleDefiner();
            nozzle.Owner = this;
            nozzle.txtShellNozzle.Text = UnitConverter.ToUnitLength(myLab.shellNozzleDiameter, unitsystem.UnitofDiameter).ToString();
            nozzle.txtTubeNozzle.Text = UnitConverter.ToUnitLength(myLab.tubeNozzleDiameter, unitsystem.UnitofDiameter).ToString();
            nozzle.txthHeaterVolume.Text = UnitConverter.ToUnitVolume(myLab.HeaterVolume, unitsystem.UnitofVolume).ToString();
            nozzle.txtHeaterPower.Text = UnitConverter.ToUnitEnergyFlow(myLab.HeaterPower, unitsystem.UnitofHeatTransfer).ToString();
            nozzle.Closed += nozzle_Closed;
            nozzle.ShowDialog();
        }

        void nozzle_Closed(object sender, EventArgs e)
        { NozzleDefiner noz = sender as NozzleDefiner;
            if (noz.madeChange)
            {
                myLab.shellNozzleDiameter = noz.ShellNozzleD;
                myLab.tubeNozzleDiameter = noz.TubeNozzleD;
                myLab.HeaterPower = noz.heaterPower;
                myLab.HeaterVolume = noz.heatervolume;
                TankcoldTank.txtVolumeChange.Text = Math.Round((UnitConverter.ToUnitVolume(myLab.HeaterVolume, unitsystem.UnitofVolume)), 4).ToString();
                TankHottank.txtVolumeChange.Text = Math.Round((UnitConverter.ToUnitVolume(myLab.HeaterVolume, unitsystem.UnitofVolume)), 4).ToString();
            }
        }

        private void btnResult_Click_1(object sender, RoutedEventArgs e)
        {
            ResultWindow result = new ResultWindow();
            result.Owner = this;
            result.ColumnA.Header += " (" + UnitConverter.ShowUnitArea(unitsystem.UnitofArea) + ")";
            result.Columnhi.Header += " (" + UnitConverter.ShowUnitHeatTransferCoefficientI(unitsystem.UnitofU) + ")";
            result.Columnho.Header += " (" + UnitConverter.ShowUnitHeatTransferCoefficientI(unitsystem.UnitofU) + ")";
            result.ColumnU.Header += " (" + UnitConverter.ShowUnitHeatTransferCoefficientI(unitsystem.UnitofU) + ")";
            result.ColumnDPi.Header += " (" + UnitConverter.ShowUnitPressure(unitsystem.UnitofPressure) + ")";
            result.ColumnDPo.Header += " (" + UnitConverter.ShowUnitPressure(unitsystem.UnitofPressure) + ")";
            result.ColumnQ.Header += " (" + UnitConverter.ShowUnitEnergyFlow(unitsystem.UnitofHeatTransfer) + ")";
            result.ColumnSVF.Header += " (" + UnitConverter.ShowUnitVolumeFlow(unitsystem.UnitofVolumetricFlowrate) + ")";
            result.ColumnTVF.Header += " (" + UnitConverter.ShowUnitVolumeFlow(unitsystem.UnitofVolumetricFlowrate) + ")";
            result.ColumnSMF.Header += " (" + UnitConverter.ShowUnitMassFlow(unitsystem.UnitofMassflow) + ")";
            result.ColumnTMF.Header += " (" + UnitConverter.ShowUnitMassFlow(unitsystem.UnitofMassflow) + ")";
            result.ColumnThi.Header += " (" + UnitConverter.ShowUnitTemperature(unitsystem.UnitofTemperature) + ")";
            result.ColumnTci.Header += " (" + UnitConverter.ShowUnitTemperature(unitsystem.UnitofTemperature) + ")";
            result.ColumnTho.Header += " (" + UnitConverter.ShowUnitTemperature(unitsystem.UnitofTemperature) + ")";
            result.ColumnTco.Header += " (" + UnitConverter.ShowUnitTemperature(unitsystem.UnitofTemperature) + ")";

            foreach (Run item in myLab.runs)
            {
                Run runresult = new Run()
                {
                    runNo = item.runNo,
                    hi =Math.Round( UnitConverter.ToUnitHeatTransferCoefficient(item.hi, unitsystem.UnitofU),4),
                    ho = Math.Round( UnitConverter.ToUnitHeatTransferCoefficient(item.ho, unitsystem.UnitofU),4),
                    U = Math.Round( UnitConverter.ToUnitHeatTransferCoefficient(item.U, unitsystem.UnitofU),4),
                    A = Math.Round( UnitConverter.ToUnitArea(item.A, unitsystem.UnitofArea),4),
                    DPi = Math.Round( UnitConverter.ToUnitPressure(item.DPi, unitsystem.UnitofPressure),4),
                    DPo = Math.Round( UnitConverter.ToUnitPressure(item.DPo, unitsystem.UnitofPressure),4),
                    Q = Math.Round( UnitConverter.ToUnitEnergyFlow(item.Q, unitsystem.UnitofHeatTransfer),4),
                    SVF = Math.Round( UnitConverter.ToUnitVolumeFlow(item.SVF, unitsystem.UnitofVolumetricFlowrate),4),
                    TVF = Math.Round( UnitConverter.ToUnitVolumeFlow(item.TVF, unitsystem.UnitofVolumetricFlowrate),4),
                    SMF = Math.Round( UnitConverter.ToUnitMassFlow(item.SMF, unitsystem.UnitofMassflow),4),
                    TMF = Math.Round( UnitConverter.ToUnitMassFlow(item.TMF, unitsystem.UnitofMassflow),4),

                    REi = item.REi,
                    REo = item.REo,
                    SF = item.SF,
                    TF = item.TF,
                    HF = item.HF,

                    Thi = Math.Round( UnitConverter.ToUnitTemperature(item.Thi, unitsystem.UnitofTemperature),4),
                    Tho = Math.Round( UnitConverter.ToUnitTemperature(item.Tho, unitsystem.UnitofTemperature),4),
                    Tci = Math.Round( UnitConverter.ToUnitTemperature(item.Tci, unitsystem.UnitofTemperature),4),
                    Tco = Math.Round( UnitConverter.ToUnitTemperature(item.Tco, unitsystem.UnitofTemperature),4),
                    EXchanger=item.EXchanger
          
                };
                result.dataGridResult.Items.Add(runresult);
            }
            result.ShowDialog();
        }
        int i;
        private void btnRecord_Click_1(object sender, RoutedEventArgs e)
        {
            if (!IsNotRunning)
            {
                if (calculator.hasvalue)
                {
                    if (canrecord)
                    {
                        Run run = new Run();
                        if (myLab.runs.Count != 0)
                        {
                            i = myLab.runs.Count + 1;
                        }
                        else
                        {
                            i = 1;
                        }
                        run.runNo = i;
                        i++;
                        run.REi = Math.Round(calculator.REi, 4);
                        run.REo = Math.Round(calculator.REo, 4);
                        run.SF = calculator.ShellSideFluid;
                        run.TF = calculator.TubeSideFluid;
                        run.HF = myLab.HotFluid;

                        run.hi = Math.Round(calculator.hi, 4);
                        run.ho = Math.Round(calculator.ho, 4);
                        run.U = Math.Round(calculator.U, 4);
                        run.A = Math.Round(calculator.He.A, 4);
                        run.DPi = Math.Round(calculator.DPi, 4);
                        run.DPo = Math.Round(calculator.DPo, 4);
                        run.Q = Math.Round(calculator.Q, 4);
                        run.SVF = Math.Round(calculator.SVolFlow, 4);
                        run.TVF = Math.Round(calculator.tVolFlow, 4);
                        run.SMF = Math.Round(calculator.sMassFlow, 4);
                        run.TMF = Math.Round(calculator.tMassFlow, 4);


                        if (myLab.HotIsToShell)
                        {
                            run.Thi = Math.Round(calculator.Tsi, 4);
                            run.Tci = Math.Round(calculator.Tti, 4);
                            run.Tco = Math.Round(calculator.Tto, 4);
                            run.Tho = Math.Round(calculator.Tso, 4);

                        }
                        else
                        {
                            run.Thi = Math.Round(calculator.Tti, 4);
                            run.Tci = Math.Round(calculator.Tsi, 4);
                            run.Tho = Math.Round(calculator.Tto, 4);
                            run.Tco = Math.Round(calculator.Tso, 4);

                        }

                        // these variables do not appear in result sheet
                        run.Tsi = Math.Round(calculator.Tsi, 4);
                        run.Tto = Math.Round(calculator.Tto, 4);
                        run.Tso = Math.Round(calculator.Tso, 4);
                        run.Tti = Math.Round(calculator.Tti, 4);
                        run.SFF = myLab.shellFF;
                        run.TFF = myLab.tubeFF;
                        run.EXchanger = myLab.ExchangerInUse;
                        //


                        myLab.runs.Add(run);
                        Comment("VALUES RECORDED");
                        changemade = true;
                    }
                    else
                    {
                        Comment("NOT DONE!! Wait for Thermometer readings to appear");
                    }
                }
                else
                {
                    Comment("NOT DONE!! Temperature out of single phase range");
                }
            }
            else
            {
                Comment("NOT DONE!! Start experiment to record");
            }
        }

        private void menuNew_Click_1(object sender, RoutedEventArgs e)
        {
            NewExperiment();
        }

        private void menuOpen_Click_1(object sender, RoutedEventArgs e)
        {
            OpenExperiment();
        }

        private void menuSave_Click_1(object sender, RoutedEventArgs e)
        {
            SaveExperiment();
        }

        private void menuSaveAs_Click_1(object sender, RoutedEventArgs e)
        {
            SaveExperimentAs();
        }

        private void menuExit_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtHFT_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            textaltered = true;
        }

        private void txtCFT_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            textaltered = true;
        }

        private void txtShellFlow_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            textaltered = true;
        }

        private void txtTubeFlow_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            textaltered = true;
        }

        private void txtShellFF_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            textaltered = true;
        }

        private void menuitemCaseStudyVariables_Click_1(object sender, RoutedEventArgs e)
        {
            if (IsNotRunning)
            {
                CaseStudy mycasestudy = new CaseStudy();
                mycasestudy.Owner = this;
                mycasestudy.ShowDialog();
            }
            else
            {
                Comment("NOT DONE!! Stop experiment before trying to alter variables");
            }
        }

        private void btnStudy_Click_1(object sender, RoutedEventArgs e)
        {
            if (IsNotRunning)
            {
                try
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + "SETUPFILE002";
                    study = BinarySerialization.ReadFromBinaryFile<CaseStudySystem>(path);
                    CaseStudyWinow newstudy = new CaseStudyWinow(study,myLab);
                    newstudy.Owner = this;
                    newstudy.ShowDialog();
                }
                catch (Exception)
                {
                    CaseStudy mycasestudy = new CaseStudy();
                    mycasestudy.Owner = this;
                    mycasestudy.ShowDialog();
                }
            }
            else
            {
                Comment("NOT DONE!! Stop experiment before conducting case study");
            }
        }

        private void menuHelp_Click_1(object sender, RoutedEventArgs e)
        {
            HELP help = new HELP();
            help.Owner = this;
            help.ShowDialog();
        }

        private void menuRealTime_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeDynamics(true);
        }

        private void menuSteadyState_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeDynamics(false);
        }
        void ChangeDynamics(bool isdynanic)
        {
            if (IsNotRunning)
            {

                if (isdynanic)
                {
                    dynamic = true;
                    txtdynamics.Text = "REAL TIME OPERATION";
                   
                }
                else
                {
                    dynamic = false;
                    txtdynamics.Text = "STEADY STATE OPERATION";
                }
               
            }
            else
            {
              
            }


        }
        void ChangeBackground( int index)
        {  colorindex = index;
            switch (colorindex)
            {
                case 0:
                    canBackground.Background = Brushes.WhiteSmoke;
                    break;
                case 1:
                    canBackground.Background = Brushes.White;
                    break;
                case 2:
                    canBackground.Background = Brushes.Black;
                    break;
                case 3:
                    canBackground.Background = Brushes.SkyBlue;
                    break;
                case 4:
                    canBackground.Background = Brushes.DarkCyan;
                    break;
                default:
                    break;
            }
           
           
        }
        void ChangeEquipmentColour(int index)
        {
            equipmentcoloindex = index;
            switch (equipmentcoloindex)
            {
                case 0:
                    rectExchanger.Fill = Brushes.LightGray;
                    TankcoldTank.can.Fill = Brushes.LightGray;
                    TankHottank.can.Fill = Brushes.LightGray;
                    break;
                case 1:
                     rectExchanger.Fill = Brushes.White;
                     TankcoldTank.can.Fill = Brushes.White;
                     TankHottank.can.Fill = Brushes.White;
                    break;
                case 2:
                    rectExchanger.Fill = Brushes.SandyBrown;
                    TankcoldTank.can.Fill = Brushes.SandyBrown;
                    TankHottank.can.Fill = Brushes.SandyBrown;
                    break;
                case 3:
                    Color color = (Color)ColorConverter.ConvertFromString("#FF3E3E3E");
                      rectExchanger.Fill = new SolidColorBrush(color);
                      TankcoldTank.can.Fill = new SolidColorBrush(color);
                      TankHottank.can.Fill = new SolidColorBrush(color);
                    break;
                default:
                    break;
            }


        }
       

        private void menuWDefault_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeBackground(0);
        }

        private void menuWWhite_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeBackground(1);
        }

        private void menuWDark_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeBackground(2);
        }

        private void menuWSky_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeBackground(3);
        }

        private void menuCyan_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeBackground(4);
        }

        private void menuESteel_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeEquipmentColour(1);
        }

        private void menuEcopper_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeEquipmentColour(2);
        }

        private void menuEDefault_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeEquipmentColour(0);
        }

        private void menuEDark_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeEquipmentColour(3);
        }
      
   
         
    }
}
