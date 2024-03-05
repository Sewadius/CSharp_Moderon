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
        private ModbusRTU modbusRTU = new ModbusRTU();

        ///<summary>Инициализация для загрузки по CAN порту</summary>
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

            // Выбор последнего доступного CAN порта в списке
            if (canSelectBox.Items.Count > 0)
                canSelectBox.SelectedIndex = canSelectBox.Items.Count - 1;
        }

        ///<summary>Обновление списка CAN портов</summary>
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

        ///<summary>Выбор чётности в зависимости от выбора comboBox</summary>
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

        ///<summary>Нажали на кнопку "Подключить/Отключиться"</summary>
        private void ConnectPlkBtn_Click(object sender, EventArgs e)
        {
            if (canSelectBox.SelectedItem == null) return;
            
            modbusRTU.mySp = serialPort;
            modbusRTU.PortName = canSelectBox.SelectedItem.ToString();
            modbusRTU.BaudRate = int.Parse(speedCanCombo.SelectedItem.ToString());
            modbusRTU.Parity = SetParity();
            modbusRTU.DataBits = 8;
            modbusRTU.StopBits = StopBits.One;

            if (!modbusRTU.mySp.IsOpen)
            {
                modbusRTU.StartSession();
            }
            else
            {
                modbusRTU.StopSession();
            }

            LabelButtonChange(modbusRTU.mySp.IsOpen);
        }

        ///<summary>Изменение текста, цвета и блокировка кнопки загрузки</summary>
        private void LabelButtonChange(bool portOpen)
        {
            string[] PORT_STATUS = ["Порт COM открыт", "Нет соединения"];
            string[] CONNECT_STATUS = ["ЗАКРЫТЬ СОЕДИНЕНИЕ", "УСТАНОВИТЬ СОЕДИНЕНИЕ"];

            if (portOpen)
            {
                connectCanLabel.Text = PORT_STATUS[0];
                connectCanLabel.ForeColor = Color.Blue;
                connectPlkBtn.Text = CONNECT_STATUS[0];
                loadCanButton.Enabled = true;
            }
            else
            {
                connectCanLabel.Text = PORT_STATUS[1];
                connectCanLabel.ForeColor = Color.Red;
                connectPlkBtn.Text = CONNECT_STATUS[1];
                loadCanButton.Enabled = false;
            }
        }

        ///<summary>Нажали на кнопку "Загрузить данные в ПЛК"</summary>
        private void LoadCanButton_Click(object sender, EventArgs e)
        {
            byte address = byte.Parse(canAddressBox.Text);
            short[] values = new short[1];

            if (modbusRTU.SendFc3(address, 0, 1, ref values))
                MessageBox.Show(values[0].ToString());
        }
    }
}
