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


        SerialPort sp;

        AnalogInput analogInput = new AnalogInput();


        public Form1()
        {
            InitializeComponent();
            Draw();

            //sp = new SerialPort();
            //sp.BaudRate = 115200;
            //sp.PortName = "COM4";
            //sp.ReadTimeout = 5;
            //sp.Open();

            //SerialPortRead();

            RTSerialCom.SerialClient sclient = new RTSerialCom.SerialClient("COM4", 115200);
            sclient.OpenConn();
            sclient.OnReceiving += Sclient_OnReceiving;

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        int counter = 0;

        private void Timer_Tick(object sender, EventArgs e)
        {
            label5.Text = counter.ToString();
            counter = 0;
        }

        private void Sclient_OnReceiving(object sender, RTSerialCom.DataStreamEventArgs e)
        {
            var buffer =  e.Response;
            var values = analogInput.AddArray(buffer);

            Action action = () =>
            {
                foreach (var value in values)
                {
                    //Добавление точки на график амплитуды
                    chartAmplitude.Series[0].Points.AddXY(0, value);
                    X++;

                    counter++;
                }

                //Если на графике амплитуды точек больше 100 - удаляем самую первую
                do
                {
                    chartAmplitude.Series[0].Points.RemoveAt(0);
                }
                while (chartAmplitude.Series[0].Points.Count > 1000);

                //Костыль
                for (int i = 0; i < chartAmplitude.Series[0].Points.Count; i++)
                {
                    chartAmplitude.Series[0].Points[i].XValue = i;
                }

                //Очистка графика спектра
                chartSpectr.Series[0].Points.Clear();

                //Если точек меньше 1000 - выход
                if (chartAmplitude.Series[0].Points.Count < 1000)
                {
                    return;
                }

                Complex[] samplies = new Complex[numSamples];


                for (int i = 0; i < numSamples; i++)
                {
                    samplies[i] = new Complex(chartAmplitude.Series[0].Points[i].YValues[0], 0);
                }

                Fourier.Forward(samplies, FourierOptions.NoScaling);


                //Получаем спектр
                for (int i = 0; i < samplies.Length / 2; i++)
                {
                    double mag = (2.0 / numSamples) * Math.Abs(Math.Sqrt(Math.Pow(samplies[i].Real, 2) + Math.Pow(samplies[i].Imaginary, 2)));
                    double hzSample = sampleRate / numSamples;

                    chartSpectr.Series[0].Points.AddXY(hzSample * i, mag);
                }


                Get_RMS_A();
                Get_RMS_F();
            };

            this.Invoke(action);
        }

        void SerialPortRead()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {


                    if (sp.BytesToRead > 0)
                    {

                        byte bt = (byte)sp.ReadByte();
                        var value = analogInput.Add(bt);

                        if (value != null)
                        {
                            buffer.Add((int)value);

                            if (buffer.Count > 1000)
                            {
                                buffer.RemoveAt(0);
                            }

                            Action action = () =>
                            {
                                DrawOnline((int)value);
                            };

                            this.Invoke(action);
                        }
                    }
                }
            });
        }

        Int16 X = 0;
        void DrawOnline(int value)
        {
            //Добавление точки на график амплитуды
            chartAmplitude.Series[0].Points.AddXY(0, value);
            X++;

            //Если на графике амплитуды точек больше 100 - удаляем самую первую
            if (chartAmplitude.Series[0].Points.Count > 100)
            {
                chartAmplitude.Series[0].Points.RemoveAt(0);
            }

            //Костыль
            for (int i = 0; i < chartAmplitude.Series[0].Points.Count; i++)
            {
                chartAmplitude.Series[0].Points[i].XValue = i;
            }

            //Очистка графика спектра
            chartSpectr.Series[0].Points.Clear();

            //Если точек меньше 1000 - выход
            if (buffer.Count < 1000)
            {
                return;
            }

            Complex[] samplies = new Complex[numSamples];


            for (int i = 0; i < numSamples; i++)
            {
                samplies[i] = new Complex(buffer[i], 0);
            }

            Fourier.Forward(samplies, FourierOptions.NoScaling);


            //Получаем спектр
            for (int i = 0; i < samplies.Length / 2; i++)
            {
                double mag = (2.0 / numSamples) * Math.Abs(Math.Sqrt(Math.Pow(samplies[i].Real, 2) + Math.Pow(samplies[i].Imaginary, 2)));
                double hzSample = sampleRate / numSamples;

                chartSpectr.Series[0].Points.AddXY(hzSample * i, mag);
            }

            Get_RMS_A();
            Get_RMS_F();
        }

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

        private void trackBar_60Hz_Scroll(object sender, EventArgs e)
        {
            amplitude_60Hz = trackBar_60Hz.Value;
            Draw();
        }

        private void trackBar_120Hz_Scroll(object sender, EventArgs e)
        {
            amplitude_120Hz = trackBar_120Hz.Value;
            Draw();
        }

        private void trackBar_180Hz_Scroll(object sender, EventArgs e)
        {
            amplitude_180Hz = trackBar_180Hz.Value;
            Draw();
        }

        private void trackBar_240Hz_Scroll(object sender, EventArgs e)
        {
            amplitude_240Hz = trackBar_240Hz.Value;
            Draw();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
