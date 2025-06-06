﻿using System;
using System.Windows.Forms;

namespace Moderon
{
    public partial class Form1 : Form
    {
        ///<summary>Выбрали блок заслонки</summary>
        private void DampCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1006, code_2 = 1039;                                                // Подтверждение открытия приточной/вытяжной заслонки

            if (dampCheck.Checked && confPrDampCheck.Checked)                                   // Приточная заслонка и подтверждение открытия
                CheckAddUIToList("Подтверждение открытия приточной заслонки", code_1, DI);
            if (dampCheck.Checked && outDampCheck.Checked && confOutDampCheck.Checked)          // Вытяжная заслонка и подтверждение открытия
                CheckAddUIToList("Подтверждение открытия вытяжной заслонки", code_2, DI);
            else                                                                                // Отмена выбора блока заслонки
            {
                SubFromCombosUI(code_1); SubFromCombosUI(code_2);
            }
        }

        ///<summary>Выбрали подтверждение открытия приточной заслонки</summary>
        private void ConfPrDampCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1006;                                                               // Подтверждение открытия приточной заслонки

            if (dampCheck.Checked && confPrDampCheck.Checked)                                   // Выбрана приточная заслонка и подтвреждение открытия
                CheckAddUIToList("Подтверждение открытия приточной заслонки", code_1, DI);
            else                                                                                // Отмена выбора сигнала
                SubFromCombosUI(code_1);
        }

        ///<summary>Выбрали вытяжную заслонку</summary>
        private void OutDampCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1039;                                                                       // Подтверждение открытия вытяжной заслонки

            if (comboSysType.SelectedIndex == 1 && dampCheck.Checked && outDampCheck.Checked)           // ПВ-система, выбрана вытяжная заслонка
            { 
                if (confOutDampCheck.Checked)                                                           // Подтверждение открытия
                    CheckAddUIToList("Подтверждение открытия вытяжной заслонки", code_1, DI);
            }
            else                                                                                        // Отмена выбора заслонки
                SubFromCombosUI(code_1);
        }

        ///<summary>Выбрали подтверждение открытия вытяжной заслонки</summary>
        private void ConfOutDampCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1039;                                                                       // Подтверждение открытия вытяжной заслонки

            if (comboSysType.SelectedIndex == 1 && dampCheck.Checked && outDampCheck.Checked)           // ПВ-система, выбрана вытяжная заслонка
            { 
                if (confOutDampCheck.Checked)                                                           // Подтверждение открытия
                    CheckAddUIToList("Подтверждение открытия вытяжной заслонки", code_1, DI);
                else                                                                                    // Отмена выбора подтверждения открытия
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Выбрали PS приточного вентилятора</summary>
        private void PrFanPSCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1011, code_2 = 1022;                                                        // PS приточного вентилятора 1 и 2

            if (prFanPSCheck.Checked)                                                                   // Выбрали PS
            {
                CheckAddUIToList("PS приточного вентилятора 1", code_1, DI);
                if (checkResPrFan.Checked)                                                              // Если выбран резерв
                    CheckAddUIToList("PS приточного вентилятора 2", code_2, DI);
            }
            else                                                                                        // Отмена выбора PS
            {
                SubFromCombosUI(code_1); SubFromCombosUI(code_2);
            }
        }

        ///<summary>Выбрали термоконтакты приточного вентилятора</summary>
        private void PrFanThermoCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1013, code_2 = 1024;                                                        // Термоконтакты приточного вентилятора 1 и 2

            if (prFanThermoCheck.Checked)                                                               // Выбрали термоконтакты
            {
                CheckAddUIToList("Термоконтакты приточного вентилятора 1", code_1, DI);
                if (checkResPrFan.Checked)                                                              // Если выбран резерв
                    CheckAddUIToList("Термоконтакты приточного вентилятора 2", code_2, DI);
            }
            else                                                                                        // Отмена выбора термоконтактов
            {
                SubFromCombosUI(code_1); SubFromCombosUI(code_2);
            }
        }

        ///<summary>Изменили тип управления ПЧ приточного вентилятора</summary>
        private void PrFanControlCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            if (prFanFC_ECcombo.SelectedIndex == 1)                                                     // Когда выбран ПЧ
            {
                if (prFanControlCombo.SelectedIndex == 0)                                               // Внешние контакты
                {
                    prFanAlarmCheck.Enabled = true;                                                     // Разблокировка выбора аварии
                    if (!prFanAlarmCheck.Checked)                                                       // Выбор сигнала аварии
                        prFanAlarmCheck.Checked = true; 
                }
                else if (prFanControlCombo.SelectedIndex == 1)                                          // Modbus
                {
                    if (prFanAlarmCheck.Checked)                                                        // Снятие выбора опций
                        prFanAlarmCheck.Checked = false;
                    // Блокировка опций
                    prFanAlarmCheck.Enabled = false;
                }
            }
        }

        ///<summary>Выбрали защиту по току приточного вентилятора</summary>
        private void CurDefPrFanCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1014, code_2 = 1025;                                                         // Защита по току приточного вентилятора 1 и 2

            if (curDefPrFanCheck.Checked) // Выбрана защита по току
            {
                CheckAddUIToList("Защита по току приточного вентилятора 1", code_1, DI);
                if (checkResPrFan.Checked) // Если выбран резерв
                    CheckAddUIToList("Защита по току приточного вентилятора 2", code_2, DI);
            } 
            else // Отмена выбора защиты по току
            {
                SubFromCombosUI(code_1); SubFromCombosUI(code_2);
            }
        }

        ///<summary>Выбрали резерв приточного вентилятора</summary>
        private void CheckResPrFan_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort
                code_1 = 1022,                               // PS приточного вентилятора 2
                code_2 = 1024,                               // Термоконтакты приточного вентилятора 2
                code_3 = 1023,                               // Сигнал аварии приточного вентилятора 2
                code_4 = 1025;                               // Защита по току приточного вентилятора 2

            if (checkResPrFan.Checked)                                                              // Выбран резерв приточного
            {
                if (prFanPSCheck.Checked)                                                           // Выбран сигнал PS
                    CheckAddUIToList("PS приточного вентилятора 2", code_1, DI);
                if (prFanThermoCheck.Checked)                                                       // Выбраны термоконтакты приточного
                    CheckAddUIToList("Термоконтакты приточного вентилятора 2", code_2, DI);
                if (prFanAlarmCheck.Checked)                                                        // Выбран сигнал аварии
                    CheckAddUIToList("Сигнал аварии приточного вентилятора 2", code_3, DI);
                if (curDefPrFanCheck.Checked)                                                       // Выбрана защита по току
                    CheckAddUIToList("Защита по току приточного вентилятора 2", code_4, DI);
            }
            else                                                                                    // Отмена выбора резерва приточного
            {
                SubFromCombosUI(code_1); SubFromCombosUI(code_2); SubFromCombosUI(code_3); SubFromCombosUI(code_4);
            }
        }

        ///<summary>Выбрали подтверждение открытия заслонки приточного вентилятора</summary>
        private void PrDampConfirmFanCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1017;                                                               // Сигнал подтверждения открытия заслонки приточного вентилятора

            if (prDampFanCheck.Checked && prDampConfirmFanCheck.Checked)                        // Выбрали подтверждение открытия
            {
                CheckAddUIToList("Подтверждение для заслонки приточного вентилятора", code_1, DI);
            }
            else                                                                                // Отмена выбора подтверждения открытия
                SubFromCombosUI(code_1);
        }

        ///<summary>Выбрали PS вытяжного вентилятора</summary>
        private void OutFanPSCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1044, code_2 = 1055;                                                    // Сигнал PS вытяжного вентилятора 1 и 2

            if (comboSysType.SelectedIndex == 1 && outFanCheck.Checked && outFanPSCheck.Checked)    // Выбрали PS вытяжного вентилятора
            {
                CheckAddUIToList("PS вытяжного вентилятора 1", code_1, DI);
                if (checkResOutFan.Checked)                                                         // Если выбран резерв
                    CheckAddUIToList("PS вытяжного вентилятора 2", code_2, DI);
            }
            else                                                                                    // Отмена выбора PS
            {
                SubFromCombosUI(code_1); SubFromCombosUI(code_2);
            }
        }

        ///<summary>Выбрали термоконтакты вытяжного вентилятора</summary>
        private void OutFanThermoCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1046, code_2 = 1057;                                                    // Термоконтакты вытяжного вентилятора 1 и 2
            
            if (comboSysType.SelectedIndex == 1 && outFanCheck.Checked && outFanThermoCheck.Checked)
            {
                CheckAddUIToList("Термоконтакты вытяжного вентилятора 1", code_1, DI);
                if (checkResOutFan.Checked)                                                         // Если выбран резерв
                    CheckAddUIToList("Термоконтакты вытяжного вентилятора 2", code_2, DI);
            }
            else                                                                                    // Отмена выбора термоконтактов
            {
                SubFromCombosUI(code_1); SubFromCombosUI(code_2);
            }
        }

        ///<summary>Изменили тип управления ПЧ вытяжного вентилятора</summary>
        private void OutFanControlCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSysType.SelectedIndex == 1 && outFanFC_ECcombo.SelectedIndex == 1)             // Когда выбран ПЧ
            {
                if (outFanControlCombo.SelectedIndex == 0)                                          // Внешние контакты
                {
                    outFanAlarmCheck.Enabled = true;                                                // Разблокировка выбора аварии
                    if (!outFanAlarmCheck.Checked)                                                  // Выбор сигнала аварии
                        outFanAlarmCheck.Checked = true; 
                }
                else if (outFanControlCombo.SelectedIndex == 1)                                     // Modbus
                {
                    // Снятие выбора опций
                    if (outFanAlarmCheck.Checked) 
                        outFanAlarmCheck.Checked = false;
                    // Блокировка опций
                    outFanAlarmCheck.Enabled = false;
                }
            }
        }

        ///<summary>Выбрали защиту по току вытяжного вентилятора</summary>
        private void CurDefOutFanCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1047, code_2 = 1058;                                                    // Защита по току вытяжного вентилятора 1 и 2

            if (comboSysType.SelectedIndex == 1 && outFanCheck.Checked && curDefOutFanCheck.Checked)
            {
                CheckAddUIToList("Защита по току вытяжного вентилятора 1", code_1, DI);
                if (checkResOutFan.Checked)                                                         // Если выбран резерв
                    CheckAddUIToList("Защита по току вытяжного вентилятора 2", code_2, DI);
            }
            else                                                                                    // Отмена выбора защиты по току
            {
                SubFromCombosUI(code_1); SubFromCombosUI(code_2);
            }
        }

        ///<summary>Выбрали резерв вытяжного вентилятора</summary>
        private void CheckResOutFan_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort 
                code_1 = 1055,                     // PS вытяжного вентилятра 2
                code_2 = 1057,                     // Термоконтакты вытяжного вентилятора 2
                code_3 = 1056,                     // Сигнал аварии вытяжного вентилятора 2
                code_4 = 1058;                     // Защита по току вытяжного вентилятора 2

            if (comboSysType.SelectedIndex == 1 && outFanCheck.Checked && checkResOutFan.Checked)  // Выбран резерв вытяжного
            {
                if (outFanPSCheck.Checked)                                                          // Выбран сигнал PS
                    CheckAddUIToList("PS вытяжного вентилятора 2", code_1, DI);
                if (outFanThermoCheck.Checked)                                                      // Выбраны термоконтакты
                    CheckAddUIToList("Термоконтакты вытяжного вентилятора 2", code_2, DI);
                if (outFanAlarmCheck.Checked)                                                       // Выбрали сигнал аварии
                    CheckAddUIToList("Сигнал аварии вытяжного вентилятора 2", code_3, DI);
                if (curDefOutFanCheck.Checked)                                                      // Защита по току
                    CheckAddUIToList("Защита по току вытяжного вентилятора 2", code_4, DI);
            }
            else // Отмена выбора резерва вытяжного
            {
                SubFromCombosUI(code_1); SubFromCombosUI(code_2); 
                SubFromCombosUI(code_3); SubFromCombosUI(code_4);
            }
        }

        ///<summary>Выбрали подтверждение открытия заслонки вытяжного вентилятора</summary>
        private void OutDampConfirmFanCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code = 1050;                                                                     // Сигнал подтверждения открытия заслонки вытяжного вентилятора

            // Вытяжной вентилятор, вытяжная заслонка и подтверждение открытия
            if (outFanCheck.Checked && outDampFanCheck.Checked && outDampConfirmFanCheck.Checked)   
            {
                CheckAddUIToList("Подтверждение для заслонки вытяжного вентилятора", code, DI);
            }
            else                                                                                    // Отмена выбора подтверждения открытия
            {
                SubFromCombosUI(code);
            }
        }

        ///<summary>Выбрали нагреватель</summary>
        private void HeaterCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1072, code_2 = 1073;                                                    // Воздушный термостат и подтверждение работы насоса

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)                            // Выбран водяной нагреватель
            { 
                if (TF_heaterCheck.Checked)                                                         // Выбран термостат
                    CheckAddUIToList("Термостат водяного нагревателя", code_1, DI);
                if (confHeatPumpCheck.Checked)                                                      // Подтверждение работы насоса
                    CheckAddUIToList("Подтверждение работы насоса водяного нагревателя", code_2, DI);
            }
            else                                                                                    // Отмена выбора нагревателя
            {
                SubFromCombosUI(code_1); SubFromCombosUI(code_2);
            }

            OverheatThermCheck_signalsDICheckedChanged(this, e);                                    // Проверка термовыключателя перегрева
            FireThermCheck_signalsDICheckedChanged(this, e);                                        // Проверка термовыключателя пожара
        }

        ///<summary>Изменили тип основного нагревателя</summary>
        private void HeatTypeCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            // Воздушный термостат, подтверждение работы насоса, защита по току
            ushort code_1 = 1072, code_2 = 1073, code_3 = 1075, code_4 = 1074, code_5 = 1076;                                          

            if (heaterCheck.Checked)                                                                // Выбран нагреватель
            { 
                if (heatTypeCombo.SelectedIndex == 0)                                               // Водяной нагреватель
                {
                    if (TF_heaterCheck.Checked)                                                     // Воздушный термостат
                        CheckAddUIToList("Термостат водяного нагревателя", code_1, DI);
                    if (confHeatPumpCheck.Checked)                                                  // Подтверждение работы насоса
                        CheckAddUIToList("Подтверждение работы насоса водяного нагревателя", code_2, DI);
                    if (pumpCurProtect.Checked)                                                     // Защита насоса по току
                        CheckAddUIToList("Защита по току основного насоса нагревателя", code_3, DI);

                    if (reservPumpHeater.Checked)                                                   // Выбран резервный насос
                    {
                        if (confHeatResPumpCheck.Checked)                                           // Подтверждение работы резервного насоса
                            CheckAddUIToList("Подтверждение работы резервного насоса нагревателя", code_4, DI);
                        if (pumpCurResProtect.Checked)                                              // Защита резервного насоса по току
                            CheckAddUIToList("Защита по току резервного насоса нагревателя", code_5, DI);
                    }
                }
                else                                                                                // Электрический нагреватель
                {
                    SubFromCombosUI(code_1); SubFromCombosUI(code_2); SubFromCombosUI(code_3);
                    SubFromCombosUI(code_4); SubFromCombosUI(code_5);
                }

                OverheatThermCheck_signalsDICheckedChanged(this, e);                                // Проверка термовыключателя перегрева
                FireThermCheck_signalsDICheckedChanged(this, e);                                    // Проверка термовыключателя пожара
            }
        }

        ///<summary>Выбрали термостат защиты от замерзания</summary>
        private void TF_heaterCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1072;                                                                   // Воздушный термостат

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)                            // Выбран водяной нагреватель
            { 
                if (TF_heaterCheck.Checked) // Выбрали воздушный термостат
                    CheckAddUIToList("Термостат водяного нагревателя", code_1, DI);
                else // Отмена выбора
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Подтверждение работы основного насоса водяного нагревателя</summary>
        private void ConfHeatPumpCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1073;                                                                   // Подтверждение работы основного насоса нагревателя

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)                            // Выбран водяной нагреватель
            { 
                if (confHeatPumpCheck.Checked)                                                      // Выбрали подтверждение работы насоса
                    CheckAddUIToList("Подтверждение работы основного насоса нагревателя", code_1, DI);
                else                                                                                // Отмена выбора
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Подтверждение работы резервного насоса водяного нагревателя</summary>
        private void ConfHeatResPumpCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1074;                                                                       // Подтверждение работы резервного насоса нагревателя

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)                                // Выбран водяной нагреватель
            {
                if (reservPumpHeater.Checked && confHeatResPumpCheck.Checked)                           // Есть насос и подтверждение работы насоса
                    CheckAddUIToList("Подтверждение работы резервного насоса нагревателя", code_1, DI);
                else
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Защита по току основного насоса водяного нагревателя</summary>
        private void PumpCurProtect_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1075;                                                                   // Защита по току основного насоса

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)                            // Выбран водяной нагреватель
            {
                if (pumpCurProtect.Checked)                                                         // Выбрали защиту по току
                    CheckAddUIToList("Защита по току основного насоса нагревателя", code_1, DI);
                else
                    SubFromCombosUI(code_1);                                                        // Отмена выбора
            }
        }

        ///<summary>Защита по току резервного насоса водяного нагревателя</summary>
        private void PumpCurResProtect_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1076;                                                                   // Защита по току основного насоса

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)                            // Выбран водяной нагреватель
            {
                if (reservPumpHeater.Checked && pumpCurResProtect.Checked)                          // Выбран резервный насос и защита по току
                    CheckAddUIToList("Защита по току резервного насоса нагревателя", code_1, DI);
                else
                    SubFromCombosUI(code_1);                                                        // Отмена выбора
            }
        }

        ///<summary>Выбрали термовыключатель перегрева ТЭНов</summary>
        private void OverheatThermCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1078;                                                                   // Термовыключатель перегрева ТЭНов

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 1)                            // Выбран электрический нагреватель
            {
                if (overheatThermCheck.Checked)
                    CheckAddUIToList("Термовыключатель перегрева ТЭНов нагревателя", code_1, DI);
                else
                    SubFromCombosUI(code_1);

            }
            else SubFromCombosUI(code_1);
        }

        ///<summary>Выбрали термовыключатель пожара ТЭНов</summary>
        private void FireThermCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1079;                                                                   // Термовыключатель пожара ТЭНов

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 1)                            // Выбран электрический нагреватель
            {
                if (fireThermCheck.Checked)
                    CheckAddUIToList("Термовыключатель пожара ТЭНов нагревателя", code_1, DI);
                else
                    SubFromCombosUI(code_1);

            }
            else SubFromCombosUI(code_1);
        }

        ///<summary>Выбрали догреватель</summary>
        private void AddHeatCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1133, code_2 = 1134;                                                    // Воздушный термостат и подтверждение работы насоса

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)                        // Выбран водяной догреватель
            { 
                if (TF_addHeaterCheck.Checked)                                                      // Выбран термостат
                    CheckAddUIToList("Термостат водяного догревателя", code_1, DI);
                if (confAddHeatPumpCheck.Checked)                                                   // Подтверждение работы насоса
                    CheckAddUIToList("Подтверждение работы насоса водяного догревателя", code_2, DI);
            } 
            else                                                                                    // Отмена выбора догревателя
            {
                SubFromCombosUI(code_1); SubFromCombosUI(code_2);
            }

            OverheatAddThermCheck_signalsDICheckedChanged(this, e);                                 // Проверка для термовыключателя перегрева
            FireAddThermCheck_signalsDICheckedChanged(this, e);                                     // Проверка для термовыключателя пожара
        }

        ///<summary>Изменили тип догревателя</summary>
        private void HeatAddTypeCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            // Воздушный термостат, подтверждение работы насоса и защита насосов по току
            ushort code_0 = 1136, code_1 = 1133, code_2 = 1134, code_3 = 1135, code_4 = 1137;                      

            if (addHeatCheck.Checked)                                                               // Выбран догреватель
            {
                if (heatAddTypeCombo.SelectedIndex == 0)                                            // Водяной догреватель
                {
                    if (TF_addHeaterCheck.Checked)                                                  // Воздушный термостат
                        CheckAddUIToList("Термостат водяного догревателя", code_1, DI);
                    if (confAddHeatPumpCheck.Checked)                                               // Подтверждение работы насоса
                        CheckAddUIToList("Подтверждение работы насоса водяного догревателя", code_2, DI);
                    if (pumpCurAddProtect.Checked)                                                  // Защита по току основного насоса
                        CheckAddUIToList("Защита по току основного насоса догревателя", code_0, DI);
                    
                    if (reservPumpAddHeater.Checked)                                                // Для резервного насоса догревателя
                    {
                        if (confAddHeatResPumpCheck.Checked)                                        // Подтверждение работы насоса
                            CheckAddUIToList("Подтверждение работы резервного насоса догревателя", code_3, DI);
                        if (pumpCurResAddProtect.Checked)                                           // Защита резервного насоса по току
                            CheckAddUIToList("Защита по току резервного насоса догревателя", code_4, DI);
                    }
                }
                else // Электродогреватель
                {
                    SubFromCombosUI(code_0); SubFromCombosUI(code_1); SubFromCombosUI(code_2);      // Удаление сигналов, основной насос
                    SubFromCombosUI(code_3); SubFromCombosUI(code_4);                               // Удаление сигналов, резервный насос

                }

                OverheatAddThermCheck_signalsDICheckedChanged(this, e);                             // Проверка для термовыключателя перегрева
                FireAddThermCheck_signalsDICheckedChanged(this, e);                                 // Проверка для термовыключателя пожара
            }
        }

        ///<summary>Выбрали термостат догревателя</summary>
        private void TF_addHeaterCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1133;                                                                   // Воздушный термостат

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)                        // Выбран водяной догреватель
            { 
                if (TF_addHeaterCheck.Checked)                                                      // Выбрали воздушный термостат
                    CheckAddUIToList("Термостат водяного догревателя", code_1, DI);
                else                                                                                // Отмена выбора
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Выбрали защиту по току основного насоса догревателя</summary>
        private void PumpCurAddProtect_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1136;                                                                   // Защита по току основного насоса догревателя

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)                        // Выбран водяной догреватель
            {
                if (pumpAddHeatCheck.Checked && pumpCurAddProtect.Checked)                          // Выбран насос и защита по току
                    CheckAddUIToList("Защита по току основного насоса догревателя", code_1, DI);
                else                                                                                // Отмена выбора
                    SubFromCombosUI(code_1);
            }    
        }

        ///<summary>Выбрали защиту по току резервного насоса догревателя</summary>
        private void PumpCurResAddProtect_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1137;                                                                   // Защита по току основного насоса догревателя

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)                        // Выбран водяной догреватель
            {
                if (reservPumpAddHeater.Checked && pumpCurResAddProtect.Checked)                    // Выбран резервный насос и защита по току
                    CheckAddUIToList("Защита по току резервного насоса догревателя", code_1, DI);
                else                                                                                // Отмена выбора
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Подтверждение работы основного насоса водяного догревателя</summary>
        private void ConfAddHeatPumpCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1134;                                                                   // Подтверждение работы основного насоса догревателя

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)                        // Выбран водяной догреватель
            { 
                if (pumpAddHeatCheck.Checked && confAddHeatPumpCheck.Checked)                       // Есть насос и подтверждение работы насоса
                    CheckAddUIToList("Подтверждение работы основного насоса догревателя", code_1, DI);
                else                                                                                // Отмена выбора
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Подтверждение работы резервного насоса водяного догревателя</summary>
        private void ConfAddHeatResPumpCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1135;                                                                               // Подтверждение работы резервного насоса догревателя

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)                                    // Выбран водяной догреватель
            {
                if (reservPumpAddHeater.Checked && confAddHeatResPumpCheck.Checked)                             // Есть резервный насос и подтверждение работы
                    CheckAddUIToList("Подтверждение работы резервного насоса догревателя", code_1, DI);
                else                                                                                            // Отмена выбора
                    SubFromCombosUI(code_1);
            }
        }  

        ///<summary>Выбрали термовыключатель перегрева догревателя</summary>
        private void OverheatAddThermCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1128;                                                                   // Термовыключатель перегрева догревателя

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 1)                        // Выбран электрический догреватель
            {
                if (overheatAddThermCheck.Checked)
                    CheckAddUIToList("Термовыключатель перегрева ТЭНов догревателя", code_1, DI);
                else
                    SubFromCombosUI(code_1);
            }
            else SubFromCombosUI(code_1);
        }

        ///<summary>Выбрали термовыключатель пожара догревателя</summary>
        private void FireAddThermCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1129;                                                                   // Термовыключатель пожара догревателя

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 1)                        // Выбран электрический догреватель
            {
                if (overheatAddThermCheck.Checked)
                    CheckAddUIToList("Термовыключатель пожара ТЭНов догревателя", code_1, DI);
                else
                    SubFromCombosUI(code_1);
            }
            else SubFromCombosUI(code_1);
        }

        ///<summary>Выбрали фильтр</summary>
        private void FilterCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1033, code_2 = 1034, code_3 = 1035;                                     // Фильтр 1, 2, 3 приточные
            ushort code_4 = 1066, code_5 = 1067, code_6 = 1068;                                     // Фильтр 1, 2, 3 вытяжные

            if (filterCheck.Checked)                                                                // Выбрали фильтры
            {
                CheckAddUIToList("Приточный фильтр 1", code_1, DI);
                if (filterPrCombo.SelectedIndex > 0)                                                // Два приточных фильтра
                    CheckAddUIToList("Приточный фильтр 2", code_2, DI);
                if (filterPrCombo.SelectedIndex > 1)                                                // Три приточных фильтра
                    CheckAddUIToList("Приточный фильтр 3", code_3, DI);
                if (comboSysType.SelectedIndex == 1)                                                // Выбрана ПВ-система
                {
                    if (filterOutCombo.SelectedIndex > 0)                                           // Один вытяжной фильтр
                        CheckAddUIToList("Вытяжной фильтр 1", code_4, DI);
                    if (filterOutCombo.SelectedIndex > 1)                                           // Два вытяжных фильтра
                        CheckAddUIToList("Вытяжной фильтр 2", code_5, DI);
                    if (filterOutCombo.SelectedIndex > 2)                                           // Три вытяжных фильтра
                        CheckAddUIToList("Вытяжной фильтр 3", code_6, DI);
                }
            }
            else                                                                                    // Отмена выбора фильтров
            {
                SubFromCombosUI(code_1); SubFromCombosUI(code_2); SubFromCombosUI(code_3);
                SubFromCombosUI(code_4); SubFromCombosUI(code_5); SubFromCombosUI(code_6);
            }
        }

        ///<summary>Изменили количество приточных фильтров</summary>
        private void FilterPrCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1034, code_2 = 1035;                                                    // Фильтр 2 и 3 приточные

            if (filterCheck.Checked)                                                                // Выбран фильтр
            {
                if (filterPrCombo.SelectedIndex == 0)                                               // Один фильтр
                {
                    SubFromCombosUI(code_1); SubFromCombosUI(code_2);                               // Удаление сигналов
                }
                else if (filterPrCombo.SelectedIndex == 1)                                          // Два приточных фильтра
                {
                    SubFromCombosUI(code_2);
                    CheckAddUIToList("Приточный фильтр 2", code_1, DI);
                }
                else if (filterPrCombo.SelectedIndex == 2)                                          // Три приточных фильтра
                {
                    CheckAddUIToList("Приточный фильтр 2", code_1, DI);
                    CheckAddUIToList("Приточный фильтр 3", code_2, DI);
                }
            }
        }

        ///<summary>Изменили количество вытяжных фильтров</summary>
        private void FilterOutCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1066, code_2 = 1067, code_3 = 1068;                                     // Фильтр 1, 2, 3 вытяжные

            if (comboSysType.SelectedIndex == 1 && filterCheck.Checked)                             // Выбрана ПВ-система и фильтр
            { 
                if (filterOutCombo.SelectedIndex == 0)                                              // Нет вытяжных фильтров
                { 
                    SubFromCombosUI(code_1); SubFromCombosUI(code_2); SubFromCombosUI(code_3);      // Удаление сигналов
                }
                else if (filterOutCombo.SelectedIndex == 1)                                         // Один вытяжной фильтр
                {
                    SubFromCombosUI(code_2); SubFromCombosUI(code_3);
                    CheckAddUIToList("Вытяжной фильтр 1", code_1, DI);
                }
                else if (filterOutCombo.SelectedIndex == 2)                                         // Два вытяжных фильтра
                {
                    SubFromCombosUI(code_3);
                    CheckAddUIToList("Вытяжной фильтр 1", code_1, DI);
                    CheckAddUIToList("Вытяжной фильтр 2", code_2, DI);
                }
                else if (filterOutCombo.SelectedIndex == 3)                                         // Три вытяжных фильтра
                {
                    CheckAddUIToList("Вытяжной фильтр 1", code_1, DI);
                    CheckAddUIToList("Вытяжной фильтр 2", code_2, DI);
                    CheckAddUIToList("Вытяжной фильтр 3", code_3, DI);
                }
            } 
        }

        ///<summary>Выбрали охладитель</summary>
        private void CoolerCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1083, code_2 = 1084;                                                    // Термостат и авария фреонового охладителя

            if (coolerCheck.Checked && coolTypeCombo.SelectedIndex == 0)                            // Выбран фреоновый охладитель
            { 
                if (thermoCoolerCheck.Checked)                                                      // Выбран термостат
                    CheckAddUIToList("Термостат фреонового охладителя", code_1, DI);
                if (alarmFrCoolCheck.Checked)                                                       // Авария фреонового охладителя
                    CheckAddUIToList("Авария фреонового охладителя", code_2, DI);
            }
            else                                                                                    // Отмена выбора охладителя
            {
                SubFromCombosUI(code_1); SubFromCombosUI(code_2);
            }
        }

        ///<summary>Изменили тип охладителя</summary>
        private void CoolTypeCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1083, code_2 = 1084;                                                    // Термостат и авария фреонового охладителя

            if (coolerCheck.Checked)                                                                // Когда выбран охладитель
            {
                if (coolTypeCombo.SelectedIndex == 0)                                               // Фреоновый охладитель
                {
                    if (thermoCoolerCheck.Checked)                                                  // Выбран термостат
                        CheckAddUIToList("Термостат фреонового охладителя", code_1, DI);
                    if (alarmFrCoolCheck.Checked)                                                   // Выбран сигнал аварии
                        CheckAddUIToList("Авария фреонового охладителя", code_2, DI);
                }
                else if (coolTypeCombo.SelectedIndex == 1)                                          // Водяной охладитель
                {
                    SubFromCombosUI(code_1); SubFromCombosUI(code_2);
                }
            }
        }

        ///<summary>Выбрали термостат фреонового охладителя</summary>
        private void ThermoCoolerCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1083;                                                                   // Термостат фреонового охладителя

            if (coolerCheck.Checked && coolTypeCombo.SelectedIndex == 0)                            // Выбран фреоновый охладитель
            { 
                if (thermoCoolerCheck.Checked)                                                      // Выбран термостат
                    CheckAddUIToList("Термостат фреонового охладителя", code_1, DI);
                else                                                                                // Отмена выбора
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Выбрали аварийный сигнал фреонового охладителя</summary>
        private void AlarmFrCoolCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1084;                                                                   // Авария фреонового охладителя

            if (coolerCheck.Checked && coolTypeCombo.SelectedIndex == 0)                            // Выбран фреоновый охладитель
            { 
                if (alarmFrCoolCheck.Checked)                                                       // Выбран сигнал аварии
                    CheckAddUIToList("Авария фреонового охладителя", code_1, DI);
                else                                                                                // Отмена выбора
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Выбрали увлажнитель</summary>
        private void HumidCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1139;                                                                   // Авария парового увлажнителя

            if (humidCheck.Checked)                                                                 // Когда выбран увлажнитель
            {
                if (humidTypeCombo.SelectedIndex == 0 && alarmHumidCheck.Checked)                   // Паровой увлажнитель и выбран аварийный сигнал
                    CheckAddUIToList("Авария парового увлажнителя", code_1, DI);
            }
            else                                                                                    // Отмена выбора увлажнителя
                SubFromCombosUI(code_1);
        }

        ///<summary>Изменили тип увлажнителя</summary>
        private void HumidTypeCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1139;                                                                   // Авария парового увлажнителя

            if (humidCheck.Checked)                                                                 // Когда выбран увлажнитель
            {
                if (humidTypeCombo.SelectedIndex == 0)                                              // Паровой увлажнитель
                {
                    if (alarmHumidCheck.Checked)
                        CheckAddUIToList("Авария парового увлажнителя", code_1, DI);
                }
                else if (humidTypeCombo.SelectedIndex == 1)                                         // Сотовый увлажнитель
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Выбрали сигнал аварии парового увлажнителя</summary>
        private void AlarmHumidCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1139;                                                                   // Авария парового увлажнителя

            if (humidCheck.Checked && humidTypeCombo.SelectedIndex == 0)                            // Выбран паровой увлажнитель
            { 
                if (alarmHumidCheck.Checked)
                    CheckAddUIToList("Авария парового увлажнителя", code_1, DI);
                else                                                                                // Отмена выбора
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Выбрали рекуператор</summary>
        private void RecupCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1090, code_2 = 1091;                                                    // Сигнал PS и аварии ПЧ для роторного рекуператора

            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)                              // Выбрали рекуператор
            {
                if (recDefPsCheck.Checked)                                                          // Выбрали сигнал PS
                    CheckAddUIToList("PS рекуператора", code_1, DI);
                if (recupTypeCombo.SelectedIndex == 0 && outSigAlarmRotRecCheck.Checked)            // Роторный рекуператор и выбран сигнал аварии
                    CheckAddUIToList("Авария роторного рекуператора", code_2, DI);
            }
            else // Отмена выбора рекуператора
            { 
                SubFromCombosUI(code_1); SubFromCombosUI(code_2);
            }
        }

        ///<summary>Изменили тип рекуператора</summary>
        private void RecupTypeCombo_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1091;                                                                   // Сигнал аварии ПЧ для роторного рекуператора

            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)                              // Выбран рекуператор
            {
                if (recupTypeCombo.SelectedIndex == 0)                                              // Роторный рекуператор
                    CheckAddUIToList("Авария роторного рекуператора", code_1, DI);
                else                                                                                // Другой тип рекуператора
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Выбрали сигнал PS рекуператора</summary>
        private void RecDefPsCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1090;                                                                   // Сигнал PS рекуператора  

            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)                              // Выбран рекуператор
            { 
                if (recDefPsCheck.Checked)                                                          // Выбрали сигнал PS
                    CheckAddUIToList("PS рекуператора", code_1, DI);
                else                                                                                // Отмена выбора сигнала PS
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Выбрали подтверждение работы насоса гликолевого рекуператора</summary>
        private void PumpGlikConfCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1095;

            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)                              // ПВ-система, выбран рекуператор
            {
                if (recupTypeCombo.SelectedIndex == 2)                                              // Гликолевый рекуператор
                {
                    if (pumpGlikConfCheck.Checked)
                        CheckAddUIToList("KPI насоса гликолевого рекуператора", code_1, DI);
                    else 
                        SubFromCombosUI(code_1);
                }
            }
        }

        ///<summary>Выбрали подтверждение работы резервного насоса гликолевого рекуператора</summary>
        private void ConfGlikResPumpCheck_signalsCheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1096;

            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)                              // ПВ-система, выбран рекуператор 
            {
                if (recupTypeCombo.SelectedIndex == 2)                                              // Гликолевый рекуператор
                {
                    if (confGlikResPumpCheck.Checked)
                        CheckAddUIToList("KPI резервного насоса гликолевого рекуператора", code_1, DI);
                    else
                        SubFromCombosUI(code_1);
                }
            }
        }

        ///<summary>Выбрали защиту по току насоса гликолевого рекуператора</summary>
        private void PumpGlikCurProtect_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1094;

            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)                              // ПВ-система, выбран рекуператор
            {
                if (recupTypeCombo.SelectedIndex == 2)                                              // Гликолевый рекуператор
                {
                    if (pumpGlikCurProtect.Checked)
                        CheckAddUIToList("Защита по току насоса гликолевого рекуператора", code_1, DI);
                    else 
                        SubFromCombosUI(code_1);
                }
            }
        }

        ///<summary>Выбрали защиту по току резервного насоса гликолевого рекуператора</summary>
        private void PumpGlikResCurProtect_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1097;

            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)                              // ПВ-система, выбран рекуператор  
            {
                if (recupTypeCombo.SelectedIndex == 2)                                              // Гликолевый рекуператор
                {
                    if (pumpGlikResCurProtect.Checked)
                        CheckAddUIToList("Защита по току резервного насоса рекуператора", code_1, DI);
                    else 
                        SubFromCombosUI(code_1);
                }
            }
        }

        ///<summary>Выбрали сигнал аварии роторного рекуператора</summary>
        private void OutSigAlarmRotRecCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1091;

            // ПВ-система и выбран роторный рекуператор
            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked && recupTypeCombo.SelectedIndex == 0)
            {
                if (outSigAlarmRotRecCheck.Checked)
                    CheckAddUIToList("Авария роторного рекуператора", code_1, DI);
                else 
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Изменили тип запуска</summary>
        private void StopStartCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ushort
                code_1 = 1001,                                                                      // Переключатель/кнопка "СТОП/ПУСК"
                code_2 = 1002;                                                                      // Кнопка "СТОП"

            CommandWord_1();                                                                        // Определение командного слова

            if (ignoreEvents) return;

            SubFromCombosUI(code_1); SubFromCombosUI(code_2);                                       // Удаление сигналов

            if (stopStartCombo.SelectedIndex == 0)                                                  // Переключатель
            {
                CheckAddUIToList("Переключатель \"СТОП/ПУСК\"", code_1, DI);
            }
            else if (stopStartCombo.SelectedIndex == 1)                                             // Кнопка "СТОП/ПУСК" 
            {
                CheckAddUIToList("Импульсная кнопка \"СТОП/ПУСК\"", code_1, DI);
            }
            else if (stopStartCombo.SelectedIndex == 2)                                             // Кнопки "ПУСК" и "СТОП"
            {
                CheckAddUIToList("Кнопка \"ПУСК\"", code_1, DI);                                    // Кнопка "ПУСК"
                CheckAddUIToList("Кнопка \"СТОП\"", code_2, DI);                                    // Кнопка "СТОП"
            }
        }

        ///<summary>Выбрали сигнал аварии для приточного вентилятора</summary> 
        private void PrFanAlarmCheck_signalsDICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1012, code_2 = 1023;                                                    // Сигнал аварии 1 и 2

            if (prFanAlarmCheck.Checked)                                                            // Выбрали сигнал аварии
            {
                CheckAddUIToList("Сигнал аварии приточного вентилятора 1", code_1, DI);
                if (checkResPrFan.Checked)                                                          // Выбран резерв приточного вентилятора
                    CheckAddUIToList("Сигнал аварии приточного вентилятора 2", code_2, DI);
            }
            else                                                                                    // Отмена выбора сигнала аварии
            {
                SubFromCombosUI(code_1); SubFromCombosUI(code_2);
            }
        }

        ///<summary>Выбрали сигнал аварии для вытяжного вентилятора</summary>
        private void OutFanAlarmCheck_CheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1045, code_2 = 1056;                                                    // Сигнал аварии 1 и 2
            
            if (outFanCheck.Checked && outFanAlarmCheck.Checked)                                    // В вентилятор и выбрали сигнал аварии                                                 
            {
                CheckAddUIToList("Сигнал аварии вытяжного вентилятора 1", code_1, DI);
                if (checkResOutFan.Checked)                                                         // Выбран резерв вытяжного вентилятора
                    CheckAddUIToList("Сигнал аварии вытяжного вентилятора 2", code_2, DI);
            }
            else                                                                                    // Отмена выбора сигнала аварии
            {
                SubFromCombosUI(code_1); SubFromCombosUI(code_2);
            }
        }

        ///<summary>Выбрали сигнал пожарной сигнализации</summary>
        private void FireCheck_signalsDISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1098;                                                                   // Сигнал пожарной сигнализации

            if (fireCheck.Checked)                                                                  // Выбран сигнал
                CheckAddUIToList("Сигнал пожарной сигнализации", code_1, DI);
            else                                                                                    // Отмена выбора сигнала пожарной сигнализации
                SubFromCombosUI(code_1);
        }
    }
}