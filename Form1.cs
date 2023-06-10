using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows.Forms;

namespace RollChannelControl
{
    public partial class Form1 : Form
    {
        private readonly ChartValues<double> _chartValues;
        private readonly Timer _timer;
        private readonly double Abruptness = 0.2;
        private double X;
        private double XDotIn;
        private double XDotOut;
        private double XDotDot;

        private double DeltaXDot
        {
            get
            {
                return XDotIn - XDotOut;
            }
        }

        public Form1()
        {
            InitializeComponent();
            
            cartesianChart1.DisableAnimations = true;
            
            cartesianChart1.Series = new LiveCharts.SeriesCollection
            {
                new LineSeries
                {
                    Title = "X value",
                    Values = _chartValues = new ChartValues<double>(),
                }
            };
            
            _timer = new Timer { Interval = 50 };
            _timer.Tick += TimerOnTick;
            
            _timer.Start();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out var value))
            {
                XDotIn = value;
                if (!_timer.Enabled)
                    _timer.Start();
                
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid number.");
            }
        }
        
        private void EvaluateX()
        {
            X += XDotOut;
        }
        
        private void EvaluateXDotOut()
        {
            XDotOut += XDotDot;
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            EvaluateXDotDot();
            EvaluateXDotOut();
            EvaluateX();
            _chartValues.Add(XDotOut);
            if (_chartValues.Count > 50)
                _chartValues.RemoveAt(0);
            
            Console.Write("Next:");
            System.Diagnostics.Debug.WriteLine("{0, -10:F2} {1, -10:F2} {2, -10:F2}", X, XDotOut, XDotDot);

            cartesianChart1.Update(false, true);
        }

        private void EvaluateXDotDot()
        {
            XDotDot = Math.Abs(DeltaXDot) > 0.2 ?
                (DeltaXDot > 0 ? Abruptness : -Abruptness):
                0;
        }
    }
}