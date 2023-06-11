using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RollChannelControl
{
    public partial class Form1 : Form
    {
        private readonly ChartValues<double> _chartValues;
        private readonly Timer _timer;
        private double XDotIntegral;
        private double X;
        private double XDotIn;
        private double XDotOut;
        private double XDotDot;
        private double PreviousXDotDot = 0;
        private double DeltaXDotDot;
        
        private double Kp = 0.25;
        private double Ki = 0;
        private double Kd = 0.1;

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

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            EvaluateXDotIntegral();
            EvaluateXDotDot();
            EvaluateXDotOut();
            EvaluateX();
            _chartValues.Add(XDotDot);
            if (_chartValues.Count > 100)
                _chartValues.RemoveAt(0);
            
            Console.Write("Next:");
            System.Diagnostics.Debug.WriteLine("{0, -10:F2} {1, -10:F2} {2, -10:F2}", X, XDotOut, XDotDot);

            cartesianChart1.Update(false, true);
        }

        private void EvaluateXDotIntegral()
        {
            XDotIntegral += DeltaXDot;
        }

        private void EvaluateX()
        {
            X += XDotOut;
        }
        
        private void EvaluateXDotOut()
        {
            XDotOut += XDotDot;
        }
        
        private void EvaluateXDotDot()
        {
            XDotDot = Kp * DeltaXDot + Ki * XDotIntegral + Kd * DeltaXDotDot;
        }

        // Evaluate the derivative of XDotDot
        private void EvaluateXDotDotDot()
        {
            DeltaXDotDot = XDotDot - PreviousXDotDot;
            PreviousXDotDot = XDotDot;
        }
    }
}