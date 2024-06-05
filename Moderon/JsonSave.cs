using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using GemBox.Spreadsheet;
using System.Linq;
using System.Xml.Linq;

// Файл для сохранения/загрузки параметров программы в формате JSON

namespace Moderon
{
    class JsonObject
    {
        [JsonProperty("CheckBoxState")]
        public Dictionary<string, bool> CheckBoxState { get; set; }                 // Состояние всех checkBox

        [JsonProperty("ComboBoxElemState")]
        public Dictionary<string, int> ComboBoxElemState { get; set; }              // Состояние всех comboBox элементов

        [JsonProperty("TextBoxElemState")]
        public Dictionary<string, string> TextBoxElemState { get; set; }            // Значение для textBox элементов

        [JsonProperty("LabelSignalsState")]
        public Dictionary<string, string> LabelSignalsState { get; set; }           // Значение для label таблицы сигналов

        [JsonProperty("ComboSignalsItems")]
        public Dictionary<string, string[]> ComboSignalsItems { get; set; }         // Элементы для comboBox таблицы сигналов

        [JsonProperty("ComboSignalsState")]
        public Dictionary<string, string> ComboSignalsState { get; set; }           // Значение для comboBox таблицы сигналов

        [JsonProperty("UiCode")]
        public Dictionary<string, ushort> UiCode { get; set; }                      // Словарь для кодов сигналов UI

        [JsonProperty("UiType")]
        public Dictionary<string, string> UiType { get; set; }                      // Словарь для типов сигналов UI

        [JsonProperty("UiActive")]
        public Dictionary<string, bool> UiActive { get; set; }                      // Словарь для активности сигналов UI

        [JsonProperty("AoCode")]
        public Dictionary<string, ushort> AoCode { get; set; }                      // Словарь для кодов сигналов AO

        [JsonProperty("AoActive")]
        public Dictionary<string, bool> AoActive { get; set; }                      // Словарь для активности сигналов AO

        [JsonProperty("DoCode")]
        public Dictionary<string, ushort> DoCode { get; set; }                      // Словарь для кодов сигналов DO

        [JsonProperty("DoActive")]
        public Dictionary<string, bool> DoActive { get; set; }                      // Словарь для активности сигналов DO

        [JsonProperty("UiTypeEnable")]
        public Dictionary<string, bool> UiTypeEnable { get; set; }                  // Словарь для доступности типа UI сигналов

        [JsonProperty("CommandWords")]
        public Dictionary<string, ushort> CommandWords { get; set; }                // Словарь для командных слов программы

        [JsonProperty("ComboIndex")]
        public Dictionary<string, int> ComboIndex { get; set; }                     // Словарь для сохранения выбранного ранее индекса comboBox

        [JsonProperty("ComboText")]
        public Dictionary<string, string> ComboText { get; set; }                   // Словарь для сохранения текста выбранного сигнала comboBox

        [JsonProperty("PanelsEnable")]                                          
        public Dictionary<string, bool> PanelsEnable { get; set; }                  // Словарь для сохранения доступности панелей блоков расширения

        [JsonProperty("PanelsHeaders")]
        public Dictionary<string, string> PanelsHeaders { get; set; }               // Словарь для сохранения названия заголовков панелей

        [JsonProperty("ExpBlocks")]                                                 
        public Dictionary<string, int> ExpBlocks { get; set; }                      // Словарь для сохранения количества блоков расширения

        [JsonProperty("PlkType")]                                                   
        public Dictionary<string, int> PlkType { get; set; }                        // Опция для сохранения типа выбранного ПЛК

        [JsonConstructor]
        public JsonObject(){
            CheckBoxState = []; ComboBoxElemState = []; TextBoxElemState = []; LabelSignalsState = [];
            ComboSignalsItems = []; ComboSignalsState = []; UiCode = []; UiType = []; UiActive = [];
            AoCode = []; AoActive = []; UiTypeEnable = []; CommandWords = []; DoCode = []; DoActive = [];
            ComboIndex = []; ComboText = []; PanelsEnable = []; PanelsHeaders = []; ExpBlocks = []; PlkType = [];
        }
    }

    public partial class Form1 : Form
    {
        JsonObject json;                                // Объект для сохранения файла
        bool correctFile = false;                       // Выбран корректный файл для загрузки конфигурации

        ///<summary>Нажали "Сохранить" в главном меню</summary> 
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            json = new JsonObject();                    // Создание файла для сохранения
            BuildCheckBoxAll();                         // Сохранение для всех элементов checkBox
            BuildComboBoxElemAll();                     // Сохранение для всех элементов comboBox
            BuildTextBoxAll();                          // Сохранение для всех элементов textBox
            BuildLabelSignalsAll();                     // Сохранение для подписей кодов таблицы сигналов
            BuildComboItemsSignals();                   // Сохранение элементов comboBox таблицы сигналов
            BuildComboSignalsAll();                     // Сохранение выбранного элемента для comboBox таблицы сигналов
            BuildSignalArrays();                        // Сохранение для перечня сигналов, массивы
            BuildComboIndex();                          // Сохранение ранее выбранного индекса для comboBox таблицы сигналов
            BuildComboText();                           // Сохранение ранее выбранного текста comboBox таблицы сигналов
            BuildPanelsEnable();                        // Сохранение статуса доступности панелей блоков расширения
            BuildPanelsHeaders();                       // Сохранение заголовков подписей панелей блоков расширения
            BuildExpBlocks();                           // Сохранение количества и типов блоков расширения
            Build_UITypeEnable();                       // Сохранение доступности типов UI сигналов
            Build_CommandWords();                       // Сохранение командных слов программы
            BuildPlkType();                             // Сохранение типа выбранного контроллера
            SaveJsonFile();                             // Сохранение файла JSON  
        }

        ///<summary>Сохранение кодов командных слов программы</summary>
        private void Build_CommandWords()
        {
            BuildCmdWords();        // Сборка командных слов перед сохранением

            for (ushort i = 0; i < cmdWords.Length; i++)
                json.CommandWords.Add($"Cmd_{i + 1}", cmdWords[i]);
           
            json.CommandWords.Add("Cmd_31", cmdW_fire);
        }

        ///<summary>Сохранение состояния доступности для UI типов сигналов</summary>
        private void Build_UITypeEnable()
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

            foreach (var el in ui_combos_type)
                json.UiTypeEnable.Add(el.Name, el.Enabled);
        }

        ///<summary>Сохранение ранее выбранного текста сигнала comboBox таблицы сигналов</summary>
        private void BuildComboText()
        {
            var texts = new Dictionary<string, string>()
            {
                // AO сигналы ПЛК
                {"AO1combo_text", AO1combo_text }, 
                {"AO2combo_text", AO2combo_text },
                {"AO3combo_text", AO3combo_text },
                // AO сигналы блок расширения 1
                { "AO1bl1combo_text", AO1bl1combo_text },
                { "AO2bl1combo_text", AO2bl1combo_text },
                // AO сигналы блок расширения 2
                { "AO1bl2combo_text", AO1bl2combo_text },
                { "AO2bl2combo_text", AO2bl2combo_text },
                // AO сигналы блок расширения 3
                { "AO1bl3combo_text", AO1bl3combo_text },
                { "AO2bl3combo_text", AO2bl3combo_text },
                // DO сигналы ПЛК
                { "DO1combo_text", DO1combo_text },
                { "DO2combo_text", DO2combo_text },
                { "DO3combo_text", DO3combo_text },
                { "DO4combo_text", DO4combo_text },
                { "DO5combo_text", DO5combo_text },
                { "DO6combo_text", DO6combo_text },
                // DO сигналы блок расширения 1
                { "DO1bl1combo_text", DO1bl1combo_text },
                { "DO2bl1combo_text", DO2bl1combo_text },
                { "DO3bl1combo_text", DO3bl1combo_text },
                { "DO4bl1combo_text", DO4bl1combo_text },
                { "DO5bl1combo_text", DO5bl1combo_text },
                { "DO6bl1combo_text", DO6bl1combo_text },
                { "DO7bl1combo_text", DO7bl1combo_text },
                { "DO8bl1combo_text", DO8bl1combo_text },
                // DO сигналы блок расширения 2
                { "DO1bl2combo_text", DO1bl2combo_text },
                { "DO2bl2combo_text", DO2bl2combo_text },
                { "DO3bl2combo_text", DO3bl2combo_text },
                { "DO4bl2combo_text", DO4bl2combo_text },
                { "DO5bl2combo_text", DO5bl2combo_text },
                { "DO6bl2combo_text", DO6bl2combo_text },
                { "DO7bl2combo_text", DO7bl2combo_text },
                { "DO8bl2combo_text", DO8bl2combo_text },
                // DO сигналы блок расширения 3
                { "DO1bl3combo_text", DO1bl3combo_text },
                { "DO2bl3combo_text", DO2bl3combo_text },
                { "DO3bl3combo_text", DO3bl3combo_text },
                { "DO4bl3combo_text", DO4bl3combo_text },
                { "DO5bl3combo_text", DO5bl3combo_text },
                { "DO6bl3combo_text", DO6bl3combo_text },
                { "DO7bl3combo_text", DO7bl3combo_text },
                { "DO8bl3combo_text", DO8bl3combo_text },
                // UI сигналы ПЛК
                { "UI1combo_text", UI1combo_text },
                { "UI2combo_text", UI2combo_text },
                { "UI3combo_text", UI3combo_text },
                { "UI4combo_text", UI4combo_text },
                { "UI5combo_text", UI5combo_text },
                { "UI6combo_text", UI6combo_text },
                { "UI7combo_text", UI7combo_text },
                { "UI8combo_text", UI8combo_text },
                { "UI9combo_text", UI9combo_text },
                { "UI10combo_text", UI10combo_text },
                { "UI11combo_text", UI11combo_text },
                // UI сигналы блок расширения 1
                { "UI1bl1combo_text", UI1bl1combo_text },
                { "UI2bl1combo_text", UI2bl1combo_text },
                { "UI3bl1combo_text", UI3bl1combo_text },
                { "UI4bl1combo_text", UI4bl1combo_text },
                { "UI5bl1combo_text", UI5bl1combo_text },
                { "UI6bl1combo_text", UI6bl1combo_text },
                { "UI7bl1combo_text", UI7bl1combo_text },
                { "UI8bl1combo_text", UI8bl1combo_text },
                { "UI9bl1combo_text", UI9bl1combo_text },
                { "UI10bl1combo_text", UI10bl1combo_text },
                { "UI11bl1combo_text", UI11bl1combo_text },
                { "UI12bl1combo_text", UI12bl1combo_text },
                { "UI13bl1combo_text", UI13bl1combo_text },
                { "UI14bl1combo_text", UI14bl1combo_text },
                { "UI15bl1combo_text", UI15bl1combo_text },
                { "UI16bl1combo_text", UI16bl1combo_text },
                // UI сигналы блок расширения 2
                { "UI1bl2combo_text", UI1bl2combo_text },
                { "UI2bl2combo_text", UI2bl2combo_text },
                { "UI3bl2combo_text", UI3bl2combo_text },
                { "UI4bl2combo_text", UI4bl2combo_text },
                { "UI5bl2combo_text", UI5bl2combo_text },
                { "UI6bl2combo_text", UI6bl2combo_text },
                { "UI7bl2combo_text", UI7bl2combo_text },
                { "UI8bl2combo_text", UI8bl2combo_text },
                { "UI9bl2combo_text", UI9bl2combo_text },
                { "UI10bl2combo_text", UI10bl2combo_text },
                { "UI11bl2combo_text", UI11bl2combo_text },
                { "UI12bl2combo_text", UI12bl2combo_text },
                { "UI13bl2combo_text", UI13bl2combo_text },
                { "UI14bl2combo_text", UI14bl2combo_text },
                { "UI15bl2combo_text", UI15bl2combo_text },
                { "UI16bl2combo_text", UI16bl2combo_text },
                // UI сигналы блок расширения 3
                { "UI1bl3combo_text", UI1bl3combo_text },
                { "UI2bl3combo_text", UI2bl3combo_text },
                { "UI3bl3combo_text", UI3bl3combo_text },
                { "UI4bl3combo_text", UI4bl3combo_text },
                { "UI5bl3combo_text", UI5bl3combo_text },
                { "UI6bl3combo_text", UI6bl3combo_text },
                { "UI7bl3combo_text", UI7bl3combo_text },
                { "UI8bl3combo_text", UI8bl3combo_text },
                { "UI9bl3combo_text", UI9bl3combo_text },
                { "UI10bl3combo_text", UI10bl3combo_text },
                { "UI11bl3combo_text", UI11bl3combo_text },
                { "UI12bl3combo_text", UI12bl3combo_text },
                { "UI13bl3combo_text", UI13bl3combo_text },
                { "UI14bl3combo_text", UI14bl3combo_text },
                { "UI15bl3combo_text", UI15bl3combo_text },
                { "UI16bl3combo_text", UI16bl3combo_text }
            };

            foreach (var el in texts) json.ComboText.Add(el.Key, el.Value);
        }

        ///<summary>Сохранение признака доступности панелей блоков расширения</summary>
        private void BuildPanelsEnable()
        {
            var panels = new Dictionary<string, bool>()
            {
                // AO панели
                { "block1_AOpanel", block1_AOpanel.Enabled },
                { "block2_AOpanel", block2_AOpanel.Enabled },
                { "block3_AOpanel", block3_AOpanel.Enabled },
                // DO панели
                { "block1_DOpanel", block1_DOpanel.Enabled },
                { "block2_DOpanel", block2_DOpanel.Enabled },
                { "block3_DOpanel", block3_DOpanel.Enabled },
                // UI панели
                { "block1_UIpanel", block1_UIpanel.Enabled },
                { "block2_UIpanel", block2_UIpanel.Enabled },
                { "block3_UIpanel", block3_UIpanel.Enabled }
            };

            foreach (var el in panels) json.PanelsEnable.Add(el.Key, el.Value);
        }

        ///<summary>Сохранение заголовков панелей блоков расширения</summary>
        private void BuildPanelsHeaders()
        {
            var headers = new Dictionary<string, string>()
            {
                // AO панели
                { "AOblock1_header", AOblock1_header.Text },
                { "AOblock2_header", AOblock2_header.Text },
                { "AOblock3_header", AOblock3_header.Text },
                // DO панели
                { "DOblock1_header", DOblock1_header.Text },
                { "DOblock2_header", DOblock2_header.Text },
                { "DOblock3_header", DOblock3_header.Text },
                // UI панели
                { "UIblock1_header", UIblock1_header.Text },
                { "UIblock2_header", UIblock2_header.Text },
                { "UIblock3_header", UIblock3_header.Text }
            };

            foreach (var el in headers) json.PanelsHeaders.Add(el.Key, el.Value);
        }

        ///<summary>Сохранение ранее выбранного индекса comboBox таблицы сигналов</summary>
        private void BuildComboIndex()
        {
            var indexes = new Dictionary<string, int>()
            {
                // AO сигналы ПЛК
                { "AO1combo_index", AO1combo_index }, 
                { "AO2combo_index", AO2combo_index },
                { "AO3combo_index", AO3combo_index },
                // AO сигналы блок расширения 1
                { "AO1bl1combo_index",  AO1bl1combo_index },
                { "AO2bl1combo_index",  AO2bl1combo_index },
                // AO сигналы блок расширения 2
                { "AO1bl2combo_index",  AO1bl2combo_index },
                { "AO2bl2combo_index",  AO2bl2combo_index },
                // AO сигналы блок расширения 3
                { "AO1bl3combo_index",  AO1bl3combo_index },
                { "AO2bl3combo_index",  AO2bl3combo_index },
                // DO сигналы ПЛК
                { "DO1combo_index", DO1combo_index },
                { "DO2combo_index", DO2combo_index },
                { "DO3combo_index", DO3combo_index },
                { "DO4combo_index", DO4combo_index },
                { "DO5combo_index", DO5combo_index },
                { "DO6combo_index", DO6combo_index },
                // DO сигналы блок расширения 1
                { "DO1bl1combo_index", DO1bl1combo_index },
                { "DO2bl1combo_index", DO2bl1combo_index },
                { "DO3bl1combo_index", DO3bl1combo_index },
                { "DO4bl1combo_index", DO4bl1combo_index },
                { "DO5bl1combo_index", DO5bl1combo_index },
                { "DO6bl1combo_index", DO6bl1combo_index },
                { "DO7bl1combo_index", DO7bl1combo_index },
                { "DO8bl1combo_index", DO8bl1combo_index },
                // DO сигналы блок расширения 2
                { "DO1bl2combo_index", DO1bl2combo_index },
                { "DO2bl2combo_index", DO2bl2combo_index },
                { "DO3bl2combo_index", DO3bl2combo_index },
                { "DO4bl2combo_index", DO4bl2combo_index },
                { "DO5bl2combo_index", DO5bl2combo_index },
                { "DO6bl2combo_index", DO6bl2combo_index },
                { "DO7bl2combo_index", DO7bl2combo_index },
                { "DO8bl2combo_index", DO8bl2combo_index },
                // DO сигналы блок расширения 3
                { "DO1bl3combo_index", DO1bl3combo_index },
                { "DO2bl3combo_index", DO2bl3combo_index },
                { "DO3bl3combo_index", DO3bl3combo_index },
                { "DO4bl3combo_index", DO4bl3combo_index },
                { "DO5bl3combo_index", DO5bl3combo_index },
                { "DO6bl3combo_index", DO6bl3combo_index },
                { "DO7bl3combo_index", DO7bl3combo_index },
                { "DO8bl3combo_index", DO8bl3combo_index },
                // UI сигналы ПЛК
                { "UI1combo_index", UI1combo_index },
                { "UI2combo_index", UI2combo_index },
                { "UI3combo_index", UI3combo_index },
                { "UI4combo_index", UI4combo_index },
                { "UI5combo_index", UI5combo_index },
                { "UI6combo_index", UI6combo_index },
                { "UI7combo_index", UI7combo_index },
                { "UI8combo_index", UI8combo_index },
                { "UI9combo_index", UI9combo_index },
                { "UI10combo_index", UI10combo_index },
                { "UI11combo_index", UI11combo_index },
                // UI сигналы блок расширения 1
                { "UI1bl1combo_index", UI1bl1combo_index },
                { "UI2bl1combo_index", UI2bl1combo_index },
                { "UI3bl1combo_index", UI3bl1combo_index },
                { "UI4bl1combo_index", UI4bl1combo_index },
                { "UI5bl1combo_index", UI5bl1combo_index },
                { "UI6bl1combo_index", UI6bl1combo_index },
                { "UI7bl1combo_index", UI7bl1combo_index },
                { "UI8bl1combo_index", UI8bl1combo_index },
                { "UI9bl1combo_index", UI9bl1combo_index },
                { "UI10bl1combo_index", UI10bl1combo_index },
                { "UI11bl1combo_index", UI11bl1combo_index },
                { "UI12bl1combo_index", UI12bl1combo_index },
                { "UI13bl1combo_index", UI13bl1combo_index },
                { "UI14bl1combo_index", UI14bl1combo_index },
                { "UI15bl1combo_index", UI15bl1combo_index },
                { "UI16bl1combo_index", UI16bl1combo_index },
                // UI сигналы блок расширения 2
                { "UI1bl2combo_index", UI1bl2combo_index },
                { "UI2bl2combo_index", UI2bl2combo_index },
                { "UI3bl2combo_index", UI3bl2combo_index },
                { "UI4bl2combo_index", UI4bl2combo_index },
                { "UI5bl2combo_index", UI5bl2combo_index },
                { "UI6bl2combo_index", UI6bl2combo_index },
                { "UI7bl2combo_index", UI7bl2combo_index },
                { "UI8bl2combo_index", UI8bl2combo_index },
                { "UI9bl2combo_index", UI9bl2combo_index },
                { "UI10bl2combo_index", UI10bl2combo_index },
                { "UI11bl2combo_index", UI11bl2combo_index },
                { "UI12bl2combo_index", UI12bl2combo_index },
                { "UI13bl2combo_index", UI13bl2combo_index },
                { "UI14bl2combo_index", UI14bl2combo_index },
                { "UI15bl2combo_index", UI15bl2combo_index },
                { "UI16bl2combo_index", UI16bl2combo_index },
                // UI сигналы блок расширения 3
                { "UI1bl3combo_index", UI1bl3combo_index },
                { "UI2bl3combo_index", UI2bl3combo_index },
                { "UI3bl3combo_index", UI3bl3combo_index },
                { "UI4bl3combo_index", UI4bl3combo_index },
                { "UI5bl3combo_index", UI5bl3combo_index },
                { "UI6bl3combo_index", UI6bl3combo_index },
                { "UI7bl3combo_index", UI7bl3combo_index },
                { "UI8bl3combo_index", UI8bl3combo_index },
                { "UI9bl3combo_index", UI9bl3combo_index },
                { "UI10bl3combo_index", UI10bl3combo_index },
                { "UI11bl3combo_index", UI11bl3combo_index },
                { "UI12bl3combo_index", UI12bl3combo_index },
                { "UI13bl3combo_index", UI13bl3combo_index },
                { "UI14bl3combo_index", UI14bl3combo_index },
                { "UI15bl3combo_index", UI15bl3combo_index },
                { "UI16bl3combo_index", UI16bl3combo_index }
            };

            foreach (var el in indexes) json.ComboIndex.Add(el.Key, el.Value);
        }

        ///<summary>Сохранение выбранного типа ПЛК</summary>
        private void BuildPlkType()
        {
            json.PlkType.Add("comboPlkType", comboPlkType.SelectedIndex);
        }

        ///<summary>Сохранение количеcтва и типов блоков расширения</summary>
        private void BuildExpBlocks()
        {
            Dictionary<ExpBlock, int> currentBlocks = CalcExpBlocks_typeNums();

            var exp_blocks = new List<ExpBlock>()
            {
                M72E12RB, M72E12RA, M72E08RA, M72E16NA
            };

            foreach (var el in exp_blocks)
                if (currentBlocks.ContainsKey(el))
                    json.ExpBlocks.Add(el.Name, currentBlocks[el]);
        }

        ///<summary>Перенос перечня сигналов, массивы UI, AO, DO</summary>
        private void BuildSignalArrays()
        {
            // Значения полей для сигналов UI
            for (int i = 0; i < list_ui.Count; i++)
            {
                json.UiCode.Add(list_ui[i].Name, list_ui[i].Code);
                json.UiType.Add(list_ui[i].Name, list_ui[i].Type);
                json.UiActive.Add(list_ui[i].Name, list_ui[i].Active);
            }

            // Значения полей для сигналов AO
            for (int i = 0; i < list_ao.Count; i++)
            {
                json.AoCode.Add(list_ao[i].Name, list_ao[i].Code);
                json.AoActive.Add(list_ao[i].Name, list_ao[i].Active);
            }

            // Значения полей для сигналов DO
            for (int i = 0; i < list_do.Count; i++)
            {
                json.DoCode.Add(list_do[i].Name, list_do[i].Code);
                json.DoActive.Add(list_do[i].Name, list_do[i].Active);
            }
        }

        ///<summary>Создание и сохранение файла для сохранения пользователем</summary>
        private void SaveJsonFile()
        {
            var tempPath = Path.GetTempPath() + "/save.json";
            using (StreamWriter file = System.IO.File.CreateText(tempPath))
            {
                JsonSerializer serializer = new JsonSerializer();  
                serializer.Serialize(file, json);
                file.Close();
            }
            SaveFileDialog dlg = new SaveFileDialog(); // Окно для сохранения файла
            dlg.FileName = "save";
            dlg.DefaultExt = ".json";
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dlg.Filter = "Json file (.json)|*.json";
            if (dlg.ShowDialog() == DialogResult.OK)
                try
                {
                    System.IO.File.WriteAllBytes(dlg.FileName, System.IO.File.ReadAllBytes(tempPath));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        ///<summary>Загрузка Json файла в программу</summary>
        private void LoadJsonFile()
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Json file (.json)|*.json";
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)             // Выбрали нужный файл и подтвердили выбор
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                string message, caption;
                var filePath = openFileDialog.FileName;
                using Stream fileStream = openFileDialog.OpenFile();
                try
                {
                    json_read = JsonConvert.DeserializeObject<JsonObject>(File.ReadAllText(filePath));
                    message = "Файл успешно загружен!";
                    caption = "Загрузка файла";
                    result = MessageBox.Show(message, caption, buttons);
                    correctFile = true;                                     // Признак корректного файла для загрузки
                    if (result == DialogResult.OK) return;
                }
                catch
                {
                    message = "Файл поврежден!";
                    caption = "Ошибка загрузки файла";
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == DialogResult.OK) return;
                }
                fileStream.Close(); fileStream.Dispose();
            }

        }

        

        ///<summary>Сохранение для всех checkBox программы</summary>
        private void BuildCheckBoxAll()
        {
            var check_boxes = new List<CheckBox>()
            {
                // Выбор элементов (боковая панель)
                filterCheck, dampCheck, heaterCheck, addHeatCheck, coolerCheck, humidCheck, recircCheck, recupCheck,
                // Заслонки
                confPrDampCheck, heatPrDampCheck, outDampCheck, confOutDampCheck, heatOutDampCheck,
                // Основной нагреватель
                TF_heaterCheck, confHeatPumpCheck, pumpCurProtect, reservPumpHeater, confHeatResPumpCheck, pumpCurResProtect, watSensHeatCheck,
                // Второй нагреватель
                TF_addHeaterCheck, pumpAddHeatCheck, confAddHeatPumpCheck, pumpCurAddProtect, reservPumpAddHeater, 
                confAddHeatResPumpCheck, pumpCurResAddProtect, sensWatAddHeatCheck,
                // Охладитель и увлажнитель
                alarmFrCoolCheck, thermoCoolerCheck, analogFreonCheck, dehumModeCheck, alarmHumidCheck,
                // Рециркуляция и рекуператор
                recircPrDampAOCheck, pumpGlicRecCheck, pumpGlikConfCheck, pumpGlikCurProtect, reservPumpGlik,
                confGlikResPumpCheck, pumpGlikResCurProtect, recDefTempCheck, recDefPsCheck,
                // Датчики и сигналы
                prChanSensCheck, roomTempSensCheck, chanHumSensCheck, roomHumSensCheck, outdoorChanSensCheck, 
                outChanSensCheck, sigWorkCheck, sigAlarmCheck, sigFilAlarmCheck, stopStartCheck, fireCheck,
                // Приточный вентилятор
                prFanPSCheck, prFanFC_check, prFanThermoCheck, curDefPrFanCheck, checkResPrFan, prFanAlarmCheck, prFanStStopCheck,
                prFanSpeedCheck, prDampFanCheck, prDampConfirmFanCheck,
                // Вытяжной вентилятор
                outFanPSCheck, outFanFC_check, outFanThermoCheck, curDefOutFanCheck, checkResOutFan, outFanAlarmCheck, outFanStStopCheck,
                outFanSpeedCheck, outDampFanCheck, outDampConfirmFanCheck
            };

            foreach (var el in check_boxes) json.CheckBoxState.Add(el.Name, el.Checked);
        }

        ///<summary>Сохранение для всех comboBox элементов программы</summary>
        private void BuildComboBoxElemAll()
        {
            var combo_boxes = new List<ComboBox>()
            {
                // Тип системы, приточный и вытяжной вентиляторы
                comboSysType, prFanPowCombo, prFanControlCombo, outFanPowCombo, outFanControlCombo, prFanFcTypeCombo,
                outFanFcTypeCombo,
                // Воздушные фильтры
                filterPrCombo, filterOutCombo,
                // Нагреватель
                heatTypeCombo, elHeatStagesCombo, firstStHeatCombo, thermSwitchCombo,
                // Второй нагреватель
                heatAddTypeCombo, elHeatAddStagesCombo, firstStAddHeatCombo, thermAddSwitchCombo,
                // Охладитель и увлажнитель
                coolTypeCombo, frCoolStagesCombo, humidTypeCombo,
                // Рекуператор и датчики
                recupTypeCombo, rotorPowCombo, bypassPlastCombo, fireTypeCombo
            };

            foreach (var el in combo_boxes) json.ComboBoxElemState.Add(el.Name, el.SelectedIndex);
        }

        ///<summary>Сохранение для всех textBox элементов программы</summary>
        private void BuildTextBoxAll()
        {
            var text_boxes = new List<TextBox>()
            {
                // Нагреватель, второй нагреватель и рекуператор
                elHeatPowBox, elAddHeatPowBox, powRotRecBox
            };

            foreach (var el in text_boxes) json.TextBoxElemState.Add(el.Name, el.Text);
        }

        ///<summary>Добавление подписи кода таблицы сигналов</summary>
        private void AddLabelSignalsState(Label el) =>
            json.LabelSignalsState.Add(el.Name, el.Text);       

        ///<summary>Сохранение для подписей кодов таблицы сигналов</summary>
        private void BuildLabelSignalsAll()
        {
            // UI labels
            var ui_labels = new List<Label>()
            {
                // ПЛК
                UI1_lab, UI2_lab, UI3_lab, UI4_lab, UI5_lab, UI6_lab, UI7_lab, UI8_lab, UI9_lab, UI10_lab, UI11_lab,
                // Блок расширения 1
                UI1bl1_lab, UI2bl1_lab, UI3bl1_lab, UI4bl1_lab, UI5bl1_lab, UI6bl1_lab, UI7bl1_lab, UI8bl1_lab, UI9bl1_lab, UI10bl1_lab,
                UI11bl1_lab, UI12bl1_lab, UI13bl1_lab, UI14bl1_lab, UI15bl1_lab, UI16bl1_lab,
                // Блок расширения 2
                UI1bl2_lab, UI2bl2_lab, UI3bl2_lab, UI4bl2_lab, UI5bl2_lab, UI6bl2_lab, UI7bl2_lab, UI8bl2_lab, UI9bl2_lab, UI10bl2_lab,
                UI11bl2_lab, UI12bl2_lab, UI13bl2_lab, UI14bl2_lab, UI15bl2_lab, UI16bl2_lab,
                // Блок расширения 3
                UI1bl3_lab, UI2bl3_lab, UI3bl3_lab, UI4bl3_lab, UI5bl3_lab, UI6bl3_lab, UI7bl3_lab, UI8bl3_lab, UI9bl3_lab, UI10bl3_lab,
                UI11bl3_lab, UI12bl3_lab, UI13bl3_lab, UI14bl3_lab, UI15bl3_lab, UI16bl3_lab,
            };

            // AO labels
            var ao_labels = new List<Label>()
            {
                // ПЛК
                AO1_lab, AO2_lab, AO3_lab,
                // Блоки расширения 1, 2, 3
                AO1bl1_lab, AO2bl1_lab, AO1bl2_lab, AO2bl2_lab, AO1bl3_lab, AO2bl3_lab
            };

            // DO labels
            var do_labels = new List<Label>()
            {
                // ПЛК
                DO1_lab, DO2_lab, DO3_lab, DO4_lab, DO5_lab, DO6_lab,
                // Блок расширения 1
                DO1bl1_lab, DO2bl1_lab, DO3bl1_lab, DO4bl1_lab, DO5bl1_lab, DO6bl1_lab, DO7bl1_lab, DO8bl1_lab,
                // Блок расширения 2
                DO1bl2_lab, DO2bl2_lab, DO3bl2_lab, DO4bl2_lab, DO5bl2_lab, DO6bl2_lab, DO7bl2_lab, DO8bl2_lab,
                // Блок расширения 3
                DO1bl3_lab, DO2bl3_lab, DO3bl3_lab, DO4bl3_lab, DO5bl3_lab, DO6bl3_lab, DO7bl3_lab, DO8bl3_lab
            };

            foreach (var el in ui_labels) AddLabelSignalsState(el);
            foreach (var el in ao_labels) AddLabelSignalsState(el);
            foreach (var el in do_labels) AddLabelSignalsState(el);
        }

        ///<summary>Добавление состояния для comboBox по таблице сигналов</summary>
        private void AddComboSignalsState(ComboBox el) =>
            json.ComboSignalsState.Add(el.Name, el.SelectedItem.ToString());
        
        ///<summary>Сохранение состояний для comboBox таблицы сигналов</summary>
        private void BuildComboSignalsAll()
        {
            // UI сигналы
            var ui_combos = new List<ComboBox>()
            {
                // ПЛК
                UI1_combo, UI2_combo, UI3_combo, UI4_combo, UI5_combo, UI6_combo, UI7_combo, UI8_combo, UI9_combo, UI10_combo, UI11_combo,
                // Блок расширения 1
                UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo, UI7bl1_combo, UI8bl1_combo, UI9bl1_combo,
                UI10bl1_combo, UI11bl1_combo, UI12bl1_combo, UI13bl1_combo, UI14bl1_combo, UI15bl1_combo, UI16bl1_combo,
                // Блок расширения 2
                UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo, UI7bl2_combo, UI8bl2_combo, UI9bl2_combo,
                UI10bl2_combo, UI11bl2_combo, UI12bl2_combo, UI13bl2_combo, UI14bl2_combo, UI15bl2_combo, UI16bl2_combo,
                // Блок расширения 3
                UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo, UI7bl3_combo, UI8bl3_combo, UI9bl3_combo,
                UI10bl3_combo, UI11bl3_combo, UI12bl3_combo, UI13bl3_combo, UI14bl3_combo, UI15bl3_combo, UI16bl3_combo
            };

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

            // AO сигналы
            var ao_combos = new List<ComboBox>()
            {
                // ПЛК
                AO1_combo, AO2_combo, AO3_combo,
                // Блоки расширения 1, 2, 3
                AO1bl1_combo, AO2bl1_combo, AO1bl2_combo, AO2bl2_combo, AO1bl3_combo, AO2bl3_combo
            };

            // DO сигналы
            var do_combos = new List<ComboBox>()
            {
                // ПЛК
                DO1_combo, DO2_combo, DO3_combo, DO4_combo, DO5_combo, DO6_combo,
                // Блок расширения 1
                DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo, DO5bl1_combo, DO6bl1_combo, DO7bl1_combo, DO8bl1_combo,
                // Блок расширения 2
                DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo, DO5bl2_combo, DO6bl2_combo, DO7bl2_combo, DO8bl2_combo,
                // Блок расширения 3
                DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo, DO5bl3_combo, DO6bl3_combo, DO7bl3_combo, DO8bl3_combo
            };

            // Сохранение состояния выбранного элемента
            foreach (var el in ui_combos) AddComboSignalsState(el);
            foreach (var el in ui_combos_type) AddComboSignalsState(el);
            foreach (var el in ao_combos) AddComboSignalsState(el);
            foreach (var el in do_combos) AddComboSignalsState(el);
        }

        ///<summary>Добавление данных по comboBox в файл JSON</summary>
        private void AddComboSignalsItems(ComboBox el)
        {
            string[] arr_combo = new string[el.Items.Count];
            for (int i = 0; i < el.Items.Count; i++)
                arr_combo[i] = el.GetItemText(el.Items[i]);
            json.ComboSignalsItems.Add(el.Name, arr_combo);
        }

        ///<summary>Сохранение коллекций элементов для таблицы сигналов</summary>
        private void BuildComboItemsSignals()
        {
            // UI сигналы
            var ui_combos = new List<ComboBox>()
            {
                // ПЛК
                UI1_combo, UI2_combo, UI3_combo, UI4_combo, UI5_combo, UI6_combo, UI7_combo, UI8_combo, UI9_combo, UI10_combo, UI11_combo,
                // Блок расширения 1
                UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo, UI7bl1_combo, UI8bl1_combo, UI9bl1_combo,
                UI10bl1_combo, UI11bl1_combo, UI12bl1_combo, UI13bl1_combo, UI14bl1_combo, UI15bl1_combo, UI16bl1_combo,
                // Блок расширения 2
                UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo, UI7bl2_combo, UI8bl2_combo, UI9bl2_combo,
                UI10bl2_combo, UI11bl2_combo, UI12bl2_combo, UI13bl2_combo, UI14bl2_combo, UI15bl2_combo, UI16bl2_combo,
                // Блок расширения 3
                UI1bl3_combo, UI2bl3_combo, UI3bl3_combo, UI4bl3_combo, UI5bl3_combo, UI6bl3_combo, UI7bl3_combo, UI8bl3_combo, UI9bl3_combo,
                UI10bl3_combo, UI11bl3_combo, UI12bl3_combo, UI13bl3_combo, UI14bl3_combo, UI15bl3_combo, UI16bl3_combo
            };

            // AO сигналы
            var ao_combos = new List<ComboBox>()
            {
                // ПЛК
                AO1_combo, AO2_combo, AO3_combo,
                // Блоки расширения 1, 2, 3
                AO1bl1_combo, AO2bl1_combo, AO1bl2_combo, AO2bl2_combo, AO1bl3_combo, AO2bl3_combo
            };

            // DO сигналы
            var do_combos = new List<ComboBox>()
            {
                // ПЛК
                DO1_combo, DO2_combo, DO3_combo, DO4_combo, DO5_combo, DO6_combo,
                // Блок расширения 1
                DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo, DO5bl1_combo, DO6bl1_combo, DO7bl1_combo, DO8bl1_combo,
                // Блок расширения 2
                DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo, DO5bl2_combo, DO6bl2_combo, DO7bl2_combo, DO8bl2_combo,
                // Блок расширения 3
                DO1bl3_combo, DO2bl3_combo, DO3bl3_combo, DO4bl3_combo, DO5bl3_combo, DO6bl3_combo, DO7bl3_combo, DO8bl3_combo
            };

            foreach (var el in ui_combos) AddComboSignalsItems(el);
            foreach (var el in ao_combos) AddComboSignalsItems(el);
            foreach (var el in do_combos) AddComboSignalsItems(el);
        }

        ///<summary>Нажали на кнопку "Выгрузить в Excel"</summary>
		private void LoadToExl_Click(object sender, EventArgs e)
        {
            var filePath = Directory.GetCurrentDirectory();             // Для шаблона
            var savePath = Path.GetTempPath() + "/Signals.xlsx";        // Во временную папку
            SaveFileDialog dlg = new SaveFileDialog();                  // Окно для сохранения файла
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            var workbook = ExcelFile.Load(filePath + "/Template.xls");
            var worksheet = workbook.Worksheets[0];
            LoadtoExl_PLC_name_blocks(worksheet);                       // Сохранение модели ПЛК и блоков расширения
            LoadtoExl_PLC(worksheet);                                   // Формирование для ПЛК
            LoadtoExl_Ex1(worksheet);                                   // Формирование для блока расширения 1
            LoadtoExl_Ex2(worksheet);                                   // Формирование для блока расширения 2
            LoadtoExl_Ex3(worksheet);                                   // Формирование для блока расширения 3
            dlg.FileName = "Сигналы";
            dlg.DefaultExt = ".xlsx";

            // Сохранение в папку "Документы"
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dlg.Filter = "Excel table (.xlsx)|*.xlsx";
            workbook.Save(savePath);                                    // Сохранение во временную папку
            if (dlg.ShowDialog() == DialogResult.OK)
                try
                {
                    File.WriteAllBytes(dlg.FileName, File.ReadAllBytes(savePath));
                }
                catch (Exception ex)                                    // Нет доступа к файлу или другая ошибка
                {
                    MessageBox.Show(ex.Message);
                }  
        }

        ///<summary>Заполнение серым цветом неиспользуемых входов/выходов ПЛК Mini</summary>
        private void MiniPLC_fillGray(ExcelWorksheet wh)
        {
            var cells = new List<string>()
            {
                // UI сигналы
                "D11", "E11", "F11", "D12", "E12", "F12", "D13", "E13", "F13", "D14", "E14", "F14",
                // AO и DO сигналы
                "D18", "E18", "D24", "E24", "D25", "E25" 
            };

            foreach (var cell in cells)
            {
                wh.Cells[cell].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                wh.Cells[cell].Style.FillPattern.PatternForegroundColor = System.Drawing.Color.Gray;
            }
        }

        ///<summary>Заполнение серым цветом неиспользуемых сигналов 1-го блока M72E12RB (AO)</summary>
        private void M72E12RB_first_fillGray(ExcelWorksheet wh, string name)
        {
            wh.Cells["B26"].Value = "Модуль расширения 1 - " + name;

            var cells = new List<string>()
            {
                // UI сигналы
                "D33", "E33", "F33", "D34", "E34", "F34", "D35", "E35", "F35", "D36", "E36", "F36", "D37", "E37", "F37",
                "D38", "E38", "F38", "D39", "E39", "F39", "D40", "E40", "F40", "D41", "E41", "F41", "D42", "E42", "F42",
                // DO сигналы
                "D51", "E51", "D52", "E52", "D53", "E53", "D54", "E54"
            };

            foreach (var cell in cells)
            {
                wh.Cells[cell].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                wh.Cells[cell].Style.FillPattern.PatternForegroundColor = System.Drawing.Color.Gray;
            }
        }

        ///<summary>Заполнение серым цветом неиспользуемых сигналов 2-го блока M72E12RB (AO)</summary>
        private void M72E12RB_second_fillGray(ExcelWorksheet wh, string name)
        {
            wh.Cells["B55"].Value = "Модуль расширения 2 - " + name;

            var cells = new List<string>()
            {
                // UI сигналы
                "D62", "E62", "F62", "D63", "E63", "F63", "D64", "E64", "F64", "D65", "E65", "F65", "D66", "E66", "F66",
                "D67", "E67", "F67", "D68", "E68", "F68", "D69", "E69", "F69", "D70", "E70", "F70", "D71", "E71", "F71",
                // DO сигналы
                "D80", "E80", "D81", "E81", "D82", "E82", "D83", "E83"
            };

            foreach (var cell in cells)
            {
                wh.Cells[cell].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                wh.Cells[cell].Style.FillPattern.PatternForegroundColor = System.Drawing.Color.Gray;
            }
        }

        ///<summary>Заполнение серым цветом неиспользуемых сигналов 3 -го блока M72E12RB (AO)</summary>
        private void M72E12RB_third_fillGray(ExcelWorksheet wh, string name)
        {
            wh.Cells["B84"].Value = "Модуль расширения 3 - " + name;

            var cells = new List<string>()
            {
                // UI сигналы
                "D91", "E91", "F91", "D92", "E92", "F92", "D93", "E93", "F93", "D94", "E94", "F94", "D95", "E95", "F95",
                "D96", "E96", "F96", "D97", "E97", "F97", "D98", "E98", "F98", "D99", "E99", "F99", "D100", "E100", "F100",
                // DO сигналы
                "D109", "E109", "D110", "E110", "D111", "E111", "D112", "E112"
            };

            foreach (var cell in cells)
            {
                wh.Cells[cell].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                wh.Cells[cell].Style.FillPattern.PatternForegroundColor = System.Drawing.Color.Gray;
            }
        }

        ///<summary>Заполнение серым цветом неиспользуемых сигналов 1-го блока M72E12RA (DO + UI)</summary>
        private void M72E12RA_first_fillGray(ExcelWorksheet wh, string name)
        {
            wh.Cells["B26"].Value = "Модуль расширения 1 - " + name;

            var cells = new List<string>()
            {
                // UI сигналы
                "D33", "E33", "F33", "D34", "E34", "F34", "D35", "E35", "F35", "D36", "E36", "F36", "D37", "E37", "F37",
                "D38", "E38", "F38", "D39", "E39", "F39", "D40", "E40", "F40", "D41", "E41", "F41", "D42", "E42", "F42",
                // AO сигналы
                "D44", "E44", "D45", "E45",
                // DO сигналы
                "D53", "E53", "D54", "E54"
            };

            foreach (var cell in cells)
            {
                wh.Cells[cell].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                wh.Cells[cell].Style.FillPattern.PatternForegroundColor = System.Drawing.Color.Gray;
            }
        }

        ///<summary>Заполнение серым цветом неиспользуемых сигналов 2-го блока M72E12RA (DO + UI)</summary>
        private void M72E12RA_second_fillGray(ExcelWorksheet wh, string name)
        {
            wh.Cells["B55"].Value = "Модуль расширения 2 - " + name;

            var cells = new List<string>()
            {
                // UI сигналы
                "D62", "E62", "F62", "D63", "E63", "F63", "D64", "E64", "F64", "D65", "E65", "F65", "D66", "E66", "F66",
                "D67", "E67", "F67", "D68", "E68", "F68", "D69", "E69", "F69", "D70", "E70", "F70", "D71", "E71", "F71",
                // AO сигналы
                "D73", "E73", "D74", "E74",
                // DO сигналы
                "D82", "E82", "D83", "E83"
            };

            foreach (var cell in cells)
            {
                wh.Cells[cell].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                wh.Cells[cell].Style.FillPattern.PatternForegroundColor = System.Drawing.Color.Gray;
            }
        }

        ///<summary>Заполнение серым цветом неиспользуемых сигналов 3-го блока M72E12RA (DO + UI)</summary>
        private void M72E12RA_third_fillGray(ExcelWorksheet wh, string name)
        {
            wh.Cells["B84"].Value = "Модуль расширения 3 - " + name;

            var cells = new List<string>()
            {
                // UI сигналы
                "D91", "E91", "F91", "D92", "E92", "F92", "D93", "E93", "F93", "D94", "E94", "F94", "D95", "E95", "F95",
                "D96", "E96", "F96", "D97", "E97", "F97", "D98", "E98", "F98", "D99", "E99", "F99", "D100", "E100", "F100",
                // AO сигналы
                "D102", "E102", "D103", "E103",
                // DO сигналы
                "D111", "E111", "D112", "E112"
            };

            foreach (var cell in cells)
            {
                wh.Cells[cell].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                wh.Cells[cell].Style.FillPattern.PatternForegroundColor = System.Drawing.Color.Gray;
            }
        }

        ///<summary>Заполнение серым цветом неиспользуемых сигналов 1-го блока M72E08RA (DO)</summary>
        private void M72E08RA_first_fillGray(ExcelWorksheet wh, string name)
        {
            wh.Cells["B26"].Value = "Модуль расширения 1 - " + name;

            var cells = new List<string>()
            {
                // UI сигналы
                "D27", "E27", "F27", "D28", "E28", "F28", "D29", "E29", "F29", "D30", "E30", "F30", "D31", "E31", "F31",
                "D32", "E32", "F32", "D33", "E33", "F33", "D34", "E34", "F34", "D35", "E35", "F35", "D36", "E36", "F36",
                "D37", "E37", "F37", "D38", "E38", "F38", "D39", "E39", "F39", "D40", "E40", "F40", "D41", "E41", "F41",
                "D42", "E42", "F42",
                // AO сигналы
                "D44", "E44", "D45", "E45"
            };

            foreach (var cell in cells)
            {
                wh.Cells[cell].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                wh.Cells[cell].Style.FillPattern.PatternForegroundColor = System.Drawing.Color.Gray;
            }
        }

        ///<summary>Заполнение серым цветом неиспользуемых сигналов 2-го блока M72E08RA (DO)</summary>
        private void M72E08RA_second_fillGray(ExcelWorksheet wh, string name)
        {
            wh.Cells["B55"].Value = "Модуль расширения 2 - " + name;

            var cells = new List<string>()
            {
                // UI сигналы
                "D56", "E56", "F56", "D57", "E57", "F57", "D58", "E58", "F58", "D59", "E59", "F59", "D60", "E60", "F60",
                "D61", "E61", "F61", "D62", "E62", "F62", "D63", "E63", "F63", "D64", "E64", "F64", "D65", "E65", "F65", 
                "D66", "E66", "F66", "D67", "E67", "F67", "D68", "E68", "F68", "D69", "E69", "F69", "D70", "E70", "F70", 
                "D71", "E71", "F71",
                // AO сигналы
                "D73", "E73", "D74", "E74"
            };

            foreach (var cell in cells)
            {
                wh.Cells[cell].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                wh.Cells[cell].Style.FillPattern.PatternForegroundColor = System.Drawing.Color.Gray;
            }
        }

        ///<summary>Заполнение серым цветом неиспользуемых сигналов 3-го блока M72E08RA (DO)</summary>
        private void M72E08RA_third_fillGray(ExcelWorksheet wh, string name)
        {
            wh.Cells["B84"].Value = "Модуль расширения 3 - " + name;

            var cells = new List<string>()
            {
                // UI сигналы
                "D85", "E85", "F85", "D86", "E86", "F86", "D87", "E87", "F87", "D88", "E88", "F88", "D89", "E89", "F89",
                "D90", "E90", "F90", "D91", "E91", "F91", "D92", "E92", "F92", "D93", "E93", "F93", "D94", "E94", "F94", 
                "D95", "E95", "F95", "D96", "E96", "F96", "D97", "E97", "F97", "D98", "E98", "F98", "D99", "E99", "F99", 
                "D100", "E100", "F100",
                // AO сигналы
                "D102", "E102", "D103", "E103"
            };

            foreach (var cell in cells)
            {
                wh.Cells[cell].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                wh.Cells[cell].Style.FillPattern.PatternForegroundColor = System.Drawing.Color.Gray;
            }
        }

        ///<summary>Заполнение серым цветом неиспользуемых сигналов 1-го блока M72E16NA (UI)</summary>
        private void M72E16NA_first_fillGray(ExcelWorksheet wh, string name)
        {
            wh.Cells["B26"].Value = "Модуль расширения 1 - " + name;

            var cells = new List<string>()
            {
                // AO сигналы
                "D44", "E44", "D45", "E45",
                // DO сигналы
                "D47", "E47", "D48", "E48", "D49", "E49", "D50", "E50", "D51", "E51", "D52", "E52", "D53", "E53", "D54", "E54"
            };

            foreach (var cell in cells)
            {
                wh.Cells[cell].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                wh.Cells[cell].Style.FillPattern.PatternForegroundColor = System.Drawing.Color.Gray;
            }
        }

        ///<summary>Заполнение серым цветом неиспользуемых сигналов 2-го блока M72E16NA (UI)</summary>
        private void M72E16NA_second_fillGray(ExcelWorksheet wh, string name)
        {
            wh.Cells["B55"].Value = "Модуль расширения 2 - " + name;

            var cells = new List<string>()
            {
                // AO сигналы
                "D73", "E73", "D74", "E74",
                // DO сигналы
                "D76", "E76", "D77", "E77", "D78", "E78", "D79", "E79", "D80", "E80", "D81", "E81", "D82", "E82", "D83", "E83"
            };

            foreach (var cell in cells)
            {
                wh.Cells[cell].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                wh.Cells[cell].Style.FillPattern.PatternForegroundColor = System.Drawing.Color.Gray;
            }
        }

        ///<summary>Заполнение серым цветом неиспользуемых сигналов 3-го блока M72E16NA (UI)</summary>
        private void M72E16NA_third_fillGray(ExcelWorksheet wh, string name)
        {
            wh.Cells["B84"].Value = "Модуль расширения 3 - " + name;

            var cells = new List<string>()
            {
                // AO сигналы
                "D102", "E102", "D103", "E103",
                // DO сигналы
                "D105", "E105", "D106", "E106", "D107", "E107", "D108", "E108", "D109", "E109", 
                "D110", "E110", "D111", "E111", "D112", "E112"
            };

            foreach (var cell in cells)
            {
                wh.Cells[cell].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                wh.Cells[cell].Style.FillPattern.PatternForegroundColor = System.Drawing.Color.Gray;
            }
        }

        ///<summary>Заполнение серым цветом неиспользуемых сигналов 1-го блока расширения</summary>
        private void First_EmptyBlock_fillGray(ExcelWorksheet wh)
        {
            wh.Cells["B26"].Value = "(нет модуля расширения)";

            var cells = new List<string>()
            {
                // UI сигналы, блок 1
                "D27", "E27", "F27", "D28", "E28", "F28", "D29", "E29", "F29", "D30", "E30", "F30", "D31", "E31", "F31",
                "D32", "E32", "F32", "D33", "E33", "F33", "D34", "E34", "F34", "D35", "E35", "F35", "D36", "E36", "F36",
                "D37", "E37", "F37", "D38", "E38", "F38", "D39", "E39", "F39", "D40", "E40", "F40", "D41", "E41", "F41",
                "D42", "E42", "F42",
                // AO сигналы, блок 1
                "D44", "E44", "D45", "E45",
                // DO сигналы, блок 1
                "D47", "E47", "D48", "E48", "D49", "E49", "D50", "E50", "D51", "E51", "D52", "E52", "D53", "E53", "D54", "E54"
            };

            foreach (var cell in cells)
            {
                wh.Cells[cell].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                wh.Cells[cell].Style.FillPattern.PatternForegroundColor = System.Drawing.Color.Gray;
            }
        }

        ///<summary>Заполнение серым цветом неиспользуемых сигналов 2-го блока расширения</summary>
        private void Second_EmptyBlock_fillGray(ExcelWorksheet wh)
        {
            wh.Cells["B55"].Value = "(нет модуля расширения)";

            var cells = new List<string>()
            {
                // UI сигналы, блок 2
                "D56", "E56", "F56", "D57", "E57", "F57", "D58", "E58", "F58", "D59", "E59", "F59", "D60", "E60", "F60",
                "D61", "E61", "F61", "D62", "E62", "F62", "D63", "E63", "F63", "D64", "E64", "F64", "D65", "E65", "F65",
                "D66", "E66", "F66", "D67", "E67", "F67", "D68", "E68", "F68", "D69", "E69", "F69", "D70", "E70", "F70",
                "D71", "E71", "F71",
                // AO сигналы, блок 2
                "D73", "E73", "D74", "E74",
                // DO сигналы, блок 2
                "D76", "E76", "D77", "E77", "D78", "E78", "D79", "E79", "D80", "E80", "D81", "E81", "D82", "E82", "D83", "E83"
            };

            foreach (var cell in cells)
            {
                wh.Cells[cell].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                wh.Cells[cell].Style.FillPattern.PatternForegroundColor = System.Drawing.Color.Gray;
            }
        }

        ///<summary>Заполнение серым цветом неиспользуемых сигналов 3-го блока расширения</summary>
        private void Third_EmptyBlock_fillGray(ExcelWorksheet wh)
        {
            wh.Cells["B84"].Value = "(нет модуля расширения)";

            var cells = new List<string>()
            {
                // UI сигналы, блок 3
                "D85", "E85", "F85", "D86", "E86", "F86", "D87", "E87", "F87", "D88", "E88", "F88", "D89", "E89", "F89",
                "D90", "E90", "F90", "D91", "E91", "F91", "D92", "E92", "F92", "D93", "E93", "F93", "D94", "E94", "F94",
                "D95", "E95", "F95", "D96", "E96", "F96", "D97", "E97", "F97", "D98", "E98", "F98", "D99", "E99", "F99",
                "D100", "E100", "F100",
                // AO сигналы, блок 3
                "D102", "E102", "D103", "E103",
                // DO сигналы, блок 3
                "D105", "E105", "D106", "E106", "D107", "E107", "D108", "E108", "D109", "E109",
                "D110", "E110", "D111", "E111", "D112", "E112"
            };

            foreach (var cell in cells)
            {
                wh.Cells[cell].Style.FillPattern.PatternStyle = FillPatternStyle.Solid;
                wh.Cells[cell].Style.FillPattern.PatternForegroundColor = System.Drawing.Color.Gray;
            }
        }

        ///<summary>Сохранение модели и типов блоков расширения</summary>
        private void LoadtoExl_PLC_name_blocks(ExcelWorksheet wh)
        {   // Запись для модели ПЛК
            if (plkChangeIndexLast == 0)                                                // Выбран контроллер Mini
            {
                wh.Cells["B3"].Value = "Программируемый контроллер - M72OD13R (Mini)";
                MiniPLC_fillGray(wh);                                                   // Заливка серым цветом неиспользуемых ячеек ПЛК Mini
            }
            else if (plkChangeIndexLast == 1)                                           // Выбран контроллер Optimized
                wh.Cells["B3"].Value = 
                    "Программируемый контроллер - M72OD20R (Optimized)";

            // Запись по блокам расширения
            var count_M72E12RB = expansion_blocks.Where(x => x == M72E12RB).Count();    // Блок AO
            var count_M72E12RA = expansion_blocks.Where(x => x == M72E12RA).Count();    // Блок DO + UI
            var count_M72E08RA = expansion_blocks.Where(x => x == M72E08RA).Count();    // Блок DO
            var count_M72E16NA = expansion_blocks.Where(x => x == M72E16NA).Count();    // Блок UI

            if (expansion_blocks.Count == 0) {                                          // Нет блоков расширения
                First_EmptyBlock_fillGray(wh);                                          // 1-й блок расширения
                Second_EmptyBlock_fillGray(wh);                                         // 2-й блок расширения
                Third_EmptyBlock_fillGray(wh);                                          // 3-й блок расширения
            }
            else if (expansion_blocks.Count == 1)                                       // Один блок расширения
            {
                string name = expansion_blocks[0].Name;                                 // Название блока расширения 1
                Second_EmptyBlock_fillGray(wh);                                         // 2-й блок расширения
                Third_EmptyBlock_fillGray(wh);                                          // 3-й блок расширения

                if (expansion_blocks[0] == M72E12RB)                                    // Заливка серым цветом, AO блок
                    M72E12RB_first_fillGray(wh, name);       
                if (expansion_blocks[0] == M72E12RA)                                    // Заливка серым цветом, DO + UI блок
                    M72E12RA_first_fillGray(wh, name);       
                if (expansion_blocks[0] == M72E08RA)                                    // Заливка серым цветом, DO блок
                    M72E08RA_first_fillGray(wh, name);       
                if (expansion_blocks[0] == M72E16NA)                                    // Заливка серым цветом, UI блок
                    M72E16NA_first_fillGray(wh, name);       
            } 
            else if (expansion_blocks.Count == 2)                                       // Два блока расширения
            {
                if (expansion_blocks.Contains(M72E12RB))                                // Есть блок расширения с AO
                {  
                    M72E12RB_first_fillGray(wh, M72E12RB.Name);                         // Заливка серым цветом, AO блок 1

                    if (count_M72E12RB == 2)                                            // Второй блок AO
                        M72E12RB_second_fillGray(wh, M72E12RB.Name);
                    else if (count_M72E12RA == 1)                                       // Второй блок DO + UI
                        M72E12RA_second_fillGray(wh, M72E12RA.Name);
                    else if (count_M72E08RA == 1)                                       // Второй блок DO
                        M72E08RA_second_fillGray(wh, M72E08RA.Name);
                    else if (count_M72E16NA == 1)                                       // Второй блок UI
                        M72E16NA_second_fillGray(wh, M72E16NA.Name);
                }
                else if (expansion_blocks.Contains(M72E12RA))                           // Есть блок расширения DO + UI
                {
                    M72E12RA_first_fillGray(wh, M72E12RA.Name);                         // Заливка серым цветом, DO + UI блок 1

                    if (count_M72E12RA == 2)                                            // Второй блок DO + UI
                        M72E12RA_second_fillGray(wh, M72E12RA.Name);
                    else if (count_M72E08RA == 1)                                       // Второй блок DO
                        M72E08RA_second_fillGray(wh, M72E08RA.Name);
                    else if (count_M72E16NA == 1)                                       // Второй блок UI
                        M72E16NA_second_fillGray(wh, M72E16NA.Name);
                }
                else if (expansion_blocks.Contains(M72E08RA))                           // Есть блок расширения DO
                {
                    M72E08RA_first_fillGray(wh, M72E08RA.Name);                         // Заливка серым цветом, DO блок 1

                    if (count_M72E08RA == 2)                                            // Второй блок DO
                        M72E08RA_second_fillGray(wh, M72E08RA.Name);
                    else if (count_M72E16NA == 1)                                       // Второй блок UI
                        M72E16NA_second_fillGray(wh, M72E16NA.Name);        
                }
                else if (expansion_blocks.Contains(M72E16NA))
                {
                    M72E16NA_first_fillGray(wh, M72E16NA.Name);                         // Заливка серым цветом, UI блок 1

                    if (count_M72E16NA == 2)                                            // Второй блок UI
                        M72E16NA_second_fillGray(wh, M72E16NA.Name);
                }

                Third_EmptyBlock_fillGray(wh);                                          // 3-й блок расширения
            }
            else if (expansion_blocks.Count == 3)                                       // Три блока расширения
            {
                if (expansion_blocks.Contains(M72E12RB))                                // Есть блок расширения с AO
                {
                    M72E12RB_first_fillGray(wh, M72E12RB.Name);                         // Заливка серым цветом, AO блок 1

                    if (count_M72E12RB > 1)                                             // Два блока AO
                    {
                        M72E12RB_second_fillGray(wh, M72E12RB.Name);                    // Второй блок AO

                        if (count_M72E12RB == 3)
                            M72E12RB_third_fillGray(wh, M72E12RB.Name);                 // Третий блок AO
                        else if (count_M72E12RA == 1)
                            M72E12RA_third_fillGray(wh, M72E12RA.Name);                 // Третий блок DO + UI
                        else if (count_M72E08RA == 1)
                            M72E08RA_third_fillGray(wh, M72E08RA.Name);                 // Третий блок DO
                        else if (count_M72E16NA == 1)
                            M72E16NA_third_fillGray(wh, M72E16NA.Name);                 // Третий блок UI
                    }
                    else if (count_M72E12RA > 0)                                        // Первый блок AO, второй DO + UI
                    {
                        M72E12RA_second_fillGray(wh, M72E12RA.Name);                    // Второй блок DO + UI

                        if (count_M72E12RA > 1)
                            M72E12RA_third_fillGray(wh, M72E12RA.Name);                 // Третий блок DO + UI
                        else if (count_M72E08RA == 1)
                            M72E08RA_third_fillGray(wh, M72E08RA.Name);                 // Третий блок DO
                        else if (count_M72E16NA == 1)
                            M72E16NA_third_fillGray(wh, M72E16NA.Name);                 // Третий блок UI
                    }
                    else if (count_M72E08RA > 0)                                        // Первый блок AO, второй DO 
                    {
                        M72E08RA_second_fillGray(wh, M72E08RA.Name);                    // Второй блок DO

                        if (count_M72E08RA > 1)
                            M72E08RA_third_fillGray(wh, M72E08RA.Name);                 // Третий блок DO
                        else if (count_M72E16NA == 1)
                            M72E16NA_third_fillGray(wh, M72E16NA.Name);                 // Третий блок UI
                    }
                    else if (count_M72E16NA > 0)                                        // Первый блок AO, второй UI
                    {
                        M72E16NA_second_fillGray(wh, M72E16NA.Name);                    // Второй блок UI

                        if (count_M72E16NA > 1)
                            M72E16NA_third_fillGray(wh, M72E16NA.Name);                 // Третий блок UI
                    }
                }
                else if (expansion_blocks.Contains(M72E12RA))                           // Есть блок расширения DO + UI
                {
                    M72E12RA_first_fillGray(wh, M72E12RA.Name);                         // Заливка серым цветом, DO + UI блок 1
                    
                    if (count_M72E12RA > 1)                                             // Два блока DO + UI
                    {
                        M72E12RA_second_fillGray(wh, M72E12RA.Name);                    // Второй блок DO + UI
                        if (count_M72E12RA == 3)
                            M72E12RA_third_fillGray(wh, M72E12RA.Name);                 // Третий блок DO + UI
                        else if (count_M72E08RA == 1)
                            M72E08RA_third_fillGray(wh, M72E08RA.Name);                 // Третий блок DO
                        else if (count_M72E16NA == 1)
                            M72E16NA_third_fillGray(wh, M72E16NA.Name);                 // Третией блок UI
                    }
                    else if (count_M72E08RA > 0)                                        // Первый блок DO + UI, второй DO
                    {
                        M72E08RA_second_fillGray(wh, M72E08RA.Name);                    // Второй блок DO

                        if (count_M72E08RA > 1)
                            M72E08RA_third_fillGray(wh, M72E08RA.Name);                 // Третий блок DO
                        else if (count_M72E16NA == 1)
                            M72E16NA_third_fillGray(wh, M72E16NA.Name);                 // Третий блок UI
                    }
                    else if (count_M72E16NA > 0)                                        // Первый блок DO + UI, второй UI
                    {   
                        M72E16NA_second_fillGray(wh, M72E16NA.Name);                    // Второй блок UI

                        if (count_M72E16NA > 1)
                            M72E16NA_third_fillGray(wh, M72E16NA.Name);                 // Третий блок UI
                    }
                }
                else if (expansion_blocks.Contains(M72E08RA))                           // Есть блок расширения DO
                {
                    M72E08RA_first_fillGray(wh, M72E08RA.Name);                         // Заливка серым цветом, DO блок 1

                    if (count_M72E08RA > 1)                                             // Два блока DO
                    {
                        M72E08RA_second_fillGray(wh, M72E08RA.Name);                    // Второй блок DO

                        if (count_M72E08RA == 3)
                            M72E08RA_third_fillGray(wh, M72E08RA.Name);                 // Третий блок DO
                        else if (count_M72E16NA == 1)
                            M72E16NA_third_fillGray(wh, M72E16NA.Name);                 // Третий блок UI
                    }
                    else if (count_M72E16NA > 0)
                    {
                        M72E16NA_second_fillGray(wh, M72E16NA.Name);                    // Второй блок UI

                        if (count_M72E16NA > 1)
                            M72E16NA_third_fillGray(wh, M72E16NA.Name);                 // Третий блок UI
                    }
                }
                else if (expansion_blocks.Contains(M72E16NA))                           // Есть блок расширения UI
                {
                    M72E16NA_first_fillGray(wh, M72E16NA.Name);                         // Первый блок UI

                    if (count_M72E16NA > 1)
                    {
                        M72E16NA_second_fillGray(wh, M72E16NA.Name);                    // Второй блок UI
                        if (count_M72E16NA == 3)
                            M72E16NA_third_fillGray(wh, M72E16NA.Name);                 // Третий блок UI
                    }
                }
            }
        }

        ///<summary>Формирование сигналов для ПЛК</summary>
        private void LoadtoExl_PLC(ExcelWorksheet wh)
        {
            // UI сигналы для ПЛК
            if (UI1_combo.SelectedItem.ToString() != NOT_SELECTED)              // UI1
            {
                wh.Cells["D4"].Value = UI1_combo.SelectedItem.ToString();
                wh.Cells["E4"].Value = int.Parse(UI1_lab.Text);
                wh.Cells["F4"].Value = UI1_typeCombo.SelectedItem.ToString();
            }
            if (UI2_combo.SelectedItem.ToString() != NOT_SELECTED)              // UI2
            {
                wh.Cells["D5"].Value = UI2_combo.SelectedItem.ToString();
                wh.Cells["E5"].Value = int.Parse(UI2_lab.Text);
                wh.Cells["F5"].Value = UI2_typeCombo.SelectedItem.ToString();
            }
            if (UI3_combo.SelectedItem.ToString() != NOT_SELECTED)              // UI3
            {
                wh.Cells["D6"].Value = UI3_combo.SelectedItem.ToString();
                wh.Cells["E6"].Value = int.Parse(UI3_lab.Text);
                wh.Cells["F6"].Value = UI3_typeCombo.SelectedItem.ToString();
            }
            if (UI4_combo.SelectedItem.ToString() != NOT_SELECTED)              // UI4
            {
                wh.Cells["D7"].Value = UI4_combo.SelectedItem.ToString();
                wh.Cells["E7"].Value = int.Parse(UI4_lab.Text);
                wh.Cells["F7"].Value = UI4_typeCombo.SelectedItem.ToString();
            }
            if (UI5_combo.SelectedItem.ToString() != NOT_SELECTED)              // UI5
            {
                wh.Cells["D8"].Value = UI5_combo.SelectedItem.ToString();
                wh.Cells["E8"].Value = int.Parse(UI5_lab.Text);
                wh.Cells["F8"].Value = UI5_typeCombo.SelectedItem.ToString();
            }
            if (UI6_combo.SelectedItem.ToString() != NOT_SELECTED)              // UI6
            {
                wh.Cells["D9"].Value = UI6_combo.SelectedItem.ToString();
                wh.Cells["E9"].Value = int.Parse(UI6_lab.Text);
                wh.Cells["F9"].Value = UI6_typeCombo.SelectedItem.ToString();
            }
            if (UI7_combo.SelectedItem.ToString() != NOT_SELECTED)              // UI7
            {
                wh.Cells["D10"].Value = UI7_combo.SelectedItem.ToString();
                wh.Cells["E10"].Value = int.Parse(UI7_lab.Text);
                wh.Cells["F10"].Value = UI7_typeCombo.SelectedItem.ToString();
            }
            if (UI8_combo.SelectedItem.ToString() != NOT_SELECTED)              // UI8
            {
                wh.Cells["D11"].Value = UI8_combo.SelectedItem.ToString();
                wh.Cells["E11"].Value = int.Parse(UI8_lab.Text);
                wh.Cells["F11"].Value = UI8_typeCombo.SelectedItem.ToString();
            }
            if (UI9_combo.SelectedItem.ToString() != NOT_SELECTED)              // UI9
            {
                wh.Cells["D12"].Value = UI9_combo.SelectedItem.ToString();
                wh.Cells["E12"].Value = int.Parse(UI9_lab.Text);
                wh.Cells["F12"].Value = UI9_typeCombo.SelectedItem.ToString();
            }
            if (UI10_combo.SelectedItem.ToString() != NOT_SELECTED)              // UI10
            {
                wh.Cells["D13"].Value = UI10_combo.SelectedItem.ToString();
                wh.Cells["E13"].Value = int.Parse(UI10_lab.Text);
                wh.Cells["F13"].Value = UI10_typeCombo.SelectedItem.ToString();
            }
            if (UI11_combo.SelectedItem.ToString() != NOT_SELECTED)              // UI11
            {
                wh.Cells["D14"].Value = UI11_combo.SelectedItem.ToString();
                wh.Cells["E14"].Value = int.Parse(UI11_lab.Text);
                wh.Cells["F14"].Value = UI11_typeCombo.SelectedItem.ToString();
            }
            // AO сигналы для ПЛК
            if (AO1_combo.SelectedItem.ToString() != NOT_SELECTED)              // AO1
            {
                wh.Cells["D16"].Value = AO1_combo.SelectedItem.ToString();
                wh.Cells["E16"].Value = int.Parse(AO1_lab.Text);
            }
            if (AO2_combo.SelectedItem.ToString() != NOT_SELECTED)              // AO2
            {
                wh.Cells["D17"].Value = AO2_combo.SelectedItem.ToString();
                wh.Cells["E17"].Value = int.Parse(AO2_lab.Text);
            }
            if (AO3_combo.SelectedItem.ToString() != NOT_SELECTED)              // AO3
            {
                wh.Cells["D18"].Value = AO3_combo.SelectedItem.ToString();
                wh.Cells["E18"].Value = int.Parse(AO3_lab.Text);
            }
            // DO сигналы для ПЛК
            if (DO1_combo.SelectedItem.ToString() != NOT_SELECTED)              // DO1
            {
                wh.Cells["D20"].Value = DO1_combo.SelectedItem.ToString();
                wh.Cells["E20"].Value = int.Parse(DO1_lab.Text);
            }
            if (DO2_combo.SelectedItem.ToString() != NOT_SELECTED)              // DO2
            {
                wh.Cells["D21"].Value = DO2_combo.SelectedItem.ToString();
                wh.Cells["E21"].Value = int.Parse(DO2_lab.Text);
            }
            if (DO3_combo.SelectedItem.ToString() != NOT_SELECTED)              // DO3
            {
                wh.Cells["D22"].Value = DO3_combo.SelectedItem.ToString();
                wh.Cells["E22"].Value = int.Parse(DO3_lab.Text);
            }
            if (DO4_combo.SelectedItem.ToString() != NOT_SELECTED)              // DO4
            {
                wh.Cells["D23"].Value = DO4_combo.SelectedItem.ToString();
                wh.Cells["E23"].Value = int.Parse(DO4_lab.Text);
            }
            if (DO5_combo.SelectedItem.ToString() != NOT_SELECTED)              // DO5
            {
                wh.Cells["D24"].Value = DO5_combo.SelectedItem.ToString();
                wh.Cells["E24"].Value = int.Parse(DO5_lab.Text);
            }
            if (DO6_combo.SelectedItem.ToString() != NOT_SELECTED)              // DO6
            {
                wh.Cells["D25"].Value = DO6_combo.SelectedItem.ToString();
                wh.Cells["E25"].Value = int.Parse(DO6_lab.Text);
            }
        }

        ///<summary>Формирование для блока расширения 1</summary>
        private void LoadtoExl_Ex1(ExcelWorksheet wh)
        {
            // UI сигналы для блока расширения 1
            if (UI1bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI1
            {
                wh.Cells["D27"].Value = UI1bl1_combo.SelectedItem.ToString();
                wh.Cells["E27"].Value = int.Parse(UI1bl1_lab.Text);
                wh.Cells["F27"].Value = UI1bl1_typeCombo.SelectedItem.ToString();
            }
            if (UI2bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI2
            {
                wh.Cells["D28"].Value = UI2bl1_combo.SelectedItem.ToString();
                wh.Cells["E28"].Value = int.Parse(UI2bl1_lab.Text);
                wh.Cells["F28"].Value = UI2bl1_typeCombo.SelectedItem.ToString();
            }
            if (UI3bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI3
            {
                wh.Cells["D29"].Value = UI3bl1_combo.SelectedItem.ToString();
                wh.Cells["E29"].Value = int.Parse(UI3bl1_lab.Text);
                wh.Cells["F29"].Value = UI3bl1_typeCombo.SelectedItem.ToString();
            }
            if (UI4bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI4
            {
                wh.Cells["D30"].Value = UI4bl1_combo.SelectedItem.ToString();
                wh.Cells["E30"].Value = int.Parse(UI4bl1_lab.Text);
                wh.Cells["F30"].Value = UI4bl1_typeCombo.SelectedItem.ToString();
            }
            if (UI5bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI5
            {
                wh.Cells["D31"].Value = UI5bl1_combo.SelectedItem.ToString();
                wh.Cells["E31"].Value = int.Parse(UI5bl1_lab.Text);
                wh.Cells["F31"].Value = UI5bl1_typeCombo.SelectedItem.ToString();
            }
            if (UI6bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI6
            {
                wh.Cells["D32"].Value = UI6bl1_combo.SelectedItem.ToString();
                wh.Cells["E32"].Value = int.Parse(UI6bl1_lab.Text);
                wh.Cells["F32"].Value = UI6bl1_typeCombo.SelectedItem.ToString();
            }
            if (UI7bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI7
            {
                wh.Cells["D33"].Value = UI7bl1_combo.SelectedItem.ToString();
                wh.Cells["E33"].Value = int.Parse(UI7bl1_lab.Text);
                wh.Cells["F33"].Value = UI7bl1_typeCombo.SelectedItem.ToString();
            }
            if (UI8bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI8
            {
                wh.Cells["D34"].Value = UI8bl1_combo.SelectedItem.ToString();
                wh.Cells["E34"].Value = int.Parse(UI8bl1_lab.Text);
                wh.Cells["F34"].Value = UI8bl1_typeCombo.SelectedItem.ToString();
            }
            if (UI9bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI9
            {
                wh.Cells["D35"].Value = UI9bl1_combo.SelectedItem.ToString();
                wh.Cells["E35"].Value = int.Parse(UI9bl1_lab.Text);
                wh.Cells["F35"].Value = UI9bl1_typeCombo.SelectedItem.ToString();
            }
            if (UI10bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI10
            {
                wh.Cells["D36"].Value = UI10bl1_combo.SelectedItem.ToString();
                wh.Cells["E36"].Value = int.Parse(UI10bl1_lab.Text);
                wh.Cells["F36"].Value = UI10bl1_typeCombo.SelectedItem.ToString();
            }
            if (UI11bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI11
            {
                wh.Cells["D37"].Value = UI11bl1_combo.SelectedItem.ToString();
                wh.Cells["E37"].Value = int.Parse(UI11bl1_lab.Text);
                wh.Cells["F37"].Value = UI11bl1_typeCombo.SelectedItem.ToString();
            }
            if (UI12bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI12
            {
                wh.Cells["D38"].Value = UI12bl1_combo.SelectedItem.ToString();
                wh.Cells["E38"].Value = int.Parse(UI12bl1_lab.Text);
                wh.Cells["F38"].Value = UI12bl1_typeCombo.SelectedItem.ToString();
            }
            if (UI13bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI13
            {
                wh.Cells["D39"].Value = UI13bl1_combo.SelectedItem.ToString();
                wh.Cells["E39"].Value = int.Parse(UI13bl1_lab.Text);
                wh.Cells["F39"].Value = UI13bl1_typeCombo.SelectedItem.ToString();
            }
            if (UI14bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI14
            {
                wh.Cells["D40"].Value = UI14bl1_combo.SelectedItem.ToString();
                wh.Cells["E40"].Value = int.Parse(UI14bl1_lab.Text);
                wh.Cells["F40"].Value = UI14bl1_typeCombo.SelectedItem.ToString();
            }
            if (UI15bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI15
            {
                wh.Cells["D41"].Value = UI15bl1_combo.SelectedItem.ToString();
                wh.Cells["E41"].Value = int.Parse(UI15bl1_lab.Text);
                wh.Cells["F41"].Value = UI15bl1_typeCombo.SelectedItem.ToString();
            }
            if (UI16bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI16
            {
                wh.Cells["D42"].Value = UI16bl1_combo.SelectedItem.ToString();
                wh.Cells["E42"].Value = int.Parse(UI16bl1_lab.Text);
                wh.Cells["F42"].Value = UI16bl1_typeCombo.SelectedItem.ToString();
            }
            // AO сигналы для блока расширения 1
            if (AO1bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // AO1
            {
                wh.Cells["D44"].Value = AO1bl1_combo.SelectedItem.ToString();
                wh.Cells["E44"].Value = int.Parse (AO1bl1_lab.Text);
            }
            if (AO2bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // AO2
            {
                wh.Cells["D45"].Value = AO2bl1_combo.SelectedItem.ToString();
                wh.Cells["E45"].Value = int.Parse(AO2bl1_lab.Text);
            }
            // DO сигналы для блока расширения 1
            if (DO1bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO1
            {
                wh.Cells["D47"].Value = DO1bl1_combo.SelectedItem.ToString();
                wh.Cells["E47"].Value = int.Parse(DO1bl1_lab.Text);
            }
            if (DO2bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO2
            {
                wh.Cells["D48"].Value = DO2bl1_combo.SelectedItem.ToString();
                wh.Cells["E48"].Value = int.Parse(DO2bl1_lab.Text);
            }
            if (DO3bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO3
            {
                wh.Cells["D49"].Value = DO3bl1_combo.SelectedItem.ToString();
                wh.Cells["E49"].Value = int.Parse(DO3bl1_lab.Text);
            }
            if (DO4bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO4
            {
                wh.Cells["D50"].Value = DO4bl1_combo.SelectedItem.ToString();
                wh.Cells["E50"].Value = int.Parse(DO4bl1_lab.Text);
            }
            if (DO5bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO5
            {
                wh.Cells["D51"].Value = DO5bl1_combo.SelectedItem.ToString();
                wh.Cells["E51"].Value = int.Parse(DO5bl1_lab.Text);
            }
            if (DO6bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO6
            {
                wh.Cells["D52"].Value = DO6bl1_combo.SelectedItem.ToString();
                wh.Cells["E52"].Value = int.Parse(DO6bl1_lab.Text);
            }
            if (DO7bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO7
            {
                wh.Cells["D53"].Value = DO7bl1_combo.SelectedItem.ToString();
                wh.Cells["E53"].Value = int.Parse(DO7bl1_lab.Text);
            }
            if (DO8bl1_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO8
            {
                wh.Cells["D54"].Value = DO8bl1_combo.SelectedItem.ToString();
                wh.Cells["E54"].Value = int.Parse(DO8bl1_lab.Text);
            }
        }

        ///<summary>Формирование для блока расширения 2</summary>
        private void LoadtoExl_Ex2(ExcelWorksheet wh)
        {
            // UI сигналы для блока расширения 2
            if (UI1bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI1
            {
                wh.Cells["D56"].Value = UI1bl2_combo.SelectedItem.ToString();
                wh.Cells["E56"].Value = int.Parse(UI1bl2_lab.Text);
                wh.Cells["F56"].Value = UI1bl2_typeCombo.SelectedItem.ToString();
            }
            if (UI2bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI2
            {
                wh.Cells["D57"].Value = UI2bl2_combo.SelectedItem.ToString();
                wh.Cells["E57"].Value = int.Parse(UI2bl2_lab.Text);
                wh.Cells["F57"].Value = UI2bl2_typeCombo.SelectedItem.ToString();
            }
            if (UI3bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI3
            {
                wh.Cells["D58"].Value = UI3bl2_combo.SelectedItem.ToString();
                wh.Cells["E58"].Value = int.Parse(UI3bl2_lab.Text);
                wh.Cells["F58"].Value = UI3bl2_typeCombo.SelectedItem.ToString();
            }
            if (UI4bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI4
            {
                wh.Cells["D59"].Value = UI4bl2_combo.SelectedItem.ToString();
                wh.Cells["E59"].Value = int.Parse(UI4bl2_lab.Text);
                wh.Cells["F59"].Value = UI4bl2_typeCombo.SelectedItem.ToString();
            }
            if (UI5bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI5
            {
                wh.Cells["D60"].Value = UI5bl2_combo.SelectedItem.ToString();
                wh.Cells["E60"].Value = int.Parse(UI5bl2_lab.Text);
                wh.Cells["F60"].Value = UI5bl2_typeCombo.SelectedItem.ToString();
            }
            if (UI6bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI6
            {
                wh.Cells["D61"].Value = UI6bl2_combo.SelectedItem.ToString();
                wh.Cells["E61"].Value = int.Parse(UI6bl2_lab.Text);
                wh.Cells["F61"].Value = UI6bl2_typeCombo.SelectedItem.ToString();
            }
            if (UI7bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI7
            {
                wh.Cells["D62"].Value = UI7bl2_combo.SelectedItem.ToString();
                wh.Cells["E62"].Value = int.Parse(UI7bl2_lab.Text);
                wh.Cells["F62"].Value = UI7bl2_typeCombo.SelectedItem.ToString();
            }
            if (UI8bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI8
            {
                wh.Cells["D63"].Value = UI8bl2_combo.SelectedItem.ToString();
                wh.Cells["E63"].Value = int.Parse(UI8bl2_lab.Text);
                wh.Cells["F63"].Value = UI8bl2_typeCombo.SelectedItem.ToString();
            }
            if (UI9bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI9
            {
                wh.Cells["D64"].Value = UI9bl2_combo.SelectedItem.ToString();
                wh.Cells["E64"].Value = int.Parse(UI9bl2_lab.Text);
                wh.Cells["F64"].Value = UI9bl2_typeCombo.SelectedItem.ToString();
            }
            if (UI10bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI10
            {
                wh.Cells["D65"].Value = UI10bl2_combo.SelectedItem.ToString();
                wh.Cells["E65"].Value = int.Parse(UI10bl2_lab.Text);
                wh.Cells["F65"].Value = UI10bl2_typeCombo.SelectedItem.ToString();
            }
            if (UI11bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI11
            {
                wh.Cells["D66"].Value = UI11bl2_combo.SelectedItem.ToString();
                wh.Cells["E66"].Value = int.Parse(UI11bl2_lab.Text);
                wh.Cells["F66"].Value = UI11bl2_typeCombo.SelectedItem.ToString();
            }
            if (UI12bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI12
            {
                wh.Cells["D67"].Value = UI12bl2_combo.SelectedItem.ToString();
                wh.Cells["E67"].Value = int.Parse(UI12bl2_lab.Text);
                wh.Cells["F67"].Value = UI12bl2_typeCombo.SelectedItem.ToString();
            }
            if (UI13bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI13
            {
                wh.Cells["D68"].Value = UI13bl2_combo.SelectedItem.ToString();
                wh.Cells["E68"].Value = int.Parse(UI13bl2_lab.Text);
                wh.Cells["F68"].Value = UI13bl2_typeCombo.SelectedItem.ToString();
            }
            if (UI14bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI14
            {
                wh.Cells["D69"].Value = UI14bl2_combo.SelectedItem.ToString();
                wh.Cells["E69"].Value = int.Parse(UI14bl2_lab.Text);
                wh.Cells["F69"].Value = UI14bl2_typeCombo.SelectedItem.ToString();
            }
            if (UI15bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI15
            {
                wh.Cells["D70"].Value = UI15bl2_combo.SelectedItem.ToString();
                wh.Cells["E70"].Value = int.Parse(UI15bl2_lab.Text);
                wh.Cells["F70"].Value = UI15bl2_typeCombo.SelectedItem.ToString();
            }
            if (UI16bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI16
            {
                wh.Cells["D71"].Value = UI16bl2_combo.SelectedItem.ToString();
                wh.Cells["E71"].Value = int.Parse(UI16bl2_lab.Text);
                wh.Cells["F71"].Value = UI16bl2_typeCombo.SelectedItem.ToString();
            }
            // AO сигналы для блока расширения 1
            if (AO1bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // AO1
            {
                wh.Cells["D73"].Value = AO1bl2_combo.SelectedItem.ToString();
                wh.Cells["E73"].Value = int.Parse(AO1bl2_lab.Text);
            }
            if (AO2bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // AO2
            {
                wh.Cells["D74"].Value = AO2bl2_combo.SelectedItem.ToString();
                wh.Cells["E74"].Value = int.Parse(AO2bl2_lab.Text);
            }
            // DO сигналы для блока расширения 1
            if (DO1bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO1
            {
                wh.Cells["D76"].Value = DO1bl2_combo.SelectedItem.ToString();
                wh.Cells["E76"].Value = int.Parse(DO1bl2_lab.Text);
            }
            if (DO2bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO2
            {
                wh.Cells["D77"].Value = DO2bl2_combo.SelectedItem.ToString();
                wh.Cells["E77"].Value = int.Parse(DO2bl2_lab.Text);
            }
            if (DO3bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO3
            {
                wh.Cells["D78"].Value = DO3bl2_combo.SelectedItem.ToString();
                wh.Cells["E78"].Value = int.Parse(DO3bl2_lab.Text);
            }
            if (DO4bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO4
            {
                wh.Cells["D79"].Value = DO4bl2_combo.SelectedItem.ToString();
                wh.Cells["E79"].Value = int.Parse(DO4bl2_lab.Text);
            }
            if (DO5bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO5
            {
                wh.Cells["D80"].Value = DO5bl2_combo.SelectedItem.ToString();
                wh.Cells["E80"].Value = int.Parse(DO5bl2_lab.Text);
            }
            if (DO6bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO6
            {
                wh.Cells["D81"].Value = DO6bl2_combo.SelectedItem.ToString();
                wh.Cells["E81"].Value = int.Parse(DO6bl2_lab.Text);
            }
            if (DO7bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO7
            {
                wh.Cells["D82"].Value = DO7bl2_combo.SelectedItem.ToString();
                wh.Cells["E82"].Value = int.Parse(DO7bl2_lab.Text);
            }
            if (DO8bl2_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO8
            {
                wh.Cells["D83"].Value = DO8bl2_combo.SelectedItem.ToString();
                wh.Cells["E83"].Value = int.Parse(DO8bl2_lab.Text);
            }
        }

        ///<summary>Формирование для блока расширения 3</summary>
        private void LoadtoExl_Ex3(ExcelWorksheet wh)
        {
            // UI сигналы для блока расширения 2
            if (UI1bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI1
            {
                wh.Cells["D85"].Value = UI1bl3_combo.SelectedItem.ToString();
                wh.Cells["E85"].Value = int.Parse(UI1bl3_lab.Text);
                wh.Cells["F85"].Value = UI1bl3_typeCombo.SelectedItem.ToString();
            }
            if (UI2bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI2
            {
                wh.Cells["D86"].Value = UI2bl3_combo.SelectedItem.ToString();
                wh.Cells["E86"].Value = int.Parse(UI2bl3_lab.Text);
                wh.Cells["F86"].Value = UI2bl3_typeCombo.SelectedItem.ToString();
            }
            if (UI3bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI3
            {
                wh.Cells["D87"].Value = UI3bl3_combo.SelectedItem.ToString();
                wh.Cells["E87"].Value = int.Parse(UI3bl3_lab.Text);
                wh.Cells["F87"].Value = UI3bl3_typeCombo.SelectedItem.ToString();
            }
            if (UI4bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI4
            {
                wh.Cells["D88"].Value = UI4bl3_combo.SelectedItem.ToString();
                wh.Cells["E88"].Value = int.Parse(UI4bl3_lab.Text);
                wh.Cells["F88"].Value = UI4bl3_typeCombo.SelectedItem.ToString();
            }
            if (UI5bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI5
            {
                wh.Cells["D89"].Value = UI5bl3_combo.SelectedItem.ToString();
                wh.Cells["E89"].Value = int.Parse(UI5bl3_lab.Text);
                wh.Cells["F89"].Value = UI5bl3_typeCombo.SelectedItem.ToString();
            }
            if (UI6bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI6
            {
                wh.Cells["D90"].Value = UI6bl3_combo.SelectedItem.ToString();
                wh.Cells["E90"].Value = int.Parse(UI6bl3_lab.Text);
                wh.Cells["F90"].Value = UI6bl3_typeCombo.SelectedItem.ToString();
            }
            if (UI7bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI7
            {
                wh.Cells["D91"].Value = UI7bl3_combo.SelectedItem.ToString();
                wh.Cells["E91"].Value = int.Parse(UI7bl3_lab.Text);
                wh.Cells["F91"].Value = UI7bl3_typeCombo.SelectedItem.ToString();
            }
            if (UI8bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI8
            {
                wh.Cells["D92"].Value = UI8bl3_combo.SelectedItem.ToString();
                wh.Cells["E92"].Value = int.Parse(UI8bl3_lab.Text);
                wh.Cells["F92"].Value = UI8bl3_typeCombo.SelectedItem.ToString();
            }
            if (UI9bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI9
            {
                wh.Cells["D93"].Value = UI9bl3_combo.SelectedItem.ToString();
                wh.Cells["E93"].Value = int.Parse(UI9bl3_lab.Text);
                wh.Cells["F93"].Value = UI9bl3_typeCombo.SelectedItem.ToString();
            }
            if (UI10bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI10
            {
                wh.Cells["D94"].Value = UI10bl3_combo.SelectedItem.ToString();
                wh.Cells["E94"].Value = int.Parse(UI10bl3_lab.Text);
                wh.Cells["F94"].Value = UI10bl3_typeCombo.SelectedItem.ToString();
            }
            if (UI11bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI11
            {
                wh.Cells["D95"].Value = UI11bl3_combo.SelectedItem.ToString();
                wh.Cells["E95"].Value = int.Parse(UI11bl3_lab.Text);
                wh.Cells["F95"].Value = UI11bl3_typeCombo.SelectedItem.ToString();
            }
            if (UI12bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI12
            {
                wh.Cells["D96"].Value = UI12bl3_combo.SelectedItem.ToString();
                wh.Cells["E96"].Value = int.Parse(UI12bl3_lab.Text);
                wh.Cells["F96"].Value = UI12bl3_typeCombo.SelectedItem.ToString();
            }
            if (UI13bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI13
            {
                wh.Cells["D97"].Value = UI13bl3_combo.SelectedItem.ToString();
                wh.Cells["E97"].Value = int.Parse(UI13bl3_lab.Text);
                wh.Cells["F97"].Value = UI13bl3_typeCombo.SelectedItem.ToString();
            }
            if (UI14bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI14
            {
                wh.Cells["D98"].Value = UI14bl3_combo.SelectedItem.ToString();
                wh.Cells["E98"].Value = int.Parse(UI14bl3_lab.Text);
                wh.Cells["F98"].Value = UI14bl3_typeCombo.SelectedItem.ToString();
            }
            if (UI15bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI15
            {
                wh.Cells["D99"].Value = UI15bl3_combo.SelectedItem.ToString();
                wh.Cells["E99"].Value = int.Parse(UI15bl3_lab.Text);
                wh.Cells["F99"].Value = UI15bl3_typeCombo.SelectedItem.ToString();
            }
            if (UI16bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // UI16
            {
                wh.Cells["D100"].Value = UI16bl3_combo.SelectedItem.ToString();
                wh.Cells["E100"].Value = int.Parse(UI16bl3_lab.Text);
                wh.Cells["F100"].Value = UI16bl3_typeCombo.SelectedItem.ToString();
            }
            // AO сигналы для блока расширения 1
            if (AO1bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // AO1
            {
                wh.Cells["D102"].Value = AO1bl3_combo.SelectedItem.ToString();
                wh.Cells["E102"].Value = int.Parse(AO1bl3_lab.Text);
            }
            if (AO2bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // AO2
            {
                wh.Cells["D103"].Value = AO2bl3_combo.SelectedItem.ToString();
                wh.Cells["E103"].Value = int.Parse(AO2bl3_lab.Text);
            }
            // DO сигналы для блока расширения 1
            if (DO1bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO1
            {
                wh.Cells["D105"].Value = DO1bl3_combo.SelectedItem.ToString();
                wh.Cells["E105"].Value = int.Parse(DO1bl3_lab.Text);
            }
            if (DO2bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO2
            {
                wh.Cells["D106"].Value = DO2bl3_combo.SelectedItem.ToString();
                wh.Cells["E106"].Value = int.Parse(DO2bl3_lab.Text);
            }
            if (DO3bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO3
            {
                wh.Cells["D107"].Value = DO3bl3_combo.SelectedItem.ToString();
                wh.Cells["E107"].Value = int.Parse(DO3bl3_lab.Text);
            }
            if (DO4bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO4
            {
                wh.Cells["D108"].Value = DO4bl3_combo.SelectedItem.ToString();
                wh.Cells["E108"].Value = int.Parse(DO4bl3_lab.Text);
            }
            if (DO5bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO5
            {
                wh.Cells["D109"].Value = DO5bl3_combo.SelectedItem.ToString();
                wh.Cells["E109"].Value = int.Parse(DO5bl3_lab.Text);
            }
            if (DO6bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO6
            {
                wh.Cells["D110"].Value = DO6bl3_combo.SelectedItem.ToString();
                wh.Cells["E110"].Value = int.Parse(DO6bl3_lab.Text);
            }
            if (DO7bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO7
            {
                wh.Cells["D111"].Value = DO7bl3_combo.SelectedItem.ToString();
                wh.Cells["E111"].Value = int.Parse(DO7bl3_lab.Text);
            }
            if (DO8bl3_combo.SelectedItem.ToString() != NOT_SELECTED)           // DO8
            {
                wh.Cells["D112"].Value = DO8bl3_combo.SelectedItem.ToString();
                wh.Cells["E112"].Value = int.Parse(DO8bl3_lab.Text);
            }
        }
    }
}