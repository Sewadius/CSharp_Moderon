﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Moderon
{
    public partial class Form1 : Form
    {
        private const int height = 280; // Высота для панелей настройки элементов
        private bool hintEnabled = true; // Подсказки выбраны по умолчанию
        private bool showCode = true; // Код сигнала отображается по умолчанию
        private bool fromSignalsMove = false; // Переход из панели выбора сигналов
        double s_prDamp = 0; // Площадь для приточной заслонки
        double s_outDamp = 0; // Площадь для вытяжной заслонки
        double s_recircDamp = 0; // Площадь для рециркуляционной заслонки
        int torq_prDamp = 0; // Крутящий момент для приточной заслонки
        int torq_outDamp = 0; // Крутящий момент для вытяжной заслонки
        int torq_recircDamp = 0; // Крутящий момент для рециркуляционной заслонки

        // Класс для всплывающих подсказок (основные элементы)
        readonly ToolTip toolTip = new ToolTip
        {
            AutoPopDelay = 3000,
            InitialDelay = 1000,
            ReshowDelay = 500,
            ShowAlways = true
        };

        // Класс для всплывающих подсказок (зеленые галочки подбора приводов)
        readonly ToolTip driveTip = new ToolTip
        {
            AutoPopDelay = 3000,
            InitialDelay = 1000,
            ReshowDelay = 500,
            ShowAlways = true
        };

        public Form1()
        {
            InitializeComponent();          // Загрузка конструктора формы
            BlockTabControlInitial();       // Скрытие вкладок элементов
            SelectComboBoxesInitial();      // Изначальный выбор для comboBox
            SizePanels();                   // Изменение размера панелей
            SizeForm();                     // Изменение размера основной формы
            ClearIO_codes();                // Очистка наименования кодов для входов/выходов
        }

        // Изменение размера формы
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            const int deltaW_tabControl = 245; // Промежуток по ширине, панель с вкладками tabControl1
            const int deltaH_tabControl = 155; // Промежуток по высоте, панель с вкладками tabControl1
            const int deltaW_FanPanel = 278; // Промежуток по ширине, панель приточного вентилятора prFanPanel
            const int deltaW_panel = 210; // Промежуток по ширине, панель с выбором элементов panel1
            const int height_panel1 = 120; // Высота для панели panel1
            var s1 = new Size(Size.Width - deltaW_tabControl, Size.Height - deltaH_tabControl);
            var s2 = new Size(Size.Width - deltaW_FanPanel, prFanPanel.Height);
            var s3 = new Size(Size.Width - deltaW_FanPanel, outFanPanel.Height);
            var s4 = new Size(Size.Width - deltaW_FanPanel, filterPanel.Height);
            var s5 = new Size(Size.Width - deltaW_FanPanel, sensorsPanel.Height);
            var s6 = new Size(Size.Width - deltaW_FanPanel, dampPanel.Height);
            var s7 = new Size(Size.Width - deltaW_FanPanel, heatPanel.Height);
            var s8 = new Size(Size.Width - deltaW_FanPanel, coolPanel.Height);
            var s9 = new Size(Size.Width - deltaW_FanPanel, humidPanel.Height);
            var s10 = new Size(Size.Width - deltaW_FanPanel, recircPanel.Height);
            var s11 = new Size(Size.Width - deltaW_FanPanel, recupPanel.Height);
            var s12 = new Size(Size.Width - deltaW_FanPanel, secHeatPanel.Height);
            var p1 = new Point(Size.Width - deltaW_panel, height_panel1);
            // Изменение размеров
            tabControl1.Size = s1; // Изменение размера панели с вкладками
            prFanPanel.Size = s2;
            outFanPanel.Size = s3;
            filterPanel.Size = s4;
            sensorsPanel.Size = s5;
            dampPanel.Size = s6;
            heatPanel.Size = s7;
            coolPanel.Size = s8;
            humidPanel.Size = s9;
            recircPanel.Size = s10;
            recupPanel.Size = s11;
            secHeatPanel.Size = s12;
            // Изменение положения
            panelElements.Location = p1;
            PicturesMove(Size.Width); // Перемещение изображений
            PDF_ReSize(Size.Width, Size.Height); // Область для отображения PDF
            SignalsTableReSize(Size.Width, Size.Height); // Таблица сигналов
        }

        // Переменещение изображений элементов 
        private void PicturesMove(int width)
        {
            const int fan_height = 3;
            const int fan1_delta = 458;
            const int fan2_delta = 437;
            const int filter_delta = 409;
            const int sensors_delta = 411;
            const int damp_delta = 421;
            const int heat_delta = 416;
            const int humid_delta = 449;
            const int recirc_delta = 418;
            const int recup_delta = 398;
            const int secHeat_delta = 416;
            const int recup_2_delta = 507;

            var p1 = new Point(width - fan1_delta, fan_height);
            var p2 = new Point(width - fan2_delta, fan_height);
            var p3 = new Point(width - filter_delta, fan_height);
            var p4 = new Point(width - sensors_delta, fan_height);
            var p5 = new Point(width - damp_delta, fan_height);
            var p6 = new Point(width - heat_delta, fan_height);
            var p7 = new Point(width - heat_delta, fan_height);
            var p8 = new Point(width - humid_delta, fan_height);
            var p9 = new Point(width - recirc_delta, fan_height);
            var p10 = new Point(width - recup_delta, fan_height);
            var p11 = new Point(width - secHeat_delta, fan_height);
            var p12 = new Point(width - recup_2_delta, fan_height);
            fanPicture1.Location = p1;
            fanPicture2.Location = p2;
            filterPicture.Location = p3;
            sensorPicture.Location = p4;
            dampPicture.Location = p5; 
            heatPicture.Location = p6;
            coolPicture.Location = p7;
            humidPicture.Location = p8;
            recircPicture.Location = p9;
            heatAddPicture.Location = p11;
            // Два варианта для рекуператора
            if (recupTypeCombo.SelectedIndex == 0)
                recupPicture.Location = p10;
            else
                recupPicture.Location = p12;
        }

        ///<summary>Изменение размера области для отображения руковоства PDF</summary>
        private void PDF_ReSize(int width, int height)
        {
            helpPanel.Width = width - 50;
            helpPanel.Height = height - 50;
            axAcroPDF1.Width = helpPanel.Width;
            axAcroPDF1.Height = helpPanel.Height - 140;
        }

        ///<summary>Изменение размера области для таблицы сигналов</summary>
        private void SignalsTableReSize(int width, int height)
        {
            signalsPanel.Width = width - 50;
            signalsPanel.Height = height - 150;
            tabControlSignals.Width = signalsPanel.Width - 20;
            tabControlSignals.Height = signalsPanel.Height - 50;
        }

        ///<summary>Изначальный выбор для comboBox</summary>
        private void SelectComboBoxesInitial()
        {
            comboSysType.SelectedItem = "П-система"; // Выбор системы изначально
            filterPrCombo.SelectedItem = "1";
            filterOutCombo.SelectedItem = "0";
            prFanPowCombo.SelectedItem = "380 В";
            prFanControlCombo.SelectedItem = "Внешние контакты";
            outFanPowCombo.SelectedItem = "380 В";
            outFanControlCombo.SelectedItem = "Внешние контакты";
            prDampPowCombo.SelectedItem = "24 В";
            outDampPowCombo.SelectedItem = "24 В";
            heatTypeCombo.SelectedItem = "Водяной";
            powPumpCombo.SelectedItem = "230 В";
            elHeatStagesCombo.SelectedItem = "1";
            thermSwitchCombo.SelectedItem = "0";
            coolTypeCombo.SelectedItem = "Фреоновый";
            frCoolStagesCombo.SelectedItem = "1";
            powWatCoolCombo.SelectedItem = "24 В";
            humidTypeCombo.SelectedItem = "Паровой";
            recircPowCombo.SelectedItem = "24 В";
            recupTypeCombo.SelectedItem = "Роторный";
            rotorPowCombo.SelectedItem = "230 В";
            heatAddTypeCombo.SelectedItem = "Водяной";
            elHeatAddStagesCombo.SelectedItem = "1";
            thermAddSwitchCombo.SelectedItem = "0";
            powPumpAddCombo.SelectedItem = "230 В";
            bypassPlastCombo.SelectedItem = "Нет";
            firstStHeatCombo.SelectedItem = "Дискретное";
            firstStAddHeatCombo.SelectedItem = "Дискретное";
            comboReadType.SelectedItem = "Все сигналы";
            fireTypeCombo.SelectedItem = "НО";
        }

        // Установка размера для панелей настройки элементов
        private void SizePanels()
        {
            watHeatPanel.Height = height;
            steamHumidPanel.Height = height;
            watAddHeatPanel.Height = height;
            rotorRecupPanel.Height = height;
        }

        // Установка размера формы
        private void SizeForm()
        {
            this.Width = 995;
            this.Height = 680; // 610, 630
        }

        // Очистка для подписей кодов у comboBox входов/выходов
        private void ClearIO_codes()
        {
            // Сигналы DO, ПЛК
            DO1_lab.Text = ""; DO2_lab.Text = ""; DO3_lab.Text = ""; DO4_lab.Text = "";
            DO5_lab.Text = ""; DO6_lab.Text = ""; DO7_lab.Text = "";
            // Сигналы DO, блок 1
            DO1bl1_lab.Text = ""; DO2bl1_lab.Text = ""; DO3bl1_lab.Text = ""; DO4bl1_lab.Text = "";
            DO5bl1_lab.Text = ""; DO6bl1_lab.Text = ""; DO7bl1_lab.Text = "";
            // Сигналы DO, блок 2
            DO1bl2_lab.Text = ""; DO2bl2_lab.Text = ""; DO3bl2_lab.Text = ""; DO4bl2_lab.Text = "";
            DO5bl2_lab.Text = ""; DO6bl2_lab.Text = ""; DO7bl2_lab.Text = "";
            // Сигналы DO, блок 3
            DO1bl3_lab.Text = ""; DO2bl3_lab.Text = ""; DO3bl3_lab.Text = ""; DO4bl3_lab.Text = "";
            DO5bl3_lab.Text = ""; DO6bl3_lab.Text = ""; DO7bl3_lab.Text = "";
            // Сигналы AO, ПЛК
            AO1_lab.Text = ""; AO2_lab.Text = ""; AO3_lab.Text = "";
            // Сигналы AO, блок 1
            AO1bl1_lab.Text = ""; AO2bl1_lab.Text = ""; AO3bl1_lab.Text = "";
            // Сигналы AO, блок 2
            AO1bl2_lab.Text = ""; AO2bl2_lab.Text = ""; AO3bl2_lab.Text = "";
            // Сигнаыл AO, блок 3
            AO1bl3_lab.Text = ""; AO2bl3_lab.Text = ""; AO3bl3_lab.Text = "";
            // Сигналы AI, ПЛК
            AI1_lab.Text = ""; AI2_lab.Text = ""; AI3_lab.Text = ""; AI4_lab.Text = "";
            AI5_lab.Text = ""; AI6_lab.Text = "";
            // Сигналы AI, блок 1
            AI1bl1_lab.Text = ""; AI2bl1_lab.Text = ""; AI3bl1_lab.Text = ""; AI4bl1_lab.Text = "";
            AI5bl1_lab.Text = ""; AI6bl1_lab.Text = "";
            // Сигналы AI, блок 2
            AI1bl2_lab.Text = ""; AI2bl2_lab.Text = ""; AI3bl2_lab.Text = ""; AI4bl2_lab.Text = "";
            AI5bl2_lab.Text = ""; AI6bl2_lab.Text = "";
            // Сигналы AI, блок 3
            AI1bl3_lab.Text = ""; AI2bl3_lab.Text = ""; AI3bl3_lab.Text = ""; AI4bl3_lab.Text = "";
            AI5bl3_lab.Text = ""; AI6bl3_lab.Text = "";
            // Сигналы DI, ПЛК
            DI1_lab.Text = ""; DI2_lab.Text = ""; DI3_lab.Text = ""; DI4_lab.Text = "";
            DI5_lab.Text = "";
            // Сигналы DI, блок 1
            DI1bl1_lab.Text = ""; DI2bl1_lab.Text = ""; DI3bl1_lab.Text = ""; DI4bl1_lab.Text = "";
            DI5bl1_lab.Text = "";
            // Сигналы DI, блок 2
            DI1bl2_lab.Text = ""; DI2bl2_lab.Text = ""; DI3bl2_lab.Text = ""; DI4bl2_lab.Text = "";
            DI5bl2_lab.Text = "";
            // Сигналы DI, блок 3
            DI1bl3_lab.Text = ""; DI2bl3_lab.Text = ""; DI3bl3_lab.Text = ""; DI4bl3_lab.Text = "";
            DI5bl3_lab.Text = "";
        }

        // Выход из программы, "Выход" в меню
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Вы действительно хотите выйти?";
            const string caption = "Выход";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes) this.Close(); // Выход из приложения
        }

        // При загрузке Form1, всплывающие подсказки
        private void Form1_Load(object sender, EventArgs e)
        {
            string ai_sig = "Добавляет AI сигнал";
            string di_sig = "Добавляет DI сигнал";
            string do_sig = "Добавляет DO сигнал";
            string ao_sig = "Добавляет AO сигнал";
            string ps = "Добавляет DI сигнал и датчик давления";
            string drive = "Привод добавлен в спецификацию";
            toolTip.Active = hintEnabled;
            // Датчики
            toolTip.SetToolTip(prChanSensCheck, ai_sig);
            toolTip.SetToolTip(roomTempSensCheck, ai_sig);
            toolTip.SetToolTip(chanHumSensCheck, ai_sig);
            toolTip.SetToolTip(roomHumSensCheck, ai_sig);
            toolTip.SetToolTip(outChanSensCheck, ai_sig);
            // Вентилятор
            toolTip.SetToolTip(prFanPSCheck, ps);
            toolTip.SetToolTip(outFanPSCheck, ps);
            // Заслонка
            toolTip.SetToolTip(confPrDampCheck, di_sig);
            toolTip.SetToolTip(confOutDampCheck, di_sig);
            toolTip.SetToolTip(heatPrDampCheck, do_sig);
            toolTip.SetToolTip(heatOutDampCheck, do_sig);
            // Нагреватель
            toolTip.SetToolTip(TF_heaterCheck, di_sig);
            // Охладитель
            toolTip.SetToolTip(alarmFrCoolCheck, di_sig);
            // Второй нагреватель
            toolTip.SetToolTip(TF_addHeaterCheck, di_sig);
            // Увлажнитель
            toolTip.SetToolTip(alarmHumidCheck, di_sig);
            // Рециркуляция
            toolTip.SetToolTip(recircAOSigCheck, ao_sig);
            // Рекуператор
            toolTip.SetToolTip(recDefTempCheck, ai_sig);
            toolTip.SetToolTip(recDefPsCheck, di_sig);
            toolTip.SetToolTip(pumpGlicRecCheck, do_sig);
            toolTip.SetToolTip(outSigAlarmRotRecCheck, di_sig);
            toolTip.SetToolTip(startRotRecCheck, do_sig);
            // Зеленые галочки при подборе приводов заслонок
            driveTip.SetToolTip(markPrDampPanel, drive);
            driveTip.SetToolTip(markOutDampPanel, drive);
            driveTip.SetToolTip(markRecircPanel, drive);
            // Изменение размера для tabControl выбора оборудования
            tabControl1.Height = 465; 
            Form1_InitCmdWord(this, e); // Подготовка командных слов
            Form1_InitSignals(this, e); // Подготовка сигналов ПЛК
            // Изменения для панели данных записи 
            LoadNetOnLoad();
            Form1_SizeChanged(this, e); // Изменение размеров для формы
        }

        /// <summary>Скрытие всех вкладок элементов</summary>
        private void BlockTabControlInitial()
        {
            filterPage.Parent = null;
            dampPage.Parent = null;
            heatPage.Parent = null;
            coolPage.Parent = null;
            humidPage.Parent = null;
            recircPage.Parent = null;
            recupPage.Parent = null;
            addHeatPage.Parent = null;
        }

        ///<summary>Проверка выбора опций для разблокировки типа системы</summary>

        private void CheckOptions()
        {
            List<bool> options = new List<bool> {
                filterCheck.Checked,
                dampCheck.Checked,
                heaterCheck.Checked,
                coolerCheck.Checked,
                humidCheck.Checked,
                recircCheck.Checked,
                recupCheck.Checked
            };

            if (options.All(el => el == false))
                comboSysType.Enabled = true;
        }

        ///<summary>Выбрали нагреватель</summary>
        private void HeaterCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (heaterCheck.Checked)
            {   // Выбрали нагреватель
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                heatPage.Parent = tabControl1;
                prChanSensCheck.Checked = true;
                prChanSensCheck.Enabled = false;
            }
            else
            {   // Отмена выбора нагревателя
                heatPage.Parent = null;
                CheckOptions();
                if (!coolerCheck.Checked && !addHeatCheck.Checked) // Не выбран охладитель и второй нагреватель
                {
                    prChanSensCheck.Checked = false;
                    prChanSensCheck.Enabled = true;
                }
            }
            HeaterCheck_cmdCheckedChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            HeaterCheck_signalsDOCheckedChanged(this, e); // Сигналы DO ПЛК
            HeaterCheck_signalsAOCheckedChanged(this, e); // Сигналы AO ПЛК
            HeaterCheck_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
            HeaterCheck_signalsAICheckedChanged(this, e); // Сигналы AI ПЛК
        }

        ///<summary>Выбрали охладитель</summary>
        private void CoolerCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (coolerCheck.Checked)
            { // Выбрали охладитель
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                coolPage.Parent = tabControl1;
                prChanSensCheck.Checked = true;
                prChanSensCheck.Enabled = false;
            }
            else
            { // Отмена выбора охладителя
                coolPage.Parent = null;
                CheckOptions();
                if (!heaterCheck.Checked && !addHeatCheck.Checked) // Не выбран нагреватель и второй нагреватель
                {
                    prChanSensCheck.Checked = false;
                    prChanSensCheck.Enabled = true;
                }
            }
            CoolerCheck_cmdCheckedChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            CoolerCheck_signalsDOCheckedChanged(this, e); // Сигналы DO ПЛК
            CoolerCheck_signalsAOCheckedChanged(this, e); // Сигналы AO ПЛК
            CoolerCheck_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
        }

        ///<summary>Выбрали увлажнитель</summary>
        private void HumidCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (humidCheck.Checked)
            {
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                humidPage.Parent = tabControl1;
            }
            else
            {
                humidPage.Parent = null;
                CheckOptions();
            }
            CheckHumidSensors(); // Проверка датчиков влажности
            HumidCheck_cmdCheckedChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            HumidCheck_signalsDOCheckedChanged(this, e); // Сигналы DO ПЛК
            HumidCheck_signalsAOCheckedChanged(this, e); // Сигналы AO ПЛК
            HumidCheck_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
        }

        ///<summary>Выбрали рециркуляцию</summary>
        private void RecircCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (recircCheck.Checked)
            {
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                recircPage.Parent = tabControl1;
            }
            else
            {
                recircPage.Parent = null;
                CheckOptions();
            }
            RecircCheck_cmdCheckedChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            RecircCheck_signalsAOCheckedChanged(this, e); // Сигналы AO ПЛК
        }

        ///<summary>Выбрали рекуператор</summary>
        private void RecupCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (recupCheck.Checked) // Выбран рекуператор
            {
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                recupPage.Parent = tabControl1;
                defRecupSensPanel.Show();
                // Выбор и блокировка приточного канального датчика температуры
                if (!prChanSensCheck.Checked) prChanSensCheck.Checked = true;
                prChanSensCheck.Enabled = false;
            }
            else // Отмена выбора рекуператора
            {
                prChanSensCheck.Enabled = true; // Разблокировка канального датчика температуры
                recupPage.Parent = null;
                defRecupSensPanel.Hide();
                CheckOptions();
            }
            RecupCheck_cmdCheckedChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            RecupCheck_signalsDOCheckedChanged(this, e); // Сигналы DO ПЛК
            RecupCheck_signalsAOCheckedChanged(this, e); // Сигналы AO ПЛК
            RecupCheck_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
            RecupCheck_signalsAICheckedChanged(this, e); // Сигналы AI ПЛК
        }

        ///<summary>Нажали на кнопку "Сброс"</summary>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            comboSysType.Enabled = true; // Разблокировка выбора типа системы
            comboSysType.SelectedIndex = 0; // Выбор приточной системы
            filterCheck.Checked = false;
            dampCheck.Checked = false;
            heaterCheck.Checked = false;
            addHeatCheck.Checked = false;
            coolerCheck.Checked = false;
            humidCheck.Checked = false;
            recircCheck.Checked = false;
            recupCheck.Checked = false;
            // Блокировка для рециркуляции и рекуператора
            recircCheck.Enabled = false;
            recupCheck.Enabled = false;
            outFanPanel.Hide();
            SelectComboBoxesInitial(); // Возврат к изначальным значения выбора
            // Блокровка датчиков для ПВ-системы
            outChanSensCheck.Enabled = false;
            outChanSensCheck.Checked = false;
            // Блокировка элементов
            prFanControlCombo.Enabled = false;
            outFanControlCombo.Enabled = false;
            chanHumSensCheck.Enabled = false;
            roomHumSensCheck.Enabled = false;
            fireTypeCombo.Enabled = false;
            ResetElementsOptions(); // Сброс настроек для элементов
            ResetButton_signalsDOClick(this, e); // Сброс сигналов ПЛК, DO
            ResetButton_signalsAOClick(this, e); // Сброс сигналов ПЛК, AO
            ResetButton_signalsDIClick(this, e); // Сброс сигналов ПЛК, DI
            ResetButton_signalsAIClick(this, e); // Сброс сигналов ПЛК, AI
            ClearIO_codes(); // Очистка кодов для comboBox
            // Сброс опции для таблицы сигналов
            showWriteBoxCheck.Checked = false;
            Form1_InitSignals(this, e); // Начальная расстановка сигналов
        }

        ///<summary>Сброс настроек для всего оборудования</summary>
        private void ResetElementsOptions()
        {
            ResetFansOptions();         // Сброс настроек для вентиляторов
            ResetDampOptions();         // Сброс настроек для заслонок
            ResetHeaterOptions();       // Сброс настроек для нагревателя
            ResetAddHeaterOptions();    // Сброс настроек для доп нагрева
            ResetCoolerOptions();       // Сброс настроек для охладителя
            ResetHumidOptions();        // Сброс настроек для увлажнителя
            ResetRecircOptions();       // Сброс настроек для рециркуляции
            ResetRecupOpitons();        // Сброс настроек для рекуператора
            ResetSensors();             // Сброс настроек для датчиков
        }

        private void ResetFansOptions()
        {
            // Приточный вентилятор
            prFanPSCheck.Checked = false;
            prFanFC_check.Checked = false;
            powPrFanBox.Text = "1,5";
            prFanThermoCheck.Checked = false;
            curDefPrFanCheck.Checked = false;
            checkResPrFan.Checked = false;
            powPrResFanBox.Text = "1,5";
            prFanPowSupCheck.Checked = false;
            prFanAlarmCheck.Checked = false;
            prFanStStopCheck.Checked = false;
            prFanSpeedCheck.Checked = false;

            // Вытяжной вентилятор
            outFanPSCheck.Checked = false;
            outFanFC_check.Checked = false;
            powOutFanBox.Text = "1,5";
            outFanThermoCheck.Checked = false;
            curDefOutFanCheck.Checked = false;
            checkResOutFan.Checked = false;
            powOutResFanBox.Text = "1,5";
            outFanPowSupCheck.Checked = false;
            outFanAlarmCheck.Checked = false;
            outFanStStopCheck.Checked = false;
            outFanSpeedCheck.Checked = false;
        }

        private void ResetDampOptions()
        {
            b_prDampBox.Text = "";
            h_prDampBox.Text = "";
            b_outDampBox.Text = "";
            h_outDampBox.Text = "";
            confPrDampCheck.Checked = false;
            heatPrDampCheck.Checked = false;
            springRetPrDampCheck.Checked = false;
            confOutDampCheck.Checked = false;
            heatOutDampCheck.Checked = false;
            outDampCheck.Checked = false;
            springRetOutDampCheck.Checked = false;
        }

        private void ResetHeaterOptions()
        {
            TF_heaterCheck.Checked = false;
            confHeatPumpCheck.Checked = false;
            elHeatPowBox.Text = "4,0";
        }

        private void ResetAddHeaterOptions()
        {
            TF_addHeaterCheck.Checked = false;
            pumpAddHeatCheck.Checked = true;
            confAddHeatPumpCheck.Checked = false;
            sensWatAddHeatCheck.Checked = false;
            elAddHeatPowBox.Text = "4,0";
        }

        private void ResetCoolerOptions()
        {
            alarmFrCoolCheck.Checked = false;
            dehumModeCheck.Checked = false;
            analogFreonCheck.Checked = false;
        }

        private void ResetHumidOptions()
        {
            alarmHumidCheck.Checked = false;
        }

        private void ResetRecircOptions()
        {
            b_recircBox.Text = "";
            h_recircBox.Text = "";
            springRetRecircCheck.Checked = false;
        }

        private void ResetRecupOpitons()
        {
            recDefTempCheck.Checked = false;
            recDefPsCheck.Checked = false;
            powRotRecBox.Text = "0,18";
            pumpGlicRecCheck.Checked = false;
        }

        private void ResetSensors()
        {
            prChanSensCheck.Checked = false;
            roomTempSensCheck.Checked = false;
            outdoorChanSensCheck.Checked = false;
            sigWorkCheck.Checked = false;
            sigAlarmCheck.Checked = false;
            ignoreEvents = true; // Отключение событий
            stopStartCheck.Checked = true;
            fireCheck.Checked = false;
            ignoreEvents = false; // Возврат обработки событий
        }

        ///<summary>Изменили тип системы</summary>
        private void ComboSysType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSysType.SelectedIndex == 0) // Приточная система
            {
                recircCheck.Enabled = false;
                recupCheck.Enabled = false;
                outFanPanel.Visible = false;
                outFilterPanel.Visible = false;
                outDampPanel.Visible = false;
                outChanSensCheck.Enabled = false;
            }
            else // Приточно-вытяжная система
            {
                recircCheck.Enabled = true;
                recupCheck.Enabled = true;
                outFanPanel.Visible = true;
                outFilterPanel.Visible = true;
                outDampPanel.Visible = true;
                outChanSensCheck.Enabled = true;
            }
            if (comboSysType.SelectedIndex == 1) comboSysType.Enabled = false; // Блокировка выбора типа системы
            ComboSysType_cmdSelectedIndexChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            ComboSysType_signalsSelectedIndexChanged(this, e); // Сигналы ПЛК
        }

        ///<summary>Выбрали фильтр</summary>
        private void FilterCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (filterCheck.Checked) // Выбрали фильтр
            {
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                filterPage.Parent = tabControl1;
                sigFilAlarmCheck.Enabled = true; // Разблокировка сигнала "Загрязнение фильтра"
            }
            else // Отмена выбора фильтра
            {
                filterPage.Parent = null;
                sigFilAlarmCheck.Enabled = false; // Блокировка сигнала "Загрязнение фильтра"
                CheckOptions();
            }
            FilterCheck_cmdCheckedChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            SigFilAlarmCheck_signalsDOCheckedChanged(this, e); // Обработка для сигнала DO фильтра
            FilterCheck_signalsDICheckedChanged(this, e); // Сигналы DI
        }

        /// <summary>Выбрали заслонку</summary>
        private void DampCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (dampCheck.Checked) // Выбрана заслонка
            {
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                dampPage.Parent = tabControl1;
            }
            else // Отмена выбора заслонки
            {
                dampPage.Parent = null;
                CheckOptions();
            }
            DampCheck_cmdCheckedChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            DampCheck_signalsDOCheckedChanged(this, e); // Сигналы DO ПЛК
            DampCheck_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
        }

        ///<summary>Изменили тип нагревателя</summary>
        private void HeatTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Point a = new Point(3, 36);
            if (heatTypeCombo.SelectedIndex == 1) // Электрокалорифер
            {
                watHeatPanel.Hide();
                elHeatPanel.Show();
                heatPicture.Image = Resource1.electroHeater;
                elHeatPanel.Location = a;
            }
            else // Водяной калорифер
            {
                elHeatPanel.Hide();
                watHeatPanel.Show();
                heatPicture.Image = Resource1.waterHeater;
                watHeatPanel.Location = a;
            }
            HeatTypeCombo_cmdSelectedIndexChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            HeatTypeCombo_signalsDOSelectedIndexChanged(this, e); // Сигналы DO ПЛК
            HeatTypeCombo_signalsAOSelectedIndexChanged(this, e); // Сигналы AO ПЛК
            HeatTypeCombo_signalsDISelectedIndexChanged(this, e); // Сигналы DI ПЛК
            HeatTypeCombo_signalsAISelectedIndexChanged(this, e); // Сигналы AI ПЛК
        }

        ///<summary>Изменили тип охладителя</summary>
        private void CoolTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Point a = new Point(3, 36);
            if (coolTypeCombo.SelectedIndex == 1) // Водяной охладитель
            {
                frCoolPanel.Hide();
                watCoolPanel.Show();
                coolPicture.Image = Resource1.waterCooler;
                watCoolPanel.Location = a;
            }
            else // Фреоновый охладитель
            {
                watCoolPanel.Hide();
                frCoolPanel.Show();
                coolPicture.Image = Resource1.freonCooler;
                frCoolPanel.Location = a;
            }
            CheckHumidSensors(); // Проверка датчиков влажности
            CoolTypeCombo_cmdSelectedIndexChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            CoolTypeCombo_signalsDOSelectedIndexChanged(this, e); // Сигналы DO
            CoolTypeCombo_signalsDISelectedIndexChanged(this, e); // Сигналы DI
            CoolTypeCombo_signalsAOSelectedIndexChanged(this, e); // Сигналы AO
        }

        ///<summary>Изменили тип увлажнителя</summary>
        private void HumidTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Point a = new Point(3, 36);
            if (humidTypeCombo.SelectedIndex == 1) // Сотовый 
            {
                steamHumidPanel.Hide();
                cellHumidPanel.Show();
                cellHumidPanel.Location = a;
            }
            else // Паровой
            {
                cellHumidPanel.Hide();
                steamHumidPanel.Show();
                steamHumidPanel.Location = a;
            }
            HumidTypeCombo_cmdSelectedIndexChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            HumidTypeCombo_signalsDOSelectedIndexChanged(this, e); // Сигналы DO ПЛК
            HumidTypeCombo_signalsAOSelectedIndexChanged(this, e); // Сигналы AO ПЛК
            HumidTypeCombo_signalsDISelectedIndexChanged(this, e); // Сигналы DI ПЛК
        }

        ///<summary>Изменили тип рекуператора</summary>
        private void RecupTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            const int delta_1 = 398;
            const int delta_2 = 507;
            const int height = 3;
            Point a = new Point(3, 36);
            if (recupTypeCombo.SelectedIndex == 1) // Пластинчатый
            {
                Point p1 = new Point(this.Size.Width - delta_2, height);
                rotorRecupPanel.Hide();
                glikRecupPanel.Hide();
                plastRecupPanel.Show();
                recupPicture.Image = Resource1.plastRecup;
                recupPicture.Size = new System.Drawing.Size(226, 215);
                recupPicture.Location =  p1;
                plastRecupPanel.Location = a;
            }
            else if (recupTypeCombo.SelectedIndex == 0) // Роторный
            {
                Point p1 = new Point(this.Size.Width - delta_1, height);
                plastRecupPanel.Hide();
                glikRecupPanel.Hide();
                rotorRecupPanel.Show();
                recupPicture.Image = Resource1.rotorRecup;
                recupPicture.Size = new System.Drawing.Size(117, 221);
                recupPicture.Location = p1;
                rotorRecupPanel.Location = a;
            }
            else if (recupTypeCombo.SelectedIndex == 2) // Гликолевый
            {
                Point p1 = new Point(this.Size.Width - delta_2, height);
                plastRecupPanel.Hide();
                rotorRecupPanel.Hide();
                glikRecupPanel.Show();
                recupPicture.Image = Resource1.plastRecup;
                recupPicture.Size = new System.Drawing.Size(226, 215);
                recupPicture.Location = p1;
                glikRecupPanel.Location = a;
            }
            RecupTypeCombo_cmdSelectedIndexChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            RecupTypeCombo_signalsDOSelectedIndexChanged(this, e); // Сигналы DO ПЛК
            RecupTypeCombo_signalsAOSelectedIndexChanged(this, e); // Сигналы AO ПЛК
            RecupTypeCombo_signalsDISelectedIndexChanged(this, e); // Сигналы DI ПЛК
        }

        ///<summary>Выбрали резерв приточного вентилятора</summary>
        private void CheckResPrFan_CheckedChanged(object sender, EventArgs e)
        {
             if (checkResPrFan.Checked) // Выбрали резерв приточного
            {
                labelResPrFan.Show(); powPrResFanBox.Show(); labelResPrFan_2.Show();
            }
            else // Отмена выбора резерва приточного
            {
                labelResPrFan.Hide(); powPrResFanBox.Hide(); labelResPrFan_2.Hide();
            }
            CheckResPrFan_cmdCheckedChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            CheckResPrFan_signalsDOCheckedChanged(this, e); // Сигналы DO ПЛК
            CheckResPrFan_signalsAOCheckedChanged(this, e); // Сигналы AO ПЛК
            CheckResPrFan_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
        }

        ///<summary>Выбрали резерв вытяжного вентилятора</summary>
        private void CheckResOutFan_CheckedChanged(object sender, EventArgs e)
        {
            if (checkResOutFan.Checked) // Выбрали резерв вытяжного
            {
                labelResOutFan.Show(); powOutResFanBox.Show(); labelResOutFan_2.Show();
            }
            else // Отмена выбора резерва вытяжного
            {
                labelResOutFan.Hide(); powOutResFanBox.Hide(); labelResOutFan_2.Hide();
            }
            CheckResOutFan_cmdCheckedChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            CheckResOutFan_signalsCheckedChanged(this, e); // Сигналы DO ПЛК
            CheckResOutFan_signalsAOCheckedChanged(this, e); // Сигналы AO ПЛК
            CheckResOutFan_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
        }

        ///<summary>Изменили тип догревателя</summary>
        private void HeatAddTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var a = new Point(3, 36);
            if (heatAddTypeCombo.SelectedIndex == 1) // Электрокалорифер
            {
                watAddHeatPanel.Hide();
                elAddHeatPanel.Show();
                heatAddPicture.Image = Resource1.electroHeater;
                elAddHeatPanel.Location = a;
            }
            else // Водный калорифер
            {
                elAddHeatPanel.Hide();
                watAddHeatPanel.Show();
                heatAddPicture.Image = Resource1.waterHeater;
                watAddHeatPanel.Location = a;
            }
            HeatAddTypeCombo_cmdSelectedIndexChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            HeatAddTypeCombo_signalsDOSelectedIndexChanged(this, e); // Сигналы DO ПЛК
            HeatAddTypeCombo_signalsAOSelectedIndexChanged(this, e); // Сигналы AO ПЛК
            HeatAddTypeCombo_signalsDISelectedIndexChanged(this, e); // Сигналы DI ПЛК
            HeatAddTypeCombo_signalsAISelectedIndexChanged(this, e); // Сигналы AI ПЛК
        }

        // Функция проверки доступности датчиков влажности
        private void CheckHumidSensors()
        {
            // Выбран доп.нагрев + фреон + осушиние ИЛИ увлажнитель
            if ((addHeatCheck.Checked && coolerCheck.Checked && coolTypeCombo.SelectedIndex == 0 && dehumModeCheck.Checked) || humidCheck.Checked)
            {
                roomHumSensCheck.Checked = true;
                chanHumSensCheck.Checked = true;
                chanHumSensCheck.Enabled = true;
            }
            else
            {
                roomHumSensCheck.Checked = false;
                chanHumSensCheck.Checked = false;
                chanHumSensCheck.Enabled = false;
            }
        }

        ///<summary>Выбрали ПЧ приточного вентилятора</summary>
        private void PrFanFC_check_CheckedChanged(object sender, EventArgs e)
        {
            if (prFanFC_check.Checked) // Выбрали ПЧ приточного вентилятора
            {
                prFanControlCombo.Enabled = true;
                // Разблокировка опций для ПЧ
                if (prFanControlCombo.SelectedIndex == 0) // Только для внешних контактов
                    prFanPowSupCheck.Enabled = true;
                prFanAlarmCheck.Enabled = true;
                prFanSpeedCheck.Enabled = true;
            }  
            else // Отмена выбора ПЧ приточного вентилятора
            {
                prFanControlCombo.Enabled = false;
                // Блокировка выбора опций для ПЧ
                prFanPowSupCheck.Enabled = false;
                prFanAlarmCheck.Enabled = false;
                prFanSpeedCheck.Enabled = false;
            }
            PrFanFC_check_cmdCheckedChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            PrFanFC_check_signalsAOCheckedChanged(this, e); // Сигналы AO ПЛК
            PrFanFC_check_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
            PrFanFC_check_signalsDOCheckedChanged(this, e); // Сигналы DO ПЛК
        }

        ///<summary>Выбрали ПЧ вытяжного вентилятора</summary>
        private void OutFanFC_check_CheckedChanged(object sender, EventArgs e)
        {
            if (outFanFC_check.Checked) // Выбрали ПЧ вытяжного вентилятора
            {
                outFanControlCombo.Enabled = true;
                // Разблокировка опций для ПЧ
                if (outFanControlCombo.SelectedIndex == 0) // Только для внешних контактов
                    outFanPowSupCheck.Enabled = true;
                outFanAlarmCheck.Enabled = true;
                outFanSpeedCheck.Enabled = true;
            } 
            else // Отмена выбора ПЧ вытяжного вентилятора
            {
                outFanControlCombo.Enabled = false;
                // Блокировка выбора опций для ПЧ
                outFanPowSupCheck.Enabled = false;
                outFanAlarmCheck.Enabled = false;
                outFanSpeedCheck.Enabled = false;
            }
            OutFanFC_check_cmdCheckedChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            OutFanFC_check_signalsAOCheckedChanged(this, e); // Сигналы AO ПЛК
            OutFanFC_check_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
            OutFanFC_check_signalsDOCheckedChanged(this, e); // Сигналы DO ПЛК
        }

        private void DehumModeCheck_CheckedChanged(object sender, EventArgs e)
        {
            CheckHumidSensors(); // Проверка датчиков влажности
            DehumModeCheck_cmdCheckedChanged(this, e); // Командное слово
        }

        private void PumpAddHeatCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (pumpAddHeatCheck.Checked)
            {
                powPumpAddCombo.Enabled = true;
                confAddHeatPumpCheck.Enabled = true;
            }
            else
            {
                powPumpAddCombo.Enabled = false;
                confAddHeatPumpCheck.Enabled = false;
            }
            PumpAddHeatCheck_cmdCheckedChanged(this, e); // Командное слово
        }

        ///<summary>Выбрали догреватель</summary>
        private void AddHeatCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (addHeatCheck.Checked) // Выбрали второй нагрев
            {
                addHeatPage.Parent = tabControl1;
                dehumModeCheck.Show();
                prChanSensCheck.Checked = true;
                prChanSensCheck.Enabled = false;
            }
            else // Отмена выбора второго нагрева
            {
                addHeatPage.Parent = null;
                dehumModeCheck.Hide();
                if (!heaterCheck.Checked && !coolerCheck.Checked) // Нет выбранного нагревателя и охладителя
                {
                    prChanSensCheck.Checked = false;
                    prChanSensCheck.Enabled = true;
                }
            }
            CheckHumidSensors(); // Проверка датчиков влажности
            AddHeatCheck_cmdCheckedChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            AddHeatCheck_signalsDOCheckedChanged(this, e); // Сигналы DO ПЛК
            AddHeatCheck_signalsAOCheckedChanged(this, e); // Сигналы AO ПЛК
            AddHeatCheck_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
            AddHeatCheck_signalsAICheckedChanged(this, e); // Сигналы AI ПЛК
        }

        ///<summary>Выбрали вытяжную воздушную заслонку</summary>
        private void OutDampCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (outDampCheck.Checked) // Выбрана вытяжная заслонка
            {
                outDampPowCombo.Enabled = true;
                confOutDampCheck.Enabled = true;
                heatOutDampCheck.Enabled = true;
                // Элементы для подбора приводов
                springRetOutDampCheck.Enabled = true;
                bOutDampLabel.Show(); hOutDampLabel.Show();
                b_outDampBox.Show(); h_outDampBox.Show();
                cmbOutDampLabel.Show(); cmhOutDampLabel.Show();
                B_outDampBox_TextChanged(this, e); // Пересчет для привода 
            }
            else // Отмена выбора вытяжной заслонки
            {
                outDampPowCombo.Enabled = false;
                confOutDampCheck.Enabled = false;
                heatOutDampCheck.Enabled = false;
                // Элементы для подбора приводов
                springRetOutDampCheck.Enabled = false;
                bOutDampLabel.Hide(); hOutDampLabel.Hide();
                b_outDampBox.Hide(); h_outDampBox.Hide();
                cmbOutDampLabel.Hide(); cmhOutDampLabel.Hide();
                outDampSLabel.Hide(); outDampTorqLabel.Hide();
                markOutDampPanel.Hide();
            }
            OutDampCheck_cmdCheckedChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            OutDampCheck_signalsDOCheckedChanged(this, e); // Сигналы DO ПЛК
            OutDampCheck_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
        }

        ///<summary>Нажали на вкладку "Настройка"</summary>
        private void ToolStripMenuItem_load_Click(object sender, EventArgs e)
        {
            Point p1 = new Point(15, 90);
            tabControl1.Hide(); // Скрытие панели опций элементов
            signalsPanel.Hide(); // Скрытие панели распределения сигналов
            helpPanel.Hide(); // Скрытие панели отображения помощи
            parameterPanel.Hide(); // Скрытие панели параметров
            label_comboSysType.Text = "ЗАГРУЗКА ПРОГРАММЫ";
            comboSysType.Hide(); panelElements.Hide();
            loadPanel.Location = p1;
            loadPanel.Height = 468; // 390
            loadPanel.Show();
            formSignalsButton.Hide(); // Скрытие кнопки "Сформировать"
            //ToolStripMenuItem_help.Enabled = false;
        }

        ///<summary>Нажали на вкладку "Параметры"</summary>
        private void ToolStripMenuItem_parameter_Click(object sender, EventArgs e)
        {
            Point p1 = new Point(15, 90);
            tabControl1.Hide(); // Скрытие панели опций элементов
            signalsPanel.Hide(); // Скрытие панели распределения сигналов
            helpPanel.Hide(); // Скрытие панели отображения помощи
            loadPanel.Hide(); // Скрытие панели настроек
            label_comboSysType.Text = "ПАРАМЕТРЫ ПРОГРАММЫ";
            comboSysType.Hide(); panelElements.Hide();
            parameterPanel.Location = p1;
            parameterPanel.Height = 468;
            parameterPanel.Show();
            formSignalsButton.Hide(); // Скрытие кнопки "Сформировать"
        }

        ///<summary>Нажали вкладку "Помощь" в главном меню</summary>
        private void ToolStripMenuItem_help_Click(object sender, EventArgs e)
        {
            Point p1 = new Point(15, 90);
            tabControl1.Hide(); // Скрытие панели опций элементов
            signalsPanel.Hide(); // Скрытие панели распределения сигналов
            parameterPanel.Hide(); // Скрытие панели параметров
            label_comboSysType.Text = "ПОМОЩЬ И РУВОДСТВО";
            comboSysType.Hide(); panelElements.Hide();
            helpPanel.Location = p1;
            helpPanel.Height = 485;
            helpPanel.Width = Width - 50; // Ширина по границе окна
            helpPanel.Show();
            formSignalsButton.Hide(); // Скрытие кнопки "Сформировать"
            //ToolStripMenuItem_options.Enabled = false; // Блокировка "Настройки"
            axAcroPDF1.Width = helpPanel.Width; // Ширина по границе панели
            axAcroPDF1.src = System.IO.Directory.GetCurrentDirectory() + @"\ManualPDF.pdf"; // В папке приложения
            PDF_ReSize(Size.Width, Size.Height); // Область для отображения PDF
            //ToolStripMenuItem_help.Enabled = false; // Блокировка повторного выбора "Помощь"
        }

        // Нажали кнопку "Назад" в панели настроек
        private void BackOptionsButton_Click(object sender, EventArgs e)
        {
            if (!fromSignalsMove) // Переход был не из панели выбора сигналов
            {
                loadPanel.Hide(); // Скрытие панели настроек
                tabControl1.Show(); // Отображение панели опции элементов 
                label_comboSysType.Text = "ТИП СИСТЕМЫ";
                comboSysType.Show();
                panelElements.Show();
                formSignalsButton.Show(); // Отображение кнопки "Сформировать"
            }
            else
            {
                FormSignalsButton_Click(this, e); // Переход на панель выбора сигналов
            }
        }

        ///<summary>Нажали кнопку "Назад" в панели помощи</summary>
        private void BackHelpButton_Click(object sender, EventArgs e)
        {
            helpPanel.Hide(); // Скрытие панели помощи
            loadPanel.Hide(); // Скрытие панели настроек
            tabControl1.Show(); panelElements.Show();
            label_comboSysType.Text = "ТИП СИСТЕМЫ";
            comboSysType.Show();
            formSignalsButton.Show(); // Отображение кнопки "Сформировать"
        }

        ///<summary>Нажали кнопку "Назад" в панели параметров</summary>
        private void BackParameterButton_Click(object sender, EventArgs e)
        {
            parameterPanel.Hide(); // Скрытие панели параметров
            tabControl1.Show(); panelElements.Show();
            label_comboSysType.Text = "ТИП СИСТЕМЫ";
            comboSysType.Show();
            formSignalsButton.Show(); // Отображение кнопки "Сформировать"
        }

        // Опция для включения всплывающих подсказок
        private void ShowHintCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (showHintCheck.Checked) // Всплывающие подсказки включены
                hintEnabled = true;
            else // Всплывающие подсказки отключены
                hintEnabled = false;
            Form1_Load(this, e); // Обработка всплывающих подсказок
        }

        ///<summary>Нажали кнопку "Далее" в панели сигналов</summary>
        private void NextSignalsButton_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem_load_Click(this, e); // Открытие панели настроек
            fromSignalsMove = true; // Переход из панели выбора сигналов
            FormNetButton_Click(this, e); // Формирование списка сигналов для записи
        }

        ///<summary>Нажали на ссылку сайта ONI</summary>
        private void LinkOniWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkOniWeb.LinkVisited = true;
            System.Diagnostics.Process.Start("https://oni-system.com/otraslevye-resheniya/ventilacya/");
        }

        // Настройка для поля мощности основного приточного вентилятора
        private void PowPrFanBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Числа, точка и Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != (char)Keys.Back) 
                e.Handled = true;
        }

        // Настройка для поля мощности резервного приточного вентилятора
        private void PowPrResFanBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Числа, точка и Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        // Настройка для поля мощности основного вытяжного вентилятора
        private void PowOutFanBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Числа, точка и Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        // Настройка для поля мощности резервного вытяжного вентилятора
        private void PowOutResFanBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Числа, точка и Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        // Настройка для поля мощности основного электрического калорифера
        private void ElHeatPowBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Числа, точка и Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        // Настройка для поля мощности второго электрического калорифера
        private void ElAddHeatPowBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Числа, точка и Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        // Настройка для поля мощности роторного рекуператора
        private void PowRotRecBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Числа, точка и Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        // Нажали на пункт "О программе"
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Отображение информационного окна
            FormInfo fi = new FormInfo();
            fi.Show(this);
        }

        ///<summary>Настройка поля для ширины приточной заслонки</summary>
        private void B_prDampBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Числа и Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) e.Handled = true;
        }

        ///<summary>Настройка поля для высоты приточной заслонки</summary>
        private void H_prDampBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Числа и Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) e.Handled = true;
        }

        ///<summary>Настройка поля для ширины вытяжной заслонки</summary>
        private void B_outDampBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Числа и Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) e.Handled = true;
        } 

        ///<summary>Настройка поля для высоты вытяжной заслонки</summary>
        private void H_outDampBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Числа и Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) e.Handled = true;
        }

        ///<summary>Настройка поля для ширины рециркуляционной заслонки</summary>
        private void B_recircBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Числа и Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) e.Handled = true;
        }

        ///<summary>Настройка поля для высоты рециркуляционной заслонки</summary>
        private void H_recircBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Числа и Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) e.Handled = true;
        }

        ///<summary>Загрузка бинарного файла для контроллера</summary>
        private void SaveBinFileButton_Click(object sender, EventArgs e)
        {
            var filePath = System.IO.Directory.GetCurrentDirectory(); // Для бинарного файла
            SaveFileDialog dlg = new SaveFileDialog(); // Окно для сохранения файла
            dlg.FileName = "ONI_HVAC_PLC_v1.4.5"; // Имя файла по умолчанию
            dlg.DefaultExt = ".zip";
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dlg.Filter = "Zip file(.zip)|*.zip";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(dlg.FileName, File.ReadAllBytes(filePath + "/ONI_HVAC_PLC_v1.4.5.zip"));
            }
        }
    }
}
