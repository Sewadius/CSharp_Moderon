using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Deployment.Application;

namespace Moderon
{
    public partial class Form1 : Form
    {
        readonly static public string 
            NOT_SELECTED = "Не выбрано",                                // Статус для состояния входов/выходов
            VERSION = "v.1.1.9.7";                                      // Текущая версия программы

        private const int
            WIDTH_MAIN = 955,                                           // Ширина основной формы
            HEIGHT_MAIN = 750,                                          // Высота основной формы
            HEIGHT = 280,                                               // Высота для панелей настройки элементов
            DELTA = 31,                                                 // Расстояние между comboBox в таблице сигналов
            HEIGHT_RECUP = 221,                                         // Высота изображения для обычного рекуператора
            HEIGHT_GLICK = 398,                                         // Высота изображения для гликолевого рекуператора
            DELTA_Y = 80,                                               // Сдвиг для элементов "Нет/ПЧ/ЕС двигатель"
            INIT_FAN_LOC = 120;                                         // Изначальное положение для панели П/В вентилятора

        private Point
            MENU_POSITION = new(3, 36),                                 // Позиция для меню элементов
            PANEL_POSITION = new(15, 90),                               // Позиция для остальных панелей
            EXTRA_FAN_POSITION = new(9, 119),                           // Позиция для П/В дополнительной панели
            RES_FAN_POSITION = new(9, 279);                             // Позиция для П/В резервного вентилятора панели

        readonly private bool showCode = true;                          // Код сигнала отображается по умолчанию в таблице сигналов

        private bool
            initialConfigure = true,                                    // Признак начальной конфигурации (при загрузке и после сброса)
            optimizeOnly = false,                                       // Признак 8 датчиков температуры (для отключения разблокировки типа ПЛК)
            isAutoSelect = true;                                        // Признак автоматического подбора блоков расширения

        // Ранее сохраненные значения индексов для элементов
        private int
            plkChangeIndexLast = 1,                                     // Значение для выбранного типа контроллера (по умолчанию Optimized для сброса)
            heatTypeComboIndex = 0,                                     // Значение для типа основного нагревателя
            coolTypeComboIndex = 0,                                     // Значение для типа охладителя
            heatAddTypeComboIndex = 0,                                  // Значение для типа дополнительного нагревателя
            humidTypeComboIndex = 0,                                    // Значение для типа увлажнителя
            recupTypeComboIndex = 0,                                    // Значение для типа рекуператора
            prFanFC_EC_Index = 0,                                       // Значение для выбранного типа (нет/ПЧ/ЕС) приточного вентилятора
            outFanFC_EC_Index = 0;                                      // Значение для выбранного типа (нет/ПЧ/ЕС) вытяжного вентилятора

        private int
            panelOutFan_height;                                         // Значение для высоты панели вытяжного вентилятора
        
        private bool 
            hintEnabled = true,                                         // Отображение подсказок выбрано по умолчанию
            fromSignalsMove = false;                                    // Переход из панели выбора сигналов

        // Класс для всплывающих подсказок (основные элементы)
        public ToolTip toolTip = new()
        {
            AutoPopDelay = 1500, InitialDelay = 500, ReshowDelay = 500, ShowAlways = true
        };

        public Form1()
        {
            InitializeComponent();          // Загрузка конструктора формы
            BlockTabControlInitial();       // Скрытие вкладок элементов
            SelectComboBoxesInitial();      // Изначальный выбор для comboBox элементов
            SelectManBlocksCombos();        // Изначальный выбор для comboBox ручного выбора блоков расширения
            SizePanels();                   // Изменение размера панелей элементов
            ClearIO_codes();                // Очистка наименования кодов для входов/выходов
            InitialSet_ComboTextIndex();    // Начальная установка для входов и выходов
          
            // Подготовка для блоков расширения
            DoCombosBlocks_Reset();         // Скрытие и блокировка comboBox DO блоков расширение, скрытие Label подписей
            UiCombosBlocks_Reset();         // Скрытие и блокировка comboBox UI блоков расширение, скрытие Label подписей

            // Размер для основной формы
            Size = new Size(WIDTH_MAIN, HEIGHT_MAIN);

            // Изначальное положение для дополнительной панели приточного/вытяжного вентиляторов
            extraPrFanPanel.Location = new Point(extraPrFanPanel.Location.X, INIT_FAN_LOC);
            extraOutFanPanel.Location = new Point(extraOutFanPanel.Location.X, INIT_FAN_LOC);

            // Изначальное положение для панелей резервов вентиляторов
            resFanPrPanel.Location = new Point(resFanPrPanel.Location.X, INIT_FAN_LOC + extraPrFanPanel.Height);
            resFanOutPanel.Location = new Point(resFanOutPanel.Location.X, INIT_FAN_LOC + extraOutFanPanel.Height);

            // Размер панелей вентиляторов без учёта высоты панелей ПЧ
            prFanPanel.Size = new Size(prFanPanel.Width, prFanPanel.Height - FC_fanPrPanel.Height);
            outFanPanel.Size = new Size(outFanPanel.Width, outFanPanel.Height - FC_fanOutPanel.Height);

            // Положение панели вытяжного вентилятора с учётом высоты ПЧ приточного
            outFanPanel.Location = new Point(outFanPanel.Location.X, 
                outFanPanel.Location.Y - FC_fanPrPanel.Height);

            // Положение кнопки распределения сигналов по подписи сформированной карты
            sig_distributionBtn.Location = 
                new Point(signalsReadyLabel.Location.X + signalsReadyLabel.Width, sig_distributionBtn.Location.Y);

            //LoadHints();                    // Загрузка всплывающих подсказок
        }

        ///<summary>Начальная установка для входов и выходов</summary>
        private void InitialSet_ComboTextIndex()
        {
            Set_UIComboTextIndex();         // Входные сигналы, UI
            Set_AOComboTextIndex();         // Аналоговые выходы, AO
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

            // Положение для панели элементов, панели выбора ПЛК и изображения подбора оборудования
            panelElements.Location = new Point(Size.Width - deltaW_panel, height_panel1);
            Plk_copyPanel.Location = new Point(Size.Width - deltaW_panel, height_panel1 +
                panelElements.Height + BETWEEN_PANELS);

            // Положение для выбора опции автоподбора блоков
            autoSelectBlocks_check.Location = new Point(Size.Width - deltaW_panel, height_panel1 +
                panelElements.Height + Plk_copyPanel.Height + BETWEEN_PANELS * 2);

            // Положение для панели ручного выбора блоков расширения
            panManBlocks.Location = new Point(Size.Width - deltaW_panel, height_panel1 +
                panelElements.Height + Plk_copyPanel.Height + BETWEEN_PANELS * 4);

            pic_signalsReady.Location = new Point(panelElements.Location.X, panelElements.Location.Y - BETWEEN_PANELS * 4);
            pictureBoxLogo.Location = new Point(panelElements.Location.X + logo_X_delta, pictureBoxLogo.Location.Y);

            // Положение для панели блоков расширения
            panelBlocks.Location = new Point(Size.Width - deltaW_panel, height_panel1 +
                panelElements.Height + Plk_copyPanel.Height + BETWEEN_PANELS + 5);
            

            // Положение для блока защиты рекуператора
            defRecupSensPanel.Location = new Point(3, 365);

            // Положение для label версии программы
            label_progVersion.Location = new Point(Size.Width - 90, Size.Height - 60);

            PicturesMove(Size.Width);                       // Перемещение изображений
            SignalsTableReSize(Size.Width, Size.Height);    // Таблица сигналов
        }

        /// <summary>Переменещение изображений элементов при изменении размера основной формы</summary>
        private void PicturesMove(int width)
        {
            const int
                fan_height = 3, fan1_delta = 499, fan2_delta = 499, filter_delta = 435,
                sensors_delta = 422, damp_delta = 395, heat_delta = 427, humid_delta = 420,
                recirc_delta = 415, recup_delta = 483, secHeat_delta = 433;

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
            recupPicture.Location = new Point(width - recup_delta, fan_height);

            /*recupPicture.Location = recupTypeCombo.SelectedIndex == 0 ? 
                new Point(width - recup_delta, fan_height) : new Point(width - recup_2_delta, fan_height); */
        }

        /// <summary>Положение дополнительной/резервной панели вентиляторов</summary>
        private void PositionPanelsFans()
        {
            // Положение для панелей приточного вентилятора
            extraPrFanPanel.Location = EXTRA_FAN_POSITION;
            resFanPrPanel.Location = RES_FAN_POSITION;

            // Положение для панелей вытяжного вентилятора
            extraOutFanPanel.Location = EXTRA_FAN_POSITION;
            resFanOutPanel.Location = RES_FAN_POSITION;
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

        ///<summary>Изменение размера области для таблицы сигналов</summary>
        private void SignalsTableReSize(int width, int height)
        {
            signalsPanel.Size = new Size(width - 50, height - 150);
            tabControlSignals.Size = new Size(signalsPanel.Width - 20, signalsPanel.Height - 50);
        }

        ///<summary>Изначальный выбор для comboBox элементов</summary>
        private void SelectComboBoxesInitial()
        {
            var elements = new List<ComboBox>()
            {
                comboSysType, comboPlkType, comboPlkType_copy, filterPrCombo, filterOutCombo,
                prFanFC_ECcombo, prFanPowCombo, prFanControlCombo, prFanFcTypeCombo, outFanFcTypeCombo,
                outFanPowCombo, outFanControlCombo, outFanFC_ECcombo, heatTypeCombo,
                elHeatStagesCombo, coolTypeCombo, frCoolStagesCombo,
                humidTypeCombo, recupTypeCombo,
                heatAddTypeCombo, elHeatAddStagesCombo,
                bypassPlastCombo, firstStHeatCombo, firstStAddHeatCombo, comboReadType, fireTypeCombo
            };

            foreach (var element in elements) element.SelectedIndex = 0;
        }

        ///<summary>Изначальный выбор для comboBox ручного выбора блоков расширения</summary>
        private void SelectManBlocksCombos()
        {
            var combos = new List<ComboBox>()
            {
                comboManBl_1, comboManBl_2, comboManBl_3
            };

            foreach (var el in combos) el.SelectedIndex = 0;
        }

        /// <summary>Установка размера для панелей настройки элементов</summary>
        private void SizePanels()
        {
            watHeatPanel.Height = watAddHeatPanel.Height = 400;         // Водяной основной и второй нагреватель
            glikRecupPanel.Height = 330;                                // Гликолевый рекуператор
            plastRecupPanel.Height = 95;                                // Пластинчатый рекуператор

            rotorRecupPanel.Height = 170;                               // Роторный рекуператор
            steamHumidPanel.Height = HEIGHT;                            // Паровой увлажнитель
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

        /// <summary>Назначение всплывающих подсказок</summary>
        private void LoadHints()
        {
            string
                ai_sig_temp = "Добавляет UI сигнал температурного датчика",
                ai_sig_hum = "Добавляет UI сигнал датчика влажности",
                di_sig = "Добавляет UI сигнал дискретного входа",
                do_sig = "Добавляет DO сигнал дискретного выхода",
                ao_sig = "Добавляет AO сигнал аналогового выхода",

                pic_sig_ready = "Состояние карты входов/выходов",
                pic_refresh = "Обновить список COM портов";

            // Датчики температуры и внешние сигналы
            toolTip.SetToolTip(prChanSensCheck, ai_sig_temp);
            toolTip.SetToolTip(roomTempSensCheck, ai_sig_temp);
            toolTip.SetToolTip(outdoorChanSensCheck, ai_sig_temp);
            toolTip.SetToolTip(outChanSensCheck, ai_sig_temp);
            toolTip.SetToolTip(fireCheck, di_sig);
            toolTip.SetToolTip(sigWorkCheck, do_sig);
            toolTip.SetToolTip(sigAlarmCheck, do_sig);
            toolTip.SetToolTip(sigFilAlarmCheck, do_sig);
            // Датчики влажности
            toolTip.SetToolTip(chanHumSensCheck, ai_sig_hum);
            toolTip.SetToolTip(roomHumSensCheck, ai_sig_hum);
            // Вентилятор приточный
            toolTip.SetToolTip(prFanPSCheck, di_sig);
            toolTip.SetToolTip(prFanThermoCheck, di_sig);
            toolTip.SetToolTip(curDefPrFanCheck, di_sig);
            toolTip.SetToolTip(prDampConfirmFanCheck, di_sig);
            toolTip.SetToolTip(prFanAlarmCheck, di_sig);
            toolTip.SetToolTip(prDampFanCheck, do_sig);
            toolTip.SetToolTip(prFanSpeedCheck, ao_sig);
            // Вентилятор вытяжной
            toolTip.SetToolTip(outFanPSCheck, di_sig);
            toolTip.SetToolTip(outFanThermoCheck, di_sig);
            toolTip.SetToolTip(curDefOutFanCheck, di_sig);
            toolTip.SetToolTip(outDampConfirmFanCheck, di_sig);
            toolTip.SetToolTip(outFanAlarmCheck, di_sig);
            toolTip.SetToolTip(outDampFanCheck, do_sig);
            toolTip.SetToolTip(outFanSpeedCheck, ao_sig);
            // Приточная и вытяжная воздушные заслонки
            toolTip.SetToolTip(confPrDampCheck, di_sig);
            toolTip.SetToolTip(confOutDampCheck, di_sig);
            toolTip.SetToolTip(outDampCheck, do_sig);
            toolTip.SetToolTip(heatPrDampCheck, do_sig);
            toolTip.SetToolTip(heatOutDampCheck, do_sig);
            // Основной водяной нагреватель
            toolTip.SetToolTip(TF_heaterCheck, di_sig);
            toolTip.SetToolTip(confHeatPumpCheck, di_sig);
            toolTip.SetToolTip(pumpCurProtect, di_sig);
            toolTip.SetToolTip(confHeatResPumpCheck, di_sig);
            toolTip.SetToolTip(pumpCurResProtect, di_sig);
            // Дополнительный водяной нагреватель
            toolTip.SetToolTip(TF_addHeaterCheck, di_sig);
            toolTip.SetToolTip(confAddHeatPumpCheck, di_sig);
            toolTip.SetToolTip(pumpCurAddProtect, di_sig);
            toolTip.SetToolTip(confAddHeatResPumpCheck, di_sig);
            toolTip.SetToolTip(pumpCurResAddProtect, di_sig);
            toolTip.SetToolTip(pumpAddHeatCheck, do_sig);
            // Фреоновый охладитель
            toolTip.SetToolTip(alarmFrCoolCheck, di_sig);
            toolTip.SetToolTip(thermoCoolerCheck, di_sig);
            toolTip.SetToolTip(analogFreonCheck, ao_sig);
            // Увлажнитель
            toolTip.SetToolTip(alarmHumidCheck, di_sig);
            // Рециркуляция
            toolTip.SetToolTip(recircAOSigCheck, ao_sig);
            toolTip.SetToolTip(recircPrDampAOCheck, ao_sig);
            // Защита рекуператора PS и датчик температуры
            toolTip.SetToolTip(recDefTempCheck, ai_sig_temp);
            toolTip.SetToolTip(recDefPsCheck, di_sig);
            // Гликолевый рекуператор
            toolTip.SetToolTip(pumpGlikConfCheck, di_sig);
            toolTip.SetToolTip(pumpGlikCurProtect, di_sig);
            toolTip.SetToolTip(confGlikResPumpCheck, di_sig);
            toolTip.SetToolTip(pumpGlikResCurProtect, di_sig);
            toolTip.SetToolTip(pumpGlicRecCheck, do_sig);
            toolTip.SetToolTip(reservPumpGlik, do_sig);
            // Роторный рекуператор
            toolTip.SetToolTip(outSigAlarmRotRecCheck, di_sig);
            toolTip.SetToolTip(startRotRecCheck, do_sig);
            // Изображение для карты входов/выходов
            toolTip.SetToolTip(pic_signalsReady, pic_sig_ready);
            // Изображение для обновления списка CAN портов
            toolTip.SetToolTip(refreshCanPorts, pic_refresh);

            toolTip.Active = hintEnabled;               // Признак активации отображения подсказок элементов
        }

        /// <summary>Действия при загрузке Form1</summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Установка текущей версии программы для label основной формы
            label_progVersion.Text = VERSION.Substring(0, VERSION.Length - 2);

            mainPage.Height = 465;                          // Изменение размера для tabControl выбора оборудования, 465px
            Form1_InitCmdWord(this, e);                     // Подготовка командных слов
            Form1_InitSignals(this, e);                     // Подготовка сигналов ПЛК
            
            signalsPanel.Width = 845;                       // Изменения для таблицы формирования сигналов, 845px
            LoadNetOnLoad();                                // Изменения для панели данных записи 

            Form1_SizeChanged(this, e);                     // Изменение размеров для формы
            ComboPlkType_SelectedIndexChanged(this, e);     // Блокировка изначально входов/выходов для контроллера "Mini"
            MouseWheelCheck_CheckedChanged(this, e);        // Обработка блокировки прокрутки колёсиком мыши элементов comboBox
            ClearPanelHeaders();                            // Начальная очистка заголовков для панелей блоков расширения

            LoadHints();                                    // Обработка для всплывающих подсказок
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
            List<bool> options = [
                filterCheck.Checked, dampCheck.Checked, heaterCheck.Checked,
                addHeatCheck.Checked, coolerCheck.Checked, humidCheck.Checked,
                recircCheck.Checked, recupCheck.Checked
            ];

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
                prChanSensCheck.Enabled = true;                         // Разблокировка канального датчика температуры
                recupPage.Parent = null;
                defRecupSensPanel.Hide();
                CheckOptions();
            }
            DefRecupSensPanel_Location(recupTypeCombo.SelectedIndex);   // Положение для панели защиты от заморозки
            RecupCheck_cmdCheckedChanged(this, e);                      // Командное слово
            if (ignoreEvents) return;
            RecupCheck_signalsAOCheckedChanged(this, e);                // Сигналы AO ПЛК
            RecupCheck_signalsDOCheckedChanged(this, e);                // Сигналы DO ПЛК
            RecupCheck_signalsDICheckedChanged(this, e);                // Сигналы DI ПЛК
            RecupCheck_signalsAICheckedChanged(this, e);                // Сигналы AI ПЛК
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
                    pumpGlikConfCheck.Checked = false;                          // Снятие выбора элементов
                    pumpGlikCurProtect.Checked = false;
                    pumpGlikConfCheck.Enabled = false;                          // Блокировка элементов
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

            // Возврат checkBox панели настроек в состояние по умолчанию
            tooltipsCheck.Checked = true;                   // Выбор подсказок по умолчанию
            mouseWheelCheck.Checked = true;                 // События колёсика мыши

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

            // Установка размера изображения для рекуператора по умолчанию
            recupPicture.Size = new Size(recupPicture.Width, HEIGHT_RECUP);

            outFanPanel.Hide();                             // Скрытие панели вытяжного вентилятора

            HideExpansionBlocksPanels();                    // Скрытие панелей для блоков расширения в таблице сигналов
            SelectComboBoxesInitial();                      // Возврат к изначальным значения выбора
            SelectManBlocksCombos();                        // Возврат к изначальным индексам ручного выбора блоков расширения
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
            ClearPanelHeaders();                            // Очистка заголовков для панелей блоков расширения
            InitialSet_ComboTextIndex();                    // Изначальная установка для подписей/индексов comboBox
            Form1_InitSignals(this, e);                     // Начальная расстановка сигналов

            PositionPanelsFans();                           // Возврат положения панелей для вентиляторов

            // Различные признаки-флаги для программы
            correctFile = false;                            // Установка начального признака корректного файла загрузки
            initialConfigure = true;                        // Установка признака начальной расстановки системы
            optimizeOnly = false;                           // Сброс признака блокировки выбора ПЛК Optimize                       

            autoSelectBlocks_check.Checked = true;          // Сброс признака автоматическог подбора блоков расширения
            autoSelectBlocks_check.Show();

            expansion_blocks.Clear();                       // Очистка списка задействованных блоков расширения
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
                prFanPSCheck, prFanThermoCheck, curDefPrFanCheck, checkResPrFan, 
                prFanAlarmCheck, prFanStStopCheck, prFanSpeedCheck,
                outFanPSCheck, outFanThermoCheck, curDefOutFanCheck, checkResOutFan,
                outFanAlarmCheck, outFanStStopCheck, outFanSpeedCheck
            };

            var fanOptionsUnenabled = new List<CheckBox>()
            {
                prFanAlarmCheck, prFanSpeedCheck, outFanAlarmCheck, outFanSpeedCheck
            };

            foreach (var el in fanPrOutOptions) el.Checked = false;
            foreach (var el in fanOptionsUnenabled) el.Enabled = false;

            prFanFC_ECcombo.SelectedIndex = 0;              // Сброс выбора ПЧ/ЕС двигателя
            outFanCheck.Checked = true;                     // Выбор наличия вытяжного вентилятора
        }

        /// <summary>Сброс настроек для воздушных заслонок</summary>
        private void ResetDampOptions()
        {
            var dampCheck = new List<CheckBox>()
            {
                confPrDampCheck, heatPrDampCheck, confOutDampCheck,
                heatOutDampCheck, outDampCheck
            };

            foreach (var el in dampCheck) el.Checked = false;
        }

        /// <summary>Сброс настроек для основного нагревателя</summary>
        private void ResetHeaterOptions()
        {
            TF_heaterCheck.Checked = false;             // Термостат
            confHeatPumpCheck.Checked = false;          // Подтверждение работы основного насоса
            pumpCurProtect.Checked = false;             // Защита по току основного насоса
            reservPumpHeater.Checked = false;           // Резервный насос калорифера
            overheatThermCheck.Checked = false;         // Термовыключатель перегрева ТЭНов
            fireThermCheck.Checked = false;             // Термовыключатель пожара ТЭНов

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
            
            pumpAddHeatCheck.Checked = true;        // Насос второго нагревателя
            pumpCurAddProtect.Checked = false;      // Защита по току основного насоса
            reservPumpAddHeater.Checked = false;    // Резервный насос калорифера
            overheatAddThermCheck.Checked = false;  // Термовыключатель перегрева
            fireAddThermCheck.Checked = false;      // Термовыключатель пожара

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
            analogHumCheck.Checked = true;
        }

        /// <summary>Сброс настроек для рециркуляции</summary>
        private void ResetRecircOptions()
        {
            recircPrDampAOCheck.Checked = false;
        }

        /// <summary>Сброс настроек для рекуператора</summary>
        private void ResetRecupOpitons()
        {
            // Защита рекуператора
            recDefTempCheck.Checked = false;
            recDefPsCheck.Checked = false;
            // Роторный рекуператор
            outSigAlarmRotRecCheck.Checked = false;
            startRotRecCheck.Checked = false;
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
            
            if (comboSysType.SelectedIndex == 1) comboSysType.Enabled = false;      // Блокировка выбора типа системы при выборе ПВ
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

        ///<summary>Положение панели защиты от заморозки рекуператора, зависит от типа</summary>
        private void DefRecupSensPanel_Location(int index)
        {
            if (index == 0)         // Роторный рекуператор
                defRecupSensPanel.Location = new Point(rotorRecupPanel.Location.X,
                    rotorRecupPanel.Location.Y + rotorRecupPanel.Height);
            else if (index == 1)    // Пластинчатый рекуператор
                defRecupSensPanel.Location = new Point(plastRecupPanel.Location.X,
                    plastRecupPanel.Location.Y + plastRecupPanel.Height);
            else if (index == 2)    // Гликолевый рекуператор
                defRecupSensPanel.Location = new Point(glikRecupPanel.Location.X,
                    glikRecupPanel.Location.Y + glikRecupPanel.Height);
        }

        ///<summary>Изменили тип рекуператора</summary>
        private void RecupTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (recupTypeCombo.SelectedIndex == recupTypeComboIndex) return;        // Не изменился тип рекуператора

            if (recupTypeCombo.SelectedIndex == 1)                                  // Пластинчатый
            {
                recupTypeComboIndex = 1;
                rotorRecupPanel.Hide(); glikRecupPanel.Hide(); plastRecupPanel.Show();
                if (bypassPlastCombo.SelectedIndex == 0)                            // Нет выбранного байпасса
                    recupPicture.Image = Properties.Resources.plastRecup;
                else                                                                // Есть выбранный байпасс
                    recupPicture.Image = Properties.Resources.plastRecupBypass;
                plastRecupPanel.Location = MENU_POSITION;
                recupPicture.Size = new Size(recupPicture.Width, HEIGHT_RECUP);     // Размер для изображения рекуператора
            }
            else if (recupTypeCombo.SelectedIndex == 0)                             // Роторный
            {
                recupTypeComboIndex = 0;
                plastRecupPanel.Hide(); glikRecupPanel.Hide(); rotorRecupPanel.Show();
                recupPicture.Image = Properties.Resources.rotorRecup;
                rotorRecupPanel.Location = MENU_POSITION;
                recupPicture.Size = new Size(recupPicture.Width, HEIGHT_RECUP);     // Размер для изображения рекуператора
            }
            else if (recupTypeCombo.SelectedIndex == 2)                             // Гликолевый
            {
                recupTypeComboIndex = 2;
                plastRecupPanel.Hide(); rotorRecupPanel.Hide(); glikRecupPanel.Show();
                recupPicture.Image = Properties.Resources.glikRecup;
                recupPicture.Size = new Size(recupPicture.Width, recupPicture.Height + 100);
                glikRecupPanel.Location = MENU_POSITION;
                recupPicture.Size = new Size(recupPicture.Width, HEIGHT_GLICK);     // Размер для изображения рекуператора
            }
            DefRecupSensPanel_Location(recupTypeComboIndex);                        // Положение для панели защиты от заморозки
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
                prDampFanCheck.Enabled = true;                                              // Разблокировка выбора заслонки приточного вентилятора
            }
            else                                                                            // Отмена выбора резерва приточного
            {
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
                outDampFanCheck.Enabled = true;                                             // Разблокировка выбора заслонки вытяжного вентилятора
            }
            else                                                                            // Отмена выбора резерва вытяжного
            {
                outDampFanCheck.Checked = false;                                            // Отмена выбора заслонки вытяжного вентилятора
                outDampFanCheck.Enabled = false;                                            // Блокировка выбора заслонки вытяжного вентилятора
            }
            CheckResOutFan_cmdCheckedChanged(this, e);                                      // Командное слово
            if (ignoreEvents) return;
            CheckResOutFan_signalsDOCheckedChanged(this, e);                                // Сигналы DO ПЛК
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

        ///<summary>Выбрали "Нет/ПЧ/ЕС" для приточного вентилятора</summary>
        private void PrFanFC_ECcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (prFanFC_ECcombo.SelectedIndex == prFanFC_EC_Index)      // Индекс не изменился
                return;

            // Выбран ПЧ или ЕС-двигатель
            if (prFanFC_ECcombo.SelectedIndex == 1 || prFanFC_ECcombo.SelectedIndex == 2)       
            {
                FC_fanPrPanel.Show();                                   // Отображение панели ПЧ приточного вентилятора
                prFanStStopCheck.Enabled = true;                        // Разблокировка выбора сигнала "Пуск/Стоп"
                
                if (prFanFC_ECcombo.SelectedIndex == 1)                 // Выбран ПЧ приточного вентилятора 
                {
                    // Отображение элементов управления
                    prFanControlCombo.Show(); prFanControlCombo_label.Show();       // Вид управления ПЧ
                    prFanFcTypeCombo.Show(); prFanFcTypeCombo_label.Show();         // Тип выбранного ПЧ

                    prFanControlCombo.Enabled = true;                   // Разблокировка типа управления ПЧ приточного вентилятора

                    if (prFanControlCombo.SelectedIndex == 0)           // Внешние контакты 
                    {
                        prFanAlarmCheck.Enabled = true;                 // Разблокировка сигнала аварии ПЧ
                        prFanSpeedCheck.Enabled = true;                 // Разблокировка выбора скорости 0-10 В
                                                                        
                        if (!prFanAlarmCheck.Checked) 
                            prFanAlarmCheck.Checked = true;             // Выбор сигнала аварии ПЧ
                    }
                    else if (prFanControlCombo.SelectedIndex == 1)      // Modbus
                    {
                        // Отмена выбора с опций
                        if (prFanAlarmCheck.Checked) prFanAlarmCheck.Checked = false;
                        if (prFanSpeedCheck.Checked) prFanSpeedCheck.Checked = false;

                        // Блокировка выбора опций
                        prFanAlarmCheck.Enabled = false;                // Блокировка сигнала аварии ПЧ
                        prFanSpeedCheck.Enabled = false;                // Блокировка выбора скорости 0-10 В

                        prFanFcTypeCombo.Enabled = true;                // Разблокировка выбора типа ПЧ
                    }

                    if (prFanFC_EC_Index == 2)                          // Был ранее выбран ЕС-двигатель
                    {
                        // Сдвиг панелей приточного вентилятора "вниз"
                        extraPrFanPanel.Location = new Point(extraPrFanPanel.Location.X,
                            extraPrFanPanel.Location.Y + DELTA_Y);
                        resFanPrPanel.Location = new Point(resFanPrPanel.Location.X,
                            resFanPrPanel.Location.Y + DELTA_Y);

                        // Сдвиг для панели вытяжного вентилятора "вниз"
                        outFanPanel.Location = new Point(outFanPanel.Location.X,
                            outFanPanel.Location.Y + DELTA_Y);
                    }
                }

                // Выбран EC-двигатель
                else if (prFanFC_ECcombo.SelectedIndex == 2)
                {
                    prFanAlarmCheck.Enabled = true;                     // Разблокировка сигнала аварии ПЧ
                    prFanSpeedCheck.Enabled = true;                     // Разблокировка выбора скорости 0-10 В

                    // Скрытие элементов управления
                    prFanControlCombo.Hide(); prFanControlCombo_label.Hide();   // Вид управления ПЧ
                    prFanFcTypeCombo.Hide(); prFanFcTypeCombo_label.Hide();     // Тип выбранного ПЧ
                }

                if (prFanFC_EC_Index == 1)                                      // Был ранее выбран ПЧ
                {
                    // Сдвиг панелей приточного вентилятора "вверх"
                    extraPrFanPanel.Location = new Point(extraPrFanPanel.Location.X,
                        extraPrFanPanel.Location.Y - DELTA_Y);
                    resFanPrPanel.Location = new Point(resFanPrPanel.Location.X,
                        resFanPrPanel.Location.Y - DELTA_Y);

                    // Сдвиг для панели вытяжного вентилятора "вверх"
                    outFanPanel.Location = new Point(outFanPanel.Location.X,
                        outFanPanel.Location.Y - DELTA_Y);
                }
                    
                if (prFanFC_EC_Index == 0)                              // Не было ранее выбранного ПЧ или ЕС
                {
                    // Изменение размера панели П вентилятора
                    prFanPanel.Size = new Size(prFanPanel.Width, prFanPanel.Height + FC_fanPrPanel.Height);

                    // Если выбран ПЧ
                    if (prFanFC_ECcombo.SelectedIndex == 1)
                    {
                        // Сдвиг панелей приточного вентилятора "вниз"
                        extraPrFanPanel.Location = new Point(extraPrFanPanel.Location.X,
                            extraPrFanPanel.Location.Y + FC_fanPrPanel.Height);
                        resFanPrPanel.Location = new Point(resFanPrPanel.Location.X,
                            resFanPrPanel.Location.Y + FC_fanPrPanel.Height);

                        // Сдвиг для панели вытяжного вентилятора "вниз"
                        outFanPanel.Location = new Point(outFanPanel.Location.X,
                            outFanPanel.Location.Y + FC_fanPrPanel.Height);
                    }

                    // Выбран ЕС-двигатель
                    else if (prFanFC_ECcombo.SelectedIndex == 2)
                    {
                        // Сдвиг панелей приточного вентилятора "вниз"
                        extraPrFanPanel.Location = new Point(extraPrFanPanel.Location.X,
                            extraPrFanPanel.Location.Y + DELTA_Y);
                        resFanPrPanel.Location = new Point(resFanPrPanel.Location.X,
                            resFanPrPanel.Location.Y + DELTA_Y);

                        // Сдвиг для панели вытяжного вентилятора "вниз"
                        outFanPanel.Location = new Point(outFanPanel.Location.X,
                            outFanPanel.Location.Y + DELTA_Y);
                    }
                }
            }
            else                                                    // Выбрали "Нет"
            {
                FC_fanPrPanel.Hide();                               // Скрытие панели ПЧ приточного вентилятора

                if (!prFanStStopCheck.Checked)                      // Выбор сигнала "Пуск/Стоп"
                    prFanStStopCheck.Checked = true;
                
                prFanStStopCheck.Enabled = false;                   // Блокировка выбора сигнала "Пуск/Стоп"

                // Изменение размера панели П вентилятора
                prFanPanel.Size = new Size(prFanPanel.Width, prFanPanel.Height - FC_fanPrPanel.Height);

                if (prFanFC_EC_Index == 1)                          // Был ранее выбран ПЧ
                {
                    // Сдвиг панелей приточного вентилятора "вверх"
                    extraPrFanPanel.Location = new Point(extraPrFanPanel.Location.X,
                        extraPrFanPanel.Location.Y - FC_fanPrPanel.Height);
                    resFanPrPanel.Location = new Point(resFanPrPanel.Location.X,
                        resFanPrPanel.Location.Y - FC_fanPrPanel.Height);

                    // Сдвиг панели вытяжного вентилятора "вверх"
                    outFanPanel.Location = new Point(outFanPanel.Location.X,
                        outFanPanel.Location.Y - FC_fanPrPanel.Height);
                }
                else if (prFanFC_EC_Index == 2)                     // Был ранее выбран ЕС-двигатель
                {
                    // Сдвиг панелей приточного вентилятора "вверх"
                    extraPrFanPanel.Location = new Point(extraPrFanPanel.Location.X,
                        extraPrFanPanel.Location.Y - DELTA_Y);
                    resFanPrPanel.Location = new Point(resFanPrPanel.Location.X,
                        resFanPrPanel.Location.Y - DELTA_Y);

                    // Сдвиг панели вытяжного вентилятора "вверх"
                    outFanPanel.Location = new Point(outFanPanel.Location.X,
                        outFanPanel.Location.Y - DELTA_Y);
                }

                prFanControlCombo.Enabled = false;                  // Блокировка типа управления ПЧ
                prFanSpeedCheck.Enabled = false;                    // Блокировка выбора скорости 0-10 В
                prFanAlarmCheck.Enabled = false;                    // Блокировка выбора сигнала аварии ПЧ
                prFanFcTypeCombo.Enabled = false;                   // Блокировка выбора типа ПЧ
                
                // Отмена сигнала скорости 0-10 В
                if (prFanSpeedCheck.Checked) prFanSpeedCheck.Checked = false;
                // Отмена выбора сигнала аварии ПЧ
                if (prFanAlarmCheck.Checked) prFanAlarmCheck.Checked = false;
            }

            // Выбрали ЕС, был выбран ПЧ или "Нет"
            if (prFanFC_ECcombo.SelectedIndex == 2 && (prFanFC_EC_Index == 0 || prFanFC_EC_Index == 1))  
            {
                // Перемещение элементов управления вверх
                prFanAlarmCheck.Location = new Point(prFanAlarmCheck.Location.X, prFanAlarmCheck.Location.Y - DELTA_Y);
                prFanSpeedCheck.Location = new Point(prFanSpeedCheck.Location.X, prFanSpeedCheck.Location.Y - DELTA_Y);
            }

            // Выбрали "Нет" или ПЧ, был выбран ЕС
            if ((prFanFC_ECcombo.SelectedIndex == 0 || prFanFC_ECcombo.SelectedIndex == 1) && prFanFC_EC_Index == 2)  
            {
                // Перемещение элементов управления вниз
                prFanAlarmCheck.Location = new Point(prFanAlarmCheck.Location.X, prFanAlarmCheck.Location.Y + DELTA_Y);
                prFanSpeedCheck.Location = new Point(prFanSpeedCheck.Location.X, prFanSpeedCheck.Location.Y + DELTA_Y);
            }

            prFanFC_EC_Index = prFanFC_ECcombo.SelectedIndex;        // Сохранение значения нового выбранного индекса
            PrFanFC_ECcombo_cmdCheckedChanged(this, e);              // Командное слово
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

        ///<summary>Выбрали "Нет/ПЧ/ЕС" для вытяжного вентилятора</summary>
        private void OutFanFC_ECcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (outFanFC_ECcombo.SelectedIndex == outFanFC_EC_Index)      // Индекс не изменился
                return;

            // Выбран ПЧ или ЕС-двигатель
            if (outFanFC_ECcombo.SelectedIndex == 1 || outFanFC_ECcombo.SelectedIndex == 2)
            {
                FC_fanOutPanel.Show();                                   // Отображение панели ПЧ вытяжного вентилятора
                outFanStStopCheck.Enabled = true;                        // Разблокировка выбора сигнала "Пуск/Стоп"

                if (outFanFC_ECcombo.SelectedIndex == 1)                 // Выбран ПЧ приточного вентилятора 
                {
                    // Отображение элементов управления
                    outFanControlCombo.Show(); outFanControlCombo_label.Show();     // Вид управления ПЧ
                    outFanFcTypeCombo.Show(); outFanFcTypeCombo_label.Show();       // Тип выбранного ПЧ

                    outFanControlCombo.Enabled = true;                   // Разблокировка типа управления ПЧ приточного вентилятора

                    if (outFanControlCombo.SelectedIndex == 0)           // Внешние контакты 
                    {
                        outFanAlarmCheck.Enabled = true;                 // Разблокировка сигнала аварии ПЧ
                        outFanSpeedCheck.Enabled = true;                 // Разблокировка выбора скорости 0-10 В

                        if (!outFanAlarmCheck.Checked)
                            outFanAlarmCheck.Checked = true;             // Выбор сигнала аварии ПЧ
                    }
                    else if (outFanControlCombo.SelectedIndex == 1)      // Modbus
                    {
                        // Снятие выбора с опций
                        if (outFanAlarmCheck.Checked) outFanAlarmCheck.Checked = false;
                        if (outFanSpeedCheck.Checked) outFanSpeedCheck.Checked = false;

                        // Блокировка выбора опций
                        outFanAlarmCheck.Enabled = false;               // Блокировка сигнала аварии ПЧ
                        outFanSpeedCheck.Enabled = false;               // Блокировка выбора скорости 0-10 В

                        outFanFcTypeCombo.Enabled = true;                // Разблокировка выбора типа ПЧ
                    }

                    if (outFanFC_EC_Index == 2)                         // Был ранее выбран ЕС-двигатель
                    {
                        // Сдвиг панелей вытяжного вентилятора "вниз" 
                        extraOutFanPanel.Location = new Point(extraOutFanPanel.Location.X,
                            extraOutFanPanel.Location.Y + DELTA_Y);
                        resFanOutPanel.Location = new Point(resFanOutPanel.Location.X,
                            resFanOutPanel.Location.Y + DELTA_Y);
                    }
                }

                // Выбран EC-двигатель
                else if (outFanFC_ECcombo.SelectedIndex == 2)
                {
                    outFanAlarmCheck.Enabled = true;                 // Разблокировка сигнала аварии ПЧ
                    outFanSpeedCheck.Enabled = true;                 // Разблокировка выбора скорости 0-10 В

                    // Скрытие элементов управления
                    outFanControlCombo.Hide(); outFanControlCombo_label.Hide();     // Вид управления ПЧ
                    outFanFcTypeCombo.Hide(); outFanFcTypeCombo_label.Hide();       // Тип выбранного ПЧ

                    if (outFanFC_EC_Index == 1)                     // Был ранее выбран ПЧ
                    {
                        // Сдвиг панелей вытяжного вентилятора "вверх" 
                        extraOutFanPanel.Location = new Point(extraOutFanPanel.Location.X,
                            extraOutFanPanel.Location.Y - DELTA_Y);
                        resFanOutPanel.Location = new Point(resFanOutPanel.Location.X,
                            resFanOutPanel.Location.Y - DELTA_Y);
                    }
                }

                if (outFanFC_EC_Index == 0)                              // Не было ранее выбранного ПЧ или ЕС
                {
                    // Изменение размера панели В вентилятора
                    outFanPanel.Size = new Size(outFanPanel.Width, outFanPanel.Height + FC_fanPrPanel.Height);

                    // Если выбран ПЧ
                    if (outFanFC_ECcombo.SelectedIndex == 1)
                    {
                        // Сдвиг вниз для дополнительной панели вытяжного вентилятора
                        extraOutFanPanel.Location = new Point(extraOutFanPanel.Location.X,
                            extraOutFanPanel.Location.Y + FC_fanOutPanel.Height);

                        // Сдвиг вниз для панели резерва вытяжного вентилятора
                        resFanOutPanel.Location = new Point(resFanOutPanel.Location.X,
                            resFanOutPanel.Location.Y + FC_fanOutPanel.Height);
                    }

                    // Выбран ЕС-двигатель
                    else if (outFanFC_ECcombo.SelectedIndex == 2)
                    {
                        // Сдвиг вниз для дополнительной панели вытяжного вентилятора
                        extraOutFanPanel.Location = new Point(extraOutFanPanel.Location.X,
                            extraOutFanPanel.Location.Y + DELTA_Y);

                        // Сдвиг вниз для панели резерва вытяжного вентилятора
                        resFanOutPanel.Location = new Point(resFanOutPanel.Location.X,
                            resFanOutPanel.Location.Y + DELTA_Y);
                    }
                }
            }
            else                                                    // Выбрали "Нет"
            {
                FC_fanOutPanel.Hide();                               // Скрытие панели ПЧ вытяжного вентилятора

                if (!outFanStStopCheck.Checked)                      // Выбор сигнала "Пуск/Стоп"
                    outFanStStopCheck.Checked = true;

                outFanStStopCheck.Enabled = false;                   // Блокировка выбора сигнала "Пуск/Стоп"

                // Изменение размера панели В вентилятора
                outFanPanel.Size = new Size(outFanPanel.Width, outFanPanel.Height - FC_fanPrPanel.Height);

                if (outFanFC_EC_Index == 1)                         // Был ранее выбран ПЧ
                {
                    // Сдвиг вверх для дополнительной панели вытяжного вентилятора
                    extraOutFanPanel.Location = new Point(extraOutFanPanel.Location.X,
                        extraOutFanPanel.Location.Y - FC_fanOutPanel.Height);

                    // Сдвиг вверх для панели резерва вытяжного вентилятора
                    resFanOutPanel.Location = new Point(resFanOutPanel.Location.X,
                        resFanOutPanel.Location.Y - FC_fanOutPanel.Height);
                }
                else if (outFanFC_EC_Index == 2)                    // Был ранее выбран ЕС-двигатель   
                {
                    // Сдвиг вверх для дополнительной панели вытяжного вентилятора
                    extraOutFanPanel.Location = new Point(extraOutFanPanel.Location.X,
                        extraOutFanPanel.Location.Y - DELTA_Y);

                    // Сдвиг вверх для панели резерва вытяжного вентилятора
                    resFanOutPanel.Location = new Point(resFanOutPanel.Location.X,
                        resFanOutPanel.Location.Y - DELTA_Y);
                }

                outFanControlCombo.Enabled = false;                  // Блокировка типа управления ПЧ
                outFanSpeedCheck.Enabled = false;                    // Блокировка выбора скорости 0-10 В
                outFanAlarmCheck.Enabled = false;                    // Блокировка выбора сигнала аварии ПЧ
                outFanFcTypeCombo.Enabled = false;                   // Блокировка выбора типа ПЧ

                // Отмена сигнала скорости 0-10 В
                if (outFanSpeedCheck.Checked) outFanSpeedCheck.Checked = false;
                // Отмена выбора сигнала аварии ПЧ
                if (outFanAlarmCheck.Checked) outFanAlarmCheck.Checked = false;
            }

            // Выбрали ЕС, был выбран ПЧ или "Нет"
            if (outFanFC_ECcombo.SelectedIndex == 2 && (outFanFC_EC_Index == 0 || outFanFC_EC_Index == 1))
            {
                // Перемещение элементов управления вверх
                outFanAlarmCheck.Location = new Point(outFanAlarmCheck.Location.X, outFanAlarmCheck.Location.Y - DELTA_Y);
                outFanSpeedCheck.Location = new Point(outFanSpeedCheck.Location.X, outFanSpeedCheck.Location.Y - DELTA_Y);
            }

            // Выбрали "Нет" или ПЧ, был выбран ЕС
            if ((outFanFC_ECcombo.SelectedIndex == 0 || outFanFC_ECcombo.SelectedIndex == 1) && outFanFC_EC_Index == 2)
            {
                // Перемещение элементов управления вниз
                outFanAlarmCheck.Location = new Point(outFanAlarmCheck.Location.X, outFanAlarmCheck.Location.Y + DELTA_Y);
                outFanSpeedCheck.Location = new Point(outFanSpeedCheck.Location.X, outFanSpeedCheck.Location.Y + DELTA_Y);
            }

            outFanFC_EC_Index = outFanFC_ECcombo.SelectedIndex;        // Сохранение значения выбранного индекса
            OutFanFC_ECcombo_cmdCheckedChanged(this, e);               // Командное слово
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
                confAddHeatPumpCheck.Enabled = true;                                    // Подтверждение работы
                pumpCurAddProtect.Enabled = true;                                       // Защита по току
            }
            else                                                                        // Отмена выбора насоса
            {
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
                closeOpenOutDampCheck.Checked = true;

                var outDampCheck = new List<CheckBox>()
                {
                    confOutDampCheck, heatOutDampCheck
                };

                foreach (var el in outDampCheck) el.Enabled = true;
            }
            else // Отмена выбора вытяжной заслонки
            {
                closeOpenOutDampCheck.Checked = false;
                confOutDampCheck.Enabled = false;
                heatOutDampCheck.Enabled = false;
            }
            OutDampCheck_cmdCheckedChanged(this, e);            // Командное слово
            if (ignoreEvents) return;
            OutDampCheck_signalsDOCheckedChanged(this, e);      // Сигналы DO ПЛК
            OutDampCheck_signalsDICheckedChanged(this, e);      // Сигналы DI ПЛК
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
            loadCanPanel.Height = 595;                          // Высота панели загрузки через CAN порт
            loadCanPanel.Show();
            InitializeCAN();                                    // Инициализация для загрузки по CAN порту
            formSignalsButton.Hide();                           // Скрытие кнопки "Сформировать"
            pic_signalsReady.Hide();                            // Скртие изображения сформированной карты сигналов
        }

        ///<summary>Нажали на вкладку "Загрузка", панель загрузки через Modbus</summary>
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

        ///<summary>Нажали на вкладку "Настройки" в строке меню</summary>
        private void ToolStripMenuItem_options_Click(object sender, EventArgs e)
        {
            mainPage.Hide();                                    // Скрытие панели опций элементов
            signalsPanel.Hide();                                // Скрытие панели распределения сигналов
            helpPanel.Hide();                                   // Скрытие панели отображения помощи
            loadCanPanel.Hide();                                // Скрытие панели загрузки через CAN-порт
            label_comboSysType.Text = "СПИСОК ПАРАМЕТРОВ";
            comboSysType.Hide(); panelElements.Hide();
            optionsPanel.Location = PANEL_POSITION;
            optionsPanel.Height = 468;
            optionsPanel.Show();
            formSignalsButton.Hide();                           // Скрытие кнопки "Сформировать"
            comboPlkType.Hide();                                // Скрытие выбора типа ПЛК
            panelBlocks.Hide();                                 // Скрытие панели выбора блоков расширения
            Plk_copyPanel.Hide();                               // Скрытие панели выбора типа контроллера
            pic_signalsReady.Hide();                            // Скрытие изображения статуса распределения сигналов
            panManBlocks.Hide();                                // Скрытие панели ручного выбора блоков
            autoSelectBlocks_check.Hide();                      // Скрытие выбора режима подбора блоков
        }

        ///<summary>Нажали вкладку "Помощь" в главном меню</summary>
        private void ToolStripMenuItem_help_Click(object sender, EventArgs e)
        {
            mainPage.Hide();                                    // Скрытие панели опций элементов
            signalsPanel.Hide();                                // Скрытие панели распределения сигналов
            optionsPanel.Hide();                                // Скрытие панели настроек
            loadCanPanel.Hide();                                // Скрытие панели загрузки через CAN-порт
            label_comboSysType.Text = "ПОМОЩЬ И РУКОВОДСТВО";
            comboSysType.Hide(); panelElements.Hide();
            helpPanel.Location = PANEL_POSITION;
            helpPanel.Height = 485;
            helpPanel.Width = Width - 50;                       // Ширина по границе окна
            helpPanel.Show();
            formSignalsButton.Hide();                           // Скрытие кнопки "Сформировать"
            comboPlkType.Hide();                                // Скрытие выбора типа ПЛК
            panelBlocks.Hide();                                 // Скрытие панели выбора блоков расширения
            Plk_copyPanel.Hide();                               // Скрытие панели выбора типа контроллера
            pic_signalsReady.Hide();                            // Скрытие изображения статуса распределения сигналов
            panManBlocks.Hide();                                // Скрытие панели ручного выбора блоков
            autoSelectBlocks_check.Hide();                      // Скрытие выбора режима подбора блоков
        }

        // Нажали кнопку "Назад" в панели загрузки Modbus
        private void BackModbusButton_Click(object sender, EventArgs e)
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

        ///<summary>Нажали кнопку "Назад" в панели настроек</summary>
        private void BackOptionsButton_Click_1(object sender, EventArgs e)
        {
            optionsPanel.Hide();                                                        // Скрытие панели настроек
            mainPage.Show(); panelElements.Show();
            label_comboSysType.Text = "ТИП СИСТЕМЫ";
            comboSysType.Show();
            formSignalsButton.Show();                                                   // Отображение кнопки "Сформировать"
            if (pic_signalsReady.Image == Properties.Resources.red_cross)
                pic_signalsReady.Show();                                                // Отображение статуса распределения сигналов
            
            // Отображение панели блоков расширения при наличии и автоматическом подборе
            if (expansion_blocks.Count > 0 && isAutoSelect) panelBlocks.Show();

            if (!isAutoSelect)                                                          // При ручном варианте подбора блоков расширения
            {
                autoSelectBlocks_check.Show(); panManBlocks.Show();
            }
            else if (isAutoSelect && !panelBlocks.Visible)                              // При авто варианте подбора блоков расширения
            {
                autoSelectBlocks_check.Show();
            }

            Plk_copyPanel.Show();                                                       // Отображение панели выбора типа контроллера
        }

        ///<summary>Нажали кнопку "Назад" в панели помощи</summary>
        private void BackHelpButton_Click(object sender, EventArgs e)
        {
            helpPanel.Hide();                                                           // Скрытие панели помощи
            loadModbusPanel.Hide();                                                     // Скрытие панели загрузки Modbus
            mainPage.Show(); panelElements.Show();
            label_comboSysType.Text = "ТИП СИСТЕМЫ";
            comboSysType.Show();
            formSignalsButton.Show();                                                   // Отображение кнопки "Сформировать"
            if (pic_signalsReady.Image == Properties.Resources.red_cross)
                pic_signalsReady.Show();                                                // Отображение статуса распределения сигналов
            
            // Отображение панели блоков расширения при наличии и автоматическом подборе
            if (expansion_blocks.Count > 0 && isAutoSelect) panelBlocks.Show();

            if (!isAutoSelect)                                                          // При ручном варианте подбора блоков расширения
            {
                autoSelectBlocks_check.Show(); panManBlocks.Show();
            }
            else if (isAutoSelect && !panelBlocks.Visible)                              // При авто варианте подбора блоков расширения
            {
                autoSelectBlocks_check.Show();
            }

            Plk_copyPanel.Show();                                                       // Отображение панели выбора типа контроллера
        }

        /// <summary>Опция для включения всплывающих подсказок</summary>
        private void TooltipsCheck_CheckedChanged(object sender, EventArgs e)   
        {
            if (tooltipsCheck.Checked) hintEnabled = true;      // Подсказки включены
            else hintEnabled = false;                           // Подсказки выключены
            LoadHints();                                        // Повторная инициализация подсказок
        }

        ///<summary>Блокировка события прокрутки для отдельного comboBox</summary>
        private void MouseWheel_comboBox(ComboBox comboBox, bool disable)
        {
            if (disable) comboBox.MouseWheel += ComboBox_MouseWheel;
            else comboBox.MouseWheel -= ComboBox_MouseWheel;
        }

        ///<summary>Проверка обновления до новой версии</summary>
        private void UpdateCheckBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Отключаем проверку SSL-сертификатов (осторожно: снижает безопасность!)
                /*ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, errors) => true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // Убедитесь, что используется актуальный протокол */

                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                    UpdateCheckInfo info = ad.CheckForDetailedUpdate();
                    
                    if (info.UpdateAvailable)
                    {
                        //You can create a dialog or message that prompts the user that there's an update. Make sure the user knows that your are updating the application.
                        DialogResult dialogResult = MessageBox.Show("Обнаружена новая версия. Обновить приложение?", "Обновление", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            //var diagRes = DialogResult.Yes;
                            var diagRes = new UpdateForm(ad).ShowDialog();
                            if (diagRes != DialogResult.OK)
                            {
                                //ad.UpdateAsyncCancel();
                                MessageBox.Show("Обновление отменено");
                            }
                            if (diagRes == DialogResult.OK)
                            {
                                Application.Restart();
                            }
                            //do something
                        }
                        //Updates the application 
                    }
                    else
                    {
                        if (sender != null)
                            MessageBox.Show("Обновлений не найдено");
                    }
                }
            }
            catch (DeploymentDownloadException ex)
            {
                MessageBox.Show($"Не удалось проверить наличие обновлений, проверьте подключение к интернету:\n{ex.Message}");
                Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Что-то пошло не так:\n{ex.Message}");
                Enabled = true;
                //ex.Log();
            }
        }

        ///<summary>Активация прокрутки колёсиком мыши элементов comboBox</summary>
        private void MouseWheelCheck_CheckedChanged(object sender, EventArgs e)
        {
            bool disable = !mouseWheelCheck.Checked;                // Признак блокировки прокрутки

            MouseWheelEvent_mainElements(disable);                  // Основные элементы формы, панели
            MouseWheelEvent_tableSignals(disable);                  // Элементы для таблицы сигналов
        }

        ///<summary>Блокировка прокрутки колёсиком мыши основных элементов формы</summary>
        private void MouseWheelEvent_mainElements(bool disable)
        {
            // Основная форма
            foreach (ComboBox comboBox in Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Датчики и сигналы
            foreach (ComboBox comboBox in sensorsPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Приточный вентилятор
            foreach (ComboBox comboBox in prFanPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Вытяжной вентилятор
            foreach (ComboBox comboBox in outFanPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Приточные фильтры
            foreach (ComboBox comboBox in filterPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Вытяжные фильтры
            foreach (ComboBox comboBox in outFilterPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Приточная воздушная заслонка
            foreach (ComboBox comboBox in dampPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Вытяжная воздушная заслонка
            foreach (ComboBox comboBox in outDampPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Основная панель нагревателя
            foreach (ComboBox comboBox in heatPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Водяной нагреватель
            foreach (ComboBox comboBox in watHeatPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Электрический нагреватель
            foreach (ComboBox comboBox in elHeatPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Основная панель второго нагревателя
            foreach (ComboBox comboBox in secHeatPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Водяной второй нагреватель
            foreach (ComboBox comboBox in watAddHeatPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Электрический второй нагреватель
            foreach (ComboBox comboBox in elAddHeatPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Основная панель охладителя
            foreach (ComboBox comboBox in coolPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Водяной охладитель
            foreach (ComboBox comboBox in watCoolPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Фреоновый охладитель
            foreach (ComboBox comboBox in frCoolPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Увлажнитель
            foreach (ComboBox comboBox in humidPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Рециркуляция
            foreach (ComboBox comboBox in recircPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Основная панель рекуператора
            foreach (ComboBox comboBox in recupPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Роторный рекуператор
            foreach (ComboBox comboBox in rotorRecupPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Пластинчатый рекуператор
            foreach (ComboBox comboBox in plastRecupPanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);
        }

        ///<summary>Изменили выбор блока 1 в ручном подборе</summary>
        private void ComboManBl_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveBlockPanel("AO", 1);                                  // Скрытие панели AO1
            RemoveBlockPanel("DO", 1);                                  // Скрытие панели DO1
            RemoveBlockPanel("UI", 1);                                  // Скрытие панели UI1

            if (comboManBl_1.SelectedIndex != 0)                        // Есть выбранный блок расширения
            {
                autoSelectBlocks_check.Enabled = false;
                manBl2_label.Show(); comboManBl_2.Show();
            }
            else                                                        // Выбрали "Нет"
            {
                autoSelectBlocks_check.Enabled = true;
                manBl2_label.Hide(); comboManBl_2.Hide();
            }

            // Алгоритм при выборе блоков расширения
            if (comboManBl_1.SelectedIndex == 1)                        // Блок расширения M72E08RA 
            {
                DO_block1_panelChanged_M72E08RA();                      // Отображение панели DO1
            }
            else if (comboManBl_1.SelectedIndex == 2)                   // Блок расширения M72E12RA
            {
                DO_block1_panelChanged_M72E12RA();
                UI_block1_panelChanged_M72E12RA();
            }
            else if (comboManBl_1.SelectedIndex == 3)                   // Блок расширения M72E12RB
            {
                DO_block1_panelChanged_M72E12RB();
                UI_block1_panelChanged_M72E12RB();
                AO_block1_panelChanged_M72E12RB();
            }
            else if (comboManBl_1.SelectedIndex == 4)                   // Блок расширения M72E16NA
            {
                UI_block1_panelChanged_M72E16NA();
            }

            // Распределение сигналов, если после добавления блока 1 карта сигналов не сформирована
            if (comboManBl_1.SelectedIndex != 0 && signalsReadyLabel.ForeColor == Color.Red) 
                Sig_distributionBtn_Click(this, e);
        }

        ///<summary>Изменили выбор блока 2 в ручном подборе</summary>
        private void ComboManBl_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveBlockPanel("AO", 2);                                  // Скрытие панели AO2
            RemoveBlockPanel("DO", 2);                                  // Скрытие панели DO2
            RemoveBlockPanel("UI", 2);                                  // Скрытие панели UI2

            if (comboManBl_2.SelectedIndex != 0)                        // Есть выбранный блок расширения
            {
                manBl3_label.Show(); comboManBl_3.Show();
                comboManBl_1.Enabled = false;
            }
            else                                                        // Выбрали "Нет"
            {
                manBl3_label.Hide(); comboManBl_3.Hide();
                comboManBl_1.Enabled= true;
            }

            // Алгоритм при выборе блоков расширения
            if (comboManBl_2.SelectedIndex == 1)                        // Блок расширения M72E08RA 
            {
                DO_block2_panelChanged_M72E08RA();                      // Отображение панели DO1
            }
            else if (comboManBl_2.SelectedIndex == 2)                   // Блок расширения M72E12RA
            {
                DO_block2_panelChanged_M72E12RA();
                UI_block2_panelChanged_M72E12RA();
            }
            else if (comboManBl_2.SelectedIndex == 3)                   // Блок расширения M72E12RB
            {
                DO_block2_panelChanged_M72E12RB();
                UI_block2_panelChanged_M72E12RB();
                AO_block2_panelChanged_M72E12RB();
            }
            else if (comboManBl_2.SelectedIndex == 4)                   // Блок расширения M72E16NA
            {
                UI_block2_panelChanged_M72E16NA();
            }

            // Распределение сигналов, если после добавления блока 2 карта сигналов не сформирована
            if (comboManBl_2.SelectedIndex != 0 && signalsReadyLabel.ForeColor == Color.Red)
                Sig_distributionBtn_Click(this, e);
        }

        ///<summary>Изменили выбор блока 3 в ручном подборе</summary>
        private void ComboManBl_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveBlockPanel("AO", 3);                                  // Скрытие панели AO3
            RemoveBlockPanel("DO", 3);                                  // Скрытие панели DO3
            RemoveBlockPanel("UI", 3);                                  // Скрытие панели UI3

            if (comboManBl_3.SelectedIndex != 0)                        // Есть выбранный блок расширения
            {
                comboManBl_2.Enabled = false;
            }
            else                                                        // Выбрали "Нет"
            {
                comboManBl_2.Enabled = true;
            }

            // Алгоритм при выборе блоков расширения
            if (comboManBl_3.SelectedIndex == 1)                        // Блок расширения M72E08RA 
            {
                DO_block3_panelChanged_M72E08RA();                      // Отображение панели DO1
            }
            else if (comboManBl_3.SelectedIndex == 2)                   // Блок расширения M72E12RA
            {
                DO_block3_panelChanged_M72E12RA();
                UI_block3_panelChanged_M72E12RA();
            }
            else if (comboManBl_3.SelectedIndex == 3)                   // Блок расширения M72E12RB
            {
                DO_block3_panelChanged_M72E12RB();
                UI_block3_panelChanged_M72E12RB();
                AO_block3_panelChanged_M72E12RB();
            }
            else if (comboManBl_3.SelectedIndex == 4)                   // Блок расширения M72E16NA
            {
                UI_block3_panelChanged_M72E16NA();
            }

            // Распределение сигналов, если после добавления блока 1 карта сигналов не сформирована
            if (comboManBl_3.SelectedIndex != 0 && signalsReadyLabel.ForeColor == Color.Red)
                Sig_distributionBtn_Click(this, e);
        }

        ///<summary>Выбор автоматического подбора блоков расширения</summary>
        private void AutoSelectBlocks_check_CheckedChanged(object sender, EventArgs e)
        {
            if (autoSelectBlocks_check.Checked)                             // Выбор автоматического подбора блоков
            {
                isAutoSelect = true;                                        // Признак автоматического подбора
                panManBlocks.Hide();                                        // Скрытие панели ручного выбора блоков

                // Автоматическое распределение сигналов, если карта сигналов не сформирована
                if (signalsReadyLabel.ForeColor == Color.Red)
                {
                    ChangeBlocks_panels();                                  // Определение удаления/отображения панелей
                    Sig_distributionBtn_Click(this, e);                     // Распределение сигналов
                }
            }
            else                                                            // Выбор ручного подбора блоков
            {
                isAutoSelect = false;                                       // Сброс признака автоматического подбора
                panManBlocks.Show();                                        // Оторажение панели ручного выбора блоков
            }
        }

        ///<summary>Выбор и отмена выбора вытяжного вентилятора</summary>
        private void OutFanCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (outFanCheck.Checked)                                        // Выбрали наличие вытяжного вентилятора
            {
                outFanPanel.Size = new Size(outFanPanel.Width, panelOutFan_height);
                fanPicture2.Show();                                         // Отображение изображения
            }
            else                                                            // Отмена выбора вытяжного вентилятора
            {
                panelOutFan_height = outFanPanel.Height;
                outFanPanel.Size = new Size(outFanPanel.Width, 30);         // Сужение высоты для одной настройки
                fanPicture2.Hide();                                         // Скрытие изображения
            }
            
            OutFanPSCheck_cmdCheckedChanged(this, e);                       // Настройка PS вытяжного вентилятора
            OutFanThermoCheck_cmdCheckedChanged(this, e);                   // Термоконтакты вытяжного вентилятора
            CurDefOutFanCheck_cmdCheckedChanged(this, e);                   // Защита по току вытяжного вентилятора
            OutFanStStopCheck_CheckedChanged(this, e);                      // Сигнал "ПУСК/СТОП" с ПЧ вытяжного
            OutFanAlarmCheck_cmdCheckedChanged(this, e);                    // Сигнал аварии ПЧ вытяжного
            OutFanSpeedCheck_cmdCheckedChanged(this, e);                    // Скорость ПЧ вытяжного
            CheckResOutFan_CheckedChanged(this, e);                         // Резервный двигатель вытяжного
            OutDampFanCheck_CheckedChanged(this, e);                        // Заслонка вытяжного вентилятора
            OutDampConfirmFanCheck_CheckedChanged(this, e);                 // Подтверждение открытия заслонки вытяжного 
        }

        ///<summary>Изменили тип контроллера (на основной форме)</summary>
        private void ComboPlkType_copy_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboPlkType.SelectedIndex = comboPlkType_copy.SelectedIndex;
        }

        ///<summary>Блокировка прокрутки колёсиком мыши таблицы сигналов</summary>
        private void MouseWheelEvent_tableSignals(bool disable)
        {
            // Сигналы UI, контроллер
            foreach (ComboBox comboBox in plk_UIpanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Сигналы UI, блок расширения 1
            foreach (ComboBox comboBox in block1_UIpanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Сигналы UI, блок расширения 2
            foreach (ComboBox comboBox in block2_UIpanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Сигналы UI, блок расширения 3
            foreach (ComboBox comboBox in block3_UIpanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Сигналы DO, контроллер
            foreach (ComboBox comboBox in plk_DOpanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Сигналы DO, блок расширения 1
            foreach (ComboBox comboBox in block1_DOpanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Сигналы DO, блок расширения 2
            foreach (ComboBox comboBox in block2_DOpanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Сигналы DO, блок расширения 3
            foreach (ComboBox comboBox in block3_DOpanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Сигналы AO, контроллер
            foreach (ComboBox comboBox in plk_AOpanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Сигналы AO, блок расширения 1
            foreach (ComboBox comboBox in block1_AOpanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Сигналы AO, блок расширения 2
            foreach (ComboBox comboBox in block2_AOpanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);

            // Сигналы AO, блок расширения 3
            foreach (ComboBox comboBox in block3_AOpanel.Controls.OfType<ComboBox>())
                MouseWheel_comboBox(comboBox, disable);
        }

        ///<summary>Блокировка события прокрутки колёсиком мыши</summary>
        private void ComboBox_MouseWheel(object sender, MouseEventArgs e) =>
            ((HandledMouseEventArgs)e).Handled = true;
       

        ///<summary>Нажали кнопку "Загрузить в ПЛК" в панели сигналов</summary>
        private void LoadPLC_SignalsButton_Click(object sender, EventArgs e)
        {
            //ToolStripMenuItem_load_Click(this, e);                                    // Открытие панели настроек
            LoadCanPanel_Open(this, e);                                                 // Открытие панели загрузки в контроллер, CAN порт
            fromSignalsMove = true;                                                     // Переход из панели выбора сигналов
            FormNetButton_Click(this, e);                                               // Формирование списка сигналов и командных слов для записи

            // Обновление сообщение о статусе данных
            dataMatchPLC_label.Text = "(неизвестно)";
            dataMatchPLC_label.ForeColor = Color.Black;

            // Обновление сообщение о статусе прошивки
            firmwareMatchPLC_label.Text = "(неизвестно)";
            firmwareMatchPLC_label.ForeColor = Color.Black;
        }

        ///<summary>Нажали на ссылку сайта Moderon</summary>
        private void LinkModeronWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkModeronWeb.LinkVisited = true;
            System.Diagnostics.Process.Start("http://moderon-electric.ru/");
        }

        ///<summary>Нажали на ссылку руководства по эксплуатации Moderon</summary>
        private void LinkManualModeron_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkManualModeron.LinkVisited = true;
            System.Diagnostics.Process.Start(
                "https://moderon-electric.ru/software/moderon-hvac/docs-moderon-hvac/");
        }

        ///<summary>Нажали на ссылку подключение к контроллеру</summary>
        private void LinkConnectPlc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkConnectPlc.LinkVisited = true;
            System.Diagnostics.Process.Start(
                "https://moderon-electric.ru/software/moderon-hvac/connecting-controller/");
        }

        ///<summary>Нажали на пункт "О программе"</summary>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Отображение информационного окна
            FormInfo formInfo = new();
            formInfo.Show(this);
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
            if (pic_signalsReady.Image == Properties.Resources.red_cross)
                pic_signalsReady.Show();                    // Отображение изображения сфомированной карты сигналов
            SignalsTableReSize(Size.Width, Size.Height);    // Таблица сигналов, пересчёт размеров
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

        ///<summary>Изменили питание приточного вентилятора</summary>
        private void PrFanPowCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (prFanPowCombo.SelectedIndex == 0)
                fanPicture1.Image = Properties.Resources.fan380;
            else
                fanPicture1.Image = Properties.Resources.fan220;
        }

        ///<summary>Изменили питание вытяжного вентилятора</summary>
        private void OutFanPowCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (outFanPowCombo.SelectedIndex == 0)
                fanPicture2.Image = Properties.Resources.fan380;
            else
                fanPicture2.Image = Properties.Resources.fan220;
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
