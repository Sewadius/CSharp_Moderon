using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Moderon
{
    class Di
    {
        public string Name { get; private set; }
        public ushort Code { get; private set; }
        public bool Active { get; private set; } = true; // Свободен по умолчанию
        public Di(string name, ushort code)
        {
            Name = name; Code = code;
        }
        public Di(string name, ushort code, bool active)
        {
            Name = name; Code = code; Active = active;
        }
        public void Select() => Active = false;
        public void Dispose() => Active = true;
    }

    public partial class Form1 : Form
    {
        readonly List<Di> list_di = new List<Di>();

        bool subDIcondition = false; // Условие при удалении DI

        // Сохранение наименования выбранного элемента для ПЛК и блоков 1, 2, 3
        string
            DI1combo_text, DI2combo_text, DI3combo_text, DI4combo_text, DI5combo_text,
            DI1bl1combo_text, DI2bl1combo_text, DI3bl1combo_text, DI4bl1combo_text, DI5bl1combo_text,
            DI1bl2combo_text, DI2bl2combo_text, DI3bl2combo_text, DI4bl2combo_text, DI5bl2combo_text,
            DI1bl3combo_text, DI2bl3combo_text, DI3bl3combo_text, DI4bl3combo_text, DI5bl3combo_text;

        // Сохранение прошлого индекса comboBox для ПЛК и блоков 1, 2, 3
        int
            DI1combo_index, DI2combo_index, DI3combo_index, DI4combo_index, DI5combo_index,
            DI1bl1combo_index, DI2bl1combo_index, DI3bl1combo_index, DI4bl1combo_index, DI5bl1combo_index,
            DI1bl2combo_index, DI2bl2combo_index, DI3bl2combo_index, DI4bl2combo_index, DI5bl2combo_index,
            DI1bl3combo_index, DI2bl3combo_index, DI3bl3combo_index, DI4bl3combo_index, DI5bl3combo_index;



        ///<summary>Начальная установка для сигналов DI таблицы сигналов</summary> 
        public void Set_DIComboTextIndex()
        {
            var di_signals = new List<string>()
            {
                DI1combo_text, DI2combo_text, DI3combo_text, DI4combo_text, DI5combo_text,
                DI1bl1combo_text, DI2bl1combo_text, DI3bl1combo_text, DI4bl1combo_text, DI5bl1combo_text,
                DI1bl2combo_text, DI2bl2combo_text, DI3bl2combo_text, DI4bl2combo_text, DI5bl2combo_text,
                DI1bl3combo_text, DI2bl3combo_text, DI3bl3combo_text, DI4bl3combo_text, DI5bl3combo_text
            };

            var di_signals_index = new List<int>()
            {
                DI1combo_index, DI2combo_index, DI3combo_index, DI4combo_index, DI5combo_index,
                DI1bl1combo_index, DI2bl1combo_index, DI3bl1combo_index, DI4bl1combo_index, DI5bl1combo_index,
                DI1bl2combo_index, DI2bl2combo_index, DI3bl2combo_index, DI4bl2combo_index, DI5bl2combo_index,
                DI1bl3combo_index, DI2bl3combo_index, DI3bl3combo_index, DI4bl3combo_index, DI5bl3combo_index
            };

            for (var i = 0; i < di_signals.Count; i++)
                di_signals[i] = NOT_SELECTED;

            for (var i = 0; i < di_signals_index.Count; i++)
                di_signals_index[i] = 0;
        }

        ///<summary>Сброс выбора сигналов DI comboBox</summary>
        private void ResetButton_signalsDIClick(object sender, EventArgs e)
        {
            var di_combos = new List<ComboBox>()
            {
                DI1_combo, DI2_combo, DI3_combo, DI4_combo, DI5_combo,
                DI1bl1_combo, DI2bl1_combo, DI3bl1_combo, DI4bl1_combo, DI5bl1_combo,
                DI1bl2_combo, DI2bl2_combo, DI3bl2_combo, DI4bl2_combo, DI5bl2_combo,
                DI1bl3_combo, DI2bl3_combo, DI3bl3_combo, DI4bl3_combo, DI5bl3_combo
            };

            foreach (var el in di_combos)
            {
                el.Items.Clear(); el.Items.Add(NOT_SELECTED);
            }
        }

        ///<summary>Удаление элемента из всех comboBox DI</summary>
        private void RemoveFromAllCombosDI(string name)
        {
            var di_combos = new List<ComboBox>()
            {
                DI1_combo, DI2_combo, DI3_combo, DI4_combo, DI5_combo,
                DI1bl1_combo, DI2bl1_combo, DI3bl1_combo, DI4bl1_combo, DI5bl1_combo,
                DI1bl2_combo, DI2bl2_combo, DI3bl2_combo, DI4bl2_combo, DI5bl2_combo,
                DI1bl3_combo, DI2bl3_combo, DI3bl3_combo, DI4bl3_combo, DI5bl3_combo,
            };

            foreach (var el in di_combos) el.Items.Remove(name);
        }

        ///<summary>Добавление элемента ко всем comboBox DI</summary>
        private void AddToAllCombosDI(string name)
        {
            var di_combos = new List<ComboBox>()
            {
                DI1_combo, DI2_combo, DI3_combo, DI4_combo, DI5_combo,
                DI1bl1_combo, DI2bl1_combo, DI3bl1_combo, DI4bl1_combo, DI5bl1_combo,
                DI1bl2_combo, DI2bl2_combo, DI3bl2_combo, DI4bl2_combo, DI5bl2_combo,
                DI1bl3_combo, DI2bl3_combo, DI3bl3_combo, DI4bl3_combo, DI5bl3_combo,
            };

            foreach (var el in di_combos) el.Items.Add(name);
        }

        ///<summary>Метод для изменения DI comboBox</summary>
        private void DI_combo_SelectedIndexChanged(ComboBox comboBox, ref int combo_index, ref string combo_text, ref Label label)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return;                                                         // Переход из вычета сигналов DI
            if (comboBox.SelectedIndex == combo_index) return;                                  // Индекс не поменялся
            if (comboBox.SelectedIndex == 0)                                                    // Выбрали "Не выбрано"
            {
                if (comboBox.Items.Count > 1)                                                   // Больше одного элемента в списке
                {
                    string nameFind = combo_text;
                    di_find = list_di.Find(x => x.Name == nameFind);
                    list_di.Remove(di_find);                 // Удаление из списка
                    if (showCode) label.Text = "";
                }
                if (di_find != null)                                                            // Найден элемент
                {
                    di_find.Dispose();                                                          // Освобождение сигнала для распределенния
                    list_di.Add(di_find);                                                       // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(combo_text, comboBox);                  // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = string.Concat(comboBox.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name));                              // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name));                              // Удаление из списка AI
                RemoveFromAllCombosAI(name);                                                    // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) label.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals)                                                       // Если не начальная расстановка
                {
                    SubFromCombosDI(name, comboBox);                                            // Удаление из других DI
                    string nameFind = combo_text;
                    di_find = list_di.Find(x => x.Name == nameFind);
                    list_di.Remove(di_find);
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(combo_text, comboBox);                                        // Добавление к другим DI
                }
            }
            combo_text = comboBox.SelectedItem.ToString();                                      // Сохранение названия выбранного элемента
            combo_index = comboBox.SelectedIndex;                                               // Сохранение индекса выбранного элемента
            CheckSignalsReady();                                                                // Проверка распределения сигналов
        }

        ///<summary>Изменили DI1 comboBox</summary>
        private void DI1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI1_combo, ref DI1combo_index, ref DI1combo_text, ref DI1_lab);
        }

        ///<summary>Изменили DI2 comboBox</summary>
        private void DI2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI2_combo, ref DI2combo_index, ref DI2combo_text, ref DI2_lab);
        }

        ///<summary>Изменили DI3 comboBox</summary>
        private void DI3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI3_combo, ref DI3combo_index, ref DI3combo_text, ref DI3_lab);
        }

        ///<summary>Изменили DI4 comboBox</summary>
        private void DI4_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI4_combo, ref DI4combo_index, ref DI4combo_text, ref DI4_lab);
        }

        ///<summary>Изменили DI5 comboBox</summary>
        private void DI5_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI5_combo, ref DI5combo_index, ref DI5combo_text, ref DI5_lab);
        }

        ///<summary>Изменили DI1 блока расширения 1</summary>
        private void DI1bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI1bl1_combo, ref DI1bl1combo_index, ref DI1bl1combo_text, ref DI1bl1_lab);
        }

        ///<summary>Изменили DI2 блока расширения 1</summary>
        private void DI2bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI2bl1_combo, ref DI2bl1combo_index, ref DI2bl1combo_text, ref DI2bl1_lab);
        }

        ///<summary>Изменили DI3 блока расширения 1</summary>
        private void DI3bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI3bl1_combo, ref DI3bl1combo_index, ref DI3bl1combo_text, ref DI3bl1_lab);
        }

        ///<summary>Изменили DI4 блока расширения 1</summary>
        private void DI4bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI4bl1_combo, ref DI4bl1combo_index, ref DI4bl1combo_text, ref DI4bl1_lab);
        }

        ///<summary>Изменили DI5 блока расширения 1</summary>
        private void DI5bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI5bl1_combo, ref DI5bl1combo_index, ref DI5bl1combo_text, ref DI5bl1_lab);
        }

        ///<summary>Изменили DI1 блока расширения 2</summary>
        private void DI1bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI1bl2_combo, ref DI1bl2combo_index, ref DI1bl2combo_text, ref DI1bl2_lab);
        }

        ///<summary>Изменили DI2 блока расширения 2</summary>
        private void DI2bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI2bl2_combo, ref DI2bl2combo_index, ref DI2bl2combo_text, ref DI2bl2_lab);
        }

        ///<summary>Изменили DI3 блока расширения 2</summary>
        private void DI3bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI3bl2_combo, ref DI3bl2combo_index, ref DI3bl2combo_text, ref DI3bl2_lab);
        }

        ///<summary>Изменили DI4 блока расширения 2</summary>
        private void DI4bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI4bl2_combo, ref DI4bl2combo_index, ref DI4bl2combo_text, ref DI4bl2_lab);
        }

        ///<summary>Изменили DI5 блока расширения 2</summary>
        private void DI5bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI5bl2_combo, ref DI5bl2combo_index, ref DI5bl2combo_text, ref DI5bl2_lab);
        }

        ///<summary>Изменили DI1 блока расширения 3</summary>
        private void DI1bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI1bl3_combo, ref DI1bl3combo_index, ref DI1bl3combo_text, ref DI1bl3_lab);
        }

        ///<summary>Изменили DI2 блока расширения 3</summary>
        private void DI2bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI2bl3_combo, ref DI2bl3combo_index, ref DI2bl3combo_text, ref DI2bl3_lab);
        }

        ///<summary>Изменили DI3 блока расширения 3</summary>
        private void DI3bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI1bl3_combo, ref DI1bl3combo_index, ref DI1bl3combo_text, ref DI1bl3_lab);
        }

        ///<summary>Изменили DI4 блока расширения 3</summary>
        private void DI4bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI4bl3_combo, ref DI4bl3combo_index, ref DI4bl3combo_text, ref DI4bl3_lab);
        }

        ///<summary>Изменили DI5 блока расширения 3</summary>
        private void DI5bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DI_combo_SelectedIndexChanged(DI5bl3_combo, ref DI5bl3combo_index, ref DI5bl3combo_text, ref DI5bl3_lab);
        }

        ///<summary>Добавление DI в другие слоты для выбора в comboBox</summary>
        private void AddToCombo_DI(string name, ComboBox cm, ref ComboBox comboBox)
        {
            bool notFound = true;                                           // Элемент в списке не найден изначально
            if (comboBox != cm)                                             // Проверка текущего comboBox с проверяемым
            {
                Di di_find = list_di.Find(x => x.Name  == name);
                if (di_find != null)                                        // Есть такой DI в списке дискретных входов
                {
                    foreach (var el in comboBox.Items)                      // Если нет такого названия в списке
                        if (el.ToString() == name) notFound = false;
                    if (notFound) comboBox.Items.Add(di_find.Name);
                    notFound = true;
                }
            }
        }

        ///<summary>Добавление освободившегося DI к остальным comboBox</summary>
        private void AddtoCombosDI(string name, ComboBox cm)
        {
            // ПЛК
            AddToCombo_DI(name, cm, ref DI1_combo); AddToCombo_DI(name, cm, ref DI2_combo); AddToCombo_DI(name, cm, ref DI3_combo);
            AddToCombo_DI(name, cm, ref DI4_combo); AddToCombo_DI(name, cm, ref DI5_combo);
            // Блок расширения 1
            AddToCombo_DI(name, cm, ref DI1bl1_combo); AddToCombo_DI(name, cm, ref DI2bl1_combo); AddToCombo_DI(name, cm, ref DI3bl1_combo);
            AddToCombo_DI(name, cm, ref DI4bl1_combo); AddToCombo_DI(name, cm, ref DI5bl1_combo);
            // Блок расширения 2
            AddToCombo_DI(name, cm, ref DI1bl2_combo); AddToCombo_DI(name, cm, ref DI2bl2_combo); AddToCombo_DI(name, cm, ref DI3bl2_combo);
            AddToCombo_DI(name, cm, ref DI4bl2_combo); AddToCombo_DI(name, cm, ref DI5bl2_combo);
            // Блок расширения 3
            AddToCombo_DI(name, cm, ref DI1bl3_combo); AddToCombo_DI(name, cm, ref DI2bl3_combo); AddToCombo_DI(name, cm, ref DI3bl3_combo);
            AddToCombo_DI(name, cm, ref DI4bl3_combo); AddToCombo_DI(name, cm, ref DI5bl3_combo);
        }

        ///<summary>Удаление DI из других comboBox</summary>
        private void SubFromCombosDI(string name, ComboBox comboBox)
        {
            var di_combos = new List<ComboBox>()
            {
                DI1_combo, DI2_combo, DI3_combo, DI4_combo, DI5_combo,
                DI1bl1_combo, DI2bl1_combo, DI3bl1_combo, DI4bl1_combo, DI5bl1_combo,
                DI1bl2_combo, DI2bl2_combo, DI3bl2_combo, DI4bl2_combo, DI5bl2_combo,
                DI1bl3_combo, DI2bl3_combo, DI3bl3_combo, DI4bl3_combo, DI5bl3_combo
            };

            foreach (var el in di_combos)
                if (el != comboBox && name != NOT_SELECTED) el.Items.Remove(name);
        }

        ///<summary>Добавление нового DI и его назначение для переданного comboBox</summary>
        private void SelectComboBox_DI(ComboBox cm, ushort code, Label label, string text, int index)
        {
            cm.Items.Add(list_di.Find(x => x.Code == code).Name);
            cm.SelectedIndex = cm.Items.Count - 1;
            text = cm.SelectedItem.ToString();
            index = cm.SelectedIndex;
            if (showCode) label.Text = code.ToString();
            list_di.Find(x => x.Code == code).Select();
        }

        ///<summary>Добавление нового DI и его назначение для переданного comboBox к сигналам AI</summary>
        private void SelectComboBoxDI_to_AI(ComboBox cm, ComboBox typeCombo, ushort code, Label label, string text, int index)
        {
            string name = list_di.Find(x => x.Code == code).Name;
            cm.Items.Add(name);
            RemoveFromAllCombosDI(name);
            SensorAIType(typeCombo, 2);
            if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
            // Выбор последнего добавленного элемента
            cm.SelectedIndex = cm.Items.Count - 1;
            text = cm.SelectedItem.ToString();
            index = cm.SelectedIndex;
            if (showCode) label.Text = (code + 1000).ToString();
        }


        ///<summary>Добавление нового DI и его назначение под выход, автораспределение</summary>
        private void AddNewDI(ushort code)
        {
            // ПЛК
            if (DI1_combo.SelectedIndex == 0) SelectComboBox_DI(DI1_combo, code, DI1_lab, DI1combo_text, DI1combo_index);
            else if (DI2_combo.SelectedIndex == 0) SelectComboBox_DI(DI2_combo, code, DI2_lab, DI2combo_text, DI2combo_index);
            else if (DI3_combo.SelectedIndex == 0) SelectComboBox_DI(DI3_combo, code, DI3_lab, DI3combo_text, DI3combo_index);
            else if (DI4_combo.SelectedIndex == 0) SelectComboBox_DI(DI4_combo, code, DI4_lab, DI4combo_text, DI4combo_index);
            else if (DI5_combo.SelectedIndex == 0) SelectComboBox_DI(DI5_combo, code, DI5_lab, DI5combo_text, DI5combo_index);
            else if (AI4_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI4_combo, AI4_typeCombo, code, AI4_lab, AI4combo_text, AI4combo_index);
            else if (AI5_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI5_combo, AI5_typeCombo, code, AI5_lab, AI5combo_text, AI5combo_index);
            else if (AI6_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI6_combo, AI6_typeCombo, code, AI6_lab, AI6combo_text, AI6combo_index);
            else if (AI1_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI1_combo, AI1_typeCombo, code, AI1_lab, AI1combo_text, AI1combo_index);
            else if (AI2_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI2_combo, AI2_typeCombo, code, AI2_lab, AI2combo_text, AI2combo_index);
            else if (AI3_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI3_combo, AI3_typeCombo, code, AI3_lab, AI3combo_text, AI3combo_index);
            // Блок расширения 1
            else if (DI1bl1_combo.SelectedIndex == 0) SelectComboBox_DI(DI1bl1_combo, code, DI1bl1_lab, DI1bl1combo_text, DI1bl1combo_index);
            else if (DI2bl1_combo.SelectedIndex == 0) SelectComboBox_DI(DI2bl1_combo, code, DI2bl1_lab, DI2bl1combo_text, DI2bl1combo_index);
            else if (DI3bl1_combo.SelectedIndex == 0) SelectComboBox_DI(DI3bl1_combo, code, DI3bl1_lab, DI3bl1combo_text, DI3bl1combo_index);
            else if (DI4bl1_combo.SelectedIndex == 0) SelectComboBox_DI(DI4bl1_combo, code, DI4bl1_lab, DI4bl1combo_text, DI4bl1combo_index);
            else if (DI5bl1_combo.SelectedIndex == 0) SelectComboBox_DI(DI5bl1_combo, code, DI5bl1_lab, DI5bl1combo_text, DI5bl1combo_index);
            else if (AI4bl1_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI4bl1_combo, AI4bl1_typeCombo, code, AI4bl1_lab, AI4bl1combo_text, AI4bl1combo_index);
            else if (AI5bl1_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI5bl1_combo, AI5bl1_typeCombo, code, AI5bl1_lab, AI5bl1combo_text, AI5bl1combo_index);
            else if (AI6bl1_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI6bl1_combo, AI6bl1_typeCombo, code, AI6bl1_lab, AI6bl1combo_text, AI6bl1combo_index);
            else if (AI1bl1_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI1bl1_combo, AI1bl1_typeCombo, code, AI1bl1_lab, AI1bl1combo_text, AI1bl1combo_index);
            else if (AI2bl1_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI2bl1_combo, AI2bl1_typeCombo, code, AI2bl1_lab, AI2bl1combo_text, AI2bl1combo_index);
            else if (AI3bl1_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI3bl1_combo, AI3bl1_typeCombo, code, AI3bl1_lab, AI3bl1combo_text, AI3bl1combo_index);
            // Блок расширения 2
            else if (DI1bl2_combo.SelectedIndex == 0) SelectComboBox_DI(DI1bl2_combo, code, DI1bl2_lab, DI1bl2combo_text, DI1bl2combo_index);
            else if (DI2bl2_combo.SelectedIndex == 0) SelectComboBox_DI(DI2bl2_combo, code, DI2bl2_lab, DI2bl2combo_text, DI2bl2combo_index);
            else if (DI3bl2_combo.SelectedIndex == 0) SelectComboBox_DI(DI3bl2_combo, code, DI3bl2_lab, DI3bl2combo_text, DI3bl2combo_index);
            else if (DI4bl2_combo.SelectedIndex == 0) SelectComboBox_DI(DI4bl2_combo, code, DI4bl2_lab, DI4bl2combo_text, DI4bl2combo_index);
            else if (DI5bl2_combo.SelectedIndex == 0) SelectComboBox_DI(DI5bl2_combo, code, DI5bl2_lab, DI5bl2combo_text, DI5bl2combo_index);
            else if (AI4bl2_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI4bl2_combo, AI4bl2_typeCombo, code, AI4bl2_lab, AI4bl2combo_text, AI4bl2combo_index);
            else if (AI5bl2_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI4bl2_combo, AI4bl2_typeCombo, code, AI4bl2_lab, AI4bl2combo_text, AI4bl2combo_index);
            else if (AI6bl2_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI4bl2_combo, AI4bl2_typeCombo, code, AI4bl2_lab, AI4bl2combo_text, AI4bl2combo_index);
            else if (AI1bl2_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI4bl2_combo, AI4bl2_typeCombo, code, AI4bl2_lab, AI4bl2combo_text, AI4bl2combo_index);
            else if (AI2bl2_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI4bl2_combo, AI4bl2_typeCombo, code, AI4bl2_lab, AI4bl2combo_text, AI4bl2combo_index);
            else if (AI3bl2_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI4bl2_combo, AI4bl2_typeCombo, code, AI4bl2_lab, AI4bl2combo_text, AI4bl2combo_index);
            // Блок расширения 3
            else if (DI1bl3_combo.SelectedIndex == 0) SelectComboBox_DI(DI1bl3_combo, code, DI1bl3_lab, DI1bl3combo_text, DI1bl3combo_index);
            else if (DI2bl3_combo.SelectedIndex == 0) SelectComboBox_DI(DI2bl3_combo, code, DI2bl3_lab, DI2bl3combo_text, DI2bl3combo_index);
            else if (DI3bl3_combo.SelectedIndex == 0) SelectComboBox_DI(DI3bl3_combo, code, DI3bl3_lab, DI3bl3combo_text, DI3bl3combo_index);
            else if (DI4bl3_combo.SelectedIndex == 0) SelectComboBox_DI(DI4bl3_combo, code, DI4bl3_lab, DI4bl3combo_text, DI4bl3combo_index);
            else if (DI5bl3_combo.SelectedIndex == 0) SelectComboBox_DI(DI5bl3_combo, code, DI5bl3_lab, DI5bl3combo_text, DI5bl3combo_index);
            else if (AI4bl3_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI4bl3_combo, AI4bl3_typeCombo, code, AI4bl3_lab, AI4bl3combo_text, AI4bl3combo_index);
            else if (AI5bl3_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI5bl3_combo, AI5bl3_typeCombo, code, AI5bl3_lab, AI5bl3combo_text, AI5bl3combo_index);
            else if (AI6bl3_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI6bl3_combo, AI6bl3_typeCombo, code, AI6bl3_lab, AI6bl3combo_text, AI6bl3combo_index);
            else if (AI1bl3_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI1bl3_combo, AI1bl3_typeCombo, code, AI1bl3_lab, AI1bl3combo_text, AI1bl3combo_index);
            else if (AI2bl3_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI2bl3_combo, AI2bl3_typeCombo, code, AI2bl3_lab, AI2bl3combo_text, AI2bl3combo_index);
            else if (AI3bl3_combo.SelectedIndex == 0) SelectComboBoxDI_to_AI(AI3bl3_combo, AI3bl3_typeCombo, code, AI3bl3_lab, AI3bl3combo_text, AI3bl3combo_index);
        }

        ///<summary>Удаление DI из определённого comboBox, дискретный/аналоговый вход</summary>
        private void RemoveDI_FromComboBox(ComboBox comboBox, string name, Label label, string text, int index, bool di_type)
        {
            Di find_di;                                                                                 // Дискретный вход для поиска
            Ai find_ai;                                                                                 // Аналоговый вход для поиска
            for (int i = 0; i < comboBox.Items.Count; i++)
                if (comboBox.Items[i].ToString() == name)                                               // Есть совпадение по имени в списке
                {
                    comboBox.Items.Remove(name);                                                        // Удаление элемента по имени
                    if (comboBox.Items.Count > 1)                                                       // Осталось больше одного элемента в списке
                    {
                        comboBox.SelectedIndex = comboBox.Items.Count - 1;                              // Выбор последнего элемента
                        if (comboBox.SelectedItem.ToString() == NOT_SELECTED)
                        {
                            SubFromCombosDI(comboBox.SelectedItem.ToString(), comboBox);                // Удаление из других comboBox выбранного элемента
                            if (di_type)                                                                // Дискретный тип comboBox
                            {
                                find_di = list_di.Find(x => x.Name == comboBox.SelectedItem.ToString());
                                if (find_di != null)
                                {
                                    list_di.Remove(find_di);
                                    if (showCode) label.Text = find_di.Code.ToString();
                                }
                            }
                            else                                                                        // Аналоговый тип comboBox
                            {
                                find_ai = list_ai.Find(x => x.Name == comboBox.SelectedItem.ToString());
                                if (find_ai != null)
                                {
                                    list_ai.Remove(find_ai);
                                    if (showCode) label.Text = find_ai.Code.ToString();
                                }
                            }     
                        }
                    }
                    else                                                                                // Только "Не выбрано"
                    {
                        comboBox.SelectedItem = NOT_SELECTED;
                        label.Text = "";
                    }
                    text = comboBox.SelectedItem.ToString();                                            // Сохранение наименование выбранного DI
                    index = comboBox.SelectedIndex;                                                     // Сохранение индекса выбранного DI
                }
        }

        ///<summary>Удаление DI из всех comboBox</summary>
        private void SubFromCombosDI(ushort code)
        {
            string name = "";                                                                  // Текстовое название дискретного входа
            Di findDi = list_di.Find(x => x.Code == code);
            if (findDi != null) name = findDi.Name;
            else return;

            subDIcondition = true; subAIcondition = true;                                      // Признак удаления DI и AI, не работает событие indexChanged

            // ПЛК
            RemoveDI_FromComboBox(DI1_combo, name, DI1_lab, DI1combo_text, DI1combo_index, true);                         // DI1
            RemoveDI_FromComboBox(DI2_combo, name, DI2_lab, DI2combo_text, DI2combo_index, true);                         // DI2
            RemoveDI_FromComboBox(DI3_combo, name, DI3_lab, DI3combo_text, DI3combo_index, true);                         // DI3
            RemoveDI_FromComboBox(DI4_combo, name, DI4_lab, DI4combo_text, DI4combo_index, true);                         // DI4
            RemoveDI_FromComboBox(DI5_combo, name, DI5_lab, DI5combo_text, DI5combo_index, true);                         // DI5
            // Блок расширения 1
            RemoveDI_FromComboBox(DI1bl1_combo, name, DI1bl1_lab, DI1bl1combo_text, DI1bl1combo_index, true);             // DI1
            RemoveDI_FromComboBox(DI2bl1_combo, name, DI2bl1_lab, DI2bl1combo_text, DI2bl1combo_index, true);             // DI2
            RemoveDI_FromComboBox(DI3bl1_combo, name, DI3bl1_lab, DI3bl1combo_text, DI3bl1combo_index, true);             // DI3
            RemoveDI_FromComboBox(DI4bl1_combo, name, DI4bl1_lab, DI4bl1combo_text, DI4bl1combo_index, true);             // DI4
            RemoveDI_FromComboBox(DI5bl1_combo, name, DI5bl1_lab, DI5bl1combo_text, DI5bl1combo_index, true);             // DI5
            // Блок расширения 2
            RemoveDI_FromComboBox(DI1bl2_combo, name, DI1bl2_lab, DI1bl2combo_text, DI1bl2combo_index, true);             // DI1
            RemoveDI_FromComboBox(DI2bl2_combo, name, DI2bl2_lab, DI2bl2combo_text, DI2bl2combo_index, true);             // DI2
            RemoveDI_FromComboBox(DI3bl2_combo, name, DI3bl2_lab, DI3bl2combo_text, DI3bl2combo_index, true);             // DI3
            RemoveDI_FromComboBox(DI4bl2_combo, name, DI4bl2_lab, DI4bl2combo_text, DI4bl2combo_index, true);             // DI4
            RemoveDI_FromComboBox(DI5bl2_combo, name, DI5bl2_lab, DI5bl2combo_text, DI5bl2combo_index, true);             // DI5
            // Блок расширения 3
            RemoveDI_FromComboBox(DI1bl3_combo, name, DI1bl3_lab, DI1bl3combo_text, DI1bl3combo_index, true);             // DI1
            RemoveDI_FromComboBox(DI2bl3_combo, name, DI2bl3_lab, DI2bl3combo_text, DI2bl3combo_index, true);             // DI2
            RemoveDI_FromComboBox(DI3bl3_combo, name, DI3bl3_lab, DI3bl3combo_text, DI3bl3combo_index, true);             // DI3
            RemoveDI_FromComboBox(DI4bl3_combo, name, DI4bl3_lab, DI4bl3combo_text, DI4bl3combo_index, true);             // DI4
            RemoveDI_FromComboBox(DI5bl3_combo, name, DI5bl3_lab, DI5bl3combo_text, DI5bl3combo_index, true);             // DI5
            // ПЛК. аналоговые входы
            RemoveDI_FromComboBox(AI1_combo, name, AI1_lab, AI1combo_text, AI1combo_index, false);                        // AI1  
            RemoveDI_FromComboBox(AI2_combo, name, AI2_lab, AI2combo_text, AI2combo_index, false);                        // AI2
            RemoveDI_FromComboBox(AI3_combo, name, AI3_lab, AI3combo_text, AI3combo_index, false);                        // AI3
            RemoveDI_FromComboBox(AI4_combo, name, AI4_lab, AI4combo_text, AI4combo_index, false);                        // AI4
            RemoveDI_FromComboBox(AI5_combo, name, AI5_lab, AI5combo_text, AI5combo_index, false);                        // AI5
            RemoveDI_FromComboBox(AI6_combo, name, AI6_lab, AI6combo_text, AI6combo_index, false);                        // AI6
            // Блок расширирения 1, аналоговые входы
            RemoveDI_FromComboBox(AI1bl1_combo, name, AI1bl1_lab, AI1bl1combo_text, AI1bl1combo_index, false);            // AI1
            RemoveDI_FromComboBox(AI2bl1_combo, name, AI2bl1_lab, AI2bl1combo_text, AI2bl1combo_index, false);            // AI2
            RemoveDI_FromComboBox(AI3bl1_combo, name, AI3bl1_lab, AI3bl1combo_text, AI3bl1combo_index, false);            // AI3
            RemoveDI_FromComboBox(AI4bl1_combo, name, AI4bl1_lab, AI4bl1combo_text, AI4bl1combo_index, false);            // AI4
            RemoveDI_FromComboBox(AI5bl1_combo, name, AI5bl1_lab, AI5bl1combo_text, AI5bl1combo_index, false);            // AI5
            RemoveDI_FromComboBox(AI6bl1_combo, name, AI6bl1_lab, AI6bl1combo_text, AI6bl1combo_index, false);            // AI6
            // Блок расширирения 2, аналоговые входы
            RemoveDI_FromComboBox(AI1bl2_combo, name, AI1bl2_lab, AI1bl2combo_text, AI1bl2combo_index, false);            // AI1
            RemoveDI_FromComboBox(AI2bl2_combo, name, AI2bl2_lab, AI2bl2combo_text, AI2bl2combo_index, false);            // AI2
            RemoveDI_FromComboBox(AI3bl2_combo, name, AI3bl2_lab, AI3bl2combo_text, AI3bl2combo_index, false);            // AI3
            RemoveDI_FromComboBox(AI4bl2_combo, name, AI4bl2_lab, AI4bl2combo_text, AI4bl2combo_index, false);            // AI4
            RemoveDI_FromComboBox(AI5bl2_combo, name, AI5bl2_lab, AI5bl2combo_text, AI5bl2combo_index, false);            // AI5
            RemoveDI_FromComboBox(AI6bl2_combo, name, AI6bl2_lab, AI6bl2combo_text, AI6bl2combo_index, false);            // AI6
            // Блок расширирения 3, аналоговые входы
            RemoveDI_FromComboBox(AI1bl3_combo, name, AI1bl3_lab, AI1bl3combo_text, AI1bl3combo_index, false);            // AI1
            RemoveDI_FromComboBox(AI2bl3_combo, name, AI2bl3_lab, AI2bl3combo_text, AI2bl3combo_index, false);            // AI2
            RemoveDI_FromComboBox(AI3bl3_combo, name, AI3bl3_lab, AI3bl3combo_text, AI3bl3combo_index, false);            // AI3
            RemoveDI_FromComboBox(AI4bl3_combo, name, AI4bl3_lab, AI4bl3combo_text, AI4bl3combo_index, false);            // AI4
            RemoveDI_FromComboBox(AI5bl3_combo, name, AI5bl3_lab, AI5bl3combo_text, AI5bl3combo_index, false);            // AI5
            RemoveDI_FromComboBox(AI6bl3_combo, name, AI6bl3_lab, AI6bl3combo_text, AI6bl3combo_index, false);            // AI6

            subDIcondition = false; subAIcondition = false;                     // Сброс признака удаления DI и AI 
            list_di.Remove(findDi);                                             // Удаление сигнала из списка DI
            CheckSignalsReady();                                                // Проверка распределения сигналов
        }

        ///<summary>Метод для добавления DI к списку сигналов</summary>
        private void AddToListDI(string name, ushort code)
        {
            list_di.Add(new Di(name, code));                                    // Добавление к свободному comboBox входа
            AddNewDI(code);
        }

        ///<summary>Проверка и добавление дискретного входа</summary>
        private void CheckAddDIToList(string name, ushort code) 
        {
            Di find_di = list_di.Find(x => x.Code == code);
            if (find_di == null)                                                // Нет такой записи
                AddToListDI(name, code);
        }

        ///<summary>Выбрали блок заслонки</summary>
        private void DampCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 6, code_2 = 39;                                                 // Подтверждение открытия приточной/вытяжной заслонки

            if (dampCheck.Checked && confPrDampCheck.Checked)                               // Приточная заслонка и подтверждение открытия
                CheckAddDIToList("Подтверждение открытия приточной заслонки", code_1);
            if (dampCheck.Checked && outDampCheck.Checked && confOutDampCheck.Checked)      // Вытяжная заслонка и подтверждение открытия
                CheckAddDIToList("Подтверждение открытия вытяжной заслонки", code_2);
            else                                                                            // Отмена выбора блока заслонки
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
        }

        ///<summary>Выбрали подтверждение открытия приточной заслонки</summary>
        private void ConfPrDampCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 6;                                                              // Подтверждение открытия приточной заслонки

            if (dampCheck.Checked && confPrDampCheck.Checked)                               // Выбрана приточная заслонка и подтвреждение открытия
                CheckAddDIToList("Подтверждение открытия приточной заслонки", code_1);
            else                                                                            // Отмена выбора сигнала
                SubFromCombosDI(code_1);
        }

        ///<summary>Выбрали вытяжную заслонку</summary>
        private void OutDampCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 39;                                                                         // Подтверждение открытия вытяжной заслонки

            if (comboSysType.SelectedIndex == 1 && dampCheck.Checked && outDampCheck.Checked)           // ПВ-система, выбрана вытяжная заслонка
            { 
                if (confOutDampCheck.Checked)                                                           // Подтверждение открытия
                    CheckAddDIToList("Подтверждение открытия вытяжной заслонки", code_1);
            }
            else                                                                                        // Отмена выбора заслонки
                SubFromCombosDI(code_1);
        }

        ///<summary>Выбрали подтверждение открытия вытяжной заслонки</summary>
        private void ConfOutDampCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 39;                                                                         // Подтверждение открытия вытяжной заслонки

            if (comboSysType.SelectedIndex == 1 && dampCheck.Checked && outDampCheck.Checked)           // ПВ-система, выбрана вытяжная заслонка
            { 
                if (confOutDampCheck.Checked)                                                           // Подтверждение открытия
                    CheckAddDIToList("Подтверждение открытия вытяжной заслонки", code_1);
                else                                                                                    // Отмена выбора подтверждения открытия
                    SubFromCombosDI(code_1);
            }
        }

        ///<summary>Выбрали PS приточного вентилятора</summary>
        private void PrFanPSCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 11, code_2 = 22;                                                            // PS приточного вентилятора 1 и 2

            if (prFanPSCheck.Checked)                                                                   // Выбрали PS
            {
                CheckAddDIToList("PS приточного вентилятора 1", code_1);
                if (checkResPrFan.Checked)                                                              // Если выбран резерв
                    CheckAddDIToList("PS приточного вентилятора 2", code_2);
            }
            else                                                                                          // Отмена выбора PS
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
        }

        ///<summary>Выбрали термоконтакты приточного вентилятора</summary>
        private void PrFanThermoCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 13, code_2 = 24;                                                            // Термоконтакты приточного вентилятора 1 и 2

            if (prFanThermoCheck.Checked)                                                               // Выбрали термоконтакты
            {
                CheckAddDIToList("Термоконтакты приточного вентилятора 1", code_1);
                if (checkResPrFan.Checked)                                                              // Если выбран резерв
                    CheckAddDIToList("Термоконтакты приточного вентилятора 2", code_2);
            }
            else                                                                                        // Отмена выбора термоконтактов
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
        }

        ///<summary>Выбрали ПЧ приточного вентилятора</summary>
        private void PrFanFC_check_signalsDICheckedChanged(object sender, EventArgs e)
        {
            if (prFanFC_check.Checked)                                                                  // Выбран ПЧ
            { 
                if (prFanControlCombo.SelectedIndex == 0)                                               // Внешние контакты
                {
                    prFanAlarmCheck.Enabled = true;                                                     // Разблокировка выбора аварии
                    if (!prFanAlarmCheck.Checked)                                                       // Выбор сигнала аварии
                        prFanAlarmCheck.Checked = true; 
                }
                else if (prFanControlCombo.SelectedIndex == 1)                                          // Управление по Modbus
                {
                    if (prFanAlarmCheck.Checked)                                                        // Снятие выбора опций
                        prFanAlarmCheck.Checked = false; 
                    // Блокировка опций
                    prFanAlarmCheck.Enabled = false;
                }
            } 
            else // Отмена выбора ПЧ
            {
                prFanAlarmCheck.Enabled = true;                                                         // Разблокировка выбора аварии
                if (prFanAlarmCheck.Checked) 
                    prFanAlarmCheck.Checked = false;                                                    // Снятие выбора сигнала аварии
            }
        }

        ///<summary>Изменили тип управления ПЧ приточного вентилятора</summary>
        private void PrFanControlCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            if (prFanFC_check.Checked)                                                                  // Когда выбран ПЧ
            {
                if (prFanControlCombo.SelectedIndex == 0)                                               // Внешние контакты
                {
                    prFanAlarmCheck.Enabled = true;                                                     // Разблокировка выбора аварии
                    if (!prFanAlarmCheck.Checked)                                                       // Выбор сигнала аварии
                        prFanAlarmCheck.Checked = true; 
                }
                else if (prFanControlCombo.SelectedIndex == 1)                                          // Modbus
                {
                    if (prFanAlarmCheck.Checked)                                                        // Снятие выбора опций
                        prFanAlarmCheck.Checked = false;
                    // Блокировка опций
                    prFanAlarmCheck.Enabled = false;
                }
            }
        }

        ///<summary>Выбрали защиту по току приточного вентилятора</summary>
        private void CurDefPrFanCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 14, code_2 = 25;                                                             // Защита по току приточного вентилятора 1 и 2

            if (curDefPrFanCheck.Checked) // Выбрана защита по току
            {
                CheckAddDIToList("Защита по току приточного вентилятора 1", code_1);
                if (checkResPrFan.Checked) // Если выбран резерв
                    CheckAddDIToList("Защита по току приточного вентилятора 2", code_2);
            } 
            else // Отмена выбора защиты по току
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
        }

        ///<summary>Выбрали резерв приточного вентилятора</summary>
        private void CheckResPrFan_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 22;                                 // PS приточного вентилятора 2
            ushort code_2 = 24;                                 // Термоконтакты приточного вентилятора 2
            ushort code_3 = 23;                                 // Сигнал аварии приточного вентилятора 2
            ushort code_4 = 25;                                 // Защита по току приточного вентилятора 2

            if (checkResPrFan.Checked)                                                              // Выбран резерв приточного
            {
                if (prFanPSCheck.Checked)                                                           // Выбран сигнал PS
                    AddToListDI("PS приточного вентилятора 2", code_1);
                if (prFanThermoCheck.Checked)                                                       // Выбраны термоконтакты приточного
                    AddToListDI("Термоконтакты приточного вентилятора 2", code_2);
                if (prFanAlarmCheck.Checked)                                                        // Выбран сигнал аварии
                    AddToListDI("Сигнал аварии приточного вентилятора 2", code_3);
                if (curDefPrFanCheck.Checked)                                                       // Выбрана защита по току
                    AddToListDI("Защита по току приточного вентилятора 2", code_4);
            }
            else                                                                                    // Отмена выбора резерва приточного
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2); SubFromCombosDI(code_3); SubFromCombosDI(code_4);
            }
        }

        ///<summary>Выбрали подтверждение открытия заслонки приточного вентилятора</summary>
        private void PrDampConfirmFanCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 17;                                                                 // Сигнал подтверждения открытия заслонки приточного вентилятора

            if (prDampFanCheck.Checked && prDampConfirmFanCheck.Checked)                        // Выбрали подтверждение открытия
            {
                AddToListDI("Подтверждение для заслонки приточного вентилятора", code_1);
            }
            else                                                                                // Отмена выбора подтверждения открытия
            {
                SubFromCombosDI(code_1);
            }
        }

        ///<summary>Выбрали PS вытяжного вентилятора</summary>
        private void OutFanPSCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 44, code_2 = 55;                                                        // Сигнал PS вытяжного вентилятора 1 и 2

            if (comboSysType.SelectedIndex == 1 && outFanPSCheck.Checked)                           // Выбрали PS вытяжного вентилятора
            {
                CheckAddDIToList("PS вытяжного вентилятора 1", code_1);
                if (checkResOutFan.Checked)                                                         // Если выбран резерв
                    CheckAddDIToList("PS вытяжного вентилятора 2", code_2);
            }
            else                                                                                    // Отмена выбора PS
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
        }

        ///<summary>Выбрали термоконтакты вытяжного вентилятора</summary>
        private void OutFanThermoCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 46, code_2 = 57;                                                        // Термоконтакты вытяжного вентилятора 1 и 2
            
            if (comboSysType.SelectedIndex == 1 && outFanThermoCheck.Checked)
            {
                CheckAddDIToList("Термоконтакты вытяжного вентилятора 1", code_1);
                if (checkResOutFan.Checked)                                                         // Если выбран резерв
                    CheckAddDIToList("Термоконтакты вытяжного вентилятора 2", code_2);
            }
            else                                                                                    // Отмена выбора термоконтактов
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
        }

        ///<summary>Выбрали ПЧ вытяжного вентилятора</summary>
        private void OutFanFC_check_signalsDICheckedChanged(object sender, EventArgs e)
        {
            if (comboSysType.SelectedIndex == 1 && outFanFC_check.Checked)                          // Выбран ПЧ 
            { 
                if (outFanControlCombo.SelectedIndex == 0)                                          // Внешние контакты
                {
                    outFanAlarmCheck.Enabled = true;                                                // Разблокировка выбора аварии
                    if (!outFanAlarmCheck.Checked) outFanAlarmCheck.Checked = true;                 // Выбор сигнала аварии
                }
                else if (outFanControlCombo.SelectedIndex == 1)                                     // Управление Modbus
                {
                    // Снятие выбора опций
                    if (outFanAlarmCheck.Checked) outFanAlarmCheck.Checked = false;
                    // Блокировка опций
                    outFanAlarmCheck.Enabled = false;
                }
            } 
            else                                                                                    // Отмена выбора ПЧ
            {
                outFanAlarmCheck.Enabled = true;                                                    // Разблокировка выбора аварии
                if (outFanAlarmCheck.Checked)                                                       // Снятие выбора сигнала аварии
                    outFanAlarmCheck.Checked = false; 
            }
        }

        ///<summary>Изменили тип управления ПЧ вытяжного вентилятора</summary>
        private void OutFanControlCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSysType.SelectedIndex == 1 && outFanFC_check.Checked)                          // Когда выбран ПЧ
            {
                if (outFanControlCombo.SelectedIndex == 0)                                          // Внешние контакты
                {
                    outFanAlarmCheck.Enabled = true;                                                // Разблокировка выбора аварии
                    if (!outFanAlarmCheck.Checked)                                                  // Выбор сигнала аварии
                        outFanAlarmCheck.Checked = true; 
                }
                else if (outFanControlCombo.SelectedIndex == 1)                                     // Modbus
                {
                    // Снятие выбора опций
                    if (outFanAlarmCheck.Checked) 
                        outFanAlarmCheck.Checked = false;
                    // Блокировка опций
                    outFanAlarmCheck.Enabled = false;
                }
            }
        }

        ///<summary>Выбрали защиту по току вытяжного вентилятора</summary>
        private void CurDefOutFanCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 47, code_2 = 58;                                                        // Защита по току вытяжного вентилятора 1 и 2

            if (comboSysType.SelectedIndex == 1 && curDefOutFanCheck.Checked)
            {
                CheckAddDIToList("Защита по току вытяжного вентилятора 1", code_1);
                if (checkResOutFan.Checked)                                                         // Если выбран резерв
                    CheckAddDIToList("Защита по току вытяжного вентилятора 2", code_2);
            }
            else                                                                                    // Отмена выбора защиты по току
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
        }

        ///<summary>Выбрали резерв вытяжного вентилятора</summary>
        private void CheckResOutFan_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 55;                     // PS вытяжного вентилятра 2
            ushort code_2 = 57;                     // Термоконтакты вытяжного вентилятора 2
            ushort code_3 = 56;                     // Сигнал аварии вытяжного вентилятора 2
            ushort code_4 = 58;                     // Защита по току вытяжного вентилятора 2

            if (comboSysType.SelectedIndex == 1 && checkResOutFan.Checked)                          // Выбран резерв вытяжного
            {
                if (outFanPSCheck.Checked)                                                          // Выбран сигнал PS
                    AddToListDI("PS вытяжного вентилятора 2", code_1);
                if (outFanThermoCheck.Checked)                                                      // Выбраны термоконтакты
                    AddToListDI("Термоконтакты вытяжного вентилятора 2", code_2);
                if (outFanAlarmCheck.Checked)                                                       // Выбрали сигнал аварии
                    AddToListDI("Сигнал аварии вытяжного вентилятора 2", code_3);
                if (curDefOutFanCheck.Checked)                                                      // Защита по току
                    AddToListDI("Защита по току вытяжного вентилятора 2", code_4);
            }
            else // Отмена выбора резерва вытяжного
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2); SubFromCombosDI(code_3); SubFromCombosDI(code_4);
            }
        }

        ///<summary>Выбрали подтверждение открытия заслонки вытяжного вентилятора</summary>
        private void OutDampConfirmFanCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code = 50;                                                                       // Сигнал подтверждения открытия заслонки вытяжного вентилятора

            if (outDampFanCheck.Checked && outDampConfirmFanCheck.Checked)                          // Выбрали подтверждение открытия
            {
                AddToListDI("Подтверждение для заслонки вытяжного вентилятора", code);
            }
            else                                                                                    // Отмена выбора подтверждения открытия
            {
                SubFromCombosDI(code);
            }
        }

        ///<summary>Выбрали нагреватель</summary>
        private void HeaterCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 72, code_2 = 73;                                                        // Воздушный термостат и подтверждение работы насоса

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)                            // Выбран водяной нагреватель
            { 
                if (TF_heaterCheck.Checked)                                                         // Выбран термостат
                    CheckAddDIToList("Термостат водяного калорифера", code_1);
                if (confHeatPumpCheck.Checked)                                                      // Подтверждение работы насоса
                    CheckAddDIToList("Подтверждение работы насоса водяного калорифера", code_2);
            }
            else                                                                                    // Отмена выбора нагревателя
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
            ThermSwitchCombo_signalsDISelectedIndexChanged(this, e);                                // Проверка термовыключателей
        }

        ///<summary>Изменили тип основного нагревателя</summary>
        private void HeatTypeCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 72, code_2 = 73;                                                        // Воздушный термостат и подтверждение работы насоса

            if (heaterCheck.Checked)                                                                // Выбран нагреватель
            { 
                if (heatTypeCombo.SelectedIndex == 0)                                               // Водяной калорифер
                {
                    if (TF_heaterCheck.Checked)                                                     // Воздушный термостат
                        CheckAddDIToList("Термостат водяного калорифера", code_1);
                    if (confHeatPumpCheck.Checked)                                                  // Подтверждение работы насоса
                        CheckAddDIToList("Подтверждение работы насоса водяного калорифера", code_2);
                }
                else                                                                                // Электрокалорифер
                {
                    SubFromCombosDI(code_1); SubFromCombosDI(code_2);
                }
                ThermSwitchCombo_signalsDISelectedIndexChanged(this, e); // Проверка термовыключателей
            }
        }

        ///<summary>Выбрали термостат защиты от замерзания</summary>
        private void TF_heaterCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 72;                                                                     // Воздушный термостат

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)                            // Выбран водяной калорифер
            { 
                if (TF_heaterCheck.Checked) // Выбрали воздушный термостат
                    CheckAddDIToList("Термостат водяного калорифера", code_1);
                else // Отмена выбора
                    SubFromCombosDI(code_1);
            }
        }

        ///<summary>Подтверждение работы основного насоса водяного калорифера</summary>
        private void ConfHeatPumpCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 73;                                                                     // Подтверждение работы основного насоса калорифера

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)                            // Выбран водяной калорифер
            { 
                if (confHeatPumpCheck.Checked)                                                      // Выбрали подтверждение работы насоса
                    CheckAddDIToList("Подтверждение работы основного насоса калорифера", code_1);
                else                                                                                // Отмена выбора
                    SubFromCombosDI(code_1);
            }
        }

        ///<summary>Подтверждение работы резервного насоса водяного калорифера</summary>
        private void ConfHeatResPumpCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 74;                                                                         // Подтверждение работы резервного насоса калорифера

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)                                // Выбран водяной калорифер
            {
                if (reservPumpHeater.Checked && confHeatResPumpCheck.Checked)                           // Есть насос и подтверждение работы насоса
                    CheckAddDIToList("Подтверждение работы резервного насоса калорифера", code_1);
                else
                    SubFromCombosDI(code_1);
            }
        }

        ///<summary>Защита по току основного насоса водяного калорифера</summary>
        private void PumpCurProtect_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 75;                                                                     // Защита по току основного насоса

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)                            // Выбран водяной калорифер
            {
                if (pumpCurProtect.Checked)                                                         // Выбрали защиту по току
                    CheckAddDIToList("Защита по току основного насоса калорифера", code_1);
                else
                    SubFromCombosDI(code_1);                                                        // Отмена выбора
            }
        }

        ///<summary>Защита по току резервного насоса водяного калорифера</summary>
        private void PumpCurResProtect_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 76;                                                                     // Защита по току основного насоса

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)                            // Выбран водяной калорифер
            {
                if (reservPumpHeater.Checked && pumpCurResProtect.Checked)                          // Выбран резервный насос и защита по току
                    CheckAddDIToList("Защита по току резервного насоса калорифера", code_1);
                else
                    SubFromCombosDI(code_1);                                                        // Отмена выбора
            }
        }

        ///<summary>Изменили количество термовыключателей основного нагревателя</summary>
        private void ThermSwitchCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 79, code_2 = 78;                                                        // Термовыключатель 1 - пожар, 2 - перегрев

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 1)                            // Выбран электрокалорифер
            { 
                if (thermSwitchCombo.SelectedIndex == 0)                                            // Нет термовыключателей
                {
                    SubFromCombosDI(code_1); SubFromCombosDI(code_2);
                }
                else if (thermSwitchCombo.SelectedIndex == 1)                                       // Один термовыключатель
                {
                    SubFromCombosDI(code_2);
                    CheckAddDIToList("Термовыключатель пожара ТЭНов калорифера", code_1);
                }
                else if (thermSwitchCombo.SelectedIndex == 2)                                       // Два термовыключателя
                {
                    CheckAddDIToList("Термовыключатель пожара ТЭНов калорифера", code_1);
                    CheckAddDIToList("Термовыключатель перегрева ТЭНов калорифера", code_2);
                }
            }
            // Выбран водяной калорифер, либо нет нагревателя
            if ((heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0) || !heaterCheck.Checked)
            { 
                SubFromCombosDI(code_1); SubFromCombosDI(code_2); // Удаление сигналов
            }
        }

        ///<summary>Выбрали догреватель</summary>
        private void AddHeatCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 133, code_2 = 134;                                                      // Воздушный термостат и подтверждение работы насоса

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)                        // Выбран водяной догреватель
            { 
                if (TF_addHeaterCheck.Checked)                                                      // Выбран термостат
                    CheckAddDIToList("Термостат водяного догревателя", code_1);
                if (confAddHeatPumpCheck.Checked)                                                   // Подтверждение работы насоса
                    CheckAddDIToList("Подтверждение работы насоса водяного догревателя", code_2);
            } 
            else                                                                                    // Отмена выбора догревателя
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
            ThermAddSwitchCombo_signalsDISelectedIndexChanged(this, e);                             // Проверка для термовыключателей
        }

        ///<summary>Изменили тип догревателя</summary>
        private void HeatAddTypeCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 133, code_2 = 134;                                                      // Воздушный термостат и подтверждение работы насоса

            if (addHeatCheck.Checked)                                                               // Выбран догреватель
            {
                if (heatAddTypeCombo.SelectedIndex == 0)                                            // Водяной догреватель
                {
                    if (TF_addHeaterCheck.Checked)                                                  // Воздушный термостат
                        CheckAddDIToList("Термостат водяного догревателя", code_1);
                    if (confAddHeatPumpCheck.Checked)                                               // Подтверждение работы насоса
                        CheckAddDIToList("Подтверждение работы насоса водяного догревателя", code_2);
                }
                else // Электродогреватель
                {
                    SubFromCombosDI(code_1); SubFromCombosDI(code_2);                               // Удаление сигналов
                }
                ThermAddSwitchCombo_signalsDISelectedIndexChanged(this, e);                         // Проверка для термовыключателей
            }
        }

        ///<summary>Выбрали термостат догревателя</summary>
        private void TF_addHeaterCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 133;                                                                    // Воздушный термостат

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)                        // Выбран водяной догреватель
            { 
                if (TF_addHeaterCheck.Checked)                                                      // Выбрали воздушный термостат
                    CheckAddDIToList("Термостат водяного догревателя", code_1);
                else                                                                                // Отмена выбора
                    SubFromCombosDI(code_1);
            }
        }

        ///<summary>Выбрали защиту по току основного насоса догревателя</summary>
        private void PumpCurAddProtect_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 136;                                                                    // Защита по току основного насоса догревателя

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)                        // Выбран водяной догреватель
            {
                if (pumpAddHeatCheck.Checked && pumpCurAddProtect.Checked)                          // Выбран насос и защита по току
                    CheckAddDIToList("Защита по току основного насоса догревателя", code_1);
                else                                                                                // Отмена выбора
                    SubFromCombosDI(code_1);
            }    
        }

        ///<summary>Выбрали защиту по току резервного насоса догревателя</summary>
        private void PumpCurResAddProtect_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 137;                                                                    // Защита по току основного насоса догревателя

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)                        // Выбран водяной догреватель
            {
                if (reservPumpAddHeater.Checked && pumpCurResAddProtect.Checked)                    // Выбран резервный насос и защита по току
                    CheckAddDIToList("Защита по току резервного насоса догревателя", code_1);
                else                                                                                // Отмена выбора
                    SubFromCombosDI(code_1);
            }
        }

        ///<summary>Подтверждение работы основного насоса водяного догревателя</summary>
        private void ConfAddHeatPumpCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 134;                                                                    // Подтверждение работы основного насоса догревателя

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)                        // Выбран водяной догреватель
            { 
                if (pumpAddHeatCheck.Checked && confAddHeatPumpCheck.Checked)                       // Есть насос и подтверждение работы насоса
                    CheckAddDIToList("Подтверждение работы основного насоса догревателя", code_1);
                else                                                                                // Отмена выбора
                    SubFromCombosDI(code_1);
            }
        }

        ///<summary>Подтверждение работы резервного насоса водяного догревателя</summary>
        private void ConfAddHeatResPumpCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 135;                                                                                // Подтверждение работы резервного насоса догревателя

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)                                    // Выбран водяной догреватель
            {
                if (reservPumpAddHeater.Checked && confAddHeatResPumpCheck.Checked)                             // Есть резервный насос и подтверждение работы
                    CheckAddDIToList("Подтверждение работы резервного насоса догревателя", code_1);
                else                                                                                            // Отмена выбора
                    SubFromCombosDI(code_1);
            }
        }

        ///<summary>Изменили количество термовыключателей догревателя</summary>
        private void ThermAddSwitchCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 129, code_2 = 128;                                                      // Термовыключатель 1 - пожар, 2 - перегрев

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 1)                        // Выбран электрический догреватель
            { 
                if (thermAddSwitchCombo.SelectedIndex == 0)                                         // Нет термовыключателей
                {
                    SubFromCombosDI(code_1); SubFromCombosDI(code_2);
                }
                else if (thermAddSwitchCombo.SelectedIndex == 1)                                    // Один термовыключатель
                {
                    SubFromCombosDI(code_2);
                    CheckAddDIToList("Термовыключатель пожара ТЭНов догревателя", code_1);
                }
                else if (thermAddSwitchCombo.SelectedIndex == 2)                                    // Два термовыключателя
                {
                    CheckAddDIToList("Термовыключатель пожара ТЭНов догревателя", code_1);
                    CheckAddDIToList("Термовыключатель перегрева ТЭНов догревателя", code_2);
                }
            }
            // Выбран водяной догреватель, либо нет догревателя
            if ((addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0) || !addHeatCheck.Checked)
            { 
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);                                   // Удаление сигналов
            } 
        }

        ///<summary>Выбрали фильтр</summary>
        private void FilterCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 33, code_2 = 34, code_3 = 35;                                           // Фильтр 1, 2, 3 приточные
            ushort code_4 = 66, code_5 = 67, code_6 = 68;                                           // Фильтр 1, 2, 3 вытяжные

            if (filterCheck.Checked)                                                                // Выбрали фильтры
            {
                CheckAddDIToList("Приточный фильтр 1", code_1);
                if (filterPrCombo.SelectedIndex > 0)                                                // Два приточных фильтра
                    CheckAddDIToList("Приточный фильтр 2", code_2);
                if (filterPrCombo.SelectedIndex > 1)                                                // Три приточных фильтра
                    CheckAddDIToList("Приточный фильтр 3", code_3);
                if (comboSysType.SelectedIndex == 1)                                                // Выбрана ПВ-система
                {
                    if (filterOutCombo.SelectedIndex > 0)                                           // Один вытяжной фильтр
                        CheckAddDIToList("Вытяжной фильтр 1", code_4);
                    if (filterOutCombo.SelectedIndex > 1)                                           // Два вытяжных фильтра
                        CheckAddDIToList("Вытяжной фильтр 2", code_5);
                    if (filterOutCombo.SelectedIndex > 2)                                           // Три вытяжных фильтра
                        CheckAddDIToList("Вытяжной фильтр 3", code_6);
                }
            }
            else                                                                                    // Отмена выбора фильтров
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2); SubFromCombosDI(code_3);
                SubFromCombosDI(code_4); SubFromCombosDI(code_5); SubFromCombosDI(code_6);
            }
        }

        ///<summary>Изменили количество приточных фильтров</summary>
        private void FilterPrCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 34, code_2 = 35;                                                        // Фильтр 2 и 3 приточные

            if (filterCheck.Checked)                                                                // Выбран фильтр
            {
                if (filterPrCombo.SelectedIndex == 0)                                               // Один фильтр
                {
                    SubFromCombosDI(code_1); SubFromCombosDI(code_2);                               // Удаление сигналов
                }
                else if (filterPrCombo.SelectedIndex == 1)                                          // Два приточных фильтра
                {
                    SubFromCombosDI(code_2);
                    CheckAddDIToList("Приточный фильтр 2", code_1);
                }
                else if (filterPrCombo.SelectedIndex == 2)                                          // Три приточных фильтра
                {
                    CheckAddDIToList("Приточный фильтр 2", code_1);
                    CheckAddDIToList("Приточный фильтр 3", code_2);
                }
            }
        }

        ///<summary>Изменили количество вытяжных фильтров</summary>
        private void FilterOutCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 66, code_2 = 67, code_3 = 68;                                           // Фильтр 1, 2, 3 вытяжные

            if (comboSysType.SelectedIndex == 1 && filterCheck.Checked)                             // Выбрана ПВ-система и фильтр
            { 
                if (filterOutCombo.SelectedIndex == 0)                                              // Нет вытяжных фильтров
                { 
                    SubFromCombosDI(code_1); SubFromCombosDI(code_2); SubFromCombosDI(code_3);      // Удаление сигналов
                }
                else if (filterOutCombo.SelectedIndex == 1)                                         // Один вытяжной фильтр
                {
                    SubFromCombosDI(code_2); SubFromCombosDI(code_3);
                    CheckAddDIToList("Вытяжной фильтр 1", code_1);
                }
                else if (filterOutCombo.SelectedIndex == 2)                                         // Два вытяжных фильтра
                {
                    SubFromCombosDI(code_3);
                    CheckAddDIToList("Вытяжной фильтр 1", code_1);
                    CheckAddDIToList("Вытяжной фильтр 2", code_2);
                }
                else if (filterOutCombo.SelectedIndex == 3)                                         // Три вытяжных фильтра
                {
                    CheckAddDIToList("Вытяжной фильтр 1", code_1);
                    CheckAddDIToList("Вытяжной фильтр 2", code_2);
                    CheckAddDIToList("Вытяжной фильтр 3", code_3);
                }
            } 
        }

        ///<summary>Выбрали охладитель</summary>
        private void CoolerCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 83, code_2 = 84;                                                        // Термостат и авария фреонового охладителя

            if (coolerCheck.Checked && coolTypeCombo.SelectedIndex == 0)                            // Выбран фреоновый охладитель
            { 
                if (thermoCoolerCheck.Checked)                                                      // Выбран термостат
                    CheckAddDIToList("Термостат фреонового охладителя", code_1);
                if (alarmFrCoolCheck.Checked)                                                       // Авария фреонового охладителя
                    CheckAddDIToList("Авария фреонового охладителя", code_2);
            }
            else                                                                                    // Отмена выбора охладителя
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
        }

        ///<summary>Изменили тип охладителя</summary>
        private void CoolTypeCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 83, code_2 = 84;                                                        // Термостат и авария фреонового охладителя

            if (coolerCheck.Checked)                                                                // Когда выбран охладитель
            {
                if (coolTypeCombo.SelectedIndex == 0)                                               // Фреоновый охладитель
                {
                    if (thermoCoolerCheck.Checked)                                                  // Выбран термостат
                        CheckAddDIToList("Термостат фреонового охладителя", code_1);
                    if (alarmFrCoolCheck.Checked)                                                   // Выбран сигнал аварии
                        CheckAddDIToList("Авария фреонового охладителя", code_2);
                }
                else if (coolTypeCombo.SelectedIndex == 1)                                          // Водяной охладитель
                {
                    SubFromCombosDI(code_1); SubFromCombosDI(code_2);
                }
            }
        }

        ///<summary>Выбрали термостат фреонового охладителя</summary>
        private void ThermoCoolerCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 83;                                                                     // Термостат фреонового охладителя

            if (coolerCheck.Checked && coolTypeCombo.SelectedIndex == 0)                            // Выбран фреоновый охладитель
            { 
                if (thermoCoolerCheck.Checked)                                                      // Выбран термостат
                    CheckAddDIToList("Термостат фреонового охладителя", code_1);
                else                                                                                // Отмена выбора
                    SubFromCombosDI(code_1);
            }
        }

        ///<summary>Выбрали аварийный сигнал фреонового охладителя</summary>
        private void AlarmFrCoolCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 84;

            if (coolerCheck.Checked && coolTypeCombo.SelectedIndex == 0)                            // Выбран фреоновый охладитель
            { 
                if (alarmFrCoolCheck.Checked)                                                       // Выбран сигнал аварии
                    CheckAddDIToList("Авария фреонового охладителя", code_1);
                else                                                                                // Отмена выбора
                    SubFromCombosDI(code_1);
            }
        }

        ///<summary>Выбрали увлажнитель</summary>
        private void HumidCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 69;                                                                     // Авария парового увлажнителя

            if (humidCheck.Checked)                                                                 // Когда выбран увлажнитель
            {
                if (humidTypeCombo.SelectedIndex == 0)                                              // Паровой увлажнитель
                    CheckAddDIToList("Авария парового увлажнителя", code_1);
            }
            else                                                                                    // Отмена выбора увлажнителя
                SubFromCombosDI(code_1);
        }

        ///<summary>Изменили тип увлажнителя</summary>
        private void HumidTypeCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 69;                                                                     // Авария парового увлажнителя

            if (humidCheck.Checked)                                                                 // Когда выбран увлажнитель
            {
                if (humidTypeCombo.SelectedIndex == 0)                                              // Паровой увлажнитель
                {
                    if (alarmHumidCheck.Checked)
                        CheckAddDIToList("Авария парового увлажнителя", code_1);
                }
                else if (humidTypeCombo.SelectedIndex == 1)                                         // Сотовый увлажнитель
                    SubFromCombosDI(code_1);
            }
        }

        ///<summary>Выбрали сигнал аварии парового увлажнителя</summary>
        private void AlarmHumidCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 69;                                                                     // Авария парового увлажнителя

            if (humidCheck.Checked && humidTypeCombo.SelectedIndex == 0)                            // Выбран паровой увлажнитель
            { 
                if (alarmHumidCheck.Checked)
                    CheckAddDIToList("Авария парового увлажнителя", code_1);
                else                                                                                // Отмена выбора
                    SubFromCombosDI(code_1);
            }
        }

        ///<summary>Выбрали рекуператор</summary>
        private void RecupCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 90, code_2 = 91;                                                        // Сигнал PS и аварии ПЧ для роторного рекуператора

            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)                              // Выбрали рекуператор
            {
                if (recDefPsCheck.Checked)                                                          // Выбрали сигнал PS
                    CheckAddDIToList("PS рекуператора", code_1);
                if (recupTypeCombo.SelectedIndex == 0)                                              // Роторный рекуператор
                    CheckAddDIToList("Авария роторного рекуператора", code_2);
            }
            else // Отмена выбора рекуператора
            { 
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
        }

        ///<summary>Изменили тип рекуператора</summary>
        private void RecupTypeCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 91;                                                                     // Сигнал аварии ПЧ для роторного рекуператора

            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)                              // Выбран рекуператор
            {
                if (recupTypeCombo.SelectedIndex == 0)                                              // Роторный рекуператор
                    CheckAddDIToList("Авария роторного рекуператора", code_1);
                else                                                                                // Другой тип рекуператора
                    SubFromCombosDI(code_1);
            }
        }

        ///<summary>Выбрали сигнал PS рекуператора</summary>
        private void RecDefPsCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 90;                                                                     // Сигнал PS рекуператора

            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)                              // Выбран рекуператор
            { 
                if (recDefPsCheck.Checked)                                                          // Выбрали сигнал PS
                    CheckAddDIToList("PS рекуператора", code_1);
                else                                                                                // Отмена выбора сигнала PS
                    SubFromCombosDI(code_1);
            }
        }

        ///<summary>Выбрали сигнал переключателя "Стоп/Пуск"</summary>
        private void StopStartCheck_CheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 3;                                                                      // Переключатель "Стоп/Пуск"

            if (stopStartCheck.Checked)                                                             // Выбрали сигнал для переключателя
                CheckAddDIToList("Переключатель \"Стоп/Пуск\"", code_1);
            else // Отмена выбора сигнала переключателя
                SubFromCombosDI(code_1);
        }

        ///<summary>Выбрали сигнал аварии для приточного вентилятора</summary> 
        private void PrFanAlarmCheck_CheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 12, code_2 = 23;                                                        // Сигнал аварии 1 и 2

            if (prFanAlarmCheck.Checked)                                                            // Выбрали сигнал аварии
            {
                CheckAddDIToList("Сигнал аварии приточного вентилятора 1", code_1);
                if (checkResPrFan.Checked)                                                          // Выбран резерв приточного вентилятора
                    CheckAddDIToList("Сигнал аварии приточного вентилятора 2", code_2);
            }
            else                                                                                    // Отмена выбора сигнала аварии
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
        }

        ///<summary>Выбрали сигнал аварии для вытяжного вентилятора</summary>
        private void OutFanAlarmCheck_CheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 45, code_2 = 56;                                                        // Сигнал аварии 1 и 2

            if (outFanAlarmCheck.Checked)                                                           // Выбрали сигнал аварии
            {
                CheckAddDIToList("Сигнал аварии вытяжного вентилятора 1", code_1);
                if (checkResOutFan.Checked)                                                         // Выбран резерв вытяжного вентилятора
                    CheckAddDIToList("Сигнал аварии вытяжного вентилятора 2", code_2);
            }
            else                                                                                    // Отмена выбора сигнала аварии
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
        }

        ///<summary>Выбрали сигнал пожарной сигнализации</summary>
        private void FireCheck_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 98;                                                                     // Сигнал пожарной сигнализации

            if (fireCheck.Checked)                                                                  // Выбран сигнал
                CheckAddDIToList("Сигнал пожарной сигнализации", code_1);
            else                                                                                    // Отмена выбора сигнала пожарной сигнализации
                SubFromCombosDI(code_1);
        }
    }
}