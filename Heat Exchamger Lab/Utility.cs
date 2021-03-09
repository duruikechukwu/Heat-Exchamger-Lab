using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Heat_Exchamger_Lab
{
    class Utility
    {
    }
    
    public static class BinarySerialization
    {
        public static void WriteToBinaryFile<T>(string filepath, T objecttoWrite, bool Append = false)
        {
            using (Stream stream = File.Open(filepath, Append ? FileMode.Append : FileMode.Create, FileAccess.ReadWrite))
            {
                var BinaryFormater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                BinaryFormater.Serialize(stream, objecttoWrite);

            }
        }
        public static T ReadFromBinaryFile<T>(string filepath)
        {
            using (Stream stream = File.Open(filepath, FileMode.Open))
            {
                var BinaryFormater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)BinaryFormater.Deserialize(stream);
            }
        }
    }

    [Serializable]
    public class UnitSystem
    {
        public static string UnitSetupPath { get; set; }
        public  int UnitofArea { get; set; }
        public  int UnitofBaffleSpace { get; set; }
        public  int UnitofClearance { get; set; }
        public  int UnitofDensity { get; set; }
        public  int UnitofDiameter { get; set; }
        public  int UnitofU { get; set; }
        public  int UnitofLenght { get; set; }
        public  int UnitofMassflow { get; set; }
        public  int  UnitofPitch { get; set; }
        public  int UnitofPressure { get; set; }
        public  int UnitofCp { get; set; }
        public  int UnitofTemperature { get; set; }
        public  int UnitofK { get; set; }
        //public  int UnitofTime { get; set; }
        public  int  UnitofViscosity { get; set; }
        public  int UnitofVolumetricFlowrate { get; set; }
        public int UnitofVolume { get; set; }
        public int UnitofHeatTransfer { get; set; }
        public UnitSystem()
        {

        }
    }
    [Serializable]
    public class CaseStudySystem
    {
        public CaseStudySystem()
        {

        }
        public string setuppath { get; set; }
        public int IndependependentPlotVardent { get; set; }
        public int dependentPlotVar { get; set; }
     
    }
    [Serializable]
    public class Settings
    {
        public bool isdynamic { get; set; }
        public int backcolor { get; set; }
        public int equipmentcolor { get; set; }
        public Settings()
        {
           
        }
                

    }


    public static class UnitConverter
    {
        public static double ToStandardLength(double variable,int index )
        {
            double value=variable;
            switch (index)
            {
                case 0:
                    value /= 0.3048;
                    break;
                case 2:
                    value /= 304.8;
                    break;
                case 3:
                    value /=12.0;
                    break;
                default:
                    break;
            }
            return value;
        }

        public static double ToStandardTime(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value /=3600.0;
                    break;
                case 1:
                    value /= 60.0;
                    break;
                default:
                    break;
            }
            return value;
        }

        public static double ToStandardVolumeFlow(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value /= 0.007865791;
                    break;
                case 1:
                    value /= 0.47194746;
                    break;
                case 2:
                    value /= 28.3168476;
                    break;
                case 3:
                    value /= 0.000007865791;
                    break;
                case 4:
                    value /= 0.00047194746;
                    break;
                case 5:
                    value /= 0.0283168476;
                    break;
                case 6:
                    value *= 3600.0;
                    break;
                case 7:
                    value *= 60.0;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToStandardTemperature(double variable, int index)
        {
            double value = variable;
            
            switch (index)
            {
                case 0:
                    value = 1.8 * value + 32;
                    break;
                case 1:
                    value = 1.8 * (value - 273) + 32;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToStandardPressure(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 1:
                    value /= 0.1450377346;
                    break;
                case 2:
                    value *= 6.89475744;
                    break;
                case 3:
                    value *= 14.696;
                    break;
                case 4:
                    value /= 144.0;
                    break;
                case 5:
                    value *= 14.50382433;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToStandardDensity(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value /= 16.01846;
                    break;
                case 1:
                    value /= 0.01601846;
                    break;
                case 2:
                    value *= 28.31685;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToStandardSpecificHeat(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value /= 4.1868;
                    break;
                case 2:
                    value /= (4.1868 / 4.184);
                    break;
                case 3:
                    value /= (4.1867 / 3600);
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToStandardViscosity(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value /= (0.0004133789 / 0.001);
                    break;
                case 1:
                    value /= 0.0004133789;
                    break;
                case 3:
                    value *= 3600.0;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToStandardThermalConductivity(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value /= 1.730735;
                    break;
                case 1:
                    value /= 0.001730735;
                    break;
                case 3:
                    value /= (1.730735 / 1.162222);
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToStandsrdVolume(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value /= 28.31685;
                    break;
                case 1:
                    value /= 0.02831685;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToStandardEnergyFlow(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value *= 3600.0;
                    break;
                case 1:
                    value *= 60.0;
                    break;
                case 3:
                    value /= (1.055056 / 3600.0);
                    break;
                case 4:
                    value /= 1.055056 / 60.0;
                    break;
                case 5:
                    value /= 1.055056;
                    break;
                case 6:
                    value /= (1.055056 / 15062.4);
                    break;
                case 7:
                    value /= (1.055056 / 251.04);
                    break;
                case 8:
                    value /= 1.055056 / 4.184;
                    break;
                default:
                    break;
            }
            return value;
        }


        public static double ToUnitLength(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value *= 0.3048;
                    break;
                case 2:
                    value *= 304.8;
                    break;
                case 3:
                    value *= 12.0;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToUnitVolumeFlow(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value *= 0.007865791;
                    break;
                case 1:
                    value *= 0.47194746;
                    break;
                case 2:
                    value *= 28.3168476;
                    break;
                case 3:
                    value *= 0.000007865791;
                    break;
                case 4:
                    value *= 0.00047194746;
                    break;
                case 5:
                    value *= 0.0283168476;
                    break;
                case 6:
                    value /= 3600.0;
                    break;
                case 7:
                    value /= 60.0;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToUnitTemperature(double variable, int index)
        {
            double value = variable;
           
            switch (index)
            {
                case 0:
                    value = (5.0 / 9.0) *(value - 32);
                  
                    break;
                case 1:
                    value = (5.0 / 9.0) * (value - 32) + 273;
                    
                    break;
                default:
                    break;
            }
            
            
            return value;
        }
        public static double ToUnitPressure(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 1:
                    value *= 0.1450377346;
                    break;
                case 2:
                    value /= 6.89475744;
                    break;
                case 3:
                    value /= 14.696;
                    break;
                case 4:
                    value *= 144.0;
                    break;
                case 5:
                    value /= 14.50382433;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToUnitArea(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value *= 0.09290304;
                    break;
                case 2:
                    value *= 144.0;
                    break;
                case 3:
                    value *= 92903.04;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToUnitVolume(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value *= 28.31685;
                    break;
                case 1:
                    value *= 0.02831685;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToUnitDensity(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value *= 16.01846;
                    break;
                case 1:
                    value *= 0.01601846;
                    break;
                case 2:
                    value /= 28.31685;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToUnitSpecificHeat(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value *= 4.1868;
                    break;
                case 2:
                    value *= (4.1868 / 4.184);
                    break;
                case 3:
                    value *= (4.1867 / 3600);
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToUnitViscosity(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value *= (0.0004133789 / 0.001);
                    break;
                case 1:
                    value *= 0.0004133789;
                    break;
                case 3:
                    value /= 3600.0;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToUnitThermalConductivity(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value *= 1.730735;
                    break;
                case 1:
                    value *= 0.001730735;
                    break;
                case 3:
                    value *= (1.730735 / 1.162222);
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToUnitHeatTransferCoefficient(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value *= 5.678263;
                    break;
                case 1:
                    value *= 0.005678263;
                    break;
                case 3:
                    value *= (0.005678263/0.001162222);
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToUnitMassFlow(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 1:
                    value /= 60.0;
                    break;
                case 2:
                    value /= 3600.0;
                    break;
                case 3:
                    value *= 0.45359244;
                    break;
                case 4:
                    value *= 0.007559874;
                    break;
                case 5:
                    value *= 0.0001259979;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToUnitEnergyFlow(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value /= 3600.0;
                    break;
                case 1:
                    value /= 60.0;
                    break;
                case 3:
                    value *= (1.055056 / 3600.0);
                    break;
                case 4:
                    value *= 1.055056 / 60.0;
                    break;
                case 5:
                    value *= 1.055056;
                    break;
                case 6:
                    value *= (1.055056 / 15062.4);
                    break;
                case 7:
                    value *= (1.055056 / 251.04);
                    break;
                case 8:
                    value *= 1.055056 / 4.184;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static double ToUnitTime(double variable, int index)
        {
            double value = variable;
            switch (index)
            {
                case 0:
                    value *= 3600.0;
                    break;
                case 1:
                    value *= 60.0;
                    break;
                default:
                    break;
            }
            return value;
        }

        public static string ShowUnitime( int index)
        {
            string value = "hr";
            switch (index)
            {
                case 0:
                    value = "s";
                    break;
                case 1:
                    value = "min";
                    break;
                default:
                    break;
            }
            return value;
        }
        public static string ShowUnitLength(int index)
        {
            string value = "ft";
            switch (index)
            {
                case 0:
                    value ="m";
                    break;
                case 2:
                    value = "mm";
                    break;
                case 3:
                    value = "in";
                    break;
                default:
                    break;
            }
            return value;
        }
        public static string ShowUnitVolumeFlow( int index)
        {
            string value = "ft^3/hr";
            switch (index)
            {
                case 0:
                    value = "L/s";
                    break;
                case 1:
                    value = "L/min";
                    break;
                case 2:
                    value ="L/hr";
                    break;
                case 3:
                    value = "m^3/s";
                    break;
                case 4:
                    value = "m^3/min";
                    break;
                case 5:
                    value = "m^3/hr";
                    break;
                case 6:
                    value = "ft^3/s";
                    break;
                case 7:
                    value = "ft^3/min";
                    break;
                default:
                    break;
            }
            return value;
        }
        public static string ShowUnitTemperature( int index)
        {
            string value = "F";
            switch (index)
            {
                case 0:
                    value = "C";
                    break;
                case 1:
                    value = "K";
                    break;
                default:
                    break;
            }
            return value;
        }
        public static string ShowUnitPressure(int index)
        {
            string value = "psi";
            switch (index)
            {
                case 1:
                    value = "Pa";
                    break;
                case 2:
                    value = "KPa";
                    break;
                case 3:
                    value = "Atm";
                    break;
                case 4:
                    value = "lbf/ft^3";
                    break;
                case 5:
                    value ="Bar";
                    break;
                default:
                    break;
            }
            return value;
        }
        public static string ShowUnitArea(int index)
        {
            string value = "ft^2";
            switch (index)
            {
                case 0:
                    value = "m^2";
                    break;
                case 2:
                    value = "in^2";
                    break;
                case 3:
                    value = "mm^2";
                    break;
                default:
                    break;
            }
            return value;
        }
        public static string ShowUnitVolume(int index)
        {
            string value = "ft^3";
            switch (index)
            {
                case 0:
                    value = "L";
                    break;
                case 1:
                    value = "m^3";
                    break;
                default:
                    break;
            }
            return value;
        }
        public static string ShowUnitDensity( int index)
        {
            string value = "lbm/ft^3";
            switch (index)
            {
                case 0:
                    value = "kg/m^3";
                    break;
                case 1:
                    value = "kg/L";
                    break;
                case 2:
                    value = "lbm/L";
                    break;
                default:
                    break;
            }
            return value;
        }
        public static string ShowUnitSpecificHeat(int index)
        {
            string value = "Btu/lbm.F";
            switch (index)
            {
                case 0:
                    value = "KJ/Kg.K";
                    break;
                case 2:
                    value = "Kcal/Kg.K";
                    break;
                case 3:
                    value = "Kwhr/Kg.K";
                    break;
                default:
                    break;
            }
            return value;
        }
        public static string ShowUnitViscosity(int index)
        {
            string value = "lbm/ft/hr";
            switch (index)
            {
                case 0:
                    value = "cp";
                    break;
                case 1:
                    value = "Pa.s";
                    break;
                case 3:
                    value = "lbm/ft.s" ;
                    break;
                default:
                    break;
            }
            return value;
        }
        public static string ShowUnitThermalConductivity(int index)
        {
            string value = "Btu/hr.ft.F";
            switch (index)
            {
                case 0:
                    value = "W/m.K";
                    break;
                case 1:
                    value = "KW/m.K";
                    break;
                case 3:
                    value = "Kcal/hr.ft.F";
                    break;
                default:
                    break;
            }
            return value;
        }
        public static string ShowUnitHeatTransferCoefficientI(int index)
        {
            string value = "Btu/hr.ft^2.F";
            switch (index)
            {
                case 0:
                    value = "W/m^2.K";
                    break;
                case 1:
                    value = "Kw/m^2.K";
                    break;
                case 3:
                    value = "Kcal/hr.m^2.K";
                    break;
                default:
                    break;
            }
            return value;
        }
        public static string ShowUnitMassFlow(int index)
        {
            string value = "lbm/hr";
            switch (index)
            {
                case 1:
                    value = "lbm/min";
                    break;
                case 2:
                    value = "lbm/s";
                    break;
                case 3:
                    value = "Kg/hr";
                    break;
                case 4:
                    value = "Kg/min";
                    break;
                case 5:
                    value = "Kg/s";
                    break;
                default:
                    break;
            }
            return value;
        }
        public static string ShowUnitEnergyFlow(int index)
        {
            string value = "Btu/hr";
            switch (index)
            {
                case 0:
                    value = "Btu/s";
                    break;
                case 1:
                    value = "Btu/min";
                    break;
                case 3:
                    value = "KJ/s";
                    break;
                case 4:
                    value = "KJ/min";
                    break;
                case 5:
                    value ="KJ/hr";
                    break;
                case 6:
                    value = "Kcal/hr";
                    break;
                case 7:
                    value = "Kcal/min";
                    break;
                case 8:
                    value ="Kcal/hr";
                    break;
                default:
                    break;
            }
            return value;
        }
    }

    [Serializable]
    public class Lab
    {
        public List<Run> runs;
        public string ExchangerInUse { get; set; }
        public string HotFluid { get; set; }
        public string ColdFluid { get; set; }
        public bool HotIsToShell { get; set; }
        public double shellNozzleDiameter { get; set; }
        public double tubeNozzleDiameter { get; set; }

        public double HeaterVolume { get; set; }
        public double HeaterPower { get; set; }

        public double SVF { get; set; }
        public double TVF { get; set; }
        public double CFT { get; set; }
        public double HFT { get; set; }
        public double shellFF { get; set; }
        public double tubeFF { get; set; }
        public int UnitSVF { get; set; }
        public int UnitTVF { get; set; }
        public int UnitCFT { get; set; }
        public int UnitHFT { get; set; }
       

        public Lab()
        {
            runs = new List<Run>();
            HotFluid = "Water";
            ColdFluid = "Water";
            ExchangerInUse = "SE-EX";
            shellNozzleDiameter = 3.068 / 12.0;
            tubeNozzleDiameter = 4.026 / 12.0;

            HeaterVolume = 1.76573312356424;
            HeaterPower = 34121.4115648838;
            shellFF = 0.00;
            tubeFF = 0.00;
            HotIsToShell = true;
            
            

        }
    }

     [Serializable]
    public class Run
    {
        public int runNo { get; set; }
        public double Tsi { get; set; }
        public double Tso { get; set; }
        public double Tti { get; set; }
        public double Tto { get; set; }
        public double SFF { get; set; }
        public double TFF { get; set; }
        public string EXchanger { get; set; }

        public double Thi { get; set; }
        public double Tho { get; set; }
        public double Tci { get; set; }
        public double Tco { get; set; }
        public double hi { get; set; }
        public double ho { get; set; }
        public double U { get; set; }
        public double A { get; set; }
        public double REi { get; set; }
        public double REo { get; set; }
        public double DPi { get; set; }
        public double DPo { get; set; }
        public double Q { get; set; }
        public double SVF { get; set; }
        public double TVF { get; set; }    
        public double SMF { get; set; }
        public double TMF { get; set; }
        public string SF { get; set; }
        public string TF { get; set; }
        public string HF { get; set; }
    }

}
