using System.Windows.Forms;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

// Файл для работы с таблицей сигналов ПЛК и блоков расширения

namespace Moderon
{
    /// <summary>Класс для дискретных выходов</summary>
    class Do
    {
        public string Name { get; private set; }
        public ushort Code { get; private set; }
        public bool Active { get; private set; } = true;        // Свободен к распределению по умолчанию

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
            DO1combo_text, DO2combo_text, DO3combo_text, DO4combo_text, DO5combo_text, DO6combo_text,
            DO1bl1combo_text, DO2bl1combo_text, DO3bl1combo_text, DO4bl1combo_text, DO5bl1combo_text, DO6bl1combo_text, DO7bl1combo_text, DO8bl1combo_text,
            DO1bl2combo_text, DO2bl2combo_text, DO3bl2combo_text, DO4bl2combo_text, DO5bl2combo_text, DO6bl2combo_text, DO7bl2combo_text, DO8bl2combo_text,
            DO1bl3combo_text, DO2bl3combo_text, DO3bl3combo_text, DO4bl3combo_text, DO5bl3combo_text, DO6bl3combo_text, DO7bl3combo_text, DO8bl3combo_text;

        // Сохранение прошлого индекса comboBox элементов для ПЛК и блоков расширения
        int
            DO1combo_index, DO2combo_index, DO3combo_index, DO4combo_index, DO5combo_index, DO6combo_index,
            DO1bl1combo_index, DO2bl1combo_index, DO3bl1combo_index, DO4bl1combo_index, DO5bl1combo_index, DO6bl1combo_index, DO7bl1combo_index, DO8bl1combo_index,
            DO1bl2combo_index, DO2bl2combo_index, DO3bl2combo_index, DO4bl2combo_index, DO5bl2combo_index, DO6bl2combo_index, DO7bl2combo_index, DO8bl2combo_index,
            DO1bl3combo_index, DO2bl3combo_index, DO3bl3combo_index, DO4bl3combo_index, DO5bl3combo_index, DO6bl3combo_index, DO7bl3combo_index, DO8bl3combo_index;

        ///<summary>Начальная установка для сигналов DO таблицы сигналов</summary> 
        public void Set_DOComboTextIndex()
        {
            string workSignal = "Сигнал \"Работа\"";
            DO1combo_text = "Сигнал \"Пуск/Стоп\" приточного вентилятора 1";
            DO2combo_text = workSignal; DO3combo_text = workSignal;

            DO1combo_index = 1; DO2combo_index = 1; DO3combo_index = 1;

            var do_signals = new List<string>()
            {
                DO4combo_text, DO5combo_text, DO6combo_text,
                DO1bl1combo_text, DO2bl1combo_text, DO3bl1combo_text, DO4bl1combo_text, DO5bl1combo_text, DO6bl1combo_text, DO7bl1combo_text, DO8bl1combo_text,
                DO1bl2combo_text, DO2bl2combo_text, DO3bl2combo_text, DO4bl2combo_text, DO5bl2combo_text, DO6bl2combo_text, DO7bl2combo_text, DO8bl2combo_text,
                DO1bl3combo_text, DO2bl3combo_text, DO3bl3combo_text, DO4bl3combo_text, DO5bl3combo_text, DO6bl3combo_text, DO7bl3combo_text, DO8bl3combo_text
            };

            var do_signals_index = new List<int>()
            {
                DO4combo_index, DO5combo_index, DO6combo_index,
                DO1bl1combo_index, DO2bl1combo_index, DO3bl1combo_index, DO4bl1combo_index, DO5bl1combo_index, DO6bl1combo_index, DO7bl1combo_index, DO8bl1combo_index,
                DO1bl2combo_index, DO2bl2combo_index, DO3bl2combo_index, DO4bl2combo_index, DO5bl2combo_index, DO6bl2combo_index, DO7bl2combo_index, DO8bl2combo_index,
                DO1bl3combo_index, DO2bl3combo_index, DO3bl3combo_index, DO4bl3combo_index, DO5bl3combo_index, DO6bl3combo_index, DO7bl3combo_index, DO8bl3combo_index
            };

            for (var i = 0; i < do_signals.Count; i++)
                do_signals[i] = NOT_SELECTED;

            for (var i = 0; i < do_signals_index.Count; i++)
                do_signals_index[i] = 0;
        }

        ///<summary>Нажали на кнопку "Сформировать"</summary> 
        public void FormSignalsButton_Click(object sender, EventArgs e)
        {
            var p1 = new Point(15, 90);                     // Позиция для панели таблицы сигналов
            var p2 = new Point(200, 46);                    // Позиция для comboBox выбора типа контроллера

            mainPage.Hide(); loadModbusPanel.Hide();
            label_comboSysType.Text = "ТАБЛИЦА СИГНАЛОВ";
            comboSysType.Hide(); 
            panelElements.Hide(); panelBlocks.Hide();       // Скрытие панели выбора элементов и блоков расширения
            signalsPanel.Location = p1;
            signalsPanel.Show();
            signalsPanel.Height = 845;
            // Отображение выбора типа контроллера
            comboPlkType.Location = p2;
            comboPlkType.Show();
            formSignalsButton.Hide();                       // Скрытие кнопки
            SignalsTableReSize(Size.Width, Size.Height);    // Таблица сигналов
        }

        ///<summary>Нажали кнопку "Назад" в панели сигналов</summary> 
        private void BackSignalsButton_Click(object sender, EventArgs e)
        {
            signalsPanel.Hide();                            // Скрытие панели сигналов
            comboPlkType.Hide();                            // Скрытие comboBox выбора типа контроллера
            mainPage.Show();
            label_comboSysType.Text = "ТИП СИСТЕМЫ";
            comboSysType.Show();
            panelElements.Show();                           // Отображение панели выбора элементов
            // Отображение панели блоков расширения при наличии
            if (expansion_blocks.Count > 0) panelBlocks.Show();
            formSignalsButton.Show();                       // Отображение кнопки "Сформировать IO"
            // ToolStripMenuItem_load.Enabled = true;       // Разблокировка "Настройка" (опция оключена!)
            fromSignalsMove = false;                        // Сброс признака перехода с панели выбора сигналов
            ToolStripMenuItem_help.Enabled = true;          // Разблокировка "Помощь"
        }

        ///<summary>Сигналы ПЛК при загрузке формы</summary> 
        private void Form1_InitSignals(object sender, EventArgs e)
        {
            SetComboInitial_signals(); // Начальная установка для comboBox

            ushort start_signal_code = 1;       // UI сигнал пуск/стоп
            ushort fanPr_1_start_code = 8;      // DO сигнал пуск/стоп приточного вентилятора 1

            // Добавление начальных DI
            list_ui.Add(new Ui("Переключатель \"Стоп/Пуск\"", start_signal_code, DI));
            // Добавление начальных DO
            list_do.Add(new Do("Сигнал \"Пуск/Стоп\" приточного вентилятора 1", fanPr_1_start_code));
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
            // UI сигналы, сигнал переключатель пуск/стоп
            
            Ui start_stop = list_ui.Find(x => x.Code == start_signal_code);
            string start_stop_name = start_stop.Name; 

            UI1_combo.Items.Add(start_stop_name);                                   // Пуск/Старт
            start_stop.Select();
            UI1_combo.SelectedIndex = 1;                                            // Выбор сигнала
            if (showCode) UI1_lab.Text = (1000 + start_signal_code).ToString();

            // DO сигналы
            Do start_fan = list_do.Find(x => x.Code == fanPr_1_start_code);
            string start_fan_name = start_fan.Name;

            DO1_combo.Items.Add(start_fan_name);                                    // Сигнал "Пуск/Стоп" приточного вентилятора 1
            start_fan.Select();
            DO1_combo.SelectedIndex = 1; // Выбор сигнала
            if (showCode) DO1_lab.Text = fanPr_1_start_code.ToString();

            /*DO2_combo.Items.Add(list_do.Find(x => x.Code == 1).Name);             // Сигнал "Работа"
            DO2_combo.SelectedIndex = 1;
            if (showCode) DO2_lab.Text = 1.ToString();
            list_do.Find(x => x.Code == 1).Select();
            DO3_combo.Items.Add(list_do.Find(x => x.Code == 3).Name);               // Сигнал "Авария"
            DO3_combo.SelectedIndex = 1;
            if (showCode) DO3_lab.Text = 3.ToString();
            list_do.Find(x => x.Code == 3).Select(); */

            initialComboSignals = false; // Сброс признака начальной настройки comboBox
        }

        ///<summary>Очистка массивов сигналов DI, DO, AI, AO</summary>
        private void ResetSignalsLists()
        {
            list_ui.Clear(); list_do.Clear(); list_ao.Clear();
        }

        ///<summary>Нажали на кнопку "Сброс"</summary>
        private void ResetButton_signalsDOClick(object sender, EventArgs e)
        {
            subDOcondition = subAOcondition = false;               // Очистка переменных

            // Очистка DO comboBox ПЛК и блоков расширения
            var do_combos = new List<ComboBox>()
            {
                DO1_combo, DO2_combo, DO3_combo, DO4_combo, DO5_combo, DO6_combo,
                DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo, DO5bl1_combo, DO6bl1_combo, DO7bl1_combo, DO8bl1_combo,
                DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo, DO5bl2_combo, DO6bl2_combo, DO7bl2_combo, DO8bl2_combo,
                DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo, DO5bl3_combo, DO6bl3_combo, DO7bl3_combo, DO8bl3_combo
            };

            foreach (var el in do_combos)
            {
                el.Items.Clear(); el.Items.Add(NOT_SELECTED);
            }
        }

        ///<summary>Установка для comboBox изначального выбора сигналов</summary> 
        private void SetComboInitial_signals()
        {
            var combo_elements = new List<ComboBox>()
            {
                // Аналоговые выходы
                AO1_combo, AO2_combo, AO3_combo, AO1bl1_combo, AO2bl1_combo,
                AO1bl2_combo, AO2bl2_combo, AO1bl3_combo, AO2bl3_combo,
                // Дискретные выходы
                DO1_combo, DO2_combo, DO3_combo, DO4_combo, DO5_combo, DO6_combo,
                DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo, DO5bl1_combo, DO6bl1_combo, DO7bl1_combo, DO8bl1_combo,
                DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo, DO5bl2_combo, DO6bl2_combo, DO7bl2_combo, DO8bl2_combo,
                DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo, DO5bl3_combo, DO6bl3_combo, DO7bl3_combo, DO8bl3_combo,
                // Универсальные входы, ПЛК
                UI1_combo, UI2_combo, UI3_combo, UI4_combo, UI5_combo, UI6_combo, UI7_combo, UI8_combo, UI9_combo,
                UI10_combo, UI11_combo,
                // Универсальные входы, блок расширения 1
                UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo, UI7bl1_combo, UI8bl1_combo,
                UI9bl1_combo, UI10bl1_combo, UI11bl1_combo, UI12bl1_combo, UI13bl1_combo, UI14bl1_combo, UI15bl1_combo, UI16bl1_combo,
                // Универсальные входы, блок расширения 2
                UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo, UI7bl2_combo, UI8bl2_combo,
                UI9bl2_combo, UI10bl2_combo, UI11bl2_combo, UI12bl2_combo, UI13bl2_combo, UI14bl2_combo, UI15bl2_combo, UI16bl2_combo,
                // Универсальные входы, блок расширения 3
                UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo, UI7bl3_combo, UI8bl3_combo,
                UI9bl3_combo, UI10bl3_combo, UI11bl3_combo, UI12bl3_combo, UI13bl3_combo, UI14bl3_combo, UI15bl3_combo, UI16bl3_combo
              };

            foreach (var el in combo_elements) el.SelectedItem = NOT_SELECTED;

            // Тип универасального входного сигнала
            var ui_typeCombos = new List<ComboBox>()
            {
                // ПЛК
                UI1_typeCombo, UI2_typeCombo, UI3_typeCombo, UI4_typeCombo, UI5_typeCombo, UI6_typeCombo,
                UI7_typeCombo, UI8_typeCombo, UI9_typeCombo, UI10_typeCombo, UI11_typeCombo,
                // Блок расширения 1
                UI1bl1_typeCombo, UI2bl1_typeCombo, UI3bl1_typeCombo, UI4bl1_typeCombo, UI5bl1_typeCombo, UI6bl1_typeCombo,
                UI7bl1_typeCombo, UI8bl1_typeCombo, UI9bl1_typeCombo, UI10bl1_typeCombo, UI11bl1_typeCombo, UI12bl1_typeCombo,
                UI13bl1_typeCombo, UI14bl1_typeCombo, UI15bl1_typeCombo, UI16bl1_typeCombo,
                // Блок расширения 2
                UI1bl2_typeCombo, UI2bl2_typeCombo, UI3bl2_typeCombo, UI4bl2_typeCombo, UI5bl2_typeCombo, UI6bl2_typeCombo,
                UI7bl2_typeCombo, UI8bl2_typeCombo, UI9bl2_typeCombo, UI10bl2_typeCombo, UI11bl2_typeCombo, UI12bl2_typeCombo,
                UI13bl2_typeCombo, UI14bl2_typeCombo, UI15bl2_typeCombo, UI16bl2_typeCombo,
                // Блок расширения 3
                UI1bl3_typeCombo, UI2bl3_typeCombo, UI3bl3_typeCombo, UI4bl3_typeCombo, UI5bl3_typeCombo, UI6bl3_typeCombo,
                UI7bl3_typeCombo, UI8bl3_typeCombo, UI9bl3_typeCombo, UI10bl3_typeCombo, UI11bl3_typeCombo, UI12bl3_typeCombo,
                UI13bl3_typeCombo, UI14bl3_typeCombo, UI15bl3_typeCombo, UI16bl3_typeCombo
            };

            foreach (var el in ui_typeCombos)
            {
                el.SelectedItem = NTC; el.Enabled = false;
            }
        }

        ///<summary>Проверка распределения сигналов</summary>
        private bool CheckSignalsReady()
        {
            bool a = true;                      // Сигналы распределены по умолчанию

            foreach (var elem in list_do)       // Нераспеределённый сигнал в DO
                if (elem.Active) a = false;
            foreach (var elem in list_ao)       // Нераспеределённый сигнал в AO
                if (elem.Active) a = false;
            foreach (var elem in list_ui)       // Нераспеределённый сигнал в UI
                if (elem.Active) a = false;

            if (a) // Сигналы распределены
            {
                comboPlkType.Enabled = true;                                        // Разблокировка смены типа ПЛК
                saveToolStripMenuItem.Enabled = true;                               // Разблокировка сохранения файла
                signalsReadyLabel.Text = "Карта входов/выходов сформирована";
                signalsReadyLabel.ForeColor = Color.Green;
                loadPLC_SignalsButton.Show();                                       // Кнопка "Далее"
                loadToExl.Show();                                                   // Кнопка экспорта таблицы сигналов в Excel
                saveSpecToolStripMenuItem.Enabled = true;                           // Возможность сохранить спецификацию
                // Установка изображения, подобрано
                pic_signalsReady.Image = Properties.Resources.green_check;
            } 
            else // Сигналы не распределены
            {
                comboPlkType.Enabled = false;                                       // Блокировка смены типа ПЛК
                saveToolStripMenuItem.Enabled = false;                              // Блокировка сохранения файла
                signalsReadyLabel.Text = "Карта входов/выходов некорректна";
                signalsReadyLabel.ForeColor = Color.Red;
                loadPLC_SignalsButton.Hide();                                       // Кнопка "Далее"
                loadToExl.Hide();                                                   // Скрытие кнопки экспорта таблицы сигналов в Excel
                saveSpecToolStripMenuItem.Enabled = false;                          // Невозможность сохранить спецификацию
                // Установка изображения, не подобрано
                pic_signalsReady.Image = Properties.Resources.red_cross;
            }

            CheckPanelBlocks(CalcExpBlocks_typeNums());             // Проверка отображения панели блоков расширения Form1, тип и количество
            return a;
        }

        ///<summary>Метод для изменения DO comboBox</summary>
        private void DO_combo_SelectedIndexChanged(ComboBox comboBox, ref int combo_index, ref string combo_text, ref Label label)
        {
            if (ignoreEvents) return;                                   // Выход при активном параметре игнорирования событий
            string name = "";
            Do do_find = null;
            if (subDOcondition) return;                                 // Выход при режиме вычета сигналов DO
            if (comboBox.SelectedIndex == combo_index) return;          // Индекс не поменялся
            if (comboBox.SelectedIndex == 0)                            // Выбрали "Не выбрано"
            {
                if (comboBox.Items.Count > 1)                           // Больше одного элемента в списке
                {
                    string nameFind = combo_text;
                    do_find = list_do.Find(x => x.Name == nameFind);
                    list_do.Remove(do_find);
                    if (showCode) label.Text = "";
                }
                if (do_find != null)                                    // Найден элемент
                {
                    do_find.Dispose();                                  // Освобождение сигнала для распределения
                    list_do.Add(do_find);                               // Добавление с новым значением
                }
                if (!initialComboSignals)                               // Добавление к другим сигналам DO
                    AddtoCombosDO(combo_text, ref comboBox);
            }
            else                                                        // Выбран сигнал DO
            {
                name = string.Concat(comboBox.SelectedItem);
                do_find = list_do.Find(x => x.Name == name);
                list_do.Remove(do_find);                                // Удаление из списка
                if (do_find != null)
                {
                    do_find.Select();
                    list_do.Add(do_find);
                    if (showCode) label.Text = do_find.Code.ToString();
                }
                if (!initialComboSignals)                               // Если не начальная расстановка
                {
                    SubFromCombosDO(name, comboBox);                    // Удаление из других DO
                    string nameFind = combo_text;
                    do_find = list_do.Find(x => x.Name == nameFind);
                    list_do.Remove(do_find);
                    if (do_find != null)
                    {
                        do_find.Dispose();
                        list_do.Add(do_find);
                    }
                    AddtoCombosDO(combo_text, ref comboBox);            // Добавление к другим DO
                }
            }
            combo_text = comboBox.SelectedItem.ToString();              // Сохранение названия выбранного элемента
            combo_index = comboBox.SelectedIndex;                       // Сохранение индекса выбранного элемента
            CheckSignalsReady();                                        // Проверка распределения сигналов
        }

        ///<summary>Изменили DO1 comboBox</summary> 
        private void DO1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO1_combo, ref DO1combo_index, ref DO1combo_text, ref DO1_lab);
        }

        ///<summary>Изменили DO2 comboBox</summary> 
        private void DO2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO2_combo, ref DO2combo_index, ref DO2combo_text, ref DO2_lab);
        }

        ///<summary>Изменили DO3 comboBox</summary> 
        private void DO3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO3_combo, ref DO3combo_index, ref DO3combo_text, ref DO3_lab);
        }

        ///<summary>Изменили DO4 comboBox</summary> 
        private void DO4_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO4_combo, ref DO4combo_index, ref DO4combo_text, ref DO4_lab);
        }

        ///<summary>Изменили DO5 comboBox</summary> 
        private void DO5_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO5_combo, ref DO5combo_index, ref DO5combo_text, ref DO5_lab);
        }

        ///<summary>Изменили DO6 comboBox</summary> 
        private void DO6_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO6_combo, ref DO6combo_index, ref DO6combo_text, ref DO6_lab);
        }

        ///<summary>Изменили DO1 блока расширения 1 comboBox</summary>
        private void DO1bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO1bl1_combo, ref DO1bl1combo_index, ref DO1bl1combo_text, ref DO1bl1_lab);
        }

        ///<summary>Изменили DO2 блока расширения 1 comboBox</summary>
        private void DO2bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO2bl1_combo, ref DO2bl1combo_index, ref DO2bl1combo_text, ref DO2bl1_lab);
        }

        ///<summary>Изменили DO3 блока расширения 1 comboBox</summary>
        private void DO3bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO3bl1_combo, ref DO3bl1combo_index, ref DO3bl1combo_text, ref DO3bl1_lab);
        }

        ///<summary>Изменили DO4 блока расширения 1 comboBox</summary>
        private void DO4bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO4bl1_combo, ref DO4bl1combo_index, ref DO4bl1combo_text, ref DO4bl1_lab);
        }

        ///<summary>Изменили DO5 блока расширения 1 comboBox</summary>
        private void DO5bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO5bl1_combo, ref DO5bl1combo_index, ref DO5bl1combo_text, ref DO5bl1_lab);
        }

        ///<summary>Изменили DO6 блока расширения 1 comboBox</summary>
        private void DO6bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO6bl1_combo, ref DO6bl1combo_index, ref DO6bl1combo_text, ref DO6bl1_lab);
        }

        ///<summary>Изменили DO7 блока расширения 1 comboBox</summary>
        private void DO7bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO7bl1_combo, ref DO7bl1combo_index, ref DO7bl1combo_text, ref DO7bl1_lab);
        }

        ///<summary>Изменили DO8 блока расширения 1 comboBox</summary>
        private void DO8bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO8bl1_combo, ref DO8bl1combo_index, ref DO8bl1combo_text, ref DO8bl1_lab);
        }

        ///<summary>Изменили DO1 блока расширения 2 comboBox</summary>
        private void DO1bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO1bl2_combo, ref DO1bl2combo_index, ref DO1bl2combo_text, ref DO1bl2_lab);
        }

        ///<summary>Изменили DO2 блока расширения 2 comboBox</summary>
        private void DO2bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO2bl2_combo, ref DO2bl2combo_index, ref DO2bl2combo_text, ref DO2bl2_lab);
        }

        ///<summary>Изменили DO3 блока расширения 2 comboBox</summary>
        private void DO3bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO3bl2_combo, ref DO3bl2combo_index, ref DO3bl2combo_text, ref DO3bl2_lab);
        }

        ///<summary>Изменили DO4 блока расширения 2 comboBox</summary>
        private void DO4bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO4bl2_combo, ref DO4bl2combo_index, ref DO4bl2combo_text, ref DO4bl2_lab);
        }

        ///<summary>Изменили DO5 блока расширения 2 comboBox</summary>
        private void DO5bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO5bl2_combo, ref DO5bl2combo_index, ref DO5bl2combo_text, ref DO5bl2_lab);
        }

        ///<summary>Изменили DO6 блока расширения 2 comboBox</summary>
        private void DO6bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO6bl2_combo, ref DO6bl2combo_index, ref DO6bl2combo_text, ref DO6bl2_lab);
        }

        ///<summary>Изменили DO7 блока расширения 2 comboBox</summary>
        private void DO7bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO7bl2_combo, ref DO7bl2combo_index, ref DO7bl2combo_text, ref DO7bl2_lab);
        }

        ///<summary>Изменили DO8 блока расширения 2 comboBox</summary>
        private void DO8bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO8bl2_combo, ref DO8bl2combo_index, ref DO8bl2combo_text, ref DO8bl2_lab);
        }

        ///<summary>Изменили DO1 блока расширения 3 comboBox</summary>
        private void DO1bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO1bl3_combo, ref DO1bl3combo_index, ref DO1bl3combo_text, ref DO1bl3_lab);
        }

        ///<summary>Изменили DO2 блока расширения 3 comboBox</summary>
        private void DO2bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO2bl3_combo, ref DO2bl3combo_index, ref DO2bl3combo_text, ref DO2bl3_lab);
        }

        ///<summary>Изменили DO3 блока расширения 3 comboBox</summary>
        private void DO3bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO3bl3_combo, ref DO3bl3combo_index, ref DO3bl3combo_text, ref DO3bl3_lab);
        }

        ///<summary>Изменили DO4 блока расширения 3 comboBox</summary>
        private void DO4bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO4bl3_combo, ref DO4bl3combo_index, ref DO4bl3combo_text, ref DO4bl3_lab);
        }

        ///<summary>Изменили DO5 блока расширения 3 comboBox</summary>
        private void DO5bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO5bl3_combo, ref DO5bl3combo_index, ref DO5bl3combo_text, ref DO5bl3_lab);
        }

        ///<summary>Изменили DO6 блока расширения 3 comboBox</summary>
        private void DO6bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO6bl3_combo, ref DO6bl3combo_index, ref DO6bl3combo_text, ref DO6bl3_lab);
        }

        ///<summary>Изменили DO7 блока расширения 3 comboBox</summary>
        private void DO7bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO7bl3_combo, ref DO7bl3combo_index, ref DO7bl3combo_text, ref DO7bl3_lab);
        }

        ///<summary>Изменили DO8 блока расширения 3 comboBox</summary>
        private void DO8bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DO_combo_SelectedIndexChanged(DO8bl3_combo, ref DO8bl3combo_index, ref DO8bl3combo_text, ref DO8bl3_lab);
        }

        ///<summary>Добавление DO в другие слоты для выбора в comboBox</summary>
        private void AddToCombo_DO(string name, ComboBox cm, ref ComboBox comboBox)
        { 
            bool notFound = true;                                       // Элемент в списке не найден изначально
            if (comboBox != cm)                                         // Проверка текущего comboBox с проверяемым
            {
                Do do_find = list_do.Find(x => x.Name == name);
                if (do_find != null)                                    // Есть такой DO в списке дискретных выходов
                {
                    foreach (var el in comboBox.Items)                  // Если нет такого названия в списке
                        if (el.ToString() == name) notFound = false;
                    if (notFound) comboBox.Items.Add(do_find.Name);
                    notFound = true;
                }
            }
        }

        ///<summary>Добавление освободившегося DO к остальным comboBox</summary> 
        private void AddtoCombosDO(string name, ref ComboBox cm)
        {
            // ПЛК
            AddToCombo_DO(name, cm, ref DO1_combo); AddToCombo_DO(name, cm, ref DO2_combo); AddToCombo_DO(name, cm, ref DO3_combo);
            AddToCombo_DO(name, cm, ref DO4_combo); AddToCombo_DO(name, cm, ref DO5_combo); AddToCombo_DO(name, cm, ref DO6_combo);
            // Блок расширения 1
            AddToCombo_DO(name, cm, ref DO1bl1_combo); AddToCombo_DO(name, cm, ref DO2bl1_combo); AddToCombo_DO(name, cm, ref DO3bl1_combo);
            AddToCombo_DO(name, cm, ref DO4bl1_combo); AddToCombo_DO(name, cm, ref DO5bl1_combo); AddToCombo_DO(name, cm, ref DO6bl1_combo);
            AddToCombo_DO(name, cm, ref DO7bl1_combo); AddToCombo_DO(name, cm, ref DO8bl1_combo);
            // Блок расширения 2
            AddToCombo_DO(name, cm, ref DO1bl2_combo); AddToCombo_DO(name, cm, ref DO2bl2_combo); AddToCombo_DO(name, cm, ref DO3bl2_combo);
            AddToCombo_DO(name, cm, ref DO4bl2_combo); AddToCombo_DO(name, cm, ref DO5bl2_combo); AddToCombo_DO(name, cm, ref DO6bl2_combo);
            AddToCombo_DO(name, cm, ref DO7bl2_combo); AddToCombo_DO(name, cm, ref DO8bl2_combo);
            // Блок расширения 3
            AddToCombo_DO(name, cm, ref DO1bl3_combo); AddToCombo_DO(name, cm, ref DO2bl3_combo); AddToCombo_DO(name, cm, ref DO3bl3_combo);
            AddToCombo_DO(name, cm, ref DO4bl3_combo); AddToCombo_DO(name, cm, ref DO5bl3_combo); AddToCombo_DO(name, cm, ref DO6bl3_combo);
            AddToCombo_DO(name, cm, ref DO7bl3_combo); AddToCombo_DO(name, cm, ref DO8bl3_combo);
        }

        /// <summary>Удаление DO из других comboBox</summary>
        private void SubFromCombosDO(string name, ComboBox cm)
        {
            var do_combos = new List<ComboBox>()
            {
                DO1_combo, DO2_combo, DO3_combo, DO4_combo, DO5_combo, DO6_combo,
                DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo, DO5bl1_combo, DO6bl1_combo, DO7bl1_combo, DO8bl1_combo,
                DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo, DO5bl2_combo, DO6bl2_combo, DO7bl2_combo, DO8bl2_combo,
                DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo, DO5bl3_combo, DO6bl3_combo, DO7bl3_combo, DO8bl3_combo

            };

            if (name != NOT_SELECTED) // Кроме "Не выбрано"
                foreach (var el in do_combos)
                    if (el != cm) el.Items.Remove(name);
        }

        ///<summary>Добавление нового DO и его назначение для переданного comboBox</summary>
        private void SelectComboBox_DO(ComboBox cm, ushort code, Label label, string text, int index)
        {
            string name = list_do.Find(x => x.Code == code).Name;           // Поиск есть ли уже такое наименование в элементах comboBox
            if (!cm.Items.Contains(name)) cm.Items.Add(name);               // Добавление лишь когда совпадения нет
            cm.SelectedIndex = cm.Items.Count - 1;
            text = cm.SelectedItem.ToString();                              // Сохранение названия выбранного элемента
            index = cm.SelectedIndex;                                       // Сохранение выбранного индекса
            if (showCode) label.Text = code.ToString();
            list_do.Find(x => x.Code == code).Select();
        }

        ///<summary>Добавление нового DO и его назначение под первый нераспределённый выход</summary>
        private void AddNewDO(ushort code)
        {
            var blocks = CalcExpBlocks_typeNums();                          // Определение типов и количества блоков расширения

            RemoveThirdBlockUI_M72E16NA(blocks);                            // Проверка на удаление 3-го блока расширения UI
            RemoveSecondBlockUI_M72E16NA(blocks);                           // Проверка на удаление 2-го блока расширения UI
            RemoveFirstBlockUI_M72E16NA(blocks);                            // Проверка на удаление 1-го блока расширения UI

            AddFirstBlock_DOUI_M72E12RA(blocks);                            // Проверка добавления 1-го блока расширения M72E12RA (DO + UI)
            AddSecondBlock_DOUI_M72E12RA(blocks);                           // Проверка добавления 2-го блока расширения M72E12RA (DO + UI)
            AddThirdBlock_DOUI_M72E12RA(blocks);                            // Проверка добавления 3-го блока расширения M72E12RA (DO + UI)

            AddFirstBlockDO_M72E08RA(blocks);                               // Проверка добавления 1-го блока расширения M72E08RA (DO)
            AddSecondBlockDO_M72E08RA(blocks);                              // Проверка добавления 2-го блока расширения M72E08RA (DO)
            AddThirdBlockDO_M72E08RA(blocks);                               // Проверка добавления 3-го блока расширения M72E08RA (DO)

            // ПЛК
            if (DO1_combo.SelectedIndex == 0) SelectComboBox_DO(DO1_combo, code, DO1_lab, DO1combo_text, DO1combo_index);
            else if (DO2_combo.SelectedIndex == 0) SelectComboBox_DO(DO2_combo, code, DO2_lab, DO2combo_text, DO2combo_index);
            else if (DO3_combo.SelectedIndex == 0) SelectComboBox_DO(DO3_combo, code, DO3_lab, DO3combo_text, DO3combo_index);
            else if (DO4_combo.SelectedIndex == 0) SelectComboBox_DO(DO4_combo, code, DO4_lab, DO4combo_text, DO4combo_index);
            else if (DO5_combo.SelectedIndex == 0 && DO5_combo.Enabled) SelectComboBox_DO(DO5_combo, code, DO5_lab, DO5combo_text, DO5combo_index);
            else if (DO6_combo.SelectedIndex == 0 && DO6_combo.Enabled) SelectComboBox_DO(DO6_combo, code, DO6_lab, DO6combo_text, DO6combo_index);
            // Блок расширения 1
            else if (DO1bl1_combo.SelectedIndex == 0 && DO1bl1_combo.Enabled) 
                SelectComboBox_DO(DO1bl1_combo, code, DO1bl1_lab, DO1bl1combo_text, DO1bl1combo_index);
            else if (DO2bl1_combo.SelectedIndex == 0 && DO2bl1_combo.Enabled) 
                SelectComboBox_DO(DO2bl1_combo, code, DO2bl1_lab, DO2bl1combo_text, DO2bl1combo_index);
            else if (DO3bl1_combo.SelectedIndex == 0 && DO3bl1_combo.Enabled) 
                SelectComboBox_DO(DO3bl1_combo, code, DO3bl1_lab, DO3bl1combo_text, DO3bl1combo_index);
            else if (DO4bl1_combo.SelectedIndex == 0 && DO4bl1_combo.Enabled) 
                SelectComboBox_DO(DO4bl1_combo, code, DO4bl1_lab, DO4bl1combo_text, DO4bl1combo_index);
            else if (DO5bl1_combo.SelectedIndex == 0 && DO5bl1_combo.Enabled) 
                SelectComboBox_DO(DO5bl1_combo, code, DO5bl1_lab, DO5bl1combo_text, DO5bl1combo_index);
            else if (DO6bl1_combo.SelectedIndex == 0 && DO6bl1_combo.Enabled) 
                SelectComboBox_DO(DO6bl1_combo, code, DO6bl1_lab, DO6bl1combo_text, DO6bl1combo_index);
            else if (DO7bl1_combo.SelectedIndex == 0 && DO7bl1_combo.Enabled) 
                SelectComboBox_DO(DO7bl1_combo, code, DO7bl1_lab, DO7bl1combo_text, DO7bl1combo_index);
            else if (DO8bl1_combo.SelectedIndex == 0 && DO8bl1_combo.Enabled) 
                SelectComboBox_DO(DO8bl1_combo, code, DO8bl1_lab, DO8bl1combo_text, DO8bl1combo_index);
            // Блок расширения 2
            else if (DO1bl2_combo.SelectedIndex == 0 && DO1bl2_combo.Enabled) 
                SelectComboBox_DO(DO1bl2_combo, code, DO1bl2_lab, DO1bl2combo_text, DO1bl2combo_index);
            else if (DO2bl2_combo.SelectedIndex == 0 && DO2bl2_combo.Enabled) 
                SelectComboBox_DO(DO2bl2_combo, code, DO2bl2_lab, DO2bl2combo_text, DO2bl2combo_index);
            else if (DO3bl2_combo.SelectedIndex == 0 && DO3bl2_combo.Enabled) 
                SelectComboBox_DO(DO3bl2_combo, code, DO3bl2_lab, DO3bl2combo_text, DO3bl2combo_index);
            else if (DO4bl2_combo.SelectedIndex == 0 && DO4bl2_combo.Enabled) 
                SelectComboBox_DO(DO4bl2_combo, code, DO4bl2_lab, DO4bl2combo_text, DO4bl2combo_index);
            else if (DO5bl2_combo.SelectedIndex == 0 && DO5bl2_combo.Enabled) 
                SelectComboBox_DO(DO5bl2_combo, code, DO5bl2_lab, DO5bl2combo_text, DO5bl2combo_index);
            else if (DO6bl2_combo.SelectedIndex == 0 && DO6bl2_combo.Enabled) 
                SelectComboBox_DO(DO6bl2_combo, code, DO6bl2_lab, DO6bl2combo_text, DO6bl2combo_index);
            else if (DO7bl2_combo.SelectedIndex == 0 && DO7bl2_combo.Enabled) 
                SelectComboBox_DO(DO7bl2_combo, code, DO7bl2_lab, DO7bl2combo_text, DO7bl2combo_index);
            else if (DO8bl2_combo.SelectedIndex == 0 && DO8bl2_combo.Enabled) 
                SelectComboBox_DO(DO8bl2_combo, code, DO8bl2_lab, DO8bl2combo_text, DO8bl2combo_index);
            // Блок расширения 3
            else if (DO1bl3_combo.SelectedIndex == 0 && DO1bl3_combo.Enabled) 
                SelectComboBox_DO(DO1bl3_combo, code, DO1bl3_lab, DO1bl3combo_text, DO1bl3combo_index);
            else if (DO2bl3_combo.SelectedIndex == 0 && DO2bl3_combo.Enabled) 
                SelectComboBox_DO(DO2bl3_combo, code, DO2bl3_lab, DO2bl3combo_text, DO2bl3combo_index);
            else if (DO3bl3_combo.SelectedIndex == 0 && DO3bl3_combo.Enabled)
                SelectComboBox_DO(DO3bl3_combo, code, DO3bl3_lab, DO3bl3combo_text, DO3bl3combo_index);
            else if (DO4bl3_combo.SelectedIndex == 0 && DO4bl3_combo.Enabled)
                SelectComboBox_DO(DO4bl3_combo, code, DO4bl3_lab, DO4bl3combo_text, DO4bl3combo_index);
            else if (DO5bl3_combo.SelectedIndex == 0 && DO5bl3_combo.Enabled) 
                SelectComboBox_DO(DO5bl3_combo, code, DO5bl3_lab, DO5bl3combo_text, DO5bl3combo_index);
            else if (DO6bl3_combo.SelectedIndex == 0 && DO6bl3_combo.Enabled) 
                SelectComboBox_DO(DO6bl3_combo, code, DO6bl3_lab, DO6bl3combo_text, DO6bl3combo_index);
            else if (DO7bl3_combo.SelectedIndex == 0 && DO7bl3_combo.Enabled) 
                SelectComboBox_DO(DO7bl3_combo, code, DO7bl3_lab, DO7bl3combo_text, DO7bl3combo_index);
            else if (DO8bl3_combo.SelectedIndex == 0 && DO8bl3_combo.Enabled) 
                SelectComboBox_DO(DO8bl3_combo, code, DO8bl3_lab, DO8bl3combo_text, DO8bl3combo_index);
            
            CheckSignalsReady();    // Проверка распределения сигналов
        }

        ///<summary>Удаление DO из определённого comboBox</summary>
        private void RemoveDO_FromComboBox(ComboBox comboBox, string name, Label label, string text, int index)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
                if (comboBox.Items[i].ToString() == name)                                                   // Есть совпадение по имени в списке
                {
                    comboBox.Items.Remove(name);                                                            // Удаление элемента по имени
                    if (comboBox.Items.Count > 1)                                                           // Осталось больше одного элемента в списке
                    {
                        comboBox.SelectedIndex = comboBox.Items.Count - 1;                                  // Выбор последнего элемента
                        if (comboBox.SelectedItem.ToString() != NOT_SELECTED)                               
                        {
                            SubFromCombosDO(comboBox.SelectedItem.ToString(), comboBox);                    // Удаление из других comboBox выбранного элемента
                            Do find_do = list_do.Find(x => x.Name == comboBox.SelectedItem.ToString());     // Распределение выбранного DO из списка
                            if (find_do != null)
                            {
                                list_do.Remove(find_do);
                                find_do.Select();                                                           // Распределение DO
                                list_do.Add(find_do);
                                if (showCode) label.Text = find_do.Code.ToString();
                            }
                        }
                    }
                    else                                                                                    // Если в списке только "Не выбрано"
                    {
                        comboBox.SelectedItem = NOT_SELECTED;
                        label.Text = "";
                    }
                    text = comboBox.SelectedItem.ToString();                                                // Сохранение наименование выбранного DO
                    index = comboBox.SelectedIndex;                                                         // Сохранение индекса выбранного DO
                    break;
                }
        }

        ///<summary>Удаление DO из всех comboBox</summary>
        private void SubFromCombosDO(ushort code)
        {
            string name = "";                                               // Текстовое название дискретного выхода

            Do find_do = list_do.Find(x => x.Code == code);                 // Поиск имени дискретного выхода по числовому коду
            if (find_do != null) name = find_do.Name;                       // Найдено текстовое название дискретного выхода по коду
            else return;                                                    // Выход из метода

            subDOcondition = true;                                          // Признак удаления DO, не работает событие indexChanged

            // ПЛК
            RemoveDO_FromComboBox(DO1_combo, name, DO1_lab, DO1combo_text, DO1combo_index);                 // DO1
            RemoveDO_FromComboBox(DO2_combo, name, DO2_lab, DO2combo_text, DO2combo_index);                 // DO2
            RemoveDO_FromComboBox(DO3_combo, name, DO3_lab, DO3combo_text, DO3combo_index);                 // DO3
            RemoveDO_FromComboBox(DO4_combo, name, DO4_lab, DO4combo_text, DO4combo_index);                 // DO4
            RemoveDO_FromComboBox(DO5_combo, name, DO5_lab, DO5combo_text, DO5combo_index);                 // DO5
            RemoveDO_FromComboBox(DO6_combo, name, DO6_lab, DO6combo_text, DO6combo_index);                 // DO6
            // Блок расширения 1
            RemoveDO_FromComboBox(DO1bl1_combo, name, DO1bl1_lab, DO1bl1combo_text, DO1bl1combo_index);     // DO1
            RemoveDO_FromComboBox(DO2bl1_combo, name, DO2bl1_lab, DO2bl1combo_text, DO2bl1combo_index);     // DO2
            RemoveDO_FromComboBox(DO3bl1_combo, name, DO3bl1_lab, DO3bl1combo_text, DO3bl1combo_index);     // DO3
            RemoveDO_FromComboBox(DO4bl1_combo, name, DO4bl1_lab, DO4bl1combo_text, DO4bl1combo_index);     // DO4
            RemoveDO_FromComboBox(DO5bl1_combo, name, DO5bl1_lab, DO5bl1combo_text, DO5bl1combo_index);     // DO5
            RemoveDO_FromComboBox(DO6bl1_combo, name, DO6bl1_lab, DO6bl1combo_text, DO6bl1combo_index);     // DO6
            RemoveDO_FromComboBox(DO7bl1_combo, name, DO7bl1_lab, DO7bl1combo_text, DO7bl1combo_index);     // DO7
            RemoveDO_FromComboBox(DO8bl1_combo, name, DO8bl1_lab, DO8bl1combo_text, DO8bl1combo_index);     // DO8
            // Блок расширения 2
            RemoveDO_FromComboBox(DO1bl2_combo, name, DO1bl2_lab, DO1bl2combo_text, DO1bl2combo_index);     // DO1
            RemoveDO_FromComboBox(DO2bl2_combo, name, DO2bl2_lab, DO2bl2combo_text, DO2bl2combo_index);     // DO2
            RemoveDO_FromComboBox(DO3bl2_combo, name, DO3bl2_lab, DO3bl2combo_text, DO3bl2combo_index);     // DO3
            RemoveDO_FromComboBox(DO4bl2_combo, name, DO4bl2_lab, DO4bl2combo_text, DO4bl2combo_index);     // DO4
            RemoveDO_FromComboBox(DO5bl2_combo, name, DO5bl2_lab, DO5bl2combo_text, DO5bl2combo_index);     // DO5
            RemoveDO_FromComboBox(DO6bl2_combo, name, DO6bl2_lab, DO6bl2combo_text, DO6bl2combo_index);     // DO6
            RemoveDO_FromComboBox(DO7bl2_combo, name, DO7bl2_lab, DO7bl2combo_text, DO7bl2combo_index);     // DO7
            RemoveDO_FromComboBox(DO8bl2_combo, name, DO8bl2_lab, DO8bl2combo_text, DO8bl2combo_index);     // DO8
            // Блок расширения 3
            RemoveDO_FromComboBox(DO1bl3_combo, name, DO1bl3_lab, DO1bl3combo_text, DO1bl3combo_index);     // DO1
            RemoveDO_FromComboBox(DO2bl3_combo, name, DO2bl3_lab, DO2bl3combo_text, DO2bl3combo_index);     // DO2
            RemoveDO_FromComboBox(DO3bl3_combo, name, DO3bl3_lab, DO3bl3combo_text, DO3bl3combo_index);     // DO3
            RemoveDO_FromComboBox(DO4bl3_combo, name, DO4bl3_lab, DO4bl3combo_text, DO4bl3combo_index);     // DO4
            RemoveDO_FromComboBox(DO5bl3_combo, name, DO5bl3_lab, DO5bl3combo_text, DO5bl3combo_index);     // DO5
            RemoveDO_FromComboBox(DO6bl3_combo, name, DO6bl3_lab, DO6bl3combo_text, DO6bl3combo_index);     // DO6
            RemoveDO_FromComboBox(DO7bl3_combo, name, DO7bl3_lab, DO7bl3combo_text, DO7bl3combo_index);     // DO7
            RemoveDO_FromComboBox(DO8bl3_combo, name, DO8bl3_lab, DO8bl3combo_text, DO8bl3combo_index);     // DO8

            subDOcondition = false;                     // Сброс признака удаление из DO
            list_do.Remove(find_do);                    // Удаление сигнала из списка DO

            var blocks = CalcExpBlocks_typeNums();      // Определение типов и количества блоков расширения после удаления

            RemoveThirdBlock_DOUI_M72E12RA(blocks);     // Проверка для удаления 3-го блока расширения DO + UI
            RemoveSecondBlock_DOUI_M72E12RA(blocks);    // Проверка для удаления 2-го блока расширения DO + UI
            RemoveFirstBlock_DOUI_M72E12RA(blocks);     // Проверка для удаления 1-го блока расширения DO + UI

            RemoveThirdBlockDO_M72E08RA(blocks);        // Проверка для удаления 3-го блока расширения DO
            RemoveSecondBlockDO_M72E08RA(blocks);       // Проверка для удаления 2-го блока расширения DO
            RemoveFirstBlockDO_M72E08RA(blocks);        // Проверка для удаления 1-го блока расширения DO

            CheckSignalsReady();                        // Проверка распределения сигналов
        }

        /*** ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ***/

        ///<summary>Метод для добавления DO к списку сигналов</summary>
        private void AddToListDo(string name, ushort code)
        {
            list_do.Add(new Do(name, code));
            AddNewDO(code);                                                             // Добавление DO к свободному combobox выхода
        }

        ///<summary>Проверка и добавление дискретного выхода</summary>
        private void CheckAddDoToList(string name, ushort code)
        {
            Do find_do = list_do.Find(x => x.Code == code);
            if (find_do == null)                                                        // Нет такой записи
                AddToListDo(name, code);
        }

        /*** ВЫБОР ЭЛЕМЕНТОВ ***/

        /// <summary>Выбрали приточную заслонку</summary>
        private void DampCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort code = 6;                                                            // Открытие приточной заслонки
            if (dampCheck.Checked) 
                AddToListDo("Открытие приточной заслонки", code);
            else SubFromCombosDO(code);                                                 // Удаление DO из comboBox выходов
            HeatPrDampCheck_signalsCheckedChanged(this, e);                             // Проверка для обогрева заслонки
            OutDampCheck_signalsDOCheckedChanged(this, e);                              // Проверка для вытяжной воздушной заслонки
        }

        /// <summary>Выбрали обогрев приточной заслонки</summary>
        private void HeatPrDampCheck_signalsCheckedChanged(object sender, EventArgs e)
        {
            ushort code = 7;                                                            // Обогрев приточной заслонки
            if (dampCheck.Checked)                                                      // Выбрана приточная заслонка
            {
                if (heatPrDampCheck.Checked) 
                    AddToListDo("Обогрев приточной заслонки", code);
                else SubFromCombosDO(code);                                             // Удаление DO из comboBox выходов
            }
            else SubFromCombosDO(code);                                                 // Если заслонка не выбрана
        }

        ///<summary>Выбрали вытяжную воздушную заслонку</summary>
        private void OutDampCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort code = 20;                                                           // Открытие вытяжной заслонки
            if (comboSysType.SelectedIndex == 1 && dampCheck.Checked && outDampCheck.Checked)
                AddToListDo("Открытие вытяжной заслонки", code);
            else SubFromCombosDO(code);                                                 // Заслонка не выбрана или П-система
            HeatOutDampCheck_signalsCheckedChanged(this, e);                            // Обогрев вытяжной заслонки
        }

        ///<summary>Выбрали обогрев вытяжной заслонки</summary>
        private void HeatOutDampCheck_signalsCheckedChanged(object sender, EventArgs e)
        {
            ushort code = 21;                                                           // Обогрев вытяжной заслонки
            if (comboSysType.SelectedIndex == 1 && dampCheck.Checked && outDampCheck.Checked)
            {
                if (heatOutDampCheck.Checked)                                           // Выбрали обогрев заслонки
                    AddToListDo("Обогрев вытяжной заслонки", code);
                else SubFromCombosDO(code);                                             // Отмена выбора обогрева заслонки
            }
            else SubFromCombosDO(code);                                                 // Заслонка не выбрана
        }

        ///<summary>Выбрали наличие нагревателя</summary>
        private void HeaterCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort // Насос водяного калорифера и ступени электрокалорифера
                code_0 = 34, code_1 = 38, code_2 = 39, code_3 = 40,
                code_4 = 41, code_5 = 42, code_6 = 43, code_7 = 44, code_8 = 45;

            if (heaterCheck.Checked) // Выбрали нагреватель
            {
                if (heatTypeCombo.SelectedIndex == 0)                                   // Водяной калорифер
                    AddToListDo("Запуск насоса водяного калорифера", code_0);
                else if (heatTypeCombo.SelectedIndex == 1)                              // Электрокалорифер
                {
                    AddToListDo("1 ступень электрического калорифера", code_1);         // Первая ступень электрокалорифера
                    if (elHeatStagesCombo.SelectedIndex > 0)                            // Две ступени
                        AddToListDo("2 ступень электрического калорифера", code_2);
                    if (elHeatStagesCombo.SelectedIndex > 1)                            // Три ступени
                        AddToListDo("3 ступень электрического калорифера", code_3);
                    if (elHeatStagesCombo.SelectedIndex > 2)                            // Четыре ступени
                        AddToListDo("4 ступень электрического калорифера", code_4);
                    if (elHeatStagesCombo.SelectedIndex > 3)                            // Пять ступеней
                        AddToListDo("5 ступень электрического калорифера", code_5);     
                    if (elHeatStagesCombo.SelectedIndex > 4)                            // Шесть ступеней
                        AddToListDo("6 ступень электрического калорифера", code_6);
                    if (elHeatStagesCombo.SelectedIndex > 5)                            // Семь ступеней
                        AddToListDo("7 ступень электрического калорифера", code_7);
                    if (elHeatStagesCombo.SelectedIndex > 6)                            // Восемь ступеней
                        AddToListDo("8 ступень электрического калорифера", code_8);
                }
            }
            else // Отмена выбора нагревателя, удаление сигналов
            {
                var codes = new List<ushort>()
                {
                    code_0, code_1, code_2, code_3, code_4, code_5, code_6, code_7, code_8
                };

                foreach (var el in codes) SubFromCombosDO(el);
            }
        }

        ///<summary>Изменили тип основного нагревателя</summary>
        private void HeatTypeCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort                                                                      // Насос водяного калорифера и ступени электрокалорифера
                code_0 = 34, code_1 = 38, code_2 = 39, code_3 = 40,
                code_4 = 41, code_5 = 42, code_6 = 43, code_7 = 44, code_8 = 45;

            if (heaterCheck.Checked)                                                    // Когда выбран нагреватель
            {
                if (heatTypeCombo.SelectedIndex == 0)                                   // Водяной калорифер
                {
                    SubFromCombosDO(code_1); SubFromCombosDO(code_2); SubFromCombosDO(code_3);
                    SubFromCombosDO(code_4); SubFromCombosDO(code_5); SubFromCombosDO(code_6);
                    SubFromCombosDO(code_7); SubFromCombosDO(code_8);
                    AddToListDo("Запуск насоса водяного калорифера", code_0);
                }
                else if (heatTypeCombo.SelectedIndex == 1) // Электрокалорифер
                {
                    SubFromCombosDO(code_0);                                            // Удаление запуска насоса
                    AddToListDo("1 ступень электрического калорифера", code_1);         // Первая ступень нагрева
                    if (elHeatStagesCombo.SelectedIndex > 0)                            // Две ступени
                        AddToListDo("2 ступень электрического калорифера", code_2);
                    if (elHeatStagesCombo.SelectedIndex > 1)                            // Три ступени
                        AddToListDo("3 ступень электрического калорифера", code_3);
                    if (elHeatStagesCombo.SelectedIndex > 2)                            // Четыре ступени
                        AddToListDo("4 ступень электрического калорифера", code_4);
                    if (elHeatStagesCombo.SelectedIndex > 3)                            // Пять ступеней
                        AddToListDo("5 ступень электрического калорифера", code_5);
                    if (elHeatStagesCombo.SelectedIndex > 4)                            // Шесть ступеней
                        AddToListDo("6 ступень электрического калорифера", code_6);
                    if (elHeatStagesCombo.SelectedIndex > 5)                            // Семь ступеней
                        AddToListDo("7 ступень электрического калорифера", code_7);
                    if (elHeatStagesCombo.SelectedIndex > 6)                            // Восемь ступеней
                        AddToListDo("8 ступень электрического калорифера", code_8);
                }
            }
        }

        ///<summary>Выбрали резервный насос основного водяного калорифера</summary>
        private void ReservPumpHeater_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 35;                                                 // Сигнал "Пуск/стоп" резервного насоса

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)        // Водяной калорифер
            {
                if (reservPumpHeater.Checked)                                   // Выбран резервный насос
                    AddToListDo("Запуск резервного насоса водяного калорифера", code_1);
                else                                                            // Отмена выбора резервного насоса                                   
                    SubFromCombosDO(code_1);                                                 
            }
        }

        ///<summary>Изменили количество ступеней основного электрокалорифера</summary>
        private void ElHeatStagesCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort                                                              // Ступени электрокалорифера
                code_2 = 39, code_3 = 40, code_4 = 41, code_5 = 42, 
                code_6 = 43, code_7 = 44, code_8 = 45;

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 1)        // Выбран электрокалорифер
                switch (elHeatStagesCombo.SelectedIndex)                        // Выборка ступеней электрокалорифера
                {
                    case 0: // Одна ступень нагрева
                        SubFromCombosDO(code_8); SubFromCombosDO(code_7); SubFromCombosDO(code_6);
                        SubFromCombosDO(code_5); SubFromCombosDO(code_4); SubFromCombosDO(code_3); 
                        SubFromCombosDO(code_2); break;
                    case 1: // Две ступени нагрева
                        SubFromCombosDO(code_8); SubFromCombosDO(code_7); SubFromCombosDO(code_6);
                        SubFromCombosDO(code_5); SubFromCombosDO(code_4); SubFromCombosDO(code_3);
                        CheckAddDoToList("2 ступень электрического калорифера", code_2); break;
                    case 2: // Три ступени нагрева
                        SubFromCombosDO(code_8); SubFromCombosDO(code_7); SubFromCombosDO(code_6);
                        SubFromCombosDO(code_5); SubFromCombosDO(code_4);
                        CheckAddDoToList("2 ступень электрического калорифера", code_2);
                        CheckAddDoToList("3 ступень электрического калорифера", code_3); break;
                    case 3: // Четыре ступени нагрева
                        SubFromCombosDO(code_8); SubFromCombosDO(code_7); SubFromCombosDO(code_6); SubFromCombosDO(code_5);
                        CheckAddDoToList("2 ступень электрического калорифера", code_2);
                        CheckAddDoToList("3 ступень электрического калорифера", code_3);
                        CheckAddDoToList("4 ступень электрического калорифера", code_4); break;
                    case 4: // Пять ступеней нагрева
                        SubFromCombosDO(code_8); SubFromCombosDO(code_7); SubFromCombosDO(code_6);
                        CheckAddDoToList("2 ступень электрического калорифера", code_2);
                        CheckAddDoToList("3 ступень электрического калорифера", code_3);
                        CheckAddDoToList("4 ступень электрического калорифера", code_4);
                        CheckAddDoToList("5 ступень электрического калорифера", code_5); break;
                    case 5: // Шесть ступеней нагрева
                        SubFromCombosDO(code_8); SubFromCombosDO(code_7);
                        CheckAddDoToList("2 ступень электрического калорифера", code_2);
                        CheckAddDoToList("3 ступень электрического калорифера", code_3);
                        CheckAddDoToList("4 ступень электрического калорифера", code_4);
                        CheckAddDoToList("5 ступень электрического калорифера", code_5);
                        CheckAddDoToList("6 ступень электрического калорифера", code_6); break;
                    case 6: // Семь ступеней нагрева
                        SubFromCombosDO(code_8);
                        CheckAddDoToList("2 ступень электрического калорифера", code_2);
                        CheckAddDoToList("3 ступень электрического калорифера", code_3);
                        CheckAddDoToList("4 ступень электрического калорифера", code_4);
                        CheckAddDoToList("5 ступень электрического калорифера", code_5);
                        CheckAddDoToList("6 ступень электрического калорифера", code_6);
                        CheckAddDoToList("7 ступень электрического калорифера", code_7); break;
                    case 7: // Восемь ступеней нагрева
                        CheckAddDoToList("2 ступень электрического калорифера", code_2);
                        CheckAddDoToList("3 ступень электрического калорифера", code_3);
                        CheckAddDoToList("4 ступень электрического калорифера", code_4);
                        CheckAddDoToList("5 ступень электрического калорифера", code_5);
                        CheckAddDoToList("6 ступень электрического калорифера", code_6);
                        CheckAddDoToList("7 ступень электрического калорифера", code_7);
                        CheckAddDoToList("8 ступень электрического калорифера", code_8); break;
                }
        }

        ///<summary>Выбрали дополнительный нагреватель</summary>
        private void AddHeatCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort
                code_0 = 78, code_1 = 73, code_2 = 74, code_3 = 75,
                code_4 = 76, code_5 = 77, code_6 = 88, code_7 = 89, code_8 = 90;

            if (addHeatCheck.Checked)                                                   // Когда выбран второй нагреватель
            {
                if (heatAddTypeCombo.SelectedIndex == 0)                                // Водяной догреватель 
                    AddToListDo("Запуск насоса водяного догревателя", code_0);
                else if (heatAddTypeCombo.SelectedIndex == 1)                           // Электрический догреватель
                {
                    AddToListDo("1 ступень электрического догревателя", code_1);        // Первая ступень электрического догревателя
                    if (heatAddTypeCombo.SelectedIndex > 0)                             // Две ступени
                        AddToListDo("2 ступень электрического догревателя", code_2);
                    if (heatAddTypeCombo.SelectedIndex > 1)                             // Три ступени
                        AddToListDo("3 ступень электрического догревателя", code_3);
                    if (heatAddTypeCombo.SelectedIndex > 2)                             // Четыре ступени
                        AddToListDo("4 ступень электрического догревателя", code_4);
                    if (heatAddTypeCombo.SelectedIndex > 3)                             // Пять ступеней
                        AddToListDo("5 ступень электрического догревателя", code_5);
                    if (heatAddTypeCombo.SelectedIndex > 4)                             // Шесть ступеней
                        AddToListDo("6 ступень электрического догревателя", code_6);
                    if (heatAddTypeCombo.SelectedIndex > 5)                             // Семь ступеней
                        AddToListDo("7 ступень электрического догревателя", code_7);
                    if (heatAddTypeCombo.SelectedIndex > 6)                             // Восемь ступеней
                        AddToListDo("8 ступень электрического догревателя", code_8);
                }
            }
            else // Отмена выбора догревателя
            {
                var codes = new List<ushort>()
                {
                    code_0, code_1, code_2, code_3, code_4, code_5, code_6, code_7, code_8
                };

                foreach (var el in codes) SubFromCombosDO(el);
            }
        }

        ///<summary>Изменили тип второго нагревателя</summary>
        private void HeatAddTypeCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort
                code_0 = 78, code_1 = 73, code_2 = 74, code_3 = 75,
                code_4 = 76, code_5 = 77, code_6 = 88, code_7 = 89, code_8 = 90;

            if (addHeatCheck.Checked)                               // Когда выбран второй нагреватель
            {
                if (heatAddTypeCombo.SelectedIndex == 0)            // Водяной догреватель
                {
                    SubFromCombosDO(code_1); SubFromCombosDO(code_2); SubFromCombosDO(code_3); SubFromCombosDO(code_4);
                    SubFromCombosDO(code_5); SubFromCombosDO(code_6); SubFromCombosDO(code_7); SubFromCombosDO(code_8);
                    AddToListDo("Запуск насоса водяного догревателя", code_0);
                }
                else if (heatAddTypeCombo.SelectedIndex == 1)       // Электрический догреватель
                {
                    SubFromCombosDO(code_0);                        // Удаление запуска насоса
                    AddToListDo("1 ступень электрического догревателя", code_1);
                    if (heatAddTypeCombo.SelectedIndex > 0)                             // Две ступени
                        AddToListDo("2 ступень электрического догревателя", code_2);
                    if (heatAddTypeCombo.SelectedIndex > 1)                             // Три ступени
                        AddToListDo("3 ступень электрического догревателя", code_3);
                    if (heatAddTypeCombo.SelectedIndex > 2)                             // Четыре ступени
                        AddToListDo("4 ступень электрического догревателя", code_4);
                    if (heatAddTypeCombo.SelectedIndex > 3)                             // Пять ступеней
                        AddToListDo("5 ступень электрического догревателя", code_5);
                    if (heatAddTypeCombo.SelectedIndex > 4)                             // Шесть ступеней
                        AddToListDo("6 ступень электрического догревателя", code_6);
                    if (heatAddTypeCombo.SelectedIndex > 5)                             // Семь ступеней
                        AddToListDo("7 ступень электрического догревателя", code_7);
                    if (heatAddTypeCombo.SelectedIndex > 6)                             // Восемь ступеней
                        AddToListDo("8 ступень электрического догревателя", code_8);
                }
            }
        }

        ///<summary>Выбрали резервный насос водяного догревателя</summary>
        private void ReservPumpAddHeater_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 79;                                                         // Сигнал "Пуск/стоп" резервного насоса

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)            // Водяной догреватель
            {
                if (reservPumpAddHeater.Checked)                                        // Выбран резервный насос
                    AddToListDo("Запуск резервного насоса водяного догревателя", code_1);
                else
                    SubFromCombosDO(code_1);
            }
        }

        ///<summary>Изменили количество ступеней электрического догревателя</summary>
        private void ElHeatAddStagesCombo_signalsSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort
                code_2 = 74, code_3 = 75, code_4 = 76, code_5 = 77, 
                code_6 = 88, code_7 = 89, code_8 = 90;

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 1)        // Выбран электрический догреватель
            { 
                switch (elHeatAddStagesCombo.SelectedIndex)                         // Выборка ступеней
                {
                    case 0: // Одна ступень нагрева
                        SubFromCombosDO(code_8); SubFromCombosDO(code_7); SubFromCombosDO(code_6); SubFromCombosDO(code_5); 
                        SubFromCombosDO(code_4); SubFromCombosDO(code_3); SubFromCombosDO(code_2); break;
                    case 1: // Две ступени нагрева
                        SubFromCombosDO(code_8); SubFromCombosDO(code_7); SubFromCombosDO(code_6);
                        SubFromCombosDO(code_5); SubFromCombosDO(code_4); SubFromCombosDO(code_3);
                        CheckAddDoToList("2 ступень электрического догревателя", code_2); break;
                    case 2: // Три ступени нагрева
                        SubFromCombosDO(code_8); SubFromCombosDO(code_7); SubFromCombosDO(code_6);
                        SubFromCombosDO(code_5); SubFromCombosDO(code_4);
                        CheckAddDoToList("2 ступень электрического догревателя", code_2);
                        CheckAddDoToList("3 ступень электрического догревателя", code_3); break;
                    case 3: // Четыре ступени нагрева
                        SubFromCombosDO(code_8); SubFromCombosDO(code_7); SubFromCombosDO(code_6); SubFromCombosDO(code_5);
                        CheckAddDoToList("2 ступень электрического догревателя", code_2);
                        CheckAddDoToList("3 ступень электрического догревателя", code_3);
                        CheckAddDoToList("4 ступень электрического догревателя", code_4); break;
                    case 4: // Пять ступеней нагрева
                        SubFromCombosDO(code_8); SubFromCombosDO(code_7); SubFromCombosDO(code_6);
                        CheckAddDoToList("2 ступень электрического догревателя", code_2);
                        CheckAddDoToList("3 ступень электрического догревателя", code_3);
                        CheckAddDoToList("4 ступень электрического догревателя", code_4);
                        CheckAddDoToList("5 ступень электрического догревателя", code_5); break;
                    case 5: // Шесть ступеней нагрева
                        SubFromCombosDO(code_8); SubFromCombosDO(code_7);
                        CheckAddDoToList("2 ступень электрического догревателя", code_2);
                        CheckAddDoToList("3 ступень электрического догревателя", code_3);
                        CheckAddDoToList("4 ступень электрического догревателя", code_4);
                        CheckAddDoToList("5 ступень электрического догревателя", code_5);
                        CheckAddDoToList("6 ступень электрического догревателя", code_6); break;
                    case 6: // Семь ступеней нагрева
                        SubFromCombosDO(code_8);
                        CheckAddDoToList("2 ступень электрического догревателя", code_2);
                        CheckAddDoToList("3 ступень электрического догревателя", code_3);
                        CheckAddDoToList("4 ступень электрического догревателя", code_4);
                        CheckAddDoToList("5 ступень электрического догревателя", code_5);
                        CheckAddDoToList("6 ступень электрического догревателя", code_6);
                        CheckAddDoToList("7 ступень электрического догревателя", code_7); break;
                    case 7: // Восемь ступеней нагрева
                        CheckAddDoToList("2 ступень электрического догревателя", code_2);
                        CheckAddDoToList("3 ступень электрического догревателя", code_3);
                        CheckAddDoToList("4 ступень электрического догревателя", code_4);
                        CheckAddDoToList("5 ступень электрического догревателя", code_5);
                        CheckAddDoToList("6 ступень электрического догревателя", code_6);
                        CheckAddDoToList("7 ступень электрического догревателя", code_7);
                        CheckAddDoToList("8 ступень электрического догревателя", code_8); break;
                }
            }
        }

        ///<summary>Выбрали охладитель</summary>
        private void CoolerCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 48, code_2 = 49, code_3 = 50, code_4 = 51;              // Ступени фреонового охладителя

            if (coolerCheck.Checked)                                                // Выбрали охладитель
            {
                if (coolTypeCombo.SelectedIndex == 0)                               // Фреоновый 
                {
                    AddToListDo("1 ступень фреонового охладителя", code_1);
                    if (frCoolStagesCombo.SelectedIndex > 0)
                        AddToListDo("2 ступень фреонового охладителя", code_2);
                    if (frCoolStagesCombo.SelectedIndex > 1)
                        AddToListDo("3 ступень фреонового охладителя", code_3);
                    if (frCoolStagesCombo.SelectedIndex > 2)
                        AddToListDo("4 ступень фреонового охладителя", code_4);
                }
            } 
            else // Отмена выбора охладителя
            {
                var codes = new List<ushort>() { code_1, code_2, code_3, code_4 };
                foreach (var el in codes) SubFromCombosDO(el);
            }
        }

        ///<summary>Изменили тип охладителя</summary>
        private void CoolTypeCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 48, code_2 = 49, code_3 = 50, code_4 = 51;                          // Ступени фреонового охладителя

            if (coolerCheck.Checked)                                                            // Выбрали охладитель
            {
                if (coolTypeCombo.SelectedIndex == 0)                                           // Фреоновый охладитель
                {
                    AddToListDo("1 ступень фреонового охладителя", code_1);
                    if (frCoolStagesCombo.SelectedIndex > 0)
                        AddToListDo("2 ступень фреонового охладителя", code_2);
                    if (frCoolStagesCombo.SelectedIndex > 1)
                        AddToListDo("3 ступень фреонового охладителя", code_3);
                    if (frCoolStagesCombo.SelectedIndex > 2)
                        AddToListDo("4 ступень фреонового охладителя", code_4);
                }
                else if (coolTypeCombo.SelectedIndex == 1)                                      // Водяной охладитель
                {
                    var codes = new List<ushort>() { code_1, code_2, code_3, code_4 };
                    foreach (var el in codes) SubFromCombosDO(el);
                }
            }
        }

        ///<summary>Изменили количество ступеней фреонового охладителя</summary>
        private void FrCoolStagesCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_2 = 49, code_3 = 50, code_4 = 51;                                       // 2-4 ступени фреонового охладителя

            if (coolerCheck.Checked && coolTypeCombo.SelectedIndex == 0)                        // Выбран фреоновый охладитель
            { 
                switch (frCoolStagesCombo.SelectedIndex)                                        // Выборка ступеней охладителя
                {
                    case 0: // Одна ступень охладителя
                        SubFromCombosDO(code_4); SubFromCombosDO(code_3); SubFromCombosDO(code_2); break;
                    case 1: // Две ступени охладителя
                        SubFromCombosDO(code_4); SubFromCombosDO(code_3);
                        CheckAddDoToList("2 ступень фреонового охладителя", code_2); break;
                    case 2: // Три ступени охладителя
                        SubFromCombosDO(code_4);
                        CheckAddDoToList("2 ступень фреонового охладителя", code_2);
                        CheckAddDoToList("3 ступень фреонового охладителя", code_3); break;
                    case 3: // Четыре ступени охладителя
                        CheckAddDoToList("2 ступень фреонового охладителя", code_2);
                        CheckAddDoToList("3 ступень фреонового охладителя", code_3);
                        CheckAddDoToList("4 ступень фреонового охладителя", code_4); break;
                }
            }
        }

        ///<summary>Изменили тип системы</summary>
        private void ComboSysType_signalsSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 22, code_2 = 28;                                                // Сигнал "Пуск/Стоп" вытяжного вентилятора 1, 2

            if (comboSysType.SelectedIndex == 1)                                            // Выбрана ПВ-система
            {
                AddToListDo("Сигнал \"Пуск/Стоп\" вытяжного вентилятора 1", code_1);
                if (checkResOutFan.Checked)                                                 // Выбран резерв вытяжного вентилятора
                    AddToListDo("Сигнал \"Пуск/Стоп\" вытяжного вентилятора 2", code_2);
            } 
            else if (comboSysType.SelectedIndex == 0)                                       // Выбрана П-система
            {
                SubFromCombosDO(code_2); SubFromCombosDO(code_1);
            }
        }
        
        ///<summary>Выбрали резерв приточного вентилятора</summary>
        private void CheckResPrFan_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            // Сигнал "Пуск/Стоп" приточного вентилятора 2 / сигнал подачи питания приточного вентилятора 2
            ushort code_1 = 14;

            if (checkResPrFan.Checked)                                                          // Выбрали резервирование
            {
                if (prFanStStopCheck.Checked)                                                   // Выбран сигнал "Пуск/Стоп"
                    AddToListDo("Сигнал \"Пуск/Стоп\" приточного вентилятора 2", code_1);
            }
            else // Отмена выбора резервирования
            {
                SubFromCombosDO(code_1);                                                        // Отмена выбора сигнала "Пуск/Стоп"
            }
        }

        ///<summary>Выбрали резерв вытяжного вентилятора</summary>
        private void CheckResOutFan_signalsCheckedChanged(object sender, EventArgs e)
        {
            // Сигнал "Пуск/Стоп" вытяжного вентилятора 2 / сигнал подачи питания вытяжного вентилятора 2
            ushort code_1 = 28;

            if (comboSysType.SelectedIndex == 1 && checkResOutFan.Checked)                      // ПВ-система и резерв вытяжного
            {
                if (outFanStStopCheck.Checked)                                                  // Выбран сигнал "Пуск/Стоп"
                    AddToListDo("Сигнал \"Пуск/Стоп\" вытяжного вентилятора 2", code_1);
            }
            else // Не выбран резерв вытяжного вентилятора
            {
                SubFromCombosDO(code_1);                                                        // Отмена выбора сигнала "Пуск/Стоп"
            }
        }

        ///<summary>Выбрали увлажнитель</summary>
        private void HumidCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 80;                                                 // Питание насоса / 1-й секции
            if (humidCheck.Checked)                                             // Выбрали увлажнитель
            {
                if (humidTypeCombo.SelectedIndex == 0)                          // Паровой увлажнитель
                    AddToListDo("Сигнал ПУСК/СТОП увлажнителя", code_1);
                else if (humidTypeCombo.SelectedIndex == 1)                     // Сотовый увлажнитель
                    AddToListDo("Запуск насоса увлажнителя", code_1);
            }
            else // Отмена выбора увлажнителя
            {
                SubFromCombosDO(code_1);
            }
        }

        ///<summary>Изменили тип увлажнителя</summary>
        private void HumidTypeCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 80;                                                 // Питание насоса / 1-й секции
            if (humidCheck.Checked)                                             // Выбран увлажнитель
            {
                if (humidTypeCombo.SelectedIndex == 0)                          // Паровой увлажнитель
                {
                    SubFromCombosDO(code_1);
                    AddToListDo("Сигнал ПУСК/СТОП увлажнителя", code_1);
                }
                else if (humidTypeCombo.SelectedIndex == 1)                     // Сотовый увлажнитель
                {
                    SubFromCombosDO(code_1);
                    AddToListDo("Запуск насоса увлажнителя", code_1);
                }
            }
        }

        ///<summary>Выбрали рекуператор</summary>
        private void RecupCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            // Сигнал ПУСК/СТОП рекуператора (роторный) / запуск насоса гликолевого рекуператора
            ushort code_1 = 56, code_2 = 57; 

            if (recupCheck.Checked && comboSysType.SelectedIndex == 1)                              // Выбран рекуператор и ПВ система
            {
                if (recupTypeCombo.SelectedIndex == 0)                                              // Роторный рекуператор
                    AddToListDo("Сигнал ПУСК/СТОП рекуператора", code_1);
                else if (recupTypeCombo.SelectedIndex == 2 && pumpGlicRecCheck.Checked)             // Гликолевый рекуператор, выбран насос
                    AddToListDo("Запуск насоса гликолевого рекуператора", code_2);
            }
            else // Отмена выбора рекуператора
            {
                SubFromCombosDO(code_2); SubFromCombosDO(code_1);
            }
        }

        ///<summary>Изменили тип рекуператора</summary>
        private void RecupTypeCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            // Сигнал ПУСК/СТОП рекуператора (роторный) / запуск насоса гликолевого рекуператора
            ushort code_1 = 56, code_2 = 57;

            if (recupCheck.Checked && comboSysType.SelectedIndex == 1)                              // Выбран рекуператор и ПВ система
            {
                if (recupTypeCombo.SelectedIndex == 0)                                              // Роторный рекуператор
                {
                    SubFromCombosDO(code_2);
                    AddToListDo("Сигнал ПУСК/СТОП рекуператора", code_1);
                }
                else if (recupTypeCombo.SelectedIndex == 2)                                         // Гликолевый рекуператор
                {
                    SubFromCombosDO(code_1);
                    if (pumpGlicRecCheck.Checked)                                                   // Выбран насос рекуператора
                        AddToListDo("Запуск насоса гликолевого рекуператора", code_2);
                }
                else if (recupTypeCombo.SelectedIndex == 1)                                         // Пластинчатый рекуператор
                {
                    SubFromCombosDO(code_1); SubFromCombosDO(code_2);                               // Сигнал ПУСК/СТОП роторного рекуператора / насос гликолевого
                }
            }
        }

        ///<summary>Выбрали насос гликолевого рекуператора</summary>
        private void PumpGlicRecCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            
            ushort code_1 = 57;                                                                                 // Запуск насоса гликолевого рекуператора

            if (recupCheck.Checked && comboSysType.SelectedIndex == 1 && recupTypeCombo.SelectedIndex == 2)     // ПВ-система и гликолевый рекуператор
            { 
                if (pumpGlicRecCheck.Checked)                                                                   // Выбран насос гликолевого рекуператора
                    AddToListDo("Запуск насоса гликолевого рекуператора", code_1);
                else                                                                                            // Отмена выбора насоса гликолевого рекуператора
                    SubFromCombosDO(code_1);
            }
        }

        ///<summary>Выбрали резервный насос гликолевого рекуператора</summary>
        private void ReservPumpGlik_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;

            ushort code_1 = 58;

            if (recupCheck.Checked && comboSysType.SelectedIndex == 1 && recupTypeCombo.SelectedIndex == 2)     // ПВ-система и гликолевый рекуператор
            {
                if (reservPumpGlik.Checked)
                    AddToListDo("Запуск резервного насоса гликолевого рекуператора", code_1);
                else
                    SubFromCombosDO(code_1);
            }
        }

        ///<summary>Выбрали внешний сигнал "Работа"</summary>
        private void SigWorkCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;

            ushort code_1 = 1;                                                              // Сигнал "Работа"

            if (sigWorkCheck.Checked)                                                       // Выбрали сигнал
                AddToListDo("Сигнал \"Работа\"", code_1);
            else                                                                            // Отмена выбора сигнала
                SubFromCombosDO(code_1);
        }

        ///<summary>Выбрали внешний сигнал "Авария"</summary>
        private void SigAlarmCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;

            ushort code_1 = 3;                                                              // Сигнал "Авария"

            if (sigAlarmCheck.Checked)                                                      // Выбрали сигнал
                AddToListDo("Сигнал \"Авария\"", code_1);
            else                                                                            // Отмена выбора сигнала
                SubFromCombosDO(code_1);
        }

        ///<summary>Выбрали внешний сигнал "Загрязнение фильтра"</summary>
        private void SigFilAlarmCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;

            ushort code_1 = 2;                                                              // Сигнал "Загрязнение фильтра"

            if (sigFilAlarmCheck.Checked && filterCheck.Checked)                            // Выбрали сигнал и выбран фильтр
                AddToListDo("Сигнал \"Загрязнение фильтра\"", code_1);
            else                                                                            // Отмена выбора сигнала/выбора фильтра
                SubFromCombosDO(code_1);
        }

        ///<summary>Выбрали сигнал "Пуск/Стоп" для приточного и резервного вентилятора</summary>
        private void PrFanStStopCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;

            ushort code_1 = 8, code_2 = 14;                                                         // Сигнал "Пуск/Стоп" вентилятор П основной / резерв
 
            if (prFanStStopCheck.Checked)                                                           // Выбрали сигнал "Пуск/Стоп"
            {
                AddToListDo("Сигнал \"Пуск/Стоп\" приточного вентилятора 1", code_1);
                if (checkResPrFan.Checked)                                                          // Если выбран резерв П вентилятора
                    AddToListDo("Сигнал \"Пуск/Стоп\" приточного вентилятора 2", code_2);
            }
            else                                                                                    // Отмена выбора сигнала "Пуск/Стоп"
            {
                SubFromCombosDO(code_1); SubFromCombosDO(code_2);
            }
        }

        ///<summary>Выбрали сигнал "Пуск/Стоп" для вытяжного вентилятора</summary>
        private void OutFanStStopCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;

            ushort code_1 = 22, code_2 = 28;                                                            // Сигнал "Пуск/Стоп" вентилятор В основной / резерв


            if (outFanStStopCheck.Checked)                                                              // Выбрали сигнал "Пуск/Стоп"
            {
                AddToListDo("Сигнал \"Пуск/Стоп\" вытяжного вентилятора 1", code_1);
                if (checkResOutFan.Checked)                                                             // Если выбран резерв вытяжного
                    AddToListDo("Сигнал \"Пуск/Стоп\" вытяжного вентилятора 2", code_2);
            }
            else                                                                                        // Отмена выбора сигнала "Пуск/Стоп"
            {
                SubFromCombosDO(code_1); SubFromCombosDO(code_2);
            }
        }

        ///<summary>Изменили тип управления приточного ПЧ</summary>
        private void PrFanControlCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            if (prFanFC_check.Checked)                                                                  // Когда выбран ПЧ
            {
                if (prFanControlCombo.SelectedIndex == 0 && !prFanStStopCheck.Checked)                  // Внешние контакты и не выбран сигнал "Пуск/Стоп"
                    prFanStStopCheck.Checked = true;
                else if (prFanControlCombo.SelectedIndex == 1 && prFanStStopCheck.Checked)              // Modbus, был выбран сигнал "Пуск/Стоп"
                    prFanStStopCheck.Checked = false;
            }
        }

        ///<summary>Выбрали заслонку приточного вентилятора</summary>
        private void PrDampFanCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort code = 12;                                                                           // Сигнал открытия заслонки приточного вентилятора

            if (prDampFanCheck.Checked)                                                                 // Выбрана заслонка приточного вентилятора
            {
                AddToListDo("Открытие заслонки приточного вентилятора", code);
            }
            else                                                                                        // Отмена выбора сигнала открытия заслонки
            {
                SubFromCombosDO(code);
            }
        }

        ///<summary>Изменили тип управления ПЧ вытяжного вентилятора</summary>
        private void OutFanControlCombo_signalsDOSelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSysType.SelectedIndex == 1 && outFanFC_check.Checked)                              // Выбран ПЧ и ПВ-система
            {
                if (outFanControlCombo.SelectedIndex == 0 && !outFanStStopCheck.Checked)                // Внешние контакты и не выбран сигнал "Пуск/Стоп"
                    outFanStStopCheck.Checked = true;
                else if (outFanControlCombo.SelectedIndex == 1 && outFanStStopCheck.Checked)            // Modbus, был выбран сигнал "Пуск/Стоп"
                    outFanStStopCheck.Checked = false;
            }
        }

        ///<summary>Выбрали заслонку вытяжного вентилятора</summary>
        private void OutDampFanCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            ushort code = 26;                                                                           // Сигнал открытия заслонки вытяжного вентилятора

            if (outDampFanCheck.Checked)                                                                // Выбрана заслонка вытяжного вентилятора
            {
                AddToListDo("Открытие заслонки вытяжного вентилятора", code);
            }
            else                                                                                        // Отмена выбора открытия заслонки
            {
                SubFromCombosDO(code);
            }
        }

        ///<summary>Выбрали насос дополнительного водяного нагревателя</summary>
        private void PumpAddHeatCheck_signalsDOCheckedChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;

            ushort code_1 = 78;                                                                         // Сигнал запуска насоса 

            if (addHeatCheck.Checked && pumpAddHeatCheck.Checked)                                       // Выбран второй нагреватель и насос
                AddToListDo("Запуск насоса водяного догревателя", code_1);
            else if (addHeatCheck.Checked && !pumpAddHeatCheck.Checked)                                 // Отмена выбора запуска насоса
                SubFromCombosDO(code_1);
        }
    }
}