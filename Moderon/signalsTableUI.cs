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
        public Ui(string name, ushort code, string type)
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
        List<Ui> list_ui = new List<Ui>();

        // Сохранение наименование для выбранного элемента для ПЛК
        string
            UI1combo_text, UI2combo_text, UI3combo_text, UI4combo_text, UI5combo_text,
            UI6combo_text, UI7combo_text, UI8combo_text, UI9combo_text, UI10combo_text, UI11combo_text;

        // Сохранение ранее выбранного индекса для comboBox для ПЛК
        int
            UI1combo_index, UI2combo_index, UI3combo_index, UI4combo_index, UI5combo_index,
            UI6combo_index, UI7combo_index, UI8combo_index, UI9combo_index, UI10combo_index, UI11combo_index;

        /// <summary>Начальная установка для входов UI таблицы сигналов</summary>
        public void Set_UIComboTextIndex()
        {
            var ui_signals = new List<string>() 
            {
                UI1combo_text, UI2combo_text, UI3combo_text, UI4combo_text, UI5combo_text,
                UI6combo_text, UI7combo_text, UI8combo_text, UI9combo_text, UI10combo_text, UI11combo_text
            };

            var ui_signals_index = new List<int>()
            {
                UI1combo_index, UI2combo_index, UI3combo_index, UI4combo_index, UI5combo_index,
                UI6combo_index, UI7combo_index, UI8combo_index, UI9combo_index, UI10combo_index, UI11combo_index
            };

            for (var i = 0; i < ui_signals.Count; i++) ui_signals[i] = NOT_SELECTED;
            for (var i = 0; i < ui_signals_index.Count; i++) ui_signals_index[i] = 0;
        }

        ///<summary>Сброс выбора сигналов для UI comboBox</summary>
        public void ResetButton_signalsUIClick(object sender, EventArgs e)
        {
            var ui_combos = new List<ComboBox>()
            {
                UI1_combo, UI2_combo, UI3_combo, UI4_combo, UI5_combo, UI6_combo, UI7_combo,
                UI8_combo, UI9_combo, UI10_combo, UI11_combo
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
    }
}
