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
        private readonly double maxAbruptness = 0.2;
        private double setAbruptness;
        private double Abruptness;
        private double AbruptnessCoefficient = 0.01;
        private double XDotIntegral;
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
        
        private double DeltaXDotDot
        {
            get
            {
                return setAbruptness - XDotDot;
            }
        }
        
        private Queue<double> DeltaXDotQueue = new Queue<double>();
        

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
            EvaluateXDotIntegral();
            EvaluateXDotDot();
            EvaluateXDotOut();
            EvaluateX();
            _chartValues.Add(XDotOut);
            if (_chartValues.Count > 100)
                _chartValues.RemoveAt(0);
            
            Console.Write("Next:");
            System.Diagnostics.Debug.WriteLine("{0, -10:F2} {1, -10:F2} {2, -10:F2}", X, XDotOut, XDotDot);

            cartesianChart1.Update(false, true);
        }

        private void EvaluateXDotIntegral()
        {
            DeltaXDotQueue.Enqueue(DeltaXDot);
            if (DeltaXDotQueue.Count > 10)
                DeltaXDotQueue.Dequeue();
            
            for (int i = 0; i < DeltaXDotQueue.Count; i++)
            {
                XDotIntegral += DeltaXDotQueue.ElementAt(i) * Math.Exp(-i);
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
        
        private void EvaluateXDotDot()
        {
            setAbruptness = Math.Abs(DeltaXDot) > 0.01 ?
                (DeltaXDot > 0 ? maxAbruptness : -maxAbruptness):
                0;
            XDotDot += Abruptness;
        }
        
        private void EvaluateXDotDotDot()
        {
            Abruptness = Math.Abs(DeltaXDotDot) > 0.005 ?
                (DeltaXDotDot > 0 ? AbruptnessCoefficient : -AbruptnessCoefficient):
                0;
        }
    }
}