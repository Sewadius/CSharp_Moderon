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
                if (type == "sensor") SensorAIType(AI4_typeCombo, 3);
                else if (type == "di") SensorAIType(AI4_typeCombo, 2);
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
            else if (AI2bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI2, блок 1
                AI2bl1_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI2bl1_combo.SelectedIndex = AI2bl1_combo.Items.Count - 1;
                AI2bl1combo_text = AI2bl1_combo.SelectedItem.ToString();
                AI2bl1combo_index = AI2bl1_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI2bl1_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI2bl1_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI2bl1_typeCombo, 0);
                else if (type == "humidSensor") SensorAIType(AI2bl1_typeCombo, 1);
                else if (type == "di") SensorAIType(AI2bl1_typeCombo, 2);
            }
            else if (AI3bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI3, блок 1
                AI3bl1_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI3bl1_combo.SelectedIndex = AI3bl1_combo.Items.Count - 1;
                AI3bl1combo_text = AI3bl1_combo.SelectedItem.ToString();
                AI3bl1combo_index = AI3bl1_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI3bl1_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI3bl1_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI3bl1_typeCombo, 0);
                else if (type == "humidSensor") SensorAIType(AI3bl1_typeCombo, 1);
                else if (type == "di") SensorAIType(AI3bl1_typeCombo, 2);
            }
            else if (AI4bl2_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor") // "Не выбрано", проверка типа
            { // AI4, блок 2
                AI4bl2_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI4bl2_combo.SelectedIndex = AI4bl2_combo.Items.Count - 1;
                AI4bl2combo_text = AI4bl2_combo.SelectedItem.ToString();
                AI4bl2combo_index = AI4bl2_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI4bl2_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI4bl2_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI4bl2_typeCombo, 3);
                else if (type == "di") SensorAIType(AI4bl2_typeCombo, 2);
            }
            else if (AI5bl2_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor") // "Не выбрано", проверка типа
            { // AI5, блок 2
                AI5bl2_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI5bl2_combo.SelectedIndex = AI5bl2_combo.Items.Count - 1;
                AI5bl2combo_text = AI5bl2_combo.SelectedItem.ToString();
                AI5bl2combo_index = AI5bl2_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI5bl2_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI5bl2_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI5bl2_typeCombo, 3);
                else if (type == "di") SensorAIType(AI5bl2_typeCombo, 2);
            }
            else if (AI6bl2_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor") // "Не выбрано", проверка типа
            { // AI6, блок 2
                AI6bl2_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI6bl2_combo.SelectedIndex = AI6bl2_combo.Items.Count - 1;
                AI6bl2combo_text = AI6bl2_combo.SelectedItem.ToString();
                AI6bl2combo_index = AI6bl2_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI6bl2_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI6bl2_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI6bl2_typeCombo, 3);
                else if (type == "di") SensorAIType(AI6bl2_typeCombo, 2);
            }
            else if (AI1bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI1, блок 2
                AI1bl2_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI1bl2_combo.SelectedIndex = AI1bl2_combo.Items.Count - 1;
                AI1bl2combo_text = AI1bl2_combo.SelectedItem.ToString();
                AI1bl2combo_index = AI1bl2_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI1bl2_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI1bl2_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI1bl2_typeCombo, 0);
                else if (type == "humidSensor") SensorAIType(AI1bl2_typeCombo, 1);
                else if (type == "di") SensorAIType(AI1bl2_typeCombo, 2);
            }
            else if (AI2bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI2, блок 2
                AI2bl2_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI2bl2_combo.SelectedIndex = AI2bl2_combo.Items.Count - 1;
                AI2bl2combo_text = AI2bl2_combo.SelectedItem.ToString();
                AI2bl2combo_index = AI2bl2_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI2bl2_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI2bl2_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI2bl2_typeCombo, 0);
                else if (type == "humidSensor") SensorAIType(AI2bl2_typeCombo, 1);
                else if (type == "di") SensorAIType(AI2bl2_typeCombo, 2);
            }
            else if (AI3bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI3, блок 2
                AI3bl2_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI3bl2_combo.SelectedIndex = AI3bl2_combo.Items.Count - 1;
                AI3bl2combo_text = AI3bl2_combo.SelectedItem.ToString();
                AI3bl2combo_index = AI3bl2_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI3bl2_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI3bl2_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI3bl2_typeCombo, 0);
                else if (type == "humidSensor") SensorAIType(AI3bl2_typeCombo, 1);
                else if (type == "di") SensorAIType(AI3bl2_typeCombo, 2);
            }
            else if (AI4bl3_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor") // "Не выбрано", проверка типа
            { // AI4, блок 3
                AI4bl3_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI4bl3_combo.SelectedIndex = AI4bl3_combo.Items.Count - 1;
                AI4bl3combo_text = AI4bl3_combo.SelectedItem.ToString();
                AI4bl3combo_index = AI4bl3_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI4bl3_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI4bl3_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI4bl3_typeCombo, 3);
                else if (type == "di") SensorAIType(AI4bl3_typeCombo, 2);
            }
            else if (AI5bl3_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor") // "Не выбрано", проверка типа
            { // AI5, блок 3
                AI5bl3_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI5bl3_combo.SelectedIndex = AI5bl3_combo.Items.Count - 1;
                AI5bl3combo_text = AI5bl3_combo.SelectedItem.ToString();
                AI5bl3combo_index = AI5bl3_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI5bl3_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI5bl3_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI5bl3_typeCombo, 3);
                else if (type == "di") SensorAIType(AI5bl3_typeCombo, 2);
            }
            else if (AI6bl3_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor") // "Не выбрано", проверка типа
            { // AI6, блок 3
                AI6bl3_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI6bl3_combo.SelectedIndex = AI6bl3_combo.Items.Count - 1;
                AI6bl3combo_text = AI6bl3_combo.SelectedItem.ToString();
                AI6bl3combo_index = AI6bl3_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI6bl3_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI6bl3_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI6bl3_typeCombo, 3);
                else if (type == "di") SensorAIType(AI6bl3_typeCombo, 2);
            }
            else if (AI1bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI1, блок 3
                AI1bl3_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI1bl3_combo.SelectedIndex = AI1bl3_combo.Items.Count - 1;
                AI1bl3combo_text = AI1bl3_combo.SelectedItem.ToString();
                AI1bl3combo_index = AI1bl3_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI1bl3_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI1bl3_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI1bl3_typeCombo, 0);
                else if (type == "humidSensor") SensorAIType(AI1bl3_typeCombo, 1);
                else if (type == "di") SensorAIType(AI1bl3_typeCombo, 2);
            }
            else if (AI2bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI2, блок 3
                AI2bl3_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI2bl3_combo.SelectedIndex = AI2bl3_combo.Items.Count - 1;
                AI2bl3combo_text = AI2bl3_combo.SelectedItem.ToString();
                AI2bl3combo_index = AI2bl3_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI2bl3_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI2bl3_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI2bl3_typeCombo, 0);
                else if (type == "humidSensor") SensorAIType(AI2bl3_typeCombo, 1);
                else if (type == "di") SensorAIType(AI2bl3_typeCombo, 2);
            }
            else if (AI3bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI3, блок 3
                AI3bl3_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI3bl3_combo.SelectedIndex = AI3bl3_combo.Items.Count - 1;
                AI3bl3combo_text = AI3bl3_combo.SelectedItem.ToString();
                AI3bl3combo_index = AI3bl3_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI3bl3_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI3bl3_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI3bl3_typeCombo, 0);
                else if (type == "humidSensor") SensorAIType(AI3bl3_typeCombo, 1);
                else if (type == "di") SensorAIType(AI3bl3_typeCombo, 2);
            }
        }

        ///<summary>Очистка и блокировка comboBox выбора типа AI</summary>
        private void ClearAITypeCombo(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add(NTC);
            comboBox.SelectedIndex = 0;
            comboBox.Enabled = false;
        }

        ///<summary>Удаление AI из всех comboBox </summary>
        private void SubFromCombosAI(ushort code)
        {
            Ai find_ai, find_ai2;
            string name = "";
            find_ai = list_ai.Find(x => x.Code == code);
            if (find_ai != null) name = find_ai.Name;
            else return;
            subAIcondition = true; // Признак удаления AI, не работает событие indexChanged
            for (int i = 0; i < AI1_combo.Items.Count; i++) // AI1, для всех элементов списка
            {
                if (AI1_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI1_combo.Items.Remove(name);
                    if (AI1_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI1_combo.SelectedIndex = AI1_combo.Items.Count - 1;
                        if (AI1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI1_combo.SelectedItem.ToString(), AI1_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI1_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI1_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI1_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI1_typeCombo); // Обнуление типа для выбора типа AI
                        AI1_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI1_lab.Text = "";
                    }
                    AI1combo_text = AI1_combo.SelectedItem.ToString();
                    AI1combo_index = AI1_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI2_combo.Items.Count; i++) // AI2, для всех элементов списка
            {
                if (AI2_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI2_combo.Items.Remove(name);
                    if (AI2_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI2_combo.SelectedIndex = AI2_combo.Items.Count - 1;
                        if (AI2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI2_combo.SelectedItem.ToString(), AI2_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI2_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI2_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI2_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI2_typeCombo); // Обнуление типа для выбора типа AI
                        AI2_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI2_lab.Text = "";
                    }
                    AI2combo_text = AI2_combo.SelectedItem.ToString();
                    AI2combo_index = AI2_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI3_combo.Items.Count; i++) // AI3, для всех элементов списка
            {
                if (AI3_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI3_combo.Items.Remove(name);
                    if (AI3_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI3_combo.SelectedIndex = AI3_combo.Items.Count - 1;
                        if (AI3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI3_combo.SelectedItem.ToString(), AI3_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI3_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI3_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI3_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI3_typeCombo); // Обнуление типа для выбора типа AI
                        AI3_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI3_lab.Text = "";
                    }
                    AI3combo_text = AI3_combo.SelectedItem.ToString();
                    AI3combo_index = AI3_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI4_combo.Items.Count; i++) // AI4, для всех элементов списка
            {
                if (AI4_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI4_combo.Items.Remove(name);
                    if (AI4_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI4_combo.SelectedIndex = AI4_combo.Items.Count - 1;
                        if (AI4_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI4_combo.SelectedItem.ToString(), AI4_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI4_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI4_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI4_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI4_typeCombo); // Обнуление типа для выбора типа AI
                        AI4_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI4_lab.Text = "";
                    }
                    AI4combo_text = AI4_combo.SelectedItem.ToString();
                    AI4combo_index = AI4_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI5_combo.Items.Count; i++) // AI5, для всех элементов списка
            {
                if (AI5_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI5_combo.Items.Remove(name);
                    if (AI5_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI5_combo.SelectedIndex = AI5_combo.Items.Count - 1;
                        if (AI5_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI5_combo.SelectedItem.ToString(), AI5_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI5_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI5_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI5_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI5_typeCombo); // Обнуление типа для выбора типа AI
                        AI5_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI5_lab.Text = "";
                    }
                    AI5combo_text = AI5_combo.SelectedItem.ToString();
                    AI5combo_index = AI5_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI6_combo.Items.Count; i++) // AI6, для всех элементов списка
            {
                if (AI6_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI6_combo.Items.Remove(name);
                    if (AI6_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI6_combo.SelectedIndex = AI6_combo.Items.Count - 1;
                        if (AI6_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI6_combo.SelectedItem.ToString(), AI6_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI6_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI6_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI6_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI6_typeCombo); // Обнуление типа для выбора типа AI
                        AI6_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI6_lab.Text = "";
                    }
                    AI6combo_text = AI6_combo.SelectedItem.ToString();
                    AI6combo_index = AI6_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI1bl1_combo.Items.Count; i++) // AI1 блок 1, для всех элементов списка
            {
                if (AI1bl1_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI1bl1_combo.Items.Remove(name);
                    if (AI1bl1_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI1bl1_combo.SelectedIndex = AI1bl1_combo.Items.Count - 1;
                        if (AI1bl1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI1bl1_combo.SelectedItem.ToString(), AI1bl1_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI1bl1_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI1bl1_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI1bl1_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI1bl1_typeCombo); // Обнуление типа для выбора типа AI
                        AI1bl1_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI1bl1_lab.Text = "";
                    }
                    AI1bl1combo_text = AI1bl1_combo.SelectedItem.ToString();
                    AI1bl1combo_index = AI1bl1_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI2bl1_combo.Items.Count; i++) // AI2 блок 1, для всех элементов списка
            {
                if (AI2bl1_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI2bl1_combo.Items.Remove(name);
                    if (AI2bl1_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI2bl1_combo.SelectedIndex = AI2bl1_combo.Items.Count - 1;
                        if (AI2bl1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI2bl1_combo.SelectedItem.ToString(), AI2bl1_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI2bl1_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI2bl1_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI2bl1_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI2bl1_typeCombo); // Обнуление типа для выбора типа AI
                        AI2bl1_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI2bl1_lab.Text = "";
                    }
                    AI2bl1combo_text = AI2bl1_combo.SelectedItem.ToString();
                    AI2bl1combo_index = AI2bl1_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI3bl1_combo.Items.Count; i++) // AI3 блок 1, для всех элементов списка
            {
                if (AI3bl1_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI3bl1_combo.Items.Remove(name);
                    if (AI3bl1_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI3bl1_combo.SelectedIndex = AI3bl1_combo.Items.Count - 1;
                        if (AI3bl1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI3bl1_combo.SelectedItem.ToString(), AI3bl1_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI3bl1_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI3bl1_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI3bl1_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI3bl1_typeCombo); // Обнуление типа для выбора типа AI
                        AI3bl1_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI3bl1_lab.Text = "";
                    }
                    AI3bl1combo_text = AI3bl1_combo.SelectedItem.ToString();
                    AI3bl1combo_index = AI3bl1_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI4bl1_combo.Items.Count; i++) // AI4 блок 1, для всех элементов списка
            {
                if (AI4bl1_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI4bl1_combo.Items.Remove(name);
                    if (AI4bl1_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI4bl1_combo.SelectedIndex = AI4bl1_combo.Items.Count - 1;
                        if (AI4bl1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI4bl1_combo.SelectedItem.ToString(), AI4bl1_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI4bl1_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI4bl1_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI4bl1_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI4bl1_typeCombo); // Обнуление типа для выбора типа AI
                        AI4bl1_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI4bl1_lab.Text = "";
                    }
                    AI4bl1combo_text = AI4bl1_combo.SelectedItem.ToString();
                    AI4bl1combo_index = AI4bl1_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI5bl1_combo.Items.Count; i++) // AI5 блок 1, для всех элементов списка
            {
                if (AI5bl1_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI5bl1_combo.Items.Remove(name);
                    if (AI5bl1_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI5bl1_combo.SelectedIndex = AI5bl1_combo.Items.Count - 1;
                        if (AI5bl1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI5bl1_combo.SelectedItem.ToString(), AI5bl1_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI5bl1_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI5bl1_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI5bl1_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI5bl1_typeCombo); // Обнуление типа для выбора типа AI
                        AI5bl1_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI5bl1_lab.Text = "";
                    }
                    AI5bl1combo_text = AI5bl1_combo.SelectedItem.ToString();
                    AI5bl1combo_index = AI5bl1_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI6bl1_combo.Items.Count; i++) // AI6 блок 1, для всех элементов списка
            {
                if (AI6bl1_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI6bl1_combo.Items.Remove(name);
                    if (AI6bl1_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI6bl1_combo.SelectedIndex = AI6bl1_combo.Items.Count - 1;
                        if (AI6bl1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI6bl1_combo.SelectedItem.ToString(), AI6bl1_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI6bl1_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI6bl1_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI6bl1_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI6bl1_typeCombo); // Обнуление типа для выбора типа AI
                        AI6bl1_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI6bl1_lab.Text = "";
                    }
                    AI6bl1combo_text = AI6bl1_combo.SelectedItem.ToString();
                    AI6bl1combo_index = AI6bl1_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI1bl2_combo.Items.Count; i++) // AI1 блок 2, для всех элементов списка
            {
                if (AI1bl2_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI1bl2_combo.Items.Remove(name);
                    if (AI1bl2_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI1bl2_combo.SelectedIndex = AI1bl2_combo.Items.Count - 1;
                        if (AI1bl2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI1bl2_combo.SelectedItem.ToString(), AI1bl2_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI1bl2_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI1bl2_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI1bl2_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI1bl2_typeCombo); // Обнуление типа для выбора типа AI
                        AI1bl2_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI1bl2_lab.Text = "";
                    }
                    AI1bl2combo_text = AI1bl2_combo.SelectedItem.ToString();
                    AI1bl2combo_index = AI1bl2_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI2bl2_combo.Items.Count; i++) // AI2 блок 2, для всех элементов списка
            {
                if (AI2bl2_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI2bl2_combo.Items.Remove(name);
                    if (AI2bl2_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI2bl2_combo.SelectedIndex = AI2bl2_combo.Items.Count - 1;
                        if (AI2bl2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI2bl2_combo.SelectedItem.ToString(), AI2bl2_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI2bl2_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI2bl2_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI2bl2_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI2bl2_typeCombo); // Обнуление типа для выбора типа AI
                        AI2bl2_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI2bl2_lab.Text = "";
                    }
                    AI2bl2combo_text = AI2bl2_combo.SelectedItem.ToString();
                    AI2bl2combo_index = AI2bl2_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI3bl2_combo.Items.Count; i++) // AI3 блок 2, для всех элементов списка
            {
                if (AI3bl2_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI3bl2_combo.Items.Remove(name);
                    if (AI3bl2_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI3bl2_combo.SelectedIndex = AI3bl2_combo.Items.Count - 1;
                        if (AI3bl2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI3bl2_combo.SelectedItem.ToString(), AI3bl2_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI3bl2_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI3bl2_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI3bl2_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI3bl2_typeCombo); // Обнуление типа для выбора типа AI
                        AI3bl2_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI3bl2_lab.Text = "";
                    }
                    AI3bl2combo_text = AI3bl2_combo.SelectedItem.ToString();
                    AI3bl2combo_index = AI3bl2_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI4bl2_combo.Items.Count; i++) // AI4 блок 2, для всех элементов списка
            {
                if (AI4bl2_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI4bl2_combo.Items.Remove(name);
                    if (AI4bl2_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI4bl2_combo.SelectedIndex = AI4bl2_combo.Items.Count - 1;
                        if (AI4bl2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI4bl2_combo.SelectedItem.ToString(), AI4bl2_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI4bl2_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI4bl2_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI4bl2_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI4bl2_typeCombo); // Обнуление типа для выбора типа AI
                        AI4bl2_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI4bl2_lab.Text = "";
                    }
                    AI4bl2combo_text = AI4bl2_combo.SelectedItem.ToString();
                    AI4bl2combo_index = AI4bl2_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI5bl2_combo.Items.Count; i++) // AI5 блок 2, для всех элементов списка
            {
                if (AI5bl2_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI5bl2_combo.Items.Remove(name);
                    if (AI5bl2_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI5bl2_combo.SelectedIndex = AI5bl2_combo.Items.Count - 1;
                        if (AI5bl2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI5bl2_combo.SelectedItem.ToString(), AI5bl2_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI5bl2_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI5bl2_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI5bl2_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI5bl2_typeCombo); // Обнуление типа для выбора типа AI
                        AI5bl2_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI5bl2_lab.Text = "";
                    }
                    AI5bl2combo_text = AI5bl2_combo.SelectedItem.ToString();
                    AI5bl2combo_index = AI5bl2_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI6bl2_combo.Items.Count; i++) // AI6 блок 2, для всех элементов списка
            {
                if (AI6bl2_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI6bl2_combo.Items.Remove(name);
                    if (AI6bl2_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI6bl2_combo.SelectedIndex = AI6bl2_combo.Items.Count - 1;
                        if (AI6bl2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI6bl2_combo.SelectedItem.ToString(), AI6bl2_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI6bl2_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI6bl2_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI6bl2_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI6bl2_typeCombo); // Обнуление типа для выбора типа AI
                        AI6bl2_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI6bl2_lab.Text = "";
                    }
                    AI6bl2combo_text = AI6bl2_combo.SelectedItem.ToString();
                    AI6bl2combo_index = AI6bl2_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI1bl3_combo.Items.Count; i++) // AI1 блок 3, для всех элементов списка
            {
                if (AI1bl3_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI1bl3_combo.Items.Remove(name);
                    if (AI1bl3_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI1bl3_combo.SelectedIndex = AI1bl3_combo.Items.Count - 1;
                        if (AI1bl3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI1bl3_combo.SelectedItem.ToString(), AI1bl3_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI1bl3_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI1bl3_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI1bl3_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI1bl3_typeCombo); // Обнуление типа для выбора типа AI
                        AI1bl3_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI1bl3_lab.Text = "";
                    }
                    AI1bl3combo_text = AI1bl3_combo.SelectedItem.ToString();
                    AI1bl3combo_index = AI1bl3_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI2bl3_combo.Items.Count; i++) // AI2 блок 3, для всех элементов списка
            {
                if (AI2bl3_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI2bl3_combo.Items.Remove(name);
                    if (AI2bl3_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI2bl3_combo.SelectedIndex = AI2bl3_combo.Items.Count - 1;
                        if (AI2bl3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI2bl3_combo.SelectedItem.ToString(), AI2bl3_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI2bl3_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI2bl3_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI2bl3_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI2bl3_typeCombo); // Обнуление типа для выбора типа AI
                        AI2bl3_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI2bl3_lab.Text = "";
                    }
                    AI2bl3combo_text = AI2bl3_combo.SelectedItem.ToString();
                    AI2bl3combo_index = AI2bl3_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI3bl3_combo.Items.Count; i++) // AI3 блок 3, для всех элементов списка
            {
                if (AI3bl3_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI3bl3_combo.Items.Remove(name);
                    if (AI3bl3_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI3bl3_combo.SelectedIndex = AI3bl3_combo.Items.Count - 1;
                        if (AI3bl3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI3bl3_combo.SelectedItem.ToString(), AI3bl3_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI3bl3_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI3bl3_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI3bl3_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI3bl3_typeCombo); // Обнуление типа для выбора типа AI
                        AI3bl3_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI3bl3_lab.Text = "";
                    }
                    AI3bl3combo_text = AI3bl3_combo.SelectedItem.ToString();
                    AI3bl3combo_index = AI3bl3_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI4bl3_combo.Items.Count; i++) // AI4 блок 3, для всех элементов списка
            {
                if (AI4bl3_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI4bl3_combo.Items.Remove(name);
                    if (AI4bl3_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI4bl3_combo.SelectedIndex = AI4bl3_combo.Items.Count - 1;
                        if (AI4bl3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI4bl3_combo.SelectedItem.ToString(), AI4bl3_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI4bl3_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI4bl3_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI4bl3_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI4bl3_typeCombo); // Обнуление типа для выбора типа AI
                        AI4bl3_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI4bl3_lab.Text = "";
                    }
                    AI4bl3combo_text = AI4bl3_combo.SelectedItem.ToString();
                    AI4bl3combo_index = AI4bl3_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI5bl3_combo.Items.Count; i++) // AI5 блок 3, для всех элементов списка
            {
                if (AI5bl3_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI5bl3_combo.Items.Remove(name);
                    if (AI5bl3_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI5bl3_combo.SelectedIndex = AI5bl3_combo.Items.Count - 1;
                        if (AI5bl3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI5bl3_combo.SelectedItem.ToString(), AI5bl3_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI5bl3_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI5bl3_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI5bl3_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI5bl3_typeCombo); // Обнуление типа для выбора типа AI
                        AI5bl3_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI5bl3_lab.Text = "";
                    }
                    AI5bl3combo_text = AI5bl3_combo.SelectedItem.ToString();
                    AI5bl3combo_index = AI5bl3_combo.SelectedIndex;
                }
            }
            for (int i = 0; i < AI6bl3_combo.Items.Count; i++) // AI6 блок 3, для всех элементов списка
            {
                if (AI6bl3_combo.Items[i].ToString() == name) // Есть совпадение по имени
                {
                    AI6bl3_combo.Items.Remove(name);
                    if (AI6bl3_combo.Items.Count > 1) // Больше одного элемента
                    {
                        AI6bl3_combo.SelectedIndex = AI6bl3_combo.Items.Count - 1;
                        if (AI6bl3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        { // Был выбран другой сигнал из списка
                            SubFromCombosAI(AI6bl3_combo.SelectedItem.ToString(), AI6bl3_combo);
                            find_ai2 = list_ai.Find(x => x.Name == AI6bl3_combo.SelectedItem.ToString());
                            if (find_ai2 != null)
                            {
                                list_ai.Remove(find_ai2);
                                //find_ai2.Select();
                                //list_ai.Add(find_ai2);
                                if (find_ai2.Type == "di")
                                {
                                    if (showCode) AI6bl3_lab.Text = (find_ai2.Code + 1000).ToString();
                                }
                                else
                                {
                                    if (showCode) AI6bl3_lab.Text = find_ai2.Code.ToString();
                                }
                            }
                        }
                    }
                    else // Последний элемент списка
                    {
                        ClearAITypeCombo(AI6bl3_typeCombo); // Обнуление типа для выбора типа AI
                        AI6bl3_combo.SelectedItem = NOT_SELECTED; // Выбор "Не выбрано"
                        AI6bl3_lab.Text = "";
                    }
                    AI6bl3combo_text = AI6bl3_combo.SelectedItem.ToString();
                    AI6bl3combo_index = AI6bl3_combo.SelectedIndex;
                }
            }
            subAIcondition = false;
            list_ai.Remove(find_ai); // Удаление сигнала из списка AI
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Выбрали основной нагреватель</summary>
        private void HeaterCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            Ai find_ai;
            ushort code_1 = 9; // Датчик обратной воды калорифера
            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0) // Водяной калорифер
            {
                find_ai = list_ai.Find(x => x.Code == code_1);
                if (find_ai == null) // Нет такой записи
                {
                    list_ai.Add(new Ai("Датчик обратной воды калорифера", code_1, "sensor"));
                    AddNewAI(code_1, "sensor"); // Добавление AI к свободному comboBox
                }
            }
            else // Отмена выбора нагревателя
            {
                SubFromCombosAI(code_1);
            }
        }

        ///<summary>Изменили тип основного нагревателя</summary>
        private void HeatTypeCombo_signalsAISelectedIndexChanged(object sender, EventArgs e)
        {
            Ai find_ai;
            ushort code_1 = 9; // Датчик обратной воды калорифера
            if (heaterCheck.Checked) // Когда выбран нагреватель
            {
                if (heatTypeCombo.SelectedIndex == 0) // Водяной калорифер
                {
                    find_ai = list_ai.Find(x => x.Code == code_1);
                    if (find_ai == null) // Нет такой записи
                    {
                        list_ai.Add(new Ai("Датчик обратной воды калорифера", code_1, "sensor"));
                        AddNewAI(code_1, "sensor"); // Добавление AI к свободному comboBox
                    }
                }
                else if (heatTypeCombo.SelectedIndex == 1) // Электрокалорифер
                {
                    SubFromCombosAI(code_1);
                }
            }
        }

        ///<summary>Выбрали догреватель, второй нагреватель</summary>
        private void AddHeatCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            Ai find_ai;
            ushort code_1 = 11; // Датчик обратной воды догревателя
            if (addHeatCheck.Checked) // Когда выбран догреватель
            {
                if (heatAddTypeCombo.SelectedIndex == 0) // Водяной догреватель
                {
                    find_ai = list_ai.Find(x => x.Code == code_1);
                    if (find_ai == null) // Нет такой записи
                    {
                        list_ai.Add(new Ai("Датчик обратной воды догревателя", code_1, "sensor"));
                        AddNewAI(code_1, "sensor"); // Добавление AI к свободному comboBox
                    }
                }
            }
            else // Отмена выбора догревателя
            {
                SubFromCombosAI(code_1);
            }
        }

        ///<summary>Изменили тип догревателя</summary>
        private void HeatAddTypeCombo_signalsAISelectedIndexChanged(object sender, EventArgs e)
        {
            Ai find_ai;
            ushort code_1 = 11; // Датчик обратной воды догревателя
            if (addHeatCheck.Checked) // Когда выбран нагреватель
            {
                if (heatAddTypeCombo.SelectedIndex == 0) // Водяной догреватель
                {
                    find_ai = list_ai.Find(x => x.Code == code_1);
                    if (find_ai == null) // Нет такой записи
                    {
                        list_ai.Add(new Ai("Датчик обратной воды догревателя", code_1, "sensor"));
                        AddNewAI(code_1, "sensor"); // Добавление AI к свободному comboBox
                    }
                }
                else if (heatAddTypeCombo.SelectedIndex == 1) // Электрокалорифер
                {
                    SubFromCombosAI(code_1);
                }
            }
        }

        ///<summary>Выбрали рекуператор</summary>
        private void RecupCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            Ai find_ai;
            ushort code_1 = 14; // Датчик температуры защиты рекуператора
            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)
            {
                if (recDefTempCheck.Checked) // Выбрана защита по температурному датчику
                {
                    find_ai = list_ai.Find(x => x.Code == code_1);
                    if (find_ai == null) // Нет такой записи
                    {
                        list_ai.Add(new Ai("Датчик температуры защиты рекуператора", code_1, "sensor"));
                        AddNewAI(code_1, "sensor"); // Добавление AI к свободному comboBox
                    }
                }
            }
            else // Отмена выбора рекуператора
            {
                SubFromCombosAI(code_1);
            }
        }

        ///<summary>Выбрали температурный датчик защиты рекуператора</summary>
        private void RecDefTempCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            Ai find_ai;
            ushort code_1 = 14; // Датчик температуры защиты рекуператора
            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked) // ПВ и рекуператор
            {
                if (recDefTempCheck.Checked) // Выбрали защиту по температурному датчику
                {
                    find_ai = list_ai.Find(x => x.Code == code_1);
                    if (find_ai == null) // Нет такой записи
                    {
                        list_ai.Add(new Ai("Датчик температуры защиты рекуператора", code_1, "sensor"));
                        AddNewAI(code_1, "sensor"); // Добавление AI к свободному comboBox
                    }
                }
                else // Отмена выбора датчика
                {
                    SubFromCombosAI(code_1);
                }
            }
        }

        ///<summary>Выбрали канальный датчик температуры</summary>
        private void PrChanSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            Ai find_ai;
            ushort code_1 = 1; // Канальный датчик температуры
            if (prChanSensCheck.Checked) // Выбрали датчик 
            {
                find_ai = list_ai.Find(x => x.Code == code_1);
                if (find_ai == null) // Нет такой записи
                {
                    list_ai.Add(new Ai("Канальный датчик Т приточного воздуха", code_1, "sensor"));
                    AddNewAI(code_1, "sensor"); // Добавление AI к свободному comboBox
                }
            }
            else // Отмена выбора датчика
            {
                SubFromCombosAI(code_1);
            }
        }

        ///<summary>Выбрали комнатный датчик температуры</summary>
        private void RoomTempSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            Ai find_ai;
            ushort code_1 = 4; // Комнатный датчик температуры
            if (roomTempSensCheck.Checked) // Выбрали датчик 
            {
                find_ai = list_ai.Find(x => x.Code == code_1);
                if (find_ai == null) // Нет такой записи
                {
                    list_ai.Add(new Ai("Датчик температуры комнатный", code_1, "sensor"));
                    AddNewAI(code_1, "sensor"); // Добавление AI к свободному comboBox
                }
            }
            else // Отмена выбора датчика
            {
                SubFromCombosAI(code_1);
            }
        }

        ///<summary>Выбрали канальный датчик влажности</summary>
        private void ChanHumSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            Ai find_ai;
            ushort code_1 = 17; // Канальный датчик влажности
            if (chanHumSensCheck.Checked) // Выбрали датчик 
            {
                find_ai = list_ai.Find(x => x.Code == code_1);
                if (find_ai == null) // Нет такой записи
                {
                    list_ai.Add(new Ai("Канальный датчик влажности", code_1, "humidSensor"));
                    AddNewAI(code_1, "humidSensor"); // Добавление AI к свободному comboBox
                }
            }
            else // Отмена выбора датчика
            {
                SubFromCombosAI(code_1);
            }
        }

        ///<summary>Выбрали комнатный датчик влажности</summary>
        private void RoomHumSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            Ai find_ai;
            ushort code_1 = 18; // Комнатный датчик влажности
            if (roomHumSensCheck.Checked) // Выбрали датчик 
            {
                find_ai = list_ai.Find(x => x.Code == code_1);
                if (find_ai == null) // Нет такой записи
                {
                    list_ai.Add(new Ai("Комнатный датчик влажности", code_1, "humidSensor"));
                    AddNewAI(code_1, "humidSensor"); // Добавление AI к свободному comboBox
                }
            }
            else // Отмена выбора датчика
            {
                SubFromCombosAI(code_1);
            }
        }

        ///<summary>Выбрали датчик температуры наружного воздуха</summary>
        private void OutdoorChanSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            Ai find_ai;
            ushort code_1 = 3; // Датчик температуры наружного воздуха
            if (outdoorChanSensCheck.Checked) // Выбрали датчик 
            {
                find_ai = list_ai.Find(x => x.Code == code_1);
                if (find_ai == null) // Нет такой записи
                {
                    list_ai.Add(new Ai("Датчик температуры наружного воздуха", code_1, "sensor"));
                    AddNewAI(code_1, "sensor"); // Добавление AI к свободному comboBox
                }
            }
            else // Отмена выбора датчика
            {
                SubFromCombosAI(code_1);
            }
        }

        ///<summary>Выбрали канальный датчик Т вытяжного воздуха</summary>
        private void OutChanSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            Ai find_ai;
            ushort code_1 = 5; // Канальный датчик температуры вытяжного воздуха
            if (outChanSensCheck.Checked) // Выбрали датчик 
            {
                find_ai = list_ai.Find(x => x.Code == code_1);
                if (find_ai == null) // Нет такой записи
                {
                    list_ai.Add(new Ai("Канальный датчик Т вытяжного воздуха", code_1, "sensor"));
                    AddNewAI(code_1, "sensor"); // Добавление AI к свободному comboBox
                }
            }
            else // Отмена выбора датчика
            {
                SubFromCombosAI(code_1);
            }
        }
    }
}