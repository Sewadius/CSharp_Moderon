﻿using System;
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
        
        private readonly byte WRITE_TRY = 10;   // Количество попыток записи для одного регистра

        private ushort countNote = 0;           // Счётчик записи для отображения адреса значения

        // Массивы для записи текста в textBox с прочитанными данными
        private readonly string[] UI_TEXT = ["UI", "EX1_UI", "EX2_UI", "EX3_UI"];
        private readonly string[] DO_TEXT = ["DO", "EX1_DO", "EX2_DO", "EX3_DO"];
        private readonly string[] AO_TEXT = ["AO", "EX1_AO", "EX2_AO", "EX3_AO"];

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

            Thread thread = new(() =>
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

            modbusRTU = new()
            {
                mySp = serialPort,
                PortName = canSelectBox.SelectedItem.ToString(),
                BaudRate = int.Parse(speedCanCombo.SelectedItem.ToString()),
                Parity = SetParity(),
                DataBits = 8,
                StopBits = StopBits.One,
                Address = byte.Parse(canAddressBox.Text)
            };

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

                    backCanPanelButton.Invoke((MethodInvoker)(() =>
                        backCanPanelButton.Enabled = !isConnected
                    ));

                    backConnectLabel.Invoke((MethodInvoker)(() =>
                        backConnectLabel.Visible = isConnected
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

            Thread.Sleep(MS_200);                        // Задержка 200 ms
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
            backCanPanelButton.Enabled = enable;
            backConnectLabel.Visible = !enable;
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
            if (PLC_connectionError(sender, e)) return;     // Выход при потери связи с ПЛК

            ushort startAddress = 0;                        // Начальный адрес для записи
            short[] values = new short[2];                  // Формирование ответа
            bool writeSuccess = true;                       // Признак успешной записи в ПЛК 
            byte tryWrite_counter = WRITE_TRY;              // Счётчик количества попыток записи
            byte codeAnswer;                                // Код ответа при попытке записи

            processWriteLabel.Visible = true;               // Отображение подписи для полосы загрузки
            progressBarWrite.Visible = true;                // Отображение полосы загрузки
            Refresh();                                      // Обновление формы для отображения элементов

            progressBarWrite.Minimum = 0;
            progressBarWrite.Maximum = 105;
            progressBarWrite.Value = 0;
            progressBarWrite.Step = 1;

            // Блокировка для кнопок во время загрузки
            connectPlkBtn.Enabled = false;
            loadCanButton.Enabled = false;
            readCanButton.Enabled = false;

            // Запись для UI сигналов
            for (ushort i = 0; i < uiSignals.Length; i++)
            {
                do  // Проверка кода при попытке записи
                {
                    codeAnswer = modbusRTU.WriteFc6(modbusRTU.Address, startAddress, uiSignals[i], ref values);
                    if (tryWrite_counter > 0) tryWrite_counter--;
                    else break;
                } while (codeAnswer != 0);

                if (tryWrite_counter == 0) break;
                tryWrite_counter = WRITE_TRY;
                startAddress++;
                progressBarWrite.PerformStep();
            }

            // Запись для DO сигналов
            for (ushort i = 0; i < doSignals.Length; i++)
            {
                do  // Проверка кода при попытке записи
                {
                    codeAnswer = modbusRTU.WriteFc6(modbusRTU.Address, startAddress, doSignals[i], ref values);
                    if (tryWrite_counter > 0) tryWrite_counter--;
                    else break;
                } while (codeAnswer != 0);

                if (tryWrite_counter == 0) break;
                tryWrite_counter = WRITE_TRY;
                startAddress++;
                progressBarWrite.PerformStep();
            }

            // Запись для AO сигналов
            for (ushort i = 0; i < aoSignals.Length; i++)
            {
                do  // Проверка кода при попытке записи
                {
                    codeAnswer = modbusRTU.WriteFc6(modbusRTU.Address, startAddress, aoSignals[i], ref values);
                    if (tryWrite_counter > 0) tryWrite_counter--;
                    else break;
                } while (codeAnswer != 0);

                if (tryWrite_counter == 0) break;
                tryWrite_counter = WRITE_TRY;
                startAddress++;
                progressBarWrite.PerformStep();
            }

            startAddress = 84;  // Начальный адрес для записи командных слов

            // Запись для командных слов
            for (ushort i = 0; i < cmdWords.Length; i++)
            {
                do  // Проверка кода при попытке записи
                {
                    codeAnswer = modbusRTU.WriteFc6(modbusRTU.Address, startAddress, cmdWords[i], ref values);
                    if (tryWrite_counter > 0) tryWrite_counter--;
                    else break;
                } while (codeAnswer != 0);

                if (tryWrite_counter == 0) break;
                tryWrite_counter = WRITE_TRY;
                startAddress++;
                progressBarWrite.PerformStep();
            }

            // Неуспешная запись, сообщение об ошибке
            if (progressBarWrite.Value != progressBarWrite.Maximum)
            {
                writeSuccess = false;
                MessageBox.Show("Ошибка записи данных в контроллер!", "Ошибка записи",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                dataMatchPLC_label.Text = "(неизвестно)";           // Новое сообщение о статусе данных
                dataMatchPLC_label.ForeColor = Color.Black;         // Изменение цвета для сообщения

                ConnectPlkBtn_Click(this, e);                       // Закрытие соединения с ПЛК
                RefreshCanPorts_Click(this, e);                     // Обновление сведений о COM портах
            }
                
            // Разблокирока кнопок после загрузки
            connectPlkBtn.Enabled = true;

            if (writeSuccess)
            {
                loadCanButton.Enabled = true;       // Запись данных ПЛК
                readCanButton.Enabled = true;       // Чтение данных ПЛК
            }

            // Сообщение об успешной записи и повторное чтение данных из ПЛК
            if (writeSuccess) MessageBox.Show("Запись в ПЛК успешно завершена!", 
                "Операция выполнена", MessageBoxButtons.OK, MessageBoxIcon.Information);

            processWriteLabel.Visible = false;          // Скрытие панели прогресса загрузки
            progressBarWrite.Visible = false;           // Скрытие прогресса записи после загрузки

            if (writeSuccess)                           // При успешной записи данных
            {
                ReadCanButton_Click(this, e);           // Повторное чтение для проверки загрузки
            }
        }

        ///<summary>Потеря связи с контроллером для чтения/записи</summary>
        private bool PLC_connectionError(object sender, EventArgs e)
        {
            if (!CheckPLC_connection())     // Если нет связи с контроллером 
            {
                MessageBox.Show("Потеря связи с контроллером!", "Ошибка соединения",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                dataMatchPLC_label.Text = "(неизвестно)";           // Новое сообщение о статусе данных
                dataMatchPLC_label.ForeColor = Color.Black;         // Изменение цвета для сообщения

                ConnectPlkBtn_Click(this, e);                       // Закрытие соединения с ПЛК
                RefreshCanPorts_Click(this, e);                     // Обновление сведений о COM портах

                return true;
            }

            return false;
        }

        ///<summary>Нажали на кнопку "Читать данные из ПЛК"</summary>
        private void ReadCanButton_Click(object sender, EventArgs e)
        {
            if (PLC_connectionError(sender, e)) return;     // Выход при потери связи с ПЛК

            dataCanTextBox.Text = "";                       // Очистка текстового поля для прочтённых данных
            countNote = 0;                                  // Обнуление счётчика по адресам ПЛК

            // Чтение UI сигналов из ПЛК
            ReadUI_Values_fromPLC(); dataCanTextBox.Text += Environment.NewLine;

            // Чтение DO сигналов из ПЛК
            ReadDO_Values_fromPLC(); dataCanTextBox.Text += Environment.NewLine;

            // Чтение AO сигналов из ПЛК
            ReadAO_Values_fromPLC(); dataCanTextBox.Text += Environment.NewLine;

            // Чтение командных слов из ПЛК
            ReadCMD_Values_fromPLC();

            // Проверка совпадения данных в окнах для чтения / записи
            if (dataCanTextBox.Text == writeCanTextBox.Text)                    // Данные совпадают
            {
                dataMatchPLC_label.Text = "Данные в ПЛК совпадают";
                dataMatchPLC_label.ForeColor = Color.DarkGreen;
            }
            else                                                                // Данные не совпадают
            {
                dataMatchPLC_label.Text = "Данные в ПЛК не совпадают";
                dataMatchPLC_label.ForeColor = Color.Red;
            }
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
                
                if (countNote != border - 1)                        // Не добавляет пустую строку для последнего элемента
                {
                    dataCanTextBox.Text += Environment.NewLine;
                    value_index += 1;
                }
            }
        }
    }
}
