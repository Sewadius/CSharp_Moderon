using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Moderon
{
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

        /// <summary>Распределение свободного UI сигнала</summary>
        public void Select() => Active = false;

        /// <summary>Освобождение ранее распределённого UI сигнала</summary>
        public void Dispose() => Active = true;
    }

    public partial class Form1 : Form
    {
        static readonly string
            DI = "di";

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

        ///<summary>Метод для добавления UI к списку сигналов</summary>
        private void AddToListUI(string name, ushort code, string type)
        {
            list_ui.Add(new Ui(name, code, type));          // Добавление в список сигналов UI
            AddNewUI(code, type);                           // Добавление нового UI и его назначение под выход
        }

        ///<summary>Добавление нового UI и его назначнение под выход</summary>
        private void AddNewUI(ushort code, string type) 
        {

        }

        ///<summary>Добавление освободившегося UI к остальным comboBox</summary>
        private void AddtoCombo_UI(string name, ComboBox cm, ref ComboBox comboBox)
        {
            bool notFound = true;                                       // Элемент в списке не найден

            if (comboBox != cm)                                         // comboBox отличаются
            {
                Ui ui_find = list_ui.Find(x => x.Name == name);

                if (ui_find != null)
                {
                    foreach (var el in comboBox.Items)
                        if (el.ToString() == name) notFound = false;
                    if (notFound) comboBox.Items.Add(ui_find.Name);
                    notFound = false;
                }
            }
        }

        ///<summary>Добавление в другие слоты для выбора в comboBox</summary>
        private void AddToCombosUI(string name, ComboBox cm)
        {
            // ПЛК
            AddtoCombo_UI(name, cm, ref UI1_combo); AddtoCombo_UI(name, cm, ref UI2_combo); AddtoCombo_UI(name, cm, ref UI3_combo);
            AddtoCombo_UI(name, cm, ref UI4_combo); AddtoCombo_UI(name, cm, ref UI5_combo); AddtoCombo_UI(name, cm, ref UI6_combo);
            AddtoCombo_UI(name, cm, ref UI7_combo);
        }

        ///<summary>Удаление UI из других comboBox</summary>
        private void SubFromCombosUI(string name, ComboBox cm)
        {
            var ui_combos = new List<ComboBox>()
            {
                UI1_combo, UI2_combo, UI3_combo, UI4_combo, UI5_combo, UI6_combo, UI7_combo
            };

            foreach (var el in ui_combos)
                if (name != NOT_SELECTED && el != cm)
                    el.Items.Remove(name);
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
                    list_ui.Remove(ui_find);                                        // Удаление сигнала из списка
                    if (showCode) label.Text = "";
                }
                if (ui_find != null)                                                // Найден элемент
                {
                    ui_find.Dispose();                                              // Освобождение сигнала для распределения
                    if (!list_ui.Contains(ui_find))
                        list_ui.Add(ui_find);
                }
                if (!initialComboSignals) AddToCombosUI(combo_text, comboBox);      // Добавление к другим UI

            }
            else // Если выбран сигнал UI
            {
                name = string.Concat(comboBox.SelectedItem);
                ui_find = list_ui.Find(x => x.Name == name);
                list_ui.Remove(list_ui.Find(x => x.Name == name));                  // Удаление из списка UI
                if (ui_find != null)
                {
                    ui_find.Select();
                    if (list_ui.Contains(ui_find))
                        list_ui.Add(ui_find);
                    if (showCode) label.Text = ui_find.Code.ToString();
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
                    }
                    AddToCombosUI(combo_text, comboBox);                            // Добавление к другим UI
                }
            }
            combo_text = comboBox.SelectedItem.ToString();                          // Сохранение название выбранного элемента
            combo_index = comboBox.SelectedIndex;                                   // Сохранение индекса выбранного элемента
            CheckSignalsReady();
        }
    }
}
