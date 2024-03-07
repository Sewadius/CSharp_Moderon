using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Drawing;
using System.Threading.Tasks;

// Файл для соединения, подготовки и загрузки файла в ПЛК через CAN-порт

namespace Moderon
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort = new SerialPort();
        private ModbusRTU modbusRTU = new();

        private readonly int MS_200 = 200;      // Задержка 200 мс
        private readonly int MS_500 = 500;      // Задержка 500 мс
        private readonly int MS_1000 = 1000;    // Задержка 1000 мс

        private ushort countNote = 0;           // Счётчик записи для отображения адреса значения

        // Массивы для записи текста в textBox с прочитанными данными
        private readonly string[] UI_TEXT = { "UI", "EX1_UI", "EX2_UI", "EX3_UI" };
        private readonly string[] DO_TEXT = { "DO", "EX1_DO", "EX2_DO", "EX3_DO" };
        private readonly string[] AO_TEXT = { "AO", "EX1_AO", "EX2_AO", "EX3_AO" };

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
                Thread.Sleep(MS_1000);
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

            modbusRTU = new();
            
            modbusRTU.mySp = serialPort;
            modbusRTU.PortName = canSelectBox.SelectedItem.ToString();
            modbusRTU.BaudRate = int.Parse(speedCanCombo.SelectedItem.ToString());
            modbusRTU.Parity = SetParity();
            modbusRTU.DataBits = 8;
            modbusRTU.StopBits = StopBits.One;
            modbusRTU.Address = byte.Parse(canAddressBox.Text);

            if (!modbusRTU.mySp.IsOpen)  // Изначально порт COM закрыт, открытие соединия
            {
                modbusRTU.StartSession();
                connectPlkBtn.Enabled = false;  // Блокировка кнопки на время запроса

                Thread thread = new(() =>
                {
                    Thread.Sleep(MS_500);
                    bool isConnected = CheckPLC_connection();

                    if (!isConnected)
                    {
                        modbusRTU.StopSession();         // Закрытие COM порта
                    }

                    connectCanLabel.Invoke((MethodInvoker)(() =>
                    {
                        connectCanLabel.ForeColor = 
                            isConnected ? Color.DarkGreen : Color.Red;
                        connectCanLabel.Text =
                            isConnected ? "Соединение установлено" : "Нет соединения"; 
                    }));

                    connectPlkBtn.Invoke((MethodInvoker)(() =>
                    {
                        if (!isConnected) connectPlkBtn.Text = "УСТАНОВИТЬ СОЕДИНЕНИЕ";
                        connectPlkBtn.Enabled = true;
                    }));

                    loadCanButton.Invoke((MethodInvoker)(() =>
                        loadCanButton.Enabled = isConnected
                    ));

                    readCanButton.Invoke((MethodInvoker)(() =>
                        readCanButton.Enabled = isConnected
                    ));
                    
                    canSelectBox.Invoke((MethodInvoker)(() =>
                        canSelectBox.Enabled = !isConnected
                    ));

                    canAddressBox.Invoke((MethodInvoker)(() =>
                        canAddressBox.Enabled = !isConnected
                    ));

                    speedCanCombo.Invoke((MethodInvoker)(() =>
                        speedCanCombo.Enabled = !isConnected
                    ));

                    parityCanCombo.Invoke((MethodInvoker)(() =>
                        parityCanCombo.Enabled = !isConnected
                    ));

                    refreshCanPorts.Invoke((MethodInvoker)(() =>
                        refreshCanPorts.Enabled = !isConnected
                    ));
                });

                thread.Start();
            }
            else                                        // Закрытие соединения
            {
                modbusRTU.StopSession();                // Закрытие COM порта
                BlockElements_forConnection(true);      // Разблокировка элементов для подключения
                dataCanTextBox.Text = "";               // Очистка текстового поля с прочтёнными данными
            }

            Thread.Sleep(MS_200);                           // Задержка 200 ms
            LabelButtonChange(modbusRTU.mySp.IsOpen);    // Изменение текста, цвета и проверка соединения
        }

        ///<summary>Изменение текста, цвета и блокировка кнопки загрузки</summary>
        private void LabelButtonChange(bool portOpen)
        {
            string[] PORT_STATUS = ["Порт COM открыт", "Нет соединения"];
            string[] CONNECT_STATUS = ["ЗАКРЫТЬ СОЕДИНЕНИЕ", "УСТАНОВИТЬ СОЕДИНЕНИЕ"];

            if (portOpen)                                   // Если порт открыт
            {
                connectCanLabel.Text = PORT_STATUS[0];      // Порт открыт
                connectCanLabel.ForeColor = Color.Blue;
                connectPlkBtn.Text = CONNECT_STATUS[0];
            }
            else                                            // Если порт закрыт
            {
                connectCanLabel.Text = PORT_STATUS[1];      // Нет соединения
                connectCanLabel.ForeColor = Color.Red;
                connectPlkBtn.Text = CONNECT_STATUS[1];
                loadCanButton.Enabled = false;              // Загрузка данных
                readCanButton.Enabled = false;              // Чтение данных
            }
        }

        ///<summary>Блокировка полей ввода и выбора при подключении</summary>
        private void BlockElements_forConnection(bool enable)
        {
            canSelectBox.Enabled = enable;
            canAddressBox.Enabled = enable;
            speedCanCombo.Enabled = enable;
            parityCanCombo.Enabled = enable;
            refreshCanPorts.Enabled = enable;
        }

        ///<summary>Проверка соединения с ПЛК через проверку чтения (функция 3)</summary>
        private bool CheckPLC_connection()
        {
            short[] values = new short[1];

            try
            {
                // Create a CancellationTokenSource with timeout
                using CancellationTokenSource cts = new(MS_200);
                Task<bool> sendTask = Task.Run(() => 
                    modbusRTU.SendFc3(modbusRTU.Address, 0, 1, ref values), cts.Token);

                // Wait for the task to complete or timeout to occur
                if (sendTask.Wait(MS_200))
                {
                    return sendTask.Result;
                }
                else
                {
                    cts.Cancel();
                    return false;
                }
            }
            catch { return false; }
        }

        ///<summary>Нажали на кнопку "Загрузить данные в ПЛК"</summary>
        private void LoadCanButton_Click(object sender, EventArgs e)
        {
            short[] values = new short[1];

            if (modbusRTU.SendFc3(modbusRTU.Address, 0, 1, ref values))
                MessageBox.Show(values[0].ToString());
        }

        ///<summary>Нажали на кнопку "Читать данные из ПЛК"</summary>
        private void ReadCanButton_Click(object sender, EventArgs e)
        {
            dataCanTextBox.Text = "";               // Очистка текстового поля для прочтённых данных
            countNote = 0;                          // Обнуление счётчика по адресам

            // Чтение UI сигналов из ПЛК
            ReadUI_Values_fromPLC(); dataCanTextBox.Text += Environment.NewLine;

            // Чтение DO сигналов из ПЛК
            ReadDO_Values_fromPLC(); dataCanTextBox.Text += Environment.NewLine;

            // Чтение AO сигналов из ПЛК
            ReadAO_Values_fromPLC(); dataCanTextBox.Text += Environment.NewLine;

            // Чтение командных слов из ПЛК
            ReadCMD_Values_fromPLC();
        }

        ///<summary>Чтение данных по UI входам из регистров ПЛК</summary>
        private void ReadUI_Values_fromPLC()
        {
            ushort UI_PLC_LENGTH = 16;

            // ПЛК, UI сигналы
            short[] values = new short[UI_PLC_LENGTH];

            for (int i = 0; i < 3; i++)
            {
                modbusRTU.SendFc3(modbusRTU.Address, countNote, UI_PLC_LENGTH, ref values);
                WriteUI_Values_toTextBox(i, UI_PLC_LENGTH, values);
            }   
        }

        ///<summary>Запись для группы UI сигналов в текстовое поле textBox</summary>
        private void WriteUI_Values_toTextBox(int counter, ushort length, short[] values)
        {
            int border = countNote + length;                // Граница по длине чтения группы данных
            int value_index = 0;
            const ushort LENGTH = 4;                        // Размер строки для количества табуляций

            for (; countNote < border; countNote++)
            {
                if (UI_TEXT[counter].Length > LENGTH)
                    dataCanTextBox.Text += $"{countNote}) {UI_TEXT[counter]}{value_index + 1}:\t{values[value_index]}";
                else
                    dataCanTextBox.Text += $"{countNote}) {UI_TEXT[counter]}{value_index + 1}:\t\t{values[value_index]}";

                dataCanTextBox.Text += Environment.NewLine;
                value_index += 1;
            }
        }

        ///<summary>Чтение данных по DO выходам из регистров ПЛК</summary>
        private void ReadDO_Values_fromPLC()
        {
            ushort DO_PLC_LENGTH = 10;

            short[] values = new short[DO_PLC_LENGTH];

            for (int i = 0; i < 3; i++)
            {
                modbusRTU.SendFc3(modbusRTU.Address, countNote, DO_PLC_LENGTH, ref values);
                WriteDO_Values_toTextBox(i, DO_PLC_LENGTH, values);
            }
        }

        ///<summary>Запись для группы DO сигналов в текстовое поле textBox</summary>
        private void WriteDO_Values_toTextBox(int counter, ushort length, short[] values)
        {
            int border = countNote + length;
            int value_index = 0;
            const ushort LENGTH = 4;                    // Размер строки для количества табуляций

            if (counter > 0) border -= 2;               // 10 DO для ПЛК и 8 DO для блоков расширения

            for (; countNote < border; countNote++)
            {
                if (DO_TEXT[counter].Length > LENGTH || value_index == 9)
                    dataCanTextBox.Text += $"{countNote}) {DO_TEXT[counter]}{value_index + 1}:\t{values[value_index]}";
                else
                    dataCanTextBox.Text += $"{countNote}) {DO_TEXT[counter]}{value_index + 1}:\t\t{values[value_index]}";

                dataCanTextBox.Text += Environment.NewLine;
                value_index += 1;
            }
        }

        ///<summary>Чтение данных по AO выходам из регистров ПЛК</summary>
        private void ReadAO_Values_fromPLC()
        {
            ushort AO_PLC_LENGTH = 3;

            short[] values = new short[AO_PLC_LENGTH];

            for (int i = 0; i < 3; i++)
            {
                modbusRTU.SendFc3(modbusRTU.Address, countNote, AO_PLC_LENGTH, ref values);
                WriteAO_Values_toTextBox(i, AO_PLC_LENGTH, values);
            }
        }

        ///<summary>Запись для группы AO сигналов в текстовое поле textBox</summary>
        private void WriteAO_Values_toTextBox(int counter, ushort length, short[] values)
        {
            int border = countNote + length;
            int value_index = 0;
            const ushort LENGTH = 4;

            if (counter > 0) border -= 1;

            for (; countNote < border; countNote++)
            {
                if (AO_TEXT[counter].Length > LENGTH)
                    dataCanTextBox.Text += $"{countNote}) {AO_TEXT[counter]}{value_index + 1}:\t{values[value_index]}";
                else
                    dataCanTextBox.Text += $"{countNote}) {AO_TEXT[counter]}{value_index + 1}:\t\t{values[value_index]}";

                dataCanTextBox.Text += Environment.NewLine;
                value_index += 1;
            }
        }

        ///<summary>Чтение данных по командным словам из регистров ПЛК</summary>
        private void ReadCMD_Values_fromPLC()
        {
            ushort CMD_PLC_LENGTH = 30;                     // Количество командных слов
            countNote = 84;                                 // Начальный адрес для считывания командных слов

            short[] values = new short[CMD_PLC_LENGTH];

            modbusRTU.SendFc3(modbusRTU.Address, countNote, CMD_PLC_LENGTH, ref values);
            WriteCmd_Values_toTextBox(CMD_PLC_LENGTH, values);
        }

        ///<summary>Запись для группы cmdWords в текстовое поле textBox</summary>
        private void WriteCmd_Values_toTextBox(ushort length, short[] values)
        {
            int border = countNote + length;
            int value_index = 0;

            for (; countNote < border; countNote++)
            {
                dataCanTextBox.Text += $"{countNote}) Word_{value_index + 1}:\t{values[value_index]}";
                
                dataCanTextBox.Text += Environment.NewLine;
                value_index += 1; 
            }
        }
    }
}
