using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Moderon
{
    public partial class Form1 : Form
    {
        JsonObject json_read;                           // Объект для загрузки файла
        bool ignoreEvents = false;                      // Игнорирование событий для элементов при загрузке файла

        ///<summary>Нажали "Загрузить" в главном меню</summary> 
        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!initialConfigure)      // Если не начальная расстановка
            {
                const string
                    MESSAGE = "Загрузка сбросит текущую конфигурацию. Вы уверены?",
                    CAPTION = "Загрузка файла";

                var result = MessageBox.Show(MESSAGE, CAPTION, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No) return;
            }

            ResetButton_Click(sender, e);                               // Первоначальный сброс перед загрузкой файла
            LoadJsonFile();                                             // Загрузка файла JSON в программу
            if (!correctFile) return;                                   // Выход при некорректном файле

            ignoreEvents = true;                                        // Отключение событий на время загрузки
            initialConfigure = false;                                   // Сброс признака начальной расстановки при загрузке

            if (json_read != null)                                      // Загрузочный файл содержит информацию
            {
                LoadManBlocksState();                                   // Загрузка режимов блоков расширения
                LoadCheckBoxAll();                                      // Загрузка для всех сheckBox
                LoadComboBoxElemAll();                                  // Загрузка для всех comboBox элементов
                LoadLabelSignalsAll();                                  // Загрузка для подписей кодов таблицы сигналов
                LoadComboItemsSignals();                                // Загрузка элементов для comboBox таблицы сигналов
                LoadComboSignalsAll();                                  // Загрузка состояний для comboBox таблицы сигналов
                LoadSignalArrays();                                     // Загрузка для массива сигналов
                LoadPanelsEnable();                                     // Загрузка статуса доступности панелей блоков расширения
                LoadPanelsHeaders();                                    // Загрузка заголовков панелей блоков расширения
                LoadExpBlocks();                                        // Загрузка количества и типов блоков расширения
                LoadComboIndex();                                       // Загрузка выбранного ранее индекса для comboBox сигналов
                LoadComboText();                                        // Загрузка выбранного ранее названия для comboBox сигналов
                Load_UITypeEnable();                                    // Загрузка типов для UI сигналов
                LoadPlkType();                                          // Загрузка ранее выбранного типа контроллера
                CheckPanelBlocks(CalcExpBlocks_typeNums());             // Проверка отображения панели блоков расширения Form1, тип и количество
            }

            ignoreEvents = false;                                       // Возврат активации событий
        }

        ///<summary>Загрузка доступности типов UI сигналов</summary>
        private void Load_UITypeEnable()
        {
            // UI сигналы, тип сигнала
            var ui_combos_type = new List<ComboBox>()
            {
                // ПЛК
                UI1_typeCombo, UI2_typeCombo, UI3_typeCombo, UI4_typeCombo, UI5_typeCombo, UI6_typeCombo, UI7_typeCombo, UI8_typeCombo,
                UI9_typeCombo, UI10_typeCombo, UI11_typeCombo,
                // Блок расширения 1
                UI1bl1_typeCombo, UI2bl1_typeCombo, UI3bl1_typeCombo, UI4bl1_typeCombo, UI5bl1_typeCombo, UI6bl1_typeCombo, UI7bl1_typeCombo,
                UI8bl1_typeCombo, UI9bl1_typeCombo, UI10bl1_typeCombo, UI11bl1_typeCombo, UI12bl1_typeCombo, UI13bl1_typeCombo, UI14bl1_typeCombo,
                UI15bl1_typeCombo, UI16bl1_typeCombo,
                // Блок расширения 2
                UI1bl2_typeCombo, UI2bl2_typeCombo, UI3bl2_typeCombo, UI4bl2_typeCombo, UI5bl2_typeCombo, UI6bl2_typeCombo, UI7bl2_typeCombo,
                UI8bl2_typeCombo, UI9bl2_typeCombo, UI10bl2_typeCombo, UI11bl2_typeCombo, UI12bl2_typeCombo, UI13bl2_typeCombo, UI14bl2_typeCombo,
                UI15bl2_typeCombo, UI16bl2_typeCombo,
                // Блок расширения 3
                UI1bl3_typeCombo, UI2bl3_typeCombo, UI3bl3_typeCombo, UI4bl3_typeCombo, UI5bl3_typeCombo, UI6bl3_typeCombo, UI7bl3_typeCombo,
                UI8bl3_typeCombo, UI9bl3_typeCombo, UI10bl3_typeCombo, UI11bl3_typeCombo, UI12bl3_typeCombo, UI13bl3_typeCombo, UI14bl3_typeCombo,
                UI15bl3_typeCombo, UI16bl3_typeCombo,
            };

            for (int i = 0; i < ui_combos_type.Count; i++)
                ui_combos_type[i].Enabled = json_read.UiTypeEnable[ui_combos_type[i].Name];
        }

        ///<summary>Загрузка выбранного ранее индекса для comboBox сигналов</summary>
        private void LoadComboIndex()
        {
            // AO сигналы ПЛК
            AO1combo_index = json_read.ComboIndex["AO1combo_index"];
            AO2combo_index = json_read.ComboIndex["AO2combo_index"];
            AO3combo_index = json_read.ComboIndex["AO3combo_index"];
            // AO сигналы блок расширения 1
            AO1bl1combo_index = json_read.ComboIndex["AO1bl1combo_index"];
            AO2bl1combo_index = json_read.ComboIndex["AO2bl1combo_index"];
            // AO сигналы блок расширения 2
            AO1bl2combo_index = json_read.ComboIndex["AO1bl2combo_index"];
            AO2bl2combo_index = json_read.ComboIndex["AO2bl2combo_index"];
            // AO сигналы блок расширения 3
            AO1bl3combo_index = json_read.ComboIndex["AO1bl3combo_index"];
            AO2bl3combo_index = json_read.ComboIndex["AO2bl3combo_index"];
            // DO сигналы ПЛК
            DO1combo_index = json_read.ComboIndex["DO1combo_index"];
            DO2combo_index = json_read.ComboIndex["DO2combo_index"];
            DO3combo_index = json_read.ComboIndex["DO3combo_index"];
            DO4combo_index = json_read.ComboIndex["DO4combo_index"];
            DO5combo_index = json_read.ComboIndex["DO5combo_index"];
            DO6combo_index = json_read.ComboIndex["DO6combo_index"];
            // DO сигналы блок расширения 1
            DO1bl1combo_index = json_read.ComboIndex["DO1bl1combo_index"];
            DO2bl1combo_index = json_read.ComboIndex["DO2bl1combo_index"];
            DO3bl1combo_index = json_read.ComboIndex["DO3bl1combo_index"];
            DO4bl1combo_index = json_read.ComboIndex["DO4bl1combo_index"];
            DO5bl1combo_index = json_read.ComboIndex["DO5bl1combo_index"];
            DO6bl1combo_index = json_read.ComboIndex["DO6bl1combo_index"];
            DO7bl1combo_index = json_read.ComboIndex["DO7bl1combo_index"];
            DO8bl1combo_index = json_read.ComboIndex["DO8bl1combo_index"];
            // DO сигналы блок расширения 2
            DO1bl2combo_index = json_read.ComboIndex["DO1bl2combo_index"];
            DO2bl2combo_index = json_read.ComboIndex["DO2bl2combo_index"];
            DO3bl2combo_index = json_read.ComboIndex["DO3bl2combo_index"];
            DO4bl2combo_index = json_read.ComboIndex["DO4bl2combo_index"];
            DO5bl2combo_index = json_read.ComboIndex["DO5bl2combo_index"];
            DO6bl2combo_index = json_read.ComboIndex["DO6bl2combo_index"];
            DO7bl2combo_index = json_read.ComboIndex["DO7bl2combo_index"];
            DO8bl2combo_index = json_read.ComboIndex["DO8bl2combo_index"];
            // DO сигналы блок расширения 3
            DO1bl3combo_index = json_read.ComboIndex["DO1bl3combo_index"];
            DO2bl3combo_index = json_read.ComboIndex["DO2bl3combo_index"];
            DO3bl3combo_index = json_read.ComboIndex["DO3bl3combo_index"];
            DO4bl3combo_index = json_read.ComboIndex["DO4bl3combo_index"];
            DO5bl3combo_index = json_read.ComboIndex["DO5bl3combo_index"];
            DO6bl3combo_index = json_read.ComboIndex["DO6bl3combo_index"];
            DO7bl3combo_index = json_read.ComboIndex["DO7bl3combo_index"];
            DO8bl3combo_index = json_read.ComboIndex["DO8bl3combo_index"];
            // UI сигналы ПЛК
            UI1combo_index = json_read.ComboIndex["UI1combo_index"];
            UI2combo_index = json_read.ComboIndex["UI2combo_index"];
            UI3combo_index = json_read.ComboIndex["UI3combo_index"];
            UI4combo_index = json_read.ComboIndex["UI4combo_index"];
            UI5combo_index = json_read.ComboIndex["UI5combo_index"];
            UI6combo_index = json_read.ComboIndex["UI6combo_index"];
            UI7combo_index = json_read.ComboIndex["UI7combo_index"];
            UI8combo_index = json_read.ComboIndex["UI8combo_index"];
            UI9combo_index = json_read.ComboIndex["UI9combo_index"];
            UI10combo_index = json_read.ComboIndex["UI10combo_index"];
            UI11combo_index = json_read.ComboIndex["UI11combo_index"];
            // UI сигналы блок расширения 1
            UI1bl1combo_index = json_read.ComboIndex["UI1bl1combo_index"];
            UI2bl1combo_index = json_read.ComboIndex["UI2bl1combo_index"];
            UI3bl1combo_index = json_read.ComboIndex["UI3bl1combo_index"];
            UI4bl1combo_index = json_read.ComboIndex["UI4bl1combo_index"];
            UI5bl1combo_index = json_read.ComboIndex["UI5bl1combo_index"];
            UI6bl1combo_index = json_read.ComboIndex["UI6bl1combo_index"];
            UI7bl1combo_index = json_read.ComboIndex["UI7bl1combo_index"];
            UI8bl1combo_index = json_read.ComboIndex["UI8bl1combo_index"];
            UI9bl1combo_index = json_read.ComboIndex["UI9bl1combo_index"];
            UI10bl1combo_index = json_read.ComboIndex["UI10bl1combo_index"];
            UI11bl1combo_index = json_read.ComboIndex["UI11bl1combo_index"];
            UI12bl1combo_index = json_read.ComboIndex["UI12bl1combo_index"];
            UI13bl1combo_index = json_read.ComboIndex["UI13bl1combo_index"];
            UI14bl1combo_index = json_read.ComboIndex["UI14bl1combo_index"];
            UI15bl1combo_index = json_read.ComboIndex["UI15bl1combo_index"];
            UI16bl1combo_index = json_read.ComboIndex["UI16bl1combo_index"];
            // UI сигналы блок расширения 2
            UI1bl2combo_index = json_read.ComboIndex["UI1bl2combo_index"];
            UI2bl2combo_index = json_read.ComboIndex["UI2bl2combo_index"];
            UI3bl2combo_index = json_read.ComboIndex["UI3bl2combo_index"];
            UI4bl2combo_index = json_read.ComboIndex["UI4bl2combo_index"];
            UI5bl2combo_index = json_read.ComboIndex["UI5bl2combo_index"];
            UI6bl2combo_index = json_read.ComboIndex["UI6bl2combo_index"];
            UI7bl2combo_index = json_read.ComboIndex["UI7bl2combo_index"];
            UI8bl2combo_index = json_read.ComboIndex["UI8bl2combo_index"];
            UI9bl2combo_index = json_read.ComboIndex["UI9bl2combo_index"];
            UI10bl2combo_index = json_read.ComboIndex["UI10bl2combo_index"];
            UI11bl2combo_index = json_read.ComboIndex["UI11bl2combo_index"];
            UI12bl2combo_index = json_read.ComboIndex["UI12bl2combo_index"];
            UI13bl2combo_index = json_read.ComboIndex["UI13bl2combo_index"];
            UI14bl2combo_index = json_read.ComboIndex["UI14bl2combo_index"];
            UI15bl2combo_index = json_read.ComboIndex["UI15bl2combo_index"];
            UI16bl2combo_index = json_read.ComboIndex["UI16bl2combo_index"];
            // UI сигналы блок расширения 3
            UI1bl3combo_index = json_read.ComboIndex["UI1bl3combo_index"];
            UI2bl3combo_index = json_read.ComboIndex["UI2bl3combo_index"];
            UI3bl3combo_index = json_read.ComboIndex["UI3bl3combo_index"];
            UI4bl3combo_index = json_read.ComboIndex["UI4bl3combo_index"];
            UI5bl3combo_index = json_read.ComboIndex["UI5bl3combo_index"];
            UI6bl3combo_index = json_read.ComboIndex["UI6bl3combo_index"];
            UI7bl3combo_index = json_read.ComboIndex["UI7bl3combo_index"];
            UI8bl3combo_index = json_read.ComboIndex["UI8bl3combo_index"];
            UI9bl3combo_index = json_read.ComboIndex["UI9bl3combo_index"];
            UI10bl3combo_index = json_read.ComboIndex["UI10bl3combo_index"];
            UI11bl3combo_index = json_read.ComboIndex["UI11bl3combo_index"];
            UI12bl3combo_index = json_read.ComboIndex["UI12bl3combo_index"];
            UI13bl3combo_index = json_read.ComboIndex["UI13bl3combo_index"];
            UI14bl3combo_index = json_read.ComboIndex["UI14bl3combo_index"];
            UI15bl3combo_index = json_read.ComboIndex["UI15bl3combo_index"];
            UI16bl3combo_index = json_read.ComboIndex["UI16bl3combo_index"];
        }

        ///<summary>Загрузка выбранного ранее названия для comboBox сигналов</summary>
        private void LoadComboText()
        {
            // AO сигналы ПЛК
            AO1combo_text = json_read.ComboText["AO1combo_text"];
            AO2combo_text = json_read.ComboText["AO2combo_text"];
            AO3combo_text = json_read.ComboText["AO3combo_text"];
            // AO сигналы блок расширения 1
            AO1bl1combo_text = json_read.ComboText["AO1bl1combo_text"];
            AO2bl1combo_text = json_read.ComboText["AO2bl1combo_text"];
            // AO сигналы блок расширения 2
            AO1bl2combo_text = json_read.ComboText["AO1bl2combo_text"];
            AO2bl2combo_text = json_read.ComboText["AO2bl2combo_text"];
            // AO сигналы блок расширения 3
            AO1bl3combo_text = json_read.ComboText["AO1bl3combo_text"];
            AO2bl3combo_text = json_read.ComboText["AO2bl3combo_text"];
            // DO сигналы ПЛК
            DO1combo_text = json_read.ComboText["DO1combo_text"];
            DO2combo_text = json_read.ComboText["DO2combo_text"];
            DO3combo_text = json_read.ComboText["DO3combo_text"];
            DO4combo_text = json_read.ComboText["DO4combo_text"];
            DO5combo_text = json_read.ComboText["DO5combo_text"];
            DO6combo_text = json_read.ComboText["DO6combo_text"];
            // DO сигналы блок расширения 1
            DO1bl1combo_text = json_read.ComboText["DO1bl1combo_text"];
            DO2bl1combo_text = json_read.ComboText["DO2bl1combo_text"];
            DO3bl1combo_text = json_read.ComboText["DO3bl1combo_text"];
            DO4bl1combo_text = json_read.ComboText["DO4bl1combo_text"];
            DO5bl1combo_text = json_read.ComboText["DO5bl1combo_text"];
            DO6bl1combo_text = json_read.ComboText["DO6bl1combo_text"];
            DO7bl1combo_text = json_read.ComboText["DO7bl1combo_text"];
            DO8bl1combo_text = json_read.ComboText["DO8bl1combo_text"];
            // DO сигналы блок расширения 2
            DO1bl2combo_text = json_read.ComboText["DO1bl2combo_text"];
            DO2bl2combo_text = json_read.ComboText["DO2bl2combo_text"];
            DO3bl2combo_text = json_read.ComboText["DO3bl2combo_text"];
            DO4bl2combo_text = json_read.ComboText["DO4bl2combo_text"];
            DO5bl2combo_text = json_read.ComboText["DO5bl2combo_text"];
            DO6bl2combo_text = json_read.ComboText["DO6bl2combo_text"];
            DO7bl2combo_text = json_read.ComboText["DO7bl2combo_text"];
            DO8bl2combo_text = json_read.ComboText["DO8bl2combo_text"];
            // DO сигналы блок расширения 3
            DO1bl3combo_text = json_read.ComboText["DO1bl3combo_text"];
            DO2bl3combo_text = json_read.ComboText["DO2bl3combo_text"];
            DO3bl3combo_text = json_read.ComboText["DO3bl3combo_text"];
            DO4bl3combo_text = json_read.ComboText["DO4bl3combo_text"];
            DO5bl3combo_text = json_read.ComboText["DO5bl3combo_text"];
            DO6bl3combo_text = json_read.ComboText["DO6bl3combo_text"];
            DO7bl3combo_text = json_read.ComboText["DO7bl3combo_text"];
            DO8bl3combo_text = json_read.ComboText["DO8bl3combo_text"];
            // UI сигналы ПЛК
            UI1combo_text = json_read.ComboText["UI1combo_text"];
            UI2combo_text = json_read.ComboText["UI2combo_text"];
            UI3combo_text = json_read.ComboText["UI3combo_text"];
            UI4combo_text = json_read.ComboText["UI4combo_text"];
            UI5combo_text = json_read.ComboText["UI5combo_text"];
            UI6combo_text = json_read.ComboText["UI6combo_text"];
            UI7combo_text = json_read.ComboText["UI7combo_text"];
            UI8combo_text = json_read.ComboText["UI8combo_text"];
            UI9combo_text = json_read.ComboText["UI9combo_text"];
            UI10combo_text = json_read.ComboText["UI10combo_text"];
            UI11combo_text = json_read.ComboText["UI11combo_text"];
            // UI сигналы блок расширения 1
            UI1bl1combo_text = json_read.ComboText["UI1bl1combo_text"];
            UI2bl1combo_text = json_read.ComboText["UI2bl1combo_text"];
            UI3bl1combo_text = json_read.ComboText["UI3bl1combo_text"];
            UI4bl1combo_text = json_read.ComboText["UI4bl1combo_text"];
            UI5bl1combo_text = json_read.ComboText["UI5bl1combo_text"];
            UI6bl1combo_text = json_read.ComboText["UI6bl1combo_text"];
            UI7bl1combo_text = json_read.ComboText["UI7bl1combo_text"];
            UI8bl1combo_text = json_read.ComboText["UI8bl1combo_text"];
            UI9bl1combo_text = json_read.ComboText["UI9bl1combo_text"];
            UI10bl1combo_text = json_read.ComboText["UI10bl1combo_text"];
            UI11bl1combo_text = json_read.ComboText["UI11bl1combo_text"];
            UI12bl1combo_text = json_read.ComboText["UI12bl1combo_text"];
            UI13bl1combo_text = json_read.ComboText["UI13bl1combo_text"];
            UI14bl1combo_text = json_read.ComboText["UI14bl1combo_text"];
            UI15bl1combo_text = json_read.ComboText["UI15bl1combo_text"];
            UI16bl1combo_text = json_read.ComboText["UI16bl1combo_text"];
            // UI сигналы блок расширения 2
            UI1bl2combo_text = json_read.ComboText["UI1bl2combo_text"];
            UI2bl2combo_text = json_read.ComboText["UI2bl2combo_text"];
            UI3bl2combo_text = json_read.ComboText["UI3bl2combo_text"];
            UI4bl2combo_text = json_read.ComboText["UI4bl2combo_text"];
            UI5bl2combo_text = json_read.ComboText["UI5bl2combo_text"];
            UI6bl2combo_text = json_read.ComboText["UI6bl2combo_text"];
            UI7bl2combo_text = json_read.ComboText["UI7bl2combo_text"];
            UI8bl2combo_text = json_read.ComboText["UI8bl2combo_text"];
            UI9bl2combo_text = json_read.ComboText["UI9bl2combo_text"];
            UI10bl2combo_text = json_read.ComboText["UI10bl2combo_text"];
            UI11bl2combo_text = json_read.ComboText["UI11bl2combo_text"];
            UI12bl2combo_text = json_read.ComboText["UI12bl2combo_text"];
            UI13bl2combo_text = json_read.ComboText["UI13bl2combo_text"];
            UI14bl2combo_text = json_read.ComboText["UI14bl2combo_text"];
            UI15bl2combo_text = json_read.ComboText["UI15bl2combo_text"];
            UI16bl2combo_text = json_read.ComboText["UI16bl2combo_text"];
            // UI сигналы блок расширения 3
            UI1bl3combo_text = json_read.ComboText["UI1bl3combo_text"];
            UI2bl3combo_text = json_read.ComboText["UI2bl3combo_text"];
            UI3bl3combo_text = json_read.ComboText["UI3bl3combo_text"];
            UI4bl3combo_text = json_read.ComboText["UI4bl3combo_text"];
            UI5bl3combo_text = json_read.ComboText["UI5bl3combo_text"];
            UI6bl3combo_text = json_read.ComboText["UI6bl3combo_text"];
            UI7bl3combo_text = json_read.ComboText["UI7bl3combo_text"];
            UI8bl3combo_text = json_read.ComboText["UI8bl3combo_text"];
            UI9bl3combo_text = json_read.ComboText["UI9bl3combo_text"];
            UI10bl3combo_text = json_read.ComboText["UI10bl3combo_text"];
            UI11bl3combo_text = json_read.ComboText["UI11bl3combo_text"];
            UI12bl3combo_text = json_read.ComboText["UI12bl3combo_text"];
            UI13bl3combo_text = json_read.ComboText["UI13bl3combo_text"];
            UI14bl3combo_text = json_read.ComboText["UI14bl3combo_text"];
            UI15bl3combo_text = json_read.ComboText["UI15bl3combo_text"];
            UI16bl3combo_text = json_read.ComboText["UI16bl3combo_text"];
        }

        ///<summary>Загрузка статуса доступности панелей блоков расширения</summary>
        private void LoadPanelsEnable()
        {
            // Панели AO
            block1_AOpanel.Enabled = json_read.PanelsEnable["block1_AOpanel"];
            block2_AOpanel.Enabled = json_read.PanelsEnable["block2_AOpanel"];
            block3_AOpanel.Enabled = json_read.PanelsEnable["block3_AOpanel"];
            // Панели DO
            block1_DOpanel.Enabled = json_read.PanelsEnable["block1_DOpanel"];
            block2_DOpanel.Enabled = json_read.PanelsEnable["block2_DOpanel"];
            block3_DOpanel.Enabled = json_read.PanelsEnable["block3_DOpanel"];
            // Панели UI
            block1_UIpanel.Enabled = json_read.PanelsEnable["block1_UIpanel"];
            block2_UIpanel.Enabled = json_read.PanelsEnable["block2_UIpanel"];
            block3_UIpanel.Enabled = json_read.PanelsEnable["block3_UIpanel"];
        }

        ///<summary>Загрузка заголовков панелей блоков расширения</summary>
        private void LoadPanelsHeaders()
        {
            // Панели AO
            AOblock1_header.Text = json_read.PanelsHeaders["AOblock1_header"];
            AOblock2_header.Text = json_read.PanelsHeaders["AOblock2_header"];
            AOblock3_header.Text = json_read.PanelsHeaders["AOblock3_header"];
            // Панели DO
            DOblock1_header.Text = json_read.PanelsHeaders["DOblock1_header"];
            DOblock2_header.Text = json_read.PanelsHeaders["DOblock2_header"];
            DOblock3_header.Text = json_read.PanelsHeaders["DOblock3_header"];
            // Панели UI
            UIblock1_header.Text = json_read.PanelsHeaders["UIblock1_header"];
            UIblock2_header.Text = json_read.PanelsHeaders["UIblock2_header"];
            UIblock3_header.Text = json_read.PanelsHeaders["UIblock3_header"];
        }

        ///<summary>Загрузка выбранного ранее типа контроллера</summary>
        private void LoadPlkType()
        {
            comboPlkType.SelectedIndex = json_read.PlkType["comboPlkType"];
        }

        ///<summary>Загрузка количества и типов блоков расширения</summary>
        private void LoadExpBlocks()
        {
            Dictionary<string, int> exp_blocks = json_read.ExpBlocks;

            // Блок расширения AO
            if (exp_blocks.ContainsKey("M72E12RB"))
            {
                if (DOblock1_header.Text.Contains("M72E12RB") && block1_DOpanel.Enabled)
                {
                    AO_block1_panelChanged_M72E12RB();                      // AO панель блок 1
                    UI_block1_panelChanged_M72E12RB();                      // UI панель блок 1
                    DO_block1_panelChanged_M72E12RB();                      // DO панель блок 1
                }
                else if (DOblock2_header.Text.Contains("M72E12RB") && block2_DOpanel.Enabled)
                {
                    AO_block2_panelChanged_M72E12RB();                      // AO панель блок 2
                    UI_block2_panelChanged_M72E12RB();                      // UI панель блок 2
                    DO_block2_panelChanged_M72E12RB();                      // DO панель блок 2
                }
                else if (DOblock3_header.Text.Contains("M72E12RB") && block3_DOpanel.Enabled)
                {
                    AO_block3_panelChanged_M72E12RB();                      // AO панель блок 3
                    UI_block3_panelChanged_M72E12RB();                      // UI панель блок 3
                    DO_block3_panelChanged_M72E12RB();                      // DO панель блок 3
                }
                
                expansion_blocks.Add(M72E12RB);

                if (exp_blocks["M72E12RB"] > 1)                             // Два блока расширения AO
                {
                    if (block1_UIpanel.Enabled || block1_DOpanel.Enabled)   // Есть активная панель 1-го блока
                    {
                        AO_block2_panelChanged_M72E12RB();                  // AO панель блок 2
                        UI_block2_panelChanged_M72E12RB();                  // UI панель блок 2
                        DO_block2_panelChanged_M72E12RB();                  // DO панель блок 2
                    }
                    else
                    {
                        AO_block3_panelChanged_M72E12RB();                  // AO панель блок 3
                        UI_block3_panelChanged_M72E12RB();                  // UI панель блок 3
                        DO_block3_panelChanged_M72E12RB();                  // DO панель блок 3
                    }
                    
                    expansion_blocks.Add(M72E12RB);

                    if (exp_blocks["M72E12RB"] > 2)                         // Три блока расширения AO
                    {
                        AO_block3_panelChanged_M72E12RB();                  // AO панель блок 3
                        UI_block3_panelChanged_M72E12RB();                  // UI панель блок 3
                        DO_block3_panelChanged_M72E12RB();                  // DO панель блок 3
                        expansion_blocks.Add(M72E12RB);
                    }
                }
            }

            // Блок расширения UI + DO
            if (exp_blocks.ContainsKey("M72E12RA"))
            {
                if (DOblock1_header.Text.Contains("M72E12RA") && block1_DOpanel.Enabled && block1_UIpanel.Enabled)
                {
                    DO_block1_panelChanged_M72E12RA();                      // DO панель блок 1
                    UI_block1_panelChanged_M72E12RA();                      // UI панель блок 1
                } 
                else if (DOblock2_header.Text.Contains("M72E12RA") && block2_DOpanel.Enabled && block2_UIpanel.Enabled)
                {
                    DO_block2_panelChanged_M72E12RA();                      // DO панель блок 2
                    UI_block2_panelChanged_M72E12RA();                      // UI панель блок 2
                }
                else if (DOblock3_header.Text.Contains("M72E12RA") && block3_DOpanel.Enabled && block3_UIpanel.Enabled)
                {
                    DO_block3_panelChanged_M72E12RA();                      // DO панель блок 3
                    UI_block3_panelChanged_M72E12RA();                      // UI панель блок 3
                }

                expansion_blocks.Add(M72E12RA);

                if (exp_blocks["M72E12RA"] > 1)                             // Два блока расширения UI + DO
                {
                    if (DOblock2_header.Text.Contains("M72E12RA") && block2_DOpanel.Enabled && block2_UIpanel.Enabled)
                    {
                        DO_block2_panelChanged_M72E12RA();                  // DO панель блок 2
                        UI_block2_panelChanged_M72E12RA();                  // UI панель блок 2
                    }
                    else if (DOblock3_header.Text.Contains("M72E12RA") && block3_DOpanel.Enabled && block3_UIpanel.Enabled)
                    {
                        DO_block3_panelChanged_M72E12RA();                  // DO панель блок 3
                        UI_block3_panelChanged_M72E12RA();                  // UI панель блок 3
                    }

                    expansion_blocks.Add(M72E12RA);

                    if (exp_blocks["M72E12RA"] > 2)
                    {
                        DO_block3_panelChanged_M72E12RA();                  // DO панель блок 3
                        UI_block3_panelChanged_M72E12RA();                  // UI панель блок 3
                        expansion_blocks.Add(M72E12RA);
                    }
                }
            }

            // Блок расширения DO
            if (exp_blocks.ContainsKey("M72E08RA"))                         // Один блок DO
            {
                bool 
                    do_block2_locked = false,                               // Признак занятой панели DO2
                    do_block3_locked = false;                               // Признак занятой панели DO3

                if (DOblock1_header.Text.Contains("M72E08RA") && block1_DOpanel.Enabled)
                {
                    DO_block1_panelChanged_M72E08RA();                      // DO панель блок 1
                }
                else if (DOblock2_header.Text.Contains("M72E08RA") && block2_DOpanel.Enabled)
                {
                    DO_block2_panelChanged_M72E08RA();                      // DO панель блок 2
                    do_block2_locked = true;                                // Установка признака занятой панели DO2
                }
                else if (DOblock3_header.Text.Contains("M72E08RA") && block3_DOpanel.Enabled)
                {
                    DO_block3_panelChanged_M72E08RA();                      // DO панель блок 3
                    do_block3_locked = true;                                // Установка признака занятой панели DO3
                }
                    
                expansion_blocks.Add(M72E08RA);

                if (exp_blocks["M72E08RA"] > 1)                             // Два блока DO
                {
                    if (DOblock2_header.Text.Contains("M72E08RA") && block2_DOpanel.Enabled && !do_block2_locked)
                        DO_block2_panelChanged_M72E08RA();                  // DO панель блок 2
                    else if (DOblock3_header.Text.Contains("M72E08RA") && block3_DOpanel.Enabled && !do_block3_locked)
                    {
                        DO_block3_panelChanged_M72E08RA();                  // DO панель блок 3
                        do_block3_locked = true;                            // Установка признака занятой панели DO3
                    }

                    expansion_blocks.Add(M72E08RA);

                    if (exp_blocks["M72E08RA"] > 2)                         // Три блока DO
                    {
                        if (DOblock3_header.Text.Contains("M72E08RA") && !do_block3_locked)
                        {
                            DO_block3_panelChanged_M72E08RA();              // DO панель блок 3
                            expansion_blocks.Add(M72E08RA);
                        }
                    }
                }
            }

            // Блок расширения UI
            if (exp_blocks.ContainsKey("M72E16NA"))
            {
                if (UIblock1_header.Text.Contains("M72E16NA") && block1_UIpanel.Enabled)
                    UI_block1_panelChanged_M72E16NA();          // UI панель блок 1
                else if (UIblock2_header.Text.Contains("M72E16NA") && block2_UIpanel.Enabled)
                    UI_block2_panelChanged_M72E16NA();          // UI панель блок 2
                else if (UIblock3_header.Text.Contains("M72E16NA") && block3_UIpanel.Enabled)
                    UI_block3_panelChanged_M72E16NA();          // UI панель блок 3

                expansion_blocks.Add(M72E16NA);

                if (exp_blocks["M72E16NA"] > 1)
                {
                    if (UIblock2_header.Text.Contains("M72E16NA") && block2_UIpanel.Enabled)
                        UI_block2_panelChanged_M72E16NA();      // UI панель блок 2
                    else if (UIblock3_header.Text.Contains("M72E16NA") && block3_UIpanel.Enabled)
                        UI_block3_panelChanged_M72E16NA();      // UI панель блок 3

                    expansion_blocks.Add(M72E16NA);

                    if (exp_blocks["M72E16NA"] > 2)
                    {
                        if (UIblock3_header.Text.Contains("M72E16NA") && block3_UIpanel.Enabled)
                            UI_block3_panelChanged_M72E16NA();  // UI панель блок 3

                        expansion_blocks.Add(M72E16NA);
                    }
                }
            }
        }

        ///<summary>Загрузка состояния режима выбора блоков расширения</summary>
        private void LoadManBlocksState()
        {
            int autoModeBlocksSelect = json_read.ManBlocksState[autoSelectBlocks_check.Name];
            autoSelectBlocks_check.Checked = autoModeBlocksSelect == 1 ? true : false;

            var check_blocks = new List<ComboBox>()
            {
                comboManBl_1, comboManBl_2, comboManBl_3
            };

            foreach (var el in check_blocks) el.SelectedIndex = json_read.ManBlocksState[el.Name];
        }

        ///<summary>Загрузка для всех checkBox</summary>
        private void LoadCheckBoxAll()
        {
            var comboBoxes = new List<CheckBox>()
            {
                // Выбор элементов (боковая панель)
                filterCheck, dampCheck, heaterCheck, addHeatCheck, coolerCheck, humidCheck, recircCheck, recupCheck,
                // Заслонки
                confPrDampCheck, heatPrDampCheck, outDampCheck, confOutDampCheck, heatOutDampCheck,
                // Нагреватель
                TF_heaterCheck, confHeatPumpCheck, pumpCurProtect, reservPumpHeater, confHeatResPumpCheck,
                pumpCurResProtect, watSensHeatCheck, overheatThermCheck, fireThermCheck,
                // Второй нагреватель
                TF_addHeaterCheck, pumpAddHeatCheck, confAddHeatPumpCheck, pumpCurAddProtect, reservPumpAddHeater,
                confAddHeatResPumpCheck, pumpCurResAddProtect, sensWatAddHeatCheck, overheatAddThermCheck, fireAddThermCheck,
                // Охладитель
                alarmFrCoolCheck, thermoCoolerCheck, analogFreonCheck, dehumModeCheck, alarmHumidCheck, analogHumCheck,
                // Увлажнитель, рециркуляция и рекуператор
                recircPrDampAOCheck, pumpGlicRecCheck, pumpGlikConfCheck, pumpGlikCurProtect, reservPumpGlik,
                confGlikResPumpCheck, pumpGlikResCurProtect, recDefTempCheck, recDefPsCheck, outSigAlarmRotRecCheck,
                startRotRecCheck,
                // Датчики и сигналы
                prChanSensCheck, roomTempSensCheck, chanHumSensCheck, roomHumSensCheck, outdoorChanSensCheck, outChanSensCheck,
                sigWorkCheck, sigAlarmCheck, sigFilAlarmCheck, fireCheck,
                // Приточный вентилятор
                prFanPSCheck, prFanThermoCheck, curDefPrFanCheck, checkResPrFan, prDampFanCheck,
                prDampConfirmFanCheck, prFanAlarmCheck, prFanStStopCheck, prFanSpeedCheck,
                // Вытяжной вентилятор
                outFanPSCheck, outFanFC_check, outFanThermoCheck, curDefOutFanCheck, checkResOutFan, outDampFanCheck,
                outDampConfirmFanCheck, outFanAlarmCheck, outFanStStopCheck, outFanSpeedCheck, outFanCheck
            };

            foreach (var el in comboBoxes) el.Checked = json_read.CheckBoxState[el.Name];

            // Разблокировка элементов при выборе, основной насос гликолевого рекуператора
            if (pumpGlicRecCheck.Checked)
            {
                pumpGlikConfCheck.Enabled = true;
                pumpGlikCurProtect.Enabled = true;
            }

            // Разблокировка элементов при выборе, резервный насос гликолевого рекуператора
            if (reservPumpGlik.Checked)
            {
                confGlikResPumpCheck.Enabled = true;
                pumpGlikResCurProtect.Enabled = true;
            }
        }

        ///<summary>Загрузка для всех comboBox элементов</summary>
        private void LoadComboBoxElemAll()
        {
            var comboBoxes = new List<ComboBox>()
            {
                // Вентиляторы
                prFanFC_ECcombo,
                // Сигналы и датчики
                stopStartCombo,
                // Выбор типа системы
                comboSysType,
                // Приточный и вытяжной вентилятор
                prFanPowCombo, prFanControlCombo, outFanPowCombo, outFanControlCombo, prFanFcTypeCombo, outFanFcTypeCombo,
                // Воздушные фильтры
                filterPrCombo, filterOutCombo,
                // Нагреватель
                heatTypeCombo, elHeatStagesCombo, firstStHeatCombo,
                // Второй нагреватель
                heatAddTypeCombo, elHeatAddStagesCombo, firstStAddHeatCombo,
                // Охладитель и увлажнитель
                coolTypeCombo, frCoolStagesCombo, humidTypeCombo,
                // Рекуператор и датчики
                recupTypeCombo, bypassPlastCombo, fireTypeCombo
            };

            foreach (var el in comboBoxes) el.SelectedIndex = json_read.ComboBoxElemState[el.Name];

            // Операции при выборе Modbus П вентилятора
            if (prFanControlCombo.SelectedIndex == 1)   // Приточный вентилятор
            {
                prFanAlarmCheck.Enabled = false;        // Сигнал аварии
                prFanSpeedCheck.Enabled = false;        // Скорость 0-10 В
                prFanFcTypeCombo.Enabled = true;        // Разблокировка выбора модели ПЧ
            }

            // Операции при выборе Modbus П вентилятора
            if (outFanControlCombo.SelectedIndex == 1)  // Вытяжной вентилятор
            {
                outFanAlarmCheck.Enabled = false;       // Сигнал аварии
                outFanSpeedCheck.Enabled = false;       // Скорость 0-10 В
                outFanFcTypeCombo.Enabled = true;       // Разблокировка выбора модели ПЧ
            }
        }

        ///<summary>Загрузка для подписей кодов таблицы сигналов</summary>
        private void LoadLabelSignalsAll()
        {
            var labels = new List<Label>()
            {
                // UI сигналы ПЛК
                UI1_lab, UI2_lab, UI3_lab, UI4_lab, UI5_lab, UI6_lab, UI7_lab, UI8_lab, UI9_lab, UI10_lab, UI11_lab,
                // UI сигналы, блок 1
                UI1bl1_lab, UI2bl1_lab, UI3bl1_lab, UI4bl1_lab, UI5bl1_lab, UI6bl1_lab, UI7bl1_lab, UI8bl1_lab,
                UI9bl1_lab, UI10bl1_lab, UI11bl1_lab, UI12bl1_lab, UI13bl1_lab, UI14bl1_lab, UI15bl1_lab, UI16bl1_lab,
                // UI сигналы, блок 2
                UI1bl2_lab, UI2bl2_lab, UI3bl2_lab, UI4bl2_lab, UI5bl2_lab, UI6bl2_lab, UI7bl2_lab, UI8bl2_lab,
                UI9bl2_lab, UI10bl2_lab, UI11bl2_lab, UI12bl2_lab, UI13bl2_lab, UI14bl2_lab, UI15bl2_lab, UI16bl2_lab,
                // UI сигналы, блок 3
                UI1bl3_lab, UI2bl3_lab, UI3bl3_lab, UI4bl3_lab, UI5bl3_lab, UI6bl3_lab, UI7bl3_lab, UI8bl3_lab,
                UI9bl3_lab, UI10bl3_lab, UI11bl3_lab, UI12bl3_lab, UI13bl3_lab, UI14bl3_lab, UI15bl3_lab, UI16bl3_lab,
                // AO сигналы, ПЛК и блоки расширения
                AO1_lab, AO2_lab, AO3_lab, AO1bl1_lab, AO2bl1_lab, AO1bl2_lab, AO2bl2_lab, AO1bl3_lab, AO2bl3_lab,
                // DO сигналы ПЛК
                DO1_lab, DO2_lab, DO3_lab, DO4_lab, DO5_lab, DO6_lab,
                // DO сигналы, блок 1
                DO1bl1_lab, DO2bl1_lab, DO3bl1_lab, DO4bl1_lab, DO5bl1_lab, DO6bl1_lab, DO7bl1_lab, DO8bl1_lab,
                // DO сигналы, блок 2
                DO1bl2_lab, DO2bl2_lab, DO3bl2_lab, DO4bl2_lab, DO5bl2_lab, DO6bl2_lab, DO7bl2_lab, DO8bl2_lab,
                // DO сигналы, блок 3
                DO1bl3_lab, DO2bl3_lab, DO3bl3_lab, DO4bl3_lab, DO5bl3_lab, DO6bl3_lab, DO7bl3_lab, DO8bl3_lab
            };

            foreach (var el in labels) el.Text = json_read.LabelSignalsState[el.Name];
        }

        ///<summary>Сброс сигналов перед загрузкой сигналов</summary>
        private void ResetSignalsBeforeLoad()
        {
            // AO сигналы, ПЛК и блоки расширения
            AO1_combo.Items.Clear(); AO2_combo.Items.Clear(); AO3_combo.Items.Clear();
            AO1bl1_combo.Items.Clear(); AO2bl1_combo.Items.Clear();
            AO1bl2_combo.Items.Clear(); AO2bl2_combo.Items.Clear();
            AO1bl3_combo.Items.Clear(); AO2bl3_combo.Items.Clear();
            // DO сигналы, ПЛК
            DO1_combo.Items.Clear(); DO2_combo.Items.Clear(); DO3_combo.Items.Clear();
            DO4_combo.Items.Clear(); DO5_combo.Items.Clear(); DO6_combo.Items.Clear();
            // DO сигналы, блок расширения 1
            DO1bl1_combo.Items.Clear(); DO2bl1_combo.Items.Clear(); DO3bl1_combo.Items.Clear(); DO4bl1_combo.Items.Clear();
            DO5bl1_combo.Items.Clear(); DO6bl1_combo.Items.Clear(); DO7bl1_combo.Items.Clear(); DO8bl1_combo.Items.Clear();
            // DO сигналы, блок расширения 2
            DO1bl2_combo.Items.Clear(); DO2bl2_combo.Items.Clear(); DO3bl2_combo.Items.Clear(); DO4bl2_combo.Items.Clear();
            DO5bl2_combo.Items.Clear(); DO6bl2_combo.Items.Clear(); DO7bl2_combo.Items.Clear(); DO8bl2_combo.Items.Clear();
            // DO сигналы, блок расширения 3
            DO1bl3_combo.Items.Clear(); DO2bl3_combo.Items.Clear(); DO3bl3_combo.Items.Clear(); DO4bl3_combo.Items.Clear();
            DO5bl3_combo.Items.Clear(); DO6bl3_combo.Items.Clear(); DO7bl3_combo.Items.Clear(); DO8bl3_combo.Items.Clear();
            // UI сигналы, ПЛК
            UI1_combo.Items.Clear(); UI2_combo.Items.Clear(); UI3_combo.Items.Clear(); UI4_combo.Items.Clear();
            UI5_combo.Items.Clear(); UI6_combo.Items.Clear(); UI7_combo.Items.Clear(); UI8_combo.Items.Clear();
            UI9_combo.Items.Clear(); UI10_combo.Items.Clear(); UI11_combo.Items.Clear();
            // UI сигналы, блок расширения 1
            UI1bl1_combo.Items.Clear(); UI2bl1_combo.Items.Clear(); UI3bl1_combo.Items.Clear(); UI4bl1_combo.Items.Clear();
            UI5bl1_combo.Items.Clear(); UI6bl1_combo.Items.Clear(); UI7bl1_combo.Items.Clear(); UI8bl1_combo.Items.Clear();
            UI9bl1_combo.Items.Clear(); UI10bl1_combo.Items.Clear(); UI11bl1_combo.Items.Clear(); UI12bl1_combo.Items.Clear();
            UI13bl1_combo.Items.Clear(); UI14bl1_combo.Items.Clear(); UI15bl1_combo.Items.Clear(); UI16bl1_combo.Items.Clear();
            // UI сигналы, блок расширения 2
            UI1bl2_combo.Items.Clear(); UI2bl2_combo.Items.Clear(); UI3bl2_combo.Items.Clear(); UI4bl2_combo.Items.Clear();
            UI5bl2_combo.Items.Clear(); UI6bl2_combo.Items.Clear(); UI7bl2_combo.Items.Clear(); UI8bl2_combo.Items.Clear();
            UI9bl2_combo.Items.Clear(); UI10bl2_combo.Items.Clear(); UI11bl2_combo.Items.Clear(); UI12bl2_combo.Items.Clear();
            UI13bl2_combo.Items.Clear(); UI14bl2_combo.Items.Clear(); UI15bl2_combo.Items.Clear(); UI16bl2_combo.Items.Clear();
            // UI сигналы, блок расширения 3
            UI1bl3_combo.Items.Clear(); UI2bl3_combo.Items.Clear(); UI3bl3_combo.Items.Clear(); UI4bl3_combo.Items.Clear();
            UI5bl3_combo.Items.Clear(); UI6bl3_combo.Items.Clear(); UI7bl3_combo.Items.Clear(); UI8bl3_combo.Items.Clear();
            UI9bl3_combo.Items.Clear(); UI10bl3_combo.Items.Clear(); UI11bl3_combo.Items.Clear(); UI12bl3_combo.Items.Clear();
            UI13bl3_combo.Items.Clear(); UI14bl3_combo.Items.Clear(); UI15bl3_combo.Items.Clear(); UI16bl3_combo.Items.Clear();
        }

        ///<summary>Загрузка списка элементов коллекции для comboBox таблицы сигналов</summary>
        private void LoadComboItemsSignals()
        {
            ResetSignalsBeforeLoad();       // Сброс изначально выбранных сигналов

            var comboBoxes = new List<ComboBox>()
            {
                // AO сигналы, ПЛК и блоки расширения
                AO1_combo, AO2_combo, AO3_combo, AO1bl1_combo, AO2bl1_combo, AO1bl2_combo, AO2bl2_combo,
                AO1bl3_combo, AO2bl3_combo,
                // DO сигналы, ПЛК и блоки расширения
                DO1_combo, DO2_combo, DO3_combo, DO4_combo, DO5_combo, DO6_combo,
                DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo, DO5bl1_combo, DO6bl1_combo, DO7bl1_combo, DO8bl1_combo,
                DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo, DO5bl2_combo, DO6bl2_combo, DO7bl2_combo, DO8bl2_combo,
                DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo, DO5bl3_combo, DO6bl3_combo, DO7bl3_combo, DO8bl3_combo,
                // UI сигналы, ПЛК
                UI1_combo, UI2_combo, UI3_combo, UI4_combo, UI5_combo, UI6_combo, UI7_combo, UI8_combo, UI9_combo, UI10_combo, UI11_combo,
                // UI сигналы, блок расширения 1
                UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo, UI7bl1_combo, UI8bl1_combo,
                UI9bl1_combo, UI10bl1_combo, UI11bl1_combo, UI12bl1_combo, UI13bl1_combo, UI14bl1_combo, UI15bl1_combo, UI16bl1_combo,
                // UI сигналы, блок расширения 2
                UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo, UI7bl2_combo, UI8bl2_combo,
                UI9bl2_combo, UI10bl2_combo, UI11bl2_combo, UI12bl2_combo, UI13bl2_combo, UI14bl2_combo, UI15bl2_combo, UI16bl2_combo,
                // UI сигналы, блок расширения 3
                UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo, UI7bl3_combo, UI8bl3_combo,
                UI9bl3_combo, UI10bl3_combo, UI11bl3_combo, UI12bl3_combo, UI13bl3_combo, UI14bl3_combo, UI15bl3_combo, UI16bl3_combo
            };

            foreach (var el in comboBoxes)
                for (int i = 0; i < json_read.ComboSignalsItems[el.Name].Length; i++)
                    el.Items.Add(json_read.ComboSignalsItems[el.Name][i]);
        }

        ///<summary>Загрузка состояний для comboBox таблицы сигналов</summary>
        private void LoadComboSignalsAll()
        {
            var comboBoxes = new List<ComboBox>()           // comboBox для выбранных сигналов
            {
                // AO сигналы, ПЛК и блоки расширения
                AO1_combo, AO2_combo, AO3_combo, AO1bl1_combo, AO2bl1_combo, AO1bl2_combo, AO2bl2_combo,
                AO1bl3_combo, AO2bl3_combo,
                // DO сигналы, ПЛК и блоки расширения
                DO1_combo, DO2_combo, DO3_combo, DO4_combo, DO5_combo, DO6_combo,
                DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo, DO5bl1_combo, DO6bl1_combo, DO7bl1_combo, DO8bl1_combo,
                DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo, DO5bl2_combo, DO6bl2_combo, DO7bl2_combo, DO8bl2_combo,
                DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo, DO5bl3_combo, DO6bl3_combo, DO7bl3_combo, DO8bl3_combo,
                // UI сигналы, ПЛК
                UI1_combo, UI2_combo, UI3_combo, UI4_combo, UI5_combo, UI6_combo, UI7_combo, UI8_combo, UI9_combo, UI10_combo, UI11_combo,
                // UI сигналы, блок расширения 1
                UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo, UI7bl1_combo, UI8bl1_combo,
                UI9bl1_combo, UI10bl1_combo, UI11bl1_combo, UI12bl1_combo, UI13bl1_combo, UI14bl1_combo, UI15bl1_combo, UI16bl1_combo,
                // UI сигналы, блок расширения 2
                UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo, UI7bl2_combo, UI8bl2_combo,
                UI9bl2_combo, UI10bl2_combo, UI11bl2_combo, UI12bl2_combo, UI13bl2_combo, UI14bl2_combo, UI15bl2_combo, UI16bl2_combo,
                // UI сигналы, блок расширения 3
                UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo, UI7bl3_combo, UI8bl3_combo,
                UI9bl3_combo, UI10bl3_combo, UI11bl3_combo, UI12bl3_combo, UI13bl3_combo, UI14bl3_combo, UI15bl3_combo, UI16bl3_combo
            };

            var comboBoxesType = new List<ComboBox>()       
            {
                // UI сигналы, тип сигнала, ПЛК
                UI1_typeCombo, UI2_typeCombo, UI3_typeCombo, UI4_typeCombo, UI5_typeCombo, UI6_typeCombo, UI7_typeCombo, UI8_typeCombo,
                UI9_typeCombo, UI10_typeCombo, UI11_typeCombo,
                // UI сигналы, тип сигнала, блок расширения 1
                UI1bl1_typeCombo, UI2bl1_typeCombo, UI3bl1_typeCombo, UI4bl1_typeCombo, UI5bl1_typeCombo, UI6bl1_typeCombo, UI7bl1_typeCombo,
                UI8bl1_typeCombo, UI9bl1_typeCombo, UI10bl1_typeCombo, UI11bl1_typeCombo, UI12bl1_typeCombo, UI13bl1_typeCombo, UI14bl1_typeCombo,
                UI15bl1_typeCombo, UI16bl1_typeCombo,
                // UI сигналы, тип сигнала, блок расширения 2
                UI1bl2_typeCombo, UI2bl2_typeCombo, UI3bl2_typeCombo, UI4bl2_typeCombo, UI5bl2_typeCombo, UI6bl2_typeCombo, UI7bl2_typeCombo,
                UI8bl2_typeCombo, UI9bl2_typeCombo, UI10bl2_typeCombo, UI11bl2_typeCombo, UI12bl2_typeCombo, UI13bl2_typeCombo, UI14bl2_typeCombo,
                UI15bl2_typeCombo, UI16bl2_typeCombo,
                // UI сигналы, тип сигнала, блок расширения 3
                UI1bl3_typeCombo, UI2bl3_typeCombo, UI3bl3_typeCombo, UI4bl3_typeCombo, UI5bl3_typeCombo, UI6bl3_typeCombo, UI7bl3_typeCombo,
                UI8bl3_typeCombo, UI9bl3_typeCombo, UI10bl3_typeCombo, UI11bl3_typeCombo, UI12bl3_typeCombo, UI13bl3_typeCombo, UI14bl3_typeCombo,
                UI15bl3_typeCombo, UI16bl3_typeCombo
            };

            // Расстановка элементов сигналов
            for (var i = 0; i < comboBoxes.Count; i++) 
                comboBoxes[i].SelectedItem = json_read.ComboSignalsState[comboBoxes[i].Name];

            // Расстановка типов сигналов
            for (var i = 0; i < comboBoxesType.Count; i++)
            {
                string selectedItem = json_read.ComboSignalsState[comboBoxesType[i].Name];
                comboBoxesType[i].SelectedItem = selectedItem;

                // Удаление опции DI из typeComboBox
                if (selectedItem != DI)
                    comboBoxesType[i].Items.Remove(DI);
            }
        }

        ///<summary>Загрузка для массивов сигналов</summary>
        private void LoadSignalArrays()
        {
            string name;
            ushort code;
            string type;
            bool active;

            list_ui.Clear();                                                    // Очистка прежнего списка сигналов UI
            list_ao.Clear();                                                    // Очистка прежнего списка сигналов AO
            list_do.Clear();                                                    // Очистка прежнего списка сигналов DO 

            foreach (var elem in json_read.UiCode)                              // Загрузка по сигналам UI
            {
                name = elem.Key;
                code = json_read.UiCode[name];
                type = json_read.UiType[name];
                active = json_read.UiActive[name];
                list_ui.Add(new Ui(name, code, type, active));
            }
            foreach (var elem in json_read.AoCode)                              // Загрузка по сигналам AO
            {
                name = elem.Key;
                code = json_read.AoCode[name];
                active = json_read.AoActive[name];
                list_ao.Add(new Ao(name, code, active));
            }
            foreach (var elem in json_read.DoCode)                              // Загрузка по сигналам DO
            {
                name = elem.Key;
                code = json_read.DoCode[name];
                active = json_read.DoActive[name];
                list_do.Add(new Do(name, code, active));
            }

            CheckSignalsReady(); // Проверка распределения сигналов после загрузки
        }
    }
}
