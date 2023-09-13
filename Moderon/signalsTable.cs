using System.Windows.Forms;
using System;
using System.Drawing;
using System.Collections.Generic;

// Файл для работы с таблицей сигналов ПЛК и блоков расширения

namespace Exhibit
{

    class Ai
    {
        public string Name { get; private set; }
        public ushort Code { get; private set; }
        public bool Active { get; private set; } = true; // Свободен по умолчанию
        public Ai(string name, ushort code)
        {
            Name = name; Code = code;
        }
        public void Select() => Active = false;
        public void Dispose() => Active = true;
    }

    class Di
    {
        public string Name { get; private set; }
        public ushort Code { get; private set; }
        public bool Active { get; private set; } = true; // Свободен по умолчанию
        public Di(string name, ushort code)
        {
            Name = name; Code = code;
        }
        public void Select() => Active = false;
        public void Dispose() => Active = true;
    }

    class Ao
    {
        public string Name { get; private set; }
        public ushort Code { get; private set; }
        public bool Active { get; private set; } = true; // Свободен по умолчанию
        public Ao(string name, ushort code)
        {
            Name = name; Code = code;
        }
        public void Select() => Active = false;
        public void Dispose() => Active = true;
    }

    class Do
    {
        public string Name { get; private set; }
        public ushort Code { get; private set; }
        public bool Active { get; private set; } = true; // Свободен по умолчанию

        public Do(string name, ushort code)
        {
            Name = name; Code = code;
        }
        public Do() { }
        public void Select() => Active = false;
        public void Dispose() => Active = true;
    }

    public partial class Form1 : Form
    {
        List<Ai> list_ai = new List<Ai>();
        List<Di> list_di = new List<Di>();
        List<Ao> list_ao = new List<Ao>();
        List<Do> list_do = new List<Do>();

        /// <summary>Начальная установка comboBox для сигналов ПЛК</summary>
        /// <value>true</value>
        bool initialComboSignals = true;
        string notSelected = "Не выбрано";

        // Нажали на кнопку "Сформировать"
        public void FormSignalsButton_Click(object sender, EventArgs e)
        {
            var p1 = new Point(15, 90);
            tabControl1.Hide();
            label_comboSysType.Text = "ТАБЛИЦА СИГНАЛОВ";
            comboSysType.Hide(); panel1.Hide();
            signalsPanel.Location = p1;
            signalsPanel.Show();
        }

        // Нажали кнопку "Назад" в панели сигналов
        private void BackSignalsButton_Click(object sender, EventArgs e)
        {
            signalsPanel.Hide();
            tabControl1.Show();
            label_comboSysType.Text = "ТИП СИСТЕМЫ";
            comboSysType.Show();
            panel1.Show();
        }

        // Сигналы ПЛК при загрузке формы
        private void Form1_InitSignals(object sender, EventArgs e)
        {
            SetComboInitial_signals(); // Начальная установка для comboBox
            // Добавление начальных DI
            list_di.Add(new Di("Переключатель \"Пуск/Старт\"", 1));
            // Добавление начальных DO
            list_do.Add(new Do("Запуск приточного вентилятора 1", 8));
            list_do.Add(new Do("Сигнал \"Работа\"", 1));
            list_do.Add(new Do("Сигнал \"Авария\"", 3));
            // Добавление к выбору начальных сигналов
            // DI сигналы
            DI1_combo.Items.Add(list_di.Find(x => x.Code == 1).Name); // Пуск/Старт
            list_di.Find(x => x.Code == 1).Select();
            DI1_combo.SelectedIndex = 1; // Выбор сигнала
            // DO сигналы
            DO1_combo.Items.Add(list_do.Find(x => x.Code == 8).Name); // Запуск вентилятора
            list_do.Find(x => x.Code == 8).Select();
            DO1_combo.SelectedIndex = 1; // Выбор сигнала
            DO2_combo.Items.Add(list_do.Find(x => x.Code == 1).Name); // Сигнал "Работа"
            DO2_combo.SelectedIndex = 1;
            list_do.Find(x => x.Code == 1).Select();
            DO3_combo.Items.Add(list_do.Find(x => x.Code == 3).Name); // Сигнал "Авария"
            DO3_combo.SelectedIndex = 1;
            list_do.Find(x => x.Code == 3).Select();
            initialComboSignals = false; // Сброс признака начальной настройки comboBox
        }

        // Устновка для comboBox выбора сигналов
        private void SetComboInitial_signals()
        {
            // AI сигналы ПЛК
            AI1_combo.SelectedItem = notSelected; AI2_combo.SelectedItem = notSelected; 
            AI3_combo.SelectedItem = notSelected; AI4_combo.SelectedItem = notSelected; 
            AI5_combo.SelectedItem = notSelected; AI6_combo.SelectedItem = notSelected;
            // DI сигналы ПЛК
            DI1_combo.SelectedItem = notSelected; DI2_combo.SelectedItem = notSelected; 
            DI3_combo.SelectedItem = notSelected; DI4_combo.SelectedItem = notSelected; 
            DI5_combo.SelectedItem = notSelected;
            // AO сигналы ПЛК
            AO1_combo.SelectedItem = notSelected; AO2_combo.SelectedItem = notSelected; 
            AO3_combo.SelectedItem = notSelected;
            // DO сигналы ПЛК
            DO1_combo.SelectedItem = notSelected; DO2_combo.SelectedItem = notSelected; 
            DO3_combo.SelectedItem = notSelected; DO4_combo.SelectedItem = notSelected; 
            DO5_combo.SelectedItem = notSelected; DO6_combo.SelectedItem = notSelected;
            DO7_combo.SelectedItem = notSelected;
        }

        // Изменили DO1 comboBox
        private void DO1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = "";
            Do do_find = null;
            if (DO1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO1_combo.Items.Count > 1) // Больше одного элемента
                {
                    name = String.Concat(DO1_combo.Items[1]);
                    do_find = list_do.Find(x => x.Name == name);
                    list_do.Remove(list_do.Find(x => x.Name == name)); // Удаление из списка
                }
                if (do_find != null) // Найден элемент
                {
                    do_find.Dispose(); 
                    list_do.Add(do_find); // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosDO(name, DO1_combo);
            }
        }

        // Изменили DO2 comboBox
        private void DO2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = "";
            Do do_find = null;
            if (DO2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO2_combo.Items.Count > 1) // Больше одного элемента
                {
                    name = String.Concat(DO2_combo.Items[1]);
                    do_find = list_do.Find(x => x.Name == name);
                    list_do.Remove(list_do.Find(x => x.Name == name));
                }
                if (do_find != null)
                {
                    do_find.Dispose();
                    list_do.Add(do_find);
                }
                if (!initialComboSignals) AddtoCombosDO(name, DO2_combo);
            }
        }

        // Изменили DO3 comboBox
        private void DO3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = "";
            Do do_find = null;
            if (DO3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO3_combo.Items.Count > 1) // Больше одного элемента
                {
                    name = String.Concat(DO3_combo.Items[1]);
                    do_find = list_do.Find(x => x.Name == name);
                    list_do.Remove(list_do.Find(x => x.Name == name));
                }
                if (do_find != null)
                {
                    do_find.Dispose();
                    list_do.Add(do_find);
                }
                if (!initialComboSignals) AddtoCombosDO(name, DO3_combo);
            }
        }

        // Добавление освободившегося DO к остальным comboBox
        private void AddtoCombosDO(string name, ComboBox cm)
        {
            string s = "Не выбрано";
            Do do_find;
            // DO1 comboBox, добавление в остальные слоты, где "Не выбрано"
            if (DO1_combo != cm && DO1_combo.SelectedIndex == 0) // DO1
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null) DO1_combo.Items.Add(do_find.Name);
                //DO1_combo.Items.Add(list_do.Find(x => x.Name == name).Name);
            }
            // Добавление в существующий comboBox
            else if (DO1_combo == cm && DO1_combo.SelectedIndex == 0)
            {
                foreach (Do elem in list_do) // Все активные сигналы
                    if (elem.Active && !DO1_combo.Items.Contains(elem.Name))
                        DO1_combo.Items.Add(elem.Name);
            }
            // Удаление позиции из других comboBox
            else if (DO1_combo != cm && DO1_combo.SelectedIndex != 0)
            {
                DO1_combo.Items.Remove(name);
            }
            // Удаление из текущего, кроме "Не выбрано"
            else if (DO1_combo == cm && DO1_combo.SelectedIndex != 0)
            {
                for (int i = 0; i < DO1_combo.Items.Count; i++)
                    if (String.Concat(DO1_combo.Items[i]) != s && String.Concat(DO1_combo.Items[i]) != name) 
                        DO1_combo.Items.RemoveAt(i);
            }
            // DO2 comboBox, добавление в остальные слоты, где "Не выбрано"
            if (DO2_combo != cm && DO2_combo.SelectedIndex == 0) // DO2
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null) DO2_combo.Items.Add(do_find.Name);
                //DO2_combo.Items.Add(list_do.Find(x => x.Name == name).Name);
            }
            // Добавление в существующий comboBox
            else if (DO2_combo == cm && DO2_combo.SelectedIndex == 0)
            {
                foreach (Do elem in list_do)
                    if (elem.Active && !DO2_combo.Items.Contains(elem.Name))
                        DO2_combo.Items.Add(elem.Name);
            }
            // Удаление позиции из других comboBox
            else if (DO2_combo != cm && DO2_combo.SelectedIndex != 0)
            {
                DO2_combo.Items.Remove(name);
            }
            // Удаление из текущего, кроме "Не выбрано" и выбранной позиции
            else if (DO2_combo == cm && DO2_combo.SelectedIndex != 0)
            {
                if (String.Concat(DO2_combo.SelectedItem) != name && String.Concat(DO2_combo.SelectedItem) != s)
                    for (int i = 0; i < DO2_combo.Items.Count; i++) DO2_combo.Items.RemoveAt(i);
            }
            // DO3 ComboBox
            if (DO3_combo != cm && DO3_combo.SelectedIndex == 0) // DO3
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null) DO3_combo.Items.Add(do_find.Name);
                //DO3_combo.Items.Add(list_do.Find(x => x.Name == name).Name);
            }
            // Добавление в существующий comboBox
            else if (DO3_combo == cm && DO3_combo.SelectedIndex == 0)
                foreach (Do elem in list_do)
                    if (elem.Active && !DO3_combo.Items.Contains(elem.Name))
                        DO3_combo.Items.Add(elem.Name);
            /*
            // DO4 ComboBox
            if (DO4_combo != cm && DO4_combo.SelectedIndex == 0) // DO4
                DO4_combo.Items.Add(list_do.Find(x => x.Name == name).Name);
            // DO5 ComboBox
            if (DO5_combo != cm && DO5_combo.SelectedIndex == 0) // DO5
                DO5_combo.Items.Add(list_do.Find(x => x.Name == name).Name);
            // DO6 ComboBox
            if (DO6_combo != cm && DO6_combo.SelectedIndex == 0) // DO6
                DO6_combo.Items.Add(list_do.Find(x => x.Name == name).Name);
            // DO7 ComboBox
            if (DO7_combo != cm && DO7_combo.SelectedIndex == 0) // DO7
                DO7_combo.Items.Add(list_do.Find(x => x.Name == name).Name); */
        }
    }
}