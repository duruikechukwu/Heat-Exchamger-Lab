using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
namespace Heat_Exchamger_Lab
{
    class FluidCalculator
    {
        public string name;
        public double T { get; private set; }
        public double Bp { get; set; }
        public double Mp { get; set; }

        public double Cp { get; private set; }
        public double Rho { get; private set; }
        public double K { get; private set; }
        public double Miu { get; private set; }
     
        string text;
        int initialIndex;
        public FluidCalculator(String FluidName, double _T)
        {
            this.name = FluidName;
            this.T = _T;
            
        }
        public bool Calculate()
        {
            GetBPandMp();
            bool result = false;
            try
            {
                double previousT = 0;
                double previousCp = 0;
                double previousMiu = 0;
                double previousRho = 0;
                double previousK = 0;

                Store.CreateConnection();
                initialIndex = 0;
                Store.connect.Open();
                Store.command = Store.connect.CreateCommand();
                text = @"SELECT * FROM " + name;
                Store.command.CommandText = text;
                Store.reader = Store.command.ExecuteReader();
                while (Store.reader.Read())
                {
                    if (initialIndex == 0)
                    {
                        previousT = double.Parse(Store.reader.GetValue(0).ToString());
                        previousCp = double.Parse(Store.reader.GetValue(1).ToString());
                        previousMiu = double.Parse(Store.reader.GetValue(2).ToString());
                        previousRho = double.Parse(Store.reader.GetValue(3).ToString());
                        previousK = double.Parse(Store.reader.GetValue(4).ToString());
                        initialIndex++;
                    }
                    else
                    {
                        double PresentT = double.Parse(Store.reader.GetValue(0).ToString());
                        double presentCp = double.Parse(Store.reader.GetValue(1).ToString());
                        double presentMiu = double.Parse(Store.reader.GetValue(2).ToString());
                        double presentRho = double.Parse(Store.reader.GetValue(3).ToString());
                        double presentK = double.Parse(Store.reader.GetValue(4).ToString());
                      //  MessageBox.Show("T: " + T + " PresentT: " + PresentT + " PrevT: " + previousT);
                        if (T == previousT)
                        {
                            Cp = previousCp;
                            Miu = previousMiu;
                            Rho = previousRho;
                            K = previousK;
                            return true;
                        }
                        else if (T > previousT && T < PresentT)
                        {
                            double a = (T - previousT) / (PresentT - previousT);
                            Cp = (a * (presentCp - previousCp)) + previousCp;
                            Miu = (a * (presentMiu - previousMiu)) + previousMiu;
                            Rho = (a * ( presentRho- previousRho)) + previousRho;
                            K = (a * (presentK - previousK)) + previousK;
                         //   MessageBox.Show("cp: " + Cp + " miu: " + Miu + " rho: " + Rho + " K: " + K);

                            return true;
                  
                        }
                        else
                        {
                            previousT = PresentT;
                            previousCp = presentCp;
                            previousMiu = presentMiu;
                            previousRho = presentRho;
                            previousK = presentK;
                        }
                    }

                }
                Store.connect.Close();
           
            }
            catch (Exception)
            {
                
               
            }
            return result;
        }
        void GetBPandMp()
        {

            Store.CreateConnection();
            Store.connect.Open();

            Store.command = Store.connect.CreateCommand();
            string text = "SELECT * FROM FLUIDNAMES WHERE FLUID = '" + name + "'";
            Store.command.CommandText = text;
            Store.reader = Store.command.ExecuteReader();
            while (Store.reader.Read())
            {
                Bp = double.Parse(Store.reader.GetValue(1).ToString());
                Mp = double.Parse(Store.reader.GetValue(2).ToString());
               
            }
            Store.connect.Close();
        }
        public double CalculateViscosity(double _Tw)
        {
            double viscosity = Miu;
            try
            {
                double previousT = 0;
                double previousMiu = 0;

                Store.CreateConnection();
                initialIndex = 0;
                Store.connect.Open();
                Store.command = Store.connect.CreateCommand();
                text = @"SELECT * FROM " + name;
                Store.command.CommandText = text;
                Store.reader = Store.command.ExecuteReader();
                while (Store.reader.Read())
                {
                    if (initialIndex == 0)
                    {

                        previousMiu = double.Parse(Store.reader.GetValue(2).ToString());
                        previousT = double.Parse(Store.reader.GetValue(0).ToString());
                        initialIndex++;
                    }
                    else
                    {
                        double PresentT = double.Parse(Store.reader.GetValue(0).ToString());
                        previousMiu = double.Parse(Store.reader.GetValue(2).ToString());
                        if (_Tw == previousT)
                        {
                    
                            viscosity = previousMiu;
                            return viscosity;
              
                        }
                        else if (_Tw > previousT && _Tw < PresentT)
                        {
                            viscosity = (((_Tw - previousT) / (PresentT - previousT)) * (double.Parse(Store.reader.GetValue(2).ToString()) - previousMiu)) + previousMiu;
                            return viscosity;
                        }
                        else
                        {
                            previousT = PresentT;
                     
                        }
                    }

                }
                Store.connect.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
               
            }
            return viscosity;
        }

        public bool CalculateDensityAndSpecificHeat()
        {
                double previousT = 0;
                double previousCp = 0;
                double previousRho = 0;


                bool result = false;
              
           try
           {
                initialIndex = 0;
                Store.CreateConnection();
                Store.connect.Open();
                Store.command = Store.connect.CreateCommand();
                text = @"SELECT * FROM " + name;
                Store.command.CommandText = text;
                Store.reader = Store.command.ExecuteReader();
                while (Store.reader.Read())
                {
                    if (initialIndex == 0)
                    {
                        previousT = double.Parse(Store.reader.GetValue(0).ToString());
                        previousCp = double.Parse(Store.reader.GetValue(1).ToString());
                        previousRho = double.Parse(Store.reader.GetValue(3).ToString());          
                        initialIndex++;
                    }
                    else
                    {
                        double PresentT = double.Parse(Store.reader.GetValue(0).ToString());
                        if (T == previousT)
                        {
                            Cp = previousCp;       
                            Rho = previousRho;
                            return true;
                        }
                        else if (T > previousT && T < PresentT)
                        {
                            double a = (T - previousT) / (PresentT - previousT);
                            Cp = (a * (double.Parse(Store.reader.GetValue(1).ToString()) - previousCp)) + previousCp;
                            Rho = (a * (double.Parse(Store.reader.GetValue(3).ToString()) - previousRho)) + previousRho;
                            return true;
                        }
                        else
                        {
                            previousT = PresentT;
                        }
                    }

                }
                Store.connect.Close();
            
            }
            catch (Exception)
            {
                
               
            }
            return result;
        }

        public static double GetWaterDensity(double _T)
        {
            double WaterDensity=62.42;
            try
            {
                
                double previousT = 0;
                double previousRho = 0;
                Store.CreateConnection();
                int Index = 0;
                Store.connect.Open();
                Store.command = Store.connect.CreateCommand();
                string commtext = @"SELECT * FROM Water";
                Store.command.CommandText = commtext;
                Store.reader = Store.command.ExecuteReader();
                while (Store.reader.Read())
                {
                    if (Index == 0)
                    {
                        previousRho = double.Parse(Store.reader.GetValue(3).ToString());
                        previousT = double.Parse(Store.reader.GetValue(0).ToString());
                
                     Index++;
                    }
                    else
                    {
                        double PresentT = double.Parse(Store.reader.GetValue(0).ToString());
                        if (_T == previousT)
                        {
                          
                            WaterDensity = previousRho;
                            return WaterDensity;
                        }
                        else if (_T > previousT && _T < PresentT)
                        {

                            WaterDensity = (((_T - previousT) / (PresentT - previousT)) * (double.Parse(Store.reader.GetValue(3).ToString()) - previousRho)) + previousRho;
                            return WaterDensity;
                        }
                        else
                        {
                            previousT = PresentT;
                        }
                    }

                }
                Store.connect.Close();
            }
            catch (Exception)
            {

                
            }
            return WaterDensity; 
        }
    
    }
}
