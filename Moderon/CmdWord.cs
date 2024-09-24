using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Windows.Forms;

// Файл для формирования и вывода готовых командных слов под запись

namespace Moderon
{
    public partial class Form1 : Form
    {
        ushort[] uiSignals = new ushort[48];            // Массив для сигналов UI
        ushort[] doSignals = new ushort[26];            // Массив для сигналов DO
        ushort[] aoSignals = new ushort[7];             // Массив для сигналов AO
        ushort[] cmdWords = new ushort[30];             // Массив для командных слов

        ushort cmdW1, cmdW2, cmdW3, cmdW4, cmdW5, cmdW6, cmdW7, cmdW8, cmdW9, cmdW10,
            cmdW11, cmdW12, cmdW13, cmdW14, cmdW15, cmdW16, cmdW17, cmdW18, cmdW19, cmdW20,
            cmdW21, cmdW22, cmdW23, cmdW24, cmdW25, cmdW26, cmdW27, cmdW28, cmdW29, cmdW30,
            cmdW_fire;

        ///<summary>Предварительная очистка массивов перед формированием значений</summary>
        private void ResetSignalsAll()
        {
            for (int i = 0; i < uiSignals.Length; i++) uiSignals[i] = 0;
            for (int i = 0; i < doSignals.Length; i++) doSignals[i] = 0;
            for (int i = 0; i < aoSignals.Length; i++) aoSignals[i] = 0;
        }

        ///<summary>Нажали на кнопку "Собрать", формирование массивов сигналов</summary>
        private void FormNetButton_Click(object sender, EventArgs e)
        {
            ushort count = 1;                       // Глобальный счетчик учета позиции

            ResetSignalsAll();                      // Сброс для массивов сигналов
            BuildUI_Signals();                      // Сборка массива для сигналов UI
            BuildDO_Signals();                      // Сборка массива для сигналов DO
            BuildAO_Signals();                      // Сборка массива для сигналов AO
            BuildCmdWords();                        // Сборка массива командных слов

            cmdWordsTextBox.Text = "";              // Очистка текстового поля вывода командных слов

            // Формирование командных слов для вывода в таблице сигналов
            for (ushort counter = 1; counter <= cmdWords.Length; counter++) 
            { // Для командных слов
                cmdWordsTextBox.Text += counter.ToString() + ") " + 
                    cmdWords[counter - 1].ToString();
                if (counter < cmdWords.Length) cmdWordsTextBox.Text += Environment.NewLine;
                ++count;
            } 
            
            // Отображение сигнала пожарной сигнализации
            cmdWordsTextBox.Text += Environment.NewLine;
            cmdWordsTextBox.Text += count.ToString() + "_fire" + ") " + cmdW_fire.ToString();

            // Формирование кодов сигналов и командных слов для записи в ПЛК
            if (loadCanPanel.Visible) FillWriteCanTextBox();              
        }

        ///<summary>Заполнение textBox данных для записи в ПЛК</summary>
        private void FillWriteCanTextBox()
        {  
            ushort count = 0;                           // Глобальный счётчик учёта номера позиции
            ushort value_index = 1;
            string firmwareType = comboPlkType.SelectedIndex == 0 ? FIRMWARE_FILE_MINI : FIRMWARE_FILE_OPT;

            writeCanTextBox.Text = "";                  // Очистка textBox перед заполнением

            // Версия прошивки контроллера в зависимости от типа ПЛК
            short firmware_file = firmwareType == FIRMWARE_FILE_OPT ?
                short.Parse(firmwareType.Substring(4, 5)) : short.Parse(firmwareType.Substring(5, 5));

            string firmwre_version = ParseFirmware_toDate(firmware_file, false);
            
            writeCanTextBox.Text += $"Версия прошивки: {firmware_file} ({firmwre_version})";
            for (int i = 0; i < 2; i++) writeCanTextBox.Text += Environment.NewLine;
            
            // Запись textBox для UI сигналов
            for (int i = 0; i < uiSignals.Length; i++)
            {
                if (value_index == 17) value_index = 1;
                
                if (i < 16)                             // ПЛК
                {
                    writeCanTextBox.Text += $"{count}) UI{value_index}:\t\t{uiSignals[i]}";
                }

                else if (i >= 16 && i < 32)             // Первый блок расширения
                {
                    writeCanTextBox.Text += $"{count}) EX1_UI{value_index}:\t{uiSignals[i]}";
                }

                else if (i >= 32 && i < 48)             // Второй блок расширения
                {
                    writeCanTextBox.Text += $"{count}) EX2_UI{value_index}:\t{uiSignals[i]}";
                }

                writeCanTextBox.Text += Environment.NewLine;
                value_index++;
                count++;
            }

            writeCanTextBox.Text += Environment.NewLine;
            value_index = 1;

            // DO сигналы
            for (int i = 0; i < doSignals.Length; i++)
            {
                if (value_index == 11) value_index = 1;

                if (i < 10)                             // ПЛК
                {
                    if (i == 9)
                        writeCanTextBox.Text += $"{count}) DO{value_index}:\t{doSignals[i]}";
                    else
                        writeCanTextBox.Text += $"{count}) DO{value_index}:\t\t{doSignals[i]}";
                }

                else if (i >= 10 && i < 18)             // Первый блок расширения
                {
                    writeCanTextBox.Text += $"{count}) EX1_DO{value_index}:\t{doSignals[i]}";
                    if (value_index == 8) value_index = 0;
                }

                else if (i >= 18 && i < 26)             // Второй блок расширения
                {
                    writeCanTextBox.Text += $"{count}) EX2_DO{value_index}:\t{doSignals[i]}";
                }

                writeCanTextBox.Text += Environment.NewLine;
                value_index++;
                count++;
            }

            writeCanTextBox.Text += Environment.NewLine;
            value_index = 1;

            // AO сигналы
            for (int i = 0; i < aoSignals.Length; i++)
            {
                if (value_index == 4) value_index = 1;

                if (i < 3)                              // ПЛК
                {
                    writeCanTextBox.Text += $"{count}) AO{value_index}:\t\t{aoSignals[i]}";
                }

                else if (i >= 3 && i < 5)               // Первый блок расширения
                {
                    writeCanTextBox.Text += $"{count}) EX1_AO{value_index}:\t{aoSignals[i]}";
                    if (value_index == 2) value_index = 0;
                }

                else if (i >= 5 && i < 7)
                {
                    writeCanTextBox.Text += $"{count}) EX2_AO{value_index}:\t{aoSignals[i]}";
                }

                writeCanTextBox.Text += Environment.NewLine;
                value_index++;
                count++;
            }

            writeCanTextBox.Text += Environment.NewLine;
            value_index = 1;
            count = 84;             // Позиция для начала командных слов

            // Командные слова
            for (int i = 0; i < cmdWords.Length; i++)
            {
                writeCanTextBox.Text += $"{count}) Word_{value_index}:\t{cmdWords[i]}";
                if (i != cmdWords.Length - 1) writeCanTextBox.Text += Environment.NewLine;
                value_index++;
                count++;
            }
        }

        ///<summary>Сборка массива командных слов из полученных значений</summary>
        private void BuildCmdWords()
        {
            cmdWords[0] = cmdW1; cmdWords[1] = cmdW2; cmdWords[2] = cmdW3; cmdWords[3] = cmdW4;
            cmdWords[4] = cmdW5; cmdWords[5] = cmdW6; cmdWords[6] = cmdW7; cmdWords[7] = cmdW8;
            cmdWords[8] = cmdW9; cmdWords[9] = cmdW10; cmdWords[10] = cmdW11; cmdWords[11] = cmdW12;
            cmdWords[12] = cmdW13; cmdWords[13] = cmdW14; cmdWords[14] = cmdW15; cmdWords[15] = cmdW16;
            cmdWords[16] = cmdW17; cmdWords[17] = cmdW18; cmdWords[18] = cmdW19; cmdWords[19] = cmdW20;
            cmdWords[20] = cmdW21; cmdWords[21] = cmdW22; cmdWords[22] = cmdW23; cmdWords[23] = cmdW24;
            cmdWords[24] = cmdW25; cmdWords[25] = cmdW26; cmdWords[26] = cmdW27; cmdWords[27] = cmdW28;
            cmdWords[28] = cmdW29; cmdWords[29] = cmdW30;
        }

        ///<summary>Сборка массива для сигналов UI</summary>
        private void BuildUI_Signals()
        {
            ushort counter = 0;

            var ui_combos = new List<ComboBox>()
            {
                // ПЛК (для 16 UI)
                UI1_combo, UI2_combo, UI3_combo, UI4_combo, UI5_combo, UI6_combo, UI7_combo, UI8_combo,
                UI9_combo, UI10_combo, UI11_combo, null, null, null, null, null,
                // Блок расширения 1, 16 UI
                UI1bl1_combo, UI2bl1_combo, UI3bl1_combo, UI4bl1_combo, UI5bl1_combo, UI6bl1_combo, UI7bl1_combo,
                UI8bl1_combo, UI9bl1_combo, UI10bl1_combo, UI11bl1_combo, UI12bl1_combo, UI13bl1_combo,
                UI14bl1_combo, UI15bl1_combo, UI16bl1_combo,
                // Блок расширения 2, 16 UI
                UI1bl2_combo, UI2bl2_combo, UI3bl2_combo, UI4bl2_combo, UI5bl2_combo, UI6bl2_combo, UI7bl2_combo,
                UI8bl2_combo, UI9bl2_combo, UI10bl2_combo, UI11bl2_combo, UI12bl2_combo, UI13bl2_combo,
                UI14bl2_combo, UI15bl2_combo, UI16bl2_combo,
            };

            foreach (var el in ui_combos)
            {
                if (el != null && el.SelectedItem.ToString() != NOT_SELECTED)
                {
                    string name = el.SelectedItem.ToString();
                    Ui find_ui = list_ui.Find(x => x.Name == name);
                    ushort value = find_ui.Code;

                    if (value > 100 && value < 150) value -= 100;   // Коды для датчиков, вычитаем 100

                    uiSignals[counter] = value;
                }
                else uiSignals[counter] = 0;

                counter++;
            }
        }

        ///<summary>Сборка массива для сигналов DO</summary>
        private void BuildDO_Signals()
        {
            ushort counter = 0;

            var do_combos = new List<ComboBox>()
            {
                // ПЛК
                DO1_combo, DO2_combo, DO3_combo, DO4_combo, DO5_combo, DO6_combo, null, null, null, null,
                // Блок расширения 1, 8 DO
                DO1bl1_combo, DO2bl1_combo, DO3bl1_combo, DO4bl1_combo, DO5bl1_combo, DO6bl1_combo,
                DO7bl1_combo, DO8bl1_combo,
                // Блок расширения 2, 8 DO
                DO1bl2_combo, DO2bl2_combo, DO3bl2_combo, DO4bl2_combo, DO5bl2_combo, DO6bl2_combo,
                DO7bl2_combo, DO8bl2_combo,
            };

            foreach (var el in do_combos)
            {
                if (el != null && el.SelectedItem.ToString() != NOT_SELECTED)
                {
                    string name = el.SelectedItem.ToString();
                    Do find_do = list_do.Find(x => x.Name == name);
                    ushort value = find_do.Code;

                    doSignals[counter] = value;
                }
                else doSignals[counter] = 0;

                counter++;
            }
        }

        ///<summary>Сборка массива для сигналов AO</summary>
        private void BuildAO_Signals()
        {
            ushort counter = 0;

            var ao_combos = new List<ComboBox>()
            {
                // ПЛК
                AO1_combo, AO2_combo, AO3_combo,
                // Блок расширения 1, 2
                AO1bl1_combo, AO2bl1_combo, AO1bl2_combo, AO2bl2_combo
            };

            foreach (var el in ao_combos)
            {
                if (el != null && el.SelectedItem.ToString() != NOT_SELECTED)
                {
                    string name = el.SelectedItem.ToString();
                    Ao find_ao = list_ao.Find(x => x.Name == name);
                    ushort value = find_ao.Code;

                    aoSignals[counter] = value;
                } 
                else aoSignals[counter] = 0;

                counter++;
            }
        }

        ///<summary>Инициализация командных слов</summary>
        private void Form1_InitCmdWord(object sender, EventArgs e)
        {
            CommandWord_1();            // Основной блок запуска (тип управления системой)
            CommandWord_2();            // Основная приточная заслонка (либо общая приточная воздушная заслонка)
            CommandWord_3();            // Основная вытяжная заслонка (либо общая вытяжная воздушная заслонка)
            CommandWord_4();            // Приточные и вытяжные фильтры
            CommandWord_5();            // (удалено!)
            CommandWord_6();            // Датчики
            CommandWord_7();            // Рекуперация, второе слово
            CommandWord_8();            // Рециркуляция
            CommandWord_9();            // Рекуператор
            CommandWord_10();           // Основной электрический нагреватель
            CommandWord_11();           // Основной водяной нагреватель
            CommandWord_12();           // Основной водяной охладитель
            CommandWord_13();           // Водяной догреватель
            CommandWord_14();           // Электрический догреватель
            CommandWord_15();           // Основной увлажнитель
            CommandWord_16();           // Резервное слово 1
            CommandWord_17();           // Резервное слово 2 
            CommandWord_18();           // Дополнительная вытяжка
            CommandWord_19();           // Основной приточный вентилятор
            CommandWord_20();           // Резервный приточный вентилятор
            CommandWord_21();           // Основной вытяжной вентилятор
            CommandWord_22();           // Резервный вытяжной вентилятор
            CommandWord_23();           // Водяной преднагреватель
            CommandWord_24();           // Электрический преднагреватель
            CommandWord_25();           // Резервное слово 3
            CommandWord_26();           // Резервное слово 4
            CommandWord_27();           // Резервное слово 5
            CommandWord_28();           // Резервное слово 6
            CommandWord_29();           // Резервное слово 7
            CommandWord_30();           // Резервное слово 8
            CommandWord_fire();         // Пожарная сигнализация
        }

        ///<summary>Формирование командного слова основного блока старт/стоп</summary>>
        private void CommandWord_1()
        {
            bool bit0, bit1, bit2, bit3, bit4;

            bit0 = stopStartCombo.SelectedIndex == 0;                           // Переключатель СТОП/ПУСК
            bit1 = stopStartCombo.SelectedIndex == 1;                           // Импульсная кнопка СТОП/ПУСК
            bit2 = stopStartCombo.SelectedIndex == 2;                           // Кнопки ПУСК и СТОП
            bit3 = bit4 = false;                                                // Не используются

            cmdW1 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4));
        }

        ///<summary>Формирование командного слова приточной воздушной заслонки</summary>
        private void CommandWord_2()
        {
            bool bit0, bit1, bit2, bit3;

            bit0 = dampCheck.Checked;                                           // Наличие приточной заслонки
            bit1 = dampCheck.Checked && confPrDampCheck.Checked;                // Подтверждение открытия
            bit2 = false; // Не используется
            bit3 = dampCheck.Checked && heatPrDampCheck.Checked;                // Обогрев заслонки

            cmdW2 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3));
        }

        /// <summary>Формирование командного слова вытяжной воздушной заслонки</summary>
        private void CommandWord_3()
        {
            bool bit0, bit1, bit2, bit3;

            bit0 = outDampCheck.Checked;                                        // Наличие вытяжной заслонки
            bit1 = outDampCheck.Checked && confOutDampCheck.Checked;            // Подтверждение открытия
            bit2 = false; // Не используется
            bit3 = outDampCheck.Checked && heatOutDampCheck.Checked;            // Обогрев заслонки

            cmdW3 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3));
        }

        ///<summary>Формирование командного слова приточных фильтров</summary>
        private void CommandWord_4()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5;

            bit0 = filterCheck.Checked;                                         // Воздушный фильтр 1, приток
            bit1 = filterCheck.Checked && filterPrCombo.SelectedIndex > 0;      // Воздушный фильтр 2, приток
            bit2 = filterCheck.Checked && filterPrCombo.SelectedIndex > 1;      // Воздушный фильтр 3, приток
            bit3 = filterCheck.Checked && filterOutCombo.SelectedIndex > 0;
            bit4 = filterCheck.Checked && filterOutCombo.SelectedIndex > 1;
            bit5 = filterCheck.Checked && filterOutCombo.SelectedIndex > 2;

            cmdW4 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5));
        }

        /// <summary>Формирование командного слова вытяжных фильтров (удалено!)</summary>
        private void CommandWord_5() => cmdW5 = 0;

        /// <summary>Формирование командного слова датчиков</summary>
        private void CommandWord_6()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9, bit10;

            bit0 = prChanSensCheck.Checked;                                     // Канальный датчик температуры, приток
            bit1 = false;                                                       // Канальный датчик приток 2
            bit2 = outdoorChanSensCheck.Checked;                                // Датчик наружного воздуха
            bit3 = heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0;     // Датчик обратной воды
            bit4 = roomTempSensCheck.Checked;                                   // Комнатный датчик температуры  
            bit5 = recupCheck.Checked && recDefTempCheck.Checked;               // Датчик защиты рекуператора
            bit6 = chanHumSensCheck.Checked;                                    // Канальный датчик влажности
            bit7 = roomHumSensCheck.Checked;                                    // Комнатный датчик влажности
            bit8 = addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0; // Датчик обратной воды, догреватель
            bit9 = outChanSensCheck.Checked;                                    // Вытяжной канальный датчик
            bit10 = false;                                                      // Датчик обратной воды гликолевого рекуператора

            cmdW6 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8) +
                512 * Convert.ToUInt16(bit9) + 1024 * Convert.ToUInt16(bit10));
        }

        /// <summary>Командное слово рекуперация, второе слово</summary>
        private void CommandWord_7()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8;

            // ПВ-система и выбран гликолевый рекуператор
            bool glikRecup = comboSysType.SelectedIndex == 1 && recupCheck.Checked && recupTypeCombo.SelectedIndex == 2;

            bit0 = false;                                                           // (нет)
            bit1 = glikRecup && pumpGlicRecCheck.Checked;                           // Наличие основного насоса гликолевого                                                      
            bit2 = glikRecup && pumpGlicRecCheck.Checked &&
                pumpGlikConfCheck.Checked;                                          // Наличие KPI основного насоса гликолевого
            bit3 = glikRecup && reservPumpGlik.Checked;                             // Наличие резервного насоса гликолевого
            bit4 = glikRecup && reservPumpGlik.Checked &&                           // Наличие KPI резервного насоса гликолевого
                confGlikResPumpCheck.Checked;
            bit5 = false;                                                           // (нет)
            bit6 = false;                                                           // (нет)
            bit7 = glikRecup && pumpGlicRecCheck.Checked &&                         // Реле тока основного насоса гликолевого
                pumpGlikCurProtect.Checked;                                         
            bit8 = glikRecup && reservPumpGlik.Checked &&                           // Реле тока резервного насоса гликолевого 
                pumpGlikResCurProtect.Checked;

            cmdW7 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8));
        }

        /// <summary>Командное слово рециркуляция</summary>
        private void CommandWord_8()
        {
            bool bit0, bit1, bit2;

            bit0 = recircCheck.Checked;                                         // Наличие рециркуляции
            bit1 = recircCheck.Checked && recircAOSigCheck.Checked;             // Управление 0-10 В
            bit2 = recircCheck.Checked && recircPrDampAOCheck.Checked;          // Сигнал 0-10 В на приточную заслонку

            cmdW8 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2));
        }

        /// <summary>Командное слово рекуператора</summary>
        private void CommandWord_9()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9, bit10;

            if (recupCheck.Checked)                                             // Выбран рекуператор
            { 
                bit0 = recupTypeCombo.SelectedIndex == 1;                       // Пластинчатый рекуператор
                bit1 = recupTypeCombo.SelectedIndex == 0;                       // Роторный рекуператор
                bit2 = recupTypeCombo.SelectedIndex == 2;                       // Гликолевый рекуператор
                // 0-10 В
                bit3 = (recupTypeCombo.SelectedIndex == 0 && analogRotRecCheck.Checked) ||
                    (recupTypeCombo.SelectedIndex == 1 && bypassPlastCombo.SelectedIndex == 2) ||
                        (recupTypeCombo.SelectedIndex == 2 && analogGlikRecCheck.Checked); 
                bit4 = false;                                                   // Контактор рекуператора
                // Откр/закр
                bit5 = (recupTypeCombo.SelectedIndex == 0 && startRotRecCheck.Checked) ||
                    (recupTypeCombo.SelectedIndex == 1 && bypassPlastCombo.SelectedIndex == 1); 
                bit6 = false;                                                   // Наличие ATV212
                bit7 = recDefPsCheck.Checked;                                   // Датчик PS рекуператора
                bit8 = recDefTempCheck.Checked;                                 // Датчик температуры за рекуператором
                bit9 = false;                                                   // Реле давления насоса гликолевого
                // Внешний сигнал аварии роторного рекуператора
                bit10 = outSigAlarmRotRecCheck.Checked && recupTypeCombo.SelectedIndex == 0; 
            }
            else
                bit0 = bit1 = bit2 = bit3 = bit4 = bit5 = bit6 = bit7 = bit8 = bit9 = bit10 = false;

            cmdW9 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8) +
                512 * Convert.ToUInt16(bit9) + 1024 * Convert.ToUInt16(bit10));
        }

        /// <summary>Командное слово основного электрического нагревателя</summary>
        private void CommandWord_10()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9, bit10, bit11;

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 1)        // Выбран электрический нагреватель
            { 
                bit0 = firstStHeatCombo.SelectedIndex == 2;                     // Управление 0-10 В
                bit1 = firstStHeatCombo.SelectedIndex == 1;                     // Управление ШИМ 5В
                bit2 = elHeatStagesCombo.SelectedIndex > 6;                     // Ступерь 8
                bit3 = elHeatStagesCombo.SelectedIndex > 5;                     // Ступень 7
                bit4 = elHeatStagesCombo.SelectedIndex > 4;                     // Ступень 6
                bit5 = elHeatStagesCombo.SelectedIndex > 3;                     // Ступень 5
                bit6 = elHeatStagesCombo.SelectedIndex > 2;                     // Ступень 4
                bit7 = elHeatStagesCombo.SelectedIndex > 1;                     // Ступень 3
                bit8 = elHeatStagesCombo.SelectedIndex > 0;                     // Ступень 2
                bit9 = true;                                                    // Ступень 1 по умолчанию
                bit10 = fireThermCheck.Checked;                                 // Термовыключатель 1, пожар ТЭНов
                bit11 = overheatThermCheck.Checked;                             // Термовыключатель 2, перегрев ТЭНов
            }
            else                                                                // Не выбран электрический нагреватель
                bit0 = bit1 = bit2 = bit3 = bit4 = bit5 = bit6 = bit7 = bit8 = bit9 = bit10 = bit11 = false;

            cmdW10 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8) +
                512 * Convert.ToUInt16(bit9) + 1024 * Convert.ToUInt16(bit10) + 2048 * Convert.ToUInt16(bit11));
        }

        /// <summary>Командное слово основного водяного нагревателя</summary>
        private void CommandWord_11()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8;

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)                // Выбран водяной нагреватель
            { 
                bit0 = true;                                                            // Наличие водяного нагревателя
                bit1 = true;                                                            // Наличие основного насоса
                bit2 = confHeatPumpCheck.Checked;                                       // Подтверждение работы насоса
                bit3 = reservPumpHeater.Checked;                                        // Наличие резервного насоса
                bit4 = reservPumpHeater.Checked && confHeatResPumpCheck.Checked;        // Подтверждение работы резервного насоса
                bit5 = TF_heaterCheck.Checked;                                          // Воздушный термостат
                bit6 = false;                                                           // Контроль протечки
                bit7 = pumpCurProtect.Checked;                                          // Наличие реле тока основного насоса
                bit8 = pumpCurResProtect.Checked;                                       // Наличие реле тока резервного насоса
            }
            else
                bit0 = bit1 = bit2 = bit3 = bit4 = bit5 = bit6 = bit7 = bit8 = false;

            cmdW11 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8));
        }

        /// <summary>Командное слово основного охладителя</summary>
        private void CommandWord_12()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9;

            if (coolerCheck.Checked)                                                    // Выбран охладитель
            { 
                if (coolTypeCombo.SelectedIndex == 0)                                   // Фреоновый охладитель
                { 
                    bit0 = false;                                                       // Наличие водяного охладителя
                    bit1 = true;                                                        // Наличие фреонового охладителя
                    bit2 = true;                                                        // Первая ступень
                    bit3 = frCoolStagesCombo.SelectedIndex > 0;                         // Вторая ступень
                    bit4 = frCoolStagesCombo.SelectedIndex > 1;                         // Третья ступень
                    bit5 = frCoolStagesCombo.SelectedIndex > 2;                         // Четвертая ступень
                    bit6 = analogFreonCheck.Checked;                                    // Аналоговый сигнал охладителя
                    bit7 = alarmFrCoolCheck.Checked;                                    // Аварийный сигнал, фреон
                    bit8 = thermoCoolerCheck.Checked;                                   // Воздушный термостат
                    bit9 = dehumModeCheck.Checked && addHeatCheck.Checked;              // Режим осушителя
                } 
                else                                                                    // Водяной охладитель
                {
                    bit0 = true;                                                        // Наличие водяного охладителя
                    bit1 = false;                                                       // Наличие фреонового охладителя
                    bit2 = bit3 = bit4 = bit5 = false;                                  // Ступени фреонового охладителя
                    bit6 = analogCoolCheck.Checked;                                     // Аналоговый сигнал
                    bit7 = false;                                                       // Аварийный сигнал, фреон
                    bit8 = false;                                                       // Воздушный термостат, фреон
                    bit9 = false;                                                       // Режим осушителя, фреон
                }
            }
            else                                                                        // Нет выбранного охладителя
                bit0 = bit1 = bit2 = bit3 = bit4 = bit5 = bit6 = bit7 = bit8 = bit9 = false;

            cmdW12 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8) +
                512 * Convert.ToUInt16(bit9));
        }

        /// <summary>Командное слово водяного догревателя (второго нагревателя)</summary>
        private void CommandWord_13()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8;

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)            // Выбран второй водяной нагреватель
            { 
                bit0 = true;                                                            // Наличие водяного догревателя
                bit1 = pumpAddHeatCheck.Checked;                                        // Наличие водяного насоса
                bit2 = pumpAddHeatCheck.Checked && confAddHeatPumpCheck.Checked;        // Подтверждение работы насоса
                bit3 = reservPumpAddHeater.Checked;                                     // Наличие резервного насоса
                bit4 = reservPumpAddHeater.Checked && confAddHeatResPumpCheck.Checked;  // Подтверждение работы резервного насоса
                bit5 = TF_addHeaterCheck.Checked;                                       // Термостат
                bit6 = false;                                                           // Контроль протечки
                bit7 = pumpCurAddProtect.Checked;                                       // Наличие реле тока основного насоса
                bit8 = pumpCurResAddProtect.Checked;                                    // Наличие реле тока резервного насоса
            }
            else                                                                        // Не выбран водяной догреватель
                bit0 = bit1 = bit2 = bit3 = bit4 = bit5 = bit6 = bit7 = bit8 = false;

            cmdW13 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) * 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8));
        }

        /// <summary>Командное слово электрического догревателя</summary>
        private void CommandWord_14()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9, bit10, bit11;

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 1)            // Выбран электрический догреватель
            { 
                bit0 = firstStAddHeatCombo.SelectedIndex == 2;                          // Управление 0-10 В
                bit1 = firstStAddHeatCombo.SelectedIndex == 1;                          // Управление ШИМ 5В
                bit2 = elHeatAddStagesCombo.SelectedIndex > 6;                          // Ступень 8
                bit3 = elHeatAddStagesCombo.SelectedIndex > 5;                          // Ступень 7
                bit4 = elHeatAddStagesCombo.SelectedIndex > 4;                          // Ступень 6
                bit5 = elHeatAddStagesCombo.SelectedIndex > 3;                          // Ступень 5
                bit6 = elHeatAddStagesCombo.SelectedIndex > 2;                          // Ступень 4
                bit7 = elHeatAddStagesCombo.SelectedIndex > 1;                          // Ступень 3
                bit8 = elHeatAddStagesCombo.SelectedIndex > 0;                          // Ступень 2
                bit9 = true;                                                            // Ступень 1 по умолчанию
                bit10 = fireAddThermCheck.Checked;                                      // Термовыключатель, пожар
                bit11 = overheatAddThermCheck.Checked;                                  // Термовыключатель, перегрев
            }
            else
                bit0 = bit1 = bit2 = bit3 = bit4 = bit5 = bit6 = bit7 = bit8 = bit9 = bit10 = bit11 = false;

            cmdW14 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8) +
                512 * Convert.ToUInt16(bit9) + 1024 * Convert.ToUInt16(bit10) + 2048 * Convert.ToUInt16(bit11));
        }

        /// <summary>Командное слово основного увлажнителя</summary>
        private void CommandWord_15()
        {
            bool bit0, bit1, bit2, bit3;

            if (humidCheck.Checked)                                                     // Выбран увлажнитель
            { 
                bit0 = true;                                                            // Наличие увлажнителя
                // Наличие насоса сотового или пуск/стоп парового
                bit1 = (humidTypeCombo.SelectedIndex == 0 && startHumidCheck.Checked) ||
                    (humidTypeCombo.SelectedIndex == 1 && powPumpHumidCheck.Checked);
                bit2 = humidTypeCombo.SelectedIndex == 0 && analogHumCheck.Checked;     // 0-10 В
                bit3 = humidTypeCombo.SelectedIndex == 0 && alarmHumidCheck.Checked;    // Аварийный сигнал
            }
            else                                                                        // Не выбран увлажнитель
                bit0 = bit1 = bit2 = bit3 = false;

            cmdW15 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3));
        }

        /// <summary>Командное слово резерв 1</summary>
        private void CommandWord_16() => cmdW16 = 0;

        /// <summary>Командное слово резерв 2</summary>
        private void CommandWord_17() => cmdW17 = 0;

        /// <summary>Командное слово дополнительной вытяжки</summary>
        private void CommandWord_18() => cmdW18 = 0;

        /// <summary>Командное слово основного приточного вентилятора</summary>
        private void CommandWord_19()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9, bit10, bit11, bit12, bit13, bit14, bit15;

            // Признак выбранного сигнала Modbus (выбран ПЧ и тип управления Modbus)
            bool modbus_selected = prFanFC_ECcombo.SelectedIndex == 1 && prFanControlCombo.SelectedIndex == 1;

            bit0 = true;                                // Наличие вентилятора
            bit1 = prFanPSCheck.Checked;                // Подтверждение работы вентилятора
            bit2 = prFanAlarmCheck.Checked;             // Внешний сигнал аварии
            bit3 = prFanThermoCheck.Checked;            // Наличие термоконтактов
            bit4 = curDefPrFanCheck.Checked;            // Защита по току
            bit5 = prDampFanCheck.Checked;              // Наличие заслонки вентилятора
            bit6 = prDampConfirmFanCheck.Checked;       // Наличие подтверждения заслонки вентилятора
            bit7 = prFanStStopCheck.Checked ||          // Наличие сухого контакта - запуск ПЧ (либо Modbus)
                modbus_selected;             
            bit8 = false;                               // Наличие контактора, сигнал подачи питания
            bit9 = modbus_selected &&                   // Наличие Modbus и ПЧ STV600
                prFanFcTypeCombo.SelectedIndex == 1;                               
            bit10 = false;                              // Наличие ATV212
            bit11 = prFanSpeedCheck.Checked ||          // Управление 0-10 В (либо Modbus)
                modbus_selected;            
            bit12 = false;                              // Позисторные термоконтакты
            bit13 = false;                              // Поддержание давления по аналоговому датчику
            bit14 = false;
            bit15 = modbus_selected &&                  // Наличие Modbus и ПЧ Veda (или Ридан, Овен)
                prFanFcTypeCombo.SelectedIndex != 1;

            cmdW19 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8) +
                512 * Convert.ToUInt16(bit9) + 1024 * Convert.ToUInt16(bit10) + 2048 * Convert.ToUInt16(bit11) +
                4096 * Convert.ToUInt16(bit12) + 8192 * Convert.ToUInt16(bit13) + 16384 * Convert.ToUInt16(bit14) +
                32768 * Convert.ToUInt16(bit15));
        }

        /// <summary>Командное слово резервного приточного вентилятора</summary>
        private void CommandWord_20()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9, bit10, bit11, bit12, bit13, bit14, bit15;

            // Признак выбранного сигнала Modbus (выбран ПЧ и тип управления Modbus)
            bool modbus_selected = prFanFC_ECcombo.SelectedIndex == 1 && prFanControlCombo.SelectedIndex == 1;

            if (checkResPrFan.Checked)                          // Выбран резервный двигатель
            { 
                bit0 = true;                                    // Наличие вентилятора
                bit1 = prFanPSCheck.Checked;                    // Подтверждение работы вентилятора
                bit2 = prFanAlarmCheck.Checked;                 // Внешний сигнал аварии
                bit3 = prFanThermoCheck.Checked;                // Наличие термоконтактов
                bit4 = curDefPrFanCheck.Checked;                // Защита по току
                bit5 = prDampFanCheck.Checked;                  // Наличие заслонки
                bit6 = prDampConfirmFanCheck.Checked;           // Наличие подтверждения заслонки
                bit7 = prFanStStopCheck.Checked ||              // Наличие сухого контакта - запуск ПЧ (либо Modbus)
                    modbus_selected;                                  
                bit8 = false;                                   // Наличие контактора, сигнал подачи питания
                bit9 = modbus_selected &&                       // Наличие Modbus и ПЧ STV600
                    prFanFcTypeCombo.SelectedIndex == 1;        
                bit10 = false;                                  // Наличие ATV212
                bit11 = prFanSpeedCheck.Checked ||              // Управление 0-10 В (либо Modbus)
                    modbus_selected;                
                bit12 = false;                                  // Позисторные термоконтакты
                bit13 = false;                                  // Поддержание давления по аналоговому датчику
                bit14 = false;
                bit15 = modbus_selected &&                      // Наличие Modbus и ПЧ Veda (или Ридан, Овен)
                    prFanFcTypeCombo.SelectedIndex != 1;
            }
            else                                                // Не выбран резервный двигатель
                bit0 = bit1 = bit2 = bit3 = bit4 = bit5 = bit6 = bit7 = bit8 = bit9 = bit10 =
                    bit11 = bit12 = bit13 = bit14 = bit15 = false;

            cmdW20 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8) +
                512 * Convert.ToUInt16(bit9) + 1024 * Convert.ToUInt16(bit10) + 2048 * Convert.ToUInt16(bit11) +
                4096 * Convert.ToUInt16(bit12) + 8192 * Convert.ToUInt16(bit13) + 16384 * Convert.ToUInt16(bit14) +
                32768 * Convert.ToUInt16(bit15));
        }

        /// <summary>Командное слово основного вытяжного вентилятора</summary>
        private void CommandWord_21()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9, bit10, bit11, bit12, bit13, bit14, bit15;

            bool modbus_selected = prFanFC_ECcombo.SelectedIndex == 1 && outFanControlCombo.SelectedIndex == 1;

            // Выбрана ПВ-система и В вентилятор
            if (comboSysType.SelectedIndex == 1 && outFanCheck.Checked)                
            { 
                bit0 = true;                                    // Наличие вентилятора
                bit1 = outFanPSCheck.Checked;                   // Подтверждение работы вентилятора
                bit2 = outFanAlarmCheck.Checked;                // Внешний сигнал аварии
                bit3 = outFanThermoCheck.Checked;               // Наличие термоконтактов
                bit4 = curDefOutFanCheck.Checked;               // Защита по току
                bit5 = outDampFanCheck.Checked;                 // Наличие заслонки вентилятора
                bit6 = outDampConfirmFanCheck.Checked;          // Наличие подтверждения заслонки вентилятора
                bit7 = outFanStStopCheck.Checked ||             // Наличие сухого контакта - запуск ПЧ (либо Modbus)
                    modbus_selected;                                  
                bit8 = false;                                   // Наличие контактора, сигнал подачи питания
                bit9 = modbus_selected &&                       // Наличие Modbus и ПЧ STV600
                    outFanFcTypeCombo.SelectedIndex == 1;                                   
                bit10 = false;                                  // Наличие ATV212
                bit11 = outFanSpeedCheck.Checked ||             // Управление 0-10 В (либо Modbus)
                    modbus_selected;               
                bit12 = false;                                  // Позисторные термоконтакты
                bit13 = false;                                  // Поддержание давления по аналоговому датчику
                bit14 = false;
                bit15 = modbus_selected &&                      // Наличие Modbus и ПЧ Veda (или Ридан, Овен)
                    outFanFcTypeCombo.SelectedIndex != 1;
            }
            else                                                // Не выбрана ПВ-система
                bit0 = bit1 = bit2 = bit3 = bit4 = bit5 = bit6 = bit7 = bit8 = bit9 = bit10 =
                    bit11 = bit12 = bit13 = bit14 = bit15 = false;

            cmdW21 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8) +
                512 * Convert.ToUInt16(bit9) + 1024 * Convert.ToUInt16(bit10) + 2048 * Convert.ToUInt16(bit11) +
                4096 * Convert.ToUInt16(bit12) + 8192 * Convert.ToUInt16(bit13) + 16384 * Convert.ToUInt16(bit14) +
                32768 * Convert.ToUInt16(bit15));
        }

        /// <summary>Командное слово резервного вытяжного вентилятора</summary>
        private void CommandWord_22()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9, bit10, bit11, bit12, bit13, bit14, bit15;

            bool modbus_selected = prFanFC_ECcombo.SelectedIndex == 1 && outFanControlCombo.SelectedIndex == 1;

            // Выбрана ПВ-система, выбран В вентилятор и резерв В вентилятора
            if (comboSysType.SelectedIndex == 1 && outFanCheck.Checked && checkResOutFan.Checked)      
            { 
                bit0 = true;                                                    // Наличие вентилятора
                bit1 = outFanPSCheck.Checked;                                   // Подтверждение работы вентилятора
                bit2 = outFanAlarmCheck.Checked;                                // Внешний сигнал аварии
                bit3 = outFanThermoCheck.Checked;                               // Наличие термоконтактов
                bit4 = curDefOutFanCheck.Checked;                               // Защита по току
                bit5 = outDampFanCheck.Checked;                                 // Наличие заслонки вентилятора
                bit6 = outDampConfirmFanCheck.Checked;                          // Наличие подтверждения заслонки вентилятора
                bit7 = outFanStStopCheck.Checked ||                             // Наличие сухого контакта - запуск ПЧ (либо Modbus)
                    modbus_selected;                                                   
                bit8 = false;                                                   // Наличие контактора, сигнал подачи питания
                bit9 = modbus_selected &&                                       // Наличие Modbus и ПЧ STV600
                     outFanFcTypeCombo.SelectedIndex == 1;
                bit10 = false;                                                  // Наличие ATV212
                bit11 = outFanSpeedCheck.Checked ||                             // Управление 0-10 В (либо Modbus)
                    modbus_selected;                               
                bit12 = false;                                                  // Позисторные термоконтакты
                bit13 = false;                                                  // Поддержание давления по аналоговому датчику
                bit14 = false;
                bit15 = modbus_selected &&                                      // Наличие Modbus и ПЧ Veda (или Ридан, Овен)
                    outFanFcTypeCombo.SelectedIndex != 1;

            } else                                                              // Не выбрана ПВ-система или резерв вытяжного вентилятора
                bit0 = bit1 = bit2 = bit3 = bit4 = bit5 = bit6 = bit7 = bit8 = bit9 = bit10 =
                    bit11 = bit12 = bit13 = bit14 = bit15 = false;

            cmdW22 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8) +
                512 * Convert.ToUInt16(bit9) + 1024 * Convert.ToUInt16(bit10) + 2048 * Convert.ToUInt16(bit11) +
                4096 * Convert.ToUInt16(bit12) + 8192 * Convert.ToUInt16(bit13) + 16384 * Convert.ToUInt16(bit14) +
                32768 * Convert.ToUInt16(bit15));
        }

        /// <summary>Командное слово водяного преднагревателя</summary>
        private void CommandWord_23() => cmdW23 = 0;

        /// <summary>Командное слово электрического преднагревателя</summary>
        private void CommandWord_24() => cmdW24 = 0;

        /// <summary>Командное слово резерв 3</summary>
        private void CommandWord_25() => cmdW25 = 0;

        /// <summary>Командное слово резерв 4</summary>
        private void CommandWord_26() => cmdW26 = 0;

        /// <summary>Командное слово резерв 5</summary>
        private void CommandWord_27() => cmdW27 = 0;

        /// <summary>Командное слово резерв 6</summary>
        private void CommandWord_28() => cmdW28 = 0;

        /// <summary>Командное слово резерв 7</summary>
        private void CommandWord_29() => cmdW29 = 0;

        /// <summary>Командное слово резерв 8</summary>
        private void CommandWord_30() => cmdW30 = 0;

        ///<summary>Командное слово для пожарной сигнализации</summary>
        private void CommandWord_fire()
        {
            if (!fireCheck.Checked) cmdW_fire = 0;                                              // Нет сигнала
            else if (fireCheck.Checked && fireTypeCombo.SelectedIndex == 0) cmdW_fire = 0;      // Сигнал + НО
            else if (fireCheck.Checked && fireTypeCombo.SelectedIndex == 1) cmdW_fire = 1;      // Сигнал + НЗ
        }

        ///<summary>Выбрали заслонку</summary>
        private void DampCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_2(); CommandWord_3();
        }

        ///<summary>Выбрали подтверждение открытия приточной заслонки</summary>
        private void ConfPrDampCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_2();
            if (ignoreEvents) return;
            ConfPrDampCheck_signalsDICheckedChanged(this, e);                                   // Сигналы DI ПЛК
        }

        ///<summary>Выбрали обогрев приточной заслонки</summary>
        private void HeatPrDampCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_2();
            if (ignoreEvents) return;
            HeatPrDampCheck_signalsCheckedChanged(this, e);                                     // Сигналы ПЛК
        }

        ///<summary>Выбрали вытяжную заслонку</summary>
        private void OutDampCheck_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_3();

        ///<summary>Выбрали подтверждение открытия вытяжной заслонки</summary>
        private void ConfOutDampCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_3();
            if (ignoreEvents) return;
            ConfOutDampCheck_signalsDICheckedChanged(this, e);                                  // Сигналы DI ПЛК
        }

        ///<summary>Выбрали обогрев вытяжной заслонки</summary>
        private void HeatOutDampCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_3();
            if (ignoreEvents) return;
            HeatOutDampCheck_signalsCheckedChanged(this, e);                                    // Сигналы ПЛК
        }

        ///<summary>Выбрали фильтр</summary>
        private void FilterCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_4();
        }

        ///<summary>Изменили количество приточных фильтров</summary>
        private void FilterPrCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_4();
            if (ignoreEvents) return;
            FilterPrCombo_signalsDISelectedIndexChanged(this, e);                               // Сигналы DI
        }

        ///<summary>Изменили количество вытяжных фильтров</summary>
        private void FilterOutCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_4();
            if (ignoreEvents) return;
            FilterOutCombo_signalsDISelectedIndexChanged(this, e);                              // Сигналы DI
        }

        ///<summary>Выбрали сигнал пожарной сигнализации</summary>
        private void FireCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            if (fireCheck.Checked) fireTypeCombo.Enabled = true;
            else fireTypeCombo.Enabled = false;
            CommandWord_fire();                                                                 // Пересчет командного слова для ПС
            if (ignoreEvents) return;
            FireCheck_signalsDISelectedIndexChanged(this, e);                                   // Сигналы DI
        }

        ///<summary>Изменили тип пожарной сигнализации</summary>
        private void FireTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_fire();                                                                 // Пересчет командного слова для ПС
        }

        ///<summary>Выбрали приточный канальный датчик температуры</summary>
        private void PrChanSensCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_6();
            if (ignoreEvents) return;
            PrChanSensCheck_signalsAICheckedChanged(this, e);                                   // Сигналы AI
        }

        ///<summary>Выбрали датчик температуры наружного воздуха</summary>
        private void OutdoorChanSensCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_6();
            if (ignoreEvents) return;
            OutdoorChanSensCheck_signalsAICheckedChanged(this, e);                              // Сигналы AI
        }

        ///<summary>Выбрали комнатный датчик температуры</summary>
        private void RoomTempSensCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_6();
            if (ignoreEvents) return;
            RoomTempSensCheck_signalsAICheckedChanged(this, e);                                 // Сигналы AI
        }

        ///<summary>Выбрали рекуператор</summary>
        private void RecupCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_6(); CommandWord_9();
        }

        ///<summary>Выбрали температурный датчик защиты рекуператора</summary>
        private void RecDefTempCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_6(); CommandWord_9();
            if (ignoreEvents) return;
            RecDefTempCheck_signalsAICheckedChanged(this, e);                                   // Сигналы AI
        }

        ///<summary>Выбрали наличие насоса гликолевого рекуператора</summary>
        private void PumpGlicRecCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_7();
            if (ignoreEvents) return;
            PumpGlicRecCheck_signalsDOCheckedChanged(this, e);                                  // Сигналы DO ПЛК
        }

        ///<summary>Выбрали наличие резервного насоса гликолевого рекуператора</summary>
        private void ReservPumpGlik_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_7();
            if (ignoreEvents) return;
            ReservPumpGlik_signalsDOCheckedChanged(this, e);                                    // Сигналы DO ПЛК
        }

        ///<summary>Выбрали подтверждение работы насоса гликолевого рекуператора</summary>
        private void PumpGlikConfCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_7();
            if (ignoreEvents) return;
            PumpGlikConfCheck_signalsDICheckedChanged(this, e);                                 // Сигналы DI
        }

        ///<summary>Выбрали подтверждение работы резервного насоса гликолевого рекуператора</summary>
        private void ConfGlikResPumpCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_7();
            if (ignoreEvents) return;
            ConfGlikResPumpCheck_signalsCheckedChanged(this, e);                                // Сигналы DI
        }

        ///<summary>Выбрали защиту по току насоса гликолевого рекуператора</summary>
        private void PumpGlikCurProtect_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_7();
            if (ignoreEvents) return;
            PumpGlikCurProtect_signalsDICheckedChanged(this, e);                                // Сигналы DI
        }

        ///<summary>Выбрали защиту по току резервного насоса гликолевого рекуператора</summary>
        private void PumpGlikResCurProtect_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_7();
            if (ignoreEvents) return;
            PumpGlikResCurProtect_signalsDICheckedChanged(this, e);                             // Сигналы DI
        }

        ///<summary>Выбрали канальный датчик влажности</summary>
        private void ChanHumSensCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_6();
            if (ignoreEvents) return;
            ChanHumSensCheck_signalsAICheckedChanged(this, e);                                  // Сигналы AI
        }

        ///<summary>Выбрали комнатный датчик влажности</summary>
        private void RoomHumSensCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_6();
            if (ignoreEvents) return;
            RoomHumSensCheck_signalsAICheckedChanged(this, e);                                  // Сигналы AI
        }

        ///<summary>Выбрали канальный датчик Т вытяжного воздуха</summary>
        private void OutChanSensCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_6();
            if (ignoreEvents) return;
            OutChanSensCheck_signalsAICheckedChanged(this, e);                                  // Сигналы AI
        }

        ///<summary>Выбрали рециркуляцию</summary>
        private void RecircCheck_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_8();

        ///<summary>Выбрали сигнал 0-10 В для рециркуляции</summary>
        private void RecircAOSigCheck_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_8();

        ///<summary>Изменили тип рекуператора</summary>
        private void RecupTypeCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
            => CommandWord_9();

        ///<summary>Выбрали сигнал аварии роторного рекуператора</summary>
        private void OutSigAlarmRotRecCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_9();
            if (ignoreEvents) return;
            OutSigAlarmRotRecCheck_signalsDICheckedChanged(this, e);                            // Сигналы DI
        }
        
        ///<summary>Выбрали сигнал ПУСК/СТОП роторного рекуператора</summary>
        private void StartRotRecCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_9();
            if (ignoreEvents) return;
            StartRotRecCheck_signalsDOCheckedChanged(this, e);                                  // Сигналы DO
        }

        ///<summary>Выбрали сигнал 0-10 В роторного рекуператора</summary>
        private void AnalogRotRecCheck_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_9();
        
        ///<summary>Изменили тип управления пластинчатого рекуператора</summary>
        private void AnalogGlikRecCheck_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_9();

        ///<summary>Изменили тип управления пластинчатого рекуператора</summary>
        private void BypassPlastCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            // Изменение изображения при выборе байпасса пластинчатого рекуператора
            if (recupTypeCombo.SelectedIndex == 1 && bypassPlastCombo.SelectedIndex == 0)
                recupPicture.Image = Properties.Resources.plastRecup;
            else if (recupTypeCombo.SelectedIndex == 1)
                recupPicture.Image = Properties.Resources.plastRecupBypass;

            CommandWord_9();
            if (ignoreEvents) return;
            BypassPlastCombo_signalsAOSelectedIndexChanged(this, e);                            // Сигналы AO ПЛК
        }

        ///<summary>Выбрали сигнал PS рекуператора</summary>
        private void RecDefPsCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_9();
            if (ignoreEvents) return;
            RecDefPsCheck_signalsDICheckedChanged(this, e);                                     // Сигналы DI
        }

        ///<summary>Выбрали основной калорифер</summary>
        private void HeaterCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_6(); CommandWord_10(); CommandWord_11();
        }

        ///<summary>Изменили тип основного калорифера</summary>
        private void HeatTypeCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_6(); CommandWord_10(); CommandWord_11();
        }

        ///<summary>Выбрали резервный насос основного калорифера</summary>
        private void ReservPumpHeater_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_11();
        

        ///<summary>Изменили количество ступеней основного электрокалорифера</summary>
        private void ElHeatStagesCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_10();
            if (ignoreEvents) return;
            ElHeatStagesCombo_signalsDOSelectedIndexChanged(this, e);                           // Сигналы ПЛК
        }

        ///<summary>Изменили тип управления первой ступенью нагревателя</summary>
        private void FirstStHeatCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_10();
            if (ignoreEvents) return;
            FirstStHeatCombo_SignalsAOSelectedIndexChanged(this, e);                            // Сигналы AO ПЛК
        }

        ///<summary>Выбрали термовыключатель перегрева основного нагревателя</summary>
        private void OverheatThermCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_10();
            if (ignoreEvents) return;
            OverheatThermCheck_signalsDICheckedChanged(this, e);                                // Сигналы DI ПЛК
        }

        ///<summary>Выбрали термовыключатель пожара основного нагревателя</summary>
        private void FireThermCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_10();
            if (ignoreEvents) return;
            FireThermCheck_signalsDICheckedChanged(this, e);                                    // Сигналы DI ПЛК
        }

        ///<summary>Подтверждение работы основного насоса водяного калорифера</summary>
        private void ConfHeatPumpCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_11();
            if (ignoreEvents) return;
            ConfHeatPumpCheck_signalsDICheckedChanged(this, e);                                 // Сигналы DI  
        }

        ///<summary>Подтверждение работы резервного насоса водяного калорифера</summary>
        private void ConfHeatResPumpCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_11();
            if (ignoreEvents) return;
            ConfHeatResPumpCheck_signalsDICheckedChanged(this, e); // Сигналы DI
        }

        ///<summary>Выбрали термостат защиты от замерзания калорифера</summary>
        private void TF_heaterCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_11();
            if (ignoreEvents) return;
            TF_heaterCheck_signalsDICheckedChanged(this, e);                                    // Сигналы DI
        }

        ///<summary>Выбрали защиту по току для основного насоса калорифера</summary>
        private void PumpCurProtect_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_11();
            if (ignoreEvents) return;
            PumpCurProtect_signalsDICheckedChanged(this, e);                                    // Сигналы DI
        }

        ///<summary>Выбрали защиту по току для резервного насоса калорифера</summary>
        private void PumpCurResProtect_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_11();
            if (ignoreEvents) return;
            PumpCurResProtect_signalsDICheckedChanged(this, e);                                 // Сигналы DI
        }

        ///<summary>Выбрали догреватель</summary>
        private void AddHeatCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_6(); CommandWord_12(); CommandWord_13(); CommandWord_14();
        }

        ///<summary>Изменили тип догревателя</summary>
        private void HeatAddTypeCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_6(); CommandWord_13(); CommandWord_14();
        }

        ///<summary>Выбрали резервный насос догревателя</summary>
        private void ReservPumpAddHeater_cmdCheckedChanged(object sender, EventArgs e) 
            => CommandWord_13();

        ///<summary>Выбрали защиту по току для основного насоса догревателя</summary>
        private void PumpCurAddProtect_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_13();
            if (ignoreEvents) return;
            PumpCurAddProtect_signalsDICheckedChanged(this, e);                                 // Сигналы DI
        }

        ///<summary>Выбрали защиту по току для резервного насоса догревателя</summary>
        private void PumpCurResAddProtect_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_13();
            if (ignoreEvents) return;
            PumpCurResAddProtect_signalsDICheckedChanged(this, e);                              // Сигналы DI
        }

        ///<summary>Изменили тип охладителя</summary>
        private void CoolerCheck_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_12();

        ///<summary>Изменили тип охладителя</summary>
        private void CoolTypeCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
            => CommandWord_12();
        
        ///<summary>Изменили количество ступеней фреонового охладителя</summary>
        private void FrCoolStagesCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_12();
            if (ignoreEvents) return;
            FrCoolStagesCombo_signalsDOSelectedIndexChanged(this, e);                           // Сигналы DO
        }

        ///<summary>Выбрали аварийный сигнал для фреонового охладителя</summary>
        private void AlarmFrCoolCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_12();
            if (ignoreEvents) return;
            AlarmFrCoolCheck_signalsDICheckedChanged(this, e);                                  // Сигналы DI
        }

        ///<summary>Выбрали сигнал 0-10 В для фреонового охладителя</summary>
        private void AnalogFreonCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_12();
            if (ignoreEvents) return;
            AnalogFreonCheck_signalsAOCheckedChanged(this, e);                                  // Сигналы AO
        }

        private void ThermoCoolerCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_12();
            if (ignoreEvents) return;
            ThermoCoolerCheck_signalsDICheckedChanged(this, e);                                 // Сигналы DI
        }
        
        ///<summary>Выбрали режим осушителя</summary>
        private void DehumModeCheck_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_12();

        ///<summary>Выбрали сигнал 0-10 В для охладителя</summary>
        private void AnalogCoolCheck_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_12();

        ///<summary>Выбрали насос догревателя</summary>
        private void PumpAddHeatCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_13();
            if (ignoreEvents) return;
            PumpAddHeatCheck_signalsDOCheckedChanged(this, e);                                  // Сигналы DO
        }

        ///<summary>Подтверждение работы основного насоса водяного догревателя</summary>
        private void ConfAddHeatPumpCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_13();
            if (ignoreEvents) return;
            ConfAddHeatPumpCheck_signalsDICheckedChanged(this, e);                              // Сигналы DI
        }

        ///<summary>Подтверждение работы резервного насоса водяного догревателя</summary>
        private void ConfAddHeatResPumpCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_13();
            if (ignoreEvents) return;
            ConfAddHeatResPumpCheck_signalsDICheckedChanged(this, e);                           // Сигналы DI
        }

        ///<summary>Выбрали термостат догревателя</summary>
        private void TF_addHeaterCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_13();
            if (ignoreEvents) return;
            TF_addHeaterCheck_signalsDICheckedChanged(this, e);                                 // Сигналы DI
        }

        ///<summary>Изменили количество ступеней электрического догревателя</summary>
        private void ElHeatAddStagesCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_14();
            if (ignoreEvents) return;
            ElHeatAddStagesCombo_signalsSelectedIndexChanged(this, e);                          // Сигналы ПЛК
        }

        ///<summary>Изменили тип управления первой ступенью догревателя</summary>
        private void FirstStAddHeatCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_14();
            if (ignoreEvents) return;
            FirstStAddHeatCombo_signalsAOSelectedIndexChanged(this, e);
        }

        ///<summary>Выбрали термовыключатель перегрева догревателя</summary>
        private void OverheatAddThermCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_14();
            if (ignoreEvents) return;
            OverheatAddThermCheck_signalsDICheckedChanged(this, e);                             // Сигналы DI
        }

        ///<summary>Выбрали термовыключатель перегрева догревателя</summary>
        private void FireAddThermCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_14();
            if (ignoreEvents) return;
            FireAddThermCheck_signalsDICheckedChanged(this, e);                                 // Сигналы DI
        }

        ///<summary>Выбрали увлажнитель</summary>
        private void HumidCheck_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_15();

        ///<summary>Изменили тип увлажнителя</summary>
        private void HumidTypeCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
            => CommandWord_15();

        ///<summary>Выбрали сигнал пуск/стоп увлажнителя</summary>
        private void StartHumidCheck_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_15();

        ///<summary>Выбрали управляющий сигнал 0-10 В увлажнителя</summary>
        private void AnalogHumCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_15();
            if (ignoreEvents) return;
            AnalogHumCheck_signalsAOCheckedChanged(this, e);                                    // Сигналы AO
        }

        ///<summary>Выбрали сигнал аварии парового увлажнителя</summary>
        private void AlarmHumidCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_15();
            if (ignoreEvents) return;
            AlarmHumidCheck_signalsDICheckedChanged(this, e);                                   // Сигналы DI
        }

        ///<summary>Выбрали подачу питания на насос увлажнителя</summary>
        private void PowPumpHumidCheck_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_15();

        ///<summary>Выбрали PS приточного вентилятора</summary>
        private void PrFanPSCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_19(); CommandWord_20();
            if (ignoreEvents) return;
            PrFanPSCheck_signalsDICheckedChanged(this, e);                                      // Сигналы DI
        }

        ///<summary>Выбрали заслонку для приточного вентилятора</summary>
        private void PrDampFanCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_19(); CommandWord_20();
        }

        ///<summary>Выбрали подтверждение открытия заслонки приточного вентилятора</summary>
        private void PrDampConfirmFanCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_19(); CommandWord_20();
        }
 
        ///<summary>Выбрали Нет/ПЧ/ЕС двигатель приточного вентилятора</summary>
        private void PrFanFC_ECcombo_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_19(); CommandWord_20();
        }

        ///<summary>Изменили тип ПЧ приточного вентилятора</summary>
        private void PrFanFcTypeCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_19(); CommandWord_20();
        }

        ///<summary>Выбрали управление скоростью для приточного вентилятора</summary>
        private void PrFanSpeedCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_19(); CommandWord_20();
            if (ignoreEvents) return;
            PrFanSpeedCheck_signalsAOCheckedChanged(this, e);                                   // Сигналы AO
        }

        ///<summary>Выбрали выход аварии с ПЧ для приточного вентилятора</summary>
        private void PrFanAlarmCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_19(); CommandWord_20();
            if (ignoreEvents) return;
            PrFanAlarmCheck_signalsDICheckedChanged(this, e);                                   // Сигналы DI
        }

        ///<summary>Выбрали термоконтакты приточного вентилятора</summary>
        private void PrFanThermoCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_19(); CommandWord_20();
            if (ignoreEvents) return;
            PrFanThermoCheck_signalsDICheckedChanged(this, e);                                  // Сигналы DI
        }

        ///<summary>Выбрали защиту по току приточного вентилятора</summary>
        private void CurDefPrFanCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_19(); CommandWord_20();
            if (ignoreEvents) return;
            CurDefPrFanCheck_signalsDICheckedChanged(this, e);                                  // Сигналы DI
        }

        ///<summary>Выбрали резерв приточного вентилятора</summary>
        private void CheckResPrFan_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_20();

        ///<summary>Изменили тип системы</summary>
        private void ComboSysType_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_21(); CommandWord_22();
        }

        ///<summary>Выбрали PS вытяжного вентилятора</summary>
        private void OutFanPSCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_21(); CommandWord_22();
            if (ignoreEvents) return;
            OutFanPSCheck_signalsDICheckedChanged(this, e);                                     // Сигналы DI
        }

        ///<summary>Выбрали заслонку для вытяжного вентилятора</summary>
        private void OutDampFanCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_21(); CommandWord_22();
        }

        ///<summary>Выбрали подтверждение открытия заслонки вытяжного вентилятора</summary>
        private void OutDampConfirmFanCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_21(); CommandWord_22();
        }

        ///<summary>Выбрали ПЧ для вытяжного вентилятора</summary>
        private void OutFanFC_ECcombo_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_21(); CommandWord_22();
        }

        ///<summary>Выбрали ПЧ для вытяжного вентилятора</summary>
        private void OutFanFcTypeCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_21(); CommandWord_22();
        }

        ///<summary>Выбрали управление скоростью для вытяжного вентилятора</summary>
        private void OutFanSpeedCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_21(); CommandWord_22();
            if (ignoreEvents) return;
            OutFanSpeedCheck_signalsAOCheckedChanged(this, e);                                  // Сигналы AO
        }

        ///<summary>Выбрали выход аварии с ПЧ для вытяжного вентилятора</summary>
        private void OutFanAlarmCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_21(); CommandWord_22();
            if (ignoreEvents) return;
            OutFanAlarmCheck_CheckedChanged(this, e);                                           // Сигналы DI
        }

        ///<summary>Выбрали термоконтакты вытяжного вентилятора</summary>
        private void OutFanThermoCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_21(); CommandWord_22();
            if (ignoreEvents) return;
            OutFanThermoCheck_signalsDICheckedChanged(this, e);                                 // Сигналы DI
        }

        ///<summary>Выбрали защиту по току для вытяжного вентилятора</summary>
        private void CurDefOutFanCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_21(); CommandWord_22();
            if (ignoreEvents) return;
            CurDefOutFanCheck_signalsDICheckedChanged(this, e);                                 // Сигналы DI
        }

        ///<summary>Командное слово для вытяжного резерва</summary>
        private void CheckResOutFan_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_22();
    }
}