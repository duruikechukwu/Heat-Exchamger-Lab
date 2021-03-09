using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heat_Exchamger_Lab
{
    [Serializable]

    public class HeatExchanger
    {
        private string _Identifier;
        public string Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; }
        }

        private double _TubeLayout;
        public double TubeLayout
        {
            get { return _TubeLayout; }
            set { _TubeLayout = value; }
        }
        public string TubelayoutCaption { get;  set; }

        private double _TEMAType;
        public double TEMAType
        {
            get { return _TEMAType; }
            set { _TEMAType = value; }
        }
        public string TEMATypeCaption { get; set; }
        private double _Di;
        public double Di
        {
            get { return _Di; }
            set { _Di = value; }
        }

        private double _Do;
        public double Do
        {
            get { return _Do; }
            set { _Do = value; }
        }

        private double _Ds;
        public double Ds
        {
            get { return _Ds; }
            set { _Ds = value; }
        }

        private double _Pt;
        public double Pt
        {
            get { return _Pt; }
            set { _Pt = value; }
        }

        private double _nt;
        public double nt
        {
            get { return _nt; }
            set { _nt = value; }
        }

        private double _np;
        public double np
        {
            get { return _np; }
            set { _np = value; }
        }

        private double _nb;
        public double nb
        {
            get { return _nb; }
            set { _nb = value; }
        }

        private double _B;
        public double B
        {
            get { return _B; }
            set { _B = value; }
        }

        private double _Bin;
        public double Bin
        {
            get { return _Bin; }
            set { _Bin = value; }
        }

        private double _Bout;
        public double Bout
        {
            get { return _Bout; }
            set { _Bout = value; }
        }

        private double _Bc;
        public double Bc
        {
            get { return _Bc; }
            set { _Bc = value; }
        }

        private double _Nss;
        public double Nss
        {
            get { return _Nss; }
            set { _Nss = value; }
        }

        public double rss { get;  set; }

        private double _L;
        public double L
        {
            get { return _L; }
            set { _L = value; }
        }

        private double _Dtb;
        public double Dtb
        {
            get { return _Dtb; }
            set { _Dtb = value; }
        }

        private double _Dsb;
        public double Dsb
        {
            get { return _Dsb; }
            set { _Dsb = value; }
        }

        private double _Ds_Dotl;
        public double Ds_Dotl
        {
            get { return _Ds_Dotl; }
            set { _Ds_Dotl = value; }
        }

        private double _Dotl;
        public double Dotl
        {
            get { return _Dotl; }
            set { _Dotl = value; }
        }

        private double _Dctl;
        public double Dctl
        {
            get { return _Dctl; }
            set { _Dctl = value; }
        }

      
        private double _Fc;

        public double Fc
        {
            get { return _Fc; }
            set { _Fc = value; }
        }

        private double _Fw;

        public double Fw
        {
            get { return _Fw; }
            set { _Fw = value; }
        }

        private double _Sm;
        public double Sm
        {
            get { return _Sm; }
            set { _Sm = value; }
        }

        private double _Stb;

        public double Stb
        {
            get { return _Stb; }
            set { _Stb = value; }
        }

        private double _Ssb;

        public double Ssb
        {
            get { return _Ssb; }
            set { _Ssb = value; }
        }

        private double _Sb;

        public double Sb
        {
            get { return _Sb; }
            set { _Sb = value; }
        }

        private double _Sw;

        public double Sw
        {
            get { return _Sw; }
            set { _Sw = value; }
        }

        private double _A;

        public double A
        {
            get { return _A; }
            set { _A = value; }
        }

        private double PTEff;
        private double ThetaCtl;
        private double ThetaDs;

        public double PTprime { get; private set; }
        public double Nc { get; private set; }
        public double Ncw { get; private set; }
        public double Dw { get; private set; }
       

        public double Dns { get;  set; }  //shell side nozzle diameter
        public double Dnt { get;  set; }     //Tube side nozzle diameter

        public string Material { get;  set; }
        public double MaterialK { get; private set; }

        private bool _IncludeSafetyFactor;
        public bool IncludeSafetyFactor
        {
            get { return _IncludeSafetyFactor; }
            set { _IncludeSafetyFactor = value; }
        }
        
        public HeatExchanger()
        {
         
        }

        private void EstimateDtb()
        { double a =12*_Do; // Do in inches
            if (a>1.25)
            {
                _Dtb = 0.4; // in mm
            }
            else
            {
                if (_B==Bin && _Bout==Bin )
                { double b = 2*_B;
                    if (b<3)
                    {
                        _Dtb = 0.4; // in mm
                    }
                    else
                    {
                        _Dtb = 0.2; // in mm
                    }
                }
                else
                {
                    double b;
                    double c;
                    if (_Bin>B || _Bout>_B)
                    {
                        if (_Bin>_Bout)
                        {
                            b = _Bin;
                        }
                        else
                        {
                            b = _Bout;
                        }
                    }
                    else
                    {
                        b = _B;
                    }
                    c = b + _B;
                    if (c<3)
                    {
                        _Dtb = 0.4; // in mm
                    }
                    else
                    {
                        _Dtb = 0.2; // in mm
                    }
                }
            }
            _Dtb /= 304.8; // in ft
        }

        private void EstimateDsb()
        {
            double a = _Ds * 304.8; // Ds in mm
            if (_IncludeSafetyFactor)
            {
                _Dsb = 0.8 + (0.002 * a) + 0.75; // in mm
            }
            else
            {
                _Dsb = 0.8 + (0.002 * a); // in mm
            }
            _Dsb /= 304.8;
        }

        private void EstimateDs_Dotl()
        {
            double a = _Ds * 304.8; // Ds in mm
            if (_TEMAType<=4)
            {
                if (a<=250)
                {
                    _Ds_Dotl = 14; // in mm
                }
                else if (a>2500)
                {
                    _Ds_Dotl = 26; // in mm
                }
                else if (a>250 && a <=2500)
                {
                    _Ds_Dotl = 0.005 * a + 13; // in mm
                }
            }
            else if (_TEMAType>=5&&_TEMAType<=10)
            {
                if (a<=250)
                {
                    _Ds_Dotl = 29; // in mm
                }
                else if (a>2500)
                {
                    _Ds_Dotl = 68; // in mm
                }
                else if (a>250 && a <=2500)
                {
                    _Ds_Dotl = 0.016 * a + 26.176; // in mm
                }
            }
            else if (_TEMAType>10)
            {
                if (a <= 250)
                {
                    _Ds_Dotl = 89; // in mm
                }
                else if (a > 2500)
                {
                    _Ds_Dotl = 160; // in mm
                }
                else if (a > 250 && a <= 2500)
                {
                    _Ds_Dotl = 0.034 * a + 81; // in mm
                }
            }
            _Ds_Dotl /= 304.8; // in ft
        }

        private void CalculateDotlAndDctl()
        {
            _Dotl = _Ds - _Ds_Dotl;
            _Dctl = _Dotl - _Do;
        }

        private void CalculatePTEffandPTprime()
        {
            if (_TubeLayout==30|| _TubeLayout==90)
            {
                PTEff = _Pt;
            }
            else if (_TubeLayout==45)
            {
                PTEff = _Pt / Math.Sqrt(2);
            }

            if (TubeLayout==30)
            {
                PTprime = _Pt*Math.Cos(Math.PI / 6);
            }
            else if (TubeLayout==45)
            {
                PTprime = _Pt*Math.Cos(Math.PI / 4);
            }
            else if(TubeLayout==90)
            {
                PTprime = _Pt;
            }

        }
        
        private void CalculateArea()
        {
            _A = Math.PI * _Do * _L * _nt;
            _Sm = _B * (_Ds_Dotl + ((_Dotl - _Do) * (_Pt - _Do) / PTEff));

            double a = _Ds * (1 - (2 * _Bc)) / _Dctl;
            ThetaCtl = 2 * Math.Acos(a); // in radians

            _Fc = 1 + (1 / Math.PI) * (Math.Sin(ThetaCtl) - ThetaCtl);

            _Stb = 0.5 * Math.PI * _Do * _Dtb * _nt * (1 + _Fc);

            ThetaDs = 2 * Math.Acos(1-(2*_Bc));

            _Ssb = _Ds * _Dsb * (Math.PI - (0.5 * ThetaDs));

            _Sb = _B * _Ds_Dotl;

            _Fw = (1 - _Fc) / 2;

             double Swg = 0.125 * (Math.Pow(_Ds, 2)) * (ThetaDs - Math.Sin(ThetaDs));
             double Atubes = _nt * _Fw * (Math.PI * (Math.Pow(_Do, 2)) / 4.0);
            _Sw = Swg - Atubes;
            Dw = (4.0 * _Sw) / (Math.PI * _Do * _nt * 0.5 * (1 - _Fc)) +( _Ds + ThetaDs);

        }

        private void CalculateCostants()
        {
            Nc = (_Ds * (1 - 2 * _Bc)) / PTprime;
            Ncw = (0.8 * _Bc * _Ds) / PTprime;
        
            if (_Nss==-1)
            {
                   _Nss = rss * Nc;
            }
            else if (rss==-1)
            {
                rss = _Nss / Nc;
            }
            _Nss = rss * Nc;
            switch (Material)
            {
                case "Carbon-Steel":
                    MaterialK=26;
                        break;
                case "Admiralty-Brass":
                        MaterialK = 64;
                        break;
                case "Aluminium":
                        MaterialK = 157.7;
                        break;
                case "Copper":
                        MaterialK = 230;
                        break;
                default:
                    break;
            }
        }

        public void calculate()
        {
            EstimateDtb();
            EstimateDsb(); 
            EstimateDs_Dotl();
            CalculatePTEffandPTprime();
            CalculateCostants();
            CalculateDotlAndDctl();
            CalculateArea();

        }
    }
}
