using LiveCharts.Wpf;

namespace RollChannelControl
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private LiveCharts.WinForms.CartesianChart rollRateCartesianChart;
        private LiveCharts.WinForms.CartesianChart accelerationRateCartesianChart;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textBox1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rollRateCartesianChart = new LiveCharts.WinForms.CartesianChart();
            this.accelerationRateCartesianChart = new LiveCharts.WinForms.CartesianChart();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // rollRateCartesianChart
            // 
            this.rollRateCartesianChart.Location = new System.Drawing.Point(9, 10);
            this.rollRateCartesianChart.Margin = new System.Windows.Forms.Padding(2);
            this.rollRateCartesianChart.Name = "rollRateCartesianChart";
            this.rollRateCartesianChart.Size = new System.Drawing.Size(723, 373);
            this.rollRateCartesianChart.TabIndex = 0;
            // 
            // accelerationRateCartesianChart
            // 
            this.accelerationRateCartesianChart.Location = new System.Drawing.Point(9, 388);
            this.accelerationRateCartesianChart.Name = "accelerationRateCartesianChart";
            this.accelerationRateCartesianChart.Size = new System.Drawing.Size(723, 391);
            this.accelerationRateCartesianChart.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(736, 35);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(56, 19);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(736, 11);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(256, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(36, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Roll Rate Chart";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(36, 388);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "Acceleration Rate Chart (blue)\r\nJerk Rate Chart (red, scaled up for visibility)";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(736, 359);
            this.trackBar1.Maximum = 15;
            this.trackBar1.Minimum = -15;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(254, 45);
            this.trackBar1.TabIndex = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1003, 790);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.rollRateCartesianChart);
            this.Controls.Add(this.accelerationRateCartesianChart);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "RollChannelControl";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TrackBar trackBar1;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}
