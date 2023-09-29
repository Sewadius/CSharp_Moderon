using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Moderon
{
    /// <summary>
    /// Класс для аналоговых выходов
    /// </summary>
    class Ao
    {
        public string Name { get; private set; }
        public ushort Code { get; private set; }
        public bool Active { get; private set; } = true;            // Свободен к распределению по умолчанию
        public Ao(string name, ushort code)
        {
            Name = name; Code = code;
        }
        public Ao(string name, ushort code, bool active)
        {
            Name = name; Code = code; Active = active;
        }
        public void Select() => Active = false;
        public void Dispose() => Active = true;
    }

    public partial class Form1 : Form
    {
        readonly List<Ao> list_ao = new List<Ao>();

        bool subAOcondition = false; // Условие при удалении AO

        // Сохранение наименования выбранного элемента для ПЛК и блоков расширения
        string
            AO1combo_text, AO2combo_text, AO3combo_text,
            AO1bl1combo_text, AO2bl1combo_text, AO3bl1combo_text,
            AO1bl2combo_text, AO2bl2combo_text, AO3bl2combo_text,
            AO1bl3combo_text, AO2bl3combo_text, AO3bl3combo_text;

        // Сохранение прошлого индекса comboBox элементов для ПЛК и блоков расширения
        int
            AO1combo_index, AO2combo_index, AO3combo_index,
            AO1bl1combo_index, AO2bl1combo_index, AO3bl1combo_index,
            AO1bl2combo_index, AO2bl2combo_index, AO3bl2combo_index,
            AO1bl3combo_index, AO2bl3combo_index, AO3bl3combo_index;


        ///<summary>Начальная установка для сигналов AO таблицы сигналов</summary>
        public void Set_AOComboTextIndex()
        {
            var ao_signals = new List<string>()
            {
                AO1combo_text, AO2combo_text, AO3combo_text,
                AO1bl1combo_text, AO2bl1combo_text, AO3bl1combo_text,
                AO1bl2combo_text, AO2bl2combo_text, AO3bl2combo_text,
                AO1bl3combo_text, AO2bl3combo_text, AO3bl3combo_text
            };

            var ao_signals_index = new List<int>()
            {
                AO1combo_index, AO2combo_index, AO3combo_index,
                AO1bl1combo_index, AO2bl1combo_index, AO3bl1combo_index,
                AO1bl2combo_index, AO2bl2combo_index, AO3bl2combo_index,
                AO1bl3combo_index, AO2bl3combo_index, AO3bl3combo_index
            };

            for (var i = 0; i < ao_signals.Count; i++)
                ao_signals[i] = NOT_SELECTED;

            for (var i = 0; i < ao_signals_index.Count; i++)
                ao_signals_index[i] = 0;
        }

        ///<summary>Сброс выбора сигналов AO comboBox</summary>
        private void ResetButton_signalsAOClick(object sender, EventArgs e)
        {
            var ao_combos = new List<ComboBox>()
            {
                AO1_combo, AO2_combo, AO3_combo,
                AO1bl1_combo, AO2bl1_combo, AO2bl1_combo,
                AO1bl2_combo, AO2bl2_combo, AO2bl2_combo,
                AO1bl3_combo, AO2bl3_combo, AO2bl3_combo
            };

            for (var i = 0; i < ao_combos.Count; i++)
            {
                ao_combos[i].Items.Clear();
                ao_combos[i].Items.Add(NOT_SELECTED);

            }
        }

        ///<summary>Метод для изменения AO comboBox</summary>
        private void AO_combo_SelectedIndexChanged(ComboBox comboBox, ref int combo_index, ref string combo_text, Label label)
        {
            if (ignoreEvents) return;
            string name = "";
            Ao ao_find = null;
            if (subAOcondition) return;                             // Переход из вычета сигналов AO
            if (comboBox.SelectedIndex == combo_index) return;      // Индекс не поменялся
            if (comboBox.SelectedIndex == 0)                        // Выбрали "Не выбрано"
            {
                if (comboBox.Items.Count > 1)                       // Больше одного элемента в списке
                {
                    string nameFind = combo_text;
                    ao_find = list_ao.Find(x => x.Name == nameFind);
                    list_ao.Remove(ao_find);                        // Удаление из списка
                    if (showCode) label.Text = "";
                }
                if (ao_find != null)                                // Найден элемент
                {
                    ao_find.Dispose();                              // Освобождение сигнала для распределения
                    list_ao.Add(ao_find);                           // Добавление с новым значением
                }
                if (!initialComboSignals)                           // Добавление к другим AO
                    AddtoCombosAO(combo_text, comboBox);
            }
            else
            {
                name = string.Concat(comboBox.SelectedItem);
                ao_find = list_ao.Find(x => x.Name == name);
                list_ao.Remove(ao_find);                            // Удаление из списка
                if (ao_find != null)
                {
                    ao_find.Select();
                    list_ao.Add(ao_find);
                    if (showCode) label.Text = ao_find.Code.ToString();
                }
                if (!initialComboSignals)                           // Если не начальная расстановка
                {
                    SubFromCombosAO(name, comboBox);                // Удаление из других AO
                    string nameFind = combo_text;
                    ao_find = list_ao.Find(x => x.Name == nameFind);
                    list_ao.Remove(ao_find);
                    if (ao_find != null)
                    {
                        ao_find.Dispose();
                        list_ao.Add(ao_find);
                    }
                    AddtoCombosAO(combo_text, comboBox);            // Добавление к другим AO
                }
            }
            combo_text = comboBox.SelectedItem.ToString();          // Сохранение названия выбранного элемента 
            combo_index = comboBox.SelectedIndex;                   // Сохранение индекса выбранного элемента
            CheckSignalsReady();                                    // Проверка распределения сигналов
        }

        ///<summary>Изменили AO1 comboBox</summary>
        private void AO1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AO_combo_SelectedIndexChanged(AO1_combo, ref AO1combo_index, ref AO1combo_text, AO1_lab);
        }

        ///<summary>Изменили AO2 comboBox</summary>
        private void AO2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AO_combo_SelectedIndexChanged(AO2_combo, ref AO2combo_index, ref AO2combo_text, AO2_lab);
        }

        ///<summary>Изменили AO3 comboBox</summary>
        private void AO3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AO_combo_SelectedIndexChanged(AO3_combo, ref AO3combo_index, ref AO3combo_text, AO3_lab);
        }

        ///<summary>Изменили AO1 блока расширения 1</summary>
        private void AO1bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AO_combo_SelectedIndexChanged(AO1bl1_combo, ref AO1bl1combo_index, ref AO1bl1combo_text, AO1bl1_lab);
        }

        ///<summary>Изменили AO2 блока расширения 1</summary>
        private void AO2bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AO_combo_SelectedIndexChanged(AO2bl1_combo, ref AO2bl1combo_index, ref AO2bl1combo_text, AO2bl1_lab);
        }

        ///<summary>Изменили AO3 блока расширения 1</summary>
        private void AO3bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AO_combo_SelectedIndexChanged(AO3bl1_combo, ref AO3bl1combo_index, ref AO3bl1combo_text, AO3bl1_lab);
        }

        ///<summary>Изменили AO1 блока расширения 2</summary>
        private void AO1bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AO_combo_SelectedIndexChanged(AO1bl2_combo, ref AO1bl2combo_index, ref AO1bl2combo_text, AO1bl2_lab);
        }

        ///<summary>Изменили AO2 блока расширения 2</summary>
        private void AO2bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AO_combo_SelectedIndexChanged(AO2bl2_combo, ref AO2bl2combo_index, ref AO2bl2combo_text, AO2bl2_lab);
        }

        ///<summary>Изменили AO3 блока расширения 2</summary>
        private void AO3bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AO_combo_SelectedIndexChanged(AO3bl2_combo, ref AO3bl2combo_index, ref AO3bl2combo_text, AO3bl2_lab);
        }

        ///<summary>Изменили AO1 блока расширения 3</summary>
        private void AO1bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AO_combo_SelectedIndexChanged(AO1bl3_combo, ref AO1bl3combo_index, ref AO1bl3combo_text, AO1bl3_lab);
        }

        ///<summary>Изменили AO2 блока расширения 3</summary>
        private void AO2bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AO_combo_SelectedIndexChanged(AO2bl3_combo, ref AO2bl3combo_index, ref AO2bl3combo_text, AO2bl3_lab);
        }

        ///<summary>Изменили AO3 блока расширения 3</summary>
        private void AO3bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AO_combo_SelectedIndexChanged(AO3bl3_combo, ref AO3bl3combo_index, ref AO3bl3combo_text, AO3bl3_lab);
        }

        ///<summary>Добавление освободившегося AO к остальным comboBox</summary>
        private void AddtoCombosAO(string name, ComboBox cm)
        {
            Ao ao_find;
            bool notFound = true; // Элемент в списке не найден
            // Для AO1 comboBox, добавление в остальные слоты для выбора
            if (AO1_combo != cm)
            {
                ao_find = list_ao.Find(x => x.Name == name);
                if (ao_find != null)
                {
                    foreach (var elem in AO1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) AO1_combo.Items.Add(ao_find.Name); notFound = true;
                }
            }
            // Для AO2 comboBox, добавление в остальные слоты для выбора
            if (AO2_combo != cm)
            {
                ao_find = list_ao.Find(x => x.Name == name);
                if (ao_find != null)
                {
                    foreach (var elem in AO2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) AO2_combo.Items.Add(ao_find.Name); notFound = true;
                }
            }
            // Для AO3 comboBox, добавление в остальные слоты для выбора
            if (AO3_combo != cm)
            {
                ao_find = list_ao.Find(x => x.Name == name);
                if (ao_find != null)
                {
                    foreach (var elem in AO3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) AO3_combo.Items.Add(ao_find.Name); notFound = true;
                }
            }
            // Для AO1 блока расширения 1, добавление в остальные слоты для выбора
            if (AO1bl1_combo != cm)
            {
                ao_find = list_ao.Find(x => x.Name == name);
                if (ao_find != null)
                {
                    foreach (var elem in AO1bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) AO1bl1_combo.Items.Add(ao_find.Name); notFound = true;
                }
            }
            // Для AO2 блока расширения 1, добавление в остальные слоты для выбора
            if (AO2bl1_combo != cm)
            {
                ao_find = list_ao.Find(x => x.Name == name);
                if (ao_find != null)
                {
                    foreach (var elem in AO2bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) AO2bl1_combo.Items.Add(ao_find.Name); notFound = true;
                }
            }
            // Для AO3 блока расширения 1, добавление в остальные слоты для выбора
            if (AO3bl1_combo != cm)
            {
                ao_find = list_ao.Find(x => x.Name == name);
                if (ao_find != null)
                {
                    foreach (var elem in AO3bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) AO3bl1_combo.Items.Add(ao_find.Name); notFound = true;
                }
            }
            // Для AO1 блока расширения 2, добавление в остальные слоты для выбора
            if (AO1bl2_combo != cm)
            {
                ao_find = list_ao.Find(x => x.Name == name);
                if (ao_find != null)
                {
                    foreach (var elem in AO1bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) AO1bl2_combo.Items.Add(ao_find.Name); notFound = true;
                }
            }
            // Для AO2 блока расширения 2, добавление в остальные слоты для выбора
            if (AO2bl2_combo != cm)
            {
                ao_find = list_ao.Find(x => x.Name == name);
                if (ao_find != null)
                {
                    foreach (var elem in AO2bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) AO2bl2_combo.Items.Add(ao_find.Name); notFound = true;
                }
            }
            // Для AO3 блока расширения 2, добавление в остальные слоты для выбора
            if (AO3bl2_combo != cm)
            {
                ao_find = list_ao.Find(x => x.Name == name);
                if (ao_find != null)
                {
                    foreach (var elem in AO3bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) AO3bl2_combo.Items.Add(ao_find.Name); notFound = true;
                }
            }
            // Для AO1 блока расширения 3, добавление в остальные слоты для выбора
            if (AO1bl3_combo != cm)
            {
                ao_find = list_ao.Find(x => x.Name == name);
                if (ao_find != null)
                {
                    foreach (var elem in AO1bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) AO1bl3_combo.Items.Add(ao_find.Name); notFound = true;
                }
            }
            // Для AO2 блока расширения 3, добавление в остальные слоты для выбора
            if (AO2bl3_combo != cm)
            {
                ao_find = list_ao.Find(x => x.Name == name);
                if (ao_find != null)
                {
                    foreach (var elem in AO2bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) AO2bl3_combo.Items.Add(ao_find.Name); notFound = true;
                }
            }
            // Для AO3 блока расширения 3, добавление в остальные слоты для выбора
            if (AO3bl3_combo != cm)
            {
                ao_find = list_ao.Find(x => x.Name == name);
                if (ao_find != null)
                {
                    foreach (var elem in AO3bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) AO3bl3_combo.Items.Add(ao_find.Name); notFound = true;
                }
            }
        }

        ///<summary>Удаление AO из других comboBox</summary>
        private void SubFromCombosAO(string name, ComboBox comboBox)
        {
            var ao_combos = new List<ComboBox>()
            {
                AO1_combo, AO2_combo, AO3_combo, AO1bl1_combo, AO2bl1_combo, AO3bl1_combo,
                AO1bl2_combo, AO2bl2_combo, AO3bl2_combo, AO1bl3_combo, AO2bl3_combo, AO3bl3_combo
            };

            foreach (var el in ao_combos)
                if (name != NOT_SELECTED && el != comboBox)
                    el.Items.Remove(name);
        }

        //private void 

        ///<summary>Добавление нового AO и его назначение под выход, автораспределение</summary>
        private void AddNewAO(ushort code)
        {
            if (AO1_combo.SelectedIndex == 0) // "Не выбрано"
            { // AO1
                AO1_combo.Items.Add(list_ao.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AO1_combo.SelectedIndex = AO1_combo.Items.Count - 1;
                AO1combo_text = AO1_combo.SelectedItem.ToString();
                AO1combo_index = AO1_combo.SelectedIndex;
                if (showCode) AO1_lab.Text = code.ToString();
                list_ao.Find(x => x.Code == code).Select();
            }
            else if (AO2_combo.SelectedIndex == 0) // "Не выбрано"
            { // AO2
                AO2_combo.Items.Add(list_ao.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AO2_combo.SelectedIndex = AO2_combo.Items.Count - 1;
                AO2combo_text = AO2_combo.SelectedItem.ToString();
                AO2combo_index = AO2_combo.SelectedIndex;
                list_ao.Find(x => x.Code == code).Select();
            }
            else if (AO3_combo.SelectedIndex == 0) // "Не выбрано"
            { // AO3
                AO3_combo.Items.Add(list_ao.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AO3_combo.SelectedIndex = AO3_combo.Items.Count - 1;
                AO3combo_text = AO3_combo.SelectedItem.ToString();
                AO3combo_index = AO3_combo.SelectedIndex;
                if (showCode) AO3_lab.Text = code.ToString();
                list_ao.Find(x => x.Code == code).Select();
            }
            else if (AO1bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // AO1, блок 1
                AO1bl1_combo.Items.Add(list_ao.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AO1bl1_combo.SelectedIndex = AO1bl1_combo.Items.Count - 1;
                AO1bl1combo_text = AO1bl1_combo.SelectedItem.ToString();
                AO1bl1combo_index = AO1bl1_combo.SelectedIndex;
                if (showCode) AO1bl1_lab.Text = code.ToString();
                list_ao.Find(x => x.Code == code).Select();
            }
            else if (AO2bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // AO2, блок 1
                AO2bl1_combo.Items.Add(list_ao.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AO2bl1_combo.SelectedIndex = AO2bl1_combo.Items.Count - 1;
                AO2bl1combo_text = AO2bl1_combo.SelectedItem.ToString();
                AO2bl1combo_index = AO2bl1_combo.SelectedIndex;
                if (showCode) AO2bl1_lab.Text = code.ToString();
                list_ao.Find(x => x.Code == code).Select();
            }
            else if (AO3bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // AO3, блок 1
                AO3bl1_combo.Items.Add(list_ao.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AO3bl1_combo.SelectedIndex = AO3bl1_combo.Items.Count - 1;
                AO3bl1combo_text = AO3bl1_combo.SelectedItem.ToString();
                AO3bl1combo_index = AO3bl1_combo.SelectedIndex;
                if (showCode) AO3bl1_lab.Text = code.ToString();
                list_ao.Find(x => x.Code == code).Select();
            }
            else if (AO1bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // AO1, блок 2
                AO1bl2_combo.Items.Add(list_ao.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AO1bl2_combo.SelectedIndex = AO1bl2_combo.Items.Count - 1;
                AO1bl2combo_text = AO1bl2_combo.SelectedItem.ToString();
                AO1bl2combo_index = AO1bl2_combo.SelectedIndex;
                if (showCode) AO1bl2_lab.Text = code.ToString();
                list_ao.Find(x => x.Code == code).Select();
            }
            else if (AO2bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // AO2, блок 2
                AO2bl2_combo.Items.Add(list_ao.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AO2bl2_combo.SelectedIndex = AO2bl2_combo.Items.Count - 1;
                AO2bl2combo_text = AO2bl2_combo.SelectedItem.ToString();
                AO2bl2combo_index = AO2bl2_combo.SelectedIndex;
                if (showCode) AO2bl2_lab.Text = code.ToString();
                list_ao.Find(x => x.Code == code).Select();
            }
            else if (AO3bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // AO3, блок 2
                AO3bl2_combo.Items.Add(list_ao.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AO3bl2_combo.SelectedIndex = AO3bl2_combo.Items.Count - 1;
                AO3bl2combo_text = AO3bl2_combo.SelectedItem.ToString();
                AO3bl2combo_index = AO3bl2_combo.SelectedIndex;
                if (showCode) AO3bl2_lab.Text = code.ToString();
                list_ao.Find(x => x.Code == code).Select();
            }
            else if (AO1bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // AO1, блок 3
                AO1bl3_combo.Items.Add(list_ao.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AO1bl3_combo.SelectedIndex = AO1bl3_combo.Items.Count - 1;
                AO1bl3combo_text = AO1bl3_combo.SelectedItem.ToString();
                AO1bl3combo_index = AO1bl3_combo.SelectedIndex;
                if (showCode) AO1bl3_lab.Text = code.ToString();
                list_ao.Find(x => x.Code == code).Select();
            }
            else if (AO2bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // AO2, блок 3
                AO2bl3_combo.Items.Add(list_ao.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AO2bl3_combo.SelectedIndex = AO2bl3_combo.Items.Count - 1;
                AO2bl3combo_text = AO2bl3_combo.SelectedItem.ToString();
                AO2bl3combo_index = AO2bl3_combo.SelectedIndex;
                if (showCode) AO2bl3_lab.Text = code.ToString();
                list_ao.Find(x => x.Code == code).Select();
            }
            else if (AO3bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // AO3, блок 3
                AO3bl3_combo.Items.Add(list_ao.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AO3bl3_combo.SelectedIndex = AO3bl3_combo.Items.Count - 1;
                AO3bl3combo_text = AO3bl3_combo.SelectedItem.ToString();
                AO3bl3combo_index = AO3bl3_combo.SelectedIndex;
                if (showCode) AO3bl3_lab.Text = code.ToString();
                list_ao.Find(x => x.Code == code).Select();
            }
        }

        ///<summary>Удаление AO из всех comboBox</summary>
        private void SubFromCombosAO(ushort code)
        {
            Ao findAo, findAo_2;
            string name = "";
            findAo = list_ao.Find(x => x.Code == code);
            if (findAo != null) name = findAo.Name;
            else return;
            subAOcondition = true; // Признак удаления AO, не работает событие indexChanged
            for (int i = 0; i < AO1_combo.Items.Count; i++) // AO1
                if (AO1_combo.Items[i].ToString() == name)
                {
                    AO1_combo.Items.Remove(name);
                    if (AO1_combo.Items.Count > 1)
                    {
                        AO1_combo.SelectedIndex = AO1_combo.Items.Count - 1;
                        if (AO1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosAO(AO1_combo.SelectedItem.ToString(), AO1_combo);
                            findAo_2 = list_ao.Find(x => x.Name == AO1_combo.SelectedItem.ToString());
                            if (findAo_2 != null)
                            {
                                list_ao.Remove(findAo_2);
                                findAo_2.Select();
                                list_ao.Add(findAo_2);
                                if (showCode) AO1_lab.Text = findAo_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AO1_combo.SelectedItem = NOT_SELECTED;
                        AO1_lab.Text = "";
                    }
                    AO1combo_text = AO1_combo.SelectedItem.ToString();
                    AO1combo_index = AO1_combo.SelectedIndex;
                }
            for (int i = 0; i < AO2_combo.Items.Count; i++) // AO2
                if (AO2_combo.Items[i].ToString() == name)
                {
                    AO2_combo.Items.Remove(name);
                    if (AO2_combo.Items.Count > 1)
                    {
                        AO2_combo.SelectedIndex = AO2_combo.Items.Count - 1;
                        if (AO2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosAO(AO2_combo.SelectedItem.ToString(), AO2_combo);
                            findAo_2 = list_ao.Find(x => x.Name == AO2_combo.SelectedItem.ToString());
                            if (findAo_2 != null)
                            {
                                list_ao.Remove(findAo_2);
                                findAo_2.Select();
                                list_ao.Add(findAo_2);
                                if (showCode) AO2_lab.Text = findAo_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AO2_combo.SelectedItem = NOT_SELECTED;
                        AO2_lab.Text = "";
                    }
                    AO2combo_text = AO2_combo.SelectedItem.ToString();
                    AO2combo_index = AO2_combo.SelectedIndex;
                }
            for (int i = 0; i < AO3_combo.Items.Count; i++) // AO3
                if (AO3_combo.Items[i].ToString() == name)
                {
                    AO3_combo.Items.Remove(name);
                    if (AO3_combo.Items.Count > 1)
                    {
                        AO3_combo.SelectedIndex = AO3_combo.Items.Count - 1;
                        if (AO3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosAO(AO3_combo.SelectedItem.ToString(), AO3_combo);
                            findAo_2 = list_ao.Find(x => x.Name == AO3_combo.SelectedItem.ToString());
                            if (findAo_2 != null)
                            {
                                list_ao.Remove(findAo_2);
                                findAo_2.Select();
                                list_ao.Add(findAo_2);
                                if (showCode) AO3_lab.Text = findAo_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AO3_combo.SelectedItem = NOT_SELECTED;
                        AO3_lab.Text = "";
                    }
                    AO3combo_text = AO3_combo.SelectedItem.ToString();
                    AO3combo_index = AO3_combo.SelectedIndex;
                }
            for (int i = 0; i < AO1bl1_combo.Items.Count; i++) // AO1 блок 1
                if (AO1bl1_combo.Items[i].ToString() == name)
                {
                    AO1bl1_combo.Items.Remove(name);
                    if (AO1bl1_combo.Items.Count > 1)
                    {
                        AO1bl1_combo.SelectedIndex = AO1bl1_combo.Items.Count - 1;
                        if (AO1bl1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosAO(AO1bl1_combo.SelectedItem.ToString(), AO1bl1_combo);
                            findAo_2 = list_ao.Find(x => x.Name == AO1bl1_combo.SelectedItem.ToString());
                            if (findAo_2 != null)
                            {
                                list_ao.Remove(findAo_2);
                                findAo_2.Select();
                                list_ao.Add(findAo_2);
                                if (showCode) AO1bl1_lab.Text = findAo_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AO1bl1_combo.SelectedItem = NOT_SELECTED;
                        AO1bl1_lab.Text = "";
                    }
                    AO1bl1combo_text = AO1bl1_combo.SelectedItem.ToString();
                    AO1bl1combo_index = AO1bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < AO2bl1_combo.Items.Count; i++) // AO2 блок 1
                if (AO2bl1_combo.Items[i].ToString() == name)
                {
                    AO2bl1_combo.Items.Remove(name);
                    if (AO2bl1_combo.Items.Count > 1)
                    {
                        AO2bl1_combo.SelectedIndex = AO2bl1_combo.Items.Count - 1;
                        if (AO2bl1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosAO(AO2bl1_combo.SelectedItem.ToString(), AO2bl1_combo);
                            findAo_2 = list_ao.Find(x => x.Name == AO2bl1_combo.SelectedItem.ToString());
                            if (findAo_2 != null)
                            {
                                list_ao.Remove(findAo_2);
                                findAo_2.Select();
                                list_ao.Add(findAo_2);
                                if (showCode) AO2bl1_lab.Text = findAo_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AO2bl1_combo.SelectedItem = NOT_SELECTED;
                        AO2bl1_lab.Text = "";
                    }
                    AO2bl1combo_text = AO2bl1_combo.SelectedItem.ToString();
                    AO2bl1combo_index = AO2bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < AO3bl1_combo.Items.Count; i++) // AO3 блок 1
                if (AO3bl1_combo.Items[i].ToString() == name)
                {
                    AO3bl1_combo.Items.Remove(name);
                    if (AO3bl1_combo.Items.Count > 1)
                    {
                        AO3bl1_combo.SelectedIndex = AO3bl1_combo.Items.Count - 1;
                        if (AO3bl1_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosAO(AO3bl1_combo.SelectedItem.ToString(), AO3bl1_combo);
                            findAo_2 = list_ao.Find(x => x.Name == AO3bl1_combo.SelectedItem.ToString());
                            if (findAo_2 != null)
                            {
                                list_ao.Remove(findAo_2);
                                findAo_2.Select();
                                list_ao.Add(findAo_2);
                                if (showCode) AO3bl1_lab.Text = findAo_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AO3bl1_combo.SelectedItem = NOT_SELECTED;
                        AO3bl1_lab.Text = "";
                    }
                    AO3bl1combo_text = AO3bl1_combo.SelectedItem.ToString();
                    AO3bl1combo_index = AO3bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < AO1bl2_combo.Items.Count; i++) // AO1 блок 2
                if (AO1bl2_combo.Items[i].ToString() == name)
                {
                    AO1bl2_combo.Items.Remove(name);
                    if (AO1bl2_combo.Items.Count > 1)
                    {
                        AO1bl2_combo.SelectedIndex = AO1bl2_combo.Items.Count - 1;
                        if (AO1bl2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosAO(AO1bl2_combo.SelectedItem.ToString(), AO1bl2_combo);
                            findAo_2 = list_ao.Find(x => x.Name == AO1bl2_combo.SelectedItem.ToString());
                            if (findAo_2 != null)
                            {
                                list_ao.Remove(findAo_2);
                                findAo_2.Select();
                                list_ao.Add(findAo_2);
                                if (showCode) AO1bl2_lab.Text = findAo_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AO1bl2_combo.SelectedItem = NOT_SELECTED;
                        AO1bl2_lab.Text = "";
                    }
                    AO1bl2combo_text = AO1bl2_combo.SelectedItem.ToString();
                    AO1bl2combo_index = AO1bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < AO2bl2_combo.Items.Count; i++) // AO2 блок 2
                if (AO2bl2_combo.Items[i].ToString() == name)
                {
                    AO2bl2_combo.Items.Remove(name);
                    if (AO2bl2_combo.Items.Count > 1)
                    {
                        AO2bl2_combo.SelectedIndex = AO2bl2_combo.Items.Count - 1;
                        if (AO2bl2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosAO(AO2bl2_combo.SelectedItem.ToString(), AO2bl2_combo);
                            findAo_2 = list_ao.Find(x => x.Name == AO2bl2_combo.SelectedItem.ToString());
                            if (findAo_2 != null)
                            {
                                list_ao.Remove(findAo_2);
                                findAo_2.Select();
                                list_ao.Add(findAo_2);
                                if (showCode) AO2bl2_lab.Text = findAo_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AO2bl2_combo.SelectedItem = NOT_SELECTED;
                        AO2bl2_lab.Text = "";
                    }
                    AO2bl2combo_text = AO2bl2_combo.SelectedItem.ToString();
                    AO2bl2combo_index = AO2bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < AO3bl2_combo.Items.Count; i++) // AO3 блок 2
                if (AO3bl2_combo.Items[i].ToString() == name)
                {
                    AO3bl2_combo.Items.Remove(name);
                    if (AO3bl2_combo.Items.Count > 1)
                    {
                        AO3bl2_combo.SelectedIndex = AO3bl2_combo.Items.Count - 1;
                        if (AO3bl2_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosAO(AO3bl2_combo.SelectedItem.ToString(), AO3bl2_combo);
                            findAo_2 = list_ao.Find(x => x.Name == AO3bl2_combo.SelectedItem.ToString());
                            if (findAo_2 != null)
                            {
                                list_ao.Remove(findAo_2);
                                findAo_2.Select();
                                list_ao.Add(findAo_2);
                                if (showCode) AO3bl2_lab.Text = findAo_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AO3bl2_combo.SelectedItem = NOT_SELECTED;
                        AO3bl2_lab.Text = "";
                    }
                    AO3bl2combo_text = AO3bl2_combo.SelectedItem.ToString();
                    AO3bl2combo_index = AO3bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < AO1bl3_combo.Items.Count; i++) // AO1 блок 3
                if (AO1bl3_combo.Items[i].ToString() == name)
                {
                    AO1bl3_combo.Items.Remove(name);
                    if (AO1bl3_combo.Items.Count > 1)
                    {
                        AO1bl3_combo.SelectedIndex = AO1bl3_combo.Items.Count - 1;
                        if (AO1bl3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosAO(AO1bl3_combo.SelectedItem.ToString(), AO1bl3_combo);
                            findAo_2 = list_ao.Find(x => x.Name == AO1bl3_combo.SelectedItem.ToString());
                            if (findAo_2 != null)
                            {
                                list_ao.Remove(findAo_2);
                                findAo_2.Select();
                                list_ao.Add(findAo_2);
                                if (showCode) AO3bl2_lab.Text = findAo_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AO1bl3_combo.SelectedItem = NOT_SELECTED;
                        AO1bl3_lab.Text = "";
                    }
                    AO1bl3combo_text = AO1bl3_combo.SelectedItem.ToString();
                    AO1bl3combo_index = AO1bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < AO2bl3_combo.Items.Count; i++) // AO2 блок 3
                if (AO2bl3_combo.Items[i].ToString() == name)
                {
                    AO2bl3_combo.Items.Remove(name);
                    if (AO2bl3_combo.Items.Count > 1)
                    {
                        AO2bl3_combo.SelectedIndex = AO2bl3_combo.Items.Count - 1;
                        if (AO2bl3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosAO(AO2bl3_combo.SelectedItem.ToString(), AO2bl3_combo);
                            findAo_2 = list_ao.Find(x => x.Name == AO2bl3_combo.SelectedItem.ToString());
                            if (findAo_2 != null)
                            {
                                list_ao.Remove(findAo_2);
                                findAo_2.Select();
                                list_ao.Add(findAo_2);
                                if (showCode) AO3bl2_lab.Text = findAo_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AO2bl3_combo.SelectedItem = NOT_SELECTED;
                        AO2bl3_lab.Text = "";
                    }
                    AO2bl3combo_text = AO2bl3_combo.SelectedItem.ToString();
                    AO2bl3combo_index = AO2bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < AO3bl3_combo.Items.Count; i++) // AO3 блок 3
                if (AO3bl3_combo.Items[i].ToString() == name)
                {
                    AO3bl3_combo.Items.Remove(name);
                    if (AO3bl3_combo.Items.Count > 1)
                    {
                        AO3bl3_combo.SelectedIndex = AO3bl3_combo.Items.Count - 1;
                        if (AO3bl3_combo.SelectedItem.ToString() != NOT_SELECTED)
                        {
                            SubFromCombosAO(AO3bl3_combo.SelectedItem.ToString(), AO3bl3_combo);
                            findAo_2 = list_ao.Find(x => x.Name == AO3bl3_combo.SelectedItem.ToString());
                            if (findAo_2 != null)
                            {
                                list_ao.Remove(findAo_2);
                                findAo_2.Select();
                                list_ao.Add(findAo_2);
                                if (showCode) AO3bl2_lab.Text = findAo_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AO3bl3_combo.SelectedItem = NOT_SELECTED;
                        AO3bl3_lab.Text = "";
                    }
                    AO3bl3combo_text = AO3bl3_combo.SelectedItem.ToString();
                    AO3bl3combo_index = AO3bl3_combo.SelectedIndex;
                }
            subAOcondition = false; // Сброс признака удаления из AO
            list_ao.Remove(findAo); // Удаление сигнала из списка AO
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        /*** ВЫБОР ЭЛЕМЕНТОВ ***/

        ///<summary>Выбрали ПЧ приточного вентилятора</summary>
        private void PrFanFC_check_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            if (prFanFC_check.Checked) // Выбран ПЧ
            { 
                if (prFanControlCombo.SelectedIndex == 0) // Внешние сигналы
                {
                    prFanSpeedCheck.Enabled = true; // Разблокировка выбора скорости
                }  
                else if (prFanControlCombo.SelectedIndex == 1) // Управление Modbus
                {
                    if (prFanSpeedCheck.Checked) // Отмена выбора скорости
                        prFanSpeedCheck.Checked = false;
                    prFanSpeedCheck.Enabled = false; // Блокировка выбора опции
                }
            }
            else // Отмена выбора ПЧ
            {
                prFanSpeedCheck.Enabled = true; // Разблокировка выбора опции
            }
            //CheckResPrFan_signalsAOCheckedChanged(this, e); // Проверка для резервного вентилятора
        }

        ///<summary>Изменили тип управления ПЧ приточного вентилятора</summary>
        private void PrFanControlCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (prFanFC_check.Checked) // Если выбран ПЧ
            {
                if (prFanControlCombo.SelectedIndex == 0) // Внешние контакты
                {
                    prFanSpeedCheck.Enabled = true; // Разблокировка выбора опции скорости
                    prFanPowSupCheck.Enabled = true; // Разблокировка выбора опции питания
                }
                else if (prFanControlCombo.SelectedIndex == 1) // Modbus
                {
                    if (prFanSpeedCheck.Checked) // Снятие выбора опции
                        prFanSpeedCheck.Checked = false;
                    prFanSpeedCheck.Enabled = false; // Блокировка выбора опции скорости
                    prFanPowSupCheck.Enabled = false; // Блокировка опции выбора питания
                }
            }
            PrFanControlCombo_signalsDISelectedIndexChanged(this, e); // Сигналы DI
            PrFanControlCombo_signalsDOSelectedIndexChanged(this, e); // Сигналы DO
        }

        ///<summary>Выбрали резервный вентилятор приточного</summary>
        private void CheckResPrFan_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 3; // Скорость приточного вентилятора 2
            if (checkResPrFan.Checked) // Выбран резервный вентилятор
            {
                // Выбрано управление скоростью вентилятора
                if (prFanSpeedCheck.Checked)
                {
                    list_ao.Add(new Ao("Скорость приточного вентилятора 2", code_1));
                    AddNewAO(code_1); // Добавление AO к свободному comboBox выхода
                }
            }
            else // Отмена выбора резервного вентилятора
            {
                SubFromCombosAO(code_1); // Удаление AO из comboBox выходов
            }
        }

        ///<summary>Выбрали ПЧ вытяжного вентилятора</summary>
        private void OutFanFC_check_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            if (outFanFC_check.Checked && comboSysType.SelectedIndex == 1) // Выбран ПЧ, ПВ-система
            { 
                if (outFanControlCombo.SelectedIndex == 0) // Внешние контакты
                {
                    outFanSpeedCheck.Enabled = true; // Разблокировка выбора скорости
                }
                else if (outFanControlCombo.SelectedIndex == 1) // Управление Modbus
                {
                    if (outFanSpeedCheck.Checked) // Отмена выбора опции
                        outFanSpeedCheck.Checked = false;
                    outFanSpeedCheck.Enabled = false; // Блокировка опции
                }
            }
            else // Отмена выбора ПЧ
            {
                outFanSpeedCheck.Enabled = true; // Разблокировка выбора скорости
            }
            //CheckResOutFan_signalsAOCheckedChanged(this, e); // Проверка для резервного вентилятора
        }

        ///<summary>Изменили тип управления ПЧ вытяжного вентилятора</summary>
        private void OutFanControlCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (comboSysType.SelectedIndex == 1 && outFanFC_check.Checked) // Если выбран ПЧ, ПВ-система
            {
                if (outFanControlCombo.SelectedIndex == 0) // Внешние контакты
                {
                    outFanSpeedCheck.Enabled = true; // Разблокировка выбора скорости
                    outFanPowSupCheck.Enabled = true; // Разблокировка выбора питания
                }
                else if (outFanControlCombo.SelectedIndex == 1) // Modbus
                {
                    if (outFanSpeedCheck.Checked) // Отмена выбора опции скорости
                        outFanSpeedCheck.Checked = false;
                    outFanSpeedCheck.Enabled = false; // Блокировка выбора скорости
                    outFanPowSupCheck.Enabled = false; // Блокировка выбора питания
                }
            }
            OutFanControlCombo_signalsDISelectedIndexChanged(this, e); // Сигналы DI
            OutFanControlCombo_signalsDOSelectedIndexChanged(this, e); // Сигналы DO
        }

        ///<summary>Выбрали резервный вентилятор вытяжного</summary>
        private void CheckResOutFan_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 7; // Скорость вытяжного вентилятора 2
            if (checkResOutFan.Checked && comboSysType.SelectedIndex == 1) // Выбран резервный вентилятор, ПВ-система
            {
                if (outFanSpeedCheck.Checked) // Выбрана скорость для вытяжного вентилятора
                {
                    list_ao.Add(new Ao("Скорость вытяжного вентилятора 2", code_1));
                    AddNewAO(code_1); // Добавление AO к свободному comboBox выхода
                }
            }
            else // Отмена выбора резервного вентилятора
            {
                SubFromCombosAO(code_1); // Удаление AO из comboBox выходов
            }
        }

        ///<summary>Выбрали нагреватель</summary>
        private void HeaterCheck_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 9, code_2 = 11; // Водяной нагреватель 0-10 В, электронагреватель
            if (heaterCheck.Checked) // Выбрали нагреватель
            {
                if (heatTypeCombo.SelectedIndex == 0) // Водяной нагреватель
                {
                    list_ao.Add(new Ao("Водяной калорифер 0-10 В", code_1));
                    AddNewAO(code_1); // Добавление AO к свободному comboBox выхода
                }
                else if (heatTypeCombo.SelectedIndex == 1) // Электрический нагреватель
                {
                    if (firstStHeatCombo.SelectedIndex == 1) // ШИМ 5 В
                        list_ao.Add(new Ao("Электрический калорифер ШИМ 5 В", code_2));
                    else if (firstStHeatCombo.SelectedIndex == 2) // 0-10 В
                        list_ao.Add(new Ao("Электрический калорифер 0-10 В", code_2));
                    if (firstStHeatCombo.SelectedIndex != 0) // Если не дискретное управление
                        AddNewAO(code_2); // Добавление AO к свободному comboBox выхода
                }
            } 
            else // Отмена выбора нагревателя
            { // Удаление AO из comboBox выходов
                SubFromCombosAO(code_1); SubFromCombosAO(code_2);
            }
        }

        ///<summary>Изменили тип нагревателя</summary>
        private void HeatTypeCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 9, code_2 = 11; // Водяной нагреватель 0-10 В, электронагреватель
            if (heaterCheck.Checked) // Когда выбран нагреватель
            {
                if (heatTypeCombo.SelectedIndex == 0) // Водяной нагреватель
                {
                    SubFromCombosAO(code_2); // Удаление для электронагревателя
                    System.Threading.Thread.Sleep(10);
                    list_ao.Add(new Ao("Водяной калорифер 0-10 В", code_1));
                    AddNewAO(code_1); // Добавление AO к свободному comboBox выхода
                }
                else if (heatTypeCombo.SelectedIndex == 1) // Электрический нагреватель
                {
                    SubFromCombosAO(code_1); // Удаление для водяного калорифера
                    System.Threading.Thread.Sleep(10);
                    if (firstStHeatCombo.SelectedIndex == 1) // ШИМ 5 В
                        list_ao.Add(new Ao("Электрический калорифер ШИМ 5 В", code_2));
                    else if (firstStHeatCombo.SelectedIndex == 2) // 0-10 В
                        list_ao.Add(new Ao("Электрический калорифер 0-10 В", code_2));
                    if (firstStHeatCombo.SelectedIndex != 0) // Если не дискретное управление
                        AddNewAO(code_2); // Добавление AO к свободному comboBox выхода
                }
            }
        }

        ///<summary>Изменили тип управления первой ступенью нагревателя</summary>
        private void FirstStHeatCombo_SignalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 11; // Электронагреватель
            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 1)
            { // Когда выбран электрический нагреватель
                if (firstStHeatCombo.SelectedIndex == 0) // Дискретное управление
                {
                    SubFromCombosAO(code_1); // Удаление сигнала
                }
                else if (firstStHeatCombo.SelectedIndex == 1) // ШИМ, 5В
                {
                    SubFromCombosAO(code_1); // Удаление сигнала
                    System.Threading.Thread.Sleep(10);
                    list_ao.Add(new Ao("Электрический калорифер ШИМ 5 В", code_1));
                    AddNewAO(code_1);
                }
                else if (firstStHeatCombo.SelectedIndex == 2) // Сигнал 0-10 В
                {
                    SubFromCombosAO(code_1); // Удаление сигнала
                    System.Threading.Thread.Sleep(10);
                    list_ao.Add(new Ao("Электрический калорифер 0-10 В", code_1));
                    AddNewAO(code_1);
                }
            }
        }

        ///<summary>Выбрали догреватель</summary>
        private void AddHeatCheck_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 23, code_2 = 21; // Водяной догреватель 0-10 В, электродогреватель
            if (addHeatCheck.Checked) // Выбрали догреватель
            {
                if (heatAddTypeCombo.SelectedIndex == 0) // Водяной догреватель
                {
                    list_ao.Add(new Ao("Водяной догреватель 0-10 В", code_1));
                    AddNewAO(code_1); // Добавление AO к свободному comboBox выхода
                }
                else if (heatAddTypeCombo.SelectedIndex == 1) // Электрическй догреватель
                {
                    if (firstStAddHeatCombo.SelectedIndex == 1) // ШИМ 5 В
                        list_ao.Add(new Ao("Электрический догреватель ШИМ 5 В", code_2));
                    else if (firstStAddHeatCombo.SelectedIndex == 2) // 0-10 В
                        list_ao.Add(new Ao("Электрический догреватель 0-10 В", code_2));
                    if (firstStAddHeatCombo.SelectedIndex != 0) // Если не дискретное управление
                        AddNewAO(code_2); // Добавление AO к свободному comboBox выхода
                }
            }
            else // Отмена выбора догревателя
            { // Удаление AO из comboBox выходов
                SubFromCombosAO(code_1); SubFromCombosAO(code_2);
            }
        }

        ///<summary>Изменили тип догревателя</summary>
        private void HeatAddTypeCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 23, code_2 = 21; // Водяной догреватель 0-10 В, электродогреватель
            if (addHeatCheck.Checked) // Когда выбран догреватель
            {
                if (heatAddTypeCombo.SelectedIndex == 0) // Водяной догреватель
                {
                    SubFromCombosAO(code_2); // Удаление для электродогревателя
                    list_ao.Add(new Ao("Водяной догреватель 0-10 В", code_1));
                    System.Threading.Thread.Sleep(10);
                    AddNewAO(code_1); // Добавление AO к свободному comboBox выхода
                }
                else if (heatAddTypeCombo.SelectedIndex == 1) // Электрический догреватель
                {
                    SubFromCombosAO(code_1); // Удаление для водяного догревателя
                    System.Threading.Thread.Sleep(10);
                    if (firstStAddHeatCombo.SelectedIndex == 1) // ШИМ 5 В
                        list_ao.Add(new Ao("Электрический догреватель ШИМ 5 В", code_2));
                    else if (firstStAddHeatCombo.SelectedIndex == 2) // 0-10 В
                        list_ao.Add(new Ao("Электрический догреватель 0-10 В", code_2));
                    if (firstStAddHeatCombo.SelectedIndex != 0) // Если не дискретное управление
                        AddNewAO(code_2); // Добавление AO к свободному comboBox выхода
                }
            }
        }

        ///<summary>Изменили тип управления первой ступенью догревателя</summary>
        private void FirstStAddHeatCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 21; // Электродогреватель
            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 1)
            { // Когда выбран электрический догреватель
                if (firstStAddHeatCombo.SelectedIndex == 0) // Дискретное управление
                {
                    SubFromCombosAO(code_1); // Удаление сигнала
                }
                else if (firstStAddHeatCombo.SelectedIndex == 1) // ШИМ, 5В
                {
                    SubFromCombosAO(code_1); // Удаление сигнала
                    System.Threading.Thread.Sleep(10);
                    list_ao.Add(new Ao("Электрический догреватель ШИМ 5 В", code_1));
                    AddNewAO(code_1);
                }
                else if (firstStAddHeatCombo.SelectedIndex == 2) // Сигнал 0-10 В
                {
                    SubFromCombosAO(code_1); // Удаление сигнала
                    System.Threading.Thread.Sleep(10);
                    list_ao.Add(new Ao("Электрический догреватель 0-10 В", code_1));
                    AddNewAO(code_1);
                }
            }
        }

        ///<summary>Выбрали охладитель</summary>
        private void CoolerCheck_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 13; // Аналоговое управление охладителем
            if (coolerCheck.Checked) // Выбрали охладитель
            {   // Водяной охладитель, либо выбран сигнал 0-10 В фреон
                if (coolTypeCombo.SelectedIndex == 1 || analogFreonCheck.Checked)
                {
                    list_ao.Add(new Ao("Охладитель 0-10 В", code_1));
                    AddNewAO(code_1);
                }
            }
            else // Отмена выбора охладителя
            {   // Водяной охладитель, либо сигнал 0-10 В фреон
                if (coolTypeCombo.SelectedIndex == 1 || analogFreonCheck.Checked) 
                    SubFromCombosAO(code_1); // Удаление сигнала
            }
        }

        ///<summary>Изменили тип охладителя</summary>
        private void CoolTypeCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 13; // Аналоговое управление охладителем
            if (coolerCheck.Checked) // Выбран охладитель
            {
                if (coolTypeCombo.SelectedIndex == 0) // Фреоновый
                {
                    if (!analogFreonCheck.Checked) // Не было выбрано сигнала 0-10 В фреон
                        SubFromCombosAO(code_1); // Удаление сигнала
                }
                else if (coolTypeCombo.SelectedIndex == 1) // Водяной
                {
                    if (!analogFreonCheck.Checked) // Не было выбрано сигнала 0-10 В фреон
                    {
                        list_ao.Add(new Ao("Охладитель 0-10 В", code_1));
                        AddNewAO(code_1);
                    }
                }
            }
        }

        ///<summary>Выбрали сигнал 0-10 В для фреонового охладителя</summary>
        private void AnalogFreonCheck_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 13; // Аналоговое управление охладителем
            if (coolerCheck.Checked && coolTypeCombo.SelectedIndex == 0) // Выбран фреоновый охладитель
            {
                if (analogFreonCheck.Checked) // Выбрали сигнал 0-10 В
                {
                    list_ao.Add(new Ao("Охладитель 0-10 В", code_1));
                    AddNewAO(code_1);
                }
                else // Отмена выбора сигнала 0-10 В
                    SubFromCombosAO(code_1); // Удаление сигнала
            }
        }

        ///<summary>Выбрали увлажнитель</summary>
        private void HumidCheck_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 25; // Аналоговое управление увлажнителем
            if (humidCheck.Checked) // Выбрали увлажнитель
            {
                if (humidTypeCombo.SelectedIndex == 0) // Паровой увлажнитель
                {
                    list_ao.Add(new Ao("Увлажнитель 0-10 В", code_1));
                    AddNewAO(code_1);
                }
            }
            else // Отмена выбора увлажнителя
            {
                SubFromCombosAO(code_1); // Удаление сигнала
            }
        }

        ///<summary>Изменили тип увлажнителя</summary>
        private void HumidTypeCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 25; // Аналоговое управление увлажнителем
            if (humidCheck.Checked) // Когда выбран увлажнитель
            {
                if (humidTypeCombo.SelectedIndex == 0) // Паровой увлажнитель
                {
                    list_ao.Add(new Ao("Увлажнитель 0-10 В", code_1));
                    AddNewAO(code_1);
                }
                else if (humidTypeCombo.SelectedIndex == 1) // Сотовый увлажнитель
                {
                    SubFromCombosAO(code_1); // Удаление сигнала
                }
            }
        }

        ///<summary>Выбрали рециркуляцию</summary>
        private void RecircCheck_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 14; // Аналоговое управление рециркуляцией
            if (recircCheck.Checked && comboSysType.SelectedIndex == 1)
            { // Выбрали рециркуляцию и ПВ-система
                list_ao.Add(new Ao("Рециркуляция 0-10 В", code_1));
                AddNewAO(code_1);
            }
            else // Отмена выбора рециркуляции
            {
                SubFromCombosAO(code_1); // Удаление сигнала
            }
        }

        ///<summary>Выбрали рекуператор</summary>
        private void RecupCheck_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 15; // Аналоговое управление рекуператором
            if (recupCheck.Checked && comboSysType.SelectedIndex == 1)
            { // Выбрали рекуператор и ПВ-система
                if (recupTypeCombo.SelectedIndex == 0 || recupTypeCombo.SelectedIndex == 2 ||
                        (recupTypeCombo.SelectedIndex == 1 && bypassPlastCombo.SelectedIndex == 2))
                { // Роторный, гликолевый или пластинчатый 0-10 В
                    list_ao.Add(new Ao("Рекуператор 0-10 В", code_1));
                    AddNewAO(code_1);
                }
            }
            else // Отмена выбора рекуператора
            {
                SubFromCombosAO(code_1); // Удаление сигнала
            }
        }

        ///<summary>Изменили тип рекуператора</summary>
        private void RecupTypeCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 15; // Аналоговое управление рекуператором
            if (recupCheck.Checked && comboSysType.SelectedIndex == 1)
            { // Выбрали рекуператор и ПВ-система
                SubFromCombosAO(code_1); // Удаление сигнала
                System.Threading.Thread.Sleep(10);
                if (recupTypeCombo.SelectedIndex == 0 || recupTypeCombo.SelectedIndex == 2)
                { // Роторный или гликолевый рекуператор
                    list_ao.Add(new Ao("Рекуператор 0-10 В", code_1));
                    AddNewAO(code_1);
                }
                else if (recupTypeCombo.SelectedIndex == 1) // Пластинчатый рекуператор
                {
                    if (bypassPlastCombo.SelectedIndex == 2) // Управление 0-10 В
                    {
                        list_ao.Add(new Ao("Рекуператор 0-10 В", code_1));
                        AddNewAO(code_1);
                    }
                }
            }
        }

        ///<summary>Изменили тип управления пластинчатого рекуператора</summary>
        private void BypassPlastCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 15; // Аналоговое управление рекуператором
            if (recupCheck.Checked && comboSysType.SelectedIndex == 1 && recupTypeCombo.SelectedIndex == 1)
            { // Выбран пластинчатый рекуператор и ПВ-система
                if (bypassPlastCombo.SelectedIndex == 2) // Управление 0-10 В
                {
                    list_ao.Add(new Ao("Рекуператор 0-10 В", code_1));
                    AddNewAO(code_1);
                }
                else // Другой тип управления
                {
                    SubFromCombosAO(code_1); // Удаление сигнала
                }
            }
        }

        ///<summary>Выбрали управление скоростью для приточного вентилятора</summary>
        private void PrFanSpeedCheck_CheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1; // Скорость приточного 1
            ushort code_2 = 3; // Скорость приточного 2
            if (prFanSpeedCheck.Checked) // Выбрали управление скоростью вентилятора
            {
                list_ao.Add(new Ao("Скорость приточного вентилятора 1", code_1));
                AddNewAO(code_1);
                if (checkResPrFan.Checked) // Если выбран резерв П
                {
                    list_ao.Add(new Ao("Скорость приточного вентилятора 2", code_2));
                    AddNewAO(code_2);
                }
            }
            else // Отмена выбора управления скоростью
            {
                SubFromCombosAO(code_1); 
                if (checkResPrFan.Checked) SubFromCombosAO(code_2); // Удаление сигналов
            }
        }

        ///<summary>Выбрали управление скоростью для вытяжного вентилятора</summary>
        private void OutFanSpeedCheck_CheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 5; // Скорость вытяжного 1
            ushort code_2 = 7; // Скорость вытяжного 2
            if (outFanSpeedCheck.Checked) // Выбрали управление скоростью вентилятора
            {
                list_ao.Add(new Ao("Скорость вытяжного вентилятора 1", code_1));
                AddNewAO(code_1);
                if (checkResPrFan.Checked) // Если выбран резерв В
                {
                    list_ao.Add(new Ao("Скорость вытяжного вентилятора 2", code_2));
                    AddNewAO(code_2);
                }
            }
            else // Отмена выбора управления скоростью
            {
                SubFromCombosAO(code_1); 
                if (checkResOutFan.Checked) SubFromCombosAO(code_2); // Удаление сигналов
            }
        }
    }
}
