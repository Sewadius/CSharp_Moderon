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
        public bool Active { get; private set; } = true; // Свободен по умолчанию
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

        static readonly string NTC = "NTC";
        static readonly string Pt1000 = "Pt1000";
        static readonly string mA_4_20 = "4-20 мА";
        static readonly string V0_10 = "0-10 В";

        // Сохранение наименования выбранного элемента для ПЛК
        string AI1combo_text = notSelected;
        string AI2combo_text = notSelected;
        string AI3combo_text = notSelected;
        string AI4combo_text = notSelected;
        string AI5combo_text = notSelected;
        string AI6combo_text = notSelected;

        // Сохранение наименования выбранного элемента для блока 1
        string AI1bl1combo_text = notSelected;
        string AI2bl1combo_text = notSelected;
        string AI3bl1combo_text = notSelected;
        string AI4bl1combo_text = notSelected;
        string AI5bl1combo_text = notSelected;
        string AI6bl1combo_text = notSelected;

        // Сохранение наименования выбранного элемента для блока 2
        string AI1bl2combo_text = notSelected;
        string AI2bl2combo_text = notSelected;
        string AI3bl2combo_text = notSelected;
        string AI4bl2combo_text = notSelected;
        string AI5bl2combo_text = notSelected;
        string AI6bl2combo_text = notSelected;

        // Сохранение наименование выбранного элемента для блока 3
        string AI1bl3combo_text = notSelected;
        string AI2bl3combo_text = notSelected;
        string AI3bl3combo_text = notSelected;
        string AI4bl3combo_text = notSelected;
        string AI5bl3combo_text = notSelected;
        string AI6bl3combo_text = notSelected;

        // Сохранение прошлого индекса для comboBox ПЛК
        int AI1combo_index = 0;
        int AI2combo_index = 0;
        int AI3combo_index = 0;
        int AI4combo_index = 0;
        int AI5combo_index = 0;
        int AI6combo_index = 0;

        // Сохранение прошлого индекса для comboBox блока 1
        int AI1bl1combo_index = 0;
        int AI2bl1combo_index = 0;
        int AI3bl1combo_index = 0;
        int AI4bl1combo_index = 0;
        int AI5bl1combo_index = 0;
        int AI6bl1combo_index = 0;

        // Сохранение прошлого индекса для comboBox блока 2
        int AI1bl2combo_index = 0;
        int AI2bl2combo_index = 0;
        int AI3bl2combo_index = 0;
        int AI4bl2combo_index = 0;
        int AI5bl2combo_index = 0;
        int AI6bl2combo_index = 0;

        // Сохранение прошлого индекса для comboBox блока 3
        int AI1bl3combo_index = 0;
        int AI2bl3combo_index = 0;
        int AI3bl3combo_index = 0;
        int AI4bl3combo_index = 0;
        int AI5bl3combo_index = 0;
        int AI6bl3combo_index = 0;

        ///<summary>Сброс выбора сигналов AI comboBox</summary>
        private void ResetButton_signalsAIClick(object sender, EventArgs e)
        {
            // Очистка comboBox ПЛК
            AI1_combo.Items.Clear(); AI1_combo.Items.Add(notSelected);
            AI2_combo.Items.Clear(); AI2_combo.Items.Add(notSelected);
            AI3_combo.Items.Clear(); AI3_combo.Items.Add(notSelected);
            AI4_combo.Items.Clear(); AI4_combo.Items.Add(notSelected);
            AI5_combo.Items.Clear(); AI5_combo.Items.Add(notSelected);
            AI6_combo.Items.Clear(); AI6_combo.Items.Add(notSelected);
            // Очистка comboBox блок расширения 1
            AI1bl1_combo.Items.Clear(); AI1bl1_combo.Items.Add(notSelected);
            AI2bl1_combo.Items.Clear(); AI2bl1_combo.Items.Add(notSelected);
            AI3bl1_combo.Items.Clear(); AI3bl1_combo.Items.Add(notSelected);
            AI4bl1_combo.Items.Clear(); AI4bl1_combo.Items.Add(notSelected);
            AI5bl1_combo.Items.Clear(); AI5bl1_combo.Items.Add(notSelected);
            AI6bl1_combo.Items.Clear(); AI6bl1_combo.Items.Add(notSelected);
            // Очистка comboBox блок расширения 2
            AI1bl2_combo.Items.Clear(); AI1bl2_combo.Items.Add(notSelected);
            AI2bl2_combo.Items.Clear(); AI2bl2_combo.Items.Add(notSelected);
            AI3bl2_combo.Items.Clear(); AI3bl2_combo.Items.Add(notSelected);
            AI4bl2_combo.Items.Clear(); AI4bl2_combo.Items.Add(notSelected);
            AI5bl2_combo.Items.Clear(); AI5bl2_combo.Items.Add(notSelected);
            AI6bl2_combo.Items.Clear(); AI6bl2_combo.Items.Add(notSelected);
            // Очистка comboBox блок расширения 3
            AI1bl3_combo.Items.Clear(); AI1bl3_combo.Items.Add(notSelected);
            AI2bl3_combo.Items.Clear(); AI2bl3_combo.Items.Add(notSelected);
            AI3bl3_combo.Items.Clear(); AI3bl3_combo.Items.Add(notSelected);
            AI4bl3_combo.Items.Clear(); AI4bl3_combo.Items.Add(notSelected);
            AI5bl3_combo.Items.Clear(); AI5bl3_combo.Items.Add(notSelected);
            AI6bl3_combo.Items.Clear(); AI6bl3_combo.Items.Add(notSelected);
        }

        ///<summary>Изменили AI1 comboBox</summary>
        private void AI1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI1_combo.SelectedIndex == AI1combo_index) return; // Индекс не поменялся
            if (AI1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI1_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI1combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI1combo_text)); // Удаление из списка
                    if (showCode) AI1_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI1combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI1combo_text, AI1_combo); // Добавление к другим AI
                SensorAIType(AI1_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI1_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI 
                    SensorAIType(AI1_typeCombo, 2); // Установка NTC и блокировка
                } 
                else if (ai_find.Type == "humidSensor")
                    SensorAIType(AI1_typeCombo, 1); // Установка для датчика влажности
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI1_typeCombo, 0); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI1_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                { // Удаление ранее выбранного сигнала AI
                    SubFromCombosAI(name, AI1_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI1combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI1combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI1combo_text, AI1_combo); // Добавление к другим AI
                }
            }
            AI1combo_text = AI1_combo.SelectedItem.ToString(); // Сохранение название выбранного элемента в переменной
            AI1combo_index = AI1_combo.SelectedIndex; // Сохранение индекса выбранного элемента
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI2 comboBox</summary>
        private void AI2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI2_combo.SelectedIndex == AI2combo_index) return; // Индекс не поменялся
            if (AI2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI2_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI2combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI2combo_text)); // Удаление из списка
                    if (showCode) AI2_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI2combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI2combo_text, AI2_combo); // Добавление к другим AI
                SensorAIType(AI2_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI2_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI 
                    SensorAIType(AI2_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "humidSensor")
                    SensorAIType(AI2_typeCombo, 1); // Установка для датчика влажности
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI2_typeCombo, 0); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI2_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI2_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI2combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI2combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI2combo_text, AI2_combo); // Добавление к другим AI
                }
            }
            AI2combo_text = AI2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI2combo_index = AI2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI3 comboBox</summary>
        private void AI3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI3_combo.SelectedIndex == AI3combo_index) return; // Индекс не поменялся
            if (AI3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI3_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI3combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI3combo_text)); // Удаление из списка
                    if (showCode) AI3_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI3combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI3combo_text, AI3_combo); // Добавление к другим AI
                SensorAIType(AI3_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI3_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI
                    SensorAIType(AI3_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "humidSensor")
                    SensorAIType(AI3_typeCombo, 1); // Установка для датчика влажности
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI3_typeCombo, 0); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI3_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI3_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI3combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI3combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI3combo_text, AI3_combo); // Добавление к другим AI
                }
            }
            AI3combo_text = AI3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI3combo_index = AI3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI4 comboBox</summary>
        private void AI4_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI4_combo.SelectedIndex == AI4combo_index) return; // Индекс не поменялся
            if (AI4_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI4_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI4combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI4combo_text)); // Удаление из списка
                    if (showCode) AI4_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI4combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI4combo_text, AI4_combo); // Добавление к другим AI
                SensorAIType(AI4_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI4_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI
                    SensorAIType(AI4_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI4_typeCombo, 3); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI4_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI4_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI4combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI4combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI4combo_text, AI4_combo); // Добавление к другим AI
                }
            }
            AI4combo_text = AI4_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI4combo_index = AI4_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI5 comboBox</summary>
        private void AI5_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI5_combo.SelectedIndex == AI5combo_index) return; // Индекс не поменялся
            if (AI5_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI5_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI5combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI5combo_text)); // Удаление из списка
                    if (showCode) AI5_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI5combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI5combo_text, AI5_combo); // Добавление к другим AI
                SensorAIType(AI5_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI5_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI
                    SensorAIType(AI5_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI5_typeCombo, 3); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI5_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI5_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI5combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI5combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI5combo_text, AI5_combo); // Добавление к другим AI
                }
            }
            AI5combo_text = AI5_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI5combo_index = AI5_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI6 comboBox</summary>
        private void AI6_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI6_combo.SelectedIndex == AI6combo_index) return; // Индекс не поменялся
            if (AI6_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI6_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI6combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI6combo_text)); // Удаление из списка
                    if (showCode) AI6_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI6combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI6combo_text, AI6_combo); // Добавление к другим AI
                SensorAIType(AI6_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI6_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI 
                    SensorAIType(AI6_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI6_typeCombo, 3); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI6_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI6_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI6combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI6combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI6combo_text, AI6_combo); // Добавление к другим AI
                }
            }
            AI6combo_text = AI6_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI6combo_index = AI6_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI1 блока расширения 1 comboBox</summary>
        private void AI1bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI1bl1_combo.SelectedIndex == AI1bl1combo_index) return; // Индекс не поменялся
            if (AI1bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI1bl1_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI1bl1combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI1bl1combo_text)); // Удаление из списка
                    if (showCode) AI1bl1_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI1bl1combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI1bl1combo_text, AI1bl1_combo); // Добавление к другим AI
                SensorAIType(AI1bl1_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI1bl1_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI 
                    SensorAIType(AI1bl1_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "humidSensor")
                    SensorAIType(AI1bl1_typeCombo, 1); // Установка для датчика влажности
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI1bl1_typeCombo, 0); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI1bl1_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI1bl1_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI1bl1combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI1bl1combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI1bl1combo_text, AI1bl1_combo); // Добавление к другим AI
                }
            }
            AI1bl1combo_text = AI1bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI1bl1combo_index = AI1bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI2 блока расширения 1 comboBox</summary>
        private void AI2bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI2bl1_combo.SelectedIndex == AI2bl1combo_index) return; // Индекс не поменялся
            if (AI2bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI2bl1_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI2bl1combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI2bl1combo_text)); // Удаление из списка
                    if (showCode) AI2bl1_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI2bl1combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI2bl1combo_text, AI2bl1_combo); // Добавление к другим AI
                SensorAIType(AI2bl1_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI2bl1_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI 
                    SensorAIType(AI2bl1_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "humidSensor")
                    SensorAIType(AI2bl1_typeCombo, 1); // Установка для датчика влажности
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI2bl1_typeCombo, 0); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI2bl1_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI2bl1_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI2bl1combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI2bl1combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI2bl1combo_text, AI2bl1_combo); // Добавление к другим AI
                }
            }
            AI2bl1combo_text = AI2bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI2bl1combo_index = AI2bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI3 блока расширения 1 comboBox</summary>
        private void AI3bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI3bl1_combo.SelectedIndex == AI3bl1combo_index) return; // Индекс не поменялся
            if (AI3bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI3bl1_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI3bl1combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI3bl1combo_text)); // Удаление из списка
                    if (showCode) AI3bl1_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI3bl1combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI3bl1combo_text, AI3bl1_combo); // Добавление к другим AI
                SensorAIType(AI3bl1_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI3bl1_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI 
                    SensorAIType(AI3bl1_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "humidSensor")
                    SensorAIType(AI3bl1_typeCombo, 1); // Установка для датчика влажности
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI3bl1_typeCombo, 0); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI3bl1_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI3bl1_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI3bl1combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI3bl1combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI3bl1combo_text, AI3bl1_combo); // Добавление к другим AI
                }
            }
            AI3bl1combo_text = AI3bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI3bl1combo_index = AI3bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI4 блока расширения 1 comboBox</summary>
        private void AI4bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI4bl1_combo.SelectedIndex == AI4bl1combo_index) return; // Индекс не поменялся
            if (AI4bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI4bl1_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI4bl1combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI4bl1combo_text)); // Удаление из списка
                    if (showCode) AI4bl1_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI4bl1combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI4bl1combo_text, AI4bl1_combo); // Добавление к другим AI
                SensorAIType(AI4bl1_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI4bl1_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI
                    SensorAIType(AI4bl1_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI4bl1_typeCombo, 3); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI4bl1_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI4bl1_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI4bl1combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI4bl1combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI4bl1combo_text, AI4bl1_combo); // Добавление к другим AI
                }
            }
            AI4bl1combo_text = AI4bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI4bl1combo_index = AI4bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI5 блока расширения 1 comboBox</summary>
        private void AI5bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI5bl1_combo.SelectedIndex == AI5bl1combo_index) return; // Индекс не поменялся
            if (AI5bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI5bl1_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI5bl1combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI5bl1combo_text)); // Удаление из списка
                    if (showCode) AI5bl1_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI5bl1combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI5bl1combo_text, AI5bl1_combo); // Добавление к другим AI
                SensorAIType(AI5bl1_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI5bl1_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI
                    SensorAIType(AI5bl1_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI5bl1_typeCombo, 3); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI5bl1_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI5bl1_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI5bl1combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI5bl1combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI5bl1combo_text, AI5bl1_combo); // Добавление к другим AI
                }
            }
            AI5bl1combo_text = AI5bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI5bl1combo_index = AI5bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI6 блока расширения 1 comboBox</summary>
        private void AI6bl1_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI6bl1_combo.SelectedIndex == AI6bl1combo_index) return; // Индекс не поменялся
            if (AI6bl1_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI6bl1_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI6bl1combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI6bl1combo_text)); // Удаление из списка
                    if (showCode) AI6bl1_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI6bl1combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI6bl1combo_text, AI6bl1_combo); // Добавление к другим AI
                SensorAIType(AI6bl1_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI6bl1_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI
                    SensorAIType(AI6bl1_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI6bl1_typeCombo, 3); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI6bl1_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI6bl1_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI6bl1combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI6bl1combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI6bl1combo_text, AI6bl1_combo); // Добавление к другим AI
                }
            }
            AI6bl1combo_text = AI6bl1_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI6bl1combo_index = AI6bl1_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI1 блока расширения 2 comboBox</summary>
        private void AI1bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI1bl2_combo.SelectedIndex == AI1bl2combo_index) return; // Индекс не поменялся
            if (AI1bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI1bl2_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI1bl2combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI1bl2combo_text)); // Удаление из списка
                    if (showCode) AI1bl2_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI1bl2combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI1bl2combo_text, AI1bl2_combo); // Добавление к другим AI
                SensorAIType(AI1bl2_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI1bl2_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI 
                    SensorAIType(AI1bl2_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "humidSensor")
                    SensorAIType(AI1bl2_typeCombo, 1); // Установка для датчика влажности
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI1bl2_typeCombo, 0); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI1bl2_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI1bl2_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI1bl2combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI1bl2combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI1bl2combo_text, AI1bl2_combo); // Добавление к другим AI
                }
            }
            AI1bl2combo_text = AI1bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI1bl2combo_index = AI1bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI2 блока расширения 2 comboBox</summary>
        private void AI2bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI2bl2_combo.SelectedIndex == AI2bl2combo_index) return; // Индекс не поменялся
            if (AI2bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI2bl2_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI2bl2combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI2bl2combo_text)); // Удаление из списка
                    if (showCode) AI2bl2_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI2bl2combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI2bl2combo_text, AI2bl2_combo); // Добавление к другим AI
                SensorAIType(AI2bl2_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI2bl2_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI 
                    SensorAIType(AI2bl2_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "humidSensor")
                    SensorAIType(AI2bl2_typeCombo, 1); // Установка для датчика влажности
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI2bl2_typeCombo, 0); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI2bl2_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI2bl2_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI2bl2combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI2bl2combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI2bl2combo_text, AI2bl2_combo); // Добавление к другим AI
                }
            }
            AI2bl2combo_text = AI2bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI2bl2combo_index = AI2bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI3 блока расширения 2 comboBox</summary>
        private void AI3bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI3bl2_combo.SelectedIndex == AI3bl2combo_index) return; // Индекс не поменялся
            if (AI3bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI3bl2_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI3bl2combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI3bl2combo_text)); // Удаление из списка
                    if (showCode) AI3bl2_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI3bl2combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI3bl2combo_text, AI3bl2_combo); // Добавление к другим AI
                SensorAIType(AI3bl2_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI3bl2_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI 
                    SensorAIType(AI3bl2_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "humidSensor")
                    SensorAIType(AI3bl2_typeCombo, 1); // Установка для датчика влажности
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI3bl2_typeCombo, 0); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI3bl2_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI3bl2_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI3bl2combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI3bl2combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI3bl2combo_text, AI3bl2_combo); // Добавление к другим AI
                }
            }
            AI3bl2combo_text = AI3bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI3bl2combo_index = AI3bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI4 блока расширения 2 comboBox</summary>
        private void AI4bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI4bl2_combo.SelectedIndex == AI4bl2combo_index) return; // Индекс не поменялся
            if (AI4bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI4bl2_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI4bl2combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI4bl2combo_text)); // Удаление из списка
                    if (showCode) AI4bl2_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI4bl2combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI4bl2combo_text, AI4bl2_combo); // Добавление к другим AI
                SensorAIType(AI4bl2_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI4bl2_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI
                    SensorAIType(AI4bl2_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI4bl2_typeCombo, 3); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI4bl2_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI4bl2_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI4bl2combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI4bl2combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI4bl2combo_text, AI4bl2_combo); // Добавление к другим AI
                }
            }
            AI4bl2combo_text = AI4bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI4bl2combo_index = AI4bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI5 блока расширения 2 comboBox</summary>
        private void AI5bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI5bl2_combo.SelectedIndex == AI5bl2combo_index) return; // Индекс не поменялся
            if (AI5bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI5bl2_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI5bl2combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI5bl2combo_text)); // Удаление из списка
                    if (showCode) AI5bl2_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI5bl2combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI5bl2combo_text, AI5bl2_combo); // Добавление к другим AI
                SensorAIType(AI5bl2_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI5bl2_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI
                    SensorAIType(AI5bl2_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI5bl2_typeCombo, 3); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI5bl2_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI5bl2_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI5bl2combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI5bl2combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI5bl2combo_text, AI5bl2_combo); // Добавление к другим AI
                }
            }
            AI5bl2combo_text = AI5bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI5bl2combo_index = AI5bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI6 блока расширения 2 comboBox</summary>
        private void AI6bl2_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI6bl2_combo.SelectedIndex == AI6bl2combo_index) return; // Индекс не поменялся
            if (AI6bl2_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI6bl2_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI6bl2combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI6bl2combo_text)); // Удаление из списка
                    if (showCode) AI6bl2_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI6bl2combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI6bl2combo_text, AI6bl2_combo); // Добавление к другим AI
                SensorAIType(AI6bl2_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI6bl2_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI
                    SensorAIType(AI6bl2_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI6bl2_typeCombo, 3); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI6bl2_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI6bl2_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI6bl2combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI6bl2combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI6bl2combo_text, AI6bl2_combo); // Добавление к другим AI
                }
            }
            AI6bl2combo_text = AI6bl2_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI6bl2combo_index = AI6bl2_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI1 блока расширения 3 comboBox</summary>
        private void AI1bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI1bl3_combo.SelectedIndex == AI1bl3combo_index) return; // Индекс не поменялся
            if (AI1bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI1bl3_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI1bl3combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI1bl3combo_text)); // Удаление из списка
                    if (showCode) AI1bl3_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI1bl3combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI1bl3combo_text, AI1bl3_combo); // Добавление к другим AI
                SensorAIType(AI1bl3_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI1bl3_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI 
                    SensorAIType(AI1bl3_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "humidSensor")
                    SensorAIType(AI1bl3_typeCombo, 1); // Установка для датчика влажности
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI1bl3_typeCombo, 0); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI1bl3_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI1bl3_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI1bl3combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI1bl3combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI1bl3combo_text, AI1bl3_combo); // Добавление к другим AI
                }
            }
            AI1bl3combo_text = AI1bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI1bl3combo_index = AI1bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI2 блока расширения 3 comboBox</summary>
        private void AI2bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI2bl3_combo.SelectedIndex == AI2bl3combo_index) return; // Индекс не поменялся
            if (AI2bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI2bl3_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI2bl3combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI2bl3combo_text)); // Удаление из списка
                    if (showCode) AI2bl3_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI2bl3combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI2bl3combo_text, AI2bl3_combo); // Добавление к другим AI
                SensorAIType(AI2bl3_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI2bl3_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI 
                    SensorAIType(AI2bl3_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "humidSensor")
                    SensorAIType(AI2bl3_typeCombo, 1); // Установка для датчика влажности
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI2bl3_typeCombo, 0); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI2bl3_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI2bl3_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI2bl3combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI2bl3combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI2bl3combo_text, AI2bl3_combo); // Добавление к другим AI
                }
            }
            AI2bl3combo_text = AI2bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI2bl3combo_index = AI2bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI3 блока расширения 3 comboBox</summary>
        private void AI3bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI3bl3_combo.SelectedIndex == AI3bl3combo_index) return; // Индекс не поменялся
            if (AI3bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI3bl3_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI3bl3combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI3bl3combo_text)); // Удаление из списка
                    if (showCode) AI3bl3_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI3bl3combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI3bl3combo_text, AI3bl3_combo); // Добавление к другим AI
                SensorAIType(AI3bl3_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI3bl3_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI 
                    SensorAIType(AI3bl3_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "humidSensor")
                    SensorAIType(AI3bl3_typeCombo, 1); // Установка для датчика влажности
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI3bl3_typeCombo, 0); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI3bl3_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI3bl3_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI3bl3combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI3bl3combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI3bl3combo_text, AI3bl3_combo); // Добавление к другим AI
                }
            }
            AI3bl3combo_text = AI3bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI3bl3combo_index = AI3bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI4 блока расширения 3 comboBox</summary>
        private void AI4bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI4bl3_combo.SelectedIndex == AI4bl3combo_index) return; // Индекс не поменялся
            if (AI4bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI4bl3_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI4bl3combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI4bl3combo_text)); // Удаление из списка
                    if (showCode) AI4bl3_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI4bl3combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI4bl3combo_text, AI4bl3_combo); // Добавление к другим AI
                SensorAIType(AI4bl3_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI4bl3_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI
                    SensorAIType(AI4bl3_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI4bl3_typeCombo, 3); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI4bl3_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI4bl3_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI4bl3combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI4bl3combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI4bl3combo_text, AI4bl3_combo); // Добавление к другим AI
                }
            }
            AI4bl3combo_text = AI4bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI4bl3combo_index = AI4bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI5 блока расширения 3 comboBox</summary>
        private void AI5bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI5bl3_combo.SelectedIndex == AI5bl3combo_index) return; // Индекс не поменялся
            if (AI5bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI5bl3_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI5bl3combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI5bl3combo_text)); // Удаление из списка
                    if (showCode) AI5bl3_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI5bl3combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI5bl3combo_text, AI5bl3_combo); // Добавление к другим AI
                SensorAIType(AI5bl3_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI5bl3_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI
                    SensorAIType(AI5bl3_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI5bl3_typeCombo, 3); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI5bl3_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI5bl3_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI5bl3combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI5bl3combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI5bl3combo_text, AI5bl3_combo); // Добавление к другим AI
                }
            }
            AI5bl3combo_text = AI5bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI5bl3combo_index = AI5bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Изменили AI6 блока расширения 3 comboBox</summary>
        private void AI6bl3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            string name = "";
            Ai ai_find = null;
            if (subAIcondition) return; // Переход из вычета сигналов AI
            if (AI6bl3_combo.SelectedIndex == AI6bl3combo_index) return; // Индекс не поменялся
            if (AI6bl3_combo.SelectedIndex == 0) // Выбрали "не выбрано"
            {
                if (AI6bl3_combo.Items.Count > 1) // Больше одного элемента в списке
                {
                    ai_find = list_ai.Find(x => x.Name == AI6bl3combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI6bl3combo_text)); // Удаление из списка
                    if (showCode) AI6bl3_lab.Text = "";
                    if (ai_find.Type == "di") // Если DI тип
                    {
                        AddToAllCombosDI(AI6bl3combo_text); // Добавление ко всем DI
                    }
                }
                if (ai_find != null) // Найден элемент
                {
                    ai_find.Dispose(); // Освобождение сигнала для распределенния
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);  // Добавление с новым значением
                }
                if (!initialComboSignals) AddtoCombosAI(AI6bl3combo_text, AI6bl3_combo); // Добавление к другим AI
                SensorAIType(AI6bl3_typeCombo, 2); // Установка NTC и блокировка
            }
            else // Если выбран сигнал AI
            {
                name = String.Concat(AI6bl3_combo.SelectedItem);
                ai_find = list_ai.Find(x => x.Name == name);
                list_ai.Remove(list_ai.Find(x => x.Name == name)); // Удаление из списка AI
                if (ai_find.Type == "di") // Если сигнал DI
                {
                    list_di.Find(x => x.Name == name).Select(); // Распределение сигнала из списка DI
                    RemoveFromAllCombosDI(name); // Удаление из всех comboBox DI
                    SensorAIType(AI6bl3_typeCombo, 2); // Установка NTC и блокировка
                }
                else if (ai_find.Type == "sensor")
                    SensorAIType(AI6bl3_typeCombo, 3); // Установка для датчика температуры
                if (ai_find != null)
                {
                    ai_find.Select();
                    if (!list_ai.Contains(ai_find))
                        list_ai.Add(ai_find);
                    if (showCode) AI6bl3_lab.Text = ai_find.Code.ToString();
                }
                if (!initialComboSignals) // Если не начальная расстановка
                {
                    SubFromCombosAI(name, AI6bl3_combo); // Удаление из других AI
                    ai_find = list_ai.Find(x => x.Name == AI6bl3combo_text);
                    list_ai.Remove(list_ai.Find(x => x.Name == AI6bl3combo_text));
                    if (ai_find != null)
                    {
                        ai_find.Dispose();
                        if (!list_ai.Contains(ai_find))
                            list_ai.Add(ai_find);
                    }
                    AddtoCombosAI(AI6bl3combo_text, AI6bl3_combo); // Добавление к другим AI
                }
            }
            AI6bl3combo_text = AI6bl3_combo.SelectedItem.ToString(); // Сохранение выбора в переменной
            AI6bl3combo_index = AI6bl3_combo.SelectedIndex; // Сохранение индекса
            CheckSignalsReady(); // Проверка распределения сигналов
        }

        ///<summary>Добавление освободившегося AI к остальным comboBox</summary>
        private void AddtoCombosAI(string name, ComboBox cm)
        {
            Ai ai_find;
            bool notFound = true; // Элемент в списке не найден
            // Для AI1 comboBox, добавление в остальные слоты для выбора
            if (AI1_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null)
                {
                    foreach (var elem in AI1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI1_combo.Items.Add(ai_find.Name); 
                    }
                    notFound = true;
                }
            }
            // Для AI2 comboBox, добавление в остальные слоты для выбора
            if (AI2_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null)
                {
                    foreach (var elem in AI2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI2_combo.Items.Add(ai_find.Name); 
                    }
                    notFound = true;
                }
            }
            // Для AI3 comboBox, добавление в остальные слоты для выбора
            if (AI3_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null)
                {
                    foreach (var elem in AI3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI3_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI4 comboBox, добавление в остальные слоты для выбора
            if (AI4_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null && ai_find.Type != "humidSensor")
                {
                    foreach (var elem in AI4_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI4_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI5 comboBox, добавление в остальные слоты для выбора
            if (AI5_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null && ai_find.Type != "humidSensor")
                {
                    foreach (var elem in AI5_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI5_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI6 comboBox, добавление в остальные слоты для выбора
            if (AI6_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null && ai_find.Type != "humidSensor")
                {
                    foreach (var elem in AI6_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI6_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI1 comboBox блока расширения 1, добавление в остальные слоты для выбора
            if (AI1bl1_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null)
                {
                    foreach (var elem in AI1bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI1bl1_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI2 comboBox блока расширения 1, добавление в остальные слоты для выбора
            if (AI2bl1_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null)
                {
                    foreach (var elem in AI2bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI2bl1_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI3 comboBox блока расширения 1, добавление в остальные слоты для выбора
            if (AI3bl1_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null)
                {
                    foreach (var elem in AI3bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI3bl1_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI4 comboBox блока расширения 1, добавление в остальные слоты для выбора
            if (AI4bl1_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null && ai_find.Type != "humidSensor")
                {
                    foreach (var elem in AI4bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI4bl1_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI5 comboBox блока расширения 1, добавление в остальные слоты для выбора
            if (AI5bl1_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null && ai_find.Type != "humidSensor")
                {
                    foreach (var elem in AI5bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI5bl1_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI6 comboBox блока расширения 1, добавление в остальные слоты для выбора
            if (AI6bl1_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null && ai_find.Type != "humidSensor")
                {
                    foreach (var elem in AI6bl1_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI6bl1_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI1 comboBox блока расширения 2, добавление в остальные слоты для выбора
            if (AI1bl2_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null)
                {
                    foreach (var elem in AI1bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI1bl2_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI2 comboBox блока расширения 2, добавление в остальные слоты для выбора
            if (AI2bl2_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null)
                {
                    foreach (var elem in AI2bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI2bl2_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI3 comboBox блока расширения 2, добавление в остальные слоты для выбора
            if (AI3bl2_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null)
                {
                    foreach (var elem in AI3bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI3bl2_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI4 comboBox блока расширения 2, добавление в остальные слоты для выбора
            if (AI4bl2_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null && ai_find.Type != "humidSensor")
                {
                    foreach (var elem in AI4bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI4bl2_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI5 comboBox блока расширения 2, добавление в остальные слоты для выбора
            if (AI5bl2_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null && ai_find.Type != "humidSensor")
                {
                    foreach (var elem in AI5bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI5bl2_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI6 comboBox блока расширения 2, добавление в остальные слоты для выбора
            if (AI6bl2_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null && ai_find.Type != "humidSensor")
                {
                    foreach (var elem in AI6bl2_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI6bl2_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI1 comboBox блока расширения 3, добавление в остальные слоты для выбора
            if (AI1bl3_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null)
                {
                    foreach (var elem in AI1bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI1bl3_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI2 comboBox блока расширения 3, добавление в остальные слоты для выбора
            if (AI2bl3_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null)
                {
                    foreach (var elem in AI2bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI2bl3_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI3 comboBox блока расширения 3, добавление в остальные слоты для выбора
            if (AI3bl3_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null)
                {
                    foreach (var elem in AI3bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI3bl3_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI4 comboBox блока расширения 3, добавление в остальные слоты для выбора
            if (AI4bl3_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null && ai_find.Type != "humidSensor")
                {
                    foreach (var elem in AI4bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI4bl3_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI5 comboBox блока расширения 3, добавление в остальные слоты для выбора
            if (AI5bl3_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null && ai_find.Type != "humidSensor")
                {
                    foreach (var elem in AI5bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI5bl3_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
            // Для AI6 comboBox блока расширения 3, добавление в остальные слоты для выбора
            if (AI6bl3_combo != cm)
            {
                ai_find = list_ai.Find(x => x.Name == name);
                if (ai_find != null && ai_find.Type != "humidSensor")
                {
                    foreach (var elem in AI6bl3_combo.Items) // Если нет такого названия
                        if (elem.ToString() == name) notFound = false;
                    if (notFound) // Нет такого элемента в списке
                    {
                        AI6bl3_combo.Items.Add(ai_find.Name);
                    }
                    notFound = true;
                }
            }
        }

        ///<summary>Добавление ко всем входам AI (для сигнала DI)</summary>
        private void AddToAllCombosAI(string name)
        {
            if (name != notSelected) // Кроме "Не выбрано"
            {
                // Основной ПЛК
                AI1_combo.Items.Add(name); AI2_combo.Items.Add(name);
                AI3_combo.Items.Add(name); AI4_combo.Items.Add(name);
                AI5_combo.Items.Add(name); AI6_combo.Items.Add(name);
                // Блок расширения 1
                AI1bl1_combo.Items.Add(name); AI2bl1_combo.Items.Add(name);
                AI3bl1_combo.Items.Add(name); AI4bl1_combo.Items.Add(name);
                AI5bl1_combo.Items.Add(name); AI6bl1_combo.Items.Add(name);
                // Блок расширения 2
                AI1bl2_combo.Items.Add(name); AI2bl2_combo.Items.Add(name);
                AI3bl2_combo.Items.Add(name); AI4bl2_combo.Items.Add(name);
                AI5bl2_combo.Items.Add(name); AI6bl2_combo.Items.Add(name);
                // Блок расширения 3
                AI1bl3_combo.Items.Add(name); AI2bl3_combo.Items.Add(name);
                AI3bl3_combo.Items.Add(name); AI4bl3_combo.Items.Add(name);
                AI5bl3_combo.Items.Add(name); AI6bl3_combo.Items.Add(name);
            }
        }

        ///<summary>Удаление из всех входов AI (для сигнала DI)</summary>
        private void RemoveFromAllCombosAI(string name)
        {
            // Основной ПЛК
            AI1_combo.Items.Remove(name); AI2_combo.Items.Remove(name);
            AI3_combo.Items.Remove(name); AI4_combo.Items.Remove(name);
            AI5_combo.Items.Remove(name); AI6_combo.Items.Remove(name);
            // Блок расширения 1
            AI1bl1_combo.Items.Remove(name); AI2bl1_combo.Items.Remove(name);
            AI3bl1_combo.Items.Remove(name); AI4bl1_combo.Items.Remove(name);
            AI5bl1_combo.Items.Remove(name); AI6bl1_combo.Items.Remove(name);
            // Блок расширения 2
            AI1bl2_combo.Items.Remove(name); AI2bl2_combo.Items.Remove(name);
            AI3bl2_combo.Items.Remove(name); AI4bl2_combo.Items.Remove(name);
            AI5bl2_combo.Items.Remove(name); AI6bl2_combo.Items.Remove(name);
            // Блок расширения 3
            AI1bl3_combo.Items.Remove(name); AI2bl3_combo.Items.Remove(name);
            AI3bl3_combo.Items.Remove(name); AI4bl3_combo.Items.Remove(name);
            AI5bl3_combo.Items.Remove(name); AI6bl3_combo.Items.Remove(name);
        }

        ///<summary>Удаление AI из других comboBox</summary> 
        private void SubFromCombosAI(string name, ComboBox cm)
        {
            if (name != notSelected) // Кроме "Не выбрано"
            {
                // Основной ПЛК
                if (AI1_combo != cm) AI1_combo.Items.Remove(name); // AI1
                if (AI2_combo != cm) AI2_combo.Items.Remove(name); // AI2
                if (AI3_combo != cm) AI3_combo.Items.Remove(name); // AI3
                if (AI4_combo != cm) AI4_combo.Items.Remove(name); // AI4
                if (AI5_combo != cm) AI5_combo.Items.Remove(name); // AI5
                if (AI6_combo != cm) AI6_combo.Items.Remove(name); // AI6
                // Блок расширения 1
                if (AI1bl1_combo != cm) AI1bl1_combo.Items.Remove(name); // AI1, блок 1
                if (AI2bl1_combo != cm) AI2bl1_combo.Items.Remove(name); // AI2, блок 1
                if (AI3bl1_combo != cm) AI3bl1_combo.Items.Remove(name); // AI3, блок 1
                if (AI4bl1_combo != cm) AI4bl1_combo.Items.Remove(name); // AI4, блок 1
                if (AI5bl1_combo != cm) AI5bl1_combo.Items.Remove(name); // AI5, блок 1
                if (AI6bl1_combo != cm) AI6bl1_combo.Items.Remove(name); // AI6, блок 1
                // Блок расширения 2
                if (AI1bl2_combo != cm) AI1bl2_combo.Items.Remove(name); // AI1, блок 2
                if (AI2bl2_combo != cm) AI2bl2_combo.Items.Remove(name); // AI2, блок 2
                if (AI3bl2_combo != cm) AI3bl2_combo.Items.Remove(name); // AI3, блок 2
                if (AI4bl2_combo != cm) AI4bl2_combo.Items.Remove(name); // AI4, блок 2
                if (AI5bl2_combo != cm) AI5bl2_combo.Items.Remove(name); // AI5, блок 2
                if (AI6bl2_combo != cm) AI6bl2_combo.Items.Remove(name); // AI6, блок 2
                // Блок расширения 3
                if (AI1bl3_combo != cm) AI1bl3_combo.Items.Remove(name); // AI1, блок 3
                if (AI2bl3_combo != cm) AI2bl3_combo.Items.Remove(name); // AI2, блок 3
                if (AI3bl3_combo != cm) AI3bl3_combo.Items.Remove(name); // AI3, блок 3
                if (AI4bl3_combo != cm) AI4bl3_combo.Items.Remove(name); // AI4, блок 3
                if (AI5bl3_combo != cm) AI5bl3_combo.Items.Remove(name); // AI5, блок 3
                if (AI6bl3_combo != cm) AI6bl3_combo.Items.Remove(name); // AI6, блок 3
            }
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

        ///<summary>Добавление нового AI и его назначнение под выход</summary>
        private void AddNewAI(ushort code, string type)
        {
            if (AI4_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor") // "Не выбрано", проверка типа
            { // AI4
                AI4_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI4_combo.SelectedIndex = AI4_combo.Items.Count - 1;
                AI4combo_text = AI4_combo.SelectedItem.ToString();
                AI4combo_index = AI4_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI4_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI4_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI4_typeCombo, 3);
                else if (type == "di") SensorAIType(AI4_typeCombo, 2);
            }
            else if (AI5_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor") // "Не выбрано", проверка типа
            { // AI5
                AI5_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI5_combo.SelectedIndex = AI5_combo.Items.Count - 1;
                AI5combo_text = AI5_combo.SelectedItem.ToString();
                AI5combo_index = AI5_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI5_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI5_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI5_typeCombo, 3);
                else if (type == "di") SensorAIType(AI5_typeCombo, 2);
            }
            else if (AI6_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor") // "Не выбрано", проверка типа
            { // AI6
                AI6_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI6_combo.SelectedIndex = AI6_combo.Items.Count - 1;
                AI6combo_text = AI6_combo.SelectedItem.ToString();
                AI6combo_index = AI6_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI6_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI6_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI6_typeCombo, 3);
                else if (type == "di") SensorAIType(AI6_typeCombo, 2);
            }
            else if (AI1_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI1
                AI1_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI1_combo.SelectedIndex = AI1_combo.Items.Count - 1;
                AI1combo_text = AI1_combo.SelectedItem.ToString();
                AI1combo_index = AI1_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI1_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI1_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI1_typeCombo, 0);
                else if (type == "humidSensor") SensorAIType(AI1_typeCombo, 1);
                else if (type == "di") SensorAIType(AI1_typeCombo, 2);
            }
            else if (AI2_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI2
                AI2_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI2_combo.SelectedIndex = AI2_combo.Items.Count - 1;
                AI2combo_text = AI2_combo.SelectedItem.ToString();
                AI2combo_index = AI2_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI2_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI2_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI2_typeCombo, 0);
                else if (type == "humidSensor") SensorAIType(AI2_typeCombo, 1);
                else if (type == "di") SensorAIType(AI2_typeCombo, 2);
            }
            else if (AI3_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI3
                AI3_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI3_combo.SelectedIndex = AI3_combo.Items.Count - 1;
                AI3combo_text = AI3_combo.SelectedItem.ToString();
                AI3combo_index = AI3_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI3_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI3_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI3_typeCombo, 0);
                else if (type == "humidSensor") SensorAIType(AI3_typeCombo, 1);
                else if (type == "di") SensorAIType(AI3_typeCombo, 2);
            }
            else if (AI4bl1_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor") // "Не выбрано", проверка типа
            { // AI4, блок 1
                AI4bl1_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI4bl1_combo.SelectedIndex = AI4bl1_combo.Items.Count - 1;
                AI4bl1combo_text = AI4bl1_combo.SelectedItem.ToString();
                AI4bl1combo_index = AI4bl1_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI4bl1_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI4bl1_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI4bl1_typeCombo, 3);
                else if (type == "di") SensorAIType(AI4bl1_typeCombo, 2);
            }
            else if (AI5bl1_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor") // "Не выбрано", проверка типа
            { // AI5, блок 1
                AI5bl1_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI5bl1_combo.SelectedIndex = AI5bl1_combo.Items.Count - 1;
                AI5bl1combo_text = AI5bl1_combo.SelectedItem.ToString();
                AI5bl1combo_index = AI5bl1_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI5bl1_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI5bl1_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI5bl1_typeCombo, 3);
                else if (type == "di") SensorAIType(AI5bl1_typeCombo, 2);
            }
            else if (AI6bl1_combo.SelectedIndex == 0 && list_ai.Find(x => x.Code == code).Type != "humidSensor") // "Не выбрано", проверка типа
            { // AI6, блок 1
                AI6bl1_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI6bl1_combo.SelectedIndex = AI6bl1_combo.Items.Count - 1;
                AI6bl1combo_text = AI6bl1_combo.SelectedItem.ToString();
                AI6bl1combo_index = AI6bl1_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI6bl1_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI6bl1_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI6bl1_typeCombo, 3);
                else if (type == "di") SensorAIType(AI6bl1_typeCombo, 2);
            }
            else if (AI1bl1_combo.SelectedIndex == 0) // "Не выбрано"
            { // AI1, блок 1
                AI1bl1_combo.Items.Add(list_ai.Find(x => x.Code == code).Name);
                // Выбор последнего добавленного элемента
                AI1bl1_combo.SelectedIndex = AI1bl1_combo.Items.Count - 1;
                AI1bl1combo_text = AI1bl1_combo.SelectedItem.ToString();
                AI1bl1combo_index = AI1bl1_combo.SelectedIndex;
                if (type == "di")
                {
                    if (showCode) AI1bl1_lab.Text = (code + 1000).ToString();
                }
                else
                {
                    if (showCode) AI1bl1_lab.Text = code.ToString();
                }
                list_ai.Find(x => x.Code == code).Select();
                // Изменения для выбора типа AI
                if (type == "sensor") SensorAIType(AI1bl1_typeCombo, 0);
                else if (type == "humidSensor") SensorAIType(AI1bl1_typeCombo, 1);
                else if (type == "di") SensorAIType(AI1bl1_typeCombo, 2);
            }
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
        private void ClearAITypeCombo(ComboBox cm)
        {
            cm.Items.Clear();
            cm.Items.Add(NTC);
            cm.SelectedIndex = 0;
            cm.Enabled = false;
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
                        if (AI1_combo.SelectedItem.ToString() != notSelected)
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
                        AI1_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI2_combo.SelectedItem.ToString() != notSelected)
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
                        AI2_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI3_combo.SelectedItem.ToString() != notSelected)
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
                        AI3_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI4_combo.SelectedItem.ToString() != notSelected)
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
                        AI4_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI5_combo.SelectedItem.ToString() != notSelected)
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
                        AI5_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI6_combo.SelectedItem.ToString() != notSelected)
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
                        AI6_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI1bl1_combo.SelectedItem.ToString() != notSelected)
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
                        AI1bl1_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI2bl1_combo.SelectedItem.ToString() != notSelected)
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
                        AI2bl1_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI3bl1_combo.SelectedItem.ToString() != notSelected)
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
                        AI3bl1_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI4bl1_combo.SelectedItem.ToString() != notSelected)
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
                        AI4bl1_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI5bl1_combo.SelectedItem.ToString() != notSelected)
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
                        AI5bl1_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI6bl1_combo.SelectedItem.ToString() != notSelected)
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
                        AI6bl1_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI1bl2_combo.SelectedItem.ToString() != notSelected)
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
                        AI1bl2_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI2bl2_combo.SelectedItem.ToString() != notSelected)
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
                        AI2bl2_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI3bl2_combo.SelectedItem.ToString() != notSelected)
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
                        AI3bl2_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI4bl2_combo.SelectedItem.ToString() != notSelected)
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
                        AI4bl2_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI5bl2_combo.SelectedItem.ToString() != notSelected)
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
                        AI5bl2_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI6bl2_combo.SelectedItem.ToString() != notSelected)
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
                        AI6bl2_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI1bl3_combo.SelectedItem.ToString() != notSelected)
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
                        AI1bl3_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI2bl3_combo.SelectedItem.ToString() != notSelected)
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
                        AI2bl3_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI3bl3_combo.SelectedItem.ToString() != notSelected)
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
                        AI3bl3_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI4bl3_combo.SelectedItem.ToString() != notSelected)
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
                        AI4bl3_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI5bl3_combo.SelectedItem.ToString() != notSelected)
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
                        AI5bl3_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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
                        if (AI6bl3_combo.SelectedItem.ToString() != notSelected)
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
                        AI6bl3_combo.SelectedItem = notSelected; // Выбор "Не выбрано"
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