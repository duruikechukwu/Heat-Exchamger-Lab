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
    /// Interaction logic for LineGraph.xaml
    /// </summary>
    public partial class LineGraph : Window
    {
        public PointCollection mypoints { get; set; }
        PointCollection plotpoints;
        double plotwidth;
        double plotheight;
        bool case0x = false;
        bool case0y = false;
        DoubleCollection xvalues = new DoubleCollection();
        DoubleCollection yvalues = new DoubleCollection();
        ScaleTransform t = new ScaleTransform(1, -1);
        double xrange;
        double yrange;
       
        public LineGraph()
        {
            InitializeComponent();
            plotwidth = plotCanvas.Width;
            plotheight = plotCanvas.Height;
            plotpoints = new PointCollection();
        }
        private void NormalizeUnits()
        {
         
            xvalues.Clear();
            yvalues.Clear();
            plotpoints.Clear();
            plotCanvas.Children.Clear();
            foreach (Point item in mypoints)
            {
                xvalues.Add(item.X);
                yvalues.Add(item.Y);
            }
            if (xvalues.Count > 1 && xvalues.Max() != xvalues.Min())
            {
                 xrange = xvalues.Max() - xvalues.Min();             
            }
            if (xvalues.Max() == xvalues.Min())
            {
                xrange = 2*plotwidth;
                case0x = true;
            }

            if (yvalues.Count > 1)
            {
                 yrange = yvalues.Max() - yvalues.Min();
            
            }
            if (yvalues.Max() == yvalues.Min())
            {
                yrange = 2*plotheight;
                case0y = true;
            }
        
            foreach (Point item in mypoints)
            {
               Point p = new Point();
               p.X = ((item.X - xvalues.Min()) * (plotwidth / xrange));
               p.Y = ((item.Y - yvalues.Min()) * (plotheight / yrange));
               if (case0x)
               {
                   p.X += plotwidth / 2;
               }
               if (case0y)
               {
                   p.Y += plotheight / 2;
               }

            

               //Label ly = new Label() { Width = 80, Height=25, Margin= new Thickness(-80,10,0,0)};
               //Canvas.SetTop(ly, p.Y);
               //Canvas.SetLeft(ly, 0);
               //ly.Content = item.Y;    
               //ly.RenderTransform = t;
               //plotCanvas.Children.Add(ly);

               //Label lx = new Label() { Width = 80, Height = 25, Margin = new Thickness(0, -10, 0, 0) };
               //Canvas.SetTop(lx, 0);
               //Canvas.SetLeft(lx, p.X);
               //lx.Content = item.X;
               //lx.RenderTransform = t;
               //plotCanvas.Children.Add(lx);

               plotpoints.Add(p);
            }
            ArrangePoints();
           
        }
        public void ArrangePoints()
        {
            for (double i = 0; i <= 10; i++)
            {
                Label ly = new Label() { Width = 80, Height = 25, Margin = new Thickness(-80, 10, 0, 0) };
                Canvas.SetTop(ly, (i * plotheight) / 10);
                Canvas.SetLeft(ly, 0);
                ly.RenderTransform = t;
                if (!case0y)
                {
                    ly.Content = Math.Round((yvalues.Min() + (i * yrange / 10)), 4);
                }
                else
                {
                    ly.Content = Math.Round(((i * 2*yvalues.Min() / 10)),4);
                }
                plotCanvas.Children.Add(ly);
            }
            for (double i = 0; i <= 10; i++)
            {
                Label lx = new Label() { Width = 80, Height = 25, Margin = new Thickness(0, -10, 0, 0) };
                Canvas.SetTop(lx, 0);
                Canvas.SetLeft(lx, (i * plotwidth) / 10);
                lx.RenderTransform = t;
                if (!case0x)
                {
                    lx.Content = Math.Round((xvalues.Min() + (i * xrange / 10)),4);
                }
                else
                {
                    lx.Content = Math.Round(((i * 2*xvalues.Min() / 10)), 4);
                }
                plotCanvas.Children.Add(lx);
            }
        }
        public void Plot(ref PointCollection points)
        {
            this.mypoints = points;
            if (mypoints.Count!=0)
            {
                NormalizeUnits();
                Polyline line = new Polyline() { Stroke = Brushes.Blue, StrokeThickness = 1 };
                line.Points = plotpoints;
                plotCanvas.Children.Add(line);
            }
        }
        public void ShowVariables(string indi, string indiU, string depen, string depenU)
        {
            txtdependent.Text = depen;
            txtdependentunit.Text = depenU;
            txtindependent.Text = indi;
            txtindependentunit.Text = indiU;
        }
    }
}
