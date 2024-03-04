using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Drawing;

// Файл для соединения, подготовки и загрузки файла в ПЛК через CAN-порт

namespace Moderon
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;

        /// <summary>Инициализация для загрузки по CAN порту</summary>
        public void InitializeCAN()
        {
            serialPort = new SerialPort();              // Инициализация объекта SerialPort
            GetSerialPorts();                           // Формирование comboBox с актуальными CAN портами

            // Выбор скорости и чётности соединения по умолчанию
            speedCanCombo.SelectedIndex = 0;            // Скорость 9600
            parityCanCombo.SelectedIndex = 0;           // Чётность Even
        }

        /// <summary>Получение доступных CAN портов для выбора</summary>
        public void GetSerialPorts()
        {
            canSelectBox.Items.Clear();

            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
                canSelectBox.Items.Add(port);

            if (canSelectBox.Items.Count > 0)
                canSelectBox.SelectedIndex = 0;
        }

        /// <summary>Обновление списка CAN портов</summary>
        private void RefreshCanPorts_Click(object sender, EventArgs e)
        {
            refreshCanPorts.Image = Properties.Resources.refresh_red;
            GetSerialPorts();                           // Формирование comboBox с актуальными CAN портами

            Thread thread = new Thread(() =>
            {
                Thread.Sleep(1000);
                refreshCanPorts.Image = Properties.Resources.refresh;

            });
            thread.Start();
        }

        /// <summary>Выбор чётности в зависимости от выбора comboBox</summary>
        private Parity SetParity()
        {
            Parity parity = Parity.None;

            switch (parityCanCombo.SelectedIndex)
            {
                case 0: parity = Parity.Even; break;
                case 1: parity = Parity.Odd; break;
            }

            return parity;
        }

        /// <summary>Нажали на кнопку "Подключить/Отключиться"</summary>
        private void ConnectPlkBtn_Click(object sender, EventArgs e)
        {
            if (canSelectBox.SelectedItem == null) return;

            string portName = canSelectBox.SelectedItem.ToString();                 // Номер порта
            int baudRate = int.Parse(speedCanCombo.SelectedItem.ToString());        // Скорость подключения
            Parity parity = SetParity();                                            // Чётность
            int dataBits = 8;
            StopBits stopBits = StopBits.One;

            serialPort.PortName = portName;
            serialPort.BaudRate = baudRate;
            serialPort.Parity = parity;
            serialPort.DataBits = dataBits;
            serialPort.StopBits = stopBits;

            try
            {
                if (!serialPort.IsOpen) serialPort.Open();
                else serialPort.Close();

                if (serialPort.IsOpen)
                {
                    connectCanLabel.Text = "Соединение установлено";
                    connectCanLabel.ForeColor = Color.DarkGreen;
                    connectPlkBtn.Text = "Отключиться от ПЛК";
                } 
                else
                {
                    connectCanLabel.Text = "Нет соединения";
                    connectCanLabel.ForeColor = Color.Red;
                    connectPlkBtn.Text = "Подключиться к ПЛК";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (serialPort.IsOpen) serialPort.Close();
            }
        }
    }
}
