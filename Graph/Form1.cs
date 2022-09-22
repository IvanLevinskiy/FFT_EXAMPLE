using MathNet.Numerics.IntegralTransforms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;


namespace FTT_ARDUINO
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Экземпляр Serial port
        /// </summary>
        RTSerialCom.SerialClient serial;

        int numSamples = 2048;
        int sampleRate = 1024;

        /// <summary>
        /// Аналоговый вход
        /// </summary>
        AnalogInput analogInput = new AnalogInput();        

        //Коллекции точек на графике
        PointPairList S0 = new PointPairList();
        PointPairList S1 = new PointPairList();

        /// <summary>
        /// GraphPanes
        /// </summary>
        GraphPane gpFTT;

        /// <summary>
        /// Буфер точек
        /// </summary>
        List<double> bufferValues = new List<double>();

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            initGraphPane();

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick_;
            timer.Start();

            Task.Factory.StartNew(() =>
            {
                udp();
            });

            
        }

        int id_frame = 0;
        int adc_raw = 0;

        /// <summary>
        /// Локальный сокет
        /// </summary>
        private Socket _mSocket;

        /// <summary>
        /// IP адрес
        /// </summary>
        public string IP
        {
            get
            {
                return ip;
            }
            set
            {
                ip = value;
            }
        }
        string ip = "192.168.100.6";

        /// <summary>
        /// TCP порт
        /// </summary>
        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }
        int port = 180;

        /// <summary>
        /// Метод для открытия сессии с ПЛК
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            try
            {
                this._mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this._mSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 1000);
                this._mSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 1000);
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(this.IP), Port);
                this._mSocket.Connect(remoteEP);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Метод для завершения сессии с ПЛК
        /// </summary>
        public void Close()
        {

            if (this._mSocket != null && this._mSocket.Connected)
            {
                this._mSocket.Shutdown(SocketShutdown.Both);
                this._mSocket.Close();
            }
        }

        List<double> frame = new List<double>();

        void udp()
        {
            //Наполнение пустыми точками
            for (int i = 0; i < 1024 / 2; i++)
            {
                double hzSample = sampleRate / numSamples;

                S1.Add(hzSample * i, 0);
            }

            //TcpListener server = null;

            //IPAddress ipAddr = IPAddress.Parse(IP);
            //server = new TcpListener(IPAddress.Any, Port);
            //server.Start();



            //while (true)
            //{
            //    // получаем входящее подключение
            //    TcpClient client = server.AcceptTcpClient();

            //    // получаем сетевой поток для чтения и записи
            //    NetworkStream stream = client.GetStream();

            //    byte[] buffer = new byte[2048];

            //    var q = stream.Read(buffer, 0, buffer.Length);

            //    client.Close();

            //    int counter = 0;

            //    for(int i = 0; i < q; i += 2)
            //    {
            //        int value = buffer[i + 1] << 8;
            //            value |= buffer[i];

            //            frame[counter] = value;

            //           counter++;
            //    }

            //    Udp_OnReceiving();
            //}


            

            while (true)
            {
                UdpClient client = new UdpClient(6300);
                IPEndPoint ip = null;
                byte[] data = client.Receive(ref ip);

                int counter = 0;

                for (int j = 0; j < data.Length - 1; j += 2)
                {
                    int value = data[j + 1] << 8;
                    value |= data[j];

                    frame.Add(value);

                    if(frame.Count > numSamples)
                    {
                        frame.RemoveAt(0);
                    }

                    counter++;
                }

                client.Close();

                Udp_OnReceiving();
            }

            var open = Open();

            while (true)
            {
                //Проверка на нулевой указатель
                if (this._mSocket == null)
                {
                    continue;
                }

                byte[] b = new byte[1];
                byte[] buffer = new byte[2048];
                this._mSocket.Send(b, 1, SocketFlags.None);

                var count = _mSocket.Receive(buffer, buffer.Length, SocketFlags.None);
         

                for(int i = 0; i < count; i += 2)
                {
                    int value = buffer[i + 1] << 8;
                    value |= buffer[i];

                    int index = i / 2;
                    frame[index] = value;
                }



                Udp_OnReceiving();

            }

            Close();

        }

        private void Udp_OnReceiving()
        {

            Action action = () =>
            {
                S1.Clear();

                Complex[] samplies = new Complex[frame.Count];

                for (int i = 0; i < frame.Count; i++)
                {
                    samplies[i] = new Complex(frame[i], 0.0);
                }

                Fourier.Forward(samplies, FourierOptions.NoScaling);

                int counter = 0;

                for (int i = 0; i < samplies.Length; i+=2)
                {
                    double mag = (2.0 / numSamples) * Math.Abs(Math.Sqrt(Math.Pow(samplies[i].Real, 2) + Math.Pow(samplies[i].Imaginary, 2)));
                    double hzSample = sampleRate / numSamples;

                    //if (hzSample * i >= 5)
                    //{
                        S1.Add(hzSample * i, mag);
                    //}

                    counter++;
                }

                // Обновляем график
                zgFrequency.Invalidate();
                zgFrequency.Refresh();
                zgFrequency.AxisChange();
            };

            this.Invoke(action);
        }



        Random random = new Random();

        double x = 0;

        private void Timer_Tick_(object sender, EventArgs e)
        {
            if (bufferValues.Count < 1000)
            {
                return;
            }

            S0.Clear();

            double x = 0.0;

            for(double i = 0; i < 100; i +=1.0)
            {
                PointPair p = new PointPair(i, bufferValues[(int)i]);
                S0.Add(p);
            }

            return; 

            var value = Math.Sin(x * 2 * Math.PI / 60);
            value += Math.Cos(x * 2 * Math.PI / 30) * 10.0;
            value += Math.Sin(x * 2 * Math.PI / 10);
            value += Math.Sin(x * 2 * Math.PI / 5);
            value += Math.Sin(x * 2 * Math.PI / 1);


            value = value * 1000.0;
            value += (double)random.Next(-500, 500);

            x += 1.0;

            //Добавление точки на график амплитуды
            PointPair point = new PointPair(new XDate(DateTime.Now), value);
            S0.Add(point);

            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            // В противном случае на рисунке будет показана только часть графика,
            // которая умещается в интервалы по осям, установленные по умолчанию
            //zgFrequency.AxisChange();

            // Обновляем график
            //zgFrequency.Invalidate();

            //zgFrequency.Refresh();

            //Если на графике амплитуды точек больше 100 - удаляем самую первую
            if(S0.Count > 1000)
            {
                S0.RemoveAt(0);
            }           

            //Если точек меньше 1000 - выход
            if (S0.Count < 1000)
            {
                return;
            }

            //Очистка графика спектра
            S1.Clear();

            //Создание массива комплексных чисел
            Complex[] samplies = new Complex[numSamples];

            //Перемещение данных из амплитудного графика
            //в массив комплексных чисел
            for (int i = 0; i < numSamples; i++)
            {
                samplies[i] = new Complex(S0[i].Y, 0);
            }

            //Выполнение преобразования фурье
            Fourier.Forward(samplies, FourierOptions.NoScaling);

            var mag = samplies.Select(c => c.Magnitude).Take(samplies.Length / 2).ToArray();
            var freq = Fourier.FrequencyScale(samplies.Length, 1000).Take(samplies.Length / 2).ToList();


            //Вывод спектра на график
            //for (int i = 0; i < samplies.Length / 2; i++)
            //{
            //    double mag = (2.0 / numSamples) * Math.Abs(Math.Sqrt(Math.Pow(samplies[i].Real, 2) + Math.Pow(samplies[i].Imaginary, 2)));
            //    double hzSample = sampleRate / numSamples;

            //    S1.Add(hzSample * i, mag);
            //}

            for (int i = 0; i < mag.Length; i++)
            {
                //double mag = (2.0 / numSamples) * Math.Abs(Math.Sqrt(Math.Pow(samplies[i].Real, 2) + Math.Pow(samplies[i].Imaginary, 2)));
                double hzSample = sampleRate / numSamples;

                S1.Add(freq[i], mag[i]);
            }


            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            // В противном случае на рисунке будет показана только часть графика,
            // которая умещается в интервалы по осям, установленные по умолчанию
            //zgAmplitude.AxisChange();
            zgFrequency.AxisChange();

            // Обновляем график
            //zgAmplitude.Invalidate();
            zgFrequency.Invalidate();

            //zgAmplitude.Refresh();
            zgFrequency.Refresh();
        }

        /// <summary>
        /// Инициализация графика
        /// </summary>
        void initGraphPane()
        {
            /// <summary>
            /// Цвета графика
            /// </summary>
            Color GridColor;
            Color BackColor;
            Color CrosHairColor;

            //Экземпляры кривых
            LineItem s0_Curve;
            LineItem s1_Curve;

            //Получение GraphPane
            gpFTT = zgFrequency.GraphPane;

            //Подписка на перемещение курсора по графику
            //zedGraph.MouseMove += ZedGraph_MouseMove;

            //Подписка на изменение масштаба
            //zedGraph.ZoomEvent += zedGraph_ZoomEvent;

            //Подписка на показ сообщения о точке
            //zedGraph.PointValueEvent += zedGraph_PointValueEvent;

            //Подписка на клик по графику
            //zedGraph.MouseClick += zedGraph_MouseClick;

            //Определение цветов
            GridColor = System.Drawing.Color.Black;  //System.Drawing.Color.FromArgb(0x50, 0x83, 0x83, 0x86);
            BackColor = System.Drawing.Color.White;  //System.Drawing.Color.FromArgb(0xFF, 0x26, 0x26, 0x26);
            CrosHairColor = System.Drawing.Color.FromArgb(0xFF, 0x40, 0xA6, 0xA4);

           
            // Точки можно перемещать, как по горизонтали,...
            zgFrequency.IsEnableHEdit = false;

            // ... так и по вертикали.
            zgFrequency.IsEnableVEdit = false;

            //Убираем легенду
            gpFTT.Legend.IsVisible = false;

            // !!! Установим значение параметра IsBoundedRanges как true.
            // !!! Это означает, что при автоматическом подборе масштаба
            // !!! нужно учитывать только видимый интервал графика
            gpFTT.IsBoundedRanges = true;

            //Кнопки для перемещения графика
            //zedGraph.PanButtons = System.Windows.Forms.MouseButtons.Left;
            //zedGraph.PanModifierKeys = System.Windows.Forms.Keys.None;

            //Кнопки увеличения графика
            //zedGraph.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            //zedGraph.ZoomModifierKeys = System.Windows.Forms.Keys.Shift;

            // !!! Изменим угол наклона меток по осям. 
            // Углы задаются в градусах
            gpFTT.XAxis.Scale.FontSpec.Angle = 0;

            //Настройка обрамления
            gpFTT.XAxis.Scale.FontSpec.Angle = 0;

            //Масштам по умолчанию
            gpFTT.XAxis.Title = new AxisLabel("", "Times New Roman", 7, System.Drawing.Color.Blue, false, false, false);
            gpFTT.XAxis.Scale.FontSpec.Size = 7;
            gpFTT.XAxis.Scale.FontSpec.FontColor = Color.LightGray;
            gpFTT.XAxis.Title.FontSpec.FontColor = Color.LightGray;
            gpFTT.XAxis.MinorTic.Color = GridColor;
            gpFTT.XAxis.MajorTic.Color = GridColor;


            gpFTT.YAxis.Title = new AxisLabel("", "Times New Roman", 14, System.Drawing.Color.Black, false, false, false);
            gpFTT.YAxis.Scale.FontSpec.Size = 7;
            gpFTT.YAxis.Scale.FontSpec.FontColor = Color.LightGray;
            gpFTT.YAxis.Title.FontSpec.FontColor = Color.LightGray;
            gpFTT.YAxisList[0].Scale.IsVisible = true;
            gpFTT.YAxisList[0].Color = GridColor;
            gpFTT.YAxis.MinorTic.Color = GridColor;
            gpFTT.YAxis.MajorTic.Color = GridColor;

            // Заголовок

            gpFTT.Title.IsVisible = false;
            gpFTT.IsBoundedRanges = true;

            //Цвет осей
            gpFTT.XAxis.Scale.FontSpec.FontColor = GridColor;
            gpFTT.YAxis.Scale.FontSpec.FontColor = GridColor;

            //СЕТКА
            // Включаем отображение сетки напротив крупных рисок по оси X
            gpFTT.XAxis.MajorGrid.IsVisible = true;
            gpFTT.XAxis.MajorGrid.Color = GridColor;
            gpFTT.XAxis.MajorGrid.PenWidth = 0.2f;

            // Задаем вид пунктирной линии для крупных рисок по оси X:
            // Длина штрихов равна 10 пикселям, ... 
            gpFTT.XAxis.MajorGrid.DashOn = 10;

            // затем 5 пикселей - пропуск
            gpFTT.XAxis.MajorGrid.DashOff = 0;

            // Включаем отображение сетки напротив крупных рисок по оси Y
            gpFTT.YAxis.MajorGrid.IsVisible = true;
            gpFTT.YAxis.MajorGrid.Color = GridColor;
            gpFTT.YAxis.MajorGrid.PenWidth = 0.2f;
            gpFTT.YAxis.Color = GridColor;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            gpFTT.YAxis.MajorGrid.DashOn = 10;
            gpFTT.YAxis.MajorGrid.DashOff = 0;
                        
            
            // Включаем отображение сетки напротив мелких рисок по оси X
            gpFTT.YAxis.MinorGrid.IsVisible = false;
            gpFTT.YAxis.MinorGrid.Color = GridColor;
            gpFTT.YAxis.MinorGrid.PenWidth = 0.1f;

            // Задаем вид пунктирной линии для крупных рисок по оси Y: 
            // Длина штрихов равна одному пикселю, ... 
            gpFTT.YAxis.MinorGrid.DashOn = 1;

            // затем 2 пикселя - пропуск
            gpFTT.YAxis.MinorGrid.DashOff = 0;


            // Включаем отображение сетки напротив мелких рисок по оси Y
            gpFTT.XAxis.MinorGrid.IsVisible = false;
            gpFTT.XAxis.MinorGrid.Color = GridColor;
            gpFTT.XAxis.MinorGrid.PenWidth = 0.1f;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            gpFTT.XAxis.MinorGrid.DashOn = 1;
            gpFTT.XAxis.MinorGrid.DashOff = 0;

            gpFTT.XAxis.Color = GridColor;

            //Настраиваем остальные свойства пане
            gpFTT.Fill = new Fill(BackColor);
            gpFTT.Margin.Right = 5;
            gpFTT.Margin.Left = 10;

            //Цвет графика

            //Добавление кривых
            Color s1_color = System.Drawing.Color.FromArgb(0xFF, 0xED, 0xCF, 0x06);
            s1_Curve = gpFTT.AddCurve("", S1, Color.Green, SymbolType.None);
            s1_Curve.Line.Width = 1.3f;


            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            // В противном случае на рисунке будет показана только часть графика,
            // которая умещается в интервалы по осям, установленные по умолчанию
            zgFrequency.AxisChange();

            // Обновляем график
            zgFrequency.Invalidate();

            zgFrequency.Refresh();
        }

        /// <summary>
        /// Загрузка портов при выборе выпадабщего списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxPorts_Click(object sender, EventArgs e)
        {
            var ports = SerialPort.GetPortNames();
            cbxPorts.Items.Clear();
            cbxPorts.Items.AddRange(ports);
        }

        /// <summary>
        /// Метод, вызываемый при изменении
        /// выбранного порта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxPorts_SelectedValueChanged(object sender, EventArgs e)
        {
            buttonStream.Enabled = !string.IsNullOrEmpty(cbxPorts.SelectedItem.ToString());
        }

        private void buttonStream_Click(object sender, EventArgs e)
        {
            if(serial == null)
            {
                var portname = cbxPorts.SelectedItem.ToString();
                serial = new RTSerialCom.SerialClient(portname, 115200);
                serial.OpenConn();
                serial.OnReceiving += Serial_OnReceiving;
                return;
            }

            serial.CloseConn();
            serial.OnReceiving -= Serial_OnReceiving;
            serial = null;
        }


        private void Serial_OnReceiving(object sender, RTSerialCom.DataStreamEventArgs e)
        {
            var buffer = e.Response;
            var values = analogInput.AddArray(buffer);

            Action action = () =>
            {
                foreach (var value in values)
                {
                    bufferValues.Add((double)value);
                }

                //Если на графике амплитуды точек больше 100 - удаляем самую первую
                if(bufferValues.Count > numSamples)
                {
                    do
                    {
                        bufferValues.RemoveAt(0);
                    }
                    while (bufferValues.Count > numSamples);
                }

                //Если точек меньше 1000 - выход
                if (bufferValues.Count < numSamples)
                {
                    return;
                }


                if(S0.Count >= numSamples)
                {
                    S0.Clear();
                }

                //Добавление точки на график амплитуды
                //PointPair point = new PointPair(new XDate(DateTime.Now), value);
                //S0.Add(point);

                //Очистка графика спектра
                S1.Clear();

                //zgAmplitude.AxisChange();

                // Обновляем график
                //zgAmplitude.Invalidate();
                //zgAmplitude.Refresh();
                //zgAmplitude.AxisChange();

                Complex[] samplies = new Complex[numSamples];

                for (int i = 0; i < numSamples; i++)
                {
                    samplies[i] = new Complex(bufferValues[i], 0.0);
                }

                Fourier.Forward(samplies, FourierOptions.NoScaling);

                //Получаем спектр
                //for (int i = 0; i < samplies.Length / 2; i++)
                //{
                //    double mag = (2.0 / numSamples) * Math.Abs(Math.Sqrt(Math.Pow(samplies[i].Real, 2) + Math.Pow(samplies[i].Imaginary, 2)));
                //    double hzSample = sampleRate / numSamples;

                //    S1.Add(hzSample * i, mag);
                //}

                for (int i = 0; i < samplies.Length/2; i++)
                {
                    double mag = (2.0 / numSamples) * Math.Abs(Math.Sqrt(Math.Pow(samplies[i].Real, 2) + Math.Pow(samplies[i].Imaginary, 2)));
                    double hzSample = sampleRate/numSamples;

                    if (hzSample * i >= 5)
                    {
                        S1.Add(hzSample * i, mag);
                    }
                    
                }

                // Обновляем график
                zgFrequency.Invalidate();
                zgFrequency.Refresh();
                zgFrequency.AxisChange();


                //Get_RMS_A();
                //Get_RMS_F();
            };

            this.Invoke(action);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
