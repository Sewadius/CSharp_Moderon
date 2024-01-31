using System;
using System.Net;
using System.Windows.Forms;
using System.Drawing;
using GemBox.Spreadsheet;
using System.IO;

// Файл для соединения, чтения, подготовки и загрузки параметров в ПЛК

namespace Moderon
{
    public partial class Form1 : Form
    {
		Master MBmaster;
		private byte[] data;
		private ushort startAddress = 16749;
		int counter_read = 1; // Номер позиции для чтения из ПЛК

		///<summary>Изменения для панели данных для записи & изменение размера поля чтения</summary>
		private void LoadNetOnLoad()
        {
			writeNetTextBox.Hide();
			labelWriteNetTextBox.Hide();
			dataNetTextBox.Height = 400;
        }

		///<summary>Опция для отображения данных на запись в ПЛК</summary>
		private void ShowWriteBoxCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (showWriteBoxCheck.Checked) // Выбрали опцию
            {
				dataNetTextBox.Height = 200;
				labelWriteNetTextBox.Show();
				writeNetTextBox.Show();
            }
			else // Отмена выбора опции
            {
				writeNetTextBox.Hide();
				labelWriteNetTextBox.Hide();
				dataNetTextBox.Height = 400;
			}
		}

		///<summary>Нажали на кнопку "Подключить"</summary>
		private void ConnectBtn_Click(object sender, EventArgs e)
        {
			try
			{
				// Create new modbus master and add event functions
				MBmaster = new Master(ipAddressBox.Text, ushort.Parse(netPortBox.Text), true);
				MBmaster.OnResponseData += new Master.ResponseData(MBmaster_OnResponseData);
				MBmaster.OnException += new Master.ExceptionData(MBmaster_OnException);
				if (MBmaster.connected) // Соединение установлено
				{
					connectLabel.Text = "Соединение установлено";
					connectLabel.ForeColor = Color.Green;
				}
			}
			catch (SystemException error) // Обработка ошибки
			{
				MessageBox.Show(error.Message);
			}
		}

		///<summary>Нажали на кнопку "Отключить"</summary>
		private void DisconnectBtn_Click(object sender, EventArgs e)
		{
			if (MBmaster != null && !MBmaster.connected) // Разрыв соединения
			{
				MBmaster.disconnect(); // Отключение от ПЛК
			}
		}

		///<summary>Выбрали опцию "Подключиться к ПЛК"</summary>
		private void ConnectCheck_CheckedChanged(object sender, EventArgs e)
        {
			if (connectCheck.Checked) // Выбрали подключение к ПЛК
			{
				ConnectBtn_Click (this, e);
				if (MBmaster != null)
                {
					readNetBtn.Enabled = true;
					writeNetButton.Enabled = true;
					comboReadType.Enabled = true;
				} 
				else
                {
					connectCheck.Checked = false;
                }
			}
			else if (!connectCheck.Checked) // Отмена выбора подключения к ПЛК
			{
				DisconnectBtn_Click(this, e);
				// Отображение "Нет соединения"
				connectLabel.Text = "Нет соединения";
				connectLabel.ForeColor = Color.Red;
				// Блокировка элементов
				readNetBtn.Enabled = false;
				writeNetButton.Enabled = false;
				comboReadType.Enabled = false;
			}
		}

		///<summary>Нажали кнопку "Подключиться к ПЛК"</summary>
		private void ConnectButton_Click(object sender, EventArgs e)
		{
			if (!connectCheck.Checked) // Не было установлено подключение
			{
				connectCheck.Checked = true;
				if (connectCheck.Checked)
				{
					connectBtn.Text = "ОТКЛЮЧИТЬСЯ";
					ipAddressBox.Enabled = false; // Блокировка выбора IP адреса
					netPortBox.Enabled = false; // Блокировка выбора порта
				}
			}
			else if (connectCheck.Checked) // Подключение к ПЛК установлено
			{
				connectCheck.Checked = false;
				if (!connectCheck.Checked) 
				{
					connectBtn.Text = "ПОДКЛЮЧИТЬСЯ К ПЛК";
					ipAddressBox.Enabled = true; // Разблокировка выбора IP адреса
					netPortBox.Enabled = true; // Разблокировка выбора порта
				} 
			}
		}

		///<summary>Чтение данных для командных слов</summary>
		private void ReadCommandWord(ushort ID, byte unit, ushort stAddress, ushort length)
        {
			MBmaster.ReadHoldingRegister(ID, unit, stAddress, length);
		}

		///<summary>Чтение данных для сигналов DI</summary>
		private void Read_DISignals(ushort ID, byte unit, ushort stAddress, ushort length)
        {
			MBmaster.ReadHoldingRegister(ID, unit, stAddress, length);
		}

		///<summary>Чтение данных для сигналов AI</summary>
		private void Read_AISignals(ushort ID, byte unit, ushort stAddress, ushort length)
        {
			MBmaster.ReadHoldingRegister(ID, unit, stAddress, length);
		}

		///<summary>Чтение данных для сигналов DO</summary>
		private void Read_DOSignals(ushort ID, byte unit, ushort stAddress, ushort length)
        {
			MBmaster.ReadHoldingRegister(ID, unit, stAddress, length);
		}

		///<summary>Чтение данных для сигналов AO</summary>
		private void Read_AOSignals(ushort ID, byte unit, ushort stAddress, ushort length)
        {
			MBmaster.ReadHoldingRegister(ID, unit, stAddress, length);
		}

		///<summary>Чтение регистров из ПЛК</summary>
		private void ReadNetBtn_Click(object sender, EventArgs e)
		{
			counter_read = 1; // Обнуление счетчика позиций для чтения
			ushort ID = 3; // Чтение регистров
			byte unit = 1; 
			UInt16 Length;
			connectCheck.Checked = false;
			System.Threading.Thread.Sleep(300);
			connectCheck.Checked = true; // Если было потеряно соединение с ПЛК
			System.Threading.Thread.Sleep(400);
			if (connectLabel.Text != "Нет соединения") // Если есть подключение к контроллеру
            {
				dataNetTextBox.Text = ""; // Очистка текстового поля
				switch (comboReadType.SelectedIndex) // Выборка селектора для чтения
				{
					case 0: // Все сигналы
						dataNetTextBox.Text += "Все сигналы";
						dataNetTextBox.Text += System.Environment.NewLine;
						/*ReadCommandWord(ID, unit, 16749, 30); // Чтение для командных слов
						Read_DISignals(ID, unit, 16799, 20); // Чтение для сигналов DI
						Read_AISignals(ID, unit, 16824, 24); // Чтение для сигналов AI
						Read_DOSignals(ID, unit, 16849, 28); // Чтение для сигналов DO 
						Read_AOSignals(ID, unit, 16879, 12); // Чтение для сигналов AO */
						Length = 120; startAddress = 16749;
						MBmaster.ReadHoldingRegister(ID, unit, startAddress, Length);
						break;
					case 1: // Командные слова
						dataNetTextBox.Text += "Командные слова";
						dataNetTextBox.Text += System.Environment.NewLine;
						//ReadCommandWord(ID, unit, 16749, 30);
						Length = 30; startAddress = 16749;
						MBmaster.ReadHoldingRegister(ID, unit, startAddress, Length);
						break;
					case 2: // Сигналы DI
						dataNetTextBox.Text += "Сигналы DI";
						dataNetTextBox.Text += System.Environment.NewLine;
						//Read_DISignals(ID, unit, 16799, 20);
						Length = 20; startAddress = 16799;
						MBmaster.ReadHoldingRegister(ID, unit, startAddress, Length);
						break;
					case 3: // Сигналы AI
						dataNetTextBox.Text += "Сигналы AI";
						dataNetTextBox.Text += System.Environment.NewLine;
						//Read_AISignals(ID, unit, 16824, 24);
						Length = 24; startAddress = 16824;
						MBmaster.ReadHoldingRegister(ID, unit, startAddress, Length);
						break;
					case 4: // Сигналы DO
						dataNetTextBox.Text += "Сигналы DO";
						dataNetTextBox.Text += System.Environment.NewLine;
						//Read_DOSignals(ID, unit, 16849, 28);
						Length = 28; startAddress = 16849;
						MBmaster.ReadHoldingRegister(ID, unit, startAddress, Length);
						break;
					case 5: // Сигналы AO
						dataNetTextBox.Text += "Сигналы AO";
						dataNetTextBox.Text += System.Environment.NewLine;
						//Read_AOSignals(ID, unit, 16879, 12);
						Length = 12; startAddress = 16879;
						MBmaster.ReadHoldingRegister(ID, unit, startAddress, Length);
						break;
					case 6: // Сигнал пожарной сигнализации
						dataNetTextBox.Text += "Сигнал пожарной сигнализации";
						dataNetTextBox.Text += System.Environment.NewLine;
						Length = 1; startAddress = 16411;
						MBmaster.ReadHoldingRegister(ID, unit, startAddress, Length);
						break;
				}
			}
		}

		///<summary>Запись регистров в ПЛК (командные слова и сигналы)</summary> 
		private void WriteNetButton_Click(object sender, EventArgs e)
		{
			connectCheck.Checked = false;
			//System.Threading.Thread.Sleep(200);
			connectCheck.Checked = true; // Если было потеряно соединение с ПЛК
			System.Threading.Thread.Sleep(400);
			if (connectLabel.Text != "Нет соединения") // Если установлено соединение
            {
				WriteCommandWords(); // Запись для командных слов
				System.Threading.Thread.Sleep(200);
				WriteSignalsDI(); // Запись для сигналов DI
				System.Threading.Thread.Sleep(200);
				WriteSignalsAI(); // Запись для сигналов AI
				System.Threading.Thread.Sleep(200);
				WriteSignalsDO(); // Запись для сигналов DO
				System.Threading.Thread.Sleep(200);
				WriteSignalsAO(); // Запись для сигналов AO
				System.Threading.Thread.Sleep(200);
				WriteFireSignal(); // Запись для сигнала пожарной сигнализации
				// Вывод сообщения об удачной записи
				const string message = "Данные успешно записаны!";
				const string caption = "Загрузка";
				MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
				ReadNetBtn_Click(this, e); // Повторное чтение данных из ПЛК после записи
			}
		}

		///<summary>Запись командных слов</summary>
		private void WriteCommandWords()
        {
			ushort ID = 8;
			byte unit = 1;
			startAddress = 16749; // Начальный адрес (был 16750)
			data = GetDataCmd(Convert.ToByte(30)); // Запись командных слов
			MBmaster.WriteMultipleRegister(ID, unit, startAddress, data);
		}

		///<summary>Запись для сигналов DI</summary>
		private void WriteSignalsDI()
        {
			ushort ID = 8;
			byte unit = 1;
			startAddress = 16799; // Начальный адрес (был 16800)
			data = GetDataSignalsDI(Convert.ToByte(20)); // Запись сигналов DI
			MBmaster.WriteMultipleRegister(ID, unit, startAddress, data);
		}

		///<summary>Запись для сигналов AI</summary>
		private void WriteSignalsAI()
        {
			ushort ID = 8;
			byte unit = 1;
			startAddress = 16824; // Начальный адрес (был 16825)
			data = GetDataSignalsAI(Convert.ToByte(24)); // Запись сигналов AI
			MBmaster.WriteMultipleRegister(ID, unit, startAddress, data);
		}

		///<summary>Запись для сигналов DO</summary>
		private void WriteSignalsDO()
        {
			ushort ID = 8;
			byte unit = 1;
			startAddress = 16849; // Начальный адрес (был 16850)
			data = GetDataSignalsDO(Convert.ToByte(28)); // Запись сигналов DO
			MBmaster.WriteMultipleRegister(ID, unit, startAddress, data);
		}

		///<summary>Запись для сигналов AO</summary>
		private void WriteSignalsAO()
        {
			ushort ID = 8;
			byte unit = 1;
			startAddress = 16879; // Начальный адрес (был 16880)
			data = GetDataSignalsAO(Convert.ToByte(12)); // Запись сигналов DO
			MBmaster.WriteMultipleRegister(ID, unit, startAddress, data);
		}

		///<summary>Запись для сигнала пожарной сигнализации</summary>
		private void WriteFireSignal()
        {
			ushort ID = 8;
			byte unit = 1;
			startAddress = 16410; // Начальный адрес (был 16411)
			data = GetDataFireSignal(Convert.ToByte(1)); // Запись сигнала ПС
			MBmaster.WriteMultipleRegister(ID, unit, startAddress, data);
		}

		private void MBmaster_OnResponseData(ushort ID, byte unit, byte function, byte[] values)
        {
			// ------------------------------------------------------------------
			// Seperate calling threads
			if (this.InvokeRequired)
			{
				this.BeginInvoke(new Master.ResponseData(MBmaster_OnResponseData), new object[] { ID, unit, function, values });
				return;
			}

			// Identify requested data
			switch (ID)
            {
				case 3: // Read holding register
					data = values;
					//if (comboReadType.SelectedIndex != 0) // Если нет чтения для всех типов сигнлов
						ShowAs(null, null); // Отображение данных через адрес
					break;
				case 8: break; // Write multiple register
			}
		}

		// Отображение данных из ПЛК через адрес
		private void ShowAs(object sender, EventArgs e)
        {
			int count = 1; // Локальный счетчик
			if (data.Length < 2) return;
			int length = data.Length / 2 + Convert.ToInt16(data.Length % 2 > 0);
			int[] word = new int[length];
			for (int i = 0; i < length; i++)
				word[i] = data[i * 2] * 256 + data[i * 2 + 1];
			foreach (int i in word) // Для всех позиций по адресам
			{
				 if (comboReadType.SelectedIndex == 0) // Все сигналы
					dataNetTextBox.Text += counter_read.ToString() + ") " + startAddress.ToString() + " - " + i.ToString() +
							System.Environment.NewLine;
				else if (comboReadType.SelectedIndex == 1) // Командые слова
					dataNetTextBox.Text += counter_read.ToString() + ") " + startAddress.ToString() + " - " + i.ToString() +
						System.Environment.NewLine;
				else if (comboReadType.SelectedIndex == 2) // Сигналы DI
					dataNetTextBox.Text += "DI_" + count.ToString() + ") " + startAddress.ToString() + " - " + i.ToString() +
						System.Environment.NewLine;
				else if (comboReadType.SelectedIndex == 3) // Сигналы AI
					dataNetTextBox.Text += "AI_" + count.ToString() + ") " + startAddress.ToString() + " - " + i.ToString() +
						System.Environment.NewLine;
				else if (comboReadType.SelectedIndex == 4) // Сигналы DO
					dataNetTextBox.Text += "DO_" + count.ToString() + ") " + startAddress.ToString() + " - " + i.ToString() +
							System.Environment.NewLine;
				else if (comboReadType.SelectedIndex == 5) // Сигналы AO
					dataNetTextBox.Text += "AO_" + count.ToString() + ") " + startAddress.ToString() + " - " + i.ToString() +
							System.Environment.NewLine;
				else if (comboReadType.SelectedIndex == 6) // Сигнал пожарной сигнализации
					dataNetTextBox.Text += "Fire_" + count.ToString() + ") " + startAddress.ToString() + " - " + i.ToString() +
							System.Environment.NewLine;
				++counter_read; // Увеличение глобального счетчика
				++count; // Увеличение внутреннего счетчика
				++startAddress; // Увеличение адреса
			}
        }

		// Формирование данных для записи в ПЛК, командые слова
		private byte[] GetDataCmd(int num)
        {
			int[] word = new int[num];
			byte[] data = new Byte[num];
			string[] tempArray = writeNetTextBox.Lines;
			ushort counter = 0;

			foreach (string s in tempArray) // Обрезка порядкового номера
            {
				string w = s.Substring(s.IndexOf(' '));
				tempArray[counter] = w;
				++counter;
            }
			counter = 0;
			foreach (string s in tempArray) // Формирование командных слов из строк, int
            {
				if (counter < 30) // Командные слова
                {
					word[counter] = Convert.ToInt16(tempArray[counter]);
					++counter;
				}
            }
			data = new byte[num * 2];
			for (int i = 0; i < num; i++)
            {
				byte[] dat = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)word[i]));
				data[i * 2] = dat[0];
				data[i * 2 + 1] = dat[1];
            }
			return data;
        }

		///<summary>Формирование данных для записи в ПЛК, сигналы DI</summary>
		private byte[] GetDataSignalsDI(int num)
        {
			int[] word = new int[num];
			byte[] data = new Byte[num];
			string[] tempArray = writeNetTextBox.Lines;
			ushort counter = 0;
			ushort k = 0;

			foreach (string s in tempArray) // Обрезка порядкового номера
			{
				string w = s.Substring(s.IndexOf(' '));
				tempArray[counter] = w;
				++counter;
			}
			counter = 30;
			foreach (string s in tempArray) // Формирование командных слов из строк, int
			{
				if (k < 20) // Сигналы DI
				{
					word[k] = Convert.ToInt16(tempArray[counter]);
					++counter; ++k;
				}
			}
			data = new byte[num * 2];
			for (int i = 0; i < num; i++)
			{
				byte[] dat = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)word[i]));
				data[i * 2] = dat[0];
				data[i * 2 + 1] = dat[1];
			}
			return data;
		}

		///<summary>Формирование данных для записи в ПЛК, сигналы AI</summary>
		private byte[] GetDataSignalsAI(int num)
		{
			int[] word = new int[num];
			byte[] data = new Byte[num];
			string[] tempArray = writeNetTextBox.Lines;
			ushort counter = 0;
			ushort k = 0;

			foreach (string s in tempArray) // Обрезка порядкового номера
			{
				string w = s.Substring(s.IndexOf(' '));
				tempArray[counter] = w;
				++counter;
			}
			counter = 50;
			foreach (string s in tempArray) // Формирование командных слов из строк, int
			{
				if (k < 24) // Сигналы AI
				{
					word[k] = Convert.ToInt16(tempArray[counter]);
					++counter; ++k;
				}
			}
			data = new byte[num * 2];
			for (int i = 0; i < num; i++)
			{
				byte[] dat = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)word[i]));
				data[i * 2] = dat[0];
				data[i * 2 + 1] = dat[1];
			}
			return data;
		}

		///<summary>Формирование данных для записи в ПЛК, сигналы DO</summary>
		private byte[] GetDataSignalsDO(int num)
		{
			int[] word = new int[num];
			byte[] data = new Byte[num];
			string[] tempArray = writeNetTextBox.Lines;
			ushort counter = 0;
			ushort k = 0;

			foreach (string s in tempArray) // Обрезка порядкового номера
			{
				string w = s.Substring(s.IndexOf(' '));
				tempArray[counter] = w;
				++counter;
			}
			counter = 74;
			foreach (string s in tempArray) // Формирование командных слов из строк, int
			{
				if (k < 28) // Сигналы DO
				{
					word[k] = Convert.ToInt16(tempArray[counter]);
					++counter; ++k;
				}
			}
			data = new byte[num * 2];
			for (int i = 0; i < num; i++)
			{
				byte[] dat = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)word[i]));
				data[i * 2] = dat[0];
				data[i * 2 + 1] = dat[1];
			}
			return data;
		}

		///<summary>Формирование данных для записи в ПЛК, сигналы AO</summary>
		private byte[] GetDataSignalsAO(int num)
		{
			int[] word = new int[num];
			byte[] data = new Byte[num];
			string[] tempArray = writeNetTextBox.Lines;
			ushort counter = 0;
			ushort k = 0;

			foreach (string s in tempArray) // Обрезка порядкового номера
			{
				string w = s.Substring(s.IndexOf(' '));
				tempArray[counter] = w;
				++counter;
			}
			counter = 102;
			foreach (string s in tempArray) // Формирование командных слов из строк, int
			{
				if (k < 12) // Сигналы AO
				{
					word[k] = Convert.ToInt16(tempArray[counter]);
					++counter; ++k;
				}
			}
			data = new byte[num * 2];
			for (int i = 0; i < num; i++)
			{
				byte[] dat = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)word[i]));
				data[i * 2] = dat[0];
				data[i * 2 + 1] = dat[1];
			}
			return data;
		}

		///<summary>Формирование данных для записи в ПЛК, пожарная сигнализация</summary>
		private byte[] GetDataFireSignal(int num)
        {
			int[] word = new int[num];
			byte[] data = new Byte[num];
			string[] tempArray = writeNetTextBox.Lines;
			ushort counter = 0;
			ushort k = 0;

			foreach (string s in tempArray) // Обрезка порядкового номера
			{
				string w = s.Substring(s.IndexOf(' '));
				tempArray[counter] = w;
				++counter;
			}
			counter = 114;
			foreach (string s in tempArray) // Формирование командных слов из строк, int
			{
				if (k < 1) // Сигнал ПС
				{
					word[k] = Convert.ToInt16(tempArray[counter]);
					++counter; ++k;
				}
			}
			data = new byte[num * 2];
			for (int i = 0; i < num; i++)
			{
				byte[] dat = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)word[i]));
				data[i * 2] = dat[0];
				data[i * 2 + 1] = dat[1];
			}
			return data;
		}

		///<summary>Изменили тип сигналов для чтения</summary>
		private void СomboReadType_SelectedIndexChanged(object sender, EventArgs e)
		{
			comboReadType.Enabled = false; // Временная блокировка элемента
			if (connectLabel.Text != "Нет соединения") // Если подключение к ПЛК установлено
            {
				ReadNetBtn_Click(this, e); // Чтение данных из ПЛК 
            }
			comboReadType.Enabled = true; // Разблокировка элемента
		}

		private void MBmaster_OnException(ushort id, byte unit, byte function, byte exception){}
    }
}