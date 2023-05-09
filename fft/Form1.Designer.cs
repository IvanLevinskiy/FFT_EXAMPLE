
namespace fft
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.trackBar_120Hz = new System.Windows.Forms.TrackBar();
            this.trackBar_180Hz = new System.Windows.Forms.TrackBar();
            this.label_rms_amplitude = new System.Windows.Forms.Label();
            this.label_rms_freq = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar_60Hz = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBar_240Hz = new System.Windows.Forms.TrackBar();
            this.chartSpectr = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chartAmplitude = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_120Hz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_180Hz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_60Hz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_240Hz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSpectr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAmplitude)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar_120Hz
            // 
            this.trackBar_120Hz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar_120Hz.Location = new System.Drawing.Point(1083, 86);
            this.trackBar_120Hz.Margin = new System.Windows.Forms.Padding(4);
            this.trackBar_120Hz.Name = "trackBar_120Hz";
            this.trackBar_120Hz.Size = new System.Drawing.Size(163, 56);
            this.trackBar_120Hz.TabIndex = 2;
            this.trackBar_120Hz.Scroll += new System.EventHandler(this.trackBar_120Hz_Scroll);
            // 
            // trackBar_180Hz
            // 
            this.trackBar_180Hz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar_180Hz.Location = new System.Drawing.Point(1083, 149);
            this.trackBar_180Hz.Margin = new System.Windows.Forms.Padding(4);
            this.trackBar_180Hz.Name = "trackBar_180Hz";
            this.trackBar_180Hz.Size = new System.Drawing.Size(163, 56);
            this.trackBar_180Hz.TabIndex = 3;
            this.trackBar_180Hz.Scroll += new System.EventHandler(this.trackBar_180Hz_Scroll);
            // 
            // label_rms_amplitude
            // 
            this.label_rms_amplitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_rms_amplitude.AutoSize = true;
            this.label_rms_amplitude.Location = new System.Drawing.Point(1053, 545);
            this.label_rms_amplitude.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_rms_amplitude.Name = "label_rms_amplitude";
            this.label_rms_amplitude.Size = new System.Drawing.Size(113, 17);
            this.label_rms_amplitude.TabIndex = 5;
            this.label_rms_amplitude.Text = "СКЗ Амплитуды";
            // 
            // label_rms_freq
            // 
            this.label_rms_freq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_rms_freq.AutoSize = true;
            this.label_rms_freq.Location = new System.Drawing.Point(1053, 594);
            this.label_rms_freq.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_rms_freq.Name = "label_rms_freq";
            this.label_rms_freq.Size = new System.Drawing.Size(96, 17);
            this.label_rms_freq.TabIndex = 6;
            this.label_rms_freq.Text = "СКЗ Частоты";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1027, 87);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "120Гц";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1027, 160);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "180Гц";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1027, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "60Гц";
            // 
            // trackBar_60Hz
            // 
            this.trackBar_60Hz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar_60Hz.Location = new System.Drawing.Point(1083, 23);
            this.trackBar_60Hz.Margin = new System.Windows.Forms.Padding(4);
            this.trackBar_60Hz.Name = "trackBar_60Hz";
            this.trackBar_60Hz.Size = new System.Drawing.Size(163, 56);
            this.trackBar_60Hz.TabIndex = 9;
            this.trackBar_60Hz.Value = 10;
            this.trackBar_60Hz.Scroll += new System.EventHandler(this.trackBar_60Hz_Scroll);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1027, 223);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "240Гц";
            // 
            // trackBar_240Hz
            // 
            this.trackBar_240Hz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar_240Hz.Location = new System.Drawing.Point(1083, 212);
            this.trackBar_240Hz.Margin = new System.Windows.Forms.Padding(4);
            this.trackBar_240Hz.Name = "trackBar_240Hz";
            this.trackBar_240Hz.Size = new System.Drawing.Size(163, 56);
            this.trackBar_240Hz.TabIndex = 11;
            this.trackBar_240Hz.Scroll += new System.EventHandler(this.trackBar_240Hz_Scroll);
            // 
            // chartSpectr
            // 
            this.chartSpectr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.Title = "Частота, Гц";
            chartArea1.AxisY.Title = "Магнитуда, дБ";
            chartArea1.Name = "ChartArea1";
            this.chartSpectr.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chartSpectr.Legends.Add(legend1);
            this.chartSpectr.Location = new System.Drawing.Point(4, 22);
            this.chartSpectr.Margin = new System.Windows.Forms.Padding(4);
            this.chartSpectr.Name = "chartSpectr";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartSpectr.Series.Add(series1);
            this.chartSpectr.Size = new System.Drawing.Size(1011, 322);
            this.chartSpectr.TabIndex = 1;
            this.chartSpectr.Text = "chart2";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(1, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.chartAmplitude);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.chartSpectr);
            this.splitContainer1.Size = new System.Drawing.Size(1019, 704);
            this.splitContainer1.SplitterDistance = 352;
            this.splitContainer1.TabIndex = 13;
            // 
            // chartAmplitude
            // 
            this.chartAmplitude.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.AxisX.Title = "Сэмплы";
            chartArea2.AxisY.Title = "Амплитуда, дБ";
            chartArea2.AxisY2.Minimum = 0D;
            chartArea2.Name = "ChartArea1";
            this.chartAmplitude.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.chartAmplitude.Legends.Add(legend2);
            this.chartAmplitude.Location = new System.Drawing.Point(4, 25);
            this.chartAmplitude.Margin = new System.Windows.Forms.Padding(4);
            this.chartAmplitude.Name = "chartAmplitude";
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Red;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.chartAmplitude.Series.Add(series2);
            this.chartAmplitude.Size = new System.Drawing.Size(1011, 323);
            this.chartAmplitude.TabIndex = 1;
            this.chartAmplitude.Text = "chart1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Амплитудный сигнал:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "Спектр сигнала:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 706);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.trackBar_240Hz);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBar_60Hz);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_rms_freq);
            this.Controls.Add(this.label_rms_amplitude);
            this.Controls.Add(this.trackBar_180Hz);
            this.Controls.Add(this.trackBar_120Hz);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Разложение амплитудного сигнала на спектры";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_120Hz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_180Hz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_60Hz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_240Hz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSpectr)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartAmplitude)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar trackBar_120Hz;
        private System.Windows.Forms.TrackBar trackBar_180Hz;
        private System.Windows.Forms.Label label_rms_amplitude;
        private System.Windows.Forms.Label label_rms_freq;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar_60Hz;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBar_240Hz;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSpectr;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAmplitude;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

