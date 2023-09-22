using System.Windows.Forms;
using System;
using System.Drawing;
using System.Collections.Generic;

// Файл для работы с таблицей сигналов ПЛК и блоков расширения

namespace Moderon
{
    /// <summary>
    /// Класс для дискретных выходов
    /// </summary>
    class Do
    {
        public string Name { get; private set; }
        public ushort Code { get; private set; }
        public bool Active { get; private set; } = true; // Свободен по умолчанию

        public Do(string name, ushort code)
        {
            Name = name; Code = code;
        }
        public Do(string name, ushort code, bool active)
        {
            Name = name; Code = code; Active = active;
        }
        public Do() { }
        /// <summary>Выбор дискретного выхода</summary>
        public void Select() => Active = false;
        
        /// <summary>Освобождение дискретного выхода</summary>
        public void Dispose() => Active = true;
    }

    public partial class Form1 : Form
    {
        List<Do> list_do = new List<Do>();

        /// <summary>Начальная установка comboBox для сигналов ПЛК</summary>
        bool initialComboSignals = true;        // Начальная расстановка
        bool subDOcondition = false;            // Условие при удалении DO

        // Сохранение наименования выбранного элемента для ПЛК и блоков расширения
        string
            DO1combo_text, DO2combo_text, DO3combo_text, DO4combo_text, DO5combo_text, DO6combo_text, DO7combo_text,
            DO1bl1combo_text, DO2bl1combo_text, DO3bl1combo_text, DO4bl1combo_text, DO5bl1combo_text, DO6bl1combo_text, DO7bl1combo_text,
            DO1bl2combo_text, DO2bl2combo_text, DO3bl2combo_text, DO4bl2combo_text, DO5bl2combo_text, DO6bl2combo_text, DO7bl2combo_text,
            DO1bl3combo_text, DO2bl3combo_text, DO3bl3combo_text, DO4bl3combo_text, DO5bl3combo_text, DO6bl3combo_text, DO7bl3combo_text;

        // Сохранение прошлого индекса comboBox элементов для ПЛК и блоков расширения
        int
            DO1combo_index, DO2combo_index, DO3combo_index, DO4combo_index, DO5combo_index, DO6combo_index, DO7combo_index,
            DO1bl1combo_index, DO2bl1combo_index, DO3bl1combo_index, DO4bl1combo_index, DO5bl1combo_index, DO6bl1combo_index, DO7bl1combo_index,
            DO1bl2combo_index, DO2bl2combo_index, DO3bl2combo_index, DO4bl2combo_index, DO5bl2combo_index, DO6bl2combo_index, DO7bl2combo_index,
            DO1bl3combo_index, DO2bl3combo_index, DO3bl3combo_index, DO4bl3combo_index, DO5bl3combo_index, DO6bl3combo_index, DO7bl3combo_index;

        ///<summary>Начальная установка для сигналов DO таблицы сигналов</summary> 
        public void Set_DOComboTextIndex()
        {
            string workSignal = "Сигнал \"Работа\"";
            DO1combo_text = "Сигнал \"Пуск/Стоп\" приточного вентилятора 1";
            DO2combo_text = workSignal; DO3combo_text = workSignal;

            DO1combo_index = 1; DO2combo_index = 1; DO3combo_index = 1;

            var do_signals = new List<string>()
            {
                DO4combo_text, DO5combo_text, DO6combo_text, DO7combo_text,
                DO1bl1combo_text, DO2bl1combo_text, DO3bl1combo_text, DO4bl1combo_text, DO5bl1combo_text, DO6bl1combo_text, DO7bl1combo_text,
                DO1bl2combo_text, DO2bl2combo_text, DO3bl2combo_text, DO4bl2combo_text, DO5bl2combo_text, DO6bl2combo_text, DO7bl2combo_text,
                DO1bl3combo_text, DO2bl3combo_text, DO3bl3combo_text, DO4bl3combo_text, DO5bl3combo_text, DO6bl3combo_text, DO7bl3combo_text
            };

            var do_signals_index = new List<int>()
            {
                DO4combo_index, DO5combo_index, DO6combo_index, DO7combo_index,
                DO1bl1combo_index, DO2bl1combo_index, DO3bl1combo_index, DO4bl1combo_index, DO5bl1combo_index, DO6bl1combo_index, DO7bl1combo_index,
                DO1bl2combo_index, DO2bl2combo_index, DO3bl2combo_index, DO4bl2combo_index, DO5bl2combo_index, DO6bl2combo_index, DO7bl2combo_index,
                DO1bl3combo_index, DO2bl3combo_index, DO3bl3combo_index, DO4bl3combo_index, DO5bl3combo_index, DO6bl3combo_index, DO7bl3combo_index
            };

            for (var i = 0; i < do_signals.Count; i++)
                do_signals[i] = NOT_SELECTED;

            for (var i = 0; i < do_signals_index.Count; i++)
                do_signals_index[i] = 0;
        }

        ///<summary>Нажали на кнопку "Сформировать"</summary> 
        public void FormSignalsButton_Click(object sender, EventArgs e)
        {
            var p1 = new Point(15, 90);
            mainPage.Hide(); loadModbusPanel.Hide();
            label_comboSysType.Text = "ТАБЛИЦА СИГНАЛОВ";
            comboSysType.Hide(); panelElements.Hide();
            signalsPanel.Location = p1;
            signalsPanel.Show();
            signalsPanel.Height = 845;
            formSignalsButton.Hide(); // Скрытие кнопки
            SignalsTableReSize(Size.Width, Size.Height); // Таблица сигналов
        }

        ///<summary>Нажали кнопку "Назад" в панели сигналов</summary> 
        private void BackSignalsButton_Click(object sender, EventArgs e)
        {
            signalsPanel.Hide();
            mainPage.Show();
            label_comboSysType.Text = "ТИП СИСТЕМЫ";
            comboSysType.Show();
            panelElements.Show();
            formSignalsButton.Show(); // Отображение кнопки "Сформировать IO"
            // ToolStripMenuItem_load.Enabled = true; // Разблокировка "Настройка"
            fromSignalsMove = false; // Сброс признака перехода с панели выбора сигналов
            ToolStripMenuItem_help.Enabled = true; // Разблокировка "Помощь"
        }

        ///<summary>Сигналы ПЛК при загрузке формы</summary> 
        private void Form1_InitSignals(object sender, EventArgs e)
        {
            SetComboInitial_signals(); // Начальная установка для comboBox
            // Добавление начальных DI
            list_di.Add(new Di("Переключатель \"Стоп/Пуск\"", 3));
            // Добавление начальных DO
            list_do.Add(new Do("Сигнал \"Пуск/Стоп\" приточного вентилятора 1", 9));
            //list_do.Add(new Do("Сигнал \"Работа\"", 1));
            //list_do.Add(new Do("Сигнал \"Авария\"", 3));
            // Выбор сигналов "Работа" и "Авария" по умолчанию после сброса
            ignoreEvents = true;
            //sigWorkCheck.Checked = true;
            //sigAlarmCheck.Checked = true;
            // Выбор сигналов "Пуск/Стоп" для вентиляторов по умолчанию после сброса
            prFanStStopCheck.Checked = true;
            outFanStStopCheck.Checked = true;
            ignoreEvents = false;
            // Добавление к выбору начальных сигналов
            // DI сигналы
            DI1_combo.Items.Add(list_di.Find(x => x.Code == 3).Name); // Пуск/Старт
            list_di.Find(x => x.Code == 3).Select();
            DI1_combo.SelectedIndex = 1; // Выбор сигнала
            if (showCode) DI1_lab.Text = 3.ToString();
            // DO сигналы
            DO1_combo.Items.Add(list_do.Find(x => x.Code == 9).Name); // Сигнал "Пуск/Стоп" приточного вентилятора 1
            list_do.Find(x => x.Code == 9).Select();
            DO1_combo.SelectedIndex = 1; // Выбор сигнала
            if (showCode) DO1_lab.Text = 9.ToString();
            /*DO2_combo.Items.Add(list_do.Find(x => x.Code == 1).Name); // Сигнал "Работа"
            DO2_combo.SelectedIndex = 1;
            if (showCode) DO2_lab.Text = 1.ToString();
            list_do.Find(x => x.Code == 1).Select();
            DO3_combo.Items.Add(list_do.Find(x => x.Code == 3).Name); // Сигнал "Авария"
            DO3_combo.SelectedIndex = 1;
            if (showCode) DO3_lab.Text = 3.ToString();
            list_do.Find(x => x.Code == 3).Select(); */
            initialComboSignals = false; // Сброс признака начальной настройки comboBox
        }

        ///<summary>Нажали на кнопку "Сброс"</summary>
        private void ResetButton_signalsDOClick(object sender, EventArgs e)
        {
            // Очистка массивов сигналов
            list_di.Clear(); list_do.Clear(); list_ai.Clear(); list_ao.Clear();
            // Очистка переменных
            subDOcondition = subAOcondition = subDIcondition = false;
            // Очистка comboBox ПЛК
            DO1_combo.Items.Clear(); DO1_combo.Items.Add(NOT_SELECTED);
            DO2_combo.Items.Clear(); DO2_combo.Items.Add(NOT_SELECTED);
            DO3_combo.Items.Clear(); DO3_combo.Items.Add(NOT_SELECTED);
            DO4_combo.Items.Clear(); DO4_combo.Items.Add(NOT_SELECTED);
            DO5_combo.Items.Clear(); DO5_combo.Items.Add(NOT_SELECTED);
            DO6_combo.Items.Clear(); DO6_combo.Items.Add(NOT_SELECTED);
            DO7_combo.Items.Clear(); DO7_combo.Items.Add(NOT_SELECTED);
            // Очистка comboBox блок расширения 1
            DO1bl1_combo.Items.Clear(); DO1bl1_combo.Items.Add(NOT_SELECTED);
            DO2bl1_combo.Items.Clear(); DO2bl1_combo.Items.Add(NOT_SELECTED);
            DO3bl1_combo.Items.Clear(); DO3bl1_combo.Items.Add(NOT_SELECTED);
            DO4bl1_combo.Items.Clear(); DO4bl1_combo.Items.Add(NOT_SELECTED);
            DO5bl1_combo.Items.Clear(); DO5bl1_combo.Items.Add(NOT_SELECTED);
            DO6bl1_combo.Items.Clear(); DO6bl1_combo.Items.Add(NOT_SELECTED);
            DO7bl1_combo.Items.Clear(); DO7bl1_combo.Items.Add(NOT_SELECTED);
            // Очистка comboBox блок расширения 2
            DO1bl2_combo.Items.Clear(); DO1bl2_combo.Items.Add(NOT_SELECTED);
            DO2bl2_combo.Items.Clear(); DO2bl2_combo.Items.Add(NOT_SELECTED);
            DO3bl2_combo.Items.Clear(); DO3bl2_combo.Items.Add(NOT_SELECTED);
            DO4bl2_combo.Items.Clear(); DO4bl2_combo.Items.Add(NOT_SELECTED);
            DO5bl2_combo.Items.Clear(); DO5bl2_combo.Items.Add(NOT_SELECTED);
            DO6bl2_combo.Items.Clear(); DO6bl2_combo.Items.Add(NOT_SELECTED);
            DO7bl2_combo.Items.Clear(); DO7bl2_combo.Items.Add(NOT_SELECTED);
            // Очистка comboBox блок расширения 3
            DO1bl3_combo.Items.Clear(); DO1bl3_combo.Items.Add(NOT_SELECTED);
            DO2bl3_combo.Items.Clear(); DO2bl3_combo.Items.Add(NOT_SELECTED);
            DO3bl3_combo.Items.Clear(); DO3bl3_combo.Items.Add(NOT_SELECTED);
            DO4bl3_combo.Items.Clear(); DO4bl3_combo.Items.Add(NOT_SELECTED);
            DO5bl3_combo.Items.Clear(); DO5bl3_combo.Items.Add(NOT_SELECTED);
            DO6bl3_combo.Items.Clear(); DO6bl3_combo.Items.Add(NOT_SELECTED);
            DO7bl3_combo.Items.Clear(); DO7bl3_combo.Items.Add(NOT_SELECTED);
        }
        ///<summary>Установка для comboBox изначального выбора сигналов</summary> 
        private void SetComboInitial_signals()
        {
            // AI сигналы ПЛК
            AI1_combo.SelectedItem = NOT_SELECTED; AI2_combo.SelectedItem = NOT_SELECTED; 
            AI3_combo.SelectedItem = NOT_SELECTED; AI4_combo.SelectedItem = NOT_SELECTED; 
            AI5_combo.SelectedItem = NOT_SELECTED; AI6_combo.SelectedItem = NOT_SELECTED;
            // AI сигналы для блока расширения 1
            AI1bl1_combo.SelectedItem = NOT_SELECTED; AI2bl1_combo.SelectedItem = NOT_SELECTED;
            AI3bl1_combo.SelectedItem = NOT_SELECTED; AI4bl1_combo.SelectedItem = NOT_SELECTED;
            AI5bl1_combo.SelectedItem = NOT_SELECTED; AI6bl1_combo.SelectedItem = NOT_SELECTED;
            // AI сигналы для блока расширения 2
            AI1bl2_combo.SelectedItem = NOT_SELECTED; AI2bl2_combo.SelectedItem = NOT_SELECTED;
            AI3bl2_combo.SelectedItem = NOT_SELECTED; AI4bl2_combo.SelectedItem = NOT_SELECTED;
            AI5bl2_combo.SelectedItem = NOT_SELECTED; AI6bl2_combo.SelectedItem = NOT_SELECTED;
            // AI сигналы для блока расширения 3
            AI1bl3_combo.SelectedItem = NOT_SELECTED; AI2bl3_combo.SelectedItem = NOT_SELECTED;
            AI3bl3_combo.SelectedItem = NOT_SELECTED; AI4bl3_combo.SelectedItem = NOT_SELECTED;
            AI5bl3_combo.SelectedItem = NOT_SELECTED; AI6bl3_combo.SelectedItem = NOT_SELECTED;
            // DI сигналы ПЛК
            DI1_combo.SelectedItem = NOT_SELECTED; DI2_combo.SelectedItem = NOT_SELECTED; 
            DI3_combo.SelectedItem = NOT_SELECTED; DI4_combo.SelectedItem = NOT_SELECTED; 
            DI5_combo.SelectedItem = NOT_SELECTED;
            // DI сигналы для блока расширения 1
            DI1bl1_combo.SelectedItem = NOT_SELECTED; DI2bl1_combo.SelectedItem = NOT_SELECTED;
            DI3bl1_combo.SelectedItem = NOT_SELECTED; DI4bl1_combo.SelectedItem = NOT_SELECTED;
            DI5bl1_combo.SelectedItem = NOT_SELECTED;
            // DI сигналы для блока расширения 2
            DI1bl2_combo.SelectedItem = NOT_SELECTED; DI2bl2_combo.SelectedItem = NOT_SELECTED;
            DI3bl2_combo.SelectedItem = NOT_SELECTED; DI4bl2_combo.SelectedItem = NOT_SELECTED;
            DI5bl2_combo.SelectedItem = NOT_SELECTED;
            // DI сигналы для блока расширения 3
            DI1bl3_combo.SelectedItem = NOT_SELECTED; DI2bl3_combo.SelectedItem = NOT_SELECTED;
            DI3bl3_combo.SelectedItem = NOT_SELECTED; DI4bl3_combo.SelectedItem = NOT_SELECTED;
            DI5bl3_combo.SelectedItem = NOT_SELECTED;
            // AO сигналы ПЛК
            AO1_combo.SelectedItem = NOT_SELECTED; AO2_combo.SelectedItem = NOT_SELECTED; 
            AO3_combo.SelectedItem = NOT_SELECTED;
            // AO сигналы для блока расширения 1
            AO1bl1_combo.SelectedItem = NOT_SELECTED; AO2bl1_combo.SelectedItem = NOT_SELECTED;
            AO3bl1_combo.SelectedItem = NOT_SELECTED;
            // AO сигналы для блока расширения 2
            AO1bl2_combo.SelectedItem = NOT_SELECTED; AO2bl2_combo.SelectedItem = NOT_SELECTED; 
            AO3bl2_combo.SelectedItem = NOT_SELECTED;
            // AO сигналы для блока расширения 3
            AO1bl3_combo.SelectedItem = NOT_SELECTED; AO2bl3_combo.SelectedItem = NOT_SELECTED;
            AO3bl3_combo.SelectedItem = NOT_SELECTED;
            // DO сигналы ПЛК
            DO1_combo.SelectedItem = NOT_SELECTED; DO2_combo.SelectedItem = NOT_SELECTED; 
            DO3_combo.SelectedItem = NOT_SELECTED; DO4_combo.SelectedItem = NOT_SELECTED; 
            DO5_combo.SelectedItem = NOT_SELECTED; DO6_combo.SelectedItem = NOT_SELECTED;
            DO7_combo.SelectedItem = NOT_SELECTED;
            // DO сигналы для блока расширения 1
            DO1bl1_combo.SelectedItem = NOT_SELECTED; DO2bl1_combo.SelectedItem = NOT_SELECTED;
            DO3bl1_combo.SelectedItem = NOT_SELECTED; DO4bl1_combo.SelectedItem = NOT_SELECTED;
            DO5bl1_combo.SelectedItem = NOT_SELECTED; DO6bl1_combo.SelectedItem = NOT_SELECTED;
            DO7bl1_combo.SelectedItem = NOT_SELECTED;
            // DO сигналы для блока расширения 2
            DO1bl2_combo.SelectedItem = NOT_SELECTED; DO2bl2_combo.SelectedItem = NOT_SELECTED;
            DO3bl2_combo.SelectedItem = NOT_SELECTED; DO4bl2_combo.SelectedItem = NOT_SELECTED;
            DO5bl2_combo.SelectedItem = NOT_SELECTED; DO6bl2_combo.SelectedItem = NOT_SELECTED;
            DO7bl2_combo.SelectedItem = NOT_SELECTED;
            // DO сигналы для блока расширения 3
            DO1bl3_combo.SelectedItem = NOT_SELECTED; DO2bl3_combo.SelectedItem = NOT_SELECTED;
            DO3bl3_combo.SelectedItem = NOT_SELECTED; DO4bl3_combo.SelectedItem = NOT_SELECTED;
            DO5bl3_combo.SelectedItem = NOT_SELECTED; DO6bl3_combo.SelectedItem = NOT_SELECTED;
            DO7bl3_combo.SelectedItem = NOT_SELECTED;
            // Типы AI сигналов, ПЛК
            AI1_typeCombo.SelectedItem = NTC; AI1_typeCombo.Enabled = false;
            AI2_typeCombo.SelectedItem = NTC; AI2_typeCombo.Enabled = false;
            AI3_typeCombo.SelectedItem = NTC; AI3_typeCombo.Enabled = false;
            AI4_typeCombo.SelectedItem = NTC; AI4_typeCombo.Enabled = false;
            AI5_typeCombo.SelectedItem = NTC; AI5_typeCombo.Enabled = false;
            AI6_typeCombo.SelectedItem = NTC; AI6_typeCombo.Enabled = false;
            // Типы AI сигналов, блок расширения 1
            AI1bl1_typeCombo.SelectedItem = NTC; AI1bl1_typeCombo.Enabled = false;
            AI2bl1_typeCombo.SelectedItem = NTC; AI2bl1_typeCombo.Enabled = false;
            AI3bl1_typeCombo.SelectedItem = NTC; AI3bl1_typeCombo.Enabled = false;
            AI4bl1_typeCombo.SelectedItem = NTC; AI4bl1_typeCombo.Enabled = false;
            AI5bl1_typeCombo.SelectedItem = NTC; AI5bl1_typeCombo.Enabled = false;
            AI6bl1_typeCombo.SelectedItem = NTC; AI6bl1_typeCombo.Enabled = false;
            // Типы AI сигналов, блок расширения 2
            AI1bl2_typeCombo.SelectedItem = NTC; AI1bl2_typeCombo.Enabled = false;
            AI2bl2_typeCombo.SelectedItem = NTC; AI2bl2_typeCombo.Enabled = false;
            AI3bl2_typeCombo.SelectedItem = NTC; AI3bl2_typeCombo.Enabled = false;
            AI4bl2_typeCombo.SelectedItem = NTC; AI4bl2_typeCombo.Enabled = false;
            AI5bl2_typeCombo.SelectedItem = NTC; AI5bl2_typeCombo.Enabled = false;
            AI6bl2_typeCombo.SelectedItem = NTC; AI6bl2_typeCombo.Enabled = false;
            // Типы AI сигналов, блок расширения 3
            AI1bl3_typeCombo.SelectedItem = NTC; AI1bl3_typeCombo.Enabled = false;
            AI2bl3_typeCombo.SelectedItem = NTC; AI2bl3_typeCombo.Enabled = false;
            AI3bl3_typeCombo.SelectedItem = NTC; AI3bl3_typeCombo.Enabled = false;
            AI4bl3_typeCombo.SelectedItem = NTC; AI4bl3_typeCombo.Enabled = false;
            AI5bl3_typeCombo.SelectedItem = NTC; AI5bl3_typeCombo.Enabled = false;
            AI6bl3_typeCombo.SelectedItem = NTC; AI6bl3_typeCombo.Enabled = false;
        }

        ///<summary>Проверка распределения сигналов</summary>
        private void CheckSignalsReady()
        {
            bool a = true;
            foreach (var elem in list_do)
                if (elem.Active) a = false;
            foreach (var elem in list_ao)
                if (elem.Active) a = false;
            foreach (var elem in list_ai)
                if (elem.Active) a = false;
            foreach (var elem in list_di)
                if (elem.Active) a = false;

            if (a) // Сигналы распределены
            {
                signalsReadyLabel.Text = "Карта входов/выходов сформирована";
                signalsReadyLabel.ForeColor = Color.Green;
                loadPLC_SignalsButton.Show(); // Кнопка "Далее"
                loadToExl.Show(); // Кнопка экспорта таблицы сигналов в Excel
                saveSpecToolStripMenuItem.Enabled = true; // Возможность сохранить спецификацию
            } 
            else // Сигналы не распределены
            {
                signalsReadyLabel.Text = "Карта входов/выходов некорректна";
                signalsReadyLabel.ForeColor = Color.Red;
                loadPLC_SignalsButton.Hide(); // Кнопка "Далее"
                loadToExl.Hide(); // Скрытие кнопки экспорта таблицы сигналов в Excel
                saveSpecToolStripMenuItem.Enabled = false; // Невозможность сохранить спецификацию
            }
        }

        ///<summary>Изменили DO1 comboBox</summary> 
        private void DO1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO1_combo.SelectedIndex == DO1combo_index) return; // Индекс не поменялся
            if (DO1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO1_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    do_find = list_do.Find(x => x.Name == DO1combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO1combo_text)); // Удаление из списка
                    if (showCode) DO1_lab.Text = "";
                }
                if (do_find != null) // Найден элемент
                {
                    do_find.Dispose(); // Освобождение сигнала для распределенния
                    list_do.Add(do_find); // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosDO(DO1combo_text, DO1_combo); // Добавление к другим DO
            } 
            else // Выбран сигнал DO
            {
                name = string.Concat(DO1_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name)); // Удаление из списка
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO1_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO1_combo); // Удаление из других DO
                    do_find = list_do.Find(x => x.Name == DO1combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO1combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO1combo_text, DO1_combo); // Добавление к другим DO
                }
            }
            DO1combo_text = DO1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO1combo_index = DO1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO2 comboBox</summary> 
        private void DO2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO2_combo.SelectedIndex == DO2combo_index) return; // Индекс не поменялся
            if (DO2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO2_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO2combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO2combo_text));
                    if (showCode) DO2_lab.Text = "";
                }
                if (do_find != null)
                {
                    do_find.Dispose();
                    list_do.Add(do_find);
                }
                if (!initialComboSignals) AddtoCombosDO(DO2combo_text, DO2_combo);
            } 
            else // Выбран сигнал DO
            {
                name = string.Concat(DO2_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name)); // Удаление из списка
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO2_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO2_combo); // Удаление из других DO
                    do_find = list_do.Find(x => x.Name == DO2combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO2combo_text)); // Удаление из списка
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO2combo_text, DO2_combo); // Добавление к другим DO
                }
            }
            DO2combo_text = DO2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO2combo_index = DO2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO3 comboBox</summary> 
        private void DO3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO3_combo.SelectedIndex == DO3combo_index) return; // Индекс не поменялся
            if (DO3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO3_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO3combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO3combo_text));
                    if (showCode) DO3_lab.Text = "";
                }
                if (do_find != null)
                {
                    do_find.Dispose();
                    list_do.Add(do_find);
                }
                if (!initialComboSignals) AddtoCombosDO(DO3combo_text, DO3_combo);
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO3_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name)); // Удаление из списка
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO3_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO3_combo); // Удаление из других DO
                    do_find = list_do.Find(x => x.Name == DO3combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO3combo_text)); // Удаление из списка
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO3combo_text, DO3_combo); // Добавление к другим DO
                }
            }
            DO3combo_text = DO3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO3combo_index = DO3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO4 comboBox</summary> 
        private void DO4_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO4_combo.SelectedIndex == DO4combo_index) return; // Индекс не поменялся
            if (DO4_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO4_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO4combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO4combo_text));
                    if (showCode) DO4_lab.Text = "";
                }
                if (do_find != null)
                {
                    do_find.Dispose();
                    list_do.Add(do_find);
                }
                if (!initialComboSignals) AddtoCombosDO(DO4combo_text, DO4_combo);
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO4_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name)); // Удаление из списка
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO4_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO4_combo); // Удаление из других DO
                    do_find = list_do.Find(x => x.Name == DO4combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO4combo_text)); // Удаление из списка
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO4combo_text, DO4_combo); // Добавление к другим DO
                }
            }
            DO4combo_text = DO4_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO4combo_index = DO4_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO5 comboBox</summary> 
        private void DO5_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO5_combo.SelectedIndex == DO5combo_index) return; // Индекс не поменялся
            if (DO5_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO5_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO5combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO5combo_text));
                    if (showCode) DO5_lab.Text = "";
                }
                if (do_find != null)
                {
                    do_find.Dispose();
                    list_do.Add(do_find);
                }
                if (!initialComboSignals) AddtoCombosDO(DO5combo_text, DO5_combo);
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO5_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name)); // Удаление из списка
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO5_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO5_combo); // Удаление из других DO
                    do_find = list_do.Find(x => x.Name == DO5combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO5combo_text)); // Удаление из списка
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO5combo_text, DO5_combo); // Добавление к другим DOs
                }
            }
            DO5combo_text = DO5_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO5combo_index = DO5_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO6 comboBox</summary> 
        private void DO6_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO6_combo.SelectedIndex == DO6combo_index) return; // Индекс не поменялся
            if (DO6_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO6_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO6combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO6combo_text));
                    if (showCode) DO6_lab.Text = "";
                }
                if (do_find != null)
                {
                    do_find.Dispose();
                    list_do.Add(do_find);
                }
                if (!initialComboSignals) AddtoCombosDO(DO6combo_text, DO6_combo);
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO6_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name)); // Удаление из списка
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO6_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO6_combo); // Удаление из других DO
                    do_find = list_do.Find(x => x.Name == DO6combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO6combo_text)); // Удаление из списка
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO6combo_text, DO6_combo); // Добавление к остальным DO
                }
            }
            DO6combo_text = DO6_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO6combo_index = DO6_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO7 comboBox</summary> 
        private void DO7_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO7_combo.SelectedIndex == DO7combo_index) return; // Индекс не поменялся
            if (DO7_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO7_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO7combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO7combo_text));
                    if (showCode) DO7_lab.Text = "";
                }
                if (do_find != null)
                {
                    do_find.Dispose();
                    list_do.Add(do_find);
                }
                if (!initialComboSignals) AddtoCombosDO(DO7combo_text, DO7_combo);
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO7_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name)); // Удаление из списка
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO7_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO7_combo); // Удаление из других DO
                    do_find = list_do.Find(x => x.Name == DO7combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO7combo_text)); // Удаление из списка
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO7combo_text, DO7_combo); // Добавление к другим DO
                }
            }
            DO7combo_text = DO7_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO7combo_index = DO7_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO1 блока расширения 1 comboBox</summary>
        private void DO1bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO1bl1_combo.SelectedIndex == DO1bl1combo_index) return; // Индекс не поменялся
            if (DO1bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO1bl1_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO1bl1combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO1bl1combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO1bl1_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO1bl1combo_text, DO1bl1_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO1bl1_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO1bl1_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO1bl1_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO1bl1combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO1bl1combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO1bl1combo_text, DO1bl1_combo); // Добавление к другим DO
                }
            }
            DO1bl1combo_text = DO1bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO1bl1combo_index = DO1bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO2 блока расширения 1 comboBox</summary>
        private void DO2bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO2bl1_combo.SelectedIndex == DO2bl1combo_index) return; // Индекс не поменялся
            if (DO2bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO2bl1_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO2bl1combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO2bl1combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO2bl1_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO2bl1combo_text, DO2bl1_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO2bl1_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO2bl1_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO2bl1_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO2bl1combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO2bl1combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO2bl1combo_text, DO2bl1_combo); // Добавление к другим DO
                }
            }
            DO2bl1combo_text = DO2bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO2bl1combo_index = DO2bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO3 блока расширения 1 comboBox</summary>
        private void DO3bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO3bl1_combo.SelectedIndex == DO3bl1combo_index) return; // Индекс не поменялся
            if (DO3bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO3bl1_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO3bl1combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO3bl1combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO3bl1_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO3bl1combo_text, DO3bl1_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO3bl1_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO3bl1_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO3bl1_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO3bl1combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO3bl1combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO3bl1combo_text, DO3bl1_combo); // Добавление к другим DO
                }
            }
            DO3bl1combo_text = DO3bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO3bl1combo_index = DO3bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO4 блока расширения 1 comboBox</summary>
        private void DO4bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO4bl1_combo.SelectedIndex == DO4bl1combo_index) return; // Индекс не поменялся
            if (DO4bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO4bl1_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO4bl1combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO4bl1combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO4bl1_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO4bl1combo_text, DO4bl1_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO4bl1_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO4bl1_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO4bl1_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO4bl1combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO4bl1combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO4bl1combo_text, DO4bl1_combo); // Добавление к другим DO
                }
            }
            DO4bl1combo_text = DO4bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO4bl1combo_index = DO4bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO5 блока расширения 1 comboBox</summary>
        private void DO5bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO5bl1_combo.SelectedIndex == DO5bl1combo_index) return; // Индекс не поменялся
            if (DO5bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO5bl1_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO5bl1combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO5bl1combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO5bl1_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO5bl1combo_text, DO5bl1_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO5bl1_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO5bl1_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO5bl1_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO5bl1combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO5bl1combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO5bl1combo_text, DO5bl1_combo); // Добавление к другим DO
                }
            }
            DO5bl1combo_text = DO5bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO5bl1combo_index = DO5bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO6 блока расширения 1 comboBox</summary>
        private void DO6bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO6bl1_combo.SelectedIndex == DO6bl1combo_index) return; // Индекс не поменялся
            if (DO6bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO6bl1_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO6bl1combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO6bl1combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO6bl1_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO6bl1combo_text, DO6bl1_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO6bl1_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO6bl1_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO6bl1_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO6bl1combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO6bl1combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO6bl1combo_text, DO6bl1_combo); // Добавление к другим DO
                }
            }
            DO6bl1combo_text = DO6bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO6bl1combo_index = DO6bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO7 блока расширения 1 comboBox</summary>
        private void DO7bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO7bl1_combo.SelectedIndex == DO7bl1combo_index) return; // Индекс не поменялся
            if (DO7bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO7bl1_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO7bl1combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO7bl1combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO7bl1_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO7bl1combo_text, DO7bl1_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO7bl1_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO7bl1_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO7bl1_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO7bl1combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO7bl1combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO7bl1combo_text, DO7bl1_combo); // Добавление к другим DO
                }
            }
            DO7bl1combo_text = DO7bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO7bl1combo_index = DO7bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO1 блока расширения 2 comboBox</summary>
        private void DO1bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO1bl2_combo.SelectedIndex == DO1bl2combo_index) return; // Индекс не поменялся
            if (DO1bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO1bl2_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO1bl2combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO1bl2combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO1bl2_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO1bl2combo_text, DO1bl2_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO1bl2_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO1bl2_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO1bl2_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO1bl2combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO1bl2combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO1bl2combo_text, DO1bl2_combo); // Добавление к другим DO
                }
            }
            DO1bl2combo_text = DO1bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO1bl2combo_index = DO1bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO2 блока расширения 2 comboBox</summary>
        private void DO2bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO2bl2_combo.SelectedIndex == DO2bl2combo_index) return; // Индекс не поменялся
            if (DO2bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO2bl2_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO2bl2combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO2bl2combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO2bl2_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO2bl2combo_text, DO2bl2_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO2bl2_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO2bl2_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO2bl2_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO2bl2combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO2bl2combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO2bl2combo_text, DO2bl2_combo); // Добавление к другим DO
                }
            }
            DO2bl2combo_text = DO2bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO2bl2combo_index = DO2bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO3 блока расширения 2 comboBox</summary>
        private void DO3bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO3bl2_combo.SelectedIndex == DO3bl2combo_index) return; // Индекс не поменялся
            if (DO3bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO3bl2_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO3bl2combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO3bl2combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO3bl2_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO3bl2combo_text, DO3bl2_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO3bl2_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO3bl2_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO3bl2_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO3bl2combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO3bl2combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO3bl2combo_text, DO3bl2_combo); // Добавление к другим DO
                }
            }
            DO3bl2combo_text = DO3bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO3bl2combo_index = DO3bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO4 блока расширения 2 comboBox</summary>
        private void DO4bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO4bl2_combo.SelectedIndex == DO4bl2combo_index) return; // Индекс не поменялся
            if (DO4bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO4bl2_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO4bl2combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO4bl2combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO4bl2_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO4bl2combo_text, DO4bl2_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO4bl2_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO4bl2_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO4bl2_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO4bl2combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO4bl2combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO4bl2combo_text, DO4bl2_combo); // Добавление к другим DO
                }
            }
            DO4bl2combo_text = DO4bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO4bl2combo_index = DO4bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO5 блока расширения 2 comboBox</summary>
        private void DO5bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO5bl2_combo.SelectedIndex == DO5bl2combo_index) return; // Индекс не поменялся
            if (DO5bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO5bl2_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO5bl2combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO5bl2combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO5bl2_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO5bl2combo_text, DO5bl2_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO5bl2_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO5bl2_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO5bl2_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO5bl2combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO5bl2combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO5bl2combo_text, DO5bl2_combo); // Добавление к другим DO
                }
            }
            DO5bl2combo_text = DO5bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO5bl2combo_index = DO5bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO6 блока расширения 2 comboBox</summary>
        private void DO6bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO6bl2_combo.SelectedIndex == DO6bl2combo_index) return; // Индекс не поменялся
            if (DO6bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO6bl2_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO6bl2combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO6bl2combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO6bl2_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO6bl2combo_text, DO6bl2_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO6bl2_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO6bl2_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO6bl2_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO6bl2combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO6bl2combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO6bl2combo_text, DO6bl2_combo); // Добавление к другим DO
                }
            }
            DO6bl2combo_text = DO6bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO6bl2combo_index = DO6bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO7 блока расширения 2 comboBox</summary>
        private void DO7bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO7bl2_combo.SelectedIndex == DO7bl2combo_index) return; // Индекс не поменялся
            if (DO7bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO7bl2_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO7bl2combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO7bl2combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO7bl2_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO7bl2combo_text, DO7bl2_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO7bl2_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO7bl2_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO7bl2_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO7bl2combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO7bl2combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO7bl2combo_text, DO7bl2_combo); // Добавление к другим DO
                }
            }
            DO7bl2combo_text = DO7bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO7bl2combo_index = DO7bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO1 блока расширения 3 comboBox</summary>
        private void DO1bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO1bl3_combo.SelectedIndex == DO1bl3combo_index) return; // Индекс не поменялся
            if (DO1bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO1bl3_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO1bl3combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO1bl3combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO1bl3_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO1bl3combo_text, DO1bl3_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO1bl3_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO1bl3_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO1bl3_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO1bl3combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO1bl3combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO1bl3combo_text, DO1bl3_combo); // Добавление к другим DO
                }
            }
            DO1bl3combo_text = DO1bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO1bl3combo_index = DO1bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO2 блока расширения 3 comboBox</summary>
        private void DO2bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO2bl3_combo.SelectedIndex == DO2bl3combo_index) return; // Индекс не поменялся
            if (DO2bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO2bl3_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO2bl3combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO2bl3combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO2bl3_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO2bl3combo_text, DO2bl3_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO2bl3_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO2bl3_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO2bl3_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO2bl3combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO2bl3combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO2bl3combo_text, DO2bl3_combo); // Добавление к другим DO
                }
            }
            DO2bl3combo_text = DO2bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO2bl3combo_index = DO2bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO3 блока расширения 3 comboBox</summary>
        private void DO3bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO3bl3_combo.SelectedIndex == DO3bl3combo_index) return; // Индекс не поменялся
            if (DO3bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO3bl3_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO3bl3combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO3bl3combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO3bl3_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO3bl3combo_text, DO3bl3_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO3bl3_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO3bl3_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO3bl3_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO3bl3combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO3bl3combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO3bl3combo_text, DO3bl3_combo); // Добавление к другим DO
                }
            }
            DO3bl3combo_text = DO3bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO3bl3combo_index = DO3bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO4 блока расширения 3 comboBox</summary>
        private void DO4bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO4bl3_combo.SelectedIndex == DO4bl3combo_index) return; // Индекс не поменялся
            if (DO4bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO4bl3_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO4bl3combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO4bl3combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO4bl3_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO4bl3combo_text, DO4bl3_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO4bl3_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO4bl3_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO4bl3_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO4bl3combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO4bl3combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO4bl3combo_text, DO4bl3_combo); // Добавление к другим DO
                }
            }
            DO4bl3combo_text = DO4bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO4bl3combo_index = DO4bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO5 блока расширения 3 comboBox</summary>
        private void DO5bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO5bl3_combo.SelectedIndex == DO5bl3combo_index) return; // Индекс не поменялся
            if (DO5bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO5bl3_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO5bl3combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO5bl3combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO5bl3_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO5bl3combo_text, DO5bl3_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO5bl3_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO5bl3_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO5bl3_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO5bl3combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO5bl3combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO5bl3combo_text, DO5bl3_combo); // Добавление к другим DO
                }
            }
            DO5bl3combo_text = DO5bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO5bl3combo_index = DO5bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO6 блока расширения 3 comboBox</summary>
        private void DO6bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO6bl3_combo.SelectedIndex == DO6bl3combo_index) return; // Индекс не поменялся
            if (DO6bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO6bl3_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO6bl3combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO6bl3combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO6bl3_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO6bl3combo_text, DO6bl3_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO6bl3_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO6bl3_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO6bl3_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO6bl3combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO6bl3combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO6bl3combo_text, DO6bl3_combo); // Добавление к другим DO
                }
            }
            DO6bl3combo_text = DO6bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO6bl3combo_index = DO6bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DO7 блока расширения 3 comboBox</summary>
        private void DO7bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Do do_find = null;
            if (subDOcondition) return; // Переход из вычета сигналов DO
            if (DO7bl3_combo.SelectedIndex == DO7bl3combo_index) return; // Индекс не поменялся
            if (DO7bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DO7bl3_combo.Items.Count > 1) // Больше одного элемента
                {
                    do_find = list_do.Find(x => x.Name == DO7bl3combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO7bl3combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                        if (showCode) DO7bl3_lab.Text = "";
                    }
                    if (!initialComboSignals) AddtoCombosDO(DO7bl3combo_text, DO7bl3_combo);
                }
            }
            else // Выбран сигнал DO
            {
                name = string.Concat(DO7bl3_combo.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(list_do.Find(x => x.Name == name));
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) DO7bl3_lab.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDO(name, DO7bl3_combo); // Удаление из других comboBox
                    do_find = list_do.Find(x => x.Name == DO7bl3combo_text);
                    list_do.Remove(list_do.Find(x => x.Name == DO7bl3combo_text));
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(DO7bl3combo_text, DO7bl3_combo); // Добавление к другим DO
                }
            }
            DO7bl3combo_text = DO7bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DO7bl3combo_index = DO7bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Добавление освободившегося DO к остальным comboBox</summary> 
        private void AddtoCombosDO(string name, ComboBox cm)
        {
            Do do_find;
            bool notFound = true; // Элемент в списке не найден
            // Для DO1 comboBox, добавление в остальные слоты для выбора
            if (DO1_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO1_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO2 comboBox, добавление в остальные слоты для выбора
            if (DO2_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO2_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO3 comboBox, добавление в остальные слоты для выбора
            if (DO3_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO3_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO4 comboBox, добавление в остальные слоты для выбора
            if (DO4_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO4_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO4_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO5 comboBox, добавление в остальные слоты для выбора
            if (DO5_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO5_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO5_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO6 comboBox, добавление в остальные слоты для выбора
            if (DO6_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO6_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO6_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO7 comboBox, добавление в остальные слоты для выбора
            if (DO7_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO7_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO7_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO1 блока расширения 1, добавление в остальные слоты для выбора
            if (DO1bl1_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO1bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO1bl1_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO2 блока расширения 1, добавление в остальные слоты для выбора
            if (DO2bl1_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO2bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO2bl1_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO3 блока расширения 1, добавление в остальные слоты для выбора
            if (DO3bl1_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO3bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO3bl1_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO4 блока расширения 1, добавление в остальные слоты для выбора
            if (DO4bl1_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO4bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO4bl1_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO5 блока расширения 1, добавление в остальные слоты для выбора
            if (DO5bl1_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO5bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO5bl1_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO6 блока расширения 1, добавление в остальные слоты для выбора
            if (DO6bl1_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO6bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO6bl1_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO7 блока расширения 1, добавление в остальные слоты для выбора
            if (DO7bl1_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO7bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO7bl1_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO1 блока расширения 2, добавление в остальные слоты для выбора
            if (DO1bl2_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO1bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO1bl2_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO2 блока расширения 2, добавление в остальные слоты для выбора
            if (DO2bl2_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO2bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO2bl2_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO3 блока расширения 2, добавление в остальные слоты для выбора
            if (DO3bl2_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO3bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO3bl2_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO4 блока расширения 2, добавление в остальные слоты для выбора
            if (DO4bl2_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO4bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO4bl2_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO5 блока расширения 2, добавление в остальные слоты для выбора
            if (DO5bl2_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO5bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO5bl2_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO6 блока расширения 2, добавление в остальные слоты для выбора
            if (DO6bl2_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO6bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO6bl2_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO7 блока расширения 2, добавление в остальные слоты для выбора
            if (DO7bl2_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO7bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO7bl2_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO1 блока расширения 3, добавление в остальные слоты для выбора
            if (DO1bl3_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO1bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO1bl3_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO2 блока расширения 3, добавление в остальные слоты для выбора
            if (DO2bl3_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO2bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO2bl3_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO3 блока расширения 3, добавление в остальные слоты для выбора
            if (DO3bl3_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO3bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO3bl3_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO4 блока расширения 3, добавление в остальные слоты для выбора
            if (DO4bl3_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO4bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO4bl3_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO5 блока расширения 3, добавление в остальные слоты для выбора
            if (DO5bl3_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO5bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO5bl3_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO6 блока расширения 3, добавление в остальные слоты для выбора
            if (DO6bl3_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO6bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO6bl3_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
            // Для DO7 блока расширения 3, добавление в остальные слоты для выбора
            if (DO7bl3_combo != cm)
            {
                do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)
                {
                    foreach (var elem in DO7bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DO7bl3_combo.Items.Add(do_find.Name); notFound = true;
                }
            }
        }

        /// <summary>Удаление DO из других comboBox</summary>
        private void SubFromCombosDO(string name, ComboBox cm)
        {
            if (name != NOT_SELECTED) // Кроме "Не выбрано"
            {
                if (DO1_combo != cm) DO1_combo.Items.Remove(name); // DO1
                if (DO2_combo != cm) DO2_combo.Items.Remove(name); // DO2
                if (DO3_combo != cm) DO3_combo.Items.Remove(name); // DO3
                if (DO4_combo != cm) DO4_combo.Items.Remove(name); // DO4
                if (DO5_combo != cm) DO5_combo.Items.Remove(name); // DO5
                if (DO6_combo != cm) DO6_combo.Items.Remove(name); // DO6
                if (DO7_combo != cm) DO7_combo.Items.Remove(name); // DO7
                if (DO1bl1_combo != cm) DO1bl1_combo.Items.Remove(name); // DO1, блок 1 
                if (DO2bl1_combo != cm) DO2bl1_combo.Items.Remove(name); // DO2, блок 1 
                if (DO3bl1_combo != cm) DO3bl1_combo.Items.Remove(name); // DO3, блок 1 
                if (DO4bl1_combo != cm) DO4bl1_combo.Items.Remove(name); // DO4, блок 1 
                if (DO5bl1_combo != cm) DO5bl1_combo.Items.Remove(name); // DO5, блок 1 
                if (DO6bl1_combo != cm) DO6bl1_combo.Items.Remove(name); // DO6, блок 1 
                if (DO7bl1_combo != cm) DO7bl1_combo.Items.Remove(name); // DO7, блок 1 
                if (DO1bl2_combo != cm) DO1bl2_combo.Items.Remove(name); // DO1, блок 2 
                if (DO2bl2_combo != cm) DO2bl2_combo.Items.Remove(name); // DO2, блок 2 
                if (DO3bl2_combo != cm) DO3bl2_combo.Items.Remove(name); // DO3, блок 2 
                if (DO4bl2_combo != cm) DO4bl2_combo.Items.Remove(name); // DO4, блок 2 
                if (DO5bl2_combo != cm) DO5bl2_combo.Items.Remove(name); // DO5, блок 2 
                if (DO6bl2_combo != cm) DO6bl2_combo.Items.Remove(name); // DO6, блок 2 
                if (DO7bl2_combo != cm) DO7bl2_combo.Items.Remove(name); // DO7, блок 2 
                if (DO1bl3_combo != cm) DO1bl3_combo.Items.Remove(name); // DO1, блок 3
                if (DO2bl3_combo != cm) DO2bl3_combo.Items.Remove(name); // DO2, блок 3
                if (DO3bl3_combo != cm) DO3bl3_combo.Items.Remove(name); // DO3, блок 3
                if (DO4bl3_combo != cm) DO4bl3_combo.Items.Remove(name); // DO4, блок 3
                if (DO5bl3_combo != cm) DO5bl3_combo.Items.Remove(name); // DO5, блок 3
                if (DO6bl3_combo != cm) DO6bl3_combo.Items.Remove(name); // DO6, блок 3
                if (DO7bl3_combo != cm) DO7bl3_combo.Items.Remove(name); // DO7, блок 3
            }
        }

        ///<summary>Добавление нового DO и его назначение под выход</summary>
        private void AddNewDO(ushort code)
        {
            if (DO1_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO1
                DO1_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO1_combo.SelectedIndex = DO1_combo.Items.Count - 1;
                DO1combo_text = DO1_combo.SelectedItem.ToString();
                DO1combo_index = DO1_combo.SelectedIndex;
                if (showCode) DO1_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO2_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO2
                DO2_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO2_combo.SelectedIndex = DO2_combo.Items.Count - 1;
                DO2combo_text = DO2_combo.SelectedItem.ToString();
                DO2combo_index = DO2_combo.SelectedIndex;
                if (showCode) DO2_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO3_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO3
                DO3_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO3_combo.SelectedIndex = DO3_combo.Items.Count - 1;
                DO3combo_text = DO3_combo.SelectedItem.ToString();
                DO3combo_index = DO3_combo.SelectedIndex;
                if (showCode) DO3_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO4_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO4
                DO4_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO4_combo.SelectedIndex = DO4_combo.Items.Count - 1;
                DO4combo_text = DO4_combo.SelectedItem.ToString();
                DO4combo_index = DO4_combo.SelectedIndex;
                if (showCode) DO4_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO5_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO5
                DO5_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO5_combo.SelectedIndex = DO5_combo.Items.Count - 1;
                DO5combo_text = DO5_combo.SelectedItem.ToString();
                DO5combo_index = DO5_combo.SelectedIndex;
                if (showCode) DO5_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO6_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO6
                DO6_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO6_combo.SelectedIndex = DO6_combo.Items.Count - 1;
                DO6combo_text = DO6_combo.SelectedItem.ToString();
                DO6combo_index = DO6_combo.SelectedIndex;
                if (showCode) DO6_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO7_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO7
                DO7_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO7_combo.SelectedIndex = DO7_combo.Items.Count - 1;
                DO7combo_text = DO7_combo.SelectedItem.ToString();
                DO7combo_index = DO7_combo.SelectedIndex;
                if (showCode) DO7_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO1bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO1, блок 1
                DO1bl1_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO1bl1_combo.SelectedIndex = DO1bl1_combo.Items.Count - 1;
                DO1bl1combo_text = DO1bl1_combo.SelectedItem.ToString();
                DO1bl1combo_index = DO1bl1_combo.SelectedIndex;
                if (showCode) DO1bl1_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO2bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO2, блок 1
                DO2bl1_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO2bl1_combo.SelectedIndex = DO2bl1_combo.Items.Count - 1;
                DO2bl1combo_text = DO2bl1_combo.SelectedItem.ToString();
                DO2bl1combo_index = DO2bl1_combo.SelectedIndex;
                if (showCode) DO2bl1_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO3bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO3, блок 1
                DO3bl1_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO3bl1_combo.SelectedIndex = DO3bl1_combo.Items.Count - 1;
                DO3bl1combo_text = DO3bl1_combo.SelectedItem.ToString();
                DO3bl1combo_index = DO3bl1_combo.SelectedIndex;
                if (showCode) DO3bl1_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO4bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO4, блок 1
                DO4bl1_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO4bl1_combo.SelectedIndex = DO4bl1_combo.Items.Count - 1;
                DO4bl1combo_text = DO4bl1_combo.SelectedItem.ToString();
                DO4bl1combo_index = DO4bl1_combo.SelectedIndex;
                if (showCode) DO4bl1_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO5bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO5, блок 1
                DO5bl1_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO5bl1_combo.SelectedIndex = DO5bl1_combo.Items.Count - 1;
                DO5bl1combo_text = DO5bl1_combo.SelectedItem.ToString();
                DO5bl1combo_index = DO5bl1_combo.SelectedIndex;
                if (showCode) DO5bl1_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO6bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO6, блок 1
                DO6bl1_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO6bl1_combo.SelectedIndex = DO6bl1_combo.Items.Count - 1;
                DO6bl1combo_text = DO6bl1_combo.SelectedItem.ToString();
                DO6bl1combo_index = DO6bl1_combo.SelectedIndex;
                if (showCode) DO6bl1_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO7bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO7, блок 1
                DO7bl1_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO7bl1_combo.SelectedIndex = DO7bl1_combo.Items.Count - 1;
                DO7bl1combo_text = DO7bl1_combo.SelectedItem.ToString();
                DO7bl1combo_index = DO7bl1_combo.SelectedIndex;
                if (showCode) DO7bl1_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO1bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO1, блок 2
                DO1bl2_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO1bl2_combo.SelectedIndex = DO1bl2_combo.Items.Count - 1;
                DO1bl2combo_text = DO1bl2_combo.SelectedItem.ToString();
                DO1bl2combo_index = DO1bl2_combo.SelectedIndex;
                if (showCode) DO1bl2_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO2bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO2, блок 2
                DO2bl2_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO2bl2_combo.SelectedIndex = DO2bl2_combo.Items.Count - 1;
                DO2bl2combo_text = DO2bl2_combo.SelectedItem.ToString();
                DO2bl2combo_index = DO2bl2_combo.SelectedIndex;
                if (showCode) DO2bl2_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO3bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO3, блок 2
                DO3bl2_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO3bl2_combo.SelectedIndex = DO3bl2_combo.Items.Count - 1;
                DO3bl2combo_text = DO3bl2_combo.SelectedItem.ToString();
                DO3bl2combo_index = DO3bl2_combo.SelectedIndex;
                if (showCode) DO3bl2_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO4bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO4, блок 2
                DO4bl2_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO4bl2_combo.SelectedIndex = DO4bl2_combo.Items.Count - 1;
                DO4bl2combo_text = DO4bl2_combo.SelectedItem.ToString();
                DO4bl2combo_index = DO4bl2_combo.SelectedIndex;
                if (showCode) DO4bl2_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO5bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO5, блок 2
                DO5bl2_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO5bl2_combo.SelectedIndex = DO5bl2_combo.Items.Count - 1;
                DO5bl2combo_text = DO5bl2_combo.SelectedItem.ToString();
                DO5bl2combo_index = DO5bl2_combo.SelectedIndex;
                if (showCode) DO5bl2_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO6bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO6, блок 2
                DO6bl2_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO6bl2_combo.SelectedIndex = DO6bl2_combo.Items.Count - 1;
                DO6bl2combo_text = DO6bl2_combo.SelectedItem.ToString();
                DO6bl2combo_index = DO6bl2_combo.SelectedIndex;
                if (showCode) DO6bl2_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO7bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO7, блок 2
                DO7bl2_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO7bl2_combo.SelectedIndex = DO7bl2_combo.Items.Count - 1;
                DO7bl2combo_text = DO7bl2_combo.SelectedItem.ToString();
                DO7bl2combo_index = DO7bl2_combo.SelectedIndex;
                if (showCode) DO7bl2_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO1bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO1, блок 3
                DO1bl3_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO1bl3_combo.SelectedIndex = DO1bl3_combo.Items.Count - 1;
                DO1bl3combo_text = DO1bl3_combo.SelectedItem.ToString();
                DO1bl3combo_index = DO1bl3_combo.SelectedIndex;
                if (showCode) DO1bl3_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO2bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO2, блок 3
                DO2bl3_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO2bl3_combo.SelectedIndex = DO2bl3_combo.Items.Count - 1;
                DO2bl3combo_text = DO2bl3_combo.SelectedItem.ToString();
                DO2bl3combo_index = DO2bl3_combo.SelectedIndex;
                if (showCode) DO2bl3_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO3bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO3, блок 3
                DO3bl3_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO3bl3_combo.SelectedIndex = DO3bl3_combo.Items.Count - 1;
                DO3bl3combo_text = DO3bl3_combo.SelectedItem.ToString();
                DO3bl3combo_index = DO3bl3_combo.SelectedIndex;
                if (showCode) DO3bl3_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO4bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO4, блок 3
                DO4bl3_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO4bl3_combo.SelectedIndex = DO4bl3_combo.Items.Count - 1;
                DO4bl3combo_text = DO4bl3_combo.SelectedItem.ToString();
                DO4bl3combo_index = DO4bl3_combo.SelectedIndex;
                if (showCode) DO4bl3_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO5bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO5, блок 3
                DO5bl3_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO5bl3_combo.SelectedIndex = DO5bl3_combo.Items.Count - 1;
                DO5bl3combo_text = DO5bl3_combo.SelectedItem.ToString();
                DO5bl3combo_index = DO5bl3_combo.SelectedIndex;
                if (showCode) DO5bl3_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO6bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO6, блок 3
                DO6bl3_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO6bl3_combo.SelectedIndex = DO6bl3_combo.Items.Count - 1;
                DO6bl3combo_text = DO6bl3_combo.SelectedItem.ToString();
                DO6bl3combo_index = DO6bl3_combo.SelectedIndex;
                if (showCode) DO6bl3_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
            else if (DO7bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // DO7, блок 3
                DO7bl3_combo.Items.Add(list_do.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DO7bl3_combo.SelectedIndex = DO7bl3_combo.Items.Count - 1;
                DO7bl3combo_text = DO7bl3_combo.SelectedItem.ToString();
                DO7bl3combo_index = DO7bl3_combo.SelectedIndex;
                if (showCode) DO7bl3_lab.Text = code.ToString();
                list_do.Find(x => x.Code == code).Select();
            }
        }

        ///<summary>Удаление DO из всех comboBox</summary>
        private void SubFromCombosDO(ushort code)
        {
            Do findDo, findDO_2;
            string name = "";
            findDo = list_do.Find(x => x.Code == code);
            if (findDo != null) name = findDo.Name;
            else return;
            subDOcondition = true; // Признак удаления DO, не работает событие indexChanged
            for (int i = 0; i < DO1_combo.Items.Count; i++) // DO1
                if (DO1_combo.Items[i].ToString() == name)
                {
                    DO1_combo.Items.Remove(name);
                    if (DO1_combo.Items.Count > 1) // Больше одного элемента
                    {
                        DO1_combo.SelectedIndex = DO1_combo.Items.Count - 1;
                        if (DO1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO1_combo.SelectedItem.ToString(), DO1_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO1_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO1_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO1_combo.SelectedItem = NOT_SELECTED;
                        DO1_lab.Text = "";
                    }
                        
                    DO1combo_text = DO1_combo.SelectedItem.ToString();
                    DO1combo_index = DO1_combo.SelectedIndex;
                }
            for (int i = 0; i < DO2_combo.Items.Count; i++) // DO2
                if (DO2_combo.Items[i].ToString() == name)
                {
                    DO2_combo.Items.Remove(name);
                    if (DO2_combo.Items.Count > 1) // Больше одного элемента
                    {
                        DO2_combo.SelectedIndex = DO2_combo.Items.Count - 1;
                        if (DO2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO2_combo.SelectedItem.ToString(), DO2_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO2_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO2_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO2_combo.SelectedItem = NOT_SELECTED;
                        DO2_lab.Text = "";
                    }
                    DO2combo_text = DO2_combo.SelectedItem.ToString();
                    DO2combo_index = DO2_combo.SelectedIndex;
                }
            for (int i = 0; i < DO3_combo.Items.Count; i++) // DO3
                if (DO3_combo.Items[i].ToString() == name)
                {
                    DO3_combo.Items.Remove(name);
                    if (DO3_combo.Items.Count > 1)
                    {
                        DO3_combo.SelectedIndex = DO3_combo.Items.Count - 1;
                        if (DO3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO3_combo.SelectedItem.ToString(), DO3_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO3_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO3_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO3_combo.SelectedItem = NOT_SELECTED;
                        DO3_lab.Text = "";
                    }
                    DO3combo_text = DO3_combo.SelectedItem.ToString();
                    DO3combo_index = DO3_combo.SelectedIndex;
                }
            for (int i = 0; i < DO4_combo.Items.Count; i++) // DO4
                if (DO4_combo.Items[i].ToString() == name)
                {
                    DO4_combo.Items.Remove(name);
                    if (DO4_combo.Items.Count > 1)
                    {
                        DO4_combo.SelectedIndex = DO4_combo.Items.Count - 1;
                        if (DO4_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO4_combo.SelectedItem.ToString(), DO4_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO4_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO4_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO4_combo.SelectedItem = NOT_SELECTED;
                        DO4_lab.Text = "";
                    }
                    DO4combo_text = DO4_combo.SelectedItem.ToString();
                    DO4combo_index = DO4_combo.SelectedIndex;
                }
            for (int i = 0; i < DO5_combo.Items.Count; i++) // DO5
                if (DO5_combo.Items[i].ToString() == name)
                {
                    DO5_combo.Items.Remove(name);
                    if (DO5_combo.Items.Count > 1)
                    {
                        DO5_combo.SelectedIndex = DO5_combo.Items.Count - 1;
                        if (DO5_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO5_combo.SelectedItem.ToString(), DO5_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO5_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO5_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO5_combo.SelectedItem = NOT_SELECTED;
                        DO5_lab.Text = "";
                    }
                    DO5combo_text = DO5_combo.SelectedItem.ToString();
                    DO5combo_index = DO5_combo.SelectedIndex;
                }
            for (int i = 0; i < DO6_combo.Items.Count; i++) // DO6
                if (DO6_combo.Items[i].ToString() == name)
                {
                    DO6_combo.Items.Remove(name);
                    if (DO6_combo.Items.Count > 1)
                    {
                        DO6_combo.SelectedIndex = DO6_combo.Items.Count - 1;
                        if (DO6_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO6_combo.SelectedItem.ToString(), DO6_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO6_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO6_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO6_combo.SelectedItem = NOT_SELECTED;
                        DO6_lab.Text = "";
                    }
                    DO6combo_text = DO6_combo.SelectedItem.ToString();
                    DO6combo_index = DO6_combo.SelectedIndex;
                }
            for (int i = 0; i < DO7_combo.Items.Count; i++) // DO7
                if (DO7_combo.Items[i].ToString() == name)
                {
                    DO7_combo.Items.Remove(name);
                    if (DO7_combo.Items.Count > 1)
                    {
                        DO7_combo.SelectedIndex = DO7_combo.Items.Count - 1;
                        if (DO7_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO7_combo.SelectedItem.ToString(), DO7_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO7_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO7_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO7_combo.SelectedItem = NOT_SELECTED;
                        DO7_lab.Text = "";
                    }
                    DO7combo_text = DO7_combo.SelectedItem.ToString();
                    DO7combo_index = DO7_combo.SelectedIndex;
                }
            for (int i = 0; i < DO1bl1_combo.Items.Count; i++) // DO1 блок 1
                if (DO1bl1_combo.Items[i].ToString() == name)
                {
                    DO1bl1_combo.Items.Remove(name);
                    if (DO1bl1_combo.Items.Count > 1)
                    {
                        DO1bl1_combo.SelectedIndex = DO1bl1_combo.Items.Count - 1;
                        if (DO1bl1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO1bl1_combo.SelectedItem.ToString(), DO1bl1_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO1bl1_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO1bl1_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO1bl1_combo.SelectedItem = NOT_SELECTED;
                        DO1bl1_lab.Text = "";
                    }
                    DO1bl1combo_text = DO1bl1_combo.SelectedItem.ToString();
                    DO1bl1combo_index = DO1bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < DO2bl1_combo.Items.Count; i++) // DO2 блок 1
                if (DO2bl1_combo.Items[i].ToString() == name)
                {
                    DO2bl1_combo.Items.Remove(name);
                    if (DO2bl1_combo.Items.Count > 1)
                    {
                        DO2bl1_combo.SelectedIndex = DO2bl1_combo.Items.Count - 1;
                        if (DO2bl1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO2bl1_combo.SelectedItem.ToString(), DO2bl1_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO2bl1_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO2bl1_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO2bl1_combo.SelectedItem = NOT_SELECTED;
                        DO2bl1_lab.Text = "";
                    }
                    DO2bl1combo_text = DO2bl1_combo.SelectedItem.ToString();
                    DO2bl1combo_index = DO2bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < DO3bl1_combo.Items.Count; i++) // DO3 блок 1
                if (DO3bl1_combo.Items[i].ToString() == name)
                {
                    DO3bl1_combo.Items.Remove(name);
                    if (DO3bl1_combo.Items.Count > 1)
                    {
                        DO3bl1_combo.SelectedIndex = DO3bl1_combo.Items.Count - 1;
                        if (DO3bl1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO3bl1_combo.SelectedItem.ToString(), DO3bl1_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO3bl1_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO3bl1_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO3bl1_combo.SelectedItem = NOT_SELECTED;
                        DO3bl1_lab.Text = "";
                    }
                    DO3bl1combo_text = DO3bl1_combo.SelectedItem.ToString();
                    DO3bl1combo_index = DO3bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < DO4bl1_combo.Items.Count; i++) // DO4 блок 1
                if (DO4bl1_combo.Items[i].ToString() == name)
                {
                    DO4bl1_combo.Items.Remove(name);
                    if (DO4bl1_combo.Items.Count > 1)
                    {
                        DO4bl1_combo.SelectedIndex = DO4bl1_combo.Items.Count - 1;
                        if (DO4bl1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO4bl1_combo.SelectedItem.ToString(), DO4bl1_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO4bl1_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO4bl1_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO4bl1_combo.SelectedItem = NOT_SELECTED;
                        DO4bl1_lab.Text = "";
                    }
                    DO4bl1combo_text = DO4bl1_combo.SelectedItem.ToString();
                    DO4bl1combo_index = DO4bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < DO5bl1_combo.Items.Count; i++) // DO5 блок 1
                if (DO5bl1_combo.Items[i].ToString() == name)
                {
                    DO5bl1_combo.Items.Remove(name);
                    if (DO5bl1_combo.Items.Count > 1)
                    {
                        DO5bl1_combo.SelectedIndex = DO5bl1_combo.Items.Count - 1;
                        if (DO5bl1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO5bl1_combo.SelectedItem.ToString(), DO5bl1_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO5bl1_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO5bl1_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO5bl1_combo.SelectedItem = NOT_SELECTED;
                        DO5bl1_lab.Text = "";
                    }
                    DO5bl1combo_text = DO5bl1_combo.SelectedItem.ToString();
                    DO5bl1combo_index = DO5bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < DO6bl1_combo.Items.Count; i++) // DO6 блок 1
                if (DO6bl1_combo.Items[i].ToString() == name)
                {
                    DO6bl1_combo.Items.Remove(name);
                    if (DO6bl1_combo.Items.Count > 1)
                    {
                        DO6bl1_combo.SelectedIndex = DO6bl1_combo.Items.Count - 1;
                        if (DO6bl1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO6bl1_combo.SelectedItem.ToString(), DO6bl1_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO6bl1_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO6bl1_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO6bl1_combo.SelectedItem = NOT_SELECTED;
                        DO6bl1_lab.Text = "";
                    }
                    DO6bl1combo_text = DO6bl1_combo.SelectedItem.ToString();
                    DO6bl1combo_index = DO6bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < DO7bl1_combo.Items.Count; i++) // DO7 блок 1
                if (DO7bl1_combo.Items[i].ToString() == name)
                {
                    DO7bl1_combo.Items.Remove(name);
                    if (DO7bl1_combo.Items.Count > 1)
                    {
                        DO7bl1_combo.SelectedIndex = DO7bl1_combo.Items.Count - 1;
                        if (DO7bl1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO7bl1_combo.SelectedItem.ToString(), DO7bl1_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO7bl1_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO7bl1_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO7bl1_combo.SelectedItem = NOT_SELECTED;
                        DO7bl1_lab.Text = "";
                    }
                    DO7bl1combo_text = DO7bl1_combo.SelectedItem.ToString();
                    DO7bl1combo_index = DO7bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < DO1bl2_combo.Items.Count; i++) // DO1 блок 2
                if (DO1bl2_combo.Items[i].ToString() == name)
                {
                    DO1bl2_combo.Items.Remove(name);
                    if (DO1bl2_combo.Items.Count > 1)
                    {
                        DO1bl2_combo.SelectedIndex = DO1bl2_combo.Items.Count - 1;
                        if (DO1bl2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO1bl2_combo.SelectedItem.ToString(), DO1bl2_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO1bl2_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO1bl2_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO1bl2_combo.SelectedItem = NOT_SELECTED;
                        DO1bl2_lab.Text = "";
                    }
                    DO1bl2combo_text = DO1bl2_combo.SelectedItem.ToString();
                    DO1bl2combo_index = DO1bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < DO2bl2_combo.Items.Count; i++) // DO2 блок 2
                if (DO2bl2_combo.Items[i].ToString() == name)
                {
                    DO2bl2_combo.Items.Remove(name);
                    if (DO2bl2_combo.Items.Count > 1)
                    {
                        DO2bl2_combo.SelectedIndex = DO2bl2_combo.Items.Count - 1;
                        if (DO2bl2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO2bl2_combo.SelectedItem.ToString(), DO2bl2_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO2bl2_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO2bl2_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO2bl2_combo.SelectedItem = NOT_SELECTED;
                        DO2bl2_lab.Text = "";
                    }
                    DO2bl2combo_text = DO2bl2_combo.SelectedItem.ToString();
                    DO2bl2combo_index = DO2bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < DO3bl2_combo.Items.Count; i++) // DO3 блок 2
                if (DO3bl2_combo.Items[i].ToString() == name)
                {
                    DO3bl2_combo.Items.Remove(name);
                    if (DO3bl2_combo.Items.Count > 1)
                    {
                        DO3bl2_combo.SelectedIndex = DO3bl2_combo.Items.Count - 1;
                        if (DO3bl2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO3bl2_combo.SelectedItem.ToString(), DO3bl2_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO3bl2_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO3bl2_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO3bl2_combo.SelectedItem = NOT_SELECTED;
                        DO3bl2_lab.Text = "";
                    }
                    DO3bl2combo_text = DO3bl2_combo.SelectedItem.ToString();
                    DO3bl2combo_index = DO3bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < DO4bl2_combo.Items.Count; i++) // DO4 блок 2
                if (DO4bl2_combo.Items[i].ToString() == name)
                {
                    DO4bl2_combo.Items.Remove(name);
                    if (DO4bl2_combo.Items.Count > 1)
                    {
                        DO4bl2_combo.SelectedIndex = DO4bl2_combo.Items.Count - 1;
                        if (DO4bl2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO4bl2_combo.SelectedItem.ToString(), DO4bl2_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO4bl2_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO4bl2_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO4bl2_combo.SelectedItem = NOT_SELECTED;
                        DO4bl2_lab.Text = "";
                    }
                    DO4bl2combo_text = DO4bl2_combo.SelectedItem.ToString();
                    DO4bl2combo_index = DO4bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < DO5bl2_combo.Items.Count; i++) // DO5 блок 2
                if (DO5bl2_combo.Items[i].ToString() == name)
                {
                    DO5bl2_combo.Items.Remove(name);
                    if (DO5bl2_combo.Items.Count > 1)
                    {
                        DO5bl2_combo.SelectedIndex = DO5bl2_combo.Items.Count - 1;
                        if (DO5bl2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO5bl2_combo.SelectedItem.ToString(), DO5bl2_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO5bl2_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO5bl2_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO5bl2_combo.SelectedItem = NOT_SELECTED;
                        DO5bl2_lab.Text = "";
                    }
                    DO5bl2combo_text = DO5bl2_combo.SelectedItem.ToString();
                    DO5bl2combo_index = DO5bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < DO6bl2_combo.Items.Count; i++) // DO6 блок 2
                if (DO6bl2_combo.Items[i].ToString() == name)
                {
                    DO6bl2_combo.Items.Remove(name);
                    if (DO6bl2_combo.Items.Count > 1)
                    {
                        DO6bl2_combo.SelectedIndex = DO6bl2_combo.Items.Count - 1;
                        if (DO6bl2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO6bl2_combo.SelectedItem.ToString(), DO6bl2_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO6bl2_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO6bl2_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO6bl2_combo.SelectedItem = NOT_SELECTED;
                        DO6bl2_lab.Text = "";
                    }
                    DO6bl2combo_text = DO6bl2_combo.SelectedItem.ToString();
                    DO6bl2combo_index = DO6bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < DO7bl2_combo.Items.Count; i++) // DO7 блок 2
                if (DO7bl2_combo.Items[i].ToString() == name)
                {
                    DO7bl2_combo.Items.Remove(name);
                    if (DO7bl2_combo.Items.Count > 1)
                    {
                        DO7bl2_combo.SelectedIndex = DO7bl2_combo.Items.Count - 1;
                        if (DO7bl2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO7bl2_combo.SelectedItem.ToString(), DO7bl2_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO7bl2_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO7bl2_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO7bl2_combo.SelectedItem = NOT_SELECTED;
                        DO7bl2_lab.Text = "";   
                    }
                    DO7bl2combo_text = DO7bl2_combo.SelectedItem.ToString();
                    DO7bl2combo_index = DO7bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < DO1bl3_combo.Items.Count; i++) // DO1 блок 3
                if (DO1bl3_combo.Items[i].ToString() == name)
                {
                    DO1bl3_combo.Items.Remove(name);
                    if (DO1bl3_combo.Items.Count > 1)
                    {
                        DO1bl3_combo.SelectedIndex = DO1bl3_combo.Items.Count - 1;
                        if (DO1bl3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO1bl3_combo.SelectedItem.ToString(), DO1bl3_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO1bl3_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO1bl3_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO1bl3_combo.SelectedItem = NOT_SELECTED;
                        DO1bl3_lab.Text = "";
                    }
                    DO1bl3combo_text = DO1bl3_combo.SelectedItem.ToString();
                    DO1bl3combo_index = DO1bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < DO2bl3_combo.Items.Count; i++) // DO2 блок 3
                if (DO2bl3_combo.Items[i].ToString() == name)
                {
                    DO2bl3_combo.Items.Remove(name);
                    if (DO2bl3_combo.Items.Count > 1)
                    {
                        DO2bl3_combo.SelectedIndex = DO2bl3_combo.Items.Count - 1;
                        if (DO2bl3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO2bl3_combo.SelectedItem.ToString(), DO2bl3_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO2bl3_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO2bl3_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO2bl3_combo.SelectedItem = NOT_SELECTED;
                        DO2bl3_lab.Text = "";
                    }
                    DO2bl3combo_text = DO2bl3_combo.SelectedItem.ToString();
                    DO2bl3combo_index = DO2bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < DO3bl3_combo.Items.Count; i++) // DO3 блок 3
                if (DO3bl3_combo.Items[i].ToString() == name)
                {
                    DO3bl3_combo.Items.Remove(name);
                    if (DO3bl3_combo.Items.Count > 1)
                    {
                        DO3bl3_combo.SelectedIndex = DO3bl3_combo.Items.Count - 1;
                        if (DO3bl3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO3bl3_combo.SelectedItem.ToString(), DO3bl3_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO3bl3_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO3bl3_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO3bl3_combo.SelectedItem = NOT_SELECTED;
                        DO3bl3_lab.Text = "";
                    }
                    DO3bl3combo_text = DO3bl3_combo.SelectedItem.ToString();
                    DO3bl3combo_index = DO3bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < DO4bl3_combo.Items.Count; i++) // DO4 блок 3
                if (DO4bl3_combo.Items[i].ToString() == name)
                {
                    DO4bl3_combo.Items.Remove(name);
                    if (DO4bl3_combo.Items.Count > 1)
                    {
                        DO4bl3_combo.SelectedIndex = DO4bl3_combo.Items.Count - 1;
                        if (DO4bl3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO4bl3_combo.SelectedItem.ToString(), DO4bl3_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO4bl3_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO4bl3_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO4bl3_combo.SelectedItem = NOT_SELECTED;
                        DO4bl3_lab.Text = "";
                    }
                    DO4bl3combo_text = DO4bl3_combo.SelectedItem.ToString();
                    DO4bl3combo_index = DO4bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < DO5bl3_combo.Items.Count; i++) // DO5 блок 3
                if (DO5bl3_combo.Items[i].ToString() == name)
                {
                    DO5bl3_combo.Items.Remove(name);
                    if (DO5bl3_combo.Items.Count > 1)
                    {
                        DO5bl3_combo.SelectedIndex = DO5bl3_combo.Items.Count - 1;
                        if (DO5bl3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO5bl3_combo.SelectedItem.ToString(), DO5bl3_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO5bl3_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO5bl3_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO5bl3_combo.SelectedItem = NOT_SELECTED;
                        DO5bl3_lab.Text = "";
                    }
                    DO5bl3combo_text = DO5bl3_combo.SelectedItem.ToString();
                    DO5bl3combo_index = DO5bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < DO6bl3_combo.Items.Count; i++) // DO6 блок 3
                if (DO6bl3_combo.Items[i].ToString() == name)
                {
                    DO6bl3_combo.Items.Remove(name);
                    if (DO6bl3_combo.Items.Count > 1)
                    {
                        DO6bl3_combo.SelectedIndex = DO6bl3_combo.Items.Count - 1;
                        if (DO6bl3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO6bl3_combo.SelectedItem.ToString(), DO6bl3_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO6bl3_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO6bl3_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO6bl3_combo.SelectedItem = NOT_SELECTED;
                        DO6bl3_lab.Text = "";
                    }
                    DO6bl3combo_text = DO6bl3_combo.SelectedItem.ToString();
                    DO6bl3combo_index = DO6bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < DO7bl3_combo.Items.Count; i++) // DO7 блок 3
                if (DO7bl3_combo.Items[i].ToString() == name)
                {
                    DO7bl3_combo.Items.Remove(name);
                    if (DO7bl3_combo.Items.Count > 1)
                    {
                        DO7bl3_combo.SelectedIndex = DO7bl3_combo.Items.Count - 1;
                        if (DO7bl3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosDO(DO7bl3_combo.SelectedItem.ToString(), DO7bl3_combo);
                            findDO_2 = list_do.Find(x => x.Name == DO7bl3_combo.SelectedItem.ToString());
                            if (findDO_2 != null)
                            {
                                list_do.Remove(findDO_2);
                                findDO_2.Select();
                                list_do.Add(findDO_2);
                                if (showCode) DO7bl3_lab.Text = findDO_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DO7bl3_combo.SelectedItem = NOT_SELECTED;
                        DO7bl3_lab.Text = "";
                    }
                    DO7bl3combo_text = DO7bl3_combo.SelectedItem.ToString();
                    DO7bl3combo_index = DO7bl3_combo.SelectedIndex;
                }
            subDOcondition = false; // Сброс признака удаление из DO
            list_do.Remove(findDo); // Удаление сигнала из списка DO
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        /// <summary>Выбрали приточную заслонку</summary>
        private void DampCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort code = 6; // Открытие приточной заслонки
            if (dampCheck.Checked) // Выбрали приточную заслонку
            {
                list_do.Add(new Do("Открытие приточной заслонки", code));
                AddNewDO(code); // Добавление DO к свободному comboBox выхода
            }
            else // Отмена выбора приточной заслонки
            {
                SubFromCombosDO(code); // Удаление DO из comboBox выходов
            }
            HeatPrDampCheck_signalsCheckedChanged(this, e); // Проверка для обогрева заслонки
            OutDampCheck_signalsDOCheckedChanged(this, e); // Проверка для вытяжной воздушной заслонки
        }

        /// <summary>Выбрали обогрев приточной заслонки</summary>
        private void HeatPrDampCheck_signalsCheckedChanged(object sender, EventArgs e)
        {
            ushort code = 7; // Обогрев приточной заслонки
            if (dampCheck.Checked) // Выбрана приточная заслонка
            {
                if (heatPrDampCheck.Checked) // Выбрали обогрев заслонки
                {
                    list_do.Add(new Do("Обогрев приточной заслонки", code));
                    AddNewDO(code); // Добавление DO к свободному comboBox выхода
                }
                else // Отмена выбора обогрева заслонки
                {
                    SubFromCombosDO(code); // Удаление DO из comboBox выходов
                }
            }
            else // Если заслонка не выбрана
            {
                SubFromCombosDO(code); // Удаление DO из comboBox выходов
            }
        }

        ///<summary>Выбрали вытяжную воздушную заслонку</summary>
        private void OutDampCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort code = 20; // Открытие вытяжной заслонки
            if (comboSysType.SelectedIndex == 1 && dampCheck.Checked && outDampCheck.Checked)
            {
                list_do.Add(new Do("Открытие вытяжной заслонки", code));
                AddNewDO(code); // Добавление DO к свободному comboBox выхода
            }
            else // Заслонка не выбрана или П-система
            {
                SubFromCombosDO(code); // Удаление DO из comboBox выходов
            }
            HeatOutDampCheck_signalsCheckedChanged(this, e); // Обогрев вытяжной заслонки
        }

        ///<summary>Выбрали обогрев вытяжной заслонки</summary>
        private void HeatOutDampCheck_signalsCheckedChanged(object sender, EventArgs e)
        {
            ushort code = 21; // Обогрев вытяжной заслонки
            if (comboSysType.SelectedIndex == 1 && dampCheck.Checked && outDampCheck.Checked)
            {
                if (heatOutDampCheck.Checked) // Выбрали обогрев заслонки
                {
                    list_do.Add(new Do("Обогрев вытяжной заслонки", code));
                    AddNewDO(code); // Добавление DO к свободному comboBox выхода
                } 
                else // Отмена выбора обогрева заслонки
                {
                    SubFromCombosDO(code); // Удаление DO из comboBox выходов
                }
            }
            else // Заслонка не выбрана
            {
                SubFromCombosDO(code); // Удаление DO из comboBox выходов
            }
        }

        ///<summary>Выбрали наличие нагревателя</summary>
        private void HeaterCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 34; // Запуск насоса водяного калорифера
            ushort code_2 = 38; // Первая ступень электрокалорифера
            ushort code_3 = 39; // Вторая ступень электрокалорифера
            ushort code_4 = 40; // Третья ступень электрокалорифера
            ushort code_5 = 41; // Четвертая ступень электрокалорифера
            ushort code_6 = 42; // Пятая ступень электрокалорифера
            if (heaterCheck.Checked) // Выбрали нагреватель
            {
                if (heatTypeCombo.SelectedIndex == 0) // Водяной калорифер
                {
                    list_do.Add(new Do("Запуск насоса водяного калорифера", code_1));
                    AddNewDO(code_1);
                }
                else if (heatTypeCombo.SelectedIndex == 1) // Электрокалорифер
                {
                    list_do.Add(new Do("1 ступень электрического калорифера", code_2));
                    AddNewDO(code_2);
                    switch (elHeatStagesCombo.SelectedIndex) // Выборка количества ступеней
                    {
                        case 0: break; // Одна ступень нагрева
                        case 1: // Две ступени нагрева
                            list_do.Add(new Do("2 ступень электрического калорифера", code_3));
                            AddNewDO(code_3); break;
                        case 2: // Три ступени нагрева
                            list_do.Add(new Do("2 ступень электрического калорифера", code_3));
                            AddNewDO(code_3);
                            list_do.Add(new Do("3 ступень электрического калорифера", code_4));
                            AddNewDO(code_4); break;
                        case 3: // Четыре ступени нагрева
                            list_do.Add(new Do("2 ступень электрического калорифера", code_3));
                            AddNewDO(code_3);
                            list_do.Add(new Do("3 ступень электрического калорифера", code_4));
                            AddNewDO(code_4);
                            list_do.Add(new Do("4 ступень электрического калорифера", code_5));
                            AddNewDO(code_5); break;
                        case 4: // Пять ступеней нагрева
                            list_do.Add(new Do("2 ступень электрического калорифера", code_3));
                            AddNewDO(code_3);
                            list_do.Add(new Do("3 ступень электрического калорифера", code_4));
                            AddNewDO(code_4);
                            list_do.Add(new Do("4 ступень электрического калорифера", code_5));
                            AddNewDO(code_5);
                            list_do.Add(new Do("5 ступень электрического калорифера", code_6));
                            AddNewDO(code_6); break;
                    }
                }
            }
            else // Отмена выбора нагревателя
            {
                SubFromCombosDO(code_1);
                SubFromCombosDO(code_2);
                SubFromCombosDO(code_3);
                SubFromCombosDO(code_4);
                SubFromCombosDO(code_5);
                SubFromCombosDO(code_6);
            }
        }

        ///<summary>Изменили тип основного нагревателя</summary>
        private void HeatTypeCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 34; // Запуск насоса водяного калорифера
            ushort code_2 = 38; // Первая ступень электрокалорифера
            ushort code_3 = 39; // Вторая ступень электрокалорифера
            ushort code_4 = 40; // Третья ступень электрокалорифера
            ushort code_5 = 41; // Четвертая ступень электрокалорифера
            ushort code_6 = 42; // Пятая ступень электрокалорифера
            if (heaterCheck.Checked) // Когда выбран нагреватель
            {
                if (heatTypeCombo.SelectedIndex == 0) // Водяной калорифер
                {
                    SubFromCombosDO(code_2);
                    SubFromCombosDO(code_3);
                    SubFromCombosDO(code_4);
                    SubFromCombosDO(code_5);
                    SubFromCombosDO(code_6);
                    list_do.Add(new Do("Запуск насоса водяного калорифера", code_1));
                    AddNewDO(code_1);
                }
                else if (heatTypeCombo.SelectedIndex == 1) // Электрокалорифер
                {
                    SubFromCombosDO(code_1); // Удаление запуска насоса
                    list_do.Add(new Do("1 ступень электрического калорифера", code_2));
                    AddNewDO(code_2); // Первая ступень нагрева
                    switch (elHeatStagesCombo.SelectedIndex) // Выборка ступеней нагрева
                    {
                        case 0: break; // Одна ступень нагрева
                        case 1: // Две ступени нагрева
                            list_do.Add(new Do("2 ступень электрического калорифера", code_3));
                            AddNewDO(code_3); break;
                        case 2: // Три ступени нагрева
                            list_do.Add(new Do("2 ступень электрического калорифера", code_3));
                            AddNewDO(code_3);
                            list_do.Add(new Do("3 ступень электрического калорифера", code_4));
                            AddNewDO(code_4); break;
                        case 3: // Четыре ступени нагрева
                            list_do.Add(new Do("2 ступень электрического калорифера", code_3));
                            AddNewDO(code_3);
                            list_do.Add(new Do("3 ступень электрического калорифера", code_4));
                            AddNewDO(code_4);
                            list_do.Add(new Do("4 ступень электрического калорифера", code_5));
                            AddNewDO(code_5); break;
                        case 4: // Пять ступеней нагрева
                            list_do.Add(new Do("2 ступень электрического калорифера", code_3));
                            AddNewDO(code_3);
                            list_do.Add(new Do("3 ступень электрического калорифера", code_4));
                            AddNewDO(code_4);
                            list_do.Add(new Do("4 ступень электрического калорифера", code_5));
                            AddNewDO(code_5);
                            list_do.Add(new Do("5 ступень электрического калорифера", code_6));
                            AddNewDO(code_6); break;
                    }
                }
            }
        }

        ///<summary>Изменили количество ступеней основного электрокалорифера</summary>
        private void ElHeatStagesCombo_signalsSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 39; // Вторая ступень электрокалорифера
            ushort code_2 = 40; // Третья ступень электрокалорифера
            ushort code_3 = 41; // Четвертая ступень электрокалорифера
            ushort code_4 = 42; // Пятая ступень электрокалорифера
            Do find_do;
            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 1)
            { // Выбран электрокалорифер
                switch (elHeatStagesCombo.SelectedIndex) // Выборка ступеней электрокалорифера
                {
                    case 0: // Одна ступень нагрева
                        SubFromCombosDO(code_4); SubFromCombosDO(code_3); 
                        SubFromCombosDO(code_2); SubFromCombosDO(code_1); break;
                    case 1: // Две ступени нагрева
                        SubFromCombosDO(code_4); SubFromCombosDO(code_3); SubFromCombosDO(code_2);
                        find_do = list_do.Find(x => x.Code == code_1);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("2 ступень электрического калорифера", code_1));
                            AddNewDO(code_1); 
                        }
                        break;
                    case 2: // Три ступени нагрева
                        SubFromCombosDO(code_4); SubFromCombosDO(code_3);
                        find_do = list_do.Find(x => x.Code == code_1);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("2 ступень электрического калорифера", code_1));
                            AddNewDO(code_1); 
                        }
                        find_do = list_do.Find(x => x.Code == code_2);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("3 ступень электрического калорифера", code_2));
                            AddNewDO(code_2); 
                        }
                        break;
                    case 3: // Четыре ступени нагрева
                        SubFromCombosDO(code_4);
                        find_do = list_do.Find(x => x.Code == code_1);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("2 ступень электрического калорифера", code_1));
                            AddNewDO(code_1); 
                        }
                        find_do = list_do.Find(x => x.Code == code_2);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("3 ступень электрического калорифера", code_2));
                            AddNewDO(code_2); 
                        }
                        find_do = list_do.Find(x => x.Code == code_3);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("4 ступень электрического калорифера", code_3));
                            AddNewDO(code_3); 
                        }
                        break;
                    case 4: // Пять ступеней нагрева
                        find_do = list_do.Find(x => x.Code == code_1);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("2 ступень электрического калорифера", code_1));
                            AddNewDO(code_1); 
                        }
                        find_do = list_do.Find(x => x.Code == code_2);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("3 ступень электрического калорифера", code_2));
                            AddNewDO(code_2); 
                        }
                        find_do = list_do.Find(x => x.Code == code_3);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("4 ступень электрического калорифера", code_3));
                            AddNewDO(code_3); 
                        }
                        find_do = list_do.Find(x => x.Code == code_4);
                        if (find_do == null)
                        {
                            list_do.Add(new Do("5 ступень электрического калорифера", code_4));
                            AddNewDO(code_4); 
                        }
                        break;
                }
            }
        }

        ///<summary>Выбрали дополнительный нагреватель</summary>
        private void AddHeatCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 78; // Запуск насоса водяного догревателя
            ushort code_2 = 73; // Первая ступень электрического догревателя
            ushort code_3 = 74; // Вторая ступень электрического догревателя
            ushort code_4 = 75; // Третья ступень электрического догревателя
            ushort code_5 = 76; // Четвертая ступень электрического догревателя
            ushort code_6 = 77; // Пятая ступень электрического догревателя
            if (addHeatCheck.Checked) // Когда выбран второй нагреватель
            {
                if (heatAddTypeCombo.SelectedIndex == 0) // Водяной догреватель 
                {
                    list_do.Add(new Do("Запуск насоса водяного догревателя", code_1));
                    AddNewDO(code_1);
                }
                else if (heatAddTypeCombo.SelectedIndex == 1) // Электрический догреватель
                {
                    list_do.Add(new Do("1 ступень электрического догревателя", code_2));
                    AddNewDO(code_2);
                    switch (elHeatAddStagesCombo.SelectedIndex) // Выборка ступеней
                    {
                        case 0: break; // Одна ступень нагрева
                        case 1: // Две ступени нагрева
                            list_do.Add(new Do("2 ступень электрического догревателя", code_3));
                            AddNewDO(code_3); break;
                        case 2: // Три ступени нагрева
                            list_do.Add(new Do("2 ступень электрического догревателя", code_3));
                            AddNewDO(code_3);
                            list_do.Add(new Do("3 ступень электрического догревателя", code_4));
                            AddNewDO(code_4); break;
                        case 3: // Четыре ступени нагрева
                            list_do.Add(new Do("2 ступень электрического догревателя", code_3));
                            AddNewDO(code_3);
                            list_do.Add(new Do("3 ступень электрического догревателя", code_4));
                            AddNewDO(code_4);
                            list_do.Add(new Do("4 ступень электрического догревателя", code_5));
                            AddNewDO(code_5); break;
                        case 4: // Пять ступеней нагрева
                            list_do.Add(new Do("2 ступень электрического догревателя", code_3));
                            AddNewDO(code_3);
                            list_do.Add(new Do("3 ступень электрического догревателя", code_4));
                            AddNewDO(code_4);
                            list_do.Add(new Do("4 ступень электрического догревателя", code_5));
                            AddNewDO(code_5);
                            list_do.Add(new Do("5 ступень электрического догревателя", code_6));
                            AddNewDO(code_6); break;
                    }
                }
            }
            else // Отмена выбора догревателя
            {
                SubFromCombosDO(code_1);
                SubFromCombosDO(code_2);
                SubFromCombosDO(code_3);
                SubFromCombosDO(code_4);
                SubFromCombosDO(code_5);
                SubFromCombosDO(code_6);
            }
        }

        ///<summary>Изменили тип второго нагревателя</summary>
        private void HeatAddTypeCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 78; // Запуск насоса водяного догревателя
            ushort code_2 = 73; // Первая ступень электрического догревателя
            ushort code_3 = 74; // Вторая ступень электрического догревателя
            ushort code_4 = 75; // Третья ступень электрического догревателя
            ushort code_5 = 76; // Четвертая ступень электрического догревателя
            ushort code_6 = 77; // Пятая ступень электрического догревателя
            if (addHeatCheck.Checked) // Когда выбран второй нагреватель
            {
                if (heatAddTypeCombo.SelectedIndex == 0) // Водяной догреватель
                {
                    SubFromCombosDO(code_2);
                    SubFromCombosDO(code_3);
                    SubFromCombosDO(code_4);
                    SubFromCombosDO(code_5);
                    SubFromCombosDO(code_6);
                    list_do.Add(new Do("Запуск насоса водяного догревателя", code_1));
                    AddNewDO(code_1);
                }
                else if (heatAddTypeCombo.SelectedIndex == 1) // Электрический догреватель
                {
                    SubFromCombosDO(code_1); // Удаление запуска насоса
                    list_do.Add(new Do("1 ступень электрического догревателя", code_2));
                    AddNewDO(code_2);
                    switch (elHeatAddStagesCombo.SelectedIndex) // Выборка количества ступеней
                    {
                        case 0: break; // Одна ступень нагрева
                        case 1: // Две ступени нагрева
                            list_do.Add(new Do("2 ступень электрического догревателя", code_3));
                            AddNewDO(code_3); break;
                        case 2: // Три ступени нагрева
                            list_do.Add(new Do("2 ступень электрического догревателя", code_3));
                            AddNewDO(code_3);
                            list_do.Add(new Do("3 ступень электрического догревателя", code_4));
                            AddNewDO(code_4); break;
                        case 3: // Четыре ступени нагрева
                            list_do.Add(new Do("2 ступень электрического догревателя", code_3));
                            AddNewDO(code_3);
                            list_do.Add(new Do("3 ступень электрического догревателя", code_4));
                            AddNewDO(code_4);
                            list_do.Add(new Do("4 ступень электрического догревателя", code_5));
                            AddNewDO(code_5); break;
                        case 4: // Пять ступеней нагрева
                            list_do.Add(new Do("2 ступень электрического догревателя", code_3));
                            AddNewDO(code_3);
                            list_do.Add(new Do("3 ступень электрического догревателя", code_4));
                            AddNewDO(code_4);
                            list_do.Add(new Do("4 ступень электрического догревателя", code_5));
                            AddNewDO(code_5);
                            list_do.Add(new Do("5 ступень электрического догревателя", code_6));
                            AddNewDO(code_6); break;
                    }
                }
            }
        }

        ///<summary>Изменили количество ступеней электрического догревателя</summary>
        private void ElHeatAddStagesCombo_signalsSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 74; // Вторая ступень электродогревателя
            ushort code_2 = 75; // Третья ступень электродогревателя
            ushort code_3 = 76; // Четвертая ступень электродогревателя
            ushort code_4 = 77; // Пятая ступень электродогревателя
            Do find_do;
            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 1)
            { // Выбран электрический догреватель
                switch (elHeatAddStagesCombo.SelectedIndex) // Выборка ступеней
                {
                    case 0: // Одна ступень нагрева
                        SubFromCombosDO(code_4); SubFromCombosDO(code_3);
                        SubFromCombosDO(code_2); SubFromCombosDO(code_1); break;
                    case 1: // Две ступени нагрева
                        SubFromCombosDO(code_4); SubFromCombosDO(code_3); SubFromCombosDO(code_2);
                        find_do = list_do.Find(x => x.Code == code_1);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("2 ступень электрического догревателя", code_1));
                            AddNewDO(code_1);
                        }
                        break;
                    case 2: // Три ступени нагрева
                        SubFromCombosDO(code_4); SubFromCombosDO(code_3);
                        find_do = list_do.Find(x => x.Code == code_1);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("2 ступень электрического догревателя", code_1));
                            AddNewDO(code_1);
                        }
                        find_do = list_do.Find(x => x.Code == code_2);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("3 ступень электрического догревателя", code_2));
                            AddNewDO(code_2);
                        }
                        break;
                    case 3: // Четыре ступени нагрева
                        SubFromCombosDO(code_4);
                        find_do = list_do.Find(x => x.Code == code_1);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("2 ступень электрического догревателя", code_1));
                            AddNewDO(code_1);
                        }
                        find_do = list_do.Find(x => x.Code == code_2);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("3 ступень электрического догревателя", code_2));
                            AddNewDO(code_2);
                        }
                        find_do = list_do.Find(x => x.Code == code_3);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("4 ступень электрического догревателя", code_3));
                            AddNewDO(code_3);
                        }
                        break;
                    case 4: // Пять ступеней нагрева
                        find_do = list_do.Find(x => x.Code == code_1);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("2 ступень электрического догревателя", code_1));
                            AddNewDO(code_1);
                        }
                        find_do = list_do.Find(x => x.Code == code_2);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("3 ступень электрического догревателя", code_2));
                            AddNewDO(code_2);
                        }
                        find_do = list_do.Find(x => x.Code == code_3);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("4 ступень электрического догревателя", code_3));
                            AddNewDO(code_3);
                        }
                        find_do = list_do.Find(x => x.Code == code_4);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("5 ступень электрического догревателя", code_4));
                            AddNewDO(code_4);
                        }
                        break;
                }
            }
        }

        ///<summary>Выбрали охладитель</summary>
        private void CoolerCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 48; // Первая ступень фреонового охладителя
            ushort code_2 = 49; // Вторая ступень фреонового охладителя
            ushort code_3 = 50; // Третья ступень фреонового охладителя
            ushort code_4 = 51; // Четвертая ступень фреонового охладителя
            if (coolerCheck.Checked) // Выбрали охладитель
            {
                if (coolTypeCombo.SelectedIndex == 0) // Фреоновый 
                {
                    list_do.Add(new Do("1 ступень фреонового охладителя", code_1));
                    AddNewDO(code_1);
                    switch (frCoolStagesCombo.SelectedIndex) // Выборка количества ступеней
                    {
                        case 0: break; // Одна ступень
                        case 1: // Две ступени
                            list_do.Add(new Do("2 ступень фреонового охладителя", code_2));
                            AddNewDO(code_2); break;
                        case 2: // Три ступени
                            list_do.Add(new Do("2 ступень фреонового охладителя", code_2));
                            AddNewDO(code_2);
                            list_do.Add(new Do("3 ступень фреонового охладителя", code_3));
                            AddNewDO(code_3); break;
                        case 3: // Четыре ступени
                            list_do.Add(new Do("2 ступень фреонового охладителя", code_2));
                            AddNewDO(code_2);
                            list_do.Add(new Do("3 ступень фреонового охладителя", code_3));
                            AddNewDO(code_3);
                            list_do.Add(new Do("4 ступень фреонового охладителя", code_4));
                            AddNewDO(code_4); break;
                    }
                }
            } 
            else // Отмена выбора охладителя
            {
                SubFromCombosDO(code_4); SubFromCombosDO(code_3);
                SubFromCombosDO(code_2); SubFromCombosDO(code_1);
            }
        }

        ///<summary>Изменили тип охладителя</summary>
        private void CoolTypeCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 48; // Первая ступень фреонового охладителя
            ushort code_2 = 49; // Вторая ступень фреонового охладителя
            ushort code_3 = 50; // Третья ступень фреонового охладителя
            ushort code_4 = 51; // Четвертая ступень фреонового охладителя
            if (coolerCheck.Checked) // Когда выбран охладитель
            {
                if (coolTypeCombo.SelectedIndex == 0) // Фреоновый охладитель
                {
                    list_do.Add(new Do("1 ступень фреонового охладителя", code_1));
                    AddNewDO(code_1);
                    switch (frCoolStagesCombo.SelectedIndex) // Выборка количества ступеней
                    {
                        case 0: break; // Одна ступень
                        case 1: // Две ступени
                            list_do.Add(new Do("2 ступень фреонового охладителя", code_2));
                            AddNewDO(code_2); break;
                        case 2: // Три ступени
                            list_do.Add(new Do("2 ступень фреонового охладителя", code_2));
                            AddNewDO(code_2);
                            list_do.Add(new Do("3 ступень фреонового охладителя", code_3));
                            AddNewDO(code_3); break;
                        case 3: // Четыре ступени
                            list_do.Add(new Do("2 ступень фреонового охладителя", code_2));
                            AddNewDO(code_2);
                            list_do.Add(new Do("3 ступень фреонового охладителя", code_3));
                            AddNewDO(code_3);
                            list_do.Add(new Do("4 ступень фреонового охладителя", code_4));
                            AddNewDO(code_4); break;
                    }
                }
                else if (coolTypeCombo.SelectedIndex == 1) // Водяной охладитель
                {
                    SubFromCombosDO(code_4); SubFromCombosDO(code_3);
                    SubFromCombosDO(code_2); SubFromCombosDO(code_1);
                }
            }
        }

        ///<summary>Изменили количество ступеней фреонового охладителя</summary>
        private void FrCoolStagesCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 49; // Вторая ступень фреонового охладителя
            ushort code_2 = 50; // Третья ступень фреонового охладителя
            ushort code_3 = 51; // Четвертая ступень фреонового охладителя
            Do find_do;
            if (coolerCheck.Checked && coolTypeCombo.SelectedIndex == 0)
            { // Выбран фреоновых охладитель
                switch (frCoolStagesCombo.SelectedIndex) // Выборка ступеней охладителя
                {
                    case 0: // Одна ступень охладителя
                        SubFromCombosDO(code_3); SubFromCombosDO(code_2); SubFromCombosDO(code_1); break;
                    case 1: // Две ступени охладителя
                        SubFromCombosDO(code_3); SubFromCombosDO(code_2);
                        find_do = list_do.Find(x => x.Code == code_1);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("2 ступень фреонового охладителя", code_1));
                            AddNewDO(code_1);
                        }
                        break;
                    case 2: // Три ступени охладителя
                        SubFromCombosDO(code_3);
                        find_do = list_do.Find(x => x.Code == code_1);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("2 ступень фреонового охладителя", code_1));
                            AddNewDO(code_1);
                        }
                        find_do = list_do.Find(x => x.Code == code_2);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("3 ступень фреонового охладителя", code_2));
                            AddNewDO(code_2);
                        }
                        break;
                    case 3: // Четыре ступени охладителя
                        find_do = list_do.Find(x => x.Code == code_1);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("2 ступень фреонового охладителя", code_1));
                            AddNewDO(code_1);
                        }
                        find_do = list_do.Find(x => x.Code == code_2);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("3 ступень фреонового охладителя", code_2));
                            AddNewDO(code_2);
                        }
                        find_do = list_do.Find(x => x.Code == code_3);
                        if (find_do == null) // Нет такой записи
                        {
                            list_do.Add(new Do("4 ступень фреонового охладителя", code_3));
                            AddNewDO(code_3);
                        }
                        break;
                }
            }
        }

        ///<summary>Изменили тип системы</summary>
        private void ComboSysType_signalsSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 23; // Сигнал "Пуск/Стоп" вытяжного вентилятора 1
            ushort code_2 = 29; // Сигнал "Пуск/Стоп" вытяжного вентилятора 2
            if (comboSysType.SelectedIndex == 1) // Выбрана ПВ-система
            {
                list_do.Add(new Do("Сигнал \"Пуск/Стоп\" вытяжного вентилятора 1", code_1));
                AddNewDO(code_1);
                if (checkResOutFan.Checked) // Выбран резерв вытяжного вентилятора
                {
                    list_do.Add(new Do("Сигнал \"Пуск/Стоп\" вытяжного вентилятора 2", code_2));
                    AddNewDO(code_2);
                }
            } 
            else if (comboSysType.SelectedIndex == 0) // Выбрана П-система
            {
                SubFromCombosDO(code_2);
                SubFromCombosDO(code_1);
            }
        }
        
        ///<summary>Выбрали резерв приточного вентилятора</summary>
        private void CheckResPrFan_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 15; // Сигнал "Пуск/Стоп" приточного вентилятора 2
            ushort code_2 = 14; // Сигнал подачи питания приточного вентилятора 2
            if (checkResPrFan.Checked) // Выбрали резервирование
            {
                if (prFanStStopCheck.Checked) // Выбран сигнал "Пуск/Стоп"
                {
                    list_do.Add(new Do("Сигнал \"Пуск/Стоп\" приточного вентилятора 2", code_1));
                    AddNewDO(code_1);
                }
                if (prFanPowSupCheck.Checked) // Выбран сигнал подачи питания
                {
                    list_do.Add(new Do("Подача питания приточного вентилятора 2", code_2));
                    AddNewDO(code_2);
                }
            }
            else // Отмена выбора резервирования
            {
                SubFromCombosDO(code_1); // Отмена выбора сигнала "Пуск/Стоп"
                SubFromCombosDO(code_2); // Отмена выбора подачи питания
            }
        }

        ///<summary>Выбрали резерв вытяжного вентилятора</summary>
        private void CheckResOutFan_signalsCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 29; // Сигнал "Пуск/Стоп" вытяжного вентилятора 2
            ushort code_2 = 28; // Сигнал подачи питания вытяжного вентилятора 2 
            if (comboSysType.SelectedIndex == 1 && checkResOutFan.Checked) // ПВ-система и резерв вытяжного
            { // Выбрана ПВ-система и резерв вытяжного вентилятора
                if (outFanStStopCheck.Checked) // Выбран сигнал "Пуск/Стоп"
                {
                    list_do.Add(new Do("Сигнал \"Пуск/Стоп\" вытяжного вентилятора 2", code_1));
                    AddNewDO(code_1);
                }
                if (outFanPowSupCheck.Checked) // Выбран сигнал подачи питания
                {
                    list_do.Add(new Do("Подача питания вытяжного вентилятора 2", code_2));
                    AddNewDO(code_2);
                }
            }
            else // Не выбран резерв вытяжного вентилятора
            {
                SubFromCombosDO(code_1); // Отмена выбора сигнала "Пуск/Стоп"
                SubFromCombosDO(code_2); // Отмена выбора подачи питания
            }
        }

        ///<summary>Выбрали увлажнитель</summary>
        private void HumidCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 80;
            if (humidCheck.Checked) // Выбрали увлажнитель
            {
                if (humidTypeCombo.SelectedIndex == 0) // Паровой увлажнитель
                {
                    list_do.Add(new Do("Сигнал ПУСК/СТОП увлажнителя", code_1));
                    AddNewDO(code_1);
                }
                else if (humidTypeCombo.SelectedIndex == 1) // Сотовый увлажнитель
                {
                    list_do.Add(new Do("Запуск насоса увлажнителя", code_1));
                    AddNewDO(code_1);
                }
            }
            else // Отмена выбора увлажнителя
            {
                SubFromCombosDO(code_1);
            }
        }

        ///<summary>Изменили тип увлажнителя</summary>
        private void HumidTypeCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 80;
            if (humidCheck.Checked) // Когда выбран увлажнитель
            {
                if (humidTypeCombo.SelectedIndex == 0) // Паровой увлажнитель
                {
                    SubFromCombosDO(code_1);
                    list_do.Add(new Do("Сигнал ПУСК/СТОП увлажнителя", code_1));
                    AddNewDO(code_1);

                }
                else if (humidTypeCombo.SelectedIndex == 1) // Сотовый увлажнитель
                {
                    SubFromCombosDO(code_1);
                    list_do.Add(new Do("Запуск насоса увлажнителя", code_1));
                    AddNewDO(code_1);
                }
            }
        }

        ///<summary>Выбрали рекуператор</summary>
        private void RecupCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 56; // Сигнал ПУСК/СТОП рекуператора (роторный)
            ushort code_2 = 57; // Запуск насоса гликолевого рекуператора
            if (recupCheck.Checked && comboSysType.SelectedIndex == 1) // Выбран рекуператор, ПВ
            {
                if (recupTypeCombo.SelectedIndex == 0) // Роторный рекуператор
                {
                    list_do.Add(new Do("Сигнал ПУСК/СТОП рекуператора", code_1));
                    AddNewDO(code_1);
                }
                else if (recupTypeCombo.SelectedIndex == 2) // Гликолевый рекуператор
                {
                    if (pumpGlicRecCheck.Checked) // Выбран насос рекуператора
                    {
                        list_do.Add(new Do("Запуск насоса гликолевого рекуператора", code_2));
                        AddNewDO(code_2);
                    }
                }
            }
            else // Отмена выбора рекуператора
            {
                SubFromCombosDO(code_2);
                SubFromCombosDO(code_1);
            }
        }

        ///<summary>Изменили тип рекуператора</summary>
        private void RecupTypeCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 56; // Сигнал ПУСК/СТОП рекуператора (роторный)
            ushort code_2 = 57; // Запуск насоса гликолевого рекуператора
            if (recupCheck.Checked && comboSysType.SelectedIndex == 1) // Выбран рекуператор и ПВ-система
            {
                if (recupTypeCombo.SelectedIndex == 0) // Роторный рекуператор
                {
                    SubFromCombosDO(code_2);
                    list_do.Add(new Do("Сигнал ПУСК/СТОП рекуператора", code_1));
                    AddNewDO(code_1);
                }
                else if (recupTypeCombo.SelectedIndex == 2) // Гликолевый рекуператор
                {
                    SubFromCombosDO(code_1);
                    if (pumpGlicRecCheck.Checked) // Выбран насос рекуператора
                    {
                        list_do.Add(new Do("Запуск насоса гликолевого рекуператора", code_2));
                        AddNewDO(code_2);
                    }
                }
                else if (recupTypeCombo.SelectedIndex == 1)
                {
                    SubFromCombosDO(code_1); // Сигнал ПУСК/СТОП роторного рекуператора
                    SubFromCombosDO(code_2); // Запуск насоса гликолевого
                }
            }
        }

        ///<summary>Выбрали насос гликолевого рекуператора</summary>
        private void PumpGlicRecCheck_signalsCheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            ushort code_1 = 57; // Запуск насоса гликолевого рекуператора
            if (recupCheck.Checked && comboSysType.SelectedIndex == 1 && recupTypeCombo.SelectedIndex == 2)
            { // ПВ-система и гликолевый рекуператор
                if (pumpGlicRecCheck.Checked) // Выбран насос гликолевого рекуператора
                {
                    list_do.Add(new Do("Запуск насоса гликолевого рекуператора", code_1));
                    AddNewDO(code_1);
                }
                else // Отмена выбора насоса гликолевого рекуператора
                {
                    SubFromCombosDO(code_1);
                }
            }
        }

        ///<summary>Выбрали внешний сигнал "Работа"</summary>
        private void SigWorkCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            ushort code_1 = 1; // Сигнал "Работа"
            if (sigWorkCheck.Checked) // Выбрали сигнал
            {
                list_do.Add(new Do("Сигнал \"Работа\"", code_1));
                AddNewDO(code_1);
            }
            else // Отмена выбора сигнала
            {
                SubFromCombosDO(code_1);
            }
        }

        ///<summary>Выбрали внешний сигнал "Авария"</summary>
        private void SigAlarmCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            ushort code_1 = 3; // Сигнал "Авария"
            if (sigAlarmCheck.Checked) // Выбрали сигнал
            {
                list_do.Add(new Do("Сигнал \"Авария\"", code_1));
                AddNewDO(code_1);
            }
            else // Отмена выбора сигнала
            {
                SubFromCombosDO(code_1);
            }
        }

        ///<summary>Выбрали внешний сигнал "Загрязнение фильтра"</summary>
        private void SigFilAlarmCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            ushort code_1 = 2; // Сигнал "Загрязнение фильтра"
            if (sigFilAlarmCheck.Checked && filterCheck.Checked) // Выбрали сигнал и выбран фильтр
            {
                list_do.Add(new Do("Сигнал \"Загрязнение фильтра\"", code_1));
                AddNewDO(code_1);
            }
            else // Отмена выбора сигнала/выбора фильтра
            {
                SubFromCombosDO(code_1);
            }
        }

        ///<summary>Выбрали подачу питания для приточного вентилятора</summary>
        private void PrFanPowSupCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            ushort code_1 = 8; // Сигнал "Подача питания" 1
            ushort code_2 = 14; // Сигнал "Подача питания" 2
            if (prFanPowSupCheck.Checked) // Выбрали сигнал подачи питания
            {
                list_do.Add(new Do("Подача питания приточного вентилятора 1", code_1));
                AddNewDO(code_1);
                if (checkResPrFan.Checked) // Если выбран резерв П
                {
                    list_do.Add(new Do("Подача питания приточного вентилятора 2", code_2));
                    AddNewDO(code_2);
                }
            }
            else // Отмена выбора сигнала подачи питания
            {
                SubFromCombosDO(code_1); 
                if (checkResPrFan.Checked) SubFromCombosDO(code_2);
            }    
        }

        ///<summary>Выбрали подачу питания для вытяжного вентилятора</summary>
        private void OutFanPowSupCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            ushort code_1 = 22; // Сигнал "Подача питания" 1
            ushort code_2 = 28; // Сигнал "Подача питания" 2
            if (outFanPowSupCheck.Checked) // Выбрали сигнал подачи питания
            {
                list_do.Add(new Do("Подача питания вытяжного вентилятора 1", code_1));
                AddNewDO(code_1);
                if (checkResOutFan.Checked) // Если выбран резерв В
                {
                    list_do.Add(new Do("Подача питания вытяжного вентилятора 2", code_2));
                    AddNewDO(code_2);
                }
            }
            else // Отмена выбора сигнала подачи питания
            {
                SubFromCombosDO(code_1); 
                if (checkResOutFan.Checked) SubFromCombosDO(code_2);
            }
        }

        ///<summary>Выбрали сигнал "Пуск/Стоп" для приточного вентилятора</summary>
        private void PrFanStStopCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            ushort code_1 = 9; // Сигнал "Пуск/Стоп" 1
            ushort code_2 = 15; // Сигнал "Пуск/Стоп" 2
            if (prFanStStopCheck.Checked) // Выбрали сигнал "Пуск/Стоп"
            {
                list_do.Add(new Do("Сигнал \"Пуск/Стоп\" приточного вентилятора 1", code_1));
                AddNewDO(code_1);
                if (checkResPrFan.Checked) // Если выбран резерв П
                {
                    list_do.Add(new Do("Сигнал \"Пуск/Стоп\" приточного вентилятора 2", code_2));
                    AddNewDO(code_2);
                }
            }
            else // Отмена выбора сигнала "Пуск/Стоп"
            {
                SubFromCombosDO(code_1); 
                if (checkResPrFan.Checked) SubFromCombosDO(code_2);
            }
        }

        ///<summary>Выбрали сигнал "Пуск/Стоп" для вытяжного вентилятора</summary>
        private void OutFanStStopCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            ushort code_1 = 23; // Сигнал "Пуск/Стоп" 1
            ushort code_2 = 29; // Сигнал "Пуск/Стоп" 2
            if (outFanStStopCheck.Checked) // Выбрали сигнал "Пуск/Стоп"
            {
                list_do.Add(new Do("Сигнал \"Пуск/Стоп\" вытяжного вентилятора 1", code_1));
                AddNewDO(code_1);
                if (checkResOutFan.Checked) // Если выбран резерв В
                {
                    list_do.Add(new Do("Сигнал \"Пуск/Стоп\" вытяжного вентилятора 2", code_2));
                    AddNewDO(code_2);
                }
            }
            else // Отмена выбора сигнала "Пуск/Стоп"
            {
                SubFromCombosDO(code_1); 
                if (checkResOutFan.Checked) SubFromCombosDO(code_2);
            }
        }

        ///<summary>Выбрали ПЧ приточного вентилятора, сигналы DO</summary>>
        private void PrFanFC_check_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            if (prFanFC_check.Checked) // Выбран ПЧ
            {
                if (prFanControlCombo.SelectedIndex == 1) // Управление по Modbus
                {
                    if (prFanStStopCheck.Checked) // Отмена выбора сигнала "Пуск/Стоп"
                        prFanStStopCheck.Checked = false; 
                }
            }
            else // Отмена выбора ПЧ
            {
                if (!prFanStStopCheck.Checked) // Выбор сигнала "Пуск/Стоп"
                    prFanStStopCheck.Checked = true;
            }
        }

        ///<summary>Изменили тип управления приточного ПЧ</summary>
        private void PrFanControlCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            if (prFanFC_check.Checked) // Когда выбран ПЧ
            {
                if (prFanControlCombo.SelectedIndex == 0) // Внешние контакты
                {
                    if (!prFanStStopCheck.Checked)
                        prFanStStopCheck.Checked = true;
                }
                else if (prFanControlCombo.SelectedIndex == 1) // Modbus
                {
                    if (prFanStStopCheck.Checked)
                        prFanStStopCheck.Checked = false;
                }
            }
        }

        ///<summary>Выбрали ПЧ вытяжного вентилятора</summary>
        private void OutFanFC_check_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            if (comboSysType.SelectedIndex == 1 && outFanFC_check.Checked) // Выбран ПЧ и ПВ-система
            {
                if (outFanControlCombo.SelectedIndex == 1) // Управление по Modbus
                {
                    if (outFanStStopCheck.Checked) // Отмена выбора сигнала "Пуск/Стоп"
                        outFanStStopCheck.Checked = false;
                }
            }
            else if (!outFanFC_check.Checked) // Отмена выбора ПЧ
            {
                if (!outFanStStopCheck.Checked) // Выбор сигнала "Пуск/Стоп"
                    outFanStStopCheck.Checked = true;
            }
        }

        ///<summary>Изменили тип управления ПЧ вытяжного вентилятора</summary>
        private void OutFanControlCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSysType.SelectedIndex == 1 && outFanFC_check.Checked) // Выбран ПЧ и ПВ-система
            {
                if (outFanControlCombo.SelectedIndex == 0) // Внешние контакты
                {
                    if (!outFanStStopCheck.Checked) // Выбор сигнала "Пуск/Стоп"
                        outFanStStopCheck.Checked = true;
                }
                else if (outFanControlCombo.SelectedIndex == 1) // Modbus
                {
                    if (outFanStStopCheck.Checked) // Отмена выбора сигнала "Пуск/Стоп"
                        outFanStStopCheck.Checked = false;
                }
            }
        }

        ///<summary>Выбрали насос дополнительного водяного нагревателя</summary>
        private void PumpAddHeatCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            ushort code_1 = 78; // Сигнал запуска насоса 
            if (addHeatCheck.Checked && pumpAddHeatCheck.Checked) // Выбран второй нагреватель и насос
            {
                list_do.Add(new Do("Запуск насоса водяного догревателя", code_1));
                AddNewDO(code_1);
            }
            else if (!pumpAddHeatCheck.Checked) // Отмена выбора запуска насоса
            {
                SubFromCombosDO(code_1);
            }
        }
    }
}