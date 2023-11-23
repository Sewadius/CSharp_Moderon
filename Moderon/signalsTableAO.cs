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
            AO1bl1combo_text, AO2bl1combo_text,
            AO1bl2combo_text, AO2bl2combo_text,
            AO1bl3combo_text, AO2bl3combo_text;

        // Сохранение прошлого индекса comboBox элементов для ПЛК и блоков расширения
        int
            AO1combo_index, AO2combo_index, AO3combo_index,
            AO1bl1combo_index, AO2bl1combo_index,
            AO1bl2combo_index, AO2bl2combo_index,
            AO1bl3combo_index, AO2bl3combo_index;


        ///<summary>Начальная установка для сигналов AO таблицы сигналов</summary>
        public void Set_AOComboTextIndex()
        {
            var ao_signals = new List<string>()
            {
                AO1combo_text, AO2combo_text, AO3combo_text,
                AO1bl1combo_text, AO2bl1combo_text,
                AO1bl2combo_text, AO2bl2combo_text,
                AO1bl3combo_text, AO2bl3combo_text
            };

            var ao_signals_index = new List<int>()
            {
                AO1combo_index, AO2combo_index, AO3combo_index,
                AO1bl1combo_index, AO2bl1combo_index,
                AO1bl2combo_index, AO2bl2combo_index,
                AO1bl3combo_index, AO2bl3combo_index
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

        ///<summary>Добавление AO в другие слоты для выбора в comboBox</summary>
        private void AddToCombo_AO(string name, ComboBox cm, ref ComboBox comboBox)
        {
            bool notFound = true;                                               // Элемент в списке не найден изначально
            if (comboBox != cm)                                                 // Проверка текущего comboBox с проверяемым
            {
                Ao ao_find = list_ao.Find(x => x.Name == name);
                if (ao_find != null)                                            // Есть такой AO в списке аналоговых выходов
                {
                    foreach (var el in comboBox.Items)                          // Если нет такого названия в списке
                        if (el.ToString() == name) notFound = false;
                    if (notFound) comboBox.Items.Add(name);
                    notFound = true;
                }
            }
        }

        ///<summary>Добавление освободившегося AO к остальным comboBox</summary>
        private void AddtoCombosAO(string name, ComboBox cm)
        {
            AddToCombo_AO(name, cm, ref AO1_combo); AddToCombo_AO(name, cm, ref AO2_combo); AddToCombo_AO(name, cm, ref AO3_combo);             // ПЛК
            AddToCombo_AO(name, cm, ref AO1bl1_combo); AddToCombo_AO(name, cm, ref AO2bl1_combo);                                               // Блок 1
            AddToCombo_AO(name, cm, ref AO1bl2_combo); AddToCombo_AO(name, cm, ref AO2bl2_combo);                                               // Блок 2
            AddToCombo_AO(name, cm, ref AO1bl3_combo); AddToCombo_AO(name, cm, ref AO2bl3_combo);                                               // Блок 3
        }

        ///<summary>Удаление AO из других comboBox</summary>
        private void SubFromCombosAO(string name, ComboBox comboBox)
        {
            var ao_combos = new List<ComboBox>()
            {
                AO1_combo, AO2_combo, AO3_combo, AO1bl1_combo, AO2bl1_combo,
                AO1bl2_combo, AO2bl2_combo, AO1bl3_combo, AO2bl3_combo,  
            };

            foreach (var el in ao_combos)
                if (name != NOT_SELECTED && el != comboBox)
                    el.Items.Remove(name);
        }

        ///<summary>Добавление нового DO и его назначение для переданного comboBox</summary>
        private void SelectComboBox_AO(ComboBox cm, ushort code, Label label, string text, int index)
        {
            cm.Items.Add(list_ao.Find(x => x.Code == code).Name);
            cm.SelectedIndex = cm.Items.Count - 1;
            text = cm.SelectedItem.ToString();
            index = cm.SelectedIndex;
            if (showCode) label.Text = code.ToString();
            list_ao.Find(x => x.Code == code).Select();
        }

        ///<summary>Добавление нового AO и его назначение под выход, автораспределение</summary>
        private void AddNewAO(ushort code)
        {
            if (AO1_combo.SelectedIndex == 0) SelectComboBox_AO(AO1_combo, code, AO1_lab, AO1combo_text, AO1combo_index);
            else if (AO2_combo.SelectedIndex == 0) SelectComboBox_AO(AO2_combo, code, AO2_lab, AO2combo_text, AO2combo_index);
            else if (AO3_combo.SelectedIndex == 0 && AO3_combo.Visible) SelectComboBox_AO(AO3_combo, code, AO3_lab, AO3combo_text, AO3combo_index);
            else if (AO1bl1_combo.SelectedIndex == 0) SelectComboBox_AO(AO1bl1_combo, code, AO1bl1_lab, AO1bl1combo_text, AO1bl1combo_index);
            else if (AO2bl1_combo.SelectedIndex == 0) SelectComboBox_AO(AO2bl1_combo, code, AO2bl1_lab, AO2bl1combo_text, AO2bl1combo_index);
            else if (AO1bl2_combo.SelectedIndex == 0) SelectComboBox_AO(AO1bl2_combo, code, AO1bl2_lab, AO1bl2combo_text, AO1bl2combo_index);
            else if (AO2bl2_combo.SelectedIndex == 0) SelectComboBox_AO(AO2bl2_combo, code, AO2bl2_lab, AO2bl2combo_text, AO2bl2combo_index);
            else if (AO1bl3_combo.SelectedIndex == 0) SelectComboBox_AO(AO1bl3_combo, code, AO1bl3_lab, AO1bl3combo_text, AO1bl3combo_index);
            else if (AO2bl3_combo.SelectedIndex == 0) SelectComboBox_AO(AO2bl3_combo, code, AO2bl3_lab, AO2bl3combo_text, AO2bl3combo_index);
        }

        ///<summary>Удаление AO из определённого comboBox</summary>
        private void RemoveAO_FromComboBox(ComboBox comboBox, string name, Label label, string text, int index)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
                if (comboBox.Items[i].ToString() == name)                                                   // Есть совпадение по имени в списке
                {
                    comboBox.Items.Remove(name);                                                            // Удаление элемента по имени
                    if (comboBox.Items.Count > 1)                                                           // Осталось больше одного элемента в списке
                    {
                        comboBox.SelectedIndex = comboBox.Items.Count - 1;                                  // Выбор последнего элемента
                        if (comboBox.SelectedItem.ToString() == NOT_SELECTED)
                        {
                            SubFromCombosAO(comboBox.SelectedItem.ToString(), comboBox);                    // Удаление из других comboBox выбранного элемента
                            Ao find_ao = list_ao.Find(x => x.Name == comboBox.SelectedItem.ToString());     // Распределение выбранного AO из списка
                            if (find_ao != null)
                            {
                                list_ao.Remove(find_ao);
                                find_ao.Select();
                                list_ao.Add(find_ao);
                                if (showCode) label.Text = find_ao.Code.ToString();
                            }
                        }
                    }
                    else                                                                                    // Если в списке только "Не выбрано"
                    {
                        comboBox.SelectedItem = NOT_SELECTED;
                        label.Text = "";
                    }
                    text = comboBox.SelectedItem.ToString();                                                // Сохранение наименования выбранного AO
                    index = comboBox.SelectedIndex;                                                         // Сохранение индекса выбранного AO
                    break;
                }                                  

        }

        ///<summary>Удаление AO из всех comboBox</summary>
        private void SubFromCombosAO(ushort code)
        {
            string name = "";                                                                   // Текстовое название аналогового выхода

            Ao findAo = list_ao.Find(x => x.Code == code);                                      // Поиск имени аналогового выхода по числовому коду
            if (findAo != null) name = findAo.Name;                                             // Найдено текстовое название аналогового выхода по коду
            else return;                                                                        // Выход из метода

            subAOcondition = true;                                                              // Признак удаления AO, не работает событие indexChanged

            // ПЛК
            RemoveAO_FromComboBox(AO1_combo, name, AO1_lab, AO1combo_text, AO1combo_index);
            RemoveAO_FromComboBox(AO2_combo, name, AO2_lab, AO2combo_text, AO2combo_index);
            RemoveAO_FromComboBox(AO3_combo, name, AO3_lab, AO3combo_text, AO3combo_index);
            // Блок расширения 1
            RemoveAO_FromComboBox(AO1bl1_combo, name, AO1bl1_lab, AO1bl1combo_text, AO1bl1combo_index);
            RemoveAO_FromComboBox(AO2bl1_combo, name, AO2bl1_lab, AO2bl1combo_text, AO2bl1combo_index);
            // Блок расширения 2
            RemoveAO_FromComboBox(AO1bl2_combo, name, AO1bl2_lab, AO1bl2combo_text, AO1bl2combo_index);
            RemoveAO_FromComboBox(AO2bl2_combo, name, AO2bl2_lab, AO2bl2combo_text, AO2bl2combo_index);
            // Блок расширения 2
            RemoveAO_FromComboBox(AO1bl3_combo, name, AO1bl3_lab, AO1bl3combo_text, AO1bl3combo_index);
            RemoveAO_FromComboBox(AO2bl3_combo, name, AO2bl3_lab, AO2bl3combo_text, AO2bl3combo_index);

            subAOcondition = false;                 // Сброс признака удаления из AO
            list_ao.Remove(findAo);                 // Удаление сигнала из списка AO
            CheckSignalsReady();                    // Проверка распределения сигналов
        }

        ///<summary>Метод для добавления AO к списку сигналов</summary>
        private void AddToListAO(string name, ushort code)
        {
            list_ao.Add(new Ao(name, code));
            AddNewAO(code);
        }

        /*** ВЫБОР ЭЛЕМЕНТОВ ***/

        ///<summary>Выбрали ПЧ приточного вентилятора</summary>
        private void PrFanFC_check_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            if (prFanFC_check.Checked)                                          // Выбран ПЧ
            { 
                if (prFanControlCombo.SelectedIndex == 0)                       // Внешние сигналы
                    prFanSpeedCheck.Enabled = true;                             // Разблокировка выбора скорости
                else if (prFanControlCombo.SelectedIndex == 1)                  // Управление Modbus
                {
                    if (prFanSpeedCheck.Checked)                                // Отмена выбора скорости
                        prFanSpeedCheck.Checked = false;
                    prFanSpeedCheck.Enabled = false;                            // Блокировка выбора опции
                }
            }
            else                                                                // Отмена выбора ПЧ
                prFanSpeedCheck.Enabled = true;                                 // Разблокировка выбора опции
        }

        ///<summary>Изменили тип управления ПЧ приточного вентилятора</summary>
        private void PrFanControlCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (prFanFC_check.Checked)                                          // Если выбран ПЧ
            {
                if (prFanControlCombo.SelectedIndex == 0)                       // Внешние контакты
                {
                    prFanSpeedCheck.Enabled = true;                             // Разблокировка выбора опции скорости
                    prFanPowSupCheck.Enabled = true;                            // Разблокировка выбора опции питания
                }
                else if (prFanControlCombo.SelectedIndex == 1)                  // Modbus
                {
                    if (prFanSpeedCheck.Checked)                                // Снятие выбора опции
                        prFanSpeedCheck.Checked = false;
                    prFanSpeedCheck.Enabled = false;                            // Блокировка выбора опции скорости
                    prFanPowSupCheck.Enabled = false;                           // Блокировка опции выбора питания
                }
            }
            PrFanControlCombo_signalsDISelectedIndexChanged(this, e);           // Проверка для сигналов DI
            PrFanControlCombo_signalsDOSelectedIndexChanged(this, e);           // Проверка для сигналов DO
        }

        ///<summary>Выбрали резервный вентилятор приточного</summary>
        private void CheckResPrFan_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 3;                                                  // Скорость приточного вентилятора 2

            if (checkResPrFan.Checked && prFanSpeedCheck.Checked)               // Выбран резервный вентилятор и управление скоростью
                AddToListAO("Скорость приточного вентилятора 2", code_1);
            else                                                                // Отмена выбора резервного вентилятора
                SubFromCombosAO(code_1);                                        // Удаление AO из comboBox выходов
        }

        ///<summary>Выбрали ПЧ вытяжного вентилятора</summary>
        private void OutFanFC_check_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            if (outFanFC_check.Checked && comboSysType.SelectedIndex == 1)      // Выбран ПЧ, ПВ-система
            { 
                if (outFanControlCombo.SelectedIndex == 0)                      // Внешние контакты
                {
                    outFanSpeedCheck.Enabled = true;                            // Разблокировка выбора скорости
                }
                else if (outFanControlCombo.SelectedIndex == 1)                 // Управление Modbus
                {
                    if (outFanSpeedCheck.Checked)                               // Отмена выбора опции
                        outFanSpeedCheck.Checked = false;
                    outFanSpeedCheck.Enabled = false;                           // Блокировка опции
                }
            }
            else // Отмена выбора ПЧ
                outFanSpeedCheck.Enabled = true;                                // Разблокировка выбора скорости
        }

        ///<summary>Изменили тип управления ПЧ вытяжного вентилятора</summary>
        private void OutFanControlCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (comboSysType.SelectedIndex == 1 && outFanFC_check.Checked)      // Если выбран ПЧ, ПВ-система
            {
                if (outFanControlCombo.SelectedIndex == 0)                      // Внешние контакты
                {
                    outFanSpeedCheck.Enabled = true;                            // Разблокировка выбора скорости
                    outFanPowSupCheck.Enabled = true;                           // Разблокировка выбора питания
                }
                else if (outFanControlCombo.SelectedIndex == 1)                 // Modbus
                {
                    if (outFanSpeedCheck.Checked)                               // Отмена выбора опции скорости
                        outFanSpeedCheck.Checked = false;
                    outFanSpeedCheck.Enabled = false;                           // Блокировка выбора скорости
                    outFanPowSupCheck.Enabled = false;                          // Блокировка выбора питания
                }
            }
            OutFanControlCombo_signalsDISelectedIndexChanged(this, e);          // Проверка для сигналов DI
            OutFanControlCombo_signalsDOSelectedIndexChanged(this, e);          // Проверка для сигналов DO
        }

        ///<summary>Выбрали резервный вентилятор вытяжного</summary>
        private void CheckResOutFan_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 7;                                                  // Скорость вытяжного вентилятора 2

            if (checkResOutFan.Checked && comboSysType.SelectedIndex == 1)      // Выбран резервный вентилятор, ПВ-система
            {
                if (outFanSpeedCheck.Checked)                                   // Выбрана скорость для вытяжного вентилятора
                    AddToListAO("Скорость вытяжного вентилятора 2", code_1);
            }
            else                                                                // Отмена выбора резервного вентилятора
                SubFromCombosAO(code_1);                                        // Удаление AO из comboBox выходов
        }

        ///<summary>Выбрали нагреватель</summary>
        private void HeaterCheck_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 9, code_2 = 11;                                     // Водяной нагреватель 0-10 В, электронагреватель

            if (heaterCheck.Checked)                                            // Выбрали нагреватель
            {
                if (heatTypeCombo.SelectedIndex == 0)                           // Водяной нагреватель
                    AddToListAO("Водяной калорифер 0-10 В", code_1);
                else if (heatTypeCombo.SelectedIndex == 1)                      // Электрический нагреватель
                {
                    if (firstStHeatCombo.SelectedIndex == 1)                    // ШИМ 5 В
                        list_ao.Add(new Ao("Электрический калорифер ШИМ 5 В", code_2));
                    else if (firstStHeatCombo.SelectedIndex == 2)               // 0-10 В
                        list_ao.Add(new Ao("Электрический калорифер 0-10 В", code_2));
                    if (firstStHeatCombo.SelectedIndex != 0)                    // Если не дискретное управление
                        AddNewAO(code_2);                                       // Добавление AO к свободному comboBox выхода
                }
            } 
            else                                                                // Отмена выбора нагревателя
            {                                                                   // Удаление AO из comboBox выходов
                SubFromCombosAO(code_1); SubFromCombosAO(code_2);
            }
        }

        ///<summary>Изменили тип нагревателя</summary>
        private void HeatTypeCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 9, code_2 = 11;                                     // Водяной нагреватель 0-10 В, электронагреватель

            if (heaterCheck.Checked)                                            // Когда выбран нагреватель
            {
                if (heatTypeCombo.SelectedIndex == 0)                           // Водяной нагреватель
                {
                    SubFromCombosAO(code_2);                                    // Удаление для электронагревателя
                    System.Threading.Thread.Sleep(10);
                    AddToListAO("Водяной калорифер 0-10 В", code_1);
                }
                else if (heatTypeCombo.SelectedIndex == 1)                      // Электрический нагреватель
                {
                    SubFromCombosAO(code_1);                                    // Удаление для водяного калорифера
                    System.Threading.Thread.Sleep(10);
                    if (firstStHeatCombo.SelectedIndex == 1)                    // ШИМ 5 В
                        list_ao.Add(new Ao("Электрический калорифер ШИМ 5 В", code_2));
                    else if (firstStHeatCombo.SelectedIndex == 2)               // 0-10 В
                        list_ao.Add(new Ao("Электрический калорифер 0-10 В", code_2));
                    if (firstStHeatCombo.SelectedIndex != 0)                    // Если не дискретное управление
                        AddNewAO(code_2);                                       // Добавление AO к свободному comboBox выхода
                }
            }
        }

        ///<summary>Изменили тип управления первой ступенью нагревателя</summary>
        private void FirstStHeatCombo_SignalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 11;                                                 // Электронагреватель

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 1)        // Когда выбран электрический нагреватель
            { 
                if (firstStHeatCombo.SelectedIndex == 0)                        // Дискретное управление
                    SubFromCombosAO(code_1);                                    // Удаление сигнала
                else if (firstStHeatCombo.SelectedIndex == 1)                   // ШИМ, 5В
                {
                    SubFromCombosAO(code_1);                                    // Удаление сигнала
                    System.Threading.Thread.Sleep(10);
                    AddToListAO("Электрический калорифер ШИМ 5 В", code_1);
                }
                else if (firstStHeatCombo.SelectedIndex == 2)                   // Сигнал 0-10 В
                {
                    SubFromCombosAO(code_1);                                    // Удаление сигнала
                    System.Threading.Thread.Sleep(10);
                    AddToListAO("Электрический калорифер 0-10 В", code_1);
                }
            }
        }

        ///<summary>Выбрали догреватель</summary>
        private void AddHeatCheck_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 23, code_2 = 21;                                    // Водяной догреватель 0-10 В, электродогреватель

            if (addHeatCheck.Checked)                                           // Выбрали догреватель
            {
                if (heatAddTypeCombo.SelectedIndex == 0)                        // Водяной догреватель
                    AddToListAO("Водяной догреватель 0-10 В", code_1);          // Добавление AO к свободному comboBox выхода
                else if (heatAddTypeCombo.SelectedIndex == 1)                   // Электрическй догреватель
                {
                    if (firstStAddHeatCombo.SelectedIndex == 1)                 // ШИМ 5 В
                        list_ao.Add(new Ao("Электрический догреватель ШИМ 5 В", code_2));
                    else if (firstStAddHeatCombo.SelectedIndex == 2)            // 0-10 В
                        list_ao.Add(new Ao("Электрический догреватель 0-10 В", code_2));
                    if (firstStAddHeatCombo.SelectedIndex != 0)                 // Если не дискретное управление
                        AddNewAO(code_2);                                       // Добавление AO к свободному comboBox выхода
                }
            }
            else                                                                // Отмена выбора догревателя
            { 
                SubFromCombosAO(code_1); SubFromCombosAO(code_2);               // Удаление AO из comboBox выходов
            }
        }

        ///<summary>Изменили тип догревателя</summary>
        private void HeatAddTypeCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 23, code_2 = 21;                                    // Водяной догреватель 0-10 В, электродогреватель

            if (addHeatCheck.Checked) // Когда выбран догреватель
            {
                if (heatAddTypeCombo.SelectedIndex == 0)                        // Водяной догреватель
                {
                    SubFromCombosAO(code_2);                                    // Удаление для электродогревателя
                    System.Threading.Thread.Sleep(10);
                    AddToListAO("Водяной догреватель 0-10 В", code_1);          // Добавление AO к свободному comboBox выхода
                }
                else if (heatAddTypeCombo.SelectedIndex == 1)                   // Электрический догреватель
                {
                    SubFromCombosAO(code_1);                                    // Удаление для водяного догревателя
                    System.Threading.Thread.Sleep(10);
                    if (firstStAddHeatCombo.SelectedIndex == 1)                 // ШИМ 5 В
                        list_ao.Add(new Ao("Электрический догреватель ШИМ 5 В", code_2));
                    else if (firstStAddHeatCombo.SelectedIndex == 2)            // 0-10 В
                        list_ao.Add(new Ao("Электрический догреватель 0-10 В", code_2));
                    if (firstStAddHeatCombo.SelectedIndex != 0)                 // Если не дискретное управление
                        AddNewAO(code_2);                                       // Добавление AO к свободному comboBox выхода
                }
            }
        }

        ///<summary>Изменили тип управления первой ступенью догревателя</summary>
        private void FirstStAddHeatCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 21;                                                 // Электродогреватель

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 1)    // Когда выбран электрический догреватель
            { 
                if (firstStAddHeatCombo.SelectedIndex == 0)                     // Дискретное управление
                    SubFromCombosAO(code_1);                                    // Удаление сигнала
                else if (firstStAddHeatCombo.SelectedIndex == 1)                // ШИМ, 5В
                {
                    SubFromCombosAO(code_1);                                    // Удаление сигнала
                    System.Threading.Thread.Sleep(10);
                    AddToListAO("Электрический догреватель ШИМ 5 В", code_1);   // Добавление AO к свободному comboBox выхода
                }
                else if (firstStAddHeatCombo.SelectedIndex == 2)                // Сигнал 0-10 В
                {
                    SubFromCombosAO(code_1);                                    // Удаление сигнала
                    System.Threading.Thread.Sleep(10);
                    AddToListAO("Электрический догреватель 0-10 В", code_1);
                }
            }
        }

        ///<summary>Выбрали охладитель</summary>
        private void CoolerCheck_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 13;                                                     // Аналоговое управление охладителем

            if (coolerCheck.Checked)                                                // Выбрали охладитель
            {   
                if (coolTypeCombo.SelectedIndex == 1 || analogFreonCheck.Checked)   // Водяной охладитель, либо выбран сигнал 0-10 В фреон
                    AddToListAO("Охладитель 0-10 В", code_1);
            }
            else                                                                    // Отмена выбора охладителя
            {   
                if (coolTypeCombo.SelectedIndex == 1 || analogFreonCheck.Checked)   // Водяной охладитель, либо сигнал 0-10 В фреон
                    SubFromCombosAO(code_1);                                        // Удаление сигнала
            }
        }

        ///<summary>Изменили тип охладителя</summary>
        private void CoolTypeCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 13;                                                     // Аналоговое управление охладителем

            if (coolerCheck.Checked)                                                // Выбран охладитель
            {
                if (coolTypeCombo.SelectedIndex == 0)                               // Фреоновый
                {
                    if (!analogFreonCheck.Checked)                                  // Не было выбрано сигнала 0-10 В фреон
                        SubFromCombosAO(code_1);                                    // Удаление сигнала
                }
                else if (coolTypeCombo.SelectedIndex == 1)                          // Водяной
                {
                    if (!analogFreonCheck.Checked)                                  // Не было выбрано сигнала 0-10 В фреон
                        AddToListAO("Охладитель 0-10 В", code_1);
                }
            }
        }

        ///<summary>Выбрали сигнал 0-10 В для фреонового охладителя</summary>
        private void AnalogFreonCheck_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 13;                                                     // Аналоговое управление охладителем

            if (coolerCheck.Checked && coolTypeCombo.SelectedIndex == 0)            // Выбран фреоновый охладитель
            {
                if (analogFreonCheck.Checked)                                       // Выбрали сигнал 0-10 В
                    AddToListAO("Охладитель 0-10 В", code_1);
                else                                                                // Отмена выбора сигнала 0-10 В
                    SubFromCombosAO(code_1);                                        // Удаление сигнала
            }
        }

        ///<summary>Выбрали увлажнитель</summary>
        private void HumidCheck_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 25;                                                     // Аналоговое управление увлажнителем

            if (humidCheck.Checked && humidTypeCombo.SelectedIndex == 0)            // Выбрали паровой увлажнитель
                AddToListAO("Увлажнитель 0-10 В", code_1);
            else                                                                    // Отмена выбора увлажнителя
                SubFromCombosAO(code_1); // Удаление сигнала
        }

        ///<summary>Изменили тип увлажнителя</summary>
        private void HumidTypeCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 25;                                                     // Аналоговое управление увлажнителем

            if (humidCheck.Checked)                                                 // Когда выбран увлажнитель
            {
                if (humidTypeCombo.SelectedIndex == 0)                              // Паровой увлажнитель
                    AddToListAO("Увлажнитель 0-10 В", code_1);
                else if (humidTypeCombo.SelectedIndex == 1)                         // Сотовый увлажнитель
                    SubFromCombosAO(code_1);                                        // Удаление сигнала
            }
        }

        ///<summary>Выбрали рециркуляцию</summary>
        private void RecircCheck_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 14;                                                     // Аналоговое управление рециркуляцией

            if (recircCheck.Checked && comboSysType.SelectedIndex == 1)             // Выбрали рециркуляцию и ПВ-система
                AddToListAO("Рециркуляция 0-10 В", code_1);
            else                                                                    // Отмена выбора рециркуляции
                SubFromCombosAO(code_1);                                            // Удаление сигнала
        }

        ///<summary>Выбрали сигнал 0-10 В на приточную заслонку</summary>
        private void RecircPrDampAOCheck_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 12;                                                     // Сигнал 0-10 В на приточную заслонку

            if (recircCheck.Checked && recircPrDampAOCheck.Checked)
                AddToListAO("Рециркуляция для приточной заслонки 0-10 В", code_1);
            else
                SubFromCombosAO(code_1);
        }

        ///<summary>Выбрали рекуператор</summary>
        private void RecupCheck_signalsAOCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 15;                                                     // Аналоговое управление рекуператором

            if (recupCheck.Checked && comboSysType.SelectedIndex == 1)              // Выбрали рекуператор и ПВ-система
            {
                // Роторный, гликолевый или пластинчатый 0-10 В
                if (recupTypeCombo.SelectedIndex == 0 || recupTypeCombo.SelectedIndex == 2 ||
                        (recupTypeCombo.SelectedIndex == 1 && bypassPlastCombo.SelectedIndex == 2))
                    AddToListAO("Рекуператор 0-10 В", code_1);
            }
            else                                                                    // Отмена выбора рекуператора
                SubFromCombosAO(code_1);                                            // Удаление сигнала
        }

        ///<summary>Изменили тип рекуператора</summary>
        private void RecupTypeCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 15;                                                     // Аналоговое управление рекуператором

            if (recupCheck.Checked && comboSysType.SelectedIndex == 1)              // Выбрали рекуператор и ПВ-система
            { 
                SubFromCombosAO(code_1);                                            // Удаление сигнала
                System.Threading.Thread.Sleep(10);
                // Роторный или гликолевый рекуператор
                if (recupTypeCombo.SelectedIndex == 0 || recupTypeCombo.SelectedIndex == 2)
                    AddToListAO("Рекуператор 0-10 В", code_1);
                // Пластинчатый рекуператор и управление 0-10 В
                else if (recupTypeCombo.SelectedIndex == 1 && bypassPlastCombo.SelectedIndex == 2) 
                    AddToListAO("Рекуператор 0-10 В", code_1);
            }
        }

        ///<summary>Изменили тип управления пластинчатого рекуператора</summary>
        private void BypassPlastCombo_signalsAOSelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 15; // Аналоговое управление рекуператором

            // Выбран пластинчатый рекуператор и ПВ-система
            if (recupCheck.Checked && comboSysType.SelectedIndex == 1 && recupTypeCombo.SelectedIndex == 1)
            { 
                if (bypassPlastCombo.SelectedIndex == 2)                            // Управление 0-10 В
                    AddToListAO("Рекуператор 0-10 В", code_1);
                else                                                                // Другой тип управления
                    SubFromCombosAO(code_1);                                        // Удаление сигнала
            }
        }

        ///<summary>Выбрали управление скоростью для приточного вентилятора</summary>
        private void PrFanSpeedCheck_CheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1, code_2 = 3;                                          // Скорость приточного 1 и 2

            if (prFanSpeedCheck.Checked)                                            // Выбрали управление скоростью вентилятора
            {
                AddToListAO("Скорость приточного вентилятора 1", code_1);
                if (checkResPrFan.Checked)                                          // Если выбран резерв П
                    AddToListAO("Скорость приточного вентилятора 2", code_2);
            }
            else                                                                    // Отмена выбора управления скоростью
            {
                SubFromCombosAO(code_1); SubFromCombosAO(code_2);                   // Удаление сигналов
            }
        }

        ///<summary>Выбрали управление скоростью для вытяжного вентилятора</summary>
        private void OutFanSpeedCheck_CheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 5, code_2 = 7;                                          // Скорость вытяжного 1 и 2

            if (outFanSpeedCheck.Checked)                                           // Выбрали управление скоростью вентилятора
            {
                AddToListAO("Скорость вытяжного вентилятора 1", code_1);
                if (checkResPrFan.Checked)                                          // Если выбран резерв В
                    AddToListAO("Скорость вытяжного вентилятора 2", code_2);
            }
            else                                                                    // Отмена выбора управления скоростью
            {
                SubFromCombosAO(code_1); SubFromCombosAO(code_2);                   // Удаление сигналов
            }
        }
    }
}
