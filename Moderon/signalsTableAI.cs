using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Moderon
{
    class Ai
    {
        public string Name { get; private set; }
        public ushort Code { get; private set; }
        public string Type { get; private set; }
        public bool Active { get; private set; } = true;        // Свободен к распределению по умолчанию
        public Ai(string name, ushort code, string type)
        {
            Name = name; Code = code; Type = type;
        }
        public Ai(string name, ushort code, string type, bool active)
        {
            Name = name; Code = code; Type = type; Active = active;
        }
        public void Select() => Active = false;
        public void Dispose() => Active = true;
    }

    public partial class Form1 : Form
    {
        List<Ai> list_ai = new List<Ai>();

        bool subAIcondition = false; // Условие при удалении AI

        static readonly string 
            NTC = "NTC", Pt1000 = "Pt1000", mA_4_20 = "4-20 мА", V0_10 = "0-10 В";

        // Сохранение наименования выбранного элемента для ПЛК и блоков расширения 1, 2, 3
        string
            AI1combo_text, AI2combo_text, AI3combo_text, AI4combo_text, AI5combo_text, AI6combo_text,
            AI1bl1combo_text, AI2bl1combo_text, AI3bl1combo_text, AI4bl1combo_text, AI5bl1combo_text, AI6bl1combo_text,
            AI1bl2combo_text, AI2bl2combo_text, AI3bl2combo_text, AI4bl2combo_text, AI5bl2combo_text, AI6bl2combo_text,
            AI1bl3combo_text, AI2bl3combo_text, AI3bl3combo_text, AI4bl3combo_text, AI5bl3combo_text, AI6bl3combo_text;

        // Сохранение прошлого индекса для comboBox ПЛК
        int
            AI1combo_index, AI2combo_index, AI3combo_index, AI4combo_index, AI5combo_index, AI6combo_index,
            AI1bl1combo_index, AI2bl1combo_index, AI3bl1combo_index, AI4bl1combo_index, AI5bl1combo_index, AI6bl1combo_index,
            AI1bl2combo_index, AI2bl2combo_index, AI3bl2combo_index, AI4bl2combo_index, AI5bl2combo_index, AI6bl2combo_index,
            AI1bl3combo_index, AI2bl3combo_index, AI3bl3combo_index, AI4bl3combo_index, AI5bl3combo_index, AI6bl3combo_index;

        ///<summary>Начальная установка для сигналов AI таблицы сигналов</summary>
        public void Set_AIComboTextIndex()
        {
            var ai_signals = new List<string>()
            {
                AI1combo_text, AI2combo_text, AI3combo_text, AI4combo_text, AI5combo_text, AI6combo_text,
                AI1bl1combo_text, AI2bl1combo_text, AI3bl1combo_text, AI4bl1combo_text, AI5bl1combo_text, AI6bl1combo_text,
                AI1bl2combo_text, AI2bl2combo_text, AI3bl2combo_text, AI4bl2combo_text, AI5bl2combo_text, AI6bl2combo_text,
                AI1bl3combo_text, AI2bl3combo_text, AI3bl3combo_text, AI4bl3combo_text, AI5bl3combo_text, AI6bl3combo_text
            };

            var ai_signals_index = new List<int>()
            {
                AI1combo_index, AI2combo_index, AI3combo_index, AI4combo_index, AI5combo_index, AI6combo_index,
                AI1bl1combo_index, AI2bl1combo_index, AI3bl1combo_index, AI4bl1combo_index, AI5bl1combo_index, AI6bl1combo_index,
                AI1bl2combo_index, AI2bl2combo_index, AI3bl2combo_index, AI4bl2combo_index, AI5bl2combo_index, AI6bl2combo_index,
                AI1bl3combo_index, AI2bl3combo_index, AI3bl3combo_index, AI4bl3combo_index, AI5bl3combo_index, AI6bl3combo_index
            };

            for (var i = 0; i < ai_signals.Count; i++)
                ai_signals[i] = NOT_SELECTED;

            for (var i = 0; i < ai_signals_index.Count; i++)
                ai_signals_index[i] = 0;
        }

        ///<summary>Сброс выбора сигналов AI comboBox</summary>
        private void ResetButton_signalsAIClick(object sender, EventArgs e)
        {
            var ai_combos = new List<ComboBox>()
            {
                AI1_combo, AI2_combo, AI3_combo, AI4_combo, AI5_combo, AI6_combo,
                AI1bl1_combo, AI2bl1_combo, AI3bl1_combo, AI4bl1_combo, AI5bl1_combo, AI6bl1_combo,
                AI1bl2_combo, AI2bl2_combo, AI3bl2_combo, AI4bl2_combo, AI5bl2_combo, AI6bl2_combo,
                AI1bl3_combo, AI2bl3_combo, AI3bl3_combo, AI4bl3_combo, AI5bl3_combo, AI6bl3_combo
            };

            foreach (var el in ai_combos)
            {
                el.Items.Clear(); el.Items.Add(NOT_SELECTED);
            }
        }

        ///<summary>Метод для изменения AI comboBox</summary>
        private void AI_combo_SelectedIndexChanged(ComboBox comboBox, ref int combo_index, ref string combo_text, Label label, ComboBox typeCombo)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return;                                             // Переход из вычета сигналов AI
            if (comboBox.SelectedIndex == combo_index) return;                      // Индекс не поменялся
            if (comboBox.SelectedIndex == 0)                                        // Выбрали "Не выбрано"
            {
                if (comboBox.Items.Count > 1)                                       // Больше одного элемента в списке
                {
                    string nameFind = combo_text;
                    ai_find = list_ai.Find(x => x.Name == nameFind);
                    list_ai.Remove(ai_find);                                        // Удаление из списка
                    if (showCode) label.Text = "";
                    if (ai_find.Type == "di")                                       // Если DI тип
                    {
                        AddToAllCombosDI(combo_text);                               // Добавление ко всем DI
                    }
                }
                if (ai_find != null)                                                // Найден элемент
                {
                    ai_find.Dispose();                                              // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);                                       // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(combo_text, comboBox);      // Добавление к другим AI
                SensorAIType(typeCombo, 2);                                         // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = string.Concat(comboBox.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name));                  // Удаление из списка AI
                if (ai_find.Type == "di")                                           // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select();                     // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name);                                    // Удаление из всех comboBox DI 
                    SensorAIType(typeCombo, 2);                                     // Установка NTC и блокировка
                }
                else if (ai_find.Type == "humidSensor")
                    SensorAIType(typeCombo, 1);                                     // Установка для датчика влажности
                else if (ai_find.Type == "sensor")
                    SensorAIType(typeCombo, 0);                                     // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) label.Text = ai_find.Code.ToString();             // Отображение числового кода
                }
                if (!initialComboSignals)                                           // Если не начальная расстановка
                {                                                                   // Удаление ранее выбранного сигнала AI
                    SubFromCombosAI(name, comboBox);                                // Удаление из других AI
                    string nameFind = combo_text;
                    ai_find = list_ai.Find(x => x.Name == nameFind);
                    list_ai.Remove(ai_find);
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(combo_text, comboBox);                            // Добавление к другим AI
                }
            }
            combo_text = comboBox.SelectedItem.ToString();                          // Сохранение названия выбранного элемента в переменной
            combo_index = comboBox.SelectedIndex;                                   // Сохранение индекса выбранного элемента
            CheckSignalsReady();                                                    // Проверка распределения сигналов
        }

        ///<summary>Изменили AI1 comboBox</summary>
        private void AI1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI1_combo, ref AI1combo_index, ref AI1combo_text, AI1_lab, AI1_typeCombo);
        }

        ///<summary>Изменили AI2 comboBox</summary>
        private void AI2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI2_combo, ref AI2combo_index, ref AI2combo_text, AI2_lab, AI2_typeCombo);
        }

        ///<summary>Изменили AI3 comboBox</summary>
        private void AI3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI3_combo, ref AI3combo_index, ref AI3combo_text, AI3_lab, AI3_typeCombo);
        }

        ///<summary>Изменили AI4 comboBox</summary>
        private void AI4_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI4_combo, ref AI4combo_index, ref AI4combo_text, AI4_lab, AI4_typeCombo);
        }

        ///<summary>Изменили AI5 comboBox</summary>
        private void AI5_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI5_combo, ref AI5combo_index, ref AI5combo_text, AI5_lab, AI5_typeCombo);
        }

        ///<summary>Изменили AI6 comboBox</summary>
        private void AI6_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI6_combo, ref AI6combo_index, ref AI6combo_text, AI6_lab, AI6_typeCombo);
        }

        ///<summary>Изменили AI1 блока расширения 1 comboBox</summary>
        private void AI1bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI1bl1_combo, ref AI1bl1combo_index, ref AI1bl1combo_text, AI1bl1_lab, AI1bl1_typeCombo);
        }

        ///<summary>Изменили AI2 блока расширения 1 comboBox</summary>
        private void AI2bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI2bl1_combo, ref AI2bl1combo_index, ref AI2bl1combo_text, AI2bl1_lab, AI2bl1_typeCombo);
        }

        ///<summary>Изменили AI3 блока расширения 1 comboBox</summary>
        private void AI3bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI3bl1_combo, ref AI3bl1combo_index, ref AI3bl1combo_text, AI3bl1_lab, AI3bl1_typeCombo);
        }

        ///<summary>Изменили AI4 блока расширения 1 comboBox</summary>
        private void AI4bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI4bl1_combo, ref AI4bl1combo_index, ref AI4bl1combo_text, AI4bl1_lab, AI4bl1_typeCombo);
        }

        ///<summary>Изменили AI5 блока расширения 1 comboBox</summary>
        private void AI5bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI5bl1_combo, ref AI5bl1combo_index, ref AI5bl1combo_text, AI5bl1_lab, AI5bl1_typeCombo);
        }

        ///<summary>Изменили AI6 блока расширения 1 comboBox</summary>
        private void AI6bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI6bl1_combo, ref AI6bl1combo_index, ref AI6bl1combo_text, AI6bl1_lab, AI6bl1_typeCombo);
        }

        ///<summary>Изменили AI1 блока расширения 2 comboBox</summary>
        private void AI1bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI1bl2_combo, ref AI1bl2combo_index, ref AI1bl2combo_text, AI1bl2_lab, AI1bl2_typeCombo);
        }

        ///<summary>Изменили AI2 блока расширения 2 comboBox</summary>
        private void AI2bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI2bl2_combo, ref AI2bl2combo_index, ref AI2bl2combo_text, AI2bl2_lab, AI2bl2_typeCombo);
        }

        ///<summary>Изменили AI3 блока расширения 2 comboBox</summary>
        private void AI3bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI3bl2_combo, ref AI3bl2combo_index, ref AI3bl2combo_text, AI3bl2_lab, AI3bl2_typeCombo);
        }

        ///<summary>Изменили AI4 блока расширения 2 comboBox</summary>
        private void AI4bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI4bl2_combo, ref AI4bl2combo_index, ref AI4bl2combo_text, AI4bl2_lab, AI4bl2_typeCombo);
        }

        ///<summary>Изменили AI5 блока расширения 2 comboBox</summary>
        private void AI5bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI5bl2_combo, ref AI5bl2combo_index, ref AI5bl2combo_text, AI5bl2_lab, AI5bl2_typeCombo);
        }

        ///<summary>Изменили AI6 блока расширения 2 comboBox</summary>
        private void AI6bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI6bl2_combo, ref AI6bl2combo_index, ref AI6bl2combo_text, AI6bl2_lab, AI6bl2_typeCombo);
        }

        ///<summary>Изменили AI1 блока расширения 3 comboBox</summary>
        private void AI1bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI1bl3_combo, ref AI1bl3combo_index, ref AI1bl3combo_text, AI1bl3_lab, AI1bl3_typeCombo);
        }

        ///<summary>Изменили AI2 блока расширения 3 comboBox</summary>
        private void AI2bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI2bl3_combo, ref AI2bl3combo_index, ref AI2bl3combo_text, AI2bl3_lab, AI2bl3_typeCombo);
        }

        ///<summary>Изменили AI3 блока расширения 3 comboBox</summary>
        private void AI3bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI3bl3_combo, ref AI3bl3combo_index, ref AI3bl3combo_text, AI3bl3_lab, AI3bl3_typeCombo);
        }

        ///<summary>Изменили AI4 блока расширения 3 comboBox</summary>
        private void AI4bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI4bl3_combo, ref AI4bl3combo_index, ref AI4bl3combo_text, AI4bl3_lab, AI4bl3_typeCombo);
        }

        ///<summary>Изменили AI5 блока расширения 3 comboBox</summary>
        private void AI5bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI5bl3_combo, ref AI5bl3combo_index, ref AI5bl3combo_text, AI5bl3_lab, AI5bl3_typeCombo);
        }

        ///<summary>Изменили AI6 блока расширения 3 comboBox</summary>
        private void AI6bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AI_combo_SelectedIndexChanged(AI6bl3_combo, ref AI6bl3combo_index, ref AI6bl3combo_text, AI6bl3_lab, AI6bl3_typeCombo);
        }

        ///<summary>Добавление в другие слоты для выбора в comboBox</summary>
        private void AddToCombo_AI(string name, ComboBox cm, ref ComboBox comboBox)
        {
            bool notFound = true;                                               // Элемент в списке не найден
            bool humidTypeCheck = true;
            var ai_check_list = new List<ComboBox>()                            // Список для проверки AI4-AI6 для типа входа
            {
                AI4_combo, AI4bl1_combo, AI4bl2_combo, AI4bl3_combo,
                AI5_combo, AI5bl1_combo, AI5bl2_combo, AI5bl3_combo,
                AI6_combo, AI6bl1_combo, AI6bl2_combo, AI6bl3_combo
            };

            if (comboBox != cm)
            {
                Ai ai_find = list_ai.Find(x => x.Name == name);

                if (ai_check_list.Contains(comboBox) && ai_find != null)
                    humidTypeCheck = ai_find.Type != "humidSensor";

                if (ai_find != null && humidTypeCheck)
                {
                    foreach (var el in comboBox.Items)
                        if (el.ToString() == name) notFound = false;
                    if (notFound) comboBox.Items.Add(ai_find.Name);
                    notFound = false;
                }
            }
        }

        ///<summary>Добавление освободившегося AI к остальным comboBox</summary>
        private void AddtoCombosAI(string name, ComboBox cm)
        {
            // ПЛК
            AddToCombo_AI(name, cm, ref AI1_combo); AddToCombo_AI(name, cm, ref AI2_combo); AddToCombo_AI(name, cm, ref AI3_combo);
            AddToCombo_AI(name, cm, ref AI4_combo); AddToCombo_AI(name, cm, ref AI5_combo); AddToCombo_AI(name, cm, ref AI6_combo);
            // Блок расширения 1
            AddToCombo_AI(name, cm, ref AI1bl1_combo); AddToCombo_AI(name, cm, ref AI2bl1_combo); AddToCombo_AI(name, cm, ref AI3bl1_combo);
            AddToCombo_AI(name, cm, ref AI4bl1_combo); AddToCombo_AI(name, cm, ref AI5bl1_combo); AddToCombo_AI(name, cm, ref AI6bl1_combo);
            // Блок расширения 2
            AddToCombo_AI(name, cm, ref AI1bl2_combo); AddToCombo_AI(name, cm, ref AI2bl2_combo); AddToCombo_AI(name, cm, ref AI3bl2_combo);
            AddToCombo_AI(name, cm, ref AI4bl2_combo); AddToCombo_AI(name, cm, ref AI5bl2_combo); AddToCombo_AI(name, cm, ref AI6bl2_combo);
            // Блок расширения 3
            AddToCombo_AI(name, cm, ref AI1bl3_combo); AddToCombo_AI(name, cm, ref AI2bl3_combo); AddToCombo_AI(name, cm, ref AI3bl3_combo);
            AddToCombo_AI(name, cm, ref AI4bl3_combo); AddToCombo_AI(name, cm, ref AI5bl3_combo); AddToCombo_AI(name, cm, ref AI6bl3_combo);
        }

        ///<summary>Добавление ко всем входам AI (для сигнала DI)</summary>
        private void AddToAllCombosAI(string name)
        {
            var ai_combos = new List<ComboBox>()
            {
                AI1_combo, AI2_combo, AI3_combo, AI4_combo, AI5_combo, AI6_combo,
                AI1bl1_combo, AI2bl1_combo, AI3bl1_combo, AI4bl1_combo, AI5bl1_combo, AI6bl1_combo,
                AI1bl2_combo, AI2bl2_combo, AI3bl2_combo, AI4bl2_combo, AI5bl2_combo, AI6bl2_combo,
                AI1bl3_combo, AI2bl3_combo, AI3bl3_combo, AI4bl3_combo, AI5bl3_combo, AI6bl3_combo
            };

            foreach (var el in ai_combos)
                if (name != NOT_SELECTED)       // Кроме "Не выбрано"
                    el.Items.Add(name);
        }

        ///<summary>Удаление из всех входов AI (для сигнала DI)</summary>
        private void RemoveFromAllCombosAI(string name)
        {
            var ai_combos = new List<ComboBox>()
            {
                AI1_combo, AI2_combo, AI3_combo, AI4_combo, AI5_combo, AI6_combo,
                AI1bl1_combo, AI2bl1_combo, AI3bl1_combo, AI4bl1_combo, AI5bl1_combo, AI6bl1_combo,
                AI1bl2_combo, AI2bl2_combo, AI3bl2_combo, AI4bl2_combo, AI5bl2_combo, AI6bl2_combo,
                AI1bl3_combo, AI2bl3_combo, AI3bl3_combo, AI4bl3_combo, AI5bl3_combo, AI6bl3_combo
            };

            foreach (var el in ai_combos)
                el.Items.Remove(name);
        }

        ///<summary>Удаление AI из других comboBox</summary> 
        private void SubFromCombosAI(string name, ComboBox cm)
        {
            var ai_combos = new List<ComboBox>()
            {
                AI1_combo, AI2_combo, AI3_combo, AI4_combo, AI5_combo, AI6_combo,
                AI1bl1_combo, AI2bl1_combo, AI3bl1_combo, AI4bl1_combo, AI5bl1_combo, AI6bl1_combo,
                AI1bl2_combo, AI2bl2_combo, AI3bl2_combo, AI4bl2_combo, AI5bl2_combo, AI6bl2_combo,
                AI1bl3_combo, AI2bl3_combo, AI3bl3_combo, AI4bl3_combo, AI5bl3_combo, AI6bl3_combo
            };

            foreach (var el in ai_combos)
                if (name != NOT_SELECTED && el != cm)
                    el.Items.Remove(name);
        }

        ///<summary>Изменение списка типа входа AI</summary>
        private void SensorAIType(ComboBox cm, ushort typeAI)
        {
            cm.Items.Clear(); // Очистка списка типов AI
            if (typeAI == 0) // "sensor" для датчика температуры (AI1-AI3)
            {
                cm.Items.Add(NTC);
                cm.Items.Add(Pt1000);
                cm.Items.Add(V0_10);
                cm.Items.Add(mA_4_20);
                cm.Enabled = true;
            }
            else if (typeAI == 1) // "humidSensor" для датчика влажности
            {
                cm.Items.Add(V0_10);
                cm.Items.Add(mA_4_20);
                cm.Enabled = true;
            }
            else if (typeAI == 2) // "di" для дискретного входа
            {
                cm.Items.Add(NTC);
                cm.Enabled = false;
            }
            else if (typeAI == 3) // "sensor" для датчика температуры (AI4-AI6)
            {
                cm.Items.Add(NTC);
                cm.Items.Add(Pt1000);
                cm.Enabled = true;
            }
            cm.SelectedIndex = 0;
        }

        ///<summary>Добавление нового AI и его назначение для переданного comboBox</summary>
        private void SelectComboBox_AI(ComboBox cm, ushort code, string type, Label label, string text, int index, ComboBox typeCombo)
        {
            var ai_combos = new List<ComboBox>()
            {
                AI1_combo, AI2_combo, AI3_combo,
                AI1bl1_combo, AI2bl1_combo, AI3bl1_combo,
                AI1bl2_combo, AI2bl2_combo, AI3bl2_combo,
                AI1bl3_combo, AI2bl3_combo, AI3bl3_combo
            };

            cm.Items.Add(list_ai.Find(x => x.Code == code).Name);
            cm.SelectedIndex = cm.Items.Count - 1;
            text = cm.SelectedItem.ToString();
            index = cm.SelectedIndex;
            if (type == "di")
            {
                if (showCode) label.Text = (code + 1000).ToString();
            }
            else
            {
                if (showCode) label.Text = code.ToString(); 
            }
            list_ai.Find(x => x.Code == code).Select();
            // Изменения для выбора типа AI
            if (ai_combos.Contains(cm))
            {
                if (type == "sensor") SensorAIType(typeCombo, 0);
                else if (type == "humidSensor") SensorAIType(typeCombo, 1);
                else if (type == "di") SensorAIType(typeCombo, 2);
            } 
            else
            {
                if (type == "sensor") SensorAIType(typeCombo, 3);
                else if (type == "di") SensorAIType(typeCombo, 2);
            }
        }

        ///<summary>Добавление нового AI и его назначнение под выход</summary>
        private void AddNewAI(ushort code, string type)
        {
            if (AI4_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor")            // "Не выбрано", проверка типа, AI4
                SelectComboBox_AI(AI4_combo, code, type, AI4_lab, AI4combo_text, AI4combo_index, AI4_typeCombo);
            else if (AI5_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor")       // "Не выбрано", проверка типа, AI5
                SelectComboBox_AI(AI5_combo, code, type, AI5_lab, AI5combo_text, AI5combo_index, AI5_typeCombo);
            else if (AI6_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor")       // "Не выбрано", проверка типа, AI6
                SelectComboBox_AI(AI6_combo, code, type, AI6_lab, AI5combo_text, AI6combo_index, AI6_typeCombo);
            else if (AI1_combo.SelectedIndex == 0)                                                                  // "Не выбрано", AI1
                SelectComboBox_AI(AI1_combo, code, type, AI1_lab, AI1combo_text, AI1combo_index, AI1_typeCombo);
            else if (AI2_combo.SelectedIndex == 0)                                                                  // "Не выбрано", AI2
                SelectComboBox_AI(AI2_combo, code, type, AI2_lab, AI2combo_text, AI2combo_index, AI2_typeCombo);
            else if (AI3_combo.SelectedIndex == 0)                                                                  // "Не выбрано", AI3
                SelectComboBox_AI(AI3_combo, code, type, AI3_lab, AI3combo_text, AI3combo_index, AI3_typeCombo);
            else if (AI4bl1_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor")    // "Не выбрано", проверка типа, AI4 блок 1
                SelectComboBox_AI(AI4bl1_combo, code, type, AI4bl1_lab, AI4bl1combo_text, AI4bl1combo_index, AI4bl1_typeCombo);
            else if (AI5bl1_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor")    // "Не выбрано", проверка типа, AI5 блок 1
                SelectComboBox_AI(AI5bl1_combo, code, type, AI5bl1_lab, AI5bl1combo_text, AI5bl1combo_index, AI5bl1_typeCombo);
            else if (AI6bl1_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor")    // "Не выбрано", проверка типа, AI6 блок 1
                SelectComboBox_AI(AI6bl1_combo, code, type, AI6bl1_lab, AI6bl1combo_text, AI6bl1combo_index, AI6bl1_typeCombo);
            else if (AI1bl1_combo.SelectedIndex == 0)                                                               // "Не выбрано", AI1 блок 1
                SelectComboBox_AI(AI1bl1_combo, code, type, AI1bl1_lab, AI1bl1combo_text, AI1bl1combo_index, AI1bl1_typeCombo);
            else if (AI2bl1_combo.SelectedIndex == 0)                                                               // "Не выбрано", AI2 блок 1
                SelectComboBox_AI(AI2bl1_combo, code, type, AI2bl1_lab, AI2bl1combo_text, AI2bl1combo_index, AI2bl1_typeCombo);
            else if (AI3bl1_combo.SelectedIndex == 0)                                                               // "Не выбрано", AI3 блок 1
                SelectComboBox_AI(AI3bl1_combo, code, type, AI3bl1_lab, AI3bl1combo_text, AI3bl1combo_index, AI3bl1_typeCombo);
            else if (AI4bl2_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor")    // "Не выбрано", проверка типа, AI4 блок 2
                SelectComboBox_AI(AI4bl2_combo, code, type, AI4bl2_lab, AI4bl2combo_text, AI4bl2combo_index, AI4bl2_typeCombo);
            else if (AI5bl2_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor")    // "Не выбрано", проверка типа, AI5 блок 2
                SelectComboBox_AI(AI5bl2_combo, code, type, AI5bl2_lab, AI5bl2combo_text, AI5bl2combo_index, AI5bl2_typeCombo);
            else if (AI6bl2_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor")    // "Не выбрано", проверка типа
                SelectComboBox_AI(AI6bl2_combo, code, type, AI6bl2_lab, AI5bl2combo_text, AI5bl2combo_index, AI5bl2_typeCombo);
            else if (AI1bl2_combo.SelectedIndex == 0)                                                               // "Не выбрано", AI1 блок 2
                SelectComboBox_AI(AI1bl2_combo, code, type, AI1bl2_lab, AI1bl2combo_text, AI1bl2combo_index, AI1bl2_typeCombo);
            else if (AI2bl2_combo.SelectedIndex == 0)                                                               // "Не выбрано", AI2 блок 2
                SelectComboBox_AI(AI2bl2_combo, code, type, AI2bl2_lab, AI2bl2combo_text, AI2bl2combo_index, AI2bl2_typeCombo);
            else if (AI3bl2_combo.SelectedIndex == 0)                                                               // "Не выбрано", AI3 блок 2
                SelectComboBox_AI(AI3bl2_combo, code, type, AI3bl2_lab, AI3bl2combo_text, AI3bl2combo_index, AI3bl2_typeCombo);
            else if (AI4bl3_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor")    // "Не выбрано", проверка типа, AI4 блок 3
                SelectComboBox_AI(AI4bl3_combo, code, type, AI4bl3_lab, AI4bl3combo_text, AI4bl3combo_index, AI4bl3_typeCombo);
            else if (AI5bl3_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor")    // "Не выбрано", проверка типа, AI5 блок 3
                SelectComboBox_AI(AI5bl3_combo, code, type, AI5bl3_lab, AI5bl3combo_text, AI5bl3combo_index, AI5bl3_typeCombo);
            else if (AI6bl3_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor")    // "Не выбрано", проверка типа, AI6 блок 3
                SelectComboBox_AI(AI6bl3_combo, code, type, AI6bl3_lab, AI6bl3combo_text, AI6bl3combo_index, AI6bl3_typeCombo);
            else if (AI1bl3_combo.SelectedIndex == 0)                                                               // "Не выбрано", AI1 блок 3
                SelectComboBox_AI(AI1bl3_combo, code, type, AI1bl3_lab, AI1bl3combo_text, AI1bl3combo_index, AI1bl3_typeCombo);
            else if (AI2bl3_combo.SelectedIndex == 0)                                                               // "Не выбрано", AI2 блок 3
                SelectComboBox_AI(AI2bl3_combo, code, type, AI2bl3_lab, AI2bl3combo_text, AI2bl3combo_index, AI2bl3_typeCombo);
            else if (AI3bl3_combo.SelectedIndex == 0)                                                               // "Не выбрано", AI3 блок 3
                SelectComboBox_AI(AI3bl3_combo, code, type, AI3bl3_lab, AI3bl3combo_text, AI3bl3combo_index, AI3bl3_typeCombo);
        }

        ///<summary>Очистка и блокировка comboBox выбора типа AI</summary>
        private void ClearAITypeCombo(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add(NTC);
            comboBox.SelectedIndex = 0;
            comboBox.Enabled = false;
        }

        ///<summary>Удаление AI из определённого comboBox </summary>
        private void RemoveAI_FromComboBox(ComboBox comboBox, string name, Label label, string text, int index, ComboBox typeCombo)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString() == name)                                   // Есть совпадение по имени
                {
                    comboBox.Items.Remove(name);
                    if (comboBox.Items.Count > 1)                                           // Больше одного элемента
                    {
                        comboBox.SelectedIndex = comboBox.Items.Count - 1;
                        if (comboBox.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosAI(comboBox.SelectedItem.ToString(), comboBox);
                            Ai find_ai = list_ai.Find(x => x.Name == comboBox.SelectedItem.ToString());
                            if (find_ai != null)
                            {
                                list_ai.Remove(find_ai);
                                if (find_ai.Type == "di")
                                {
                                    if (showCode) label.Text = (find_ai.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) label.Text = find_ai.Code.ToString();
                                }
                            }
                        }
                    } 
                    else    // Больше одного элемента
                    {
                        ClearAITypeCombo(typeCombo);                                        // Обнуление типа для выбора типа AI
                        comboBox.SelectedItem = NOT_SELECTED;                               // Выбор "Не выбрано"
                        label.Text = "";
                    }
                    text = comboBox.SelectedItem.ToString();
                    index = comboBox.SelectedIndex;
                }
            }
        }

        ///<summary>Удаление AI из всех comboBox </summary>
        private void SubFromCombosAI(ushort code)
        {
            string name = "";
            Ai find_ai = list_ai.Find(x => x.Code == code);

            if (find_ai != null) name = find_ai.Name;
            else return;
            
            subAIcondition = true; // Признак удаления AI, не работает событие indexChanged

            // ПЛК
            RemoveAI_FromComboBox(AI1_combo, name, AI1_lab, AI1combo_text, AI1combo_index, AI1_typeCombo);                     // AI1
            RemoveAI_FromComboBox(AI2_combo, name, AI2_lab, AI2combo_text, AI2combo_index, AI2_typeCombo);                     // AI2
            RemoveAI_FromComboBox(AI3_combo, name, AI3_lab, AI3combo_text, AI3combo_index, AI3_typeCombo);                     // AI3
            RemoveAI_FromComboBox(AI4_combo, name, AI4_lab, AI4combo_text, AI4combo_index, AI4_typeCombo);                     // AI4
            RemoveAI_FromComboBox(AI5_combo, name, AI5_lab, AI5combo_text, AI5combo_index, AI5_typeCombo);                     // AI5
            RemoveAI_FromComboBox(AI6_combo, name, AI6_lab, AI6combo_text, AI6combo_index, AI6_typeCombo);                     // AI6
            // Блок расширения 1
            RemoveAI_FromComboBox(AI1bl1_combo, name, AI1bl1_lab, AI1bl1combo_text, AI1bl1combo_index, AI1bl1_typeCombo);      // AI1
            RemoveAI_FromComboBox(AI2bl1_combo, name, AI2bl1_lab, AI2bl1combo_text, AI2bl1combo_index, AI2bl1_typeCombo);      // AI2
            RemoveAI_FromComboBox(AI3bl1_combo, name, AI3bl1_lab, AI3bl1combo_text, AI3bl1combo_index, AI3bl1_typeCombo);      // AI3
            RemoveAI_FromComboBox(AI4bl1_combo, name, AI4bl1_lab, AI4bl1combo_text, AI4bl1combo_index, AI4bl1_typeCombo);      // AI4
            RemoveAI_FromComboBox(AI5bl1_combo, name, AI5bl1_lab, AI5bl1combo_text, AI5bl1combo_index, AI5bl1_typeCombo);      // AI5
            RemoveAI_FromComboBox(AI6bl1_combo, name, AI6bl1_lab, AI6bl1combo_text, AI6bl1combo_index, AI6bl1_typeCombo);      // AI6
            // Блок расширения 2
            RemoveAI_FromComboBox(AI1bl2_combo, name, AI1bl2_lab, AI1bl2combo_text, AI1bl2combo_index, AI1bl2_typeCombo);      // AI1
            RemoveAI_FromComboBox(AI2bl2_combo, name, AI2bl2_lab, AI2bl2combo_text, AI2bl2combo_index, AI2bl2_typeCombo);      // AI2
            RemoveAI_FromComboBox(AI3bl2_combo, name, AI3bl2_lab, AI3bl2combo_text, AI3bl2combo_index, AI3bl2_typeCombo);      // AI3
            RemoveAI_FromComboBox(AI4bl2_combo, name, AI4bl2_lab, AI4bl2combo_text, AI4bl2combo_index, AI4bl2_typeCombo);      // AI4
            RemoveAI_FromComboBox(AI5bl2_combo, name, AI5bl2_lab, AI5bl2combo_text, AI5bl2combo_index, AI5bl2_typeCombo);      // AI5
            RemoveAI_FromComboBox(AI6bl2_combo, name, AI6bl2_lab, AI6bl2combo_text, AI6bl2combo_index, AI6bl2_typeCombo);      // AI6
            // Блок расширения 3
            RemoveAI_FromComboBox(AI1bl3_combo, name, AI1bl3_lab, AI1bl3combo_text, AI1bl3combo_index, AI1bl3_typeCombo);      // AI1
            RemoveAI_FromComboBox(AI2bl3_combo, name, AI2bl3_lab, AI2bl3combo_text, AI2bl3combo_index, AI2bl3_typeCombo);      // AI2
            RemoveAI_FromComboBox(AI3bl3_combo, name, AI3bl3_lab, AI3bl3combo_text, AI3bl3combo_index, AI3bl3_typeCombo);      // AI3
            RemoveAI_FromComboBox(AI4bl3_combo, name, AI4bl3_lab, AI4bl3combo_text, AI4bl3combo_index, AI4bl3_typeCombo);      // AI4
            RemoveAI_FromComboBox(AI5bl3_combo, name, AI5bl3_lab, AI5bl3combo_text, AI5bl3combo_index, AI5bl3_typeCombo);      // AI5
            RemoveAI_FromComboBox(AI6bl3_combo, name, AI6bl3_lab, AI6bl3combo_text, AI6bl3combo_index, AI6bl3_typeCombo);      // AI6

            subAIcondition = false;             
            list_ai.Remove(find_ai);            // Удаление сигнала из списка AI
            CheckSignalsReady();                // Проверка распределения сигналов
        }

        ///<summary>Метод для добавления AI к списку сигналов</summary>
        private void AddToListAI(string name, ushort code, string type)
        {
            list_ai.Add(new Ai(name, code, type));
            AddNewAI(code, type);
        }

        ///<summary>Проверка и добавление аналогового входа</summary>
        private void CheckAddAIToList(string name, ushort code, string type) 
        {
            Ai find_ai = list_ai.Find(x => x.Code == code);
            if (find_ai == null)
                AddToListAI(name, code, type);
        }

        ///<summary>Выбрали основной нагреватель</summary>
        private void HeaterCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 9;                                                                          // Датчик обратной воды калорифера

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)                                // Водяной калорифер
                CheckAddAIToList("Датчик обратной воды калорифера", code_1, "sensor");
            else // Отмена выбора нагревателя
                SubFromCombosAI(code_1);
        }

        ///<summary>Изменили тип основного нагревателя</summary>
        private void HeatTypeCombo_signalsAISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 9;                                                                          // Датчик обратной воды калорифера

            if (heaterCheck.Checked)                                                                    // Когда выбран нагреватель
            {
                if (heatTypeCombo.SelectedIndex == 0)                                                   // Водяной калорифер
                    CheckAddAIToList("Датчик обратной воды калорифера", code_1, "sensor");
                else if (heatTypeCombo.SelectedIndex == 1)                                              // Электрокалорифер
                    SubFromCombosAI(code_1);
            }
        }

        ///<summary>Выбрали догреватель, второй нагреватель</summary>
        private void AddHeatCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 11;                                                                         // Датчик обратной воды догревателя

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)                            // Когда выбран водяной догреватель
                CheckAddAIToList("Датчик обратной воды догревателя", code_1, "sensor");
            else                                                                                        // Отмена выбора догревателя
                SubFromCombosAI(code_1);
        }

        ///<summary>Изменили тип догревателя</summary>
        private void HeatAddTypeCombo_signalsAISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 11;                                                                         // Датчик обратной воды догревателя

            if (addHeatCheck.Checked)                                                                   // Когда выбран догреватель
            {
                if (heatAddTypeCombo.SelectedIndex == 0)                                                // Водяной догреватель
                    CheckAddAIToList("Датчик обратной воды догревателя", code_1, "sensor");
                else if (heatAddTypeCombo.SelectedIndex == 1)                                           // Электрокалорифер
                    SubFromCombosAI(code_1);
            }
        }

        ///<summary>Выбрали рекуператор</summary>
        private void RecupCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 14;                                                                         // Датчик температуры защиты рекуператора

            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)                                  // Рекуператор
            {
                if (recDefTempCheck.Checked)                                                            // Выбрана защита по температурному датчику
                    CheckAddAIToList("Датчик температуры защиты рекуператора", code_1, "sensor");
            }
            else                                                                                        // Отмена выбора рекуператора
                SubFromCombosAI(code_1);
        }

        ///<summary>Выбрали температурный датчик защиты рекуператора</summary>
        private void RecDefTempCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 14;                                                                         // Датчик температуры защиты рекуператора

            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)                                  // ПВ и рекуператор
            {
                if (recDefTempCheck.Checked)                                                            // Выбрали защиту по температурному датчику
                    CheckAddAIToList("Датчик температуры защиты рекуператора", code_1, "sensor");
                else // Отмена выбора датчика
                    SubFromCombosAI(code_1);
            }
        }

        ///<summary>Выбрали канальный датчик температуры</summary>
        private void PrChanSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1;                                                                          // Канальный датчик температуры

            if (prChanSensCheck.Checked)                                                                // Выбрали датчик 
                CheckAddAIToList("Канальный датчик Т приточного воздуха", code_1, "sensor");
            else                                                                                        // Отмена выбора датчика
                SubFromCombosAI(code_1);
        }

        ///<summary>Выбрали комнатный датчик температуры</summary>
        private void RoomTempSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 4;                                                                          // Комнатный датчик температуры

            if (roomTempSensCheck.Checked)                                                              // Выбрали датчик 
                CheckAddAIToList("Датчик температуры комнатный", code_1, "sensor");
            else                                                                                        // Отмена выбора датчика
                SubFromCombosAI(code_1);
        }

        ///<summary>Выбрали канальный датчик влажности</summary>
        private void ChanHumSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 17;                                                                         // Канальный датчик влажности

            if (chanHumSensCheck.Checked)                                                               // Выбрали датчик 
                CheckAddAIToList("Канальный датчик влажности", code_1, "humidSensor");
            else                                                                                        // Отмена выбора датчика
                SubFromCombosAI(code_1);
        }

        ///<summary>Выбрали комнатный датчик влажности</summary>
        private void RoomHumSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 18;                                                                         // Комнатный датчик влажности

            if (roomHumSensCheck.Checked)                                                               // Выбрали датчик 
                CheckAddAIToList("Комнатный датчик влажности", code_1, "humidSensor");
            else                                                                                        // Отмена выбора датчика
                SubFromCombosAI(code_1);
        }

        ///<summary>Выбрали датчик температуры наружного воздуха</summary>
        private void OutdoorChanSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 3;                                                                          // Датчик температуры наружного воздуха

            if (outdoorChanSensCheck.Checked)                                                           // Выбрали датчик 
                CheckAddAIToList("Датчик температуры наружного воздуха", code_1, "sensor");
            else                                                                                        // Отмена выбора датчика
                SubFromCombosAI(code_1);
        }

        ///<summary>Выбрали канальный датчик Т вытяжного воздуха</summary>
        private void OutChanSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 5;                                                                          // Канальный датчик температуры вытяжного воздуха

            if (outChanSensCheck.Checked)                                                               // Выбрали датчик 
                CheckAddAIToList("Канальный датчик Т вытяжного воздуха", code_1, "sensor");
            else // Отмена выбора датчика
                SubFromCombosAI(code_1);
        }
    }
}