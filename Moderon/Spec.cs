using GemBox.Spreadsheet;
using System;
using System.IO;
using System.Windows.Forms;

// Файл для формирования файла спецификации Excel

namespace Moderon
{
    partial class Form1 : Form
    {
        int posSpec = 2; // Позиция в файле по строчке

        // Счетчики для подсчета количества ПЧ
        // ПЧ 220 В без Modbus, A150
        int A150_21_04h = 0; // 0.4 кВт
        int A150_21_075h = 0; // 0.75 кВт
        int A150_21_11n = 0; // 1.1 кВт 
        int A150_21_15n = 0; // 1.5 кВт
        int A150_21_22n = 0; // 2.2 кВт

        // ПЧ 220 В с Modbus, A150
        int A150_21_04h_ec = 0; // 0.4 кВт
        int A150_21_075h_ec = 0; // 0.75 кВт
        int A150_21_11n_ec = 0; // 1.1 кВт
        int A150_21_15n_ec = 0; // 1.5 кВт
        int A150_21_22n_ec = 0; // 2.2 кВт

        // ПЧ 380 В без Modbus, A150
        int A150_33_075ht = 0; // 0.75 кВт
        int A150_33_15nt = 0; // 1.5 кВт
        int A150_33_22nt = 0; // 2.2 кВт
        int A150_33_37nt = 0; // 3.7 кВт
        int A150_33_55nt = 0; // 5.5 кВт
        int A150_33_75nt = 0; // 7.5 кВт
        int A150_33_11t = 0; // 11 кВт
        int A150_33_15t = 0; // 15 кВт

        // ПЧ 380 В c Modbus, A150
        int A150_33_075ht_ec = 0; // 0.75 кВт
        int A150_33_15nt_ec = 0; // 1.5 кВт
        int A150_33_22nt_ec = 0; // 2.2 кВт
        int A150_33_37nt_ec = 0; // 3.7 кВт
        int A150_33_55nt_ec = 0; // 5.5 кВт
        int A150_33_75nt_ec = 0; // 7.5 кВт
        int A150_33_11t_ec = 0; // 11 кВт
        int A150_33_15t_ec = 0; // 15 кВт

        // Датчики температуры и влажности
        int humidSensorTotal_0_10 = 0; // Датчики влажности 0-10 В
        int humidSensorTotal_4_20 = 0; // Датчики влажности 4_20 мА
        int chanSensorTotal_ntc = 0; // Канальный датчик температуры, NTC
        int chanSensorTotal_pt = 0; // Канальный датчик температуры, Pt1000
        int roomSensorTotal_ntc = 0; // Комнатный датчик температуры, NTC
        int roomSensorTotal_pt = 0; // Комнатный датчик температуры, Pt1000
        int outSensorTotal_ntc = 0; // Датчик наружного воздуха, NTC
        int outSensorTotal_pt = 0; // Датчик наружного воздуха, Pt1000
        int watSensorTotal_pt = 0; // Погружной датчик температуры, Pt1000
        int sleeveForSensorTotal = 0; // Гильза для погружного датчика температуры
        int thermoTSTotal = 0; // Воздушный термостат
        int PSfilterTotal = 0; // PS для воздушного фильтра
        int PSfanTotal = 0; // PS для вентилятора

        // Привода воздушных заслонок
        // 24 В с подтверждением открытия и с возвратной пружиной
        int oda_03_d_024_sa_total = 0;
        int oda_05_d_024_sa_total = 0;
        int oda_10_d_024_sa_total = 0;
        int oda_15_d_024_sa_total = 0;
        // 24 В с подтверждением открытия и без возвратной пружины
        int oda_08_d_024_na_total = 0;
        int oda_16_d_024_na_total = 0;
        int oda_24_d_024_na_total = 0;
        int oda_40_d_024_na_total = 0;
        // 24 В без подтверждения открытия и с возвратной пружиной
        int oda_03_d_024_s_total = 0;
        int oda_05_d_024_s_total = 0;
        // 24 В без подтверждения открытия и без возвратной пружины
        int oda_02_d_024_n_total = 0;
        int oda_04_d_024_n_total = 0;
        int oda_06_d_024_n_total = 0;
        // 220 В с подтверждением открытия и с возвратной пружиной
        int oda_03_d_230_sa_total = 0;
        int oda_05_d_230_sa_total = 0;
        int oda_10_d_230_sa_total = 0;
        int oda_15_d_230_sa_total = 0;
        // 220 В с подтверждением открытия и без возвратной пружины
        int oda_08_d_230_na_total = 0;
        int oda_16_d_230_na_total = 0;
        int oda_24_d_230_na_total = 0;
        int oda_40_d_230_na_total = 0;
        // 220 В без подтверждения открытия и с возвратной пружиной
        int oda_03_d_230_s_total = 0;
        int oda_05_d_230_s_total = 0;
        // 220 В без подтверждения открытия и без возвратной пружины
        int oda_02_d_230_n_total = 0;
        int oda_04_d_230_n_total = 0;
        int oda_06_d_230_n_total = 0;

        // *** Артикулы для оборудования ***
        // ПЛК и блоки расширения
        readonly string plc_spec_art = "HVAC-CPU21LCD2110";
        readonly string bl_spec_art = "HVAC-EXP21DRA";
        readonly string plcConnect_spec_art = "HVAC-CPUCKIT1";
        readonly string blConnect1_spec_art = "HVAC-EXPCKIT2";
        readonly string blConnect2_spec_art = "HVAC-EXPCKIT3";
        // ПЧ без Modbus
        // ПЧ 220 В без Modbus, A150
        readonly string a150_21_04h_art = "A150-21-04H"; // 0.4 кВт
        readonly string a150_21_075h_art = "A150-21-075H"; // 0.75 кВт
        readonly string a150_21_11n_art = "A150-21-11N"; // 1.1 кВт
        readonly string a150_21_15n_art = "A150-21-15N"; // 1.5 кВт
        readonly string a150_21_22n_art = "A150-21-22N"; // 2.2 кВт
        // ПЧ 380 В без Modbus, A150
        readonly string a150_33_075ht_art = "A150-33-075HT"; // 0.75 кВт
        readonly string a150_33_15nt_art = "A150-33-15NT"; // 1.5 кВт
        readonly string a150_33_22nt_art = "A150-33-22NT"; // 2.2 кВт
        readonly string a150_33_37nt_art = "A150-33-37NT"; // 3.7 кВт
        readonly string a150_33_55nt_art = "A150-33-55NT"; // 5.5 кВт
        readonly string a150_33_75nt_art = "A150-33-75NT"; // 7.5 кВт
        readonly string a150_33_11t_art = "A150-33-11T"; // 11 кВт
        readonly string a150_33_15t_art = "A150-33-15T"; // 15 кВт
        // ПЧ с Modbus
        // ПЧ 220 В с Modbus, A150
        readonly string a150_21_04h_ec_art = "A150-21-04H + EC-A150-485"; // 0.4 кВт
        readonly string a150_21_075h_ec_art = "A150-21-075H + EC-A150-485"; // 0.75 кВт
        readonly string a150_21_11n_ec_art = "A150-21-11N + EC-A150-485"; // 1.1 кВт
        readonly string a150_21_15n_ec_art = "A150-21-15N + EC-A150-485"; // 1.5 кВт
        readonly string a150_21_22n_ec_art = "A150-21-22N + EC-A150-485"; // 2.2 кВт
        // ПЧ 380 В с Modbus, A150
        readonly string a150_33_075ht_ec_art = "A150-33-075HT + EC-A150-485"; // 0.75 кВт
        readonly string a150_33_15nt_ec_art = "A150-33-15NT + EC-A150-485"; // 1.5 кВт
        readonly string a150_33_22nt_ec_art = "A150-33-22NT + EC-A150-485"; // 2.2 кВт
        readonly string a150_33_37nt_ec_art = "A150-33-37NT + EC-A150-485"; // 3.7 кВт
        readonly string a150_33_55nt_ec_art = "A150-33-55NT + EC-A150-485"; // 5.5 кВт
        readonly string a150_33_75nt_ec_art = "A150-33-75NT + EC-A150-485"; // 7.5 кВт
        readonly string a150_33_11t_ec_art = "A150-33-75NT + EC-A150-485"; // 11 кВт
        readonly string a150_33_15t_ec_art = "A150-33-15T + EC-A150-485"; // 15 кВт

        // Датчики температуры и влажности
        readonly string chanSensor_ntc_art = "TSD-2-NTC10K-200"; // Канальный датчик температуры, NTC
        readonly string chanSensor_pt_art = "TSD-2-PT1000-200"; // Канальный датчик температуры, Pt1000
        readonly string roomSensor_ntc_art = "TSI-1-NTC10K"; // Комнатный датчик температуры, NTC
        readonly string roomSensor_pt_art = "TSI-1-PT1000"; // Комнатный датчик температуры, Pt1000
        readonly string outSensor_ntc_art = "TSO-1-NTC10K"; // Наружный датчик температуры, NTC
        readonly string outSensor_pt_art = "TSO-1-PT1000"; // Наружный датчик температуры, Pt1000
        readonly string watSensor_pt_art = "TSD-1-PT1000-050"; // Погружной датчик температуры, Pt1000
        readonly string sleeveForSensor_pt_art = "IPB-1-050"; // Гильза для погружного датчика
        readonly string thermoTS_art = "FPT-1-300"; // Воздушный термостат
        readonly string psFilter_art = "RDD-02-500"; // PS для воздушного фильтра
        readonly string psFan_art = "RDD-02-2500"; // PS для вентилятора

        // Привода воздушных заслонок
        // 24 В с подтверждением открытия и с возвратной пружиной
        readonly string oda_03_d_024_sa_art = "ODA-15-D-024-S-A"; // 3 Нм
        readonly string oda_05_d_024_sa_art = "ODA-05-D-024-S-A"; // 5 Нм
        readonly string oda_10_d_024_sa_art = "ODA-10-D-024-S-A"; // 10 Нм
        readonly string oda_15_d_024_sa_art = "ODA-15-D-024-S-A"; // 15 Нм
        // 24 В с подтверждением открытия и без возвратной пружины
        readonly string oda_08_d_024_na_art = "ODA-08-D-024-N-A"; // 8 Нм
        readonly string oda_16_d_024_na_art = "ODA-16-D-024-N-A"; // 16 Нм
        readonly string oda_24_d_024_na_art = "ODA-24-D-024-N-A"; // 24 Нм
        readonly string oda_40_d_024_na_art = "ODA-40-D-024-N-A"; // 40 Нм
        // 24 В без подтверждения открытия и с возвратной пружиной
        readonly string oda_03_d_024_s_art = "ODA-03-D-024-S"; // 3 Нм
        readonly string oda_05_d_024_s_art = "ODA-05-D-024-S"; // 5 Нм
        // 24 В без подтверждения открытия и без возвратной пружины
        readonly string oda_02_d_024_n_art = "ODA-02-D-024-N"; // 2 Нм
        readonly string oda_04_d_024_n_art = "ODA-04-D-024-N"; // 4 Нм
        readonly string oda_06_d_024_n_art = "ODA-06-D-024-N"; // 6 Нм
        // 220 В с подтверждением открытия и с возвратной пружиной
        readonly string oda_03_d_230_sa_art = "ODA-03-D-230-S-A"; // 3 Нм
        readonly string oda_05_d_230_sa_art = "ODA-05-D-230-S-A"; // 5 Нм
        readonly string oda_10_d_230_sa_art = "ODA-10-D-230-S-A"; // 10 Нм
        readonly string oda_15_d_230_sa_art = "ODA-15-D-230-S-A"; // 15 Нм
        // 220 В с подтверждением открытия и без возвратной пружины
        readonly string oda_08_d_230_na_art = "ODA-08-D-230-N-A"; // 8 Нм
        readonly string oda_16_d_230_na_art = "ODA-16-D-230-N-A"; // 16 Нм
        readonly string oda_24_d_230_na_art = "ODA-24-D-230-N-A"; // 24 Нм
        readonly string oda_40_d_230_na_art = "ODA-40-D-230-N-A"; // 40 Нм
        // 220 В без потверждения открытия и с возвратной пружиной
        readonly string oda_03_d_230_s_art = "ODA-03-D-230-S"; // 3 Нм
        readonly string oda_05_d_230_s_art = "ODA-05-D-230-S"; // 5 Нм
        // 220 В без подтверждения открытия и без возвратной пружины
        readonly string oda_02_d_230_n_art = "ODA-02-D-230-N"; // 2 Нм
        readonly string oda_04_d_230_n_art = "ODA-04-D-230-N"; // 4 Нм
        readonly string oda_06_d_230_n_art = "ODA-06-D-230-N"; // 6 Нм

        // *** Наименование в прайсе ***
        // ПЛК и блоки расширения
        readonly string plc_spec_name = "HVAC ПЛК ЦПУ 5DI 7DO 6AI 3AO LCD 2хRS485 1xEthernet 1Мб ONI";
        readonly string bl_spec_name = "HVAC ПЛК модуль расширения 5DI 7DO 6AI 3AO ONI";
        readonly string plcConnect_spec_name = "HVAC ПЛК комплект разъемов 1 для ЦПУ ONI";
        readonly string blConnect1_spec_name = "HVAC ПЛК комплект разъемов 2 для модуля расширения ONI";
        readonly string blConnect2_spec_name = "HVAC ПЛК комплект разъемов 3 для модуля расширения ONI";
        // ПЧ 220 В без Modbus, A150
        readonly string a150_21_04h_name = "ПЧ A150 220В 1Ф 0,4кВт 3А ONI"; // 0.4 кВт
        readonly string a150_21_075h_name = "ПЧ A150 220В 1Ф 0,75кВт 5А ONI"; // 0.75 кВт
        readonly string a150_21_11n_name = "ПЧ A150 220В 1Ф 1,1кВт 6А ONI"; // 1.1 кВт
        readonly string a150_21_15n_name = "ПЧ A150 220В 1Ф 1,5кВт 7А ONI"; // 1.5 кВт
        readonly string a150_21_22n_name = "ПЧ A150 220В 1Ф 2,2кВт 10А ONI"; // 2.2 кВт
        // ПЧ 380 В без Modbus, A150
        readonly string a150_33_075ht_name = "ПЧ A150 380В 3Ф 0,75кВт 3А встр. торм ONI"; // 0.75 кВт
        readonly string a150_33_15nt_name = "ПЧ A150 380В 3Ф 1,5кВт 5А встр. торм ONI"; // 1.5 кВт
        readonly string a150_33_22nt_name = "ПЧ A150 380В 3Ф 2,2кВт 6А встр. торм ONI"; // 2.2 кВт
        readonly string a150_33_37nt_name = "ПЧ A150 380В 3Ф 3,7кВт 10А встр. торм ONI"; // 3.7 кВт
        readonly string a150_33_55nt_name = "ПЧ A150 380В 3Ф 5,5кВт 13А встр. торм ONI"; // 5.5 кВт
        readonly string a150_33_75nt_name = "ПЧ A150 380В 3Ф 7,5кВт 17А встр. торм ONI"; // 7.5 кВт
        readonly string a150_33_11t_name = "ПЧ A150 380В 3Ф 11кВт 25А встр. торм ONI"; // 11 кВт
        readonly string a150_33_15t_name = "ПЧ A150 380В 3Ф 15кВт 32А встр. торм ONI"; // 15 кВт

        // ПЧ 220 В с Modbus, A150
        readonly string a150_21_04h_ec_name = "ПЧ A150 220В 1Ф 0,4кВт 3А ONI + Плата расширения вх/вых. RS 485 Modbus ONI"; // 0.4 кВт
        readonly string a150_21_075h_ec_name = "ПЧ A150 220В 1Ф 0,75кВт 5А ONI + Плата расширения вх/вых. RS 485 Modbus ONI"; // 0.75 кВт
        readonly string a150_21_11n_ec_name = "ПЧ A150 220В 1Ф 1,1кВт 6А ONI + Плата расширения вх/вых. RS 485 Modbus ONI"; // 1.1 кВт
        readonly string a150_21_15n_ec_name = "ПЧ A150 220В 1Ф 1,5кВт 7А ONI + Плата расширения вх/вых. RS 485 Modbus ONI"; // 1.5 кВт
        readonly string a150_21_22n_ec_name = "ПЧ A150 220В 1Ф 2,2кВт 10А ONI + Плата расширения вх/вых. RS 485 Modbus ONI"; // 2.2 кВт
        // ПЧ 380 В с Modbus, A150
        readonly string a150_33_075ht_ec_name = "ПЧ A150 380В 3Ф 0,75кВт 3А встр. торм ONI + Плата расширения вх/вых. RS 485 Modbus ONI"; // 0.75 кВт
        readonly string a150_33_15nt_ec_name = "ПЧ A150 380В 3Ф 1,5кВт 5А встр. торм ONI + Плата расширения вх/вых. RS 485 Modbus ONI";  // 1.5 кВт
        readonly string a150_33_22nt_ec_name = "ПЧ A150 380В 3Ф 2,2кВт 6А встр. торм ONI + Плата расширения вх/вых. RS 485 Modbus ONI"; // 2.2 кВт
        readonly string a150_33_37nt_ec_name = "ПЧ A150 380В 3Ф 3,7кВт 10А встр. торм ONI + Плата расширения вх/вых. RS 485 Modbus ONI"; // 3.7 кВт
        readonly string a150_33_55nt_ec_name = "ПЧ A150 380В 3Ф 5,5кВт 13А встр. торм ONI + Плата расширения вх/вых. RS 485 Modbus ONI"; // 5.5 кВт
        readonly string a150_33_75nt_ec_name = "ПЧ A150 380В 3Ф 7,5кВт 17А встр. торм ONI + Плата расширения вх/вых. RS 485 Modbus ONI"; // 7.5 кВт
        readonly string a150_33_11t_ec_name = "ПЧ A150 380В 3Ф 11кВт 25А встр. торм ONI + Плата расширения вх/вых. RS 485 Modbus ONI"; // 11 кВт
        readonly string a150_33_15t_ec_name = "ПЧ A150 380В 3Ф 15кВт 32А встр. торм ONI + Плата расширения вх/вых. RS 485 Modbus ONI"; // 15 кВт

        // Датчики температуры и влажности
        readonly string humidSensor_4_20_name = "Датчик влажности 4-20 мА";
        readonly string humidSensor_0_10_name = "Датчик влажности 0-10 В";
        readonly string chanSensor_ntc_name = "Датчик температуры погружной NTC10K L= 200мм ONI";
        readonly string chanSensor_pt_name = "Датчик температуры погружной PT1000 L= 200мм ONI";
        readonly string roomSensor_ntc_name = "Датчик температуры для помещений NTC10K ONI";
        readonly string roomSensor_pt_name = "Датчик температуры для помещений PT1000 ONI";
        readonly string outSensor_ntc_name = "Датчик температуры для помещений NTC10K ONI";
        readonly string outSensor_pt_name = "Датчик температуры для помещений PT1000 ONI";
        readonly string watSensor_pt_name = "Датчик температуры погружной PT1000 L= 50мм ONI";
        readonly string sleeveForSensor_pt_name = "Гильза датчика температуры латунная L=50мм ONI";
        readonly string thermoTS_name = "Термостат защиты от замерзания механический L=3м ONI";
        readonly string psFilter_name = "Реле дифференциального давления 50-500Па ONI";
        readonly string psFan_name = "Реле дифференциального давления 500-2500Па ONI";

        // Привода воздушных заслонок
        // 24 В с подтверждением открытия и с возвратной пружиной
        readonly string oda_03_d_024_sa_name = "Привод заслонки 3Нм 24В AC/DC 2 позиционный с возвратной пружиной с дополнительным переключателем ONI"; // 3 Нм
        readonly string oda_05_d_024_sa_name = "Привод заслонки 5Нм 24В AC/DC 2 позиционный с возвратной пружиной с дополнительным переключателем ONI"; // 5 Нм
        readonly string oda_10_d_024_sa_name = "Привод заслонки 10Нм 24В AC/DC 2 позиционный с возвратной пружиной с дополнительным переключателем ONI"; // 10 Нм
        readonly string oda_15_d_024_sa_name = "Привод заслонки 15Нм 24В AC/DC 2 позиционный с возвратной пружиной с дополнительным переключателем ONI"; // 15 Нм
        // 24 В с подтверждением открытия и без возвратной пружины
        readonly string oda_08_d_024_na_name = "Привод заслонки 8Нм 24В AC/DC 2/3 позиционный без возвратной пружины с дополнительным переключателем ONI"; // 8 Нм
        readonly string oda_16_d_024_na_name = "Привод заслонки 16Нм 24В AC/DC 2/3 позиционный без возвратной пружины с дополнительным переключателем ONI"; // 16 Нм
        readonly string oda_24_d_024_na_name = "Привод заслонки 24Нм 24В AC/DC 2/3 позиционный без возвратной пружины с дополнительным переключателем ONI"; // 24 Нм
        readonly string oda_40_d_024_na_name = "Привод заслонки 40Нм 24В AC/DC 2/3 позиционный без возвратной пружины с дополнительным переключателем ONI"; // 40 Нм
        // 24 В без подтверждения открытия и с возвратной пружиной
        readonly string oda_03_d_024_s_name = "Привод заслонки 3Нм 24В AC/DC 2 позиционный с возвратной пружиной ONI"; // 3 Нм
        readonly string oda_05_d_024_s_name = "Привод заслонки 5Нм 24В AC/DC 2 позиционный с возвратной пружиной ONI"; // 5 Нм
        // 24 В без подтверждения открытия и без возвратной пружины
        readonly string oda_02_d_024_n_name = "Привод заслонки 2Нм 24В AC/DC 2/3 позиционный без возвратной пружины ONI"; // 2 Нм
        readonly string oda_04_d_024_n_name = "Привод заслонки 4Нм 24В AC/DC 2/3 позиционный без возвратной пружины ONI"; // 4 Нм
        readonly string oda_06_d_024_n_name = "Привод заслонки 6Нм 24В AC/DC 2/3 позиционный без возвратной пружины ONI"; // 6 Нм
        // 220 В с подтверждением открытия и с возвратной пружиной
        readonly string oda_03_d_230_sa_name = "Привод заслонки 3Нм 230В AC 2 позиционный с возвратной пружиной с дополнительным переключателем ONI"; // 3 Нм
        readonly string oda_05_d_230_sa_name = "Привод заслонки 5Нм 230В AC 2 позиционный с возвратной пружиной с дополнительным переключателем ONI"; // 5 Нм
        readonly string oda_10_d_230_sa_name = "Привод заслонки 10Нм 230В AC 2 позиционный с возвратной пружиной с дополнительным переключателем ONI"; // 10 Нм
        readonly string oda_15_d_230_sa_name = "Привод заслонки 15Нм 230В AC 2 позиционный с возвратной пружиной с дополнительным переключателем ONI"; // 15 Нм
        // 220 В с подтверждением открытия и без возвратной пружины
        readonly string oda_08_d_230_na_name = "Привод заслонки 8Нм 230В AC 2/3 позиционный без возвратной пружины с дополнительным переключателем ONI"; // 8 Нм
        readonly string oda_16_d_230_na_name = "Привод заслонки 16Нм 230В AC 2/3 позиционный без возвратной пружины с дополнительным переключателем ONI"; // 16 Нм
        readonly string oda_24_d_230_na_name = "Привод заслонки 24Нм 230В AC 2/3 позиционный без возвратной пружины с дополнительным переключателем ONI"; // 24 Нм
        readonly string oda_40_d_230_na_name = "Привод заслонки 40Нм 230В AC 2/3 позиционный без возвратной пружины с дополнительным переключателем ONI"; // 40 Нм
        // 220 В без подтверждения открытия и с возвратной пружиной
        readonly string oda_03_d_230_s_name = "Привод заслонки 3Нм 230В AC 2 позиционный с возвратной пружиной ONI"; // 3 Нм
        readonly string oda_05_d_230_s_name = "Привод заслонки 5Нм 230В AC 2 позиционный с возвратной пружиной ONI"; // 5 Нм
        // 220 В без подтверждения открытия и без возвратной пружины
        readonly string oda_02_d_230_n_name = "Привод заслонки 2Нм 230В AC/DC 2/3 позиционный без возвратной пружины ONI"; // 2 Нм
        readonly string oda_04_d_230_n_name = "Привод заслонки 4Нм 230В AC/DC 2/3 позиционный без возвратной пружины ONI"; // 4 Нм
        readonly string oda_06_d_230_n_name = "Привод заслонки 6Нм 230В AC/DC 2/3 позиционный без возвратной пружины ONI"; // 6 Нм

        ///<summary>Обнуление количества для оборудования</summary>
        private void ResetNumEquip()
        {
            // ПЧ 220 В без Modbus, A150
            A150_21_04h = 0; // 0.4 кВт
            A150_21_075h = 0; // 0.75 кВт
            A150_21_11n = 0; // 1.1 кВт 
            A150_21_15n = 0; // 1.5 кВт
            A150_21_22n = 0; // 2.2 кВт

            // ПЧ 220 В с Modbus, A150
            A150_21_04h_ec = 0; // 0.4 кВт
            A150_21_075h_ec = 0; // 0.75 кВт
            A150_21_11n_ec = 0; // 1.1 кВт
            A150_21_15n_ec = 0; // 1.5 кВт
            A150_21_22n_ec = 0; // 2.2 кВт

            // ПЧ 380 В без Modbus, A150
            A150_33_075ht = 0; // 0.75 кВт
            A150_33_15nt = 0; // 1.5 кВт
            A150_33_22nt = 0; // 2.2 кВт
            A150_33_37nt = 0; // 3.7 кВт
            A150_33_55nt = 0; // 5.5 кВт
            A150_33_75nt = 0; // 7.5 кВт
            A150_33_11t = 0; // 11 кВт
            A150_33_15t = 0; // 15 кВт

            // ПЧ 380 В c Modbus, A150
            A150_33_075ht_ec = 0; // 0.75 кВт
            A150_33_15nt_ec = 0; // 1.5 кВт
            A150_33_22nt_ec = 0; // 2.2 кВт
            A150_33_37nt_ec = 0; // 3.7 кВт
            A150_33_55nt_ec = 0; // 5.5 кВт
            A150_33_75nt_ec = 0; // 7.5 кВт
            A150_33_11t_ec = 0; // 11 кВт
            A150_33_15t_ec = 0; // 15 кВт

            // Датчики температуры и влажности
            humidSensorTotal_0_10 = 0;
            humidSensorTotal_4_20 = 0;
            chanSensorTotal_ntc = 0;
            chanSensorTotal_pt = 0;
            roomSensorTotal_ntc = 0;
            roomSensorTotal_pt = 0;
            outSensorTotal_ntc = 0;
            outSensorTotal_pt = 0;
            watSensorTotal_pt = 0;
            sleeveForSensorTotal = 0;
            thermoTSTotal = 0;
            PSfilterTotal = 0;
            PSfanTotal = 0;

            // Привода воздушных заслонок
            // Привода на 24 В
            oda_03_d_024_sa_total = 0;
            oda_05_d_024_sa_total = 0;
            oda_10_d_024_sa_total = 0;
            oda_15_d_024_sa_total = 0;
            oda_08_d_024_na_total = 0;
            oda_16_d_024_na_total = 0;
            oda_24_d_024_na_total = 0;
            oda_40_d_024_na_total = 0;
            oda_03_d_024_s_total = 0;
            oda_05_d_024_s_total = 0;
            oda_02_d_024_n_total = 0;
            oda_04_d_024_n_total = 0;
            oda_06_d_024_n_total = 0;
            // Привода на 220 В
            oda_03_d_230_sa_total = 0;
            oda_05_d_230_sa_total = 0;
            oda_10_d_230_sa_total = 0;
            oda_15_d_230_sa_total = 0;
            oda_08_d_230_na_total = 0;
            oda_16_d_230_na_total = 0;
            oda_24_d_230_na_total = 0;
            oda_40_d_230_na_total = 0;
            oda_03_d_230_s_total = 0;
            oda_05_d_230_s_total = 0;
            oda_02_d_230_n_total = 0;
            oda_04_d_230_n_total = 0;
            oda_06_d_230_n_total = 0;
        }

        ///<summary>Сохранение спецификации в Excel, только когда распределены сигналы</summary> 
        private void SaveSpecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filePath = Directory.GetCurrentDirectory(); // Для шаблона
            var savePath = Path.GetTempPath() + "/Specification.xlsx"; // Во временную папку
            SaveFileDialog dlg = new SaveFileDialog(); // Окно для сохранения файла
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            var workbook = ExcelFile.Load(filePath + "/TemplateSpec.xls");
            var worksheet = workbook.Worksheets[0];
            // Добавление информации в файл спецификации
            posSpec = 2; // Возврат начального значения
            ResetNumEquip(); // Обнуление количества для позиций оборудования
            Spec_PLC(worksheet); // ПЛК и блоки расширения
            Spec_FreqConv(worksheet); // Частотные преобразователи (пока что А150)
            Spec_TempSens(worksheet); // Датчики температуры, PS, термостат
            Spec_DamperDrive(worksheet); // Привода воздушных заслонок
            dlg.FileName = "Спецификация";
            dlg.DefaultExt = ".xlsx";
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // "Документы"
            dlg.Filter = "Excel table (.xlsx)|*.xlsx";
            workbook.Save(savePath); // Сохранение во временную папку
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(dlg.FileName, File.ReadAllBytes(savePath));
            }
        }

        ///<summary>Добавление информации о ПЛК и блоках расширения</summary> 
        private void Spec_PLC(ExcelWorksheet wh)
        {
            bool a = IsFirstBlockinProgram(); // Наличие 1-го блока расширения
            bool b = IsSecondBlockinProgram(); // Наличие 2-го блока расширения
            bool c = IsThirdBlockinProgram(); // Наличие 3-го блока расширения
            // Добавление информации о ПЛК
            wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
            wh.Cells['B' + posSpec.ToString()].Value = plc_spec_art;
            wh.Cells['C' + posSpec.ToString()].Value = 1;
            wh.Cells['D' + posSpec.ToString()].Value = plc_spec_name;
            posSpec++;
            // Добавление информации о комплекте разъемов для ПЛК
            wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
            wh.Cells['B' + posSpec.ToString()].Value = plcConnect_spec_art;
            wh.Cells['C' + posSpec.ToString()].Value = 1;
            wh.Cells['D' + posSpec.ToString()].Value = plcConnect_spec_name;
            posSpec++;
            // Если есть хотя бы один блок расширения
            if (a || b || c)
            {
                // Добавление информации о блоке расширения
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = bl_spec_art;
                wh.Cells['D' + posSpec.ToString()].Value = bl_spec_name;
                if ((a && !b && !c) || (!a && b && !c) || (!a && !b && c)) // Один блок расширения
                    wh.Cells['C' + posSpec.ToString()].Value = 1;
                else if ((a && b && !c) || (a && !b && c) || (!a && b && c)) // Два блока расшиирения
                    wh.Cells['C' + posSpec.ToString()].Value = 2;
                else // Три блока расширения
                    wh.Cells['C' + posSpec.ToString()].Value = 3;
                posSpec++;
                // Комплект разъемов 2 для блока расширения
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = blConnect1_spec_art;
                wh.Cells['D' + posSpec.ToString()].Value = blConnect1_spec_name;
                if ((a && !b && !c) || (!a && b && !c) || (!a && !b && c)) // Один блок расширения
                    wh.Cells['C' + posSpec.ToString()].Value = 1;
                else if ((a && b && !c) || (a && !b && c) || (!a && b && c)) // Два блока расшиирения
                    wh.Cells['C' + posSpec.ToString()].Value = 2;
                else // Три блока расширения
                    wh.Cells['C' + posSpec.ToString()].Value = 3;
                posSpec++;
                // Комлект разъемов 3 для блока расширения
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = blConnect2_spec_art;
                wh.Cells['D' + posSpec.ToString()].Value = blConnect2_spec_name;
                if ((a && !b && !c) || (!a && b && !c) || (!a && !b && c)) // Один блок расширения
                    wh.Cells['C' + posSpec.ToString()].Value = 1;
                else if ((a && b && !c) || (a && !b && c) || (!a && b && c)) // Два блока расшиирения
                    wh.Cells['C' + posSpec.ToString()].Value = 2;
                else // Три блока расширения
                    wh.Cells['C' + posSpec.ToString()].Value = 3;
                posSpec++;
            }
        }

        ///<summary>Добавление информации о частотных преобразователях</summary> 
        private void Spec_FreqConv(ExcelWorksheet wh)
        {
            // Приточный вентилятор
            // Приточный ПЧ, 220 В, нет Modbus
            if (prFanFC_check.Checked && prFanPowCombo.SelectedIndex == 1 && prFanControlCombo.SelectedIndex == 0)
            {
                double value = Convert.ToDouble(powPrFanBox.Text);
                Spec_CountA150_21(value); // Подсчет для ПЧ
                if (checkResPrFan.Checked) // Выбрано резервирование
                {
                    value = Convert.ToDouble(powPrResFanBox.Text);
                    Spec_CountA150_21(value); // Подсчет для ПЧ
                }
            }
            // Приточный ПЧ, 220 В, есть Modbus
            else if (prFanFC_check.Checked && prFanPowCombo.SelectedIndex == 1 && prFanControlCombo.SelectedIndex == 1)
            {
                double value = Convert.ToDouble(powPrFanBox.Text);
                Spec_CountA150_21_ec(value); // Подсчет для ПЧ
                if (checkResPrFan.Checked) // Выбрано резервирование
                {
                    value = Convert.ToDouble(powPrResFanBox.Text);
                    Spec_CountA150_21_ec(value); // Подсчет для ПЧ
                }
            }
            // Приточный ПЧ, 380 В, нет Modbus
            else if (prFanFC_check.Checked && prFanPowCombo.SelectedIndex == 0 && prFanControlCombo.SelectedIndex == 0)
            {
                double value = Convert.ToDouble(powPrFanBox.Text);
                Spec_CountA150_33(value); // Подсчет для ПЧ
                if (checkResPrFan.Checked) // Выбрано резервирование
                {
                    value = Convert.ToDouble(powPrFanBox.Text);
                    Spec_CountA150_33(value); // Подсчет для ПЧ
                }
            }
            // Приточный ПЧ, 380 В, есть Modbus
            else if (prFanFC_check.Checked && prFanPowCombo.SelectedIndex == 0 && prFanControlCombo.SelectedIndex == 1)
            {
                double value = Convert.ToDouble(powPrFanBox.Text);
                Spec_CountA150_33_ec(value); // Подсчет для ПЧ
                if (checkResPrFan.Checked) // Выбрано резервирование
                {
                    value = Convert.ToDouble(powPrFanBox.Text);
                    Spec_CountA150_33_ec(value); // Подсчет для ПЧ
                }
            }
            // Вытяжной вентилятор
            // Вытяжной ПЧ, 220 В, нет Modbus
            if (outFanFC_check.Checked && outFanPowCombo.SelectedIndex == 1 && outFanControlCombo.SelectedIndex == 0)
            {
                double value = Convert.ToDouble(powOutFanBox.Text);
                Spec_CountA150_21(value); // Подсчет для ПЧ
                if (checkResOutFan.Checked) // Выбрано резервирование
                {
                    value = Convert.ToDouble(powOutFanBox.Text);
                    Spec_CountA150_21(value); // Подсчет для ПЧ
                }
            }
            // Вытяжной ПЧ, 220 В, есть Modbus
            else if (outFanFC_check.Checked && outFanPowCombo.SelectedIndex == 1 && outFanControlCombo.SelectedIndex == 1)
            {
                double value = Convert.ToDouble(powOutFanBox.Text);
                Spec_CountA150_21_ec(value); // Подсчет для ПЧ
                if (checkResPrFan.Checked) // Выбрано резервирование
                {
                    value = Convert.ToDouble(powOutResFanBox.Text);
                    Spec_CountA150_21_ec(value); // Подсчет для ПЧ
                }
            }
            // Вытяжной ПЧ, 380 В, нет Modbus
            else if (outFanFC_check.Checked && outFanPowCombo.SelectedIndex == 0 && outFanControlCombo.SelectedIndex == 0)
            {
                double value = Convert.ToDouble(powOutFanBox.Text);
                Spec_CountA150_33(value); // Подсчет для ПЧ
                if (checkResOutFan.Checked) // Выбрано резервирование
                {
                    value = Convert.ToDouble(powOutFanBox.Text);
                    Spec_CountA150_33(value); // Подсчет для ПЧ
                }
            }
            // Вытяжной ПЧ, 380 В, есть Modbus
            else if (outFanFC_check.Checked && outFanPowCombo.SelectedIndex == 0 && outFanControlCombo.SelectedIndex == 1)
            {
                double value = Convert.ToDouble(powOutFanBox.Text);
                Spec_CountA150_33_ec(value); // Подсчет для ПЧ
                if (checkResPrFan.Checked) // Выбрано резервирование
                {
                    value = Convert.ToDouble(powOutResFanBox.Text);
                    Spec_CountA150_33_ec(value); // Подсчет для ПЧ
                }
            }
            Spec_ShowA150_21(wh); // Запись в файл, ПЧ 220 В, нет Modbus
            Spec_ShowA150_21_ec(wh); // Запис в файл, ПЧ 220 В, есть Modbus
            Spec_ShowA150_33(wh); // Запись в файл, ПЧ 380 В, нет Modbus
            Spec_ShowA150_33_ec(wh); // Запись в файл, ПЧ 380 В, есть Modbus
        }

        ///<summary>Добавление информации о датчиках</summary>
        private void Spec_TempSens(ExcelWorksheet wh)
        {
            CheckThermoTS_spec(); // Поиск воздушных термостатов по выбранным опциям
            CheckFilterPS_spec(); // Поиск PS для воздушных фильтров по выбранным опциям
            CheckFanPS_spec(); // Поиск PS для двигателей по выбранным опциям
            Spec_ShowHumidSensors(wh); // Запись в файл для датчиков влажности
            Spec_ShowChanSensors(wh); // Запись в файл для канальных датчиков температуры
            Spec_ShowRoomSensor(wh); // Запись в файл для комнатного датчика температуры
            Spec_ShowOutSensor(wh); // Запись в файл для наружного датчика температуры
            Spec_ShowWatSensor(wh); // Запись в файл для датчиков обратной воды и гильз
            Spec_ThermoTS(wh); // Запись в файл для воздушных термостатов
            Spec_FilterPS(wh); // Запись в файл для датчиков PS, фильтры
            Spec_FanPS(wh); // Запись в файл для датчиков PS, двигатели
        }
        
        ///<summary>Добавление информации про привода заслонок</summary>
        private void Spec_DamperDrive(ExcelWorksheet wh)
        {
            if (dampCheck.Checked) Spec_prDampDrive(); // Привод для приточной заслонки
            if (comboSysType.SelectedIndex == 1 && dampCheck.Checked && outDampCheck.Checked)
                Spec_outDampDrive(); // Привод для вытяжной заслонки
            if (comboSysType.SelectedIndex == 1 && recircCheck.Checked)
                Spec_recircDrive(); // Привод для рециркуляционной заслонки
            Spec_DampDrives(wh); // Запись в файл для приводов воздушных заслонок
        }

        ///<summary>Определение привода для приточной заслонки</summary>
        private void Spec_prDampDrive()
        {
            if (prDampPowCombo.SelectedIndex == 0) // Питание 24 В
            {
                if (confPrDampCheck.Checked) // Есть подтверждение открытия заслонки
                {
                    if (springRetPrDampCheck.Checked) // Привод с пружинным возвратом
                    {
                        if (torq_prDamp > 0) // Если определен момент для приточной заслонки
                        {
                            if (torq_prDamp <= 3.0) oda_03_d_024_sa_total++;
                            else if (torq_prDamp <= 5.0) oda_05_d_024_sa_total++;
                            else if (torq_prDamp <= 10.0) oda_10_d_024_sa_total++;
                            else if (torq_prDamp <= 15.0) oda_15_d_024_sa_total++;
                        }
                    }
                    else // Привод без пружинного возврата
                    {
                        if (torq_prDamp > 0) // Если определен момент для приточной заслонки
                        {
                            if (torq_prDamp <= 8.0) oda_08_d_024_na_total++;
                            else if (torq_prDamp <= 16.0) oda_16_d_024_na_total++;
                            else if (torq_prDamp <= 24.0) oda_24_d_024_na_total++;
                            else if (torq_prDamp <= 40.0) oda_40_d_024_na_total++;
                        }
                    }
                }
                else // Нет подтверждения открытия заслонки
                {
                    if (springRetPrDampCheck.Checked) // Привод с пружинным возвратом
                    {
                        if (torq_prDamp > 0) // Если определен момент для приточной заслонки
                        {
                            if (torq_prDamp <= 3.0) oda_03_d_024_s_total++;
                            else if (torq_prDamp <= 5.0) oda_05_d_024_s_total++;
                        }
                    }
                    else // Привод без пружинного возврата
                    {
                        if (torq_prDamp > 0) // Если определен момент для приточной заслонки
                        {
                            if (torq_prDamp <= 2.0) oda_02_d_024_n_total++;
                            else if (torq_prDamp <= 4.0) oda_04_d_024_n_total++;
                            else if (torq_prDamp <= 6.0) oda_06_d_024_n_total++;
                        }
                    }
                }
            }
            else // Питание 220 В
            {
                if (confPrDampCheck.Checked) // Есть подтверждение открытия заслонки
                {
                    if (springRetPrDampCheck.Checked) // Привод с пружинным возвратом
                    {
                        if (torq_prDamp > 0) // Если определен момент для приточной заслонки
                        {
                            if (torq_prDamp <= 3.0) oda_03_d_230_sa_total++;
                            else if (torq_prDamp <= 5.0) oda_05_d_230_sa_total++;
                            else if (torq_prDamp <= 10.0) oda_10_d_230_sa_total++;
                            else if (torq_prDamp <= 15.0) oda_15_d_230_sa_total++;
                        }
                    }
                    else // Привод без пружинного возврата
                    {
                        if (torq_prDamp > 0) // Если определен момент для приточной заслонки
                        {
                            if (torq_prDamp <= 8.0) oda_08_d_230_na_total++;
                            else if (torq_prDamp <= 16.0) oda_16_d_230_na_total++;
                            else if (torq_prDamp <= 24.0) oda_24_d_230_na_total++;
                            else if (torq_prDamp <= 40.0) oda_40_d_230_na_total++;
                        }
                    }
                }
                else // Нет подтверждения открытия заслонки
                {
                    if (springRetPrDampCheck.Checked) // Привод с пружинным возвратом
                    {
                        if (torq_prDamp > 0) // Если определен момент для приточной заслонки
                        {
                            if (torq_prDamp <= 3.0) oda_03_d_230_s_total++;
                            else if (torq_prDamp <= 5.0) oda_05_d_230_s_total++;
                        }
                    }
                    else // Привод без пружинного возврата
                    {
                        if (torq_prDamp > 0) // Если определен момент для приточной заслонки
                        {
                            if (torq_prDamp <= 2.0) oda_02_d_230_n_total++;
                            else if (torq_prDamp <= 4.0) oda_04_d_230_n_total++;
                            else if (torq_prDamp <= 6.0) oda_06_d_230_n_total++;
                        }
                    }
                }
            }
        }
        
        ///<summary>Определение привода для вытяжной заслонки</summary>
        private void Spec_outDampDrive()
        {
            if (outDampPowCombo.SelectedIndex == 0) // Питание 24 В
            {
                if (confOutDampCheck.Checked) // Есть подтверждение открытия заслонки
                {
                    if (springRetOutDampCheck.Checked) // Привод с пружинным возвратом
                    {
                        if (torq_outDamp > 0) // Если определен момент для приточной заслонки
                        {
                            if (torq_outDamp <= 3.0) oda_03_d_024_sa_total++;
                            else if (torq_outDamp <= 5.0) oda_05_d_024_sa_total++;
                            else if (torq_outDamp <= 10.0) oda_10_d_024_sa_total++;
                            else if (torq_outDamp <= 15.0) oda_15_d_024_sa_total++;
                        }
                    }
                    else // Привод без пружинного возврата
                    {
                        if (torq_outDamp > 0) // Если определен момент для приточной заслонки
                        {
                            if (torq_outDamp <= 8.0) oda_08_d_024_na_total++;
                            else if (torq_outDamp <= 16.0) oda_16_d_024_na_total++;
                            else if (torq_outDamp <= 24.0) oda_24_d_024_na_total++;
                            else if (torq_outDamp <= 40.0) oda_40_d_024_na_total++;
                        }
                    }
                }
                else // Нет подтверждения открытия заслонки
                {
                    if (springRetOutDampCheck.Checked) // Привод с пружинным возвратом
                    {
                        if (torq_outDamp > 0) // Если определен момент для приточной заслонки
                        {
                            if (torq_outDamp <= 3.0) oda_03_d_024_s_total++;
                            else if (torq_outDamp <= 5.0) oda_05_d_024_s_total++;
                        }
                    }
                    else // Привод без пружинного возврата
                    {
                        if (torq_outDamp > 0) // Если определен момент для приточной заслонки
                        {
                            if (torq_outDamp <= 2.0) oda_02_d_024_n_total++;
                            else if (torq_outDamp <= 4.0) oda_04_d_024_n_total++;
                            else if (torq_outDamp <= 6.0) oda_06_d_024_n_total++;
                        }
                    }
                }
            }
            else // Питание 220 В
            {
                if (confOutDampCheck.Checked) // Есть подтверждение открытия заслонки
                {
                    if (springRetOutDampCheck.Checked) // Привод с пружинным возвратом
                    {
                        if (torq_outDamp > 0) // Если определен момент для приточной заслонки
                        {
                            if (torq_outDamp <= 3.0) oda_03_d_230_sa_total++;
                            else if (torq_outDamp <= 5.0) oda_05_d_230_sa_total++;
                            else if (torq_outDamp <= 10.0) oda_10_d_230_sa_total++;
                            else if (torq_outDamp <= 15.0) oda_15_d_230_sa_total++;
                        }
                    }
                    else // Привод без пружинного возврата
                    {
                        if (torq_outDamp > 0) // Если определен момент для приточной заслонки
                        {
                            if (torq_outDamp <= 8.0) oda_08_d_230_na_total++;
                            else if (torq_outDamp <= 16.0) oda_16_d_230_na_total++;
                            else if (torq_outDamp <= 24.0) oda_24_d_230_na_total++;
                            else if (torq_outDamp <= 40.0) oda_40_d_230_na_total++;
                        }
                    }
                }
                else // Нет подтверждения открытия заслонки
                {
                    if (springRetOutDampCheck.Checked) // Привод с пружинным возвратом
                    {
                        if (torq_outDamp > 0) // Если определен момент для приточной заслонки
                        {
                            if (torq_outDamp <= 3.0) oda_03_d_230_s_total++;
                            else if (torq_outDamp <= 5.0) oda_05_d_230_s_total++;
                        }
                    }
                    else // Привод без пружинного возврата
                    {
                        if (torq_outDamp > 0) // Если определен момент для приточной заслонки
                        {
                            if (torq_outDamp <= 2.0) oda_02_d_230_n_total++;
                            else if (torq_outDamp <= 4.0) oda_04_d_230_n_total++;
                            else if (torq_outDamp <= 6.0) oda_06_d_230_n_total++;
                        }
                    }
                }
            }
        }

        ///<summary>Определение привода для рециркуляционной заслонки</summary>
        private void Spec_recircDrive()
        {
            if (recircPowCombo.SelectedIndex == 0) // Питание 24 В
            {
                if (springRetRecircCheck.Checked) // Привод с пружинным возвратом
                {
                    if (torq_recircDamp > 0) // Если определен момент для приточной заслонки
                    {
                        if (torq_recircDamp <= 3.0) oda_03_d_024_s_total++;
                        else if (torq_recircDamp <= 5.0) oda_05_d_024_s_total++;
                    }
                }
                else // Привод без пружинного возврата
                {
                    if (torq_recircDamp > 0) // Если определен момент для приточной заслонки
                    {
                        if (torq_recircDamp <= 2.0) oda_02_d_024_n_total++;
                        else if (torq_recircDamp <= 4.0) oda_04_d_024_n_total++;
                        else if (torq_recircDamp <= 6.0) oda_06_d_024_n_total++;
                    }
                }
            }
            else // Питание 220 В
            {
                if (springRetRecircCheck.Checked) // Привод с пружинным возвратом
                {
                    if (torq_recircDamp > 0) // Если определен момент для приточной заслонки
                    {
                        if (torq_recircDamp <= 3.0) oda_03_d_230_s_total++;
                        else if (torq_recircDamp <= 5.0) oda_05_d_230_s_total++;
                    }
                }
                else // Привод без пружинного возврата
                {
                    if (torq_recircDamp > 0) // Если определен момент для приточной заслонки
                    {
                        if (torq_recircDamp <= 2.0) oda_02_d_230_n_total++;
                        else if (torq_recircDamp <= 4.0) oda_04_d_230_n_total++;
                        else if (torq_recircDamp <= 6.0) oda_06_d_230_n_total++;
                    }
                }
            }
        }

        private void Spec_DampDrives(ExcelWorksheet wh)
        {
            // Привода на 24 В
            if (oda_03_d_024_sa_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_03_d_024_sa_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_03_d_024_sa_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_03_d_024_sa_name;
                posSpec++;
            }
            if (oda_05_d_024_sa_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_05_d_024_sa_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_05_d_024_sa_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_05_d_024_sa_name;
                posSpec++;
            }
            if (oda_10_d_024_sa_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_10_d_024_sa_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_10_d_024_sa_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_10_d_024_sa_name;
                posSpec++;
            }
            if (oda_15_d_024_sa_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_15_d_024_sa_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_15_d_024_sa_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_15_d_024_sa_name;
                posSpec++;
            }
            if (oda_08_d_024_na_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_08_d_024_na_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_08_d_024_na_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_08_d_024_na_name;
                posSpec++;
            }
            if (oda_16_d_024_na_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_16_d_024_na_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_16_d_024_na_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_16_d_024_na_name;
                posSpec++;
            }
            if (oda_24_d_024_na_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_24_d_024_na_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_24_d_024_na_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_24_d_024_na_name;
                posSpec++;
            }
            if (oda_40_d_024_na_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_40_d_024_na_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_40_d_024_na_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_40_d_024_na_name;
                posSpec++;
            }
            if (oda_03_d_024_s_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_03_d_024_s_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_03_d_024_s_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_03_d_024_s_name;
                posSpec++;
            }
            if (oda_05_d_024_s_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_05_d_024_s_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_05_d_024_s_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_05_d_024_s_name;
                posSpec++;
            }
            if (oda_02_d_024_n_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_02_d_024_n_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_02_d_024_n_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_02_d_024_n_name;
                posSpec++;
            }
            if (oda_04_d_024_n_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_04_d_024_n_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_04_d_024_n_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_04_d_024_n_name;
                posSpec++;
            }
            if (oda_06_d_024_n_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_06_d_024_n_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_06_d_024_n_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_06_d_024_n_name;
                posSpec++;
            }
            // Привода на 220 В
            if (oda_03_d_230_sa_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_03_d_230_sa_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_03_d_230_sa_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_03_d_230_sa_name;
                posSpec++;
            }
            if (oda_05_d_230_sa_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_05_d_230_sa_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_05_d_230_sa_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_05_d_230_sa_name;
                posSpec++;
            }
            if (oda_10_d_230_sa_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_10_d_230_sa_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_10_d_230_sa_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_10_d_230_sa_name;
                posSpec++;
            }
            if (oda_15_d_230_sa_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_15_d_230_sa_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_15_d_230_sa_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_15_d_230_sa_name;
                posSpec++;
            }
            if (oda_08_d_230_na_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_08_d_230_na_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_08_d_230_na_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_08_d_230_na_name;
                posSpec++;
            }
            if (oda_16_d_230_na_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_16_d_230_na_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_16_d_230_na_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_16_d_230_na_name;
                posSpec++;
            }
            if (oda_24_d_230_na_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_24_d_230_na_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_24_d_230_na_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_24_d_230_na_name;
                posSpec++;
            }
            if (oda_40_d_230_na_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_40_d_230_na_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_40_d_230_na_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_40_d_230_na_name;
                posSpec++;
            }
            if (oda_03_d_230_s_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_03_d_230_s_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_03_d_230_s_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_03_d_230_s_name;
                posSpec++;
            }
            if (oda_05_d_230_s_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_05_d_230_s_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_05_d_230_s_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_05_d_230_s_name;
                posSpec++;
            }
            if (oda_02_d_230_n_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_02_d_230_n_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_02_d_230_n_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_02_d_230_n_name;
                posSpec++;
            }
            if (oda_04_d_230_n_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_04_d_230_n_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_04_d_230_n_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_04_d_230_n_name;
                posSpec++;
            }
            if (oda_06_d_230_n_total > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = oda_06_d_230_n_art;
                wh.Cells['C' + posSpec.ToString()].Value = oda_06_d_230_n_total;
                wh.Cells['D' + posSpec.ToString()].Value = oda_06_d_230_n_name;
                posSpec++;
            }
        }

        ///<summary>Поиск воздушных термостатов по выбранным опциям</summary>
        private void CheckThermoTS_spec()
        {
            // Основной водяной нагреватель, выбран воздушный термостат
            if (heaterCheck.Checked && heatTypeCombo.SelectedIndex == 0 && TF_heaterCheck.Checked)
                thermoTSTotal++;
            // Второй водяной нагреватель, выбран воздушный термостат
            if (addHeatCheck.Checked && heatAddTypeCombo.SelectedIndex == 0 && TF_addHeaterCheck.Checked)
                thermoTSTotal++;
        }

        ///<summary>Поиск PS для воздушных фильтров по выбранным опциям</summary>
        private void CheckFilterPS_spec()
        {
            // Приточные фильтры
            if (filterCheck.Checked)
            {
                switch (filterPrCombo.SelectedIndex)
                {
                    case 0: PSfilterTotal++; break; // Один приточный фильтр
                    case 1: PSfilterTotal += 2; break; // Два приточных фильтра
                    case 2: PSfilterTotal += 3; break; // Три приточных фильтра
                }
            }
            // Вытяжные фильтры
            if (filterCheck.Checked && comboSysType.SelectedIndex == 1)
            {
                switch (filterOutCombo.SelectedIndex)
                {
                    case 0: break; // Ноль вытяжных фильтров
                    case 1: PSfilterTotal++; break; // Один вытяжной фильтр
                    case 2: PSfilterTotal += 2; break; // Два вытяжных фильтра
                    case 3: PSfilterTotal += 3; break; // Три вытяжных фильтра
                }
            }
        }

        ///<summary>Поиск PS для двигателей вентиляторов и рекуператора по опциям</summary>
        private void CheckFanPS_spec()
        {
            // PS для приточного вентилятора
            if (prFanPSCheck.Checked) PSfanTotal++;
            // PS для вытяжного вентилятора
            if (comboSysType.SelectedIndex == 1 && outFanPSCheck.Checked) PSfanTotal++;
            // PS для защиты рекуператора
            if (comboSysType.SelectedIndex == 1 && recupCheck.Checked && recDefPsCheck.Checked) PSfanTotal++;
        }

        ///<summary>Подсчет для ПЧ A150, 220 В, нет Modbus</summary>
        private void Spec_CountA150_21(double v)
        {
            if (v <= 0.4) A150_21_04h++; // 0.4 кВт
            else if (v <= 0.75) A150_21_075h++; // 0.75 кВт
            else if (v <= 1.1) A150_21_11n++; // 1.1 кВт
            else if (v <= 1.5) A150_21_15n++; // 1.5 кВт
            else if (v <= 2.2) A150_21_22n++; // 2.2 кВт
        }

        ///<summary>Подсчет для ПЧ A150, 220 В, есть Modbus</summary>
        private void Spec_CountA150_21_ec(double v)
        {
            if (v <= 0.4) A150_21_04h_ec++;
            else if (v <= 0.75) A150_21_075h_ec++;
            else if (v <= 1.1) A150_21_11n_ec++;
            else if (v <= 1.5) A150_21_15n_ec++;
            else if (v <= 2.2) A150_21_22n_ec++;
        }
        
        ///<summary>Подсчет для ПЧ A150, 380 В, нет Modbus</summary>
        private void Spec_CountA150_33(double v)
        {
            if (v <= 0.75) A150_33_075ht++;
            else if (v <= 1.5) A150_33_15nt++;
            else if (v <= 2.2) A150_33_22nt++;
            else if (v <= 3.7) A150_33_37nt++;
            else if (v <= 5.5) A150_33_55nt++;
            else if (v <= 7.5) A150_33_75nt++;
            else if (v <= 11.0) A150_33_11t++;
            else if (v <= 15.0) A150_33_15t++;
        }
        
        ///<summary>Подсчет для ПЧ A150, 380 В, есть Modbus</summary>
        private void Spec_CountA150_33_ec(double v)
        {
            if (v <= 0.75) A150_33_075ht_ec++;
            else if (v <= 1.5) A150_33_15nt_ec++;
            else if (v <= 2.2) A150_33_22nt_ec++;
            else if (v <= 3.7) A150_33_37nt_ec++;
            else if (v <= 5.5) A150_33_55nt_ec++;
            else if (v <= 7.5) A150_33_75nt_ec++;
            else if (v <= 11.0) A150_33_11t_ec++;
            else if (v <= 15.0) A150_33_15t_ec++;
        }

        ///<summary>Запись в файл спецификации для ПЧ А150, 220 В, нет Modbus</summary>
        private void Spec_ShowA150_21(ExcelWorksheet wh)
        {
            if (A150_21_04h > 0) // 0.4 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_21_04h_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_21_04h;
                wh.Cells['D' + posSpec.ToString()].Value = a150_21_04h_name;
                posSpec++;
            }
            if (A150_21_075h > 0) // 0.75 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_21_075h_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_21_075h;
                wh.Cells['D' + posSpec.ToString()].Value = a150_21_075h_name;
                posSpec++;
            }
            if (A150_21_11n > 0) // 1.1 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_21_11n_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_21_11n;
                wh.Cells['D' + posSpec.ToString()].Value = a150_21_11n_name;
                posSpec++;
            }
            if (A150_21_15n > 0) // 1.5 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_21_15n_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_21_15n;
                wh.Cells['D' + posSpec.ToString()].Value = a150_21_15n_name;
                posSpec++;
            }
            if (A150_21_22n > 0) // 2.2 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_21_22n_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_21_22n;
                wh.Cells['D' + posSpec.ToString()].Value = a150_21_22n_name;
                posSpec++;
            }
        }

        ///<summary>Запись в файл спецификации для ПЧ А150, 380 В, нет Modbus</summary>
        private void Spec_ShowA150_33(ExcelWorksheet wh)
        {
            if (A150_33_075ht > 0) // 0.75 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_33_075ht_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_33_075ht;
                wh.Cells['D' + posSpec.ToString()].Value = a150_33_075ht_name;
                posSpec++;
            }
            if (A150_33_15nt > 0) // 1.5 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_33_15nt_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_33_15nt;
                wh.Cells['D' + posSpec.ToString()].Value = a150_33_15nt_name;
                posSpec++;
            }
            if (A150_33_22nt > 0) // 2.2 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_33_22nt_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_33_22nt;
                wh.Cells['D' + posSpec.ToString()].Value = a150_33_22nt_name;
                posSpec++;
            }
            if (A150_33_37nt > 0) // 3.7 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_33_37nt_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_33_37nt;
                wh.Cells['D' + posSpec.ToString()].Value = a150_33_37nt_name;
                posSpec++;
            }
            if (A150_33_55nt > 0) // 5.5 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_33_55nt_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_33_55nt;
                wh.Cells['D' + posSpec.ToString()].Value = a150_33_55nt_name;
                posSpec++;
            }
            if (A150_33_75nt > 0) // 7.5 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_33_75nt_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_33_75nt;
                wh.Cells['D' + posSpec.ToString()].Value = a150_33_75nt_name;
                posSpec++;
            }
            if (A150_33_11t > 0) // 11 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_33_11t_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_33_11t;
                wh.Cells['D' + posSpec.ToString()].Value = a150_33_11t_name;
                posSpec++;
            }
            if (A150_33_15t > 0) // 15 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_33_15t_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_33_15t;
                wh.Cells['D' + posSpec.ToString()].Value = a150_33_15t_name;
                posSpec++;
            }
        }

        ///<summary>Запись в файл спецификации для ПЧ А150, 220 В, есть Modbus</summary>
        private void Spec_ShowA150_21_ec(ExcelWorksheet wh)
        {
            if (A150_21_04h_ec > 0) // 0.4 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_21_04h_ec_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_21_04h_ec;
                wh.Cells['D' + posSpec.ToString()].Value = a150_21_04h_ec_name;
                posSpec++;
            }
            if (A150_21_075h_ec > 0) // 0.75 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_21_075h_ec_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_21_075h_ec;
                wh.Cells['D' + posSpec.ToString()].Value = a150_21_075h_ec_name;
                posSpec++;
            }
            if (A150_21_11n_ec > 0) // 1.1 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_21_11n_ec_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_21_11n_ec;
                wh.Cells['D' + posSpec.ToString()].Value = a150_21_11n_ec_name;
                posSpec++;
            }
            if (A150_21_15n_ec > 0) // 1.5 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_21_15n_ec_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_21_15n_ec;
                wh.Cells['D' + posSpec.ToString()].Value = a150_21_15n_ec_name;
                posSpec++;
            }
            if (A150_21_22n > 0) // 2.2 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_21_22n_ec_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_21_22n_ec;
                wh.Cells['D' + posSpec.ToString()].Value = a150_21_22n_ec_name;
                posSpec++;
            }
        }

        ///<summary>Запись в файл спецификации для ПЧ А150, 380 В, есть Modbus</summary>
        private void Spec_ShowA150_33_ec(ExcelWorksheet wh)
        {
            if (A150_33_075ht_ec > 0) // 0.75 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_33_075ht_ec_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_33_075ht_ec;
                wh.Cells['D' + posSpec.ToString()].Value = a150_33_075ht_ec_name;
                posSpec++;
            }
            if (A150_33_15nt_ec > 0) // 1.5 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_33_15nt_ec_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_33_15nt_ec;
                wh.Cells['D' + posSpec.ToString()].Value = a150_33_15nt_ec_name;
                posSpec++;
            }
            if (A150_33_22nt_ec > 0) // 2.2 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_33_22nt_ec_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_33_22nt_ec;
                wh.Cells['D' + posSpec.ToString()].Value = a150_33_22nt_ec_name;
                posSpec++;
            }
            if (A150_33_37nt_ec > 0) // 3.7 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_33_37nt_ec_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_33_37nt_ec;
                wh.Cells['D' + posSpec.ToString()].Value = a150_33_37nt_ec_name;
                posSpec++;
            }
            if (A150_33_55nt_ec > 0) // 5.5 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_33_55nt_ec_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_33_55nt_ec;
                wh.Cells['D' + posSpec.ToString()].Value = a150_33_55nt_ec_name;
                posSpec++;
            }
            if (A150_33_75nt_ec > 0) // 7.5 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_33_75nt_ec_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_33_75nt_ec;
                wh.Cells['D' + posSpec.ToString()].Value = a150_33_75nt_ec_name;
                posSpec++;
            }
            if (A150_33_11t_ec > 0) // 11 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_33_11t_ec_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_33_11t_ec;
                wh.Cells['D' + posSpec.ToString()].Value = a150_33_11t_ec_name;
                posSpec++;
            }
            if (A150_33_15t_ec > 0) // 15 кВт
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = a150_33_15t_ec_art;
                wh.Cells['C' + posSpec.ToString()].Value = A150_33_15t_ec;
                wh.Cells['D' + posSpec.ToString()].Value = a150_33_15t_ec_name;
                posSpec++;
            }
        }

        ///<summary>Запись в файл спецификации для датчиков влажности</summary>
        private void Spec_ShowHumidSensors(ExcelWorksheet wh)
        {
            if (humidSensorTotal_0_10 > 0) // Датчики влажности 0-10 В
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = "-";
                wh.Cells['C' + posSpec.ToString()].Value = humidSensorTotal_0_10;
                wh.Cells['D' + posSpec.ToString()].Value = humidSensor_0_10_name;
                posSpec++;
            }
            if (humidSensorTotal_4_20 > 0) // Датчики влажности 4-20 мА
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = "-";
                wh.Cells['C' + posSpec.ToString()].Value = humidSensorTotal_4_20;
                wh.Cells['D' + posSpec.ToString()].Value = humidSensor_4_20_name;
                posSpec++;
            }
        }

        ///<summary>Запись в файл спецификации для канальных датчиков</summary>
        private void Spec_ShowChanSensors(ExcelWorksheet wh)
        {
            if (chanSensorTotal_ntc > 0) // Канальные датчики температуры NTC
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = chanSensor_ntc_art;
                wh.Cells['C' + posSpec.ToString()].Value = chanSensorTotal_ntc;
                wh.Cells['D' + posSpec.ToString()].Value = chanSensor_ntc_name;
                posSpec++;
            }
            if (chanSensorTotal_pt > 0) // Канальные датчики температуры PT1000
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = chanSensor_pt_art;
                wh.Cells['C' + posSpec.ToString()].Value = chanSensorTotal_pt;
                wh.Cells['D' + posSpec.ToString()].Value = chanSensor_pt_name;
                posSpec++;
            }
        }

        ///<summary>Запись в файл спецификации для комнатного датчика</summary>
        private void Spec_ShowRoomSensor(ExcelWorksheet wh)
        {
            if (roomSensorTotal_ntc > 0) // Комнатный датчик температуры NTC
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = roomSensor_ntc_art;
                wh.Cells['C' + posSpec.ToString()].Value = roomSensorTotal_ntc;
                wh.Cells['D' + posSpec.ToString()].Value = roomSensor_ntc_name;
                posSpec++;
            }
            if (roomSensorTotal_pt > 0) // Комнатный датчик температуры Pt1000
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = roomSensor_pt_art;
                wh.Cells['C' + posSpec.ToString()].Value = roomSensorTotal_pt;
                wh.Cells['D' + posSpec.ToString()].Value = roomSensor_pt_name;
                posSpec++;
            }
        }

        ///<summary>Запись в файл спецификации для наружного датчика температуры</summary>
        private void Spec_ShowOutSensor(ExcelWorksheet wh)
        {
            if (outSensorTotal_ntc > 0) // Наружный датчик температуры, NTC
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = outSensor_ntc_art;
                wh.Cells['C' + posSpec.ToString()].Value = outSensorTotal_ntc;
                wh.Cells['D' + posSpec.ToString()].Value = outSensor_ntc_name;
                posSpec++;
            }
            if (outSensorTotal_pt > 0) // Наружный датчик температуры, Pt1000
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = outSensor_pt_art;
                wh.Cells['C' + posSpec.ToString()].Value = outSensorTotal_pt;
                wh.Cells['D' + posSpec.ToString()].Value = outSensor_pt_name;
                posSpec++;
            }
        }

        ///<summary>Запись в файл для датчиков обратной воды и гильз</summary>
        private void Spec_ShowWatSensor(ExcelWorksheet wh)
        {
            if (watSensorTotal_pt > 0) // Погружные датчики температуры (обратной воды)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = watSensor_pt_art;
                wh.Cells['C' + posSpec.ToString()].Value = watSensorTotal_pt;
                wh.Cells['D' + posSpec.ToString()].Value = watSensor_pt_name;
                posSpec++;
            }
            if (sleeveForSensorTotal > 0) // Гильзы для погружных датчиков
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = sleeveForSensor_pt_art;
                wh.Cells['C' + posSpec.ToString()].Value = sleeveForSensorTotal;
                wh.Cells['D' + posSpec.ToString()].Value = sleeveForSensor_pt_name;
                posSpec++;
            }
        }

        ///<summary>Запись в файл для воздушных термостатов</summary>
        private void Spec_ThermoTS(ExcelWorksheet wh)
        {
            if (thermoTSTotal > 0) // Есть в наличии воздушные термостаты
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = thermoTS_art;
                wh.Cells['C' + posSpec.ToString()].Value = thermoTSTotal;
                wh.Cells['D' + posSpec.ToString()].Value = thermoTS_name;
                posSpec++;
            }
        }

        ///<summary>Запись в файл для датчиков PS для фильтров</summary>
        private void Spec_FilterPS(ExcelWorksheet wh)
        {
            if (PSfilterTotal > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = psFilter_art;
                wh.Cells['C' + posSpec.ToString()].Value = PSfilterTotal;
                wh.Cells['D' + posSpec.ToString()].Value = psFilter_name;
                posSpec++;
            }
        }

        ///<summary>Запись в файл для датчиков PS двигателей</summary>
        private void Spec_FanPS(ExcelWorksheet wh)
        {
            if (PSfanTotal > 0)
            {
                wh.Cells['A' + posSpec.ToString()].Value = posSpec - 1;
                wh.Cells['B' + posSpec.ToString()].Value = psFan_art;
                wh.Cells['C' + posSpec.ToString()].Value = PSfanTotal;
                wh.Cells['D' + posSpec.ToString()].Value = psFan_name;
                posSpec++;
            }
        }

        // Проверка наличия первого блока расширения в программе (по подписям сигналов)
        private bool IsFirstBlockinProgram()
        {
            if (DO1bl1_lab.Text == "" && DO2bl1_lab.Text == "" && DO3bl1_lab.Text == "" && DO4bl1_lab.Text == "" &&
                DO5bl1_lab.Text == "" && DO6bl1_lab.Text == "" && DO7bl1_lab.Text == "")
                if (AO1bl1_lab.Text == "" && AO2bl1_lab.Text == "")
                    return false;
            return true;
        }

        // Проверка наличия второго блока расширения в программе (по подписям сигналов)
        private bool IsSecondBlockinProgram()
        {
            if (DO1bl2_lab.Text == "" && DO2bl2_lab.Text == "" && DO3bl2_lab.Text == "" && DO4bl2_lab.Text == "" &&
                DO5bl2_lab.Text == "" && DO6bl2_lab.Text == "" && DO7bl2_lab.Text == "")
                if (AO1bl2_lab.Text == "" && AO2bl2_lab.Text == "")
                    return false;
            return true;
        }

        // Проверка наличия третьего блока расширения в программе (по подписям сигналов)
        private bool IsThirdBlockinProgram()
        {
            if (DO1bl3_lab.Text == "" && DO2bl3_lab.Text == "" && DO3bl3_lab.Text == "" && DO4bl3_lab.Text == "" &&
                DO5bl3_lab.Text == "" && DO6bl3_lab.Text == "" && DO7bl3_lab.Text == "")
                if (AO1bl3_lab.Text == "" && AO2bl3_lab.Text == "")
                    return false;
            return true;
        }

        ///<summary>Изменили ширину для приточной воздушной заслонки</summary>
        private void B_prDampBox_TextChanged(object sender, EventArgs e)
        {
            bool b = b_prDampBox.Text != string.Empty && h_prDampBox.Text != string.Empty &&
                int.Parse(b_prDampBox.Text) != 0 && int.Parse(h_prDampBox.Text) != 0;
            if (b) // Не пустые поля и не ноль
            {
                s_prDamp = int.Parse(b_prDampBox.Text) * int.Parse(h_prDampBox.Text);
                prDampSLabel.Text = "Площадь заслонки: " + (s_prDamp / 10000).ToString() + " м²";
                prDampSLabel.Show(); // Отображение площади
                TorquePrDamp_Spec(s_prDamp); // Определение и отображение момента заслонки
                CheckMarkPrDamp_Spec(); // Проверка отображения галочки о подобранном приводе в файл
            }
            else // Пустые поля, либо ноль
            {
                prDampSLabel.Hide(); prDampTorqLabel.Hide(); markPrDampPanel.Hide();
            }
        }

        ///<summary>Изменили высоту для приточной воздушной заслонки</summary>
        private void H_prDampBox_TextChanged(object sender, EventArgs e)
        {
            bool b = b_prDampBox.Text != string.Empty && h_prDampBox.Text != string.Empty &&
                int.Parse(b_prDampBox.Text) != 0 && int.Parse(h_prDampBox.Text) != 0;
            if (b) // Не пустые поля и не ноль
            {
                s_prDamp = int.Parse(b_prDampBox.Text) * int.Parse(h_prDampBox.Text);
                prDampSLabel.Text = "Площадь заслонки: " + (s_prDamp / 10000).ToString() + " м²";
                prDampSLabel.Show(); // Отображение площади
                TorquePrDamp_Spec(s_prDamp); // Определение и отображение момента заслонки
                CheckMarkPrDamp_Spec(); // Проверка отображения галочки о подобранном приводе в файл
            }
            else // Пустые поля, либо ноль
            {
                prDampSLabel.Hide(); prDampTorqLabel.Hide(); markPrDampPanel.Hide();
            }
        }

        ///<summary>Расчет и отображение момента для приточной заслонки</summary>
        private void TorquePrDamp_Spec(double square)
        {
            const int VAL = 10000;
            square /= VAL; // Перевод в квадратные метры
            if (springRetPrDampCheck.Checked) // С возвратной пружиной
            {
                if (square <= 0.6) torq_prDamp = 3;
                else if (square <= 1.0) torq_prDamp = 5;
                else if (square <= 1.5) torq_prDamp = 10;
                else if (square <= 3.0) torq_prDamp = 15;
                else if (square <= 4.0) torq_prDamp = 20;
                else torq_prDamp = 0;
            }
            else // Без пружинного возврата
            {
                if (square <= 0.5) torq_prDamp = 2;
                else if (square <= 0.8) torq_prDamp = 4;
                else if (square <= 1.2) torq_prDamp = 6;
                else if (square <= 1.5) torq_prDamp = 8;
                else if (square <= 3.0) torq_prDamp = 16;
                else if (square <= 4.5) torq_prDamp = 24;
                else if (square <= 6.0) torq_prDamp = 40;
                else torq_prDamp = 0;
            }
            if (torq_prDamp != 0) // Определено значение момента по площади
            {
                prDampTorqLabel.Text = "Крутящий момент: " + torq_prDamp.ToString() + " Нм";
                prDampTorqLabel.Show();
            }
            else prDampTorqLabel.Hide(); // Скрытие отображения значения момента
        }

        ///<summary>Изменили ширину для вытяжной воздушной заслонки</summary>
        private void B_outDampBox_TextChanged(object sender, EventArgs e)
        {
            bool b = b_outDampBox.Text != string.Empty && h_outDampBox.Text != string.Empty &&
                int.Parse(b_outDampBox.Text) != 0 && int.Parse(h_outDampBox.Text) != 0;
            if (b) // Не пустые поля и не ноль
            {
                s_outDamp = int.Parse(b_outDampBox.Text) * int.Parse(h_outDampBox.Text);
                outDampSLabel.Text = "Площадь заслонки: " + (s_outDamp / 10000).ToString() + " м²";
                outDampSLabel.Show(); // Отображение площади
                TorqueOutDamp_Spec(s_outDamp); // Определение и отображение момента заслонки
                CheckMarkOutDamp_Spec(); // Проверка добавления привода в спецификацию
            }
            else // Пустые поля, либо ноль
            {
                outDampSLabel.Hide(); outDampTorqLabel.Hide(); markOutDampPanel.Hide();
            }
        }

        ///<summary>Изменили высоту для вытяжной воздушной заслонки</summary>
        private void H_outDampBox_TextChanged(object sender, EventArgs e)
        {
            bool b = b_outDampBox.Text != string.Empty && h_outDampBox.Text != string.Empty &&
                int.Parse(b_outDampBox.Text) != 0 && int.Parse(h_outDampBox.Text) != 0;
            if (b) // Не пустые поля и не ноль
            {
                s_outDamp = int.Parse(b_outDampBox.Text) * int.Parse(h_outDampBox.Text);
                outDampSLabel.Text = "Площадь заслонки: " + (s_outDamp / 10000).ToString() + " м²";
                outDampSLabel.Show(); // Отображение площади
                TorqueOutDamp_Spec(s_outDamp); // Определение и отображение момента заслонки
                CheckMarkOutDamp_Spec(); // Проверка добавления привода в спецификацию
            }
            else // Пустые поля, либо ноль
            {
                outDampSLabel.Hide(); outDampTorqLabel.Hide(); markOutDampPanel.Hide();
            }
        }

        ///<summary>Расчет и отображение момента для вытяжной заслонки</summary>
        private void TorqueOutDamp_Spec(double square)
        {
            const int VAL = 10000;
            square /= VAL; // Перевод в квадратные метры
            if (springRetOutDampCheck.Checked) // С возвратной пружиной
            {
                if (square <= 0.6) torq_outDamp = 3;
                else if (square <= 1.0) torq_outDamp = 5;
                else if (square <= 1.5) torq_outDamp = 10;
                else if (square <= 3.0) torq_outDamp = 15;
                else if (square <= 4.0) torq_outDamp = 20;
                else torq_outDamp = 0;
            }
            else // Без пружинного возврата
            {
                if (square <= 0.5) torq_outDamp = 2;
                else if (square <= 0.8) torq_outDamp = 4;
                else if (square <= 1.2) torq_outDamp = 6;
                else if (square <= 1.5) torq_outDamp = 8;
                else if (square <= 3.0) torq_outDamp = 16;
                else if (square <= 4.5) torq_outDamp = 24;
                else if (square <= 6.0) torq_outDamp = 40;
                else torq_outDamp = 0;
            }
            if (torq_outDamp != 0) // Определено значение момента по площади
            {
                outDampTorqLabel.Text = "Крутящий момент: " + torq_outDamp.ToString() + " Нм";
                outDampTorqLabel.Show();
            }
            else outDampTorqLabel.Hide(); // Скрытие отображения значения момента
        }

        ///<summary>Изменили ширину для рециркуляционной воздушной заслонки</summary>
        private void B_recircBox_TextChanged(object sender, EventArgs e)
        {
            bool b = b_recircBox.Text != string.Empty && h_recircBox.Text != string.Empty &&
               int.Parse(b_recircBox.Text) != 0 && int.Parse(h_recircBox.Text) != 0;
            if (b) // Не пустые поля и не ноль
            {
                s_recircDamp = int.Parse(b_recircBox.Text) * int.Parse(h_recircBox.Text);
                recircSLabel.Text = "Площадь заслонки: " + (s_recircDamp / 10000).ToString() + " м²";
                recircSLabel.Show(); // Отображение площади
                TorqueRecirc_Spec(s_recircDamp); // Определение и отображение момента заслонки
                CheckMarkRecirc_Spec(); // Проверка наличия привода в спецификации
            }
            else // Пустые поля, либо ноль
            {
                recircSLabel.Hide(); recircTorqLabel.Hide(); markRecircPanel.Hide();
            }
        }

        ///<summary>Изменили высоту для рециркуляционной воздушной заслонки</summary>
        private void H_recircBox_TextChanged(object sender, EventArgs e)
        {
            bool b = b_recircBox.Text != string.Empty && h_recircBox.Text != string.Empty &&
               int.Parse(b_recircBox.Text) != 0 && int.Parse(h_recircBox.Text) != 0;
            if (b) // Не пустые поля и не ноль
            {
                s_recircDamp = int.Parse(b_recircBox.Text) * int.Parse(h_recircBox.Text);
                recircSLabel.Text = "Площадь заслонки: " + (s_recircDamp / 10000).ToString() + " м²";
                recircSLabel.Show(); // Отображение площади
                TorqueRecirc_Spec(s_recircDamp); // Определение и отображение момента заслонки
                CheckMarkRecirc_Spec(); // Проверка наличия привода в спецификации
            }
            else // Пустые поля, либо ноль
            {
                recircSLabel.Hide(); recircTorqLabel.Hide(); markRecircPanel.Hide();
            }
        }

        ///<summary>Расчет и отображение момента для рециркуляционной заслонки</summary>
        private void TorqueRecirc_Spec(double square)
        {
            const int VAL = 10000;
            square /= VAL; // Перевод в квадратные метры
            if (springRetRecircCheck.Checked) // С возвратной пружиной
            {
                if (square <= 0.6) torq_recircDamp = 3;
                else if (square <= 1.0) torq_recircDamp = 5;
                else if (square <= 1.5) torq_recircDamp = 10;
                else if (square <= 3.0) torq_recircDamp = 15;
                else if (square <= 4.0) torq_recircDamp = 20;
                else torq_recircDamp = 0;
            }
            else // Без пружинного возврата
            {
                if (square <= 0.5) torq_recircDamp = 2;
                else if (square <= 0.8) torq_recircDamp = 4;
                else if (square <= 1.2) torq_recircDamp = 6;
                else if (square <= 1.5) torq_recircDamp = 8;
                else if (square <= 3.0) torq_recircDamp = 16;
                else if (square <= 4.5) torq_recircDamp = 24;
                else if (square <= 6.0) torq_recircDamp = 40;
                else torq_recircDamp = 0;
            }
            if (torq_recircDamp != 0) // Определено значение момента по площади
            {
                recircTorqLabel.Text = "Крутящий момент: " + torq_recircDamp.ToString() + " Нм";
                recircTorqLabel.Show();
            }
            else recircTorqLabel.Hide(); // Скрытие отображения значения момента
        }

        ///<summary>Выбрали привод с пружинным возвратом для приточной заслонки</summary>
        private void SpringRetPrDampCheck_CheckedChanged(object sender, EventArgs e)
        {
            B_prDampBox_TextChanged(this, e); // Пересчет привода
        }

        ///<summary>Выбрали привод с пружинным возвратом для вытяжной заслонки</summary>
        private void SpringRetOutDampCheck_CheckedChanged(object sender, EventArgs e)
        {
            B_outDampBox_TextChanged(this, e); // Пересчет привода
        }

        ///<summary>Выбрали привод с пружинным возвратом для рециркуляционной заслонки</summary>
        private void SpringRetRecircCheck_CheckedChanged(object sender, EventArgs e)
        {
            B_recircBox_TextChanged(this, e); // Пересчет привода
        }

        ///<summary>Проверка условий для подбора привода П в спецификации</summary>
        private void CheckMarkPrDamp_Spec()
        {
            bool b = false; // Признак отображения подобранного привода
            // Есть приточная заслонка и расчетное значение момента (от 0 до 40 Нм)
            if (dampCheck.Checked && torq_prDamp > 0 && torq_prDamp <= 40) 
            {
                if (prDampPowCombo.SelectedIndex == 0) // 24 В
                {
                    if (confPrDampCheck.Checked) // Есть подтверждение открытия заслонки
                    {
                        // Привод с возвратной пружиной
                        if (springRetPrDampCheck.Checked && torq_prDamp <= 15.0) b = true;
                        // Привод без возвратной пружины
                        else if (!springRetPrDampCheck.Checked && torq_prDamp <= 40.0) b = true;
                    } 
                    else // Без подтверждения открытия заслонки
                    {
                        // Привод с возвратной пружиной
                        if (springRetPrDampCheck.Checked && torq_prDamp <= 5.0) b = true;
                        // Привод без возвратной пружины
                        else if (!springRetPrDampCheck.Checked && torq_prDamp <= 6.0) b = true;
                    }
                }
                else // 220 В
                {
                    if (confPrDampCheck.Checked) // Есть подтверждение открытия заслонки
                    {
                        // Привод с возвратной пружиной
                        if (springRetPrDampCheck.Checked && torq_prDamp <= 15.0) b = true;
                        // Привод без возвратной пружины
                        else if (!springRetPrDampCheck.Checked && torq_prDamp <= 40.0) b = true;
                    }
                    else // Без подтверждения открытия заслонки
                    {
                        // Привод с возвратной пружиной
                        if (springRetPrDampCheck.Checked && torq_prDamp <= 5.0) b = true;
                        // Привод без возвратной пружины
                        else if (!springRetPrDampCheck.Checked && torq_prDamp <= 6.0) b = true;
                    }
                }
            }
            if (b) markPrDampPanel.Show();
            else markPrDampPanel.Hide();
        }

        ///<summary>Проверка условий для подбора привода В в спецификации</summary>
        private void CheckMarkOutDamp_Spec()
        {
            bool b = false; // Признак отображения подобранного привода
            // Есть вытяжная заслонка, ПВ-система и расчетное значение момента (от 0 до 40 Нм)
            if (dampCheck.Checked && outDampCheck.Checked && comboSysType.SelectedIndex == 1 && 
                    torq_outDamp > 0 && torq_outDamp <= 40)
            {
                if (outDampPowCombo.SelectedIndex == 0) // 24 В
                {
                    if (confOutDampCheck.Checked) // Есть подтверждение открытия заслонки
                    {
                        // Привод с возвратной пружиной
                        if (springRetOutDampCheck.Checked && torq_outDamp <= 15.0) b = true;
                        // Привод без возвратной пружины
                        else if (!springRetOutDampCheck.Checked && torq_outDamp <= 40.0) b = true;
                    }
                    else // Без подтверждения открытия заслонки
                    {
                        // Привод с возвратной пружиной
                        if (springRetOutDampCheck.Checked && torq_outDamp <= 5.0) b = true;
                        // Привод без возвратной пружины
                        else if (!springRetOutDampCheck.Checked && torq_outDamp <= 6.0) b = true;
                    }
                }
                else // 220 В
                {
                    if (confOutDampCheck.Checked) // Есть подтверждение открытия заслонки
                    {
                        // Привод с возвратной пружиной
                        if (springRetOutDampCheck.Checked && torq_outDamp <= 15.0) b = true;
                        // Привод без возвратной пружины
                        else if (!springRetOutDampCheck.Checked && torq_outDamp <= 40.0) b = true;
                    }
                    else // Без подтверждения открытия заслонки
                    {
                        // Привод с возвратной пружиной
                        if (springRetOutDampCheck.Checked && torq_outDamp <= 5.0) b = true;
                        // Привод без возвратной пружины
                        else if (!springRetOutDampCheck.Checked && torq_outDamp <= 6.0) b = true;
                    }
                }
            }
            if (b) markOutDampPanel.Show();
            else markOutDampPanel.Hide();
        }

        ///<summary>Проверка условий для подбора привода рециркуляции в спецификации</summary>
        private void CheckMarkRecirc_Spec()
        {
            bool b = false; // Признак отображения подобранного привода
            // Есть рециркуляционная заслонка, ПВ-система и расчетное значение момента (от 0 до 40 Нм)
            if (recircCheck.Checked && comboSysType.SelectedIndex == 1 && 
                    torq_recircDamp > 0 && torq_recircDamp <= 40)
            {
                if (recircPowCombo.SelectedIndex == 0) // 24 В
                {
                    // Привод с возвратной пружиной
                    if (springRetRecircCheck.Checked && torq_recircDamp <= 5.0) b = true;
                    // Привод без возвратной пружины
                    else if (!springRetRecircCheck.Checked && torq_recircDamp <= 6.0) b = true;
                }
                else // 220 В
                {
                    // Привод с возвратной пружиной
                    if (springRetRecircCheck.Checked && torq_recircDamp <= 5.0) b = true;
                    // Привод без возвратной пружины
                    else if (!springRetRecircCheck.Checked && torq_recircDamp <= 6.0) b = true;
                }
            }
            if (b) markRecircPanel.Show();
            else markRecircPanel.Hide();
        }

        ///<summary>Изменили питание приточной воздушной заслонки</summary>
        private void PrDampPowCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckMarkPrDamp_Spec(); // Проверка для подбора привода в спецификации
        }

        ///<summary>Изменили питание вытяжной воздушной заслонки</summary>
        private void OutDampPowCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckMarkOutDamp_Spec(); // Проверка для подбора привода в спецификации
        }

        ///<summary>Изменили питание рециркуляционнной воздушной заслонки</summary>
        private void RecircPowCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckMarkRecirc_Spec(); // Проверка для подбора привода в спецификации
        }
    }
}