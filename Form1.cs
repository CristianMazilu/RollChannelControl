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

        public Form1()
        {
            InitializeComponent();

            // initialize chart
            cartesianChart1.Series = new LiveCharts.SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = _chartValues = new ChartValues<double>(),
                }
            };

            // setup timer
            _timer = new Timer { Interval = 50 };
            _timer.Tick += TimerOnTick;

            // start timer
            _timer.Start();
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            // add new value and remove oldest if necessary
            _chartValues.Add(System.Environment.TickCount);
            if (_chartValues.Count > 50)
                _chartValues.RemoveAt(0);

            // refresh chart
            cartesianChart1.Update(false, true);
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out var value))
            {
                // add value and start timer if necessary
                _chartValues.Add(value);
                if (!_timer.Enabled)
                    _timer.Start();

                // clear text box
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid number.");
            }
        }
    }
}