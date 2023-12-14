using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

// Файл для обработки алгоритма расстановки блоков расширения

namespace Moderon
{
    /// <summary>Класс для представления блока расширения</summary>
    class ExpBlock
    {
        public string Name { get; private set; }
        public short DO {  get; private set; }
        public short AO { get; private set; }
        public short UI { get; private set; }
        public ExpBlock(string name, short do_num, short ao_num, short ui_num)
        {
            Name = name; DO = do_num; AO = ao_num; UI = ui_num;
        }
    }

    public partial class Form1 : Form
    {
        private const int HEIGHT_DO_PANEL_BLOCK = 289;                  // Высота панели блока расширения для DO панели
        private const int HEIGHT_UI_PANEL_BLOCK = 537;                  // Высота панели блока расширения для UI панели
        private const int BETWEEN_PANELS = 12;                          // Расстояние между панелями

        /// <summary>Блоки расширения</summary>
        private ExpBlock
            M72E08RA = new ExpBlock("M72E08RA", 8, 0, 0),               // 8 DO, 0 AO, 0 UI
            M72E12RA = new ExpBlock("M72E12RA", 6, 0, 6),               // 6 DO, 0 AO, 6 UI
            M72E12RB = new ExpBlock("M72E12RB", 4, 2, 6),               // 4 DO, 2 AO, 6 UI
            M72E16NA = new ExpBlock("M72E16NA", 0, 0, 16);              // 0 DO, 0 AO, 16 UI

        /// <summary>Список для определения задействованных блоков расширения</summary>
        static private List<ExpBlock> expansion_blocks = new List<ExpBlock>();

        /// <summary>Изначальная блокировка и скрытие comboBox DO блоков расширение, скрытие подписей сигналов</summary>
        private void DoCombosBlocks_Reset()
        {
            DoCombosBlock_1_Reset();                // Блок расширения 1
            DoCombosBlock_2_Reset();                // Блок расширения 2
            DoCombosBlock_3_Reset();                // Блок расширения 3
        }

        /// <summary>Изначальная блокировка и скрытие comboBox UI блоков расширение, скрытие подписей сигналов</summary>
        private void UiCombosBlocks_Reset()
        {
            UiCombosBlock_1_Reset();                // Блок расширения 1
            UiCombosBlock_2_Reset();                // Блок расширения 2
            UiCombosBlock_3_Reset();                // Блок расширения 3
        }

        /// <summary>Расчёт типов и количества блоков по сигналам</summary>
        private Dictionary<ExpBlock, int> CalcExpBlocks_typeNums()
        {  
            Dictionary<ExpBlock, int> blocks = new Dictionary<ExpBlock, int>();
            ushort UI = 7, AO = 2, DO = 4;

            if (plkChangeIndexLast == 1) { UI = 11; AO = 3; DO = 6; }               // ПЛК Optimized

            // Проверка для добавления блоков расширения AO
            if (list_ao.Count > AO) blocks.Add(M72E12RB, 1);                        // Проверка для добавления 1-го блока расширения AO M72E12RB
            if (list_ao.Count > AO + M72E12RB.AO) blocks[M72E12RB] = 2;             // Проверка для добавления 2-го блока расширения AO M72E12RB
            if (list_ao.Count > AO + M72E12RB.AO * 2) blocks[M72E12RB] = 3;         // Проверка для добавления 3-го блока расширения AO M72E12RB

            return blocks;
        }

        ///<summary>Алгоритм перераспределения сигналов DO, UI и AO при смене типа контроллера</summary>
        private void RellocateSignals_DO_UI_AO_signals(short type, ComboBox cm)
        {
            Do do_find = null;                                                          // DO сигнал для поиска
            Ui ui_find = null;                                                          // UI сигнал для поиска
            Ao ao_find = null;                                                          // AO сигнал для поиска

            if (type == 1)                                                              // Для сигналов DO
                try { do_find = list_do.Find(x => x.Name == cm.SelectedItem.ToString()); }
                catch (NullReferenceException) { return; }
            else if (type == 2)                                                         // Для сигналов UI
                try { ui_find = list_ui.Find(x => x.Name == cm.SelectedItem.ToString()); }
                catch (NullReferenceException) { return; }
            else if (type == 3)                                                         // Для сигналов AO
                try { ao_find = list_ao.Find(x => x.Name == cm.SelectedItem.ToString()); }
                catch (NullReferenceException) { return; }

            cm.SelectedIndex = 0;                                                       // Сброс comboBox в "Не выбрано"
            if (do_find != null) AddNewDO(do_find.Code);                                // Распределение для сигнала DO
            if (ui_find != null) AddNewUI(ui_find.Code, ui_find.Type);                  // Распределение для сигнала UI
            if (ao_find != null) AddNewAO(ao_find.Code);                                // Распределение для сигнала AO
        }

        ///<summary>Перераспределения сигналов AO, DO, UI из списков comboBox</summary>
        private void RellocateSignals_fromLists(List<ComboBox> ao_signals, List<ComboBox> do_signals, List<ComboBox> ui_signals)
        {
            // Для сигналов AO
            foreach (var el in ao_signals)
                if (el.SelectedIndex != 0) RellocateSignals_DO_UI_AO_signals(3, el);       // Есть ранее выбранный сигнал AO 

            // Для сигналов DO
            foreach (var el in do_signals)
                if (el.SelectedIndex != 0) RellocateSignals_DO_UI_AO_signals(1, el);       // Есть ранее выбранный сигнал DO

            // Для сигналов UI
            foreach (var el in ui_signals)
                if (el.SelectedIndex != 0) RellocateSignals_DO_UI_AO_signals(2, el);       // Есть ранее выбранный сигнал UI
        }

        ///<summary>Проверка распределенных сигналов на Optimized при выборе Mini ПЛК</summary>
        private void CheckSignals_plkChange()
        {
            List<ComboBox> ao_signals = new List<ComboBox>() { AO3_combo };                                         // Сигналы AO
            List<ComboBox> do_signals = new List<ComboBox>() { DO5_combo, DO6_combo };                              // Сигналы DO
            List<ComboBox> ui_signals = new List<ComboBox>() { UI8_combo, UI9_combo, UI10_combo, UI11_combo };      // Сигналы UI

            // Перераспределение сигналов
            RellocateSignals_fromLists(ao_signals, do_signals, ui_signals);                                         
        }

        ///<summary>Проверка распределенных сигналов при удалении 1-го блока расширения M72E12RB сигналов AO</summary>
        private void CheckSignals_block1_M72E12RB()
        {
            List<ComboBox> ao_signals = new List<ComboBox>() { AO1bl1_combo, AO2bl1_combo };                                   // Сигналы AO
            List<ComboBox> do_signals = new List<ComboBox>() { DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo };       // Сигналы DO
            List<ComboBox> ui_signals = new List<ComboBox>() {                                                                 // Сигналы UI
                UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo
            };

            // Перераспределение сигналов
            RellocateSignals_fromLists(ao_signals, do_signals, ui_signals);
        }
        
        ///<summary>Проверка распределенных сигналов при удалении 2-го блока расширения M72E12RB сигналов AO</summary>
        private void CheckSignals_block2_M72E12RB()
        {
            List<ComboBox> ao_signals = new List<ComboBox>() { AO1bl2_combo, AO2bl2_combo };                                   // Сигналы AO
            List<ComboBox> do_signals = new List<ComboBox>() { DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo };       // Сигналы DO
            List<ComboBox> ui_signals = new List<ComboBox>() {                                                                 // Сигналы UI
                UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo
            };

            // Перераспределение сигналов
            RellocateSignals_fromLists(ao_signals, do_signals, ui_signals);
        }

        ///<summary>Проверка распределенных сигналов при удалении 3-го блока расширения M72E12RB сигналов AO</summary>
        private void CheckSignals_block3_M72E12RB()
        {
            List<ComboBox> ao_signals = new List<ComboBox>() { AO1bl3_combo, AO2bl3_combo };                                   // Сигналы AO
            List<ComboBox> do_signals = new List<ComboBox>() { DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo };       // Сигналы DO
            List<ComboBox> ui_signals = new List<ComboBox>() {                                                                 // Сигналы UI
                UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo
            };

            // Перераспределение сигналов
            RellocateSignals_fromLists(ao_signals, do_signals, ui_signals);
        }

        ///<summary>Изменение типа контроллера "Mini" или "Optimized"</summary>
        private void ComboPlkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ui_combos = new List<ComboBox>() { UI8_combo, UI9_combo, UI10_combo, UI11_combo };                          // UI_combo
            var ui_combos_type = new List<ComboBox>() { UI8_typeCombo, UI9_typeCombo, UI10_typeCombo, UI11_typeCombo };     // UI_typeCombo
            var ui_labels = new List<Label>() { UI8_plkLabel, UI9_plkLabel, UI10_plkLabel, UI11_plkLabel };                 // UI подписи сигналов
            var do_combos = new List<ComboBox>() { DO5_combo, DO6_combo };                                                  // DO_combo
            var do_labels = new List<Label>() { DO5_plkLabel, DO6_plkLabel };                                               // DO подписи сигналов 

            if (comboPlkType.SelectedIndex == plkChangeIndexLast) return;           // Выбранный индекс не изменился

            if (comboPlkType.SelectedIndex == 0)                                    // Выбрали контроллер "Mini"
            {
                plkChangeIndexLast = 0;                                             // Сохранение нового значения состояния
                foreach (var el in ui_combos)
                {
                    el.Hide(); el.Enabled = false;                                  // Скрытие и блокировка UI входных сигналов
                }
                foreach (var el in do_combos)
                {
                    el.Hide(); el.Enabled = false;                                  // Скрытие и блокировка DO comboBox выходных сигналов
                }
                foreach (var el in ui_combos_type) el.Hide();                       // Скрытие UI типов для входных сигналов
                foreach (var el in ui_labels) el.Hide();                            // Скрытие подписей для UI сигналов
                foreach (var el in do_labels) el.Hide();                            // Скрытие подписей для DO сигналов
                
                AO3_plkLabel.Hide(); AO3_combo.Hide(); AO3_combo.Enabled = false;   // Скрытие и блокировка AO3 выходного сигнала

                ChangeSizeLocationSignalsPanels(true);                              // Изменение размера и положения панелей
                CheckSignals_plkChange();                                           // Проверка распределённых сигналов на Optimized  
                AddFirstBlockAO_M72E12RB();                                         // Проверка на добавление 1-го блока расширения AO
                AddSecondBlockAO_M72E12RB();                                        // Проверка на добавление 2-го блока расширения AO
                AddThirdBlockAO_M72E12RB();                                         // Проверка на добавление 3-го блока расширения AO
            }
            else if (comboPlkType.SelectedIndex == 1)                               // Выбрали контроллер "Optimized"
            {
                plkChangeIndexLast = 1;                                             // Сохранение нового значения состояния
                foreach (var el in ui_combos)
                {
                    el.Show(); el.Enabled = true;                                   // Отображение и разблокировка UI входных сигналов                            
                }
                foreach (var el in do_combos)
                {
                    el.Show(); el.Enabled = true;                                   // Отображение и разблокировка DO comboBox выходных сигналов
                }
                foreach (var el in ui_combos_type) el.Show();                       // Отображение UI типов для входных сигналов
                foreach (var el in ui_labels) el.Show();                            // Отображение подписей для UI сигналов

                foreach (var el in do_labels) el.Show();                            // Отображение подписей для DO сигналов
                // Отображение и разблокировка AO3 выходного сигнала
                AO3_plkLabel.Show(); AO3_combo.Show(); AO3_combo.Enabled = true;

                ChangeSizeLocationSignalsPanels(false);                             // Изменение размера и положения панелей
                RemoveThirdBlockAO_M72E12RB();                                      // Проверка на удаление 3-го блока расширения AO
                RemoveSecondBlockAO_M72E12RB();                                     // Проверка на удаление 2-го блока расширения AO
                RemoveFirstBlockAO_M72E12RB();                                      // Проверка на удаление 1-го блока расширения AO
            }
        }

        ///<summary>Изменение панелей UI при добавлении блока расширения M72E12RB (6 UI)</summary>
        private void UI_panelBlock_M72E12RB_add(Panel panel)
        {
            if (panel == block1_UIpanel)                                            // Для блока расширения 1
            {
                UIblock1_header.Text = "Блок расширения 1 - M72E12RB";
                block1_UIpanel.Height = HEIGHT_UI_PANEL_BLOCK - 10 * DELTA;
                block1_UIpanel.Show(); block1_UIpanel.Enabled = true;
            }
            else if (panel == block2_UIpanel)                                       // Для блока расширения 2
            {
                UIblock2_header.Text = "Блок расширения 2 - M72E12RB";
                block2_UIpanel.Height = HEIGHT_UI_PANEL_BLOCK - 10 * DELTA;
                block2_UIpanel.Show(); block2_UIpanel.Enabled = true;

                // Положение по высоте для панели UI блока расширения 2
                block2_UIpanel.Location = new Point(block2_UIpanel.Location.X, plk_UIpanel.Height + 
                    block1_UIpanel.Height + BETWEEN_PANELS);
            } 
            else if (panel == block3_UIpanel)                                       // Для блока расширения 3
            {
                UIblock3_header.Text = "Блок расширения 3 - M72E12RB";
                block3_UIpanel.Height = HEIGHT_UI_PANEL_BLOCK - 10 * DELTA;
                block3_UIpanel.Show(); block3_UIpanel.Enabled = true;

                // Положение по высоте для панели UI блока расширения 2
                block3_UIpanel.Location = new Point(block3_UIpanel.Location.X, plk_UIpanel.Height +
                    block1_UIpanel.Height + block2_UIpanel.Height + BETWEEN_PANELS);
            }
        }

        ///<summary>Изменение панелей DO при добавлении блока расширения M72E12RB (6 UI)</summary>
        private void DO_panelBlock_M72E12RB_add(Panel panel)
        {
            if (panel == block1_DOpanel)
            {
                DOblock1_header.Text = "Блок расширения 1 - M72E12RB";
                block1_DOpanel.Height = HEIGHT_DO_PANEL_BLOCK - 4 * DELTA;
                block1_DOpanel.Show(); block1_DOpanel.Enabled = true;
            }
            else if (panel == block2_DOpanel)
            {
                DOblock2_header.Text = "Блок расширения 2 - M72E12RB";
                block2_DOpanel.Height = HEIGHT_DO_PANEL_BLOCK - 4 * DELTA;
                block2_DOpanel.Show(); block2_DOpanel.Enabled = true;

                // Положение по высоте для панели DO блока расширения
                block2_DOpanel.Location = new Point(block2_DOpanel.Location.X, plk_DOpanel.Height +
                    block1_DOpanel.Height + BETWEEN_PANELS);
            }
            else if (panel == block3_DOpanel)
            {
                DOblock3_header.Text = "Блок расширения 3 - M72E12RB";
                block3_DOpanel.Height = HEIGHT_DO_PANEL_BLOCK - 4 * DELTA;
                block3_DOpanel.Show(); block3_DOpanel.Enabled = true;

                // Положение по высоте для панели DO блока расширения
                block3_DOpanel.Location = new Point(block3_DOpanel.Location.X, plk_DOpanel.Height +
                    block1_DOpanel.Height + block2_DOpanel.Height + BETWEEN_PANELS);
            }   
        }

        ///<summary>Отображение и разблкировка comboBox и Labels UI панелей блоков расширения</summary>
        private void UI_showEnable_combos_labels(List<ComboBox> ui_combos, List<ComboBox> ui_type_combos, List<Label> ui_labels)
        {
            foreach (var el in ui_combos) { el.Show(); el.Enabled = true; }
            foreach (var el in ui_type_combos) el.Show();
            foreach (var el in ui_labels) el.Show();
        }

        ///<summary>Отображение и разблкировка comboBox и Labels DO панелей блоков расширения</summary>
        private void DO_showEnable_combos_labels(List<ComboBox> do_combos, List<Label> do_labels)
        {
            foreach (var el in do_combos) { el.Show(); el.Enabled = true; }
            foreach (var el in do_labels) el.Show();
        }

        ///<summary>Изменение панели UI блока расширения 1 под M72E12RB (6 UI)</summary>
        private void UI_block1_panelChanged_M72E12RB()
        {
            var ui_combos = new List<ComboBox>() { UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo };
            var ui_type_combos = new List<ComboBox>()
            {
                UI1bl1_typeCombo, UI2bl1_typeCombo, UI3bl1_typeCombo, UI4bl1_typeCombo, UI5bl1_typeCombo, UI6bl1_typeCombo
            };
            var ui_labels = new List<Label>() { UI1_bl1Label, UI2_bl1Label, UI3_bl1Label, UI4_bl1Label, UI5_bl1Label, UI6_bl1Label };

            UI_panelBlock_M72E12RB_add(block1_UIpanel);                             // Изменения для панели UI блока расширения 1
            UI_showEnable_combos_labels(ui_combos, ui_type_combos, ui_labels);      // Отображение и разблокировка элементов
        }

        ///<summary>Изменение панели UI блока расширения 2 под M72E12RB (6 UI)</summary>
        private void UI_block2_panelChanged_M72E12RB()
        {
            var ui_combos = new List<ComboBox>() { UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo };
            var ui_type_combos = new List<ComboBox>()
            {
                UI1bl2_typeCombo, UI2bl2_typeCombo, UI3bl2_typeCombo, UI4bl2_typeCombo, UI5bl2_typeCombo, UI6bl2_typeCombo
            };
            var ui_labels = new List<Label>() { UI1_bl2Label, UI2_bl2Label, UI3_bl2Label, UI4_bl2Label, UI5_bl2Label, UI6_bl2Label };

            UI_panelBlock_M72E12RB_add(block2_UIpanel);                             // Изменения для панели UI блока расширения 2
            UI_showEnable_combos_labels(ui_combos, ui_type_combos, ui_labels);      // Отображение и разблокировка элементов
        }

        ///<summary>Изменение панели UI блока расширения 3 под M72E12RB (6 UI)</summary>
        private void UI_block3_panelChanged_M72E12RB()
        {
            var ui_combos = new List<ComboBox>() { UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo };
            var ui_type_combos = new List<ComboBox>()
            {
                UI1bl3_typeCombo, UI2bl3_typeCombo, UI3bl3_typeCombo, UI4bl3_typeCombo, UI5bl3_typeCombo, UI6bl3_typeCombo
            };
            var ui_labels = new List<Label>() { UI1_bl3Label, UI2_bl3Label, UI3_bl3Label, UI4_bl3Label, UI5_bl3Label, UI6_bl3Label };

            UI_panelBlock_M72E12RB_add(block3_UIpanel);                             // Изменения для панели UI блока расширения 3
            UI_showEnable_combos_labels(ui_combos, ui_type_combos, ui_labels);      // Отображение и разблокировка элементов
        }

        ///<summary>Изменение панели DO блока расширения 1 под M72E12RB (4 DO)</summary>
        private void DO_block1_panelChanged_M72E12RB()
        {
            var do_combos = new List<ComboBox>() { DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo };    // DO сигналы
            var do_labels = new List<Label>() { DO1_bl1Label, DO2_bl1Label, DO3_bl1Label, DO4_bl1Label };       // Подписи DO сигналов

            DO_panelBlock_M72E12RB_add(block1_DOpanel);                             // Изменения для панели DO блока расширения 1
            DO_showEnable_combos_labels(do_combos, do_labels);                      // Отображение и разблокировка элементов
        }

        ///<summary>Изменение панели DO блока расширения 2 под M72E12RB (4 DO)</summary>
        private void DO_block2_panelChanged_M72E12RB()
        {
            var do_combos = new List<ComboBox>() { DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo };    // DO сигналы
            var do_labels = new List<Label>() { DO1_bl2Label, DO2_bl2Label, DO3_bl2Label, DO4_bl2Label };       // Подписи DO сигналов

            DO_panelBlock_M72E12RB_add(block2_DOpanel);                             // Изменения для панели DO блока расширения 2
            DO_showEnable_combos_labels(do_combos, do_labels);                      // Отображение и разблокировка элементов
        }

        ///<summary>Изменение панели DO блока расширения 3 под M72E12RB (4 DO)</summary>
        private void DO_block3_panelChanged_M72E12RB()
        {
            var do_combos = new List<ComboBox>() { DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo };    // DO сигналы
            var do_labels = new List<Label>() { DO1_bl3Label, DO2_bl3Label, DO3_bl3Label, DO4_bl3Label };       // Подписи DO сигналов

            DO_panelBlock_M72E12RB_add(block3_DOpanel);                             // Изменения для панели DO блока расширения 3
            DO_showEnable_combos_labels(do_combos, do_labels);                      // Отображение и разблокировка элементов
        }

        ///<summary>Проверка для добавления 1-го блока расширения M72E12RB сигналов AO</summary>
        private void AddFirstBlockAO_M72E12RB()
        {
            ushort count_AO_plk = 2;                                                // Количество 2 AO для ПЛК Mini 
            if (plkChangeIndexLast == 1) count_AO_plk = 3;                          // Количество 3 AO для ПЛК Optimized

            // Добавление блока M72E12RB в качестве 1-го блока расширения
            if (list_ao.Count > count_AO_plk && !expansion_blocks.Contains(M72E12RB) && expansion_blocks.Count == 0)
            {
                expansion_blocks.Add(M72E12RB);                                     // Добавление блока M72E12RB в список блоков расширения
                block1_AOpanel.Show(); block1_AOpanel.Enabled = true;               // Отображение и разблокировка панели AO для блока расширения 1
                DO_block1_panelChanged_M72E12RB();                                  // Изменение панели блока расширения 1 для сигналов DO
                UI_block1_panelChanged_M72E12RB();                                  // Изменение панели блока расширения 1 для сигналов UI
            }
        }

        ///<summary>Проверка для добавления 2-го блока расширения M72E12RB сигналов AO</summary>
        private void AddSecondBlockAO_M72E12RB()
        {
            if (!expansion_blocks.Contains(M72E12RB)) return;                      // Если нет первого блока расширения с AO

            ushort count_AO = 2;                                                   // Количество 2 AO для ПЛК Mini 
            if (plkChangeIndexLast == 1) count_AO = 3;                             // Количество 3 AO для ПЛК Optimized
            
            count_AO += (ushort)M72E12RB.AO;                                       // Добавление количества AO в первом блоке расширения

            // Добавление блока M72E12RB в качестве 2-го блока расширения
            if (list_ao.Count > count_AO && expansion_blocks.Contains(M72E12RB) && expansion_blocks.Count == 1)
            {
                expansion_blocks.Add(M72E12RB);                                    // Добавление блока M72E12RB в список блоков расширения
                block2_AOpanel.Show(); block2_AOpanel.Enabled = true;              // Отображение и разблокировка панели AO для блока расширения 2
                DO_block2_panelChanged_M72E12RB();                                 // Изменение панели блока расширения 2 для сигналов DO
                UI_block2_panelChanged_M72E12RB();                                 // Изменение панели блока расширения 2 для сигналов UI
            }
        }

        ///<summary>Проверка для добавления 3-го блока расширения M72E12RB сигналов AO</summary>
        private void AddThirdBlockAO_M72E12RB()
        {
            var count_M72E12RB = expansion_blocks.Where(x => x == M72E12RB).Count();    // Количество M72E12RB блока расширения в списке блоков
            if (count_M72E12RB != 2) return;                                            // Меньше двух блоков расширения M72E12RB

            ushort count_AO = 2;                                                        // Количество 2 AO для ПЛК Mini 
            if (plkChangeIndexLast == 1) count_AO = 3;                                  // Количество 3 AO для ПЛК Optimized

            count_AO += (ushort)(2 * M72E12RB.AO);                                      // Добавление количества AO для 1 и 2 блок расширения

            // Добавление блока M72E12RB в качестве 3-го блока расширения
            if (list_ao.Count > count_AO && count_M72E12RB == 2 && expansion_blocks.Count == 2) 
            {
                expansion_blocks.Add(M72E12RB);                                 // Добавление блока M72E12RB в список блоков расширения
                block3_AOpanel.Show(); block3_AOpanel.Enabled = true;           // Отображение и разблокировка панели AO для блока расширения 3
                DO_block3_panelChanged_M72E12RB();                              // Изменение панели блока расширения 3 для сигналов DO
                UI_block3_panelChanged_M72E12RB();                              // Изменение панели блока расширения 3 для сигналов UI
            }
        }

        ///<summary>Проверка для удаления 1-го блока расширения M72E12RB сигналов AO</summary>
        private void RemoveFirstBlockAO_M72E12RB()
        {
            ushort count_AO_plk = 2;                                                // Количество 2 AO для ПЛК Mini 
            if (plkChangeIndexLast == 1) count_AO_plk = 3;                          // Количество 3 AO для ПЛК Optimized

            // Условия для удаления M72E12RB в качестве 1-го блока расширения
            if (list_ao.Count <= count_AO_plk && expansion_blocks.Contains(M72E12RB) && expansion_blocks.Count == 1)
            {
                expansion_blocks.Remove(M72E12RB);                                  // Удаление блока из списка блоков
                CheckSignals_block1_M72E12RB();                                     // Автораспределение ранее выбранных сигналов с блока 1
                block1_AOpanel.Hide(); block1_AOpanel.Enabled = false;              // Скрытие и блокировка панели
                block1_DOpanel.Hide();                                              // Скрытие панели DO для блока расширения 1
                block1_UIpanel.Hide();                                              // Скрытие панели UI для блока расширения 1
                DoCombosBlock_1_Reset();                                            // Скрытие и блокировка элементов DO блока расширения 1
                UiCombosBlock_1_Reset();                                            // Скрытие и блокировка элементов UI блока расширения 1
            }
        }

        ///<summary>Проверка для удаления 2-го блока расширения M72E12RB сигналов AO</summary>
        private void RemoveSecondBlockAO_M72E12RB()
        {
            ushort count_AO = 2;                                                // Количество 2 AO для ПЛК Mini 
            if (plkChangeIndexLast == 1) count_AO = 3;                          // Количество 3 AO для ПЛК Optimized

            // Проверка количества AO в первом блоке расширения
            if (expansion_blocks.Contains(M72E12RB)) count_AO += (ushort)M72E12RB.AO;

            // Условия для удаления M72E12RB в качестве второго блока расширения
            var count_M72E12RB = expansion_blocks.Where(x => x == M72E12RB).Count();                    
            
            // Количество блоков расширения M72E12RB == 2
            if (list_ao.Count <= count_AO && count_M72E12RB == 2 && expansion_blocks.Count == 2)
            {
                expansion_blocks.Remove(M72E12RB);                                  // Удаление блока из списка блоков
                CheckSignals_block2_M72E12RB();                                     // Автораспределение ранее выбранных сигналов с блока 2
                block2_AOpanel.Hide(); block2_AOpanel.Enabled = false;              // Скрытие и блокировка панели
                block2_DOpanel.Hide();                                              // Скрытие панели DO для блока расширения 2
                block2_UIpanel.Hide();                                              // Скрытие панели UI для блока расширения 2
                DoCombosBlock_2_Reset();                                            // Скрытие и блокировка элементов DO блока расширения 2
                UiCombosBlock_2_Reset();                                            // Скрытие и блокировка элементов UI блока расширения 2
            }
        }

        ///<summary>Проверка для удаления 3-го блока расширения M72E12RB сигналов AO</summary>
        private void RemoveThirdBlockAO_M72E12RB()
        {
            ushort count_AO = 2;                                                // Количество 2 AO для ПЛК Mini 
            if (plkChangeIndexLast == 1) count_AO = 3;                          // Количество 3 AO для ПЛК Optimized

            // Добавление количество AO для двух блоков расширения
            var count_M72E12RB = expansion_blocks.Where(x => x == M72E12RB).Count();
            if (count_M72E12RB > 2) count_AO += (ushort)(2 * M72E12RB.AO);

            // Количество блоков расширения M72E12RB == 3
            if (list_ao.Count <= count_AO && count_M72E12RB == 3 && expansion_blocks.Count == 3)
            {
                expansion_blocks.Remove(M72E12RB);                                  // Удаление блока из списка блоков
                CheckSignals_block3_M72E12RB();                                     // Автораспределение ранее выбранных сигналов с блока 3
                block3_AOpanel.Hide(); block3_AOpanel.Enabled = false;              // Скрытие и блокировка панели
                block3_DOpanel.Hide();                                              // Скрытие панели DO для блока расширения 3
                block3_UIpanel.Hide();                                              // Скрытие панели UI для блока расширения 3
                DoCombosBlock_3_Reset();                                            // Скрытие и блокировка элементов DO блока расширения 3
                UiCombosBlock_3_Reset();                                            // Скрытие и блокировка элементов UI блока расширения 3
            }
        }

        ///<summary>Изменение размера и положения панелей при изменении типа контроллера</summary>
        private void ChangeSizeLocationSignalsPanels(bool flag)
        {
            if (flag)   // Для контроллера "Mini"
            {
                // Изменение размера и положения панелей для аналоговых выходов, AO
                plk_AOpanel.Height -= DELTA;                                                                            // AO для контроллера
                block1_AOpanel.Location = new Point(block1_AOpanel.Location.X, block1_AOpanel.Location.Y - DELTA);      // Блок 1, AO сигналы
                block2_AOpanel.Location = new Point(block2_AOpanel.Location.X, block2_AOpanel.Location.Y - DELTA);      // Блок 2, AO сигналы
                block3_AOpanel.Location = new Point(block3_AOpanel.Location.X, block3_AOpanel.Location.Y - DELTA);      // Блок 3, AO сигналы
                // Изменение размера и положения панелей для дискретных выходов, DO
                plk_DOpanel.Height -= DELTA * 2;                                                                        // DO для контроллера
                block1_DOpanel.Location = new Point(block1_DOpanel.Location.X, block1_DOpanel.Location.Y - DELTA * 2);  // Блок 1, DO сигналы
                block2_DOpanel.Location = new Point(block2_DOpanel.Location.X, block2_DOpanel.Location.Y - DELTA * 2);  // Блок 2, DO сигналы
                block3_DOpanel.Location = new Point(block3_DOpanel.Location.X, block3_DOpanel.Location.Y - DELTA * 2);  // Блок 3, DO сигналы
                // Изменение размера и положения панелей для универсальных выходов, UI
                plk_UIpanel.Height -= DELTA * 4;                                                                        // UI для контроллера
                block1_UIpanel.Location = new Point(block1_UIpanel.Location.X, block1_UIpanel.Location.Y - DELTA * 4);  // Блок 1, UI сигналы
                block2_UIpanel.Location = new Point(block2_UIpanel.Location.X, block2_UIpanel.Location.Y - DELTA * 4);  // Блок 2, UI сигналы
                block3_UIpanel.Location = new Point(block3_UIpanel.Location.X, block3_UIpanel.Location.Y - DELTA * 4);  // Блок 3, UI сигналы
            }
            else        // Для контроллера "Optimized"
            {
                // Изменение размера и положения панелей для аналоговых выходов, AO
                plk_AOpanel.Height += DELTA;                                                                            // AO для контроллера
                block1_AOpanel.Location = new Point(block1_AOpanel.Location.X, block1_AOpanel.Location.Y + DELTA);      // Блок 1, AO сигналы
                block2_AOpanel.Location = new Point(block2_AOpanel.Location.X, block2_AOpanel.Location.Y + DELTA);      // Блок 2, AO сигналы
                block3_AOpanel.Location = new Point(block3_AOpanel.Location.X, block3_AOpanel.Location.Y + DELTA);      // Блок 3, AO сигналы
                // Изменение размера и положения панелей для дискретных выходов, DO
                plk_DOpanel.Height += DELTA * 2;                                                                        // DO для контроллера
                block1_DOpanel.Location = new Point(block1_DOpanel.Location.X, block1_DOpanel.Location.Y + DELTA * 2);  // Блок 1, DO сигналы
                block2_DOpanel.Location = new Point(block2_DOpanel.Location.X, block2_DOpanel.Location.Y + DELTA * 2);  // Блок 2, DO сигналы
                block3_DOpanel.Location = new Point(block3_DOpanel.Location.X, block3_DOpanel.Location.Y + DELTA * 2);  // Блок 3, DO сигналы
                // Изменение размера и положения панелей для универсальных выходов, UI
                plk_UIpanel.Height += DELTA * 4;                                                                        // UI для контроллера
                block1_UIpanel.Location = new Point(block1_UIpanel.Location.X, block1_UIpanel.Location.Y + DELTA * 4);  // Блок 1, UI сигналы
                block2_UIpanel.Location = new Point(block2_UIpanel.Location.X, block2_UIpanel.Location.Y + DELTA * 4);  // Блок 2, UI сигналы
                block3_UIpanel.Location = new Point(block3_UIpanel.Location.X, block3_UIpanel.Location.Y + DELTA * 4);  // Блок 3, UI сигналы
            }
        }

        /// <summary>Блокировка и скрытие comboBox DO блока расширения 1, скрытие подписей сигналов</summary>
        private void DoCombosBlock_1_Reset()
        {
            var do_combos = new List<ComboBox>() {
                DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo, DO5bl1_combo, DO6bl1_combo, DO7bl1_combo, DO8bl1_combo
            };
            var do_labels = new List<Label>()
            {
                DO1_bl1Label, DO2_bl1Label, DO3_bl1Label, DO4_bl1Label, DO5_bl1Label, DO6_bl1Label, DO7_bl1Label, DO8_bl1Label
            };

            foreach (var el in do_combos) { el.SelectedIndex = 0; el.Hide(); el.Enabled = false; }
            foreach (var el in do_labels) el.Hide();
        }

        /// <summary>Блокировка и скрытие comboBox DO блока расширения 2, скрытие подписей сигналов</summary>
        private void DoCombosBlock_2_Reset()
        {
            var do_combos = new List<ComboBox>() {
                DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo, DO5bl2_combo, DO6bl2_combo, DO7bl2_combo, DO8bl2_combo
            };
            var do_labels = new List<Label>()
            {
                DO1_bl2Label, DO2_bl2Label, DO3_bl2Label, DO4_bl2Label, DO5_bl2Label, DO6_bl2Label, DO7_bl2Label, DO8_bl2Label
            };

            foreach (var el in do_combos) { el.Hide(); el.Enabled = false; }
            foreach (var el in do_labels) el.Hide();
        }

        /// <summary>Блокировка и скрытие comboBox DO блока расширения 3, скрытие подписей сигналов</summary>
        private void DoCombosBlock_3_Reset()
        {
            var do_combos = new List<ComboBox>() {
                DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo, DO5bl3_combo, DO6bl3_combo, DO7bl3_combo, DO8bl3_combo
            };
            var do_labels = new List<Label>()
            {
                DO1_bl3Label, DO2_bl3Label, DO3_bl3Label, DO4_bl3Label, DO5_bl3Label, DO6_bl3Label, DO7_bl3Label, DO8_bl3Label
            };

            foreach (var el in do_combos) { el.Hide(); el.Enabled = false; }
            foreach (var el in do_labels) el.Hide();
        }

        /// <summary>Блокировка и скрытие comboBox UI блока расширения 1, скрытие подписей сигналов</summary>
        private void UiCombosBlock_1_Reset()
        {
            var ui_combos = new List<ComboBox>()
            {
                UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo, UI7bl1_combo, UI8bl1_combo,
                UI9bl1_combo, UI10bl1_combo, UI11bl1_combo, UI12bl1_combo, UI13bl1_combo, UI14bl1_combo, UI15bl1_combo, UI16bl1_combo
            };
            var ui_type_combos = new List<ComboBox>()
            {
                UI1bl1_typeCombo, UI2bl1_typeCombo, UI3bl1_typeCombo, UI4bl1_typeCombo, UI5bl1_typeCombo, UI6bl1_typeCombo, UI7bl1_typeCombo,
                UI8bl1_typeCombo, UI9bl1_typeCombo, UI10bl1_typeCombo, UI11bl1_typeCombo, UI12bl1_typeCombo, UI13bl1_typeCombo,
                UI14bl1_typeCombo, UI15bl1_typeCombo, UI16bl1_typeCombo
            };
            var ui_labels = new List<Label>()
            {
                UI1_bl1Label, UI2_bl1Label, UI3_bl1Label, UI4_bl1Label, UI5_bl1Label, UI6_bl1Label, UI7_bl1Label, UI8_bl1Label, UI9_bl1Label,
                UI10_bl1Label, UI11_bl1Label, UI12_bl1Label, UI13_bl1Label, UI14_bl1Label, UI15_bl1Label, UI16_bl1Label
            };

            UIblock1_header.Text = "";  // Очистка заголовка для панели UI блока расширения 1

            foreach (var el in ui_combos) { el.SelectedIndex = 0; el.Hide(); el.Enabled = false; }
            foreach (var el in ui_type_combos) { el.SelectedIndex = 0; el.Hide(); el.Enabled = false; }
            foreach (var el in ui_labels) el.Hide();
        }

        /// <summary>Изначальная блокировка и скрытие comboBox UI блока расширения 2, скрытие подписей сигналов</summary>
        private void UiCombosBlock_2_Reset()
        {
            var ui_combos = new List<ComboBox>()
            {
                UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo, UI7bl2_combo, UI8bl2_combo,
                UI9bl2_combo, UI10bl2_combo, UI11bl2_combo, UI12bl2_combo, UI13bl2_combo, UI14bl2_combo, UI15bl2_combo, UI16bl2_combo
            };
            var ui_type_combos = new List<ComboBox>()
            {
                UI1bl2_typeCombo, UI2bl2_typeCombo, UI3bl2_typeCombo, UI4bl2_typeCombo, UI5bl2_typeCombo, UI6bl2_typeCombo, UI7bl2_typeCombo,
                UI8bl2_typeCombo, UI9bl2_typeCombo, UI10bl2_typeCombo, UI11bl2_typeCombo, UI12bl2_typeCombo, UI13bl2_typeCombo,
                UI14bl2_typeCombo, UI15bl2_typeCombo, UI16bl2_typeCombo
            };
            var ui_labels = new List<Label>()
            {
                UI1_bl2Label, UI2_bl2Label, UI3_bl2Label, UI4_bl2Label, UI5_bl2Label, UI6_bl2Label, UI7_bl2Label, UI8_bl2Label, UI9_bl2Label,
                UI10_bl2Label, UI11_bl2Label, UI12_bl2Label, UI13_bl2Label, UI14_bl2Label, UI15_bl2Label, UI16_bl2Label
            };

            UIblock2_header.Text = "";  // Очистка заголовка для панели UI блока расширения 2

            foreach (var el in ui_combos) { el.SelectedIndex = 0; el.Hide(); el.Enabled = false; }
            foreach (var el in ui_type_combos) { el.SelectedIndex = 0; el.Hide(); el.Enabled = false; }
            foreach (var el in ui_labels) el.Hide();
        }

        /// <summary>Изначальная блокировка и скрытие comboBox UI блока расширения 3, скрытие подписей сигналов</summary>
        private void UiCombosBlock_3_Reset()
        {
            var ui_combos = new List<ComboBox>()
            {
                UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo, UI7bl3_combo, UI8bl3_combo,
                UI9bl3_combo, UI10bl3_combo, UI11bl3_combo, UI12bl3_combo, UI13bl3_combo, UI14bl3_combo, UI15bl3_combo, UI16bl3_combo
            };
            var ui_type_combos = new List<ComboBox>()
            {
                UI1bl3_typeCombo, UI2bl3_typeCombo, UI3bl3_typeCombo, UI4bl3_typeCombo, UI5bl3_typeCombo, UI6bl3_typeCombo, UI7bl3_typeCombo,
                UI8bl3_typeCombo, UI9bl3_typeCombo, UI10bl3_typeCombo, UI11bl3_typeCombo, UI12bl3_typeCombo, UI13bl3_typeCombo,
                UI14bl3_typeCombo, UI15bl3_typeCombo, UI16bl3_typeCombo
            };
            var ui_labels = new List<Label>()
            {
                UI1_bl3Label, UI2_bl3Label, UI3_bl3Label, UI4_bl3Label, UI5_bl3Label, UI6_bl3Label, UI7_bl3Label, UI8_bl3Label, UI9_bl3Label,
                UI10_bl3Label, UI11_bl3Label, UI12_bl3Label, UI13_bl3Label, UI14_bl3Label, UI15_bl3Label, UI16_bl3Label
            };

            foreach (var el in ui_combos) { el.SelectedIndex = 0; el.Hide(); el.Enabled = false; }
            foreach (var el in ui_type_combos) { el.SelectedIndex = 0; el.Hide(); el.Enabled = false; }
            foreach (var el in ui_labels) el.Hide();
        }
    }
}
