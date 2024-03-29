﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Moderon
{
    public partial class Form1 : Form
    {
        // Статус для состояния входов/выходов
        readonly static public string NOT_SELECTED = "Не выбрано"; 

        private const int HEIGHT = 280;                                 // Высота для панелей настройки элементов
        private const int DELTA = 31;                                   // Расстояние между comboBox в таблице сигналов
        private Point MENU_POSITION = new Point(3, 36);                 // Позиция для меню элементов
        private Point PANEL_POSITION = new Point(15, 90);               // Позиция для остальных панелей
        readonly private bool showCode = true;                          // Код сигнала отображается по умолчанию в таблице сигналов
        
        private bool initialConfigure = true;                           // Признак начальной конфигурации (при загрузке и после сброса)
        private bool optimizeOnly = true;                               // Признак 8 датчиков температуры (для отключения разблокировки типа ПЛК)
        
        // Ранее сохраненные значения индексов для элементов
        private int plkChangeIndexLast = 1;                             // Значение для выбранного типа контроллера
        private int heatTypeComboIndex = 0;                             // Значение для типа основного нагревателя
        private int coolTypeComboIndex = 0;                             // Значение для типа охладителя
        private int heatAddTypeComboIndex = 0;                          // Значение для типа дополнительного нагревателя
        private int humidTypeComboIndex = 0;                            // Значение для типа увлажнителя
        private int recupTypeComboIndex = 0;                            // Значение для типа рекуператора
        
        private bool 
            hintEnabled = true,                                         // Отображение подсказок выбрано по умолчанию
            fromSignalsMove = false;                                    // Переход из панели выбора сигналов
        
        double 
            s_prDamp = 0,                                               // Площадь для приточной заслонки
            s_outDamp = 0,                                              // Площадь для вытяжной заслонки
            s_recircDamp = 0;                                           // Площадь для рециркуляционной заслонки
        
        int 
            torq_prDamp = 0,                                            // Крутящий момент для приточной заслонки
            torq_outDamp = 0,                                           // Крутящий момент для вытяжной заслонки
            torq_recircDamp = 0;                                        // Крутящий момент для рециркуляционной заслонки

        // Класс для всплывающих подсказок (основные элементы)
        readonly ToolTip toolTip = new ToolTip
        {
            AutoPopDelay = 3000, InitialDelay = 1000, ReshowDelay = 500, ShowAlways = true
        };

        // Класс для всплывающих подсказок (зеленые галочки подбора приводов)
        readonly ToolTip driveTip = new ToolTip
        {
            AutoPopDelay = 3000, InitialDelay = 1000, ReshowDelay = 500, ShowAlways = true
        };

        public Form1()
        {
            InitializeComponent();          // Загрузка конструктора формы
            BlockTabControlInitial();       // Скрытие вкладок элементов
            SelectComboBoxesInitial();      // Изначальный выбор для comboBox
            SizePanels();                   // Изменение размера панелей
            ClearIO_codes();                // Очистка наименования кодов для входов/выходов

            InitialSet_ComboTextIndex();    // Начальная установка для входов и выходов
          
            // Подготовка для блоков расширения
            DoCombosBlocks_Reset();         // Скрытие и блокировка comboBox DO блоков расширение, скрытие Label подписей
            UiCombosBlocks_Reset();         // Скрытие и блокировка comboBox UI блоков расширение, скрытие Label подписей

            Size = new Size(995, 730);      // Размер для основной формы (было 995, 680)
        }

        ///<summary>Начальная установка для входов и выходов</summary>
        private void InitialSet_ComboTextIndex()
        {
            Set_UIComboTextIndex();         // Входные сигналы, UI
            Set_AOComboTextIndex();         // Аналоговый выходы, AO
            Set_DOComboTextIndex();         // Дискретные выходы, DO
        }

        ///<summary>Изменение размера формы</summary>
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            const int
                deltaW_tabControl = 245,            // Промежуток по ширине, панель с вкладками tabControl1
                deltaH_tabControl = 155,            // Промежуток по высоте, панель с вкладками tabControl1
                deltaW_FanPanel = 278,              // Промежуток по ширине, панель приточного вентилятора prFanPanel
                deltaW_panel = 210,                 // Промежуток по ширине, панель с выбором элементов panel1
                height_panel1 = 96,                 // Высота для панели panel1
                logo_X_delta = 58;                  // Промежуток по X для изображения логотипа Moderon

            // Изменение размеров для панелей
            mainPage.Size = new Size(Size.Width - deltaW_tabControl, Size.Height - deltaH_tabControl);
            prFanPanel.Size = new Size(Size.Width - deltaW_FanPanel, prFanPanel.Height);
            outFanPanel.Size = new Size(Size.Width - deltaW_FanPanel, outFanPanel.Height);
            filterPanel.Size = new Size(Size.Width - deltaW_FanPanel, filterPanel.Height);
            sensorsPanel.Size = new Size(Size.Width - deltaW_FanPanel, sensorsPanel.Height);
            dampPanel.Size = new Size(Size.Width - deltaW_FanPanel, dampPanel.Height);
            heatPanel.Size = new Size(Size.Width - deltaW_FanPanel, heatPanel.Height);
            coolPanel.Size = new Size(Size.Width - deltaW_FanPanel, coolPanel.Height);
            humidPanel.Size = new Size(Size.Width - deltaW_FanPanel, humidPanel.Height);
            recircPanel.Size = new Size(Size.Width - deltaW_FanPanel, recircPanel.Height);
            recupPanel.Size = new Size(Size.Width - deltaW_FanPanel, recupPanel.Height);
            secHeatPanel.Size = new Size(Size.Width - deltaW_FanPanel, secHeatPanel.Height);

            // Положение для панели элементов, панели блоков расширения и изображения подбора оборудования
            panelElements.Location = new Point(Size.Width - deltaW_panel, height_panel1);
            panelBlocks.Location = new Point(Size.Width - deltaW_panel, height_panel1 + 
                panelElements.Height + BETWEEN_PANELS);
            pic_signalsReady.Location = new Point(panelElements.Location.X, panelElements.Location.Y - BETWEEN_PANELS * 4);
            pictureBoxLogo.Location = new Point(panelElements.Location.X + logo_X_delta, pictureBoxLogo.Location.Y);

            // Положение для блока защиты рекуператора
            defRecupSensPanel.Location = new Point(3, 365);

            PicturesMove(Size.Width);                       // Перемещение изображений
            PDF_ReSize(Size.Width, Size.Height);            // Область для отображения PDF
            SignalsTableReSize(Size.Width, Size.Height);    // Таблица сигналов
        }

        /// <summary>Переменещение изображений элементов при изменении размера основной формы</summary>
        private void PicturesMove(int width)
        {
            const int 
                fan_height = 3, fan1_delta = 458, fan2_delta = 437, filter_delta = 435,
                sensors_delta = 422, damp_delta = 395, heat_delta = 430, humid_delta = 420,
                recirc_delta = 415, recup_delta = 398, secHeat_delta = 416, recup_2_delta = 507;

            // Положения для элементов
            fanPicture1.Location = new Point(width - fan1_delta, fan_height);
            fanPicture2.Location = new Point(width - fan2_delta, fan_height);
            filterPicture.Location = new Point(width - filter_delta, fan_height);
            sensorPicture.Location = new Point(width - sensors_delta, fan_height);
            dampPicture.Location = new Point(width - damp_delta, fan_height);
            heatPicture.Location = new Point(width - heat_delta, fan_height);
            coolPicture.Location = new Point(width - heat_delta, fan_height);
            humidPicture.Location = new Point(width - humid_delta, fan_height);
            recircPicture.Location = new Point(width - recirc_delta, fan_height);
            heatAddPicture.Location = new Point(width - secHeat_delta, fan_height);

            // Два варианта для рекуператора
            recupPicture.Location = recupTypeCombo.SelectedIndex == 0 ? 
                new Point(width - recup_delta, fan_height) : new Point(width - recup_2_delta, fan_height);
        }

        ///<summary>Скрытие и блокировка панелей блоков расширения</summary>
        private void HideExpansionBlocksPanels()
        {
            // Аналоговые выходы AO для блока M72E12RB
            var block_AO = new List<Panel>()
            {
                block1_AOpanel, block2_AOpanel, block3_AOpanel
            };

            // Дискретные выходы DO для блоков M72E12RB (4), M72E12RA (6), M72E08RA (8)
            var block_DO = new List<Panel>()
            {
                block1_DOpanel, block2_DOpanel, block3_DOpanel
            };

            foreach (var el in block_AO) { el.Hide(); el.Enabled = false; }         // Скрытие и блокировка для AO панелей
            foreach (var el in block_DO) { el.Hide(); el.Enabled = false; }         // Скрытие и блокировка для DO панелей
        }

        ///<summary>Изменение размера области для отображения руковоства PDF</summary>
        private void PDF_ReSize(int width, int height)
        {
            helpPanel.Size = new Size(width - 50, height - 50);
            PDF_manual.Size = new Size(helpPanel.Width, helpPanel.Height - 140);
        }

        ///<summary>Изменение размера области для таблицы сигналов</summary>
        private void SignalsTableReSize(int width, int height)
        {
            signalsPanel.Size = new Size(width - 50, height - 150);
            tabControlSignals.Size = new Size(signalsPanel.Width - 20, signalsPanel.Height - 50);
        }

        ///<summary>Изначальный выбор для comboBox</summary>
        private void SelectComboBoxesInitial()
        {
            var elements = new List<ComboBox>()
            {
                comboSysType, comboPlkType, filterPrCombo, filterOutCombo, prFanPowCombo, prFanControlCombo,
                prFanFcTypeCombo, outFanFcTypeCombo,
                outFanPowCombo, outFanControlCombo, prDampPowCombo, outDampPowCombo, heatTypeCombo,
                powPumpCombo, elHeatStagesCombo, thermSwitchCombo, coolTypeCombo, frCoolStagesCombo,
                powWatCoolCombo, humidTypeCombo, recircPowCombo, recupTypeCombo, rotorPowCombo,
                heatAddTypeCombo, elHeatAddStagesCombo, thermAddSwitchCombo, powPumpAddCombo,
                bypassPlastCombo, firstStHeatCombo, firstStAddHeatCombo, comboReadType, fireTypeCombo
            };

            foreach (var element in elements) element.SelectedIndex = 0;
        }

        /// <summary>Установка размера для панелей настройки элементов</summary>
        private void SizePanels()
        {
            watHeatPanel.Height = watAddHeatPanel.Height = 400;

            var panels = new List<Panel>()
            {
                steamHumidPanel, rotorRecupPanel
            };

            foreach (var panel in panels) panel.Height = HEIGHT;
            
        }
        
        /// <summary>Очистка для подписей кодов у comboBox входов/выходов</summary>
        private void ClearIO_codes()
        {
            var do_signals = new List<Label>() 
            {
                DO1_lab, DO2_lab, DO3_lab, DO4_lab, DO5_lab, DO6_lab,
                DO1bl1_lab, DO2bl1_lab, DO3bl1_lab, DO4bl1_lab, DO5bl1_lab, DO6bl1_lab, DO7bl1_lab, DO8bl1_lab,
                DO1bl2_lab, DO2bl2_lab, DO3bl2_lab, DO4bl2_lab, DO5bl2_lab, DO6bl2_lab, DO7bl2_lab, DO8bl2_lab,
                DO1bl3_lab, DO2bl3_lab, DO3bl3_lab, DO4bl3_lab, DO5bl3_lab, DO6bl3_lab, DO7bl3_lab, DO8bl3_lab
            };

            var ao_signals = new List<Label>()
            {
                AO1_lab, AO2_lab, AO3_lab, 
                AO1bl1_lab, AO2bl1_lab,  
                AO1bl2_lab, AO2bl2_lab,
                AO1bl3_lab, AO2bl3_lab, 
            };

            var ui_signals = new List<Label>()
            {
                // ПЛК
                UI1_lab, UI2_lab, UI3_lab, UI4_lab, UI5_lab, UI6_lab,
                UI7_lab, UI8_lab, UI9_lab, UI10_lab, UI11_lab,
                // Блок расширения 1
                UI1bl1_lab, UI2bl1_lab, UI3bl1_lab, UI4bl1_lab, UI5bl1_lab, UI6bl1_lab, UI7bl1_lab, UI8bl1_lab,
                UI9bl1_lab, UI10bl1_lab, UI11bl1_lab, UI12bl1_lab, UI13bl1_lab, UI14bl1_lab, UI15bl1_lab, UI16bl1_lab,
                // Блок расширения 2
                UI1bl2_lab, UI2bl2_lab, UI3bl2_lab, UI4bl2_lab, UI5bl2_lab, UI6bl2_lab, UI7bl2_lab, UI8bl2_lab,
                UI9bl2_lab, UI10bl2_lab, UI11bl2_lab, UI12bl2_lab, UI13bl2_lab, UI14bl2_lab, UI15bl2_lab, UI16bl2_lab,
                // Блок расширения 3
                UI1bl3_lab, UI2bl3_lab, UI3bl3_lab, UI4bl3_lab, UI5bl3_lab, UI6bl3_lab, UI7bl3_lab, UI8bl3_lab,
                UI9bl3_lab, UI10bl3_lab, UI11bl3_lab, UI12bl3_lab, UI13bl3_lab, UI14bl3_lab, UI15bl3_lab, UI16bl3_lab,
            };

            List<Label> signals = do_signals.Concat(ao_signals).Concat(ui_signals).ToList();

            foreach (var label in signals) label.Text = ""; 
        }

        /// <summary>Выход из программы, "Выход" в меню</summary>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string 
                MESSAGE = "Вы действительно хотите выйти?",
                CAPTION = "Выход";

            var result = MessageBox.Show(MESSAGE, CAPTION, MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes) Close(); // Выход из приложения
        }

        /// <summary>Назначение подсказок при загрузке Form1</summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            string
                ai_sig = "Добавляет AI сигнал",
                di_sig = "Добавляет DI сигнал",
                do_sig = "Добавляет DO сигнал",
                ao_sig = "Добавляет AO сигнал",
                ps = "Добавляет DI сигнал и датчик давления",
                drive = "Привод добавлен в спецификацию",
                pic_sig_ready = "Состояние карты входов/выходов",
                pic_refresh = "Обновить список COM портов";

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
            // Изображение для карты входов/выходов
            toolTip.SetToolTip(pic_signalsReady, pic_sig_ready);
            // Изображение для обновления списка CAN портов
            toolTip.SetToolTip(refreshCanPorts, pic_refresh);
            // Изменение размера для tabControl выбора оборудования
            mainPage.Height = 465; // 465
            Form1_InitCmdWord(this, e); // Подготовка командных слов
            Form1_InitSignals(this, e); // Подготовка сигналов ПЛК
            // Изменения для таблицы формирования сигналов
            signalsPanel.Width = 845;
            // Изменения для панели данных записи 
            LoadNetOnLoad();

            Form1_SizeChanged(this, e);                     // Изменение размеров для формы
            ComboPlkType_SelectedIndexChanged(this, e);     // Блокировка изначально входов/выходов для контроллера "Mini"
        }

        /// <summary>Скрытие всех вкладок элементов</summary>
        private void BlockTabControlInitial()
        {
            var pages = new List<TabPage>()
            {
                filterPage, dampPage, heatPage, coolPage, humidPage,
                recircPage, recupPage, addHeatPage
            };

            foreach (var el in pages) el.Parent = null;
        }

        ///<summary>Проверка выбора опций для разблокировки типа системы</summary>
        private void CheckOptions()
        {
            List<bool> options = new List<bool> {
                filterCheck.Checked, dampCheck.Checked, heaterCheck.Checked,
                addHeatCheck.Checked, coolerCheck.Checked, humidCheck.Checked,
                recircCheck.Checked, recupCheck.Checked
            };

            if (options.All(el => el == false)) comboSysType.Enabled = true;
        }

        ///<summary>Выбрали нагреватель</summary>
        private void HeaterCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (heaterCheck.Checked)                                            // Выбрали нагреватель
            {   
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                heatPage.Parent = mainPage;
                prChanSensCheck.Checked = true;
                prChanSensCheck.Enabled = false;
                if (!TF_heaterCheck.Checked) TF_heaterCheck.Checked = true;     // Выбор воздушного термостата по умолчанию
            }
            else                                                                // Отмена выбора нагревателя
            {   
                heatPage.Parent = null;
                CheckOptions();
                // Не выбран охладитель и второй нагреватель
                if (!coolerCheck.Checked && !addHeatCheck.Checked) 
                {
                    prChanSensCheck.Checked = false;
                    prChanSensCheck.Enabled = true;
                }
            }
            HeaterCheck_cmdCheckedChanged(this, e); // Командное слово
            if (ignoreEvents) return;
            HeaterCheck_signalsAOCheckedChanged(this, e);   // Сигналы AO ПЛК
            HeaterCheck_signalsDOCheckedChanged(this, e);   // Сигналы DO ПЛК
            HeaterCheck_signalsDICheckedChanged(this, e);   // Сигналы DI ПЛК
            HeaterCheck_signalsAICheckedChanged(this, e);   // Сигналы AI ПЛК
        }

        ///<summary>Выбрали охладитель</summary>
        private void CoolerCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (coolerCheck.Checked)                        // Выбрали охладитель
            { 
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                coolPage.Parent = mainPage;
                prChanSensCheck.Checked = true;
                prChanSensCheck.Enabled = false;
            }
            else                                            // Отмена выбора охладителя
            { 
                coolPage.Parent = null;
                CheckOptions();
                // Не выбран нагреватель и второй нагреватель
                if (!heaterCheck.Checked && !addHeatCheck.Checked) 
                {
                    prChanSensCheck.Checked = false;
                    prChanSensCheck.Enabled = true;
                }
            }
            CoolerCheck_cmdCheckedChanged(this, e);         // Командное слово
            if (ignoreEvents) return;
            CoolerCheck_signalsAOCheckedChanged(this, e);   // Сигналы AO ПЛК
            CoolerCheck_signalsDOCheckedChanged(this, e);   // Сигналы DO ПЛК
            CoolerCheck_signalsDICheckedChanged(this, e);   // Сигналы DI ПЛК
        }

        ///<summary>Выбрали увлажнитель</summary>
        private void HumidCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (humidCheck.Checked)                         // Выбрали увлажнитель
            {
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                humidPage.Parent = mainPage;
            }
            else                                            // Отмена выбора увлажнителя   
            {
                humidPage.Parent = null;
                CheckOptions();
            }
            CheckHumidSensors();                            // Проверка датчиков влажности
            HumidCheck_cmdCheckedChanged(this, e);          // Командное слово
            if (ignoreEvents) return;
            HumidCheck_signalsAOCheckedChanged(this, e);    // Сигналы AO ПЛК
            HumidCheck_signalsDOCheckedChanged(this, e);    // Сигналы DO ПЛК
            HumidCheck_signalsDICheckedChanged(this, e);    // Сигналы DI ПЛК
        }

        ///<summary>Выбрали дополнительный нагреватель</summary>
        private void AddHeatCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (addHeatCheck.Checked)                       // Выбрали второй нагрев
            {
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                addHeatPage.Parent = mainPage;
                dehumModeCheck.Show();
                prChanSensCheck.Checked = true;
                prChanSensCheck.Enabled = false;
            }
            else                                            // Отмена выбора второго нагрева
            {
                addHeatPage.Parent = null;
                dehumModeCheck.Hide();
                // Нет выбранного нагревателя и охладителя
                if (!heaterCheck.Checked && !coolerCheck.Checked) 
                {
                    prChanSensCheck.Checked = false;
                    prChanSensCheck.Enabled = true;
                }
                CheckOptions();
            }
            CheckHumidSensors();                            // Проверка датчиков влажности
            AddHeatCheck_cmdCheckedChanged(this, e);        // Командное слово
            if (ignoreEvents) return;
            AddHeatCheck_signalsAOCheckedChanged(this, e);  // Сигналы AO ПЛК
            AddHeatCheck_signalsDOCheckedChanged(this, e);  // Сигналы DO ПЛК
            AddHeatCheck_signalsDICheckedChanged(this, e);  // Сигналы DI ПЛК
            AddHeatCheck_signalsAICheckedChanged(this, e);  // Сигналы AI ПЛК
        }

        ///<summary>Выбрали рециркуляцию</summary>
        private void RecircCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (recircCheck.Checked)                        // Выбрали рециркуляцию
            {
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                recircPage.Parent = mainPage;
            }
            else                                            // Отмена выбора рециркуляции
            {
                recircPage.Parent = null;
                CheckOptions();
            }
            RecircCheck_cmdCheckedChanged(this, e);         // Командное слово
            if (ignoreEvents) return;
            RecircCheck_signalsAOCheckedChanged(this, e);   // Сигналы AO ПЛК
        }

        ///<summary>Выбрали в рециркуляции сигнал 0-10 В на приточную заслонку</summary>
        private void RecircPrDampAOCheck_CheckedChanged(object sender, EventArgs e)
        {
            RecircCheck_cmdCheckedChanged(this, e);                     // Командное слово
            RecircPrDampAOCheck_signalsAOCheckedChanged(this, e);       // Сигналы АО ПЛК
        }

        ///<summary>Выбрали рекуператор</summary>
        private void RecupCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (recupCheck.Checked) // Выбран рекуператор
            {
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                recupPage.Parent = mainPage;
                defRecupSensPanel.Show();
                // Выбор и блокировка приточного канального датчика температуры
                if (!prChanSensCheck.Checked) prChanSensCheck.Checked = true;
                prChanSensCheck.Enabled = false;
            }
            else // Отмена выбора рекуператора
            {
                prChanSensCheck.Enabled = true;             // Разблокировка канального датчика температуры
                recupPage.Parent = null;
                defRecupSensPanel.Hide();
                CheckOptions();
            }
            RecupCheck_cmdCheckedChanged(this, e);          // Командное слово
            if (ignoreEvents) return;
            RecupCheck_signalsAOCheckedChanged(this, e);    // Сигналы AO ПЛК
            RecupCheck_signalsDOCheckedChanged(this, e);    // Сигналы DO ПЛК
            RecupCheck_signalsDICheckedChanged(this, e);    // Сигналы DI ПЛК
            RecupCheck_signalsAICheckedChanged(this, e);    // Сигналы AI ПЛК
        }

        ///<summary>Выбрали насос гликолевого рекуператора</summary>
        private void PumpGlicRecCheck_checkedChanged(object sender, EventArgs e)
        {
            // Выбран гликолевый рекуператор, ПВ-система
            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked && recupTypeCombo.SelectedIndex == 2)        
            {
                if (pumpGlicRecCheck.Checked)                                   // Выбран насос гликолевого рекуператора
                {
                    pumpGlikConfCheck.Enabled = true;                           // Разблокировка элементов
                    pumpGlikCurProtect.Enabled = true;
                } 
                else                                                            // Отмена выбора насоса гликолевого рекуператора
                {
                    pumpGlikConfCheck.Checked = false;                          // Блокировка и снятие выбора элементов
                    pumpGlikCurProtect.Checked = false;
                    pumpGlikConfCheck.Enabled = false;
                    pumpGlikCurProtect.Enabled = false;
                }
            }
            PumpGlicRecCheck_cmdCheckedChanged(this, e);                        // Командное слово
        }

        ///<summary>Выбрали резервный насос гликолевого рекуператора</summary>
        private void ReservPumpGlik_CheckedChanged(object sender, EventArgs e)
        {
            // Выбран гликолевый рекуператор, ПВ-система
            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked && recupTypeCombo.SelectedIndex == 2)
            {
                if (reservPumpGlik.Checked)                                     // Выбран резервный насос гликолевого рекуператора
                {
                    confGlikResPumpCheck.Enabled = true;                        // Разблокировка элементов
                    pumpGlikResCurProtect.Enabled = true;
                }
                else                                                            // Отмена выбора резервного насоса гликолевого рекуператора
                {
                    confGlikResPumpCheck.Checked = false;                       // Блокировка и снятие выбора элементов
                    pumpGlikResCurProtect.Checked = false;
                    confGlikResPumpCheck.Enabled = false;
                    pumpGlikResCurProtect.Enabled = false;
                }
            }
            ReservPumpGlik_cmdCheckedChanged(this, e);                          // Командное слово
        }

        ///<summary>Нажали на кнопку "Сброс"</summary>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            comboSysType.Enabled = true;                    // Разблокировка выбора типа системы
            comboSysType.SelectedIndex = 0;                 // Выбор приточной системы
            expansion_blocks.Clear();                       // Очистка списка задействованных блоков расширения
            initialConfigure = true;                        // Возврат признака начальной расстановки системы
            optimizeOnly = false;                           // Сброс признака блокировка выбора ПЛК Optimize

            var mainOptions = new List<CheckBox>()
            {
                filterCheck, dampCheck, heaterCheck, addHeatCheck, showWriteBoxCheck,
                coolerCheck, humidCheck, recircCheck, recupCheck, outChanSensCheck
            };

            var checkElements = new List<CheckBox>()
            {
                recircCheck, recupCheck, chanHumSensCheck, roomHumSensCheck, outChanSensCheck
            };

            var comboElements = new List<ComboBox>()
            {
                prFanControlCombo, outFanControlCombo, fireTypeCombo
            };
            
            foreach (var el in mainOptions) el.Checked = false;
            foreach (var el in checkElements) el.Enabled = false;
            foreach (var el in comboElements) el.Enabled = false;

            loadCanButton.Enabled = false;                  // Блокировка кнопки загрузки данных в ПЛК
            readCanButton.Enabled = false;                  // Блокировка кнопки чтения данных из ПЛК

            outFanPanel.Hide();                             // Скрытие панели вытяжного вентилятора
            HideExpansionBlocksPanels();                    // Скрытие панелей для блоков расширения в таблице сигналов
            SelectComboBoxesInitial();                      // Возврат к изначальным значения выбора
            ResetElementsOptions();                         // Сброс настроек для элементов
            ResetSignalsLists();                            // Очистка массивов сигналов
            ResetButton_signalsUIClick(this, e);            // Сброс сигналов ПЛК, UI
            ResetButton_signalsDOClick(this, e);            // Сброс сигналов ПЛК, DO
            ResetButton_signalsAOClick(this, e);            // Сброс сигналов ПЛК, AO
            ClearIO_codes();                                // Очистка кодов для comboBox
            // Очистка панелей для блоков расширения
            DoCombosBlocks_Reset();                         // Блок и скрытие элементов для DO панелей блоков расширения
            UiCombosBlocks_Reset();                         // Блок и скрытие элементов для UI панелей блоков расширения
            HidePanelBlocks();                              // Скрытие отображения для блоков расширения
            Hide_panelBlocks_elements();                    // Скрытие элементов для панели блоков расширения
            InitialSet_ComboTextIndex();                    // Изначальная установка для подписей/индексов comboBox
            Form1_InitSignals(this, e);                     // Начальная расстановка сигналов
        }

        ///<summary>Сброс настроек для всего оборудования</summary>
        private void ResetElementsOptions()
        {
            ResetFansOptions();                             // Сброс настроек для вентиляторов
            ResetDampOptions();                             // Сброс настроек для заслонок
            ResetHeaterOptions();                           // Сброс настроек для нагревателя
            ResetAddHeaterOptions();                        // Сброс настроек для доп нагрева
            ResetCoolerOptions();                           // Сброс настроек для охладителя
            ResetHumidOptions();                            // Сброс настроек для увлажнителя
            ResetRecircOptions();                           // Сброс настроек для рециркуляции
            ResetRecupOpitons();                            // Сброс настроек для рекуператора
            ResetSensors();                                 // Сброс настроек для датчиков
        }

        /// <summary>Сброс настроек для вентиляторов</summary>
        private void ResetFansOptions()
        {
            var fanPrOutOptions = new List<CheckBox>()
            {
                prFanPSCheck, prFanFC_check, prFanThermoCheck, curDefPrFanCheck, checkResPrFan, 
                prFanAlarmCheck, prFanStStopCheck, prFanSpeedCheck,
                outFanPSCheck, outFanFC_check, outFanThermoCheck, curDefOutFanCheck, checkResOutFan,
                outFanAlarmCheck, outFanStStopCheck, outFanSpeedCheck
            };

            var fanTextBox = new List<TextBox>()
            {
                powPrFanBox, powPrResFanBox, powOutFanBox, powOutResFanBox
            };

            var fanOptionsUnenabled = new List<CheckBox>()
            {
                prFanAlarmCheck, prFanSpeedCheck, outFanAlarmCheck, outFanSpeedCheck
            };

            foreach (var el in fanPrOutOptions) el.Checked = false;
            foreach (var el in fanTextBox) el.Text = "1,5";
            foreach (var el in fanOptionsUnenabled) el.Enabled = false;
        }

        /// <summary>Сброс настроек для воздушных заслонок</summary>
        private void ResetDampOptions()
        {
            var dampText = new List<TextBox>()
            {
                b_prDampBox, h_prDampBox, b_outDampBox, h_outDampBox
            };

            var dampCheck = new List<CheckBox>()
            {
                confPrDampCheck, heatPrDampCheck, springRetPrDampCheck, confOutDampCheck,
                heatOutDampCheck, outDampCheck, springRetOutDampCheck
            };

            foreach (var el in dampText) el.Text = "";
            foreach (var el in dampCheck) el.Checked = false;
        }

        /// <summary>Сброс настроек для основного нагревателя</summary>
        private void ResetHeaterOptions()
        {
            TF_heaterCheck.Checked = false;             // Термостат
            confHeatPumpCheck.Checked = false;          // Подтверждение работы основного насоса
            elHeatPowBox.Text = "4,0";                  // Мощность ступени
            pumpCurProtect.Checked = false;             // Защита по току основного насоса
            reservPumpHeater.Checked = false;           // Резервный насос калорифера
            // Подтверждение работы резервного насоса
            confHeatResPumpCheck.Checked = false; confHeatResPumpCheck.Enabled = false;
            // Защита резервного насоса по току
            pumpCurResProtect.Checked = false; pumpCurResProtect.Enabled = false;
        }

        /// <summary>Сброс настроек для дополнительного (второго) нагревателя</summary>
        private void ResetAddHeaterOptions()
        {
            var addHeatCheck = new List<CheckBox>()
            {
                TF_addHeaterCheck, confAddHeatPumpCheck, sensWatAddHeatCheck
            };

            foreach (var el in addHeatCheck) el.Checked = false;
            
            pumpAddHeatCheck.Checked = true;
            elAddHeatPowBox.Text = "4,0";

            pumpCurAddProtect.Checked = false;      // Защита по току основного насоса
            reservPumpAddHeater.Checked = false;    // Резервный насос калорифера
            // Подтверждение работы резервного насоса
            confAddHeatResPumpCheck.Checked = false; confAddHeatResPumpCheck.Enabled = false;
            // Защита резервного насоса по току
            pumpCurResAddProtect.Checked = false; pumpCurResAddProtect.Enabled = false;
        }

        /// <summary>Сброс настроек для охладителя</summary>
        private void ResetCoolerOptions()
        {
            var coolerCheck = new List<CheckBox>()
            {
                alarmFrCoolCheck, dehumModeCheck, analogFreonCheck
            };

            foreach (var el in coolerCheck) el.Checked = false;
        }

        /// <summary>Сброс настроек для увлажнителя</summary>
        private void ResetHumidOptions()
        {
            alarmHumidCheck.Checked = false;
        }

        /// <summary>Сброс настроек для рециркуляции</summary>
        private void ResetRecircOptions()
        {
            b_recircBox.Text = "";
            h_recircBox.Text = "";
            springRetRecircCheck.Checked = false;
            recircPrDampAOCheck.Checked = false;
        }

        /// <summary>Сброс настроек для рекуператора</summary>
        private void ResetRecupOpitons()
        {
            // Защита рекуператора
            recDefTempCheck.Checked = false;
            recDefPsCheck.Checked = false;
            // Роторный рекуператор
            powRotRecBox.Text = "0,18";
            // Гликолевый рекуператор, основной насос
            pumpGlicRecCheck.Checked = false;
            pumpGlikConfCheck.Checked = false;
            pumpGlikCurProtect.Checked = false;
            pumpGlikConfCheck.Enabled = false;
            pumpGlikCurProtect.Enabled = false;
            // Гликолевый рекуператор, резервный насос
            reservPumpGlik.Checked = false;
            confGlikResPumpCheck.Checked = false;
            pumpGlikResCurProtect.Checked = false;
            confGlikResPumpCheck.Enabled = false;
            pumpGlikResCurProtect.Enabled = false;
        }

        /// <summary>Сброс настроек для датчиков</summary>
        private void ResetSensors()
        {
            var sensorCheck = new List<CheckBox>()
            {
                prChanSensCheck, roomTempSensCheck, outdoorChanSensCheck,
                sigWorkCheck, sigAlarmCheck
            };

            foreach (var el in sensorCheck) el.Checked= false;
            
            ignoreEvents = true;                    // Отключение событий
            stopStartCheck.Checked = true;
            fireCheck.Checked = false;
            ignoreEvents = false;                   // Возврат обработки событий
        }

        ///<summary>Изменили тип системы</summary>
        private void ComboSysType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var prOutElements = new List<CheckBox>()
            {
                recircCheck, recupCheck, outChanSensCheck
            };

            var prOutPanels = new List<Panel>()
            {
                outFanPanel, outFilterPanel, outDampPanel
            };

            if (comboSysType.SelectedIndex == 0) // Приточная система
            {
                foreach (var el in prOutElements) el.Enabled= false;
                foreach (var el in prOutPanels) el.Hide();
            }
            else // Приточно-вытяжная система
            {
                foreach(var el in prOutElements) el.Enabled = true;
                foreach (var el in prOutPanels) el.Show();
            }
            
            if (comboSysType.SelectedIndex == 1) comboSysType.Enabled = false;      // Блокировка выбора типа системы
            ComboSysType_cmdSelectedIndexChanged(this, e);                          // Командное слово
            if (ignoreEvents) return;
            ComboSysType_signalsSelectedIndexChanged(this, e);                      // Сигналы ПЛК
        }

        ///<summary>Выбрали фильтр</summary>
        private void FilterCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (filterCheck.Checked)                                                // Выбрали фильтр
            {
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                filterPage.Parent = mainPage;
                sigFilAlarmCheck.Enabled = true;                                    // Разблокировка сигнала "Загрязнение фильтра"
            }
            else                                                                    // Отмена выбора фильтра
            {
                filterPage.Parent = null;
                sigFilAlarmCheck.Enabled = false;                                   // Блокировка сигнала "Загрязнение фильтра"
                CheckOptions();
            }
            FilterCheck_cmdCheckedChanged(this, e);                                 // Командное слово
            if (ignoreEvents) return;
            SigFilAlarmCheck_signalsDOCheckedChanged(this, e);                      // Обработка для сигнала DO фильтра
            FilterCheck_signalsDICheckedChanged(this, e);                           // Сигналы DI
        }

        /// <summary>Выбрали заслонку</summary>
        private void DampCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (dampCheck.Checked)                                                  // Выбрана заслонка
            {
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                dampPage.Parent = mainPage;
            }
            else                                                                    // Отмена выбора заслонки
            {
                dampPage.Parent = null;
                CheckOptions();
            }
            DampCheck_cmdCheckedChanged(this, e);                                   // Командное слово
            if (ignoreEvents) return;
            DampCheck_signalsDOCheckedChanged(this, e);                             // Сигналы DO ПЛК
            DampCheck_signalsDICheckedChanged(this, e);                             // Сигналы DI ПЛК
        }

        ///<summary>Изменили тип нагревателя</summary>
        private void HeatTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (heatTypeCombo.SelectedIndex == heatTypeComboIndex) return;          // Не поменялся выбор типа нагревателя

            if (heatTypeCombo.SelectedIndex == 1)                                   // Электрокалорифер
            {
                heatTypeComboIndex = 1;
                watHeatPanel.Hide(); elHeatPanel.Show();
                heatPicture.Image = Properties.Resources.electroHeater;
                elHeatPanel.Location = MENU_POSITION;
            }
            else // Водяной калорифер
            {
                heatTypeComboIndex = 0;
                elHeatPanel.Hide(); watHeatPanel.Show();
                heatPicture.Image = Properties.Resources.waterHeater;
                watHeatPanel.Location = MENU_POSITION;
            }
            HeatTypeCombo_cmdSelectedIndexChanged(this, e);                         // Командное слово
            if (ignoreEvents) return;
            HeatTypeCombo_signalsDOSelectedIndexChanged(this, e);                   // Сигналы DO ПЛК
            HeatTypeCombo_signalsAOSelectedIndexChanged(this, e);                   // Сигналы AO ПЛК
            HeatTypeCombo_signalsDISelectedIndexChanged(this, e);                   // Сигналы DI ПЛК
            HeatTypeCombo_signalsAISelectedIndexChanged(this, e);                   // Сигналы AI ПЛК
        }

        ///<summary>Выбрали резервный насос основного водяного нагревателя</summary>
        private void ReservPumpHeater_CheckedChanged(object sender, EventArgs e)
        {
            if (reservPumpHeater.Checked)                                           // Выбрали резервный насос
            {
                confHeatResPumpCheck.Enabled = true;                                // Разблокировка подтверждения работы
                pumpCurResProtect.Enabled = true;                                   // Разблокировка защиты по току
            }
            else                                                                    // Отмена выбора насоса
            {
                confHeatResPumpCheck.Checked = false;                               // Отмена выбора подтверждения работы
                pumpCurResProtect.Checked = false;                                  // Отмена выбора защиты по току
                confHeatResPumpCheck.Enabled = false;                               // Блокировка подтверждения работы
                pumpCurResProtect.Enabled = false;                                  // Блокировка защиты по току
            }
            ReservPumpHeater_cmdCheckedChanged(this, e);                            // Командное слово
            if (ignoreEvents) return;
            ReservPumpHeater_signalsDOCheckedChanged(this, e);                      // Сигналы DO ПЛК
        }

        ///<summary>Изменили тип охладителя</summary>
        private void CoolTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (coolTypeCombo.SelectedIndex == coolTypeComboIndex) return;          // Не поменялся выбор типа охладителя

            if (coolTypeCombo.SelectedIndex == 1)                                   // Водяной охладитель
            {
                coolTypeComboIndex = 1;
                //frCoolStagesCombo.SelectedIndex = 0;                              // Выбор 1 ступени фреонового охладителя до смены типа
                frCoolPanel.Hide(); watCoolPanel.Show();
                coolPicture.Image = Properties.Resources.waterCooler;
                watCoolPanel.Location = MENU_POSITION;
            }
            else                                                                    // Фреоновый охладитель
            {
                coolTypeComboIndex = 0;
                watCoolPanel.Hide(); frCoolPanel.Show();
                coolPicture.Image = Properties.Resources.freonCooler;
                frCoolPanel.Location = MENU_POSITION;
            }
            CheckHumidSensors();                                                    // Проверка датчиков влажности
            CoolTypeCombo_cmdSelectedIndexChanged(this, e);                         // Командное слово
            if (ignoreEvents) return;
            CoolTypeCombo_signalsDOSelectedIndexChanged(this, e);                   // Сигналы DO
            CoolTypeCombo_signalsDISelectedIndexChanged(this, e);                   // Сигналы DI
            CoolTypeCombo_signalsAOSelectedIndexChanged(this, e);                   // Сигналы AO
        }

        ///<summary>Изменили тип увлажнителя</summary>
        private void HumidTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (humidTypeCombo.SelectedIndex == humidTypeComboIndex) return;        // Не изменился тип увлажнителя

            if (humidTypeCombo.SelectedIndex == 1)                                  // Сотовый увлажнитель
            {
                humidTypeComboIndex = 1;
                steamHumidPanel.Hide(); cellHumidPanel.Show();
                cellHumidPanel.Location = MENU_POSITION;
            }
            else                                                                    // Паровой увлажнитель
            {
                humidTypeComboIndex = 0;
                cellHumidPanel.Hide(); steamHumidPanel.Show();
                steamHumidPanel.Location = MENU_POSITION;
            }
            HumidTypeCombo_cmdSelectedIndexChanged(this, e);                        // Командное слово
            if (ignoreEvents) return;
            HumidTypeCombo_signalsDOSelectedIndexChanged(this, e);                  // Сигналы DO ПЛК
            HumidTypeCombo_signalsAOSelectedIndexChanged(this, e);                  // Сигналы AO ПЛК
            HumidTypeCombo_signalsDISelectedIndexChanged(this, e);                  // Сигналы DI ПЛК
        }

        ///<summary>Изменили тип рекуператора</summary>
        private void RecupTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            const int delta_1 = 398, delta_2 = 507, height = 3;

            if (recupTypeCombo.SelectedIndex == recupTypeComboIndex) return;        // Не изменился тип рекуператора

            if (recupTypeCombo.SelectedIndex == 1)                                  // Пластинчатый
            {
                recupTypeComboIndex = 1;
                Point p1 = new Point(Size.Width - delta_2, height);
                rotorRecupPanel.Hide(); glikRecupPanel.Hide(); plastRecupPanel.Show();
                recupPicture.Image = Properties.Resources.plastRecup;
                recupPicture.Size = new Size(226, 215);
                recupPicture.Location =  p1;
                plastRecupPanel.Location = MENU_POSITION;
            }
            else if (recupTypeCombo.SelectedIndex == 0)                             // Роторный
            {
                recupTypeComboIndex = 0;
                Point p1 = new Point(Size.Width - delta_1, height);
                plastRecupPanel.Hide(); glikRecupPanel.Hide(); rotorRecupPanel.Show();
                recupPicture.Image = Properties.Resources.rotorRecup;
                recupPicture.Size = new Size(117, 221);
                recupPicture.Location = p1;
                rotorRecupPanel.Location = MENU_POSITION;
            }
            else if (recupTypeCombo.SelectedIndex == 2)                             // Гликолевый
            {
                recupTypeComboIndex = 2;
                Point p1 = new Point(Size.Width - delta_2, height);
                plastRecupPanel.Hide(); rotorRecupPanel.Hide(); glikRecupPanel.Show();
                recupPicture.Image = Properties.Resources.plastRecup;
                recupPicture.Size = new Size(226, 215);
                recupPicture.Location = p1;
                glikRecupPanel.Location = MENU_POSITION;
            }
            RecupTypeCombo_cmdSelectedIndexChanged(this, e);                        // Командное слово
            if (ignoreEvents) return;
            RecupTypeCombo_signalsDOSelectedIndexChanged(this, e);                  // Сигналы DO ПЛК
            RecupTypeCombo_signalsAOSelectedIndexChanged(this, e);                  // Сигналы AO ПЛК
            RecupTypeCombo_signalsDISelectedIndexChanged(this, e);                  // Сигналы DI ПЛК
        }

        ///<summary>Выбрали резерв приточного вентилятора</summary>
        private void CheckResPrFan_CheckedChanged(object sender, EventArgs e)
        {
            if (checkResPrFan.Checked)                                                      // Выбрали резерв приточного
            {
                labelResPrFan.Show(); powPrResFanBox.Show(); labelResPrFan_2.Show();
                prDampFanCheck.Enabled = true;                                              // Разблокировка выбора заслонки приточного вентилятора
            }
            else                                                                            // Отмена выбора резерва приточного
            {
                labelResPrFan.Hide(); powPrResFanBox.Hide(); labelResPrFan_2.Hide();
                prDampFanCheck.Checked = false;                                             // Отмена выбора заслонки приточного вентилятора
                prDampFanCheck.Enabled = false;                                             // Блокировка выбора заслонки приточного вентилятора
            }
            CheckResPrFan_cmdCheckedChanged(this, e);                                       // Командное слово
            if (ignoreEvents) return;
            CheckResPrFan_signalsDOCheckedChanged(this, e);                                 // Сигналы DO ПЛК
            CheckResPrFan_signalsAOCheckedChanged(this, e);                                 // Сигналы AO ПЛК
            CheckResPrFan_signalsDICheckedChanged(this, e);                                 // Сигналы DI ПЛК
        }

        ///<summary>Выбрали резерв вытяжного вентилятора</summary>
        private void CheckResOutFan_CheckedChanged(object sender, EventArgs e)
        {
            if (checkResOutFan.Checked)                                                     // Выбрали резерв вытяжного
            {
                labelResOutFan.Show(); powOutResFanBox.Show(); labelResOutFan_2.Show();
                outDampFanCheck.Enabled = true;                                             // Разблокировка выбора заслонки вытяжного вентилятора
            }
            else                                                                            // Отмена выбора резерва вытяжного
            {
                labelResOutFan.Hide(); powOutResFanBox.Hide(); labelResOutFan_2.Hide();
                outDampFanCheck.Checked = false;                                            // Отмена выбора заслонки вытяжного вентилятора
                outDampFanCheck.Enabled = false;                                            // Блокировка выбора заслонки вытяжного вентилятора
            }
            CheckResOutFan_cmdCheckedChanged(this, e);                                      // Командное слово
            if (ignoreEvents) return;
            CheckResOutFan_signalsCheckedChanged(this, e);                                  // Сигналы DO ПЛК
            CheckResOutFan_signalsAOCheckedChanged(this, e);                                // Сигналы AO ПЛК
            CheckResOutFan_signalsDICheckedChanged(this, e);                                // Сигналы DI ПЛК
        }

        ///<summary>Изменили тип догревателя</summary>
        private void HeatAddTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (heatAddTypeCombo.SelectedIndex == heatAddTypeComboIndex) return;    // Не поменялся выбор типа догревателя 

            if (heatAddTypeCombo.SelectedIndex == 1)                                // Электрокалорифер
            {
                heatAddTypeComboIndex = 1;
                watAddHeatPanel.Hide(); elAddHeatPanel.Show();
                heatAddPicture.Image = Properties.Resources.electroHeater;
                elAddHeatPanel.Location = MENU_POSITION;
            }
            else                                                                    // Водный калорифер
            {
                heatAddTypeComboIndex = 0;
                elAddHeatPanel.Hide(); watAddHeatPanel.Show();
                heatAddPicture.Image = Properties.Resources.waterHeater;
                watAddHeatPanel.Location = MENU_POSITION;
            }
            HeatAddTypeCombo_cmdSelectedIndexChanged(this, e);                      // Командное слово
            if (ignoreEvents) return;
            HeatAddTypeCombo_signalsDOSelectedIndexChanged(this, e);                // Сигналы DO ПЛК
            HeatAddTypeCombo_signalsAOSelectedIndexChanged(this, e);                // Сигналы AO ПЛК
            HeatAddTypeCombo_signalsDISelectedIndexChanged(this, e);                // Сигналы DI ПЛК
            HeatAddTypeCombo_signalsAISelectedIndexChanged(this, e);                // Сигналы AI ПЛК
        }

        ///<summary>Выбрали резервный насос дополнительного (второго) калорифера</summary>
        private void ReservPumpAddHeater_CheckedChanged(object sender, EventArgs e)
        {
            if (reservPumpAddHeater.Checked)                        // Выбрали резервный насос
            {
                confAddHeatResPumpCheck.Enabled = true;             // Разблокировка подтверждения работы
                pumpCurResAddProtect.Enabled = true;                // Разблокировка защиты по току
            }
            else                                                    // Отмена выбора насоса
            {
                confAddHeatResPumpCheck.Checked = false;            // Отмена выбора подтверждения работы
                pumpCurResAddProtect.Checked = false;               // Отмена выбора защиты по току
                confAddHeatResPumpCheck.Enabled = false;            // Блокировка подтверждения работы
                pumpCurResAddProtect.Enabled = false;               // Блокировка защиты по току
            }
            ReservPumpAddHeater_cmdCheckedChanged(this, e);         // Командное слово
            if (ignoreEvents) return;
            ReservPumpAddHeater_signalsDOCheckedChanged(this, e);   // Сигналы DO ПЛК
        }

        // Функция проверки доступности датчиков влажности
        private void CheckHumidSensors()
        {
            // Выбран доп.нагрев + фреон + осушение ИЛИ увлажнитель
            bool
                is_addHeat = addHeatCheck.Checked,                                              // Дополнительный нагрев
                is_FreonCooler = coolerCheck.Checked && coolTypeCombo.SelectedIndex == 0,       // Фреоновый охладитель
                is_dehumidMode = dehumModeCheck.Checked,                                        // Режим осушения
                is_humid = humidCheck.Checked;                                                  // Увлажнитель

            if ((is_addHeat && is_FreonCooler && is_dehumidMode) || is_humid)                   // Датчики влажности активны
            {
                roomHumSensCheck.Checked = true; chanHumSensCheck.Checked = true; 
                chanHumSensCheck.Enabled = true;
            }
            else                                                                                // Датчики влажности недоступны
            {
                roomHumSensCheck.Checked = false; chanHumSensCheck.Checked = false;
                chanHumSensCheck.Enabled = false;
            }
        }

        ///<summary>Выбрали ПЧ приточного вентилятора</summary>
        private void PrFanFC_check_CheckedChanged(object sender, EventArgs e)
        {
            if (prFanFC_check.Checked)                              // Выбрали ПЧ приточного вентилятора
            {
                prFanControlCombo.Enabled = true;                   // Разблокировка типа управления ПЧ

                if (prFanControlCombo.SelectedIndex == 0)           // Внешние контакты 
                {
                    prFanAlarmCheck.Enabled = true;                 // Разблокировка сигнала аварии ПЧ
                    prFanSpeedCheck.Enabled = true;                 // Разблокировка выбора скорости 0-10 В
                    // Выбор сигнала аварии ПЧ
                    if (!prFanAlarmCheck.Checked) prFanAlarmCheck.Checked = true;
                }
                else if (prFanControlCombo.SelectedIndex == 1)      // Modbus
                {
                    prFanFcTypeCombo.Enabled = true;                // Разблокировка выбора типа ПЧ
                }
            }  
            else                                                    // Отмена выбора ПЧ приточного вентилятора
            {
                prFanControlCombo.Enabled = false;                  // Блокировка типа управления ПЧ
                prFanSpeedCheck.Enabled = false;                    // Блокировка выбора скорости 0-10 В
                prFanAlarmCheck.Enabled = false;                    // Блокировка выбора сигнала аварии ПЧ
                prFanFcTypeCombo.Enabled = false;                   // Блокировка выбора типа ПЧ
                // Отмена сигнала скорости 0-10 В
                if (prFanSpeedCheck.Checked) prFanSpeedCheck.Checked = false;
                // Отмена выбора сигнала аварии ПЧ
                if (prFanAlarmCheck.Checked) prFanAlarmCheck.Checked = false;
            }
            PrFanFC_check_cmdCheckedChanged(this, e);              // Командное слово
        }

        ///<summary>Выбрали воздушную заслонку приточного вентилятора</summary>
        private void PrDampFanCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (prDampFanCheck.Checked)                            // Выбрана заслонка приточного вентилятора
            {
                prDampConfirmFanCheck.Enabled = true;              // Разблокировка подтверждения открытия заслонки 
            }
            else                                                   // Отмена выбора заслонки приточного вентилятора 
            {
                prDampConfirmFanCheck.Checked = false;             // Отмена выбора подтверждения открытия заслонки 
                prDampConfirmFanCheck.Enabled = false;             // Блокировка выбора подтверждения открытия заслонки
            }
            PrDampFanCheck_cmdCheckedChanged(this, e);             // Командное слово
            if (ignoreEvents) return;
            PrDampFanCheck_signalsDOCheckedChanged(this, e);       // Сигналы DO ПЛК
        }

        ///<summary>Выбрали подтверждение открытия воздушной заслонки приточного вентилятора</summary>
        private void PrDampConfirmFanCheck_CheckedChanged(object sender, EventArgs e)
        {
            PrDampConfirmFanCheck_cmdCheckedChanged(this, e);         // Командное слово
            if (ignoreEvents) return;
            PrDampConfirmFanCheck_signalsDICheckedChanged(this, e);   // Сигналы DI ПЛК
        }

        ///<summary>Выбрали ПЧ вытяжного вентилятора</summary>
        private void OutFanFC_check_CheckedChanged(object sender, EventArgs e)
        {
            if (outFanFC_check.Checked)                         // Выбрали ПЧ вытяжного вентилятора
            {
                outFanControlCombo.Enabled = true;              // Разблокировка типа управления ПЧ
                if (outFanControlCombo.SelectedIndex == 0)      // Внешние контакты
                {
                    outFanAlarmCheck.Enabled = true;            // Разблокировка сигнала аварии ПЧ
                    outFanSpeedCheck.Enabled = true;            // Разблокировка выбора скорости 0-10 В
                    // Выбор сигнала аварии ПЧ
                    if (!outFanAlarmCheck.Checked) outFanAlarmCheck.Checked = true;
                }
                else if (outFanControlCombo.SelectedIndex == 1) // Modbus
                {
                    outFanFcTypeCombo.Enabled = true;           // Разблокировка выбора типа ПЧ
                }
            } 
            else                                                // Отмена выбора ПЧ вытяжного вентилятора
            {
                outFanControlCombo.Enabled = false;             // Блокировка типа управления ПЧ
                outFanSpeedCheck.Enabled = false;               // Блокировка выбора скорости 0-10 В
                outFanAlarmCheck.Enabled = false;               // Блокировка сигнала аварии ПЧ
                outFanFcTypeCombo.Enabled = false;              // Блокировка выбора типа ПЧ
                // Отмена сигнала скорости 0-10 В
                if (outFanSpeedCheck.Checked) outFanSpeedCheck.Checked = false;
                // Отмена сигнала аварии ПЧ
                if (outFanAlarmCheck.Checked) outFanAlarmCheck.Checked = false;
            }
            OutFanFC_check_cmdCheckedChanged(this, e);          // Командное слово
        }

        ///<summary>Выбрали воздушную заслонку вытяжного вентилятора</summary>
        private void OutDampFanCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (outDampFanCheck.Checked)                           // Выбрана заслонка вытяжного вентилятора 
            {
                outDampConfirmFanCheck.Enabled = true;             // Разблокировка подтверждения открытия заслонки
            }
            else                                                   // Отмена выбора заслонки вытяжного вентилятора 
            {
                outDampConfirmFanCheck.Checked = false;            // Отмена выбора подтверждения открытия заслонки 
                outDampConfirmFanCheck.Enabled = false;            // Блокировка выбора подтверждения открытия заслонки
            }
            OutDampFanCheck_cmdCheckedChanged(this, e);            // Командное слово
            if (ignoreEvents) return;
            OutDampFanCheck_signalsDOCheckedChanged(this, e);      // Сигналы DO ПЛК
        }

        ///<summary>Выбрали подтверждение открытия воздушной заслонки приточного вентилятора</summary>
        private void OutDampConfirmFanCheck_CheckedChanged(object sender, EventArgs e)
        {
            OutDampConfirmFanCheck_cmdCheckedChanged(this, e);              // Командное слово
            if (ignoreEvents) return;
            OutDampConfirmFanCheck_signalsDICheckedChanged(this, e);        // Сигналы DI ПЛК
        }


        ///<summary>Выбрали режим осушения</summary>
        private void DehumModeCheck_CheckedChanged(object sender, EventArgs e)
        {
            CheckHumidSensors();                            // Проверка датчиков влажности
            DehumModeCheck_cmdCheckedChanged(this, e);      // Командное слово
        }

        ///<summary>Выбрали основной насос дополнительного нагревателя</summary>
        private void PumpAddHeatCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (pumpAddHeatCheck.Checked)                                               // Выбрали циркуляционный насос
            {
                powPumpAddCombo.Enabled = true;
                confAddHeatPumpCheck.Enabled = true;                                    // Подтверждение работы
                pumpCurAddProtect.Enabled = true;                                       // Защита по току
            }
            else                                                                        // Отмена выбора насоса
            {
                powPumpAddCombo.Enabled = false;
                // Подтверждение работы насоса
                confAddHeatPumpCheck.Checked = false;
                confAddHeatPumpCheck.Enabled = false;
                // Защита насоса по току
                pumpCurAddProtect.Checked = false;
                pumpCurAddProtect.Enabled = false;
            }
            PumpAddHeatCheck_cmdCheckedChanged(this, e); // Командное слово
        }

        ///<summary>Выбрали вытяжную воздушную заслонку</summary>
        private void OutDampCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (outDampCheck.Checked) // Выбрана вытяжная заслонка
            {
                var outDampCheck = new List<CheckBox>()
                {
                    confOutDampCheck, heatOutDampCheck
                };

                foreach (var el in outDampCheck) el.Enabled = true;

                outDampPowCombo.Enabled = true;
                
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
            OutDampCheck_cmdCheckedChanged(this, e);        // Командное слово
            if (ignoreEvents) return;
            OutDampCheck_signalsDOCheckedChanged(this, e);  // Сигналы DO ПЛК
            OutDampCheck_signalsDICheckedChanged(this, e);  // Сигналы DI ПЛК
        }

        ///<summary>Метод для открытия панели загрузки через CAN-порт</summary>
        private void LoadCanPanel_Open(object sender, EventArgs e)
        {
            mainPage.Hide();                                    // Скрытие панели опций элементов
            signalsPanel.Hide();                                // Скрытие панели распределения сигналов
            helpPanel.Hide();                                   // Скрытие панели отображения помощи
            comboPlkType.Hide();                                // Скрытие выбора типа контроллера
            label_comboSysType.Text = "ЗАГРУЗКА ПРОГРАММЫ";
            comboSysType.Hide(); panelElements.Hide();
            loadCanPanel.Location = PANEL_POSITION;
            loadCanPanel.Height = 550;
            loadCanPanel.Show();
            InitializeCAN();                                    // Инициализация для загрузки по CAN порту
            formSignalsButton.Hide();                           // Скрытие кнопки "Сформировать"
            pic_signalsReady.Hide();                            // Скртие изображения сформированной карты сигналов
        }

        ///<summary>Нажали на вкладку "Настройка", панель загрузки через Modbus</summary>
        private void ToolStripMenuItem_load_Click(object sender, EventArgs e)
        {
            mainPage.Hide();                                    // Скрытие панели опций элементов
            signalsPanel.Hide();                                // Скрытие панели распределения сигналов
            helpPanel.Hide();                                   // Скрытие панели отображения помощи
            label_comboSysType.Text = "ЗАГРУЗКА ПРОГРАММЫ";
            comboSysType.Hide(); panelElements.Hide();
            loadModbusPanel.Location = PANEL_POSITION;
            loadModbusPanel.Height = 468; // 390
            loadModbusPanel.Show();
            formSignalsButton.Hide(); // Скрытие кнопки "Сформировать"
            //ToolStripMenuItem_help.Enabled = false;
        }

        ///<summary>Нажали на вкладку "Параметры"</summary>
        private void ToolStripMenuItem_parameter_Click(object sender, EventArgs e)
        {
            mainPage.Hide();                                    // Скрытие панели опций элементов
            signalsPanel.Hide();                                // Скрытие панели распределения сигналов
            helpPanel.Hide();                                   // Скрытие панели отображения помощи
            loadModbusPanel.Hide();                             // Скрытие панели настроек
            label_comboSysType.Text = "ПАРАМЕТРЫ ПРОГРАММЫ";
            comboSysType.Hide(); panelElements.Hide();
            formSignalsButton.Hide();                           // Скрытие кнопки "Сформировать"
        }

        ///<summary>Нажали вкладку "Помощь" в главном меню</summary>
        private void ToolStripMenuItem_help_Click(object sender, EventArgs e)
        {
            mainPage.Hide();                                                                // Скрытие панели опций элементов
            signalsPanel.Hide();                                                            // Скрытие панели распределения сигналов
            label_comboSysType.Text = "ПОМОЩЬ И РУВОДСТВО";
            comboSysType.Hide(); panelElements.Hide();
            helpPanel.Location = PANEL_POSITION;
            helpPanel.Height = 485;
            helpPanel.Width = Width - 50;                                                   // Ширина по границе окна
            helpPanel.Show();
            formSignalsButton.Hide();                                                       // Скрытие кнопки "Сформировать"
            comboPlkType.Hide();                                                            // Скрытие выбора типа ПЛК
            panelBlocks.Hide();                                                             // Скрытие панели выбора блоков расширения
            pic_signalsReady.Hide();                                                        // Скрытие изображения статуса распределения сигналов
            //ToolStripMenuItem_options.Enabled = false; // Блокировка "Настройки"
            PDF_manual.Width = helpPanel.Width;                                             // Ширина по границе панели
            PDF_manual.src = Directory.GetCurrentDirectory() + @"\ManualModeron.pdf";       // В папке приложения
            PDF_ReSize(Size.Width, Size.Height);                                            // Область для отображения PDF
            //ToolStripMenuItem_help.Enabled = false;                                       // Блокировка повторного выбора "Помощь"
        }

        // Нажали кнопку "Назад" в панели настроек
        private void BackOptionsButton_Click(object sender, EventArgs e)
        {
            if (!fromSignalsMove)                                                       // Переход был не из панели выбора сигналов
            {
                loadModbusPanel.Hide();                                                 // Скрытие панели настроек
                mainPage.Show();                                                        // Отображение панели опции элементов 
                label_comboSysType.Text = "ТИП СИСТЕМЫ";
                comboSysType.Show();
                panelElements.Show();
                formSignalsButton.Show();                                               // Отображение кнопки "Сформировать"
            }
            else
            {
                FormSignalsButton_Click(this, e);                                       // Переход на панель выбора сигналов
            }
        }

        ///<summary>Нажали кнопку "Назад" в панели помощи</summary>
        private void BackHelpButton_Click(object sender, EventArgs e)
        {
            helpPanel.Hide();                                                           // Скрытие панели помощи
            loadModbusPanel.Hide();                                                     // Скрытие панели настроек
            mainPage.Show(); panelElements.Show();
            label_comboSysType.Text = "ТИП СИСТЕМЫ";
            comboSysType.Show();
            formSignalsButton.Show();                                                   // Отображение кнопки "Сформировать"
            pic_signalsReady.Show();                                                    // Отображение статуса распределения сигналов
        }

        ///<summary>Нажали кнопку "Назад" в панели параметров</summary>
        private void BackParameterButton_Click(object sender, EventArgs e)
        {
            mainPage.Show(); panelElements.Show();
            label_comboSysType.Text = "ТИП СИСТЕМЫ";
            comboSysType.Show();
            formSignalsButton.Show();                                                   // Отображение кнопки "Сформировать"
        }

        // Опция для включения всплывающих подсказок
        private void ShowHintCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (showHintCheck.Checked)                                                  // Всплывающие подсказки включены
                hintEnabled = true;
            else                                                                        // Всплывающие подсказки отключены
                hintEnabled = false;
            Form1_Load(this, e);                                                        // Обработка всплывающих подсказок
        }

        ///<summary>Нажали кнопку "Загрузить в ПЛК" в панели сигналов</summary>
        private void LoadPLC_SignalsButton_Click(object sender, EventArgs e)
        {
            //ToolStripMenuItem_load_Click(this, e);                                    // Открытие панели настроек
            LoadCanPanel_Open(this, e);                                                 // Открытие панели загрузки в контроллер, CAN порт
            fromSignalsMove = true;                                                     // Переход из панели выбора сигналов
            FormNetButton_Click(this, e);                                               // Формирование списка сигналов и командных слов для записи

            dataMatchPLC_label.Text = "(неизвестно)";
            dataMatchPLC_label.ForeColor = Color.Black;
        }

        ///<summary>Нажали на ссылку сайта Moderon</summary>
        private void LinkModeronWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkModeronWeb.LinkVisited = true;
            System.Diagnostics.Process.Start("http://moderon-electric.ru/");
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
            FormInfo formInfo = new FormInfo();
            formInfo.Show(this);
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

        ///<summary>Кнопка "Назад" из панели загрузки по CAN порту</summary>
        private void BackCanPanelButton_Click(object sender, EventArgs e)
        {
            var p1 = new Point(15, 90);                     // Позиция для панели таблицы сигналов
            var p2 = new Point(200, 46);                    // Позиция для comboBox выбора типа контроллера

            loadCanPanel.Hide();                            // Скрытие панели загрузки
            label_comboSysType.Text = "ТАБЛИЦА СИГНАЛОВ";
            // Положение и размер таблицы сигналов
            signalsPanel.Location = p1;
            signalsPanel.Show();
            signalsPanel.Height = 845;
            // Отображение выбора типа контроллера
            comboPlkType.Location = p2;
            comboPlkType.Show();
            formSignalsButton.Hide();
            pic_signalsReady.Show();                        // Отображение изображения сфомированной карты сигналов
            SignalsTableReSize(Size.Width, Size.Height);    // Таблица сигналов, пересчёт размеров
        }

        ///<summary>Нажали "Сброс" в панели таблицы сигналов</summary>
        private void ResetButtonSignals_Click(object sender, EventArgs e)     
        {
            ResetButton_Click(sender, e);
        }

        ///<summary>Обработка клика при переходе по вкладке "Командные слова"</summary>
        private void TabControlSignals_Selected(object sender, TabControlEventArgs e)
        {
            if (CheckSignalsReady()) FormNetButton_Click(this, e);
            else cmdWordsTextBox.Text = "";
        }

        ///<summary>Изменили адрес устройства</summary>
        private void CanAddressBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
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
            var filePath = Directory.GetCurrentDirectory();                                             // Для бинарного файла
            SaveFileDialog dlg = new SaveFileDialog();                                                  // Окно для сохранения файла
            dlg.FileName = "ONI_HVAC_PLC_v1.4.5";                                                       // Имя файла по умолчанию
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
