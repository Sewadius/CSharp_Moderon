using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;

// Файл для сохранения/загрузки параметров программы в формате JSON

namespace Moderon
{

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
    }

    class JsonObject
    {
        [JsonProperty("checkBoxState")]
        public Dictionary<string, bool> checkBoxState { get; set; } // Состояние всех checkBox
        [JsonProperty("comboBoxElemState")]
        public Dictionary<string, int> comboBoxElemState { get; set; } // Состояние всех comboBox элементов
        [JsonProperty("textBoxElemState")]
        public Dictionary<string, string> textBoxElemState { get; set; } // Значение для textBox элементов
        [JsonProperty("labelSignalsState")]
        public Dictionary<string, string> labelSignalsState { get; set; } // Значение для label таблицы сигналов
        [JsonProperty("comboSignalsItems")]
        public Dictionary<string, string[]> comboSignalsItems { get; set; } // Элементы для comboBox таблицы сигналов
        [JsonProperty("comboAITypeItems")]
        public Dictionary<string, string[]> comboAITypeItems { get; set; } // Элементы для comboBox типов AI сигналов
        [JsonProperty("comboSignalsState")]
        public Dictionary<string, string> comboSignalsState { get; set; } // Значение для comboBox таблицы сигналов
        [JsonProperty("comboAITypeState")]
        public Dictionary<string, string> comboAITypeState { get; set; } // Значение для comboBox типа сигнала AI
        [JsonProperty("aiCode")]
        public Dictionary<string, ushort> aiCode { get; set; } // Словарь для кодов сигналов AI
        [JsonProperty("aiType")]
        public Dictionary<string, string> aiType { get; set; } // Словарь для типов сигналов AI
        [JsonProperty("aiActive")]
        public Dictionary<string, bool> aiActive { get; set; } // Словарь для активности сигналов AI
        [JsonProperty("aoCode")]
        public Dictionary<string, ushort> aoCode { get; set; } // Словарь для кодов сигналов AO
        [JsonProperty("aoActive")]
        public Dictionary<string, bool> aoActive { get; set; } // Словарь для активности сигналов AO
        [JsonProperty("diCode")]
        public Dictionary<string, ushort> diCode { get; set; } // Словарь для кодов сигналов DI
        [JsonProperty("diActive")]
        public Dictionary<string, bool> diActive { get; set; } // Словарь для активности сигналов DI
        [JsonProperty("doCode")]
        public Dictionary<string, ushort> doCode { get; set; } // Словарь для кодов сигналов DO
        [JsonProperty("doActive")]
        public Dictionary<string, bool> doActive { get; set; } // Словарь для активности сигналов DO
        [JsonConstructor]
        public JsonObject(){
            checkBoxState = new Dictionary<string, bool>();
            comboBoxElemState = new Dictionary<string, int>();
            textBoxElemState = new Dictionary<string, string>();
            labelSignalsState = new Dictionary<string, string>();
            comboSignalsItems = new Dictionary<string, string[]>();
            comboAITypeItems = new Dictionary<string, string[]>();
            comboSignalsState = new Dictionary<string, string>();
            comboAITypeState = new Dictionary<string, string>();
            aiCode = new Dictionary<string, ushort>();
            aiType = new Dictionary<string, string>();
            aiActive = new Dictionary<string, bool>();
            aoCode = new Dictionary<string, ushort>();
            aoActive = new Dictionary<string, bool>();
            diCode = new Dictionary<string, ushort>();
            diActive = new Dictionary<string, bool>();
            doCode = new Dictionary<string, ushort>();
            doActive = new Dictionary<string, bool>();
        }
    }

    public partial class Form1 : Form
    {
        JsonObject json; // = new JsonObject(); // Объект для сохранения
        JsonObject json_read; // Объект для загрузки
        bool ignoreEvents = false; // Игнорирование событий для элементов

        ///<summary>Нажали "Сохранить" в главном меню</summary> 
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            json = new JsonObject();
            BuildCheckBoxAll(); // Сохранение для всех элементов checkBox
            BuildComboBoxElemAll(); // Сохранение для всех элементов comboBox
            BuildTextBoxAll(); // Сохранение для всех элементов textBox
            BuildLabelSignalsAll(); // Сохранение для подписей кодов таблицы сигналов
            BuildComboItemsSignals(); // Сохранение элементов comboBox таблицы сигналов
            BuildComboSignalsAll(); // Сохранение выбранного элемента для comboBox таблицы сигналов
            BuildSignalArrays(); // Сохранение для перечня сигналов, массивы
            SaveJsonFile(); // Сохранение файла
        }

        ///<summary>Перенос перечня сигналов, массивы</summary>
        private void BuildSignalArrays()
        {
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
            using (StreamWriter file = File.CreateText(tempPath))
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
            {
                File.WriteAllBytes(dlg.FileName, File.ReadAllBytes(tempPath));
            }
        }

        ///<summary>Нажали "Загрузить" в главном меню</summary> 
        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadJsonFile();                 // Загрузка файла JSON в программу
            ignoreEvents = true;            // Временное отключение событий
            LoadCheckBoxAll();              // Загрузка для всех сheckBox
            LoadComboBoxElemAll();          // Загрузка для всех comboBox элементов
            LoadTextBoxAll();               // Загрузка для всех textBox элементов
            LoadLabelSignalsAll();          // Загрузка для подписей кодов таблицы сигналов
            LoadComboItemsSignals();        // Загрузка элементов для comboBox таблицы сигналов
            LoadComboSignalsAll();          // Загрузка состояний для comboBox таблицы сигналов
            LoadSignalArrays();             // Загрузка для массива сигналов
            ignoreEvents = false;           // Возврат активации событий
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
                            json_read = JsonConvert.DeserializeObject<JsonObject>(File.ReadAllText(filePath));
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
            // Выбор элементов (боковая панель)
            filterCheck.Checked = json_read.checkBoxState[filterCheck.Name];
            dampCheck.Checked = json_read.checkBoxState[dampCheck.Name];
            heaterCheck.Checked = json_read.checkBoxState[heaterCheck.Name];
            addHeatCheck.Checked = json_read.checkBoxState[addHeatCheck.Name];
            coolerCheck.Checked = json_read.checkBoxState[coolerCheck.Name];
            humidCheck.Checked = json_read.checkBoxState[humidCheck.Name];
            recircCheck.Checked = json_read.checkBoxState[recircCheck.Name];
            recupCheck.Checked = json_read.checkBoxState[recupCheck.Name];
            // Заслонки
            confPrDampCheck.Checked = json_read.checkBoxState[confPrDampCheck.Name];
            heatPrDampCheck.Checked = json_read.checkBoxState[heatPrDampCheck.Name];
            springRetPrDampCheck.Checked = json_read.checkBoxState[springRetPrDampCheck.Name];
            outDampCheck.Checked = json_read.checkBoxState[outDampCheck.Name];
            confOutDampCheck.Checked = json_read.checkBoxState[confOutDampCheck.Name];
            heatOutDampCheck.Checked = json_read.checkBoxState[heatOutDampCheck.Name];
            springRetOutDampCheck.Checked = json_read.checkBoxState[springRetOutDampCheck.Name];
            // Нагреватель
            TF_heaterCheck.Checked = json_read.checkBoxState[TF_heaterCheck.Name];
            confHeatPumpCheck.Checked = json_read.checkBoxState[confHeatPumpCheck.Name];
            watSensHeatCheck.Checked = json_read.checkBoxState[watSensHeatCheck.Name];
            // Второй нагреватель
            TF_addHeaterCheck.Checked = json_read.checkBoxState[TF_addHeaterCheck.Name];
            confAddHeatPumpCheck.Checked = json_read.checkBoxState[confAddHeatPumpCheck.Name];
            sensWatAddHeatCheck.Checked = json_read.checkBoxState[sensWatAddHeatCheck.Name];
            pumpAddHeatCheck.Checked = json_read.checkBoxState[pumpAddHeatCheck.Name];
            // Охладитель
            alarmFrCoolCheck.Checked = json_read.checkBoxState[alarmFrCoolCheck.Name];
            thermoCoolerCheck.Checked = json_read.checkBoxState[thermoCoolerCheck.Name];
            analogFreonCheck.Checked = json_read.checkBoxState[analogFreonCheck.Name];
            dehumModeCheck.Checked = json_read.checkBoxState[dehumModeCheck.Name];
            // Увлажнитель
            alarmHumidCheck.Checked = json_read.checkBoxState[alarmHumidCheck.Name];
            // Рециркуляция
            springRetRecircCheck.Checked = json_read.checkBoxState[springRetRecircCheck.Name];
            // Рекуператор
            pumpGlicRecCheck.Checked = json_read.checkBoxState[pumpGlicRecCheck.Name];
            recDefTempCheck.Checked = json_read.checkBoxState[recDefTempCheck.Name];
            recDefPsCheck.Checked = json_read.checkBoxState[recDefPsCheck.Name];
            // Датчики/сигналы
            prChanSensCheck.Checked = json_read.checkBoxState[prChanSensCheck.Name];
            roomTempSensCheck.Checked = json_read.checkBoxState[roomTempSensCheck.Name];
            chanHumSensCheck.Checked = json_read.checkBoxState[chanHumSensCheck.Name];
            roomHumSensCheck.Checked = json_read.checkBoxState[roomHumSensCheck.Name];
            outdoorChanSensCheck.Checked = json_read.checkBoxState[outdoorChanSensCheck.Name];
            outChanSensCheck.Checked = json_read.checkBoxState[outChanSensCheck.Name];
            sigWorkCheck.Checked = json_read.checkBoxState[sigWorkCheck.Name];
            sigAlarmCheck.Checked = json_read.checkBoxState[sigAlarmCheck.Name];
            sigFilAlarmCheck.Checked = json_read.checkBoxState[sigFilAlarmCheck.Name];
            stopStartCheck.Checked = json_read.checkBoxState[stopStartCheck.Name];
            // Приточный вентилятор
            prFanPSCheck.Checked = json_read.checkBoxState[prFanPSCheck.Name];
            prFanFC_check.Checked = json_read.checkBoxState[prFanFC_check.Name];
            prFanThermoCheck.Checked = json_read.checkBoxState[prFanThermoCheck.Name];
            curDefPrFanCheck.Checked = json_read.checkBoxState[curDefPrFanCheck.Name];
            checkResPrFan.Checked = json_read.checkBoxState[checkResPrFan.Name];
            prFanAlarmCheck.Checked = json_read.checkBoxState[prFanAlarmCheck.Name];
            prFanStStopCheck.Checked = json_read.checkBoxState[prFanStStopCheck.Name];
            prFanSpeedCheck.Checked = json_read.checkBoxState[prFanSpeedCheck.Name];
            // Вытяжной вентилятор
            outFanPSCheck.Checked = json_read.checkBoxState[outFanPSCheck.Name];
            outFanFC_check.Checked = json_read.checkBoxState[outFanFC_check.Name];
            outFanThermoCheck.Checked = json_read.checkBoxState[outFanThermoCheck.Name];
            curDefOutFanCheck.Checked = json_read.checkBoxState[curDefOutFanCheck.Name];
            checkResOutFan.Checked = json_read.checkBoxState[checkResOutFan.Name];
            outFanAlarmCheck.Checked = json_read.checkBoxState[outFanAlarmCheck.Name];
            outFanStStopCheck.Checked = json_read.checkBoxState[outFanStStopCheck.Name];
            outFanSpeedCheck.Checked = json_read.checkBoxState[outFanSpeedCheck.Name];
        }

        ///<summary>Загрузка для всех comboBox элементов</summary>
        private void LoadComboBoxElemAll()
        {
            // Выбор типа системы (П/ПВ)
            comboSysType.SelectedIndex = json_read.comboBoxElemState[comboSysType.Name];
            // Приточный вентилятор
            prFanPowCombo.SelectedIndex = json_read.comboBoxElemState[prFanPowCombo.Name];
            prFanControlCombo.SelectedIndex = json_read.comboBoxElemState[prFanControlCombo.Name];
            if (prFanControlCombo.SelectedIndex == 1) // Блокировка опций для Modbus
            {
                prFanAlarmCheck.Enabled = false; // Сигнал аварии
                prFanSpeedCheck.Enabled = false; // Скорость 0-10 В
            }
            // Вытяжной вентилятор
            outFanPowCombo.SelectedIndex = json_read.comboBoxElemState[outFanPowCombo.Name];
            outFanControlCombo.SelectedIndex = json_read.comboBoxElemState[outFanControlCombo.Name];
            if (outFanControlCombo.SelectedIndex == 1) // Блокировка опций для Modbus
            {
                outFanAlarmCheck.Enabled = false; // Сигнал аварии
                outFanSpeedCheck.Enabled = false; // Скорость 0-10 В
            }
            // Воздушные фильтры
            filterPrCombo.SelectedIndex = json_read.comboBoxElemState[filterPrCombo.Name];
            filterOutCombo.SelectedIndex = json_read.comboBoxElemState[filterOutCombo.Name];
            // Заслонки
            prDampPowCombo.SelectedIndex = json_read.comboBoxElemState[prDampPowCombo.Name];
            outDampPowCombo.SelectedIndex = json_read.comboBoxElemState[outDampPowCombo.Name];
            // Нагреватель
            heatTypeCombo.SelectedIndex = json_read.comboBoxElemState[heatTypeCombo.Name];
            powPumpCombo.SelectedIndex = json_read.comboBoxElemState[powPumpCombo.Name];
            elHeatStagesCombo.SelectedIndex = json_read.comboBoxElemState[elHeatStagesCombo.Name];
            firstStHeatCombo.SelectedIndex = json_read.comboBoxElemState[firstStHeatCombo.Name];
            thermSwitchCombo.SelectedIndex = json_read.comboBoxElemState[thermSwitchCombo.Name];
            // Второй нагреватель
            heatAddTypeCombo.SelectedIndex = json_read.comboBoxElemState[heatAddTypeCombo.Name];
            powPumpAddCombo.SelectedIndex = json_read.comboBoxElemState[powPumpAddCombo.Name];
            elHeatAddStagesCombo.SelectedIndex = json_read.comboBoxElemState[elHeatAddStagesCombo.Name];
            firstStAddHeatCombo.SelectedIndex = json_read.comboBoxElemState[firstStAddHeatCombo.Name];
            thermAddSwitchCombo.SelectedIndex = json_read.comboBoxElemState[thermAddSwitchCombo.Name];
            // Охладитель
            coolTypeCombo.SelectedIndex = json_read.comboBoxElemState[coolTypeCombo.Name];
            frCoolStagesCombo.SelectedIndex = json_read.comboBoxElemState[frCoolStagesCombo.Name];
            powWatCoolCombo.SelectedIndex = json_read.comboBoxElemState[powWatCoolCombo.Name];
            // Увлажнитель
            humidTypeCombo.SelectedIndex = json_read.comboBoxElemState[humidTypeCombo.Name];
            // Рециркуляция
            recircPowCombo.SelectedIndex = json_read.comboBoxElemState[recircPowCombo.Name];
            // Рекуператор
            recupTypeCombo.SelectedIndex = json_read.comboBoxElemState[recupTypeCombo.Name];
            rotorPowCombo.SelectedIndex = json_read.comboBoxElemState[rotorPowCombo.Name];
            bypassPlastCombo.SelectedIndex = json_read.comboBoxElemState[bypassPlastCombo.Name];
        }

        ///<summary>Загрузка для всех textBox</summary>
        private void LoadTextBoxAll()
        {
            // Приточный вентилятор
            powPrFanBox.Text = json_read.textBoxElemState[powPrFanBox.Name];
            powPrResFanBox.Text = json_read.textBoxElemState[powPrResFanBox.Name];
            // Вытяжной вентилятор
            powOutFanBox.Text = json_read.textBoxElemState[powOutFanBox.Name];
            powOutResFanBox.Text = json_read.textBoxElemState[powOutResFanBox.Name];
            // Воздушные заслонки
            b_prDampBox.Text = json_read.textBoxElemState[b_prDampBox.Name];
            h_prDampBox.Text = json_read.textBoxElemState[h_prDampBox.Name];
            b_outDampBox.Text = json_read.textBoxElemState[b_outDampBox.Name];
            h_outDampBox.Text = json_read.textBoxElemState[h_outDampBox.Name];
            // Нагреватель
            elHeatPowBox.Text = json_read.textBoxElemState[elHeatPowBox.Name];
            // Второй нагреватель
            elAddHeatPowBox.Text = json_read.textBoxElemState[elAddHeatPowBox.Name];
            // Рециркуляция
            b_recircBox.Text = json_read.textBoxElemState[b_recircBox.Name];
            h_recircBox.Text = json_read.textBoxElemState[h_recircBox.Name];
            // Рекуператор
            powRotRecBox.Text = json_read.textBoxElemState[powRotRecBox.Name];
        }

        ///<summary>Загрузка для подписей кодов таблицы сигналов</summary>
        private void LoadLabelSignalsAll()
        {
            // AO сигналы, ПЛК
            AO1_lab.Text = json_read.labelSignalsState[AO1_lab.Name];
            AO2_lab.Text = json_read.labelSignalsState[AO2_lab.Name];
            AO3_lab.Text = json_read.labelSignalsState[AO3_lab.Name];
            // AO сигналы, блок 1
            AO1bl1_lab.Text = json_read.labelSignalsState[AO1bl1_lab.Name];
            AO2bl1_lab.Text = json_read.labelSignalsState[AO2bl1_lab.Name];
            // AO сигналы, блок 2
            AO1bl2_lab.Text = json_read.labelSignalsState[AO1bl2_lab.Name];
            AO2bl2_lab.Text = json_read.labelSignalsState[AO2bl2_lab.Name];
            // AO сигналы, блок 3
            AO1bl3_lab.Text = json_read.labelSignalsState[AO1bl3_lab.Name];
            AO2bl3_lab.Text = json_read.labelSignalsState[AO2bl3_lab.Name];
            // DO сигналы, ПЛК
            DO1_lab.Text = json_read.labelSignalsState[DO1_lab.Name];
            DO2_lab.Text = json_read.labelSignalsState[DO2_lab.Name];
            DO3_lab.Text = json_read.labelSignalsState[DO3_lab.Name];
            DO4_lab.Text = json_read.labelSignalsState[DO4_lab.Name];
            DO5_lab.Text = json_read.labelSignalsState[DO5_lab.Name];
            DO6_lab.Text = json_read.labelSignalsState[DO6_lab.Name];
            // DO сигналы, блок 1
            DO1bl1_lab.Text = json_read.labelSignalsState[DO1bl1_lab.Name];
            DO2bl1_lab.Text = json_read.labelSignalsState[DO2bl1_lab.Name];
            DO3bl1_lab.Text = json_read.labelSignalsState[DO3bl1_lab.Name];
            DO4bl1_lab.Text = json_read.labelSignalsState[DO4bl1_lab.Name];
            DO5bl1_lab.Text = json_read.labelSignalsState[DO5bl1_lab.Name];
            DO6bl1_lab.Text = json_read.labelSignalsState[DO6bl1_lab.Name];
            DO7bl1_lab.Text = json_read.labelSignalsState[DO7bl1_lab.Name];
            // DO сигналы, блок 2
            DO1bl2_lab.Text = json_read.labelSignalsState[DO1bl2_lab.Name];
            DO2bl2_lab.Text = json_read.labelSignalsState[DO2bl2_lab.Name];
            DO3bl2_lab.Text = json_read.labelSignalsState[DO3bl2_lab.Name];
            DO4bl2_lab.Text = json_read.labelSignalsState[DO4bl2_lab.Name];
            DO5bl2_lab.Text = json_read.labelSignalsState[DO5bl2_lab.Name];
            DO6bl2_lab.Text = json_read.labelSignalsState[DO6bl2_lab.Name];
            DO7bl2_lab.Text = json_read.labelSignalsState[DO7bl2_lab.Name];
            // DO сигналы, блок 3
            DO1bl3_lab.Text = json_read.labelSignalsState[DO1bl3_lab.Name];
            DO2bl3_lab.Text = json_read.labelSignalsState[DO2bl3_lab.Name];
            DO3bl3_lab.Text = json_read.labelSignalsState[DO3bl3_lab.Name];
            DO4bl3_lab.Text = json_read.labelSignalsState[DO4bl3_lab.Name];
            DO5bl3_lab.Text = json_read.labelSignalsState[DO5bl3_lab.Name];
            DO6bl3_lab.Text = json_read.labelSignalsState[DO6bl3_lab.Name];
            DO7bl3_lab.Text = json_read.labelSignalsState[DO7bl3_lab.Name];
        }

        ///<summary>Загрузка списка элементов коллекции для comboBox таблицы сигналов</summary>
        private void LoadComboItemsSignals()
        {
            // AO сигналы, ПЛК
            AO1_combo.Items.Clear(); AO2_combo.Items.Clear(); AO3_combo.Items.Clear();
            // AO1, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[AO1_combo.Name].Length; i++)
                AO1_combo.Items.Add(json_read.comboSignalsItems[AO1_combo.Name][i]);
            // AO2, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[AO2_combo.Name].Length; i++)
                AO2_combo.Items.Add(json_read.comboSignalsItems[AO2_combo.Name][i]);
            // AO3, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[AO3_combo.Name].Length; i++)
                AO3_combo.Items.Add(json_read.comboSignalsItems[AO3_combo.Name][i]);
            // AO сигналы, блок 1
            AO1bl1_combo.Items.Clear(); AO2bl1_combo.Items.Clear();
            // AO1, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[AO1bl1_combo.Name].Length; i++)
                AO1bl1_combo.Items.Add(json_read.comboSignalsItems[AO1bl1_combo.Name][i]);
            // AO1, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[AO2bl1_combo.Name].Length; i++)
                AO2bl1_combo.Items.Add(json_read.comboSignalsItems[AO2bl1_combo.Name][i]);
            // AO сигналы, блок 2
            AO1bl2_combo.Items.Clear(); AO2bl2_combo.Items.Clear();
            // AO1, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[AO1bl2_combo.Name].Length; i++)
                AO1bl2_combo.Items.Add(json_read.comboSignalsItems[AO1bl2_combo.Name][i]);
            // AO2, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[AO2bl2_combo.Name].Length; i++)
                AO2bl2_combo.Items.Add(json_read.comboSignalsItems[AO2bl2_combo.Name][i]);
            // AO сигналы, блок 3
            AO1bl3_combo.Items.Clear(); AO2bl3_combo.Items.Clear();
            // AO1, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[AO1bl3_combo.Name].Length; i++)
                AO1bl3_combo.Items.Add(json_read.comboSignalsItems[AO1bl3_combo.Name][i]);
            // AO2, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[AO2bl3_combo.Name].Length; i++)
                AO2bl3_combo.Items.Add(json_read.comboSignalsItems[AO2bl3_combo.Name][i]);
            // DO сигналы, ПЛК
            DO1_combo.Items.Clear(); DO2_combo.Items.Clear(); DO3_combo.Items.Clear();
            DO4_combo.Items.Clear(); DO5_combo.Items.Clear(); DO6_combo.Items.Clear();
            // DO1, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[DO1_combo.Name].Length; i++)
                DO1_combo.Items.Add(json_read.comboSignalsItems[DO1_combo.Name][i]);
            // DO2, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[DO2_combo.Name].Length; i++)
                DO2_combo.Items.Add(json_read.comboSignalsItems[DO2_combo.Name][i]);
            // DO3, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[DO3_combo.Name].Length; i++)
                DO3_combo.Items.Add(json_read.comboSignalsItems[DO3_combo.Name][i]);
            // DO4, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[DO4_combo.Name].Length; i++)
                DO4_combo.Items.Add(json_read.comboSignalsItems[DO4_combo.Name][i]);
            // DO5, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[DO5_combo.Name].Length; i++)
                DO5_combo.Items.Add(json_read.comboSignalsItems[DO5_combo.Name][i]);
            // DO6, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[DO6_combo.Name].Length; i++)
                DO6_combo.Items.Add(json_read.comboSignalsItems[DO6_combo.Name][i]);
            //  DO сигналы, блок 1
            DO1bl1_combo.Items.Clear(); DO2bl1_combo.Items.Clear(); DO3bl1_combo.Items.Clear();
            DO4bl1_combo.Items.Clear(); DO5bl1_combo.Items.Clear(); DO6bl1_combo.Items.Clear();
            DO7bl1_combo.Items.Clear();
            // DO1, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[DO1bl1_combo.Name].Length; i++)
                DO1bl1_combo.Items.Add(json_read.comboSignalsItems[DO1bl1_combo.Name][i]);
            // DO2, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[DO2bl1_combo.Name].Length; i++)
                DO2bl1_combo.Items.Add(json_read.comboSignalsItems[DO2bl1_combo.Name][i]);
            // DO3, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[DO3bl1_combo.Name].Length; i++)
                DO3bl1_combo.Items.Add(json_read.comboSignalsItems[DO3bl1_combo.Name][i]);
            // DO4, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[DO4bl1_combo.Name].Length; i++)
                DO4bl1_combo.Items.Add(json_read.comboSignalsItems[DO4bl1_combo.Name][i]);
            // DO5, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[DO5bl1_combo.Name].Length; i++)
                DO5bl1_combo.Items.Add(json_read.comboSignalsItems[DO5bl1_combo.Name][i]);
            // DO6, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[DO6bl1_combo.Name].Length; i++)
                DO6bl1_combo.Items.Add(json_read.comboSignalsItems[DO6bl1_combo.Name][i]);
            // DO7, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[DO7bl1_combo.Name].Length; i++)
                DO7bl1_combo.Items.Add(json_read.comboSignalsItems[DO7bl1_combo.Name][i]);
            // DO сигналы, блок 2
            DO1bl2_combo.Items.Clear(); DO2bl2_combo.Items.Clear(); DO3bl2_combo.Items.Clear();
            DO4bl2_combo.Items.Clear(); DO5bl2_combo.Items.Clear(); DO6bl2_combo.Items.Clear();
            DO7bl2_combo.Items.Clear();
            // DO1, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[DO1bl2_combo.Name].Length; i++)
                DO1bl2_combo.Items.Add(json_read.comboSignalsItems[DO1bl2_combo.Name][i]);
            // DO2, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[DO2bl2_combo.Name].Length; i++)
                DO2bl2_combo.Items.Add(json_read.comboSignalsItems[DO2bl2_combo.Name][i]);
            // DO3, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[DO3bl2_combo.Name].Length; i++)
                DO3bl2_combo.Items.Add(json_read.comboSignalsItems[DO3bl2_combo.Name][i]);
            // DO4, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[DO4bl2_combo.Name].Length; i++)
                DO4bl2_combo.Items.Add(json_read.comboSignalsItems[DO4bl2_combo.Name][i]);
            // DO5, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[DO5bl2_combo.Name].Length; i++)
                DO5bl2_combo.Items.Add(json_read.comboSignalsItems[DO5bl2_combo.Name][i]);
            // DO6, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[DO6bl2_combo.Name].Length; i++)
                DO6bl2_combo.Items.Add(json_read.comboSignalsItems[DO6bl2_combo.Name][i]);
            // DO7, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[DO7bl2_combo.Name].Length; i++)
                DO7bl2_combo.Items.Add(json_read.comboSignalsItems[DO7bl2_combo.Name][i]);
            // DO сигналы, блок 3
            DO1bl3_combo.Items.Clear(); DO2bl3_combo.Items.Clear(); DO3bl3_combo.Items.Clear();
            DO4bl3_combo.Items.Clear(); DO5bl3_combo.Items.Clear(); DO6bl3_combo.Items.Clear();
            DO7bl3_combo.Items.Clear();
            // DO1, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[DO1bl3_combo.Name].Length; i++)
                DO1bl3_combo.Items.Add(json_read.comboSignalsItems[DO1bl3_combo.Name][i]);
            // DO2, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[DO2bl3_combo.Name].Length; i++)
                DO2bl3_combo.Items.Add(json_read.comboSignalsItems[DO2bl3_combo.Name][i]);
            // DO3, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[DO3bl3_combo.Name].Length; i++)
                DO3bl3_combo.Items.Add(json_read.comboSignalsItems[DO3bl3_combo.Name][i]);
            // DO4, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[DO4bl3_combo.Name].Length; i++)
                DO4bl3_combo.Items.Add(json_read.comboSignalsItems[DO4bl3_combo.Name][i]);
            // DO5, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[DO5bl3_combo.Name].Length; i++)
                DO5bl3_combo.Items.Add(json_read.comboSignalsItems[DO5bl3_combo.Name][i]);
            // DO6, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[DO6bl3_combo.Name].Length; i++)
                DO6bl3_combo.Items.Add(json_read.comboSignalsItems[DO6bl3_combo.Name][i]);
            // DO7, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[DO7bl3_combo.Name].Length; i++)
                DO7bl3_combo.Items.Add(json_read.comboSignalsItems[DO7bl3_combo.Name][i]);
        }

        ///<summary>Загрузка состояний для comboBox таблицы сигналов</summary>
        private void LoadComboSignalsAll()
        {
            // AO1, ПЛК
            AO1_combo.SelectedItem = json_read.comboSignalsState[AO1_combo.Name];
            AO1combo_index = AO1_combo.SelectedIndex;
            AO1combo_text = AO1_combo.SelectedItem.ToString();
            // AO2, ПЛК
            AO2_combo.SelectedItem = json_read.comboSignalsState[AO2_combo.Name];
            AO2combo_index = AO2_combo.SelectedIndex;
            AO2combo_text = AO2_combo.SelectedItem.ToString();
            // AO3, ПЛК
            AO3_combo.SelectedItem = json_read.comboSignalsState[AO3_combo.Name];
            AO3combo_index = AO3_combo.SelectedIndex;
            AO3combo_text = AO3_combo.SelectedItem.ToString();
            // AO1, блок 1
            AO1bl1_combo.SelectedItem = json_read.comboSignalsState[AO1bl1_combo.Name];
            AO1bl1combo_index = AO1bl1_combo.SelectedIndex;
            AO1bl1combo_text = AO1bl1_combo.SelectedItem.ToString();
            // AO2, блок 1
            AO2bl1_combo.SelectedItem = json_read.comboSignalsState[AO2bl1_combo.Name];
            AO2bl1combo_index = AO2bl1_combo.SelectedIndex;
            AO2bl1combo_text = AO2bl1_combo.SelectedItem.ToString();
            // AO1, блок 2
            AO1bl2_combo.SelectedItem = json_read.comboSignalsState[AO1bl2_combo.Name];
            AO1bl2combo_index = AO1bl2_combo.SelectedIndex;
            AO1bl2combo_text = AO1bl2_combo.SelectedItem.ToString();
            // AO2, блок 2
            AO2bl2_combo.SelectedItem = json_read.comboSignalsState[AO2bl2_combo.Name];
            AO2bl2combo_index = AO2bl2_combo.SelectedIndex;
            AO2bl2combo_text = AO2bl2_combo.SelectedItem.ToString();
            // AO1, блок 3
            AO1bl3_combo.SelectedItem = json_read.comboSignalsState[AO1bl3_combo.Name];
            AO1bl3combo_index = AO1bl3_combo.SelectedIndex;
            AO1bl3combo_text = AO1bl3_combo.SelectedItem.ToString();
            // AO2, блок 3
            AO2bl3_combo.SelectedItem = json_read.comboSignalsState[AO2bl3_combo.Name];
            AO2bl3combo_index = AO2bl3_combo.SelectedIndex;
            AO2bl3combo_text = AO2bl3_combo.SelectedItem.ToString();
            // DO1, ПЛК
            DO1_combo.SelectedItem = json_read.comboSignalsState[DO1_combo.Name];
            DO1combo_index = DO1_combo.SelectedIndex;
            DO1combo_text = DO1_combo.SelectedItem.ToString();
            // DO2, ПЛК
            DO2_combo.SelectedItem = json_read.comboSignalsState[DO2_combo.Name];
            DO2combo_index = DO2_combo.SelectedIndex;
            DO2combo_text = DO2_combo.SelectedItem.ToString();
            // DO3, ПЛК
            DO3_combo.SelectedItem = json_read.comboSignalsState[DO3_combo.Name];
            DO3combo_index = DO3_combo.SelectedIndex;
            DO3combo_text = DO3_combo.SelectedItem.ToString();
            // DO4, ПЛК
            DO4_combo.SelectedItem = json_read.comboSignalsState[DO4_combo.Name];
            DO4combo_index = DO4_combo.SelectedIndex;
            DO4combo_text = DO4_combo.SelectedItem.ToString();
            // DO5, ПЛК
            DO5_combo.SelectedItem = json_read.comboSignalsState[DO5_combo.Name];
            DO5combo_index = DO5_combo.SelectedIndex;
            DO5combo_text = DO5_combo.SelectedItem.ToString();
            // DO6, ПЛК
            DO6_combo.SelectedItem = json_read.comboSignalsState[DO6_combo.Name];
            DO6combo_index = DO6_combo.SelectedIndex;
            DO6combo_text = DO6_combo.SelectedItem.ToString();
            // DO1, блок 1
            DO1bl1_combo.SelectedItem = json_read.comboSignalsState[DO1bl1_combo.Name];
            DO1bl1combo_index = DO1bl1_combo.SelectedIndex;
            DO1bl1combo_text = DO1bl1_combo.SelectedItem.ToString();
            // DO2, блок 1
            DO2bl1_combo.SelectedItem = json_read.comboSignalsState[DO2bl1_combo.Name];
            DO2bl1combo_index = DO2bl1_combo.SelectedIndex;
            DO2bl1combo_text = DO2bl1_combo.SelectedItem.ToString();
            // DO3, блок 1
            DO3bl1_combo.SelectedItem = json_read.comboSignalsState[DO3bl1_combo.Name];
            DO3bl1combo_index = DO3bl1_combo.SelectedIndex;
            DO3bl1combo_text = DO3bl1_combo.SelectedItem.ToString();
            // DO4, блок 1
            DO4bl1_combo.SelectedItem = json_read.comboSignalsState[DO4bl1_combo.Name];
            DO4bl1combo_index = DO4bl1_combo.SelectedIndex;
            DO4bl1combo_text = DO4bl1_combo.SelectedItem.ToString();
            // DO5, блок 1
            DO5bl1_combo.SelectedItem = json_read.comboSignalsState[DO5bl1_combo.Name];
            DO5bl1combo_index = DO5bl1_combo.SelectedIndex;
            DO5bl1combo_text = DO5bl1_combo.SelectedItem.ToString();
            // DO6, блок 1
            DO6bl1_combo.SelectedItem = json_read.comboSignalsState[DO6bl1_combo.Name];
            DO6bl1combo_index = DO6bl1_combo.SelectedIndex;
            DO6bl1combo_text = DO6bl1_combo.SelectedItem.ToString();
            // DO7, блок 1
            DO7bl1_combo.SelectedItem = json_read.comboSignalsState[DO7bl1_combo.Name];
            DO7bl1combo_index = DO7bl1_combo.SelectedIndex;
            DO7bl1combo_text = DO7bl1_combo.SelectedItem.ToString();
            // DO1, блок 2
            DO1bl2_combo.SelectedItem = json_read.comboSignalsState[DO1bl2_combo.Name];
            DO1bl2combo_index = DO1bl2_combo.SelectedIndex;
            DO1bl2combo_text = DO1bl2_combo.SelectedItem.ToString();
            // DO2, блок 2
            DO2bl2_combo.SelectedItem = json_read.comboSignalsState[DO2bl2_combo.Name];
            DO2bl2combo_index = DO2bl2_combo.SelectedIndex;
            DO2bl2combo_text = DO2bl2_combo.SelectedItem.ToString();
            // DO3, блок 2
            DO3bl2_combo.SelectedItem = json_read.comboSignalsState[DO3bl2_combo.Name];
            DO3bl2combo_index = DO3bl2_combo.SelectedIndex;
            DO3bl2combo_text = DO3bl2_combo.SelectedItem.ToString();
            // DO4, блок 2
            DO4bl2_combo.SelectedItem = json_read.comboSignalsState[DO4bl2_combo.Name];
            DO4bl2combo_index = DO4bl2_combo.SelectedIndex;
            DO4bl2combo_text = DO4bl2_combo.SelectedItem.ToString();
            // DO5, блок 2
            DO5bl2_combo.SelectedItem = json_read.comboSignalsState[DO5bl2_combo.Name];
            DO5bl2combo_index = DO5bl2_combo.SelectedIndex;
            DO5bl2combo_text = DO5bl2_combo.SelectedItem.ToString();
            // DO6, блок 2
            DO6bl2_combo.SelectedItem = json_read.comboSignalsState[DO6bl2_combo.Name];
            DO6bl2combo_index = DO6bl2_combo.SelectedIndex;
            DO6bl2combo_text = DO6bl2_combo.SelectedItem.ToString();
            // DO7, блок 2
            DO7bl2_combo.SelectedItem = json_read.comboSignalsState[DO7bl2_combo.Name];
            DO7bl2combo_index = DO7bl2_combo.SelectedIndex;
            DO7bl2combo_text = DO7bl2_combo.SelectedItem.ToString();
            // DO1, блок 3
            DO1bl3_combo.SelectedItem = json_read.comboSignalsState[DO1bl3_combo.Name];
            DO1bl3combo_index = DO1bl3_combo.SelectedIndex;
            DO1bl3combo_text = DO1bl3_combo.SelectedItem.ToString();
            // DO2, блок 3
            DO2bl3_combo.SelectedItem = json_read.comboSignalsState[DO2bl3_combo.Name];
            DO2bl3combo_index = DO2bl3_combo.SelectedIndex;
            DO2bl3combo_text = DO2bl3_combo.SelectedItem.ToString();
            // DO3, блок 3
            DO3bl3_combo.SelectedItem = json_read.comboSignalsState[DO3bl3_combo.Name];
            DO3bl3combo_index = DO3bl3_combo.SelectedIndex;
            DO3bl3combo_text = DO3bl3_combo.SelectedItem.ToString();
            // DO4, блок 3
            DO4bl3_combo.SelectedItem = json_read.comboSignalsState[DO4bl3_combo.Name];
            DO4bl3combo_index = DO4bl3_combo.SelectedIndex;
            DO4bl3combo_text = DO4bl3_combo.SelectedItem.ToString();
            // DO5, блок 3
            DO5bl3_combo.SelectedItem = json_read.comboSignalsState[DO5bl3_combo.Name];
            DO5bl3combo_index = DO5bl3_combo.SelectedIndex;
            DO5bl3combo_text = DO5bl3_combo.SelectedItem.ToString();
            // DO6, блок 3
            DO6bl3_combo.SelectedItem = json_read.comboSignalsState[DO6bl3_combo.Name];
            DO6bl3combo_index = DO6bl3_combo.SelectedIndex;
            DO6bl3combo_text = DO6bl3_combo.SelectedItem.ToString();
            // DO7, блок 3
            DO7bl3_combo.SelectedItem = json_read.comboSignalsState[DO7bl3_combo.Name];
            DO7bl3combo_index = DO7bl3_combo.SelectedIndex;
            DO7bl3combo_text = DO7bl3_combo.SelectedItem.ToString();
        }

        ///<summary>Загрузка для массивов сигналов</summary>
        private void LoadSignalArrays()
        {
            string name;
            ushort code;
            bool active;
            list_ao.Clear(); // Очистка прежнего списка сигналов AO
            list_do.Clear(); // Очистка прежнего списка сигналов DO 
            /*foreach (var elem in json_read.aiCode)
            {   // Сигналы AI
                name = elem.Key;
                code = json_read.aiCode[name];
                type = json_read.aiType[name];
                active = json_read.aiActive[name];
                list_ai.Add(new Ai(name, code, type, active));
            }*/
            foreach (var elem in json_read.aoCode)
            {   // Сигналы AO
                name = elem.Key;
                code = json_read.aoCode[name];
                active = json_read.aoActive[name];
                list_ao.Add(new Ao(name, code, active));
            }
            foreach (var elem in json_read.doCode)
            {   // Сигналы DO
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
            // Выбор элементов (боковая панель)
            json.checkBoxState.Add(filterCheck.Name, filterCheck.Checked);
            json.checkBoxState.Add(dampCheck.Name, dampCheck.Checked);
            json.checkBoxState.Add(heaterCheck.Name, heaterCheck.Checked);
            json.checkBoxState.Add(addHeatCheck.Name, addHeatCheck.Checked);
            json.checkBoxState.Add(coolerCheck.Name, coolerCheck.Checked);
            json.checkBoxState.Add(humidCheck.Name, humidCheck.Checked);
            json.checkBoxState.Add(recircCheck.Name, recircCheck.Checked);
            json.checkBoxState.Add(recupCheck.Name, recupCheck.Checked);
            // Заслонки
            json.checkBoxState.Add(confPrDampCheck.Name, confPrDampCheck.Checked);
            json.checkBoxState.Add(heatPrDampCheck.Name, heatPrDampCheck.Checked);
            json.checkBoxState.Add(springRetPrDampCheck.Name, springRetPrDampCheck.Checked);
            json.checkBoxState.Add(outDampCheck.Name, outDampCheck.Checked);
            json.checkBoxState.Add(confOutDampCheck.Name, confOutDampCheck.Checked);
            json.checkBoxState.Add(heatOutDampCheck.Name, heatOutDampCheck.Checked);
            json.checkBoxState.Add(springRetOutDampCheck.Name, springRetOutDampCheck.Checked);
            // Нагреватель
            json.checkBoxState.Add(TF_heaterCheck.Name, TF_heaterCheck.Checked);
            json.checkBoxState.Add(confHeatPumpCheck.Name, confHeatPumpCheck.Checked);
            json.checkBoxState.Add(watSensHeatCheck.Name, watSensHeatCheck.Checked);
            // Второй нагреватель
            json.checkBoxState.Add(TF_addHeaterCheck.Name, TF_addHeaterCheck.Checked);
            json.checkBoxState.Add(confAddHeatPumpCheck.Name, confAddHeatPumpCheck.Checked);
            json.checkBoxState.Add(sensWatAddHeatCheck.Name, sensWatAddHeatCheck.Checked);
            json.checkBoxState.Add(pumpAddHeatCheck.Name, pumpAddHeatCheck.Checked);
            // Охладитель
            json.checkBoxState.Add(alarmFrCoolCheck.Name, alarmFrCoolCheck.Checked);
            json.checkBoxState.Add(thermoCoolerCheck.Name, thermoCoolerCheck.Checked);
            json.checkBoxState.Add(analogFreonCheck.Name, analogFreonCheck.Checked);
            json.checkBoxState.Add(dehumModeCheck.Name, dehumModeCheck.Checked);
            // Увлажнитель
            json.checkBoxState.Add(alarmHumidCheck.Name, alarmHumidCheck.Checked);
            // Рециркуляция
            json.checkBoxState.Add(springRetRecircCheck.Name, springRetRecircCheck.Checked);
            // Рекуператор
            json.checkBoxState.Add(pumpGlicRecCheck.Name, pumpGlicRecCheck.Checked);
            json.checkBoxState.Add(recDefTempCheck.Name, recDefTempCheck.Checked);
            json.checkBoxState.Add(recDefPsCheck.Name, recDefPsCheck.Checked);
            // Датчики/сигналы
            json.checkBoxState.Add(prChanSensCheck.Name, prChanSensCheck.Checked);
            json.checkBoxState.Add(roomTempSensCheck.Name, roomTempSensCheck.Checked);
            json.checkBoxState.Add(chanHumSensCheck.Name, chanHumSensCheck.Checked);
            json.checkBoxState.Add(roomHumSensCheck.Name, roomHumSensCheck.Checked);
            json.checkBoxState.Add(outdoorChanSensCheck.Name, outdoorChanSensCheck.Checked);
            json.checkBoxState.Add(outChanSensCheck.Name, outChanSensCheck.Checked);
            json.checkBoxState.Add(sigWorkCheck.Name, sigWorkCheck.Checked);
            json.checkBoxState.Add(sigAlarmCheck.Name, sigAlarmCheck.Checked);
            json.checkBoxState.Add(sigFilAlarmCheck.Name, sigFilAlarmCheck.Checked);
            json.checkBoxState.Add(stopStartCheck.Name, stopStartCheck.Checked);
            // Приточный вентилятор
            json.checkBoxState.Add(prFanPSCheck.Name, prFanPSCheck.Checked);
            json.checkBoxState.Add(prFanFC_check.Name, prFanFC_check.Checked);
            json.checkBoxState.Add(prFanThermoCheck.Name, prFanThermoCheck.Checked);
            json.checkBoxState.Add(curDefPrFanCheck.Name, curDefPrFanCheck.Checked);
            json.checkBoxState.Add(checkResPrFan.Name, checkResPrFan.Checked);
            json.checkBoxState.Add(prFanAlarmCheck.Name, prFanAlarmCheck.Checked);
            json.checkBoxState.Add(prFanStStopCheck.Name, prFanStStopCheck.Checked);
            json.checkBoxState.Add(prFanSpeedCheck.Name, prFanSpeedCheck.Checked);
            // Вытяжной вентилятор
            json.checkBoxState.Add(outFanPSCheck.Name, outFanPSCheck.Checked);
            json.checkBoxState.Add(outFanFC_check.Name, outFanFC_check.Checked);
            json.checkBoxState.Add(outFanThermoCheck.Name, outFanThermoCheck.Checked);
            json.checkBoxState.Add(curDefOutFanCheck.Name, curDefOutFanCheck.Checked);
            json.checkBoxState.Add(checkResOutFan.Name, checkResOutFan.Checked);
            json.checkBoxState.Add(outFanAlarmCheck.Name, outFanAlarmCheck.Checked);
            json.checkBoxState.Add(outFanStStopCheck.Name, outFanStStopCheck.Checked);
            json.checkBoxState.Add(outFanSpeedCheck.Name, outFanSpeedCheck.Checked);
        }

        ///<summary>Сборка для всех comboBox элементов программы</summary>
        private void BuildComboBoxElemAll()
        {
            // Выбор типа системы (П/ПВ)
            json.comboBoxElemState.Add(comboSysType.Name, comboSysType.SelectedIndex);
            // Приточный вентилятор
            json.comboBoxElemState.Add(prFanPowCombo.Name, prFanPowCombo.SelectedIndex);
            json.comboBoxElemState.Add(prFanControlCombo.Name, prFanControlCombo.SelectedIndex);
            // Вытяжной вентилятор
            json.comboBoxElemState.Add(outFanPowCombo.Name, outFanPowCombo.SelectedIndex);
            json.comboBoxElemState.Add(outFanControlCombo.Name, outFanControlCombo.SelectedIndex);
            // Воздушные фильтры
            json.comboBoxElemState.Add(filterPrCombo.Name, filterPrCombo.SelectedIndex);
            json.comboBoxElemState.Add(filterOutCombo.Name, filterOutCombo.SelectedIndex);
            // Заслонки
            json.comboBoxElemState.Add(prDampPowCombo.Name, prDampPowCombo.SelectedIndex);
            json.comboBoxElemState.Add(outDampPowCombo.Name, outDampPowCombo.SelectedIndex);
            // Нагреватель
            json.comboBoxElemState.Add(heatTypeCombo.Name, heatTypeCombo.SelectedIndex);
            json.comboBoxElemState.Add(powPumpCombo.Name, powPumpCombo.SelectedIndex);
            json.comboBoxElemState.Add(elHeatStagesCombo.Name, elHeatStagesCombo.SelectedIndex);
            json.comboBoxElemState.Add(firstStHeatCombo.Name, firstStHeatCombo.SelectedIndex);
            json.comboBoxElemState.Add(thermSwitchCombo.Name, thermSwitchCombo.SelectedIndex);
            // Второй нагреватель
            json.comboBoxElemState.Add(heatAddTypeCombo.Name, heatAddTypeCombo.SelectedIndex);
            json.comboBoxElemState.Add(powPumpAddCombo.Name, powPumpAddCombo.SelectedIndex);
            json.comboBoxElemState.Add(elHeatAddStagesCombo.Name, elHeatAddStagesCombo.SelectedIndex);
            json.comboBoxElemState.Add(firstStAddHeatCombo.Name, firstStAddHeatCombo.SelectedIndex);
            json.comboBoxElemState.Add(thermAddSwitchCombo.Name, thermAddSwitchCombo.SelectedIndex);
            // Охладитель
            json.comboBoxElemState.Add(coolTypeCombo.Name, coolTypeCombo.SelectedIndex);
            json.comboBoxElemState.Add(frCoolStagesCombo.Name, frCoolStagesCombo.SelectedIndex);
            json.comboBoxElemState.Add(powWatCoolCombo.Name, powWatCoolCombo.SelectedIndex);
            // Увлажнитель
            json.comboBoxElemState.Add(humidTypeCombo.Name, humidTypeCombo.SelectedIndex);
            // Рециркуляция
            json.comboBoxElemState.Add(recircPowCombo.Name, recircPowCombo.SelectedIndex);
            // Рекуператор
            json.comboBoxElemState.Add(recupTypeCombo.Name, recupTypeCombo.SelectedIndex);
            json.comboBoxElemState.Add(rotorPowCombo.Name, rotorPowCombo.SelectedIndex);
            json.comboBoxElemState.Add(bypassPlastCombo.Name, bypassPlastCombo.SelectedIndex);
        }

        ///<summary>Сборка для всех textBox элементов программы</summary>
        private void BuildTextBoxAll()
        {
            // Приточный вентилятор
            json.textBoxElemState.Add(powPrFanBox.Name, powPrFanBox.Text);
            json.textBoxElemState.Add(powPrResFanBox.Name, powPrResFanBox.Text);
            // Вытяжной вентилятор
            json.textBoxElemState.Add(powOutFanBox.Name, powOutFanBox.Text);
            json.textBoxElemState.Add(powOutResFanBox.Name, powOutResFanBox.Text);
            // Воздушные заслонки
            json.textBoxElemState.Add(b_prDampBox.Name, b_prDampBox.Text);
            json.textBoxElemState.Add(h_prDampBox.Name, h_prDampBox.Text);
            json.textBoxElemState.Add(b_outDampBox.Name, b_outDampBox.Text);
            json.textBoxElemState.Add(h_outDampBox.Name, h_outDampBox.Text);
            // Нагреватель
            json.textBoxElemState.Add(elHeatPowBox.Name, elHeatPowBox.Text);
            // Второй нагреватель
            json.textBoxElemState.Add(elAddHeatPowBox.Name, elAddHeatPowBox.Text);
            // Рециркуляция
            json.textBoxElemState.Add(b_recircBox.Name, b_recircBox.Text);
            json.textBoxElemState.Add(h_recircBox.Name, h_recircBox.Text);
            // Рекуператор
            json.textBoxElemState.Add(powRotRecBox.Name, powRotRecBox.Text);
        }

        ///<summary>Сборка для подписей кодов таблицы сигналов</summary>
        private void BuildLabelSignalsAll()
        {
            // AO сигналы, ПЛК
            json.labelSignalsState.Add(AO1_lab.Name, AO1_lab.Text);
            json.labelSignalsState.Add(AO2_lab.Name, AO2_lab.Text);
            json.labelSignalsState.Add(AO3_lab.Name, AO3_lab.Text);
            // AO сигналы, блок 1
            json.labelSignalsState.Add(AO1bl1_lab.Name, AO1bl1_lab.Text);
            json.labelSignalsState.Add(AO2bl1_lab.Name, AO2bl1_lab.Text);
            // AO сигналы, блок 2
            json.labelSignalsState.Add(AO1bl2_lab.Name, AO1bl2_lab.Text);
            json.labelSignalsState.Add(AO2bl2_lab.Name, AO2bl2_lab.Text);
            // AO сигналы, блок 3
            json.labelSignalsState.Add(AO1bl3_lab.Name, AO1bl3_lab.Text);
            json.labelSignalsState.Add(AO2bl3_lab.Name, AO2bl3_lab.Text);
            // DO сигналы, ПЛК
            json.labelSignalsState.Add(DO1_lab.Name, DO1_lab.Text);
            json.labelSignalsState.Add(DO2_lab.Name, DO2_lab.Text);
            json.labelSignalsState.Add(DO3_lab.Name, DO3_lab.Text);
            json.labelSignalsState.Add(DO4_lab.Name, DO4_lab.Text);
            json.labelSignalsState.Add(DO5_lab.Name, DO5_lab.Text);
            json.labelSignalsState.Add(DO6_lab.Name, DO6_lab.Text);
            // DO сигналы, блок 1
            json.labelSignalsState.Add(DO1bl1_lab.Name, DO1bl1_lab.Text);
            json.labelSignalsState.Add(DO2bl1_lab.Name, DO2bl1_lab.Text);
            json.labelSignalsState.Add(DO3bl1_lab.Name, DO3bl1_lab.Text);
            json.labelSignalsState.Add(DO4bl1_lab.Name, DO4bl1_lab.Text);
            json.labelSignalsState.Add(DO5bl1_lab.Name, DO5bl1_lab.Text);
            json.labelSignalsState.Add(DO6bl1_lab.Name, DO6bl1_lab.Text);
            json.labelSignalsState.Add(DO7bl1_lab.Name, DO7bl1_lab.Text);
            // DO сигналы, блок 2
            json.labelSignalsState.Add(DO1bl2_lab.Name, DO1bl2_lab.Text);
            json.labelSignalsState.Add(DO2bl2_lab.Name, DO2bl2_lab.Text);
            json.labelSignalsState.Add(DO3bl2_lab.Name, DO3bl2_lab.Text);
            json.labelSignalsState.Add(DO4bl2_lab.Name, DO4bl2_lab.Text);
            json.labelSignalsState.Add(DO5bl2_lab.Name, DO5bl2_lab.Text);
            json.labelSignalsState.Add(DO6bl2_lab.Name, DO6bl2_lab.Text);
            json.labelSignalsState.Add(DO7bl2_lab.Name, DO7bl2_lab.Text);
            // DO сигналы, блок 3
            json.labelSignalsState.Add(DO1bl3_lab.Name, DO1bl3_lab.Text);
            json.labelSignalsState.Add(DO2bl3_lab.Name, DO2bl3_lab.Text);
            json.labelSignalsState.Add(DO3bl3_lab.Name, DO3bl3_lab.Text);
            json.labelSignalsState.Add(DO4bl3_lab.Name, DO4bl3_lab.Text);
            json.labelSignalsState.Add(DO5bl3_lab.Name, DO5bl3_lab.Text);
            json.labelSignalsState.Add(DO6bl3_lab.Name, DO6bl3_lab.Text);
            json.labelSignalsState.Add(DO7bl3_lab.Name, DO7bl3_lab.Text);
        }

        ///<summary>Сборка состояний для comboBox таблицы сигналов</summary>
        private void BuildComboSignalsAll()
        {
            // AO сигналы, ПЛК
            json.comboSignalsState.Add(AO1_combo.Name, AO1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AO2_combo.Name, AO2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AO3_combo.Name, AO3_combo.SelectedItem.ToString());
            // AO сигналы, блок 1
            json.comboSignalsState.Add(AO1bl1_combo.Name, AO1bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AO2bl1_combo.Name, AO2bl1_combo.SelectedItem.ToString());
            // AO сигналы, блок 2
            json.comboSignalsState.Add(AO1bl2_combo.Name, AO1bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AO2bl2_combo.Name, AO2bl2_combo.SelectedItem.ToString());
            // AO сигналы, блок 3
            json.comboSignalsState.Add(AO1bl3_combo.Name, AO1bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AO2bl3_combo.Name, AO2bl3_combo.SelectedItem.ToString());
            // DO сигналы, ПЛК
            json.comboSignalsState.Add(DO1_combo.Name, DO1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO2_combo.Name, DO2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO3_combo.Name, DO3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO4_combo.Name, DO4_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO5_combo.Name, DO5_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO6_combo.Name, DO6_combo.SelectedItem.ToString());
            // DO сигналы, блок 1
            json.comboSignalsState.Add(DO1bl1_combo.Name, DO1bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO2bl1_combo.Name, DO2bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO3bl1_combo.Name, DO3bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO4bl1_combo.Name, DO4bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO5bl1_combo.Name, DO5bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO6bl1_combo.Name, DO6bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO7bl1_combo.Name, DO7bl1_combo.SelectedItem.ToString());
            // DO сигналы, блок 2
            json.comboSignalsState.Add(DO1bl2_combo.Name, DO1bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO2bl2_combo.Name, DO2bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO3bl2_combo.Name, DO3bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO4bl2_combo.Name, DO4bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO5bl2_combo.Name, DO5bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO6bl2_combo.Name, DO6bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO7bl2_combo.Name, DO7bl2_combo.SelectedItem.ToString());
            // DO сигналы, блок 3
            json.comboSignalsState.Add(DO1bl3_combo.Name, DO1bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO2bl3_combo.Name, DO2bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO3bl3_combo.Name, DO3bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO4bl3_combo.Name, DO4bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO5bl3_combo.Name, DO5bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO6bl3_combo.Name, DO6bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO7bl3_combo.Name, DO7bl3_combo.SelectedItem.ToString());
        }

        ///<summary>Сборка коллекций элементов для таблицы сигналов</summary>
        private void BuildComboItemsSignals()
        {
            // AO сигналы
            // AO1, ПЛК
            string[] arr_AO1_combo = new string[AO1_combo.Items.Count];
            for (int i = 0; i < AO1_combo.Items.Count; i++)
                arr_AO1_combo[i] = AO1_combo.GetItemText(AO1_combo.Items[i]);
            json.comboSignalsItems.Add(AO1_combo.Name, arr_AO1_combo);
            // AO2, ПЛК
            string[] arr_AO2_combo = new string[AO2_combo.Items.Count];
            for (int i = 0; i < AO2_combo.Items.Count; i++)
                arr_AO2_combo[i] = AO2_combo.GetItemText(AO2_combo.Items[i]);
            json.comboSignalsItems.Add(AO2_combo.Name, arr_AO2_combo);
            // AO3, ПЛК
            string[] arr_AO3_combo = new string[AO3_combo.Items.Count];
            for (int i = 0; i < AO3_combo.Items.Count; i++)
                arr_AO3_combo[i] = AO3_combo.GetItemText(AO3_combo.Items[i]);
            json.comboSignalsItems.Add(AO3_combo.Name, arr_AO3_combo);
            // AO1, блок 1
            string[] arr_AO1bl1_combo = new string[AO1bl1_combo.Items.Count];
            for (int i = 0; i < AO1bl1_combo.Items.Count; i++)
                arr_AO1bl1_combo[i] = AO1bl1_combo.GetItemText(AO1bl1_combo.Items[i]);
            json.comboSignalsItems.Add(AO1bl1_combo.Name, arr_AO1bl1_combo);
            // AO2, блок 1
            string[] arr_AO2bl1_combo = new string[AO2bl1_combo.Items.Count];
            for (int i = 0; i < AO2bl1_combo.Items.Count; i++)
                arr_AO2bl1_combo[i] = AO2bl1_combo.GetItemText(AO2bl1_combo.Items[i]);
            json.comboSignalsItems.Add(AO2bl1_combo.Name, arr_AO2bl1_combo);
            // AO1, блок 2
            string[] arr_AO1bl2_combo = new string[AO1bl2_combo.Items.Count];
            for (int i = 0; i < AO1bl2_combo.Items.Count; i++)
                arr_AO1bl2_combo[i] = AO1bl2_combo.GetItemText(AO1bl2_combo.Items[i]);
            json.comboSignalsItems.Add(AO1bl2_combo.Name, arr_AO1bl2_combo);
            // AO2, блок 2
            string[] arr_AO2bl2_combo = new string[AO2bl2_combo.Items.Count];
            for (int i = 0; i < AO2bl2_combo.Items.Count; i++)
                arr_AO2bl2_combo[i] = AO2bl2_combo.GetItemText(AO2bl2_combo.Items[i]);
            json.comboSignalsItems.Add(AO2bl2_combo.Name, arr_AO2bl2_combo);
            // AO1, блок 3
            string[] arr_AO1bl3_combo = new string[AO1bl3_combo.Items.Count];
            for (int i = 0; i < AO1bl3_combo.Items.Count; i++)
                arr_AO1bl3_combo[i] = AO1bl3_combo.GetItemText(AO1bl3_combo.Items[i]);
            json.comboSignalsItems.Add(AO1bl3_combo.Name, arr_AO1bl3_combo);
            // AO2, блок 3
            string[] arr_AO2bl3_combo = new string[AO2bl3_combo.Items.Count];
            for (int i = 0; i < AO2bl3_combo.Items.Count; i++)
                arr_AO2bl3_combo[i] = AO2bl3_combo.GetItemText(AO2bl3_combo.Items[i]);
            json.comboSignalsItems.Add(AO2bl3_combo.Name, arr_AO2bl3_combo);
            // DO сигналы
            // DO1, ПЛК
            string[] arr_DO1_combo = new string[DO1_combo.Items.Count];
            for (int i = 0; i < DO1_combo.Items.Count; i++)
                arr_DO1_combo[i] = DO1_combo.GetItemText(DO1_combo.Items[i]);
            json.comboSignalsItems.Add(DO1_combo.Name, arr_DO1_combo);
            // DO2, ПЛК
            string[] arr_DO2_combo = new string[DO2_combo.Items.Count];
            for (int i = 0; i < DO2_combo.Items.Count; i++)
                arr_DO2_combo[i] = DO2_combo.GetItemText(DO2_combo.Items[i]);
            json.comboSignalsItems.Add(DO2_combo.Name, arr_DO2_combo);
            // DO3, ПЛК
            string[] arr_DO3_combo = new string[DO3_combo.Items.Count];
            for (int i = 0; i < DO3_combo.Items.Count; i++)
                arr_DO3_combo[i] = DO3_combo.GetItemText(DO3_combo.Items[i]);
            json.comboSignalsItems.Add(DO3_combo.Name, arr_DO3_combo);
            // DO4, ПЛК
            string[] arr_DO4_combo = new string[DO4_combo.Items.Count];
            for (int i = 0; i < DO4_combo.Items.Count; i++)
                arr_DO4_combo[i] = DO4_combo.GetItemText(DO4_combo.Items[i]);
            json.comboSignalsItems.Add(DO4_combo.Name, arr_DO4_combo);
            // DO5, ПЛК
            string[] arr_DO5_combo = new string[DO5_combo.Items.Count];
            for (int i = 0; i < DO5_combo.Items.Count; i++)
                arr_DO5_combo[i] = DO5_combo.GetItemText(DO5_combo.Items[i]);
            json.comboSignalsItems.Add(DO5_combo.Name, arr_DO5_combo);
            // DO6, ПЛК
            string[] arr_DO6_combo = new string[DO6_combo.Items.Count];
            for (int i = 0; i < DO6_combo.Items.Count; i++)
                arr_DO6_combo[i] = DO6_combo.GetItemText(DO6_combo.Items[i]);
            json.comboSignalsItems.Add(DO6_combo.Name, arr_DO6_combo);
            // DO1, блок 1
            string[] arr_DO1bl1_combo = new string[DO1bl1_combo.Items.Count];
            for (int i = 0; i < DO1bl1_combo.Items.Count; i++)
                arr_DO1bl1_combo[i] = DO1bl1_combo.GetItemText(DO1bl1_combo.Items[i]);
            json.comboSignalsItems.Add(DO1bl1_combo.Name, arr_DO1bl1_combo);
            // DO2, блок 1
            string[] arr_DO2bl1_combo = new string[DO2bl1_combo.Items.Count];
            for (int i = 0; i < DO2bl1_combo.Items.Count; i++)
                arr_DO2bl1_combo[i] = DO2bl1_combo.GetItemText(DO2bl1_combo.Items[i]);
            json.comboSignalsItems.Add(DO2bl1_combo.Name, arr_DO2bl1_combo);
            // DO3, блок 1
            string[] arr_DO3bl1_combo = new string[DO3bl1_combo.Items.Count];
            for (int i = 0; i < DO3bl1_combo.Items.Count; i++)
                arr_DO3bl1_combo[i] = DO3bl1_combo.GetItemText(DO3bl1_combo.Items[i]);
            json.comboSignalsItems.Add(DO3bl1_combo.Name, arr_DO3bl1_combo);
            // DO4, блок 1
            string[] arr_DO4bl1_combo = new string[DO4bl1_combo.Items.Count];
            for (int i = 0; i < DO4bl1_combo.Items.Count; i++)
                arr_DO4bl1_combo[i] = DO4bl1_combo.GetItemText(DO4bl1_combo.Items[i]);
            json.comboSignalsItems.Add(DO4bl1_combo.Name, arr_DO4bl1_combo);
            // DO5, блок 1
            string[] arr_DO5bl1_combo = new string[DO5bl1_combo.Items.Count];
            for (int i = 0; i < DO5bl1_combo.Items.Count; i++)
                arr_DO5bl1_combo[i] = DO5bl1_combo.GetItemText(DO5bl1_combo.Items[i]);
            json.comboSignalsItems.Add(DO5bl1_combo.Name, arr_DO5bl1_combo);
            // DO6, блок 1
            string[] arr_DO6bl1_combo = new string[DO6bl1_combo.Items.Count];
            for (int i = 0; i < DO6bl1_combo.Items.Count; i++)
                arr_DO6bl1_combo[i] = DO6bl1_combo.GetItemText(DO6bl1_combo.Items[i]);
            json.comboSignalsItems.Add(DO6bl1_combo.Name, arr_DO6bl1_combo);
            // DO7, блок 1
            string[] arr_DO7bl1_combo = new string[DO7bl1_combo.Items.Count];
            for (int i = 0; i < DO7bl1_combo.Items.Count; i++)
                arr_DO7bl1_combo[i] = DO7bl1_combo.GetItemText(DO7bl1_combo.Items[i]);
            json.comboSignalsItems.Add(DO7bl1_combo.Name, arr_DO7bl1_combo);
            // DO1, блок 2
            string[] arr_DO1bl2_combo = new string[DO1bl2_combo.Items.Count];
            for (int i = 0; i < DO1bl2_combo.Items.Count; i++)
                arr_DO1bl2_combo[i] = DO1bl2_combo.GetItemText(DO1bl2_combo.Items[i]);
            json.comboSignalsItems.Add(DO1bl2_combo.Name, arr_DO1bl2_combo);
            // DO2, блок 2
            string[] arr_DO2bl2_combo = new string[DO2bl2_combo.Items.Count];
            for (int i = 0; i < DO2bl2_combo.Items.Count; i++)
                arr_DO2bl2_combo[i] = DO2bl2_combo.GetItemText(DO2bl2_combo.Items[i]);
            json.comboSignalsItems.Add(DO2bl2_combo.Name, arr_DO2bl2_combo);
            // DO3, блок 2
            string[] arr_DO3bl2_combo = new string[DO3bl2_combo.Items.Count];
            for (int i = 0; i < DO3bl2_combo.Items.Count; i++)
                arr_DO3bl2_combo[i] = DO3bl2_combo.GetItemText(DO3bl2_combo.Items[i]);
            json.comboSignalsItems.Add(DO3bl2_combo.Name, arr_DO3bl2_combo);
            // DO4, блок 2
            string[] arr_DO4bl2_combo = new string[DO4bl2_combo.Items.Count];
            for (int i = 0; i < DO4bl2_combo.Items.Count; i++)
                arr_DO4bl2_combo[i] = DO4bl2_combo.GetItemText(DO4bl2_combo.Items[i]);
            json.comboSignalsItems.Add(DO4bl2_combo.Name, arr_DO4bl2_combo);
            // DO5, блок 2
            string[] arr_DO5bl2_combo = new string[DO5bl2_combo.Items.Count];
            for (int i = 0; i < DO5bl2_combo.Items.Count; i++)
                arr_DO5bl2_combo[i] = DO5bl2_combo.GetItemText(DO5bl2_combo.Items[i]);
            json.comboSignalsItems.Add(DO5bl2_combo.Name, arr_DO5bl2_combo);
            // DO6, блок 2
            string[] arr_DO6bl2_combo = new string[DO6bl2_combo.Items.Count];
            for (int i = 0; i < DO6bl2_combo.Items.Count; i++)
                arr_DO6bl2_combo[i] = DO6bl2_combo.GetItemText(DO6bl2_combo.Items[i]);
            json.comboSignalsItems.Add(DO6bl2_combo.Name, arr_DO6bl2_combo);
            // DO7, блок 2
            string[] arr_DO7bl2_combo = new string[DO7bl2_combo.Items.Count];
            for (int i = 0; i < DO7bl2_combo.Items.Count; i++)
                arr_DO7bl2_combo[i] = DO7bl2_combo.GetItemText(DO7bl2_combo.Items[i]);
            json.comboSignalsItems.Add(DO7bl2_combo.Name, arr_DO7bl2_combo);
            // DO1, блок 3
            string[] arr_DO1bl3_combo = new string[DO1bl3_combo.Items.Count];
            for (int i = 0; i < DO1bl3_combo.Items.Count; i++)
                arr_DO1bl3_combo[i] = DO1bl3_combo.GetItemText(DO1bl3_combo.Items[i]);
            json.comboSignalsItems.Add(DO1bl3_combo.Name, arr_DO1bl3_combo);
            // DO2, блок 3
            string[] arr_DO2bl3_combo = new string[DO2bl3_combo.Items.Count];
            for (int i = 0; i < DO2bl3_combo.Items.Count; i++)
                arr_DO2bl3_combo[i] = DO2bl3_combo.GetItemText(DO2bl3_combo.Items[i]);
            json.comboSignalsItems.Add(DO2bl3_combo.Name, arr_DO2bl3_combo);
            // DO3, блок 3
            string[] arr_DO3bl3_combo = new string[DO3bl3_combo.Items.Count];
            for (int i = 0; i < DO3bl3_combo.Items.Count; i++)
                arr_DO3bl3_combo[i] = DO3bl3_combo.GetItemText(DO3bl3_combo.Items[i]);
            json.comboSignalsItems.Add(DO3bl3_combo.Name, arr_DO3bl3_combo);
            // DO4, блок 3
            string[] arr_DO4bl3_combo = new string[DO4bl3_combo.Items.Count];
            for (int i = 0; i < DO4bl3_combo.Items.Count; i++)
                arr_DO4bl3_combo[i] = DO4bl3_combo.GetItemText(DO4bl3_combo.Items[i]);
            json.comboSignalsItems.Add(DO4bl3_combo.Name, arr_DO4bl3_combo);
            // DO5, блок 3
            string[] arr_DO5bl3_combo = new string[DO5bl3_combo.Items.Count];
            for (int i = 0; i < DO5bl3_combo.Items.Count; i++)
                arr_DO5bl3_combo[i] = DO5bl3_combo.GetItemText(DO5bl3_combo.Items[i]);
            json.comboSignalsItems.Add(DO5bl3_combo.Name, arr_DO5bl3_combo);
            // DO6, блок 3
            string[] arr_DO6bl3_combo = new string[DO6bl3_combo.Items.Count];
            for (int i = 0; i < DO6bl3_combo.Items.Count; i++)
                arr_DO6bl3_combo[i] = DO6bl3_combo.GetItemText(DO6bl3_combo.Items[i]);
            json.comboSignalsItems.Add(DO6bl3_combo.Name, arr_DO6bl3_combo);
            // DO7, блок 3
            string[] arr_DO7bl3_combo = new string[DO7bl3_combo.Items.Count];
            for (int i = 0; i < DO7bl3_combo.Items.Count; i++)
                arr_DO7bl3_combo[i] = DO7bl3_combo.GetItemText(DO7bl3_combo.Items[i]);
            json.comboSignalsItems.Add(DO7bl3_combo.Name, arr_DO7bl3_combo);
        }
    }
}