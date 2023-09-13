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
            BuildComboTypeAIitems(); // Сохранение элементов comboBox для типов сигналов AI
            BuildComboSignalsAll(); // Сохранение выбранного элемента для comboBox таблицы сигналов
            BuildComboTypeAIAll(); // Сохранение выбранного элемента для comboBox типа сигнала AI
            BuildSignalArrays(); // Сохранение для перечня сигналов, массивы
            SaveJsonFile(); // Сохранение файла
        }

        ///<summary>Перенос перечня сигналов, массивы</summary>
        private void BuildSignalArrays()
        {
            // Значения полей для сигналов AI
            for (int i = 0; i < list_ai.Count; i++)
            {
                json.aiCode.Add(list_ai[i].Name, list_ai[i].Code);
                json.aiType.Add(list_ai[i].Name, list_ai[i].Type);
                json.aiActive.Add(list_ai[i].Name, list_ai[i].Active);
            }
            // Значения полей для сигналов AO
            for (int i = 0; i < list_ao.Count; i++)
            {
                json.aoCode.Add(list_ao[i].Name, list_ao[i].Code);
                json.aoActive.Add(list_ao[i].Name, list_ao[i].Active);
            }
            // Значения полей для сигналов DI
            for (int i = 0; i < list_di.Count; i++)
            {
                json.diCode.Add(list_di[i].Name, list_di[i].Code);
                json.diActive.Add(list_di[i].Name, list_di[i].Active);
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
            LoadJsonFile(); // Загрузка файла JSON в программу
            ignoreEvents = true; // Временное отключение событий
            LoadCheckBoxAll(); // Загрузка для всех сheckBox
            LoadComboBoxElemAll(); // Загрузка для всех comboBox элементов
            LoadTextBoxAll(); // Загрузка для всех textBox элементов
            LoadLabelSignalsAll(); // Загрузка для подписей кодов таблицы сигналов
            LoadComboItemsSignals(); // Загрузка элементов для comboBox таблицы сигналов
            LoadComboTypeAIitems(); // Загрузка элементов для comboBox AI type
            LoadComboSignalsAll(); // Загрузка состояний для comboBox таблицы сигналов
            LoadComboTypeAIAll(); // Загрузка выбранного элемента для comboBox AI type
            LoadSignalArrays(); // Загрузка для массива сигналов
            ignoreEvents = false; // Возврат активации событий
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
            prFanPowSupCheck.Checked = json_read.checkBoxState[prFanPowSupCheck.Name];
            prFanAlarmCheck.Checked = json_read.checkBoxState[prFanAlarmCheck.Name];
            prFanStStopCheck.Checked = json_read.checkBoxState[prFanStStopCheck.Name];
            prFanSpeedCheck.Checked = json_read.checkBoxState[prFanSpeedCheck.Name];
            // Вытяжной вентилятор
            outFanPSCheck.Checked = json_read.checkBoxState[outFanPSCheck.Name];
            outFanFC_check.Checked = json_read.checkBoxState[outFanFC_check.Name];
            outFanThermoCheck.Checked = json_read.checkBoxState[outFanThermoCheck.Name];
            curDefOutFanCheck.Checked = json_read.checkBoxState[curDefOutFanCheck.Name];
            checkResOutFan.Checked = json_read.checkBoxState[checkResOutFan.Name];
            outFanPowSupCheck.Checked = json_read.checkBoxState[outFanPowSupCheck.Name];
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
            // AI сигналы, ПЛК
            AI1_lab.Text = json_read.labelSignalsState[AI1_lab.Name];
            AI2_lab.Text = json_read.labelSignalsState[AI2_lab.Name];
            AI3_lab.Text = json_read.labelSignalsState[AI3_lab.Name];
            AI4_lab.Text = json_read.labelSignalsState[AI4_lab.Name];
            AI5_lab.Text = json_read.labelSignalsState[AI5_lab.Name];
            AI6_lab.Text = json_read.labelSignalsState[AI6_lab.Name];
            // AI сигналы, блок 1
            AI1bl1_lab.Text = json_read.labelSignalsState[AI1bl1_lab.Name];
            AI2bl1_lab.Text = json_read.labelSignalsState[AI2bl1_lab.Name];
            AI3bl1_lab.Text = json_read.labelSignalsState[AI3bl1_lab.Name];
            AI4bl1_lab.Text = json_read.labelSignalsState[AI4bl1_lab.Name];
            AI5bl1_lab.Text = json_read.labelSignalsState[AI5bl1_lab.Name];
            AI6bl1_lab.Text = json_read.labelSignalsState[AI6bl1_lab.Name];
            // AI сигналы, блок 2
            AI1bl2_lab.Text = json_read.labelSignalsState[AI1bl2_lab.Name];
            AI2bl2_lab.Text = json_read.labelSignalsState[AI2bl2_lab.Name];
            AI3bl2_lab.Text = json_read.labelSignalsState[AI3bl2_lab.Name];
            AI4bl2_lab.Text = json_read.labelSignalsState[AI4bl2_lab.Name];
            AI5bl2_lab.Text = json_read.labelSignalsState[AI5bl2_lab.Name];
            AI6bl2_lab.Text = json_read.labelSignalsState[AI6bl2_lab.Name];
            // AI сигналы, блок 3
            AI1bl3_lab.Text = json_read.labelSignalsState[AI1bl3_lab.Name];
            AI2bl3_lab.Text = json_read.labelSignalsState[AI2bl3_lab.Name];
            AI3bl3_lab.Text = json_read.labelSignalsState[AI3bl3_lab.Name];
            AI4bl3_lab.Text = json_read.labelSignalsState[AI4bl3_lab.Name];
            AI5bl3_lab.Text = json_read.labelSignalsState[AI5bl3_lab.Name];
            AI6bl3_lab.Text = json_read.labelSignalsState[AI6bl3_lab.Name];
            // DI сигналы, ПЛК
            DI1_lab.Text = json_read.labelSignalsState[DI1_lab.Name];
            DI2_lab.Text = json_read.labelSignalsState[DI2_lab.Name];
            DI3_lab.Text = json_read.labelSignalsState[DI3_lab.Name];
            DI4_lab.Text = json_read.labelSignalsState[DI4_lab.Name];
            DI5_lab.Text = json_read.labelSignalsState[DI5_lab.Name];
            // DI сигналы, блок 1
            DI1bl1_lab.Text = json_read.labelSignalsState[DI1bl1_lab.Name];
            DI2bl1_lab.Text = json_read.labelSignalsState[DI2bl1_lab.Name];
            DI3bl1_lab.Text = json_read.labelSignalsState[DI3bl1_lab.Name];
            DI4bl1_lab.Text = json_read.labelSignalsState[DI4bl1_lab.Name];
            DI5bl1_lab.Text = json_read.labelSignalsState[DI5bl1_lab.Name];
            // DI сигналы, блок 2
            DI1bl2_lab.Text = json_read.labelSignalsState[DI1bl2_lab.Name];
            DI2bl2_lab.Text = json_read.labelSignalsState[DI2bl2_lab.Name];
            DI3bl2_lab.Text = json_read.labelSignalsState[DI3bl2_lab.Name];
            DI4bl2_lab.Text = json_read.labelSignalsState[DI4bl2_lab.Name];
            DI5bl2_lab.Text = json_read.labelSignalsState[DI5bl2_lab.Name];
            // DI сигналы, блок 3
            DI1bl3_lab.Text = json_read.labelSignalsState[DI1bl3_lab.Name];
            DI2bl3_lab.Text = json_read.labelSignalsState[DI2bl3_lab.Name];
            DI3bl3_lab.Text = json_read.labelSignalsState[DI3bl3_lab.Name];
            DI4bl3_lab.Text = json_read.labelSignalsState[DI4bl3_lab.Name];
            DI5bl3_lab.Text = json_read.labelSignalsState[DI5bl3_lab.Name];
            // AO сигналы, ПЛК
            AO1_lab.Text = json_read.labelSignalsState[AO1_lab.Name];
            AO2_lab.Text = json_read.labelSignalsState[AO2_lab.Name];
            AO3_lab.Text = json_read.labelSignalsState[AO3_lab.Name];
            // AO сигналы, блок 1
            AO1bl1_lab.Text = json_read.labelSignalsState[AO1bl1_lab.Name];
            AO2bl1_lab.Text = json_read.labelSignalsState[AO2bl1_lab.Name];
            AO3bl1_lab.Text = json_read.labelSignalsState[AO3bl1_lab.Name];
            // AO сигналы, блок 2
            AO1bl2_lab.Text = json_read.labelSignalsState[AO1bl2_lab.Name];
            AO2bl2_lab.Text = json_read.labelSignalsState[AO2bl2_lab.Name];
            AO3bl2_lab.Text = json_read.labelSignalsState[AO3bl2_lab.Name];
            // AO сигналы, блок 3
            AO1bl3_lab.Text = json_read.labelSignalsState[AO1bl3_lab.Name];
            AO2bl3_lab.Text = json_read.labelSignalsState[AO2bl3_lab.Name];
            AO3bl3_lab.Text = json_read.labelSignalsState[AO3bl3_lab.Name];
            // DO сигналы, ПЛК
            DO1_lab.Text = json_read.labelSignalsState[DO1_lab.Name];
            DO2_lab.Text = json_read.labelSignalsState[DO2_lab.Name];
            DO3_lab.Text = json_read.labelSignalsState[DO3_lab.Name];
            DO4_lab.Text = json_read.labelSignalsState[DO4_lab.Name];
            DO5_lab.Text = json_read.labelSignalsState[DO5_lab.Name];
            DO6_lab.Text = json_read.labelSignalsState[DO6_lab.Name];
            DO7_lab.Text = json_read.labelSignalsState[DO7_lab.Name];
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
            // AI сигналы, ПЛК
            AI1_combo.Items.Clear(); AI2_combo.Items.Clear(); AI3_combo.Items.Clear();
            AI4_combo.Items.Clear(); AI5_combo.Items.Clear(); AI6_combo.Items.Clear();
            // AI1, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[AI1_combo.Name].Length; i++)
                AI1_combo.Items.Add(json_read.comboSignalsItems[AI1_combo.Name][i]);
            // AI2, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[AI2_combo.Name].Length; i++)
                AI2_combo.Items.Add(json_read.comboSignalsItems[AI2_combo.Name][i]);
            // AI3, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[AI3_combo.Name].Length; i++)
                AI3_combo.Items.Add(json_read.comboSignalsItems[AI3_combo.Name][i]);
            // AI4, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[AI4_combo.Name].Length; i++)
                AI4_combo.Items.Add(json_read.comboSignalsItems[AI4_combo.Name][i]);
            // AI5, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[AI5_combo.Name].Length; i++)
                AI5_combo.Items.Add(json_read.comboSignalsItems[AI5_combo.Name][i]);
            // AI6, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[AI6_combo.Name].Length; i++)
                AI6_combo.Items.Add(json_read.comboSignalsItems[AI6_combo.Name][i]);
            // AI сигналы, блок 1
            AI1bl1_combo.Items.Clear(); AI2bl1_combo.Items.Clear(); AI3bl1_combo.Items.Clear();
            AI4bl1_combo.Items.Clear(); AI5bl1_combo.Items.Clear(); AI6bl1_combo.Items.Clear();
            // AI1, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[AI1bl1_combo.Name].Length; i++)
                AI1bl1_combo.Items.Add(json_read.comboSignalsItems[AI1bl1_combo.Name][i]);
            // AI2, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[AI2bl1_combo.Name].Length; i++)
                AI2bl1_combo.Items.Add(json_read.comboSignalsItems[AI2bl1_combo.Name][i]);
            // AI3, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[AI3bl1_combo.Name].Length; i++)
                AI3bl1_combo.Items.Add(json_read.comboSignalsItems[AI3bl1_combo.Name][i]);
            // AI4, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[AI4bl1_combo.Name].Length; i++)
                AI4bl1_combo.Items.Add(json_read.comboSignalsItems[AI4bl1_combo.Name][i]);
            // AI5, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[AI5bl1_combo.Name].Length; i++)
                AI5bl1_combo.Items.Add(json_read.comboSignalsItems[AI5bl1_combo.Name][i]);
            // AI6, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[AI6bl1_combo.Name].Length; i++)
                AI6bl1_combo.Items.Add(json_read.comboSignalsItems[AI6bl1_combo.Name][i]);
            // AI сигналы, блок 2
            AI1bl2_combo.Items.Clear(); AI2bl2_combo.Items.Clear(); AI3bl2_combo.Items.Clear();
            AI4bl2_combo.Items.Clear(); AI5bl2_combo.Items.Clear(); AI6bl2_combo.Items.Clear();
            // AI1, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[AI1bl2_combo.Name].Length; i++)
                AI1bl2_combo.Items.Add(json_read.comboSignalsItems[AI1bl2_combo.Name][i]);
            // AI2, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[AI2bl2_combo.Name].Length; i++)
                AI2bl2_combo.Items.Add(json_read.comboSignalsItems[AI2bl2_combo.Name][i]);
            // AI3, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[AI3bl2_combo.Name].Length; i++)
                AI3bl2_combo.Items.Add(json_read.comboSignalsItems[AI3bl2_combo.Name][i]);
            // AI4, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[AI4bl2_combo.Name].Length; i++)
                AI4bl2_combo.Items.Add(json_read.comboSignalsItems[AI4bl2_combo.Name][i]);
            // AI5, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[AI5bl2_combo.Name].Length; i++)
                AI5bl2_combo.Items.Add(json_read.comboSignalsItems[AI5bl2_combo.Name][i]);
            // AI6, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[AI6bl2_combo.Name].Length; i++)
                AI6bl2_combo.Items.Add(json_read.comboSignalsItems[AI6bl2_combo.Name][i]);
            // AI сигналы, блок 3
            AI1bl3_combo.Items.Clear(); AI2bl3_combo.Items.Clear(); AI3bl3_combo.Items.Clear();
            AI4bl3_combo.Items.Clear(); AI5bl3_combo.Items.Clear(); AI6bl3_combo.Items.Clear();
            // AI1, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[AI1bl3_combo.Name].Length; i++)
                AI1bl3_combo.Items.Add(json_read.comboSignalsItems[AI1bl3_combo.Name][i]);
            // AI2, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[AI2bl3_combo.Name].Length; i++)
                AI2bl3_combo.Items.Add(json_read.comboSignalsItems[AI2bl3_combo.Name][i]);
            // AI3, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[AI3bl3_combo.Name].Length; i++)
                AI3bl3_combo.Items.Add(json_read.comboSignalsItems[AI3bl3_combo.Name][i]);
            // AI4, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[AI4bl3_combo.Name].Length; i++)
                AI4bl3_combo.Items.Add(json_read.comboSignalsItems[AI4bl3_combo.Name][i]);
            // AI5, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[AI5bl3_combo.Name].Length; i++)
                AI5bl3_combo.Items.Add(json_read.comboSignalsItems[AI5bl3_combo.Name][i]);
            // AI6, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[AI6bl3_combo.Name].Length; i++)
                AI6bl3_combo.Items.Add(json_read.comboSignalsItems[AI6bl3_combo.Name][i]);
            // DI сигналы, ПЛК
            DI1_combo.Items.Clear(); DI2_combo.Items.Clear(); DI3_combo.Items.Clear();
            DI4_combo.Items.Clear(); DI5_combo.Items.Clear();
            // DI1, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[DI1_combo.Name].Length; i++)
                DI1_combo.Items.Add(json_read.comboSignalsItems[DI1_combo.Name][i]);
            // DI2, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[DI2_combo.Name].Length; i++)
                DI2_combo.Items.Add(json_read.comboSignalsItems[DI2_combo.Name][i]);
            // DI3, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[DI3_combo.Name].Length; i++)
                DI3_combo.Items.Add(json_read.comboSignalsItems[DI3_combo.Name][i]);
            // DI4, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[DI4_combo.Name].Length; i++)
                DI4_combo.Items.Add(json_read.comboSignalsItems[DI4_combo.Name][i]);
            // DI5, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[DI5_combo.Name].Length; i++)
                DI5_combo.Items.Add(json_read.comboSignalsItems[DI5_combo.Name][i]);
            // DI сигналы, блок 1
            DI1bl1_combo.Items.Clear(); DI2bl1_combo.Items.Clear(); DI3bl1_combo.Items.Clear();
            DI4bl1_combo.Items.Clear(); DI5bl1_combo.Items.Clear();
            // DI1, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[DI1bl1_combo.Name].Length; i++)
                DI1bl1_combo.Items.Add(json_read.comboSignalsItems[DI1bl1_combo.Name][i]);
            // DI2, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[DI2bl1_combo.Name].Length; i++)
                DI2bl1_combo.Items.Add(json_read.comboSignalsItems[DI2bl1_combo.Name][i]);
            // DI3, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[DI3bl1_combo.Name].Length; i++)
                DI3bl1_combo.Items.Add(json_read.comboSignalsItems[DI3bl1_combo.Name][i]);
            // DI4, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[DI4bl1_combo.Name].Length; i++)
                DI4bl1_combo.Items.Add(json_read.comboSignalsItems[DI4bl1_combo.Name][i]);
            // DI5, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[DI5bl1_combo.Name].Length; i++)
                DI5bl1_combo.Items.Add(json_read.comboSignalsItems[DI5bl1_combo.Name][i]);
            // DI сигналы, блок 2
            DI1bl2_combo.Items.Clear(); DI2bl2_combo.Items.Clear(); DI3bl2_combo.Items.Clear();
            DI4bl2_combo.Items.Clear(); DI5bl2_combo.Items.Clear();
            // DI1, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[DI1bl2_combo.Name].Length; i++)
                DI1bl2_combo.Items.Add(json_read.comboSignalsItems[DI1bl2_combo.Name][i]);
            // DI2, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[DI2bl2_combo.Name].Length; i++)
                DI2bl2_combo.Items.Add(json_read.comboSignalsItems[DI2bl2_combo.Name][i]);
            // DI3, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[DI3bl2_combo.Name].Length; i++)
                DI3bl2_combo.Items.Add(json_read.comboSignalsItems[DI3bl2_combo.Name][i]);
            // DI4, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[DI4bl2_combo.Name].Length; i++)
                DI4bl2_combo.Items.Add(json_read.comboSignalsItems[DI4bl2_combo.Name][i]);
            // DI5, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[DI5bl2_combo.Name].Length; i++)
                DI5bl2_combo.Items.Add(json_read.comboSignalsItems[DI5bl2_combo.Name][i]);
            // DI сигналы, блок 3
            DI1bl3_combo.Items.Clear(); DI2bl3_combo.Items.Clear(); DI3bl3_combo.Items.Clear();
            DI4bl3_combo.Items.Clear(); DI5bl3_combo.Items.Clear();
            // DI1, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[DI1bl3_combo.Name].Length; i++)
                DI1bl3_combo.Items.Add(json_read.comboSignalsItems[DI1bl3_combo.Name][i]);
            // DI2, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[DI2bl3_combo.Name].Length; i++)
                DI2bl3_combo.Items.Add(json_read.comboSignalsItems[DI2bl3_combo.Name][i]);
            // DI3, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[DI3bl3_combo.Name].Length; i++)
                DI3bl3_combo.Items.Add(json_read.comboSignalsItems[DI3bl3_combo.Name][i]);
            // DI4, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[DI4bl3_combo.Name].Length; i++)
                DI4bl3_combo.Items.Add(json_read.comboSignalsItems[DI4bl3_combo.Name][i]);
            // DI5, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[DI5bl3_combo.Name].Length; i++)
                DI5bl3_combo.Items.Add(json_read.comboSignalsItems[DI5bl3_combo.Name][i]);
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
            AO1bl1_combo.Items.Clear(); AO2bl1_combo.Items.Clear(); AO3bl1_combo.Items.Clear();
            // AO1, блок 1
            for (int i = 0; i < json_read.comboSignalsItems[AO1bl1_combo.Name].Length; i++)
                AO1bl1_combo.Items.Add(json_read.comboSignalsItems[AO1bl1_combo.Name][i]);
            // AO1, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[AO2bl1_combo.Name].Length; i++)
                AO2bl1_combo.Items.Add(json_read.comboSignalsItems[AO2bl1_combo.Name][i]);
            // AO1, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[AO3bl1_combo.Name].Length; i++)
                AO3bl1_combo.Items.Add(json_read.comboSignalsItems[AO3bl1_combo.Name][i]);
            // AO сигналы, блок 2
            AO1bl2_combo.Items.Clear(); AO2bl2_combo.Items.Clear(); AO3bl2_combo.Items.Clear();
            // AO1, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[AO1bl2_combo.Name].Length; i++)
                AO1bl2_combo.Items.Add(json_read.comboSignalsItems[AO1bl2_combo.Name][i]);
            // AO2, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[AO2bl2_combo.Name].Length; i++)
                AO2bl2_combo.Items.Add(json_read.comboSignalsItems[AO2bl2_combo.Name][i]);
            // AO3, блок 2
            for (int i = 0; i < json_read.comboSignalsItems[AO3bl2_combo.Name].Length; i++)
                AO3bl2_combo.Items.Add(json_read.comboSignalsItems[AO3bl2_combo.Name][i]);
            // AO сигналы, блок 3
            AO1bl3_combo.Items.Clear(); AO2bl3_combo.Items.Clear(); AO3bl3_combo.Items.Clear();
            // AO1, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[AO1bl3_combo.Name].Length; i++)
                AO1bl3_combo.Items.Add(json_read.comboSignalsItems[AO1bl3_combo.Name][i]);
            // AO2, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[AO2bl3_combo.Name].Length; i++)
                AO2bl3_combo.Items.Add(json_read.comboSignalsItems[AO2bl3_combo.Name][i]);
            // AO3, блок 3
            for (int i = 0; i < json_read.comboSignalsItems[AO3bl3_combo.Name].Length; i++)
                AO3bl3_combo.Items.Add(json_read.comboSignalsItems[AO3bl3_combo.Name][i]);
            // DO сигналы, ПЛК
            DO1_combo.Items.Clear(); DO2_combo.Items.Clear(); DO3_combo.Items.Clear();
            DO4_combo.Items.Clear(); DO5_combo.Items.Clear(); DO6_combo.Items.Clear();
            DO7_combo.Items.Clear();
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
            // DO7, ПЛК
            for (int i = 0; i < json_read.comboSignalsItems[DO7_combo.Name].Length; i++)
                DO7_combo.Items.Add(json_read.comboSignalsItems[DO7_combo.Name][i]);
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

        ///<summary>Загрузка перечня элементов для коллекции AI type</summary>
        private void LoadComboTypeAIitems()
        {
            // AI type, ПЛК
            AI1_typeCombo.Items.Clear(); AI2_typeCombo.Items.Clear(); AI3_typeCombo.Items.Clear();
            AI4_typeCombo.Items.Clear(); AI5_typeCombo.Items.Clear(); AI6_typeCombo.Items.Clear();
            // AI1 type, ПЛК
            for (int i = 0; i < json_read.comboAITypeItems[AI1_typeCombo.Name].Length; i++)
                AI1_typeCombo.Items.Add(json_read.comboAITypeItems[AI1_typeCombo.Name][i]);
            // AI2 type, ПЛК
            for (int i = 0; i < json_read.comboAITypeItems[AI2_typeCombo.Name].Length; i++)
                AI2_typeCombo.Items.Add(json_read.comboAITypeItems[AI2_typeCombo.Name][i]);
            // AI3 type, ПЛК
            for (int i = 0; i < json_read.comboAITypeItems[AI3_typeCombo.Name].Length; i++)
                AI3_typeCombo.Items.Add(json_read.comboAITypeItems[AI3_typeCombo.Name][i]);
            // AI4 type, ПЛК
            for (int i = 0; i < json_read.comboAITypeItems[AI4_typeCombo.Name].Length; i++)
                AI4_typeCombo.Items.Add(json_read.comboAITypeItems[AI4_typeCombo.Name][i]);
            // AI5 type, ПЛК
            for (int i = 0; i < json_read.comboAITypeItems[AI5_typeCombo.Name].Length; i++)
                AI5_typeCombo.Items.Add(json_read.comboAITypeItems[AI5_typeCombo.Name][i]);
            // AI6 type, ПЛК
            for (int i = 0; i < json_read.comboAITypeItems[AI6_typeCombo.Name].Length; i++)
                AI6_typeCombo.Items.Add(json_read.comboAITypeItems[AI6_typeCombo.Name][i]);
            // AI type, блок 1
            AI1bl1_typeCombo.Items.Clear(); AI2bl1_typeCombo.Items.Clear(); AI3bl1_typeCombo.Items.Clear();
            AI4bl1_typeCombo.Items.Clear(); AI5bl1_typeCombo.Items.Clear(); AI6bl1_typeCombo.Items.Clear();
            // AI1 type, блок 1
            for (int i = 0; i < json_read.comboAITypeItems[AI1bl1_typeCombo.Name].Length; i++)
                AI1bl1_typeCombo.Items.Add(json_read.comboAITypeItems[AI1bl1_typeCombo.Name][i]);
            // AI2 type, блок 1
            for (int i = 0; i < json_read.comboAITypeItems[AI2bl1_typeCombo.Name].Length; i++)
                AI2bl1_typeCombo.Items.Add(json_read.comboAITypeItems[AI2bl1_typeCombo.Name][i]);
            // AI3 type, блок 1
            for (int i = 0; i < json_read.comboAITypeItems[AI3bl1_typeCombo.Name].Length; i++)
                AI3bl1_typeCombo.Items.Add(json_read.comboAITypeItems[AI3bl1_typeCombo.Name][i]);
            // AI4 type, блок 1
            for (int i = 0; i < json_read.comboAITypeItems[AI4bl1_typeCombo.Name].Length; i++)
                AI4bl1_typeCombo.Items.Add(json_read.comboAITypeItems[AI4bl1_typeCombo.Name][i]);
            // AI5 type, блок 1
            for (int i = 0; i < json_read.comboAITypeItems[AI5bl1_typeCombo.Name].Length; i++)
                AI5bl1_typeCombo.Items.Add(json_read.comboAITypeItems[AI5bl1_typeCombo.Name][i]);
            // AI6 type, блок 1
            for (int i = 0; i < json_read.comboAITypeItems[AI6bl1_typeCombo.Name].Length; i++)
                AI6bl1_typeCombo.Items.Add(json_read.comboAITypeItems[AI6bl1_typeCombo.Name][i]);
            // AI type, блок 2
            AI1bl2_typeCombo.Items.Clear(); AI2bl2_typeCombo.Items.Clear(); AI3bl2_typeCombo.Items.Clear();
            AI4bl2_typeCombo.Items.Clear(); AI5bl2_typeCombo.Items.Clear(); AI6bl2_typeCombo.Items.Clear();
            // AI1 type, блок 2
            for (int i = 0; i < json_read.comboAITypeItems[AI1bl2_typeCombo.Name].Length; i++)
                AI1bl2_typeCombo.Items.Add(json_read.comboAITypeItems[AI1bl2_typeCombo.Name][i]);
            // AI2 type, блок 2
            for (int i = 0; i < json_read.comboAITypeItems[AI2bl2_typeCombo.Name].Length; i++)
                AI2bl2_typeCombo.Items.Add(json_read.comboAITypeItems[AI2bl2_typeCombo.Name][i]);
            // AI3 type, блок 2
            for (int i = 0; i < json_read.comboAITypeItems[AI3bl2_typeCombo.Name].Length; i++)
                AI3bl2_typeCombo.Items.Add(json_read.comboAITypeItems[AI3bl2_typeCombo.Name][i]);
            // AI4 type, блок 2
            for (int i = 0; i < json_read.comboAITypeItems[AI4bl2_typeCombo.Name].Length; i++)
                AI4bl2_typeCombo.Items.Add(json_read.comboAITypeItems[AI4bl2_typeCombo.Name][i]);
            // AI5 type, блок 2
            for (int i = 0; i < json_read.comboAITypeItems[AI5bl2_typeCombo.Name].Length; i++)
                AI5bl2_typeCombo.Items.Add(json_read.comboAITypeItems[AI5bl2_typeCombo.Name][i]);
            // AI6 type, блок 2
            for (int i = 0; i < json_read.comboAITypeItems[AI6bl2_typeCombo.Name].Length; i++)
                AI6bl2_typeCombo.Items.Add(json_read.comboAITypeItems[AI6bl2_typeCombo.Name][i]);
            // AI type, блок 3
            AI1bl3_typeCombo.Items.Clear(); AI2bl3_typeCombo.Items.Clear(); AI3bl3_typeCombo.Items.Clear();
            AI4bl3_typeCombo.Items.Clear(); AI5bl3_typeCombo.Items.Clear(); AI6bl3_typeCombo.Items.Clear();
            // AI1 type, блок 3
            for (int i = 0; i < json_read.comboAITypeItems[AI1bl3_typeCombo.Name].Length; i++)
                AI1bl3_typeCombo.Items.Add(json_read.comboAITypeItems[AI1bl3_typeCombo.Name][i]);
            // AI2 type, блок 3
            for (int i = 0; i < json_read.comboAITypeItems[AI2bl3_typeCombo.Name].Length; i++)
                AI2bl3_typeCombo.Items.Add(json_read.comboAITypeItems[AI2bl3_typeCombo.Name][i]);
            // AI3 type, блок 3
            for (int i = 0; i < json_read.comboAITypeItems[AI3bl3_typeCombo.Name].Length; i++)
                AI3bl3_typeCombo.Items.Add(json_read.comboAITypeItems[AI3bl3_typeCombo.Name][i]);
            // AI4 type, блок 3
            for (int i = 0; i < json_read.comboAITypeItems[AI4bl3_typeCombo.Name].Length; i++)
                AI4bl3_typeCombo.Items.Add(json_read.comboAITypeItems[AI4bl3_typeCombo.Name][i]);
            // AI5 type, блок 3
            for (int i = 0; i < json_read.comboAITypeItems[AI5bl3_typeCombo.Name].Length; i++)
                AI5bl3_typeCombo.Items.Add(json_read.comboAITypeItems[AI5bl3_typeCombo.Name][i]);
            // AI6 type, блок 3
            for (int i = 0; i < json_read.comboAITypeItems[AI6bl3_typeCombo.Name].Length; i++)
                AI6bl3_typeCombo.Items.Add(json_read.comboAITypeItems[AI6bl3_typeCombo.Name][i]);
        }

        ///<summary>Загрузка состояний для comboBox таблицы сигналов</summary>
        private void LoadComboSignalsAll()
        {
            // AI1, ПЛК
            AI1_combo.SelectedItem = json_read.comboSignalsState[AI1_combo.Name];
            AI1combo_index = AI1_combo.SelectedIndex;
            AI1combo_text = AI1_combo.SelectedItem.ToString();
            // AI2, ПЛК
            AI2_combo.SelectedItem = json_read.comboSignalsState[AI2_combo.Name];
            AI2combo_index = AI2_combo.SelectedIndex;
            AI2combo_text = AI2_combo.SelectedItem.ToString();
            // AI3, ПЛК
            AI3_combo.SelectedItem = json_read.comboSignalsState[AI3_combo.Name];
            AI3combo_index = AI3_combo.SelectedIndex;
            AI3combo_text = AI3_combo.SelectedItem.ToString();
            // AI4, ПЛК
            AI4_combo.SelectedItem = json_read.comboSignalsState[AI4_combo.Name];
            AI4combo_index = AI4_combo.SelectedIndex;
            AI4combo_text = AI4_combo.SelectedItem.ToString();
            // AI5, ПЛК
            AI5_combo.SelectedItem = json_read.comboSignalsState[AI5_combo.Name];
            AI5combo_index = AI5_combo.SelectedIndex;
            AI5combo_text = AI5_combo.SelectedItem.ToString();
            // AI6, ПЛК
            AI6_combo.SelectedItem = json_read.comboSignalsState[AI6_combo.Name];
            AI6combo_index = AI6_combo.SelectedIndex;
            AI6combo_text = AI6_combo.SelectedItem.ToString();
            // AI1, блок 1
            AI1bl1_combo.SelectedItem = json_read.comboSignalsState[AI1bl1_combo.Name];
            AI1bl1combo_index = AI1bl1_combo.SelectedIndex;
            AI1bl1combo_text = AI1bl1_combo.SelectedItem.ToString();
            // AI2, блок 1
            AI2bl1_combo.SelectedItem = json_read.comboSignalsState[AI2bl1_combo.Name];
            AI2bl1combo_index = AI2bl1_combo.SelectedIndex;
            AI2bl1combo_text = AI2bl1_combo.SelectedItem.ToString();
            // AI3, блок 1
            AI3bl1_combo.SelectedItem = json_read.comboSignalsState[AI3bl1_combo.Name];
            AI3bl1combo_index = AI3bl1_combo.SelectedIndex;
            AI3bl1combo_text = AI3bl1_combo.SelectedItem.ToString();
            // AI4, блок 1
            AI4bl1_combo.SelectedItem = json_read.comboSignalsState[AI4bl1_combo.Name];
            AI4bl1combo_index = AI4bl1_combo.SelectedIndex;
            AI4bl1combo_text = AI4bl1_combo.SelectedItem.ToString();
            // AI5, блок 1
            AI5bl1_combo.SelectedItem = json_read.comboSignalsState[AI5bl1_combo.Name];
            AI5bl1combo_index = AI5bl1_combo.SelectedIndex;
            AI5bl1combo_text = AI5bl1_combo.SelectedItem.ToString();
            // AI6, блок 1
            AI6bl1_combo.SelectedItem = json_read.comboSignalsState[AI6bl1_combo.Name];
            AI6bl1combo_index = AI6bl1_combo.SelectedIndex;
            AI6bl1combo_text = AI6bl1_combo.SelectedItem.ToString();
            // AI1, блок 2
            AI1bl2_combo.SelectedItem = json_read.comboSignalsState[AI1bl2_combo.Name];
            AI1bl2combo_index = AI1bl2_combo.SelectedIndex;
            AI1bl2combo_text = AI1bl2_combo.SelectedItem.ToString();
            // AI2, блок 2
            AI2bl2_combo.SelectedItem = json_read.comboSignalsState[AI2bl2_combo.Name];
            AI2bl2combo_index = AI2bl2_combo.SelectedIndex;
            AI2bl2combo_text = AI2bl2_combo.SelectedItem.ToString();
            // AI3, блок 2
            AI3bl2_combo.SelectedItem = json_read.comboSignalsState[AI3bl2_combo.Name];
            AI3bl2combo_index = AI3bl2_combo.SelectedIndex;
            AI3bl2combo_text = AI3bl2_combo.SelectedItem.ToString();
            // AI4, блок 2
            AI4bl2_combo.SelectedItem = json_read.comboSignalsState[AI4bl2_combo.Name];
            AI4bl2combo_index = AI4bl2_combo.SelectedIndex;
            AI4bl2combo_text = AI4bl2_combo.SelectedItem.ToString();
            // AI5, блок 2
            AI5bl2_combo.SelectedItem = json_read.comboSignalsState[AI5bl2_combo.Name];
            AI5bl2combo_index = AI5bl2_combo.SelectedIndex;
            AI5bl2combo_text = AI5bl2_combo.SelectedItem.ToString();
            // AI6, блок 2
            AI6bl2_combo.SelectedItem = json_read.comboSignalsState[AI6bl2_combo.Name];
            AI6bl2combo_index = AI6bl2_combo.SelectedIndex;
            AI6bl2combo_text = AI6bl2_combo.SelectedItem.ToString();
            // AI1, блок 3
            AI1bl3_combo.SelectedItem = json_read.comboSignalsState[AI1bl3_combo.Name];
            AI1bl3combo_index = AI1bl3_combo.SelectedIndex;
            AI1bl3combo_text = AI1bl3_combo.SelectedItem.ToString();
            // AI2, блок 3
            AI2bl3_combo.SelectedItem = json_read.comboSignalsState[AI2bl3_combo.Name];
            AI2bl3combo_index = AI2bl3_combo.SelectedIndex;
            AI2bl3combo_text = AI2bl3_combo.SelectedItem.ToString();
            // AI3, блок 3
            AI3bl3_combo.SelectedItem = json_read.comboSignalsState[AI3bl3_combo.Name];
            AI3bl3combo_index = AI3bl3_combo.SelectedIndex;
            AI3bl3combo_text = AI3bl3_combo.SelectedItem.ToString();
            // AI4, блок 3
            AI4bl3_combo.SelectedItem = json_read.comboSignalsState[AI4bl3_combo.Name];
            AI4bl3combo_index = AI4bl3_combo.SelectedIndex;
            AI4bl3combo_text = AI4bl3_combo.SelectedItem.ToString();
            // AI5, блок 3
            AI5bl3_combo.SelectedItem = json_read.comboSignalsState[AI5bl3_combo.Name];
            AI5bl3combo_index = AI5bl3_combo.SelectedIndex;
            AI5bl3combo_text = AI5bl3_combo.SelectedItem.ToString();
            // AI6, блок 3
            AI6bl3_combo.SelectedItem = json_read.comboSignalsState[AI6bl3_combo.Name];
            AI6bl3combo_index = AI6bl3_combo.SelectedIndex;
            AI6bl3combo_text = AI6bl3_combo.SelectedItem.ToString();
            // DI1, ПЛК
            DI1_combo.SelectedItem = json_read.comboSignalsState[DI1_combo.Name];
            DI1combo_index = DI1_combo.SelectedIndex;
            DI1combo_text = DI1_combo.SelectedItem.ToString();
            // DI2, ПЛК
            DI2_combo.SelectedItem = json_read.comboSignalsState[DI2_combo.Name];
            DI2combo_index = DI2_combo.SelectedIndex;
            DI2combo_text = DI2_combo.SelectedItem.ToString();
            // DI3, ПЛК
            DI3_combo.SelectedItem = json_read.comboSignalsState[DI3_combo.Name];
            DI3combo_index = DI3_combo.SelectedIndex;
            DI3combo_text = DI3_combo.SelectedItem.ToString();
            // DI4, ПЛК
            DI4_combo.SelectedItem = json_read.comboSignalsState[DI4_combo.Name];
            DI4combo_index = DI4_combo.SelectedIndex;
            DI4combo_text = DI4_combo.SelectedItem.ToString();
            // DI5, ПЛК
            DI5_combo.SelectedItem = json_read.comboSignalsState[DI5_combo.Name];
            DI5combo_index = DI5_combo.SelectedIndex;
            DI5combo_text = DI5_combo.SelectedItem.ToString();
            // DI1, блок 1
            DI1bl1_combo.SelectedItem = json_read.comboSignalsState[DI1bl1_combo.Name];
            DI1bl1combo_index = DI1bl1_combo.SelectedIndex;
            DI1bl1combo_text = DI1bl1_combo.SelectedItem.ToString();
            // DI2, блок 1
            DI2bl1_combo.SelectedItem = json_read.comboSignalsState[DI2bl1_combo.Name];
            DI2bl1combo_index = DI2bl1_combo.SelectedIndex;
            DI2bl1combo_text = DI2bl1_combo.SelectedItem.ToString();
            // DI3, блок 1
            DI3bl1_combo.SelectedItem = json_read.comboSignalsState[DI3bl1_combo.Name];
            DI3bl1combo_index = DI3bl1_combo.SelectedIndex;
            DI3bl1combo_text = DI3bl1_combo.SelectedItem.ToString();
            // DI4, блок 1
            DI4bl1_combo.SelectedItem = json_read.comboSignalsState[DI4bl1_combo.Name];
            DI4bl1combo_index = DI4bl1_combo.SelectedIndex;
            DI4bl1combo_text = DI4bl1_combo.SelectedItem.ToString();
            // DI5, блок 1
            DI5bl1_combo.SelectedItem = json_read.comboSignalsState[DI5bl1_combo.Name];
            DI5bl1combo_index = DI5bl1_combo.SelectedIndex;
            DI5bl1combo_text = DI5bl1_combo.SelectedItem.ToString();
            // DI1, блок 2
            DI1bl2_combo.SelectedItem = json_read.comboSignalsState[DI1bl2_combo.Name];
            DI1bl2combo_index = DI1bl2_combo.SelectedIndex;
            DI1bl2combo_text = DI1bl2_combo.SelectedItem.ToString();
            // DI2, блок 2
            DI2bl2_combo.SelectedItem = json_read.comboSignalsState[DI2bl2_combo.Name];
            DI2bl2combo_index = DI2bl2_combo.SelectedIndex;
            DI2bl2combo_text = DI2bl2_combo.SelectedItem.ToString();
            // DI3, блок 2
            DI3bl2_combo.SelectedItem = json_read.comboSignalsState[DI3bl2_combo.Name];
            DI3bl2combo_index = DI3bl2_combo.SelectedIndex;
            DI3bl2combo_text = DI3bl2_combo.SelectedItem.ToString();
            // DI4, блок 2
            DI4bl2_combo.SelectedItem = json_read.comboSignalsState[DI4bl2_combo.Name];
            DI4bl2combo_index = DI4bl2_combo.SelectedIndex;
            DI4bl2combo_text = DI4bl2_combo.SelectedItem.ToString();
            // DI5, блок 2
            DI5bl2_combo.SelectedItem = json_read.comboSignalsState[DI5bl2_combo.Name];
            DI5bl2combo_index = DI5bl2_combo.SelectedIndex;
            DI5bl2combo_text = DI5bl2_combo.SelectedItem.ToString();
            // DI1, блок 3
            DI1bl3_combo.SelectedItem = json_read.comboSignalsState[DI1bl3_combo.Name];
            DI1bl3combo_index = DI1bl3_combo.SelectedIndex;
            DI1bl3combo_text = DI1bl3_combo.SelectedItem.ToString();
            // DI2, блок 3
            DI2bl3_combo.SelectedItem = json_read.comboSignalsState[DI2bl3_combo.Name];
            DI2bl3combo_index = DI2bl3_combo.SelectedIndex;
            DI2bl3combo_text = DI2bl3_combo.SelectedItem.ToString();
            // DI3, блок 3
            DI3bl3_combo.SelectedItem = json_read.comboSignalsState[DI3bl3_combo.Name];
            DI3bl3combo_index = DI3bl3_combo.SelectedIndex;
            DI3bl3combo_text = DI3bl3_combo.SelectedItem.ToString();
            // DI4, блок 3
            DI4bl3_combo.SelectedItem = json_read.comboSignalsState[DI4bl3_combo.Name];
            DI4bl3combo_index = DI4bl3_combo.SelectedIndex;
            DI4bl3combo_text = DI4bl3_combo.SelectedItem.ToString();
            // DI5, блок 3
            DI5bl3_combo.SelectedItem = json_read.comboSignalsState[DI5bl3_combo.Name];
            DI5bl3combo_index = DI5bl3_combo.SelectedIndex;
            DI5bl3combo_text = DI5bl3_combo.SelectedItem.ToString();
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
            // AO3, блок 1
            AO3bl1_combo.SelectedItem = json_read.comboSignalsState[AO3bl1_combo.Name];
            AO3bl1combo_index = AO3bl1_combo.SelectedIndex;
            AO3bl1combo_text = AO3bl1_combo.SelectedItem.ToString();
            // AO1, блок 2
            AO1bl2_combo.SelectedItem = json_read.comboSignalsState[AO1bl2_combo.Name];
            AO1bl2combo_index = AO1bl2_combo.SelectedIndex;
            AO1bl2combo_text = AO1bl2_combo.SelectedItem.ToString();
            // AO2, блок 2
            AO2bl2_combo.SelectedItem = json_read.comboSignalsState[AO2bl2_combo.Name];
            AO2bl2combo_index = AO2bl2_combo.SelectedIndex;
            AO2bl2combo_text = AO2bl2_combo.SelectedItem.ToString();
            // AO3, блок 2
            AO3bl2_combo.SelectedItem = json_read.comboSignalsState[AO3bl2_combo.Name];
            AO3bl2combo_index = AO3bl2_combo.SelectedIndex;
            AO3bl2combo_text = AO3bl2_combo.SelectedItem.ToString();
            // AO1, блок 3
            AO1bl3_combo.SelectedItem = json_read.comboSignalsState[AO1bl3_combo.Name];
            AO1bl3combo_index = AO1bl3_combo.SelectedIndex;
            AO1bl3combo_text = AO1bl3_combo.SelectedItem.ToString();
            // AO2, блок 3
            AO2bl3_combo.SelectedItem = json_read.comboSignalsState[AO2bl3_combo.Name];
            AO2bl3combo_index = AO2bl3_combo.SelectedIndex;
            AO2bl3combo_text = AO2bl3_combo.SelectedItem.ToString();
            // AO3, блок 3
            AO3bl3_combo.SelectedItem = json_read.comboSignalsState[AO3bl3_combo.Name];
            AO3bl3combo_index = AO3bl3_combo.SelectedIndex;
            AO3bl3combo_text = AO3bl3_combo.SelectedItem.ToString();
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
            // DO7, ПЛК
            DO7_combo.SelectedItem = json_read.comboSignalsState[DO7_combo.Name];
            DO7combo_index = DO7_combo.SelectedIndex;
            DO7combo_text = DO7_combo.SelectedItem.ToString();
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

        ///<summary>Загрузка выбранного элемента для comboBox AI type</summary>
        private void LoadComboTypeAIAll()
        {
            // ПЛК
            AI1_typeCombo.SelectedItem = json_read.comboAITypeState[AI1_typeCombo.Name];
            AI2_typeCombo.SelectedItem = json_read.comboAITypeState[AI2_typeCombo.Name];
            AI3_typeCombo.SelectedItem = json_read.comboAITypeState[AI3_typeCombo.Name];
            AI4_typeCombo.SelectedItem = json_read.comboAITypeState[AI4_typeCombo.Name];
            AI5_typeCombo.SelectedItem = json_read.comboAITypeState[AI5_typeCombo.Name];
            AI6_typeCombo.SelectedItem = json_read.comboAITypeState[AI6_typeCombo.Name];
            // Блок 1
            AI1bl1_typeCombo.SelectedItem = json_read.comboAITypeState[AI1bl1_typeCombo.Name];
            AI2bl1_typeCombo.SelectedItem = json_read.comboAITypeState[AI2bl1_typeCombo.Name];
            AI3bl1_typeCombo.SelectedItem = json_read.comboAITypeState[AI3bl1_typeCombo.Name];
            AI4bl1_typeCombo.SelectedItem = json_read.comboAITypeState[AI4bl1_typeCombo.Name];
            AI5bl1_typeCombo.SelectedItem = json_read.comboAITypeState[AI5bl1_typeCombo.Name];
            AI6bl1_typeCombo.SelectedItem = json_read.comboAITypeState[AI6bl1_typeCombo.Name];
            // Блок 2
            AI1bl2_typeCombo.SelectedItem = json_read.comboAITypeState[AI1bl2_typeCombo.Name];
            AI2bl2_typeCombo.SelectedItem = json_read.comboAITypeState[AI2bl2_typeCombo.Name];
            AI3bl2_typeCombo.SelectedItem = json_read.comboAITypeState[AI3bl2_typeCombo.Name];
            AI4bl2_typeCombo.SelectedItem = json_read.comboAITypeState[AI4bl2_typeCombo.Name];
            AI5bl2_typeCombo.SelectedItem = json_read.comboAITypeState[AI5bl2_typeCombo.Name];
            AI6bl2_typeCombo.SelectedItem = json_read.comboAITypeState[AI6bl2_typeCombo.Name];
            // Блок 3
            AI1bl3_typeCombo.SelectedItem = json_read.comboAITypeState[AI1bl3_typeCombo.Name];
            AI2bl3_typeCombo.SelectedItem = json_read.comboAITypeState[AI2bl3_typeCombo.Name];
            AI3bl3_typeCombo.SelectedItem = json_read.comboAITypeState[AI3bl3_typeCombo.Name];
            AI4bl3_typeCombo.SelectedItem = json_read.comboAITypeState[AI4bl3_typeCombo.Name];
            AI5bl3_typeCombo.SelectedItem = json_read.comboAITypeState[AI5bl3_typeCombo.Name];
            AI6bl3_typeCombo.SelectedItem = json_read.comboAITypeState[AI6bl3_typeCombo.Name];
        }

        ///<summary>Загрузка для массивов сигналов</summary>
        private void LoadSignalArrays()
        {
            string name, type;
            ushort code;
            bool active;
            list_ai.Clear(); // Очистка прежнего списка сигналов AI
            list_di.Clear(); // Очистка прежнего списка сигналов DI
            list_ao.Clear(); // Очистка прежнего списка сигналов AO
            list_do.Clear(); // Очистка прежнего списка сигналов DO 
            foreach (var elem in json_read.aiCode)
            {   // Сигналы AI
                name = elem.Key;
                code = json_read.aiCode[name];
                type = json_read.aiType[name];
                active = json_read.aiActive[name];
                list_ai.Add(new Ai(name, code, type, active));
            }
            foreach (var elem in json_read.aoCode)
            {   // Сигналы AO
                name = elem.Key;
                code = json_read.aoCode[name];
                active = json_read.aoActive[name];
                list_ao.Add(new Ao(name, code, active));
            }
            foreach (var elem in json_read.diCode)
            {   // Сигналы DI
                name = elem.Key;
                code = json_read.diCode[name];
                active = json_read.diActive[name];
                list_di.Add(new Di(name, code, active));
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
            json.checkBoxState.Add(prFanPowSupCheck.Name, prFanPowSupCheck.Checked);
            json.checkBoxState.Add(prFanAlarmCheck.Name, prFanAlarmCheck.Checked);
            json.checkBoxState.Add(prFanStStopCheck.Name, prFanStStopCheck.Checked);
            json.checkBoxState.Add(prFanSpeedCheck.Name, prFanSpeedCheck.Checked);
            // Вытяжной вентилятор
            json.checkBoxState.Add(outFanPSCheck.Name, outFanPSCheck.Checked);
            json.checkBoxState.Add(outFanFC_check.Name, outFanFC_check.Checked);
            json.checkBoxState.Add(outFanThermoCheck.Name, outFanThermoCheck.Checked);
            json.checkBoxState.Add(curDefOutFanCheck.Name, curDefOutFanCheck.Checked);
            json.checkBoxState.Add(checkResOutFan.Name, checkResOutFan.Checked);
            json.checkBoxState.Add(outFanPowSupCheck.Name, outFanPowSupCheck.Checked);
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
            // AI сигналы, ПЛК
            json.labelSignalsState.Add(AI1_lab.Name, AI1_lab.Text);
            json.labelSignalsState.Add(AI2_lab.Name, AI2_lab.Text);
            json.labelSignalsState.Add(AI3_lab.Name, AI3_lab.Text);
            json.labelSignalsState.Add(AI4_lab.Name, AI4_lab.Text);
            json.labelSignalsState.Add(AI5_lab.Name, AI5_lab.Text);
            json.labelSignalsState.Add(AI6_lab.Name, AI6_lab.Text);
            // AI сигналы, блок 1
            json.labelSignalsState.Add(AI1bl1_lab.Name, AI1bl1_lab.Text);
            json.labelSignalsState.Add(AI2bl1_lab.Name, AI2bl1_lab.Text);
            json.labelSignalsState.Add(AI3bl1_lab.Name, AI3bl1_lab.Text);
            json.labelSignalsState.Add(AI4bl1_lab.Name, AI4bl1_lab.Text);
            json.labelSignalsState.Add(AI5bl1_lab.Name, AI5bl1_lab.Text);
            json.labelSignalsState.Add(AI6bl1_lab.Name, AI6bl1_lab.Text);
            // AI сигналы, блок 2
            json.labelSignalsState.Add(AI1bl2_lab.Name, AI1bl2_lab.Text);
            json.labelSignalsState.Add(AI2bl2_lab.Name, AI2bl2_lab.Text);
            json.labelSignalsState.Add(AI3bl2_lab.Name, AI3bl2_lab.Text);
            json.labelSignalsState.Add(AI4bl2_lab.Name, AI4bl2_lab.Text);
            json.labelSignalsState.Add(AI5bl2_lab.Name, AI5bl2_lab.Text);
            json.labelSignalsState.Add(AI6bl2_lab.Name, AI6bl2_lab.Text);
            // AI сигналы, блок 3
            json.labelSignalsState.Add(AI1bl3_lab.Name, AI1bl3_lab.Text);
            json.labelSignalsState.Add(AI2bl3_lab.Name, AI2bl3_lab.Text);
            json.labelSignalsState.Add(AI3bl3_lab.Name, AI3bl3_lab.Text);
            json.labelSignalsState.Add(AI4bl3_lab.Name, AI4bl3_lab.Text);
            json.labelSignalsState.Add(AI5bl3_lab.Name, AI5bl3_lab.Text);
            json.labelSignalsState.Add(AI6bl3_lab.Name, AI6bl3_lab.Text);
            // DI сигналы, ПЛК
            json.labelSignalsState.Add(DI1_lab.Name, DI1_lab.Text);
            json.labelSignalsState.Add(DI2_lab.Name, DI2_lab.Text);
            json.labelSignalsState.Add(DI3_lab.Name, DI3_lab.Text);
            json.labelSignalsState.Add(DI4_lab.Name, DI4_lab.Text);
            json.labelSignalsState.Add(DI5_lab.Name, DI5_lab.Text);
            // DI сигналы, блок 1
            json.labelSignalsState.Add(DI1bl1_lab.Name, DI1bl1_lab.Text);
            json.labelSignalsState.Add(DI2bl1_lab.Name, DI2bl1_lab.Text);
            json.labelSignalsState.Add(DI3bl1_lab.Name, DI3bl1_lab.Text);
            json.labelSignalsState.Add(DI4bl1_lab.Name, DI4bl1_lab.Text);
            json.labelSignalsState.Add(DI5bl1_lab.Name, DI5bl1_lab.Text);
            // DI сигналы, блок 2
            json.labelSignalsState.Add(DI1bl2_lab.Name, DI1bl2_lab.Text);
            json.labelSignalsState.Add(DI2bl2_lab.Name, DI2bl2_lab.Text);
            json.labelSignalsState.Add(DI3bl2_lab.Name, DI3bl2_lab.Text);
            json.labelSignalsState.Add(DI4bl2_lab.Name, DI4bl2_lab.Text);
            json.labelSignalsState.Add(DI5bl2_lab.Name, DI5bl2_lab.Text);
            // DI сигналы, блок 3
            json.labelSignalsState.Add(DI1bl3_lab.Name, DI1bl3_lab.Text);
            json.labelSignalsState.Add(DI2bl3_lab.Name, DI2bl3_lab.Text);
            json.labelSignalsState.Add(DI3bl3_lab.Name, DI3bl3_lab.Text);
            json.labelSignalsState.Add(DI4bl3_lab.Name, DI4bl3_lab.Text);
            json.labelSignalsState.Add(DI5bl3_lab.Name, DI5bl3_lab.Text);
            // AO сигналы, ПЛК
            json.labelSignalsState.Add(AO1_lab.Name, AO1_lab.Text);
            json.labelSignalsState.Add(AO2_lab.Name, AO2_lab.Text);
            json.labelSignalsState.Add(AO3_lab.Name, AO3_lab.Text);
            // AO сигналы, блок 1
            json.labelSignalsState.Add(AO1bl1_lab.Name, AO1bl1_lab.Text);
            json.labelSignalsState.Add(AO2bl1_lab.Name, AO2bl1_lab.Text);
            json.labelSignalsState.Add(AO3bl1_lab.Name, AO3bl1_lab.Text);
            // AO сигналы, блок 2
            json.labelSignalsState.Add(AO1bl2_lab.Name, AO1bl2_lab.Text);
            json.labelSignalsState.Add(AO2bl2_lab.Name, AO2bl2_lab.Text);
            json.labelSignalsState.Add(AO3bl2_lab.Name, AO3bl2_lab.Text);
            // AO сигналы, блок 3
            json.labelSignalsState.Add(AO1bl3_lab.Name, AO1bl3_lab.Text);
            json.labelSignalsState.Add(AO2bl3_lab.Name, AO2bl3_lab.Text);
            json.labelSignalsState.Add(AO3bl3_lab.Name, AO3bl3_lab.Text);
            // DO сигналы, ПЛК
            json.labelSignalsState.Add(DO1_lab.Name, DO1_lab.Text);
            json.labelSignalsState.Add(DO2_lab.Name, DO2_lab.Text);
            json.labelSignalsState.Add(DO3_lab.Name, DO3_lab.Text);
            json.labelSignalsState.Add(DO4_lab.Name, DO4_lab.Text);
            json.labelSignalsState.Add(DO5_lab.Name, DO5_lab.Text);
            json.labelSignalsState.Add(DO6_lab.Name, DO6_lab.Text);
            json.labelSignalsState.Add(DO7_lab.Name, DO7_lab.Text);
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
            // AI сигналы, ПЛК
            json.comboSignalsState.Add(AI1_combo.Name, AI1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI2_combo.Name, AI2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI3_combo.Name, AI3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI4_combo.Name, AI4_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI5_combo.Name, AI5_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI6_combo.Name, AI6_combo.SelectedItem.ToString());
            // AI сигналы, блок 1
            json.comboSignalsState.Add(AI1bl1_combo.Name, AI1bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI2bl1_combo.Name, AI2bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI3bl1_combo.Name, AI3bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI4bl1_combo.Name, AI4bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI5bl1_combo.Name, AI5bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI6bl1_combo.Name, AI6bl1_combo.SelectedItem.ToString());
            // AI сигналы, блок 2
            json.comboSignalsState.Add(AI1bl2_combo.Name, AI1bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI2bl2_combo.Name, AI2bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI3bl2_combo.Name, AI3bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI4bl2_combo.Name, AI4bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI5bl2_combo.Name, AI5bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI6bl2_combo.Name, AI6bl2_combo.SelectedItem.ToString());
            // AI сигналы, блок 3
            json.comboSignalsState.Add(AI1bl3_combo.Name, AI1bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI2bl3_combo.Name, AI2bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI3bl3_combo.Name, AI3bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI4bl3_combo.Name, AI4bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI5bl3_combo.Name, AI5bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AI6bl3_combo.Name, AI6bl3_combo.SelectedItem.ToString());
            // DI сигналы, ПЛК
            json.comboSignalsState.Add(DI1_combo.Name, DI1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DI2_combo.Name, DI2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DI3_combo.Name, DI3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DI4_combo.Name, DI4_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DI5_combo.Name, DI5_combo.SelectedItem.ToString());
            // DI сигналы, блок 1
            json.comboSignalsState.Add(DI1bl1_combo.Name, DI1bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DI2bl1_combo.Name, DI2bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DI3bl1_combo.Name, DI3bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DI4bl1_combo.Name, DI4bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DI5bl1_combo.Name, DI5bl1_combo.SelectedItem.ToString());
            // DI сигналы, блок 2
            json.comboSignalsState.Add(DI1bl2_combo.Name, DI1bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DI2bl2_combo.Name, DI2bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DI3bl2_combo.Name, DI3bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DI4bl2_combo.Name, DI4bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DI5bl2_combo.Name, DI5bl2_combo.SelectedItem.ToString());
            // DI сигналы, блок 3
            json.comboSignalsState.Add(DI1bl3_combo.Name, DI1bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DI2bl3_combo.Name, DI2bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DI3bl3_combo.Name, DI3bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DI4bl3_combo.Name, DI4bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DI5bl3_combo.Name, DI5bl3_combo.SelectedItem.ToString());
            // AO сигналы, ПЛК
            json.comboSignalsState.Add(AO1_combo.Name, AO1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AO2_combo.Name, AO2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AO3_combo.Name, AO3_combo.SelectedItem.ToString());
            // AO сигналы, блок 1
            json.comboSignalsState.Add(AO1bl1_combo.Name, AO1bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AO2bl1_combo.Name, AO2bl1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AO3bl1_combo.Name, AO3bl1_combo.SelectedItem.ToString());
            // AO сигналы, блок 2
            json.comboSignalsState.Add(AO1bl2_combo.Name, AO1bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AO2bl2_combo.Name, AO2bl2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AO3bl2_combo.Name, AO3bl2_combo.SelectedItem.ToString());
            // AO сигналы, блок 3
            json.comboSignalsState.Add(AO1bl3_combo.Name, AO1bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AO2bl3_combo.Name, AO2bl3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(AO3bl3_combo.Name, AO3bl3_combo.SelectedItem.ToString());
            // DO сигналы, ПЛК
            json.comboSignalsState.Add(DO1_combo.Name, DO1_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO2_combo.Name, DO2_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO3_combo.Name, DO3_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO4_combo.Name, DO4_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO5_combo.Name, DO5_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO6_combo.Name, DO6_combo.SelectedItem.ToString());
            json.comboSignalsState.Add(DO7_combo.Name, DO7_combo.SelectedItem.ToString());
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

        ///<summary>Сборка состояния для типов AI comboBox</summary>
        private void BuildComboTypeAIAll()
        {
            // ПЛК
            json.comboAITypeState.Add(AI1_typeCombo.Name, AI1_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI2_typeCombo.Name, AI2_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI3_typeCombo.Name, AI3_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI4_typeCombo.Name, AI4_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI5_typeCombo.Name, AI5_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI6_typeCombo.Name, AI6_typeCombo.SelectedItem.ToString());
            // Блок 1
            json.comboAITypeState.Add(AI1bl1_typeCombo.Name, AI1bl1_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI2bl1_typeCombo.Name, AI2bl1_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI3bl1_typeCombo.Name, AI3bl1_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI4bl1_typeCombo.Name, AI4bl1_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI5bl1_typeCombo.Name, AI5bl1_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI6bl1_typeCombo.Name, AI6bl1_typeCombo.SelectedItem.ToString());
            // Блок 2
            json.comboAITypeState.Add(AI1bl2_typeCombo.Name, AI1bl2_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI2bl2_typeCombo.Name, AI2bl2_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI3bl2_typeCombo.Name, AI3bl2_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI4bl2_typeCombo.Name, AI4bl2_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI5bl2_typeCombo.Name, AI5bl2_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI6bl2_typeCombo.Name, AI6bl2_typeCombo.SelectedItem.ToString());
            // Блок 3
            json.comboAITypeState.Add(AI1bl3_typeCombo.Name, AI1bl3_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI2bl3_typeCombo.Name, AI2bl3_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI3bl3_typeCombo.Name, AI3bl3_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI4bl3_typeCombo.Name, AI4bl3_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI5bl3_typeCombo.Name, AI5bl3_typeCombo.SelectedItem.ToString());
            json.comboAITypeState.Add(AI6bl3_typeCombo.Name, AI6bl3_typeCombo.SelectedItem.ToString());
        }

        ///<summary>Сборка коллекций элементов для таблицы сигналов</summary>
        private void BuildComboItemsSignals()
        {
            // AI сигналы
            // AI1, ПЛК
            string[] arr_AI1_combo = new string[AI1_combo.Items.Count];
            for (int i = 0; i < AI1_combo.Items.Count; i++)
                arr_AI1_combo[i] = AI1_combo.GetItemText(AI1_combo.Items[i]);
            json.comboSignalsItems.Add(AI1_combo.Name, arr_AI1_combo);
            // AI2, ПЛК
            string[] arr_AI2_combo = new string[AI2_combo.Items.Count];
            for (int i = 0; i < AI2_combo.Items.Count; i++)
                arr_AI2_combo[i] = AI2_combo.GetItemText(AI2_combo.Items[i]);
            json.comboSignalsItems.Add(AI2_combo.Name, arr_AI2_combo);
            // AI3, ПЛК
            string[] arr_AI3_combo = new string[AI3_combo.Items.Count];
            for (int i = 0; i < AI3_combo.Items.Count; i++)
                arr_AI3_combo[i] = AI3_combo.GetItemText(AI3_combo.Items[i]);
            json.comboSignalsItems.Add(AI3_combo.Name, arr_AI3_combo);
            // AI4, ПЛК
            string[] arr_AI4_combo = new string[AI4_combo.Items.Count];
            for (int i = 0; i < AI4_combo.Items.Count; i++)
                arr_AI4_combo[i] = AI4_combo.GetItemText(AI4_combo.Items[i]);
            json.comboSignalsItems.Add(AI4_combo.Name, arr_AI4_combo);
            // AI5, ПЛК
            string[] arr_AI5_combo = new string[AI5_combo.Items.Count];
            for (int i = 0; i < AI5_combo.Items.Count; i++)
                arr_AI5_combo[i] = AI5_combo.GetItemText(AI5_combo.Items[i]);
            json.comboSignalsItems.Add(AI5_combo.Name, arr_AI5_combo);
            // AI6, ПЛК
            string[] arr_AI6_combo = new string[AI6_combo.Items.Count];
            for (int i = 0; i < AI6_combo.Items.Count; i++)
                arr_AI6_combo[i] = AI6_combo.GetItemText(AI6_combo.Items[i]);
            json.comboSignalsItems.Add(AI6_combo.Name, arr_AI6_combo);
            // AI1, блок 1
            string[] arr_AI1bl1_combo = new string[AI1bl1_combo.Items.Count];
            for (int i = 0; i < AI1bl1_combo.Items.Count; i++)
                arr_AI1bl1_combo[i] = AI1bl1_combo.GetItemText(AI1bl1_combo.Items[i]);
            json.comboSignalsItems.Add(AI1bl1_combo.Name, arr_AI1bl1_combo);
            // AI2, блок 1
            string[] arr_AI2bl1_combo = new string[AI2bl1_combo.Items.Count];
            for (int i = 0; i < AI2bl1_combo.Items.Count; i++)
                arr_AI2bl1_combo[i] = AI2bl1_combo.GetItemText(AI2bl1_combo.Items[i]);
            json.comboSignalsItems.Add(AI2bl1_combo.Name, arr_AI2bl1_combo);
            // AI3, блок 1
            string[] arr_AI3bl1_combo = new string[AI3bl1_combo.Items.Count];
            for (int i = 0; i < AI3bl1_combo.Items.Count; i++)
                arr_AI3bl1_combo[i] = AI3bl1_combo.GetItemText(AI3bl1_combo.Items[i]);
            json.comboSignalsItems.Add(AI3bl1_combo.Name, arr_AI3bl1_combo);
            // AI4, блок 1
            string[] arr_AI4bl1_combo = new string[AI4bl1_combo.Items.Count];
            for (int i = 0; i < AI4bl1_combo.Items.Count; i++)
                arr_AI4bl1_combo[i] = AI4bl1_combo.GetItemText(AI4bl1_combo.Items[i]);
            json.comboSignalsItems.Add(AI4bl1_combo.Name, arr_AI4bl1_combo);
            // AI5, блок 1
            string[] arr_AI5bl1_combo = new string[AI5bl1_combo.Items.Count];
            for (int i = 0; i < AI5bl1_combo.Items.Count; i++)
                arr_AI5bl1_combo[i] = AI5bl1_combo.GetItemText(AI5bl1_combo.Items[i]);
            json.comboSignalsItems.Add(AI5bl1_combo.Name, arr_AI5bl1_combo);
            // AI6, блок 1
            string[] arr_AI6bl1_combo = new string[AI6bl1_combo.Items.Count];
            for (int i = 0; i < AI6bl1_combo.Items.Count; i++)
                arr_AI6bl1_combo[i] = AI6bl1_combo.GetItemText(AI6bl1_combo.Items[i]);
            json.comboSignalsItems.Add(AI6bl1_combo.Name, arr_AI6bl1_combo);
            // AI1, блок 2
            string[] arr_AI1bl2_combo = new string[AI1bl2_combo.Items.Count];
            for (int i = 0; i < AI1bl2_combo.Items.Count; i++)
                arr_AI1bl2_combo[i] = AI1bl2_combo.GetItemText(AI1bl2_combo.Items[i]);
            json.comboSignalsItems.Add(AI1bl2_combo.Name, arr_AI1bl2_combo);
            // AI2, блок 2
            string[] arr_AI2bl2_combo = new string[AI2bl2_combo.Items.Count];
            for (int i = 0; i < AI2bl2_combo.Items.Count; i++)
                arr_AI2bl2_combo[i] = AI2bl2_combo.GetItemText(AI2bl2_combo.Items[i]);
            json.comboSignalsItems.Add(AI2bl2_combo.Name, arr_AI2bl2_combo);
            // AI3, блок 2
            string[] arr_AI3bl2_combo = new string[AI3bl2_combo.Items.Count];
            for (int i = 0; i < AI3bl2_combo.Items.Count; i++)
                arr_AI3bl2_combo[i] = AI3bl2_combo.GetItemText(AI3bl2_combo.Items[i]);
            json.comboSignalsItems.Add(AI3bl2_combo.Name, arr_AI3bl2_combo);
            // AI4, блок 2
            string[] arr_AI4bl2_combo = new string[AI4bl2_combo.Items.Count];
            for (int i = 0; i < AI4bl2_combo.Items.Count; i++)
                arr_AI4bl2_combo[i] = AI4bl2_combo.GetItemText(AI4bl2_combo.Items[i]);
            json.comboSignalsItems.Add(AI4bl2_combo.Name, arr_AI4bl2_combo);
            // AI5, блок 2
            string[] arr_AI5bl2_combo = new string[AI5bl2_combo.Items.Count];
            for (int i = 0; i < AI5bl2_combo.Items.Count; i++)
                arr_AI5bl2_combo[i] = AI5bl2_combo.GetItemText(AI5bl2_combo.Items[i]);
            json.comboSignalsItems.Add(AI5bl2_combo.Name, arr_AI5bl2_combo);
            // AI6, блок 2
            string[] arr_AI6bl2_combo = new string[AI6bl2_combo.Items.Count];
            for (int i = 0; i < AI6bl2_combo.Items.Count; i++)
                arr_AI6bl2_combo[i] = AI6bl2_combo.GetItemText(AI6bl2_combo.Items[i]);
            json.comboSignalsItems.Add(AI6bl2_combo.Name, arr_AI6bl2_combo);
            // AI1, блок 3
            string[] arr_AI1bl3_combo = new string[AI1bl3_combo.Items.Count];
            for (int i = 0; i < AI1bl3_combo.Items.Count; i++)
                arr_AI1bl3_combo[i] = AI1bl3_combo.GetItemText(AI1bl3_combo.Items[i]);
            json.comboSignalsItems.Add(AI1bl3_combo.Name, arr_AI1bl3_combo);
            // AI2, блок 3
            string[] arr_AI2bl3_combo = new string[AI2bl3_combo.Items.Count];
            for (int i = 0; i < AI2bl3_combo.Items.Count; i++)
                arr_AI2bl3_combo[i] = AI2bl3_combo.GetItemText(AI2bl3_combo.Items[i]);
            json.comboSignalsItems.Add(AI2bl3_combo.Name, arr_AI2bl3_combo);
            // AI3, блок 3
            string[] arr_AI3bl3_combo = new string[AI3bl3_combo.Items.Count];
            for (int i = 0; i < AI3bl3_combo.Items.Count; i++)
                arr_AI3bl3_combo[i] = AI3bl3_combo.GetItemText(AI3bl3_combo.Items[i]);
            json.comboSignalsItems.Add(AI3bl3_combo.Name, arr_AI3bl3_combo);
            // AI4, блок 3
            string[] arr_AI4bl3_combo = new string[AI4bl3_combo.Items.Count];
            for (int i = 0; i < AI4bl3_combo.Items.Count; i++)
                arr_AI4bl3_combo[i] = AI4bl3_combo.GetItemText(AI4bl3_combo.Items[i]);
            json.comboSignalsItems.Add(AI4bl3_combo.Name, arr_AI4bl3_combo);
            // AI5, блок 3
            string[] arr_AI5bl3_combo = new string[AI5bl3_combo.Items.Count];
            for (int i = 0; i < AI5bl3_combo.Items.Count; i++)
                arr_AI5bl3_combo[i] = AI5bl3_combo.GetItemText(AI5bl3_combo.Items[i]);
            json.comboSignalsItems.Add(AI5bl3_combo.Name, arr_AI5bl3_combo  );
            // AI6, блок 3
            string[] arr_AI6bl3_combo = new string[AI6bl3_combo.Items.Count];
            for (int i = 0; i < AI6bl3_combo.Items.Count; i++)
                arr_AI6bl3_combo[i] = AI6bl3_combo.GetItemText(AI6bl3_combo.Items[i]);
            json.comboSignalsItems.Add(AI6bl3_combo.Name, arr_AI6bl3_combo);
            // DI сигналы
            // DI1, ПЛК
            string[] arr_DI1_combo = new string[DI1_combo.Items.Count];
            for (int i = 0; i < DI1_combo.Items.Count; i++)
                arr_DI1_combo[i] = DI1_combo.GetItemText(DI1_combo.Items[i]);
            json.comboSignalsItems.Add(DI1_combo.Name, arr_DI1_combo);
            // DI2, ПЛК
            string[] arr_DI2_combo = new string[DI2_combo.Items.Count];
            for (int i = 0; i < DI2_combo.Items.Count; i++)
                arr_DI2_combo[i] = DI2_combo.GetItemText(DI2_combo.Items[i]);
            json.comboSignalsItems.Add(DI2_combo.Name, arr_DI2_combo);
            // DI3, ПЛК
            string[] arr_DI3_combo = new string[DI3_combo.Items.Count];
            for (int i = 0; i < DI3_combo.Items.Count; i++)
                arr_DI3_combo[i] = DI3_combo.GetItemText(DI3_combo.Items[i]);
            json.comboSignalsItems.Add(DI3_combo.Name, arr_DI3_combo);
            // DI4, ПЛК
            string[] arr_DI4_combo = new string[DI4_combo.Items.Count];
            for (int i = 0; i < DI4_combo.Items.Count; i++)
                arr_DI4_combo[i] = DI4_combo.GetItemText(DI4_combo.Items[i]);
            json.comboSignalsItems.Add(DI4_combo.Name, arr_DI4_combo);
            // DI5, ПЛК
            string[] arr_DI5_combo = new string[DI5_combo.Items.Count];
            for (int i = 0; i < DI5_combo.Items.Count; i++)
                arr_DI5_combo[i] = DI5_combo.GetItemText(DI5_combo.Items[i]);
            json.comboSignalsItems.Add(DI5_combo.Name, arr_DI5_combo);
            // DI1, блок 1
            string[] arr_DI1bl1_combo = new string[DI1bl1_combo.Items.Count];
            for (int i = 0; i < DI1bl1_combo.Items.Count; i++)
                arr_DI1bl1_combo[i] = DI1bl1_combo.GetItemText(DI1bl1_combo.Items[i]);
            json.comboSignalsItems.Add(DI1bl1_combo.Name, arr_DI1bl1_combo);
            // DI2, блок 1
            string[] arr_DI2bl1_combo = new string[DI2bl1_combo.Items.Count];
            for (int i = 0; i < DI2bl1_combo.Items.Count; i++)
                arr_DI2bl1_combo[i] = DI2bl1_combo.GetItemText(DI2bl1_combo.Items[i]);
            json.comboSignalsItems.Add(DI2bl1_combo.Name, arr_DI2bl1_combo);
            // DI3, блок 1
            string[] arr_DI3bl1_combo = new string[DI3bl1_combo.Items.Count];
            for (int i = 0; i < DI3bl1_combo.Items.Count; i++)
                arr_DI3bl1_combo[i] = DI3bl1_combo.GetItemText(DI3bl1_combo.Items[i]);
            json.comboSignalsItems.Add(DI3bl1_combo.Name, arr_DI3bl1_combo);
            // DI4, блок 1
            string[] arr_DI4bl1_combo = new string[DI4bl1_combo.Items.Count];
            for (int i = 0; i < DI4bl1_combo.Items.Count; i++)
                arr_DI4bl1_combo[i] = DI4bl1_combo.GetItemText(DI4bl1_combo.Items[i]);
            json.comboSignalsItems.Add(DI4bl1_combo.Name, arr_DI4bl1_combo);
            // DI5, блок 1
            string[] arr_DI5bl1_combo = new string[DI5bl1_combo.Items.Count];
            for (int i = 0; i < DI5bl1_combo.Items.Count; i++)
                arr_DI5bl1_combo[i] = DI5bl1_combo.GetItemText(DI5bl1_combo.Items[i]);
            json.comboSignalsItems.Add(DI5bl1_combo.Name, arr_DI5bl1_combo);
            // DI1, блок 2
            string[] arr_DI1bl2_combo = new string[DI1bl2_combo.Items.Count];
            for (int i = 0; i < DI1bl2_combo.Items.Count; i++)
                arr_DI1bl2_combo[i] = DI1bl2_combo.GetItemText(DI1bl2_combo.Items[i]);
            json.comboSignalsItems.Add(DI1bl2_combo.Name, arr_DI1bl2_combo);
            // DI2, блок 2
            string[] arr_DI2bl2_combo = new string[DI2bl2_combo.Items.Count];
            for (int i = 0; i < DI2bl2_combo.Items.Count; i++)
                arr_DI2bl2_combo[i] = DI2bl2_combo.GetItemText(DI2bl2_combo.Items[i]);
            json.comboSignalsItems.Add(DI2bl2_combo.Name, arr_DI2bl2_combo);
            // DI3, блок 2
            string[] arr_DI3bl2_combo = new string[DI3bl2_combo.Items.Count];
            for (int i = 0; i < DI3bl2_combo.Items.Count; i++)
                arr_DI3bl2_combo[i] = DI3bl2_combo.GetItemText(DI3bl2_combo.Items[i]);
            json.comboSignalsItems.Add(DI3bl2_combo.Name, arr_DI3bl2_combo);
            // DI4, блок 2
            string[] arr_DI4bl2_combo = new string[DI4bl2_combo.Items.Count];
            for (int i = 0; i < DI4bl2_combo.Items.Count; i++)
                arr_DI4bl2_combo[i] = DI4bl2_combo.GetItemText(DI4bl2_combo.Items[i]);
            json.comboSignalsItems.Add(DI4bl2_combo.Name, arr_DI4bl2_combo);
            // DI5, блок 2
            string[] arr_DI5bl2_combo = new string[DI5bl2_combo.Items.Count];
            for (int i = 0; i < DI5bl2_combo.Items.Count; i++)
                arr_DI5bl2_combo[i] = DI5bl2_combo.GetItemText(DI5bl2_combo.Items[i]);
            json.comboSignalsItems.Add(DI5bl2_combo.Name, arr_DI5bl2_combo);
            // DI1, блок 3
            string[] arr_DI1bl3_combo = new string[DI1bl3_combo.Items.Count];
            for (int i = 0; i < DI1bl3_combo.Items.Count; i++)
                arr_DI1bl3_combo[i] = DI1bl3_combo.GetItemText(DI1bl3_combo.Items[i]);
            json.comboSignalsItems.Add(DI1bl3_combo.Name, arr_DI1bl3_combo);
            // DI2, блок 3
            string[] arr_DI2bl3_combo = new string[DI2bl3_combo.Items.Count];
            for (int i = 0; i < DI2bl3_combo.Items.Count; i++)
                arr_DI2bl3_combo[i] = DI2bl3_combo.GetItemText(DI2bl3_combo.Items[i]);
            json.comboSignalsItems.Add(DI2bl3_combo.Name, arr_DI2bl3_combo);
            // DI3, блок 3
            string[] arr_DI3bl3_combo = new string[DI3bl3_combo.Items.Count];
            for (int i = 0; i < DI3bl3_combo.Items.Count; i++)
                arr_DI3bl3_combo[i] = DI3bl3_combo.GetItemText(DI3bl3_combo.Items[i]);
            json.comboSignalsItems.Add(DI3bl3_combo.Name, arr_DI3bl3_combo);
            // DI4, блок 3
            string[] arr_DI4bl3_combo = new string[DI4bl3_combo.Items.Count];
            for (int i = 0; i < DI4bl3_combo.Items.Count; i++)
                arr_DI4bl3_combo[i] = DI4bl3_combo.GetItemText(DI4bl3_combo.Items[i]);
            json.comboSignalsItems.Add(DI4bl3_combo.Name, arr_DI4bl3_combo);
            // DI5, блок 3
            string[] arr_DI5bl3_combo = new string[DI5bl3_combo.Items.Count];
            for (int i = 0; i < DI5bl3_combo.Items.Count; i++)
                arr_DI5bl3_combo[i] = DI5bl3_combo.GetItemText(DI5bl3_combo.Items[i]);
            json.comboSignalsItems.Add(DI5bl3_combo.Name, arr_DI5bl3_combo);
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
            // AO3, блок 1
            string[] arr_AO3bl1_combo = new string[AO3bl1_combo.Items.Count];
            for (int i = 0; i < AO3bl1_combo.Items.Count; i++)
                arr_AO3bl1_combo[i] = AO3bl1_combo.GetItemText(AO3bl1_combo.Items[i]);
            json.comboSignalsItems.Add(AO3bl1_combo.Name, arr_AO3bl1_combo);
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
            // AO3, блок 2
            string[] arr_AO3bl2_combo = new string[AO3bl2_combo.Items.Count];
            for (int i = 0; i < AO3bl2_combo.Items.Count; i++)
                arr_AO3bl2_combo[i] = AO3bl2_combo.GetItemText(AO3bl2_combo.Items[i]);
            json.comboSignalsItems.Add(AO3bl2_combo.Name, arr_AO3bl2_combo);
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
            // AO3, блок 3
            string[] arr_AO3bl3_combo = new string[AO3bl3_combo.Items.Count];
            for (int i = 0; i < AO3bl3_combo.Items.Count; i++)
                arr_AO3bl3_combo[i] = AO3bl3_combo.GetItemText(AO3bl3_combo.Items[i]);
            json.comboSignalsItems.Add(AO3bl3_combo.Name, arr_AO3bl3_combo);
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
            // DO7, ПЛК
            string[] arr_DO7_combo = new string[DO7_combo.Items.Count];
            for (int i = 0; i < DO7_combo.Items.Count; i++)
                arr_DO7_combo[i] = DO7_combo.GetItemText(DO7_combo.Items[i]);
            json.comboSignalsItems.Add(DO7_combo.Name, arr_DO7_combo);
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

        ///<summary>Сборка коллекций элементов для типов AI сигналов</summary>
        private void BuildComboTypeAIitems()
        {
            // AI1_type, ПЛК 
            string[] arr_AI1type_combo = new string[AI1_typeCombo.Items.Count];
            for (int i = 0; i < AI1_typeCombo.Items.Count; i++)
                arr_AI1type_combo[i] = AI1_typeCombo.GetItemText(AI1_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI1_typeCombo.Name, arr_AI1type_combo);
            // AI2_type, ПЛК 
            string[] arr_AI2type_combo = new string[AI2_typeCombo.Items.Count];
            for (int i = 0; i < AI2_typeCombo.Items.Count; i++)
                arr_AI2type_combo[i] = AI2_typeCombo.GetItemText(AI2_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI2_typeCombo.Name, arr_AI2type_combo);
            // AI3_type, ПЛК 
            string[] arr_AI3type_combo = new string[AI3_typeCombo.Items.Count];
            for (int i = 0; i < AI3_typeCombo.Items.Count; i++)
                arr_AI3type_combo[i] = AI3_typeCombo.GetItemText(AI3_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI3_typeCombo.Name, arr_AI3type_combo);
            // AI4_type, ПЛК 
            string[] arr_AI4type_combo = new string[AI4_typeCombo.Items.Count];
            for (int i = 0; i < AI4_typeCombo.Items.Count; i++)
                arr_AI4type_combo[i] = AI4_typeCombo.GetItemText(AI4_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI4_typeCombo.Name, arr_AI4type_combo);
            // AI5_type, ПЛК 
            string[] arr_AI5type_combo = new string[AI5_typeCombo.Items.Count];
            for (int i = 0; i < AI5_typeCombo.Items.Count; i++)
                arr_AI5type_combo[i] = AI5_typeCombo.GetItemText(AI5_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI5_typeCombo.Name, arr_AI5type_combo);
            // AI6_type, ПЛК 
            string[] arr_AI6type_combo = new string[AI6_typeCombo.Items.Count];
            for (int i = 0; i < AI6_typeCombo.Items.Count; i++)
                arr_AI6type_combo[i] = AI6_typeCombo.GetItemText(AI6_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI6_typeCombo.Name, arr_AI6type_combo);
            // AI1_type, блок 1 
            string[] arr_AI1bl1type_combo = new string[AI1bl1_typeCombo.Items.Count];
            for (int i = 0; i < AI1bl1_typeCombo.Items.Count; i++)
                arr_AI1bl1type_combo[i] = AI1bl1_typeCombo.GetItemText(AI1bl1_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI1bl1_typeCombo.Name, arr_AI1bl1type_combo);
            // AI2_type, блок 1 
            string[] arr_AI2bl1type_combo = new string[AI2bl1_typeCombo.Items.Count];
            for (int i = 0; i < AI2bl1_typeCombo.Items.Count; i++)
                arr_AI2bl1type_combo[i] = AI2bl1_typeCombo.GetItemText(AI2bl1_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI2bl1_typeCombo.Name, arr_AI2bl1type_combo);
            // AI3_type, блок 1 
            string[] arr_AI3bl1type_combo = new string[AI3bl1_typeCombo.Items.Count];
            for (int i = 0; i < AI3bl1_typeCombo.Items.Count; i++)
                arr_AI3bl1type_combo[i] = AI3bl1_typeCombo.GetItemText(AI3bl1_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI3bl1_typeCombo.Name, arr_AI3bl1type_combo);
            // AI4_type, блок 1 
            string[] arr_AI4bl1type_combo = new string[AI4bl1_typeCombo.Items.Count];
            for (int i = 0; i < AI4bl1_typeCombo.Items.Count; i++)
                arr_AI4bl1type_combo[i] = AI4bl1_typeCombo.GetItemText(AI4bl1_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI4bl1_typeCombo.Name, arr_AI4bl1type_combo);
            // AI5_type, блок 1 
            string[] arr_AI5bl1type_combo = new string[AI5bl1_typeCombo.Items.Count];
            for (int i = 0; i < AI5bl1_typeCombo.Items.Count; i++)
                arr_AI5bl1type_combo[i] = AI5bl1_typeCombo.GetItemText(AI5bl1_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI5bl1_typeCombo.Name, arr_AI5bl1type_combo);
            // AI6_type, блок 1 
            string[] arr_AI6bl1type_combo = new string[AI6bl1_typeCombo.Items.Count];
            for (int i = 0; i < AI6bl1_typeCombo.Items.Count; i++)
                arr_AI6bl1type_combo[i] = AI6bl1_typeCombo.GetItemText(AI6bl1_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI6bl1_typeCombo.Name, arr_AI6bl1type_combo);
            // AI1_type, блок 2 
            string[] arr_AI1bl2type_combo = new string[AI1bl2_typeCombo.Items.Count];
            for (int i = 0; i < AI1bl2_typeCombo.Items.Count; i++)
                arr_AI1bl2type_combo[i] = AI1bl2_typeCombo.GetItemText(AI1bl2_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI1bl2_typeCombo.Name, arr_AI1bl2type_combo);
            // AI2_type, блок 2 
            string[] arr_AI2bl2type_combo = new string[AI2bl2_typeCombo.Items.Count];
            for (int i = 0; i < AI2bl2_typeCombo.Items.Count; i++)
                arr_AI2bl2type_combo[i] = AI2bl2_typeCombo.GetItemText(AI2bl2_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI2bl2_typeCombo.Name, arr_AI2bl2type_combo);
            // AI3_type, блок 2 
            string[] arr_AI3bl2type_combo = new string[AI3bl2_typeCombo.Items.Count];
            for (int i = 0; i < AI3bl2_typeCombo.Items.Count; i++)
                arr_AI3bl2type_combo[i] = AI3bl2_typeCombo.GetItemText(AI3bl2_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI3bl2_typeCombo.Name, arr_AI3bl2type_combo);
            // AI4_type, блок 2 
            string[] arr_AI4bl2type_combo = new string[AI4bl2_typeCombo.Items.Count];
            for (int i = 0; i < AI4bl2_typeCombo.Items.Count; i++)
                arr_AI4bl2type_combo[i] = AI4bl2_typeCombo.GetItemText(AI4bl2_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI4bl2_typeCombo.Name, arr_AI4bl2type_combo);
            // AI5_type, блок 2 
            string[] arr_AI5bl2type_combo = new string[AI5bl2_typeCombo.Items.Count];
            for (int i = 0; i < AI5bl2_typeCombo.Items.Count; i++)
                arr_AI5bl2type_combo[i] = AI5bl2_typeCombo.GetItemText(AI5bl2_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI5bl2_typeCombo.Name, arr_AI5bl2type_combo);
            // AI6_type, блок 2 
            string[] arr_AI6bl2type_combo = new string[AI6bl2_typeCombo.Items.Count];
            for (int i = 0; i < AI6bl2_typeCombo.Items.Count; i++)
                arr_AI6bl2type_combo[i] = AI6bl2_typeCombo.GetItemText(AI6bl2_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI6bl2_typeCombo.Name, arr_AI6bl2type_combo);
            // AI1_type, блок 3 
            string[] arr_AI1bl3type_combo = new string[AI1bl3_typeCombo.Items.Count];
            for (int i = 0; i < AI1bl3_typeCombo.Items.Count; i++)
                arr_AI1bl3type_combo[i] = AI1bl3_typeCombo.GetItemText(AI1bl3_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI1bl3_typeCombo.Name, arr_AI1bl3type_combo);
            // AI2_type, блок 3 
            string[] arr_AI2bl3type_combo = new string[AI2bl3_typeCombo.Items.Count];
            for (int i = 0; i < AI2bl3_typeCombo.Items.Count; i++)
                arr_AI2bl3type_combo[i] = AI2bl3_typeCombo.GetItemText(AI2bl3_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI2bl3_typeCombo.Name, arr_AI2bl3type_combo);
            // AI3_type, блок 3 
            string[] arr_AI3bl3type_combo = new string[AI3bl3_typeCombo.Items.Count];
            for (int i = 0; i < AI3bl3_typeCombo.Items.Count; i++)
                arr_AI3bl3type_combo[i] = AI3bl3_typeCombo.GetItemText(AI3bl3_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI3bl3_typeCombo.Name, arr_AI3bl3type_combo);
            // AI4_type, блок 3 
            string[] arr_AI4bl3type_combo = new string[AI4bl3_typeCombo.Items.Count];
            for (int i = 0; i < AI4bl3_typeCombo.Items.Count; i++)
                arr_AI4bl3type_combo[i] = AI4bl3_typeCombo.GetItemText(AI4bl3_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI4bl3_typeCombo.Name, arr_AI4bl3type_combo);
            // AI5_type, блок 3 
            string[] arr_AI5bl3type_combo = new string[AI5bl3_typeCombo.Items.Count];
            for (int i = 0; i < AI5bl3_typeCombo.Items.Count; i++)
                arr_AI5bl3type_combo[i] = AI5bl3_typeCombo.GetItemText(AI5bl3_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI5bl3_typeCombo.Name, arr_AI5bl3type_combo);
            // AI6_type, блок 3 
            string[] arr_AI6bl3type_combo = new string[AI6bl3_typeCombo.Items.Count];
            for (int i = 0; i < AI6bl3_typeCombo.Items.Count; i++)
                arr_AI6bl3type_combo[i] = AI6bl3_typeCombo.GetItemText(AI6bl3_typeCombo.Items[i]);
            json.comboAITypeItems.Add(AI6bl3_typeCombo.Name, arr_AI6bl3type_combo);
        }
    }
}