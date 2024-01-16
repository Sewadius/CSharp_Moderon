using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

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
        public Dictionary<string, bool> checkBoxState { get; set; }                 // Состояние всех checkBox

        [JsonProperty("comboBoxElemState")]
        public Dictionary<string, int> comboBoxElemState { get; set; }              // Состояние всех comboBox элементов

        [JsonProperty("textBoxElemState")]
        public Dictionary<string, string> textBoxElemState { get; set; }            // Значение для textBox элементов

        [JsonProperty("labelSignalsState")]
        public Dictionary<string, string> labelSignalsState { get; set; }           // Значение для label таблицы сигналов

        [JsonProperty("comboSignalsItems")]
        public Dictionary<string, string[]> comboSignalsItems { get; set; }         // Элементы для comboBox таблицы сигналов

        [JsonProperty("comboAITypeItems")]
        public Dictionary<string, string[]> comboAITypeItems { get; set; }          // Элементы для comboBox типов AI сигналов

        [JsonProperty("comboSignalsState")]
        public Dictionary<string, string> comboSignalsState { get; set; }           // Значение для comboBox таблицы сигналов

        [JsonProperty("comboAITypeState")]
        public Dictionary<string, string> comboAITypeState { get; set; }            // Значение для comboBox типа сигнала AI

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

        [JsonProperty("diCode")]
        public Dictionary<string, ushort> diCode { get; set; }                      // Словарь для кодов сигналов DI

        [JsonProperty("diActive")]
        public Dictionary<string, bool> diActive { get; set; }                      // Словарь для активности сигналов DI

        [JsonProperty("doCode")]
        public Dictionary<string, ushort> doCode { get; set; }                      // Словарь для кодов сигналов DO

        [JsonProperty("doActive")]
        public Dictionary<string, bool> doActive { get; set; }                      // Словарь для активности сигналов DO

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
            uiCode = new Dictionary<string, ushort>();
            uiType = new Dictionary<string, string>();
            uiActive = new Dictionary<string, bool>();
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
        JsonObject json;                                // Объект для сохранения файла
        JsonObject json_read;                           // Объект для загрузки файла
        bool ignoreEvents = false;                      // Игнорирование событий для элементов

        ///<summary>Нажали "Сохранить" в главном меню</summary> 
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            json = new JsonObject();
            BuildCheckBoxAll();                         // Сохранение для всех элементов checkBox
            BuildComboBoxElemAll();                     // Сохранение для всех элементов comboBox
            BuildTextBoxAll();                          // Сохранение для всех элементов textBox
            BuildLabelSignalsAll();                     // Сохранение для подписей кодов таблицы сигналов
            BuildComboItemsSignals();                   // Сохранение элементов comboBox таблицы сигналов
            BuildComboSignalsAll();                     // Сохранение выбранного элемента для comboBox таблицы сигналов
            BuildSignalArrays();                        // Сохранение для перечня сигналов, массивы
            SaveJsonFile();                             // Сохранение файла
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
                File.WriteAllBytes(dlg.FileName, File.ReadAllBytes(tempPath));
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
            pumpCurProtect.Checked = json_read.checkBoxState[pumpCurProtect.Name];
            reservPumpHeater.Checked = json_read.checkBoxState[reservPumpHeater.Name];
            confHeatResPumpCheck.Checked = json_read.checkBoxState[confHeatResPumpCheck.Name];
            pumpCurResProtect.Checked = json_read.checkBoxState[pumpCurResProtect.Name];
            watSensHeatCheck.Checked = json_read.checkBoxState[watSensHeatCheck.Name];
            // Второй нагреватель
            TF_addHeaterCheck.Checked = json_read.checkBoxState[TF_addHeaterCheck.Name];
            pumpAddHeatCheck.Checked = json_read.checkBoxState[pumpAddHeatCheck.Name];
            confAddHeatPumpCheck.Checked = json_read.checkBoxState[confAddHeatPumpCheck.Name];
            pumpCurAddProtect.Checked = json_read.checkBoxState[pumpCurAddProtect.Name];
            reservPumpAddHeater.Checked = json_read.checkBoxState[reservPumpAddHeater.Name];
            confAddHeatResPumpCheck.Checked = json_read.checkBoxState[confAddHeatResPumpCheck.Name];
            pumpCurResAddProtect.Checked = json_read.checkBoxState[pumpCurResAddProtect.Name];
            sensWatAddHeatCheck.Checked = json_read.checkBoxState[sensWatAddHeatCheck.Name];
            // Охладитель
            alarmFrCoolCheck.Checked = json_read.checkBoxState[alarmFrCoolCheck.Name];
            thermoCoolerCheck.Checked = json_read.checkBoxState[thermoCoolerCheck.Name];
            analogFreonCheck.Checked = json_read.checkBoxState[analogFreonCheck.Name];
            dehumModeCheck.Checked = json_read.checkBoxState[dehumModeCheck.Name];
            // Увлажнитель
            alarmHumidCheck.Checked = json_read.checkBoxState[alarmHumidCheck.Name];
            // Рециркуляция
            recircPrDampAOCheck.Checked = json_read.checkBoxState[recircPrDampAOCheck.Name];
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
            fireCheck.Checked = json_read.checkBoxState[fireCheck.Name];
            // Приточный вентилятор
            prFanPSCheck.Checked = json_read.checkBoxState[prFanPSCheck.Name];
            prFanFC_check.Checked = json_read.checkBoxState[prFanFC_check.Name];
            prFanThermoCheck.Checked = json_read.checkBoxState[prFanThermoCheck.Name];
            curDefPrFanCheck.Checked = json_read.checkBoxState[curDefPrFanCheck.Name];
            checkResPrFan.Checked = json_read.checkBoxState[checkResPrFan.Name];
            prDampFanCheck.Checked = json_read.checkBoxState[prDampFanCheck.Name];
            prDampConfirmFanCheck.Checked = json_read.checkBoxState[prDampConfirmFanCheck.Name];
            prFanAlarmCheck.Checked = json_read.checkBoxState[prFanAlarmCheck.Name];
            prFanStStopCheck.Checked = json_read.checkBoxState[prFanStStopCheck.Name];
            prFanSpeedCheck.Checked = json_read.checkBoxState[prFanSpeedCheck.Name];
            // Вытяжной вентилятор
            outFanPSCheck.Checked = json_read.checkBoxState[outFanPSCheck.Name];
            outFanFC_check.Checked = json_read.checkBoxState[outFanFC_check.Name];
            outFanThermoCheck.Checked = json_read.checkBoxState[outFanThermoCheck.Name];
            curDefOutFanCheck.Checked = json_read.checkBoxState[curDefOutFanCheck.Name];
            checkResOutFan.Checked = json_read.checkBoxState[checkResOutFan.Name];
            outDampFanCheck.Checked = json_read.checkBoxState[outDampFanCheck.Name];
            outDampConfirmFanCheck.Checked = json_read.checkBoxState[outDampConfirmFanCheck.Name];
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
            // Датчики
            fireTypeCombo.SelectedIndex = json_read.comboBoxElemState[fireTypeCombo.Name];
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
                TF_addHeaterCheck, pumpAddHeatCheck, confAddHeatPumpCheck, reservPumpAddHeater, confAddHeatResPumpCheck, pumpCurResAddProtect, 
                sensWatAddHeatCheck,
                // Охладитель и увлажнитель
                alarmFrCoolCheck, thermoCoolerCheck, analogFreonCheck, dehumModeCheck, alarmHumidCheck,
                // Рециркуляция и рекуператор
                recircPrDampAOCheck, springRetRecircCheck, pumpGlicRecCheck, recDefTempCheck, recDefPsCheck,
                // Датчики и сигналы
                recDefPsCheck, roomTempSensCheck, chanHumSensCheck, roomHumSensCheck, outdoorChanSensCheck, outChanSensCheck, sigWorkCheck,
                sigAlarmCheck, sigFilAlarmCheck, stopStartCheck, fireCheck,
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

            foreach (var el in ui_combos) AddComboSignalsState(el);
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