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
        private double X;
        private double XDotIntegral;
        private double XDotIn;
        private double XDotOut;
        private double XDotDot;
        private double DeltaXDotDot;
        private double XDotDotSetPoint;
        private double XDotDerivative;
        
        private double Kp = 0.05; // Proportional gain
        private double Ki = 0.004; // Integral gain
        private double Kd = 0.15; // Derivative gain
        private double DerivativeSampleSize = 2; // Number of samples to use for derivative calculation
        private double IntegralSampleSize = 5; // Number of samples to use for integral calculation
        private double maxAngularJerk = 0.4; // Maximum angular jerk (deg/s^3) that can be expected of the aircraft

        private double DeltaXDot
        {
            get
            {
                return XDotIn - XDotOut;
            }
        }
        
        private Queue<double> XDotDerivativeQueue = new Queue<double>(); // Queue of XDotDerivative samples
        private Queue<double> XDotIntegralQueue = new Queue<double>(); // Queue of XDotIntegral samples

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
            EvaluateXDotDotDot();
            EvaluateXDotDerivative();
            EvaluateXDotDot();
            EvaluateIntegral();
            EvaluateXDotOut();
            EvaluateX();
            _chartValues.Add(DeltaXDot);
            if (_chartValues.Count > 100)
                _chartValues.RemoveAt(0);
            
            Console.Write("Next:");
            System.Diagnostics.Debug.WriteLine("{0, -10:F2} {1, -10:F2} {2, -10:F2}", X, XDotOut, XDotDot);

            cartesianChart1.Update(false, true);
        }

        private void EvaluateX()
        {
            X += XDotOut;
        }
        
        private void EvaluateXDotOut()
        {
            XDotOut += XDotDot;
        }
        
        private void EvaluateXDotDerivative()
        {
            XDotDerivativeQueue.Enqueue(DeltaXDot);
            if (XDotDerivativeQueue.Count > DerivativeSampleSize)
                XDotDerivativeQueue.Dequeue();
            XDotDerivative = XDotDerivativeQueue.Average();
        }
        
        private void EvaluateIntegral()
        {
            XDotIntegralQueue.Enqueue(DeltaXDot);
            if (XDotIntegralQueue.Count > IntegralSampleSize)
                XDotIntegralQueue.Dequeue();
            XDotIntegral = XDotIntegralQueue.Sum();
        }

        private void EvaluateXDotDot()
        {
            XDotDotSetPoint = Kp * DeltaXDot + Ki * XDotIntegral + Kd * XDotDerivative;
            XDotDot += DeltaXDotDot;
        }
        
        private void EvaluateXDotDotDot()
        {
            DeltaXDotDot = Math.Abs(XDotDotSetPoint - XDotDot) > maxAngularJerk
                ? Math.Sign(XDotDotSetPoint - XDotDot) * maxAngularJerk
                : XDotDotSetPoint - XDotDot;
        }
    }
}