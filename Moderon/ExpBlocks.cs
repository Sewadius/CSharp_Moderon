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
        private const int 
            HEIGHT_DO_PANEL_BLOCK = 289,                  // Высота панели блока расширения для DO панели
            HEIGHT_UI_PANEL_BLOCK = 537,                  // Высота панели блока расширения для UI панели
            HEIGHT_AO_PANEL_BLOCK = 99,                   // Высота панели блока расширения для AO панели
            BETWEEN_PANELS = 12;                          // Расстояние между панелями

        /// <summary>Блоки расширения</summary>
        private ExpBlock
            M72E08RA = new("M72E08RA", 8, 0, 0),               // 8 DO, 0 AO, 0 UI
            M72E12RA = new("M72E12RA", 6, 0, 6),               // 6 DO, 0 AO, 6 UI
            M72E12RB = new("M72E12RB", 4, 2, 6),               // 4 DO, 2 AO, 6 UI
            M72E16NA = new("M72E16NA", 0, 0, 16);              // 0 DO, 0 AO, 16 UI

        /// <summary>Список для определения задействованных блоков расширения</summary>
        static private List<ExpBlock> expansion_blocks = [];

        /// <summary>Изначальная блокировка и скрытие comboBox DO блоков расширение, скрытие подписей сигналов</summary>
        private void DoCombosBlocks_Reset()
        {
            DOCombosBlock_1_Reset();                // Блок расширения 1
            DOCombosBlock_2_Reset();                // Блок расширения 2
            DOCombosBlock_3_Reset();                // Блок расширения 3
        }

        /// <summary>Изначальная блокировка и скрытие comboBox UI блоков расширение, скрытие подписей сигналов</summary>
        private void UiCombosBlocks_Reset()
        {
            UICombosBlock_1_Reset();                // Блок расширения 1
            UICombosBlock_2_Reset();                // Блок расширения 2
            UICombosBlock_3_Reset();                // Блок расширения 3
        }

        ///<summary>Скрытие панелей для блоков расширения</summary>
        private void HidePanelBlocks()
        {
            List<Panel> panels =
            [
                block1_UIpanel, block2_UIpanel, block3_UIpanel,
                block1_AOpanel, block2_AOpanel, block3_AOpanel,
                block1_DOpanel, block2_DOpanel, block3_DOpanel
            ];

            foreach (var el in panels) { el.Hide(); el.Enabled = false; }
        }

        ///<summary>Скрытие элементов для панели блоков расширения</summary>
        private void Hide_panelBlocks_elements()
        {
            List<Label> blocks_labels = [
                M72E08RA_label, M72E12RA_label, M72E12RB_label, M72E16NA_label
            ];

            foreach (var el in blocks_labels) el.Hide();
            panelBlocks.Hide();
        }

        ///<summary>Сброс заголовков для панелей блоков расширения</summary>
        private void ClearPanelHeaders()
        {
            List<Label> headers_labels = [
                AOblock1_header, AOblock2_header, AOblock3_header,
                DOblock1_header, DOblock2_header, DOblock3_header,
                UIblock1_header, UIblock2_header, UIblock3_header
            ];

            foreach (var el in headers_labels) el.Text = "";
        }

        ///<summary>Скрытие подписей элементов для панели блоков расширения</summary>
        private void Hide_panelBlocks_labels(List<Label> labels)
        {
            // Подписи для блоков расширения
            List<Label> initial_labels = [M72E08RA_label, M72E12RA_label, M72E12RB_label, M72E16NA_label];

            foreach (var el in initial_labels)
                if (!labels.Contains(el)) el.Hide();
        }

        ///<summary>Отображение подписи блока для панели блоков расширения</summary>
        private Label LabelShow_panelBlocks(Point point, Label label, ExpBlock expBlock, Dictionary<ExpBlock, int> blocks)
        {
            string[] text = ["M72E12RB - ", "M72E12RA - ", "M72E08RA - ", "M72E16NA - "];

            if (label == M72E12RB_label) label.Text = text[0];          // M72E12RB
            if (label == M72E12RA_label) label.Text = text[1];          // M72E12RA
            if (label == M72E08RA_label) label.Text = text[2];          // M72E08RA
            if (label == M72E16NA_label) label.Text = text[3];          // M72E16NA

            label.Text += blocks[expBlock].ToString() + " шт.";         // Формирование подписи
            label.Show();                                               // Отображение подписи
            label.Location = point;                                     // Установление положения

            return label;
        }

        ///<summary>Проверка отображения, тип и количество блоков для панели блоков расширения</summary>
        private void CheckPanelBlocks(Dictionary<ExpBlock, int> blocks)
        {
            int DELTA = 25;                                                                         // Расстояние между подписями для блоков

            // Положение подписей для блоков расширения
            Point first = new(13, 40);                                                              // Положение для первой подписи
            Point second = new(first.X, first.Y + DELTA);                                           // Положение для второй подписи
            Point third = new(second.X, second.Y + DELTA);                                          // Положение для третьей подписи

            List<Label> labels_to_show = [];                                                        // Подписи для отображения
            Hide_panelBlocks_elements();                                                            // Скрытие панели блоков и подписей изначально
            if (blocks.Count > 0 && panelElements.Visible) panelBlocks.Show();                      // Отображение панели блоков расширения

            if (blocks.Count == 1)                                                                  // Один тип блока расширения
            {
                if (blocks.ContainsKey(M72E12RB))                                                   // Содержит блок расширения AO                    
                    labels_to_show.Add(LabelShow_panelBlocks(first, M72E12RB_label, M72E12RB, blocks));
                else if (blocks.ContainsKey(M72E12RA))                                              // Содержит блок UI + DO
                    labels_to_show.Add(LabelShow_panelBlocks(first, M72E12RA_label, M72E12RA, blocks));
                else if (blocks.ContainsKey(M72E08RA))                                              // Содержит блок DO
                    labels_to_show.Add(LabelShow_panelBlocks(first, M72E08RA_label, M72E08RA, blocks));
                else if (blocks.ContainsKey(M72E16NA))                                              // Содержит блок UI
                    labels_to_show.Add(LabelShow_panelBlocks(first, M72E16NA_label, M72E16NA, blocks));
            }
            else if (blocks.Count == 2)                                                             // Два типа блоков расширения
            {
                if (blocks.ContainsKey(M72E12RB))                                                   // Первый блок AO
                {
                    labels_to_show.Add(LabelShow_panelBlocks(first, M72E12RB_label, M72E12RB, blocks));
                    if (blocks.ContainsKey(M72E12RA))                                               // Второй блок UI + DO
                        labels_to_show.Add(LabelShow_panelBlocks(second, M72E12RA_label, M72E12RA, blocks));
                    else if (blocks.ContainsKey(M72E08RA))                                          // Второй блок DO
                        labels_to_show.Add(LabelShow_panelBlocks(second, M72E08RA_label, M72E08RA, blocks));
                    else if (blocks.ContainsKey(M72E16NA))                                          // Второй блок UI
                        labels_to_show.Add(LabelShow_panelBlocks(second, M72E16NA_label, M72E16NA, blocks));
                }
                else if (blocks.ContainsKey(M72E12RA))                                              // Первый блок UI + DO
                {
                    labels_to_show.Add(LabelShow_panelBlocks(first, M72E12RA_label, M72E12RA, blocks));
                    if (blocks.ContainsKey(M72E08RA))                                               // Второй блок DO
                        labels_to_show.Add(LabelShow_panelBlocks(second, M72E08RA_label, M72E08RA, blocks));
                    else if (blocks.ContainsKey(M72E16NA))                                          // Второй блок UI
                        labels_to_show.Add(LabelShow_panelBlocks(second, M72E16NA_label, M72E16NA, blocks));
                }
            }
            else if (blocks.Count == 3)                                                             // Три типа блоков расширения
            {
                if (blocks.ContainsKey(M72E12RB))                                                   // Первый блок AO
                {
                    labels_to_show.Add(LabelShow_panelBlocks(first, M72E12RB_label, M72E12RB, blocks));
                    if (blocks.ContainsKey(M72E12RA))                                               // Второй блок UI + DO
                    {
                        labels_to_show.Add(LabelShow_panelBlocks(second, M72E12RA_label, M72E12RA, blocks));
                        if (blocks.ContainsKey(M72E08RA))                                           // Третий блок DO
                            labels_to_show.Add(LabelShow_panelBlocks(third, M72E08RA_label, M72E08RA, blocks));
                        else if (blocks.ContainsKey(M72E16NA))                                      // Третий блок UI
                            labels_to_show.Add(LabelShow_panelBlocks(third, M72E16NA_label, M72E16NA, blocks));
                    }
                }
                else if (blocks.ContainsKey(M72E12RA))                                              // Первый блок UI + DO
                {
                    labels_to_show.Add(LabelShow_panelBlocks(first, M72E12RA_label, M72E12RA, blocks));
                    if (blocks.ContainsKey(M72E08RA))                                               // Второй блок DO
                        labels_to_show.Add(LabelShow_panelBlocks(second, M72E08RA_label, M72E08RA, blocks));
                    if (blocks.ContainsKey(M72E16NA))                                               // Третий блок UI
                        labels_to_show.Add(LabelShow_panelBlocks(third, M72E16NA_label, M72E16NA, blocks));
                }
            }
            Hide_panelBlocks_labels(labels_to_show);                                                // Скрытие для других блоков расширения
        }

        /// <summary>Расчёт типов и количества блоков по выбранным сигналам</summary>
        private Dictionary<ExpBlock, int> CalcExpBlocks_typeNums()
        {
            Dictionary<ExpBlock, int> blocks = [];
            ushort UI = 7, AO = 2, DO = 4;                                                                                  // ПЛК Mini

            if (plkChangeIndexLast == 1) { UI = 11; AO = 3; DO = 6; }                                                       // ПЛК Optimized

            // Нет блоков расширения
            if (blocks.Count == 0)                                                                                          // Словарь пуст
            {
                if (list_ao.Count > AO) blocks.Add(M72E12RB, 1);                                                            // AO M72E12RB
                else if (list_do.Count > DO && list_ui.Count > UI) blocks.Add(M72E12RA, 1);                                 // DO + UI M72E12RA
                else if (list_do.Count > DO) blocks.Add(M72E08RA, 1);                                                       // DO M72E08RA
                else if (list_ui.Count > UI) blocks.Add(M72E16NA, 1);                                                       // UI M72E16NA
            }

            // Есть блок расширения 1-го типа
            if (blocks.Count == 1)                                                                                          // Есть 1 тип блоков расширения
            {
                if (blocks.ContainsKey(M72E12RB))                                                                           // 1-й блок AO M72E12RB
                {
                    if (list_ao.Count > AO + M72E12RB.AO)
                    {
                        blocks[M72E12RB] = 2;                                                                               // AO M72E12RB, 2-й блок
                        if (list_ao.Count > AO + M72E12RB.AO * 2) blocks[M72E12RB] = 3;                                     // AO M72E12RB, 3-й блок
                        else if (list_do.Count > DO + M72E12RB.DO * 2 && list_ui.Count > UI + M72E12RB.UI * 2)
                            blocks.Add(M72E12RA, 1);                                                                        // 3-й блок DO + UI M72E12RA
                        else if (list_do.Count > DO + M72E12RB.DO * 2) blocks.Add(M72E08RA, 1);                             // 3-й блок DO M72E08RA
                        else if (list_ui.Count > UI + M72E12RB.UI * 2) blocks.Add(M72E16NA, 1);                             // 3-й блок UI M72E16NA
                    }
                    else if (list_do.Count > DO + M72E12RB.DO && list_ui.Count > UI + M72E12RB.UI)                          // 2-й блок DO + UI M72E12RA
                        blocks.Add(M72E12RA, 1);
                    else if (list_do.Count > DO + M72E12RB.DO) blocks.Add(M72E08RA, 1);                                     // 2-й блок DO M72E08RA
                    else if (list_ui.Count > UI + M72E12RB.UI) blocks.Add(M72E16NA, 1);                                     // 2-й блок UI M72E16NA
                }
                else if (blocks.ContainsKey(M72E12RA))                                                                      // 1-й блок DO + UI M72E12RA
                {
                    if (list_do.Count > DO + M72E12RA.DO && list_ui.Count > UI + M72E12RA.UI)
                    {
                        blocks[M72E12RA] = 2;                                                                               // DO + UI M72E12RA, 2-й блок
                        if (list_do.Count > DO + M72E12RA.DO * 2 && list_ui.Count > UI + M72E12RA.UI * 2)
                            blocks[M72E12RA] = 3;                                                                           // DO + UI M72E12RA, 3-й блок
                    }
                    else if (list_do.Count > DO + M72E12RA.DO) blocks.Add(M72E08RA, 1);                                     // DO M72E08RA
                    else if (list_ui.Count > UI + M72E12RA.UI) blocks.Add(M72E16NA, 1);                                     // UI M72E16NA
                }
                else if (blocks.ContainsKey(M72E08RA))                                                                      // 1-й блок DO M72E08RA
                {
                    if (list_do.Count > DO + M72E08RA.DO)
                    {
                        blocks[M72E08RA] = 2;                                                                               // DO M72E08RA, 2-й блок
                        if (list_do.Count > DO + M72E08RA.DO * 2) blocks[M72E08RA] = 3;                                     // DO M72E08RA, 3-й блок
                    }
                    else if (list_ui.Count > UI) blocks.Add(M72E16NA, 1);                                                   // UI M72E16NA
                }
                else if (blocks.ContainsKey(M72E16NA))                                                                      // 1-й блок M72E16NA
                {
                    if (list_ui.Count > UI + M72E16NA.UI)
                    {
                        blocks[M72E16NA] = 2;                                                                               // UI M72E16NA, 2-й блок
                        if (list_ui.Count > UI + M72E16NA.UI * 2) blocks[M72E16NA] = 3;                                     // UI M72E16NA, 3-й блок
                    }
                }
            }

            // Есть блоки расширения 2-х типов
            if (blocks.Count == 2)                                                                                          // Есть 2 типа блоков расширения
            {
                if (blocks.ContainsKey(M72E12RB))                                                                           // Есть блок AO
                {
                    if (blocks.ContainsKey(M72E12RA) && blocks[M72E12RB] == 1 && blocks[M72E12RA] == 1)                     // 1 блок AO и 1 блок DO + UI
                    {
                        if (list_do.Count > DO + M72E12RB.DO + M72E12RA.DO &&
                            list_ui.Count > UI + M72E12RB.UI + M72E12RA.UI) blocks[M72E12RA] = 2;                           // DO + UI M72E12RA

                        else if (list_do.Count > DO + M72E12RB.DO + M72E12RA.DO) blocks.Add(M72E08RA, 1);                   // DO M72E08RA
                        else if (list_ui.Count > UI + M72E12RB.UI + M72E12RA.UI) blocks.Add(M72E16NA, 1);                   // UI M72E16NA
                    }
                    else if (blocks.ContainsKey(M72E08RA) && blocks[M72E12RB] == 1 && blocks[M72E08RA] == 1)                // 1 блок AO и 1 блок DO
                    {
                        if (list_do.Count > DO + M72E12RB.DO + M72E08RA.DO) blocks[M72E08RA] = 2;                           // DO M72E08RA
                        else if (list_ui.Count > UI + M72E12RB.UI) blocks.Add(M72E16NA, 1);                                 // UI M72E16NA
                    }
                    else if (blocks.ContainsKey(M72E16NA) && blocks[M72E12RB] == 1 && blocks[M72E16NA] == 1)                // 1 блок AO и 1 блок UI
                    {
                        if (list_ui.Count > UI + M72E12RB.UI + M72E16NA.UI) blocks[M72E16NA] = 2;                           // UI M72E16NA
                    }
                }
                else if (blocks.ContainsKey(M72E12RA))                                                                      // Есть блок DO + UI
                {
                    if (blocks.ContainsKey(M72E08RA) && blocks[M72E12RA] == 1 && blocks[M72E08RA] == 1)                     // 1 блок DO + UI, 1 блок DO
                    {
                        if (list_do.Count > DO + M72E12RA.DO + M72E08RA.DO) blocks[M72E08RA] = 2;                           // DO M72E08RA
                        else if (list_ui.Count > UI + M72E12RA.UI) blocks.Add(M72E16NA, 1);                                 // UI M72E16NA
                    }
                    else if (blocks.ContainsKey(M72E16NA) && blocks[M72E12RA] == 1 && blocks[M72E16NA] == 1)                // 1 блок DO + UI, 1 блок UI
                    {
                        if (list_ui.Count > UI + M72E12RA.UI + M72E16NA.UI) blocks[M72E16NA] = 2;                           // UI M72E16NA
                    }
                }
                else if (blocks.ContainsKey(M72E08RA))                                                                      // Есть блок DO
                {
                    if (blocks.ContainsKey(M72E16NA) && blocks[M72E08RA] == 1 && blocks[M72E16NA] == 1)                     // 1 блок DO, 1 блок UI
                    {
                        if (list_ui.Count > UI + M72E16NA.UI) blocks[M72E16NA] = 2;                                         // UI M72E16NA
                    }
                }
            }
            return blocks;
        }

        ///<summary>Алгоритм перераспределения сигналов DO, UI и AO при смене типа контроллера</summary>
        private void RellocateSignals_DO_UI_AO_signals(short type, ComboBox cm)
        {
            string selectedName = null;
                
            try { selectedName = cm.SelectedItem?.ToString(); }
            catch (ArgumentOutOfRangeException) { return; }

            if (string.IsNullOrEmpty(selectedName)) return;

            Do do_find = null;                                                          // DO сигнал для поиска
            Ui ui_find = null;                                                          // UI сигнал для поиска
            Ao ao_find = null;                                                          // AO сигнал для поиска

            if (type == 1)                                                              // Для сигналов DO
                do_find = list_do.Find(x => x.Name == selectedName);
            else if (type == 2)                                                         // Для сигналов UI
                ui_find = list_ui.Find(x => x.Name == selectedName);
            else if (type == 3)                                                         // Для сигналов AO
                ao_find = list_ao.Find(x => x.Name == selectedName);

            cm.SelectedIndex = 0;                                                       // Сброс comboBox в "Не выбрано"

            if (do_find != null) AddNewDO(do_find.Code);                                // Распределение для сигнала DO
            if (ui_find != null) AddNewUI(ui_find.Code, ui_find.Type);                  // Распределение для сигнала UI
            if (ao_find != null) AddNewAO(ao_find.Code);                                // Распределение для сигнала AO
        }

        ///<summary>Перераспределения сигналов AO, DO, UI из списков comboBox</summary>
        private void RellocateSignals_fromLists(List<ComboBox> ao_signals, List<ComboBox> do_signals, List<ComboBox> ui_signals)
        {
            // Для сигналов AO
            if (ao_signals != null)
                foreach (var el in ao_signals)
                    if (el.SelectedIndex != 0) RellocateSignals_DO_UI_AO_signals(3, el);       // Есть ранее выбранный сигнал AO 

            // Для сигналов DO
            if (do_signals != null)
                foreach (var el in do_signals)
                    if (el.SelectedIndex != 0) RellocateSignals_DO_UI_AO_signals(1, el);       // Есть ранее выбранный сигнал DO

            // Для сигналов UI
            if (ui_signals != null)
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

        ///<summary>Проверка распределенных сигналов при удалении 1-го блока расширения M72E12RA сигналов UI</summary>
        private void CheckSignals_block1_M72E12RA()
        {
            List<ComboBox> ui_signals = new List<ComboBox>()
            {
                UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo
            };
            List<ComboBox> do_signals = new List<ComboBox>()
            {
                DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo, DO5bl1_combo, DO6bl1_combo
            };

            // Перераспределение сигналов
            RellocateSignals_fromLists(null, do_signals, ui_signals);
        }

        ///<summary>Проверка распределенных сигналов при удалении 2-го блока расширения M72E12RA сигналов UI</summary>
        private void CheckSignals_block2_M72E12RA()
        {
            List<ComboBox> ui_signals =
            [
                UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo
            ];
            List<ComboBox> do_signals =
            [
                DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo, DO5bl2_combo, DO6bl2_combo
            ];

            // Перераспределение сигналов
            RellocateSignals_fromLists(null, do_signals, ui_signals);
        }

        ///<summary>Проверка распределенных сигналов при удалении 3-го блока расширения M72E12RA сигналов UI</summary>
        private void CheckSignals_block3_M72E12RA()
        {
            List<ComboBox> ui_signals =
            [
                UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo
            ];
            List<ComboBox> do_signals =
            [
                DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo, DO5bl3_combo, DO6bl3_combo
            ];

            // Перераспределение сигналов
            RellocateSignals_fromLists(null, do_signals, ui_signals);
        }

        ///<summary>Проверка распределенных сигналов при удалении 1-го блока расширения M72E16NA сигналов UI</summary>
        private void CheckSignals_block1_M72E16NA()
        {
            List<ComboBox> ui_signals =
            [
                UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo, UI7bl1_combo, UI8bl1_combo, UI9bl1_combo,
                UI10bl1_combo, UI11bl1_combo, UI12bl1_combo, UI13bl1_combo, UI14bl1_combo, UI15bl1_combo, UI16bl1_combo
            ];

            // Перераспределение сигналов
            RellocateSignals_fromLists(null, null, ui_signals);
        }

        ///<summary>Проверка распределенных сигналов при удалении 2-го блока расширения M72E16NA сигналов UI</summary>
        private void CheckSignals_block2_M72E16NA()
        {
            List<ComboBox> ui_signals =
            [
                UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo, UI7bl2_combo, UI8bl2_combo, UI9bl2_combo,
                UI10bl2_combo, UI11bl2_combo, UI12bl2_combo, UI13bl2_combo, UI14bl2_combo, UI15bl2_combo, UI16bl2_combo
            ];

            // Перераспределение сигналов
            RellocateSignals_fromLists(null, null, ui_signals);
        }

        ///<summary>Проверка распределенных сигналов при удалении 3-го блока расширения M72E16NA сигналов UI</summary>
        private void CheckSignals_block3_M72E16NA()
        {
            List<ComboBox> ui_signals =
            [
                UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo, UI7bl3_combo, UI8bl3_combo, UI9bl3_combo,
                UI10bl3_combo, UI11bl3_combo, UI12bl3_combo, UI13bl3_combo, UI14bl3_combo, UI15bl3_combo, UI16bl3_combo
            ];

            // Перераспределение сигналов
            RellocateSignals_fromLists(null, null, ui_signals);
        }

        ///<summary>Проверка распределенных сигналов при удалении 1-го блока расширения M72E08RB сигналов DO</summary>
        private void CheckSignals_block1_M72E08RA()
        {
            List<ComboBox> do_signals = [                                                      // Сигналы DO
                DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo, DO5bl1_combo, DO6bl1_combo, DO7bl1_combo, DO8bl1_combo 
            ];

            // Перераспределение сигналов
            RellocateSignals_fromLists(null, do_signals, null);
        }

        ///<summary>Проверка распределенных сигналов при удалении 2-го блока расширения M72E08RB сигналов DO</summary>
        private void CheckSignals_block2_M72E08RA()
        {
            List<ComboBox> do_signals = [                                                      // Сигналы DO
                DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo, DO5bl2_combo, DO6bl2_combo, DO7bl2_combo, DO8bl2_combo
            ];

            // Перераспределение сигналов
            RellocateSignals_fromLists(null, do_signals, null);
        }

        ///<summary>Проверка распределенных сигналов при удалении 3-го блока расширения M72E08RB сигналов DO</summary>
        private void CheckSignals_block3_M72E08RA()
        {
            List<ComboBox> do_signals = [                                                                 // Сигналы DO
                DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo, DO5bl3_combo, DO6bl3_combo, DO7bl3_combo, DO8bl3_combo
            ];

            // Перераспределение сигналов
            RellocateSignals_fromLists(null, do_signals, null);
        }

        ///<summary>Проверка распределенных сигналов при удалении 1-го блока расширения M72E12RB сигналов AO</summary>
        private void CheckSignals_block1_M72E12RB()
        {
            List<ComboBox> ao_signals = [ AO1bl1_combo, AO2bl1_combo ];                                   // Сигналы AO
            List<ComboBox> do_signals = [ DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo ];       // Сигналы DO
            List<ComboBox> ui_signals = [                                                                 // Сигналы UI
                UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo
            ];

            // Перераспределение сигналов
            RellocateSignals_fromLists(ao_signals, do_signals, ui_signals);
        }

        ///<summary>Проверка распределенных сигналов при удалении 2-го блока расширения M72E12RB сигналов AO</summary>
        private void CheckSignals_block2_M72E12RB(Dictionary<ExpBlock, int> blocks, int total_blocks)
        {
            List<ComboBox> ao_signals = [ AO1bl2_combo, AO2bl2_combo ];                                   // Сигналы AO
            List<ComboBox> do_signals = [ DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo ];       // Сигналы DO
            List<ComboBox> ui_signals = [                                                                 // Сигналы UI
                UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo
            ];

            // Не требуется распределение UI, когда 2-й блок UI
            if (blocks.ContainsKey(M72E16NA) && total_blocks > 1) ui_signals = null;

            // Перераспределение сигналов
            RellocateSignals_fromLists(ao_signals, do_signals, ui_signals);
        }

        ///<summary>Проверка распределенных сигналов при удалении 3-го блока расширения M72E12RB сигналов AO</summary>
        private void CheckSignals_block3_M72E12RB()
        {
            List<ComboBox> ao_signals = [ AO1bl3_combo, AO2bl3_combo ];                                   // Сигналы AO
            List<ComboBox> do_signals = [ DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo ];       // Сигналы DO
            List<ComboBox> ui_signals = [                                                                 // Сигналы UI
                UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo
            ];

            // Перераспределение сигналов
            RellocateSignals_fromLists(ao_signals, do_signals, ui_signals);
        }

        ///<summary>Изменение подписей для типа контроллера "Mini" или "Optimized"</summary>
        private void ChangePlk_Headers(bool mini)
        {
            string
                text_mini = "Контроллер Moderon M72 Mini",
                text_optimized = "Контроллер Moderon M72 Optimized";

            List<Label> headers = [
                UIplk_header, DOplk_header, AOplk_header
            ];

            // Замена названий для заголовков
            foreach (var el in headers)
                el.Text = mini ? text_mini : text_optimized;
        }

        ///<summary>Изменение типа контроллера "Mini" или "Optimized"</summary>
        private void ComboPlkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ui_combos = new List<ComboBox>() { UI8_combo, UI9_combo, UI10_combo, UI11_combo };                          // UI_combo
            var ui_combos_type = new List<ComboBox>() { UI8_typeCombo, UI9_typeCombo, UI10_typeCombo, UI11_typeCombo };     // UI_typeCombo
            var ui_labels = new List<Label>() { UI8_plkLabel, UI9_plkLabel, UI10_plkLabel, UI11_plkLabel };                 // UI подписи сигналов
            var do_combos = new List<ComboBox>() { DO5_combo, DO6_combo };                                                  // DO_combo
            var do_labels = new List<Label>() { DO5_plkLabel, DO6_plkLabel };                                               // DO подписи сигналов

            // Количество и тип блоков расширения, расчётное значение
            Dictionary<ExpBlock, int> blocks;                                                                

            if (comboPlkType.SelectedIndex == plkChangeIndexLast) return;           // Выбранный индекс не изменился

            if (comboPlkType.SelectedIndex == 0)                                    // Выбрали контроллер "Mini"
            {
                plkChangeIndexLast = 0;                                             // Сохранение нового значения состояния "Mini"
                blocks = CalcExpBlocks_typeNums();                                  // Расчёт нового количества блоков расширения

                ChangePlk_Headers(true);                                            // Подписи заголовков для контроллера "Mini"

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

                if (ignoreEvents) return;
                CheckSignals_plkChange();                                           // Проверка распределённых сигналов на Optimized  

                RemoveThirdBlockUI_M72E16NA(blocks);                                // Проверка на удаление 3-го блока расширения UI
                RemoveSecondBlockUI_M72E16NA(blocks);                               // Проверка на удаление 2-го блока расширения UI
                RemoveFirstBlockUI_M72E16NA(blocks);                                // Проверка на удаление 1-го блока расширения UI

                RemoveThirdBlockDO_M72E08RA(blocks);                                // Проверка на удаление 3-го блока расширения DO
                RemoveSecondBlockDO_M72E08RA(blocks);                               // Проверка на удаление 2-го блока расширения DO
                RemoveFirstBlockDO_M72E08RA(blocks);                                // Проверка на удаление 1-го блока расширения DO

                RemoveThirdBlock_DOUI_M72E12RA(blocks);                             // Проверка на удаление 3-го блока расширения DO + UI
                RemoveSecondBlock_DOUI_M72E12RA(blocks);                            // Проверка на удаление 2-го блока расширения DO + UI
                RemoveFirstBlock_DOUI_M72E12RA(blocks);                             // Проверка на удаление 1-го блока расширения DO + UI

                RemoveThirdBlockAO_M72E12RB(blocks);                                // Проверка на удаление 3-го блока расширения AO
                RemoveSecondBlockAO_M72E12RB(blocks);                               // Проверка на удаление 2-го блока расширения AO
                RemoveFirstBlockAO_M72E12RB(blocks);                                // Проверка на удаление 1-го блока расширения AO

                AddFirstBlockAO_M72E12RB(blocks);                                   // Проверка на добавление 1-го блока расширения AO
                AddSecondBlockAO_M72E12RB(blocks);                                  // Проверка на добавление 2-го блока расширения AO
                AddThirdBlockAO_M72E12RB(blocks);                                   // Проверка на добавление 3-го блока расширения AO

                AddFirstBlock_DOUI_M72E12RA(blocks);                                // Проверка на добавление 1-го блока расширения DO + UI
                AddSecondBlock_DOUI_M72E12RA(blocks);                               // Проверка на добавление 2-го блока расширения DO + UI
                AddThirdBlock_DOUI_M72E12RA(blocks);                                // Проверка на добавление 3-го блока расширения DO + UI

                AddFirstBlockDO_M72E08RA(blocks);                                   // Проверка на добавление 1-го блока расширения DO
                AddSecondBlockDO_M72E08RA(blocks);                                  // Проверка на добавление 2-го блока расширения DO
                AddThirdBlockDO_M72E08RA(blocks);                                   // Проверка на добавление 3-го блока расширения DO

                AddFirstBlockUI_M72E16NA(blocks);                                   // Проверка на добавление 1-го блока расширения UI
                AddSecondBlockUI_M72E16NA(blocks);                                  // Проверка на добавление 2-го блока расширения UI
                AddThirdBlockUI_M72E16NA(blocks);                                   // Проверка на добавление 3-го блока расширения UI
            }
            else if (comboPlkType.SelectedIndex == 1)                               // Выбрали контроллер "Optimized"
            {
                plkChangeIndexLast = 1;                                             // Сохранение нового значения состояния "Optimized"
                blocks = CalcExpBlocks_typeNums();                                  // Расчёт нового количества блоков расширения

                ChangePlk_Headers(false);                                           // Подписи заголовков для контроллера "Optimized"

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

                if (ignoreEvents) return;

                RemoveThirdBlockUI_M72E16NA(blocks);                                // Проверка на удаление 3-го блока расширения UI
                RemoveSecondBlockUI_M72E16NA(blocks);                               // Проверка на удаление 2-го блока расширения UI
                RemoveFirstBlockUI_M72E16NA(blocks);                                // Проверка на удаление 1-го блока расширения UI

                RemoveThirdBlockDO_M72E08RA(blocks);                                // Проверка на удаление 3-го блока расширения DO
                RemoveSecondBlockDO_M72E08RA(blocks);                               // Проверка на удаление 2-го блока расширения DO
                RemoveFirstBlockDO_M72E08RA(blocks);                                // Проверка на удаление 1-го блока расширения DO

                RemoveThirdBlock_DOUI_M72E12RA(blocks);                             // Проверка на удаление 3-го блока расширения DO + UI
                RemoveSecondBlock_DOUI_M72E12RA(blocks);                            // Проверка на удаление 2-го блока расширения DO + UI
                RemoveFirstBlock_DOUI_M72E12RA(blocks);                             // Проверка на удаление 1-го блока расширения DO + UI

                RemoveThirdBlockAO_M72E12RB(blocks);                                // Проверка на удаление 3-го блока расширения AO
                RemoveSecondBlockAO_M72E12RB(blocks);                               // Проверка на удаление 2-го блока расширения AO
                RemoveFirstBlockAO_M72E12RB(blocks);                                // Проверка на удаление 1-го блока расширения AO

                AddFirstBlock_DOUI_M72E12RA(blocks);                                // Проверка на добавление 1-го блока расширения DO + UI
                AddSecondBlock_DOUI_M72E12RA(blocks);                               // Проверка на добавление 2-го блока расширения DO + UI
                AddThirdBlock_DOUI_M72E12RA(blocks);                                // Проверка на добавление 3-го блока расширения DO + UI

                AddFirstBlockDO_M72E08RA(blocks);                                   // Проверка на добавление 1-го блока расширения DO
                AddSecondBlockDO_M72E08RA(blocks);                                  // Проверка на добавление 2-го блока расширения DO
                AddThirdBlockDO_M72E08RA(blocks);                                   // Проверка на добавление 3-го блока расширения DO

                AddFirstBlockUI_M72E16NA(blocks);                                   // Проверка на добавление 1-го блока расширения UI
                AddSecondBlockUI_M72E16NA(blocks);                                  // Проверка на добавление 2-го блока расширения UI
                AddThirdBlockUI_M72E16NA(blocks);                                   // Проверка на добавление 3-го блока расширения UI
            }
        }

        ///<summary>Изменение панелей UI при добавлении блока расширения с DO</summary>
        private void UI_panelBlock_change(Panel panel, ExpBlock block)
        {
            int height = HEIGHT_UI_PANEL_BLOCK;                                     // Высота панели 16 UI по умолчанию

            if (block == M72E12RB || block == M72E12RA) height -= 10 * DELTA;       // Высота панели под 6 UI

            if (panel == block1_UIpanel)                                            // Для блока расширения 1
            {
                if (!ignoreEvents)
                {
                    if (block1_DOpanel.Enabled && block2_DOpanel.Enabled)
                        UIblock1_header.Text = "Блок расширения 3 - " + block.Name;
                    else
                        UIblock1_header.Text = "Блок расширения 1 - " + block.Name;
                }
                    
                block1_UIpanel.Height = height;
                block1_UIpanel.Show(); block1_UIpanel.Enabled = true;
            }
            else if (panel == block2_UIpanel)                                       // Для блока расширения 2
            {
                if (!ignoreEvents) 
                    UIblock2_header.Text = "Блок расширения 2 - " + block.Name;
                block2_UIpanel.Height = height;
                block2_UIpanel.Show(); block2_UIpanel.Enabled = true;

                // Положение по высоте для панели UI блока расширения 2 (проверка доступности панели 1)
                if (block1_UIpanel.Enabled)
                    block2_UIpanel.Location = new Point(block2_UIpanel.Location.X, 
                        block1_UIpanel.Location.Y + block1_UIpanel.Height);
                else
                    block2_UIpanel.Location = new Point(block2_UIpanel.Location.X,
                        block1_UIpanel.Location.Y);
            }
            else if (panel == block3_UIpanel)                                       // Для блока расширения 3
            {
                if (!ignoreEvents)
                {
                    if (block1_DOpanel.Enabled)
                        UIblock3_header.Text = "Блок расширения 3 - " + block.Name;
                    else
                        UIblock3_header.Text = "Блок расширения 2 - " + block.Name;
                }
                block3_UIpanel.Height = height;
                block3_UIpanel.Show(); block3_UIpanel.Enabled = true;

                // Положение по высоте для панели UI блока расширения 3 (по доступности панелей)
                if (block2_UIpanel.Enabled)
                    block3_UIpanel.Location = new Point(block3_UIpanel.Location.X, 
                        block2_UIpanel.Location.Y + block2_UIpanel.Height);
                else if (block1_UIpanel.Enabled)
                    block3_UIpanel.Location = new Point(block3_UIpanel.Location.X,
                        block1_UIpanel.Location.Y + block1_UIpanel.Height);
                else
                    block3_UIpanel.Location = new Point(block3_UIpanel.Location.X,
                        block1_UIpanel.Location.Y);
            }
        }

        ///<summary>Изменение панелей DO при добавлении блока расширения с DO</summary>
        private void DO_panelBlock_change(Panel panel, ExpBlock block)
        {
            int height = HEIGHT_DO_PANEL_BLOCK;                                         // Высота панели, 8 DO по умолчанию

            if (block == M72E12RA) height -= 2 * DELTA;                                 // 6 DO
            else if (block == M72E12RB) height -= 4 * DELTA;                            // 4 DO

            if (panel == block1_DOpanel)                                                // Для блока расширения 1
            {
                if (!ignoreEvents)
                {
                    if (!block2_DOpanel.Enabled && !block3_DOpanel.Enabled)
                        DOblock1_header.Text = "Блок расширения 1 - " + block.Name;
                    else if (block2_DOpanel.Enabled && block3_DOpanel.Enabled)
                        DOblock1_header.Text = "Блок расширения 3 - " + block.Name;
                }
                block1_DOpanel.Height = height;
                block1_DOpanel.Show(); block1_DOpanel.Enabled = true;
            }
            else if (panel == block2_DOpanel)                                           // Для блока расширения 2
            {
                if (!ignoreEvents)
                {
                    if (block1_DOpanel.Enabled)
                        DOblock2_header.Text = "Блок расширения 2 - " + block.Name;
                    else
                        DOblock2_header.Text = "Блок расширения 1 - " + block.Name;
                }
                block2_DOpanel.Height = height;
                block2_DOpanel.Show(); block2_DOpanel.Enabled = true;

                // Положение по высоте для панели DO блока расширения 2 (по доступности панели DO1)
                if (block1_DOpanel.Enabled)
                    block2_DOpanel.Location = new Point(block2_DOpanel.Location.X,
                        block1_DOpanel.Location.Y + block1_DOpanel.Height);
                else
                    block2_DOpanel.Location = new Point(block2_DOpanel.Location.X,
                        block1_DOpanel.Location.Y);

                // Положение по высоте панели DO блока расширения 3 (по доступности панели DO2)
                if (block2_DOpanel.Enabled)
                    block3_DOpanel.Location = new Point(block3_DOpanel.Location.X,
                        block2_DOpanel.Location.Y + block2_DOpanel.Height);
            }
            else if (panel == block3_DOpanel)                                           // Для блока расширения 3
            {
                if (!ignoreEvents)
                {
                    if (block1_DOpanel.Enabled)
                        DOblock3_header.Text = "Блок расширения 3 - " + block.Name;
                    else
                        DOblock3_header.Text = "Блок расширения 2 - " + block.Name;
                }
                block3_DOpanel.Height = height;
                block3_DOpanel.Show(); block3_DOpanel.Enabled = true;

                // Положение по высоте для панели DO блока расширения 3
                if (!block1_DOpanel.Enabled && !block2_DOpanel.Enabled)
                    block3_DOpanel.Location = new Point(block3_DOpanel.Location.X,
                        plk_DOpanel.Location.Y + plk_DOpanel.Height);
                else if (block1_DOpanel.Enabled && !block2_DOpanel.Enabled)
                    block3_DOpanel.Location = new Point(block3_DOpanel.Location.X,
                        block1_DOpanel.Location.Y + block1_DOpanel.Height);
                else
                    block3_DOpanel.Location = new Point(block3_DOpanel.Location.X,
                        block2_DOpanel.Location.Y + block2_DOpanel.Height);
            }
        }

        ///<summary>Изменение панелей AO при добавлении блока расширения AO</summary>
        private void AO_panelBlock_change(Panel panel, ExpBlock expBlock)
        {
            int height = HEIGHT_AO_PANEL_BLOCK;

            if (panel == block1_AOpanel)            // Для блока расширения 1
            {
                if (!ignoreEvents)
                {
                    if (block2_DOpanel.Enabled && block3_DOpanel.Enabled)
                        AOblock1_header.Text = "Блок расширения 3 - " + expBlock.Name;
                    else
                        AOblock1_header.Text = "Блок расширения 1 - " + expBlock.Name;
                }
                    
                block1_AOpanel.Height = height;
                block1_AOpanel.Show(); block1_AOpanel.Enabled = true;
            } 
            else if (panel == block2_AOpanel)       // Для блока расширения 2
            {
                if (!ignoreEvents)
                    AOblock2_header.Text = "Блок расширения 2 - " + expBlock.Name;
                block2_AOpanel.Height = height;
                block2_AOpanel.Show(); block2_AOpanel.Enabled = true;

                // Положение по высоте для панели AO блока расширения 2 (по доступности панели 1)
                if (block1_AOpanel.Enabled)
                    block2_AOpanel.Location = new Point(block2_AOpanel.Location.X,
                        block1_AOpanel.Location.Y + block1_AOpanel.Height);
                else
                    block2_AOpanel.Location = new Point(block2_AOpanel.Location.X,
                        block1_AOpanel.Location.Y);
            }
            else if (panel == block3_AOpanel)
            {
                if (!ignoreEvents)
                {
                    if (block1_DOpanel.Enabled)
                        AOblock3_header.Text = "Блок расширения 3 - " + expBlock.Name;
                    else
                        AOblock3_header.Text = "Блок расширения 2 - " + expBlock.Name;
                }
                block3_AOpanel.Height = height;
                block3_AOpanel.Show(); block3_AOpanel.Enabled = true;

                // Положение по высоте для панели AO блока расширения 3 (проверка доступности панели 1 и 2)
                if (block2_AOpanel.Enabled)
                    block3_AOpanel.Location = new Point(block3_AOpanel.Location.X,
                        block2_AOpanel.Location.Y + block2_AOpanel.Height);
                else if (block1_AOpanel.Enabled)
                    block3_AOpanel.Location = new Point(block3_AOpanel.Location.X,
                        block1_AOpanel.Location.Y + block1_AOpanel.Height);
                else
                    block3_AOpanel.Location = new Point(block3_AOpanel.Location.X,
                        block1_AOpanel.Location.Y);
            }
        }

        ///<summary>Отображение и разблокировка comboBox и Labels UI панелей блоков расширения</summary>
        private void UI_showEnable_combos_labels(List<ComboBox> ui_combos, List<ComboBox> ui_type_combos, List<Label> ui_labels)
        {
            foreach (var el in ui_combos) { el.Show(); el.Enabled = true; }
            foreach (var el in ui_type_combos) el.Show();
            foreach (var el in ui_labels) el.Show();
        }

        ///<summary>Отображение и разблокировка comboBox и Labels DO панелей блоков расширения</summary>
        private void DO_showEnable_combos_labels(List<ComboBox> do_combos, List<Label> do_labels)
        {
            foreach (var el in do_combos) { el.Show(); el.Enabled = true; }
            foreach (var el in do_labels) el.Show();
        }

        ///<summary>Изменение панели UI блока расширения 1 для M72E16NA (16 UI)</summary>
        private void UI_block1_panelChanged_M72E16NA()
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

            UI_panelBlock_change(block1_UIpanel, M72E16NA);                         // Изменения для панели UI блока расширения 1
            UI_showEnable_combos_labels(ui_combos, ui_type_combos, ui_labels);      // Отображение и разблокировка элементов
        }

        ///<summary>Изменение панели UI блока расширения 2 для M72E16NA (16 UI)</summary>
        private void UI_block2_panelChanged_M72E16NA()
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

            UI_panelBlock_change(block2_UIpanel, M72E16NA);                         // Изменения для панели UI блока расширения 2
            UI_showEnable_combos_labels(ui_combos, ui_type_combos, ui_labels);      // Отображение и разблокировка элементов
        }

        ///<summary>Изменение панели UI блока расширения 3 для M72E16NA (16 UI)</summary>
        private void UI_block3_panelChanged_M72E16NA()
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

            UI_panelBlock_change(block3_UIpanel, M72E16NA);                         // Изменения для панели UI блока расширения 3
            UI_showEnable_combos_labels(ui_combos, ui_type_combos, ui_labels);      // Отображение и разблокировка элементов
        }

        ///<summary>Изменение панели UI блока расширения 1 для M72E12RA (6 UI)</summary>
        private void UI_block1_panelChanged_M72E12RA()
        {
            var ui_combos = new List<ComboBox>() { UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo };
            var ui_type_combos = new List<ComboBox>()
            {
                UI1bl1_typeCombo, UI2bl1_typeCombo, UI3bl1_typeCombo, UI4bl1_typeCombo, UI5bl1_typeCombo, UI6bl1_typeCombo
            };
            var ui_labels = new List<Label>() { UI1_bl1Label, UI2_bl1Label, UI3_bl1Label, UI4_bl1Label, UI5_bl1Label, UI6_bl1Label };

            UI_panelBlock_change(block1_UIpanel, M72E12RA);                         // Изменения для панели UI блока расширения 1
            UI_showEnable_combos_labels(ui_combos, ui_type_combos, ui_labels);      // Отображение и разблокировка элементов

            var ui_combos_block = new List<ComboBox>() {
                UI7bl1_combo, UI8bl1_combo, UI9bl1_combo, UI10bl1_combo, UI11bl1_combo, 
                UI12bl1_combo, UI13bl1_combo, UI14bl1_combo, UI15bl1_combo, UI16bl1_combo
            };

            var ui_type_combos_block = new List<ComboBox>()
            {
                UI7bl1_typeCombo, UI8bl1_typeCombo, UI9bl1_typeCombo, UI10bl1_typeCombo, UI11bl1_typeCombo, UI12bl1_typeCombo,
                UI13bl1_typeCombo, UI14bl1_typeCombo, UI15bl1_typeCombo, UI16bl1_typeCombo
            };

            var ui_labels_block = new List<Label>()
            {
                UI7_bl1Label, UI8_bl1Label, UI9_bl1Label, UI10_bl1Label, UI11_bl1Label, 
                UI12_bl1Label, UI13_bl1Label, UI14_bl1Label, UI15_bl1Label, UI16_bl1Label
            };

            foreach (var el in ui_combos_block) { el.Hide(); el.Enabled = false; }
            foreach (var el in ui_type_combos_block) el.Hide();
            foreach (var el in ui_labels_block) el.Hide();
        }

        ///<summary>Изменение панели UI блока расширения 2 для M72E12RA (6 UI)</summary>
        private void UI_block2_panelChanged_M72E12RA()
        {
            var ui_combos = new List<ComboBox>() { UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo };
            var ui_type_combos = new List<ComboBox>()
            {
                UI1bl2_typeCombo, UI2bl2_typeCombo, UI3bl2_typeCombo, UI4bl2_typeCombo, UI5bl2_typeCombo, UI6bl2_typeCombo
            };
            var ui_labels = new List<Label>() { UI1_bl2Label, UI2_bl2Label, UI3_bl2Label, UI4_bl2Label, UI5_bl2Label, UI6_bl2Label };

            UI_panelBlock_change(block2_UIpanel, M72E12RA);                         // Изменения для панели UI блока расширения 2
            UI_showEnable_combos_labels(ui_combos, ui_type_combos, ui_labels);      // Отображение и разблокировка элементов

            var ui_combos_block = new List<ComboBox>() {
                UI7bl2_combo, UI8bl2_combo, UI9bl2_combo, UI10bl2_combo, UI11bl2_combo,
                UI12bl2_combo, UI13bl2_combo, UI14bl2_combo, UI15bl2_combo, UI16bl2_combo
            };

            var ui_type_combos_block = new List<ComboBox>()
            {
                UI7bl2_typeCombo, UI8bl2_typeCombo, UI9bl2_typeCombo, UI10bl2_typeCombo, UI11bl2_typeCombo, UI12bl2_typeCombo,
                UI13bl2_typeCombo, UI14bl2_typeCombo, UI15bl2_typeCombo, UI16bl2_typeCombo
            };

            var ui_labels_block = new List<Label>()
            {
                UI7_bl2Label, UI8_bl2Label, UI9_bl2Label, UI10_bl2Label, UI11_bl2Label,
                UI12_bl2Label, UI13_bl2Label, UI14_bl2Label, UI15_bl2Label, UI16_bl2Label
            };

            foreach (var el in ui_combos_block) { el.Hide(); el.Enabled = false; }
            foreach (var el in ui_type_combos_block) el.Hide();
            foreach (var el in ui_labels_block) el.Hide();
        }

        ///<summary>Изменение панели UI блока расширения 3 для M72E12RA (6 UI)</summary>
        private void UI_block3_panelChanged_M72E12RA()
        {
            var ui_combos = new List<ComboBox>() { UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo };
            var ui_type_combos = new List<ComboBox>()
            {
                UI1bl3_typeCombo, UI2bl3_typeCombo, UI3bl3_typeCombo, UI4bl3_typeCombo, UI5bl3_typeCombo, UI6bl3_typeCombo
            };
            var ui_labels = new List<Label>() { UI1_bl3Label, UI2_bl3Label, UI3_bl3Label, UI4_bl3Label, UI5_bl3Label, UI6_bl3Label };

            UI_panelBlock_change(block3_UIpanel, M72E12RA);                         // Изменения для панели UI блока расширения 3
            UI_showEnable_combos_labels(ui_combos, ui_type_combos, ui_labels);      // Отображение и разблокировка элементов

            var ui_combos_block = new List<ComboBox>() {
                UI7bl3_combo, UI8bl3_combo, UI9bl3_combo, UI10bl3_combo, UI11bl3_combo,
                UI12bl3_combo, UI13bl3_combo, UI14bl3_combo, UI15bl3_combo, UI16bl3_combo
            };

            var ui_type_combos_block = new List<ComboBox>()
            {
                UI7bl3_typeCombo, UI8bl3_typeCombo, UI9bl3_typeCombo, UI10bl3_typeCombo, UI11bl3_typeCombo, UI12bl3_typeCombo,
                UI13bl3_typeCombo, UI14bl3_typeCombo, UI15bl3_typeCombo, UI16bl3_typeCombo
            };

            var ui_labels_block = new List<Label>()
            {
                UI7_bl3Label, UI8_bl3Label, UI9_bl3Label, UI10_bl3Label, UI11_bl3Label,
                UI12_bl3Label, UI13_bl3Label, UI14_bl3Label, UI15_bl3Label, UI16_bl3Label
            };

            foreach (var el in ui_combos_block) { el.Hide(); el.Enabled = false; }
            foreach (var el in ui_type_combos_block) el.Hide();
            foreach (var el in ui_labels_block) el.Hide();
        }

        ///<summary>Изменение панели UI блока расширения 1 для M72E12RB (6 UI)</summary>
        private void UI_block1_panelChanged_M72E12RB()
        {
            var ui_combos = new List<ComboBox>() { UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo };
            var ui_type_combos = new List<ComboBox>()
            {
                UI1bl1_typeCombo, UI2bl1_typeCombo, UI3bl1_typeCombo, UI4bl1_typeCombo, UI5bl1_typeCombo, UI6bl1_typeCombo
            };
            var ui_labels = new List<Label>() { UI1_bl1Label, UI2_bl1Label, UI3_bl1Label, UI4_bl1Label, UI5_bl1Label, UI6_bl1Label };

            UI_panelBlock_change(block1_UIpanel, M72E12RB);                         // Изменения для панели UI блока расширения 1
            UI_showEnable_combos_labels(ui_combos, ui_type_combos, ui_labels);      // Отображение и разблокировка элементов

            var ui_combos_block = new List<ComboBox>() {
                UI7bl1_combo, UI8bl1_combo, UI9bl1_combo, UI10bl1_combo, UI11bl1_combo,
                UI12bl1_combo, UI13bl1_combo, UI14bl1_combo, UI15bl1_combo, UI16bl1_combo
            };

            var ui_type_combos_block = new List<ComboBox>()
            {
                UI7bl1_typeCombo, UI8bl1_typeCombo, UI9bl1_typeCombo, UI10bl1_typeCombo, UI11bl1_typeCombo, UI12bl1_typeCombo,
                UI13bl1_typeCombo, UI14bl1_typeCombo, UI15bl1_typeCombo, UI16bl1_typeCombo
            };

            var ui_labels_block = new List<Label>()
            {
                UI7_bl1Label, UI8_bl1Label, UI9_bl1Label, UI10_bl1Label, UI11_bl1Label,
                UI12_bl1Label, UI13_bl1Label, UI14_bl1Label, UI15_bl1Label, UI16_bl1Label
            };

            foreach (var el in ui_combos_block) { el.Hide(); el.Enabled = false; }
            foreach (var el in ui_type_combos_block) el.Hide();
            foreach (var el in ui_labels_block) el.Hide();
        }

        ///<summary>Изменение панели UI блока расширения 2 для M72E12RB (6 UI)</summary>
        private void UI_block2_panelChanged_M72E12RB()
        {
            var ui_combos = new List<ComboBox>() { UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo };
            var ui_type_combos = new List<ComboBox>()
            {
                UI1bl2_typeCombo, UI2bl2_typeCombo, UI3bl2_typeCombo, UI4bl2_typeCombo, UI5bl2_typeCombo, UI6bl2_typeCombo
            };
            var ui_labels = new List<Label>() { UI1_bl2Label, UI2_bl2Label, UI3_bl2Label, UI4_bl2Label, UI5_bl2Label, UI6_bl2Label };

            UI_panelBlock_change(block2_UIpanel, M72E12RB);                         // Изменения для панели UI блока расширения 2
            UI_showEnable_combos_labels(ui_combos, ui_type_combos, ui_labels);      // Отображение и разблокировка элементов

            var ui_combos_block = new List<ComboBox>() {
                UI7bl2_combo, UI8bl2_combo, UI9bl2_combo, UI10bl2_combo, UI11bl2_combo,
                UI12bl2_combo, UI13bl2_combo, UI14bl2_combo, UI15bl2_combo, UI16bl2_combo
            };

            var ui_type_combos_block = new List<ComboBox>()
            {
                UI7bl2_typeCombo, UI8bl2_typeCombo, UI9bl2_typeCombo, UI10bl2_typeCombo, UI11bl2_typeCombo, UI12bl2_typeCombo,
                UI13bl2_typeCombo, UI14bl2_typeCombo, UI15bl2_typeCombo, UI16bl2_typeCombo
            };

            var ui_labels_block = new List<Label>()
            {
                UI7_bl2Label, UI8_bl2Label, UI9_bl2Label, UI10_bl2Label, UI11_bl2Label,
                UI12_bl2Label, UI13_bl2Label, UI14_bl2Label, UI15_bl2Label, UI16_bl2Label
            };

            foreach (var el in ui_combos_block) { el.Hide(); el.Enabled = false; }
            foreach (var el in ui_type_combos_block) el.Hide();
            foreach (var el in ui_labels_block) el.Hide();
        }

        ///<summary>Изменение панели UI блока расширения 3 для M72E12RB (6 UI)</summary>
        private void UI_block3_panelChanged_M72E12RB()
        {
            var ui_combos = new List<ComboBox>() { UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo };
            var ui_type_combos = new List<ComboBox>()
            {
                UI1bl3_typeCombo, UI2bl3_typeCombo, UI3bl3_typeCombo, UI4bl3_typeCombo, UI5bl3_typeCombo, UI6bl3_typeCombo
            };
            var ui_labels = new List<Label>() { UI1_bl3Label, UI2_bl3Label, UI3_bl3Label, UI4_bl3Label, UI5_bl3Label, UI6_bl3Label };

            UI_panelBlock_change(block3_UIpanel, M72E12RB);                         // Изменения для панели UI блока расширения 3
            UI_showEnable_combos_labels(ui_combos, ui_type_combos, ui_labels);      // Отображение и разблокировка элементов

            var ui_combos_block = new List<ComboBox>() {
                UI7bl3_combo, UI8bl3_combo, UI9bl3_combo, UI10bl3_combo, UI11bl3_combo,
                UI12bl3_combo, UI13bl3_combo, UI14bl3_combo, UI15bl3_combo, UI16bl3_combo
            };

            var ui_type_combos_block = new List<ComboBox>()
            {
                UI7bl3_typeCombo, UI8bl3_typeCombo, UI9bl3_typeCombo, UI10bl3_typeCombo, UI11bl3_typeCombo, UI12bl3_typeCombo,
                UI13bl3_typeCombo, UI14bl3_typeCombo, UI15bl3_typeCombo, UI16bl3_typeCombo
            };

            var ui_labels_block = new List<Label>()
            {
                UI7_bl3Label, UI8_bl3Label, UI9_bl3Label, UI10_bl3Label, UI11_bl3Label,
                UI12_bl3Label, UI13_bl3Label, UI14_bl3Label, UI15_bl3Label, UI16_bl3Label
            };

            foreach (var el in ui_combos_block) { el.Hide(); el.Enabled = false; }
            foreach (var el in ui_type_combos_block) el.Hide();
            foreach (var el in ui_labels_block) el.Hide();
        }

        ///<summary>Изменение панели DO блока расширения 1 под M72E12RA (6 DO)</summary>
        private void DO_block1_panelChanged_M72E12RA()
        {
            var do_combos = new List<ComboBox>()
            {
                DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo, DO5bl1_combo, DO6bl1_combo
            };
            var do_labels = new List<Label>()
            {
                DO1_bl1Label, DO2_bl1Label, DO3_bl1Label, DO4_bl1Label, DO5_bl1Label, DO6_bl1Label
            };

            DO_panelBlock_change(block1_DOpanel, M72E12RA);                         // Изменения для панели DO блока расширения 1
            DO_showEnable_combos_labels(do_combos, do_labels);                      // Отображение и разблокировка элементов

            var do_combos_block = new List<ComboBox>() { DO7bl1_combo, DO8bl1_combo };
            var do_labels_block = new List<Label>() { DO7_bl1Label, DO8_bl1Label };

            foreach (var el in do_combos_block) { el.Hide(); el.Enabled = false; }
            foreach (var el in do_labels_block) el.Hide();
        }

        ///<summary>Изменение панели DO блока расширения 2 под M72E12RA (6 DO)</summary>
        private void DO_block2_panelChanged_M72E12RA()
        {
            var do_combos = new List<ComboBox>()
            {
                DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo, DO5bl2_combo, DO6bl2_combo
            };
            var do_labels = new List<Label>()
            {
                DO1_bl2Label, DO2_bl2Label, DO3_bl2Label, DO4_bl2Label, DO5_bl2Label, DO6_bl2Label
            };

            DO_panelBlock_change(block2_DOpanel, M72E12RA);                         // Изменения для панели DO блока расширения 2
            DO_showEnable_combos_labels(do_combos, do_labels);                      // Отображение и разблокировка элементов

            var do_combos_block = new List<ComboBox>() { DO7bl2_combo, DO8bl2_combo };
            var do_labels_block = new List<Label>() { DO7_bl2Label, DO8_bl2Label };

            foreach (var el in do_combos_block) { el.Hide(); el.Enabled = false; }
            foreach (var el in do_labels_block) el.Hide();
        }

        ///<summary>Изменение панели DO блока расширения 3 под M72E12RA (6 DO)</summary>
        private void DO_block3_panelChanged_M72E12RA()
        {
            var do_combos = new List<ComboBox>()
            {
                DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo, DO5bl3_combo, DO6bl3_combo
            };
            var do_labels = new List<Label>()
            {
                DO1_bl3Label, DO2_bl3Label, DO3_bl3Label, DO4_bl3Label, DO5_bl3Label, DO6_bl3Label
            };

            DO_panelBlock_change(block3_DOpanel, M72E12RA);                         // Изменения для панели DO блока расширения 3
            DO_showEnable_combos_labels(do_combos, do_labels);                      // Отображение и разблокировка элементов

            var do_combos_block = new List<ComboBox>() { DO7bl3_combo, DO8bl3_combo };
            var do_labels_block = new List<Label>() { DO7_bl3Label, DO8_bl3Label };

            foreach (var el in do_combos_block) { el.Hide(); el.Enabled = false; }
            foreach (var el in do_labels_block) el.Hide();
        }

        ///<summary>Изменение панели DO блока расширения 1 под M72E08RA (8 DO)</summary>
        private void DO_block1_panelChanged_M72E08RA()
        {
            var do_combos = new List<ComboBox>()  
            {
                DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo, DO5bl1_combo, DO6bl1_combo, DO7bl1_combo, DO8bl1_combo
            };
            var do_labels = new List<Label>()
            {
                DO1_bl1Label, DO2_bl1Label, DO3_bl1Label, DO4_bl1Label, DO5_bl1Label, DO6_bl1Label, DO7_bl1Label, DO8_bl1Label
            };

            DO_panelBlock_change(block1_DOpanel, M72E08RA);                         // Изменения для панели DO блока расширения 1
            DO_showEnable_combos_labels(do_combos, do_labels);                      // Отображение и разблокировка элементов
        }

        ///<summary>Изменение панели DO блока расширения 2 под M72E08RA (8 DO)</summary>
        private void DO_block2_panelChanged_M72E08RA()
        {
            var do_combos = new List<ComboBox>()
            {
                DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo, DO5bl2_combo, DO6bl2_combo, DO7bl2_combo, DO8bl2_combo
            };
            var do_labels = new List<Label>()
            {
                DO1_bl2Label, DO2_bl2Label, DO3_bl2Label, DO4_bl2Label, DO5_bl2Label, DO6_bl2Label, DO7_bl2Label, DO8_bl2Label
            };

            DO_panelBlock_change(block2_DOpanel, M72E08RA);                         // Изменения для панели DO блока расширения 2
            DO_showEnable_combos_labels(do_combos, do_labels);                      // Отображение и разблокировка элементов
        }

        ///<summary>Изменение панели DO блока расширения 3 под M72E08RA (8 DO)</summary>
        private void DO_block3_panelChanged_M72E08RA()
        {
            var do_combos = new List<ComboBox>()
            {
                DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo, DO5bl3_combo, DO6bl3_combo, DO7bl3_combo, DO8bl3_combo
            };
            var do_labels = new List<Label>()
            {
                DO1_bl3Label, DO2_bl3Label, DO3_bl3Label, DO4_bl3Label, DO5_bl3Label, DO6_bl3Label, DO7_bl3Label, DO8_bl3Label
            };

            DO_panelBlock_change(block3_DOpanel, M72E08RA);                         // Изменения для панели DO блока расширения 3
            DO_showEnable_combos_labels(do_combos, do_labels);                      // Отображение и разблокировка элементов
        }

        ///<summary>Отображение панели AO блока расширения 1 под M72E12RB (2 AO)</summary>
        private void AO_block1_panelChanged_M72E12RB()
        {
            AO_panelBlock_change(block1_AOpanel, M72E12RB);                         // Отображение и разблокировка панели AO для блока расширения 1
        }

        ///<summary>Изменение панели DO блока расширения 1 под M72E12RB (4 DO)</summary>
        private void DO_block1_panelChanged_M72E12RB()
        {
            var do_combos = new List<ComboBox>() { DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo };    // DO сигналы
            var do_labels = new List<Label>() { DO1_bl1Label, DO2_bl1Label, DO3_bl1Label, DO4_bl1Label };       // Подписи DO сигналов

            DO_panelBlock_change(block1_DOpanel, M72E12RB);                         // Изменения для панели DO блока расширения 1
            DO_showEnable_combos_labels(do_combos, do_labels);                      // Отображение и разблокировка элементов

            var do_combos_block = new List<ComboBox>() { DO5bl1_combo, DO6bl1_combo, DO7bl1_combo, DO8bl1_combo };
            var do_labels_block = new List<Label>() { DO5_bl1Label, DO6_bl1Label, DO7_bl1Label, DO8_bl1Label };

            foreach (var el in do_combos_block) { el.Hide(); el.Enabled = false; }
            foreach (var el in do_labels_block) el.Hide();
        }

        ///<summary>Отображение панели AO блока расширения 2 под M72E12RB (2 AO)</summary>
        private void AO_block2_panelChanged_M72E12RB()
        {
            AO_panelBlock_change(block2_AOpanel, M72E12RB);                         // Отображение и разблокировка панели AO для блока расширения 2
        }

        ///<summary>Изменение панели DO блока расширения 2 под M72E12RB (4 DO)</summary>
        private void DO_block2_panelChanged_M72E12RB()
        {
            var do_combos = new List<ComboBox>() { DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo };    // DO сигналы
            var do_labels = new List<Label>() { DO1_bl2Label, DO2_bl2Label, DO3_bl2Label, DO4_bl2Label };       // Подписи DO сигналов

            DO_panelBlock_change(block2_DOpanel, M72E12RB);                         // Изменения для панели DO блока расширения 2
            DO_showEnable_combos_labels(do_combos, do_labels);                      // Отображение и разблокировка элементов

            var do_combos_block = new List<ComboBox>() { DO5bl2_combo, DO6bl2_combo, DO7bl2_combo, DO8bl2_combo };
            var do_labels_block = new List<Label>() { DO5_bl2Label, DO6_bl2Label, DO7_bl2Label, DO8_bl2Label };

            foreach (var el in do_combos_block) { el.Hide(); el.Enabled = false; }
            foreach (var el in do_labels_block) el.Hide();
        }

        ///<summary>Отображение панели AO блока расширения 3 под M72E12RB (2 AO)</summary>
        private void AO_block3_panelChanged_M72E12RB()
        {
            AO_panelBlock_change(block3_AOpanel, M72E12RB);                         // Отображение и разблокировка панели AO для блока расширения 3
        }

        ///<summary>Изменение панели DO блока расширения 3 под M72E12RB (4 DO)</summary>
        private void DO_block3_panelChanged_M72E12RB()
        {
            var do_combos = new List<ComboBox>() { DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo };    // DO сигналы
            var do_labels = new List<Label>() { DO1_bl3Label, DO2_bl3Label, DO3_bl3Label, DO4_bl3Label };       // Подписи DO сигналов

            DO_panelBlock_change(block3_DOpanel, M72E12RB);                         // Изменения для панели DO блока расширения 3
            DO_showEnable_combos_labels(do_combos, do_labels);                      // Отображение и разблокировка элементов

            var do_combos_block = new List<ComboBox>() { DO5bl3_combo, DO6bl3_combo, DO7bl3_combo, DO8bl3_combo };
            var do_labels_block = new List<Label>() { DO5_bl3Label, DO6_bl3Label, DO7_bl3Label, DO8_bl3Label };

            foreach (var el in do_combos_block) { el.Hide(); el.Enabled = false; }
            foreach (var el in do_labels_block) el.Hide();
        }

        ///<summary>Проверка для добавления 1-го блока расширения M72E12RA сигналов DO + UI</summary>
        private void AddFirstBlock_DOUI_M72E12RA(Dictionary<ExpBlock, int> blocks)
        {
            int type = blocks.Count;                                                // Общее количество типов задействованных блоков
            int count_M72E12RA = 0;                                                 // Расчётное количество блоков M72E12RA
            int now_M72E12RA = expansion_blocks                                     // Текущее количество блоков M72E12RA
                .Where(x => x == M72E12RA).Count();
            int total_blocks = 0;

            if (blocks.ContainsKey(M72E12RA))
                count_M72E12RA = blocks[M72E12RA];

            foreach (var el in blocks.Values) total_blocks += el;                   // Общее количество расчётных блоков

            bool one = type == 1 && count_M72E12RA == 1 && now_M72E12RA == 0;

            if (one && total_blocks == 1 && expansion_blocks.Count == 0)
            {
                expansion_blocks.Add(M72E12RA);                                     // Добавление 1-го блока M72E12RA в список блоков расширения
                DO_block1_panelChanged_M72E12RA();                                  // Изменение панели блока расширения 1 для сигналов DO
                UI_block1_panelChanged_M72E12RA();                                  // Изменение панели блока расширения 1 для сигналов UI

                #if DEBUG
                    MessageBox.Show("Добавлен 1-й блок (DO + UI)");
                #endif

                //block1_UIpanel.Show(); block1_UIpanel.Enabled = true;
                //block1_DOpanel.Show(); block1_DOpanel.Enabled = true;
            }
        }

        ///<summary>Проверка для добавления 2-го блока расширения M72E12RA сигналов DO + UI</summary>
        private void AddSecondBlock_DOUI_M72E12RA(Dictionary<ExpBlock, int> blocks)
        {
            int type = blocks.Count;                                                // Общее количество типов задействованных блоков
            int count_M72E12RA = 0;                                                 // Расчётное количество блоков M72E12RA
            int now_M72E12RA = expansion_blocks                                     // Текущее количество блоков M72E12RA
                .Where(x => x == M72E12RA).Count();

            if (blocks.ContainsKey(M72E12RA))
                count_M72E12RA = blocks[M72E12RA];

            bool one = type == 2 && count_M72E12RA == 1 && now_M72E12RA == 0;       // Два разных типа, первый блок M72E12RA
            bool two = type == 1 && count_M72E12RA == 2 && now_M72E12RA == 1;       // Один тип, второй блок M72E12RA

            if ((one || two) && expansion_blocks.Count == 1)
            {
                expansion_blocks.Add(M72E12RA);                                     // Добавление 2-го блока M72E12RA в список блоков расширения
                DO_block2_panelChanged_M72E12RA();                                  // Изменение панели блока расширения 2 для сигналов DO
                UI_block2_panelChanged_M72E12RA();                                  // Изменение панели блока расширения 2 для сигналов UI

                #if DEBUG
                    MessageBox.Show("Добавлен 2-й блок (DO + UI)");
                #endif
            }
        }

        ///<summary>Проверка для добавления 3-го блока расширения M72E12RA сигналов DO + UI</summary>
        private void AddThirdBlock_DOUI_M72E12RA(Dictionary<ExpBlock, int> blocks)
        {
            int total_blocks = 0;                                                   // Общее количество расчётных блоков
            int type = blocks.Count;                                                // Общее количество типов задействованных блоков
            int count_M72E12RA = 0;                                                 // Расчётное количество блоков M72E12RA
            int now_M72E12RA = expansion_blocks                                     // Текущее количество блоков M72E12RA
                .Where(x => x == M72E12RA).Count();

            if (blocks.ContainsKey(M72E12RA))
                count_M72E12RA = blocks[M72E12RA];

            foreach (var el in blocks.Values) total_blocks += el;

            bool one = type == 1 && count_M72E12RA == 3 && now_M72E12RA == 2;       // Только один тип блоков M72E12RA
            bool two = type == 2 && count_M72E12RA == 1 && now_M72E12RA == 0;       // Два одинаковых одного типа, M72E12RA как 3-й блок

            if ((one || two) && expansion_blocks.Count == 2)
            {
                expansion_blocks.Add(M72E12RA);                                     // Добавление 3-го блока M72E12RA в список блоков расширения
                DO_block3_panelChanged_M72E12RA();                                  // Изменение панели блока расширения 3 для сигналов DO
                UI_block3_panelChanged_M72E12RA();                                  // Изменение панели блока расширения 3 для сигналов UI

                #if DEBUG
                    MessageBox.Show("Добавлен 3-й блок (DO + UI)");
                #endif
            }
        }

        ///<summary>Проверка для добавления 1-го блока расширения M72E16NA сигналов UI</summary>
        private void AddFirstBlockUI_M72E16NA(Dictionary<ExpBlock, int> blocks)
        {
            int total_blocks = 0;                                                   // Общее количество расчётных блоков
            foreach (var el in blocks.Values) total_blocks += el;                   

            if (blocks.ContainsKey(M72E16NA) && total_blocks == 1 && expansion_blocks.Count == 0)
            {
                expansion_blocks.Add(M72E16NA);                                     // Добавление 1-го блока M72E16NA в список блоков расширения
                UI_block1_panelChanged_M72E16NA();                                  // Изменение панели блока расширения 1 для сигналов UI

                #if DEBUG
                    MessageBox.Show("Добавлен 1-й блок UI");
                #endif
            }
        }

        ///<summary>Проверка для добавления 2-го блока расширения M72E16NA сигналов UI</summary>
        private void AddSecondBlockUI_M72E16NA(Dictionary<ExpBlock, int> blocks)
        {
            int type = blocks.Count;                                                // Общее количество типов задействованных блоков
            int count_M72E16NA = 0;                                                 // Расчётное количество блоков M72E16NA
            int now_M72E16NA = expansion_blocks                                     // Текущее количество M72E16NA
                .Where(x => x == M72E16NA).Count();

            if (blocks.ContainsKey(M72E16NA)) 
                count_M72E16NA = blocks[M72E16NA];

            bool one = type == 2 && count_M72E16NA == 1 && now_M72E16NA == 0;       // Два разных типа, первые блок M72E16NA
            bool two = type == 1 && count_M72E16NA == 2 && now_M72E16NA == 1;       // Один тип блока, второй блок M72E16NA

            if ((one || two) && expansion_blocks.Count == 1)
            {
                expansion_blocks.Add(M72E16NA);                                     // Добавление 2-го блока M72E16NA в список блоков расширения
                UI_block2_panelChanged_M72E16NA();                                  // Изменение панели блока расширения 2 для сигналов UI

                // Выбор сигнала для comboBox UI, автораспределение 
                var combos_ui = new List<ComboBox>() { 
                    UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo, UI7bl2_combo, UI8bl2_combo,
                    UI9bl2_combo, UI10bl2_combo, UI11bl2_combo, UI12bl2_combo, UI13bl2_combo, UI14bl2_combo, UI15bl2_combo, UI16bl2_combo,
                };

                #if DEBUG
                    MessageBox.Show("Добавлен 2-й блок UI");
                #endif

                /* foreach (var el in combos_ui)
                     if (el.Enabled && el.Items.Count > 1 && el.SelectedIndex == 0)
                         el.SelectedIndex = 1; */
            }
        }

        ///<summary>Проверка для добавления 3-го блока расширения M72E16NA сигналов UI</summary>
        private void AddThirdBlockUI_M72E16NA(Dictionary<ExpBlock, int> blocks)
        {
            int total_blocks = 0;                                                   // Общее количество расчётных блоков
            foreach (var el in blocks.Values) total_blocks += el;

            if (blocks.ContainsKey(M72E16NA) && total_blocks == 3 && expansion_blocks.Count == 2)
            {
                expansion_blocks.Add(M72E16NA);                                     // Добавление 1-го блока M72E16NA в список блоков расширения
                UI_block3_panelChanged_M72E16NA();                                  // Изменение панели блока расширения 3 для сигналов UI

                #if DEBUG
                    MessageBox.Show("Добавлен 3-й блок UI");
                #endif
            }
        }

        ///<summary>Проверка для добавления 1-го блока расширения M72E08RA сигналов DO</summary>
        private void AddFirstBlockDO_M72E08RA(Dictionary<ExpBlock, int> blocks)
        {
            int type = blocks.Count;                                                // Общее количество типов задействованных блоков
            int count_M72E08RA = 0;                                                 // Расчётное количество блоков M72E08RA

            if (blocks.ContainsKey(M72E08RA))
                count_M72E08RA = blocks[M72E08RA];

            if (count_M72E08RA == 1 && type == 1 && expansion_blocks.Count == 0)
            {
                expansion_blocks.Add(M72E08RA);                                     // Добавление 1-го блока M72E08RA в список блоков расширения
                DO_block1_panelChanged_M72E08RA();                                  // Изменение панели блока расширения 1 для сигналов DO

                #if DEBUG
                    MessageBox.Show("Добавлен 1-й блок DO");
                #endif
            }
        }

        ///<summary>Проверка для добавления 2-го блока расширения M72E08RA сигналов DO</summary>
        private void AddSecondBlockDO_M72E08RA(Dictionary<ExpBlock, int> blocks)
        {
            int type = blocks.Count;                                               // Общее количество типов задействованных блоков
            int count_M72E08RA = 0;                                                // Расчётное количество блоков M72E08RA
            int now_M72E08RA = expansion_blocks                                    // Текущее количество блоков M72E08RA
                .Where(x => x == M72E08RA).Count();

            if (blocks.ContainsKey(M72E08RA))
                count_M72E08RA = blocks[M72E08RA];

            bool one = type == 2 && count_M72E08RA == 1 && now_M72E08RA == 0;       // Первый блок, два разных типа
            bool two = type == 1 && count_M72E08RA == 2 && now_M72E08RA == 1;       // Второй блок, один тип

            if ((one || two) && expansion_blocks.Count == 1)
            {
                expansion_blocks.Add(M72E08RA);                                     // Добавление 2-го блока M72E08RA в список блоков расширения                   
                DO_block2_panelChanged_M72E08RA();                                  // Изменение панели блока расширения 2 для сигналов DO

                #if DEBUG
                    MessageBox.Show("Добавлен 2-й блок DO");
                #endif
            }
        }

        ///<summary>Проверка для добавления 3-го блока расширения M72E08RA сигналов DO</summary>
        private void AddThirdBlockDO_M72E08RA(Dictionary<ExpBlock, int> blocks)
        {
            int type = blocks.Count;                                                // Общее количество типов задействованных блоков
            int count_M72E08RA = 0;                                                 // Расчётное количество блоков M72E08RA
            int now_M72E08RA = expansion_blocks                                     // Текущее количество блоков M72E08RA
                .Where(x => x == M72E08RA).Count();

            if (blocks.ContainsKey(M72E08RA))
                count_M72E08RA = blocks[M72E08RA];

            bool one = (type == 2 || type == 3) && count_M72E08RA == 1 && now_M72E08RA == 0;    // Как первый блок DO
            bool two = type == 2 && count_M72E08RA == 2 && now_M72E08RA == 1;                   // Как второй блок DO
            bool three = type == 1 && count_M72E08RA == 3 && now_M72E08RA == 2;                 // Если три одинаковых блока M72E08RA

            if ((one || two || three) && expansion_blocks.Count == 2)
            {
                expansion_blocks.Add(M72E08RA);                                     // Добавление 3-го блока M72E08RA в список блоков расширения

                if (two)                                                            // Как второй блок DO
                {
                    if (!block1_DOpanel.Enabled)                                    // Свободна DO1 панель для распределения
                    {
                        DO_block1_panelChanged_M72E08RA();                          // Изменение панели блока расширения 1 для сигналов DO

                        // Сдвиг для панелей DO2 и DO3
                        if (block1_DOpanel.Enabled)
                            block2_DOpanel.Location = new Point(block2_DOpanel.Location.X,
                                block1_DOpanel.Location.Y + block1_DOpanel.Height);

                        if (block3_DOpanel.Enabled)
                            block3_DOpanel.Location = new Point(block3_DOpanel.Location.X,
                                block2_DOpanel.Location.Y + block2_DOpanel.Height);
                    }
                }
                else                                                                // Все остальные случаи
                {
                    DO_block3_panelChanged_M72E08RA();                              // Изменение панели блока расширения 3 для сигналов DO
                }

                #if DEBUG
                    MessageBox.Show("Добавлен 3-й блок DO");
                #endif
            }
        }

        ///<summary>Проверка для добавления 1-го блока расширения M72E12RB сигналов AO</summary>
        private void AddFirstBlockAO_M72E12RB(Dictionary<ExpBlock, int> blocks)
        {
            int type = blocks.Count;                                                // Общее количество типов задействованных блоков
            int count_M72E12RB = 0;                                                 // Расчётное количество блоков M72E12RB
            int now_M72E12RB = expansion_blocks                                     // Текущее количество блоков M72E12RB
                .Where(x => x == M72E12RB).Count();
            int total_blocks = 0;                                                   // Общее количество расчётных блоков

            if (blocks.ContainsKey(M72E12RB))
                count_M72E12RB = blocks[M72E12RB];

            foreach (var el in blocks.Values) total_blocks += el;

            // Добавление блока, если не было и всего блоков 1
            if (count_M72E12RB == 1 && (type == 1 || type == 2) && now_M72E12RB == 0 && total_blocks == 1)
            {
                expansion_blocks.Add(M72E12RB);                                     // Добавление 1-го блока M72E12RB в список блоков расширения
                AO_block1_panelChanged_M72E12RB();                                  // Отображение и разблокировка панели AO для блока расширения 1
                DO_block1_panelChanged_M72E12RB();                                  // Изменение панели блока расширения 1 для сигналов DO
                UI_block1_panelChanged_M72E12RB();                                  // Изменение панели блока расширения 1 для сигналов UI

                // Выбор сигнала для comboBox UI, автораспределение 
                List<ComboBox> combos_ui = [UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo];

                foreach (var el in combos_ui)
                    if (el.Enabled && el.Items.Count > 1 && el.SelectedIndex == 0)
                        el.SelectedIndex = 1;

                // Выбор сигнала для comboBox DO, автораспределение
                List<ComboBox> combos_do = [DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo];

                foreach (var el in combos_do)
                    if (el.Enabled && el.Items.Count > 1 && el.SelectedIndex == 0)
                        el.SelectedIndex = 1;

                #if DEBUG
                    MessageBox.Show("Добавлен 1-й блок AO");
                #endif
            }
        }

        ///<summary>Проверка для добавления 2-го блока расширения M72E12RB сигналов AO</summary>
        private void AddSecondBlockAO_M72E12RB(Dictionary<ExpBlock, int> blocks)
        {
            int type = blocks.Count;                                                            // Общее количество типов блоков
            int count_M72E12RB = 0;                                                             // Расчётное количество блоков M72E12RB
            int now_M72E12RB = expansion_blocks                                                 // Текущее количество блоков M72E12RB
                .Where(x => x == M72E12RB).Count();
            int total_blocks = 0;                                                               // Общее количество расчётных блоков

            if (blocks.ContainsKey(M72E12RB))
                count_M72E12RB = blocks[M72E12RB];

            foreach (var el in blocks.Values) total_blocks += el;

            bool one = type == 2 && count_M72E12RB == 1 && now_M72E12RB == 0;                   // Первый блок AO, два разных типа
            bool two = (type == 1 || type == 2) && count_M72E12RB == 2 && now_M72E12RB == 1;    // Второй блок AO (одинаковые)

            if ((one || two) && expansion_blocks.Count == 1 && total_blocks == 2)
            {
                expansion_blocks.Add(M72E12RB);                                    // Добавление 2-го блока M72E12RB в список блоков расширения

                if (one)                                                           // Добавляется как первый AO блок
                {
                    if (!block1_DOpanel.Enabled)                                   // При заблокированной 1-й панели добавляется в 3-ю
                    {
                        AO_block3_panelChanged_M72E12RB();                         // Отображение и разблокировка панели AO для блока расширения 3
                        DO_block3_panelChanged_M72E12RB();                         // Изменение панели блока расширения 3 для сигналов DO
                        UI_block3_panelChanged_M72E12RB();                         // Изменение панели блока расширения 3 для сигналов UI

                        // Выбор сигнала для comboBox UI, автораспределение (панель 3) 
                        List<ComboBox> combos_ui = [UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo];

                        foreach (var el in combos_ui)
                            if (el.Enabled && el.Items.Count > 1 && el.SelectedIndex == 0)
                                el.SelectedIndex = 1;

                        // Выбор сигнала для comboBox DO, автораспределение (панель 3)
                        List<ComboBox> combos_do = [DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo];

                        foreach (var el in combos_do)
                            if (el.Enabled && el.Items.Count > 1 && el.SelectedIndex == 0)
                                el.SelectedIndex = 1;

                        // Положение панели DO3
                        if (block2_DOpanel.Enabled)
                            block3_DOpanel.Location = new Point(block3_DOpanel.Location.X,
                                block2_DOpanel.Location.Y + block2_DOpanel.Height);
                    }
                }
                else                                                               // Остальные случаи 
                {
                    AO_block2_panelChanged_M72E12RB();                             // Отображение и разблокировка панели AO для блока расширения 2
                    DO_block2_panelChanged_M72E12RB();                             // Изменение панели блока расширения 2 для сигналов DO
                    UI_block2_panelChanged_M72E12RB();                             // Изменение панели блока расширения 2 для сигналов UI

                    // Выбор сигнала для comboBox UI, автораспределение (панель 2) 
                    List<ComboBox> combos_ui = [UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo];

                    foreach (var el in combos_ui)
                        if (el.Enabled && el.Items.Count > 1 && el.SelectedIndex == 0)
                            el.SelectedIndex = 1;

                    // Выбор сигнала для comboBox DO, автораспределение (панель 2)
                    List<ComboBox> combos_do = [DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo];

                    foreach (var el in combos_do)
                        if (el.Enabled && el.Items.Count > 1 && el.SelectedIndex == 0)
                            el.SelectedIndex = 1;
                }

                #if DEBUG
                    MessageBox.Show("Добавлен 2-й блок AO");
                #endif
            }
        }

        ///<summary>Проверка для добавления 3-го блока расширения M72E12RB сигналов AO</summary>
        private void AddThirdBlockAO_M72E12RB(Dictionary<ExpBlock, int> blocks)
        {
            int type = blocks.Count;                                                // Общее количество задействованных типов блоков
            int count_M72E12RB = 0;                                                 // Расчётное количество блоков
            int now_M72E12RB = expansion_blocks                                     // Текущее количество блоков
                .Where(x => x == M72E12RB).Count();
            int total_blocks = 0;                                                   // Общее количество расчётных блоков

            if (blocks.ContainsKey(M72E12RB))
                count_M72E12RB = blocks[M72E12RB];

            foreach (var el in blocks.Values) total_blocks += el;

            bool one = count_M72E12RB == 3 && now_M72E12RB == 2;                    // Было уже выбрано 2 блока AO   
            bool two = count_M72E12RB == 2 && now_M72E12RB == 1;                    // Был уже выбран 1 блок AO
            bool three = count_M72E12RB == 1 && now_M72E12RB == 0;                  // Первый блок AO, есть два других блока

            if ((one || two || three) && total_blocks == 3 && expansion_blocks.Count == 2)
            {
                expansion_blocks.Add(M72E12RB);                                     // Добавление 3-го блока M72E12RB в список блоков расширения

                if (three || two)                                                   // Добавление блока AO как первого/второго блока AO
                {
                    if (!block1_DOpanel.Enabled)                                    // Свободна DO1 панель для распределения
                    {
                        AO_block1_panelChanged_M72E12RB();                          // Отображение и разблокировка панели AO для блока расширения 1
                        DO_block1_panelChanged_M72E12RB();                          // Панель блока расширения 1 для сигналов DO
                        UI_block1_panelChanged_M72E12RB();                          // Панель блока расширения 1 для сигналов UI

                        // Новое положение для панелей блоков 2 и 3 DO (сдвиг на высоту панели DO1)
                        block2_DOpanel.Location = new Point(block2_DOpanel.Location.X,
                            block2_DOpanel.Location.Y + block1_DOpanel.Height);

                        block3_DOpanel.Location = new Point(block3_DOpanel.Location.X,
                            block3_DOpanel.Location.Y + block1_DOpanel.Height);

                        // Новое положение для панелей блоков 2 и 3 UI (сдвигн на высоту панели UI1)
                        block2_UIpanel.Location = new Point(block2_UIpanel.Location.X,
                            block2_UIpanel.Location.Y + block1_UIpanel.Height);

                        block3_UIpanel.Location = new Point(block3_UIpanel.Location.X,
                            block3_UIpanel.Location.Y + block1_UIpanel.Height);

                        // Новое положение для панелей блоков 2 и 3 AO (сдвигн на высоту панели AO1)
                        block2_AOpanel.Location = new Point(block2_AOpanel.Location.X,
                            block2_AOpanel.Location.Y + block1_AOpanel.Height);

                        block3_AOpanel.Location = new Point(block3_AOpanel.Location.X,
                            block3_AOpanel.Location.Y + block1_AOpanel.Height);

                        if (three)
                        {
                            DOblock2_header.Text =                                      // Возврат заголовка второй панели
                                DOblock2_header.Text.Replace("Блок расширения 1", "Блок расширения 2");
                            DOblock3_header.Text =                                      // Возврат заголова третьей панели
                                DOblock3_header.Text.Replace("Блок расширения 2", "Блок расширения 3");
                        }
                    }
                    else if (!block3_DOpanel.Enabled)                               // Свободна 3DO панель для распределения
                    {
                        AO_block3_panelChanged_M72E12RB();                          // Отображение и разблокировка панели AO для блока расширения 3
                        DO_block3_panelChanged_M72E12RB();                          // Панель блока расширения 3 для сигналов DO
                        UI_block3_panelChanged_M72E12RB();                          // Панель блока расширения 3 для сигналов UI
                    }
                }
                else                                                                // Другие случаи
                {
                    AO_block3_panelChanged_M72E12RB();                              // Отображение и разблокировка панели AO для блока расширения 3
                    DO_block3_panelChanged_M72E12RB();                              // Панель блока расширения 3 для сигналов DO
                    UI_block3_panelChanged_M72E12RB();                              // Панель блока расширения 3 для сигналов UI
                }

                #if DEBUG
                    MessageBox.Show("Добавлен 3-й блок AO");
                #endif
            }
        }

        ///<summary>Удаление выбранной панели блока расширения</summary>
        private void RemoveBlockPanel(string type, int number)
        {
            switch (type)
            {
                case "UI":  // Для панели UI
                    if (number == 1) 
                    {
                        block1_UIpanel.Hide(); block1_UIpanel.Enabled = false;      // Скрытие и блокировка панели UI для блока расширения 1
                        UIblock1_header.Text = "";                                  // Очистка заголовка UI блока расширения 1
                        UICombosBlock_1_Reset();                                    // Скрытие и блокировка элементов UI блока расширения 1
                    }
                    else if (number == 2)
                    {
                        block2_UIpanel.Hide(); block2_UIpanel.Enabled = false;      // Скрытие и блокировка панели UI для блока расширения 2
                        UIblock2_header.Text = "";                                  // Очистка заголовка UI блока расширения 2
                        UICombosBlock_2_Reset();                                    // Скрытие и блокировка элементов UI блока расширения 2
                    }
                    else if (number == 3)
                    {
                        block3_UIpanel.Hide(); block3_UIpanel.Enabled = false;      // Скрытие и блокировка панели UI для блока расширения 3
                        UIblock3_header.Text = "";                                  // Очистка заголовка UI блока расширения 3
                        UICombosBlock_3_Reset();                                    // Скрытие и блокировка элементов UI блока расширения 3
                    }
                    break;
                case "DO":  // Для панели DO
                    if (number == 1)
                    {
                        block1_DOpanel.Hide(); block1_DOpanel.Enabled = false;      // Скрытие и блокировка панели DO для блока расширения 1
                        DOblock1_header.Text = "";                                  // Очистка заголовка DO блока расширения 1
                        DOCombosBlock_1_Reset();                                    // Скрытие и блокировка элементов DO блока расширения 1
                    }
                    else if (number == 2)
                    {
                        block2_DOpanel.Hide(); block2_DOpanel.Enabled = false;      // Скрытие и блокировка панели DO для блока расширения 2
                        DOblock2_header.Text = "";                                  // Очистка заголовка DO блока расширения 2
                        DOCombosBlock_2_Reset();                                    // Скрытие и блокировка элементов DO блока расширения 2
                    }
                    else if (number == 3)
                    {
                        block3_DOpanel.Hide(); block3_DOpanel.Enabled = false;      // Скрытие и блокировка панели DO для блока расширения 3
                        DOblock3_header.Text = "";                                  // Очистка заголовка DO блока расширения 3
                        DOCombosBlock_3_Reset();                                    // Скрытие и блокировка элементов DO блока расширения 3
                    }
                    break;
                case "AO":  // Для панели AO
                    if (number == 1)
                    {
                        block1_AOpanel.Hide(); block1_AOpanel.Enabled = false;      // Скрытие и блокировка панели AO 1
                        AOblock1_header.Text = "";                                  // Очистка заголовка панели AO1
                    }
                    else if (number == 2)
                    {
                        block2_AOpanel.Hide(); block2_AOpanel.Enabled = false;      // Скрытие и блокировка панели AO 2
                        AOblock2_header.Text = "";                                  // Очистка заголовка панели AO2
                    }
                    else if (number == 3)
                    {
                        block3_AOpanel.Hide(); block3_AOpanel.Enabled = false;      // Скрытие и блокировка панели AO 3
                        AOblock3_header.Text = "";                                  // Очистка заголовка панели AO3
                    }
                    break;
            }
        }

        ///<summary>Проверка для удаления 1-го блока расширения M72E12RA сигналов (DO + UI)</summary>
        private void RemoveFirstBlock_DOUI_M72E12RA(Dictionary<ExpBlock, int> blocks)
        {
            // Условия для удаления M72E12RA в качестве 1-го блока расширения
            int count_M72E12RA = 0;                                                 // Расчётное количество блоков M72E12RA
            int now_M72E12RA = expansion_blocks                                     // Текущее количество блоков M72E12RA
                .Where(x => x == M72E12RA).Count();

            if (blocks.ContainsKey(M72E12RA))
                count_M72E12RA = blocks[M72E12RA];

            if (count_M72E12RA == 0 && expansion_blocks.Contains(M72E12RA) && now_M72E12RA == 1)
            {
                expansion_blocks.Remove(M72E12RA);                                  // Удаление блока M72E16NA из списка блоков
                CheckSignals_block1_M72E12RA();                                     // Автораспределение ранее выбранных сигналов с блока M72E12RA
                
                if (!blocks.ContainsKey(M72E16NA))                                  // Если нет подобранного блока UI
                {
                    RemoveBlockPanel("UI", 1);                                      // Скрытие панели UI1
                }

                if (!blocks.ContainsKey(M72E08RA) && !blocks.ContainsKey(M72E12RB)) // Если нет подобранного блока DO + UI или DO
                {
                    RemoveBlockPanel("DO", 1);                                      // Скрытие панели DO1
                }

                #if DEBUG
                    MessageBox.Show("Удален 1-й блок (DO + UI)");
                #endif
            }
        }

        ///<summary>Проверка для удаления 2-го блока расширения M72E12RA сигналов (DO + UI)</summary>
        private void RemoveSecondBlock_DOUI_M72E12RA(Dictionary<ExpBlock, int> blocks)
        {
            // Условия для удаления M72E12RA в качестве 2-го блока расширения
            int type = blocks.Count;                                               // Общее количество задействованных типов блоков
            int count_M72E12RA = 0;                                                // Расчётное количество блоков
            int now_M72E12RA = expansion_blocks
                .Where(x => x == M72E12RA).Count();
            int total_blocks = 0;                                                  // Общее количество расчётных блоков

            if (blocks.ContainsKey(M72E12RA))
                count_M72E12RA = blocks[M72E12RA];

            foreach (var el in blocks.Values) total_blocks += el;

            bool one = count_M72E12RA == 0 && now_M72E12RA == 1;                   // Был один блок M72E12RA
            bool two = count_M72E12RA == 1 && now_M72E12RA == 2;                   // Было два блока M72E12RA

            if ((type == 1 || type == 2) && (one || two) && expansion_blocks.Count == 2)
            {
                expansion_blocks.Remove(M72E12RA);                                  // Удаление блока M72E16NA из списка блоков
                CheckSignals_block2_M72E12RA();                                     // Автораспределение ранее выбранных сигналов с блока M72E12RA
                RemoveBlockPanel("UI", 2);                                          // Скрытие панели UI2
                RemoveBlockPanel("DO", 2);                                          // Скрытие панеил DO2

                #if DEBUG
                    MessageBox.Show("Удален 2-й блок (DO + UI)");
                #endif
            }
        }

        ///<summary>Проверка для удаления 3-го блока расширения M72E12RA сигналов (DO + UI)</summary>
        private void RemoveThirdBlock_DOUI_M72E12RA(Dictionary<ExpBlock, int> blocks)
        {
            int type = blocks.Count;                                                // Общее количество задействованных типов блоков
            int count_M72E12RA = 0;                                                 // Расчётное количество блоков
            int now_M72E12RA = expansion_blocks                                     // Текущее количество блоков
                .Where(x => x == M72E12RA).Count();
            int total_blocks = 0;                                                   // Общее количество расчётных блоков

            if (blocks.ContainsKey(M72E12RA))
                count_M72E12RA = blocks[M72E12RA];

            foreach (var el in blocks.Values) total_blocks += el;

            // Условия для удаления M72E12RA в качестве 3-го блока расширения
            bool one = type == 1 && count_M72E12RA == 2 && now_M72E12RA == 3 &&     // Три одинаковых блока M72E12RA
                total_blocks == 2;       
            bool two = type == 1 && count_M72E12RA == 0 && now_M72E12RA == 1 &&     // Один блок M72E12RA, два другого типа
                (total_blocks == 2 || total_blocks == 3);       

            if ((one || two) && expansion_blocks.Count == 3)
            {
                expansion_blocks.Remove(M72E12RA);                                  // Удаление блока M72E12RA из списка блоков
                CheckSignals_block3_M72E12RA();                                     // Автораспределение ранее выбранных сигналов с блока M72E12RA
                RemoveBlockPanel("UI", 3);                                          // Скрытие панели UI3
                RemoveBlockPanel("DO", 3);                                          // Скрытие панели DO3

                #if DEBUG
                    MessageBox.Show("Удален 3-й блок (DO + UI)");
                #endif
            }
        }

        ///<summary>Проверка для удаления 1-го блока расширения M72E16NA сигналов UI</summary>
        private void RemoveFirstBlockUI_M72E16NA(Dictionary<ExpBlock, int> blocks)
        {
            // Условия для удаления M72E16NA в качестве 1-го блока расширения
            int count = 0;                                                          // Общее количество задействованных блоков
            foreach (var el in blocks.Values) count += el;

            if ((count == 0 || !blocks.ContainsKey(M72E16NA)) && expansion_blocks.Contains(M72E16NA) && expansion_blocks.Count == 1)
            {
                expansion_blocks.Remove(M72E16NA);                                  // Удаление блока M72E16NA из списка блоков
                CheckSignals_block1_M72E16NA();                                     // Автораспределение ранее выбранных сигналов с блока M72E16NA
                
                if (!blocks.ContainsKey(M72E12RB) && !blocks.ContainsKey(M72E12RA))
                {
                    RemoveBlockPanel("UI", 1);                                      // Скрытие панели UI1
                }

                #if DEBUG
                    MessageBox.Show("Удален 1-й блок UI");
                #endif
            }
        }

        ///<summary>Проверка для удаления 2-го блока расширения M72E16NA сигналов UI</summary>
        private void RemoveSecondBlockUI_M72E16NA(Dictionary<ExpBlock, int> blocks)
        {
            // Условия для удаления M72E16NA в качестве 2-го блока расширения
            int type = blocks.Count;                                                // Количество типов задействованных блоков
            int count_M72E16NA = 0;                                                 // Расчётное количество блоков
            int now_M72E16NA = expansion_blocks
                .Where(x => x == M72E16NA).Count();
            int total_blocks = 0;                                                   // Общее количество расчётных блоков

            if (blocks.ContainsKey(M72E16NA))
                count_M72E16NA = blocks[M72E16NA];

            foreach (var el in blocks.Values) total_blocks += el;

            bool one = count_M72E16NA == 0 && now_M72E16NA == 1;                    // Был один блок расширения
            bool two = count_M72E16NA == 1 && now_M72E16NA == 2;                    // Два одинаковых

            if ((type == 2 || type == 1) && (one || two) && expansion_blocks.Count == 2)
            {
                expansion_blocks.Remove(M72E16NA);                                  // Удаление блока M72E16NA из списка блоков
                CheckSignals_block2_M72E16NA();                                     // Автораспределение ранее выбранных сигналов с блока M72E16NA

                // Блокировка UI панели не требуется, если есть 2-й блок DO + UI
                if (!(blocks.ContainsKey(M72E12RA) && total_blocks > 1))
                {
                    RemoveBlockPanel("UI", 2);                                      // Скрытие панели UI2
                }

                #if DEBUG
                    MessageBox.Show("Удален 2-й блок UI");
                #endif
            }
        }

        ///<summary>Проверка для удаления 3-го блока расширения M72E16NA сигналов UI</summary>
        private void RemoveThirdBlockUI_M72E16NA(Dictionary<ExpBlock, int> blocks)
        {
            // Условия для удаления M72E16NA в качестве 3-го блока расширения
            int count = 0;
            foreach (var el in blocks.Values) count += el;

            if (count == 2 && expansion_blocks.Contains(M72E16NA) && expansion_blocks.Count == 3)
            {
                expansion_blocks.Remove(M72E16NA);                                  // Удаление блока M72E16NA из списка блоков
                CheckSignals_block3_M72E16NA();                                     // Автораспределение ранее выбранных сигналов с блока M72E16NA
                RemoveBlockPanel("UI", 3);                                          // Скрытие панели UI3

                #if DEBUG
                    MessageBox.Show("Удален 3-й блок UI");
                #endif
            }
        }

        ///<summary>Проверка для удаления 1-го блока расширения M72E08RA сигналов DO</summary>
        private void RemoveFirstBlockDO_M72E08RA(Dictionary<ExpBlock, int> blocks)
        {
            // Условия для удаления M72E08RA в качестве 1-го блока расширения
            int count_M72E08RA = 0;                                                 // Расчётное количество блоков M72E08RA 

            if (blocks.ContainsKey(M72E08RA))
                count_M72E08RA = blocks[M72E08RA];

            if (count_M72E08RA == 0 && expansion_blocks.Contains(M72E08RA) && expansion_blocks.Count == 1)
            {
                expansion_blocks.Remove(M72E08RA);                                  // Удаление блока M72E08RA из списка блоков
                CheckSignals_block1_M72E08RA();                                     // Автораспределение ранее выбранных сигналов с блока M72E08RA
                
                if (!blocks.ContainsKey(M72E12RA))
                {
                    RemoveBlockPanel("DO", 1);                                      // Скрытие панели DO1
                }

                #if DEBUG
                    MessageBox.Show("Удален 1-й блок DO");
                #endif
            }
        }

        ///<summary>Проверка для удаления 2-го блока расширения M72E08RA сигналов DO</summary>
        private void RemoveSecondBlockDO_M72E08RA(Dictionary<ExpBlock, int> blocks)
        {
            // Условия для удаления M72E08RA в качестве 2-го блока расширения
            int type = blocks.Count;                                               // Общее количество типов задействованных блоков
            int count_M72E08RA = 0;                                                // Расчётное количество блоков M72E08RA
            int now_M72E08RA = expansion_blocks                                    // Текущее количество блоков M72E08RA
                .Where(x => x == M72E08RA).Count(); 

            if (blocks.ContainsKey(M72E08RA))                                      // Количество блоков M72E08RA подобранное
                count_M72E08RA = blocks[M72E08RA];

            bool one = count_M72E08RA == 0 && now_M72E08RA == 1;                   // Был один блок расширения
            bool two = count_M72E08RA == 1 && now_M72E08RA == 2;                   // Два одинаковых

            if ((type == 1 || type == 2) && (one || two) && expansion_blocks.Count == 2)
            {
                expansion_blocks.Remove(M72E08RA);                                  // Удаление блока M72E08RA из списка блоков

                if (two)                                                            // Два одинаковых блока M72E08RA
                {
                    if (DOblock3_header.Text.Contains("M72E08RA"))
                    {
                        CheckSignals_block3_M72E08RA();                             // Автораспределение ранее выбранных сигналов блока 3
                        RemoveBlockPanel("DO", 3);                                  // Скрытие панели DO3
                    } 
                    else if (DOblock2_header.Text.Contains("M72E08RA"))
                    {
                        CheckSignals_block2_M72E08RA();                             // Автораспределение ранее выбранных сигналов блока 2
                        RemoveBlockPanel("DO", 2);                                  // Скрытие панели DO2
                    }
                }
                else if (one)                                                       // Один блок DO
                {
                    // Панель 1 заблокирована и панель 3 - не блок DO
                    if (block3_DOpanel.Enabled && !DOblock3_header.Text.Contains("M72E08RA"))
                    {
                        CheckSignals_block2_M72E08RA();                             // Автораспределение ранее выбранных сигналов блока 2
                        RemoveBlockPanel("DO", 2);                                  // Скрытие панели DO2

                        // Положение панели DO3
                        block3_DOpanel.Location = new Point(block3_DOpanel.Location.X,
                            plk_DOpanel.Location.Y + plk_DOpanel.Height);

                        // Третья UI панель используется как первая (2 -> 1)
                        UIblock3_header.Text =
                            UIblock3_header.Text.Replace("Блок расширения 2", "Блок расширения 1");

                        // Третья DO панель используется как первая (2 -> 1)
                        DOblock3_header.Text =
                            DOblock3_header.Text.Replace("Блок расширения 2", "Блок расширения 1");

                        // Третья AO панель используется как первая (2 -> 1)
                        AOblock3_header.Text =
                            AOblock3_header.Text.Replace("Блок расширения 2", "Блок расширения 1");
                    }
                }
                else                                                                // Использование панели DO2 в остальных случаях
                {
                    CheckSignals_block2_M72E08RA();                                 // Автораспределение ранее выбранных сигналов блока 2
                    RemoveBlockPanel("DO", 2);                                      // Скрытие панели DO2
                }

                #if DEBUG
                    MessageBox.Show("Удален 2-й блок DO");
                #endif
            } 
        }

        ///<summary>Проверка для удаления 3-го блока расширения M72E08RA сигналов DO</summary>
        private void RemoveThirdBlockDO_M72E08RA(Dictionary<ExpBlock, int> blocks)
        {
            // Условия для удаления M72E08RA в качестве 3-го блока расширения
            int type = blocks.Count;                                                // Общее количество типов задействованных блоков
            int count_M72E08RA = 0;                                                 // Расчётное количество блоков M72E08RA
            int now_M72E08RA = expansion_blocks                                     // Текущее количество блоков M72E08RA
                .Where(x => x == M72E08RA).Count();

            if (blocks.ContainsKey(M72E08RA))                                       // Количество блоков M72E08RA подобранное
                count_M72E08RA = blocks[M72E08RA];

            bool one = count_M72E08RA == 0 && now_M72E08RA == 1;                    // Только один блок M72E08RA
            bool two = count_M72E08RA == 1 && now_M72E08RA == 2;                    // Было два блока M72E08RA
            bool three = count_M72E08RA == 2 && now_M72E08RA == 3;                  // Только блоки M72E08RA

            if ((type == 1 || type == 2) && (one || two || three) && expansion_blocks.Count == 3)
            {
                expansion_blocks.Remove(M72E08RA);                                  // Удаление блока M72E08RA из списка блоков

                if (two)                                                            // Было два блока DO
                {
                    if (block1_DOpanel.Enabled && DOblock1_header.Text.Contains("M72E08RA"))
                    {
                        CheckSignals_block1_M72E08RA();                             // Автораспределение ранее выбранных сигналов с блока 1
                        RemoveBlockPanel("DO", 1);                                  // Скрытие панели DO1

                        // Положение для панелей DO2 и DO3 с учётом блокировки панели DO1
                        if (block2_DOpanel.Enabled)
                            block2_DOpanel.Location = new Point(block2_DOpanel.Location.X,
                                plk_DOpanel.Location.Y + plk_DOpanel.Height);
                        if (block3_DOpanel.Enabled)
                        {
                            if (block2_DOpanel.Enabled)
                                block3_DOpanel.Location = new Point(block3_DOpanel.Location.X,
                                    block2_DOpanel.Location.Y + block2_DOpanel.Height);
                            else
                                block3_DOpanel.Location = new Point(block3_DOpanel.Location.X,
                                    plk_DOpanel.Location.Y + plk_DOpanel.Height);
                        }

                        // Распределение сигналов, выбор comboBox панели DO2
                        if (block2_DOpanel.Enabled && DOblock2_header.Text.Contains("M72E08RA"))
                        {
                            List<ComboBox> do_block2 = [
                                DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo, DO5bl2_combo, DO6bl2_combo, DO7bl2_combo, DO8bl2_combo];

                            foreach (var el in do_block2)                                       // Выбор сигнала для блока DO2
                                if (el.Enabled && el.Items.Count > 1 && el.SelectedIndex == 0)
                                    el.SelectedIndex = 1;
                        }
                    }
                }
                else                                                                // Остальные случаи
                {
                    CheckSignals_block3_M72E08RA();                                 // Автораспределение ранее выбранных сигналов с блока 3
                    RemoveBlockPanel("DO", 3);                                      // Скрытие панели DO3

                    /*
                    // Если третий блок не M72E12RA
                    if (expansion_blocks.Count == 3 && expansion_blocks[2] != M72E12RA)
                    {
                        RemoveBlockPanel("DO", 3);                                  // Скрытие панели DO3
                    } */
                }

                #if DEBUG
                    MessageBox.Show("Удален 3-й блок DO");
                #endif
            }
        }

        ///<summary>Проверка для удаления 1-го блока расширения M72E12RB сигналов AO</summary>
        private void RemoveFirstBlockAO_M72E12RB(Dictionary<ExpBlock, int> blocks)
        {
            // Условия для удаления M72E12RB в качестве 1-го блока расширения
            int count_M72E12RB = 0;                                                 // Расчётное количество блоков M72E12RB
            int now_M72E12RB = expansion_blocks                                     // Текущее количество блоков M72E12RB
                .Where(x => x == M72E12RB).Count();
            int total_blocks = 0;                                                   // Общее количество расчётных блоков

            if (blocks.ContainsKey(M72E12RB))
                count_M72E12RB = blocks[M72E12RB];

            foreach (var el in blocks.Values) total_blocks += el;

            if (count_M72E12RB == 0 && now_M72E12RB == 1 && (total_blocks == 0 || total_blocks == 1))
            {
                expansion_blocks.Remove(M72E12RB);                                  // Удаление блока M72E12RB из списка блоков
                CheckSignals_block1_M72E12RB();                                     // Автораспределение ранее выбранных сигналов с блока 1

                if (AOblock1_header.Text.Contains("M72E12RB"))
                {
                    RemoveBlockPanel("AO", 1);                                      // Скрытие панели AO1
                }
                else if (AOblock2_header.Text.Contains("M72E12RB"))
                {
                    RemoveBlockPanel("AO", 1);                                      // Скрытие панели AO2
                }

                // Проверка панелей DO
                if (!blocks.ContainsKey(M72E12RA) && !blocks.ContainsKey(M72E08RA))
                {
                    RemoveBlockPanel("DO", 1);                                      // Скрытие панели DO1
                }

                // Проверка панелей UI
                if (!blocks.ContainsKey(M72E12RA) && !blocks.ContainsKey(M72E16NA)) {
                    RemoveBlockPanel("UI", 1);                                      // Скрытие панели UI1
                }

                #if DEBUG 
                    MessageBox.Show("Удален 1-й блок AO");
                #endif
            }
        }

        ///<summary>Проверка для удаления 2-го блока расширения M72E12RB сигналов AO</summary>
        private void RemoveSecondBlockAO_M72E12RB(Dictionary<ExpBlock, int> blocks)
        {
            // Условия для удаления M72E12RB в качестве второго блока расширения
            int type = blocks.Count;                                                // Общее количество задействованных блоков
            int count_M72E12RB = 0;                                                 // Расчётное количество блоков
            int now_M72E12RB = expansion_blocks
                .Where(x => x == M72E12RB).Count();
            int total_blocks = 0;                                                   // Общее количество расчётных блоков

            if (blocks.ContainsKey(M72E12RB))                                       // Количество блоков M72E12RB подобранное
                count_M72E12RB = blocks[M72E12RB];

            foreach (var el in blocks.Values) total_blocks += el;

            bool one = count_M72E12RB == 0 && now_M72E12RB == 1;                    // Был один блок расширения
            bool two = count_M72E12RB == 1 && now_M72E12RB == 2;                    // Два одинаковых

            if ((type == 1 || type == 2) && (one || two) && expansion_blocks.Count == 2)
            {
                expansion_blocks.Remove(M72E12RB);                                  // Удаление блока из списка блоков

                if (one)                                                            // Один блок AO
                {
                    if (block1_DOpanel.Enabled && DOblock1_header.Text.Contains("M72E12RB"))
                    {
                        CheckSignals_block1_M72E12RB();                             // Автораспределение ранее выбранных сигналов с блока 1
                        RemoveBlockPanel("AO", 1);                                  // Скрытие панели AO1
                        RemoveBlockPanel("DO", 1);                                  // Скрытие панели DO1
                        RemoveBlockPanel("UI", 1);                                  // Скрытие панели UI1

                        // Положение панели DO2
                        block2_DOpanel.Location = new Point(block2_DOpanel.Location.X,
                            plk_DOpanel.Location.Y + plk_DOpanel.Height);

                        // Вторая панель используется как первая
                        DOblock2_header.Text =                                      
                            DOblock2_header.Text.Replace("Блок расширения 2", "Блок расширения 1");

                        // Выбор сигналов DO в панели блока 2
                        List<ComboBox> do_block2 = [DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo, DO5bl2_combo,
                            DO6bl2_combo, DO7bl2_combo, DO8bl2_combo];

                        foreach (var el in do_block2)                               // Выбор сигнала для блока DO2
                            if (el.Enabled && el.Items.Count > 1 && el.SelectedIndex == 0)
                                el.SelectedIndex = 1;
                    }
                }
                else                                                                // Другие варианты
                {
                    CheckSignals_block2_M72E12RB(blocks, total_blocks);             // Автораспределение ранее выбранных сигналов с блока 2
                    RemoveBlockPanel("AO", 2);                                      // Скрытие панели AO2

                    // Блокировка DO панели не требуется, если есть 2-й блок DO или (DO + UI)
                    if (!((blocks.ContainsKey(M72E08RA) || blocks.ContainsKey(M72E12RA)) && total_blocks > 1))
                    {
                        RemoveBlockPanel("DO", 2);                                  // Скрытие панели DO2
                    }

                    // Блокировка UI панели не требуется, если есть 2-й блок UI или (DO + UI)
                    if (!((blocks.ContainsKey(M72E16NA) || blocks.ContainsKey(M72E12RA)) && total_blocks > 1))
                    {
                        RemoveBlockPanel("UI", 2);                                  // Скрытие панели UI2
                    }
                }

                #if DEBUG
                    MessageBox.Show("Удален 2-й блок AO");
                #endif
            }
        }

        ///<summary>Проверка для удаления 3-го блока расширения M72E12RB сигналов AO</summary>
        private void RemoveThirdBlockAO_M72E12RB(Dictionary<ExpBlock, int> blocks)
        {
            int type = blocks.Count;                                                // Общее количество типов задействованных блоков
            int count_M72E12RB = 0;                                                 // Расчётное количество блоков M72E12RB
            int now_M72E12RB = expansion_blocks                                     // Текущее количество блоков M72E12RB
                .Where(x => x == M72E12RB).Count();
            int total_blocks = 0;                                                   // Общее количество расчётных блоков

            if (blocks.ContainsKey(M72E12RB))
                count_M72E12RB = blocks[M72E12RB];

            foreach (var el in blocks.Values) total_blocks += el;

            bool one = (type == 1 || type == 2) && count_M72E12RB == 0 && now_M72E12RB == 1;    // Один или два типа блоков, был один блок AO
            bool two = type == 2 && count_M72E12RB == 1 && now_M72E12RB == 2;                   // Два разных типа, два блока AO
            bool three = type == 1 && count_M72E12RB == 2 && now_M72E12RB == 3;                 // Только 1 тип и три блока AO

            if ((one || two || three) && expansion_blocks.Count == 3)
            {
                expansion_blocks.Remove(M72E12RB);                                          // Удаление блока из списка блоков

                if (one)                                                                    // Был только один блок AO
                {
                    if (block3_DOpanel.Enabled && DOblock3_header.Text.Contains("M72E12RB"))
                    {
                        CheckSignals_block3_M72E12RB();                                     // Автораспределение ранее выбранных сигналов блока 3
                        RemoveBlockPanel("AO", 3);                                          // Скрытие панели AO3
                        RemoveBlockPanel("DO", 3);                                          // Скрытие панели DO3
                        RemoveBlockPanel("UI", 3);                                          // Скрытие панели UI3
                    }
                    else if (block2_DOpanel.Enabled && DOblock2_header.Text.Contains("M72E12RB"))
                    {
                        CheckSignals_block2_M72E12RB(blocks, total_blocks);                 // Автораспределение ранее выбранных сигналов блока 2
                        RemoveBlockPanel("AO", 2);                                          // Скрытие панели AO2
                        RemoveBlockPanel("DO", 2);                                          // Скрытие панели DO2
                        RemoveBlockPanel("UI", 2);                                          // Скрытие панели UI2
                    }
                    else if (block1_DOpanel.Enabled && DOblock1_header.Text.Contains("M72E12RB"))
                    {
                        CheckSignals_block1_M72E12RB();                                     // Автораспределение ранее выбранных сигналов блока 1
                        RemoveBlockPanel("AO", 1);                                          // Скрытие панели AO1
                        RemoveBlockPanel("DO", 1);                                          // Скрытие панели DO1
                        RemoveBlockPanel("UI", 1);                                          // Скрытие панели UI1

                        // Новое расположение для панелей блоков 2 и 3 DO (сдвиг на высоту панели DO1)
                        block2_DOpanel.Location = new Point(block2_DOpanel.Location.X,
                            block2_DOpanel.Location.Y - block1_DOpanel.Height);

                        block3_DOpanel.Location = new Point(block3_DOpanel.Location.X,
                            block3_DOpanel.Location.Y - block1_DOpanel.Height);

                        // Выбор сигналов DO в панелях блока 2 и 3
                        List<ComboBox> do_block2 = [DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo, DO5bl2_combo,
                            DO6bl2_combo, DO7bl2_combo, DO8bl2_combo];

                        List<ComboBox> do_block3 = [DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo, DO5bl3_combo,
                            DO6bl3_combo, DO7bl3_combo, DO8bl3_combo];

                        foreach (var el in do_block2)                                       // Выбор сигнала для блока DO2
                            if (el.Enabled && el.Items.Count > 1 && el.SelectedIndex == 0)
                                el.SelectedIndex = 1;

                        foreach (var el in do_block3)                                       // Выбор сигнала для блока DO3
                            if (el.Enabled && el.Items.Count > 1 && el.SelectedIndex == 0)
                                el.SelectedIndex = 1;

                        DOblock2_header.Text =                                              // Вторая панель используется как первая
                            DOblock2_header.Text.Replace("Блок расширения 2", "Блок расширения 1");     
                        DOblock3_header.Text =                                              // Третья панель используется как вторая
                            DOblock3_header.Text.Replace("Блок расширения 3", "Блок расширения 2");     
                    }
                }
                else                                                                        // Другие случаи
                {
                    CheckSignals_block3_M72E12RB();                                         // Автораспределение ранее выбранных сигналов блока 3
                    RemoveBlockPanel("AO", 3);                                              // Скрытие панели AO3
                    RemoveBlockPanel("DO", 3);                                              // Скрытие панели DO3
                    RemoveBlockPanel("UI", 3);                                              // Скрытие панели UI3
                }

                #if DEBUG
                    MessageBox.Show("Удален 3-й блок AO");
                #endif
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
        private void DOCombosBlock_1_Reset()
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
        private void DOCombosBlock_2_Reset()
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
        private void DOCombosBlock_3_Reset()
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
        private void UICombosBlock_1_Reset()
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

            //UIblock1_header.Text = "";  // Очистка заголовка для панели UI блока расширения 1

            foreach (var el in ui_combos) { el.SelectedIndex = 0; el.Hide(); el.Enabled = false; }
            foreach (var el in ui_type_combos) { el.SelectedIndex = 0; el.Hide(); el.Enabled = false; }
            foreach (var el in ui_labels) el.Hide();
        }

        /// <summary>Изначальная блокировка и скрытие comboBox UI блока расширения 2, скрытие подписей сигналов</summary>
        private void UICombosBlock_2_Reset()
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

            //UIblock2_header.Text = "";  // Очистка заголовка для панели UI блока расширения 2

            foreach (var el in ui_combos) { el.SelectedIndex = 0; el.Hide(); el.Enabled = false; }
            foreach (var el in ui_type_combos) { el.SelectedIndex = 0; el.Hide(); el.Enabled = false; }
            foreach (var el in ui_labels) el.Hide();
        }

        /// <summary>Изначальная блокировка и скрытие comboBox UI блока расширения 3, скрытие подписей сигналов</summary>
        private void UICombosBlock_3_Reset()
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
