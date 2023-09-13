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

        // Сохранение наименования выбранного элемента для ПЛК
        string DI1combo_text = notSelected;
        string DI2combo_text = notSelected;
        string DI3combo_text = notSelected;
        string DI4combo_text = notSelected;
        string DI5combo_text = notSelected;

        // Сохранение наименования выбранного элемента для блока 1
        string DI1bl1combo_text = notSelected;
        string DI2bl1combo_text = notSelected;
        string DI3bl1combo_text = notSelected;
        string DI4bl1combo_text = notSelected;
        string DI5bl1combo_text = notSelected;

        // Сохранение наименования выбранного элемента для блока 2
        string DI1bl2combo_text = notSelected;
        string DI2bl2combo_text = notSelected;
        string DI3bl2combo_text = notSelected;
        string DI4bl2combo_text = notSelected;
        string DI5bl2combo_text = notSelected;

        // Сохранение наименования выбранного элемента для блока 3
        string DI1bl3combo_text = notSelected;
        string DI2bl3combo_text = notSelected;
        string DI3bl3combo_text = notSelected;
        string DI4bl3combo_text = notSelected;
        string DI5bl3combo_text = notSelected;

        // Сохранение прошлого индекса для comboBox ПЛК
        int DI1combo_index = 0;
        int DI2combo_index = 0;
        int DI3combo_index = 0;
        int DI4combo_index = 0;
        int DI5combo_index = 0;

        // Сохранение прошлого индекса для comboBox блока 1
        int DI1bl1combo_index = 0;
        int DI2bl1combo_index = 0;
        int DI3bl1combo_index = 0;
        int DI4bl1combo_index = 0;
        int DI5bl1combo_index = 0;

        // Сохранение прошлого индекса для comboBox блока 2
        int DI1bl2combo_index = 0;
        int DI2bl2combo_index = 0;
        int DI3bl2combo_index = 0;
        int DI4bl2combo_index = 0;
        int DI5bl2combo_index = 0;

        // Сохранение прошлого индекса для comboBox блока 3
        int DI1bl3combo_index = 0;
        int DI2bl3combo_index = 0;
        int DI3bl3combo_index = 0;
        int DI4bl3combo_index = 0;
        int DI5bl3combo_index = 0;

        ///<summary>Сброс выбора сигналов DI comboBox</summary>
        private void ResetButton_signalsDIClick(object sender, EventArgs e)
        {
            // Очистка comboBox ПЛК
            DI1_combo.Items.Clear(); DI1_combo.Items.Add(notSelected);
            DI2_combo.Items.Clear(); DI2_combo.Items.Add(notSelected);
            DI3_combo.Items.Clear(); DI3_combo.Items.Add(notSelected);
            DI4_combo.Items.Clear(); DI4_combo.Items.Add(notSelected);
            DI5_combo.Items.Clear(); DI5_combo.Items.Add(notSelected);
            // Очистка comboBox блок расширения 1
            DI1bl1_combo.Items.Clear(); DI1bl1_combo.Items.Add(notSelected);
            DI2bl1_combo.Items.Clear(); DI2bl1_combo.Items.Add(notSelected);
            DI3bl1_combo.Items.Clear(); DI3bl1_combo.Items.Add(notSelected);
            DI4bl1_combo.Items.Clear(); DI4bl1_combo.Items.Add(notSelected);
            DI5bl1_combo.Items.Clear(); DI5bl1_combo.Items.Add(notSelected);
            // Очистка comboBox блок расширения 2
            DI1bl2_combo.Items.Clear(); DI1bl2_combo.Items.Add(notSelected);
            DI2bl2_combo.Items.Clear(); DI2bl2_combo.Items.Add(notSelected);
            DI3bl2_combo.Items.Clear(); DI3bl2_combo.Items.Add(notSelected);
            DI4bl2_combo.Items.Clear(); DI4bl2_combo.Items.Add(notSelected);
            DI5bl2_combo.Items.Clear(); DI5bl2_combo.Items.Add(notSelected);
            // Очистка comboBox блок расширения 3
            DI1bl3_combo.Items.Clear(); DI1bl3_combo.Items.Add(notSelected);
            DI2bl3_combo.Items.Clear(); DI2bl3_combo.Items.Add(notSelected);
            DI3bl3_combo.Items.Clear(); DI3bl3_combo.Items.Add(notSelected);
            DI4bl3_combo.Items.Clear(); DI4bl3_combo.Items.Add(notSelected);
            DI5bl3_combo.Items.Clear(); DI5bl3_combo.Items.Add(notSelected);
        }

        ///<summary>Удаление элемента из всех comboBox DI</summary>
        private void RemoveFromAllCombosDI(string name)
        {
            // Основной ПЛК
            DI1_combo.Items.Remove(name); DI2_combo.Items.Remove(name);
            DI3_combo.Items.Remove(name); DI4_combo.Items.Remove(name);
            DI5_combo.Items.Remove(name);
            // Блок расширения 1
            DI1bl1_combo.Items.Remove(name); DI2bl1_combo.Items.Remove(name);
            DI3bl1_combo.Items.Remove(name); DI4bl1_combo.Items.Remove(name);
            DI5bl1_combo.Items.Remove(name);
            // Блок расширения 2
            DI1bl2_combo.Items.Remove(name); DI2bl2_combo.Items.Remove(name);
            DI3bl2_combo.Items.Remove(name); DI4bl2_combo.Items.Remove(name);
            DI5bl2_combo.Items.Remove(name);
            // Блок расширения 3
            DI1bl3_combo.Items.Remove(name); DI2bl3_combo.Items.Remove(name);
            DI3bl3_combo.Items.Remove(name); DI4bl3_combo.Items.Remove(name);
            DI5bl3_combo.Items.Remove(name);
        }

        ///<summary>Добавление элемента ко всем comboBox DI</summary>
        private void AddToAllCombosDI(string name)
        {
            // Основной ПЛК
            DI1_combo.Items.Add(name); DI2_combo.Items.Add(name);
            DI3_combo.Items.Add(name); DI4_combo.Items.Add(name);
            DI5_combo.Items.Add(name);
            // Блок расширения 1
            DI1bl1_combo.Items.Add(name); DI2bl1_combo.Items.Add(name);
            DI3bl1_combo.Items.Add(name); DI4bl1_combo.Items.Add(name);
            DI5bl1_combo.Items.Add(name);
            // Блок расширения 2
            DI1bl2_combo.Items.Add(name); DI2bl2_combo.Items.Add(name);
            DI3bl2_combo.Items.Add(name); DI4bl2_combo.Items.Add(name);
            DI5bl2_combo.Items.Add(name);
            // Блок расширения 3
            DI1bl3_combo.Items.Add(name); DI2bl3_combo.Items.Add(name);
            DI3bl3_combo.Items.Add(name); DI4bl3_combo.Items.Add(name);
            DI5bl3_combo.Items.Add(name);
        }

        ///<summary>Изменили DI1 comboBox</summary>
        private void DI1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI1_combo.SelectedIndex == DI1combo_index) return; // Индекс не поменялся
            if (DI1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI1_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI1combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI1combo_text)); // Удаление из списка
                    if (showCode) DI1_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI1combo_text, DI1_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI1_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI1_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI1_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI1combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI1combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI1combo_text, DI1_combo); // Добавление к другим DI
                }
            }
            DI1combo_text = DI1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI1combo_index = DI1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI2 comboBox</summary>
        private void DI2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI2_combo.SelectedIndex == DI2combo_index) return; // Индекс не поменялся
            if (DI2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI2_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI2combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI2combo_text)); // Удаление из списка
                    if (showCode) DI2_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI2combo_text, DI2_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI2_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI2_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI2_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI2combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI2combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI2combo_text, DI2_combo); // Добавление к другим DI
                }
            }
            DI2combo_text = DI2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI2combo_index = DI2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI3 comboBox</summary>
        private void DI3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI3_combo.SelectedIndex == DI3combo_index) return; // Индекс не поменялся
            if (DI3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI3_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI3combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI3combo_text)); // Удаление из списка
                    if (showCode) DI3_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI3combo_text, DI3_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI3_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI3_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI3_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI3combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI3combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI3combo_text, DI3_combo); // Добавление к другим DI
                }
            }
            DI3combo_text = DI3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI3combo_index = DI3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI4 comboBox</summary>
        private void DI4_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI4_combo.SelectedIndex == DI4combo_index) return; // Индекс не поменялся
            if (DI4_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI4_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI4combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI4combo_text)); // Удаление из списка
                    if (showCode) DI4_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI4combo_text, DI4_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI4_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI4_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI4_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI4combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI4combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI4combo_text, DI4_combo); // Добавление к другим DI
                }
            }
            DI4combo_text = DI4_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI4combo_index = DI4_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI5 comboBox</summary>
        private void DI5_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI5_combo.SelectedIndex == DI5combo_index) return; // Индекс не поменялся
            if (DI5_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI5_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI5combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI5combo_text)); // Удаление из списка
                    if (showCode) DI5_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI5combo_text, DI5_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI5_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI5_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI5_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI5combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI5combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI5combo_text, DI5_combo); // Добавление к другим DI
                }
            }
            DI5combo_text = DI5_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI5combo_index = DI5_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI1 блока расширения 1</summary>
        private void DI1bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI1bl1_combo.SelectedIndex == DI1bl1combo_index) return; // Индекс не поменялся
            if (DI1bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI1bl1_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI1bl1combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI1bl1combo_text)); // Удаление из списка
                    if (showCode) DI1bl1_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI1bl1combo_text, DI1bl1_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI1bl1_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI1bl1_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI1bl1_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI1bl1combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI1bl1combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI1bl1combo_text, DI1bl1_combo); // Добавление к другим DI
                }
            }
            DI1bl1combo_text = DI1bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI1bl1combo_index = DI1bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI2 блока расширения 1</summary>
        private void DI2bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI2bl1_combo.SelectedIndex == DI2bl1combo_index) return; // Индекс не поменялся
            if (DI2bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI2bl1_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI2bl1combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI2bl1combo_text)); // Удаление из списка
                    if (showCode) DI2bl1_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI2bl1combo_text, DI2bl1_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI2bl1_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI2bl1_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI2bl1_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI2bl1combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI2bl1combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI2bl1combo_text, DI2bl1_combo); // Добавление к другим DI
                }
            }
            DI2bl1combo_text = DI2bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI2bl1combo_index = DI2bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI3 блока расширения 1</summary>
        private void DI3bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI3bl1_combo.SelectedIndex == DI3bl1combo_index) return; // Индекс не поменялся
            if (DI3bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI3bl1_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI3bl1combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI3bl1combo_text)); // Удаление из списка
                    if (showCode) DI3bl1_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI3bl1combo_text, DI3bl1_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI3bl1_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI3bl1_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI3bl1_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI3bl1combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI3bl1combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI3bl1combo_text, DI3bl1_combo); // Добавление к другим DI
                }
            }
            DI3bl1combo_text = DI3bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI3bl1combo_index = DI3bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI4 блока расширения 1</summary>
        private void DI4bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI4bl1_combo.SelectedIndex == DI4bl1combo_index) return; // Индекс не поменялся
            if (DI4bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI4bl1_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI4bl1combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI4bl1combo_text)); // Удаление из списка
                    if (showCode) DI4bl1_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI4bl1combo_text, DI4bl1_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI4bl1_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI4bl1_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI4bl1_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI4bl1combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI4bl1combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI4bl1combo_text, DI4bl1_combo); // Добавление к другим DI
                }
            }
            DI4bl1combo_text = DI4bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI4bl1combo_index = DI4bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI5 блока расширения 1</summary>
        private void DI5bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI5bl1_combo.SelectedIndex == DI5bl1combo_index) return; // Индекс не поменялся
            if (DI5bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI5bl1_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI5bl1combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI5bl1combo_text)); // Удаление из списка
                    if (showCode) DI5bl1_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI5bl1combo_text, DI5bl1_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI5bl1_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI5bl1_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI5bl1_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI5bl1combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI5bl1combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI5bl1combo_text, DI5bl1_combo); // Добавление к другим DI
                }
            }
            DI5bl1combo_text = DI5bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI5bl1combo_index = DI5bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI1 блока расширения 2</summary>
        private void DI1bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI1bl2_combo.SelectedIndex == DI1bl2combo_index) return; // Индекс не поменялся
            if (DI1bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI1bl2_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI1bl2combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI1bl2combo_text)); // Удаление из списка
                    if (showCode) DI1bl2_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI1bl2combo_text, DI1bl2_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI1bl2_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI1bl2_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI1bl2_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI1bl2combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI1bl2combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI1bl2combo_text, DI1bl2_combo); // Добавление к другим DI
                }
            }
            DI1bl2combo_text = DI1bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI1bl2combo_index = DI1bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI2 блока расширения 2</summary>
        private void DI2bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI2bl2_combo.SelectedIndex == DI2bl2combo_index) return; // Индекс не поменялся
            if (DI2bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI2bl2_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI2bl2combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI2bl2combo_text)); // Удаление из списка
                    if (showCode) DI2bl2_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI2bl2combo_text, DI2bl2_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI2bl2_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI2bl2_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI2bl2_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI2bl2combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI2bl2combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI2bl2combo_text, DI2bl2_combo); // Добавление к другим DI
                }
            }
            DI2bl2combo_text = DI2bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI2bl2combo_index = DI2bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI3 блока расширения 2</summary>
        private void DI3bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI3bl2_combo.SelectedIndex == DI3bl2combo_index) return; // Индекс не поменялся
            if (DI3bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI3bl2_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI3bl2combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI3bl2combo_text)); // Удаление из списка
                    if (showCode) DI3bl2_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI3bl2combo_text, DI3bl2_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI3bl2_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI3bl2_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI3bl2_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI3bl2combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI3bl2combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI3bl2combo_text, DI3bl2_combo); // Добавление к другим DI
                }
            }
            DI3bl2combo_text = DI3bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI3bl2combo_index = DI3bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI4 блока расширения 2</summary>
        private void DI4bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI4bl2_combo.SelectedIndex == DI4bl2combo_index) return; // Индекс не поменялся
            if (DI4bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI4bl2_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI4bl2combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI4bl2combo_text)); // Удаление из списка
                    if (showCode) DI4bl2_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI4bl2combo_text, DI4bl2_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI4bl2_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI4bl2_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI4bl2_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI4bl2combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI4bl2combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI4bl2combo_text, DI4bl2_combo); // Добавление к другим DI
                }
            }
            DI4bl2combo_text = DI4bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI4bl2combo_index = DI4bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI5 блока расширения 2</summary>
        private void DI5bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI5bl2_combo.SelectedIndex == DI5bl2combo_index) return; // Индекс не поменялся
            if (DI5bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI5bl2_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI5bl2combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI5bl2combo_text)); // Удаление из списка
                    if (showCode) DI5bl2_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI5bl2combo_text, DI5bl2_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI5bl2_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI5bl2_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI5bl2_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI5bl2combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI5bl2combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI5bl2combo_text, DI5bl2_combo); // Добавление к другим DI
                }
            }
            DI5bl2combo_text = DI5bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI5bl2combo_index = DI5bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI1 блока расширения 3</summary>
        private void DI1bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI1bl3_combo.SelectedIndex == DI1bl3combo_index) return; // Индекс не поменялся
            if (DI1bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI1bl3_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI1bl3combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI1bl3combo_text)); // Удаление из списка
                    if (showCode) DI1bl3_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI1bl3combo_text, DI1bl3_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI1bl3_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI1bl3_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI1bl3_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI1bl3combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI1bl3combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI1bl3combo_text, DI1bl3_combo); // Добавление к другим DI
                }
            }
            DI1bl3combo_text = DI1bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI1bl3combo_index = DI1bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI2 блока расширения 3</summary>
        private void DI2bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI2bl3_combo.SelectedIndex == DI2bl3combo_index) return; // Индекс не поменялся
            if (DI2bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI2bl3_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI2bl3combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI2bl3combo_text)); // Удаление из списка
                    if (showCode) DI2bl3_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI2bl3combo_text, DI2bl3_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI2bl3_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI2bl3_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI2bl3_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI2bl3combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI2bl3combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI2bl3combo_text, DI2bl3_combo); // Добавление к другим DI
                }
            }
            DI2bl3combo_text = DI2bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI2bl3combo_index = DI2bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI3 блока расширения 3</summary>
        private void DI3bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI3bl3_combo.SelectedIndex == DI3bl3combo_index) return; // Индекс не поменялся
            if (DI3bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI3bl3_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI3bl3combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI3bl3combo_text)); // Удаление из списка
                    if (showCode) DI3bl3_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI3bl3combo_text, DI3bl3_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI3bl3_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI3bl3_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI3bl3_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI3bl3combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI3bl3combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI3bl3combo_text, DI3bl3_combo); // Добавление к другим DI
                }
            }
            DI3bl3combo_text = DI3bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI3bl3combo_index = DI3bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI4 блока расширения 3</summary>
        private void DI4bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI4bl3_combo.SelectedIndex == DI4bl3combo_index) return; // Индекс не поменялся
            if (DI4bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI4bl3_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI4bl3combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI4bl3combo_text)); // Удаление из списка
                    if (showCode) DI4bl3_lab.Text = "";
                }
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI4bl3combo_text, DI4bl3_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI4bl3_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI4bl3_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI4bl3_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI4bl3combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI4bl3combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI4bl3combo_text, DI4bl3_combo); // Добавление к другим DI
                }
            }
            DI4bl3combo_text = DI4bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI4bl3combo_index = DI4bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили DI5 блока расширения 3</summary>
        private void DI5bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Di di_find = null;
            if (subDIcondition) return; // Переход из вычета сигналов DI
            if (DI5bl3_combo.SelectedIndex == DI5bl3combo_index) return; // Индекс не поменялся
            if (DI5bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (DI5bl3_combo.Items.Count > 1)  // Больше одного элемента в списке
                {
                    di_find = list_di.Find(x => x.Name == DI5bl3combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI5bl3combo_text)); // Удаление из списка
                    if (showCode) DI5bl3_lab.Text = "";
                } 
                if (di_find != null) // Найден элемент
                {
                    di_find.Dispose(); // Освобождение сигнала для распределенния
                    list_di.Add(di_find); // Добавление с новым значением
                    Ai new_ai = new Ai(di_find.Name, (ushort)(di_find.Code + 1000), "di");
                    list_ai.Add(new_ai);
                    AddToAllCombosAI(new_ai.Name); // Добавление ко всем AI
                }
                if (!initialComboSignals) AddtoCombosDI(DI5bl3combo_text, DI5bl3_combo); // Добавление к другим DI
            }
            else // Выбран сигнал DI
            {
                name = String.Concat(DI5bl3_combo.SelectedItem);
                di_find = list_di.Find(x => x.Name == name);
                list_di.Remove(list_di.Find(x => x.Name == name)); // Удаление из списка DI
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                RemoveFromAllCombosAI(name); // Удаление из всех AI
                if (di_find != null)
                {
                    di_find.Select();
                    list_di.Add(di_find);
                    if (showCode) DI5bl3_lab.Text = di_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosDI(name, DI5bl3_combo); // Удаление из других DI
                    di_find = list_di.Find(x => x.Name == DI5bl3combo_text);
                    list_di.Remove(list_di.Find(x => x.Name == DI5bl3combo_text));
                    if (di_find != null)
                    {
                        di_find.Dispose();
                        list_di.Add(di_find);
                    }
                    AddtoCombosDI(DI5bl3combo_text, DI5bl3_combo); // Добавление к другим DI
                }
            }
            DI5bl3combo_text = DI5bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            DI5bl3combo_index = DI5bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Добавление освободившегося DI к остальным comboBox</summary>
        private void AddtoCombosDI(string name, ComboBox cm)
        {
            Di di_find;
            bool notFound = true; // Элемент в списке не найден
            // Для DI1 comboBox, добавление в остальные слоты для выбора
            if (DI1_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI1_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI2 comboBox, добавление в остальные слоты для выбора
            if (DI2_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI2_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI3 comboBox, добавление в остальные слоты для выбора
            if (DI3_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI3_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI4 comboBox, добавление в остальные слоты для выбора
            if (DI4_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI4_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI4_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI5 comboBox, добавление в остальные слоты для выбора
            if (DI5_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI5_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI5_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI1 блока расширения 1, добавление в остальные слоты для выбора
            if (DI1bl1_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI1bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI1bl1_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI2 блока расширения 1, добавление в остальные слоты для выбора
            if (DI2bl1_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI2bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI2bl1_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI3 блока расширения 1, добавление в остальные слоты для выбора
            if (DI3bl1_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI3bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI3bl1_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI4 блока расширения 1, добавление в остальные слоты для выбора
            if (DI4bl1_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI4bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI4bl1_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI5 блока расширения 1, добавление в остальные слоты для выбора
            if (DI5bl1_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI5bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI5bl1_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI1 блока расширения 2, добавление в остальные слоты для выбора
            if (DI1bl2_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI1bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI1bl2_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI2 блока расширения 2, добавление в остальные слоты для выбора
            if (DI2bl2_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI2bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI2bl2_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI3 блока расширения 2, добавление в остальные слоты для выбора
            if (DI3bl2_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI3bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI3bl2_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI4 блока расширения 2, добавление в остальные слоты для выбора
            if (DI4bl2_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI4bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI4bl2_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI5 блока расширения 2, добавление в остальные слоты для выбора
            if (DI5bl2_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI5bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI5bl2_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI1 блока расширения 3, добавление в остальные слоты для выбора
            if (DI1bl3_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI1bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI1bl3_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI2 блока расширения 3, добавление в остальные слоты для выбора
            if (DI2bl3_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI2bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI2bl3_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI3 блока расширения 3, добавление в остальные слоты для выбора
            if (DI3bl3_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI3bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI3bl3_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI4 блока расширения 3, добавление в остальные слоты для выбора
            if (DI4bl3_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI4bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI4bl3_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
            // Для DI5 блока расширения 3, добавление в остальные слоты для выбора
            if (DI5bl3_combo != cm)
            {
                di_find = list_di.Find(x => x.Name == name);
                if (di_find != null)
                {
                    foreach (var elem in DI5bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) DI5bl3_combo.Items.Add(di_find.Name); notFound = true;
                }
            }
        }

        ///<summary>Удаление DI из других comboBox</summary>
        private void SubFromCombosDI(string name, ComboBox cm)
        {
            if (name != notSelected) // Кроме "Не выбрано"
            {
                if (DI1_combo != cm) DI1_combo.Items.Remove(name); // DI1
                if (DI2_combo != cm) DI2_combo.Items.Remove(name); // DI2
                if (DI3_combo != cm) DI3_combo.Items.Remove(name); // DI3
                if (DI4_combo != cm) DI4_combo.Items.Remove(name); // DI4
                if (DI5_combo != cm) DI5_combo.Items.Remove(name); // DI5
                if (DI1bl1_combo != cm) DI1bl1_combo.Items.Remove(name); // DI1, блок 1
                if (DI2bl1_combo != cm) DI2bl1_combo.Items.Remove(name); // DI2, блок 1
                if (DI3bl1_combo != cm) DI3bl1_combo.Items.Remove(name); // DI3, блок 1
                if (DI4bl1_combo != cm) DI4bl1_combo.Items.Remove(name); // DI4, блок 1
                if (DI5bl1_combo != cm) DI5bl1_combo.Items.Remove(name); // DI5, блок 1
                if (DI1bl2_combo != cm) DI1bl2_combo.Items.Remove(name); // DI1, блок 2
                if (DI2bl2_combo != cm) DI2bl2_combo.Items.Remove(name); // DI2, блок 2
                if (DI3bl2_combo != cm) DI3bl2_combo.Items.Remove(name); // DI3, блок 2
                if (DI4bl2_combo != cm) DI4bl2_combo.Items.Remove(name); // DI4, блок 2
                if (DI5bl2_combo != cm) DI5bl2_combo.Items.Remove(name); // DI5, блок 2
                if (DI1bl3_combo != cm) DI1bl3_combo.Items.Remove(name); // DI1, блок 3
                if (DI2bl3_combo != cm) DI2bl3_combo.Items.Remove(name); // DI2, блок 3
                if (DI3bl3_combo != cm) DI3bl3_combo.Items.Remove(name); // DI3, блок 3
                if (DI4bl3_combo != cm) DI4bl3_combo.Items.Remove(name); // DI4, блок 3
                if (DI5bl3_combo != cm) DI5bl3_combo.Items.Remove(name); // DI5, блок 3
            }
        }

        ///<summary>Добавление нового DI и его назначение под выход</summary>
        private void AddNewDI(ushort code)
        {
            if (DI1_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI1
                DI1_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI1_combo.SelectedIndex = DI1_combo.Items.Count - 1;
                DI1combo_text = DI1_combo.SelectedItem.ToString();
                DI1combo_index = DI1_combo.SelectedIndex;
                if (showCode) DI1_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (DI2_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI2 
                DI2_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI2_combo.SelectedIndex = DI2_combo.Items.Count - 1;
                DI2combo_text = DI2_combo.SelectedItem.ToString();
                DI2combo_index = DI2_combo.SelectedIndex;
                if (showCode) DI2_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (DI3_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI3
                DI3_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI3_combo.SelectedIndex = DI3_combo.Items.Count - 1;
                DI3combo_text = DI3_combo.SelectedItem.ToString();
                DI3combo_index = DI3_combo.SelectedIndex;
                if (showCode) DI3_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (DI4_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI4
                DI4_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI4_combo.SelectedIndex = DI4_combo.Items.Count - 1;
                DI4combo_text = DI4_combo.SelectedItem.ToString();
                DI4combo_index = DI4_combo.SelectedIndex;
                if (showCode) DI4_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (DI5_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI5
                DI5_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI5_combo.SelectedIndex = DI5_combo.Items.Count - 1;
                DI5combo_text = DI5_combo.SelectedItem.ToString();
                DI5combo_index = DI5_combo.SelectedIndex;
                if (showCode) DI5_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (AI4_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI4
                string name = list_di.Find(x => x.Code == code).Name;
                AI4_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI4_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI4_combo.SelectedIndex = AI4_combo.Items.Count - 1;
                AI4combo_text = AI4_combo.SelectedItem.ToString();
                AI4combo_index = AI4_combo.SelectedIndex;
                if (showCode) AI4_lab.Text = (code + 1000).ToString();
            }
            else if (AI5_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI5
                string name = list_di.Find(x => x.Code == code).Name;
                AI5_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI5_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI5_combo.SelectedIndex = AI5_combo.Items.Count - 1;
                AI5combo_text = AI5_combo.SelectedItem.ToString();
                AI5combo_index = AI5_combo.SelectedIndex;
                if (showCode) AI5_lab.Text = (code + 1000).ToString();
            }
            else if (AI6_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI6
                string name = list_di.Find(x => x.Code == code).Name;
                AI6_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI6_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI6_combo.SelectedIndex = AI6_combo.Items.Count - 1;
                AI6combo_text = AI6_combo.SelectedItem.ToString();
                AI6combo_index = AI6_combo.SelectedIndex;
                if (showCode) AI6_lab.Text = (code + 1000).ToString();
            }
            else if (AI1_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI1
                string name = list_di.Find(x => x.Code == code).Name;
                AI1_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI1_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI1_combo.SelectedIndex = AI1_combo.Items.Count - 1;
                AI1combo_text = AI1_combo.SelectedItem.ToString();
                AI1combo_index = AI1_combo.SelectedIndex;
                if (showCode) AI1_lab.Text = (code + 1000).ToString();
            }
            else if (AI2_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI2
                string name = list_di.Find(x => x.Code == code).Name;
                AI2_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI2_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI2_combo.SelectedIndex = AI2_combo.Items.Count - 1;
                AI2combo_text = AI2_combo.SelectedItem.ToString();
                AI2combo_index = AI2_combo.SelectedIndex;
                if (showCode) AI2_lab.Text = (code + 1000).ToString();
            }
            else if (AI3_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI3
                string name = list_di.Find(x => x.Code == code).Name;
                AI3_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI3_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI3_combo.SelectedIndex = AI3_combo.Items.Count - 1;
                AI3combo_text = AI3_combo.SelectedItem.ToString();
                AI3combo_index = AI3_combo.SelectedIndex;
                if (showCode) AI3_lab.Text = (code + 1000).ToString();
            }
            else if (DI1bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI1, блок 1
                DI1bl1_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI1bl1_combo.SelectedIndex = DI1bl1_combo.Items.Count - 1;
                DI1bl1combo_text = DI1bl1_combo.SelectedItem.ToString();
                DI1bl1combo_index = DI1bl1_combo.SelectedIndex;
                if (showCode) DI1bl1_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (DI2bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI2, блок 1
                DI2bl1_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI2bl1_combo.SelectedIndex = DI2bl1_combo.Items.Count - 1;
                DI2bl1combo_text = DI2bl1_combo.SelectedItem.ToString();
                DI2bl1combo_index = DI2bl1_combo.SelectedIndex;
                if (showCode) DI2bl1_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (DI3bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI3, блок 1
                DI3bl1_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI3bl1_combo.SelectedIndex = DI3bl1_combo.Items.Count - 1;
                DI3bl1combo_text = DI3bl1_combo.SelectedItem.ToString();
                DI3bl1combo_index = DI3bl1_combo.SelectedIndex;
                if (showCode) DI3bl1_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (DI4bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI4, блок 1
                DI4bl1_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI4bl1_combo.SelectedIndex = DI4bl1_combo.Items.Count - 1;
                DI4bl1combo_text = DI4bl1_combo.SelectedItem.ToString();
                DI4bl1combo_index = DI4bl1_combo.SelectedIndex;
                if (showCode) DI4bl1_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (DI5bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI5, блок 1
                DI5bl1_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI5bl1_combo.SelectedIndex = DI5bl1_combo.Items.Count - 1;
                DI5bl1combo_text = DI5bl1_combo.SelectedItem.ToString();
                DI5bl1combo_index = DI5bl1_combo.SelectedIndex;
                if (showCode) DI5bl1_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (AI4bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI4, блок 1
                string name = list_di.Find(x => x.Code == code).Name;
                AI4bl1_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI4bl1_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI4bl1_combo.SelectedIndex = AI4bl1_combo.Items.Count - 1;
                AI4bl1combo_text = AI4bl1_combo.SelectedItem.ToString();
                AI4bl1combo_index = AI4bl1_combo.SelectedIndex;
                if (showCode) AI4bl1_lab.Text = (code + 1000).ToString();
            }
            else if (AI5bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI5, блок 1
                string name = list_di.Find(x => x.Code == code).Name;
                AI5bl1_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI5bl1_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI5bl1_combo.SelectedIndex = AI5bl1_combo.Items.Count - 1;
                AI5bl1combo_text = AI5bl1_combo.SelectedItem.ToString();
                AI5bl1combo_index = AI5bl1_combo.SelectedIndex;
                if (showCode) AI5bl1_lab.Text = (code + 1000).ToString();
            }
            else if (AI6bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI6, блок 1
                string name = list_di.Find(x => x.Code == code).Name;
                AI6bl1_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI6bl1_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI6bl1_combo.SelectedIndex = AI6bl1_combo.Items.Count - 1;
                AI6bl1combo_text = AI6bl1_combo.SelectedItem.ToString();
                AI6bl1combo_index = AI6bl1_combo.SelectedIndex;
                if (showCode) AI6bl1_lab.Text = (code + 1000).ToString();
            }
            else if (AI1bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI1, блок 1
                string name = list_di.Find(x => x.Code == code).Name;
                AI1bl1_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI1bl1_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI1bl1_combo.SelectedIndex = AI1bl1_combo.Items.Count - 1;
                AI1bl1combo_text = AI1bl1_combo.SelectedItem.ToString();
                AI1bl1combo_index = AI1bl1_combo.SelectedIndex;
                if (showCode) AI1bl1_lab.Text = (code + 1000).ToString();
            }
            else if (AI2bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI2, блок 1
                string name = list_di.Find(x => x.Code == code).Name;
                AI2bl1_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI2bl1_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI2bl1_combo.SelectedIndex = AI2bl1_combo.Items.Count - 1;
                AI2bl1combo_text = AI2bl1_combo.SelectedItem.ToString();
                AI2bl1combo_index = AI2bl1_combo.SelectedIndex;
                if (showCode) AI2bl1_lab.Text = (code + 1000).ToString();
            }
            else if (AI3bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI3, блок 1
                string name = list_di.Find(x => x.Code == code).Name;
                AI3bl1_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI3bl1_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI3bl1_combo.SelectedIndex = AI3bl1_combo.Items.Count - 1;
                AI3bl1combo_text = AI3bl1_combo.SelectedItem.ToString();
                AI3bl1combo_index = AI3bl1_combo.SelectedIndex;
                if (showCode) AI3bl1_lab.Text = (code + 1000).ToString();
            }
            else if (DI1bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI1, блок 2
                DI1bl2_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI1bl2_combo.SelectedIndex = DI1bl2_combo.Items.Count - 1;
                DI1bl2combo_text = DI1bl2_combo.SelectedItem.ToString();
                DI1bl2combo_index = DI1bl2_combo.SelectedIndex;
                if (showCode) DI1bl2_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (DI2bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI2, блок 2
                DI2bl2_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI2bl2_combo.SelectedIndex = DI2bl2_combo.Items.Count - 1;
                DI2bl2combo_text = DI2bl2_combo.SelectedItem.ToString();
                DI2bl2combo_index = DI2bl2_combo.SelectedIndex;
                if (showCode) DI2bl2_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (DI3bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI3, блок 2
                DI3bl2_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI3bl2_combo.SelectedIndex = DI3bl2_combo.Items.Count - 1;
                DI3bl2combo_text = DI3bl2_combo.SelectedItem.ToString();
                DI3bl2combo_index = DI3bl2_combo.SelectedIndex;
                if (showCode) DI3bl2_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (DI4bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI4, блок 2
                DI4bl2_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI4bl2_combo.SelectedIndex = DI4bl2_combo.Items.Count - 1;
                DI4bl2combo_text = DI4bl2_combo.SelectedItem.ToString();
                DI4bl2combo_index = DI4bl2_combo.SelectedIndex;
                if (showCode) DI4bl2_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (DI5bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI5, блок 2
                DI5bl2_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI5bl2_combo.SelectedIndex = DI5bl2_combo.Items.Count - 1;
                DI5bl2combo_text = DI5bl2_combo.SelectedItem.ToString();
                DI5bl2combo_index = DI5bl2_combo.SelectedIndex;
                if (showCode) DI5bl2_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (AI4bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI4, блок 2
                string name = list_di.Find(x => x.Code == code).Name;
                AI4bl2_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI4bl2_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI4bl2_combo.SelectedIndex = AI4bl2_combo.Items.Count - 1;
                AI4bl2combo_text = AI4bl2_combo.SelectedItem.ToString();
                AI4bl2combo_index = AI4bl2_combo.SelectedIndex;
                if (showCode) AI4bl2_lab.Text = (code + 1000).ToString();
            }
            else if (AI5bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI5, блок 2
                string name = list_di.Find(x => x.Code == code).Name;
                AI5bl2_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI5bl2_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI5bl2_combo.SelectedIndex = AI5bl2_combo.Items.Count - 1;
                AI5bl2combo_text = AI5bl2_combo.SelectedItem.ToString();
                AI5bl2combo_index = AI5bl2_combo.SelectedIndex;
                if (showCode) AI5bl2_lab.Text = (code + 1000).ToString();
            }
            else if (AI6bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI6, блок 2
                string name = list_di.Find(x => x.Code == code).Name;
                AI6bl2_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI6bl2_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI6bl2_combo.SelectedIndex = AI6bl2_combo.Items.Count - 1;
                AI6bl2combo_text = AI6bl2_combo.SelectedItem.ToString();
                AI6bl2combo_index = AI6bl2_combo.SelectedIndex;
                if (showCode) AI6bl2_lab.Text = (code + 1000).ToString();
            }
            else if (AI1bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI1, блок 2
                string name = list_di.Find(x => x.Code == code).Name;
                AI1bl2_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI1bl2_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI1bl2_combo.SelectedIndex = AI1bl2_combo.Items.Count - 1;
                AI1bl2combo_text = AI1bl2_combo.SelectedItem.ToString();
                AI1bl2combo_index = AI1bl2_combo.SelectedIndex;
                if (showCode) AI1bl2_lab.Text = (code + 1000).ToString();
            }
            else if (AI2bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI2, блок 2
                string name = list_di.Find(x => x.Code == code).Name;
                AI2bl2_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI2bl2_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI2bl2_combo.SelectedIndex = AI2bl2_combo.Items.Count - 1;
                AI2bl2combo_text = AI2bl2_combo.SelectedItem.ToString();
                AI2bl2combo_index = AI2bl2_combo.SelectedIndex;
                if (showCode) AI2bl2_lab.Text = (code + 1000).ToString();
            }
            else if (AI3bl2_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI3, блок 2
                string name = list_di.Find(x => x.Code == code).Name;
                AI3bl2_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI3bl2_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI3bl2_combo.SelectedIndex = AI3bl2_combo.Items.Count - 1;
                AI3bl2combo_text = AI3bl2_combo.SelectedItem.ToString();
                AI3bl2combo_index = AI3bl2_combo.SelectedIndex;
                if (showCode) AI3bl2_lab.Text = (code + 1000).ToString();
            }
            else if (DI1bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI1, блок 3
                DI1bl3_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI1bl3_combo.SelectedIndex = DI1bl3_combo.Items.Count - 1;
                DI1bl3combo_text = DI1bl3_combo.SelectedItem.ToString();
                DI1bl3combo_index = DI1bl3_combo.SelectedIndex;
                if (showCode) DI1bl3_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (DI2bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI2, блок 3
                DI2bl3_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI2bl3_combo.SelectedIndex = DI2bl3_combo.Items.Count - 1;
                DI2bl3combo_text = DI2bl3_combo.SelectedItem.ToString();
                DI2bl3combo_index = DI2bl3_combo.SelectedIndex;
                if (showCode) DI2bl3_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (DI3bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI3, блок 3
                DI3bl3_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI3bl3_combo.SelectedIndex = DI3bl3_combo.Items.Count - 1;
                DI3bl3combo_text = DI3bl3_combo.SelectedItem.ToString();
                DI3bl3combo_index = DI3bl3_combo.SelectedIndex;
                if (showCode) DI3bl3_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (DI4bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI4, блок 3
                DI4bl3_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI4bl3_combo.SelectedIndex = DI4bl3_combo.Items.Count - 1;
                DI4bl3combo_text = DI4bl3_combo.SelectedItem.ToString();
                DI4bl3combo_index = DI4bl3_combo.SelectedIndex;
                if (showCode) DI4bl3_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (DI5bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // DI5, блок 3
                DI5bl3_combo.Items.Add(list_di.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                DI5bl3_combo.SelectedIndex = DI5bl3_combo.Items.Count - 1;
                DI5bl3combo_text = DI5bl3_combo.SelectedItem.ToString();
                DI5bl3combo_index = DI5bl3_combo.SelectedIndex;
                if (showCode) DI5bl3_lab.Text = code.ToString();
                list_di.Find(x => x.Code == code).Select();
            }
            else if (AI4bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI4, блок 3
                string name = list_di.Find(x => x.Code == code).Name;
                AI4bl3_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI4bl3_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI4bl3_combo.SelectedIndex = AI4bl3_combo.Items.Count - 1;
                AI4bl3combo_text = AI4bl3_combo.SelectedItem.ToString();
                AI4bl3combo_index = AI4bl3_combo.SelectedIndex;
                if (showCode) AI4bl3_lab.Text = (code + 1000).ToString();
            }
            else if (AI5bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI5, блок 3
                string name = list_di.Find(x => x.Code == code).Name;
                AI5bl3_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI5bl3_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI5bl3_combo.SelectedIndex = AI5bl3_combo.Items.Count - 1;
                AI5bl3combo_text = AI5bl3_combo.SelectedItem.ToString();
                AI5bl3combo_index = AI5bl3_combo.SelectedIndex;
                if (showCode) AI5bl3_lab.Text = (code + 1000).ToString();
            }
            else if (AI6bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI6, блок 3
                string name = list_di.Find(x => x.Code == code).Name;
                AI6bl3_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI6bl3_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI6bl3_combo.SelectedIndex = AI6bl3_combo.Items.Count - 1;
                AI6bl3combo_text = AI6bl3_combo.SelectedItem.ToString();
                AI6bl3combo_index = AI6bl3_combo.SelectedIndex;
                if (showCode) AI6bl3_lab.Text = (code + 1000).ToString();
            }
            else if (AI1bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI1, блок 3
                string name = list_di.Find(x => x.Code == code).Name;
                AI1bl3_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI1bl3_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI1bl3_combo.SelectedIndex = AI1bl3_combo.Items.Count - 1;
                AI1bl3combo_text = AI1bl3_combo.SelectedItem.ToString();
                AI1bl3combo_index = AI1bl3_combo.SelectedIndex;
                if (showCode) AI1bl3_lab.Text = (code + 1000).ToString();
            }
            else if (AI2bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI2, блок 3
                string name = list_di.Find(x => x.Code == code).Name;
                AI2bl3_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI2bl3_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI2bl3_combo.SelectedIndex = AI2bl3_combo.Items.Count - 1;
                AI2bl3combo_text = AI2bl3_combo.SelectedItem.ToString();
                AI2bl3combo_index = AI2bl3_combo.SelectedIndex;
                if (showCode) AI2bl3_lab.Text = (code + 1000).ToString();
            }
            else if (AI3bl3_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI3, блок 3
                string name = list_di.Find(x => x.Code == code).Name;
                AI3bl3_combo.Items.Add(name);
                RemoveFromAllCombosDI(name);
                SensorAIType(AI3bl3_typeCombo, 2);
                if (!list_ai.Contains(new Ai(name, (ushort)(code + 1000), "di")))
                    list_ai.Add(new Ai(name, (ushort)(code + 1000), "di"));
                // Выбор последнего добавленного элемента
                AI3bl3_combo.SelectedIndex = AI3bl3_combo.Items.Count - 1;
                AI3bl3combo_text = AI3bl3_combo.SelectedItem.ToString();
                AI3bl3combo_index = AI3bl3_combo.SelectedIndex;
                if (showCode) AI3bl3_lab.Text = (code + 1000).ToString();
            }
        }

        ///<summary>Удаление DI из всех comboBox</summary>
        private void SubFromCombosDI(ushort code)
        {
            Di findDi, findDi_2;
            Ai findAi;
            string name = "";
            findDi = list_di.Find(x => x.Code == code);
            if (findDi != null) name = findDi.Name;
            else return;
            subDIcondition = true; // Признак удаления DI, не работает событие indexChanged
            subAIcondition = true; // Признак удаления AI, не работает событие indexChanged
            for (int i = 0; i < DI1_combo.Items.Count; i++) // DI1
                if (DI1_combo.Items[i].ToString() == name)
                {
                    DI1_combo.Items.Remove(name);
                    if (DI1_combo.Items.Count > 1)
                    {
                        DI1_combo.SelectedIndex = DI1_combo.Items.Count - 1;
                        if (DI1_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI1_combo.SelectedItem.ToString(), DI1_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI1_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI1_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI1_combo.SelectedItem = notSelected;
                        DI1_lab.Text = "";
                    }
                    DI1combo_text = DI1_combo.SelectedItem.ToString();
                    DI1combo_index = DI1_combo.SelectedIndex;
                }
            for (int i = 0; i < DI2_combo.Items.Count; i++) // DI2
                if (DI2_combo.Items[i].ToString() == name)
                {
                    DI2_combo.Items.Remove(name);
                    if (DI2_combo.Items.Count > 1)
                    {
                        DI2_combo.SelectedIndex = DI2_combo.Items.Count - 1;
                        if (DI2_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI2_combo.SelectedItem.ToString(), DI2_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI2_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI2_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI2_combo.SelectedItem = notSelected;
                        DI2_lab.Text = "";
                    }
                    DI2combo_text = DI2_combo.SelectedItem.ToString();
                    DI2combo_index = DI2_combo.SelectedIndex;
                }
            for (int i = 0; i < DI3_combo.Items.Count; i++) // DI3
                if (DI3_combo.Items[i].ToString() == name)
                {
                    DI3_combo.Items.Remove(name);
                    if (DI3_combo.Items.Count > 1)
                    {
                        DI3_combo.SelectedIndex = DI3_combo.Items.Count - 1;
                        if (DI3_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI3_combo.SelectedItem.ToString(), DI3_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI3_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI3_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI3_combo.SelectedItem = notSelected;
                        DI3_lab.Text = "";
                    }
                    DI3combo_text = DI3_combo.SelectedItem.ToString();
                    DI3combo_index = DI3_combo.SelectedIndex;
                }
            for (int i = 0; i < DI4_combo.Items.Count; i++) // DI4
                if (DI4_combo.Items[i].ToString() == name)
                {
                    DI4_combo.Items.Remove(name);
                    if (DI4_combo.Items.Count > 1)
                    {
                        DI4_combo.SelectedIndex = DI4_combo.Items.Count - 1;
                        if (DI4_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI4_combo.SelectedItem.ToString(), DI4_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI4_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI4_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI4_combo.SelectedItem = notSelected;
                        DI4_lab.Text = "";
                    }
                    DI4combo_text = DI4_combo.SelectedItem.ToString();
                    DI4combo_index = DI4_combo.SelectedIndex;
                }
            for (int i = 0; i < DI5_combo.Items.Count; i++) // DI5
                if (DI5_combo.Items[i].ToString() == name)
                {
                    DI5_combo.Items.Remove(name);
                    if (DI5_combo.Items.Count > 1)
                    {
                        DI5_combo.SelectedIndex = DI5_combo.Items.Count - 1;
                        if (DI5_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI5_combo.SelectedItem.ToString(), DI5_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI5_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI5_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI5_combo.SelectedItem = notSelected;
                        DI5_lab.Text = "";
                    }
                    DI5combo_text = DI5_combo.SelectedItem.ToString();
                    DI5combo_index = DI5_combo.SelectedIndex;
                }
            for (int i = 0; i < DI1bl1_combo.Items.Count; i++) // DI1 блок 1
                if (DI1bl1_combo.Items[i].ToString() == name)
                {
                    DI1bl1_combo.Items.Remove(name);
                    if (DI1bl1_combo.Items.Count > 1)
                    {
                        DI1bl1_combo.SelectedIndex = DI1bl1_combo.Items.Count - 1;
                        if (DI1bl1_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI1bl1_combo.SelectedItem.ToString(), DI1bl1_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI1bl1_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI1bl1_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI1bl1_combo.SelectedItem = notSelected;
                        DI1bl1_lab.Text = "";
                    }
                    DI1bl1combo_text = DI1bl1_combo.SelectedItem.ToString();
                    DI1bl1combo_index = DI1bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < DI2bl1_combo.Items.Count; i++) // DI2 блок 1
                if (DI2bl1_combo.Items[i].ToString() == name)
                {
                    DI2bl1_combo.Items.Remove(name);
                    if (DI2bl1_combo.Items.Count > 1)
                    {
                        DI2bl1_combo.SelectedIndex = DI2bl1_combo.Items.Count - 1;
                        if (DI2bl1_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI2bl1_combo.SelectedItem.ToString(), DI2bl1_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI2bl1_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI2bl1_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI2bl1_combo.SelectedItem = notSelected;
                        DI2bl1_lab.Text = "";
                    }
                    DI2bl1combo_text = DI2bl1_combo.SelectedItem.ToString();
                    DI2bl1combo_index = DI2bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < DI3bl1_combo.Items.Count; i++) // DI3 блок 1
                if (DI3bl1_combo.Items[i].ToString() == name)
                {
                    DI3bl1_combo.Items.Remove(name);
                    if (DI3bl1_combo.Items.Count > 1)
                    {
                        DI3bl1_combo.SelectedIndex = DI3bl1_combo.Items.Count - 1;
                        if (DI3bl1_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI3bl1_combo.SelectedItem.ToString(), DI3bl1_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI3bl1_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI3bl1_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI3bl1_combo.SelectedItem = notSelected;
                        DI3bl1_lab.Text = "";
                    }
                    DI3bl1combo_text = DI3bl1_combo.SelectedItem.ToString();
                    DI3bl1combo_index = DI3bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < DI4bl1_combo.Items.Count; i++) // DI4 блок 1
                if (DI4bl1_combo.Items[i].ToString() == name)
                {
                    DI4bl1_combo.Items.Remove(name);
                    if (DI4bl1_combo.Items.Count > 1)
                    {
                        DI4bl1_combo.SelectedIndex = DI4bl1_combo.Items.Count - 1;
                        if (DI4bl1_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI4bl1_combo.SelectedItem.ToString(), DI4bl1_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI4bl1_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI4bl1_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI4bl1_combo.SelectedItem = notSelected;
                        DI4bl1_lab.Text = "";
                    }
                    DI4bl1combo_text = DI4bl1_combo.SelectedItem.ToString();
                    DI4bl1combo_index = DI4bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < DI5bl1_combo.Items.Count; i++) // DI5 блок 1
                if (DI5bl1_combo.Items[i].ToString() == name)
                {
                    DI5bl1_combo.Items.Remove(name);
                    if (DI5bl1_combo.Items.Count > 1)
                    {
                        DI5bl1_combo.SelectedIndex = DI5bl1_combo.Items.Count - 1;
                        if (DI5bl1_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI5bl1_combo.SelectedItem.ToString(), DI5bl1_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI5bl1_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI5bl1_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI5bl1_combo.SelectedItem = notSelected;
                        DI5bl1_lab.Text = "";
                    }
                    DI5bl1combo_text = DI5bl1_combo.SelectedItem.ToString();
                    DI5bl1combo_index = DI5bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < DI1bl2_combo.Items.Count; i++) // DI1 блок 2
                if (DI1bl2_combo.Items[i].ToString() == name)
                {
                    DI1bl2_combo.Items.Remove(name);
                    if (DI1bl2_combo.Items.Count > 1)
                    {
                        DI1bl2_combo.SelectedIndex = DI1bl2_combo.Items.Count - 1;
                        if (DI1bl2_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI1bl2_combo.SelectedItem.ToString(), DI1bl2_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI1bl2_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI1bl2_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI1bl2_combo.SelectedItem = notSelected;
                        DI1bl2_lab.Text = "";
                    }
                    DI1bl2combo_text = DI1bl2_combo.SelectedItem.ToString();
                    DI1bl2combo_index = DI1bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < DI2bl2_combo.Items.Count; i++) // DI2 блок 2
                if (DI2bl2_combo.Items[i].ToString() == name)
                {
                    DI2bl2_combo.Items.Remove(name);
                    if (DI2bl2_combo.Items.Count > 1)
                    {
                        DI2bl2_combo.SelectedIndex = DI2bl2_combo.Items.Count - 1;
                        if (DI2bl2_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI2bl2_combo.SelectedItem.ToString(), DI2bl2_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI2bl2_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI2bl2_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI2bl2_combo.SelectedItem = notSelected;
                        DI2bl2_lab.Text = "";
                    }
                    DI2bl2combo_text = DI2bl2_combo.SelectedItem.ToString();
                    DI2bl2combo_index = DI2bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < DI3bl2_combo.Items.Count; i++) // DI3 блок 2
                if (DI3bl2_combo.Items[i].ToString() == name)
                {
                    DI3bl2_combo.Items.Remove(name);
                    if (DI3bl2_combo.Items.Count > 1)
                    {
                        DI3bl2_combo.SelectedIndex = DI3bl2_combo.Items.Count - 1;
                        if (DI3bl2_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI3bl2_combo.SelectedItem.ToString(), DI3bl2_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI3bl2_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI3bl2_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI3bl2_combo.SelectedItem = notSelected;
                        DI3bl2_lab.Text = "";
                    }
                    DI3bl2combo_text = DI3bl2_combo.SelectedItem.ToString();
                    DI3bl2combo_index = DI3bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < DI4bl2_combo.Items.Count; i++) // DI4 блок 2
                if (DI4bl2_combo.Items[i].ToString() == name)
                {
                    DI4bl2_combo.Items.Remove(name);
                    if (DI4bl2_combo.Items.Count > 1)
                    {
                        DI4bl2_combo.SelectedIndex = DI4bl2_combo.Items.Count - 1;
                        if (DI4bl2_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI4bl2_combo.SelectedItem.ToString(), DI4bl2_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI4bl2_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI4bl2_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI4bl2_combo.SelectedItem = notSelected;
                        DI4bl2_lab.Text = "";
                    }
                    DI4bl2combo_text = DI4bl2_combo.SelectedItem.ToString();
                    DI4bl2combo_index = DI4bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < DI5bl2_combo.Items.Count; i++) // DI5 блок 2
                if (DI5bl2_combo.Items[i].ToString() == name)
                {
                    DI5bl2_combo.Items.Remove(name);
                    if (DI5bl2_combo.Items.Count > 1)
                    {
                        DI5bl2_combo.SelectedIndex = DI5bl2_combo.Items.Count - 1;
                        if (DI5bl2_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI5bl2_combo.SelectedItem.ToString(), DI5bl2_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI5bl2_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI5bl2_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI5bl2_combo.SelectedItem = notSelected;
                        DI5bl2_lab.Text = "";
                    }
                    DI5bl2combo_text = DI5bl2_combo.SelectedItem.ToString();
                    DI5bl2combo_index = DI5bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < DI1bl3_combo.Items.Count; i++) // DI1 блок 3
                if (DI1bl3_combo.Items[i].ToString() == name)
                {
                    DI1bl3_combo.Items.Remove(name);
                    if (DI1bl3_combo.Items.Count > 1)
                    {
                        DI1bl3_combo.SelectedIndex = DI1bl3_combo.Items.Count - 1;
                        if (DI1bl3_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI1bl3_combo.SelectedItem.ToString(), DI1bl3_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI1bl3_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI1bl3_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI1bl3_combo.SelectedItem = notSelected;
                        DI1bl3_lab.Text = "";
                    }
                    DI1bl3combo_text = DI1bl3_combo.SelectedItem.ToString();
                    DI1bl3combo_index = DI1bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < DI2bl3_combo.Items.Count; i++) // DI2 блок 3
                if (DI2bl3_combo.Items[i].ToString() == name)
                {
                    DI2bl3_combo.Items.Remove(name);
                    if (DI2bl3_combo.Items.Count > 1)
                    {
                        DI2bl3_combo.SelectedIndex = DI2bl3_combo.Items.Count - 1;
                        if (DI2bl3_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI2bl3_combo.SelectedItem.ToString(), DI2bl3_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI2bl3_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI2bl3_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI2bl3_combo.SelectedItem = notSelected;
                        DI2bl3_lab.Text = "";
                    }
                    DI2bl3combo_text = DI2bl3_combo.SelectedItem.ToString();
                    DI2bl3combo_index = DI2bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < DI3bl3_combo.Items.Count; i++) // DI3 блок 3
                if (DI3bl3_combo.Items[i].ToString() == name)
                {
                    DI3bl3_combo.Items.Remove(name);
                    if (DI3bl3_combo.Items.Count > 1)
                    {
                        DI3bl3_combo.SelectedIndex = DI3bl3_combo.Items.Count - 1;
                        if (DI3bl3_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI3bl3_combo.SelectedItem.ToString(), DI3bl3_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI3bl3_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI3bl3_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI3bl3_combo.SelectedItem = notSelected;
                        DI3bl3_lab.Text = "";
                    }
                    DI3bl3combo_text = DI3bl3_combo.SelectedItem.ToString();
                    DI3bl3combo_index = DI3bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < DI4bl3_combo.Items.Count; i++) // DI4 блок 3
                if (DI4bl3_combo.Items[i].ToString() == name)
                {
                    DI4bl3_combo.Items.Remove(name);
                    if (DI4bl3_combo.Items.Count > 1)
                    {
                        DI4bl3_combo.SelectedIndex = DI4bl3_combo.Items.Count - 1;
                        if (DI4bl3_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI4bl3_combo.SelectedItem.ToString(), DI4bl3_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI4bl3_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI4bl3_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI4bl3_combo.SelectedItem = notSelected;
                        DI4bl3_lab.Text = "";
                    }
                    DI4bl3combo_text = DI4bl3_combo.SelectedItem.ToString();
                    DI4bl3combo_index = DI4bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < DI5bl3_combo.Items.Count; i++) // DI5 блок 3
                if (DI5bl3_combo.Items[i].ToString() == name)
                {
                    DI5bl3_combo.Items.Remove(name);
                    if (DI5bl3_combo.Items.Count > 1)
                    {
                        DI5bl3_combo.SelectedIndex = DI5bl3_combo.Items.Count - 1;
                        if (DI5bl3_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosDI(DI5bl3_combo.SelectedItem.ToString(), DI5bl3_combo);
                            findDi_2 = list_di.Find(x => x.Name == DI5bl3_combo.SelectedItem.ToString());
                            if (findDi_2 != null)
                            {
                                list_di.Remove(findDi_2);
                                //findDi_2.Select();
                                //list_di.Add(findDi_2);
                                if (showCode) DI5bl3_lab.Text = findDi_2.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        DI5bl3_combo.SelectedItem = notSelected;
                        DI5bl3_lab.Text = "";
                    }
                    DI5bl3combo_text = DI5bl3_combo.SelectedItem.ToString();
                    DI5bl3combo_index = DI5bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < AI1_combo.Items.Count; i++) // AI1
                if (AI1_combo.Items[i].ToString() == name)
                {
                    AI1_combo.Items.Remove(name);
                    if (AI1_combo.Items.Count > 1)
                    {
                        AI1_combo.SelectedIndex = AI1_combo.Items.Count - 1;
                        if (AI1_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI1_combo.SelectedItem.ToString(), AI1_combo);
                            findAi = list_ai.Find(x => x.Name == AI1_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI1_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI1_combo.SelectedItem = notSelected;
                        AI1_lab.Text = "";
                    }
                    AI1combo_text = AI1_combo.SelectedItem.ToString();
                    AI1combo_index = AI1_combo.SelectedIndex;
                }
            for (int i = 0; i < AI2_combo.Items.Count; i++) // AI2
                if (AI2_combo.Items[i].ToString() == name)
                {
                    AI2_combo.Items.Remove(name);
                    if (AI2_combo.Items.Count > 1)
                    {
                        AI2_combo.SelectedIndex = AI2_combo.Items.Count - 1;
                        if (AI2_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI2_combo.SelectedItem.ToString(), AI2_combo);
                            findAi = list_ai.Find(x => x.Name == AI2_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI2_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI2_combo.SelectedItem = notSelected;
                        AI2_lab.Text = "";
                    }
                    AI2combo_text = AI2_combo.SelectedItem.ToString();
                    AI2combo_index = AI2_combo.SelectedIndex;
                }
            for (int i = 0; i < AI3_combo.Items.Count; i++) // AI3
                if (AI3_combo.Items[i].ToString() == name)
                {
                    AI3_combo.Items.Remove(name);
                    if (AI3_combo.Items.Count > 1)
                    {
                        AI3_combo.SelectedIndex = AI3_combo.Items.Count - 1;
                        if (AI3_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI3_combo.SelectedItem.ToString(), AI3_combo);
                            findAi = list_ai.Find(x => x.Name == AI3_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI3_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI3_combo.SelectedItem = notSelected;
                        AI3_lab.Text = "";
                    }
                    AI3combo_text = AI3_combo.SelectedItem.ToString();
                    AI3combo_index = AI3_combo.SelectedIndex;
                }
            for (int i = 0; i < AI4_combo.Items.Count; i++) // AI4
                if (AI4_combo.Items[i].ToString() == name)
                {
                    AI4_combo.Items.Remove(name);
                    if (AI4_combo.Items.Count > 1)
                    {
                        AI4_combo.SelectedIndex = AI4_combo.Items.Count - 1;
                        if (AI4_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI4_combo.SelectedItem.ToString(), AI4_combo);
                            findAi = list_ai.Find(x => x.Name == AI4_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI4_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI4_combo.SelectedItem = notSelected;
                        AI4_lab.Text = "";
                    }
                    AI4combo_text = AI4_combo.SelectedItem.ToString();
                    AI4combo_index = AI4_combo.SelectedIndex;
                }
            for (int i = 0; i < AI5_combo.Items.Count; i++) // AI5
                if (AI5_combo.Items[i].ToString() == name)
                {
                    AI5_combo.Items.Remove(name);
                    if (AI5_combo.Items.Count > 1)
                    {
                        AI5_combo.SelectedIndex = AI5_combo.Items.Count - 1;
                        if (AI5_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI5_combo.SelectedItem.ToString(), AI5_combo);
                            findAi = list_ai.Find(x => x.Name == AI5_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI5_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI5_combo.SelectedItem = notSelected;
                        AI5_lab.Text = "";
                    }
                    AI5combo_text = AI5_combo.SelectedItem.ToString();
                    AI5combo_index = AI5_combo.SelectedIndex;
                }
            for (int i = 0; i < AI6_combo.Items.Count; i++) // AI6
                if (AI6_combo.Items[i].ToString() == name)
                {
                    AI6_combo.Items.Remove(name);
                    if (AI6_combo.Items.Count > 1)
                    {
                        AI6_combo.SelectedIndex = AI6_combo.Items.Count - 1;
                        if (AI6_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI6_combo.SelectedItem.ToString(), AI6_combo);
                            findAi = list_ai.Find(x => x.Name == AI6_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI6_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI6_combo.SelectedItem = notSelected;
                        AI6_lab.Text = "";
                    }
                    AI6combo_text = AI6_combo.SelectedItem.ToString();
                    AI6combo_index = AI6_combo.SelectedIndex;
                }
            for (int i = 0; i < AI1bl1_combo.Items.Count; i++) // AI1, блок 1
                if (AI1bl1_combo.Items[i].ToString() == name)
                {
                    AI1bl1_combo.Items.Remove(name);
                    if (AI1bl1_combo.Items.Count > 1)
                    {
                        AI1bl1_combo.SelectedIndex = AI1bl1_combo.Items.Count - 1;
                        if (AI1bl1_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI1bl1_combo.SelectedItem.ToString(), AI1bl1_combo);
                            findAi = list_ai.Find(x => x.Name == AI1bl1_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI1bl1_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI1bl1_combo.SelectedItem = notSelected;
                        AI1bl1_lab.Text = "";
                    }
                    AI1bl1combo_text = AI1bl1_combo.SelectedItem.ToString();
                    AI1bl1combo_index = AI1bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < AI2bl1_combo.Items.Count; i++) // AI2, блок 1
                if (AI2bl1_combo.Items[i].ToString() == name)
                {
                    AI2bl1_combo.Items.Remove(name);
                    if (AI2bl1_combo.Items.Count > 1)
                    {
                        AI2bl1_combo.SelectedIndex = AI2bl1_combo.Items.Count - 1;
                        if (AI2bl1_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI2bl1_combo.SelectedItem.ToString(), AI2bl1_combo);
                            findAi = list_ai.Find(x => x.Name == AI2bl1_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI2bl1_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI2bl1_combo.SelectedItem = notSelected;
                        AI2bl1_lab.Text = "";
                    }
                    AI2bl1combo_text = AI2bl1_combo.SelectedItem.ToString();
                    AI2bl1combo_index = AI2bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < AI3bl1_combo.Items.Count; i++) // AI3, блок 1
                if (AI3bl1_combo.Items[i].ToString() == name)
                {
                    AI3bl1_combo.Items.Remove(name);
                    if (AI3bl1_combo.Items.Count > 1)
                    {
                        AI3bl1_combo.SelectedIndex = AI3bl1_combo.Items.Count - 1;
                        if (AI3bl1_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI3bl1_combo.SelectedItem.ToString(), AI3bl1_combo);
                            findAi = list_ai.Find(x => x.Name == AI3bl1_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI3bl1_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI3bl1_combo.SelectedItem = notSelected;
                        AI3bl1_lab.Text = "";
                    }
                    AI3bl1combo_text = AI3bl1_combo.SelectedItem.ToString();
                    AI3bl1combo_index = AI3bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < AI4bl1_combo.Items.Count; i++) // AI4, блок 1
                if (AI4bl1_combo.Items[i].ToString() == name)
                {
                    AI4bl1_combo.Items.Remove(name);
                    if (AI4bl1_combo.Items.Count > 1)
                    {
                        AI4bl1_combo.SelectedIndex = AI4bl1_combo.Items.Count - 1;
                        if (AI4bl1_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI4bl1_combo.SelectedItem.ToString(), AI4bl1_combo);
                            findAi = list_ai.Find(x => x.Name == AI4bl1_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI4bl1_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI4bl1_combo.SelectedItem = notSelected;
                        AI4bl1_lab.Text = "";
                    }
                    AI4bl1combo_text = AI4bl1_combo.SelectedItem.ToString();
                    AI4bl1combo_index = AI4bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < AI5bl1_combo.Items.Count; i++) // AI5, блок 1
                if (AI5bl1_combo.Items[i].ToString() == name)
                {
                    AI5bl1_combo.Items.Remove(name);
                    if (AI5bl1_combo.Items.Count > 1)
                    {
                        AI5bl1_combo.SelectedIndex = AI5bl1_combo.Items.Count - 1;
                        if (AI5bl1_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI5bl1_combo.SelectedItem.ToString(), AI5bl1_combo);
                            findAi = list_ai.Find(x => x.Name == AI5bl1_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI5bl1_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI5bl1_combo.SelectedItem = notSelected;
                        AI5bl1_lab.Text = "";
                    }
                    AI5bl1combo_text = AI5bl1_combo.SelectedItem.ToString();
                    AI5bl1combo_index = AI5bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < AI6bl1_combo.Items.Count; i++) // AI6, блок 1
                if (AI6bl1_combo.Items[i].ToString() == name)
                {
                    AI6bl1_combo.Items.Remove(name);
                    if (AI6bl1_combo.Items.Count > 1)
                    {
                        AI6bl1_combo.SelectedIndex = AI6bl1_combo.Items.Count - 1;
                        if (AI6bl1_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI6bl1_combo.SelectedItem.ToString(), AI6bl1_combo);
                            findAi = list_ai.Find(x => x.Name == AI6bl1_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI6bl1_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI6bl1_combo.SelectedItem = notSelected;
                        AI6bl1_lab.Text = "";
                    }
                    AI6bl1combo_text = AI6bl1_combo.SelectedItem.ToString();
                    AI6bl1combo_index = AI6bl1_combo.SelectedIndex;
                }
            for (int i = 0; i < AI1bl2_combo.Items.Count; i++) // AI1, блок 2
                if (AI1bl2_combo.Items[i].ToString() == name)
                {
                    AI1bl2_combo.Items.Remove(name);
                    if (AI1bl2_combo.Items.Count > 1)
                    {
                        AI1bl2_combo.SelectedIndex = AI1bl2_combo.Items.Count - 1;
                        if (AI1bl2_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI1bl2_combo.SelectedItem.ToString(), AI1bl2_combo);
                            findAi = list_ai.Find(x => x.Name == AI1bl2_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI1bl2_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI1bl2_combo.SelectedItem = notSelected;
                        AI1bl2_lab.Text = "";
                    }
                    AI1bl2combo_text = AI1bl2_combo.SelectedItem.ToString();
                    AI1bl2combo_index = AI1bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < AI2bl2_combo.Items.Count; i++) // AI2, блок 2
                if (AI2bl2_combo.Items[i].ToString() == name)
                {
                    AI2bl2_combo.Items.Remove(name);
                    if (AI2bl2_combo.Items.Count > 1)
                    {
                        AI2bl2_combo.SelectedIndex = AI2bl2_combo.Items.Count - 1;
                        if (AI2bl2_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI2bl2_combo.SelectedItem.ToString(), AI2bl2_combo);
                            findAi = list_ai.Find(x => x.Name == AI2bl2_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI2bl2_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI2bl2_combo.SelectedItem = notSelected;
                        AI2bl2_lab.Text = "";
                    }
                    AI2bl2combo_text = AI2bl2_combo.SelectedItem.ToString();
                    AI2bl2combo_index = AI2bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < AI3bl2_combo.Items.Count; i++) // AI3, блок 2
                if (AI3bl2_combo.Items[i].ToString() == name)
                {
                    AI3bl2_combo.Items.Remove(name);
                    if (AI3bl2_combo.Items.Count > 1)
                    {
                        AI3bl2_combo.SelectedIndex = AI3bl2_combo.Items.Count - 1;
                        if (AI3bl2_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI3bl2_combo.SelectedItem.ToString(), AI3bl2_combo);
                            findAi = list_ai.Find(x => x.Name == AI3bl2_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI3bl2_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI3bl2_combo.SelectedItem = notSelected;
                        AI3bl2_lab.Text = "";
                    }
                    AI3bl2combo_text = AI3bl2_combo.SelectedItem.ToString();
                    AI3bl2combo_index = AI3bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < AI4bl2_combo.Items.Count; i++) // AI4, блок 2
                if (AI4bl2_combo.Items[i].ToString() == name)
                {
                    AI4bl2_combo.Items.Remove(name);
                    if (AI4bl2_combo.Items.Count > 1)
                    {
                        AI4bl2_combo.SelectedIndex = AI4bl2_combo.Items.Count - 1;
                        if (AI4bl2_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI4bl2_combo.SelectedItem.ToString(), AI4bl2_combo);
                            findAi = list_ai.Find(x => x.Name == AI4bl2_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI4bl2_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI4bl2_combo.SelectedItem = notSelected;
                        AI4bl2_lab.Text = "";
                    }
                    AI4bl2combo_text = AI4bl2_combo.SelectedItem.ToString();
                    AI4bl2combo_index = AI4bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < AI5bl2_combo.Items.Count; i++) // AI5, блок 2
                if (AI5bl2_combo.Items[i].ToString() == name)
                {
                    AI5bl2_combo.Items.Remove(name);
                    if (AI5bl2_combo.Items.Count > 1)
                    {
                        AI5bl2_combo.SelectedIndex = AI5bl2_combo.Items.Count - 1;
                        if (AI5bl2_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI5bl2_combo.SelectedItem.ToString(), AI5bl2_combo);
                            findAi = list_ai.Find(x => x.Name == AI5bl2_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI5bl2_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI5bl2_combo.SelectedItem = notSelected;
                        AI5bl2_lab.Text = "";
                    }
                    AI5bl2combo_text = AI5bl2_combo.SelectedItem.ToString();
                    AI5bl2combo_index = AI5bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < AI6bl2_combo.Items.Count; i++) // AI6, блок 2
                if (AI6bl2_combo.Items[i].ToString() == name)
                {
                    AI6bl2_combo.Items.Remove(name);
                    if (AI6bl2_combo.Items.Count > 1)
                    {
                        AI6bl2_combo.SelectedIndex = AI6bl2_combo.Items.Count - 1;
                        if (AI6bl2_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI6bl2_combo.SelectedItem.ToString(), AI6bl2_combo);
                            findAi = list_ai.Find(x => x.Name == AI6bl2_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI6bl2_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI6bl2_combo.SelectedItem = notSelected;
                        AI6bl2_lab.Text = "";
                    }
                    AI6bl2combo_text = AI6bl2_combo.SelectedItem.ToString();
                    AI6bl2combo_index = AI6bl2_combo.SelectedIndex;
                }
            for (int i = 0; i < AI1bl3_combo.Items.Count; i++) // AI1, блок 3
                if (AI1bl3_combo.Items[i].ToString() == name)
                {
                    AI1bl3_combo.Items.Remove(name);
                    if (AI1bl3_combo.Items.Count > 1)
                    {
                        AI1bl3_combo.SelectedIndex = AI1bl3_combo.Items.Count - 1;
                        if (AI1bl3_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI1bl3_combo.SelectedItem.ToString(), AI1bl3_combo);
                            findAi = list_ai.Find(x => x.Name == AI1bl3_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI1bl3_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI1bl3_combo.SelectedItem = notSelected;
                        AI1bl3_lab.Text = "";
                    }
                    AI1bl3combo_text = AI1bl3_combo.SelectedItem.ToString();
                    AI1bl3combo_index = AI1bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < AI2bl3_combo.Items.Count; i++) // AI2, блок 3
                if (AI2bl3_combo.Items[i].ToString() == name)
                {
                    AI2bl3_combo.Items.Remove(name);
                    if (AI2bl3_combo.Items.Count > 1)
                    {
                        AI2bl3_combo.SelectedIndex = AI2bl3_combo.Items.Count - 1;
                        if (AI2bl3_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI2bl3_combo.SelectedItem.ToString(), AI2bl3_combo);
                            findAi = list_ai.Find(x => x.Name == AI2bl3_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI2bl3_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI2bl3_combo.SelectedItem = notSelected;
                        AI2bl3_lab.Text = "";
                    }
                    AI2bl3combo_text = AI2bl3_combo.SelectedItem.ToString();
                    AI2bl3combo_index = AI2bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < AI3bl3_combo.Items.Count; i++) // AI3, блок 3
                if (AI3bl3_combo.Items[i].ToString() == name)
                {
                    AI3bl3_combo.Items.Remove(name);
                    if (AI3bl3_combo.Items.Count > 1)
                    {
                        AI3bl3_combo.SelectedIndex = AI3bl3_combo.Items.Count - 1;
                        if (AI3bl3_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI3bl3_combo.SelectedItem.ToString(), AI3bl3_combo);
                            findAi = list_ai.Find(x => x.Name == AI3bl3_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI3bl3_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI3bl3_combo.SelectedItem = notSelected;
                        AI3bl3_lab.Text = "";
                    }
                    AI3bl3combo_text = AI3bl3_combo.SelectedItem.ToString();
                    AI3bl3combo_index = AI3bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < AI4bl3_combo.Items.Count; i++) // AI4, блок 3
                if (AI4bl3_combo.Items[i].ToString() == name)
                {
                    AI4bl3_combo.Items.Remove(name);
                    if (AI4bl3_combo.Items.Count > 1)
                    {
                        AI4bl3_combo.SelectedIndex = AI4bl3_combo.Items.Count - 1;
                        if (AI4bl3_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI4bl3_combo.SelectedItem.ToString(), AI4bl3_combo);
                            findAi = list_ai.Find(x => x.Name == AI4bl3_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI4bl3_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI4bl3_combo.SelectedItem = notSelected;
                        AI4bl3_lab.Text = "";
                    }
                    AI4bl3combo_text = AI4bl3_combo.SelectedItem.ToString();
                    AI4bl3combo_index = AI4bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < AI5bl3_combo.Items.Count; i++) // AI5, блок 3
                if (AI5bl3_combo.Items[i].ToString() == name)
                {
                    AI5bl3_combo.Items.Remove(name);
                    if (AI5bl3_combo.Items.Count > 1)
                    {
                        AI5bl3_combo.SelectedIndex = AI5bl3_combo.Items.Count - 1;
                        if (AI5bl3_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI5bl3_combo.SelectedItem.ToString(), AI5bl3_combo);
                            findAi = list_ai.Find(x => x.Name == AI5bl3_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI5bl3_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI5bl3_combo.SelectedItem = notSelected;
                        AI5bl3_lab.Text = "";
                    }
                    AI5bl3combo_text = AI5bl3_combo.SelectedItem.ToString();
                    AI5bl3combo_index = AI5bl3_combo.SelectedIndex;
                }
            for (int i = 0; i < AI6bl3_combo.Items.Count; i++) // AI6, блок 3
                if (AI6bl3_combo.Items[i].ToString() == name)
                {
                    AI6bl3_combo.Items.Remove(name);
                    if (AI6bl3_combo.Items.Count > 1)
                    {
                        AI6bl3_combo.SelectedIndex = AI6bl3_combo.Items.Count - 1;
                        if (AI6bl3_combo.SelectedItem.ToString() != notSelected)
                        {
                            SubFromCombosAI(AI6bl3_combo.SelectedItem.ToString(), AI6bl3_combo);
                            findAi = list_ai.Find(x => x.Name == AI6bl3_combo.SelectedItem.ToString());
                            if (findAi != null)
                            {
                                list_ai.Remove(findAi);
                                //findAi.Select();
                                //list_ai.Add(findAi);
                                if (showCode) AI6bl3_lab.Text = findAi.Code.ToString();
                            }
                        }
                    }
                    else // Только "Не выбрано"
                    {
                        AI6bl3_combo.SelectedItem = notSelected;
                        AI6bl3_lab.Text = "";
                    }
                    AI6bl3combo_text = AI6bl3_combo.SelectedItem.ToString();
                    AI6bl3combo_index = AI6bl3_combo.SelectedIndex;
                }
            subDIcondition = false; subAIcondition = false;
            list_di.Remove(findDi); // Удаление сигнала из списка DI
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Выбрали блок заслонки</summary>
        private void DampCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di1, find_di2;
            ushort code_1 = 6, code_2 = 39; // Подтверждение открытия приточной/вытяжной заслонки
            if (dampCheck.Checked && confPrDampCheck.Checked)
            { // Выбрана приточная заслонка и подтверждение открытия
                find_di1 = list_di.Find(x => x.Code == code_1);
                if (find_di1 == null) // Нет такой записи
                {
                    list_di.Add(new Di("Подтверждение открытия приточной заслонки", code_1));
                    AddNewDI(code_1); // Добавление DI к свободному comboBox
                }
            }
            if (dampCheck.Checked && outDampCheck.Checked && confOutDampCheck.Checked)
            { // Вытяжная заслонка и подтверждение открытия
                find_di2 = list_di.Find(x => x.Code == code_2);
                if (find_di2 == null) // Нет такой записи
                {
                    list_di.Add(new Di("Подтверждение открытия вытяжной заслонки", code_2));
                    AddNewDI(code_2); // Добавление DI к свободному comboBox
                }
            }
            else // Отмена выбора блока заслонки
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
        }

        ///<summary>Выбрали подтверждение открытия приточной заслонки</summary>
        private void ConfPrDampCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 6; // Подтверждение открытия приточной заслонки
            if (dampCheck.Checked && confPrDampCheck.Checked)
            { // Выбрана приточная заслонка и подтвреждение открытия
                find_di = list_di.Find(x => x.Code == code_1);
                if (find_di == null) // Нет такой записи
                {
                    list_di.Add(new Di("Подтверждение открытия приточной заслонки", code_1));
                    AddNewDI(code_1); // Добавление DI к свободному comboBox
                }
            }
            else // Отмен выбора сигнала
            {
                SubFromCombosDI(code_1);
            }
        }

        ///<summary>Выбрали вытяжную заслонку</summary>
        private void OutDampCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 39; // Подтверждение открытия вытяжной заслонки
            if (comboSysType.SelectedIndex == 1 && dampCheck.Checked && outDampCheck.Checked)
            { // ПВ-система, выбрана вытяжная заслонка
                if (confOutDampCheck.Checked) // Подтверждение открытия
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Подтверждение открытия вытяжной заслонки", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
            }
            else // Отмена выбора заслонки
            {
                SubFromCombosDI(code_1);
            }
        }

        ///<summary>Выбрали подтверждение открытия вытяжной заслонки</summary>
        private void ConfOutDampCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 39; // Подтверждение открытия вытяжной заслонки
            if (comboSysType.SelectedIndex == 1 && dampCheck.Checked && outDampCheck.Checked)
            { // ПВ-система, выбрана вытяжная заслонка
                if (confOutDampCheck.Checked) // Подтверждение открытия
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Подтверждение открытия вытяжной заслонки", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
                else // Отмена выбора подтверждения открытия
                {
                    SubFromCombosDI(code_1);
                }
            }
        }

        ///<summary>Выбрали PS приточного вентилятора</summary>
        private void PrFanPSCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 11; // PS приточного вентилятора 1
            ushort code_2 = 22; // PS приточного вентилятора 2
            if (prFanPSCheck.Checked) // Выбрали PS
            {
                find_di = list_di.Find(x => x.Code == code_1);
                if (find_di == null) // Нет такой записи
                {
                    list_di.Add(new Di("PS приточного вентилятора 1", code_1));
                    AddNewDI(code_1); // Добавление DI к свободному comboBox
                }
                if (checkResPrFan.Checked) // Если выбран резерв
                {
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("PS приточного вентилятора 2", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
            }
            else // Отмена выбора PS
            {
                SubFromCombosDI(code_1);
                if (checkResPrFan.Checked) SubFromCombosDI(code_2); // Для резерва
            }
        }

        ///<summary>Выбрали термоконтакты приточного вентилятора</summary>
        private void PrFanThermoCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 13; // Термоконтакты приточного вентилятора 1
            ushort code_2 = 24; // Термоконтакты приточного вентилятора 2
            if (prFanThermoCheck.Checked) // Выбрали термоконтакты
            {
                find_di = list_di.Find(x => x.Code == code_1);
                if (find_di == null) // Нет такой записи
                {
                    list_di.Add(new Di("Термоконтакты приточного вентилятора 1", code_1));
                    AddNewDI(code_1); // Добавление DI к свободному comboBox
                }
                if (checkResPrFan.Checked) // Если выбран резерв
                {
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Термоконтакты приточного вентилятора 2", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
            }
            else // Отмена выбора термоконтактов
            {
                SubFromCombosDI(code_1);
                if (checkResPrFan.Checked) SubFromCombosDI(code_2); // Для резерва
            }
        }

        ///<summary>Выбрали ПЧ приточного вентилятора</summary>
        private void PrFanFC_check_signalsDICheckedChanged(object sender, EventArgs e)
        {
            if (prFanFC_check.Checked) // Выбран ПЧ
            { 
                if (prFanControlCombo.SelectedIndex == 0) // Внешние контакты
                {
                    prFanAlarmCheck.Enabled = true; // Разблокировка выбора аварии
                    // prFanSpeedCheck.Enabled = true; // Разблокировка выбора скорости
                    if (!prFanAlarmCheck.Checked) prFanAlarmCheck.Checked = true; // Выбор сигнала аварии
                }
                else if (prFanControlCombo.SelectedIndex == 1) // Управление Modbus
                {
                    // Снятие выбора опций
                    if (prFanAlarmCheck.Checked) prFanAlarmCheck.Checked = false; 
                    // if (prFanSpeedCheck.Checked) prFanSpeedCheck.Checked = false;
                    // Блокировка опций
                    prFanAlarmCheck.Enabled = false;
                    // prFanSpeedCheck.Enabled = false;
                }
            } 
            else // Отмена выбора ПЧ
            {
                prFanAlarmCheck.Enabled = true; // Разблокировка выбора аварии
                // prFanSpeedCheck.Enabled = true; // Разблокировка выбора скорости
                if (prFanAlarmCheck.Checked) prFanAlarmCheck.Checked = false; // Снятие выбора сигнала аварии
                // if (!prFanStStopCheck.Checked) prFanStStopCheck.Checked = true; // Сигнал "Пуск/Стоп"
            }
        }

        ///<summary>Изменили тип управления ПЧ приточного вентилятора</summary>
        private void PrFanControlCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            if (prFanFC_check.Checked) // Когда выбран ПЧ
            {
                if (prFanControlCombo.SelectedIndex == 0) // Внешние контакты
                {
                    prFanAlarmCheck.Enabled = true; // Разблокировка выбора аварии
                    //prFanSpeedCheck.Enabled = true; // Разблокировка выбора скорости
                    if (!prFanAlarmCheck.Checked) // Выбор сигнала аварии
                        prFanAlarmCheck.Checked = true; 
                    //prFanStStopCheck.Checked = true; // Выбор сигнала "Пуск/Стоп"
                }
                else if (prFanControlCombo.SelectedIndex == 1) // Modbus
                {
                    // Снятие выбора опций
                    if (prFanAlarmCheck.Checked)
                        prFanAlarmCheck.Checked = false;
                    //prFanSpeedCheck.Checked = false;
                    //prFanStStopCheck.Checked = false;
                    // Блокировка опций
                    prFanAlarmCheck.Enabled = false;
                    //prFanSpeedCheck.Enabled = false;
                }
            }
        }

        ///<summary>Выбрали защиту по току приточного вентилятора</summary>
        private void CurDefPrFanCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 14; // Защита по току приточного вентилятора 1
            ushort code_2 = 25; // Защита по току приточного вентилятора 2
            if (curDefPrFanCheck.Checked) // Выбрана защита по току
            {
                find_di = list_di.Find(x => x.Code == code_1);
                if (find_di == null) // Нет такой записи
                {
                    list_di.Add(new Di("Защита по току приточного вентилятора 1", code_1));
                    AddNewDI(code_1); // Добавление DI к свободному comboBox
                }
                if (checkResPrFan.Checked) // Если выбран резерв
                {
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Защита по току приточного вентилятора 2", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
            } 
            else // Отмена выбора защиты по току
            {
                SubFromCombosDI(code_1);
                if (checkResPrFan.Checked) SubFromCombosDI(code_2); // Для резерва
            }
        }

        ///<summary>Выбрали резерв приточного вентилятора</summary>
        private void CheckResPrFan_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 22; // PS приточного вентилятора 2
            ushort code_2 = 24; // Термоконтакты приточного вентилятора 2
            ushort code_3 = 23; // Сигнал аварии приточного вентилятора 2
            ushort code_4 = 25; // Защита по току приточного вентилятора 2
            if (checkResPrFan.Checked) // Выбран резерв приточного
            {
                if (prFanPSCheck.Checked) // Выбран сигнал PS
                {
                    list_di.Add(new Di("PS приточного вентилятора 2", code_1));
                    AddNewDI(code_1); // Добавление DI к свободному comboBox
                }
                if (prFanThermoCheck.Checked) // Выбраны термоконтакты приточного
                {
                    list_di.Add(new Di("Термоконтакты приточного вентилятора 2", code_2));
                    AddNewDI(code_2); // Добавление DI к свободному comboBox
                }
                if (prFanAlarmCheck.Checked) // Выбран сигнал аварии
                { // Сигнал аварии
                    list_di.Add(new Di("Сигнал аварии приточного вентилятора 2", code_3));
                    AddNewDI(code_3); // Добавление DI к свободному comboBox
                }
                if (curDefPrFanCheck.Checked)
                { // Защита по току
                    list_di.Add(new Di("Защита по току приточного вентилятора 2", code_4));
                    AddNewDI(code_4); // Добавление DI к свободному comboBox
                }
            }
            else // Отмена выбора резерва приточного
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2); SubFromCombosDI(code_3);
                SubFromCombosDI(code_4);
            }
        }

        ///<summary>Выбрали PS вытяжного вентилятора</summary>
        private void OutFanPSCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 44; // Сигнал PS вытяжного вентилятора 1
            ushort code_2 = 55; // Сигнал PS вытяжного вентилятора 2
            if (comboSysType.SelectedIndex == 1 && outFanPSCheck.Checked)
            { // Выбрали PS вытяжного вентилятора
                find_di = list_di.Find(x => x.Code == code_1);
                if (find_di == null) // Нет такой записи
                {
                    list_di.Add(new Di("PS вытяжного вентилятора 1", code_1));
                    AddNewDI(code_1); // Добавление DI к свободному comboBox
                }
                if (checkResOutFan.Checked) // Если выбран резерв
                {
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("PS вытяжного вентилятора 2", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
            }
            else // Отмена выбора PS
            {
                SubFromCombosDI(code_1);
                if (checkResOutFan.Checked) SubFromCombosDI(code_2); // Для резерва
            }
        }

        ///<summary>Выбрали термоконтакты вытяжного вентилятора</summary>
        private void OutFanThermoCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 46; // Термоконтакты вытяжного вентилятора 1
            ushort code_2 = 57; // Термоконтакты вытяжного вентилятора 2
            if (comboSysType.SelectedIndex == 1 && outFanThermoCheck.Checked)
            {
                find_di = list_di.Find(x => x.Code == code_1);
                if (find_di == null) // Нет такой записи
                {
                    list_di.Add(new Di("Термоконтакты вытяжного вентилятора 1", code_1));
                    AddNewDI(code_1); // Добавление DI к свободному comboBox
                }
                if (checkResOutFan.Checked) // Если выбран резерв
                {
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Термоконтакты вытяжного вентилятора 2", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
            }
            else // Отмена выбора термоконтактов
            {
                SubFromCombosDI(code_1);
                if (checkResOutFan.Checked) SubFromCombosDI(code_2); // Для резерва
            }
        }

        ///<summary>Выбрали ПЧ вытяжного вентилятора</summary>
        private void OutFanFC_check_signalsDICheckedChanged(object sender, EventArgs e)
        {
            if (comboSysType.SelectedIndex == 1 && outFanFC_check.Checked) // Выбран ПЧ 
            { 
                if (outFanControlCombo.SelectedIndex == 0) // Внешние контакты
                {
                    outFanAlarmCheck.Enabled = true; // Разблокировка выбора аварии
                    if (!outFanAlarmCheck.Checked) outFanAlarmCheck.Checked = true; // Выбор сигнала аварии
                }
                else if (outFanControlCombo.SelectedIndex == 1) // Управление Modbus
                {
                    // Снятие выбора опций
                    if (outFanAlarmCheck.Checked) outFanAlarmCheck.Checked = false;
                    // Блокировка опций
                    outFanAlarmCheck.Enabled = false;
                }
            } 
            else // Отмена выбора ПЧ
            {
                outFanAlarmCheck.Enabled = true; // Разблокировка выбора аварии
                if (outFanAlarmCheck.Checked) // Снятие выбора сигнала аварии
                    outFanAlarmCheck.Checked = false; 
            }
        }

        ///<summary>Изменили тип управления ПЧ вытяжного вентилятора</summary>
        private void OutFanControlCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSysType.SelectedIndex == 1 && outFanFC_check.Checked) // Когда выбран ПЧ
            {
                if (outFanControlCombo.SelectedIndex == 0) // Внешние контакты
                {
                    outFanAlarmCheck.Enabled = true; // Разблокировка выбора аварии
                    if (!outFanAlarmCheck.Checked) // Выбор сигнала аварии
                        outFanAlarmCheck.Checked = true; 
                }
                else if (outFanControlCombo.SelectedIndex == 1) // Modbus
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
            Di find_di;
            ushort code_1 = 47; // Защита по току вытяжного вентилятора 1
            ushort code_2 = 58; // Защита по току вытяжного вентилятора 2
            if (comboSysType.SelectedIndex == 1 && curDefOutFanCheck.Checked)
            {
                find_di = list_di.Find(x => x.Code == code_1);
                if (find_di == null) // Нет такой записи
                {
                    list_di.Add(new Di("Защита по току вытяжного вентилятора 1", code_1));
                    AddNewDI(code_1); // Добавление DI к свободному comboBox
                }
                if (checkResOutFan.Checked) // Если выбран резерв
                {
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Защита по току вытяжного вентилятора 2", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
            }
            else // Отмена выбора защиты по току
            {
                SubFromCombosDI(code_1);
                if (checkResOutFan.Checked) SubFromCombosDI(code_2); // Для резерва
            }
        }

        ///<summary>Выбрали резерв вытяжного вентилятора</summary>
        private void CheckResOutFan_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 55; // PS вытяжного вентилятра 2
            ushort code_2 = 57; // Термоконтакты вытяжного вентилятора 2
            ushort code_3 = 56; // Сигнал аварии вытяжного вентилятора 2
            ushort code_4 = 58; // Защита по току вытяжного вентилятора 2
            if (comboSysType.SelectedIndex == 1 && checkResOutFan.Checked) // Выбран резерв вытяжного
            {
                if (outFanPSCheck.Checked) // Выбран сигнал PS
                {
                    list_di.Add(new Di("PS вытяжного вентилятора 2", code_1));
                    AddNewDI(code_1); // Добавление DI к свободному comboBox
                } 
                if (outFanThermoCheck.Checked) // Выбраны термоконтакты
                {
                    list_di.Add(new Di("Термоконтакты вытяжного вентилятора 2", code_2));
                    AddNewDI(code_2); // Добавление DI к свободному comboBox
                }
                if (outFanAlarmCheck.Checked) // Выбрали сигнал аварии
                { // Выбран ПЧ и внешние контакты
                    list_di.Add(new Di("Сигнал аварии вытяжного вентилятора 2", code_3));
                    AddNewDI(code_3); // Добавление DI к свободному comboBox
                }
                if (curDefOutFanCheck.Checked) // Защита по току
                {
                    list_di.Add(new Di("Защита по току вытяжного вентилятора 2", code_4));
                    AddNewDI(code_4); // Добавление DI к свободному comboBox
                }
            }
            else // Отмена выбора резерва вытяжного
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2); 
                SubFromCombosDI(code_3); SubFromCombosDI(code_4);
            }
        }

        ///<summary>Выбрали нагреватель</summary>
        private void HeaterCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 72; // Воздушный термостат
            ushort code_2 = 73; // Подтверждение работы насоса
            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)
            { // Выбран водяной нагреватель
                if (TF_heaterCheck.Checked) // Выбран термостат
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Термостат водяного калорифера", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
                if (confHeatPumpCheck.Checked) // Подтверждение работы насоса
                {
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Подтверждение работы насоса водяного калорифера", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
            }
            else // Отмена выбора нагревателя
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
            ThermSwitchCombo_signalsDISelectedIndexChanged(this, e); // Проверка термовыключателей
        }

        ///<summary>Изменили тип основного нагревателя</summary>
        private void HeatTypeCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 72; // Воздушный термостат
            ushort code_2 = 73; // Подтверждение работы насоса
            if (heaterCheck.Checked) // Выбран нагреватель
            { 
                if (heatTypeCombo.SelectedIndex == 0) // Водяной калорифер
                {
                    if (TF_heaterCheck.Checked) // Воздушный термостат
                    {
                        find_di = list_di.Find(x => x.Code == code_1);
                        if (find_di == null) // Нет такой записи
                        {
                            list_di.Add(new Di("Термостат водяного калорифера", code_1));
                            AddNewDI(code_1); // Добавление DI к свободному comboBox
                        }
                    }
                    if (confHeatPumpCheck.Checked) // Подтверждение работы насоса
                    {
                        find_di = list_di.Find(x => x.Code == code_2);
                        if (find_di == null) // Нет такой записи
                        {
                            list_di.Add(new Di("Подтверждение работы насоса водяного калорифера", code_2));
                            AddNewDI(code_2); // Добавление DI к свободному comboBox
                        }
                    }
                }
                else // Электрокалорифер
                {
                    SubFromCombosDI(code_1); SubFromCombosDI(code_2);
                }
                ThermSwitchCombo_signalsDISelectedIndexChanged(this, e); // Проверка термовыключателей
            }
        }

        ///<summary>Выбрали термостат защиты от замерзания</summary>
        private void TF_heaterCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 72; // Воздушный термостат
            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)
            { // Выбран водяной калорифер
                if (TF_heaterCheck.Checked) // Выбрали воздушный термостат
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Термостат водяного калорифера", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                } 
                else // Отмена выбора
                {
                    SubFromCombosDI(code_1);
                }
            }
        }

        ///<summary>Подтверждение работы насоса водяного калорифера</summary>
        private void ConfHeatPumpCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 73; // Подтверждение работы насоса калорифера
            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)
            { // Выбран водяной калорифер
                if (confHeatPumpCheck.Checked) // Выбрали подтверждение работы насоса
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Подтверждение работы насоса водяного калорифера", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
                else // Отмена выбора
                {
                    SubFromCombosDI(code_1);
                }
            }
        }

        ///<summary>Изменили количество термовыключателей основного нагревателя</summary>
        private void ThermSwitchCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 79; // Термовыключатель 1, пожар
            ushort code_2 = 78; // Термовыключатель 2, перегрев
            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 1)
            { // Выбран электрокалорифер
                if (thermSwitchCombo.SelectedIndex == 0) // Нет термовыключателей
                {
                    SubFromCombosDI(code_1); SubFromCombosDI(code_2);
                }
                else if (thermSwitchCombo.SelectedIndex == 1) // Один термовыключатель
                {
                    SubFromCombosDI(code_2);
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Термовыключатель пожара ТЭНов калорифера", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
                else if (thermSwitchCombo.SelectedIndex == 2) // Два термовыключателя
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Термовыключатель пожара ТЭНов калорифера", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Термовыключатель перегрева ТЭНов калорифера", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
            }
            if ((heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0) || !heaterCheck.Checked)
            { // Выбран водяной калорифер, либо нет нагревателя
                SubFromCombosDI(code_1); SubFromCombosDI(code_2); // Удаление сигналов
            }
        }

        ///<summary>Выбрали догреватель</summary>
        private void AddHeatCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 75; // Воздушный термостат
            ushort code_2 = 76; // Подтверждение работы насоса
            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)
            { // Выбран водяной догреватель
                if (TF_addHeaterCheck.Checked) // Выбран термостат
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Термостат водяного догревателя", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
                if (confAddHeatPumpCheck.Checked) // Подтверждение работы насоса
                {
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Подтверждение работы насоса водяного догревателя", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
            } 
            else // Отмена выбора догревателя
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
            ThermAddSwitchCombo_signalsDISelectedIndexChanged(this, e); // Проверка для термовыключателей
        }

        ///<summary>Изменили тип догревателя</summary>
        private void HeatAddTypeCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 75; // Воздушный термостат
            ushort code_2 = 76; // Подтверждение работы насоса
            if (addHeatCheck.Checked) // Выбран догреватель
            {
                if (heatAddTypeCombo.SelectedIndex == 0) // Водяной догреватель
                {
                    if (TF_addHeaterCheck.Checked) // Воздушный термостат
                    {
                        find_di = list_di.Find(x => x.Code == code_1);
                        if (find_di == null) // Нет такой записи
                        {
                            list_di.Add(new Di("Термостат водяного догревателя", code_1));
                            AddNewDI(code_1); // Добавление DI к свободному comboBox
                        }
                    }
                    if (confAddHeatPumpCheck.Checked) // Подтверждение работы насоса
                    {
                        find_di = list_di.Find(x => x.Code == code_2);
                        if (find_di == null) // Нет такой записи
                        {
                            list_di.Add(new Di("Подтверждение работы насоса водяного догревателя", code_2));
                            AddNewDI(code_2); // Добавление DI к свободному comboBox
                        }
                    }
                }
                else // Электродогреватель
                {
                    SubFromCombosDI(code_1); SubFromCombosDI(code_2); // Удаление сигналов
                }
                ThermAddSwitchCombo_signalsDISelectedIndexChanged(this, e); // Проверка для термовыключателей
            }
        }

        ///<summary>Выбрали термостат догревателя</summary>
        private void TF_addHeaterCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 75; // Воздушный термостат
            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)
            { // Выбран водяной догреватель
                if (TF_addHeaterCheck.Checked) // Выбрали воздушный термостат
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Термостат водяного догревателя", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                } 
                else // Отмена выбора
                {
                    SubFromCombosDI(code_1);
                }
            }
        }

        ///<summary>Подтверждение работы насоса водяного догревателя</summary>
        private void ConfAddHeatPumpCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 76; // Подтверждение работы насоса водяного догревателя
            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)
            { // Выбран водяной догреватель
                if (confAddHeatPumpCheck.Checked) // Выбрали подтверждение работы насоса
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Подтверждение работы насоса водяного догревателя", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
                else // Отмена выбора
                {
                    SubFromCombosDI(code_1);
                }
            }
        }

        ///<summary>Изменили количество термовыключателей</summary>
        private void ThermAddSwitchCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 81; // Термовыключатель 1, пожар
            ushort code_2 = 80; // Термовыключатель 2, перегрев
            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 1)
            { // Выбран электрический догреватель
                if (thermAddSwitchCombo.SelectedIndex == 0) // Нет термовыключателей
                {
                    SubFromCombosDI(code_1); SubFromCombosDI(code_2);
                }
                else if (thermAddSwitchCombo.SelectedIndex == 1) // Один термовыключатель
                {
                    SubFromCombosDI(code_2);
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Термовыключатель пожара ТЭНов догревателя", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
                else if (thermAddSwitchCombo.SelectedIndex == 2) // Два термовыключателя
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Термовыключатель пожара ТЭНов догревателя", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Термовыключатель перегрева ТЭНов догревателя", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
            }
            if ((addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0) || !addHeatCheck.Checked)
            { // Выбран водяной догреватель, либо нет догревателя
                SubFromCombosDI(code_1); SubFromCombosDI(code_2); // Удаление сигналов
            } 
        }

        ///<summary>Выбрали фильтр</summary>
        private void FilterCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 33; // Фильтр 1, приточный
            ushort code_2 = 34; // Фильтр 2, приточный
            ushort code_3 = 35; // Фильтр 3, приточный
            ushort code_4 = 66; // Фильтр 1, вытяжной
            ushort code_5 = 67; // Фильтр 2, вытяжной
            ushort code_6 = 68; // Фильтр 3, вытяжной
            if (filterCheck.Checked) // Выбрали фильтры
            {
                find_di = list_di.Find(x => x.Code == code_1);
                if (find_di == null) // Нет такой записи
                {
                    list_di.Add(new Di("Приточный фильтр 1", code_1));
                    AddNewDI(code_1); // Добавление DI к свободному comboBox
                }
                if (filterPrCombo.SelectedIndex == 1) // Два фильтра
                {
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Приточный фильтр 2", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
                else if (filterPrCombo.SelectedIndex == 2) // Три фильтра
                {
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Приточный фильтр 2", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                    find_di = list_di.Find(x => x.Code == code_3);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Приточный фильтр 3", code_3));
                        AddNewDI(code_3); // Добавление DI к свободному comboBox
                    }
                }
                if (comboSysType.SelectedIndex == 1) // Выбрана ПВ-система
                {
                    if (filterOutCombo.SelectedIndex == 1) // Один вытяжной фильтр
                    {
                        find_di = list_di.Find(x => x.Code == code_4);
                        if (find_di == null) // Нет такой записи
                        {
                            list_di.Add(new Di("Вытяжной фильтр 1", code_4));
                            AddNewDI(code_4); // Добавление DI к свободному comboBox
                        }
                    }
                    else if (filterOutCombo.SelectedIndex == 2) // Два вытяжных фильтра
                    {
                        find_di = list_di.Find(x => x.Code == code_4);
                        if (find_di == null) // Нет такой записи
                        {
                            list_di.Add(new Di("Вытяжной фильтр 1", code_4));
                            AddNewDI(code_4); // Добавление DI к свободному comboBox
                        }
                        find_di = list_di.Find(x => x.Code == code_5);
                        if (find_di == null) // Нет такой записи
                        {
                            list_di.Add(new Di("Вытяжной фильтр 2", code_5));
                            AddNewDI(code_5); // Добавление DI к свободному comboBox
                        }
                    }
                    else if (filterOutCombo.SelectedIndex == 3) // Три вытяжных фильтра
                    {
                        find_di = list_di.Find(x => x.Code == code_4);
                        if (find_di == null) // Нет такой записи
                        {
                            list_di.Add(new Di("Вытяжной фильтр 1", code_4));
                            AddNewDI(code_4); // Добавление DI к свободному comboBox
                        }
                        find_di = list_di.Find(x => x.Code == code_5);
                        if (find_di == null) // Нет такой записи
                        {
                            list_di.Add(new Di("Вытяжной фильтр 2", code_5));
                            AddNewDI(code_5); // Добавление DI к свободному comboBox
                        }
                        find_di = list_di.Find(x => x.Code == code_6);
                        if (find_di == null) // Нет такой записи
                        {
                            list_di.Add(new Di("Вытяжной фильтр 3", code_6));
                            AddNewDI(code_6); // Добавление DI к свободному comboBox
                        }
                    }
                }
            }
            else // Отмена выбора фильтров
            {
                // Удаление сигналов
                SubFromCombosDI(code_1); SubFromCombosDI(code_2); SubFromCombosDI(code_3);
                SubFromCombosDI(code_4); SubFromCombosDI(code_5); SubFromCombosDI(code_6);
            }
        }

        ///<summary>Изменили количество приточных фильтров</summary>
        private void FilterPrCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 34; // Фильтр 2
            ushort code_2 = 35; // Фильтр 3
            if (filterCheck.Checked) // Выбран фильтр
            {
                if (filterPrCombo.SelectedIndex == 0) // Один фильтр
                {
                    SubFromCombosDI(code_1); SubFromCombosDI(code_2); // Удаление сигналов
                }
                else if (filterPrCombo.SelectedIndex == 1) // Два фильтра
                {
                    SubFromCombosDI(code_2);
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Приточный фильтр 2", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
                else if (filterPrCombo.SelectedIndex == 2) // Три фильтра
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Приточный фильтр 2", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Приточный фильтр 3", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
            }
        }

        ///<summary>Изменили количество вытяжных фильтров</summary>
        private void FilterOutCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 66; // Фильтр 1, вытяжной
            ushort code_2 = 67; // Фильтр 2, вытяжной
            ushort code_3 = 68; // Фильтр 3, вытяжной
            if (comboSysType.SelectedIndex == 1 && filterCheck.Checked)
            { // Выбрана ПВ-система и фильтр
                if (filterOutCombo.SelectedIndex == 0) // Нет вытяжных фильтров
                { // Удаление сигналов
                    SubFromCombosDI(code_1); SubFromCombosDI(code_2); SubFromCombosDI(code_3);
                }
                else if (filterOutCombo.SelectedIndex == 1) // Один вытяжной фильтр
                {
                    SubFromCombosDI(code_2); SubFromCombosDI(code_3);
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Вытяжной фильтр 1", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
                else if (filterOutCombo.SelectedIndex == 2) // Два вытяжных фильтра
                {
                    SubFromCombosDI(code_3);
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Вытяжной фильтр 1", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Вытяжной фильтр 2", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
                else if (filterOutCombo.SelectedIndex == 3) // Три вытяжных фильтра
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Вытяжной фильтр 1", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Вытяжной фильтр 2", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                    find_di = list_di.Find(x => x.Code == code_3);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Вытяжной фильтр 3", code_3));
                        AddNewDI(code_3); // Добавление DI к свободному comboBox
                    }
                }
            } 
        }

        ///<summary>Выбрали охладитель</summary>
        private void CoolerCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 83; // Термостат фреонового охладителя
            ushort code_2 = 84; // Авария фреонового охладителя
            if (coolerCheck.Checked && coolTypeCombo.SelectedIndex == 0)
            { // Выбран фреоновый охладитель
                if (thermoCoolerCheck.Checked)
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Термостат фреонового охладителя", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
                if (alarmFrCoolCheck.Checked)
                {
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Авария фреонового охладителя", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
            }
            else // Отмена выбора охладителя
            {
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
        }

        ///<summary>Изменили тип охладителя</summary>
        private void CoolTypeCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 83; // Термостат фреонового охладителя
            ushort code_2 = 84; // Авария фреонового охладителя
            if (coolerCheck.Checked) // Когда выбран охладитель
            {
                if (coolTypeCombo.SelectedIndex == 0) // Фреоновый охладитель
                {
                    if (thermoCoolerCheck.Checked)
                    {
                        find_di = list_di.Find(x => x.Code == code_1);
                        if (find_di == null) // Нет такой записи
                        {
                            list_di.Add(new Di("Термостат фреонового охладителя", code_1));
                            AddNewDI(code_1); // Добавление DI к свободному comboBox
                        }
                    }
                    if (alarmFrCoolCheck.Checked)
                    {
                        find_di = list_di.Find(x => x.Code == code_2);
                        if (find_di == null) // Нет такой записи
                        {
                            list_di.Add(new Di("Авария фреонового охладителя", code_2));
                            AddNewDI(code_2); // Добавление DI к свободному comboBox
                        }
                    }
                }
                else if (coolTypeCombo.SelectedIndex == 1) // Водяной охладитель
                {
                    SubFromCombosDI(code_1); SubFromCombosDI(code_2);
                }
            }
        }

        ///<summary>Выбрали термостат фреонового охладителя</summary>
        private void ThermoCoolerCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 83; // Термостат фреонового охладителя
            if (coolerCheck.Checked && coolTypeCombo.SelectedIndex == 0)
            { // Выбран фреоновый охладитель
                if (thermoCoolerCheck.Checked)
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Термостат фреонового охладителя", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
                else // Отмена выбора
                {
                    SubFromCombosDI(code_1);
                }
            }
        }

        ///<summary>Выбрали аварийный сигнал фреонового охладителя</summary>
        private void AlarmFrCoolCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 84;
            if (coolerCheck.Checked && coolTypeCombo.SelectedIndex == 0)
            { // Выбран фреоновый охладитель
                if (alarmFrCoolCheck.Checked)
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Авария фреонового охладителя", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
                else // Отмена выбора
                {
                    SubFromCombosDI(code_1);
                }
            }
        }

        ///<summary>Выбрали увлажнитель</summary>
        private void HumidCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 69; // Авария парового увлажнителя
            if (humidCheck.Checked) // Когда выбран увлажнитель
            {
                if (humidTypeCombo.SelectedIndex == 0) // Паровой увлажнитель
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Авария парового увлажнителя", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
            }
            else // Отмена выбора увлажнителя
            {
                SubFromCombosDI(code_1);
            }
        }

        ///<summary>Изменили тип увлажнителя</summary>
        private void HumidTypeCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 69; // Авария парового увлажнителя
            if (humidCheck.Checked) // Когда выбран увлажнитель
            {
                if (humidTypeCombo.SelectedIndex == 0) // Паровой увлажнитель
                {
                    if (alarmHumidCheck.Checked)
                    {
                        find_di = list_di.Find(x => x.Code == code_1);
                        if (find_di == null) // Нет такой записи
                        {
                            list_di.Add(new Di("Авария парового увлажнителя", code_1));
                            AddNewDI(code_1); // Добавление DI к свободному comboBox
                        }
                    }
                }
                else if (humidTypeCombo.SelectedIndex == 1) // Сотовый увлажнитель
                {
                    SubFromCombosDI(code_1);
                }
            }
        }

        ///<summary>Выбрали сигнал аварии парового увлажнителя</summary>
        private void AlarmHumidCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 69; // Авария парового увлажнителя
            if (humidCheck.Checked && humidTypeCombo.SelectedIndex == 0)
            { // Выбран паровой увлажнитель
                if (alarmHumidCheck.Checked)
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Авария парового увлажнителя", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
                else // Отмена выбора
                {
                    SubFromCombosDI(code_1);
                }
            }
        }

        ///<summary>Выбрали рекуператор</summary>
        private void RecupCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 90; // Сигнал PS  рекуператора
            ushort code_2 = 91; // Сигнал аварии роторного рекуператора
            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)
            {
                if (recDefPsCheck.Checked) // Выбрали сигнал PS
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("PS рекуператора", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
                if (recupTypeCombo.SelectedIndex == 0) // Роторный рекуператор
                {
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Авария роторного рекуператора", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
            }
            else // Отмена выбора рекуператора
            { 
                SubFromCombosDI(code_1); SubFromCombosDI(code_2);
            }
        }

        ///<summary>Изменили тип рекуператора</summary>
        private void RecupTypeCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 91; // Сигнал аварии роторного рекуператора
            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)
            {
                if (recupTypeCombo.SelectedIndex == 0) // Роторный рекуператор
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Авария роторного рекуператора", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
                else // Другой тип рекуператора
                {
                    SubFromCombosDI(code_1);
                }
            }
        }

        ///<summary>Выбрали сигнал PS рекуператора</summary>
        private void RecDefPsCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 90; // Сигнал PS рекуператора
            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)
            { // Выбран роторный рекуператор
                if (recDefPsCheck.Checked) // Выбрали сигнал PS
                {
                    find_di = list_di.Find(x => x.Code == code_1);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("PS рекуператора", code_1));
                        AddNewDI(code_1); // Добавление DI к свободному comboBox
                    }
                }
                else // Отмена выбора сигнала PS
                {
                    SubFromCombosDI(code_1);
                }
            }
        }

        ///<summary>Выбрали сигнал переключателя "Стоп/Пуск"</summary>
        private void StopStartCheck_CheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 3; // Переключатель "Стоп/Пуск"
            if (stopStartCheck.Checked) // Выбрали сигнал для переключателя
            {
                find_di = list_di.Find(x => x.Code == code_1);
                if (find_di == null) // Нет такой записи
                {
                    list_di.Add(new Di("Переключатель \"Стоп/Пуск\"", code_1));
                    AddNewDI(code_1); // Добавление DI к свободному comboBox
                }
            }
            else // Отмена выбора сигнала переключателя
            {
                SubFromCombosDI(code_1);
            }
        }

        ///<summary>Выбрали сигнал аварии для приточного вентилятора</summary> 
        private void PrFanAlarmCheck_CheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 12; // Сигнал аварии 1
            ushort code_2 = 23; // Сигнал аварии 2
            if (prFanAlarmCheck.Checked) // Выбрали сигнал аварии
            {
                find_di = list_di.Find(x => x.Code == code_1);
                if (find_di == null) // Нет такой записи
                {
                    list_di.Add(new Di("Сигнал аварии приточного вентилятора 1", code_1));
                    AddNewDI(code_1); // Добавление DI к свободному comboBox
                }
                if (checkResPrFan.Checked) // Выбран резерв приточного вентилятора
                {
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Сигнал аварии приточного вентилятора 2", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
            }
            else // Отмена выбора сигнала аварии
            {
                SubFromCombosDI(code_1);
                if (checkResPrFan.Checked) SubFromCombosDI(code_2);
            }
        }

        ///<summary>Выбрали сигнал аварии для вытяжного вентилятора</summary>
        private void OutFanAlarmCheck_CheckedChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 45; // Сигнал аварии 1
            ushort code_2 = 56; // Сигнал аварии 2
            if (outFanAlarmCheck.Checked) // Выбрали сигнал аварии
            {
                find_di = list_di.Find(x => x.Code == code_1);
                if (find_di == null) // Нет такой записи
                {
                    list_di.Add(new Di("Сигнал аварии вытяжного вентилятора 1", code_1));
                    AddNewDI(code_1); // Добавление DI к свободному comboBox
                }
                if (checkResOutFan.Checked) // Выбран резерв вытяжного вентилятора
                {
                    find_di = list_di.Find(x => x.Code == code_2);
                    if (find_di == null) // Нет такой записи
                    {
                        list_di.Add(new Di("Сигнал аварии вытяжного вентилятора 2", code_2));
                        AddNewDI(code_2); // Добавление DI к свободному comboBox
                    }
                }
            }
            else // Отмена выбора сигнала аварии
            {
                SubFromCombosDI(code_1);
                if (checkResPrFan.Checked) SubFromCombosDI(code_2);
            }
        }

        ///<summary>Выбрали сигнал пожарной сигнализации</summary>
        private void FireCheck_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            Di find_di;
            ushort code_1 = 98; // Сигнал пожарной сигнализации
            if (fireCheck.Checked)
            {   // Выбран сигнал
                find_di = list_di.Find(x => x.Code == code_1);
                if (find_di == null) // Нет такой записи
                {
                    list_di.Add(new Di("Сигнал пожарной сигнализации", code_1));
                    AddNewDI(code_1); // Добавление DI к свободному comboBox
                }
            }
            else // Отмена выбора сигнала пожарной сигнализации
            {
                SubFromCombosDI(code_1);
            }
        }
    }
}