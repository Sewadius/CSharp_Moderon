using System;
using System.Windows.Forms;

// Файл для формирования и вывода готовых командных слов под запись

namespace Moderon
{
    public partial class Form1 : Form
    {
        ushort[] cmdWords = new ushort[30]; // Массив для командных слов
        ushort[] diSignals = new ushort[20]; // Массив для сигналов DI
        ushort[] aiSignals = new ushort[24]; // Массив для сигналов AI
        ushort[] doSignals = new ushort[28]; // Массив для сигналов DO
        ushort[] aoSignals = new ushort[12]; // Массив для сигналов AO

        ushort cmdW1, cmdW2, cmdW3, cmdW4, cmdW5, cmdW6, cmdW7, cmdW8, cmdW9, cmdW10,
            cmdW11, cmdW12, cmdW13, cmdW14, cmdW15, cmdW16, cmdW17, cmdW18, cmdW19, cmdW20,
            cmdW21, cmdW22, cmdW23, cmdW24, cmdW25, cmdW26, cmdW27, cmdW28, cmdW29, cmdW30,
            cmdW_fire;

        ///<summary>Предварительная очистка массивов перед формированием значений</summary>
        private void ResetSignalsAll()
        {
            for (int i = 0; i < diSignals.Length; i++) diSignals[i] = 0;
            for (int i = 0; i < aiSignals.Length; i++) aiSignals[i] = 0;
            for (int i = 0; i < doSignals.Length; i++) doSignals[i] = 0;
            for (int i = 0; i < aoSignals.Length; i++) aoSignals[i] = 0;
        }

        ///<summary>Нажали на кнопку "Собрать"</summary>
        private void FormNetButton_Click(object sender, EventArgs e)
        {
            ushort count = 1;                       // Глобальный счетчик учета позиции
            ResetSignalsAll();                      // Сброс для массивов сигналов
            BuildCmdWords();                        // Сборка массива командных слов
            BuildDiSignals();                       // Сборка массива для сигналов DI
            BuildAiSignals();                       // Сборка массива для сигналов AI
            BuildDoSignals();                       // Сборка массива для сигналов DO
            BuildAoSignals();                       // Сборка массива для сигналов AO
            writeNetTextBox.Text = "";              // Очистка текстового поля

            for (ushort counter = 1; counter <= cmdWords.Length; counter++) 
            { // Для командных слов
                writeNetTextBox.Text += counter.ToString() + ") " + 
                    cmdWords[counter - 1].ToString();
                if (counter < cmdWords.Length) writeNetTextBox.Text += System.Environment.NewLine;
                ++count;
            }
            for (ushort counter = 1; counter <= diSignals.Length; counter++) // DI
            { // Для сигналов DI (DI1 - DI20) - по 5 сигналов
                writeNetTextBox.Text += System.Environment.NewLine;
                writeNetTextBox.Text += (count.ToString() + "_DI_" + counter.ToString() + ") " +
                    diSignals[counter - 1]).ToString();
                ++count;
            }
            for (ushort counter = 1; counter <= aiSignals.Length; counter++) // AI
            { // Для сигналов AI (AI1 - AI24) - по 6 сигналов
                writeNetTextBox.Text += System.Environment.NewLine;
                writeNetTextBox.Text += (count.ToString() + "_AI_" + counter.ToString() + ") " +
                    aiSignals[counter - 1]).ToString();
                ++count;
            }
            for (ushort counter = 1; counter <= doSignals.Length; counter++) // DO
            { // Для сигналов DO (DO1 - DO28) - по 7 сигналов
                writeNetTextBox.Text += System.Environment.NewLine;
                writeNetTextBox.Text += (count.ToString() + "_DO_" + counter.ToString() + ") " +
                    doSignals[counter - 1]).ToString();
                ++count;
            }
            for (ushort counter = 1; counter <= aoSignals.Length; counter++) // AO
            { // Для сигналов AO (AO1 - AO12) - по 3 сигнала
                writeNetTextBox.Text += System.Environment.NewLine;
                writeNetTextBox.Text += (count.ToString() + "_AO_" + counter.ToString() + ") " +
                    aoSignals[counter - 1]).ToString();
                ++count;
            }
            // Для сигнала пожарной сигнализации
            writeNetTextBox.Text += System.Environment.NewLine;
            writeNetTextBox.Text += (count.ToString() + "_fire" + ") " + cmdW_fire.ToString());
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

        ///<summary>Сборка массива для сигналов DI</summary>
        private void BuildDiSignals()
        {
            // ПЛК
            if (DI1_combo.SelectedItem.ToString() != NOT_SELECTED) // DI1
                diSignals[0] = Convert.ToUInt16(DI1_lab.Text);
            if (DI2_combo.SelectedItem.ToString() != NOT_SELECTED) // DI2
                diSignals[1] = Convert.ToUInt16(DI2_lab.Text);
            if (DI3_combo.SelectedItem.ToString() != NOT_SELECTED) // DI3
                diSignals[2] = Convert.ToUInt16(DI3_lab.Text);
            if (DI4_combo.SelectedItem.ToString() != NOT_SELECTED) // DI4
                diSignals[3] = Convert.ToUInt16(DI4_lab.Text);
            if (DI5_combo.SelectedItem.ToString() != NOT_SELECTED) // DI5
                diSignals[4] = Convert.ToUInt16(DI5_lab.Text);
            // Блок расширения 1
            if (DI1bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // DI1
                diSignals[5] = Convert.ToUInt16(DI1bl1_lab.Text);
            if (DI2bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // DI2
                diSignals[6] = Convert.ToUInt16(DI2bl1_lab.Text);
            if (DI3bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // DI3
                diSignals[7] = Convert.ToUInt16(DI3bl1_lab.Text);
            if (DI4bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // DI4
                diSignals[8] = Convert.ToUInt16(DI4bl1_lab.Text);
            if (DI5bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // DI5
                diSignals[9] = Convert.ToUInt16(DI5bl1_lab.Text);
            // Блок расширения 2
            if (DI1bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // DI1
                diSignals[10] = Convert.ToUInt16(DI1bl2_lab.Text);
            if (DI2bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // DI2
                diSignals[11] = Convert.ToUInt16(DI2bl2_lab.Text);
            if (DI3bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // DI3
                diSignals[12] = Convert.ToUInt16(DI3bl2_lab.Text);
            if (DI4bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // DI4
                diSignals[13] = Convert.ToUInt16(DI4bl2_lab.Text);
            if (DI5bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // DI5
                diSignals[14] = Convert.ToUInt16(DI5bl2_lab.Text);
            // Блок расширения 3
            if (DI1bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // DI1
                diSignals[15] = Convert.ToUInt16(DI1bl3_lab.Text);
            if (DI2bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // DI2
                diSignals[16] = Convert.ToUInt16(DI2bl3_lab.Text);
            if (DI3bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // DI3
                diSignals[17] = Convert.ToUInt16(DI3bl3_lab.Text);
            if (DI4bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // DI4
                diSignals[18] = Convert.ToUInt16(DI4bl3_lab.Text);
            if (DI5bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // DI5
                diSignals[19] = Convert.ToUInt16(DI5bl3_lab.Text);
        }

        ///<summary>Сборка массива для сигналов AI</summary>
        private void BuildAiSignals()
        {
            // ПЛК
            if (AI1_combo.SelectedItem.ToString() != NOT_SELECTED) // AI1
                aiSignals[0] = Convert.ToUInt16(AI1_lab.Text);
            if (AI2_combo.SelectedItem.ToString() != NOT_SELECTED) // AI2
                aiSignals[1] = Convert.ToUInt16(AI2_lab.Text);
            if (AI3_combo.SelectedItem.ToString() != NOT_SELECTED) // AI3
                aiSignals[2] = Convert.ToUInt16(AI3_lab.Text);
            if (AI4_combo.SelectedItem.ToString() != NOT_SELECTED) // AI4
                aiSignals[3] = Convert.ToUInt16(AI4_lab.Text);
            if (AI5_combo.SelectedItem.ToString() != NOT_SELECTED) // AI5
                aiSignals[4] = Convert.ToUInt16(AI5_lab.Text);
            if (AI6_combo.SelectedItem.ToString() != NOT_SELECTED) // AI6
                aiSignals[5] = Convert.ToUInt16(AI6_lab.Text);
            // Блок расширения 1
            if (AI1bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // AI1
                aiSignals[6] = Convert.ToUInt16(AI1bl1_lab.Text);
            if (AI2bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // AI2
                aiSignals[7] = Convert.ToUInt16(AI2bl1_lab.Text);
            if (AI3bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // AI3
                aiSignals[8] = Convert.ToUInt16(AI3bl1_lab.Text);
            if (AI4bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // AI4
                aiSignals[9] = Convert.ToUInt16(AI4bl1_lab.Text);
            if (AI5bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // AI5
                aiSignals[10] = Convert.ToUInt16(AI5bl1_lab.Text);
            if (AI6bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // AI6
                aiSignals[11] = Convert.ToUInt16(AI6bl1_lab.Text);
            // Блок расширения 2
            if (AI1bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // AI1
                aiSignals[12] = Convert.ToUInt16(AI1bl2_lab.Text);
            if (AI2bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // AI2
                aiSignals[13] = Convert.ToUInt16(AI2bl2_lab.Text);
            if (AI3bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // AI3
                aiSignals[14] = Convert.ToUInt16(AI3bl2_lab.Text);
            if (AI4bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // AI4
                aiSignals[15] = Convert.ToUInt16(AI4bl2_lab.Text);
            if (AI5bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // AI5
                aiSignals[16] = Convert.ToUInt16(AI5bl2_lab.Text);
            if (AI6bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // AI6
                aiSignals[17] = Convert.ToUInt16(AI6bl2_lab.Text);
            // Блок расширения 3
            if (AI1bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // AI1
                aiSignals[18] = Convert.ToUInt16(AI1bl3_lab.Text);
            if (AI2bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // AI2
                aiSignals[19] = Convert.ToUInt16(AI2bl3_lab.Text);
            if (AI3bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // AI3
                aiSignals[20] = Convert.ToUInt16(AI3bl3_lab.Text);
            if (AI4bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // AI4
                aiSignals[21] = Convert.ToUInt16(AI4bl3_lab.Text);
            if (AI5bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // AI5
                aiSignals[22] = Convert.ToUInt16(AI5bl3_lab.Text);
            if (AI6bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // AI6
                aiSignals[23] = Convert.ToUInt16(AI6bl3_lab.Text);
        }

        ///<summary>Сборка массива для сигналов DO</summary>
        private void BuildDoSignals()
        {
            // ПЛК
            if (DO1_combo.SelectedItem.ToString() != NOT_SELECTED) // DO1
                doSignals[0] = Convert.ToUInt16(DO1_lab.Text);
            if (DO2_combo.SelectedItem.ToString() != NOT_SELECTED) // DO2
                doSignals[1] = Convert.ToUInt16(DO2_lab.Text);
            if (DO3_combo.SelectedItem.ToString() != NOT_SELECTED) // DO3
                doSignals[2] = Convert.ToUInt16(DO3_lab.Text);
            if (DO4_combo.SelectedItem.ToString() != NOT_SELECTED) // DO4
                doSignals[3] = Convert.ToUInt16(DO4_lab.Text);
            if (DO5_combo.SelectedItem.ToString() != NOT_SELECTED) // DO5
                doSignals[4] = Convert.ToUInt16(DO5_lab.Text);
            if (DO6_combo.SelectedItem.ToString() != NOT_SELECTED) // DO6
                doSignals[5] = Convert.ToUInt16(DO6_lab.Text);
            if (DO7_combo.SelectedItem.ToString() != NOT_SELECTED) // DO7
                doSignals[6] = Convert.ToUInt16(DO7_lab.Text);
            // Блок расширения 1
            if (DO1bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // DO1
                doSignals[7] = Convert.ToUInt16(DO1bl1_lab.Text);
            if (DO2bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // DO2
                doSignals[8] = Convert.ToUInt16(DO2bl1_lab.Text);
            if (DO3bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // DO3
                doSignals[9] = Convert.ToUInt16(DO3bl1_lab.Text);
            if (DO4bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // DO4
                doSignals[10] = Convert.ToUInt16(DO4bl1_lab.Text);
            if (DO5bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // DO5
                doSignals[11] = Convert.ToUInt16(DO5bl1_lab.Text);
            if (DO6bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // DO6
                doSignals[12] = Convert.ToUInt16(DO6bl1_lab.Text);
            if (DO7bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // DO7
                doSignals[13] = Convert.ToUInt16(DO7bl1_lab.Text);
            // Блок расширения 2
            if (DO1bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // DO1
                doSignals[14] = Convert.ToUInt16(DO1bl2_lab.Text);
            if (DO2bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // DO2
                doSignals[15] = Convert.ToUInt16(DO2bl2_lab.Text);
            if (DO3bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // DO3
                doSignals[16] = Convert.ToUInt16(DO3bl2_lab.Text);
            if (DO4bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // DO4
                doSignals[17] = Convert.ToUInt16(DO4bl2_lab.Text);
            if (DO5bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // DO5
                doSignals[18] = Convert.ToUInt16(DO5bl2_lab.Text);
            if (DO6bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // DO6
                doSignals[19] = Convert.ToUInt16(DO6bl2_lab.Text);
            if (DO7bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // DO7
                doSignals[20] = Convert.ToUInt16(DO7bl2_lab.Text);
            // Блок расширения 3
            if (DO1bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // DO1
                doSignals[21] = Convert.ToUInt16(DO1bl3_lab.Text);
            if (DO2bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // DO2
                doSignals[22] = Convert.ToUInt16(DO2bl3_lab.Text);
            if (DO3bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // DO3
                doSignals[23] = Convert.ToUInt16(DO3bl3_lab.Text);
            if (DO4bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // DO4
                doSignals[24] = Convert.ToUInt16(DO4bl3_lab.Text);
            if (DO5bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // DO5
                doSignals[25] = Convert.ToUInt16(DO5bl3_lab.Text);
            if (DO6bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // DO6
                doSignals[26] = Convert.ToUInt16(DO6bl3_lab.Text);
            if (DO7bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // DO7
                doSignals[27] = Convert.ToUInt16(DO7bl3_lab.Text);
        }

        ///<summary>Сборка массива для сигналов AO</summary>
        private void BuildAoSignals()
        {
            // ПЛК
            if (AO1_combo.SelectedItem.ToString() != NOT_SELECTED) // AO1
                aoSignals[0] = Convert.ToUInt16(AO1_lab.Text);
            if (AO2_combo.SelectedItem.ToString() != NOT_SELECTED) // AO2
                aoSignals[1] = Convert.ToUInt16(AO2_lab.Text);
            if (AO3_combo.SelectedItem.ToString() != NOT_SELECTED) // AO3
                aoSignals[2] = Convert.ToUInt16(AO3_lab.Text);
            // Блок расширения 1
            if (AO1bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // AO1
                aoSignals[3] = Convert.ToUInt16(AO1bl1_lab.Text);
            if (AO2bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // AO2
                aoSignals[4] = Convert.ToUInt16(AO2bl1_lab.Text);
            if (AO3bl1_combo.SelectedItem.ToString() != NOT_SELECTED) // AO3
                aoSignals[5] = Convert.ToUInt16(AO3bl1_lab.Text);
            // Блок расширения 2
            if (AO1bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // AO1
                aoSignals[6] = Convert.ToUInt16(AO1bl2_lab.Text);
            if (AO2bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // AO2
                aoSignals[7] = Convert.ToUInt16(AO2bl2_lab.Text);
            if (AO3bl2_combo.SelectedItem.ToString() != NOT_SELECTED) // AO3
                aoSignals[8] = Convert.ToUInt16(AO3bl2_lab.Text);
            // Блок расширения 3
            if (AO1bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // AO1
                aoSignals[9] = Convert.ToUInt16(AO1bl3_lab.Text);
            if (AO2bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // AO2
                aoSignals[10] = Convert.ToUInt16(AO2bl3_lab.Text);
            if (AO3bl3_combo.SelectedItem.ToString() != NOT_SELECTED) // AO3
                aoSignals[11] = Convert.ToUInt16(AO3bl3_lab.Text);
        }

        ///<summary>Инициализация командных слов</summary>
        private void Form1_InitCmdWord(object sender, EventArgs e)
        {
            CommandWord_1(); // Основной блок запуска
            CommandWord_2(); // Основная приточная заслонка
            CommandWord_3(); // Основная вытяжная заслонка
            CommandWord_4(); // Приточные фильтры
            CommandWord_5(); // Вытяжные фильтры
            CommandWord_6(); // Датчики
            CommandWord_7(); // Рекуперация, второе слово
            CommandWord_8(); // Рециркуляция
            CommandWord_9(); // Рекуператор
            CommandWord_10(); // Основной электрический нагреватель
            CommandWord_11(); // Основной водяной нагреватель
            CommandWord_12(); // Основной водяной охладитель
            CommandWord_13(); // Водяной догреватель
            CommandWord_14(); // Электрический догреватель
            CommandWord_15(); // Основной увлажнитель
            CommandWord_16(); // Резервное слово 1
            CommandWord_17(); // Резервное слово 2 
            CommandWord_18(); // Дополнительная вытяжка
            CommandWord_19(); // Основной приточный вентилятор
            CommandWord_20(); // Резервный приточный вентилятор
            CommandWord_21(); // Основной вытяжной вентилятор
            CommandWord_22(); // Резервный вытяжной вентилятор
            CommandWord_23(); // Водяной преднагреватель
            CommandWord_24(); // Электрический преднагреватель
            CommandWord_25(); // Резервное слово 3
            CommandWord_26(); // Резервное слово 4
            CommandWord_27(); // Резервное слово 5
            CommandWord_28(); // Резервное слово 6
            CommandWord_29(); // Резервное слово 7
            CommandWord_30(); // Резервное слово 8
            CommandWord_fire(); // Пожарная сигнализация
        }

        ///<summary>Формирование командного слова основного блока старт/стоп</summary>>
        private void CommandWord_1()
        {
            bool bit0, bit1, bit2, bit3, bit4;
            bit0 = stopStartCheck.Checked; // Переключатель пуск/стоп
            bit1 = bit2 = bit3 = bit4 = false; // Не используются
            cmdW1 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4));
        }

        ///<summary>Формирование командного слова приточной воздушной заслонки</summary>
        private void CommandWord_2()
        {
            bool bit0, bit1, bit2, bit3;
            bit0 = dampCheck.Checked; // Наличие приточной заслонки
            bit1 = dampCheck.Checked && confPrDampCheck.Checked; // Подтверждение открытия
            bit2 = false; // Не используется
            bit3 = dampCheck.Checked && heatPrDampCheck.Checked; // Обогрев заслонки
            cmdW2 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3));
        }

        /// <summary>Формирование командного слова вытяжной воздушной заслонки</summary>
        private void CommandWord_3()
        {
            bool bit0, bit1, bit2, bit3;
            bit0 = outDampCheck.Checked; // Наличие вытяжной заслонки
            bit1 = outDampCheck.Checked && confOutDampCheck.Checked; // Подтверждение открытия
            bit2 = false; // Не используется
            bit3 = outDampCheck.Checked && heatOutDampCheck.Checked; // Обогрев заслонки
            cmdW3 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3));
        }

        ///<summary>Формирование командного слова приточных фильтров</summary>
        private void CommandWord_4()
        {
            bool bit0, bit1, bit2;
            bit0 = filterCheck.Checked; // Воздушный фильтр 1
            bit1 = filterCheck.Checked && filterPrCombo.SelectedIndex > 0; // Воздушный фильтр 2
            bit2 = filterCheck.Checked && filterPrCombo.SelectedIndex > 1; // Воздушный фильтр 3
            cmdW4 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2));
        }

        /// <summary>Формирование командного слова вытяжных фильтров</summary>
        private void CommandWord_5()
        {
            bool bit0, bit1, bit2;
            bit0 = filterCheck.Checked && filterOutCombo.SelectedIndex > 0; // Воздушный фильтр 1
            bit1 = filterCheck.Checked && filterOutCombo.SelectedIndex > 1; // Воздушный фильтр 2
            bit2 = filterCheck.Checked && filterOutCombo.SelectedIndex > 2; // Воздушный фильтр 3
            cmdW5 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2));
        }

        /// <summary>Формирование командного слова датчиков</summary>
        private void CommandWord_6()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9, bit10;
            bit0 = prChanSensCheck.Checked; // Канальный датчик температуры, приток
            bit1 = false; // Канальный датчик приток 2
            bit2 = outdoorChanSensCheck.Checked; // Датчик наружного воздуха
            bit3 = heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0; // Датчик обратной воды
            bit4 = roomTempSensCheck.Checked; // Комнатный датчик температуры  
            bit5 = recupCheck.Checked && recDefTempCheck.Checked; // Датчик защиты рекуператора
            bit6 = chanHumSensCheck.Checked; // Канальный датчик влажности
            bit7 = roomHumSensCheck.Checked; // Комнатный датчик влажности
            bit8 = addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0; // Датчик обратной воды, догреватель
            bit9 = outChanSensCheck.Checked; // Вытяжной канальный датчик
            bit10 = false; // Датчик обратной воды гликолевого рекуператора
            cmdW6 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8) +
                512 * Convert.ToUInt16(bit9) + 1024 * Convert.ToUInt16(bit10));
        }

        /// <summary>Командное слово рекуперация, второе слово</summary>
        private void CommandWord_7()
        {
            bool bit0, bit1;
            bit0 = false; // Резервный насос гликолевого
            bit1 = false; // Наличие KPI резервного насоса
            cmdW7 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1));
        }

        /// <summary>Командное слово рециркуляция</summary>
        private void CommandWord_8()
        {
            bool bit0, bit1;
            bit0 = recircCheck.Checked; // Наличие рециркуляции
            bit1 = recircCheck.Checked && recircAOSigCheck.Checked; // Управление 0-10 В
            cmdW8 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1));
        }

        /// <summary>Командное слово рекуператора</summary>
        private void CommandWord_9()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9, bit10;
            if (recupCheck.Checked)
            { // Выбран рекуператор
                bit0 = recupTypeCombo.SelectedIndex == 1; // Пластинчатый рекуператор
                bit1 = recupTypeCombo.SelectedIndex == 0; // Роторный рекуператор
                bit2 = recupTypeCombo.SelectedIndex == 2; // Гликолевый рекуператор
                bit3 = (recupTypeCombo.SelectedIndex == 0 && analogRotRecCheck.Checked) ||
                    (recupTypeCombo.SelectedIndex == 1 && bypassPlastCombo.SelectedIndex == 2) ||
                        (recupTypeCombo.SelectedIndex == 2 && analogGlikRecCheck.Checked); // 0-10 В
                bit4 = false; // Контактор рекуператора
                bit5 = (recupTypeCombo.SelectedIndex == 0 && startRotRecCheck.Checked) ||
                    (recupTypeCombo.SelectedIndex == 1 && bypassPlastCombo.SelectedIndex == 1); // Откр/закр
                bit6 = false; // Наличие ATV212
                bit7 = recDefPsCheck.Checked; // Датчик PS рекуператора
                bit8 = recDefTempCheck.Checked; // Датчик температуры за рекуператором
                bit9 = false; // Реле давления насоса гликолевого
                bit10 = outSigAlarmRotRecCheck.Checked && recupTypeCombo.SelectedIndex == 0; // Внешний сигнал аварии роторного рекуператора
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
            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 1)
            { // Выбран электрический нагреватель
                bit0 = firstStHeatCombo.SelectedIndex == 2; // Управление 0-10 В
                bit1 = firstStHeatCombo.SelectedIndex == 1; // Управление ШИМ 5В
                bit2 = false; // Ступерь 8
                bit3 = false; // Ступень 7
                bit4 = false; // Ступень 6
                bit5 = elHeatStagesCombo.SelectedIndex > 3; // Ступень 5
                bit6 = elHeatStagesCombo.SelectedIndex > 2; // Ступень 4
                bit7 = elHeatStagesCombo.SelectedIndex > 1; // Ступень 3
                bit8 = elHeatStagesCombo.SelectedIndex > 0; // Ступень 2
                bit9 = true; // Ступень 1
                bit10 = thermSwitchCombo.SelectedIndex > 0; // Термовыключатель 1
                bit11 = thermSwitchCombo.SelectedIndex > 1; // Термовыключатель 2
            }
            else // Не выбран электрический нагреватель
                bit0 = bit1 = bit2 = bit3 = bit4 = bit5 = bit6 = bit7 = bit8 = bit9 = bit10 = bit11 = false;
            cmdW10 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8) +
                512 * Convert.ToUInt16(bit9) + 1024 * Convert.ToUInt16(bit10) + 2048 * Convert.ToUInt16(bit11));
        }

        /// <summary>Командное слово основного водяного нагревателя</summary>
        private void CommandWord_11()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6;
            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)
            { // Выбран водяной нагреватель
                bit0 = true; // Наличие водяного нагревателя
                bit1 = true; // Наличие основного насоса
                bit2 = confHeatPumpCheck.Checked; // Подтверждение работы насоса
                bit3 = false; // Наличие резервного насоса
                bit4 = false; // Подтверждение работы резервного насоса
                bit5 = TF_heaterCheck.Checked; // Воздушный термостат
                bit6 = false; // Контроль протечки
            }
            else
                bit0 = bit1 = bit2 = bit3 = bit4 = bit5 = bit6 = false;
            cmdW11 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6));
        }

        /// <summary>Командное слово основного охладителя</summary>
        private void CommandWord_12()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9;
            if (coolerCheck.Checked)
            { // Выбран охладитель
                if (coolTypeCombo.SelectedIndex == 0)
                { // Фреоновый охладитель
                    bit0 = false; // Наличие водяного охладителя
                    bit1 = true; // Наличие фреонового охладителя
                    bit2 = true; // Первая ступень
                    bit3 = frCoolStagesCombo.SelectedIndex > 0; // Вторая ступень
                    bit4 = frCoolStagesCombo.SelectedIndex > 1; // Третья ступень
                    bit5 = frCoolStagesCombo.SelectedIndex > 2; // Четвертая ступень
                    bit6 = analogFreonCheck.Checked; // Аналоговый сигнал охладителя
                    bit7 = alarmFrCoolCheck.Checked; // Аварийный сигнал, фреон
                    bit8 = thermoCoolerCheck.Checked; // Воздушный термостат
                    bit9 = dehumModeCheck.Checked && addHeatCheck.Checked; // Режим осушителя
                } 
                else // Водяной охладитель
                {
                    bit0 = true; // Наличие водяного охладителя
                    bit1 = false; // Наличие фреонового охладителя
                    bit2 = bit3 = bit4 = bit5 = false; // Ступени фреонового охладителя
                    bit6 = analogCoolCheck.Checked; // Аналоговый сигнал
                    bit7 = false; // Аварийный сигнал, фреон
                    bit8 = false; // Воздушный термостат, фреон
                    bit9 = false; // Режим осушителя, фреон
                }
            }
            else // Нет выбранного охладителя
                bit0 = bit1 = bit2 = bit3 = bit4 = bit5 = bit6 = bit7 = bit8 = bit9 = false;
            cmdW12 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8) +
                512 * Convert.ToUInt16(bit9));
        }

        /// <summary>Командное слово водяного догревателя</summary>
        private void CommandWord_13()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6;
            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)
            { // Выбран водяной догреватель
                bit0 = true; // Наличие водяного догревателя
                bit1 = pumpAddHeatCheck.Checked; // Наличие водяного насоса
                bit2 = pumpAddHeatCheck.Checked && confAddHeatPumpCheck.Checked; // Подтверждение работы насоса
                bit3 = false; // Наличие резервного насоса
                bit4 = false; // Подтверждение работы резервного насоса
                bit5 = TF_addHeaterCheck.Checked; // Термостат
                bit6 = false; // Контроль протечки
            }
            else // Не выбран водяной догреватель
                bit0 = bit1 = bit2 = bit3 = bit4 = bit5 = bit6 = false;
            cmdW13 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6));
        }

        /// <summary>Командное слово электрического догревателя</summary>
        private void CommandWord_14()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9, bit10, bit11;
            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 1)
            { // Выбран электрический догреватель
                bit0 = firstStAddHeatCombo.SelectedIndex == 2; // Управление 0-10 В
                bit1 = firstStAddHeatCombo.SelectedIndex == 1; // Управление ШИМ 5В
                bit2 = false; // Ступень 8
                bit3 = false; // Ступень 7
                bit4 = false; // Ступень 6
                bit5 = elHeatAddStagesCombo.SelectedIndex > 3; // Ступень 5
                bit6 = elHeatAddStagesCombo.SelectedIndex > 2; // Ступень 4
                bit7 = elHeatAddStagesCombo.SelectedIndex > 1; // Ступень 3
                bit8 = elHeatAddStagesCombo.SelectedIndex > 0; // Ступень 2
                bit9 = true; // Ступень 1
                bit10 = thermAddSwitchCombo.SelectedIndex > 0; // Термовыключатель 1
                bit11 = thermAddSwitchCombo.SelectedIndex > 1; // Термовыключатель 2
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
            if (humidCheck.Checked)
            { // Выбран увлажнитель
                bit0 = true; // Наличие увлажнителя
                // Наличие насоса сотового или пуск/стоп парового
                bit1 = (humidTypeCombo.SelectedIndex == 0 && startHumidCheck.Checked) ||
                    (humidTypeCombo.SelectedIndex == 1 && powPumpHumidCheck.Checked);
                bit2 = humidTypeCombo.SelectedIndex == 0 && analogHumCheck.Checked; // 0-10 В
                bit3 = humidTypeCombo.SelectedIndex == 0 && alarmHumidCheck.Checked; // Аварийный сигнал
            }
            else // Не выбран увлажнитель
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
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9, bit10, bit11, bit12, bit13;
            bit0 = true; // Наличие вентилятора
            bit1 = prFanPSCheck.Checked; // Подтверждение работы вентилятора
            bit2 = prFanAlarmCheck.Checked; // Внешний сигнал аварии
            bit3 = prFanThermoCheck.Checked; // Наличие термоконтактов
            bit4 = curDefPrFanCheck.Checked; // Защита по току
            bit5 = false; // Наличие заслонки
            bit6 = false; // Наличие подтверждения заслонки
            bit7 = prFanPowSupCheck.Checked; // Наличие контактора, сигнал подачи питания
            bit8 = prFanStStopCheck.Checked; // Наличие сухого контакта - запуск ПЧ
            bit9 = false; // Наличие ATV12 или ATV310
            bit10 = false; // Наличие ATV212
            bit11 = prFanSpeedCheck.Checked; // Управление 0-10 В
            bit12 = false; // Позисторные термоконтакты
            bit13 = false; // Поддержание давления по аналоговому датчику
            cmdW19 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8) +
                512 * Convert.ToUInt16(bit9) + 1024 * Convert.ToUInt16(bit10) + 2048 * Convert.ToUInt16(bit11) +
                4096 * Convert.ToUInt16(bit12) + 8192 * Convert.ToUInt16(bit13));
        }

        /// <summary>Командное слово резервного приточного вентилятора</summary>
        private void CommandWord_20()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9, bit10, bit11, bit12, bit13;
            if (checkResPrFan.Checked)
            { // Выбран резервный двигатель
                bit0 = true; // Наличие вентилятора
                bit1 = prFanPSCheck.Checked; // Подтверждение работы вентилятора
                bit2 = prFanAlarmCheck.Checked; // Внешний сигнал аварии
                bit3 = prFanThermoCheck.Checked; // Наличие термоконтактов
                bit4 = curDefPrFanCheck.Checked; // Защита по току
                bit5 = false; // Наличие заслонки
                bit6 = false; // Наличие подтверждения заслонки
                bit7 = prFanPowSupCheck.Checked; // Наличие контактора, сигнал подачи питания
                bit8 = prFanStStopCheck.Checked; // Наличие сухого контакта - запуск ПЧ
                bit9 = false; // Наличие ATV12 или ATV310
                bit10 = false; // Наличие ATV212
                bit11 = prFanSpeedCheck.Checked; // Управление 0-10 В
                bit12 = false; // Позисторные термоконтакты
                bit13 = false; // Поддержание давления по аналоговому датчику
            }
            else // Не выбран резервный двигатель
                bit0 = bit1 = bit2 = bit3 = bit4 = bit5 = bit6 = bit7 = bit8 = bit9 = bit10 =
                    bit11 = bit12 = bit13 = false;
            cmdW20 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8) +
                512 * Convert.ToUInt16(bit9) + 1024 * Convert.ToUInt16(bit10) + 2048 * Convert.ToUInt16(bit11) +
                4096 * Convert.ToUInt16(bit12) + 8192 * Convert.ToUInt16(bit13));
        }

        /// <summary>Командное слово основного вытяжного вентилятора</summary>
        private void CommandWord_21()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9, bit10, bit11, bit12, bit13;
            if (comboSysType.SelectedIndex == 1)
            { // Выбрана ПВ-система
                bit0 = true; // Наличие вентилятора
                bit1 = outFanPSCheck.Checked; // Подтверждение работы вентилятора
                bit2 = outFanAlarmCheck.Checked; // Внешний сигнал аварии
                bit3 = outFanThermoCheck.Checked; // Наличие термоконтактов
                bit4 = curDefOutFanCheck.Checked; // Защита по току
                bit5 = false; // Наличие заслонки
                bit6 = false; // Наличие подтверждения заслонки
                bit7 = outFanPowSupCheck.Checked; // Наличие контактора, сигнал подачи питания
                bit8 = outFanStStopCheck.Checked; // Наличие сухого контакта - запуск ПЧ
                bit9 = false; // Наличие ATV12 или ATV310
                bit10 = false; // Наличие ATV212
                bit11 = outFanSpeedCheck.Checked; // Управление 0-10 В
                bit12 = false; // Позисторные термоконтакты
                bit13 = false; // Поддержание давления по аналоговому датчику
            }
            else // Не выбрана ПВ-система
                bit0 = bit1 = bit2 = bit3 = bit4 = bit5 = bit6 = bit7 = bit8 = bit9 = bit10 =
                    bit11 = bit12 = bit13 = false;
            cmdW21 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8) +
                512 * Convert.ToUInt16(bit9) + 1024 * Convert.ToUInt16(bit10) + 2048 * Convert.ToUInt16(bit11) +
                4096 * Convert.ToUInt16(bit12) + 8192 * Convert.ToUInt16(bit13));
        }

        /// <summary>Командное слово резервного вытяжного вентилятора</summary>
        private void CommandWord_22()
        {
            bool bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit8, bit9, bit10, bit11, bit12, bit13;
            if (comboSysType.SelectedIndex == 1 && checkResOutFan.Checked)
            { // Выбрана ПВ-система и резерв вытяжного вентилятора
                bit0 = true; // Наличие вентилятора
                bit1 = outFanPSCheck.Checked; // Подтверждение работы вентилятора
                bit2 = outFanAlarmCheck.Checked; // Внешний сигнал аварии
                bit3 = outFanThermoCheck.Checked; // Наличие термоконтактов
                bit4 = curDefOutFanCheck.Checked; // Защита по току
                bit5 = false; // Наличие заслонки
                bit6 = false; // Наличие подтверждения заслонки
                bit7 = outFanPowSupCheck.Checked; // Наличие контактора, сигнал подачи питания
                bit8 = outFanStStopCheck.Checked; // Наличие сухого контакта - запуск ПЧ
                bit9 = false; // Наличие ATV12 или ATV310
                bit10 = false; // Наличие ATV212
                bit11 = outFanSpeedCheck.Checked; // Управление 0-10 В
                bit12 = false; // Позисторные термоконтакты
                bit13 = false; // Поддержание давления по аналоговому датчику
            } else // Не выбрана ПВ-система или резерв вытяжного вентилятора
                bit0 = bit1 = bit2 = bit3 = bit4 = bit5 = bit6 = bit7 = bit8 = bit9 = bit10 =
                    bit11 = bit12 = bit13 = false;
            cmdW22 = (ushort)(Convert.ToUInt16(bit0) + 2 * Convert.ToUInt16(bit1) + 4 * Convert.ToUInt16(bit2) +
                8 * Convert.ToUInt16(bit3) + 16 * Convert.ToUInt16(bit4) + 32 * Convert.ToUInt16(bit5) +
                64 * Convert.ToUInt16(bit6) + 128 * Convert.ToUInt16(bit7) + 256 * Convert.ToUInt16(bit8) +
                512 * Convert.ToUInt16(bit9) + 1024 * Convert.ToUInt16(bit10) + 2048 * Convert.ToUInt16(bit11) +
                4096 * Convert.ToUInt16(bit12) + 8192 * Convert.ToUInt16(bit13));
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
            if (!fireCheck.Checked) cmdW_fire = 0; // Нет сигнала
            else if (fireCheck.Checked && fireTypeCombo.SelectedIndex == 0) cmdW_fire = 0; // Сигнал + НО
            else if (fireCheck.Checked && fireTypeCombo.SelectedIndex == 1) cmdW_fire = 1; // Сигнал + НЗ
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
            CheckMarkPrDamp_Spec(); // Проверка для подобранного привода
            if (ignoreEvents) return;
            ConfPrDampCheck_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
        }

        ///<summary>Выбрали обогрев приточной заслонки</summary>
        private void HeatPrDampCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_2();
            if (ignoreEvents) return;
            HeatPrDampCheck_signalsCheckedChanged(this, e); // Сигналы ПЛК
        }

        ///<summary>Выбрали вытяжную заслонку</summary>
        private void OutDampCheck_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_3();

        ///<summary>Выбрали подтверждение открытия вытяжной заслонки</summary>
        private void ConfOutDampCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_3();
            CheckMarkOutDamp_Spec(); // Проверка для подобранного привода
            if (ignoreEvents) return;
            ConfOutDampCheck_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
        }

        ///<summary>Выбрали обогрев вытяжной заслонки</summary>
        private void HeatOutDampCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_3();
            if (ignoreEvents) return;
            HeatOutDampCheck_signalsCheckedChanged(this, e); // Сигналы ПЛК
        }

        ///<summary>Выбрали фильтр</summary>
        private void FilterCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_4(); CommandWord_5();
        }

        ///<summary>Изменили количество приточных фильтров</summary>
        private void FilterPrCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_4();
            if (ignoreEvents) return;
            FilterPrCombo_signalsDISelectedIndexChanged(this, e); // Сигналы DI
        }

        ///<summary>Изменили количество вытяжных фильтров</summary>
        private void FilterOutCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_5();
            if (ignoreEvents) return;
            FilterOutCombo_signalsDISelectedIndexChanged(this, e); // Сигналы DI
        }

        ///<summary>Выбрали сигнал пожарной сигнализации</summary>
        private void FireCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            if (fireCheck.Checked) fireTypeCombo.Enabled = true;
            else fireTypeCombo.Enabled = false;
            CommandWord_fire(); // Пересчет командного слова для ПС
            if (ignoreEvents) return;
            FireCheck_signalsDISelectedIndexChanged(this, e); // Сигналы DI
        }

        ///<summary>Изменили тип пожарной сигнализации</summary>
        private void FireTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_fire(); // Пересчет командного слова для ПС
        }

        ///<summary>Выбрали приточный канальный датчик температуры</summary>
        private void PrChanSensCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_6();
            if (ignoreEvents) return;
            PrChanSensCheck_signalsAICheckedChanged(this, e); // Сигналы AI
        }

        ///<summary>Выбрали датчик температуры наружного воздуха</summary>
        private void OutdoorChanSensCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_6();
            if (ignoreEvents) return;
            OutdoorChanSensCheck_signalsAICheckedChanged(this, e); // Сигналы AI
        }

        ///<summary>Выбрали комнатный датчик температуры</summary>
        private void RoomTempSensCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_6();
            if (ignoreEvents) return;
            RoomTempSensCheck_signalsAICheckedChanged(this, e); // Сигналы AI
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
            RecDefTempCheck_signalsAICheckedChanged(this, e); // Сигналы AI
        }

        ///<summary>Выбрали канальный датчик влажности</summary>
        private void ChanHumSensCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_6();
            if (ignoreEvents) return;
            ChanHumSensCheck_signalsAICheckedChanged(this, e); // Сигналы AI
        }

        ///<summary>Выбрали комнатный датчик влажности</summary>
        private void RoomHumSensCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_6();
            if (ignoreEvents) return;
            RoomHumSensCheck_signalsAICheckedChanged(this, e); // Сигналы AI
        }

        ///<summary>Выбрали канальный датчик Т вытяжного воздуха</summary>
        private void OutChanSensCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_6();
            if (ignoreEvents) return;
            OutChanSensCheck_signalsAICheckedChanged(this, e); // Сигналы AI
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
            => CommandWord_9();
        
        ///<summary>Выбрали сигнал 0-10 В роторного рекуператора</summary>
        private void StartRotRecCheck_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_9();

        ///<summary>Выбрали сигнал 0-10 В роторного рекуператора</summary>
        private void AnalogRotRecCheck_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_9();
        
        ///<summary>Изменили тип управления пластинчатого рекуператора</summary>
        private void AnalogGlikRecCheck_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_9();

        ///<summary>Изменили тип управления пластинчатого рекуператора</summary>
        private void BypassPlastCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_9();
            if (ignoreEvents) return;
            BypassPlastCombo_signalsAOSelectedIndexChanged(this, e); // Сигналы AO ПЛК
        }

        ///<summary>Выбрали сигнал PS рекуператора</summary>
        private void RecDefPsCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_9();
            if (ignoreEvents) return;
            RecDefPsCheck_signalsDICheckedChanged(this, e); // Сигналы DI
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

        ///<summary>Изменили количество ступеней основного электрокалорифера</summary>
        private void ElHeatStagesCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_10();
            if (ignoreEvents) return;
            ElHeatStagesCombo_signalsSelectedIndexChanged(this, e); // Сигналы ПЛК
        }

        ///<summary>Изменили тип управления первой ступенью нагревателя</summary>
        private void FirstStHeatCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_10();
            if (ignoreEvents) return;
            FirstStHeatCombo_SignalsAOSelectedIndexChanged(this, e); // Сигналы AO ПЛК
        }

        ///<summary>Изменили количество термовыключателей калорифера</summary>
        private void ThermSwitchCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_10();
            if (ignoreEvents) return;
            ThermSwitchCombo_signalsDISelectedIndexChanged(this, e); // Сигналы DI

        }

        ///<summary>Подтверждение работы насоса водяного калорифера</summary>
        private void ConfHeatPumpCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_11();
            if (ignoreEvents) return;
            ConfHeatPumpCheck_signalsDICheckedChanged(this, e); // Сигналы DI
        }

        ///<summary>Выбрали термостат защиты от замерзания калорифера</summary>
        private void TF_heaterCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_11();
            if (ignoreEvents) return;
            TF_heaterCheck_signalsDICheckedChanged(this, e); // Сигналы DI
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
            FrCoolStagesCombo_signalsDOSelectedIndexChanged(this, e); // Сигналы DO
        }

        ///<summary>Выбрали аварийный сигнал для фреонового охладителя</summary>
        private void AlarmFrCoolCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_12();
            if (ignoreEvents) return;
            AlarmFrCoolCheck_signalsDICheckedChanged(this, e); // Сигналы DI
        }

        ///<summary>Выбрали сигнал 0-10 В для фреонового охладителя</summary>
        private void AnalogFreonCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_12();
            if (ignoreEvents) return;
            AnalogFreonCheck_signalsAOCheckedChanged(this, e); // Сигналы AO
        }

        private void ThermoCoolerCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_12();
            if (ignoreEvents) return;
            ThermoCoolerCheck_signalsDICheckedChanged(this, e); // Сигналы DI
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
            PumpAddHeatCheck_signalsDOCheckedChanged(this, e); // Сигналы DO
        }

        ///<summary>Подтверждение работы насоса водяного догревателя</summary>
        private void ConfAddHeatPumpCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_13();
            if (ignoreEvents) return;
            ConfAddHeatPumpCheck_signalsDICheckedChanged(this, e); // Сигналы DI
        }

        ///<summary>Выбрали термостат догревателя</summary>
        private void TF_addHeaterCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_13();
            if (ignoreEvents) return;
            TF_addHeaterCheck_signalsDICheckedChanged(this, e); // Сигналы DI
        }

        ///<summary>Изменили количество ступеней электрического догревателя</summary>
        private void ElHeatAddStagesCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_14();
            if (ignoreEvents) return;
            ElHeatAddStagesCombo_signalsSelectedIndexChanged(this, e); // Сигналы ПЛК
        }

        ///<summary>Изменили тип управления первой ступенью догревателя</summary>
        private void FirstStAddHeatCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_14();
            if (ignoreEvents) return;
            FirstStAddHeatCombo_signalsAOSelectedIndexChanged(this, e);
        }

        ///<summary>Изменили количество термовыключателей</summary>
        private void ThermAddSwitchCombo_cmdSelectedIndexChanged(object sender, EventArgs e)
        {
            CommandWord_14();
            if (ignoreEvents) return;
            ThermAddSwitchCombo_signalsDISelectedIndexChanged(this, e); // Сигналы DI
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
            => CommandWord_15();

        ///<summary>Выбрали сигнал аварии парового увлажнителя</summary>
        private void AlarmHumidCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_15();
            if (ignoreEvents) return;
            AlarmHumidCheck_signalsDICheckedChanged(this, e); // Сигналы DI
        }

        ///<summary>Выбрали подачу питания на насос увлажнителя</summary>
        private void PowPumpHumidCheck_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_15();

        ///<summary>Выбрали PS приточного вентилятора</summary>
        private void PrFanPSCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_19(); CommandWord_20();
            if (ignoreEvents) return;
            PrFanPSCheck_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
        }

        ///<summary>Выбрали ПЧ приточного вентилятора</summary>
        private void PrFanFC_check_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_19(); CommandWord_20();
        }

        ///<summary>Выбрали термоконтакты приточного вентилятора</summary>
        private void PrFanThermoCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_19(); CommandWord_20();
            if (ignoreEvents) return;
            PrFanThermoCheck_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
        }

        ///<summary>Выбрали защиту по току приточного вентилятора</summary>
        private void CurDefPrFanCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_19(); CommandWord_20();
            if (ignoreEvents) return;
            CurDefPrFanCheck_signalsDICheckedChanged(this, e); // Сигналы DI
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
            OutFanPSCheck_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
        }

        ///<summary>Выбрали вытяжной вентилятор</summary>
        private void OutFanFC_check_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_21(); CommandWord_22();
        }

        ///<summary>Выбрали термоконтакты вытяжного вентилятора</summary>
        private void OutFanThermoCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_21(); CommandWord_22();
            if (ignoreEvents) return;
            OutFanThermoCheck_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
        }

        ///<summary>Выбрали защиту по току для вытяжного вентилятора</summary>
        private void CurDefOutFanCheck_cmdCheckedChanged(object sender, EventArgs e)
        {
            CommandWord_21(); CommandWord_22();
            if (ignoreEvents) return;
            CurDefOutFanCheck_signalsDICheckedChanged(this, e); // Сигналы DI ПЛК
        }

        ///<summary>Командное слово для вытяжного резерва</summary>
        private void CheckResOutFan_cmdCheckedChanged(object sender, EventArgs e)
            => CommandWord_22();
    }
}