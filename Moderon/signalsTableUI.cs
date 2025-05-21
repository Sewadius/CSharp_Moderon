using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Moderon
{
    /// <summary>Класс для универсальных входов</summary>
    class Ui
    {
        public string Name { get; private set; }
        public ushort Code { get; private set; }
        public string Type { get; private set; }
        public bool Active { get; private set; } = true;            // Свободен к распределению по умолчанию
        public Ui(string name, ushort code, string type)            // type: di - дискретные входы; ntc, 4_20 - аналоговые
        {
            Name = name; Code = code; Type = type;
        }
        public Ui(string name, ushort code, string type, bool active) 
        {
            Name=name; Code = code; Type = type; Active = active;
        }

        /// <summary>Сеттер для установки нового типа UI сигнала</summary>
        public void SetType(string type) { Type = type; }

        /// <summary>Распределение свободного UI сигнала</summary>
        public void Select() => Active = false;

        /// <summary>Освобождение ранее распределённого UI сигнала</summary>
        public void Dispose() => Active = true;
    }

    public partial class Form1 : Form
    {
        static readonly string
            DI = "DI", NTC = "NTC", mA_4_20 = "4-20 мА";

        List<Ui> list_ui = new List<Ui>();

        bool subUIcondition = false;    // Условие при удалении UI

        // Сохранение наименование для выбранного элемента для ПЛК
        string
            // ПЛК
            UI1combo_text, UI2combo_text, UI3combo_text, UI4combo_text, UI5combo_text,
            UI6combo_text, UI7combo_text, UI8combo_text, UI9combo_text, UI10combo_text, UI11combo_text,
            // Блок расширения 1
            UI1bl1combo_text, UI2bl1combo_text, UI3bl1combo_text, UI4bl1combo_text, UI5bl1combo_text, UI6bl1combo_text,
            UI7bl1combo_text, UI8bl1combo_text, UI9bl1combo_text, UI10bl1combo_text, UI11bl1combo_text, UI12bl1combo_text,
            UI13bl1combo_text, UI14bl1combo_text, UI15bl1combo_text, UI16bl1combo_text,
            // Блок расширения 2
            UI1bl2combo_text, UI2bl2combo_text, UI3bl2combo_text, UI4bl2combo_text, UI5bl2combo_text, UI6bl2combo_text,
            UI7bl2combo_text, UI8bl2combo_text, UI9bl2combo_text, UI10bl2combo_text, UI11bl2combo_text, UI12bl2combo_text,
            UI13bl2combo_text, UI14bl2combo_text, UI15bl2combo_text, UI16bl2combo_text,
            // Блок расширения 3
            UI1bl3combo_text, UI2bl3combo_text, UI3bl3combo_text, UI4bl3combo_text, UI5bl3combo_text, UI6bl3combo_text,
            UI7bl3combo_text, UI8bl3combo_text, UI9bl3combo_text, UI10bl3combo_text, UI11bl3combo_text, UI12bl3combo_text,
            UI13bl3combo_text, UI14bl3combo_text, UI15bl3combo_text, UI16bl3combo_text;

        // Сохранение ранее выбранного индекса для comboBox для ПЛК
        int
            // ПЛК
            UI1combo_index, UI2combo_index, UI3combo_index, UI4combo_index, UI5combo_index,
            UI6combo_index, UI7combo_index, UI8combo_index, UI9combo_index, UI10combo_index, UI11combo_index,
            // Блок расширения 1
            UI1bl1combo_index, UI2bl1combo_index, UI3bl1combo_index, UI4bl1combo_index, UI5bl1combo_index, UI6bl1combo_index,
            UI7bl1combo_index, UI8bl1combo_index, UI9bl1combo_index, UI10bl1combo_index, UI11bl1combo_index, UI12bl1combo_index,
            UI13bl1combo_index, UI14bl1combo_index, UI15bl1combo_index, UI16bl1combo_index,
            // Блок расширения 2
            UI1bl2combo_index, UI2bl2combo_index, UI3bl2combo_index, UI4bl2combo_index, UI5bl2combo_index, UI6bl2combo_index,
            UI7bl2combo_index, UI8bl2combo_index, UI9bl2combo_index, UI10bl2combo_index, UI11bl2combo_index, UI12bl2combo_index,
            UI13bl2combo_index, UI14bl2combo_index, UI15bl2combo_index, UI16bl2combo_index,
            // Блок расширения 3
            UI1bl3combo_index, UI2bl3combo_index, UI3bl3combo_index, UI4bl3combo_index, UI5bl3combo_index, UI6bl3combo_index,
            UI7bl3combo_index, UI8bl3combo_index, UI9bl3combo_index, UI10bl3combo_index, UI11bl3combo_index, UI12bl3combo_index,
            UI13bl3combo_index, UI14bl3combo_index, UI15bl3combo_index, UI16bl3combo_index;


        /// <summary>Начальная установка для входов UI таблицы сигналов</summary>
        public void Set_UIComboTextIndex()
        {
            var ui_signals = new List<string>()     // Список подписей для названия выбранного входа UI
            {
                // ПЛК
                UI1combo_text, UI2combo_text, UI3combo_text, UI4combo_text, UI5combo_text,
                UI6combo_text, UI7combo_text, UI8combo_text, UI9combo_text, UI10combo_text, UI11combo_text,
                // Блок расширения 1
                UI1bl1combo_text, UI2bl1combo_text, UI3bl1combo_text, UI4bl1combo_text, UI5bl1combo_text, UI6bl1combo_text,
                UI7bl1combo_text, UI8bl1combo_text, UI9bl1combo_text, UI10bl1combo_text, UI11bl1combo_text, UI12bl1combo_text,
                UI13bl1combo_text, UI14bl1combo_text, UI15bl1combo_text, UI16bl1combo_text,
                // Блок расширения 2
                UI1bl2combo_text, UI2bl2combo_text, UI3bl2combo_text, UI4bl2combo_text, UI5bl2combo_text, UI6bl2combo_text,
                UI7bl2combo_text, UI8bl2combo_text, UI9bl2combo_text, UI10bl2combo_text, UI11bl2combo_text, UI12bl2combo_text,
                UI13bl2combo_text, UI14bl2combo_text, UI15bl2combo_text, UI16bl2combo_text,
                // Блок расширения 3
                UI1bl3combo_text, UI2bl3combo_text, UI3bl3combo_text, UI4bl3combo_text, UI5bl3combo_text, UI6bl3combo_text,
                UI7bl3combo_text, UI8bl3combo_text, UI9bl3combo_text, UI10bl3combo_text, UI11bl3combo_text, UI12bl3combo_text,
                UI13bl3combo_text, UI14bl3combo_text, UI15bl3combo_text, UI16bl3combo_text
            };

            var ui_signals_index = new List<int>()  // Список выбранного индекса для входа UI
            {
                // ПЛК
                UI1combo_index, UI2combo_index, UI3combo_index, UI4combo_index, UI5combo_index,
                UI6combo_index, UI7combo_index, UI8combo_index, UI9combo_index, UI10combo_index, UI11combo_index,
                // Блок расширения 1
                UI1bl1combo_index, UI2bl1combo_index, UI3bl1combo_index, UI4bl1combo_index, UI5bl1combo_index, UI6bl1combo_index,
                UI7bl1combo_index, UI8bl1combo_index, UI9bl1combo_index, UI10bl1combo_index, UI11bl1combo_index, UI12bl1combo_index,
                UI13bl1combo_index, UI14bl1combo_index, UI15bl1combo_index, UI16bl1combo_index,
                // Блок расширения 2
                UI1bl2combo_index, UI2bl2combo_index, UI3bl2combo_index, UI4bl2combo_index, UI5bl2combo_index, UI6bl2combo_index,
                UI7bl2combo_index, UI8bl2combo_index, UI9bl2combo_index, UI10bl2combo_index, UI11bl2combo_index, UI12bl2combo_index,
                UI13bl2combo_index, UI14bl2combo_index, UI15bl2combo_index, UI16bl2combo_index,
                // Блок расширения 3
                UI1bl3combo_index, UI2bl3combo_index, UI3bl3combo_index, UI4bl3combo_index, UI5bl3combo_index, UI6bl3combo_index,
                UI7bl3combo_index, UI8bl3combo_index, UI9bl3combo_index, UI10bl3combo_index, UI11bl3combo_index, UI12bl3combo_index,
                UI13bl3combo_index, UI14bl3combo_index, UI15bl3combo_index, UI16bl3combo_index
            };

            for (var i = 0; i < ui_signals.Count; i++) ui_signals[i] = NOT_SELECTED;
            for (var i = 0; i < ui_signals_index.Count; i++) ui_signals_index[i] = 0;
        }

        ///<summary>Сброс выбора сигналов для UI comboBox</summary>
        public void ResetButton_signalsUIClick(object sender, EventArgs e)
        {
            var ui_combos = new List<ComboBox>()
            {
                // ПЛК
                UI1_combo, UI2_combo, UI3_combo, UI4_combo, UI5_combo, UI6_combo, UI7_combo,
                UI8_combo, UI9_combo, UI10_combo, UI11_combo,
                // Блок расширения 1
                UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo, UI7bl1_combo, UI8bl1_combo,
                UI9bl1_combo, UI10bl1_combo, UI11bl1_combo, UI12bl1_combo, UI13bl1_combo, UI14bl1_combo, UI15bl1_combo, UI16bl1_combo,
                // Блок расширения 2
                UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo, UI7bl2_combo, UI8bl2_combo,
                UI9bl2_combo, UI10bl2_combo, UI11bl2_combo, UI12bl2_combo, UI13bl2_combo, UI14bl2_combo, UI15bl2_combo, UI16bl2_combo,
                // Блок расширения 3
                UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo, UI7bl3_combo, UI8bl3_combo,
                UI9bl3_combo, UI10bl3_combo, UI11bl3_combo, UI12bl3_combo, UI13bl3_combo, UI14bl3_combo, UI15bl3_combo, UI16bl3_combo
            };

            foreach (var el in ui_combos)
            {
                el.Items.Clear(); el.Items.Add(NOT_SELECTED);
            }
        }

        ///<summary>Проверка и добавление универсального входа</summary>
        private void CheckAddUIToList(string name, ushort code, string type)
        {
            Ui find_ui = list_ui.Find(x => x.Code == code);
            if (find_ui == null) AddToListUI(name, code, type);
        }

        ///<summary>Метод для добавления UI к списку сигналов</summary>
        private void AddToListUI(string name, ushort code, string type)
        {
            list_ui.Add(new Ui(name, code, type));          // Добавление в список сигналов UI
            AddNewUI(code, type);                           // Добавление нового UI и его назначение под выход
        }

        ///<summary>Добавление освободившегося UI к остальным comboBox</summary>
        private void AddtoCombo_UI(string name, ComboBox cm, ref ComboBox comboBox)
        {
            bool notFound = true;                                       // Элемент в списке не найден

            if (comboBox != cm)                                         // comboBox отличаются
            {
                Ui ui_find = list_ui.Find(x => x.Name == name);

                if (ui_find != null)                                    // Не пустой сигнал и свободен к распределению
                {
                    foreach (var el in comboBox.Items)
                        if (el.ToString() == name) notFound = false;
                    if (notFound) comboBox.Items.Add(ui_find.Name);
                    notFound = false;
                }
            }
        }

        ///<summary>Добавление сигнала UI в другие слоты для выбора в comboBox</summary>
        private void AddToCombosUI(Ui ui, ComboBox cm)
        {
            // ПЛК
            AddtoCombo_UI(ui.Name, cm, ref UI1_combo); AddtoCombo_UI(ui.Name, cm, ref UI2_combo); AddtoCombo_UI(ui.Name, cm, ref UI3_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI4_combo); AddtoCombo_UI(ui.Name, cm, ref UI5_combo); AddtoCombo_UI(ui.Name, cm, ref UI6_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI7_combo); AddtoCombo_UI(ui.Name, cm, ref UI8_combo); AddtoCombo_UI(ui.Name, cm, ref UI9_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI10_combo); AddtoCombo_UI(ui.Name, cm, ref UI11_combo);

            if (ui.Type != DI) return;  // Датчики не добавляются к распределению для блоков расширения

            // Блок расширения 1
            AddtoCombo_UI(ui.Name, cm, ref UI1bl1_combo); AddtoCombo_UI(ui.Name, cm, ref UI2bl1_combo); AddtoCombo_UI(ui.Name, cm, ref UI3bl1_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI4bl1_combo); AddtoCombo_UI(ui.Name, cm, ref UI5bl1_combo); AddtoCombo_UI(ui.Name, cm, ref UI6bl1_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI7bl1_combo); AddtoCombo_UI(ui.Name, cm, ref UI8bl1_combo); AddtoCombo_UI(ui.Name, cm, ref UI9bl1_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI10bl1_combo); AddtoCombo_UI(ui.Name, cm, ref UI11bl1_combo); AddtoCombo_UI(ui.Name, cm, ref UI12bl1_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI13bl1_combo); AddtoCombo_UI(ui.Name, cm, ref UI14bl1_combo); AddtoCombo_UI(ui.Name, cm, ref UI15bl1_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI16bl1_combo);
            
            // Блок расширения 2
            AddtoCombo_UI(ui.Name, cm, ref UI1bl2_combo); AddtoCombo_UI(ui.Name, cm, ref UI2bl2_combo); AddtoCombo_UI(ui.Name, cm, ref UI3bl2_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI4bl2_combo); AddtoCombo_UI(ui.Name, cm, ref UI5bl2_combo); AddtoCombo_UI(ui.Name, cm, ref UI6bl2_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI7bl2_combo); AddtoCombo_UI(ui.Name, cm, ref UI8bl2_combo); AddtoCombo_UI(ui.Name, cm, ref UI9bl2_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI10bl2_combo); AddtoCombo_UI(ui.Name, cm, ref UI11bl2_combo); AddtoCombo_UI(ui.Name, cm, ref UI12bl2_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI13bl2_combo); AddtoCombo_UI(ui.Name, cm, ref UI14bl2_combo); AddtoCombo_UI(ui.Name, cm, ref UI15bl2_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI16bl2_combo);
            
            // Блок расширения 3
            AddtoCombo_UI(ui.Name, cm, ref UI1bl3_combo); AddtoCombo_UI(ui.Name, cm, ref UI2bl3_combo); AddtoCombo_UI(ui.Name, cm, ref UI3bl3_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI4bl3_combo); AddtoCombo_UI(ui.Name, cm, ref UI5bl3_combo); AddtoCombo_UI(ui.Name, cm, ref UI6bl3_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI7bl3_combo); AddtoCombo_UI(ui.Name, cm, ref UI8bl3_combo); AddtoCombo_UI(ui.Name, cm, ref UI9bl3_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI10bl3_combo); AddtoCombo_UI(ui.Name, cm, ref UI11bl3_combo); AddtoCombo_UI(ui.Name, cm, ref UI12bl3_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI13bl3_combo); AddtoCombo_UI(ui.Name, cm, ref UI14bl3_combo); AddtoCombo_UI(ui.Name, cm, ref UI15bl3_combo);
            AddtoCombo_UI(ui.Name, cm, ref UI16bl3_combo);
        }

        ///<summary>Удаление UI из других comboBox</summary>
        private void SubFromCombosUI(string name, ComboBox cm)
        {
            var ui_combos = new List<ComboBox>()
            {
                // ПЛК
                UI1_combo, UI2_combo, UI3_combo, UI4_combo, UI5_combo, UI6_combo, UI7_combo,
                UI8_combo, UI9_combo, UI10_combo, UI11_combo,
                // Блок расширения 1
                UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo, UI7bl1_combo, UI8bl1_combo,
                UI9bl1_combo, UI10bl1_combo, UI11bl1_combo, UI12bl1_combo, UI13bl1_combo, UI14bl1_combo, UI15bl1_combo, UI16bl1_combo,
                // Блок расширения 2
                UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo, UI7bl2_combo, UI8bl2_combo,
                UI9bl2_combo, UI10bl2_combo, UI11bl2_combo, UI12bl2_combo, UI13bl2_combo, UI14bl2_combo, UI15bl2_combo, UI16bl2_combo,
                // Блок расширения 3
                UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo, UI7bl3_combo, UI8bl3_combo,
                UI9bl3_combo, UI10bl3_combo, UI11bl3_combo, UI12bl3_combo, UI13bl3_combo, UI14bl3_combo, UI15bl3_combo, UI16bl3_combo
            };

            // Удаление из comboBox по названию сигнала
            foreach (var el in ui_combos)
                if (name != NOT_SELECTED && el != cm)
                    el.Items.Remove(name);
        }

        ///<summary>Добавление нового UI и его назначение для переданного comboBox</summary>
        private void SelectComboBox_UI(ComboBox cm, ushort code, Label label, string text, int index, string type, ComboBox typeCombo)
        {
            string name = list_ui.Find(x => x.Code == code).Name;       // Поиск есть ли такое наименование в элементах comboBox
            if (!cm.Items.Contains(name)) cm.Items.Add(name);           // Добавление лишь когда совпадения нет
            cm.SelectedIndex = cm.Items.Count - 1;                      // Выбор последнего элемента
            text = cm.SelectedItem.ToString();                          // Сохранение названия выбранного элемента
            index = cm.SelectedIndex;                                   // Сохранение выбранного индекса
            if (showCode)
            {
                if (type == DI)                                         // Для дискретного входа
                    label.Text = code.ToString();
                else if (type == NTC)
                    label.Text = code.ToString();                       // Аналоговый вход, тип NTC
                else if (type == mA_4_20)
                    label.Text = (code + 100).ToString();               // Аналоговый вход, тип 4-20 мА
            }
            if (type != DI)
            {
                typeCombo.Enabled = true;                               // Разблокировка comboBox для выбора типа сигнала
                typeCombo.SelectedIndex = 0;
                if (typeCombo.Items.Contains(DI)) typeCombo.Items.Remove(DI);
            } 
            else
            {
                typeCombo.Enabled = false;
                if (!typeCombo.Items.Contains(DI)) typeCombo.Items.Add(DI);
            }
            list_ui.Find(x => x.Code == code).Select();
        }

        ///<summary>Добавление нового UI и его назначение под первый нераспределённый выход</summary>
        private void AddNewUI(ushort code, string type)
        {
            initialConfigure = false;                                   // Сброс признака начальной расстановки

            var ui_combos_plk = new List<ComboBox>()
            {
                UI1_combo, UI2_combo, UI3_combo, UI4_combo, UI5_combo, UI6_combo, UI7_combo, UI8_combo, UI9_combo, UI10_combo, UI11_combo
            };

            var ui_combos_blocks = new List<ComboBox>()
            {
                // Блок расширения 1
                UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo, UI7bl1_combo, UI8bl1_combo,
                UI9bl1_combo, UI10bl1_combo, UI11bl1_combo, UI12bl1_combo, UI13bl1_combo, UI14bl1_combo, UI15bl1_combo, UI16bl1_combo,
                // Блок расширения 2
                UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo, UI7bl2_combo, UI8bl2_combo,
                UI9bl2_combo, UI10bl2_combo, UI11bl2_combo, UI12bl2_combo, UI13bl2_combo, UI14bl2_combo, UI15bl2_combo, UI16bl2_combo,
                // Блок расширения 3
                UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo, UI7bl3_combo, UI8bl3_combo,
                UI9bl3_combo, UI10bl3_combo, UI11bl3_combo, UI12bl3_combo, UI13bl3_combo, UI14bl3_combo, UI15bl3_combo, UI16bl3_combo
            };
            
            var blocks = CalcExpBlocks_typeNums();                      // Определение типов и количества блоков расширения

            RemoveThirdBlockUI_M72E16NA(blocks);                        // Проверка на удаление 3-го блока расширения UI
            RemoveSecondBlockUI_M72E16NA(blocks);                       // Проверка на удаление 2-го блока расширения UI
            RemoveFirstBlockUI_M72E16NA(blocks);                        // Проверка на удаление 1-го блока расширения UI

            RemoveThirdBlockDO_M72E08RA(blocks);                        // Проверка на удаление 3-го блока расширения DO
            RemoveSecondBlockDO_M72E08RA(blocks);                       // Проверка на удаление 2-го блока расширения DO
            RemoveFirstBlockDO_M72E08RA(blocks);                        // Проверка на удаление 1-го блока расширения DO

            AddFirstBlock_DOUI_M72E12RA(blocks);                        // Проверка добавления 1-го блока расширения M72E12RA (DO + UI)  
            AddSecondBlock_DOUI_M72E12RA(blocks);                       // Проверка добавления 2-го блока расширения M72E12RA (DO + UI)
            AddThirdBlock_DOUI_M72E12RA(blocks);                        // Проверка добавления 3-го блока расширения M72E12RA (DO + UI)

            AddFirstBlockUI_M72E16NA(blocks);                           // Проверка добавления 1-го блока расширения M72E16NA (UI)
            AddSecondBlockUI_M72E16NA(blocks);                          // Проверка добавления 2-го блока расширения M72E16NA (UI)
            AddThirdBlockUI_M72E16NA(blocks);                           // Проверка добавления 3-го блока расширения M72E16NA (UI)

            bool sensor = type != DI;                                   // Признак температурного датчика к распределению
            bool isAllocated = false;                                   // Признак распределенного сигнала температурного датчика
            Ui signalUI = null;                                         // Сигнал для распределения (после замещения датчиком) 

            do  // Алгоритм для распределения сигналов датчиков на ПЛК, с распределением ранее выбранного сигнала DI
            {
                // ПЛК, сигнал UI1 - пустой comboBox или (сигнал датчика + не распределен + выбранный сигнал DI)
                if (UI1_combo.SelectedIndex == 0 || (sensor && !isAllocated && UI1_typeCombo.SelectedItem.ToString() == DI))
                {
                    // Проверка выбранного сигнала на comboBox
                    string name = UI1_combo.SelectedItem.ToString();
                    signalUI = list_ui.Find(x => x.Name == name);

                    // Свободный слот или (сигнал DI + распределяется датчик)
                    if (signalUI == null || (signalUI.Type == DI && !isAllocated))
                    {
                        SelectComboBox_UI(UI1_combo, code, UI1_lab, UI1combo_text, UI1combo_index, type, UI1_typeCombo);
                        isAllocated = true;         // Сигнал датчика распределён
                        sensor = false;             // Сброс признака датчика

                        if (signalUI != null)       // Ранее на этом месте был выбранный сигнал DI
                        {
                            code = signalUI.Code; type = signalUI.Type;
                            continue;
                        }
                    }
                }
                // ПЛК, сигнал UI2 - пустой comboBox или (сигнал датчика + не распределен + выбранный сигнал DI)
                else if (UI2_combo.SelectedIndex == 0 || (sensor && !isAllocated && UI2_typeCombo.SelectedItem.ToString() == DI))
                {
                    // Проверка выбранного сигнала на comboBox
                    string name = UI2_combo.SelectedItem.ToString();
                    signalUI = list_ui.Find(x => x.Name == name);

                    // Свободный слот или (сигнал DI + распределяется датчик)
                    if (signalUI == null || (signalUI.Type == DI && !isAllocated))
                    {
                        SelectComboBox_UI(UI2_combo, code, UI2_lab, UI2combo_text, UI2combo_index, type, UI2_typeCombo);
                        isAllocated = true;         // Сигнал датчика распределён
                        sensor = false;             // Сброс признака датчика

                        if (signalUI != null)       // Ранее на этом месте был выбранный сигнал DI
                        {
                            code = signalUI.Code; type = signalUI.Type;
                            continue;
                        }
                    }
                }
                // ПЛК, сигнал UI3 - пустой comboBox или (сигнал датчика + не распределен + выбранный сигнал DI)
                else if (UI3_combo.SelectedIndex == 0 || (sensor && !isAllocated && UI3_typeCombo.SelectedItem.ToString() == DI))
                {
                    // Проверка выбранного сигнала на comboBox
                    string name = UI3_combo.SelectedItem.ToString();
                    signalUI = list_ui.Find(x => x.Name == name);

                    // Свободный слот или (сигнал DI + распределяется датчик)
                    if (signalUI == null || (signalUI.Type == DI && !isAllocated))
                    {
                        SelectComboBox_UI(UI3_combo, code, UI3_lab, UI3combo_text, UI3combo_index, type, UI3_typeCombo);
                        isAllocated = true;         // Сигнал датчика распределён
                        sensor = false;             // Сброс признака датчика

                        if (signalUI != null)       // Ранее на этом месте был выбранный сигнал DI
                        {
                            code = signalUI.Code; type = signalUI.Type;
                            continue;
                        }
                    }
                }
                // ПЛК, сигнал UI4 - пустой comboBox или (сигнал датчика + не распределен + выбранный сигнал DI)
                else if (UI4_combo.SelectedIndex == 0 || (sensor && !isAllocated && UI4_typeCombo.SelectedItem.ToString() == DI))
                {
                    // Проверка выбранного сигнала на comboBox
                    string name = UI4_combo.SelectedItem.ToString();
                    signalUI = list_ui.Find(x => x.Name == name);

                    // Свободный слот или (сигнал DI + распределяется датчик)
                    if (signalUI == null || (signalUI.Type == DI && !isAllocated))
                    {
                        SelectComboBox_UI(UI4_combo, code, UI4_lab, UI4combo_text, UI4combo_index, type, UI4_typeCombo);
                        isAllocated = true;         // Сигнал датчика распределён
                        sensor = false;             // Сброс признака датчика

                        if (signalUI != null)       // Ранее на этом месте был выбранный сигнал DI
                        {
                            code = signalUI.Code; type = signalUI.Type;
                            continue;
                        }
                    }
                }
                // ПЛК, сигнал UI5 - пустой comboBox или (сигнал датчика + не распределен + выбранный сигнал DI)
                else if (UI5_combo.SelectedIndex == 0 || (sensor && !isAllocated && UI5_typeCombo.SelectedItem.ToString() == DI))
                {
                    // Проверка выбранного сигнала на comboBox
                    string name = UI5_combo.SelectedItem.ToString();
                    signalUI = list_ui.Find(x => x.Name == name);

                    // Свободный слот или (сигнал DI + распределяется датчик)
                    if (signalUI == null || (signalUI.Type == DI && !isAllocated))
                    {
                        SelectComboBox_UI(UI5_combo, code, UI5_lab, UI5combo_text, UI5combo_index, type, UI5_typeCombo);
                        isAllocated = true;         // Сигнал датчика распределён
                        sensor = false;             // Сброс признака датчика

                        if (signalUI != null)       // Ранее на этом месте был выбранный сигнал DI
                        {
                            code = signalUI.Code; type = signalUI.Type;
                            continue;
                        }
                    }
                }
                // ПЛК, сигнал UI6 - пустой comboBox или (сигнал датчика + не распределен + выбранный сигнал DI)
                else if (UI6_combo.SelectedIndex == 0 || (sensor && !isAllocated && UI6_typeCombo.SelectedItem.ToString() == DI))
                {
                    // Проверка выбранного сигнала на comboBox
                    string name = UI6_combo.SelectedItem.ToString();
                    signalUI = list_ui.Find(x => x.Name == name);

                    // Свободный слот или (сигнал DI + распределяется датчик)
                    if (signalUI == null || (signalUI.Type == DI && !isAllocated))
                    {
                        SelectComboBox_UI(UI6_combo, code, UI6_lab, UI6combo_text, UI6combo_index, type, UI6_typeCombo);
                        isAllocated = true;         // Сигнал датчика распределён
                        sensor = false;             // Сброс признака датчика

                        if (signalUI != null)       // Ранее на этом месте был выбранный сигнал DI
                        {
                            code = signalUI.Code; type = signalUI.Type;
                            continue;
                        }
                    }
                }
                // ПЛК, сигнал UI7 - пустой comboBox или (сигнал датчика + не распределен + выбранный сигнал DI или выбран ПЛК Mini)
                else if (UI7_combo.SelectedIndex == 0 || (sensor && !isAllocated && 
                    (UI7_typeCombo.SelectedItem.ToString() == DI || comboPlkType.SelectedIndex == 0)))
                {
                    // Проверка выбранного сигнала на comboBox
                    string name = UI7_combo.SelectedItem.ToString();
                    signalUI = list_ui.Find(x => x.Name == name);

                    // Свободный слот или (сигнал DI + распределяется датчик)
                    if (signalUI == null || (signalUI.Type == DI && !isAllocated))
                    {
                        SelectComboBox_UI(UI7_combo, code, UI7_lab, UI7combo_text, UI7combo_index, type, UI7_typeCombo);
                        isAllocated = true;         // Сигнал датчика распределён
                        sensor = false;             // Сброс признака датчика

                        if (signalUI != null)       // Ранее на этом месте был выбранный сигнал DI
                        {
                            code = signalUI.Code; type = signalUI.Type;
                            continue;
                        }
                    }
                    // Слот занят, датчик не распределён (проверка для ПЛК Mini)
                    else if (signalUI.Type != DI && !isAllocated)
                    {
                        // Выбор ПЛК Optimized, если был выбран ПЛК Mini
                        if (comboPlkType.SelectedIndex == 0)
                        {
                            comboPlkType.SelectedIndex = 1;     // Выбор ПЛК Optimized
                            optimizeOnly = true;                // Установка признака блокировки ПЛК Optimize
                            // Блокировка выбора типа ПЛК comboBox
                            comboPlkType.Enabled = false; comboPlkType_copy.Enabled = false;       
                        }
                        continue;
                    }
                }
                // ПЛК, сигнал UI8 - пустой comboBox или (сигнал датчика + не распределен + выбранный сигнал DI)
                else if (UI8_combo.Enabled && (UI8_combo.SelectedIndex == 0 ||
                    (sensor && !isAllocated && UI8_typeCombo.SelectedItem.ToString() == DI)))
                {
                    // Проверка выбранного сигнала на comboBox
                    string name = UI8_combo.SelectedItem.ToString();
                    signalUI = list_ui.Find(x => x.Name == name);

                    // Свободный слот или (сигнал DI + распределяется датчик)
                    if (signalUI == null || (signalUI.Type == DI && !isAllocated))
                    {
                        SelectComboBox_UI(UI8_combo, code, UI8_lab, UI8combo_text, UI8combo_index, type, UI8_typeCombo);
                        isAllocated = true;         // Сигнал датчика распределён
                        sensor = false;             // Сброс признака датчика

                        if (signalUI != null)       // Ранее на этом месте был выбранный сигнал DI
                        {
                            code = signalUI.Code; type = signalUI.Type;
                            continue;
                        }
                    }
                }
                // ПЛК, сигнал UI9 - пустой comboBox или (сигнал датчика + не распределен + выбранный сигнал DI)
                else if (UI9_combo.Enabled && (UI9_combo.SelectedIndex == 0 ||
                    (sensor && !isAllocated && UI9_typeCombo.SelectedItem.ToString() == DI)))
                {
                    // Проверка выбранного сигнала на comboBox
                    string name = UI9_combo.SelectedItem.ToString();
                    signalUI = list_ui.Find(x => x.Name == name);

                    // Свободный слот или (сигнал DI + распределяется датчик)
                    if (signalUI == null || (signalUI.Type == DI && !isAllocated))
                    {
                        SelectComboBox_UI(UI9_combo, code, UI9_lab, UI9combo_text, UI9combo_index, type, UI9_typeCombo);
                        isAllocated = true;         // Сигнал датчика распределён
                        sensor = false;             // Сброс признака датчика

                        if (signalUI != null)       // Ранее на этом месте был выбранный сигнал DI
                        {
                            code = signalUI.Code; type = signalUI.Type;
                            continue;
                        }
                    }
                }
                // ПЛК, сигнал UI10 - пустой comboBox или (сигнал датчика + не распределен + выбранный сигнал DI)
                else if (UI10_combo.Enabled && (UI10_combo.SelectedIndex == 0 ||
                    (sensor && !isAllocated && UI10_typeCombo.SelectedItem.ToString() == DI)))
                {
                    // Проверка выбранного сигнала на comboBox
                    string name = UI10_combo.SelectedItem.ToString();
                    signalUI = list_ui.Find(x => x.Name == name);

                    // Свободный слот или (сигнал DI + распределяется датчик)
                    if (signalUI == null || (signalUI.Type == DI && !isAllocated))
                    {
                        SelectComboBox_UI(UI10_combo, code, UI10_lab, UI10combo_text, UI10combo_index, type, UI10_typeCombo);
                        isAllocated = true;         // Сигнал датчика распределён
                        sensor = false;             // Сброс признака датчика

                        if (signalUI != null)       // Ранее на этом месте был выбранный сигнал DI
                        {
                            code = signalUI.Code; type = signalUI.Type;
                            continue;
                        }
                    }
                }
                // ПЛК, сигнал UI11 - пустой comboBox или (сигнал датчика + не распределен + выбранный сигнал DI)
                else if (UI11_combo.Enabled && (UI11_combo.SelectedIndex == 0 ||
                    (sensor && !isAllocated && UI11_typeCombo.SelectedItem.ToString() == DI)))
                {
                    // Проверка выбранного сигнала на comboBox
                    string name = UI11_combo.SelectedItem.ToString();
                    signalUI = list_ui.Find(x => x.Name == name);

                    // Свободный слот или (сигнал DI + распределяется датчик)
                    if (signalUI == null || (signalUI.Type == DI && !isAllocated))
                    {
                        SelectComboBox_UI(UI11_combo, code, UI11_lab, UI11combo_text, UI11combo_index, type, UI11_typeCombo);
                        isAllocated = true;         // Сигнал датчика распределён
                        sensor = false;             // Сброс признака датчика

                        if (signalUI != null)       // Ранее на этом месте был выбранный сигнал DI
                        {
                            code = signalUI.Code; type = signalUI.Type;
                            continue;
                        }
                    }
                }

                // Блок расширения 1
                else if (UI1bl1_combo.Enabled && UI1bl1_combo.SelectedIndex == 0 && !sensor)    // UI1 блок 1
                {
                    SelectComboBox_UI(UI1bl1_combo, code, UI1bl1_lab, UI1bl1combo_text, UI1bl1combo_index, type, UI1bl1_typeCombo); break;  
                }
                else if (UI2bl1_combo.Enabled && UI2bl1_combo.SelectedIndex == 0 && !sensor)    // UI2 блок 1
                {
                    SelectComboBox_UI(UI2bl1_combo, code, UI2bl1_lab, UI2bl1combo_text, UI2bl1combo_index, type, UI2bl1_typeCombo); break;  
                }
                else if (UI3bl1_combo.Enabled && UI3bl1_combo.SelectedIndex == 0 && !sensor)    // UI3 блок 1
                {
                    SelectComboBox_UI(UI3bl1_combo, code, UI3bl1_lab, UI3bl1combo_text, UI3bl1combo_index, type, UI3bl1_typeCombo); break;
                }        
                else if (UI4bl1_combo.Enabled && UI4bl1_combo.SelectedIndex == 0 && !sensor)    // UI4 блок 1
                {
                    SelectComboBox_UI(UI4bl1_combo, code, UI4bl1_lab, UI4bl1combo_text, UI4bl1combo_index, type, UI4bl1_typeCombo); break;
                }
                else if (UI5bl1_combo.Enabled && UI5bl1_combo.SelectedIndex == 0 && !sensor)    // UI5 блок 1
                {
                    SelectComboBox_UI(UI5bl1_combo, code, UI5bl1_lab, UI5bl1combo_text, UI5bl1combo_index, type, UI5bl1_typeCombo); break;
                }
                else if (UI6bl1_combo.Enabled && UI6bl1_combo.SelectedIndex == 0 && !sensor)    // UI6 блок 1
                {
                    SelectComboBox_UI(UI6bl1_combo, code, UI6bl1_lab, UI6bl1combo_text, UI6bl1combo_index, type, UI6bl1_typeCombo); break;
                }
                else if (UI7bl1_combo.Enabled && UI7bl1_combo.SelectedIndex == 0 && !sensor)    // UI7 блок 1
                {
                    SelectComboBox_UI(UI7bl1_combo, code, UI7bl1_lab, UI7bl1combo_text, UI7bl1combo_index, type, UI7bl1_typeCombo); break;
                }
                else if (UI8bl1_combo.Enabled && UI8bl1_combo.SelectedIndex == 0 && !sensor)    // UI8 блок 1
                {
                    SelectComboBox_UI(UI8bl1_combo, code, UI8bl1_lab, UI8bl1combo_text, UI8bl1combo_index, type, UI8bl1_typeCombo); break;
                }
                else if (UI9bl1_combo.Enabled && UI9bl1_combo.SelectedIndex == 0 && !sensor)    // UI9 блок 1
                {
                    SelectComboBox_UI(UI9bl1_combo, code, UI9bl1_lab, UI9bl1combo_text, UI9bl1combo_index, type, UI9bl1_typeCombo); break;
                }
                else if (UI10bl1_combo.Enabled && UI10bl1_combo.SelectedIndex == 0 && !sensor)  // UI10 блок 1
                {
                    SelectComboBox_UI(UI10bl1_combo, code, UI10bl1_lab, UI10bl1combo_text, UI10bl1combo_index, type, UI10bl1_typeCombo); break;
                }
                else if (UI11bl1_combo.Enabled && UI11bl1_combo.SelectedIndex == 0 && !sensor)  // UI11 блок 1
                {
                    SelectComboBox_UI(UI11bl1_combo, code, UI11bl1_lab, UI11bl1combo_text, UI11bl1combo_index, type, UI11bl1_typeCombo); break;
                }
                else if (UI12bl1_combo.Enabled && UI12bl1_combo.SelectedIndex == 0 && !sensor)  // UI12 блок 1
                {
                    SelectComboBox_UI(UI12bl1_combo, code, UI12bl1_lab, UI12bl1combo_text, UI12bl1combo_index, type, UI12bl1_typeCombo); break;
                }
                else if (UI13bl1_combo.Enabled && UI13bl1_combo.SelectedIndex == 0 && !sensor)  // UI13 блок 1
                {
                    SelectComboBox_UI(UI13bl1_combo, code, UI13bl1_lab, UI13bl1combo_text, UI13bl1combo_index, type, UI13bl1_typeCombo); break;
                }
                else if (UI14bl1_combo.Enabled && UI14bl1_combo.SelectedIndex == 0 && !sensor)  // UI14 блок 1
                {
                    SelectComboBox_UI(UI14bl1_combo, code, UI14bl1_lab, UI14bl1combo_text, UI14bl1combo_index, type, UI14bl1_typeCombo); break;
                }
                else if (UI15bl1_combo.Enabled && UI15bl1_combo.SelectedIndex == 0 && !sensor)  // UI15 блок 1
                {
                    SelectComboBox_UI(UI15bl1_combo, code, UI15bl1_lab, UI15bl1combo_text, UI15bl1combo_index, type, UI15bl1_typeCombo); break;
                }
                else if (UI16bl1_combo.Enabled && UI16bl1_combo.SelectedIndex == 0 && !sensor)  // UI16 блок 1
                {
                    SelectComboBox_UI(UI16bl1_combo, code, UI16bl1_lab, UI16bl1combo_text, UI16bl1combo_index, type, UI16bl1_typeCombo); break;
                }

                // Блок расширения 2
                else if (UI1bl2_combo.Enabled && UI1bl2_combo.SelectedIndex == 0 && !sensor)    // UI1 блок 2
                {
                    SelectComboBox_UI(UI1bl2_combo, code, UI1bl2_lab, UI1bl2combo_text, UI1bl2combo_index, type, UI1bl2_typeCombo); break;
                }
                else if (UI2bl2_combo.Enabled && UI2bl2_combo.SelectedIndex == 0 && !sensor)    // UI2 блок 2
                {
                    SelectComboBox_UI(UI2bl2_combo, code, UI2bl2_lab, UI2bl2combo_text, UI2bl2combo_index, type, UI2bl2_typeCombo); break;
                }
                else if (UI3bl2_combo.Enabled && UI3bl2_combo.SelectedIndex == 0 && !sensor)    // UI3 блок 2
                {
                    SelectComboBox_UI(UI3bl2_combo, code, UI3bl2_lab, UI3bl2combo_text, UI3bl2combo_index, type, UI3bl2_typeCombo); break;
                }
                else if (UI4bl2_combo.Enabled && UI4bl2_combo.SelectedIndex == 0 && !sensor)    // UI4 блок 2
                {
                    SelectComboBox_UI(UI4bl2_combo, code, UI4bl2_lab, UI4bl2combo_text, UI4bl2combo_index, type, UI4bl2_typeCombo); break;
                }
                else if (UI5bl2_combo.Enabled && UI5bl2_combo.SelectedIndex == 0 && !sensor)    // UI5 блок 2
                {
                    SelectComboBox_UI(UI5bl2_combo, code, UI5bl2_lab, UI5bl2combo_text, UI5bl2combo_index, type, UI5bl2_typeCombo); break;
                }
                else if (UI6bl2_combo.Enabled && UI6bl2_combo.SelectedIndex == 0 && !sensor)    // UI6 блок 2
                {
                    SelectComboBox_UI(UI6bl2_combo, code, UI6bl2_lab, UI6bl2combo_text, UI6bl2combo_index, type, UI6bl2_typeCombo); break;
                }
                else if (UI7bl2_combo.Enabled && UI7bl2_combo.SelectedIndex == 0 && !sensor)    // UI7 блок 2
                {
                    SelectComboBox_UI(UI7bl2_combo, code, UI7bl2_lab, UI7bl2combo_text, UI7bl2combo_index, type, UI7bl2_typeCombo); break;
                }
                else if (UI8bl2_combo.Enabled && UI8bl2_combo.SelectedIndex == 0 && !sensor)    // UI8 блок 2
                {
                    SelectComboBox_UI(UI8bl2_combo, code, UI8bl2_lab, UI8bl2combo_text, UI8bl2combo_index, type, UI8bl2_typeCombo); break;
                }
                else if (UI9bl2_combo.Enabled && UI9bl2_combo.SelectedIndex == 0 && !sensor)    // UI9 блок 2
                {
                    SelectComboBox_UI(UI9bl2_combo, code, UI9bl2_lab, UI9bl2combo_text, UI9bl2combo_index, type, UI9bl2_typeCombo); break;
                }
                else if (UI10bl2_combo.Enabled && UI10bl2_combo.SelectedIndex == 0 && !sensor)  // UI10 блок 2
                {
                    SelectComboBox_UI(UI10bl2_combo, code, UI10bl2_lab, UI10bl2combo_text, UI10bl2combo_index, type, UI10bl2_typeCombo); break;
                }
                else if (UI11bl2_combo.Enabled && UI11bl2_combo.SelectedIndex == 0 && !sensor)  // UI11 блок 2
                {
                    SelectComboBox_UI(UI11bl2_combo, code, UI11bl2_lab, UI11bl2combo_text, UI11bl2combo_index, type, UI11bl2_typeCombo); break;
                }
                else if (UI12bl2_combo.Enabled && UI12bl2_combo.SelectedIndex == 0 && !sensor)  // UI12 блок 2
                {
                    SelectComboBox_UI(UI12bl2_combo, code, UI12bl2_lab, UI12bl2combo_text, UI12bl2combo_index, type, UI12bl2_typeCombo); break;
                }
                else if (UI13bl2_combo.Enabled && UI13bl2_combo.SelectedIndex == 0 && !sensor)  // UI13 блок 2
                {
                    SelectComboBox_UI(UI13bl2_combo, code, UI13bl2_lab, UI13bl2combo_text, UI13bl2combo_index, type, UI13bl2_typeCombo); break;
                }
                else if (UI14bl2_combo.Enabled && UI14bl2_combo.SelectedIndex == 0 && !sensor)  // UI14 блок 2
                {
                    SelectComboBox_UI(UI14bl2_combo, code, UI14bl2_lab, UI14bl2combo_text, UI14bl2combo_index, type, UI14bl2_typeCombo); break;
                }
                else if (UI15bl2_combo.Enabled && UI15bl2_combo.SelectedIndex == 0 && !sensor)  // UI15 блок 2
                {
                    SelectComboBox_UI(UI15bl2_combo, code, UI15bl2_lab, UI15bl2combo_text, UI15bl2combo_index, type, UI15bl2_typeCombo); break;
                }
                else if (UI16bl2_combo.Enabled && UI16bl2_combo.SelectedIndex == 0 && !sensor)  // UI16 блок 2
                {
                    SelectComboBox_UI(UI16bl2_combo, code, UI16bl2_lab, UI16bl2combo_text, UI16bl2combo_index, type, UI16bl2_typeCombo); break;
                }

                // Блок расширения 3
                else if (UI1bl3_combo.Enabled && UI1bl3_combo.SelectedIndex == 0 && !sensor)    // UI1 блок 3
                {
                    SelectComboBox_UI(UI1bl3_combo, code, UI1bl3_lab, UI1bl3combo_text, UI1bl3combo_index, type, UI1bl3_typeCombo); break;
                }
                else if (UI2bl3_combo.Enabled && UI2bl3_combo.SelectedIndex == 0 && !sensor)    // UI2 блок 3
                {
                    SelectComboBox_UI(UI2bl3_combo, code, UI2bl3_lab, UI2bl3combo_text, UI2bl3combo_index, type, UI2bl3_typeCombo); break;
                }
                else if (UI3bl3_combo.Enabled && UI3bl3_combo.SelectedIndex == 0 && !sensor)    // UI3 блок 3
                {
                    SelectComboBox_UI(UI3bl3_combo, code, UI3bl3_lab, UI3bl3combo_text, UI3bl3combo_index, type, UI3bl3_typeCombo); break;
                }
                else if (UI4bl3_combo.Enabled && UI4bl3_combo.SelectedIndex == 0 && !sensor)    // UI4 блок 3
                {
                    SelectComboBox_UI(UI4bl3_combo, code, UI4bl3_lab, UI4bl3combo_text, UI4bl3combo_index, type, UI4bl3_typeCombo); break;   
                }
                else if (UI5bl3_combo.Enabled && UI5bl3_combo.SelectedIndex == 0 && !sensor)    // UI5 блок 3
                {
                    SelectComboBox_UI(UI5bl3_combo, code, UI5bl3_lab, UI5bl3combo_text, UI5bl3combo_index, type, UI5bl3_typeCombo); break;
                }
                else if (UI6bl3_combo.Enabled && UI6bl3_combo.SelectedIndex == 0 && !sensor)    // UI6 блок 3
                {
                    SelectComboBox_UI(UI6bl3_combo, code, UI6bl3_lab, UI6bl3combo_text, UI6bl3combo_index, type, UI6bl3_typeCombo); break;
                }
                else if (UI7bl3_combo.Enabled && UI7bl3_combo.SelectedIndex == 0 && !sensor)    // UI7 блок 3
                {
                    SelectComboBox_UI(UI7bl3_combo, code, UI7bl3_lab, UI7bl3combo_text, UI7bl3combo_index, type, UI7bl3_typeCombo); break;
                }
                else if (UI8bl3_combo.Enabled && UI8bl3_combo.SelectedIndex == 0 && !sensor)    // UI8 блок 3
                {
                    SelectComboBox_UI(UI8bl3_combo, code, UI8bl3_lab, UI8bl3combo_text, UI8bl3combo_index, type, UI8bl3_typeCombo); break;
                }
                else if (UI9bl3_combo.Enabled && UI9bl3_combo.SelectedIndex == 0 && !sensor)    // UI9 блок 3
                {
                    SelectComboBox_UI(UI9bl3_combo, code, UI9bl3_lab, UI9bl3combo_text, UI9bl3combo_index, type, UI9bl3_typeCombo); break;
                }
                else if (UI10bl3_combo.Enabled && UI10bl3_combo.SelectedIndex == 0 && !sensor)  // UI10 блок 3
                {
                    SelectComboBox_UI(UI10bl3_combo, code, UI10bl3_lab, UI10bl3combo_text, UI10bl3combo_index, type, UI10bl3_typeCombo); break;
                }
                else if (UI11bl3_combo.Enabled && UI11bl3_combo.SelectedIndex == 0 && !sensor)  // UI11 блок 3
                {
                    SelectComboBox_UI(UI11bl3_combo, code, UI11bl3_lab, UI11bl3combo_text, UI11bl3combo_index, type, UI11bl3_typeCombo); break;
                }
                else if (UI12bl3_combo.Enabled && UI12bl3_combo.SelectedIndex == 0 && !sensor)  // UI12 блок 3
                {
                    SelectComboBox_UI(UI12bl3_combo, code, UI12bl3_lab, UI12bl3combo_text, UI12bl3combo_index, type, UI12bl3_typeCombo); break;
                }
                else if (UI13bl3_combo.Enabled && UI13bl3_combo.SelectedIndex == 0 && !sensor)  // UI13 блок 3
                {
                    SelectComboBox_UI(UI13bl3_combo, code, UI13bl3_lab, UI13bl3combo_text, UI13bl3combo_index, type, UI13bl3_typeCombo); break;
                }
                else if (UI14bl3_combo.Enabled && UI14bl3_combo.SelectedIndex == 0 && !sensor)  // UI14 блок 3
                {
                    SelectComboBox_UI(UI14bl3_combo, code, UI14bl3_lab, UI14bl3combo_text, UI14bl3combo_index, type, UI14bl3_typeCombo); break;
                }
                else if (UI15bl3_combo.Enabled && UI15bl3_combo.SelectedIndex == 0 && !sensor)  // UI15 блок 3
                {
                    SelectComboBox_UI(UI15bl3_combo, code, UI15bl3_lab, UI15bl3combo_text, UI15bl3combo_index, type, UI15bl3_typeCombo); break;
                }   
                else if (UI16bl3_combo.Enabled && UI16bl3_combo.SelectedIndex == 0 && !sensor)  // UI16 блок 3
                {
                    SelectComboBox_UI(UI16bl3_combo, code, UI16bl3_lab, UI16bl3combo_text, UI16bl3combo_index, type, UI16bl3_typeCombo); break;
                }
                else if (isAutoSelect) break;  // Не удалось распределить сигнал, автоматический алгоритм
                else                           // Ручной алгоритм, добавление сигнала к другим comboBox
                {
                    Ui ui_find = list_ui.Find(x => x.Code == code);
                    bool isFound = false;

                    if (ui_find != null)
                    {
                        string name = ui_find.Name;                             // Получение имени сигнала
                        foreach (var el in ui_combos_plk)                       // Добавление к сигналам ПЛК, все типы сигналов
                        {
                            if (el.Items.Contains(name)) isFound = true;
                            if (!isFound) el.Items.Add(name);
                            isFound = false;
                        }
                        if (!sensor)                                            // Если не сигнал датчика, обычный DI
                        {
                            foreach (var el in ui_combos_blocks)                // Добавление к сигналам блоков расширения
                            {
                                if (el.Items.Contains(name)) isFound = true;
                                if (!isFound) el.Items.Add(name);
                                isFound = false;
                            }
                        }
                    }
                    break;
                }

            } while (signalUI != null);     // Пока есть сигнал к распределению

            CheckSignalsReady();            // Проверка распределения сигналов
        }

        ///<summary>Удаление UI из определённого comboBox</summary>
        private void RemoveUI_FromComboBox(ComboBox cm, string name, ref Label label, ref string text, ref int index, ComboBox typeCombo)
        {
            Ui find_ui;                                                                             // UI вход для поиска

            for (int i = 0; i < cm.Items.Count; i++)
                if (cm.Items[i].ToString() == name)                                                 // Есть совпадение по имени в списке
                {
                    cm.Items.Remove(name);                                                          // Удаление элемента по имени
                    if (!typeCombo.Items.Contains(DI))
                    {
                        typeCombo.Items.Add(DI);                                                    // Добавление варианта DI, если был удален ранее
                    }
                    typeCombo.SelectedItem = DI; typeCombo.Enabled = false;                         // Блокировка и выбор DI по умолчанию typeCombo

                    if (cm.Items.Count > 1)                                                         // Осталось больше одного элемента в списке
                    {
                        cm.SelectedIndex = cm.Items.Count - 1;                                      // Выбор последнего элемента
                        if (cm.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosUI(cm.SelectedItem.ToString(), cm);                        // Удаление из других comboBox выбранного элемента
                            find_ui = list_ui.Find(x => x.Name == cm.SelectedItem.ToString());
                            if (find_ui != null)
                            {
                                list_ui.Remove(find_ui);
                                if (showCode) label.Text = find_ui.Code.ToString();
                            }
                        }
                    }
                    else                                                                            // Только "Не выбрано"
                    {
                        cm.SelectedItem = NOT_SELECTED;
                        label.Text = "";
                    }
                    text = cm.SelectedItem.ToString();                                              // Сохранение наименования выбранного UI
                    index = cm.SelectedIndex;                                                       // Сохранение индекса выбранного UI
                }
        }

        ///<summary>Удаление UI из всех comboBox</summary>
        private void SubFromCombosUI(ushort code)
        {
            string name = "";
            Ui find_ui = list_ui.Find(x => x.Code == code);
            if (find_ui != null) name = find_ui.Name;
            else return;

            subUIcondition = true;      // Признак удаления UI, не работает событие indexChanged                                                    

            // ПЛК (до 11 UI входов)
            RemoveUI_FromComboBox(UI1_combo, name, ref UI1_lab, ref UI1combo_text, ref UI1combo_index, UI1_typeCombo);          // UI1 ПЛК
            RemoveUI_FromComboBox(UI2_combo, name, ref UI2_lab, ref UI2combo_text, ref UI2combo_index, UI2_typeCombo);          // UI2 ПЛК
            RemoveUI_FromComboBox(UI3_combo, name, ref UI3_lab, ref UI3combo_text, ref UI3combo_index, UI3_typeCombo);          // UI3 ПЛК
            RemoveUI_FromComboBox(UI4_combo, name, ref UI4_lab, ref UI4combo_text, ref UI4combo_index, UI4_typeCombo);          // UI4 ПЛК
            RemoveUI_FromComboBox(UI5_combo, name, ref UI5_lab, ref UI5combo_text, ref UI5combo_index, UI5_typeCombo);          // UI5 ПЛК
            RemoveUI_FromComboBox(UI6_combo, name, ref UI6_lab, ref UI6combo_text, ref UI6combo_index, UI6_typeCombo);          // UI6 ПЛК
            RemoveUI_FromComboBox(UI7_combo, name, ref UI7_lab, ref UI7combo_text, ref UI7combo_index, UI7_typeCombo);          // UI7 ПЛК
            RemoveUI_FromComboBox(UI8_combo, name, ref UI8_lab, ref UI8combo_text, ref UI8combo_index, UI8_typeCombo);          // UI8 ПЛК
            RemoveUI_FromComboBox(UI9_combo, name, ref UI9_lab, ref UI9combo_text, ref UI9combo_index, UI9_typeCombo);          // UI9 ПЛК
            RemoveUI_FromComboBox(UI10_combo, name, ref UI10_lab, ref UI10combo_text, ref UI10combo_index, UI10_typeCombo);     // UI10 ПЛК
            RemoveUI_FromComboBox(UI11_combo, name, ref UI11_lab, ref UI11combo_text, ref UI11combo_index, UI11_typeCombo);     // UI11 ПЛК
            
            // Блок расширения 1 (до 16 UI входов)
            RemoveUI_FromComboBox(UI1bl1_combo, name, ref UI1bl1_lab, ref UI1bl1combo_text, ref UI1bl1combo_index, UI1bl1_typeCombo);           // UI1 блок 1
            RemoveUI_FromComboBox(UI2bl1_combo, name, ref UI2bl1_lab, ref UI2bl1combo_text, ref UI2bl1combo_index, UI2bl1_typeCombo);           // UI2 блок 1
            RemoveUI_FromComboBox(UI3bl1_combo, name, ref UI3bl1_lab, ref UI3bl1combo_text, ref UI3bl1combo_index, UI3bl1_typeCombo);           // UI3 блок 1
            RemoveUI_FromComboBox(UI4bl1_combo, name, ref UI4bl1_lab, ref UI4bl1combo_text, ref UI4bl1combo_index, UI4bl1_typeCombo);           // UI4 блок 1
            RemoveUI_FromComboBox(UI5bl1_combo, name, ref UI5bl1_lab, ref UI5bl1combo_text, ref UI5bl1combo_index, UI5bl1_typeCombo);           // UI5 блок 1
            RemoveUI_FromComboBox(UI6bl1_combo, name, ref UI6bl1_lab, ref UI6bl1combo_text, ref UI6bl1combo_index, UI6bl1_typeCombo);           // UI6 блок 1
            RemoveUI_FromComboBox(UI7bl1_combo, name, ref UI7bl1_lab, ref UI7bl1combo_text, ref UI7bl1combo_index, UI7bl1_typeCombo);           // UI7 блок 1
            RemoveUI_FromComboBox(UI8bl1_combo, name, ref UI8bl1_lab, ref UI8bl1combo_text, ref UI8bl1combo_index, UI8bl1_typeCombo);           // UI8 блок 1
            RemoveUI_FromComboBox(UI9bl1_combo, name, ref UI9bl1_lab, ref UI9bl1combo_text, ref UI9bl1combo_index, UI9bl1_typeCombo);           // UI9 блок 1
            RemoveUI_FromComboBox(UI10bl1_combo, name, ref UI10bl1_lab, ref UI10bl1combo_text, ref UI10bl1combo_index, UI10bl1_typeCombo);      // UI10 блок 1
            RemoveUI_FromComboBox(UI11bl1_combo, name, ref UI11bl1_lab, ref UI11bl1combo_text, ref UI11bl1combo_index, UI11bl1_typeCombo);      // UI11 блок 1
            RemoveUI_FromComboBox(UI12bl1_combo, name, ref UI12bl1_lab, ref UI12bl1combo_text, ref UI12bl1combo_index, UI12bl1_typeCombo);      // UI12 блок 1
            RemoveUI_FromComboBox(UI13bl1_combo, name, ref UI13bl1_lab, ref UI13bl1combo_text, ref UI13bl1combo_index, UI13bl1_typeCombo);      // UI13 блок 1
            RemoveUI_FromComboBox(UI14bl1_combo, name, ref UI14bl1_lab, ref UI14bl1combo_text, ref UI14bl1combo_index, UI14bl1_typeCombo);      // UI14 блок 1
            RemoveUI_FromComboBox(UI15bl1_combo, name, ref UI15bl1_lab, ref UI15bl1combo_text, ref UI15bl1combo_index, UI15bl1_typeCombo);      // UI15 блок 1
            RemoveUI_FromComboBox(UI16bl1_combo, name, ref UI16bl1_lab, ref UI16bl1combo_text, ref UI16bl1combo_index, UI16bl1_typeCombo);      // UI16 блок 1
            
            // Блок расширения 2 (до 16 UI входов)
            RemoveUI_FromComboBox(UI1bl2_combo, name, ref UI1bl2_lab, ref UI1bl2combo_text, ref UI1bl2combo_index, UI1bl2_typeCombo);           // UI1 блок 2
            RemoveUI_FromComboBox(UI2bl2_combo, name, ref UI2bl2_lab, ref UI2bl2combo_text, ref UI2bl2combo_index, UI2bl2_typeCombo);           // UI2 блок 2
            RemoveUI_FromComboBox(UI3bl2_combo, name, ref UI3bl2_lab, ref UI3bl2combo_text, ref UI3bl2combo_index, UI3bl2_typeCombo);           // UI3 блок 2
            RemoveUI_FromComboBox(UI4bl2_combo, name, ref UI4bl2_lab, ref UI4bl2combo_text, ref UI4bl2combo_index, UI4bl2_typeCombo);           // UI4 блок 2
            RemoveUI_FromComboBox(UI5bl2_combo, name, ref UI5bl2_lab, ref UI5bl2combo_text, ref UI5bl2combo_index, UI5bl2_typeCombo);           // UI5 блок 2
            RemoveUI_FromComboBox(UI6bl2_combo, name, ref UI6bl2_lab, ref UI6bl2combo_text, ref UI6bl2combo_index, UI6bl2_typeCombo);           // UI6 блок 2
            RemoveUI_FromComboBox(UI7bl2_combo, name, ref UI7bl2_lab, ref UI7bl2combo_text, ref UI7bl2combo_index, UI7bl2_typeCombo);           // UI7 блок 2
            RemoveUI_FromComboBox(UI8bl2_combo, name, ref UI8bl2_lab, ref UI8bl2combo_text, ref UI8bl2combo_index, UI8bl2_typeCombo);           // UI8 блок 2
            RemoveUI_FromComboBox(UI9bl2_combo, name, ref UI9bl2_lab, ref UI9bl2combo_text, ref UI9bl2combo_index, UI9bl2_typeCombo);           // UI9 блок 2
            RemoveUI_FromComboBox(UI10bl2_combo, name, ref UI10bl2_lab, ref UI10bl2combo_text, ref UI10bl2combo_index, UI10bl2_typeCombo);      // UI10 блок 2
            RemoveUI_FromComboBox(UI11bl2_combo, name, ref UI11bl2_lab, ref UI11bl2combo_text, ref UI11bl2combo_index, UI11bl2_typeCombo);      // UI11 блок 2
            RemoveUI_FromComboBox(UI12bl2_combo, name, ref UI12bl2_lab, ref UI12bl2combo_text, ref UI12bl2combo_index, UI12bl2_typeCombo);      // UI12 блок 2
            RemoveUI_FromComboBox(UI13bl2_combo, name, ref UI13bl2_lab, ref UI13bl2combo_text, ref UI13bl2combo_index, UI13bl2_typeCombo);      // UI13 блок 2
            RemoveUI_FromComboBox(UI14bl2_combo, name, ref UI14bl2_lab, ref UI14bl2combo_text, ref UI14bl2combo_index, UI14bl2_typeCombo);      // UI14 блок 2
            RemoveUI_FromComboBox(UI15bl2_combo, name, ref UI15bl2_lab, ref UI15bl2combo_text, ref UI15bl2combo_index, UI15bl2_typeCombo);      // UI15 блок 2
            RemoveUI_FromComboBox(UI16bl2_combo, name, ref UI16bl2_lab, ref UI16bl2combo_text, ref UI16bl2combo_index, UI16bl2_typeCombo);      // UI16 блок 2
            
            // Блок расширения 3 (до 16 UI входов)
            RemoveUI_FromComboBox(UI1bl3_combo, name, ref UI1bl3_lab, ref UI1bl3combo_text, ref UI1bl3combo_index, UI1bl3_typeCombo);           // UI1 блок 3
            RemoveUI_FromComboBox(UI2bl3_combo, name, ref UI2bl3_lab, ref UI2bl3combo_text, ref UI2bl3combo_index, UI2bl3_typeCombo);           // UI2 блок 3
            RemoveUI_FromComboBox(UI3bl3_combo, name, ref UI3bl3_lab, ref UI3bl3combo_text, ref UI3bl3combo_index, UI3bl3_typeCombo);           // UI3 блок 3
            RemoveUI_FromComboBox(UI4bl3_combo, name, ref UI4bl3_lab, ref UI4bl3combo_text, ref UI4bl3combo_index, UI4bl3_typeCombo);           // UI4 блок 3
            RemoveUI_FromComboBox(UI5bl3_combo, name, ref UI5bl3_lab, ref UI5bl3combo_text, ref UI5bl3combo_index, UI5bl3_typeCombo);           // UI5 блок 3
            RemoveUI_FromComboBox(UI6bl3_combo, name, ref UI6bl3_lab, ref UI6bl3combo_text, ref UI6bl3combo_index, UI6bl3_typeCombo);           // UI6 блок 3
            RemoveUI_FromComboBox(UI7bl3_combo, name, ref UI7bl3_lab, ref UI7bl3combo_text, ref UI7bl3combo_index, UI7bl3_typeCombo);           // UI7 блок 3
            RemoveUI_FromComboBox(UI8bl3_combo, name, ref UI8bl3_lab, ref UI8bl3combo_text, ref UI8bl3combo_index, UI8bl3_typeCombo);           // UI8 блок 3
            RemoveUI_FromComboBox(UI9bl3_combo, name, ref UI9bl3_lab, ref UI9bl3combo_text, ref UI9bl3combo_index, UI9bl3_typeCombo);           // UI9 блок 3
            RemoveUI_FromComboBox(UI10bl3_combo, name, ref UI10bl3_lab, ref UI10bl3combo_text, ref UI10bl3combo_index, UI10bl3_typeCombo);      // UI10 блок 3
            RemoveUI_FromComboBox(UI11bl3_combo, name, ref UI11bl3_lab, ref UI11bl3combo_text, ref UI11bl3combo_index, UI11bl3_typeCombo);      // UI11 блок 3
            RemoveUI_FromComboBox(UI12bl3_combo, name, ref UI12bl3_lab, ref UI12bl3combo_text, ref UI12bl3combo_index, UI12bl3_typeCombo);      // UI12 блок 3
            RemoveUI_FromComboBox(UI13bl3_combo, name, ref UI13bl3_lab, ref UI13bl3combo_text, ref UI13bl3combo_index, UI13bl3_typeCombo);      // UI13 блок 3
            RemoveUI_FromComboBox(UI14bl3_combo, name, ref UI14bl3_lab, ref UI14bl3combo_text, ref UI14bl3combo_index, UI14bl3_typeCombo);      // UI14 блок 3
            RemoveUI_FromComboBox(UI15bl3_combo, name, ref UI15bl3_lab, ref UI15bl3combo_text, ref UI15bl3combo_index, UI15bl3_typeCombo);      // UI15 блок 3
            RemoveUI_FromComboBox(UI16bl3_combo, name, ref UI16bl3_lab, ref UI16bl3combo_text, ref UI16bl3combo_index, UI16bl3_typeCombo);      // UI16 блок 3

            subUIcondition = false;         // Сброс признака удаления UI
            list_ui.Remove(find_ui);        // Удаление сигнала из списка UI

            var sensors_count = list_ui.Where(x => x.Type != DI).Count();       // Количество выбранных датчиков
            if (sensors_count <= 7) optimizeOnly = false;                       // Сброс признака только Optimize ПЛК

            var blocks = CalcExpBlocks_typeNums();      // Определение типов и количества блоков расширения после удаления

            RemoveThirdBlock_DOUI_M72E12RA(blocks);     // Проверка на удаление 3-го блока расширения DO + UI 
            RemoveSecondBlock_DOUI_M72E12RA(blocks);    // Проверка на удаление 2-го блока расширения DO + UI 
            RemoveFirstBlock_DOUI_M72E12RA(blocks);     // Проверка на удаление 1-го блока расширения DO + UI 

            RemoveThirdBlockUI_M72E16NA(blocks);        // Проверка на удаление 3-го блока расширения UI
            RemoveSecondBlockUI_M72E16NA(blocks);       // Проверка на удаление 2-го блока расширения UI
            RemoveFirstBlockUI_M72E16NA(blocks);        // Проверка на удаление 1-го блока расширения UI

            AddFirstBlockDO_M72E08RA(blocks);           // Проверка на добавление 1-го блока расширения DO
            AddSecondBlockDO_M72E08RA(blocks);          // Проверка на добавление 1-го блока расширения DO
            AddThirdBlockDO_M72E08RA(blocks);           // Проверка на добавление 1-го блока расширения DO

            AddFirstBlockUI_M72E16NA(blocks);           // Проверка на добавление 1-го блока расширения UI
            AddSecondBlockUI_M72E16NA(blocks);          // Проверка на добавление 2-го блока расширения UI
            AddThirdBlockUI_M72E16NA(blocks);           // Проверка на добавление 3-го блока расширения UI

            CheckSignalsReady();                        // Проверка распределения сигналов
        }

        ///<summary>Установка числового кода в зависимости от типа UI сигнала</summary>
        private void SetCodeLabel_UI(Ui ui_find, Label label)
        {
            if (ui_find.Type == DI)                                         // Для дискретного входа, DI
                label.Text = ui_find.Code.ToString();
            else if (ui_find.Type == NTC)                                   // Аналоговый вход, тип NTC
                label.Text = ui_find.Code.ToString();
            else if (ui_find.Type == mA_4_20)                               // Аналоговый вход, тип 4-20 мА
                label.Text = (ui_find.Code + 100).ToString();
        }

        ///<summary>Метод для изменения UI comboBox</summary>
        private void UI_combo_SelectedIndexChanged(ComboBox comboBox, ref int combo_index, ref string combo_text, Label label, ComboBox typeCombo)
        {
            if (ignoreEvents) return;
            string name = "";
            Ui ui_find = null;
            if (subUIcondition) return;                                             // Переход из вычета сигналов UI
            if (comboBox.SelectedIndex == combo_index) return;                      // Индекс не поменялся
            if (comboBox.SelectedIndex == 0)                                        // Выбрали "Не выбрано"
            {
                if (comboBox.Items.Count > 1)                                       // Больше одного элемента в списке
                {
                    string nameFind = combo_text;
                    ui_find = list_ui.Find(x => x.Name == nameFind);
                    if (!typeCombo.Items.Contains(DI)) typeCombo.Items.Add(DI);
                    typeCombo.Enabled = false; typeCombo.SelectedIndex = 2;         // typeCombo блокировка и выбора типа DI

                    list_ui.Remove(ui_find);                                        // Удаление сигнала из списка
                    if (showCode) label.Text = "";
                }
                if (ui_find != null)                                                // Найден элемент
                {
                    ui_find.Dispose();                                              // Освобождение сигнала для распределения
                    if (!list_ui.Contains(ui_find)) list_ui.Add(ui_find);           // Добавление сигнала в список как нераспределённого
                    if (!initialComboSignals) AddToCombosUI(ui_find, comboBox);     // Добавление к другим UI
                }
            }
            else                                                                    // Если выбран сигнал UI
            {
                name = string.Concat(comboBox.SelectedItem);
                ui_find = list_ui.Find(x => x.Name == name);
                list_ui.Remove(list_ui.Find(x => x.Name == name));                  // Удаление из списка UI

                if (ui_find != null)
                {
                    if (ui_find.Type != DI)
                    {
                        typeCombo.SelectedIndex = 0;
                        if (typeCombo.Items.Contains(DI)) typeCombo.Items.Remove(DI);
                        ui_find.SetType(NTC);                                       // Установка типа NTC для AI сигнала при выборе
                        typeCombo.Enabled = true;                                   // typeCombo разблокировка
                    }
                    else                                                            // Если тип сигнала DI
                    {
                        if (!typeCombo.Items.Contains(DI)) typeCombo.Items.Add(DI);
                        typeCombo.Enabled = false; typeCombo.SelectedIndex = 2;     // typeCombo блок и выбор типа DI
                    }
                    ui_find.Select();
                    list_ui.Add(ui_find);
                    if (showCode) SetCodeLabel_UI(ui_find, label);                  // Выбрано отображение числовых кодов сигналов
                }
                if (!initialComboSignals)                                           // Если не начальная расстановка
                {
                    SubFromCombosUI(name, comboBox);
                    string nameFind = combo_text;
                    ui_find = list_ui.Find(x => x.Name == nameFind);
                    list_ui.Remove(ui_find);
                    if (ui_find != null)
                    {
                        ui_find.Dispose();
                        if (!list_ui.Contains(ui_find))
                            list_ui.Add(ui_find);

                        AddToCombosUI(ui_find, comboBox);                           // Добавление сигнала к другим UI
                    }
                }
            }
            combo_text = comboBox.SelectedItem.ToString();                          // Сохранение название выбранного элемента
            combo_index = comboBox.SelectedIndex;                                   // Сохранение индекса выбранного элемента
            CheckSignalsReady();
        }

        ///<summary>Метод для изменения UI typeCombo типа сигнала AI</summary>
        private void UI_typeCombo_SelectedIndexChanged(ComboBox typeCombo, ComboBox ui_combo, Label label)
        {
            try
            {
                if (ui_combo.SelectedIndex == -1) return;

                string name = string.Concat(ui_combo.SelectedItem);
                Ui ui_find = list_ui.Find(x => x.Name == name);             // Поиск сигнала UI по имени в списке

                if (ui_find != null && typeCombo.Enabled)                   // Проверка найденного сигнала и доступности typeCombo
                {
                    if (typeCombo.SelectedIndex == 0)                       // Выбран тип NTC
                        ui_find.SetType(NTC);
                    else if (typeCombo.SelectedIndex == 1)                  // Выбран тип 4-20 мА
                        ui_find.SetType(mA_4_20);
                    else if (typeCombo.SelectedIndex == 2)                  // Выбран тип DI
                        ui_find.SetType(DI);

                    if (showCode) SetCodeLabel_UI(ui_find, label);           // Отображение нового числового кода
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }

        ///<summary>Изменили UI1 тип сигнала typeCombo comboBox</summary>
        private void UI1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI1_typeCombo, UI1_combo, UI1_lab);
        }

        ///<summary>Изменили UI2 тип сигнала typeCombo comboBox</summary>
        private void UI2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI2_typeCombo, UI2_combo, UI2_lab);
        }

        ///<summary>Изменили UI3 тип сигнала typeCombo comboBox</summary>
        private void UI3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI3_typeCombo, UI3_combo, UI3_lab);
        }

        ///<summary>Изменили UI4 тип сигнала typeCombo comboBox</summary>
        private void UI4_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI4_typeCombo, UI4_combo, UI4_lab);
        }

        ///<summary>Изменили UI5 тип сигнала typeCombo comboBox</summary>
        private void UI5_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI5_typeCombo, UI5_combo, UI5_lab);
        }

        ///<summary>Изменили UI6 тип сигнала typeCombo comboBox</summary>
        private void UI6_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI6_typeCombo, UI6_combo, UI6_lab);
        }

        ///<summary>Изменили UI7 тип сигнала typeCombo comboBox</summary>
        private void UI7_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI7_typeCombo, UI7_combo, UI7_lab);
        }

        ///<summary>Изменили UI8 тип сигнала typeCombo comboBox</summary>
        private void UI8_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI8_typeCombo, UI8_combo, UI8_lab);
        }

        ///<summary>Изменили UI9 тип сигнала typeCombo comboBox</summary>
        private void UI9_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI9_typeCombo, UI9_combo, UI9_lab);
        }

        ///<summary>Изменили UI10 тип сигнала typeCombo comboBox</summary>
        private void UI10_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI10_typeCombo, UI10_combo, UI10_lab);
        }

        ///<summary>Изменили UI11 тип сигнала typeCombo comboBox</summary>
        private void UI11_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI11_typeCombo, UI11_combo, UI11_lab);
        }

        ///<summary>Изменили UI1 тип сигнала typeCombo comboBox, блок расширения 1</summary>
        private void UI1bl1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI1bl1_typeCombo, UI1bl1_combo, UI1bl1_lab);
        }

        ///<summary>Изменили UI2 тип сигнала typeCombo comboBox, блок расширения 1</summary>
        private void UI2bl1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI2bl1_typeCombo, UI2bl1_combo, UI2bl1_lab);
        }

        ///<summary>Изменили UI3 тип сигнала typeCombo comboBox, блок расширения 1</summary>
        private void UI3bl1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI3bl1_typeCombo, UI3bl1_combo, UI3bl1_lab);
        }

        ///<summary>Изменили UI4 тип сигнала typeCombo comboBox, блок расширения 1</summary>
        private void UI4bl1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI4bl1_typeCombo, UI4bl1_combo, UI4bl1_lab);
        }

        ///<summary>Изменили UI5 тип сигнала typeCombo comboBox, блок расширения 1</summary>
        private void UI5bl1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI5bl1_typeCombo, UI5bl1_combo, UI5bl1_lab);
        }

        ///<summary>Изменили UI6 тип сигнала typeCombo comboBox, блок расширения 1</summary>
        private void UI6bl1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI6bl1_typeCombo, UI6bl1_combo, UI6bl1_lab);
        }

        ///<summary>Изменили UI7 тип сигнала typeCombo comboBox, блок расширения 1</summary>
        private void UI7bl1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI7bl1_typeCombo, UI7bl1_combo, UI7bl1_lab);
        }

        ///<summary>Изменили UI8 тип сигнала typeCombo comboBox, блок расширения 1</summary>
        private void UI8bl1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI8bl1_typeCombo, UI8bl1_combo, UI8bl1_lab);
        }

        ///<summary>Изменили UI9 тип сигнала typeCombo comboBox, блок расширения 1</summary>
        private void UI9bl1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI9bl1_typeCombo, UI9bl1_combo, UI9bl1_lab);
        }

        ///<summary>Изменили UI10 тип сигнала typeCombo comboBox, блок расширения 1</summary>
        private void UI10bl1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI10bl1_typeCombo, UI10bl1_combo, UI10bl1_lab);
        }

        ///<summary>Изменили UI11 тип сигнала typeCombo comboBox, блок расширения 1</summary>
        private void UI11bl1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI11bl1_typeCombo, UI11bl1_combo, UI11bl1_lab);
        }

        ///<summary>Изменили UI12 тип сигнала typeCombo comboBox, блок расширения 1</summary>
        private void UI12bl1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI12bl1_typeCombo, UI12bl1_combo, UI12bl1_lab);
        }

        ///<summary>Изменили UI13 тип сигнала typeCombo comboBox, блок расширения 1</summary>
        private void UI13bl1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI13bl1_typeCombo, UI13bl1_combo, UI13bl1_lab);
        }

        ///<summary>Изменили UI14 тип сигнала typeCombo comboBox, блок расширения 1</summary>
        private void UI14bl1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI14bl1_typeCombo, UI14bl1_combo, UI14bl1_lab);
        }

        ///<summary>Изменили UI15 тип сигнала typeCombo comboBox, блок расширения 1</summary>
        private void UI15bl1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI15bl1_typeCombo, UI15bl1_combo, UI15bl1_lab);
        }

        ///<summary>Изменили UI16 тип сигнала typeCombo comboBox, блок расширения 1</summary>
        private void UI16bl1_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI16bl1_typeCombo, UI16bl1_combo, UI16bl1_lab);
        }

        ///<summary>Изменили UI1 тип сигнала typeCombo comboBox, блок расширения 2</summary>
        private void UI1bl2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI1bl2_typeCombo, UI1bl2_combo, UI1bl2_lab);
        }

        ///<summary>Изменили UI2 тип сигнала typeCombo comboBox, блок расширения 2</summary>
        private void UI2bl2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI2bl2_typeCombo, UI2bl2_combo, UI2bl2_lab);
        }

        ///<summary>Изменили UI3 тип сигнала typeCombo comboBox, блок расширения 2</summary>
        private void UI3bl2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI3bl2_typeCombo, UI3bl2_combo, UI3bl2_lab);
        }

        ///<summary>Изменили UI4 тип сигнала typeCombo comboBox, блок расширения 2</summary>
        private void UI4bl2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI4bl2_typeCombo, UI4bl2_combo, UI4bl2_lab);
        }

        ///<summary>Изменили UI5 тип сигнала typeCombo comboBox, блок расширения 2</summary>
        private void UI5bl2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI5bl2_typeCombo, UI5bl2_combo, UI5bl2_lab);
        }

        ///<summary>Изменили UI6 тип сигнала typeCombo comboBox, блок расширения 2</summary>
        private void UI6bl2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI6bl2_typeCombo, UI6bl2_combo, UI6bl2_lab);
        }

        ///<summary>Изменили UI7 тип сигнала typeCombo comboBox, блок расширения 2</summary>
        private void UI7bl2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI7bl2_typeCombo, UI7bl2_combo, UI7bl2_lab);
        }

        ///<summary>Изменили UI8 тип сигнала typeCombo comboBox, блок расширения 2</summary>
        private void UI8bl2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI8bl2_typeCombo, UI8bl2_combo, UI8bl2_lab);
        }

        ///<summary>Изменили UI9 тип сигнала typeCombo comboBox, блок расширения 2</summary>
        private void UI9bl2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI9bl2_typeCombo, UI9bl2_combo, UI9bl2_lab);
        }

        ///<summary>Изменили UI10 тип сигнала typeCombo comboBox, блок расширения 2</summary>
        private void UI10bl2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI10bl2_typeCombo, UI10bl2_combo, UI10bl2_lab);
        }

        ///<summary>Изменили UI11 тип сигнала typeCombo comboBox, блок расширения 2</summary>
        private void UI11bl2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI11bl2_typeCombo, UI11bl2_combo, UI11bl2_lab);
        }

        ///<summary>Изменили UI12 тип сигнала typeCombo comboBox, блок расширения 2</summary>
        private void UI12bl2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI12bl2_typeCombo, UI12bl2_combo, UI12bl2_lab);
        }

        ///<summary>Изменили UI13 тип сигнала typeCombo comboBox, блок расширения 2</summary>
        private void UI13bl2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI13bl2_typeCombo, UI13bl2_combo, UI13bl2_lab);
        }

        ///<summary>Изменили UI14 тип сигнала typeCombo comboBox, блок расширения 2</summary>
        private void UI14bl2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI14bl2_typeCombo, UI14bl2_combo, UI14bl2_lab);
        }

        ///<summary>Изменили UI15 тип сигнала typeCombo comboBox, блок расширения 2</summary>
        private void UI15bl2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI15bl2_typeCombo, UI15bl2_combo, UI15bl2_lab);
        }

        ///<summary>Изменили UI16 тип сигнала typeCombo comboBox, блок расширения 2</summary>
        private void UI16bl2_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI16bl2_typeCombo, UI16bl2_combo, UI16bl2_lab);
        }

        ///<summary>Изменили UI1 тип сигнала typeCombo comboBox, блок расширения 3</summary>
        private void UI1bl3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI1bl3_typeCombo, UI1bl3_combo, UI1bl3_lab);
        }

        ///<summary>Изменили UI2 тип сигнала typeCombo comboBox, блок расширения 3</summary>
        private void UI2bl3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI2bl3_typeCombo, UI2bl3_combo, UI2bl3_lab);
        }

        ///<summary>Изменили UI3 тип сигнала typeCombo comboBox, блок расширения 3</summary>
        private void UI3bl3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI3bl3_typeCombo, UI3bl3_combo, UI3bl3_lab);
        }

        ///<summary>Изменили UI4 тип сигнала typeCombo comboBox, блок расширения 3</summary>
        private void UI4bl3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI1bl3_typeCombo, UI1bl3_combo, UI1bl3_lab);
        }

        ///<summary>Изменили UI5 тип сигнала typeCombo comboBox, блок расширения 3</summary>
        private void UI5bl3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI5bl3_typeCombo, UI5bl3_combo, UI5bl3_lab);
        }

        ///<summary>Изменили UI6 тип сигнала typeCombo comboBox, блок расширения 3</summary>
        private void UI6bl3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI6bl3_typeCombo, UI6bl3_combo, UI6bl3_lab);
        }

        ///<summary>Изменили UI7 тип сигнала typeCombo comboBox, блок расширения 3</summary>
        private void UI7bl3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI7bl3_typeCombo, UI7bl3_combo, UI7bl3_lab);
        }

        ///<summary>Изменили UI8 тип сигнала typeCombo comboBox, блок расширения 3</summary>
        private void UI8bl3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI8bl3_typeCombo, UI8bl3_combo, UI8bl3_lab);
        }

        ///<summary>Изменили UI9 тип сигнала typeCombo comboBox, блок расширения 3</summary>
        private void UI9bl3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI9bl3_typeCombo, UI9bl3_combo, UI9bl3_lab);
        }

        ///<summary>Изменили UI10 тип сигнала typeCombo comboBox, блок расширения 3</summary>
        private void UI10bl3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI10bl3_typeCombo, UI10bl3_combo, UI10bl3_lab);
        }

        ///<summary>Изменили UI11 тип сигнала typeCombo comboBox, блок расширения 3</summary>
        private void UI11bl3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI11bl3_typeCombo, UI11bl3_combo, UI11bl3_lab);
        }

        ///<summary>Изменили UI12 тип сигнала typeCombo comboBox, блок расширения 3</summary>
        private void UI12bl3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI12bl3_typeCombo, UI12bl3_combo, UI12bl3_lab);
        }

        ///<summary>Изменили UI13 тип сигнала typeCombo comboBox, блок расширения 3</summary>
        private void UI13bl3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI13bl3_typeCombo, UI13bl3_combo, UI13bl3_lab);
        }

        ///<summary>Изменили UI14 тип сигнала typeCombo comboBox, блок расширения 3</summary>
        private void UI14bl3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI14bl3_typeCombo, UI14bl3_combo, UI14bl3_lab);
        }

        ///<summary>Изменили UI15 тип сигнала typeCombo comboBox, блок расширения 3</summary>
        private void UI15bl3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI15bl3_typeCombo, UI15bl3_combo, UI15bl3_lab);
        }

        ///<summary>Изменили UI16 тип сигнала typeCombo comboBox, блок расширения 3</summary>
        private void UI16bl3_typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_typeCombo_SelectedIndexChanged(UI16bl3_typeCombo, UI16bl3_combo, UI16bl3_lab);
        }

        ///<summary>Изменили UI1 comboBox</summary>
        private void UI1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI1_combo, ref UI1combo_index, ref UI1combo_text, UI1_lab, UI1_typeCombo);
        }

        ///<summary>Изменили UI2 comboBox</summary>
        private void UI2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI2_combo, ref UI2combo_index, ref UI2combo_text, UI2_lab, UI2_typeCombo);
        }

        ///<summary>Изменили UI3 comboBox</summary>
        private void UI3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI3_combo, ref UI3combo_index, ref UI3combo_text, UI3_lab, UI3_typeCombo);
        }

        ///<summary>Изменили UI4 comboBox</summary>
        private void UI4_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI4_combo, ref UI4combo_index, ref UI4combo_text, UI4_lab, UI4_typeCombo);
        }

        ///<summary>Изменили UI5 comboBox</summary>
        private void UI5_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI5_combo, ref UI5combo_index, ref UI5combo_text, UI5_lab, UI5_typeCombo);
        }

        ///<summary>Изменили UI6 comboBox</summary>
        private void UI6_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI6_combo, ref UI6combo_index, ref UI6combo_text, UI6_lab, UI6_typeCombo);
        }

        ///<summary>Изменили UI7 comboBox</summary>
        private void UI7_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI7_combo, ref UI7combo_index, ref UI7combo_text, UI7_lab, UI7_typeCombo);
        }

        ///<summary>Изменили UI8 comboBox</summary>
        private void UI8_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI8_combo, ref UI8combo_index, ref UI8combo_text, UI8_lab, UI8_typeCombo);
        }

        ///<summary>Изменили UI9 comboBox</summary>
        private void UI9_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI9_combo, ref UI9combo_index, ref UI9combo_text, UI9_lab, UI9_typeCombo);
        }

        ///<summary>Изменили UI10 comboBox</summary>
        private void UI10_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI10_combo, ref UI10combo_index, ref UI10combo_text, UI10_lab, UI10_typeCombo);
        }

        ///<summary>Изменили UI11 comboBox</summary>
        private void UI11_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI11_combo, ref UI11combo_index, ref UI11combo_text, UI11_lab, UI11_typeCombo);
        }

        ///<summary>Изменили UI1 comboBox, блок расширения 1</summary>
        private void UI1bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI1bl1_combo, ref UI1bl1combo_index, ref UI1bl1combo_text, UI1bl1_lab, UI1bl1_typeCombo);
        }

        ///<summary>Изменили UI2 comboBox, блок расширения 1</summary>
        private void UI2bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI2bl1_combo, ref UI2bl1combo_index, ref UI2bl1combo_text, UI2bl1_lab, UI2bl1_typeCombo);
        }

        ///<summary>Изменили UI3 comboBox, блок расширения 1</summary>
        private void UI3bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI3bl1_combo, ref UI3bl1combo_index, ref UI3bl1combo_text, UI3bl1_lab, UI3bl1_typeCombo);
        }

        ///<summary>Изменили UI4 comboBox, блок расширения 1</summary>
        private void UI4bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI4bl1_combo, ref UI4bl1combo_index, ref UI4bl1combo_text, UI4bl1_lab, UI4bl1_typeCombo);
        }

        ///<summary>Изменили UI5 comboBox, блок расширения 1</summary>
        private void UI5bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI5bl1_combo, ref UI5bl1combo_index, ref UI5bl1combo_text, UI5bl1_lab, UI5bl1_typeCombo);
        }

        ///<summary>Изменили UI6 comboBox, блок расширения 1</summary>
        private void UI6bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI6bl1_combo, ref UI6bl1combo_index, ref UI6bl1combo_text, UI6bl1_lab, UI6bl1_typeCombo);
        }

        ///<summary>Изменили UI7 comboBox, блок расширения 1</summary>
        private void UI7bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI7bl1_combo, ref UI7bl1combo_index, ref UI7bl1combo_text, UI7bl1_lab, UI7bl1_typeCombo);
        }

        ///<summary>Изменили UI8 comboBox, блок расширения 1</summary>
        private void UI8bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI8bl1_combo, ref UI8bl1combo_index, ref UI8bl1combo_text, UI8bl1_lab, UI8bl1_typeCombo);
        }

        ///<summary>Изменили UI9 comboBox, блок расширения 1</summary>
        private void UI9bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI9bl1_combo, ref UI9bl1combo_index, ref UI9bl1combo_text, UI9bl1_lab, UI9bl1_typeCombo);
        }

        ///<summary>Изменили UI10 comboBox, блок расширения 1</summary>
        private void UI10bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI10bl1_combo, ref UI10bl1combo_index, ref UI10bl1combo_text, UI10bl1_lab, UI10bl1_typeCombo);
        }

        ///<summary>Изменили UI11 comboBox, блок расширения 1</summary>
        private void UI11bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI11bl1_combo, ref UI11bl1combo_index, ref UI11bl1combo_text, UI11bl1_lab, UI11bl1_typeCombo);
        }

        ///<summary>Изменили UI12 comboBox, блок расширения 1</summary>
        private void UI12bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI12bl1_combo, ref UI12bl1combo_index, ref UI12bl1combo_text, UI12bl1_lab, UI12bl1_typeCombo);
        }

        ///<summary>Изменили UI13 comboBox, блок расширения 1</summary>
        private void UI13bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI13bl1_combo, ref UI13bl1combo_index, ref UI13bl1combo_text, UI13bl1_lab, UI13bl1_typeCombo);
        }

        ///<summary>Изменили UI14 comboBox, блок расширения 1</summary>
        private void UI14bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI14bl1_combo, ref UI14bl1combo_index, ref UI14bl1combo_text, UI14bl1_lab, UI14bl1_typeCombo);
        }

        ///<summary>Изменили UI15 comboBox, блок расширения 1</summary>
        private void UI15bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI15bl1_combo, ref UI15bl1combo_index, ref UI15bl1combo_text, UI15bl1_lab, UI15bl1_typeCombo);
        }

        ///<summary>Изменили UI16 comboBox, блок расширения 1</summary>
        private void UI16bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI16bl1_combo, ref UI16bl1combo_index, ref UI16bl1combo_text, UI16bl1_lab, UI16bl1_typeCombo);
        }

        ///<summary>Изменили UI1 comboBox, блок расширения 2</summary>
        private void UI1bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI1bl2_combo, ref UI1bl2combo_index, ref UI1bl2combo_text, UI1bl2_lab, UI1bl2_typeCombo);
        }

        ///<summary>Изменили UI2 comboBox, блок расширения 2</summary>
        private void UI2bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI2bl2_combo, ref UI2bl2combo_index, ref UI2bl2combo_text, UI2bl2_lab, UI2bl2_typeCombo);
        }

        ///<summary>Изменили UI3 comboBox, блок расширения 2</summary>
        private void UI3bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI3bl2_combo, ref UI3bl2combo_index, ref UI3bl2combo_text, UI3bl2_lab, UI3bl2_typeCombo);
        }

        ///<summary>Изменили UI4 comboBox, блок расширения 2</summary>
        private void UI4bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI4bl2_combo, ref UI4bl2combo_index, ref UI4bl2combo_text, UI4bl2_lab, UI4bl2_typeCombo);
        }

        ///<summary>Изменили UI5 comboBox, блок расширения 2</summary>
        private void UI5bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI5bl2_combo, ref UI5bl2combo_index, ref UI5bl2combo_text, UI5bl2_lab, UI5bl2_typeCombo);
        }

        ///<summary>Изменили UI6 comboBox, блок расширения 2</summary>
        private void UI6bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI6bl2_combo, ref UI6bl2combo_index, ref UI6bl2combo_text, UI6bl2_lab, UI6bl2_typeCombo);
        }

        ///<summary>Изменили UI7 comboBox, блок расширения 2</summary>
        private void UI7bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI7bl2_combo, ref UI7bl2combo_index, ref UI7bl2combo_text, UI7bl2_lab, UI7bl2_typeCombo);
        }

        ///<summary>Изменили UI8 comboBox, блок расширения 2</summary>
        private void UI8bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI8bl2_combo, ref UI8bl2combo_index, ref UI8bl2combo_text, UI8bl2_lab, UI8bl2_typeCombo);
        }

        ///<summary>Изменили UI9 comboBox, блок расширения 2</summary>
        private void UI9bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI9bl2_combo, ref UI9bl2combo_index, ref UI9bl2combo_text, UI9bl2_lab, UI9bl2_typeCombo);
        }

        ///<summary>Изменили UI10 comboBox, блок расширения 2</summary>
        private void UI10bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI10bl2_combo, ref UI10bl2combo_index, ref UI10bl2combo_text, UI10bl2_lab, UI10bl2_typeCombo);
        }

        ///<summary>Изменили UI11 comboBox, блок расширения 2</summary>
        private void UI11bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI11bl2_combo, ref UI11bl2combo_index, ref UI11bl2combo_text, UI11bl2_lab, UI11bl2_typeCombo);
        }

        ///<summary>Изменили UI12 comboBox, блок расширения 2</summary>
        private void UI12bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI12bl2_combo, ref UI12bl2combo_index, ref UI12bl2combo_text, UI12bl2_lab, UI12bl2_typeCombo);
        }

        ///<summary>Изменили UI13 comboBox, блок расширения 2</summary>
        private void UI13bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI13bl2_combo, ref UI13bl2combo_index, ref UI13bl2combo_text, UI13bl2_lab, UI13bl2_typeCombo);
        }

        ///<summary>Изменили UI14 comboBox, блок расширения 2</summary>
        private void UI14bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI14bl2_combo, ref UI14bl2combo_index, ref UI14bl2combo_text, UI14bl2_lab, UI14bl2_typeCombo);
        }

        ///<summary>Изменили UI15 comboBox, блок расширения 2</summary>
        private void UI15bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI15bl2_combo, ref UI15bl2combo_index, ref UI15bl2combo_text, UI15bl2_lab, UI15bl2_typeCombo);
        }

        ///<summary>Изменили UI16 comboBox, блок расширения 2</summary>
        private void UI16bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI16bl2_combo, ref UI16bl2combo_index, ref UI16bl2combo_text, UI16bl2_lab, UI16bl2_typeCombo);
        }

        ///<summary>Изменили UI1 comboBox, блок расширения 3</summary>
        private void UI1bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI1bl3_combo, ref UI1bl3combo_index, ref UI1bl3combo_text, UI1bl3_lab, UI1bl3_typeCombo);
        }

        ///<summary>Изменили UI2 comboBox, блок расширения 3</summary>
        private void UI2bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI2bl3_combo, ref UI2bl3combo_index, ref UI2bl3combo_text, UI2bl3_lab, UI2bl3_typeCombo);
        }

        ///<summary>Изменили UI3 comboBox, блок расширения 3</summary>
        private void UI3bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI3bl3_combo, ref UI3bl3combo_index, ref UI3bl3combo_text, UI3bl3_lab, UI3bl3_typeCombo);
        }

        ///<summary>Изменили UI4 comboBox, блок расширения 3</summary>
        private void UI4bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI4bl3_combo, ref UI4bl3combo_index, ref UI4bl3combo_text, UI4bl3_lab, UI4bl3_typeCombo);
        }

        ///<summary>Изменили UI5 comboBox, блок расширения 3</summary>
        private void UI5bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI5bl3_combo, ref UI5bl3combo_index, ref UI5bl3combo_text, UI5bl3_lab, UI5bl3_typeCombo);
        }

        ///<summary>Изменили UI6 comboBox, блок расширения 3</summary>
        private void UI6bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI6bl3_combo, ref UI6bl3combo_index, ref UI6bl3combo_text, UI6bl3_lab, UI6bl3_typeCombo);
        }

        ///<summary>Изменили UI7 comboBox, блок расширения 3</summary>
        private void UI7bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI7bl3_combo, ref UI7bl3combo_index, ref UI7bl3combo_text, UI7bl3_lab, UI7bl3_typeCombo);
        }

        ///<summary>Изменили UI8 comboBox, блок расширения 3</summary>
        private void UI8bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI8bl3_combo, ref UI8bl3combo_index, ref UI8bl3combo_text, UI8bl3_lab, UI8bl3_typeCombo);
        }

        ///<summary>Изменили UI9 comboBox, блок расширения 3</summary>
        private void UI9bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI9bl3_combo, ref UI9bl3combo_index, ref UI9bl3combo_text, UI9bl3_lab, UI9bl3_typeCombo);
        }

        ///<summary>Изменили UI10 comboBox, блок расширения 3</summary>
        private void UI10bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI10bl3_combo, ref UI10bl3combo_index, ref UI10bl3combo_text, UI10bl3_lab, UI10bl3_typeCombo);
        }

        ///<summary>Изменили UI11 comboBox, блок расширения 3</summary>
        private void UI11bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI11bl3_combo, ref UI11bl3combo_index, ref UI11bl3combo_text, UI11bl3_lab, UI11bl3_typeCombo);
        }

        ///<summary>Изменили UI12 comboBox, блок расширения 3</summary>
        private void UI12bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI12bl3_combo, ref UI12bl3combo_index, ref UI12bl3combo_text, UI12bl3_lab, UI12bl3_typeCombo);
        }

        ///<summary>Изменили UI13 comboBox, блок расширения 3</summary>
        private void UI13bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI13bl3_combo, ref UI13bl3combo_index, ref UI13bl3combo_text, UI13bl3_lab, UI13bl3_typeCombo);
        }

        ///<summary>Изменили UI14 comboBox, блок расширения 3</summary>
        private void UI14bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI14bl3_combo, ref UI14bl3combo_index, ref UI14bl3combo_text, UI14bl3_lab, UI14bl3_typeCombo);
        }

        ///<summary>Изменили UI15 comboBox, блок расширения 3</summary>
        private void UI15bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI15bl3_combo, ref UI15bl3combo_index, ref UI15bl3combo_text, UI15bl3_lab, UI15bl3_typeCombo);
        }

        ///<summary>Изменили UI15 comboBox, блок расширения 3</summary>
        private void UI16bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI_combo_SelectedIndexChanged(UI16bl3_combo, ref UI16bl3combo_index, ref UI16bl3combo_text, UI16bl3_lab, UI16bl3_typeCombo);
        }
    }
}
