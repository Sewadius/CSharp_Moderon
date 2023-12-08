using System;
using System.Windows.Forms;

namespace Moderon
{
    public partial class Form1 : Form
    {
        ///<summary>Выбрали основной нагреватель</summary>
        private void HeaterCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 9;                                                                          // Датчик обратной воды калорифера

            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0)                                // Водяной калорифер
                CheckAddUIToList("Датчик обратной воды калорифера", code_1, NTC);
            else // Отмена выбора нагревателя
                SubFromCombosUI(code_1);
        }

        ///<summary>Изменили тип основного нагревателя</summary>
        private void HeatTypeCombo_signalsAISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 9;                                                                          // Датчик обратной воды калорифера

            if (heaterCheck.Checked)                                                                    // Когда выбран нагреватель
            {
                if (heatTypeCombo.SelectedIndex == 0)                                                   // Водяной калорифер
                    CheckAddUIToList("Датчик обратной воды калорифера", code_1, NTC);
                else if (heatTypeCombo.SelectedIndex == 1)                                              // Электрокалорифер
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Выбрали догреватель, второй нагреватель</summary>
        private void AddHeatCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 11;                                                                         // Датчик обратной воды догревателя

            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0)                            // Когда выбран водяной догреватель
                CheckAddUIToList("Датчик обратной воды догревателя", code_1, NTC);
            else                                                                                        // Отмена выбора догревателя
                SubFromCombosUI(code_1);
        }

        ///<summary>Изменили тип догревателя</summary>
        private void HeatAddTypeCombo_signalsAISelectedIndexChanged(object sender, EventArgs e)
        {
            ushort code_1 = 11;                                                                         // Датчик обратной воды догревателя

            if (addHeatCheck.Checked)                                                                   // Когда выбран догреватель
            {
                if (heatAddTypeCombo.SelectedIndex == 0)                                                // Водяной догреватель
                    CheckAddUIToList("Датчик обратной воды догревателя", code_1, NTC);
                else if (heatAddTypeCombo.SelectedIndex == 1)                                           // Электрокалорифер
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Выбрали рекуператор</summary>
        private void RecupCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 14;                                                                         // Датчик температуры защиты рекуператора

            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)                                  // Рекуператор
            {
                if (recDefTempCheck.Checked)                                                            // Выбрана защита по температурному датчику
                    CheckAddUIToList("Датчик температуры защиты рекуператора", code_1, NTC);
            }
            else                                                                                        // Отмена выбора рекуператора
                SubFromCombosUI(code_1);
        }

        ///<summary>Выбрали температурный датчик защиты рекуператора</summary>
        private void RecDefTempCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 14;                                                                         // Датчик температуры защиты рекуператора

            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked)                                  // ПВ и рекуператор
            {
                if (recDefTempCheck.Checked)                                                            // Выбрали защиту по температурному датчику
                    CheckAddUIToList("Датчик температуры защиты рекуператора", code_1, NTC);
                else // Отмена выбора датчика
                    SubFromCombosUI(code_1);
            }
        }

        ///<summary>Выбрали канальный датчик температуры</summary>
        private void PrChanSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 1;                                                                          // Канальный датчик температуры

            if (prChanSensCheck.Checked)                                                                // Выбрали датчик 
                CheckAddUIToList("Канальный датчик Т приточного воздуха", code_1, NTC);
            else                                                                                        // Отмена выбора датчика
                SubFromCombosUI(code_1);
        }

        ///<summary>Выбрали комнатный датчик температуры</summary>
        private void RoomTempSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 4;                                                                          // Комнатный датчик температуры

            if (roomTempSensCheck.Checked)                                                              // Выбрали датчик 
                CheckAddUIToList("Датчик температуры комнатный", code_1, NTC);
            else                                                                                        // Отмена выбора датчика
                SubFromCombosUI(code_1);
        }

        ///<summary>Выбрали канальный датчик влажности</summary>
        private void ChanHumSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 17;                                                                         // Канальный датчик влажности

            if (chanHumSensCheck.Checked)                                                               // Выбрали датчик 
                CheckAddUIToList("Канальный датчик влажности", code_1, NTC);
            else                                                                                        // Отмена выбора датчика
                SubFromCombosUI(code_1);
        }

        ///<summary>Выбрали комнатный датчик влажности</summary>
        private void RoomHumSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 18;                                                                         // Комнатный датчик влажности

            if (roomHumSensCheck.Checked)                                                               // Выбрали датчик 
                CheckAddUIToList("Комнатный датчик влажности", code_1, NTC);
            else                                                                                        // Отмена выбора датчика
                SubFromCombosUI(code_1);
        }

        ///<summary>Выбрали датчик температуры наружного воздуха</summary>
        private void OutdoorChanSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 3;                                                                          // Датчик температуры наружного воздуха

            if (outdoorChanSensCheck.Checked)                                                           // Выбрали датчик 
                CheckAddUIToList("Датчик температуры наружного воздуха", code_1, NTC);
            else                                                                                        // Отмена выбора датчика
                SubFromCombosUI(code_1);
        }

        ///<summary>Выбрали канальный датчик Т вытяжного воздуха</summary>
        private void OutChanSensCheck_signalsAICheckedChanged(object sender, EventArgs e)
        {
            ushort code_1 = 5;                                                                          // Канальный датчик температуры вытяжного воздуха

            if (outChanSensCheck.Checked)                                                               // Выбрали датчик 
                CheckAddUIToList("Канальный датчик Т вытяжного воздуха", code_1, NTC);
            else // Отмена выбора датчика
                SubFromCombosUI(code_1);
        }
    }
}