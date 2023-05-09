using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fft
{
    public partial class Form1 : Form
    {
        int numSamples = 1000;
        int sampleRate = 2000;
        double freq = 1.0;
        double amplitude_60Hz =  10;
        double amplitude_120Hz = 0;
        double amplitude_180Hz = 0;
        double amplitude_240Hz = 0;

        List<int> buffer = new List<int>();


        /// <summary>
        /// Конструктор класса
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            Draw();
        }

        /// <summary>
        /// Метод для построения графиков
        /// </summary>
        void Draw()
        {
            chartAmplitude.Series[0].Points.Clear();
            chartSpectr.Series[0].Points.Clear();

            Complex[] samplies = new Complex[numSamples];

            var _60Hz =  Generate.Sinusoidal(numSamples,  sampleRate, 60,  amplitude_60Hz);
            var _120Hz = Generate.Sinusoidal(numSamples,  sampleRate, 120,  amplitude_120Hz);
            var _180Hz = Generate.Sinusoidal(numSamples,  sampleRate, 180,  amplitude_180Hz);
            var _240Hz = Generate.Sinusoidal(numSamples, sampleRate, 240, amplitude_240Hz);

            for (int i = 0; i < numSamples; i++)
            {

                samplies[i] = new Complex(_60Hz[i] + _120Hz[i] + _180Hz[i] + _240Hz[i], 0);
            }

            for (int i = 0; i < samplies.Length / 10; i++)
            {
                double time = ((i + 1.0) / numSamples) / 2;
                chartAmplitude.Series[0].Points.AddXY(time, samplies[i].Real);
            }

            Fourier.Forward(samplies, FourierOptions.NoScaling);


            //Получаем спектр
            for (int i = 0; i < samplies.Length/2; i++)
            {
                double mag = (2.0/numSamples) * Math.Abs(Math.Sqrt(Math.Pow(samplies[i].Real, 2) + Math.Pow(samplies[i].Imaginary, 2)));
                double hzSample = sampleRate / numSamples;

                chartSpectr.Series[0].Points.AddXY(hzSample * i, mag);
            }

            Get_RMS_A();
            Get_RMS_F();
        }

        /// <summary>
        /// Вычисление СКЗ амплитуды
        /// </summary>
        void Get_RMS_A()
        {
            double rms = 0;
            double counter = 0;

            foreach (var point in chartAmplitude.Series[0].Points)
            {
                rms += Math.Pow(point.YValues[0], 2);
                counter++;
            }

            rms = Math.Sqrt(rms / counter);
            rms = Math.Round(rms, 5);

            label_rms_amplitude.Text = $"СКЗ амплитуды: {rms}";
                                       
        }

        /// <summary>
        /// Вычисление СКЗ частоты
        /// </summary>
        void Get_RMS_F()
        {
            double rms = 0;
            double counter = 0;

            foreach (var point in chartSpectr.Series[0].Points)
            {
                rms += Math.Pow(point.YValues[0], 2);

                if (rms != 0)
                {
                    counter++;
                }
                
            }

            rms = Math.Sqrt(rms / 2);
            rms = Math.Round(rms, 5);


            label_rms_freq.Text = $"СКЗ частоты:   {rms}";
        }

        /// <summary>
        /// Метод, вызываемый при перетаскивании 
        /// ползунка 60Hz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar_60Hz_Scroll(object sender, EventArgs e)
        {
            amplitude_60Hz = trackBar_60Hz.Value;
            Draw();
        }

        /// <summary>
        /// Метод, вызываемый при перетаскивании 
        /// ползунка 120Hz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar_120Hz_Scroll(object sender, EventArgs e)
        {
            amplitude_120Hz = trackBar_120Hz.Value;
            Draw();
        }

        /// <summary>
        /// Метод, вызываемый при перетаскивании 
        /// ползунка 180Hz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar_180Hz_Scroll(object sender, EventArgs e)
        {
            amplitude_180Hz = trackBar_180Hz.Value;
            Draw();
        }

        /// <summary>
        /// Метод, вызываемый при перетаскивании 
        /// ползунка 240Hz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar_240Hz_Scroll(object sender, EventArgs e)
        {
            amplitude_240Hz = trackBar_240Hz.Value;
            Draw();
        }
    }
}
