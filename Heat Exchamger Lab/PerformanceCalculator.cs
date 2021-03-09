using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Heat_Exchamger_Lab
{
    class PerformanceCalculator
    {
        public bool hasvalue { get; private set; }
        public bool valueHasError { get; private set; }
        List<HeatExchanger> exchangers;
        public HeatExchanger He { get; private set; }
        FluidCalculator tFluid;
        FluidCalculator sFluid;
        public string ExchangerName { get; set; }
                                                         //Tube side flow properties
        public string TubeSideFluid { get; set; }
        public double Ttave { get; private set; }
        public double Tto { get; private set; }
        public double tubeff { get;  set; }
        public double Tti { get; set; }

        public double tPressure { get; private set; }
        public double tVolFlow { get; set; }
        public double tMassFlow { get; private set; }
        public double mPerTube { get; private set; }
                                                         //shell side flow properties
        public string ShellSideFluid { get; set; }
        public double Tsave { get; private set; }
        public double Tso { get; private set; }
        public double shellff { get;  set; }
        public double Tsi { get; set; }
        public double sPressure { get; private set; }
        public double SVolFlow { get; set; }
        public double sMassFlow { get; private set; }
        public double j { get; private set; }
        public double hideal { get; private set; }
                                                           // Tube side pressure drop
        public double DPft { get; private set; }
        public double DPrt { get; private set; }
        public double DPnt { get; private set; }
        public double DPi { get; private set; }
                                                
                                                           //shell side pressure drop
        public double DPideal { get; private set; }
        public double DPwideal { get; private set; }
        public double DPfs { get; private set; }
        public double DPcs { get; private set; }
        public double DPws { get; private set; }
        public double DPes { get; private set; }
        public double DPns { get; private set; }
        public double DPo { get; private set; }
                                                        //Tube side dimensionless numbers
        public double REi { get; private set; }
        public double NUi { get; private set; }
        public double PRi { get; private set; }
        public double Gi { get; private set; }
                                                        //shell side dimensionless numbers
        public double REo { get; private set; }
        public double NUo { get; private set; }
        public double PRo { get; private set; }
        public double Go { get; private set; }

        public double hi { get; private set; }
        public double ho { get; private set; }
                                                        // friction factors
        public double fi { get; private set; }
        public double fj { get; private set; }
                                                        // correction factors
        public double Jc { get; private set; }
        public double JL { get; private set; }
        public double RL { get; private set; }
        public double JB { get; private set; }
        public double RB { get; private set; }
        public double JS { get; private set; }
        public double RS { get; private set; }
        public double JR { get; private set; }
                                                         // performance properties
        public double U { get; private set; }
        public double effectiveness { get; private set; }
        public double Cmin { get; private set; }
        public double Cmax { get; private set; }
        public double Qmax { get; private set; }
        public double DTmax { get; private set; }
        public double NTU { get; private set; }
        public double Q { get; private set; }
                                                          //others
        public double Tw { get; private set; }
        double gc = 417000000.0;
        double phi_s;
        double phi_t;
        public double Ct { get; private set; }
        public double Cs { get; private set; }
        

        public PerformanceCalculator()
        {
            
        }

        private void LoadExchanger()
        {         
            exchangers = BinarySerialization.ReadFromBinaryFile<List<HeatExchanger>>(Store.exlocation);
            IEnumerable<HeatExchanger> HES = from exchanger in exchangers where exchanger.Identifier == ExchangerName select exchanger;
                foreach (HeatExchanger item in HES)
                {
                    He = item;
                }
               
        }

       

        public void Calculate(double _Dns, double _Dnt, double _shellFF, double _tubeFF )
        {
            try
            {
                LoadExchanger();
                He.Dns = _Dns;
                He.Dnt = _Dnt;
                shellff = _shellFF;
                tubeff = _tubeFF;
                CalculatePerformance();
            }
            catch (NullReferenceException)
            {

                MessageBox.Show("Could not find heat exchanger named: " + ExchangerName+", Please check STORE","EXCHANGER ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalculatePerformance()
        {
            hasvalue = false;
            valueHasError = false;
            tFluid = new FluidCalculator(TubeSideFluid, Tti);
            sFluid = new FluidCalculator(ShellSideFluid, Tsi); 
            if (tFluid.CalculateDensityAndSpecificHeat() && sFluid.CalculateDensityAndSpecificHeat())
            {               
                Ct = tVolFlow * tFluid.Rho * tFluid.Cp;
                Cs = SVolFlow * sFluid.Rho * sFluid.Cp;
                Cmin = Ct;
                Cmax = Cs;
                if (Cs < Ct)
                {
                    Cmin = Cs;
                    Cmax = Ct;
                }
                DTmax = Math.Abs(Tsi - Tti);
         
                Qmax = Cmin * DTmax;           
           
                // //////////////////////////////////////////////////////NOT IN USE**********
                //if (Cmin == Ct)
                //{
                //    if (Tti > Tsi)
                //    {
                //        Ttave = Tti - (DTmax / 2.0);
                //        Tsave = Tsi + (Ct * DTmax) / (2.0 * Cs);
                //    }
                //    else
                //    {
                //        Ttave = Tti + (DTmax / 2.0);
                //        Tsave = Tsi - (Ct * DTmax) / (2.0 * Cs);
                //    }
                    
                //}
                //else
                //{
                //    if (Tsi>Tti)
                //    {
                //         Tsave = Tsi - (DTmax / 2.0);
                //         Ttave = Tti + (Cs * DTmax) / (2 * Ct); 
                //    }
                //    else
                //    {
                //        Tsave = Tsi + (DTmax / 2.0);
                //        Ttave = Tti - (Cs * DTmax) / (2 * Ct);
                //    }

                //}
                /////////////////////////////////////////////////////////////////////////**********
                Tsave = Tsi;
                Ttave = Tti;
                sFluid = new FluidCalculator(ShellSideFluid, Tsave);
                tFluid = new FluidCalculator(TubeSideFluid, Ttave);
                if (tFluid.Calculate()&&sFluid.Calculate())
                {
                    Estimate_hi();
                    
                    Estimate_ho();
                 
                    Tw = ((hi * Ttave) + ho * (He.Do / He.Di) * Tsave) / (hi + ho * (He.Do / He.Di));
              
                    double Miutw = tFluid.CalculateViscosity(Tw);
                    phi_t = Math.Pow((tFluid.Miu / Miutw), 0.14);
                    double Miusw = sFluid.CalculateViscosity(Tw);
                    phi_s = Math.Pow((sFluid.Miu / Miusw), 0.14);                 
                    hi *= phi_t;
                    if (REi < 10000)
                    {
                        phi_t = Math.Pow((tFluid.Miu / Miutw), 0.25);
                        phi_s = Math.Pow((sFluid.Miu / Miusw), 0.25);
                    }
                    ho *= phi_s;

                
                    Calculate_DPi();
                    Calculate_DPo();
                    

                    U = Math.Pow(((He.Do / (hi * He.Di)) +    ((He.Do * Math.Log(He.Do / He.Di)) / (2 * He.MaterialK))     + (1 / ho)+(tubeff*He.Do/He.Di)+shellff), -1.0);

           
                    ApplyE_NTU();
                    if (Tso>sFluid.Bp||Tso<sFluid.Mp||Tto>tFluid.Bp||Tto<tFluid.Mp)
                    {
                        valueHasError = true;
                    }
                    hasvalue = true;
                }
                else
                {
                    MessageBox.Show("INTERNAL FAILURE", "Could Not Estimate Flow Properties");
                }
            }
            else
            {
                MessageBox.Show("Unable to Estimate Flow Properties \nFluid Starting Temperature(s) Out Of Single Phase Region","TECHNICAL ERROR");
            }
          
        }

        private void ApplyE_NTU()
        {
            NTU = (U * He.A )/ Cmin;
            
            double r = Cmin / Cmax;
            

            double beta=Math.Sqrt(1+Math.Pow(r,2));
            

            effectiveness = 2*Math.Pow((1 + r + (beta *  ((1 + Math.Exp(-beta * NTU)) / (1 - Math.Exp(-beta * NTU)))  )  ), -1.0);
            Q = Qmax * effectiveness;
        
            if (Tti>Tsi)   // tube contains hot fluid
            {
                Tto = Tti - (Q /Ct);
                Tso = (Q /Cs) + Tsi;
                ///////////////////////////////////////////////////////// ENSURE DRIVING FORCE EXIXSTS
                //while (Tto < Tso)
                //{
                //    Q = Q - 0.01 * Q;
                //    Tto = Tti - (Q / Ct);
                //    Tso = (Q / Cs) + Tsi;
                //}
                /////////////////////////////////////////////////////////////////////////////////////////////
            }
            else  // shell contains hot fluid
            {
                Tso = Tsi - (Q /Cs);
                Tto = (Q /Ct) + Tti;
                ///////////////////////////////////////////////////////// ENSURE DRIVING FORCE EXIXSTS
                //while (Tso < Tto)
                //{
                //    Q = Q - 0.01 * Q;
                //    Tso = Tsi - (Q / Cs);
                //    Tto = (Q / Ct) + Tti;
                //}
                /////////////////////////////////////////////////////////////////////////////////////////////
            }
        }

        private void Estimate_hi() 
        {   
                tMassFlow = tFluid.Rho * tVolFlow;
                mPerTube = tMassFlow * He.np / He.nt;              
                REi = (4 * mPerTube) / (Math.PI * He.Di * tFluid.Miu);
                
                PRi = tFluid.Cp * tFluid.Miu / tFluid.K;
                Gi = REi * tFluid.Miu / He.Di;
                if (REi >= 10000)
                {
                    

                    if ((He.L / He.Di) > 10 && (He.L / He.Di) < 60)
                    {
                        NUi = 0.023 * Math.Pow(REi, 0.8) * Math.Pow(PRi, (1.0 / 3.0)) * (1 + Math.Pow((He.L / He.Di), (2.0 / 3.0))); // Miu/Miuw assumed to be 1
                       
                    }
                    else
                    {
                        NUi = 0.023 * Math.Pow(REi, 0.8) * Math.Pow(PRi, (1.0 / 3.0)); // Miu/Miuw assumed to be 1
                      
                    }
                }
                else if (REi > 2100 && REi < 10000)
                {
                    NUi = 0.116 * (Math.Pow(REi, (2.0 / 3.0)) - 125) * Math.Pow(PRi, (1.0 / 3.0)) * (1 + Math.Pow((He.L / He.Di), (2.0 / 3.0))); // Miu/Miuw assumed to be 1
                }
                else if (REi <= 2100)
                {
                    NUi = 1.86 * Math.Pow((REi * PRi * He.Di / He.L), (1.0 / 3.0)); // Miu/Miuw assumed to be 1
                }
                hi = NUi * tFluid.K / He.Di;
              
               
        }

        private void Estimate_ho()
        { 
                double a; double a1 = 0; double a2 = 0; double a3 = 0; double a4 = 0;
                double b; double b1 = 0; double b2 = 0; double b3 = 0; double b4 = 0;

                sMassFlow = SVolFlow * sFluid.Rho;
                
                Go = sMassFlow / He.Sm;

                
                REo = He.Do * Go / sFluid.Miu;
                
                PRo = sFluid.Cp * sFluid.Miu / sFluid.K;
                
                if (He.TubeLayout == 30)
                {
                    a3 = 1.450;
                    a4 = 0.519;
                    b3 = 7.00;
                    b4 = 0.50;
                    if (REo < 10)
                    {
                        a1 = 1.400;
                        a2 = -0.667;
                        b1 = 48.00;
                        b2 = -1.00;
                    }
                    else if (REo >= 10 && REo < 100)
                    {
                        a1 = 1.360;
                        a2 = -0.657;
                        b1 = 45.10;
                        b2 = -0.973;
                    }
                    else if (REo >= 100 && REo < 1000)
                    {
                        a1 = 0.593;
                        a2 = -0.447;
                        b1 = 4.570;
                        b2 = -0.476;
                    }
                    else if (REo >= 1000 && REo < 10000)
                    {
                        a1 = 0.321;
                        a2 = -0.388;
                        b1 = 0.486;
                        b2 = -0.152;
                    }
                    else
                    {
                        a1 = 0.321;
                        a2 = -0.388;
                        b1 = 0.372;
                        b2 = -0.123;
                    }
                }

                else if (He.TubeLayout == 45)
                {
                    a3 = 1.930;
                    a4 = 0.500;
                    b3 = 6.59;
                    b4 = 0.520;
                    if (REo < 10)
                    {
                        a1 = 1.550;
                        a2 = -0.667;
                        b1 = 32.00;
                        b2 = -1.00;
                    }
                    else if (REo >= 10 && REo < 100)
                    {
                        a1 = 0.498;
                        a2 = -0.656;
                        b1 = 26.20;
                        b2 = -0.913;
                    }
                    else if (REo >= 100 && REo < 1000)
                    {
                        a1 = 0.730;
                        a2 = -0.500;
                        b1 = 3.500;
                        b2 = -0.476;
                    }
                    else if (REo >= 1000 && REo < 10000)
                    {
                        a1 = 0.370;
                        a2 = -0.396;
                        b1 = 0.333;
                        b2 = -0.136;
                    }
                    else
                    {
                        a1 = 0.370;
                        a2 = -0.396;
                        b1 = 0.303;
                        b2 = -0.126;
                    }
                }
                else if (He.TubeLayout == 90)
                {
                    a3 = 1.187;
                    a4 = 0.370;
                    b3 = 6.30;
                    b4 = 0.378;
                    if (REo < 10)
                    {
                        a1 = 0.970;
                        a2 = -0.667;
                        b1 = 35.00;
                        b2 = -1.00;
                    }
                    else if (REo >= 10 && REo < 100)
                    {
                        a1 = 0.900;
                        a2 = -0.631;
                        b1 = 32.00;
                        b2 = -0.963;
                    }
                    else if (REo >= 100 && REo < 1000)
                    {
                        a1 = 0.408;
                        a2 = -0.460;
                        b1 = 6.090;
                        b2 = -0.602;
                    }
                    else if (REo >= 1000 && REo < 10000)
                    {
                        a1 = 0.107;
                        a2 = -0.266;
                        b1 = 0.0815;
                        b2 = 0.002;
                    }
                    else
                    {
                        a1 = 0.370;
                        a2 = -0.395;
                        b1 = 0.391;
                        b2 = -0.148;
                    }
                }
                a = a3 / (1 + 0.14 * Math.Pow(REo, a4));
                b = b3 / (1 + 0.14 * Math.Pow(REo, b4));

                fj = b1 * Math.Pow((1.33 / (He.Pt / He.Do)), b) * Math.Pow(REo, b2);
                j = a1 * Math.Pow((1.33 / (He.Pt / He.Do)), a) * Math.Pow(REo, a2);

                hideal = (j * sFluid.Cp * Go)/ Math.Pow(PRo, (2.0 / 3.0));
                     
               Jc = 0.55 + (0.72 * He.Fc);

                double rs = He.Ssb / (He.Ssb + He.Stb);
                double rl = (He.Ssb + He.Stb) / He.Sm;
                
                JL = 0.44 * (1 - rs) + (1 - 0.44 * (1 - rs)) * Math.Pow(Math.E, (-2.2 * rl));
                

                double p = 0.8 - 0.15 * (1 + rs);
                RL = Math.Pow(Math.E, (-1.33 * (1 + rs) * (Math.Pow(rl, p))));
                

                double n1 = 0;
                double n2 = 0;
                double CJ = 0;
                double CR = 0;

                if (REo < 100)
                {
                    CJ = 1.35;
                    CR = 4.5;
                    n1 = 1 / 3;
                    n2 = 1.0;
                }
                else if (REo > 100 || REo == 100)
                {
                    CJ = 1.25;
                    CR = 3.7;
                    n1 = 0.6;
                    n2 = 0.2;
                }

                
                if (He.rss < 0.5)
                {
                    JB = Math.Pow(Math.E, (-CJ * (He.Sb / He.Sm) * (1 - Math.Pow((2 * He.rss), (1.0 / 3.0)))));
                    RB = Math.Pow(Math.E, (-CR * (He.Sb / He.Sm) * (1 - Math.Pow((2 * He.rss), (1.0 / 3.0)))));
                    
                }
                else if (He.rss >= 0.5 )
                {
                    JB = 1.0;
                    RB = 1.0;
                }
                
                JS = ((He.nb - 1) + (Math.Pow((He.Bin / He.B), (1.0 - n1))) + (Math.Pow((He.Bout / He.B), (1.0 - n1)))) / ((He.nb - 1.0) + (He.Bin / He.B) + (He.Bout / He.B));
                RS = 0.5 * ((Math.Pow((He.B / He.Bin), (2.0 - n2))) + (Math.Pow((He.B / He.Bout), (2.0 - n2))));
                

                double NCplus = (He.nb + 1) * (He.Nc + He.Ncw);
                
                if (REo <= 20)
                {
                    JR = Math.Pow((10.0 / NCplus), 0.18);
                }
                else if (REo > 20 && REo < 100)
                {
                    JR = (1 - Math.Pow((10.0 / NCplus), 0.18)) * ((REo - 20.0) / 80.0) + Math.Pow((10.0 / NCplus), 0.18);
                }
                else if (REo > 100 || REo == 100)
                {
                    JR = 1.0;
                }
                
                ho = hideal * Jc * JL * JB * JR * JS;
              
                
                
        }

        private void Calculate_DPo()
        {
            DPideal = (2 * fj * He.Nc * (Math.Pow(Go, 2.0))) / (gc * sFluid.Rho * phi_s);

           

            DPcs = (He.nb - 1) * DPideal * RL * RB;
            DPes = 2 * DPideal * (1 + He.Ncw / He.Nc) * RB * RS;
           
            if (REo>=100)
            {
                   DPwideal=((2+0.6*He.Ncw)*Math.Pow(sMassFlow,2))/(2*gc*sFluid.Rho*He.Sm*He.Sw);
            }
            else if (REo<100)
            {
                double niu = sFluid.Miu / sFluid.Rho;
                
                DPwideal = (((26 * niu * sMassFlow) / (2*gc * (Math.Sqrt(He.Sm * He.Sw)))) * ((He.Ncw / (He.Pt - He.Do)) + ((He.Bc * He.Ds) / Math.Pow(He.Dw, 2.0)))) + (Math.Pow(sMassFlow, 2.0) / (gc * sFluid.Rho * He.Sm * He.Sw));
            }
           
            DPws = He.nb * DPwideal * RL;
            
            DPfs = DPcs + DPes + DPws;                                         // in lbf/ft2
            DPfs /= 144;                                                       // in psi

            
            double Gn = sMassFlow / (Math.PI * Math.Pow(He.Dns, 2) / 4.0);

            double REn = He.Dns * Gn / sFluid.Miu;
            
            double Rhowater = FluidCalculator.GetWaterDensity(Tsave);
            double s = sFluid.Rho / Rhowater;
            
            if (REn>=10000)
            {
                DPns =( 0.0000000000002 * Gn*Gn) / s;                                //in psi
            }
            else
            {
                DPns = (0.0000000000004 * Gn*Gn )/ s;                                 //in psi
            }
            
            DPo = DPns + DPfs;                                                   // in psi
            
        }

        private void Calculate_DPi()   
        {
            if (REi>10000)
            {
                fi = 0.4137 * Math.Pow(REi, -0.2585);
            }
            else
            {
                fi = 64 / REi;
            }
            double RhoWater = FluidCalculator.GetWaterDensity(Ttave);
            double s = tFluid.Rho / RhoWater;
            DPft = (fi * He.np * He.L * Math.Pow(Gi, 2)) / (7500000000000 * He.Di * s * phi_t);       //in psi
            
            double alphar=0;
            if (REi>10000)
            {
                if (He.TEMAType==3||He.TEMAType==4)
                {
                    alphar = 1.6 * He.np - 1.5;
                }
                else
                {
                    alphar = 2.0 * He.np - 1.5;
                }
            }
            else
            {
                if (He.TEMAType == 3 || He.TEMAType == 4)
                {
                    alphar = 2.38 * He.np - 1.5;
                }
                else
                {
                    alphar = 3.25 * He.np - 1.5;
                }
            }
            DPrt = 0.0000000000001334 * alphar * Math.Pow(Gi, 2) / s;                                       // in psi
            double Gn = tMassFlow / (Math.PI * Math.Pow(He.Dnt, 2) / 4.0);
            double REn = He.Dnt * Gn / tFluid.Miu;
            
            if (REn >= 10000)
            {
                DPnt = (0.0000000000002 * Gn*Gn) / s;                                                            //in psi
            }
            else
            {
                DPnt = (0.0000000000004 * Gn*Gn )/ s;                                                           //in psi
            }
            
            DPi =   DPrt + DPnt+ DPft;
            
            
        }


    }
}
