using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;

// Файл для сохранения/загрузки параметров программы в формате JSON

namespace Moderon
{
    /*
    ///<summary>Состояние для всех checkBox программы</summary> 
    class CheckBoxState
    {
        public string name; // Название checkBox
        public bool state; // Состояние checked 
        public CheckBoxState(string name, bool state)
        {
            this.name = name; this.state = state;
        }
    }

    ///<summary>Состояние для всех comboBox элементов программы</summary> 
    class ComboBoxState
    {
        public string name; // Название comboBox
        public int index; // Выбранный индекс для comboBox
        public ComboBoxState(string name, int index)
        {
            this.name = name; this.index = index;
        }
    }

    ///<summary>Состояние для всех text элементов программы</summary>
    class TextElemState
    {
        public string name; // Название textBox
        public string text; // Содержимое текстового поля
        public TextElemState(string name, string text)
        {
            this.name = name; this.text = text;
        }
    }

    ///<summary>Коллекция элементов для comboBox</summary>
    class ItemsComboBox
    {
        public string name; // Название comboBox
        public ComboBox.ObjectCollection items;
        public ItemsComboBox(string name, ComboBox.ObjectCollection items)
        {
            this.name = name; this.items = items;
        }
    } */

    class JsonObject
    {
        [JsonProperty("checkBoxState")]
        public Dictionary<string, bool> checkBoxState { get; set; }                 // Состояние всех checkBox

        [JsonProperty("comboBoxElemState")]
        public Dictionary<string, int> comboBoxElemState { get; set; }              // Состояние всех comboBox элементов

        [JsonProperty("textBoxElemState")]
        public Dictionary<string, string> textBoxElemState { get; set; }            // Значение для textBox элементов

        [JsonProperty("labelSignalsState")]
        public Dictionary<string, string> labelSignalsState { get; set; }           // Значение для label таблицы сигналов

        [JsonProperty("comboSignalsItems")]
        public Dictionary<string, string[]> comboSignalsItems { get; set; }         // Элементы для comboBox таблицы сигналов

        [JsonProperty("comboSignalsState")]
        public Dictionary<string, string> comboSignalsState { get; set; }           // Значение для comboBox таблицы сигналов

        [JsonProperty("uiCode")]
        public Dictionary<string, ushort> uiCode { get; set; }                      // Словарь для кодов сигналов UI

        [JsonProperty("uiType")]
        public Dictionary<string, string> uiType { get; set; }                      // Словарь для типов сигналов UI

        [JsonProperty("uiActive")]
        public Dictionary<string, bool> uiActive { get; set; }                      // Словарь для активности сигналов UI

        [JsonProperty("aoCode")]
        public Dictionary<string, ushort> aoCode { get; set; }                      // Словарь для кодов сигналов AO

        [JsonProperty("aoActive")]
        public Dictionary<string, bool> aoActive { get; set; }                      // Словарь для активности сигналов AO

        [JsonProperty("doCode")]
        public Dictionary<string, ushort> doCode { get; set; }                      // Словарь для кодов сигналов DO

        [JsonProperty("doActive")]
        public Dictionary<string, bool> doActive { get; set; }                      // Словарь для активности сигналов DO

        [JsonProperty("uiTypeEnable")]
        public Dictionary<string, bool> uiTypeEnable { get; set; }                  // Словарь для доступности типа UI сигналов

        [JsonProperty("comboIndex")]
        public Dictionary<string, int> comboIndex { get; set; }                     // Словарь для сохранения выбранного ранее индекса comboBox

        [JsonProperty("comboText")]
        public Dictionary<string, string> comboText { get; set; }                   // Словарь для сохранения текста выбранного сигнала comboBox

        [JsonProperty("expBlocks")]                                                 
        public Dictionary<string, int> expBlocks { get; set; }                      // Словарь для сохранения количества блоков расширения

        [JsonProperty("plkType")]                                                   // Опция для сохранения типа выбранного ПЛК
        public Dictionary<string, int> plkType { get; set; }

        [JsonConstructor]
        public JsonObject(){
            checkBoxState = new Dictionary<string, bool>();
            comboBoxElemState = new Dictionary<string, int>();
            textBoxElemState = new Dictionary<string, string>();
            labelSignalsState = new Dictionary<string, string>();
            comboSignalsItems = new Dictionary<string, string[]>();
            comboSignalsState = new Dictionary<string, string>();
            uiCode = new Dictionary<string, ushort>();
            uiType = new Dictionary<string, string>();
            uiActive = new Dictionary<string, bool>();
            aoCode = new Dictionary<string, ushort>();
            aoActive = new Dictionary<string, bool>();
            uiTypeEnable = new Dictionary<string, bool>();
            doCode = new Dictionary<string, ushort>();
            doActive = new Dictionary<string, bool>();
            comboIndex = new Dictionary<string, int>();
            comboText = new Dictionary<string, string>();
            expBlocks = new Dictionary<string, int>();
            plkType = new Dictionary<string, int>();
        }
    }

    public partial class Form1 : Form
    {
        JsonObject json;                                // Объект для сохранения файла
        JsonObject json_read;                           // Объект для загрузки файла
        bool ignoreEvents = false;                      // Игнорирование событий для элементов

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
            BuildExpBlocks();                           // Сохранение количества и типов блоков расширения
            Build_UITypeEnable();                       // Сохранение доступности типов UI сигналов
            BuildPlkType();                             // Сохранение типа выбранного контроллера
            SaveJsonFile();                             // Сохранение файла JSON  
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
                json.uiTypeEnable.Add(el.Name, el.Enabled);
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

            foreach (var el in texts) json.comboText.Add(el.Key, el.Value);
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

            foreach (var el in indexes) json.comboIndex.Add(el.Key, el.Value);
        }

        ///<summary>Сохранение выбранного типа ПЛК</summary>
        private void BuildPlkType()
        {
            json.plkType.Add("comboPlkType", comboPlkType.SelectedIndex);
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
                    json.expBlocks.Add(el.Name, currentBlocks[el]);
        }

        ///<summary>Перенос перечня сигналов, массивы UI, AO, DO</summary>
        private void BuildSignalArrays()
        {
            // Значения полей для сигналов UI
            for (int i = 0; i < list_ui.Count; i++)
            {
                json.uiCode.Add(list_ui[i].Name, list_ui[i].Code);
                json.uiType.Add(list_ui[i].Name, list_ui[i].Type);
                json.uiActive.Add(list_ui[i].Name, list_ui[i].Active);
            }

            // Значения полей для сигналов AO
            for (int i = 0; i < list_ao.Count; i++)
            {
                json.aoCode.Add(list_ao[i].Name, list_ao[i].Code);
                json.aoActive.Add(list_ao[i].Name, list_ao[i].Active);
            }

            // Значения полей для сигналов DO
            for (int i = 0; i < list_do.Count; i++)
            {
                json.doCode.Add(list_do[i].Name, list_do[i].Code);
                json.doActive.Add(list_do[i].Name, list_do[i].Active);
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
                System.IO.File.WriteAllBytes(dlg.FileName, System.IO.File.ReadAllBytes(tempPath));
        }

        ///<summary>Нажали "Загрузить" в главном меню</summary> 
        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string
                MESSAGE = "Загрузка сбросит текущую конфигурацию. Вы уверены?",
                CAPTION = "Загрузка файла";

            var result = MessageBox.Show(MESSAGE, CAPTION, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No) return;

            ResetButton_Click(sender, e);                           // Первоначальный сброс перед загрузкой файла
            LoadJsonFile();                                         // Загрузка файла JSON в программу
            ignoreEvents = true;                                    // Временное отключение событий
            LoadCheckBoxAll();                                      // Загрузка для всех сheckBox
            LoadComboBoxElemAll();                                  // Загрузка для всех comboBox элементов
            LoadTextBoxAll();                                       // Загрузка для всех textBox элементов
            LoadLabelSignalsAll();                                  // Загрузка для подписей кодов таблицы сигналов
            LoadComboItemsSignals();                                // Загрузка элементов для comboBox таблицы сигналов
            LoadComboSignalsAll();                                  // Загрузка состояний для comboBox таблицы сигналов
            LoadSignalArrays();                                     // Загрузка для массива сигналов
            LoadExpBlocks();                                        // Загрузка количества и типов блоков расширения
            LoadComboIndex();                                       // Загрузка выбранного ранее индекса для comboBox сигналов
            LoadComboText();                                        // Загрузка выбранного ранее названия для comboBox сигналов
            Load_UITypeEnable();                                    // Загрузка типов для UI сигналов
            LoadPlkType();                                          // Загрузка ранее выбранного типа контроллера
            CheckPanelBlocks(CalcExpBlocks_typeNums());             // Проверка отображения панели блоков расширения Form1, тип и количество
            ignoreEvents = false;                                   // Возврат активации событий
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
                ui_combos_type[i].Enabled = json_read.uiTypeEnable[ui_combos_type[i].Name];
        }

        ///<summary>Загрузка выбранного ранее типа контроллера</summary>
        private void LoadPlkType()
        {
            comboPlkType.SelectedIndex = json_read.plkType["comboPlkType"];
        }

        ///<summary>Загрузка выбранного ранее индекса для comboBox сигналов</summary>
        private void LoadComboIndex()
        {
            // AO сигналы ПЛК
            AO1combo_index = json_read.comboIndex["AO1combo_index"]; 
            AO2combo_index = json_read.comboIndex["AO2combo_index"];
            AO3combo_index = json_read.comboIndex["AO3combo_index"]; 
            // AO сигналы блок расширения 1
            AO1bl1combo_index = json_read.comboIndex["AO1bl1combo_index"];
            AO2bl1combo_index = json_read.comboIndex["AO2bl1combo_index"];
            // AO сигналы блок расширения 2
            AO1bl2combo_index = json_read.comboIndex["AO1bl2combo_index"];
            AO2bl2combo_index = json_read.comboIndex["AO2bl2combo_index"];
            // AO сигналы блок расширения 3
            AO1bl3combo_index = json_read.comboIndex["AO1bl3combo_index"];
            AO2bl3combo_index = json_read.comboIndex["AO2bl3combo_index"];
            // DO сигналы ПЛК
            DO1combo_index = json_read.comboIndex["DO1combo_index"];
            DO2combo_index = json_read.comboIndex["DO1combo_index"];
            DO3combo_index = json_read.comboIndex["DO1combo_index"];
            DO4combo_index = json_read.comboIndex["DO1combo_index"];
            DO5combo_index = json_read.comboIndex["DO1combo_index"];
            DO6combo_index = json_read.comboIndex["DO1combo_index"];
            // DO сигналы блок расширения 1
            DO1bl1combo_index = json_read.comboIndex["DO1bl1combo_index"];
            DO2bl1combo_index = json_read.comboIndex["DO2bl1combo_index"];
            DO3bl1combo_index = json_read.comboIndex["DO3bl1combo_index"];
            DO4bl1combo_index = json_read.comboIndex["DO4bl1combo_index"];
            DO5bl1combo_index = json_read.comboIndex["DO5bl1combo_index"];
            DO6bl1combo_index = json_read.comboIndex["DO6bl1combo_index"];
            DO7bl1combo_index = json_read.comboIndex["DO7bl1combo_index"];
            DO8bl1combo_index = json_read.comboIndex["DO8bl1combo_index"];
            // DO сигналы блок расширения 2
            DO1bl2combo_index = json_read.comboIndex["DO1bl2combo_index"];
            DO2bl2combo_index = json_read.comboIndex["DO2bl2combo_index"];
            DO3bl2combo_index = json_read.comboIndex["DO3bl2combo_index"];
            DO4bl2combo_index = json_read.comboIndex["DO4bl2combo_index"];
            DO5bl2combo_index = json_read.comboIndex["DO5bl2combo_index"];
            DO6bl2combo_index = json_read.comboIndex["DO6bl2combo_index"];
            DO7bl2combo_index = json_read.comboIndex["DO7bl2combo_index"];
            DO8bl2combo_index = json_read.comboIndex["DO8bl2combo_index"];
            // DO сигналы блок расширения 3
            DO1bl3combo_index = json_read.comboIndex["DO1bl3combo_index"];
            DO2bl3combo_index = json_read.comboIndex["DO2bl3combo_index"];
            DO3bl3combo_index = json_read.comboIndex["DO3bl3combo_index"];
            DO4bl3combo_index = json_read.comboIndex["DO4bl3combo_index"];
            DO5bl3combo_index = json_read.comboIndex["DO5bl3combo_index"];
            DO6bl3combo_index = json_read.comboIndex["DO6bl3combo_index"];
            DO7bl3combo_index = json_read.comboIndex["DO7bl3combo_index"];
            DO8bl3combo_index = json_read.comboIndex["DO8bl3combo_index"];
            // UI сигналы ПЛК
            UI1combo_index = json_read.comboIndex["UI1combo_index"];
            UI2combo_index = json_read.comboIndex["UI2combo_index"];
            UI3combo_index = json_read.comboIndex["UI3combo_index"];
            UI4combo_index = json_read.comboIndex["UI4combo_index"];
            UI5combo_index = json_read.comboIndex["UI5combo_index"];
            UI6combo_index = json_read.comboIndex["UI6combo_index"];
            UI7combo_index = json_read.comboIndex["UI7combo_index"];
            UI8combo_index = json_read.comboIndex["UI8combo_index"];
            UI9combo_index = json_read.comboIndex["UI9combo_index"];
            UI10combo_index = json_read.comboIndex["UI10combo_index"];
            UI11combo_index = json_read.comboIndex["UI11combo_index"];
            // UI сигналы блок расширения 1
            UI1bl1combo_index = json_read.comboIndex["UI1bl1combo_index"];
            UI2bl1combo_index = json_read.comboIndex["UI2bl1combo_index"];
            UI3bl1combo_index = json_read.comboIndex["UI3bl1combo_index"];
            UI4bl1combo_index = json_read.comboIndex["UI4bl1combo_index"];
            UI5bl1combo_index = json_read.comboIndex["UI5bl1combo_index"];
            UI6bl1combo_index = json_read.comboIndex["UI6bl1combo_index"];
            UI7bl1combo_index = json_read.comboIndex["UI7bl1combo_index"];
            UI8bl1combo_index = json_read.comboIndex["UI8bl1combo_index"];
            UI9bl1combo_index = json_read.comboIndex["UI9bl1combo_index"];
            UI10bl1combo_index = json_read.comboIndex["UI10bl1combo_index"];
            UI11bl1combo_index = json_read.comboIndex["UI11bl1combo_index"];
            UI12bl1combo_index = json_read.comboIndex["UI12bl1combo_index"];
            UI13bl1combo_index = json_read.comboIndex["UI13bl1combo_index"];
            UI14bl1combo_index = json_read.comboIndex["UI14bl1combo_index"];
            UI15bl1combo_index = json_read.comboIndex["UI15bl1combo_index"];
            UI16bl1combo_index = json_read.comboIndex["UI16bl1combo_index"];
            // UI сигналы блок расширения 2
            UI1bl2combo_index = json_read.comboIndex["UI1bl2combo_index"];
            UI2bl2combo_index = json_read.comboIndex["UI2bl2combo_index"];
            UI3bl2combo_index = json_read.comboIndex["UI3bl2combo_index"];
            UI4bl2combo_index = json_read.comboIndex["UI4bl2combo_index"];
            UI5bl2combo_index = json_read.comboIndex["UI5bl2combo_index"];
            UI6bl2combo_index = json_read.comboIndex["UI6bl2combo_index"];
            UI7bl2combo_index = json_read.comboIndex["UI7bl2combo_index"];
            UI8bl2combo_index = json_read.comboIndex["UI8bl2combo_index"];
            UI9bl2combo_index = json_read.comboIndex["UI9bl2combo_index"];
            UI10bl2combo_index = json_read.comboIndex["UI10bl2combo_index"];
            UI11bl2combo_index = json_read.comboIndex["UI11bl2combo_index"];
            UI12bl2combo_index = json_read.comboIndex["UI12bl2combo_index"];
            UI13bl2combo_index = json_read.comboIndex["UI13bl2combo_index"];
            UI14bl2combo_index = json_read.comboIndex["UI14bl2combo_index"];
            UI15bl2combo_index = json_read.comboIndex["UI15bl2combo_index"];
            UI16bl2combo_index = json_read.comboIndex["UI16bl2combo_index"];
            // UI сигналы блок расширения 3
            UI1bl3combo_index = json_read.comboIndex["UI1bl3combo_index"];
            UI2bl3combo_index = json_read.comboIndex["UI2bl3combo_index"];
            UI3bl3combo_index = json_read.comboIndex["UI3bl3combo_index"];
            UI4bl3combo_index = json_read.comboIndex["UI4bl3combo_index"];
            UI5bl3combo_index = json_read.comboIndex["UI5bl3combo_index"];
            UI6bl3combo_index = json_read.comboIndex["UI6bl3combo_index"];
            UI7bl3combo_index = json_read.comboIndex["UI7bl3combo_index"];
            UI8bl3combo_index = json_read.comboIndex["UI8bl3combo_index"];
            UI9bl3combo_index = json_read.comboIndex["UI9bl3combo_index"];
            UI10bl3combo_index = json_read.comboIndex["UI10bl3combo_index"];
            UI11bl3combo_index = json_read.comboIndex["UI11bl3combo_index"];
            UI12bl3combo_index = json_read.comboIndex["UI12bl3combo_index"];
            UI13bl3combo_index = json_read.comboIndex["UI13bl3combo_index"];
            UI14bl3combo_index = json_read.comboIndex["UI14bl3combo_index"];
            UI15bl3combo_index = json_read.comboIndex["UI15bl3combo_index"];
            UI16bl3combo_index = json_read.comboIndex["UI16bl3combo_index"];
        }

        ///<summary>Загрузка выбранного ранее названия для comboBox сигналов</summary>
        private void LoadComboText()
        {   
            // AO сигналы ПЛК
            AO1combo_text = json_read.comboText["AO1combo_text"];
            AO2combo_text = json_read.comboText["AO2combo_text"];
            AO3combo_text = json_read.comboText["AO3combo_text"];
            // AO сигналы блок расширения 1
            AO1bl1combo_text = json_read.comboText["AO1bl1combo_text"];
            AO2bl1combo_text = json_read.comboText["AO2bl1combo_text"];
            // AO сигналы блок расширения 2
            AO1bl2combo_text = json_read.comboText["AO1bl2combo_text"];
            AO2bl2combo_text = json_read.comboText["AO2bl2combo_text"];
            // AO сигналы блок расширения 3
            AO1bl3combo_text = json_read.comboText["AO1bl3combo_text"];
            AO2bl3combo_text = json_read.comboText["AO2bl3combo_text"];
            // DO сигналы ПЛК
            DO1combo_text = json_read.comboText["DO1combo_text"];
            DO2combo_text = json_read.comboText["DO2combo_text"];
            DO3combo_text = json_read.comboText["DO3combo_text"];
            DO4combo_text = json_read.comboText["DO4combo_text"];
            DO5combo_text = json_read.comboText["DO5combo_text"];
            DO6combo_text = json_read.comboText["DO6combo_text"];
            // DO сигналы блок расширения 1
            DO1bl1combo_text = json_read.comboText["DO1bl1combo_text"];
            DO2bl1combo_text = json_read.comboText["DO2bl1combo_text"];
            DO3bl1combo_text = json_read.comboText["DO3bl1combo_text"];
            DO4bl1combo_text = json_read.comboText["DO4bl1combo_text"];
            DO5bl1combo_text = json_read.comboText["DO5bl1combo_text"];
            DO6bl1combo_text = json_read.comboText["DO6bl1combo_text"];
            DO7bl1combo_text = json_read.comboText["DO7bl1combo_text"];
            DO8bl1combo_text = json_read.comboText["DO8bl1combo_text"];
            // DO сигналы блок расширения 2
            DO1bl2combo_text = json_read.comboText["DO1bl2combo_text"];
            DO2bl2combo_text = json_read.comboText["DO2bl2combo_text"];
            DO3bl2combo_text = json_read.comboText["DO3bl2combo_text"];
            DO4bl2combo_text = json_read.comboText["DO4bl2combo_text"];
            DO5bl2combo_text = json_read.comboText["DO5bl2combo_text"];
            DO6bl2combo_text = json_read.comboText["DO6bl2combo_text"];
            DO7bl2combo_text = json_read.comboText["DO7bl2combo_text"];
            DO8bl2combo_text = json_read.comboText["DO8bl2combo_text"];
            // DO сигналы блок расширения 3
            DO1bl3combo_text = json_read.comboText["DO1bl3combo_text"];
            DO2bl3combo_text = json_read.comboText["DO2bl3combo_text"];
            DO3bl3combo_text = json_read.comboText["DO3bl3combo_text"];
            DO4bl3combo_text = json_read.comboText["DO4bl3combo_text"];
            DO5bl3combo_text = json_read.comboText["DO5bl3combo_text"];
            DO6bl3combo_text = json_read.comboText["DO6bl3combo_text"];
            DO7bl3combo_text = json_read.comboText["DO7bl3combo_text"];
            DO8bl3combo_text = json_read.comboText["DO8bl3combo_text"];
            // UI сигналы ПЛК
            UI1combo_text = json_read.comboText["UI1combo_text"];
            UI2combo_text = json_read.comboText["UI2combo_text"];
            UI3combo_text = json_read.comboText["UI3combo_text"];
            UI4combo_text = json_read.comboText["UI4combo_text"];
            UI5combo_text = json_read.comboText["UI5combo_text"];
            UI6combo_text = json_read.comboText["UI6combo_text"];
            UI7combo_text = json_read.comboText["UI7combo_text"];
            UI8combo_text = json_read.comboText["UI8combo_text"];
            UI9combo_text = json_read.comboText["UI9combo_text"];
            UI10combo_text = json_read.comboText["UI10combo_text"];
            UI11combo_text = json_read.comboText["UI11combo_text"];
            // UI сигналы блок расширения 1
            UI1bl1combo_text = json_read.comboText["UI1bl1combo_text"];
            UI2bl1combo_text = json_read.comboText["UI2bl1combo_text"];
            UI3bl1combo_text = json_read.comboText["UI3bl1combo_text"];
            UI4bl1combo_text = json_read.comboText["UI4bl1combo_text"];
            UI5bl1combo_text = json_read.comboText["UI5bl1combo_text"];
            UI6bl1combo_text = json_read.comboText["UI6bl1combo_text"];
            UI7bl1combo_text = json_read.comboText["UI7bl1combo_text"];
            UI8bl1combo_text = json_read.comboText["UI8bl1combo_text"];
            UI9bl1combo_text = json_read.comboText["UI9bl1combo_text"];
            UI10bl1combo_text = json_read.comboText["UI10bl1combo_text"];
            UI11bl1combo_text = json_read.comboText["UI11bl1combo_text"];
            UI12bl1combo_text = json_read.comboText["UI12bl1combo_text"];
            UI13bl1combo_text = json_read.comboText["UI13bl1combo_text"];
            UI14bl1combo_text = json_read.comboText["UI14bl1combo_text"];
            UI15bl1combo_text = json_read.comboText["UI15bl1combo_text"];
            UI16bl1combo_text = json_read.comboText["UI16bl1combo_text"];
            // UI сигналы блок расширения 2
            UI1bl2combo_text = json_read.comboText["UI1bl2combo_text"];
            UI2bl2combo_text = json_read.comboText["UI2bl2combo_text"];
            UI3bl2combo_text = json_read.comboText["UI3bl2combo_text"];
            UI4bl2combo_text = json_read.comboText["UI4bl2combo_text"];
            UI5bl2combo_text = json_read.comboText["UI5bl2combo_text"];
            UI6bl2combo_text = json_read.comboText["UI6bl2combo_text"];
            UI7bl2combo_text = json_read.comboText["UI7bl2combo_text"];
            UI8bl2combo_text = json_read.comboText["UI8bl2combo_text"];
            UI9bl2combo_text = json_read.comboText["UI9bl2combo_text"];
            UI10bl2combo_text = json_read.comboText["UI10bl2combo_text"];
            UI11bl2combo_text = json_read.comboText["UI11bl2combo_text"];
            UI12bl2combo_text = json_read.comboText["UI12bl2combo_text"];
            UI13bl2combo_text = json_read.comboText["UI13bl2combo_text"];
            UI14bl2combo_text = json_read.comboText["UI14bl2combo_text"];
            UI15bl2combo_text = json_read.comboText["UI15bl2combo_text"];
            UI16bl2combo_text = json_read.comboText["UI16bl2combo_text"];
            // UI сигналы блок расширения 3
            UI1bl3combo_text = json_read.comboText["UI1bl3combo_text"];
            UI2bl3combo_text = json_read.comboText["UI2bl3combo_text"];
            UI3bl3combo_text = json_read.comboText["UI3bl3combo_text"];
            UI4bl3combo_text = json_read.comboText["UI4bl3combo_text"];
            UI5bl3combo_text = json_read.comboText["UI5bl3combo_text"];
            UI6bl3combo_text = json_read.comboText["UI6bl3combo_text"];
            UI7bl3combo_text = json_read.comboText["UI7bl3combo_text"];
            UI8bl3combo_text = json_read.comboText["UI8bl3combo_text"];
            UI9bl3combo_text = json_read.comboText["UI9bl3combo_text"];
            UI10bl3combo_text = json_read.comboText["UI10bl3combo_text"];
            UI11bl3combo_text = json_read.comboText["UI11bl3combo_text"];
            UI12bl3combo_text = json_read.comboText["UI12bl3combo_text"];
            UI13bl3combo_text = json_read.comboText["UI13bl3combo_text"];
            UI14bl3combo_text = json_read.comboText["UI14bl3combo_text"];
            UI15bl3combo_text = json_read.comboText["UI15bl3combo_text"];
            UI16bl3combo_text = json_read.comboText["UI16bl3combo_text"];
        }

        ///<summary>Загрузка количества и типов блоков расширения</summary>
        private void LoadExpBlocks()
        {
            Dictionary<string, int> exp_blocks = json_read.expBlocks;

            // Для блока расширения AO
            if (exp_blocks.ContainsKey("M72E12RB"))                         
            {
                AO_block1_panelChanged_M72E12RB();                          // AO панель блок 1
                UI_block1_panelChanged_M72E12RB();                          // UI панель блок 1
                DO_block1_panelChanged_M72E12RB();                          // DO панель блок 1
                expansion_blocks.Add(M72E12RB);

                if (exp_blocks["M72E12RB"] > 1)                             // Два блока расширения AO
                {
                    AO_block2_panelChanged_M72E12RB();                      // AO панель блок 2
                    UI_block2_panelChanged_M72E12RB();                      // UI панель блок 2
                    DO_block2_panelChanged_M72E12RB();                      // DO панель блок 2
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

            // Для блока расширения UI + DO
            if (exp_blocks.ContainsKey("M72E12RA"))                         
            {
                DO_block1_panelChanged_M72E12RA();                          // DO панель блок 1
                UI_block1_panelChanged_M72E12RA();                          // UI панель блок 1
                expansion_blocks.Add(M72E12RA);

                if (exp_blocks["M72E12RA"] > 1)                             // Два блока расширения UI + DO
                {
                    DO_block2_panelChanged_M72E12RA();                      // DO панель блок 2
                    UI_block2_panelChanged_M72E12RA();                      // UI панель блок 2
                    expansion_blocks.Add(M72E12RA);

                    if (exp_blocks["M72E12RA"] > 2)
                    {
                        DO_block3_panelChanged_M72E12RA();                  // DO панель блок 3
                        UI_block3_panelChanged_M72E12RA();                  // UI панель блок 3
                        expansion_blocks.Add(M72E12RA);
                    }
                }
            }

            // Для блока расширения DO
            if (exp_blocks.ContainsKey("M72E08RA"))                         
            {
                DO_block1_panelChanged_M72E08RA();                          // DO панель блок 1
                expansion_blocks.Add(M72E08RA);

                if (exp_blocks["M72E08RA"] > 1)                     
                {
                    DO_block2_panelChanged_M72E08RA();                      // DO панель блок 2
                    expansion_blocks.Add(M72E08RA);

                    if (exp_blocks["M72E08RA"] > 2)
                    {
                        DO_block3_panelChanged_M72E08RA();                  // DO панель блок 3
                        expansion_blocks.Add(M72E08RA);
                    }
                }
            }

            // Для блока расширения UI
            if (exp_blocks.ContainsKey("M72E16NA"))                         
            {
                UI_block1_panelChanged_M72E16NA();                          // UI панель блок 1
                expansion_blocks.Add(M72E16NA);

                if (exp_blocks["M72E16NA"] > 1)
                {
                    UI_block2_panelChanged_M72E16NA();                      // UI панель блок 2
                    expansion_blocks.Add(M72E16NA);

                    if (exp_blocks["M72E16NA"] > 2)
                    {
                        UI_block3_panelChanged_M72E16NA();                  // UI панель блок 3
                        expansion_blocks.Add(M72E16NA);
                    }
                }
            }
        }

        ///<summary>Загрузка Json файла в программу</summary>
        private void LoadJsonFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                openFileDialog.Filter = "Json file (.json)|*.json";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    string message, caption;
                    var filePath = openFileDialog.FileName;
                    using (Stream fileStream = openFileDialog.OpenFile())
                    {
                        try
                        {
                            json_read = JsonConvert.DeserializeObject<JsonObject>(System.IO.File.ReadAllText(filePath));
                            message = "Файл успешно загружен!";
                            caption = "Загрузка файла";
                            result = MessageBox.Show(message, caption, buttons);
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
            }
        }

        ///<summary>Загрузка для всех checkBox</summary>
        private void LoadCheckBoxAll()
        {
            var comboBoxes = new List<CheckBox>()
            {
                // Выбор элементов (боковая панель)
                filterCheck, dampCheck, heaterCheck, addHeatCheck, coolerCheck, humidCheck, recircCheck, recupCheck,
                // Заслонки
                confPrDampCheck, heatPrDampCheck, springRetPrDampCheck, outDampCheck, confOutDampCheck, heatOutDampCheck,
                springRetOutDampCheck,
                // Нагреватель
                TF_heaterCheck, confHeatPumpCheck, pumpCurProtect, reservPumpHeater, confHeatResPumpCheck,
                pumpCurResProtect, watSensHeatCheck,
                // Второй нагреватель
                TF_addHeaterCheck, pumpAddHeatCheck, confAddHeatPumpCheck, pumpCurAddProtect, reservPumpAddHeater,
                confAddHeatResPumpCheck, pumpCurResAddProtect, sensWatAddHeatCheck,
                // Охладитель
                alarmFrCoolCheck, thermoCoolerCheck, thermoCoolerCheck, dehumModeCheck,
                // Увлажнитель, рециркуляция и рекуператор
                alarmHumidCheck, recircPrDampAOCheck, springRetRecircCheck, pumpGlicRecCheck, recDefTempCheck, recDefPsCheck,
                // Датчики и сигналы
                prChanSensCheck, roomTempSensCheck, chanHumSensCheck, roomHumSensCheck, outdoorChanSensCheck, outChanSensCheck,
                sigWorkCheck, sigAlarmCheck, sigFilAlarmCheck, stopStartCheck, fireCheck,
                // Приточный вентилятор
                prFanPSCheck, prFanFC_check, prFanThermoCheck, curDefPrFanCheck, checkResPrFan, prDampFanCheck,
                prDampConfirmFanCheck, prFanAlarmCheck, prFanStStopCheck, prFanSpeedCheck,
                // Вытяжной вентилятор
                outFanPSCheck, outFanFC_check, outFanThermoCheck, curDefOutFanCheck, checkResOutFan, outDampFanCheck,
                outDampConfirmFanCheck, outFanAlarmCheck, outFanStStopCheck, outFanSpeedCheck
            };

            foreach (var el in comboBoxes) el.Checked = json_read.checkBoxState[el.Name];
        }

        ///<summary>Загрузка для всех comboBox элементов</summary>
        private void LoadComboBoxElemAll()
        {
            var comboBoxes = new List<ComboBox>()
            {
                // Выбор типа системы
                comboSysType,
                // Приточный и вытяжной вентилятор
                prFanPowCombo, prFanControlCombo, outFanPowCombo, outFanControlCombo,
                // Воздушные фильтры и заслонки
                filterPrCombo, filterOutCombo, prDampPowCombo, outDampPowCombo,
                // Нагреватель
                heatTypeCombo, powPumpCombo, elHeatStagesCombo, firstStHeatCombo, thermSwitchCombo,
                // Второй нагреватель
                heatAddTypeCombo, powPumpAddCombo, elHeatAddStagesCombo, firstStAddHeatCombo, thermAddSwitchCombo,
                // Охладитель и увлажнитель
                coolTypeCombo, frCoolStagesCombo, powWatCoolCombo, humidTypeCombo,
                // Рециркуляция, рекуператор и датчики
                recircPowCombo, recircPowCombo, rotorPowCombo, bypassPlastCombo, fireTypeCombo
            };

            foreach (var el in comboBoxes) el.SelectedIndex = json_read.comboBoxElemState[el.Name];

            // Блокировка опций для Modbus П и В вентилятора
            if (prFanControlCombo.SelectedIndex == 1)   // Приточный вентилятор
            {
                prFanAlarmCheck.Enabled = false;        // Сигнал аварии
                prFanSpeedCheck.Enabled = false;        // Скорость 0-10 В
            }

            if (outFanControlCombo.SelectedIndex == 1)  // Вытяжной вентилятор
            {
                outFanAlarmCheck.Enabled = false;       // Сигнал аварии
                outFanSpeedCheck.Enabled = false;       // Скорость 0-10 В
            }
        }

        ///<summary>Загрузка для всех textBox</summary>
        private void LoadTextBoxAll()
        {
            var textBoxes = new List<TextBox>()
            {
                // Приточный и вытяжной вентилятор
                powPrFanBox, powPrResFanBox, powOutFanBox, powOutResFanBox,
                // Воздушные заслонки
                b_prDampBox, h_prDampBox, b_outDampBox, h_outDampBox,
                // Нагреватель и второй нагреватель
                elHeatPowBox, elAddHeatPowBox,
                // Рециркуляция и рекуператор
                b_recircBox, h_recircBox, powRotRecBox
            };

            foreach (var el in textBoxes) el.Text = json_read.textBoxElemState[el.Name];
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

            foreach (var el in labels) el.Text = json_read.labelSignalsState[el.Name];
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
                for (int i = 0; i < json_read.comboSignalsItems[el.Name].Length; i++)
                    el.Items.Add(json_read.comboSignalsItems[el.Name][i]);
        }

        ///<summary>Загрузка состояний для comboBox таблицы сигналов</summary>
        private void LoadComboSignalsAll()
        {
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
                UI9bl3_combo, UI10bl3_combo, UI11bl3_combo, UI12bl3_combo, UI13bl3_combo, UI14bl3_combo, UI15bl3_combo, UI16bl3_combo,
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

            for (var i = 0; i < comboBoxes.Count; i++)
                comboBoxes[i].SelectedItem = json_read.comboSignalsState[comboBoxes[i].Name];
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

            foreach (var elem in json_read.uiCode)                              // Загрузка по сигналам UI
            {
                name = elem.Key;
                code = json_read.uiCode[name];
                type = json_read.uiType[name];
                active = json_read.uiActive[name];
                list_ui.Add(new Ui(name, code, type, active));
            }
            foreach (var elem in json_read.aoCode)                              // Загрузка по сигналам AO
            {   
                name = elem.Key;
                code = json_read.aoCode[name];
                active = json_read.aoActive[name];
                list_ao.Add(new Ao(name, code, active));
            }
            foreach (var elem in json_read.doCode)                              // Загрузка по сигналам DO
            {   
                name = elem.Key;
                code = json_read.doCode[name];
                active = json_read.doActive[name];
                list_do.Add(new Do(name, code, active));
            }

            CheckSignalsReady(); // Проверка распределения сигналов после загрузки
        }

        ///<summary>Сборка для всех checkBox программы</summary>
        private void BuildCheckBoxAll()
        {
            var check_boxes = new List<CheckBox>()
            {
                // Выбор элементов (боковая панель)
                filterCheck, dampCheck, heaterCheck, addHeatCheck, coolerCheck, humidCheck, recircCheck, recupCheck,
                // Заслонки
                confPrDampCheck, heatPrDampCheck, springRetPrDampCheck, outDampCheck, confOutDampCheck, heatOutDampCheck, springRetOutDampCheck,
                // Основной нагреватель
                TF_heaterCheck, confHeatPumpCheck, pumpCurProtect, reservPumpHeater, confHeatResPumpCheck, pumpCurResProtect, watSensHeatCheck,
                // Второй нагреватель
                TF_addHeaterCheck, pumpAddHeatCheck, confAddHeatPumpCheck, pumpCurAddProtect, reservPumpAddHeater, 
                confAddHeatResPumpCheck, pumpCurResAddProtect, sensWatAddHeatCheck,
                // Охладитель и увлажнитель
                alarmFrCoolCheck, thermoCoolerCheck, analogFreonCheck, dehumModeCheck, alarmHumidCheck,
                // Рециркуляция и рекуператор
                recircPrDampAOCheck, springRetRecircCheck, pumpGlicRecCheck, recDefTempCheck, recDefPsCheck,
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

            foreach (var el in check_boxes) json.checkBoxState.Add(el.Name, el.Checked);
        }

        ///<summary>Сборка для всех comboBox элементов программы</summary>
        private void BuildComboBoxElemAll()
        {
            var combo_boxes = new List<ComboBox>()
            {
                // Тип системы, приточный и вытяжной вентиляторы
                comboSysType, prFanPowCombo, prFanControlCombo, outFanPowCombo, outFanControlCombo,
                // Воздушные фильтры и заслонки
                filterPrCombo, filterOutCombo, prDampPowCombo, outDampPowCombo,
                // Нагреватель
                heatTypeCombo, powPumpCombo, elHeatStagesCombo, firstStHeatCombo, thermSwitchCombo,
                // Второй нагреватель
                heatAddTypeCombo, powPumpAddCombo, elHeatAddStagesCombo, firstStAddHeatCombo, thermAddSwitchCombo,
                // Охладитель и увлажнитель
                coolTypeCombo, frCoolStagesCombo, powWatCoolCombo, humidTypeCombo,
                // Рециркуляция и рекуператор
                recircPowCombo, recupTypeCombo, rotorPowCombo, bypassPlastCombo, fireTypeCombo
            };

            foreach (var el in combo_boxes) json.comboBoxElemState.Add(el.Name, el.SelectedIndex);
        }

        ///<summary>Сборка для всех textBox элементов программы</summary>
        private void BuildTextBoxAll()
        {
            var text_boxes = new List<TextBox>()
            {
                // Приточный и вытяжной вентилятор
                powPrFanBox, powPrResFanBox, powOutFanBox, powOutResFanBox,
                // Воздушные заслонки
                b_prDampBox, h_prDampBox, b_outDampBox, h_outDampBox,
                // Нагреватель, второй нагреватель, рециркуляция и рекуператор
                elHeatPowBox, elAddHeatPowBox, b_recircBox, h_recircBox, powRotRecBox
            };

            foreach (var el in text_boxes) json.textBoxElemState.Add(el.Name, el.Text);
        }

        ///<summary>Добавление подписи кода таблицы сигналов</summary>
        private void AddLabelSignalsState(Label el) =>
            json.labelSignalsState.Add(el.Name, el.Text);       

        ///<summary>Сборка для подписей кодов таблицы сигналов</summary>
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
            json.comboSignalsState.Add(el.Name, el.SelectedItem.ToString());
        
        ///<summary>Сборка состояний для comboBox таблицы сигналов</summary>
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
            json.comboSignalsItems.Add(el.Name, arr_combo);
        }

        ///<summary>Сборка коллекций элементов для таблицы сигналов</summary>
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
    }
}