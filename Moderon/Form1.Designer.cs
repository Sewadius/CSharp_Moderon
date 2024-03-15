using System.Windows.Forms;

namespace Moderon
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (disposing)
            {
                //serialPort?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSpecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_help = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPage = new System.Windows.Forms.TabControl();
            this.sensorsPage = new System.Windows.Forms.TabPage();
            this.sensorsPanel = new System.Windows.Forms.Panel();
            this.fireTypeCombo = new System.Windows.Forms.ComboBox();
            this.label169 = new System.Windows.Forms.Label();
            this.label138 = new System.Windows.Forms.Label();
            this.fireCheck = new System.Windows.Forms.CheckBox();
            this.stopStartCheck = new System.Windows.Forms.CheckBox();
            this.sigFilAlarmCheck = new System.Windows.Forms.CheckBox();
            this.sigAlarmCheck = new System.Windows.Forms.CheckBox();
            this.sigWorkCheck = new System.Windows.Forms.CheckBox();
            this.label136 = new System.Windows.Forms.Label();
            this.outdoorChanSensCheck = new System.Windows.Forms.CheckBox();
            this.sensorPicture = new System.Windows.Forms.PictureBox();
            this.outChanSensCheck = new System.Windows.Forms.CheckBox();
            this.roomHumSensCheck = new System.Windows.Forms.CheckBox();
            this.chanHumSensCheck = new System.Windows.Forms.CheckBox();
            this.roomTempSensCheck = new System.Windows.Forms.CheckBox();
            this.prChanSensCheck = new System.Windows.Forms.CheckBox();
            this.label42 = new System.Windows.Forms.Label();
            this.fanPage = new System.Windows.Forms.TabPage();
            this.outFanPanel = new System.Windows.Forms.Panel();
            this.outFanFcTypeCombo = new System.Windows.Forms.ComboBox();
            this.label63 = new System.Windows.Forms.Label();
            this.outDampConfirmFanCheck = new System.Windows.Forms.CheckBox();
            this.outDampFanCheck = new System.Windows.Forms.CheckBox();
            this.outFanSpeedCheck = new System.Windows.Forms.CheckBox();
            this.outFanStStopCheck = new System.Windows.Forms.CheckBox();
            this.outFanAlarmCheck = new System.Windows.Forms.CheckBox();
            this.fanPicture2 = new System.Windows.Forms.PictureBox();
            this.curDefOutFanCheck = new System.Windows.Forms.CheckBox();
            this.outFanThermoCheck = new System.Windows.Forms.CheckBox();
            this.labelResOutFan_2 = new System.Windows.Forms.Label();
            this.powOutResFanBox = new System.Windows.Forms.TextBox();
            this.labelResOutFan = new System.Windows.Forms.Label();
            this.checkResOutFan = new System.Windows.Forms.CheckBox();
            this.label32 = new System.Windows.Forms.Label();
            this.powOutFanBox = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.outFanControlCombo = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.outFanFC_check = new System.Windows.Forms.CheckBox();
            this.outFanPSCheck = new System.Windows.Forms.CheckBox();
            this.outFanPowCombo = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.prFanPanel = new System.Windows.Forms.Panel();
            this.prFanFcTypeCombo = new System.Windows.Forms.ComboBox();
            this.label62 = new System.Windows.Forms.Label();
            this.prDampConfirmFanCheck = new System.Windows.Forms.CheckBox();
            this.prDampFanCheck = new System.Windows.Forms.CheckBox();
            this.prFanStStopCheck = new System.Windows.Forms.CheckBox();
            this.prFanAlarmCheck = new System.Windows.Forms.CheckBox();
            this.fanPicture1 = new System.Windows.Forms.PictureBox();
            this.curDefPrFanCheck = new System.Windows.Forms.CheckBox();
            this.prFanThermoCheck = new System.Windows.Forms.CheckBox();
            this.labelResPrFan_2 = new System.Windows.Forms.Label();
            this.powPrResFanBox = new System.Windows.Forms.TextBox();
            this.labelResPrFan = new System.Windows.Forms.Label();
            this.checkResPrFan = new System.Windows.Forms.CheckBox();
            this.label30 = new System.Windows.Forms.Label();
            this.powPrFanBox = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.prFanControlCombo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.prFanFC_check = new System.Windows.Forms.CheckBox();
            this.prFanPSCheck = new System.Windows.Forms.CheckBox();
            this.prFanPowCombo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.prFanSpeedCheck = new System.Windows.Forms.CheckBox();
            this.filterPage = new System.Windows.Forms.TabPage();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.filterPicture = new System.Windows.Forms.PictureBox();
            this.outFilterPanel = new System.Windows.Forms.Panel();
            this.filterOutCombo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.filterPrCombo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dampPage = new System.Windows.Forms.TabPage();
            this.dampPanel = new System.Windows.Forms.Panel();
            this.markPrDampPanel = new System.Windows.Forms.Panel();
            this.prDampTorqLabel = new System.Windows.Forms.Label();
            this.springRetPrDampCheck = new System.Windows.Forms.CheckBox();
            this.prDampSLabel = new System.Windows.Forms.Label();
            this.label168 = new System.Windows.Forms.Label();
            this.label167 = new System.Windows.Forms.Label();
            this.h_prDampBox = new System.Windows.Forms.TextBox();
            this.b_prDampBox = new System.Windows.Forms.TextBox();
            this.label166 = new System.Windows.Forms.Label();
            this.label158 = new System.Windows.Forms.Label();
            this.dampPicture = new System.Windows.Forms.PictureBox();
            this.outDampPanel = new System.Windows.Forms.Panel();
            this.markOutDampPanel = new System.Windows.Forms.Panel();
            this.outDampTorqLabel = new System.Windows.Forms.Label();
            this.outDampSLabel = new System.Windows.Forms.Label();
            this.springRetOutDampCheck = new System.Windows.Forms.CheckBox();
            this.cmhOutDampLabel = new System.Windows.Forms.Label();
            this.outDampCheck = new System.Windows.Forms.CheckBox();
            this.cmbOutDampLabel = new System.Windows.Forms.Label();
            this.heatOutDampCheck = new System.Windows.Forms.CheckBox();
            this.h_outDampBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.b_outDampBox = new System.Windows.Forms.TextBox();
            this.confOutDampCheck = new System.Windows.Forms.CheckBox();
            this.hOutDampLabel = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.bOutDampLabel = new System.Windows.Forms.Label();
            this.outDampPowCombo = new System.Windows.Forms.ComboBox();
            this.heatPrDampCheck = new System.Windows.Forms.CheckBox();
            this.confPrDampCheck = new System.Windows.Forms.CheckBox();
            this.prDampPowCombo = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.heatPage = new System.Windows.Forms.TabPage();
            this.heatPanel = new System.Windows.Forms.Panel();
            this.heatPicture = new System.Windows.Forms.PictureBox();
            this.elHeatPanel = new System.Windows.Forms.Panel();
            this.firstStHeatCombo = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.elHeatPowBox = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.thermSwitchCombo = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.elHeatStagesCombo = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.watHeatPanel = new System.Windows.Forms.Panel();
            this.confHeatResPumpCheck = new System.Windows.Forms.CheckBox();
            this.pumpCurResProtect = new System.Windows.Forms.CheckBox();
            this.reservPumpHeater = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.pumpCurProtect = new System.Windows.Forms.CheckBox();
            this.watSensHeatCheck = new System.Windows.Forms.CheckBox();
            this.analogSigHeatCheck = new System.Windows.Forms.CheckBox();
            this.confHeatPumpCheck = new System.Windows.Forms.CheckBox();
            this.powPumpCombo = new System.Windows.Forms.ComboBox();
            this.TF_heaterCheck = new System.Windows.Forms.CheckBox();
            this.heatTypeCombo = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.coolPage = new System.Windows.Forms.TabPage();
            this.coolPanel = new System.Windows.Forms.Panel();
            this.coolPicture = new System.Windows.Forms.PictureBox();
            this.watCoolPanel = new System.Windows.Forms.Panel();
            this.analogCoolCheck = new System.Windows.Forms.CheckBox();
            this.powWatCoolCombo = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.frCoolPanel = new System.Windows.Forms.Panel();
            this.analogFreonCheck = new System.Windows.Forms.CheckBox();
            this.thermoCoolerCheck = new System.Windows.Forms.CheckBox();
            this.dehumModeCheck = new System.Windows.Forms.CheckBox();
            this.alarmFrCoolCheck = new System.Windows.Forms.CheckBox();
            this.frCoolStagesCombo = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.coolTypeCombo = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.humidPage = new System.Windows.Forms.TabPage();
            this.humidPanel = new System.Windows.Forms.Panel();
            this.humidPicture = new System.Windows.Forms.PictureBox();
            this.cellHumidPanel = new System.Windows.Forms.Panel();
            this.powPumpHumidCheck = new System.Windows.Forms.CheckBox();
            this.label36 = new System.Windows.Forms.Label();
            this.steamHumidPanel = new System.Windows.Forms.Panel();
            this.alarmHumidCheck = new System.Windows.Forms.CheckBox();
            this.analogHumCheck = new System.Windows.Forms.CheckBox();
            this.startHumidCheck = new System.Windows.Forms.CheckBox();
            this.label35 = new System.Windows.Forms.Label();
            this.humidTypeCombo = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.recircPage = new System.Windows.Forms.TabPage();
            this.recircPanel = new System.Windows.Forms.Panel();
            this.recircPrDampAOCheck = new System.Windows.Forms.CheckBox();
            this.markRecircPanel = new System.Windows.Forms.Panel();
            this.recircTorqLabel = new System.Windows.Forms.Label();
            this.recircSLabel = new System.Windows.Forms.Label();
            this.label170 = new System.Windows.Forms.Label();
            this.label171 = new System.Windows.Forms.Label();
            this.h_recircBox = new System.Windows.Forms.TextBox();
            this.b_recircBox = new System.Windows.Forms.TextBox();
            this.label172 = new System.Windows.Forms.Label();
            this.label175 = new System.Windows.Forms.Label();
            this.springRetRecircCheck = new System.Windows.Forms.CheckBox();
            this.recircPicture = new System.Windows.Forms.PictureBox();
            this.recircAOSigCheck = new System.Windows.Forms.CheckBox();
            this.recircPowCombo = new System.Windows.Forms.ComboBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.recupPage = new System.Windows.Forms.TabPage();
            this.recupPanel = new System.Windows.Forms.Panel();
            this.recupPicture = new System.Windows.Forms.PictureBox();
            this.defRecupSensPanel = new System.Windows.Forms.Panel();
            this.recDefPsCheck = new System.Windows.Forms.CheckBox();
            this.label49 = new System.Windows.Forms.Label();
            this.recDefTempCheck = new System.Windows.Forms.CheckBox();
            this.plastRecupPanel = new System.Windows.Forms.Panel();
            this.bypassPlastCombo = new System.Windows.Forms.ComboBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.glikRecupPanel = new System.Windows.Forms.Panel();
            this.pumpGlikResCurProtect = new System.Windows.Forms.CheckBox();
            this.confGlikResPumpCheck = new System.Windows.Forms.CheckBox();
            this.reservPumpGlik = new System.Windows.Forms.CheckBox();
            this.pumpGlikCurProtect = new System.Windows.Forms.CheckBox();
            this.pumpGlikConfCheck = new System.Windows.Forms.CheckBox();
            this.pumpGlicRecCheck = new System.Windows.Forms.CheckBox();
            this.analogGlikRecCheck = new System.Windows.Forms.CheckBox();
            this.label50 = new System.Windows.Forms.Label();
            this.rotorRecupPanel = new System.Windows.Forms.Panel();
            this.analogRotRecCheck = new System.Windows.Forms.CheckBox();
            this.startRotRecCheck = new System.Windows.Forms.CheckBox();
            this.outSigAlarmRotRecCheck = new System.Windows.Forms.CheckBox();
            this.label46 = new System.Windows.Forms.Label();
            this.powRotRecBox = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.rotorPowCombo = new System.Windows.Forms.ComboBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.recupTypeCombo = new System.Windows.Forms.ComboBox();
            this.label43 = new System.Windows.Forms.Label();
            this.addHeatPage = new System.Windows.Forms.TabPage();
            this.secHeatPanel = new System.Windows.Forms.Panel();
            this.heatAddPicture = new System.Windows.Forms.PictureBox();
            this.elAddHeatPanel = new System.Windows.Forms.Panel();
            this.firstStAddHeatCombo = new System.Windows.Forms.ComboBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.elAddHeatPowBox = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.thermAddSwitchCombo = new System.Windows.Forms.ComboBox();
            this.label53 = new System.Windows.Forms.Label();
            this.elHeatAddStagesCombo = new System.Windows.Forms.ComboBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.watAddHeatPanel = new System.Windows.Forms.Panel();
            this.confAddHeatResPumpCheck = new System.Windows.Forms.CheckBox();
            this.pumpCurResAddProtect = new System.Windows.Forms.CheckBox();
            this.reservPumpAddHeater = new System.Windows.Forms.CheckBox();
            this.pumpCurAddProtect = new System.Windows.Forms.CheckBox();
            this.pumpAddHeatCheck = new System.Windows.Forms.CheckBox();
            this.sensWatAddHeatCheck = new System.Windows.Forms.CheckBox();
            this.checkBox27 = new System.Windows.Forms.CheckBox();
            this.confAddHeatPumpCheck = new System.Windows.Forms.CheckBox();
            this.powPumpAddCombo = new System.Windows.Forms.ComboBox();
            this.label56 = new System.Windows.Forms.Label();
            this.TF_addHeaterCheck = new System.Windows.Forms.CheckBox();
            this.label57 = new System.Windows.Forms.Label();
            this.heatAddTypeCombo = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.comboSysType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.heaterCheck = new System.Windows.Forms.CheckBox();
            this.coolerCheck = new System.Windows.Forms.CheckBox();
            this.humidCheck = new System.Windows.Forms.CheckBox();
            this.recircCheck = new System.Windows.Forms.CheckBox();
            this.recupCheck = new System.Windows.Forms.CheckBox();
            this.panelElements = new System.Windows.Forms.Panel();
            this.addHeatCheck = new System.Windows.Forms.CheckBox();
            this.filterCheck = new System.Windows.Forms.CheckBox();
            this.dampCheck = new System.Windows.Forms.CheckBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.loadModbusPanel = new System.Windows.Forms.Panel();
            this.saveBinFileButton = new System.Windows.Forms.Button();
            this.showWriteBoxCheck = new System.Windows.Forms.CheckBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.labelWriteNetTextBox = new System.Windows.Forms.Label();
            this.label137 = new System.Windows.Forms.Label();
            this.comboReadType = new System.Windows.Forms.ComboBox();
            this.connectCheck = new System.Windows.Forms.CheckBox();
            this.formNetButton = new System.Windows.Forms.Button();
            this.writeNetButton = new System.Windows.Forms.Button();
            this.writeNetTextBox = new System.Windows.Forms.TextBox();
            this.dataNetTextBox = new System.Windows.Forms.TextBox();
            this.readNetBtn = new System.Windows.Forms.Button();
            this.connectLabel = new System.Windows.Forms.Label();
            this.netPortBox = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.ipAddressBox = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backOptionsButton = new System.Windows.Forms.Button();
            this.formSignalsButton = new System.Windows.Forms.Button();
            this.signalsPanel = new System.Windows.Forms.Panel();
            this.resetButtonSignals = new System.Windows.Forms.Button();
            this.loadToExl = new System.Windows.Forms.Button();
            this.loadPLC_SignalsButton = new System.Windows.Forms.Button();
            this.signalsReadyLabel = new System.Windows.Forms.Label();
            this.tabControlSignals = new System.Windows.Forms.TabControl();
            this.tabUI = new System.Windows.Forms.TabPage();
            this.block3_UIpanel = new System.Windows.Forms.Panel();
            this.UI16_bl3Label = new System.Windows.Forms.Label();
            this.UI16bl3_combo = new System.Windows.Forms.ComboBox();
            this.UI16bl3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI16bl3_lab = new System.Windows.Forms.Label();
            this.UI15_bl3Label = new System.Windows.Forms.Label();
            this.UI15bl3_combo = new System.Windows.Forms.ComboBox();
            this.UI15bl3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI15bl3_lab = new System.Windows.Forms.Label();
            this.UI14_bl3Label = new System.Windows.Forms.Label();
            this.UI14bl3_combo = new System.Windows.Forms.ComboBox();
            this.UI14bl3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI14bl3_lab = new System.Windows.Forms.Label();
            this.UI13_bl3Label = new System.Windows.Forms.Label();
            this.UI13bl3_combo = new System.Windows.Forms.ComboBox();
            this.UI13bl3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI13bl3_lab = new System.Windows.Forms.Label();
            this.UI12_bl3Label = new System.Windows.Forms.Label();
            this.UI12bl3_combo = new System.Windows.Forms.ComboBox();
            this.UI12bl3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI12bl3_lab = new System.Windows.Forms.Label();
            this.UI11_bl3Label = new System.Windows.Forms.Label();
            this.UI11bl3_combo = new System.Windows.Forms.ComboBox();
            this.UI11bl3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI11bl3_lab = new System.Windows.Forms.Label();
            this.UI10_bl3Label = new System.Windows.Forms.Label();
            this.UI10bl3_combo = new System.Windows.Forms.ComboBox();
            this.UI10bl3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI10bl3_lab = new System.Windows.Forms.Label();
            this.UI9_bl3Label = new System.Windows.Forms.Label();
            this.UI9bl3_combo = new System.Windows.Forms.ComboBox();
            this.UI9bl3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI9bl3_lab = new System.Windows.Forms.Label();
            this.UI8_bl3Label = new System.Windows.Forms.Label();
            this.UI8bl3_combo = new System.Windows.Forms.ComboBox();
            this.UI8bl3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI8bl3_lab = new System.Windows.Forms.Label();
            this.UI7_bl3Label = new System.Windows.Forms.Label();
            this.UI7bl3_combo = new System.Windows.Forms.ComboBox();
            this.UI7bl3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI7bl3_lab = new System.Windows.Forms.Label();
            this.UI6_bl3Label = new System.Windows.Forms.Label();
            this.UI6bl3_combo = new System.Windows.Forms.ComboBox();
            this.UI6bl3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI6bl3_lab = new System.Windows.Forms.Label();
            this.UI5_bl3Label = new System.Windows.Forms.Label();
            this.UI5bl3_combo = new System.Windows.Forms.ComboBox();
            this.UI5bl3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI5bl3_lab = new System.Windows.Forms.Label();
            this.UI4_bl3Label = new System.Windows.Forms.Label();
            this.UI4bl3_combo = new System.Windows.Forms.ComboBox();
            this.UI4bl3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI4bl3_lab = new System.Windows.Forms.Label();
            this.UI3_bl3Label = new System.Windows.Forms.Label();
            this.UI3bl3_combo = new System.Windows.Forms.ComboBox();
            this.UI3bl3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI3bl3_lab = new System.Windows.Forms.Label();
            this.UI2_bl3Label = new System.Windows.Forms.Label();
            this.UI2bl3_combo = new System.Windows.Forms.ComboBox();
            this.UI2bl3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI2bl3_lab = new System.Windows.Forms.Label();
            this.UI1_bl3Label = new System.Windows.Forms.Label();
            this.UI1bl3_combo = new System.Windows.Forms.ComboBox();
            this.UIblock3_header = new System.Windows.Forms.Label();
            this.UI1bl3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI1bl3_lab = new System.Windows.Forms.Label();
            this.block2_UIpanel = new System.Windows.Forms.Panel();
            this.UI16_bl2Label = new System.Windows.Forms.Label();
            this.UI16bl2_combo = new System.Windows.Forms.ComboBox();
            this.UI16bl2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI16bl2_lab = new System.Windows.Forms.Label();
            this.UI15_bl2Label = new System.Windows.Forms.Label();
            this.UI15bl2_combo = new System.Windows.Forms.ComboBox();
            this.UI15bl2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI15bl2_lab = new System.Windows.Forms.Label();
            this.UI14_bl2Label = new System.Windows.Forms.Label();
            this.UI14bl2_combo = new System.Windows.Forms.ComboBox();
            this.UI14bl2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI14bl2_lab = new System.Windows.Forms.Label();
            this.UI13_bl2Label = new System.Windows.Forms.Label();
            this.UI13bl2_combo = new System.Windows.Forms.ComboBox();
            this.UI13bl2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI13bl2_lab = new System.Windows.Forms.Label();
            this.UI12_bl2Label = new System.Windows.Forms.Label();
            this.UI12bl2_combo = new System.Windows.Forms.ComboBox();
            this.UI12bl2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI12bl2_lab = new System.Windows.Forms.Label();
            this.UI11_bl2Label = new System.Windows.Forms.Label();
            this.UI11bl2_combo = new System.Windows.Forms.ComboBox();
            this.UI11bl2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI11bl2_lab = new System.Windows.Forms.Label();
            this.UI10_bl2Label = new System.Windows.Forms.Label();
            this.UI10bl2_combo = new System.Windows.Forms.ComboBox();
            this.UI10bl2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI10bl2_lab = new System.Windows.Forms.Label();
            this.UI9_bl2Label = new System.Windows.Forms.Label();
            this.UI9bl2_combo = new System.Windows.Forms.ComboBox();
            this.UI9bl2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI9bl2_lab = new System.Windows.Forms.Label();
            this.UI8_bl2Label = new System.Windows.Forms.Label();
            this.UI8bl2_combo = new System.Windows.Forms.ComboBox();
            this.UI8bl2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI8bl2_lab = new System.Windows.Forms.Label();
            this.UI7_bl2Label = new System.Windows.Forms.Label();
            this.UI7bl2_combo = new System.Windows.Forms.ComboBox();
            this.UI7bl2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI7bl2_lab = new System.Windows.Forms.Label();
            this.UI6_bl2Label = new System.Windows.Forms.Label();
            this.UI6bl2_combo = new System.Windows.Forms.ComboBox();
            this.UI6bl2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI6bl2_lab = new System.Windows.Forms.Label();
            this.UI5_bl2Label = new System.Windows.Forms.Label();
            this.UI5bl2_combo = new System.Windows.Forms.ComboBox();
            this.UI5bl2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI5bl2_lab = new System.Windows.Forms.Label();
            this.UI4_bl2Label = new System.Windows.Forms.Label();
            this.UI4bl2_combo = new System.Windows.Forms.ComboBox();
            this.UI4bl2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI4bl2_lab = new System.Windows.Forms.Label();
            this.UI3_bl2Label = new System.Windows.Forms.Label();
            this.UI3bl2_combo = new System.Windows.Forms.ComboBox();
            this.UI3bl2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI3bl2_lab = new System.Windows.Forms.Label();
            this.UI2_bl2Label = new System.Windows.Forms.Label();
            this.UI2bl2_combo = new System.Windows.Forms.ComboBox();
            this.UI2bl2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI2bl2_lab = new System.Windows.Forms.Label();
            this.UI1_bl2Label = new System.Windows.Forms.Label();
            this.UI1bl2_combo = new System.Windows.Forms.ComboBox();
            this.UIblock2_header = new System.Windows.Forms.Label();
            this.UI1bl2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI1bl2_lab = new System.Windows.Forms.Label();
            this.block1_UIpanel = new System.Windows.Forms.Panel();
            this.UI16_bl1Label = new System.Windows.Forms.Label();
            this.UI16bl1_combo = new System.Windows.Forms.ComboBox();
            this.UI16bl1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI16bl1_lab = new System.Windows.Forms.Label();
            this.UI15_bl1Label = new System.Windows.Forms.Label();
            this.UI15bl1_combo = new System.Windows.Forms.ComboBox();
            this.UI15bl1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI15bl1_lab = new System.Windows.Forms.Label();
            this.UI14_bl1Label = new System.Windows.Forms.Label();
            this.UI14bl1_combo = new System.Windows.Forms.ComboBox();
            this.UI14bl1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI14bl1_lab = new System.Windows.Forms.Label();
            this.UI13_bl1Label = new System.Windows.Forms.Label();
            this.UI13bl1_combo = new System.Windows.Forms.ComboBox();
            this.UI13bl1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI13bl1_lab = new System.Windows.Forms.Label();
            this.UI12_bl1Label = new System.Windows.Forms.Label();
            this.UI12bl1_combo = new System.Windows.Forms.ComboBox();
            this.UI12bl1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI12bl1_lab = new System.Windows.Forms.Label();
            this.UI11_bl1Label = new System.Windows.Forms.Label();
            this.UI11bl1_combo = new System.Windows.Forms.ComboBox();
            this.UI11bl1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI11bl1_lab = new System.Windows.Forms.Label();
            this.UI10_bl1Label = new System.Windows.Forms.Label();
            this.UI10bl1_combo = new System.Windows.Forms.ComboBox();
            this.UI10bl1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI10bl1_lab = new System.Windows.Forms.Label();
            this.UI9_bl1Label = new System.Windows.Forms.Label();
            this.UI9bl1_combo = new System.Windows.Forms.ComboBox();
            this.UI9bl1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI9bl1_lab = new System.Windows.Forms.Label();
            this.UI8_bl1Label = new System.Windows.Forms.Label();
            this.UI8bl1_combo = new System.Windows.Forms.ComboBox();
            this.UI8bl1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI8bl1_lab = new System.Windows.Forms.Label();
            this.UI7_bl1Label = new System.Windows.Forms.Label();
            this.UI7bl1_combo = new System.Windows.Forms.ComboBox();
            this.UI7bl1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI7bl1_lab = new System.Windows.Forms.Label();
            this.UI6_bl1Label = new System.Windows.Forms.Label();
            this.UI6bl1_combo = new System.Windows.Forms.ComboBox();
            this.UI6bl1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI6bl1_lab = new System.Windows.Forms.Label();
            this.UI5_bl1Label = new System.Windows.Forms.Label();
            this.UI5bl1_combo = new System.Windows.Forms.ComboBox();
            this.UI5bl1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI5bl1_lab = new System.Windows.Forms.Label();
            this.UI4_bl1Label = new System.Windows.Forms.Label();
            this.UI4bl1_combo = new System.Windows.Forms.ComboBox();
            this.UI4bl1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI4bl1_lab = new System.Windows.Forms.Label();
            this.UI3_bl1Label = new System.Windows.Forms.Label();
            this.UI3bl1_combo = new System.Windows.Forms.ComboBox();
            this.UI3bl1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI3bl1_lab = new System.Windows.Forms.Label();
            this.UI2_bl1Label = new System.Windows.Forms.Label();
            this.UI2bl1_combo = new System.Windows.Forms.ComboBox();
            this.UI2bl1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI2bl1_lab = new System.Windows.Forms.Label();
            this.UI1_bl1Label = new System.Windows.Forms.Label();
            this.UI1bl1_combo = new System.Windows.Forms.ComboBox();
            this.UIblock1_header = new System.Windows.Forms.Label();
            this.UI1bl1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI1bl1_lab = new System.Windows.Forms.Label();
            this.plk_UIpanel = new System.Windows.Forms.Panel();
            this.UIplk_header = new System.Windows.Forms.Label();
            this.UI11_lab = new System.Windows.Forms.Label();
            this.UI1_plkLabel = new System.Windows.Forms.Label();
            this.UI11_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI1_combo = new System.Windows.Forms.ComboBox();
            this.UI11_combo = new System.Windows.Forms.ComboBox();
            this.UI1_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI11_plkLabel = new System.Windows.Forms.Label();
            this.UI1_lab = new System.Windows.Forms.Label();
            this.UI10_lab = new System.Windows.Forms.Label();
            this.UI2_plkLabel = new System.Windows.Forms.Label();
            this.UI10_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI2_combo = new System.Windows.Forms.ComboBox();
            this.UI10_combo = new System.Windows.Forms.ComboBox();
            this.UI2_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI10_plkLabel = new System.Windows.Forms.Label();
            this.UI2_lab = new System.Windows.Forms.Label();
            this.UI9_lab = new System.Windows.Forms.Label();
            this.UI3_plkLabel = new System.Windows.Forms.Label();
            this.UI9_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI3_combo = new System.Windows.Forms.ComboBox();
            this.UI9_combo = new System.Windows.Forms.ComboBox();
            this.UI3_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI9_plkLabel = new System.Windows.Forms.Label();
            this.UI3_lab = new System.Windows.Forms.Label();
            this.UI8_lab = new System.Windows.Forms.Label();
            this.UI4_plkLabel = new System.Windows.Forms.Label();
            this.UI8_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI4_combo = new System.Windows.Forms.ComboBox();
            this.UI8_combo = new System.Windows.Forms.ComboBox();
            this.UI4_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI8_plkLabel = new System.Windows.Forms.Label();
            this.UI4_lab = new System.Windows.Forms.Label();
            this.UI7_lab = new System.Windows.Forms.Label();
            this.UI5_plkLabel = new System.Windows.Forms.Label();
            this.UI7_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI5_combo = new System.Windows.Forms.ComboBox();
            this.UI7_combo = new System.Windows.Forms.ComboBox();
            this.UI5_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI7_plkLabel = new System.Windows.Forms.Label();
            this.UI5_lab = new System.Windows.Forms.Label();
            this.UI6_lab = new System.Windows.Forms.Label();
            this.UI6_plkLabel = new System.Windows.Forms.Label();
            this.UI6_typeCombo = new System.Windows.Forms.ComboBox();
            this.UI6_combo = new System.Windows.Forms.ComboBox();
            this.tabDO = new System.Windows.Forms.TabPage();
            this.plk_DOpanel = new System.Windows.Forms.Panel();
            this.DOplk_header = new System.Windows.Forms.Label();
            this.DO1_combo = new System.Windows.Forms.ComboBox();
            this.DO6_lab = new System.Windows.Forms.Label();
            this.DO1_plkLabel = new System.Windows.Forms.Label();
            this.DO5_lab = new System.Windows.Forms.Label();
            this.DO1_lab = new System.Windows.Forms.Label();
            this.DO4_lab = new System.Windows.Forms.Label();
            this.DO2_combo = new System.Windows.Forms.ComboBox();
            this.DO3_lab = new System.Windows.Forms.Label();
            this.DO2_plkLabel = new System.Windows.Forms.Label();
            this.DO2_lab = new System.Windows.Forms.Label();
            this.DO3_plkLabel = new System.Windows.Forms.Label();
            this.DO6_combo = new System.Windows.Forms.ComboBox();
            this.DO4_plkLabel = new System.Windows.Forms.Label();
            this.DO5_combo = new System.Windows.Forms.ComboBox();
            this.DO5_plkLabel = new System.Windows.Forms.Label();
            this.DO4_combo = new System.Windows.Forms.ComboBox();
            this.DO6_plkLabel = new System.Windows.Forms.Label();
            this.DO3_combo = new System.Windows.Forms.ComboBox();
            this.block3_DOpanel = new System.Windows.Forms.Panel();
            this.DO8bl3_lab = new System.Windows.Forms.Label();
            this.DO8bl3_combo = new System.Windows.Forms.ComboBox();
            this.DO8_bl3Label = new System.Windows.Forms.Label();
            this.DO7bl3_lab = new System.Windows.Forms.Label();
            this.DO7bl3_combo = new System.Windows.Forms.ComboBox();
            this.DO7_bl3Label = new System.Windows.Forms.Label();
            this.DO6bl3_lab = new System.Windows.Forms.Label();
            this.DO6bl3_combo = new System.Windows.Forms.ComboBox();
            this.DO6_bl3Label = new System.Windows.Forms.Label();
            this.DO5bl3_lab = new System.Windows.Forms.Label();
            this.DO5bl3_combo = new System.Windows.Forms.ComboBox();
            this.DO5_bl3Label = new System.Windows.Forms.Label();
            this.DO4bl3_lab = new System.Windows.Forms.Label();
            this.DO4bl3_combo = new System.Windows.Forms.ComboBox();
            this.DO4_bl3Label = new System.Windows.Forms.Label();
            this.DO3bl3_lab = new System.Windows.Forms.Label();
            this.DO3bl3_combo = new System.Windows.Forms.ComboBox();
            this.DO3_bl3Label = new System.Windows.Forms.Label();
            this.DO2bl3_lab = new System.Windows.Forms.Label();
            this.DO2bl3_combo = new System.Windows.Forms.ComboBox();
            this.DO2_bl3Label = new System.Windows.Forms.Label();
            this.DO1bl3_lab = new System.Windows.Forms.Label();
            this.DOblock3_header = new System.Windows.Forms.Label();
            this.DO1bl3_combo = new System.Windows.Forms.ComboBox();
            this.DO1_bl3Label = new System.Windows.Forms.Label();
            this.block2_DOpanel = new System.Windows.Forms.Panel();
            this.DO8bl2_lab = new System.Windows.Forms.Label();
            this.DO8bl2_combo = new System.Windows.Forms.ComboBox();
            this.DO8_bl2Label = new System.Windows.Forms.Label();
            this.DO7bl2_lab = new System.Windows.Forms.Label();
            this.DO1bl2_lab = new System.Windows.Forms.Label();
            this.DO2bl2_lab = new System.Windows.Forms.Label();
            this.DO3bl2_lab = new System.Windows.Forms.Label();
            this.DO4bl2_lab = new System.Windows.Forms.Label();
            this.DO5bl2_lab = new System.Windows.Forms.Label();
            this.DO6bl2_lab = new System.Windows.Forms.Label();
            this.DO7bl2_combo = new System.Windows.Forms.ComboBox();
            this.DOblock2_header = new System.Windows.Forms.Label();
            this.DO7_bl2Label = new System.Windows.Forms.Label();
            this.DO1bl2_combo = new System.Windows.Forms.ComboBox();
            this.DO6bl2_combo = new System.Windows.Forms.ComboBox();
            this.DO1_bl2Label = new System.Windows.Forms.Label();
            this.DO5bl2_combo = new System.Windows.Forms.ComboBox();
            this.DO2_bl2Label = new System.Windows.Forms.Label();
            this.DO4bl2_combo = new System.Windows.Forms.ComboBox();
            this.DO3_bl2Label = new System.Windows.Forms.Label();
            this.DO3bl2_combo = new System.Windows.Forms.ComboBox();
            this.DO4_bl2Label = new System.Windows.Forms.Label();
            this.DO2bl2_combo = new System.Windows.Forms.ComboBox();
            this.DO5_bl2Label = new System.Windows.Forms.Label();
            this.DO6_bl2Label = new System.Windows.Forms.Label();
            this.block1_DOpanel = new System.Windows.Forms.Panel();
            this.DO8bl1_lab = new System.Windows.Forms.Label();
            this.DO8bl1_combo = new System.Windows.Forms.ComboBox();
            this.DO8_bl1Label = new System.Windows.Forms.Label();
            this.DO7bl1_lab = new System.Windows.Forms.Label();
            this.DO1bl1_lab = new System.Windows.Forms.Label();
            this.DO2bl1_lab = new System.Windows.Forms.Label();
            this.DO3bl1_lab = new System.Windows.Forms.Label();
            this.DO4bl1_lab = new System.Windows.Forms.Label();
            this.DO5bl1_lab = new System.Windows.Forms.Label();
            this.DO6bl1_lab = new System.Windows.Forms.Label();
            this.DO7bl1_combo = new System.Windows.Forms.ComboBox();
            this.DOblock1_header = new System.Windows.Forms.Label();
            this.DO7_bl1Label = new System.Windows.Forms.Label();
            this.DO1bl1_combo = new System.Windows.Forms.ComboBox();
            this.DO6bl1_combo = new System.Windows.Forms.ComboBox();
            this.DO1_bl1Label = new System.Windows.Forms.Label();
            this.DO5bl1_combo = new System.Windows.Forms.ComboBox();
            this.DO2_bl1Label = new System.Windows.Forms.Label();
            this.DO4bl1_combo = new System.Windows.Forms.ComboBox();
            this.DO3_bl1Label = new System.Windows.Forms.Label();
            this.DO3bl1_combo = new System.Windows.Forms.ComboBox();
            this.DO4_bl1Label = new System.Windows.Forms.Label();
            this.DO2bl1_combo = new System.Windows.Forms.ComboBox();
            this.DO5_bl1Label = new System.Windows.Forms.Label();
            this.DO6_bl1Label = new System.Windows.Forms.Label();
            this.tabAO = new System.Windows.Forms.TabPage();
            this.plk_AOpanel = new System.Windows.Forms.Panel();
            this.AOplk_header = new System.Windows.Forms.Label();
            this.AO1_combo = new System.Windows.Forms.ComboBox();
            this.AO3_lab = new System.Windows.Forms.Label();
            this.AO1_plkLabel = new System.Windows.Forms.Label();
            this.AO2_lab = new System.Windows.Forms.Label();
            this.AO1_lab = new System.Windows.Forms.Label();
            this.AO3_combo = new System.Windows.Forms.ComboBox();
            this.AO2_plkLabel = new System.Windows.Forms.Label();
            this.AO3_plkLabel = new System.Windows.Forms.Label();
            this.AO2_combo = new System.Windows.Forms.ComboBox();
            this.block3_AOpanel = new System.Windows.Forms.Panel();
            this.AO2bl3_lab = new System.Windows.Forms.Label();
            this.AO2bl3_combo = new System.Windows.Forms.ComboBox();
            this.AO2_bl3Label = new System.Windows.Forms.Label();
            this.AO1bl3_lab = new System.Windows.Forms.Label();
            this.AOblock3_header = new System.Windows.Forms.Label();
            this.AO1bl3_combo = new System.Windows.Forms.ComboBox();
            this.AO1_bl3Label = new System.Windows.Forms.Label();
            this.block2_AOpanel = new System.Windows.Forms.Panel();
            this.AO1bl2_lab = new System.Windows.Forms.Label();
            this.AO2bl2_lab = new System.Windows.Forms.Label();
            this.AO1bl2_combo = new System.Windows.Forms.ComboBox();
            this.AO2bl2_combo = new System.Windows.Forms.ComboBox();
            this.AOblock2_header = new System.Windows.Forms.Label();
            this.AO1_bl2Label = new System.Windows.Forms.Label();
            this.AO2_bl2Label = new System.Windows.Forms.Label();
            this.block1_AOpanel = new System.Windows.Forms.Panel();
            this.AO1bl1_lab = new System.Windows.Forms.Label();
            this.AO2bl1_lab = new System.Windows.Forms.Label();
            this.AO1bl1_combo = new System.Windows.Forms.ComboBox();
            this.AO2bl1_combo = new System.Windows.Forms.ComboBox();
            this.AOblock1_header = new System.Windows.Forms.Label();
            this.AO1_bl1Label = new System.Windows.Forms.Label();
            this.AO2_bl1Label = new System.Windows.Forms.Label();
            this.tabCmdWord = new System.Windows.Forms.TabPage();
            this.cmdWordsTextBox = new System.Windows.Forms.RichTextBox();
            this.backSignalsButton = new System.Windows.Forms.Button();
            this.helpPanel = new System.Windows.Forms.Panel();
            this.showHintCheck = new System.Windows.Forms.CheckBox();
            this.PDF_manual = new AxAcroPDFLib.AxAcroPDF();
            this.label140 = new System.Windows.Forms.Label();
            this.linkModeronWeb = new System.Windows.Forms.LinkLabel();
            this.backHelpButton = new System.Windows.Forms.Button();
            this.label_comboSysType = new System.Windows.Forms.Label();
            this.loadCanPanel = new System.Windows.Forms.Panel();
            this.processWriteLabel = new System.Windows.Forms.Label();
            this.progressBarWrite = new System.Windows.Forms.ProgressBar();
            this.backConnectLabel = new System.Windows.Forms.Label();
            this.dataMatchPLC_label = new System.Windows.Forms.Label();
            this.readCanButton = new System.Windows.Forms.Button();
            this.loadCanButton = new System.Windows.Forms.Button();
            this.refreshCanPorts = new System.Windows.Forms.PictureBox();
            this.canSelectBox = new System.Windows.Forms.ComboBox();
            this.connectCanLabel = new System.Windows.Forms.Label();
            this.label181 = new System.Windows.Forms.Label();
            this.backCanPanelButton = new System.Windows.Forms.Button();
            this.writeCanTextBox = new System.Windows.Forms.TextBox();
            this.label180 = new System.Windows.Forms.Label();
            this.dataCanTextBox = new System.Windows.Forms.TextBox();
            this.label179 = new System.Windows.Forms.Label();
            this.connectPlkBtn = new System.Windows.Forms.Button();
            this.parityCanCombo = new System.Windows.Forms.ComboBox();
            this.label178 = new System.Windows.Forms.Label();
            this.label177 = new System.Windows.Forms.Label();
            this.speedCanCombo = new System.Windows.Forms.ComboBox();
            this.label176 = new System.Windows.Forms.Label();
            this.canAddressBox = new System.Windows.Forms.TextBox();
            this.label174 = new System.Windows.Forms.Label();
            this.label173 = new System.Windows.Forms.Label();
            this.netOptionLabel = new System.Windows.Forms.Label();
            this.comboPlkType = new System.Windows.Forms.ComboBox();
            this.panelBlocks = new System.Windows.Forms.Panel();
            this.M72E16NA_label = new System.Windows.Forms.Label();
            this.M72E12RA_label = new System.Windows.Forms.Label();
            this.M72E08RA_label = new System.Windows.Forms.Label();
            this.M72E12RB_label = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.pic_signalsReady = new System.Windows.Forms.PictureBox();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.mainPage.SuspendLayout();
            this.sensorsPage.SuspendLayout();
            this.sensorsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sensorPicture)).BeginInit();
            this.fanPage.SuspendLayout();
            this.outFanPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fanPicture2)).BeginInit();
            this.prFanPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fanPicture1)).BeginInit();
            this.filterPage.SuspendLayout();
            this.filterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filterPicture)).BeginInit();
            this.outFilterPanel.SuspendLayout();
            this.dampPage.SuspendLayout();
            this.dampPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dampPicture)).BeginInit();
            this.outDampPanel.SuspendLayout();
            this.heatPage.SuspendLayout();
            this.heatPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heatPicture)).BeginInit();
            this.elHeatPanel.SuspendLayout();
            this.watHeatPanel.SuspendLayout();
            this.coolPage.SuspendLayout();
            this.coolPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coolPicture)).BeginInit();
            this.watCoolPanel.SuspendLayout();
            this.frCoolPanel.SuspendLayout();
            this.humidPage.SuspendLayout();
            this.humidPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.humidPicture)).BeginInit();
            this.cellHumidPanel.SuspendLayout();
            this.steamHumidPanel.SuspendLayout();
            this.recircPage.SuspendLayout();
            this.recircPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recircPicture)).BeginInit();
            this.recupPage.SuspendLayout();
            this.recupPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recupPicture)).BeginInit();
            this.defRecupSensPanel.SuspendLayout();
            this.plastRecupPanel.SuspendLayout();
            this.glikRecupPanel.SuspendLayout();
            this.rotorRecupPanel.SuspendLayout();
            this.addHeatPage.SuspendLayout();
            this.secHeatPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heatAddPicture)).BeginInit();
            this.elAddHeatPanel.SuspendLayout();
            this.watAddHeatPanel.SuspendLayout();
            this.panelElements.SuspendLayout();
            this.loadModbusPanel.SuspendLayout();
            this.signalsPanel.SuspendLayout();
            this.tabControlSignals.SuspendLayout();
            this.tabUI.SuspendLayout();
            this.block3_UIpanel.SuspendLayout();
            this.block2_UIpanel.SuspendLayout();
            this.block1_UIpanel.SuspendLayout();
            this.plk_UIpanel.SuspendLayout();
            this.tabDO.SuspendLayout();
            this.plk_DOpanel.SuspendLayout();
            this.block3_DOpanel.SuspendLayout();
            this.block2_DOpanel.SuspendLayout();
            this.block1_DOpanel.SuspendLayout();
            this.tabAO.SuspendLayout();
            this.plk_AOpanel.SuspendLayout();
            this.block3_AOpanel.SuspendLayout();
            this.block2_AOpanel.SuspendLayout();
            this.block1_AOpanel.SuspendLayout();
            this.tabCmdWord.SuspendLayout();
            this.helpPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PDF_manual)).BeginInit();
            this.loadCanPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refreshCanPorts)).BeginInit();
            this.panelBlocks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_signalsReady)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.ToolStripMenuItem_help,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(979, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.saveSpecToolStripMenuItem,
            this.ToolStripMenuItem_exit});
            this.файлToolStripMenuItem.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.saveToolStripMenuItem.Text = "Сохранить подбор";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.loadToolStripMenuItem.Text = "Загрузить подбор";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
            // 
            // saveSpecToolStripMenuItem
            // 
            this.saveSpecToolStripMenuItem.Name = "saveSpecToolStripMenuItem";
            this.saveSpecToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.saveSpecToolStripMenuItem.Text = "Сохранить спецификацию";
            this.saveSpecToolStripMenuItem.Visible = false;
            this.saveSpecToolStripMenuItem.Click += new System.EventHandler(this.SaveSpecToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem_exit
            // 
            this.ToolStripMenuItem_exit.Name = "ToolStripMenuItem_exit";
            this.ToolStripMenuItem_exit.Size = new System.Drawing.Size(249, 22);
            this.ToolStripMenuItem_exit.Text = "Выход";
            this.ToolStripMenuItem_exit.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem_help
            // 
            this.ToolStripMenuItem_help.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ToolStripMenuItem_help.Name = "ToolStripMenuItem_help";
            this.ToolStripMenuItem_help.Size = new System.Drawing.Size(71, 20);
            this.ToolStripMenuItem_help.Text = "Помощь";
            this.ToolStripMenuItem_help.Click += new System.EventHandler(this.ToolStripMenuItem_help_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.aboutToolStripMenuItem.Text = "О программе";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // mainPage
            // 
            this.mainPage.Controls.Add(this.sensorsPage);
            this.mainPage.Controls.Add(this.fanPage);
            this.mainPage.Controls.Add(this.filterPage);
            this.mainPage.Controls.Add(this.dampPage);
            this.mainPage.Controls.Add(this.heatPage);
            this.mainPage.Controls.Add(this.coolPage);
            this.mainPage.Controls.Add(this.humidPage);
            this.mainPage.Controls.Add(this.recircPage);
            this.mainPage.Controls.Add(this.recupPage);
            this.mainPage.Controls.Add(this.addHeatPage);
            this.mainPage.Location = new System.Drawing.Point(15, 75);
            this.mainPage.Name = "mainPage";
            this.mainPage.SelectedIndex = 0;
            this.mainPage.Size = new System.Drawing.Size(750, 40);
            this.mainPage.TabIndex = 1;
            // 
            // sensorsPage
            // 
            this.sensorsPage.BackColor = System.Drawing.SystemColors.Control;
            this.sensorsPage.Controls.Add(this.sensorsPanel);
            this.sensorsPage.Location = new System.Drawing.Point(4, 22);
            this.sensorsPage.Name = "sensorsPage";
            this.sensorsPage.Size = new System.Drawing.Size(742, 14);
            this.sensorsPage.TabIndex = 9;
            this.sensorsPage.Text = "ДАТЧИКИ/СИГНАЛЫ";
            // 
            // sensorsPanel
            // 
            this.sensorsPanel.Controls.Add(this.fireTypeCombo);
            this.sensorsPanel.Controls.Add(this.label169);
            this.sensorsPanel.Controls.Add(this.label138);
            this.sensorsPanel.Controls.Add(this.fireCheck);
            this.sensorsPanel.Controls.Add(this.stopStartCheck);
            this.sensorsPanel.Controls.Add(this.sigFilAlarmCheck);
            this.sensorsPanel.Controls.Add(this.sigAlarmCheck);
            this.sensorsPanel.Controls.Add(this.sigWorkCheck);
            this.sensorsPanel.Controls.Add(this.label136);
            this.sensorsPanel.Controls.Add(this.outdoorChanSensCheck);
            this.sensorsPanel.Controls.Add(this.sensorPicture);
            this.sensorsPanel.Controls.Add(this.outChanSensCheck);
            this.sensorsPanel.Controls.Add(this.roomHumSensCheck);
            this.sensorsPanel.Controls.Add(this.chanHumSensCheck);
            this.sensorsPanel.Controls.Add(this.roomTempSensCheck);
            this.sensorsPanel.Controls.Add(this.prChanSensCheck);
            this.sensorsPanel.Controls.Add(this.label42);
            this.sensorsPanel.Location = new System.Drawing.Point(3, 3);
            this.sensorsPanel.Name = "sensorsPanel";
            this.sensorsPanel.Size = new System.Drawing.Size(717, 479);
            this.sensorsPanel.TabIndex = 3;
            // 
            // fireTypeCombo
            // 
            this.fireTypeCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.fireTypeCombo.DisplayMember = "380 В";
            this.fireTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fireTypeCombo.Enabled = false;
            this.fireTypeCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fireTypeCombo.ForeColor = System.Drawing.Color.White;
            this.fireTypeCombo.FormattingEnabled = true;
            this.fireTypeCombo.Items.AddRange(new object[] {
            "НО",
            "НЗ"});
            this.fireTypeCombo.Location = new System.Drawing.Point(225, 402);
            this.fireTypeCombo.Name = "fireTypeCombo";
            this.fireTypeCombo.Size = new System.Drawing.Size(59, 21);
            this.fireTypeCombo.TabIndex = 62;
            this.fireTypeCombo.SelectedIndexChanged += new System.EventHandler(this.FireTypeCombo_SelectedIndexChanged);
            // 
            // label169
            // 
            this.label169.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label169.AutoSize = true;
            this.label169.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label169.Location = new System.Drawing.Point(15, 404);
            this.label169.Name = "label169";
            this.label169.Size = new System.Drawing.Size(203, 16);
            this.label169.TabIndex = 61;
            this.label169.Text = "Тип пожарной сигнализации";
            // 
            // label138
            // 
            this.label138.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label138.AutoSize = true;
            this.label138.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label138.Location = new System.Drawing.Point(350, 289);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(183, 16);
            this.label138.TabIndex = 60;
            this.label138.Text = "ВНЕШНИЕ СИГНАЛЫ DO";
            // 
            // fireCheck
            // 
            this.fireCheck.AutoSize = true;
            this.fireCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fireCheck.Location = new System.Drawing.Point(15, 364);
            this.fireCheck.Name = "fireCheck";
            this.fireCheck.Size = new System.Drawing.Size(237, 18);
            this.fireCheck.TabIndex = 59;
            this.fireCheck.Text = "Сигнал пожарной сигнализации";
            this.fireCheck.UseVisualStyleBackColor = true;
            this.fireCheck.CheckedChanged += new System.EventHandler(this.FireCheck_cmdCheckedChanged);
            // 
            // stopStartCheck
            // 
            this.stopStartCheck.AutoSize = true;
            this.stopStartCheck.Checked = true;
            this.stopStartCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.stopStartCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stopStartCheck.Location = new System.Drawing.Point(15, 324);
            this.stopStartCheck.Name = "stopStartCheck";
            this.stopStartCheck.Size = new System.Drawing.Size(211, 18);
            this.stopStartCheck.TabIndex = 58;
            this.stopStartCheck.Text = "Переключатель \"Стоп/Пуск\"";
            this.stopStartCheck.UseVisualStyleBackColor = true;
            this.stopStartCheck.CheckedChanged += new System.EventHandler(this.StopStartCheck_CheckedChanged);
            // 
            // sigFilAlarmCheck
            // 
            this.sigFilAlarmCheck.AutoSize = true;
            this.sigFilAlarmCheck.Enabled = false;
            this.sigFilAlarmCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sigFilAlarmCheck.Location = new System.Drawing.Point(350, 384);
            this.sigFilAlarmCheck.Name = "sigFilAlarmCheck";
            this.sigFilAlarmCheck.Size = new System.Drawing.Size(230, 18);
            this.sigFilAlarmCheck.TabIndex = 57;
            this.sigFilAlarmCheck.Text = "Сигнал \"Загрязнение фильтра\"";
            this.sigFilAlarmCheck.UseVisualStyleBackColor = true;
            this.sigFilAlarmCheck.CheckedChanged += new System.EventHandler(this.SigFilAlarmCheck_signalsDOCheckedChanged);
            // 
            // sigAlarmCheck
            // 
            this.sigAlarmCheck.AutoSize = true;
            this.sigAlarmCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sigAlarmCheck.Location = new System.Drawing.Point(350, 354);
            this.sigAlarmCheck.Name = "sigAlarmCheck";
            this.sigAlarmCheck.Size = new System.Drawing.Size(134, 18);
            this.sigAlarmCheck.TabIndex = 56;
            this.sigAlarmCheck.Text = "Сигнал \"Авария\"";
            this.sigAlarmCheck.UseVisualStyleBackColor = true;
            this.sigAlarmCheck.CheckedChanged += new System.EventHandler(this.SigAlarmCheck_signalsDOCheckedChanged);
            // 
            // sigWorkCheck
            // 
            this.sigWorkCheck.AutoSize = true;
            this.sigWorkCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sigWorkCheck.Location = new System.Drawing.Point(350, 324);
            this.sigWorkCheck.Name = "sigWorkCheck";
            this.sigWorkCheck.Size = new System.Drawing.Size(135, 18);
            this.sigWorkCheck.TabIndex = 55;
            this.sigWorkCheck.Text = "Сигнал \"Работа\"";
            this.sigWorkCheck.UseVisualStyleBackColor = true;
            this.sigWorkCheck.CheckedChanged += new System.EventHandler(this.SigWorkCheck_signalsDOCheckedChanged);
            // 
            // label136
            // 
            this.label136.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label136.AutoSize = true;
            this.label136.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label136.Location = new System.Drawing.Point(15, 289);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(178, 16);
            this.label136.TabIndex = 54;
            this.label136.Text = "ВНЕШНИЕ СИГНАЛЫ DI";
            // 
            // outdoorChanSensCheck
            // 
            this.outdoorChanSensCheck.AutoSize = true;
            this.outdoorChanSensCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outdoorChanSensCheck.Location = new System.Drawing.Point(15, 134);
            this.outdoorChanSensCheck.Name = "outdoorChanSensCheck";
            this.outdoorChanSensCheck.Size = new System.Drawing.Size(294, 18);
            this.outdoorChanSensCheck.TabIndex = 53;
            this.outdoorChanSensCheck.Text = "Датчик температуры наружного воздуха";
            this.outdoorChanSensCheck.UseVisualStyleBackColor = true;
            this.outdoorChanSensCheck.CheckedChanged += new System.EventHandler(this.OutdoorChanSensCheck_cmdCheckedChanged);
            // 
            // sensorPicture
            // 
            this.sensorPicture.Image = global::Moderon.Properties.Resources.sensorTemp;
            this.sensorPicture.Location = new System.Drawing.Point(584, 3);
            this.sensorPicture.Name = "sensorPicture";
            this.sensorPicture.Size = new System.Drawing.Size(130, 172);
            this.sensorPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sensorPicture.TabIndex = 52;
            this.sensorPicture.TabStop = false;
            // 
            // outChanSensCheck
            // 
            this.outChanSensCheck.AutoSize = true;
            this.outChanSensCheck.Enabled = false;
            this.outChanSensCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outChanSensCheck.Location = new System.Drawing.Point(15, 174);
            this.outChanSensCheck.Name = "outChanSensCheck";
            this.outChanSensCheck.Size = new System.Drawing.Size(306, 18);
            this.outChanSensCheck.TabIndex = 51;
            this.outChanSensCheck.Text = "Канальный вытяжной датчик температуры";
            this.outChanSensCheck.UseVisualStyleBackColor = true;
            this.outChanSensCheck.CheckedChanged += new System.EventHandler(this.OutChanSensCheck_cmdCheckedChanged);
            // 
            // roomHumSensCheck
            // 
            this.roomHumSensCheck.AutoSize = true;
            this.roomHumSensCheck.Enabled = false;
            this.roomHumSensCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.roomHumSensCheck.Location = new System.Drawing.Point(15, 254);
            this.roomHumSensCheck.Name = "roomHumSensCheck";
            this.roomHumSensCheck.Size = new System.Drawing.Size(222, 18);
            this.roomHumSensCheck.TabIndex = 29;
            this.roomHumSensCheck.Text = "Комнатный датчик влажности";
            this.roomHumSensCheck.UseVisualStyleBackColor = true;
            this.roomHumSensCheck.CheckedChanged += new System.EventHandler(this.RoomHumSensCheck_cmdCheckedChanged);
            // 
            // chanHumSensCheck
            // 
            this.chanHumSensCheck.AutoSize = true;
            this.chanHumSensCheck.Enabled = false;
            this.chanHumSensCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chanHumSensCheck.Location = new System.Drawing.Point(15, 214);
            this.chanHumSensCheck.Name = "chanHumSensCheck";
            this.chanHumSensCheck.Size = new System.Drawing.Size(221, 18);
            this.chanHumSensCheck.TabIndex = 28;
            this.chanHumSensCheck.Text = "Канальный датчик влажности";
            this.chanHumSensCheck.UseVisualStyleBackColor = true;
            this.chanHumSensCheck.CheckedChanged += new System.EventHandler(this.ChanHumSensCheck_cmdCheckedChanged);
            // 
            // roomTempSensCheck
            // 
            this.roomTempSensCheck.AutoSize = true;
            this.roomTempSensCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.roomTempSensCheck.Location = new System.Drawing.Point(15, 94);
            this.roomTempSensCheck.Name = "roomTempSensCheck";
            this.roomTempSensCheck.Size = new System.Drawing.Size(254, 18);
            this.roomTempSensCheck.TabIndex = 27;
            this.roomTempSensCheck.Text = "Комнатный температурный датчик";
            this.roomTempSensCheck.UseVisualStyleBackColor = true;
            this.roomTempSensCheck.CheckedChanged += new System.EventHandler(this.RoomTempSensCheck_cmdCheckedChanged);
            // 
            // prChanSensCheck
            // 
            this.prChanSensCheck.AutoSize = true;
            this.prChanSensCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prChanSensCheck.Location = new System.Drawing.Point(15, 54);
            this.prChanSensCheck.Name = "prChanSensCheck";
            this.prChanSensCheck.Size = new System.Drawing.Size(313, 18);
            this.prChanSensCheck.TabIndex = 26;
            this.prChanSensCheck.Text = "Канальный приточный датчик температуры";
            this.prChanSensCheck.UseVisualStyleBackColor = true;
            this.prChanSensCheck.CheckedChanged += new System.EventHandler(this.PrChanSensCheck_cmdCheckedChanged);
            // 
            // label42
            // 
            this.label42.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label42.Location = new System.Drawing.Point(15, 9);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(300, 16);
            this.label42.TabIndex = 15;
            this.label42.Text = "ДАТЧИКИ ТЕМПЕРАТУРЫ И ВЛАЖНОСТИ";
            // 
            // fanPage
            // 
            this.fanPage.AutoScroll = true;
            this.fanPage.BackColor = System.Drawing.Color.Transparent;
            this.fanPage.Controls.Add(this.outFanPanel);
            this.fanPage.Controls.Add(this.prFanPanel);
            this.fanPage.Location = new System.Drawing.Point(4, 22);
            this.fanPage.Name = "fanPage";
            this.fanPage.Padding = new System.Windows.Forms.Padding(3);
            this.fanPage.Size = new System.Drawing.Size(742, 14);
            this.fanPage.TabIndex = 0;
            this.fanPage.Text = "ВЕНТИЛЯТОР";
            // 
            // outFanPanel
            // 
            this.outFanPanel.Controls.Add(this.outFanFcTypeCombo);
            this.outFanPanel.Controls.Add(this.label63);
            this.outFanPanel.Controls.Add(this.outDampConfirmFanCheck);
            this.outFanPanel.Controls.Add(this.outDampFanCheck);
            this.outFanPanel.Controls.Add(this.outFanSpeedCheck);
            this.outFanPanel.Controls.Add(this.outFanStStopCheck);
            this.outFanPanel.Controls.Add(this.outFanAlarmCheck);
            this.outFanPanel.Controls.Add(this.fanPicture2);
            this.outFanPanel.Controls.Add(this.curDefOutFanCheck);
            this.outFanPanel.Controls.Add(this.outFanThermoCheck);
            this.outFanPanel.Controls.Add(this.labelResOutFan_2);
            this.outFanPanel.Controls.Add(this.powOutResFanBox);
            this.outFanPanel.Controls.Add(this.labelResOutFan);
            this.outFanPanel.Controls.Add(this.checkResOutFan);
            this.outFanPanel.Controls.Add(this.label32);
            this.outFanPanel.Controls.Add(this.powOutFanBox);
            this.outFanPanel.Controls.Add(this.label33);
            this.outFanPanel.Controls.Add(this.outFanControlCombo);
            this.outFanPanel.Controls.Add(this.label11);
            this.outFanPanel.Controls.Add(this.outFanFC_check);
            this.outFanPanel.Controls.Add(this.outFanPSCheck);
            this.outFanPanel.Controls.Add(this.outFanPowCombo);
            this.outFanPanel.Controls.Add(this.label12);
            this.outFanPanel.Controls.Add(this.label4);
            this.outFanPanel.Location = new System.Drawing.Point(2, 495);
            this.outFanPanel.Name = "outFanPanel";
            this.outFanPanel.Size = new System.Drawing.Size(717, 486);
            this.outFanPanel.TabIndex = 15;
            this.outFanPanel.Visible = false;
            // 
            // outFanFcTypeCombo
            // 
            this.outFanFcTypeCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.outFanFcTypeCombo.DisplayMember = "Внешние контакты";
            this.outFanFcTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outFanFcTypeCombo.Enabled = false;
            this.outFanFcTypeCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outFanFcTypeCombo.ForeColor = System.Drawing.Color.White;
            this.outFanFcTypeCombo.FormattingEnabled = true;
            this.outFanFcTypeCombo.Items.AddRange(new object[] {
            "Veda VF-51 / VF-101",
            "Systeme Electric STV600"});
            this.outFanFcTypeCombo.Location = new System.Drawing.Point(109, 330);
            this.outFanFcTypeCombo.Name = "outFanFcTypeCombo";
            this.outFanFcTypeCombo.Size = new System.Drawing.Size(212, 21);
            this.outFanFcTypeCombo.TabIndex = 54;
            this.outFanFcTypeCombo.SelectedIndexChanged += new System.EventHandler(this.OutFanFcTypeCombo_cmdSelectedIndexChanged);
            // 
            // label63
            // 
            this.label63.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label63.Location = new System.Drawing.Point(15, 334);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(80, 16);
            this.label63.TabIndex = 53;
            this.label63.Text = "Модель ПЧ";
            // 
            // outDampConfirmFanCheck
            // 
            this.outDampConfirmFanCheck.AutoSize = true;
            this.outDampConfirmFanCheck.Enabled = false;
            this.outDampConfirmFanCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outDampConfirmFanCheck.Location = new System.Drawing.Point(350, 374);
            this.outDampConfirmFanCheck.Name = "outDampConfirmFanCheck";
            this.outDampConfirmFanCheck.Size = new System.Drawing.Size(259, 18);
            this.outDampConfirmFanCheck.TabIndex = 52;
            this.outDampConfirmFanCheck.Text = "Подтверждение открытия заслонки";
            this.outDampConfirmFanCheck.UseVisualStyleBackColor = true;
            this.outDampConfirmFanCheck.CheckedChanged += new System.EventHandler(this.OutDampConfirmFanCheck_CheckedChanged);
            // 
            // outDampFanCheck
            // 
            this.outDampFanCheck.AutoSize = true;
            this.outDampFanCheck.Enabled = false;
            this.outDampFanCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outDampFanCheck.Location = new System.Drawing.Point(350, 334);
            this.outDampFanCheck.Name = "outDampFanCheck";
            this.outDampFanCheck.Size = new System.Drawing.Size(246, 18);
            this.outDampFanCheck.TabIndex = 51;
            this.outDampFanCheck.Text = "Воздушная заслонка вентилятора";
            this.outDampFanCheck.UseVisualStyleBackColor = true;
            this.outDampFanCheck.CheckedChanged += new System.EventHandler(this.OutDampFanCheck_CheckedChanged);
            // 
            // outFanSpeedCheck
            // 
            this.outFanSpeedCheck.AutoSize = true;
            this.outFanSpeedCheck.Enabled = false;
            this.outFanSpeedCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outFanSpeedCheck.Location = new System.Drawing.Point(15, 454);
            this.outFanSpeedCheck.Name = "outFanSpeedCheck";
            this.outFanSpeedCheck.Size = new System.Drawing.Size(131, 18);
            this.outFanSpeedCheck.TabIndex = 50;
            this.outFanSpeedCheck.Text = "Скорость 0-10 В";
            this.outFanSpeedCheck.UseVisualStyleBackColor = true;
            this.outFanSpeedCheck.CheckedChanged += new System.EventHandler(this.OutFanSpeedCheck_cmdCheckedChanged);
            // 
            // outFanStStopCheck
            // 
            this.outFanStStopCheck.AutoSize = true;
            this.outFanStStopCheck.Checked = true;
            this.outFanStStopCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.outFanStStopCheck.Enabled = false;
            this.outFanStStopCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outFanStStopCheck.Location = new System.Drawing.Point(15, 374);
            this.outFanStStopCheck.Name = "outFanStStopCheck";
            this.outFanStStopCheck.Size = new System.Drawing.Size(154, 18);
            this.outFanStStopCheck.TabIndex = 49;
            this.outFanStStopCheck.Text = "Сигнал \"Пуск/Стоп\"";
            this.outFanStStopCheck.UseVisualStyleBackColor = true;
            this.outFanStStopCheck.CheckedChanged += new System.EventHandler(this.OutFanStStopCheck_CheckedChanged);
            // 
            // outFanAlarmCheck
            // 
            this.outFanAlarmCheck.AutoSize = true;
            this.outFanAlarmCheck.Enabled = false;
            this.outFanAlarmCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outFanAlarmCheck.Location = new System.Drawing.Point(15, 414);
            this.outFanAlarmCheck.Name = "outFanAlarmCheck";
            this.outFanAlarmCheck.Size = new System.Drawing.Size(149, 18);
            this.outFanAlarmCheck.TabIndex = 48;
            this.outFanAlarmCheck.Text = "Выход аварии с ПЧ";
            this.outFanAlarmCheck.UseVisualStyleBackColor = true;
            this.outFanAlarmCheck.CheckedChanged += new System.EventHandler(this.OutFanAlarmCheck_cmdCheckedChanged);
            // 
            // fanPicture2
            // 
            this.fanPicture2.Image = global::Moderon.Properties.Resources.fan_2;
            this.fanPicture2.Location = new System.Drawing.Point(558, 3);
            this.fanPicture2.Name = "fanPicture2";
            this.fanPicture2.Size = new System.Drawing.Size(156, 242);
            this.fanPicture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.fanPicture2.TabIndex = 0;
            this.fanPicture2.TabStop = false;
            // 
            // curDefOutFanCheck
            // 
            this.curDefOutFanCheck.AutoSize = true;
            this.curDefOutFanCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.curDefOutFanCheck.Location = new System.Drawing.Point(15, 214);
            this.curDefOutFanCheck.Name = "curDefOutFanCheck";
            this.curDefOutFanCheck.Size = new System.Drawing.Size(129, 18);
            this.curDefOutFanCheck.TabIndex = 47;
            this.curDefOutFanCheck.Text = "Защита по току";
            this.curDefOutFanCheck.UseVisualStyleBackColor = true;
            this.curDefOutFanCheck.CheckedChanged += new System.EventHandler(this.CurDefOutFanCheck_cmdCheckedChanged);
            // 
            // outFanThermoCheck
            // 
            this.outFanThermoCheck.AutoSize = true;
            this.outFanThermoCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outFanThermoCheck.Location = new System.Drawing.Point(15, 174);
            this.outFanThermoCheck.Name = "outFanThermoCheck";
            this.outFanThermoCheck.Size = new System.Drawing.Size(196, 18);
            this.outFanThermoCheck.TabIndex = 46;
            this.outFanThermoCheck.Text = "Термоконтакты двигателя";
            this.outFanThermoCheck.UseVisualStyleBackColor = true;
            this.outFanThermoCheck.CheckedChanged += new System.EventHandler(this.OutFanThermoCheck_cmdCheckedChanged);
            // 
            // labelResOutFan_2
            // 
            this.labelResOutFan_2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelResOutFan_2.AutoSize = true;
            this.labelResOutFan_2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelResOutFan_2.Location = new System.Drawing.Point(650, 414);
            this.labelResOutFan_2.Name = "labelResOutFan_2";
            this.labelResOutFan_2.Size = new System.Drawing.Size(30, 16);
            this.labelResOutFan_2.TabIndex = 45;
            this.labelResOutFan_2.Text = "кВт";
            this.labelResOutFan_2.Visible = false;
            // 
            // powOutResFanBox
            // 
            this.powOutResFanBox.Location = new System.Drawing.Point(590, 412);
            this.powOutResFanBox.MaxLength = 4;
            this.powOutResFanBox.Name = "powOutResFanBox";
            this.powOutResFanBox.Size = new System.Drawing.Size(54, 21);
            this.powOutResFanBox.TabIndex = 44;
            this.powOutResFanBox.Text = "1,5";
            this.powOutResFanBox.Visible = false;
            this.powOutResFanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PowOutResFanBox_KeyPress);
            // 
            // labelResOutFan
            // 
            this.labelResOutFan.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelResOutFan.AutoSize = true;
            this.labelResOutFan.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelResOutFan.Location = new System.Drawing.Point(350, 414);
            this.labelResOutFan.Name = "labelResOutFan";
            this.labelResOutFan.Size = new System.Drawing.Size(232, 16);
            this.labelResOutFan.TabIndex = 43;
            this.labelResOutFan.Text = "Мощность резервного двигателя";
            this.labelResOutFan.Visible = false;
            // 
            // checkResOutFan
            // 
            this.checkResOutFan.AutoSize = true;
            this.checkResOutFan.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkResOutFan.Location = new System.Drawing.Point(350, 294);
            this.checkResOutFan.Name = "checkResOutFan";
            this.checkResOutFan.Size = new System.Drawing.Size(166, 18);
            this.checkResOutFan.TabIndex = 42;
            this.checkResOutFan.Text = "Резервный двигатель";
            this.checkResOutFan.UseVisualStyleBackColor = true;
            this.checkResOutFan.CheckedChanged += new System.EventHandler(this.CheckResOutFan_CheckedChanged);
            // 
            // label32
            // 
            this.label32.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label32.Location = new System.Drawing.Point(249, 94);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(30, 16);
            this.label32.TabIndex = 41;
            this.label32.Text = "кВт";
            // 
            // powOutFanBox
            // 
            this.powOutFanBox.Location = new System.Drawing.Point(189, 92);
            this.powOutFanBox.MaxLength = 4;
            this.powOutFanBox.Name = "powOutFanBox";
            this.powOutFanBox.Size = new System.Drawing.Size(54, 21);
            this.powOutFanBox.TabIndex = 40;
            this.powOutFanBox.Text = "1,5";
            this.powOutFanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PowOutFanBox_KeyPress);
            // 
            // label33
            // 
            this.label33.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label33.Location = new System.Drawing.Point(15, 94);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(167, 16);
            this.label33.TabIndex = 39;
            this.label33.Text = "Мощность вентилятора";
            // 
            // outFanControlCombo
            // 
            this.outFanControlCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.outFanControlCombo.DisplayMember = "Внешние контакты";
            this.outFanControlCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outFanControlCombo.Enabled = false;
            this.outFanControlCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outFanControlCombo.ForeColor = System.Drawing.Color.White;
            this.outFanControlCombo.FormattingEnabled = true;
            this.outFanControlCombo.Items.AddRange(new object[] {
            "Внешние контакты",
            "Modbus"});
            this.outFanControlCombo.Location = new System.Drawing.Point(161, 290);
            this.outFanControlCombo.Name = "outFanControlCombo";
            this.outFanControlCombo.Size = new System.Drawing.Size(160, 21);
            this.outFanControlCombo.TabIndex = 26;
            this.outFanControlCombo.SelectedIndexChanged += new System.EventHandler(this.OutFanControlCombo_signalsAOSelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(15, 294);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(139, 16);
            this.label11.TabIndex = 25;
            this.label11.Text = "Вид управления ПЧ";
            // 
            // outFanFC_check
            // 
            this.outFanFC_check.AutoSize = true;
            this.outFanFC_check.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outFanFC_check.Location = new System.Drawing.Point(15, 254);
            this.outFanFC_check.Name = "outFanFC_check";
            this.outFanFC_check.Size = new System.Drawing.Size(198, 18);
            this.outFanFC_check.TabIndex = 24;
            this.outFanFC_check.Text = "Преобразователь частоты";
            this.outFanFC_check.UseVisualStyleBackColor = true;
            this.outFanFC_check.CheckedChanged += new System.EventHandler(this.OutFanFC_check_CheckedChanged);
            // 
            // outFanPSCheck
            // 
            this.outFanPSCheck.AutoSize = true;
            this.outFanPSCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outFanPSCheck.Location = new System.Drawing.Point(15, 134);
            this.outFanPSCheck.Name = "outFanPSCheck";
            this.outFanPSCheck.Size = new System.Drawing.Size(271, 18);
            this.outFanPSCheck.TabIndex = 23;
            this.outFanPSCheck.Text = "Подтверждение работы вентилятора";
            this.outFanPSCheck.UseVisualStyleBackColor = true;
            this.outFanPSCheck.CheckedChanged += new System.EventHandler(this.OutFanPSCheck_cmdCheckedChanged);
            // 
            // outFanPowCombo
            // 
            this.outFanPowCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.outFanPowCombo.DisplayMember = "380 В";
            this.outFanPowCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outFanPowCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outFanPowCombo.ForeColor = System.Drawing.Color.White;
            this.outFanPowCombo.FormattingEnabled = true;
            this.outFanPowCombo.Items.AddRange(new object[] {
            "380 В",
            "230 В"});
            this.outFanPowCombo.Location = new System.Drawing.Point(176, 50);
            this.outFanPowCombo.Name = "outFanPowCombo";
            this.outFanPowCombo.Size = new System.Drawing.Size(59, 21);
            this.outFanPowCombo.TabIndex = 22;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(15, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(154, 16);
            this.label12.TabIndex = 21;
            this.label12.Text = "Питание вентилятора";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(15, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "ВЫТЯЖНОЙ ВЕНТИЛЯТОР";
            // 
            // prFanPanel
            // 
            this.prFanPanel.Controls.Add(this.prFanFcTypeCombo);
            this.prFanPanel.Controls.Add(this.label62);
            this.prFanPanel.Controls.Add(this.prDampConfirmFanCheck);
            this.prFanPanel.Controls.Add(this.prDampFanCheck);
            this.prFanPanel.Controls.Add(this.prFanStStopCheck);
            this.prFanPanel.Controls.Add(this.prFanAlarmCheck);
            this.prFanPanel.Controls.Add(this.fanPicture1);
            this.prFanPanel.Controls.Add(this.curDefPrFanCheck);
            this.prFanPanel.Controls.Add(this.prFanThermoCheck);
            this.prFanPanel.Controls.Add(this.labelResPrFan_2);
            this.prFanPanel.Controls.Add(this.powPrResFanBox);
            this.prFanPanel.Controls.Add(this.labelResPrFan);
            this.prFanPanel.Controls.Add(this.checkResPrFan);
            this.prFanPanel.Controls.Add(this.label30);
            this.prFanPanel.Controls.Add(this.powPrFanBox);
            this.prFanPanel.Controls.Add(this.label31);
            this.prFanPanel.Controls.Add(this.prFanControlCombo);
            this.prFanPanel.Controls.Add(this.label10);
            this.prFanPanel.Controls.Add(this.prFanFC_check);
            this.prFanPanel.Controls.Add(this.prFanPSCheck);
            this.prFanPanel.Controls.Add(this.prFanPowCombo);
            this.prFanPanel.Controls.Add(this.label9);
            this.prFanPanel.Controls.Add(this.label3);
            this.prFanPanel.Controls.Add(this.prFanSpeedCheck);
            this.prFanPanel.Location = new System.Drawing.Point(3, 6);
            this.prFanPanel.Name = "prFanPanel";
            this.prFanPanel.Size = new System.Drawing.Size(717, 486);
            this.prFanPanel.TabIndex = 1;
            // 
            // prFanFcTypeCombo
            // 
            this.prFanFcTypeCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.prFanFcTypeCombo.DisplayMember = "Внешние контакты";
            this.prFanFcTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.prFanFcTypeCombo.Enabled = false;
            this.prFanFcTypeCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prFanFcTypeCombo.ForeColor = System.Drawing.Color.White;
            this.prFanFcTypeCombo.FormattingEnabled = true;
            this.prFanFcTypeCombo.Items.AddRange(new object[] {
            "Veda VF-51 / VF-101",
            "Systeme Electric STV600"});
            this.prFanFcTypeCombo.Location = new System.Drawing.Point(109, 330);
            this.prFanFcTypeCombo.Name = "prFanFcTypeCombo";
            this.prFanFcTypeCombo.Size = new System.Drawing.Size(212, 21);
            this.prFanFcTypeCombo.TabIndex = 52;
            this.prFanFcTypeCombo.SelectedIndexChanged += new System.EventHandler(this.PrFanFcTypeCombo_cmdSelectedIndexChanged);
            // 
            // label62
            // 
            this.label62.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label62.Location = new System.Drawing.Point(15, 334);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(80, 16);
            this.label62.TabIndex = 51;
            this.label62.Text = "Модель ПЧ";
            // 
            // prDampConfirmFanCheck
            // 
            this.prDampConfirmFanCheck.AutoSize = true;
            this.prDampConfirmFanCheck.Enabled = false;
            this.prDampConfirmFanCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prDampConfirmFanCheck.Location = new System.Drawing.Point(350, 374);
            this.prDampConfirmFanCheck.Name = "prDampConfirmFanCheck";
            this.prDampConfirmFanCheck.Size = new System.Drawing.Size(259, 18);
            this.prDampConfirmFanCheck.TabIndex = 50;
            this.prDampConfirmFanCheck.Text = "Подтверждение открытия заслонки";
            this.prDampConfirmFanCheck.UseVisualStyleBackColor = true;
            this.prDampConfirmFanCheck.CheckedChanged += new System.EventHandler(this.PrDampConfirmFanCheck_CheckedChanged);
            // 
            // prDampFanCheck
            // 
            this.prDampFanCheck.AutoSize = true;
            this.prDampFanCheck.Enabled = false;
            this.prDampFanCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prDampFanCheck.Location = new System.Drawing.Point(350, 334);
            this.prDampFanCheck.Name = "prDampFanCheck";
            this.prDampFanCheck.Size = new System.Drawing.Size(246, 18);
            this.prDampFanCheck.TabIndex = 49;
            this.prDampFanCheck.Text = "Воздушная заслонка вентилятора";
            this.prDampFanCheck.UseVisualStyleBackColor = true;
            this.prDampFanCheck.CheckedChanged += new System.EventHandler(this.PrDampFanCheck_CheckedChanged);
            // 
            // prFanStStopCheck
            // 
            this.prFanStStopCheck.AutoSize = true;
            this.prFanStStopCheck.Checked = true;
            this.prFanStStopCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.prFanStStopCheck.Enabled = false;
            this.prFanStStopCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prFanStStopCheck.Location = new System.Drawing.Point(15, 374);
            this.prFanStStopCheck.Name = "prFanStStopCheck";
            this.prFanStStopCheck.Size = new System.Drawing.Size(154, 18);
            this.prFanStStopCheck.TabIndex = 47;
            this.prFanStStopCheck.Text = "Сигнал \"Пуск/Стоп\"";
            this.prFanStStopCheck.UseVisualStyleBackColor = true;
            this.prFanStStopCheck.CheckedChanged += new System.EventHandler(this.PrFanStStopCheck_CheckedChanged);
            // 
            // prFanAlarmCheck
            // 
            this.prFanAlarmCheck.AutoSize = true;
            this.prFanAlarmCheck.Enabled = false;
            this.prFanAlarmCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prFanAlarmCheck.Location = new System.Drawing.Point(15, 414);
            this.prFanAlarmCheck.Name = "prFanAlarmCheck";
            this.prFanAlarmCheck.Size = new System.Drawing.Size(149, 18);
            this.prFanAlarmCheck.TabIndex = 46;
            this.prFanAlarmCheck.Text = "Выход аварии с ПЧ";
            this.prFanAlarmCheck.UseVisualStyleBackColor = true;
            this.prFanAlarmCheck.CheckedChanged += new System.EventHandler(this.PrFanAlarmCheck_cmdCheckedChanged);
            // 
            // fanPicture1
            // 
            this.fanPicture1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fanPicture1.Image = global::Moderon.Properties.Resources.fan;
            this.fanPicture1.Location = new System.Drawing.Point(537, 3);
            this.fanPicture1.Name = "fanPicture1";
            this.fanPicture1.Size = new System.Drawing.Size(177, 200);
            this.fanPicture1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.fanPicture1.TabIndex = 0;
            this.fanPicture1.TabStop = false;
            // 
            // curDefPrFanCheck
            // 
            this.curDefPrFanCheck.AutoSize = true;
            this.curDefPrFanCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.curDefPrFanCheck.Location = new System.Drawing.Point(15, 214);
            this.curDefPrFanCheck.Name = "curDefPrFanCheck";
            this.curDefPrFanCheck.Size = new System.Drawing.Size(129, 18);
            this.curDefPrFanCheck.TabIndex = 44;
            this.curDefPrFanCheck.Text = "Защита по току";
            this.curDefPrFanCheck.UseVisualStyleBackColor = true;
            this.curDefPrFanCheck.CheckedChanged += new System.EventHandler(this.CurDefPrFanCheck_cmdCheckedChanged);
            // 
            // prFanThermoCheck
            // 
            this.prFanThermoCheck.AutoSize = true;
            this.prFanThermoCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prFanThermoCheck.Location = new System.Drawing.Point(15, 174);
            this.prFanThermoCheck.Name = "prFanThermoCheck";
            this.prFanThermoCheck.Size = new System.Drawing.Size(196, 18);
            this.prFanThermoCheck.TabIndex = 43;
            this.prFanThermoCheck.Text = "Термоконтакты двигателя";
            this.prFanThermoCheck.UseVisualStyleBackColor = true;
            this.prFanThermoCheck.CheckedChanged += new System.EventHandler(this.PrFanThermoCheck_cmdCheckedChanged);
            // 
            // labelResPrFan_2
            // 
            this.labelResPrFan_2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelResPrFan_2.AutoSize = true;
            this.labelResPrFan_2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelResPrFan_2.Location = new System.Drawing.Point(650, 414);
            this.labelResPrFan_2.Name = "labelResPrFan_2";
            this.labelResPrFan_2.Size = new System.Drawing.Size(30, 16);
            this.labelResPrFan_2.TabIndex = 42;
            this.labelResPrFan_2.Text = "кВт";
            this.labelResPrFan_2.Visible = false;
            // 
            // powPrResFanBox
            // 
            this.powPrResFanBox.Location = new System.Drawing.Point(590, 414);
            this.powPrResFanBox.MaxLength = 4;
            this.powPrResFanBox.Name = "powPrResFanBox";
            this.powPrResFanBox.Size = new System.Drawing.Size(54, 21);
            this.powPrResFanBox.TabIndex = 41;
            this.powPrResFanBox.Text = "1,5";
            this.powPrResFanBox.Visible = false;
            this.powPrResFanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PowPrResFanBox_KeyPress);
            // 
            // labelResPrFan
            // 
            this.labelResPrFan.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelResPrFan.AutoSize = true;
            this.labelResPrFan.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelResPrFan.Location = new System.Drawing.Point(350, 414);
            this.labelResPrFan.Name = "labelResPrFan";
            this.labelResPrFan.Size = new System.Drawing.Size(232, 16);
            this.labelResPrFan.TabIndex = 40;
            this.labelResPrFan.Text = "Мощность резервного двигателя";
            this.labelResPrFan.Visible = false;
            // 
            // checkResPrFan
            // 
            this.checkResPrFan.AutoSize = true;
            this.checkResPrFan.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkResPrFan.Location = new System.Drawing.Point(350, 294);
            this.checkResPrFan.Name = "checkResPrFan";
            this.checkResPrFan.Size = new System.Drawing.Size(166, 18);
            this.checkResPrFan.TabIndex = 39;
            this.checkResPrFan.Text = "Резервный двигатель";
            this.checkResPrFan.UseVisualStyleBackColor = true;
            this.checkResPrFan.CheckedChanged += new System.EventHandler(this.CheckResPrFan_CheckedChanged);
            // 
            // label30
            // 
            this.label30.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label30.Location = new System.Drawing.Point(249, 94);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(30, 16);
            this.label30.TabIndex = 38;
            this.label30.Text = "кВт";
            // 
            // powPrFanBox
            // 
            this.powPrFanBox.Location = new System.Drawing.Point(189, 94);
            this.powPrFanBox.MaxLength = 4;
            this.powPrFanBox.Name = "powPrFanBox";
            this.powPrFanBox.Size = new System.Drawing.Size(54, 21);
            this.powPrFanBox.TabIndex = 37;
            this.powPrFanBox.Text = "1,5";
            this.powPrFanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PowPrFanBox_KeyPress);
            // 
            // label31
            // 
            this.label31.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label31.Location = new System.Drawing.Point(15, 94);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(167, 16);
            this.label31.TabIndex = 36;
            this.label31.Text = "Мощность вентилятора";
            // 
            // prFanControlCombo
            // 
            this.prFanControlCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.prFanControlCombo.DisplayMember = "Внешние контакты";
            this.prFanControlCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.prFanControlCombo.Enabled = false;
            this.prFanControlCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prFanControlCombo.ForeColor = System.Drawing.Color.White;
            this.prFanControlCombo.FormattingEnabled = true;
            this.prFanControlCombo.Items.AddRange(new object[] {
            "Внешние контакты",
            "Modbus"});
            this.prFanControlCombo.Location = new System.Drawing.Point(161, 290);
            this.prFanControlCombo.Name = "prFanControlCombo";
            this.prFanControlCombo.Size = new System.Drawing.Size(160, 21);
            this.prFanControlCombo.TabIndex = 20;
            this.prFanControlCombo.SelectedIndexChanged += new System.EventHandler(this.PrFanControlCombo_signalsAOSelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(15, 294);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(139, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "Вид управления ПЧ";
            // 
            // prFanFC_check
            // 
            this.prFanFC_check.AutoSize = true;
            this.prFanFC_check.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prFanFC_check.Location = new System.Drawing.Point(15, 254);
            this.prFanFC_check.Name = "prFanFC_check";
            this.prFanFC_check.Size = new System.Drawing.Size(198, 18);
            this.prFanFC_check.TabIndex = 18;
            this.prFanFC_check.Text = "Преобразователь частоты";
            this.prFanFC_check.UseVisualStyleBackColor = true;
            this.prFanFC_check.CheckedChanged += new System.EventHandler(this.PrFanFC_check_CheckedChanged);
            // 
            // prFanPSCheck
            // 
            this.prFanPSCheck.AutoSize = true;
            this.prFanPSCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prFanPSCheck.Location = new System.Drawing.Point(15, 134);
            this.prFanPSCheck.Name = "prFanPSCheck";
            this.prFanPSCheck.Size = new System.Drawing.Size(271, 18);
            this.prFanPSCheck.TabIndex = 17;
            this.prFanPSCheck.Text = "Подтверждение работы вентилятора";
            this.prFanPSCheck.UseVisualStyleBackColor = true;
            this.prFanPSCheck.CheckedChanged += new System.EventHandler(this.PrFanPSCheck_cmdCheckedChanged);
            // 
            // prFanPowCombo
            // 
            this.prFanPowCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.prFanPowCombo.DisplayMember = "380 В";
            this.prFanPowCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.prFanPowCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prFanPowCombo.ForeColor = System.Drawing.Color.White;
            this.prFanPowCombo.FormattingEnabled = true;
            this.prFanPowCombo.Items.AddRange(new object[] {
            "380 В",
            "230 В"});
            this.prFanPowCombo.Location = new System.Drawing.Point(176, 50);
            this.prFanPowCombo.Name = "prFanPowCombo";
            this.prFanPowCombo.Size = new System.Drawing.Size(59, 21);
            this.prFanPowCombo.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(15, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(154, 16);
            this.label9.TabIndex = 15;
            this.label9.Text = "Питание вентилятора";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(15, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "ПРИТОЧНЫЙ ВЕНТИЛЯТОР";
            // 
            // prFanSpeedCheck
            // 
            this.prFanSpeedCheck.AutoSize = true;
            this.prFanSpeedCheck.Enabled = false;
            this.prFanSpeedCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prFanSpeedCheck.Location = new System.Drawing.Point(15, 454);
            this.prFanSpeedCheck.Name = "prFanSpeedCheck";
            this.prFanSpeedCheck.Size = new System.Drawing.Size(131, 18);
            this.prFanSpeedCheck.TabIndex = 48;
            this.prFanSpeedCheck.Text = "Скорость 0-10 В";
            this.prFanSpeedCheck.UseVisualStyleBackColor = true;
            this.prFanSpeedCheck.CheckedChanged += new System.EventHandler(this.PrFanSpeedCheck_cmdCheckedChanged);
            // 
            // filterPage
            // 
            this.filterPage.Controls.Add(this.filterPanel);
            this.filterPage.Location = new System.Drawing.Point(4, 22);
            this.filterPage.Name = "filterPage";
            this.filterPage.Size = new System.Drawing.Size(742, 14);
            this.filterPage.TabIndex = 6;
            this.filterPage.Text = "ФИЛЬТР";
            // 
            // filterPanel
            // 
            this.filterPanel.Controls.Add(this.filterPicture);
            this.filterPanel.Controls.Add(this.outFilterPanel);
            this.filterPanel.Controls.Add(this.filterPrCombo);
            this.filterPanel.Controls.Add(this.label6);
            this.filterPanel.Controls.Add(this.label5);
            this.filterPanel.Location = new System.Drawing.Point(3, 5);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(717, 342);
            this.filterPanel.TabIndex = 2;
            // 
            // filterPicture
            // 
            this.filterPicture.Image = global::Moderon.Properties.Resources.filter;
            this.filterPicture.Location = new System.Drawing.Point(586, 3);
            this.filterPicture.Name = "filterPicture";
            this.filterPicture.Size = new System.Drawing.Size(128, 218);
            this.filterPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.filterPicture.TabIndex = 0;
            this.filterPicture.TabStop = false;
            // 
            // outFilterPanel
            // 
            this.outFilterPanel.Controls.Add(this.filterOutCombo);
            this.outFilterPanel.Controls.Add(this.label7);
            this.outFilterPanel.Controls.Add(this.label8);
            this.outFilterPanel.Location = new System.Drawing.Point(3, 100);
            this.outFilterPanel.Name = "outFilterPanel";
            this.outFilterPanel.Size = new System.Drawing.Size(323, 109);
            this.outFilterPanel.TabIndex = 18;
            this.outFilterPanel.Visible = false;
            // 
            // filterOutCombo
            // 
            this.filterOutCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.filterOutCombo.DisplayMember = "0";
            this.filterOutCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterOutCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.filterOutCombo.ForeColor = System.Drawing.Color.White;
            this.filterOutCombo.FormattingEnabled = true;
            this.filterOutCombo.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.filterOutCombo.Location = new System.Drawing.Point(254, 49);
            this.filterOutCombo.Name = "filterOutCombo";
            this.filterOutCombo.Size = new System.Drawing.Size(43, 21);
            this.filterOutCombo.TabIndex = 19;
            this.filterOutCombo.SelectedIndexChanged += new System.EventHandler(this.FilterOutCombo_cmdSelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(12, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(260, 16);
            this.label7.TabIndex = 19;
            this.label7.Text = "ВЫТЯЖНОЙ ВОЗДУШНЫЙ ФИЛЬТР";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(11, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(228, 16);
            this.label8.TabIndex = 20;
            this.label8.Text = "Количество вытяжных фильтров";
            // 
            // filterPrCombo
            // 
            this.filterPrCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.filterPrCombo.DisplayMember = "1";
            this.filterPrCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterPrCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.filterPrCombo.ForeColor = System.Drawing.Color.White;
            this.filterPrCombo.FormattingEnabled = true;
            this.filterPrCombo.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.filterPrCombo.Location = new System.Drawing.Point(254, 49);
            this.filterPrCombo.Name = "filterPrCombo";
            this.filterPrCombo.Size = new System.Drawing.Size(43, 21);
            this.filterPrCombo.TabIndex = 12;
            this.filterPrCombo.SelectedIndexChanged += new System.EventHandler(this.FilterPrCombo_cmdSelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(15, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(232, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Количество приточных фильтров";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(15, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(267, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "ПРИТОЧНЫЙ ВОЗДУШНЫЙ ФИЛЬТР";
            // 
            // dampPage
            // 
            this.dampPage.Controls.Add(this.dampPanel);
            this.dampPage.Location = new System.Drawing.Point(4, 22);
            this.dampPage.Name = "dampPage";
            this.dampPage.Size = new System.Drawing.Size(742, 14);
            this.dampPage.TabIndex = 7;
            this.dampPage.Text = "ЗАСЛОНКА";
            // 
            // dampPanel
            // 
            this.dampPanel.Controls.Add(this.markPrDampPanel);
            this.dampPanel.Controls.Add(this.prDampTorqLabel);
            this.dampPanel.Controls.Add(this.springRetPrDampCheck);
            this.dampPanel.Controls.Add(this.prDampSLabel);
            this.dampPanel.Controls.Add(this.label168);
            this.dampPanel.Controls.Add(this.label167);
            this.dampPanel.Controls.Add(this.h_prDampBox);
            this.dampPanel.Controls.Add(this.b_prDampBox);
            this.dampPanel.Controls.Add(this.label166);
            this.dampPanel.Controls.Add(this.label158);
            this.dampPanel.Controls.Add(this.dampPicture);
            this.dampPanel.Controls.Add(this.outDampPanel);
            this.dampPanel.Controls.Add(this.heatPrDampCheck);
            this.dampPanel.Controls.Add(this.confPrDampCheck);
            this.dampPanel.Controls.Add(this.prDampPowCombo);
            this.dampPanel.Controls.Add(this.label14);
            this.dampPanel.Controls.Add(this.label13);
            this.dampPanel.Location = new System.Drawing.Point(3, 3);
            this.dampPanel.Name = "dampPanel";
            this.dampPanel.Size = new System.Drawing.Size(717, 441);
            this.dampPanel.TabIndex = 3;
            // 
            // markPrDampPanel
            // 
            this.markPrDampPanel.BackgroundImage = global::Moderon.Properties.Resources.green_check;
            this.markPrDampPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.markPrDampPanel.Location = new System.Drawing.Point(497, 166);
            this.markPrDampPanel.Name = "markPrDampPanel";
            this.markPrDampPanel.Size = new System.Drawing.Size(30, 30);
            this.markPrDampPanel.TabIndex = 36;
            this.markPrDampPanel.Visible = false;
            // 
            // prDampTorqLabel
            // 
            this.prDampTorqLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.prDampTorqLabel.AutoSize = true;
            this.prDampTorqLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prDampTorqLabel.Location = new System.Drawing.Point(317, 174);
            this.prDampTorqLabel.Name = "prDampTorqLabel";
            this.prDampTorqLabel.Size = new System.Drawing.Size(134, 16);
            this.prDampTorqLabel.TabIndex = 35;
            this.prDampTorqLabel.Text = "Крутящий момент:";
            this.prDampTorqLabel.Visible = false;
            // 
            // springRetPrDampCheck
            // 
            this.springRetPrDampCheck.AutoSize = true;
            this.springRetPrDampCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.springRetPrDampCheck.Location = new System.Drawing.Point(18, 174);
            this.springRetPrDampCheck.Name = "springRetPrDampCheck";
            this.springRetPrDampCheck.Size = new System.Drawing.Size(234, 18);
            this.springRetPrDampCheck.TabIndex = 34;
            this.springRetPrDampCheck.Text = "Привод с пружинным возвратом";
            this.springRetPrDampCheck.UseVisualStyleBackColor = true;
            this.springRetPrDampCheck.CheckedChanged += new System.EventHandler(this.SpringRetPrDampCheck_CheckedChanged);
            // 
            // prDampSLabel
            // 
            this.prDampSLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.prDampSLabel.AutoSize = true;
            this.prDampSLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prDampSLabel.Location = new System.Drawing.Point(317, 134);
            this.prDampSLabel.Name = "prDampSLabel";
            this.prDampSLabel.Size = new System.Drawing.Size(141, 16);
            this.prDampSLabel.TabIndex = 33;
            this.prDampSLabel.Text = "Площадь заслонки:";
            this.prDampSLabel.Visible = false;
            // 
            // label168
            // 
            this.label168.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label168.AutoSize = true;
            this.label168.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label168.Location = new System.Drawing.Point(461, 94);
            this.label168.Name = "label168";
            this.label168.Size = new System.Drawing.Size(24, 16);
            this.label168.TabIndex = 32;
            this.label168.Text = "см";
            // 
            // label167
            // 
            this.label167.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label167.AutoSize = true;
            this.label167.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label167.Location = new System.Drawing.Point(461, 53);
            this.label167.Name = "label167";
            this.label167.Size = new System.Drawing.Size(24, 16);
            this.label167.TabIndex = 31;
            this.label167.Text = "см";
            // 
            // h_prDampBox
            // 
            this.h_prDampBox.Location = new System.Drawing.Point(403, 93);
            this.h_prDampBox.MaxLength = 3;
            this.h_prDampBox.Name = "h_prDampBox";
            this.h_prDampBox.Size = new System.Drawing.Size(52, 21);
            this.h_prDampBox.TabIndex = 30;
            this.h_prDampBox.TextChanged += new System.EventHandler(this.H_prDampBox_TextChanged);
            this.h_prDampBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.H_prDampBox_KeyPress);
            // 
            // b_prDampBox
            // 
            this.b_prDampBox.Location = new System.Drawing.Point(403, 52);
            this.b_prDampBox.MaxLength = 3;
            this.b_prDampBox.Name = "b_prDampBox";
            this.b_prDampBox.Size = new System.Drawing.Size(52, 21);
            this.b_prDampBox.TabIndex = 29;
            this.b_prDampBox.TextChanged += new System.EventHandler(this.B_prDampBox_TextChanged);
            this.b_prDampBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.B_prDampBox_KeyPress);
            // 
            // label166
            // 
            this.label166.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label166.AutoSize = true;
            this.label166.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label166.Location = new System.Drawing.Point(317, 94);
            this.label166.Name = "label166";
            this.label166.Size = new System.Drawing.Size(78, 16);
            this.label166.TabIndex = 28;
            this.label166.Text = "Высота, h ";
            // 
            // label158
            // 
            this.label158.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label158.AutoSize = true;
            this.label158.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label158.Location = new System.Drawing.Point(317, 53);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(78, 16);
            this.label158.TabIndex = 27;
            this.label158.Text = "Ширина, b";
            // 
            // dampPicture
            // 
            this.dampPicture.Image = global::Moderon.Properties.Resources.damp;
            this.dampPicture.Location = new System.Drawing.Point(574, 3);
            this.dampPicture.Name = "dampPicture";
            this.dampPicture.Size = new System.Drawing.Size(140, 237);
            this.dampPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dampPicture.TabIndex = 0;
            this.dampPicture.TabStop = false;
            // 
            // outDampPanel
            // 
            this.outDampPanel.Controls.Add(this.markOutDampPanel);
            this.outDampPanel.Controls.Add(this.outDampTorqLabel);
            this.outDampPanel.Controls.Add(this.outDampSLabel);
            this.outDampPanel.Controls.Add(this.springRetOutDampCheck);
            this.outDampPanel.Controls.Add(this.cmhOutDampLabel);
            this.outDampPanel.Controls.Add(this.outDampCheck);
            this.outDampPanel.Controls.Add(this.cmbOutDampLabel);
            this.outDampPanel.Controls.Add(this.heatOutDampCheck);
            this.outDampPanel.Controls.Add(this.h_outDampBox);
            this.outDampPanel.Controls.Add(this.label15);
            this.outDampPanel.Controls.Add(this.b_outDampBox);
            this.outDampPanel.Controls.Add(this.confOutDampCheck);
            this.outDampPanel.Controls.Add(this.hOutDampLabel);
            this.outDampPanel.Controls.Add(this.label16);
            this.outDampPanel.Controls.Add(this.bOutDampLabel);
            this.outDampPanel.Controls.Add(this.outDampPowCombo);
            this.outDampPanel.Location = new System.Drawing.Point(3, 196);
            this.outDampPanel.Name = "outDampPanel";
            this.outDampPanel.Size = new System.Drawing.Size(524, 242);
            this.outDampPanel.TabIndex = 26;
            this.outDampPanel.Visible = false;
            // 
            // markOutDampPanel
            // 
            this.markOutDampPanel.BackgroundImage = global::Moderon.Properties.Resources.green_check;
            this.markOutDampPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.markOutDampPanel.Location = new System.Drawing.Point(493, 166);
            this.markOutDampPanel.Name = "markOutDampPanel";
            this.markOutDampPanel.Size = new System.Drawing.Size(30, 30);
            this.markOutDampPanel.TabIndex = 37;
            this.markOutDampPanel.Visible = false;
            // 
            // outDampTorqLabel
            // 
            this.outDampTorqLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.outDampTorqLabel.AutoSize = true;
            this.outDampTorqLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outDampTorqLabel.Location = new System.Drawing.Point(317, 174);
            this.outDampTorqLabel.Name = "outDampTorqLabel";
            this.outDampTorqLabel.Size = new System.Drawing.Size(134, 16);
            this.outDampTorqLabel.TabIndex = 36;
            this.outDampTorqLabel.Text = "Крутящий момент:";
            this.outDampTorqLabel.Visible = false;
            // 
            // outDampSLabel
            // 
            this.outDampSLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.outDampSLabel.AutoSize = true;
            this.outDampSLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outDampSLabel.Location = new System.Drawing.Point(317, 134);
            this.outDampSLabel.Name = "outDampSLabel";
            this.outDampSLabel.Size = new System.Drawing.Size(141, 16);
            this.outDampSLabel.TabIndex = 41;
            this.outDampSLabel.Text = "Площадь заслонки:";
            this.outDampSLabel.Visible = false;
            // 
            // springRetOutDampCheck
            // 
            this.springRetOutDampCheck.AutoSize = true;
            this.springRetOutDampCheck.Enabled = false;
            this.springRetOutDampCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.springRetOutDampCheck.Location = new System.Drawing.Point(15, 214);
            this.springRetOutDampCheck.Name = "springRetOutDampCheck";
            this.springRetOutDampCheck.Size = new System.Drawing.Size(234, 18);
            this.springRetOutDampCheck.TabIndex = 32;
            this.springRetOutDampCheck.Text = "Привод с пружинным возвратом";
            this.springRetOutDampCheck.UseVisualStyleBackColor = true;
            this.springRetOutDampCheck.CheckedChanged += new System.EventHandler(this.SpringRetOutDampCheck_CheckedChanged);
            // 
            // cmhOutDampLabel
            // 
            this.cmhOutDampLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmhOutDampLabel.AutoSize = true;
            this.cmhOutDampLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmhOutDampLabel.Location = new System.Drawing.Point(461, 94);
            this.cmhOutDampLabel.Name = "cmhOutDampLabel";
            this.cmhOutDampLabel.Size = new System.Drawing.Size(24, 16);
            this.cmhOutDampLabel.TabIndex = 40;
            this.cmhOutDampLabel.Text = "см";
            this.cmhOutDampLabel.Visible = false;
            // 
            // outDampCheck
            // 
            this.outDampCheck.AutoSize = true;
            this.outDampCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outDampCheck.Location = new System.Drawing.Point(15, 54);
            this.outDampCheck.Name = "outDampCheck";
            this.outDampCheck.Size = new System.Drawing.Size(213, 18);
            this.outDampCheck.TabIndex = 31;
            this.outDampCheck.Text = "Наличие вытяжной заслонки";
            this.outDampCheck.UseVisualStyleBackColor = true;
            this.outDampCheck.CheckedChanged += new System.EventHandler(this.OutDampCheck_CheckedChanged);
            // 
            // cmbOutDampLabel
            // 
            this.cmbOutDampLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbOutDampLabel.AutoSize = true;
            this.cmbOutDampLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbOutDampLabel.Location = new System.Drawing.Point(461, 53);
            this.cmbOutDampLabel.Name = "cmbOutDampLabel";
            this.cmbOutDampLabel.Size = new System.Drawing.Size(24, 16);
            this.cmbOutDampLabel.TabIndex = 39;
            this.cmbOutDampLabel.Text = "см";
            this.cmbOutDampLabel.Visible = false;
            // 
            // heatOutDampCheck
            // 
            this.heatOutDampCheck.AutoSize = true;
            this.heatOutDampCheck.Enabled = false;
            this.heatOutDampCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.heatOutDampCheck.Location = new System.Drawing.Point(15, 174);
            this.heatOutDampCheck.Name = "heatOutDampCheck";
            this.heatOutDampCheck.Size = new System.Drawing.Size(212, 18);
            this.heatOutDampCheck.TabIndex = 30;
            this.heatOutDampCheck.Text = "Обогрев вытяжной заслонки";
            this.heatOutDampCheck.UseVisualStyleBackColor = true;
            this.heatOutDampCheck.CheckedChanged += new System.EventHandler(this.HeatOutDampCheck_cmdCheckedChanged);
            // 
            // h_outDampBox
            // 
            this.h_outDampBox.Location = new System.Drawing.Point(403, 93);
            this.h_outDampBox.MaxLength = 3;
            this.h_outDampBox.Name = "h_outDampBox";
            this.h_outDampBox.Size = new System.Drawing.Size(52, 21);
            this.h_outDampBox.TabIndex = 38;
            this.h_outDampBox.Visible = false;
            this.h_outDampBox.TextChanged += new System.EventHandler(this.H_outDampBox_TextChanged);
            this.h_outDampBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.H_outDampBox_KeyPress);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(15, 14);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(275, 16);
            this.label15.TabIndex = 27;
            this.label15.Text = "ВЫТЯЖНАЯ ВОЗДУШНАЯ ЗАСЛОНКА";
            // 
            // b_outDampBox
            // 
            this.b_outDampBox.Location = new System.Drawing.Point(403, 52);
            this.b_outDampBox.MaxLength = 3;
            this.b_outDampBox.Name = "b_outDampBox";
            this.b_outDampBox.Size = new System.Drawing.Size(52, 21);
            this.b_outDampBox.TabIndex = 37;
            this.b_outDampBox.Visible = false;
            this.b_outDampBox.TextChanged += new System.EventHandler(this.B_outDampBox_TextChanged);
            this.b_outDampBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.B_outDampBox_KeyPress);
            // 
            // confOutDampCheck
            // 
            this.confOutDampCheck.AutoSize = true;
            this.confOutDampCheck.Enabled = false;
            this.confOutDampCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.confOutDampCheck.Location = new System.Drawing.Point(15, 134);
            this.confOutDampCheck.Name = "confOutDampCheck";
            this.confOutDampCheck.Size = new System.Drawing.Size(259, 18);
            this.confOutDampCheck.TabIndex = 29;
            this.confOutDampCheck.Text = "Подтверждение открытия заслонки";
            this.confOutDampCheck.UseVisualStyleBackColor = true;
            this.confOutDampCheck.CheckedChanged += new System.EventHandler(this.ConfOutDampCheck_cmdCheckedChanged);
            // 
            // hOutDampLabel
            // 
            this.hOutDampLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.hOutDampLabel.AutoSize = true;
            this.hOutDampLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hOutDampLabel.Location = new System.Drawing.Point(317, 94);
            this.hOutDampLabel.Name = "hOutDampLabel";
            this.hOutDampLabel.Size = new System.Drawing.Size(78, 16);
            this.hOutDampLabel.TabIndex = 36;
            this.hOutDampLabel.Text = "Высота, h ";
            this.hOutDampLabel.Visible = false;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.Location = new System.Drawing.Point(15, 94);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(203, 16);
            this.label16.TabIndex = 27;
            this.label16.Text = "Питание вытяжной заслонки";
            // 
            // bOutDampLabel
            // 
            this.bOutDampLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.bOutDampLabel.AutoSize = true;
            this.bOutDampLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOutDampLabel.Location = new System.Drawing.Point(317, 53);
            this.bOutDampLabel.Name = "bOutDampLabel";
            this.bOutDampLabel.Size = new System.Drawing.Size(78, 16);
            this.bOutDampLabel.TabIndex = 35;
            this.bOutDampLabel.Text = "Ширина, b";
            this.bOutDampLabel.Visible = false;
            // 
            // outDampPowCombo
            // 
            this.outDampPowCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.outDampPowCombo.DisplayMember = "24 В";
            this.outDampPowCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outDampPowCombo.Enabled = false;
            this.outDampPowCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outDampPowCombo.ForeColor = System.Drawing.Color.White;
            this.outDampPowCombo.FormattingEnabled = true;
            this.outDampPowCombo.Items.AddRange(new object[] {
            "24 В",
            "230 В"});
            this.outDampPowCombo.Location = new System.Drawing.Point(229, 89);
            this.outDampPowCombo.Name = "outDampPowCombo";
            this.outDampPowCombo.Size = new System.Drawing.Size(59, 21);
            this.outDampPowCombo.TabIndex = 28;
            this.outDampPowCombo.SelectedIndexChanged += new System.EventHandler(this.OutDampPowCombo_SelectedIndexChanged);
            // 
            // heatPrDampCheck
            // 
            this.heatPrDampCheck.AutoSize = true;
            this.heatPrDampCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.heatPrDampCheck.Location = new System.Drawing.Point(18, 134);
            this.heatPrDampCheck.Name = "heatPrDampCheck";
            this.heatPrDampCheck.Size = new System.Drawing.Size(218, 18);
            this.heatPrDampCheck.TabIndex = 25;
            this.heatPrDampCheck.Text = "Обогрев приточной заслонки";
            this.heatPrDampCheck.UseVisualStyleBackColor = true;
            this.heatPrDampCheck.CheckedChanged += new System.EventHandler(this.HeatPrDampCheck_cmdCheckedChanged);
            // 
            // confPrDampCheck
            // 
            this.confPrDampCheck.AutoSize = true;
            this.confPrDampCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.confPrDampCheck.Location = new System.Drawing.Point(18, 94);
            this.confPrDampCheck.Name = "confPrDampCheck";
            this.confPrDampCheck.Size = new System.Drawing.Size(259, 18);
            this.confPrDampCheck.TabIndex = 24;
            this.confPrDampCheck.Text = "Подтверждение открытия заслонки";
            this.confPrDampCheck.UseVisualStyleBackColor = true;
            this.confPrDampCheck.CheckedChanged += new System.EventHandler(this.ConfPrDampCheck_cmdCheckedChanged);
            // 
            // prDampPowCombo
            // 
            this.prDampPowCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.prDampPowCombo.DisplayMember = "24 В";
            this.prDampPowCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.prDampPowCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prDampPowCombo.ForeColor = System.Drawing.Color.White;
            this.prDampPowCombo.FormattingEnabled = true;
            this.prDampPowCombo.Items.AddRange(new object[] {
            "24 В",
            "230 В"});
            this.prDampPowCombo.Location = new System.Drawing.Point(229, 49);
            this.prDampPowCombo.Name = "prDampPowCombo";
            this.prDampPowCombo.Size = new System.Drawing.Size(59, 21);
            this.prDampPowCombo.TabIndex = 23;
            this.prDampPowCombo.SelectedIndexChanged += new System.EventHandler(this.PrDampPowCombo_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(15, 53);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(207, 16);
            this.label14.TabIndex = 17;
            this.label14.Text = "Питание приточной заслонки";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(15, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(280, 16);
            this.label13.TabIndex = 16;
            this.label13.Text = "ПРИТОЧНАЯ ВОЗДУШНАЯ ЗАСЛОНКА";
            // 
            // heatPage
            // 
            this.heatPage.Controls.Add(this.heatPanel);
            this.heatPage.Location = new System.Drawing.Point(4, 22);
            this.heatPage.Name = "heatPage";
            this.heatPage.Padding = new System.Windows.Forms.Padding(3);
            this.heatPage.Size = new System.Drawing.Size(742, 14);
            this.heatPage.TabIndex = 1;
            this.heatPage.Text = "НАГРЕВАТЕЛЬ";
            // 
            // heatPanel
            // 
            this.heatPanel.Controls.Add(this.heatPicture);
            this.heatPanel.Controls.Add(this.elHeatPanel);
            this.heatPanel.Controls.Add(this.watHeatPanel);
            this.heatPanel.Controls.Add(this.heatTypeCombo);
            this.heatPanel.Controls.Add(this.label17);
            this.heatPanel.Location = new System.Drawing.Point(0, 6);
            this.heatPanel.Name = "heatPanel";
            this.heatPanel.Size = new System.Drawing.Size(717, 604);
            this.heatPanel.TabIndex = 2;
            // 
            // heatPicture
            // 
            this.heatPicture.Image = global::Moderon.Properties.Resources.waterHeater;
            this.heatPicture.Location = new System.Drawing.Point(579, 3);
            this.heatPicture.Name = "heatPicture";
            this.heatPicture.Size = new System.Drawing.Size(129, 222);
            this.heatPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.heatPicture.TabIndex = 0;
            this.heatPicture.TabStop = false;
            // 
            // elHeatPanel
            // 
            this.elHeatPanel.Controls.Add(this.firstStHeatCombo);
            this.elHeatPanel.Controls.Add(this.label38);
            this.elHeatPanel.Controls.Add(this.label24);
            this.elHeatPanel.Controls.Add(this.elHeatPowBox);
            this.elHeatPanel.Controls.Add(this.label23);
            this.elHeatPanel.Controls.Add(this.thermSwitchCombo);
            this.elHeatPanel.Controls.Add(this.label22);
            this.elHeatPanel.Controls.Add(this.elHeatStagesCombo);
            this.elHeatPanel.Controls.Add(this.label21);
            this.elHeatPanel.Controls.Add(this.label20);
            this.elHeatPanel.Location = new System.Drawing.Point(358, 309);
            this.elHeatPanel.Name = "elHeatPanel";
            this.elHeatPanel.Size = new System.Drawing.Size(350, 226);
            this.elHeatPanel.TabIndex = 31;
            this.elHeatPanel.Visible = false;
            // 
            // firstStHeatCombo
            // 
            this.firstStHeatCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.firstStHeatCombo.DisplayMember = "Дискретное";
            this.firstStHeatCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.firstStHeatCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.firstStHeatCombo.ForeColor = System.Drawing.Color.White;
            this.firstStHeatCombo.FormattingEnabled = true;
            this.firstStHeatCombo.Items.AddRange(new object[] {
            "Дискретное",
            "ШИМ 5В",
            "Плавное 0-10 В"});
            this.firstStHeatCombo.Location = new System.Drawing.Point(193, 89);
            this.firstStHeatCombo.Name = "firstStHeatCombo";
            this.firstStHeatCombo.Size = new System.Drawing.Size(137, 21);
            this.firstStHeatCombo.TabIndex = 37;
            this.firstStHeatCombo.SelectedIndexChanged += new System.EventHandler(this.FirstStHeatCombo_cmdSelectedIndexChanged);
            // 
            // label38
            // 
            this.label38.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label38.Location = new System.Drawing.Point(12, 94);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(176, 16);
            this.label38.TabIndex = 36;
            this.label38.Text = "Управление 1-й ступени";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label24.Location = new System.Drawing.Point(307, 174);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(30, 16);
            this.label24.TabIndex = 35;
            this.label24.Text = "кВт";
            // 
            // elHeatPowBox
            // 
            this.elHeatPowBox.Location = new System.Drawing.Point(246, 174);
            this.elHeatPowBox.MaxLength = 4;
            this.elHeatPowBox.Name = "elHeatPowBox";
            this.elHeatPowBox.Size = new System.Drawing.Size(54, 21);
            this.elHeatPowBox.TabIndex = 34;
            this.elHeatPowBox.Text = "4,0";
            this.elHeatPowBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ElHeatPowBox_KeyPress);
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label23.Location = new System.Drawing.Point(11, 174);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(228, 16);
            this.label23.TabIndex = 33;
            this.label23.Text = "Номинальная мощность ступени";
            // 
            // thermSwitchCombo
            // 
            this.thermSwitchCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.thermSwitchCombo.DisplayMember = "0";
            this.thermSwitchCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.thermSwitchCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.thermSwitchCombo.ForeColor = System.Drawing.Color.White;
            this.thermSwitchCombo.FormattingEnabled = true;
            this.thermSwitchCombo.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.thermSwitchCombo.Location = new System.Drawing.Point(247, 129);
            this.thermSwitchCombo.Name = "thermSwitchCombo";
            this.thermSwitchCombo.Size = new System.Drawing.Size(43, 21);
            this.thermSwitchCombo.TabIndex = 32;
            this.thermSwitchCombo.SelectedIndexChanged += new System.EventHandler(this.ThermSwitchCombo_cmdSelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label22.Location = new System.Drawing.Point(11, 134);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(230, 16);
            this.label22.TabIndex = 31;
            this.label22.Text = "Количество термовыключателей";
            // 
            // elHeatStagesCombo
            // 
            this.elHeatStagesCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.elHeatStagesCombo.DisplayMember = "1";
            this.elHeatStagesCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.elHeatStagesCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.elHeatStagesCombo.ForeColor = System.Drawing.Color.White;
            this.elHeatStagesCombo.FormattingEnabled = true;
            this.elHeatStagesCombo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.elHeatStagesCombo.Location = new System.Drawing.Point(192, 49);
            this.elHeatStagesCombo.Name = "elHeatStagesCombo";
            this.elHeatStagesCombo.Size = new System.Drawing.Size(43, 21);
            this.elHeatStagesCombo.TabIndex = 30;
            this.elHeatStagesCombo.SelectedIndexChanged += new System.EventHandler(this.ElHeatStagesCombo_cmdSelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label21.Location = new System.Drawing.Point(11, 54);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(175, 16);
            this.label21.TabIndex = 29;
            this.label21.Text = "Число ступеней нагрева";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.Location = new System.Drawing.Point(11, 14);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(240, 16);
            this.label20.TabIndex = 29;
            this.label20.Text = "ЭЛЕКТРИЧЕСКИЙ НАГРЕВАТЕЛЬ";
            // 
            // watHeatPanel
            // 
            this.watHeatPanel.Controls.Add(this.confHeatResPumpCheck);
            this.watHeatPanel.Controls.Add(this.pumpCurResProtect);
            this.watHeatPanel.Controls.Add(this.reservPumpHeater);
            this.watHeatPanel.Controls.Add(this.label19);
            this.watHeatPanel.Controls.Add(this.label18);
            this.watHeatPanel.Controls.Add(this.pumpCurProtect);
            this.watHeatPanel.Controls.Add(this.watSensHeatCheck);
            this.watHeatPanel.Controls.Add(this.analogSigHeatCheck);
            this.watHeatPanel.Controls.Add(this.confHeatPumpCheck);
            this.watHeatPanel.Controls.Add(this.powPumpCombo);
            this.watHeatPanel.Controls.Add(this.TF_heaterCheck);
            this.watHeatPanel.Location = new System.Drawing.Point(3, 37);
            this.watHeatPanel.Name = "watHeatPanel";
            this.watHeatPanel.Size = new System.Drawing.Size(322, 434);
            this.watHeatPanel.TabIndex = 30;
            // 
            // confHeatResPumpCheck
            // 
            this.confHeatResPumpCheck.AutoSize = true;
            this.confHeatResPumpCheck.Enabled = false;
            this.confHeatResPumpCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.confHeatResPumpCheck.Location = new System.Drawing.Point(15, 254);
            this.confHeatResPumpCheck.Name = "confHeatResPumpCheck";
            this.confHeatResPumpCheck.Size = new System.Drawing.Size(241, 18);
            this.confHeatResPumpCheck.TabIndex = 41;
            this.confHeatResPumpCheck.Text = "Подтверждение работы резерва";
            this.confHeatResPumpCheck.UseVisualStyleBackColor = true;
            this.confHeatResPumpCheck.CheckedChanged += new System.EventHandler(this.ConfHeatResPumpCheck_cmdCheckedChanged);
            // 
            // pumpCurResProtect
            // 
            this.pumpCurResProtect.AutoSize = true;
            this.pumpCurResProtect.Enabled = false;
            this.pumpCurResProtect.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pumpCurResProtect.Location = new System.Drawing.Point(15, 294);
            this.pumpCurResProtect.Name = "pumpCurResProtect";
            this.pumpCurResProtect.Size = new System.Drawing.Size(256, 18);
            this.pumpCurResProtect.TabIndex = 40;
            this.pumpCurResProtect.Text = "Защита резервного насоса по току";
            this.pumpCurResProtect.UseVisualStyleBackColor = true;
            this.pumpCurResProtect.CheckedChanged += new System.EventHandler(this.PumpCurResProtect_cmdCheckedChanged);
            // 
            // reservPumpHeater
            // 
            this.reservPumpHeater.AutoSize = true;
            this.reservPumpHeater.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.reservPumpHeater.Location = new System.Drawing.Point(15, 214);
            this.reservPumpHeater.Name = "reservPumpHeater";
            this.reservPumpHeater.Size = new System.Drawing.Size(136, 18);
            this.reservPumpHeater.TabIndex = 39;
            this.reservPumpHeater.Text = "Резервный насос";
            this.reservPumpHeater.UseVisualStyleBackColor = true;
            this.reservPumpHeater.CheckedChanged += new System.EventHandler(this.ReservPumpHeater_CheckedChanged);
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label19.Location = new System.Drawing.Point(15, 94);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(116, 16);
            this.label19.TabIndex = 38;
            this.label19.Text = "Питание насоса";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label18.Location = new System.Drawing.Point(12, 15);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(189, 16);
            this.label18.TabIndex = 38;
            this.label18.Text = "ВОДЯНОЙ НАГРЕВАТЕЛЬ";
            // 
            // pumpCurProtect
            // 
            this.pumpCurProtect.AutoSize = true;
            this.pumpCurProtect.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pumpCurProtect.Location = new System.Drawing.Point(15, 174);
            this.pumpCurProtect.Name = "pumpCurProtect";
            this.pumpCurProtect.Size = new System.Drawing.Size(177, 18);
            this.pumpCurProtect.TabIndex = 31;
            this.pumpCurProtect.Text = "Защита насоса по току";
            this.pumpCurProtect.UseVisualStyleBackColor = true;
            this.pumpCurProtect.CheckedChanged += new System.EventHandler(this.PumpCurProtect_cmdCheckedChanged);
            // 
            // watSensHeatCheck
            // 
            this.watSensHeatCheck.AutoSize = true;
            this.watSensHeatCheck.Checked = true;
            this.watSensHeatCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.watSensHeatCheck.Enabled = false;
            this.watSensHeatCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.watSensHeatCheck.Location = new System.Drawing.Point(15, 374);
            this.watSensHeatCheck.Name = "watSensHeatCheck";
            this.watSensHeatCheck.Size = new System.Drawing.Size(176, 18);
            this.watSensHeatCheck.TabIndex = 30;
            this.watSensHeatCheck.Text = "Датчик обратной воды";
            this.watSensHeatCheck.UseVisualStyleBackColor = true;
            // 
            // analogSigHeatCheck
            // 
            this.analogSigHeatCheck.AutoSize = true;
            this.analogSigHeatCheck.Checked = true;
            this.analogSigHeatCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.analogSigHeatCheck.Enabled = false;
            this.analogSigHeatCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.analogSigHeatCheck.Location = new System.Drawing.Point(15, 334);
            this.analogSigHeatCheck.Name = "analogSigHeatCheck";
            this.analogSigHeatCheck.Size = new System.Drawing.Size(210, 18);
            this.analogSigHeatCheck.TabIndex = 29;
            this.analogSigHeatCheck.Text = "Управляющий сигнал 0-10 В";
            this.analogSigHeatCheck.UseVisualStyleBackColor = true;
            // 
            // confHeatPumpCheck
            // 
            this.confHeatPumpCheck.AutoSize = true;
            this.confHeatPumpCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.confHeatPumpCheck.Location = new System.Drawing.Point(15, 134);
            this.confHeatPumpCheck.Name = "confHeatPumpCheck";
            this.confHeatPumpCheck.Size = new System.Drawing.Size(232, 18);
            this.confHeatPumpCheck.TabIndex = 28;
            this.confHeatPumpCheck.Text = "Подтверждение работы насоса";
            this.confHeatPumpCheck.UseVisualStyleBackColor = true;
            this.confHeatPumpCheck.CheckedChanged += new System.EventHandler(this.ConfHeatPumpCheck_cmdCheckedChanged);
            // 
            // powPumpCombo
            // 
            this.powPumpCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.powPumpCombo.DisplayMember = "220 В";
            this.powPumpCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.powPumpCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.powPumpCombo.ForeColor = System.Drawing.Color.White;
            this.powPumpCombo.FormattingEnabled = true;
            this.powPumpCombo.Items.AddRange(new object[] {
            "230 В",
            "380 В"});
            this.powPumpCombo.Location = new System.Drawing.Point(146, 89);
            this.powPumpCombo.Name = "powPumpCombo";
            this.powPumpCombo.Size = new System.Drawing.Size(59, 21);
            this.powPumpCombo.TabIndex = 27;
            // 
            // TF_heaterCheck
            // 
            this.TF_heaterCheck.AutoSize = true;
            this.TF_heaterCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TF_heaterCheck.Location = new System.Drawing.Point(15, 54);
            this.TF_heaterCheck.Name = "TF_heaterCheck";
            this.TF_heaterCheck.Size = new System.Drawing.Size(170, 18);
            this.TF_heaterCheck.TabIndex = 25;
            this.TF_heaterCheck.Text = "Воздушный термостат";
            this.TF_heaterCheck.UseVisualStyleBackColor = true;
            this.TF_heaterCheck.CheckedChanged += new System.EventHandler(this.TF_heaterCheck_cmdCheckedChanged);
            // 
            // heatTypeCombo
            // 
            this.heatTypeCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.heatTypeCombo.DisplayMember = "Водяной";
            this.heatTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.heatTypeCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.heatTypeCombo.ForeColor = System.Drawing.Color.White;
            this.heatTypeCombo.FormattingEnabled = true;
            this.heatTypeCombo.Items.AddRange(new object[] {
            "Водяной",
            "Электрический"});
            this.heatTypeCombo.Location = new System.Drawing.Point(144, 9);
            this.heatTypeCombo.Name = "heatTypeCombo";
            this.heatTypeCombo.Size = new System.Drawing.Size(136, 21);
            this.heatTypeCombo.TabIndex = 29;
            this.heatTypeCombo.SelectedIndexChanged += new System.EventHandler(this.HeatTypeCombo_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.Location = new System.Drawing.Point(6, 13);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(122, 16);
            this.label17.TabIndex = 18;
            this.label17.Text = "Тип нагревателя";
            // 
            // coolPage
            // 
            this.coolPage.Controls.Add(this.coolPanel);
            this.coolPage.Location = new System.Drawing.Point(4, 22);
            this.coolPage.Name = "coolPage";
            this.coolPage.Padding = new System.Windows.Forms.Padding(3);
            this.coolPage.Size = new System.Drawing.Size(742, 14);
            this.coolPage.TabIndex = 2;
            this.coolPage.Text = "ОХЛАДИТЕЛЬ";
            // 
            // coolPanel
            // 
            this.coolPanel.Controls.Add(this.coolPicture);
            this.coolPanel.Controls.Add(this.watCoolPanel);
            this.coolPanel.Controls.Add(this.frCoolPanel);
            this.coolPanel.Controls.Add(this.coolTypeCombo);
            this.coolPanel.Controls.Add(this.label25);
            this.coolPanel.Location = new System.Drawing.Point(0, 6);
            this.coolPanel.Name = "coolPanel";
            this.coolPanel.Size = new System.Drawing.Size(717, 470);
            this.coolPanel.TabIndex = 3;
            // 
            // coolPicture
            // 
            this.coolPicture.Image = global::Moderon.Properties.Resources.freonCooler;
            this.coolPicture.Location = new System.Drawing.Point(579, 3);
            this.coolPicture.Name = "coolPicture";
            this.coolPicture.Size = new System.Drawing.Size(138, 218);
            this.coolPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.coolPicture.TabIndex = 0;
            this.coolPicture.TabStop = false;
            // 
            // watCoolPanel
            // 
            this.watCoolPanel.Controls.Add(this.analogCoolCheck);
            this.watCoolPanel.Controls.Add(this.powWatCoolCombo);
            this.watCoolPanel.Controls.Add(this.label29);
            this.watCoolPanel.Controls.Add(this.label28);
            this.watCoolPanel.Location = new System.Drawing.Point(3, 298);
            this.watCoolPanel.Name = "watCoolPanel";
            this.watCoolPanel.Size = new System.Drawing.Size(277, 138);
            this.watCoolPanel.TabIndex = 33;
            this.watCoolPanel.Visible = false;
            // 
            // analogCoolCheck
            // 
            this.analogCoolCheck.AutoSize = true;
            this.analogCoolCheck.Checked = true;
            this.analogCoolCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.analogCoolCheck.Enabled = false;
            this.analogCoolCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.analogCoolCheck.Location = new System.Drawing.Point(15, 94);
            this.analogCoolCheck.Name = "analogCoolCheck";
            this.analogCoolCheck.Size = new System.Drawing.Size(210, 18);
            this.analogCoolCheck.TabIndex = 22;
            this.analogCoolCheck.Text = "Управляющий сигнал 0-10 В";
            this.analogCoolCheck.UseVisualStyleBackColor = true;
            this.analogCoolCheck.CheckedChanged += new System.EventHandler(this.AnalogCoolCheck_cmdCheckedChanged);
            // 
            // powWatCoolCombo
            // 
            this.powWatCoolCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.powWatCoolCombo.DisplayMember = "24 В";
            this.powWatCoolCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.powWatCoolCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.powWatCoolCombo.ForeColor = System.Drawing.Color.White;
            this.powWatCoolCombo.FormattingEnabled = true;
            this.powWatCoolCombo.Items.AddRange(new object[] {
            "24 В",
            "230 В"});
            this.powWatCoolCombo.Location = new System.Drawing.Point(205, 49);
            this.powWatCoolCombo.Name = "powWatCoolCombo";
            this.powWatCoolCombo.Size = new System.Drawing.Size(59, 21);
            this.powWatCoolCombo.TabIndex = 21;
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label29.Location = new System.Drawing.Point(10, 54);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(184, 16);
            this.label29.TabIndex = 20;
            this.label29.Text = "Питание привода вентиля";
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label28.Location = new System.Drawing.Point(10, 14);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(185, 16);
            this.label28.TabIndex = 19;
            this.label28.Text = "ВОДЯНОЙ ОХЛАДИТЕЛЬ";
            // 
            // frCoolPanel
            // 
            this.frCoolPanel.Controls.Add(this.analogFreonCheck);
            this.frCoolPanel.Controls.Add(this.thermoCoolerCheck);
            this.frCoolPanel.Controls.Add(this.dehumModeCheck);
            this.frCoolPanel.Controls.Add(this.alarmFrCoolCheck);
            this.frCoolPanel.Controls.Add(this.frCoolStagesCombo);
            this.frCoolPanel.Controls.Add(this.label27);
            this.frCoolPanel.Controls.Add(this.label26);
            this.frCoolPanel.Location = new System.Drawing.Point(3, 36);
            this.frCoolPanel.Name = "frCoolPanel";
            this.frCoolPanel.Size = new System.Drawing.Size(277, 254);
            this.frCoolPanel.TabIndex = 32;
            // 
            // analogFreonCheck
            // 
            this.analogFreonCheck.AutoSize = true;
            this.analogFreonCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.analogFreonCheck.Location = new System.Drawing.Point(15, 174);
            this.analogFreonCheck.Name = "analogFreonCheck";
            this.analogFreonCheck.Size = new System.Drawing.Size(210, 18);
            this.analogFreonCheck.TabIndex = 35;
            this.analogFreonCheck.Text = "Управляющий сигнал 0-10 В";
            this.analogFreonCheck.UseVisualStyleBackColor = true;
            this.analogFreonCheck.CheckedChanged += new System.EventHandler(this.AnalogFreonCheck_cmdCheckedChanged);
            // 
            // thermoCoolerCheck
            // 
            this.thermoCoolerCheck.AutoSize = true;
            this.thermoCoolerCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.thermoCoolerCheck.Location = new System.Drawing.Point(15, 134);
            this.thermoCoolerCheck.Name = "thermoCoolerCheck";
            this.thermoCoolerCheck.Size = new System.Drawing.Size(170, 18);
            this.thermoCoolerCheck.TabIndex = 34;
            this.thermoCoolerCheck.Text = "Воздушный термостат";
            this.thermoCoolerCheck.UseVisualStyleBackColor = true;
            this.thermoCoolerCheck.CheckedChanged += new System.EventHandler(this.ThermoCoolerCheck_cmdCheckedChanged);
            // 
            // dehumModeCheck
            // 
            this.dehumModeCheck.AutoSize = true;
            this.dehumModeCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dehumModeCheck.Location = new System.Drawing.Point(15, 214);
            this.dehumModeCheck.Name = "dehumModeCheck";
            this.dehumModeCheck.Size = new System.Drawing.Size(136, 18);
            this.dehumModeCheck.TabIndex = 33;
            this.dehumModeCheck.Text = "Режим осушения";
            this.dehumModeCheck.UseVisualStyleBackColor = true;
            this.dehumModeCheck.Visible = false;
            this.dehumModeCheck.CheckedChanged += new System.EventHandler(this.DehumModeCheck_CheckedChanged);
            // 
            // alarmFrCoolCheck
            // 
            this.alarmFrCoolCheck.AutoSize = true;
            this.alarmFrCoolCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.alarmFrCoolCheck.Location = new System.Drawing.Point(15, 94);
            this.alarmFrCoolCheck.Name = "alarmFrCoolCheck";
            this.alarmFrCoolCheck.Size = new System.Drawing.Size(145, 18);
            this.alarmFrCoolCheck.TabIndex = 32;
            this.alarmFrCoolCheck.Text = "Аварийный сигнал";
            this.alarmFrCoolCheck.UseVisualStyleBackColor = true;
            this.alarmFrCoolCheck.CheckedChanged += new System.EventHandler(this.AlarmFrCoolCheck_cmdCheckedChanged);
            // 
            // frCoolStagesCombo
            // 
            this.frCoolStagesCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.frCoolStagesCombo.DisplayMember = "1";
            this.frCoolStagesCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.frCoolStagesCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.frCoolStagesCombo.ForeColor = System.Drawing.Color.White;
            this.frCoolStagesCombo.FormattingEnabled = true;
            this.frCoolStagesCombo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.frCoolStagesCombo.Location = new System.Drawing.Point(182, 49);
            this.frCoolStagesCombo.Name = "frCoolStagesCombo";
            this.frCoolStagesCombo.Size = new System.Drawing.Size(43, 21);
            this.frCoolStagesCombo.TabIndex = 31;
            this.frCoolStagesCombo.SelectedIndexChanged += new System.EventHandler(this.FrCoolStagesCombo_cmdSelectedIndexChanged);
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label27.Location = new System.Drawing.Point(15, 54);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(154, 16);
            this.label27.TabIndex = 30;
            this.label27.Text = "Количество ступеней";
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label26.Location = new System.Drawing.Point(15, 14);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(208, 16);
            this.label26.TabIndex = 18;
            this.label26.Text = "ФРЕОНОВЫЙ ОХЛАДИТЕЛЬ";
            // 
            // coolTypeCombo
            // 
            this.coolTypeCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.coolTypeCombo.DisplayMember = "Фреоновый";
            this.coolTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coolTypeCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.coolTypeCombo.ForeColor = System.Drawing.Color.White;
            this.coolTypeCombo.FormattingEnabled = true;
            this.coolTypeCombo.Items.AddRange(new object[] {
            "Фреоновый",
            "Водяной"});
            this.coolTypeCombo.Location = new System.Drawing.Point(135, 9);
            this.coolTypeCombo.Name = "coolTypeCombo";
            this.coolTypeCombo.Size = new System.Drawing.Size(136, 21);
            this.coolTypeCombo.TabIndex = 31;
            this.coolTypeCombo.SelectedIndexChanged += new System.EventHandler(this.CoolTypeCombo_SelectedIndexChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label25.Location = new System.Drawing.Point(6, 13);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(115, 16);
            this.label25.TabIndex = 30;
            this.label25.Text = "Тип охладителя";
            // 
            // humidPage
            // 
            this.humidPage.Controls.Add(this.humidPanel);
            this.humidPage.Location = new System.Drawing.Point(4, 22);
            this.humidPage.Name = "humidPage";
            this.humidPage.Padding = new System.Windows.Forms.Padding(3);
            this.humidPage.Size = new System.Drawing.Size(742, 14);
            this.humidPage.TabIndex = 3;
            this.humidPage.Text = "УВЛАЖНИТЕЛЬ";
            // 
            // humidPanel
            // 
            this.humidPanel.Controls.Add(this.humidPicture);
            this.humidPanel.Controls.Add(this.cellHumidPanel);
            this.humidPanel.Controls.Add(this.steamHumidPanel);
            this.humidPanel.Controls.Add(this.humidTypeCombo);
            this.humidPanel.Controls.Add(this.label34);
            this.humidPanel.Location = new System.Drawing.Point(3, 6);
            this.humidPanel.Name = "humidPanel";
            this.humidPanel.Size = new System.Drawing.Size(717, 378);
            this.humidPanel.TabIndex = 2;
            // 
            // humidPicture
            // 
            this.humidPicture.Image = global::Moderon.Properties.Resources.humid;
            this.humidPicture.Location = new System.Drawing.Point(546, 3);
            this.humidPicture.Name = "humidPicture";
            this.humidPicture.Size = new System.Drawing.Size(165, 216);
            this.humidPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.humidPicture.TabIndex = 0;
            this.humidPicture.TabStop = false;
            // 
            // cellHumidPanel
            // 
            this.cellHumidPanel.Controls.Add(this.powPumpHumidCheck);
            this.cellHumidPanel.Controls.Add(this.label36);
            this.cellHumidPanel.Location = new System.Drawing.Point(3, 218);
            this.cellHumidPanel.Name = "cellHumidPanel";
            this.cellHumidPanel.Size = new System.Drawing.Size(234, 104);
            this.cellHumidPanel.TabIndex = 35;
            this.cellHumidPanel.Visible = false;
            // 
            // powPumpHumidCheck
            // 
            this.powPumpHumidCheck.AutoSize = true;
            this.powPumpHumidCheck.Checked = true;
            this.powPumpHumidCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.powPumpHumidCheck.Enabled = false;
            this.powPumpHumidCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.powPumpHumidCheck.Location = new System.Drawing.Point(15, 54);
            this.powPumpHumidCheck.Name = "powPumpHumidCheck";
            this.powPumpHumidCheck.Size = new System.Drawing.Size(193, 18);
            this.powPumpHumidCheck.TabIndex = 40;
            this.powPumpHumidCheck.Text = "Подача питания на насос";
            this.powPumpHumidCheck.UseVisualStyleBackColor = true;
            this.powPumpHumidCheck.CheckedChanged += new System.EventHandler(this.PowPumpHumidCheck_cmdCheckedChanged);
            // 
            // label36
            // 
            this.label36.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label36.Location = new System.Drawing.Point(10, 14);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(198, 16);
            this.label36.TabIndex = 20;
            this.label36.Text = "СОТОВЫЙ УВЛАЖНИТЕЛЬ";
            // 
            // steamHumidPanel
            // 
            this.steamHumidPanel.Controls.Add(this.alarmHumidCheck);
            this.steamHumidPanel.Controls.Add(this.analogHumCheck);
            this.steamHumidPanel.Controls.Add(this.startHumidCheck);
            this.steamHumidPanel.Controls.Add(this.label35);
            this.steamHumidPanel.Location = new System.Drawing.Point(3, 36);
            this.steamHumidPanel.Name = "steamHumidPanel";
            this.steamHumidPanel.Size = new System.Drawing.Size(226, 168);
            this.steamHumidPanel.TabIndex = 34;
            // 
            // alarmHumidCheck
            // 
            this.alarmHumidCheck.AutoSize = true;
            this.alarmHumidCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.alarmHumidCheck.Location = new System.Drawing.Point(15, 134);
            this.alarmHumidCheck.Name = "alarmHumidCheck";
            this.alarmHumidCheck.Size = new System.Drawing.Size(145, 18);
            this.alarmHumidCheck.TabIndex = 35;
            this.alarmHumidCheck.Text = "Аварийный сигнал";
            this.alarmHumidCheck.UseVisualStyleBackColor = true;
            this.alarmHumidCheck.CheckedChanged += new System.EventHandler(this.AlarmHumidCheck_cmdCheckedChanged);
            // 
            // analogHumCheck
            // 
            this.analogHumCheck.AutoSize = true;
            this.analogHumCheck.Checked = true;
            this.analogHumCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.analogHumCheck.Enabled = false;
            this.analogHumCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.analogHumCheck.Location = new System.Drawing.Point(15, 94);
            this.analogHumCheck.Name = "analogHumCheck";
            this.analogHumCheck.Size = new System.Drawing.Size(210, 18);
            this.analogHumCheck.TabIndex = 34;
            this.analogHumCheck.Text = "Управляющий сигнал 0-10 В";
            this.analogHumCheck.UseVisualStyleBackColor = true;
            this.analogHumCheck.CheckedChanged += new System.EventHandler(this.AnalogHumCheck_cmdCheckedChanged);
            // 
            // startHumidCheck
            // 
            this.startHumidCheck.AutoSize = true;
            this.startHumidCheck.Checked = true;
            this.startHumidCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.startHumidCheck.Enabled = false;
            this.startHumidCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startHumidCheck.Location = new System.Drawing.Point(15, 54);
            this.startHumidCheck.Name = "startHumidCheck";
            this.startHumidCheck.Size = new System.Drawing.Size(150, 18);
            this.startHumidCheck.TabIndex = 33;
            this.startHumidCheck.Text = "Сигнал ПУСК/СТОП";
            this.startHumidCheck.UseVisualStyleBackColor = true;
            this.startHumidCheck.CheckedChanged += new System.EventHandler(this.StartHumidCheck_cmdCheckedChanged);
            // 
            // label35
            // 
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label35.Location = new System.Drawing.Point(10, 14);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(197, 16);
            this.label35.TabIndex = 19;
            this.label35.Text = "ПАРОВОЙ УВЛАЖНИТЕЛЬ";
            // 
            // humidTypeCombo
            // 
            this.humidTypeCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.humidTypeCombo.DisplayMember = "Паровой";
            this.humidTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.humidTypeCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.humidTypeCombo.ForeColor = System.Drawing.Color.White;
            this.humidTypeCombo.FormattingEnabled = true;
            this.humidTypeCombo.Items.AddRange(new object[] {
            "Паровой",
            "Сотовый"});
            this.humidTypeCombo.Location = new System.Drawing.Point(145, 9);
            this.humidTypeCombo.Name = "humidTypeCombo";
            this.humidTypeCombo.Size = new System.Drawing.Size(136, 21);
            this.humidTypeCombo.TabIndex = 33;
            this.humidTypeCombo.SelectedIndexChanged += new System.EventHandler(this.HumidTypeCombo_SelectedIndexChanged);
            // 
            // label34
            // 
            this.label34.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label34.Location = new System.Drawing.Point(6, 14);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(127, 16);
            this.label34.TabIndex = 32;
            this.label34.Text = "Тип увлажнителя";
            // 
            // recircPage
            // 
            this.recircPage.Controls.Add(this.recircPanel);
            this.recircPage.Location = new System.Drawing.Point(4, 22);
            this.recircPage.Name = "recircPage";
            this.recircPage.Padding = new System.Windows.Forms.Padding(3);
            this.recircPage.Size = new System.Drawing.Size(742, 14);
            this.recircPage.TabIndex = 4;
            this.recircPage.Text = "РЕЦИРКУЛЯЦИЯ";
            // 
            // recircPanel
            // 
            this.recircPanel.Controls.Add(this.recircPrDampAOCheck);
            this.recircPanel.Controls.Add(this.markRecircPanel);
            this.recircPanel.Controls.Add(this.recircTorqLabel);
            this.recircPanel.Controls.Add(this.recircSLabel);
            this.recircPanel.Controls.Add(this.label170);
            this.recircPanel.Controls.Add(this.label171);
            this.recircPanel.Controls.Add(this.h_recircBox);
            this.recircPanel.Controls.Add(this.b_recircBox);
            this.recircPanel.Controls.Add(this.label172);
            this.recircPanel.Controls.Add(this.label175);
            this.recircPanel.Controls.Add(this.springRetRecircCheck);
            this.recircPanel.Controls.Add(this.recircPicture);
            this.recircPanel.Controls.Add(this.recircAOSigCheck);
            this.recircPanel.Controls.Add(this.recircPowCombo);
            this.recircPanel.Controls.Add(this.label41);
            this.recircPanel.Controls.Add(this.label39);
            this.recircPanel.Location = new System.Drawing.Point(3, 6);
            this.recircPanel.Name = "recircPanel";
            this.recircPanel.Size = new System.Drawing.Size(717, 337);
            this.recircPanel.TabIndex = 3;
            // 
            // recircPrDampAOCheck
            // 
            this.recircPrDampAOCheck.AutoSize = true;
            this.recircPrDampAOCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.recircPrDampAOCheck.Location = new System.Drawing.Point(18, 94);
            this.recircPrDampAOCheck.Name = "recircPrDampAOCheck";
            this.recircPrDampAOCheck.Size = new System.Drawing.Size(275, 18);
            this.recircPrDampAOCheck.TabIndex = 49;
            this.recircPrDampAOCheck.Text = "Сигнал 0-10 В на приточную заслонку";
            this.recircPrDampAOCheck.UseVisualStyleBackColor = true;
            this.recircPrDampAOCheck.CheckedChanged += new System.EventHandler(this.RecircPrDampAOCheck_CheckedChanged);
            // 
            // markRecircPanel
            // 
            this.markRecircPanel.BackgroundImage = global::Moderon.Properties.Resources.green_check;
            this.markRecircPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.markRecircPanel.Location = new System.Drawing.Point(497, 166);
            this.markRecircPanel.Name = "markRecircPanel";
            this.markRecircPanel.Size = new System.Drawing.Size(30, 30);
            this.markRecircPanel.TabIndex = 48;
            this.markRecircPanel.Visible = false;
            // 
            // recircTorqLabel
            // 
            this.recircTorqLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.recircTorqLabel.AutoSize = true;
            this.recircTorqLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.recircTorqLabel.Location = new System.Drawing.Point(317, 174);
            this.recircTorqLabel.Name = "recircTorqLabel";
            this.recircTorqLabel.Size = new System.Drawing.Size(134, 16);
            this.recircTorqLabel.TabIndex = 47;
            this.recircTorqLabel.Text = "Крутящий момент:";
            this.recircTorqLabel.Visible = false;
            // 
            // recircSLabel
            // 
            this.recircSLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.recircSLabel.AutoSize = true;
            this.recircSLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.recircSLabel.Location = new System.Drawing.Point(317, 134);
            this.recircSLabel.Name = "recircSLabel";
            this.recircSLabel.Size = new System.Drawing.Size(141, 16);
            this.recircSLabel.TabIndex = 46;
            this.recircSLabel.Text = "Площадь заслонки:";
            this.recircSLabel.Visible = false;
            // 
            // label170
            // 
            this.label170.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label170.AutoSize = true;
            this.label170.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label170.Location = new System.Drawing.Point(461, 94);
            this.label170.Name = "label170";
            this.label170.Size = new System.Drawing.Size(24, 16);
            this.label170.TabIndex = 45;
            this.label170.Text = "см";
            // 
            // label171
            // 
            this.label171.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label171.AutoSize = true;
            this.label171.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label171.Location = new System.Drawing.Point(461, 53);
            this.label171.Name = "label171";
            this.label171.Size = new System.Drawing.Size(24, 16);
            this.label171.TabIndex = 44;
            this.label171.Text = "см";
            // 
            // h_recircBox
            // 
            this.h_recircBox.Location = new System.Drawing.Point(403, 93);
            this.h_recircBox.MaxLength = 3;
            this.h_recircBox.Name = "h_recircBox";
            this.h_recircBox.Size = new System.Drawing.Size(52, 21);
            this.h_recircBox.TabIndex = 43;
            this.h_recircBox.TextChanged += new System.EventHandler(this.H_recircBox_TextChanged);
            this.h_recircBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.H_recircBox_KeyPress);
            // 
            // b_recircBox
            // 
            this.b_recircBox.Location = new System.Drawing.Point(403, 52);
            this.b_recircBox.MaxLength = 3;
            this.b_recircBox.Name = "b_recircBox";
            this.b_recircBox.Size = new System.Drawing.Size(52, 21);
            this.b_recircBox.TabIndex = 42;
            this.b_recircBox.TextChanged += new System.EventHandler(this.B_recircBox_TextChanged);
            this.b_recircBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.B_recircBox_KeyPress);
            // 
            // label172
            // 
            this.label172.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label172.AutoSize = true;
            this.label172.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label172.Location = new System.Drawing.Point(317, 94);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(78, 16);
            this.label172.TabIndex = 41;
            this.label172.Text = "Высота, h ";
            // 
            // label175
            // 
            this.label175.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label175.AutoSize = true;
            this.label175.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label175.Location = new System.Drawing.Point(317, 53);
            this.label175.Name = "label175";
            this.label175.Size = new System.Drawing.Size(78, 16);
            this.label175.TabIndex = 40;
            this.label175.Text = "Ширина, b";
            // 
            // springRetRecircCheck
            // 
            this.springRetRecircCheck.AutoSize = true;
            this.springRetRecircCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.springRetRecircCheck.Location = new System.Drawing.Point(18, 174);
            this.springRetRecircCheck.Name = "springRetRecircCheck";
            this.springRetRecircCheck.Size = new System.Drawing.Size(234, 18);
            this.springRetRecircCheck.TabIndex = 39;
            this.springRetRecircCheck.Text = "Привод с пружинным возвратом";
            this.springRetRecircCheck.UseVisualStyleBackColor = true;
            this.springRetRecircCheck.CheckedChanged += new System.EventHandler(this.SpringRetRecircCheck_CheckedChanged);
            // 
            // recircPicture
            // 
            this.recircPicture.Image = global::Moderon.Properties.Resources.damp;
            this.recircPicture.Location = new System.Drawing.Point(577, 3);
            this.recircPicture.Name = "recircPicture";
            this.recircPicture.Size = new System.Drawing.Size(140, 237);
            this.recircPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.recircPicture.TabIndex = 0;
            this.recircPicture.TabStop = false;
            // 
            // recircAOSigCheck
            // 
            this.recircAOSigCheck.AutoSize = true;
            this.recircAOSigCheck.Checked = true;
            this.recircAOSigCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.recircAOSigCheck.Enabled = false;
            this.recircAOSigCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.recircAOSigCheck.Location = new System.Drawing.Point(18, 54);
            this.recircAOSigCheck.Name = "recircAOSigCheck";
            this.recircAOSigCheck.Size = new System.Drawing.Size(210, 18);
            this.recircAOSigCheck.TabIndex = 38;
            this.recircAOSigCheck.Text = "Управляющий сигнал 0-10 В";
            this.recircAOSigCheck.UseVisualStyleBackColor = true;
            this.recircAOSigCheck.CheckedChanged += new System.EventHandler(this.RecircAOSigCheck_cmdCheckedChanged);
            // 
            // recircPowCombo
            // 
            this.recircPowCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.recircPowCombo.DisplayMember = "24 В";
            this.recircPowCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.recircPowCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.recircPowCombo.ForeColor = System.Drawing.Color.White;
            this.recircPowCombo.FormattingEnabled = true;
            this.recircPowCombo.Items.AddRange(new object[] {
            "24 В",
            "230 В"});
            this.recircPowCombo.Location = new System.Drawing.Point(153, 129);
            this.recircPowCombo.Name = "recircPowCombo";
            this.recircPowCombo.Size = new System.Drawing.Size(59, 21);
            this.recircPowCombo.TabIndex = 35;
            this.recircPowCombo.SelectedIndexChanged += new System.EventHandler(this.RecircPowCombo_SelectedIndexChanged);
            // 
            // label41
            // 
            this.label41.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label41.Location = new System.Drawing.Point(12, 134);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(131, 16);
            this.label41.TabIndex = 34;
            this.label41.Text = "Питание заслонки";
            // 
            // label39
            // 
            this.label39.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label39.Location = new System.Drawing.Point(12, 14);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(254, 16);
            this.label39.TabIndex = 16;
            this.label39.Text = "РЕЦИРКУЛЯЦИОННАЯ ЗАСЛОНКА";
            // 
            // recupPage
            // 
            this.recupPage.Controls.Add(this.recupPanel);
            this.recupPage.Location = new System.Drawing.Point(4, 22);
            this.recupPage.Name = "recupPage";
            this.recupPage.Size = new System.Drawing.Size(742, 14);
            this.recupPage.TabIndex = 5;
            this.recupPage.Text = "РЕКУПЕРАТОР";
            // 
            // recupPanel
            // 
            this.recupPanel.Controls.Add(this.recupPicture);
            this.recupPanel.Controls.Add(this.defRecupSensPanel);
            this.recupPanel.Controls.Add(this.plastRecupPanel);
            this.recupPanel.Controls.Add(this.glikRecupPanel);
            this.recupPanel.Controls.Add(this.rotorRecupPanel);
            this.recupPanel.Controls.Add(this.recupTypeCombo);
            this.recupPanel.Controls.Add(this.label43);
            this.recupPanel.Location = new System.Drawing.Point(3, 3);
            this.recupPanel.Name = "recupPanel";
            this.recupPanel.Size = new System.Drawing.Size(717, 536);
            this.recupPanel.TabIndex = 4;
            // 
            // recupPicture
            // 
            this.recupPicture.Image = global::Moderon.Properties.Resources.rotorRecup;
            this.recupPicture.Location = new System.Drawing.Point(597, 3);
            this.recupPicture.Name = "recupPicture";
            this.recupPicture.Size = new System.Drawing.Size(117, 221);
            this.recupPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.recupPicture.TabIndex = 0;
            this.recupPicture.TabStop = false;
            // 
            // defRecupSensPanel
            // 
            this.defRecupSensPanel.Controls.Add(this.recDefPsCheck);
            this.defRecupSensPanel.Controls.Add(this.label49);
            this.defRecupSensPanel.Controls.Add(this.recDefTempCheck);
            this.defRecupSensPanel.Location = new System.Drawing.Point(370, 246);
            this.defRecupSensPanel.Name = "defRecupSensPanel";
            this.defRecupSensPanel.Size = new System.Drawing.Size(325, 128);
            this.defRecupSensPanel.TabIndex = 51;
            this.defRecupSensPanel.Visible = false;
            // 
            // recDefPsCheck
            // 
            this.recDefPsCheck.AutoSize = true;
            this.recDefPsCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.recDefPsCheck.Location = new System.Drawing.Point(15, 93);
            this.recDefPsCheck.Name = "recDefPsCheck";
            this.recDefPsCheck.Size = new System.Drawing.Size(206, 18);
            this.recDefPsCheck.TabIndex = 52;
            this.recDefPsCheck.Text = "Датчик перепада давления";
            this.recDefPsCheck.UseVisualStyleBackColor = true;
            this.recDefPsCheck.CheckedChanged += new System.EventHandler(this.RecDefPsCheck_cmdCheckedChanged);
            // 
            // label49
            // 
            this.label49.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label49.Location = new System.Drawing.Point(12, 17);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(186, 16);
            this.label49.TabIndex = 51;
            this.label49.Text = "ЗАЩИТА ОТ ЗАМОРОЗКИ";
            // 
            // recDefTempCheck
            // 
            this.recDefTempCheck.AutoSize = true;
            this.recDefTempCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.recDefTempCheck.Location = new System.Drawing.Point(15, 53);
            this.recDefTempCheck.Name = "recDefTempCheck";
            this.recDefTempCheck.Size = new System.Drawing.Size(286, 18);
            this.recDefTempCheck.TabIndex = 50;
            this.recDefTempCheck.Text = "Датчик температуры за рекуператором";
            this.recDefTempCheck.UseVisualStyleBackColor = true;
            this.recDefTempCheck.CheckedChanged += new System.EventHandler(this.RecDefTempCheck_cmdCheckedChanged);
            // 
            // plastRecupPanel
            // 
            this.plastRecupPanel.Controls.Add(this.bypassPlastCombo);
            this.plastRecupPanel.Controls.Add(this.label48);
            this.plastRecupPanel.Controls.Add(this.label37);
            this.plastRecupPanel.Location = new System.Drawing.Point(330, 386);
            this.plastRecupPanel.Name = "plastRecupPanel";
            this.plastRecupPanel.Size = new System.Drawing.Size(365, 106);
            this.plastRecupPanel.TabIndex = 33;
            // 
            // bypassPlastCombo
            // 
            this.bypassPlastCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.bypassPlastCombo.DisplayMember = "Нет";
            this.bypassPlastCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bypassPlastCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bypassPlastCombo.ForeColor = System.Drawing.Color.White;
            this.bypassPlastCombo.FormattingEnabled = true;
            this.bypassPlastCombo.Items.AddRange(new object[] {
            "Нет",
            "Откр/Закр",
            "Плавное 0-10 В"});
            this.bypassPlastCombo.Location = new System.Drawing.Point(183, 49);
            this.bypassPlastCombo.Name = "bypassPlastCombo";
            this.bypassPlastCombo.Size = new System.Drawing.Size(139, 21);
            this.bypassPlastCombo.TabIndex = 43;
            this.bypassPlastCombo.SelectedIndexChanged += new System.EventHandler(this.BypassPlastCombo_cmdSelectedIndexChanged);
            // 
            // label48
            // 
            this.label48.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label48.Location = new System.Drawing.Point(13, 14);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(237, 16);
            this.label48.TabIndex = 43;
            this.label48.Text = "ПЛАСТИНЧАТЫЙ РЕКУПЕРАТОР";
            // 
            // label37
            // 
            this.label37.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label37.Location = new System.Drawing.Point(13, 54);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(155, 16);
            this.label37.TabIndex = 42;
            this.label37.Text = "Байпас рекуператора";
            // 
            // glikRecupPanel
            // 
            this.glikRecupPanel.Controls.Add(this.pumpGlikResCurProtect);
            this.glikRecupPanel.Controls.Add(this.confGlikResPumpCheck);
            this.glikRecupPanel.Controls.Add(this.reservPumpGlik);
            this.glikRecupPanel.Controls.Add(this.pumpGlikCurProtect);
            this.glikRecupPanel.Controls.Add(this.pumpGlikConfCheck);
            this.glikRecupPanel.Controls.Add(this.pumpGlicRecCheck);
            this.glikRecupPanel.Controls.Add(this.analogGlikRecCheck);
            this.glikRecupPanel.Controls.Add(this.label50);
            this.glikRecupPanel.Location = new System.Drawing.Point(3, 165);
            this.glikRecupPanel.Name = "glikRecupPanel";
            this.glikRecupPanel.Size = new System.Drawing.Size(322, 368);
            this.glikRecupPanel.TabIndex = 45;
            this.glikRecupPanel.Visible = false;
            // 
            // pumpGlikResCurProtect
            // 
            this.pumpGlikResCurProtect.AutoSize = true;
            this.pumpGlikResCurProtect.Enabled = false;
            this.pumpGlikResCurProtect.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pumpGlikResCurProtect.Location = new System.Drawing.Point(15, 294);
            this.pumpGlikResCurProtect.Name = "pumpGlikResCurProtect";
            this.pumpGlikResCurProtect.Size = new System.Drawing.Size(256, 18);
            this.pumpGlikResCurProtect.TabIndex = 50;
            this.pumpGlikResCurProtect.Text = "Защита резервного насоса по току";
            this.pumpGlikResCurProtect.UseVisualStyleBackColor = true;
            this.pumpGlikResCurProtect.CheckedChanged += new System.EventHandler(this.PumpGlikResCurProtect_cmdCheckedChanged);
            // 
            // confGlikResPumpCheck
            // 
            this.confGlikResPumpCheck.AutoSize = true;
            this.confGlikResPumpCheck.Enabled = false;
            this.confGlikResPumpCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.confGlikResPumpCheck.Location = new System.Drawing.Point(15, 254);
            this.confGlikResPumpCheck.Name = "confGlikResPumpCheck";
            this.confGlikResPumpCheck.Size = new System.Drawing.Size(232, 18);
            this.confGlikResPumpCheck.TabIndex = 49;
            this.confGlikResPumpCheck.Text = "Подтверждение работы насоса";
            this.confGlikResPumpCheck.UseVisualStyleBackColor = true;
            this.confGlikResPumpCheck.CheckedChanged += new System.EventHandler(this.ConfGlikResPumpCheck_cmdCheckedChanged);
            // 
            // reservPumpGlik
            // 
            this.reservPumpGlik.AutoSize = true;
            this.reservPumpGlik.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.reservPumpGlik.Location = new System.Drawing.Point(15, 214);
            this.reservPumpGlik.Name = "reservPumpGlik";
            this.reservPumpGlik.Size = new System.Drawing.Size(136, 18);
            this.reservPumpGlik.TabIndex = 48;
            this.reservPumpGlik.Text = "Резервный насос";
            this.reservPumpGlik.UseVisualStyleBackColor = true;
            this.reservPumpGlik.CheckedChanged += new System.EventHandler(this.ReservPumpGlik_CheckedChanged);
            // 
            // pumpGlikCurProtect
            // 
            this.pumpGlikCurProtect.AutoSize = true;
            this.pumpGlikCurProtect.Enabled = false;
            this.pumpGlikCurProtect.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pumpGlikCurProtect.Location = new System.Drawing.Point(15, 174);
            this.pumpGlikCurProtect.Name = "pumpGlikCurProtect";
            this.pumpGlikCurProtect.Size = new System.Drawing.Size(177, 18);
            this.pumpGlikCurProtect.TabIndex = 47;
            this.pumpGlikCurProtect.Text = "Защита насоса по току";
            this.pumpGlikCurProtect.UseVisualStyleBackColor = true;
            this.pumpGlikCurProtect.CheckedChanged += new System.EventHandler(this.PumpGlikCurProtect_cmdCheckedChanged);
            // 
            // pumpGlikConfCheck
            // 
            this.pumpGlikConfCheck.AutoSize = true;
            this.pumpGlikConfCheck.Enabled = false;
            this.pumpGlikConfCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pumpGlikConfCheck.Location = new System.Drawing.Point(15, 134);
            this.pumpGlikConfCheck.Name = "pumpGlikConfCheck";
            this.pumpGlikConfCheck.Size = new System.Drawing.Size(232, 18);
            this.pumpGlikConfCheck.TabIndex = 46;
            this.pumpGlikConfCheck.Text = "Подтверждение работы насоса";
            this.pumpGlikConfCheck.UseVisualStyleBackColor = true;
            this.pumpGlikConfCheck.CheckedChanged += new System.EventHandler(this.PumpGlikConfCheck_cmdCheckedChanged);
            // 
            // pumpGlicRecCheck
            // 
            this.pumpGlicRecCheck.AutoSize = true;
            this.pumpGlicRecCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pumpGlicRecCheck.Location = new System.Drawing.Point(15, 94);
            this.pumpGlicRecCheck.Name = "pumpGlicRecCheck";
            this.pumpGlicRecCheck.Size = new System.Drawing.Size(177, 18);
            this.pumpGlicRecCheck.TabIndex = 45;
            this.pumpGlicRecCheck.Text = "Циркуляционный насос";
            this.pumpGlicRecCheck.UseVisualStyleBackColor = true;
            this.pumpGlicRecCheck.CheckedChanged += new System.EventHandler(this.PumpGlicRecCheck_checkedChanged);
            // 
            // analogGlikRecCheck
            // 
            this.analogGlikRecCheck.AutoSize = true;
            this.analogGlikRecCheck.Checked = true;
            this.analogGlikRecCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.analogGlikRecCheck.Enabled = false;
            this.analogGlikRecCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.analogGlikRecCheck.Location = new System.Drawing.Point(15, 54);
            this.analogGlikRecCheck.Name = "analogGlikRecCheck";
            this.analogGlikRecCheck.Size = new System.Drawing.Size(207, 18);
            this.analogGlikRecCheck.TabIndex = 44;
            this.analogGlikRecCheck.Text = "Плавное управление 0-10 В";
            this.analogGlikRecCheck.UseVisualStyleBackColor = true;
            this.analogGlikRecCheck.CheckedChanged += new System.EventHandler(this.AnalogGlikRecCheck_cmdCheckedChanged);
            // 
            // label50
            // 
            this.label50.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label50.Location = new System.Drawing.Point(12, 14);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(217, 16);
            this.label50.TabIndex = 43;
            this.label50.Text = "ГЛИКОЛЕВЫЙ РЕКУПЕРАТОР";
            // 
            // rotorRecupPanel
            // 
            this.rotorRecupPanel.Controls.Add(this.analogRotRecCheck);
            this.rotorRecupPanel.Controls.Add(this.startRotRecCheck);
            this.rotorRecupPanel.Controls.Add(this.outSigAlarmRotRecCheck);
            this.rotorRecupPanel.Controls.Add(this.label46);
            this.rotorRecupPanel.Controls.Add(this.powRotRecBox);
            this.rotorRecupPanel.Controls.Add(this.label47);
            this.rotorRecupPanel.Controls.Add(this.rotorPowCombo);
            this.rotorRecupPanel.Controls.Add(this.label45);
            this.rotorRecupPanel.Controls.Add(this.label44);
            this.rotorRecupPanel.Location = new System.Drawing.Point(6, 40);
            this.rotorRecupPanel.Name = "rotorRecupPanel";
            this.rotorRecupPanel.Size = new System.Drawing.Size(322, 119);
            this.rotorRecupPanel.TabIndex = 32;
            // 
            // analogRotRecCheck
            // 
            this.analogRotRecCheck.AutoSize = true;
            this.analogRotRecCheck.Checked = true;
            this.analogRotRecCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.analogRotRecCheck.Enabled = false;
            this.analogRotRecCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.analogRotRecCheck.Location = new System.Drawing.Point(15, 214);
            this.analogRotRecCheck.Name = "analogRotRecCheck";
            this.analogRotRecCheck.Size = new System.Drawing.Size(210, 18);
            this.analogRotRecCheck.TabIndex = 44;
            this.analogRotRecCheck.Text = "Управляющий сигнал 0-10 В";
            this.analogRotRecCheck.UseVisualStyleBackColor = true;
            this.analogRotRecCheck.CheckedChanged += new System.EventHandler(this.AnalogRotRecCheck_cmdCheckedChanged);
            // 
            // startRotRecCheck
            // 
            this.startRotRecCheck.AutoSize = true;
            this.startRotRecCheck.Checked = true;
            this.startRotRecCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.startRotRecCheck.Enabled = false;
            this.startRotRecCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startRotRecCheck.Location = new System.Drawing.Point(15, 174);
            this.startRotRecCheck.Name = "startRotRecCheck";
            this.startRotRecCheck.Size = new System.Drawing.Size(217, 18);
            this.startRotRecCheck.TabIndex = 43;
            this.startRotRecCheck.Text = "Сухой контакт на включение";
            this.startRotRecCheck.UseVisualStyleBackColor = true;
            this.startRotRecCheck.CheckedChanged += new System.EventHandler(this.StartRotRecCheck_cmdCheckedChanged);
            // 
            // outSigAlarmRotRecCheck
            // 
            this.outSigAlarmRotRecCheck.AutoSize = true;
            this.outSigAlarmRotRecCheck.Checked = true;
            this.outSigAlarmRotRecCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.outSigAlarmRotRecCheck.Enabled = false;
            this.outSigAlarmRotRecCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outSigAlarmRotRecCheck.Location = new System.Drawing.Point(15, 134);
            this.outSigAlarmRotRecCheck.Name = "outSigAlarmRotRecCheck";
            this.outSigAlarmRotRecCheck.Size = new System.Drawing.Size(224, 18);
            this.outSigAlarmRotRecCheck.TabIndex = 42;
            this.outSigAlarmRotRecCheck.Text = "Внешний сигнал аварии от ПЧ";
            this.outSigAlarmRotRecCheck.UseVisualStyleBackColor = true;
            this.outSigAlarmRotRecCheck.CheckedChanged += new System.EventHandler(this.OutSigAlarmRotRecCheck_cmdCheckedChanged);
            // 
            // label46
            // 
            this.label46.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label46.Location = new System.Drawing.Point(229, 90);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(30, 16);
            this.label46.TabIndex = 41;
            this.label46.Text = "кВт";
            // 
            // powRotRecBox
            // 
            this.powRotRecBox.Location = new System.Drawing.Point(170, 89);
            this.powRotRecBox.MaxLength = 4;
            this.powRotRecBox.Name = "powRotRecBox";
            this.powRotRecBox.Size = new System.Drawing.Size(54, 21);
            this.powRotRecBox.TabIndex = 40;
            this.powRotRecBox.Text = "0,18";
            this.powRotRecBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PowRotRecBox_KeyPress);
            // 
            // label47
            // 
            this.label47.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label47.Location = new System.Drawing.Point(12, 94);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(150, 16);
            this.label47.TabIndex = 39;
            this.label47.Text = "Мощность двигателя";
            // 
            // rotorPowCombo
            // 
            this.rotorPowCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.rotorPowCombo.DisplayMember = "220 В";
            this.rotorPowCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rotorPowCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rotorPowCombo.ForeColor = System.Drawing.Color.White;
            this.rotorPowCombo.FormattingEnabled = true;
            this.rotorPowCombo.Items.AddRange(new object[] {
            "230 В",
            "380 В"});
            this.rotorPowCombo.Location = new System.Drawing.Point(183, 49);
            this.rotorPowCombo.Name = "rotorPowCombo";
            this.rotorPowCombo.Size = new System.Drawing.Size(59, 21);
            this.rotorPowCombo.TabIndex = 24;
            // 
            // label45
            // 
            this.label45.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label45.Location = new System.Drawing.Point(12, 54);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(163, 16);
            this.label45.TabIndex = 23;
            this.label45.Text = "Питание рекуператора";
            // 
            // label44
            // 
            this.label44.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label44.Location = new System.Drawing.Point(12, 14);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(197, 16);
            this.label44.TabIndex = 18;
            this.label44.Text = "РОТОРНЫЙ РЕКУПЕРАТОР";
            // 
            // recupTypeCombo
            // 
            this.recupTypeCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.recupTypeCombo.DisplayMember = "Роторный";
            this.recupTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.recupTypeCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.recupTypeCombo.ForeColor = System.Drawing.Color.White;
            this.recupTypeCombo.FormattingEnabled = true;
            this.recupTypeCombo.Items.AddRange(new object[] {
            "Роторный",
            "Пластинчатый",
            "Гликолевый"});
            this.recupTypeCombo.Location = new System.Drawing.Point(146, 9);
            this.recupTypeCombo.Name = "recupTypeCombo";
            this.recupTypeCombo.Size = new System.Drawing.Size(136, 21);
            this.recupTypeCombo.TabIndex = 31;
            this.recupTypeCombo.SelectedIndexChanged += new System.EventHandler(this.RecupTypeCombo_SelectedIndexChanged);
            // 
            // label43
            // 
            this.label43.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label43.Location = new System.Drawing.Point(6, 13);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(132, 16);
            this.label43.TabIndex = 30;
            this.label43.Text = "Тип рекуператора";
            // 
            // addHeatPage
            // 
            this.addHeatPage.BackColor = System.Drawing.SystemColors.Control;
            this.addHeatPage.Controls.Add(this.secHeatPanel);
            this.addHeatPage.Location = new System.Drawing.Point(4, 22);
            this.addHeatPage.Name = "addHeatPage";
            this.addHeatPage.Size = new System.Drawing.Size(742, 14);
            this.addHeatPage.TabIndex = 8;
            this.addHeatPage.Text = "ДОП НАГРЕВ";
            // 
            // secHeatPanel
            // 
            this.secHeatPanel.Controls.Add(this.heatAddPicture);
            this.secHeatPanel.Controls.Add(this.elAddHeatPanel);
            this.secHeatPanel.Controls.Add(this.watAddHeatPanel);
            this.secHeatPanel.Controls.Add(this.heatAddTypeCombo);
            this.secHeatPanel.Controls.Add(this.label58);
            this.secHeatPanel.Location = new System.Drawing.Point(6, 6);
            this.secHeatPanel.Name = "secHeatPanel";
            this.secHeatPanel.Size = new System.Drawing.Size(717, 657);
            this.secHeatPanel.TabIndex = 3;
            // 
            // heatAddPicture
            // 
            this.heatAddPicture.Image = global::Moderon.Properties.Resources.waterHeater;
            this.heatAddPicture.Location = new System.Drawing.Point(579, 3);
            this.heatAddPicture.Name = "heatAddPicture";
            this.heatAddPicture.Size = new System.Drawing.Size(129, 222);
            this.heatAddPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.heatAddPicture.TabIndex = 0;
            this.heatAddPicture.TabStop = false;
            // 
            // elAddHeatPanel
            // 
            this.elAddHeatPanel.Controls.Add(this.firstStAddHeatCombo);
            this.elAddHeatPanel.Controls.Add(this.label40);
            this.elAddHeatPanel.Controls.Add(this.label51);
            this.elAddHeatPanel.Controls.Add(this.elAddHeatPowBox);
            this.elAddHeatPanel.Controls.Add(this.label52);
            this.elAddHeatPanel.Controls.Add(this.thermAddSwitchCombo);
            this.elAddHeatPanel.Controls.Add(this.label53);
            this.elAddHeatPanel.Controls.Add(this.elHeatAddStagesCombo);
            this.elAddHeatPanel.Controls.Add(this.label54);
            this.elAddHeatPanel.Controls.Add(this.label55);
            this.elAddHeatPanel.Location = new System.Drawing.Point(363, 409);
            this.elAddHeatPanel.Name = "elAddHeatPanel";
            this.elAddHeatPanel.Size = new System.Drawing.Size(351, 220);
            this.elAddHeatPanel.TabIndex = 31;
            this.elAddHeatPanel.Visible = false;
            // 
            // firstStAddHeatCombo
            // 
            this.firstStAddHeatCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.firstStAddHeatCombo.DisplayMember = "Дискретное";
            this.firstStAddHeatCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.firstStAddHeatCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.firstStAddHeatCombo.ForeColor = System.Drawing.Color.White;
            this.firstStAddHeatCombo.FormattingEnabled = true;
            this.firstStAddHeatCombo.Items.AddRange(new object[] {
            "Дискретное",
            "ШИМ 5В",
            "Плавное 0-10 В"});
            this.firstStAddHeatCombo.Location = new System.Drawing.Point(189, 89);
            this.firstStAddHeatCombo.Name = "firstStAddHeatCombo";
            this.firstStAddHeatCombo.Size = new System.Drawing.Size(139, 21);
            this.firstStAddHeatCombo.TabIndex = 39;
            this.firstStAddHeatCombo.SelectedIndexChanged += new System.EventHandler(this.FirstStAddHeatCombo_cmdSelectedIndexChanged);
            // 
            // label40
            // 
            this.label40.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label40.Location = new System.Drawing.Point(9, 94);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(176, 16);
            this.label40.TabIndex = 38;
            this.label40.Text = "Управление 1-й ступени";
            // 
            // label51
            // 
            this.label51.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label51.Location = new System.Drawing.Point(305, 174);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(30, 16);
            this.label51.TabIndex = 35;
            this.label51.Text = "кВт";
            // 
            // elAddHeatPowBox
            // 
            this.elAddHeatPowBox.Location = new System.Drawing.Point(244, 174);
            this.elAddHeatPowBox.MaxLength = 4;
            this.elAddHeatPowBox.Name = "elAddHeatPowBox";
            this.elAddHeatPowBox.Size = new System.Drawing.Size(54, 21);
            this.elAddHeatPowBox.TabIndex = 34;
            this.elAddHeatPowBox.Text = "4,0";
            this.elAddHeatPowBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ElAddHeatPowBox_KeyPress);
            // 
            // label52
            // 
            this.label52.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label52.Location = new System.Drawing.Point(9, 174);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(228, 16);
            this.label52.TabIndex = 33;
            this.label52.Text = "Номинальная мощность ступени";
            // 
            // thermAddSwitchCombo
            // 
            this.thermAddSwitchCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.thermAddSwitchCombo.DisplayMember = "0";
            this.thermAddSwitchCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.thermAddSwitchCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.thermAddSwitchCombo.ForeColor = System.Drawing.Color.White;
            this.thermAddSwitchCombo.FormattingEnabled = true;
            this.thermAddSwitchCombo.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.thermAddSwitchCombo.Location = new System.Drawing.Point(245, 129);
            this.thermAddSwitchCombo.Name = "thermAddSwitchCombo";
            this.thermAddSwitchCombo.Size = new System.Drawing.Size(43, 21);
            this.thermAddSwitchCombo.TabIndex = 32;
            this.thermAddSwitchCombo.SelectedIndexChanged += new System.EventHandler(this.ThermAddSwitchCombo_cmdSelectedIndexChanged);
            // 
            // label53
            // 
            this.label53.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label53.Location = new System.Drawing.Point(9, 134);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(230, 16);
            this.label53.TabIndex = 31;
            this.label53.Text = "Количество термовыключателей";
            // 
            // elHeatAddStagesCombo
            // 
            this.elHeatAddStagesCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.elHeatAddStagesCombo.DisplayMember = "1";
            this.elHeatAddStagesCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.elHeatAddStagesCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.elHeatAddStagesCombo.ForeColor = System.Drawing.Color.White;
            this.elHeatAddStagesCombo.FormattingEnabled = true;
            this.elHeatAddStagesCombo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.elHeatAddStagesCombo.Location = new System.Drawing.Point(190, 49);
            this.elHeatAddStagesCombo.Name = "elHeatAddStagesCombo";
            this.elHeatAddStagesCombo.Size = new System.Drawing.Size(43, 21);
            this.elHeatAddStagesCombo.TabIndex = 30;
            this.elHeatAddStagesCombo.SelectedIndexChanged += new System.EventHandler(this.ElHeatAddStagesCombo_cmdSelectedIndexChanged);
            // 
            // label54
            // 
            this.label54.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label54.Location = new System.Drawing.Point(9, 54);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(175, 16);
            this.label54.TabIndex = 29;
            this.label54.Text = "Число ступеней нагрева";
            // 
            // label55
            // 
            this.label55.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label55.Location = new System.Drawing.Point(9, 14);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(303, 16);
            this.label55.TabIndex = 29;
            this.label55.Text = "ВТОРОЙ ЭЛЕКТРИЧЕСКИЙ НАГРЕВАТЕЛЬ";
            // 
            // watAddHeatPanel
            // 
            this.watAddHeatPanel.Controls.Add(this.confAddHeatResPumpCheck);
            this.watAddHeatPanel.Controls.Add(this.pumpCurResAddProtect);
            this.watAddHeatPanel.Controls.Add(this.reservPumpAddHeater);
            this.watAddHeatPanel.Controls.Add(this.pumpCurAddProtect);
            this.watAddHeatPanel.Controls.Add(this.pumpAddHeatCheck);
            this.watAddHeatPanel.Controls.Add(this.sensWatAddHeatCheck);
            this.watAddHeatPanel.Controls.Add(this.checkBox27);
            this.watAddHeatPanel.Controls.Add(this.confAddHeatPumpCheck);
            this.watAddHeatPanel.Controls.Add(this.powPumpAddCombo);
            this.watAddHeatPanel.Controls.Add(this.label56);
            this.watAddHeatPanel.Controls.Add(this.TF_addHeaterCheck);
            this.watAddHeatPanel.Controls.Add(this.label57);
            this.watAddHeatPanel.Location = new System.Drawing.Point(3, 37);
            this.watAddHeatPanel.Name = "watAddHeatPanel";
            this.watAddHeatPanel.Size = new System.Drawing.Size(298, 483);
            this.watAddHeatPanel.TabIndex = 30;
            // 
            // confAddHeatResPumpCheck
            // 
            this.confAddHeatResPumpCheck.AutoSize = true;
            this.confAddHeatResPumpCheck.Enabled = false;
            this.confAddHeatResPumpCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.confAddHeatResPumpCheck.Location = new System.Drawing.Point(15, 294);
            this.confAddHeatResPumpCheck.Name = "confAddHeatResPumpCheck";
            this.confAddHeatResPumpCheck.Size = new System.Drawing.Size(241, 18);
            this.confAddHeatResPumpCheck.TabIndex = 36;
            this.confAddHeatResPumpCheck.Text = "Подтверждение работы резерва";
            this.confAddHeatResPumpCheck.UseVisualStyleBackColor = true;
            this.confAddHeatResPumpCheck.CheckedChanged += new System.EventHandler(this.ConfAddHeatResPumpCheck_cmdCheckedChanged);
            // 
            // pumpCurResAddProtect
            // 
            this.pumpCurResAddProtect.AutoSize = true;
            this.pumpCurResAddProtect.Enabled = false;
            this.pumpCurResAddProtect.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pumpCurResAddProtect.Location = new System.Drawing.Point(15, 334);
            this.pumpCurResAddProtect.Name = "pumpCurResAddProtect";
            this.pumpCurResAddProtect.Size = new System.Drawing.Size(256, 18);
            this.pumpCurResAddProtect.TabIndex = 35;
            this.pumpCurResAddProtect.Text = "Защита резервного насоса по току";
            this.pumpCurResAddProtect.UseVisualStyleBackColor = true;
            this.pumpCurResAddProtect.CheckedChanged += new System.EventHandler(this.PumpCurResAddProtect_cmdCheckedChanged);
            // 
            // reservPumpAddHeater
            // 
            this.reservPumpAddHeater.AutoSize = true;
            this.reservPumpAddHeater.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.reservPumpAddHeater.Location = new System.Drawing.Point(15, 254);
            this.reservPumpAddHeater.Name = "reservPumpAddHeater";
            this.reservPumpAddHeater.Size = new System.Drawing.Size(136, 18);
            this.reservPumpAddHeater.TabIndex = 34;
            this.reservPumpAddHeater.Text = "Резервный насос";
            this.reservPumpAddHeater.UseVisualStyleBackColor = true;
            this.reservPumpAddHeater.CheckedChanged += new System.EventHandler(this.ReservPumpAddHeater_CheckedChanged);
            // 
            // pumpCurAddProtect
            // 
            this.pumpCurAddProtect.AutoSize = true;
            this.pumpCurAddProtect.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pumpCurAddProtect.Location = new System.Drawing.Point(15, 214);
            this.pumpCurAddProtect.Name = "pumpCurAddProtect";
            this.pumpCurAddProtect.Size = new System.Drawing.Size(177, 18);
            this.pumpCurAddProtect.TabIndex = 33;
            this.pumpCurAddProtect.Text = "Защита насоса по току";
            this.pumpCurAddProtect.UseVisualStyleBackColor = true;
            this.pumpCurAddProtect.CheckedChanged += new System.EventHandler(this.PumpCurAddProtect_cmdCheckedChanged);
            // 
            // pumpAddHeatCheck
            // 
            this.pumpAddHeatCheck.AutoSize = true;
            this.pumpAddHeatCheck.Checked = true;
            this.pumpAddHeatCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pumpAddHeatCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pumpAddHeatCheck.Location = new System.Drawing.Point(15, 94);
            this.pumpAddHeatCheck.Name = "pumpAddHeatCheck";
            this.pumpAddHeatCheck.Size = new System.Drawing.Size(177, 18);
            this.pumpAddHeatCheck.TabIndex = 32;
            this.pumpAddHeatCheck.Text = "Циркуляционный насос";
            this.pumpAddHeatCheck.UseVisualStyleBackColor = true;
            this.pumpAddHeatCheck.CheckedChanged += new System.EventHandler(this.PumpAddHeatCheck_CheckedChanged);
            // 
            // sensWatAddHeatCheck
            // 
            this.sensWatAddHeatCheck.AutoSize = true;
            this.sensWatAddHeatCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sensWatAddHeatCheck.Location = new System.Drawing.Point(15, 414);
            this.sensWatAddHeatCheck.Name = "sensWatAddHeatCheck";
            this.sensWatAddHeatCheck.Size = new System.Drawing.Size(176, 18);
            this.sensWatAddHeatCheck.TabIndex = 31;
            this.sensWatAddHeatCheck.Text = "Датчик обратной воды";
            this.sensWatAddHeatCheck.UseVisualStyleBackColor = true;
            // 
            // checkBox27
            // 
            this.checkBox27.AutoSize = true;
            this.checkBox27.Checked = true;
            this.checkBox27.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox27.Enabled = false;
            this.checkBox27.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox27.Location = new System.Drawing.Point(15, 374);
            this.checkBox27.Name = "checkBox27";
            this.checkBox27.Size = new System.Drawing.Size(210, 18);
            this.checkBox27.TabIndex = 30;
            this.checkBox27.Text = "Управляющий сигнал 0-10 В";
            this.checkBox27.UseVisualStyleBackColor = true;
            // 
            // confAddHeatPumpCheck
            // 
            this.confAddHeatPumpCheck.AutoSize = true;
            this.confAddHeatPumpCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.confAddHeatPumpCheck.Location = new System.Drawing.Point(15, 174);
            this.confAddHeatPumpCheck.Name = "confAddHeatPumpCheck";
            this.confAddHeatPumpCheck.Size = new System.Drawing.Size(232, 18);
            this.confAddHeatPumpCheck.TabIndex = 28;
            this.confAddHeatPumpCheck.Text = "Подтверждение работы насоса";
            this.confAddHeatPumpCheck.UseVisualStyleBackColor = true;
            this.confAddHeatPumpCheck.CheckedChanged += new System.EventHandler(this.ConfAddHeatPumpCheck_cmdCheckedChanged);
            // 
            // powPumpAddCombo
            // 
            this.powPumpAddCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.powPumpAddCombo.DisplayMember = "220 В";
            this.powPumpAddCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.powPumpAddCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.powPumpAddCombo.ForeColor = System.Drawing.Color.White;
            this.powPumpAddCombo.FormattingEnabled = true;
            this.powPumpAddCombo.Items.AddRange(new object[] {
            "230 В",
            "380 В"});
            this.powPumpAddCombo.Location = new System.Drawing.Point(144, 129);
            this.powPumpAddCombo.Name = "powPumpAddCombo";
            this.powPumpAddCombo.Size = new System.Drawing.Size(59, 21);
            this.powPumpAddCombo.TabIndex = 27;
            // 
            // label56
            // 
            this.label56.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label56.Location = new System.Drawing.Point(13, 134);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(116, 16);
            this.label56.TabIndex = 26;
            this.label56.Text = "Питание насоса";
            // 
            // TF_addHeaterCheck
            // 
            this.TF_addHeaterCheck.AutoSize = true;
            this.TF_addHeaterCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TF_addHeaterCheck.Location = new System.Drawing.Point(15, 54);
            this.TF_addHeaterCheck.Name = "TF_addHeaterCheck";
            this.TF_addHeaterCheck.Size = new System.Drawing.Size(170, 18);
            this.TF_addHeaterCheck.TabIndex = 25;
            this.TF_addHeaterCheck.Text = "Воздушный термостат";
            this.TF_addHeaterCheck.UseVisualStyleBackColor = true;
            this.TF_addHeaterCheck.CheckedChanged += new System.EventHandler(this.TF_addHeaterCheck_cmdCheckedChanged);
            // 
            // label57
            // 
            this.label57.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label57.Location = new System.Drawing.Point(14, 14);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(252, 16);
            this.label57.TabIndex = 17;
            this.label57.Text = "ВТОРОЙ ВОДЯНОЙ НАГРЕВАТЕЛЬ";
            // 
            // heatAddTypeCombo
            // 
            this.heatAddTypeCombo.BackColor = System.Drawing.Color.DarkGreen;
            this.heatAddTypeCombo.DisplayMember = "Водяной";
            this.heatAddTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.heatAddTypeCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.heatAddTypeCombo.ForeColor = System.Drawing.Color.White;
            this.heatAddTypeCombo.FormattingEnabled = true;
            this.heatAddTypeCombo.Items.AddRange(new object[] {
            "Водяной",
            "Электрический"});
            this.heatAddTypeCombo.Location = new System.Drawing.Point(195, 9);
            this.heatAddTypeCombo.Name = "heatAddTypeCombo";
            this.heatAddTypeCombo.Size = new System.Drawing.Size(136, 21);
            this.heatAddTypeCombo.TabIndex = 29;
            this.heatAddTypeCombo.SelectedIndexChanged += new System.EventHandler(this.HeatAddTypeCombo_SelectedIndexChanged);
            // 
            // label58
            // 
            this.label58.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label58.Location = new System.Drawing.Point(6, 13);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(180, 16);
            this.label58.TabIndex = 18;
            this.label58.Text = "Тип второго нагревателя";
            // 
            // comboSysType
            // 
            this.comboSysType.BackColor = System.Drawing.Color.DarkGreen;
            this.comboSysType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSysType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboSysType.ForeColor = System.Drawing.Color.White;
            this.comboSysType.FormattingEnabled = true;
            this.comboSysType.Items.AddRange(new object[] {
            "Приточная система",
            "Приточно-вытяжная система"});
            this.comboSysType.Location = new System.Drawing.Point(152, 46);
            this.comboSysType.Name = "comboSysType";
            this.comboSysType.Size = new System.Drawing.Size(252, 21);
            this.comboSysType.TabIndex = 4;
            this.comboSysType.SelectedIndexChanged += new System.EventHandler(this.ComboSysType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(14, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "ВЫБОР ЭЛЕМЕНТОВ";
            // 
            // heaterCheck
            // 
            this.heaterCheck.AutoSize = true;
            this.heaterCheck.Location = new System.Drawing.Point(26, 104);
            this.heaterCheck.Name = "heaterCheck";
            this.heaterCheck.Size = new System.Drawing.Size(107, 17);
            this.heaterCheck.TabIndex = 6;
            this.heaterCheck.Text = "НАГРЕВАТЕЛЬ";
            this.heaterCheck.UseVisualStyleBackColor = true;
            this.heaterCheck.CheckedChanged += new System.EventHandler(this.HeaterCheck_CheckedChanged);
            // 
            // coolerCheck
            // 
            this.coolerCheck.AutoSize = true;
            this.coolerCheck.Location = new System.Drawing.Point(26, 164);
            this.coolerCheck.Name = "coolerCheck";
            this.coolerCheck.Size = new System.Drawing.Size(104, 17);
            this.coolerCheck.TabIndex = 7;
            this.coolerCheck.Text = "ОХЛАДИТЕЛЬ";
            this.coolerCheck.UseVisualStyleBackColor = true;
            this.coolerCheck.CheckedChanged += new System.EventHandler(this.CoolerCheck_CheckedChanged);
            // 
            // humidCheck
            // 
            this.humidCheck.AutoSize = true;
            this.humidCheck.Location = new System.Drawing.Point(26, 194);
            this.humidCheck.Name = "humidCheck";
            this.humidCheck.Size = new System.Drawing.Size(114, 17);
            this.humidCheck.TabIndex = 8;
            this.humidCheck.Text = "УВЛАЖНИТЕЛЬ";
            this.humidCheck.UseVisualStyleBackColor = true;
            this.humidCheck.CheckedChanged += new System.EventHandler(this.HumidCheck_CheckedChanged);
            // 
            // recircCheck
            // 
            this.recircCheck.AutoSize = true;
            this.recircCheck.Enabled = false;
            this.recircCheck.Location = new System.Drawing.Point(26, 224);
            this.recircCheck.Name = "recircCheck";
            this.recircCheck.Size = new System.Drawing.Size(121, 17);
            this.recircCheck.TabIndex = 9;
            this.recircCheck.Text = "РЕЦИРКУЛЯЦИЯ";
            this.recircCheck.UseVisualStyleBackColor = true;
            this.recircCheck.CheckedChanged += new System.EventHandler(this.RecircCheck_CheckedChanged);
            // 
            // recupCheck
            // 
            this.recupCheck.AutoSize = true;
            this.recupCheck.Enabled = false;
            this.recupCheck.Location = new System.Drawing.Point(26, 254);
            this.recupCheck.Name = "recupCheck";
            this.recupCheck.Size = new System.Drawing.Size(109, 17);
            this.recupCheck.TabIndex = 10;
            this.recupCheck.Text = "РЕКУПЕРАТОР";
            this.recupCheck.UseVisualStyleBackColor = true;
            this.recupCheck.CheckedChanged += new System.EventHandler(this.RecupCheck_CheckedChanged);
            // 
            // panelElements
            // 
            this.panelElements.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelElements.Controls.Add(this.addHeatCheck);
            this.panelElements.Controls.Add(this.filterCheck);
            this.panelElements.Controls.Add(this.dampCheck);
            this.panelElements.Controls.Add(this.resetButton);
            this.panelElements.Controls.Add(this.label2);
            this.panelElements.Controls.Add(this.recupCheck);
            this.panelElements.Controls.Add(this.heaterCheck);
            this.panelElements.Controls.Add(this.recircCheck);
            this.panelElements.Controls.Add(this.coolerCheck);
            this.panelElements.Controls.Add(this.humidCheck);
            this.panelElements.Location = new System.Drawing.Point(785, 117);
            this.panelElements.Name = "panelElements";
            this.panelElements.Size = new System.Drawing.Size(182, 325);
            this.panelElements.TabIndex = 11;
            // 
            // addHeatCheck
            // 
            this.addHeatCheck.AutoSize = true;
            this.addHeatCheck.Location = new System.Drawing.Point(26, 134);
            this.addHeatCheck.Name = "addHeatCheck";
            this.addHeatCheck.Size = new System.Drawing.Size(99, 17);
            this.addHeatCheck.TabIndex = 14;
            this.addHeatCheck.Text = "ДОП НАГРЕВ";
            this.addHeatCheck.UseVisualStyleBackColor = true;
            this.addHeatCheck.CheckedChanged += new System.EventHandler(this.AddHeatCheck_CheckedChanged);
            // 
            // filterCheck
            // 
            this.filterCheck.AutoSize = true;
            this.filterCheck.Location = new System.Drawing.Point(26, 44);
            this.filterCheck.Name = "filterCheck";
            this.filterCheck.Size = new System.Drawing.Size(72, 17);
            this.filterCheck.TabIndex = 13;
            this.filterCheck.Text = "ФИЛЬТР";
            this.filterCheck.UseVisualStyleBackColor = true;
            this.filterCheck.CheckedChanged += new System.EventHandler(this.FilterCheck_CheckedChanged);
            // 
            // dampCheck
            // 
            this.dampCheck.AutoSize = true;
            this.dampCheck.Location = new System.Drawing.Point(26, 74);
            this.dampCheck.Name = "dampCheck";
            this.dampCheck.Size = new System.Drawing.Size(91, 17);
            this.dampCheck.TabIndex = 12;
            this.dampCheck.Text = "ЗАСЛОНКА";
            this.dampCheck.UseVisualStyleBackColor = true;
            this.dampCheck.CheckedChanged += new System.EventHandler(this.DampCheck_CheckedChanged);
            // 
            // resetButton
            // 
            this.resetButton.BackColor = System.Drawing.Color.DarkRed;
            this.resetButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resetButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resetButton.ForeColor = System.Drawing.Color.White;
            this.resetButton.Location = new System.Drawing.Point(46, 287);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(94, 27);
            this.resetButton.TabIndex = 11;
            this.resetButton.Text = "СБРОС";
            this.resetButton.UseVisualStyleBackColor = false;
            this.resetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // loadModbusPanel
            // 
            this.loadModbusPanel.Controls.Add(this.saveBinFileButton);
            this.loadModbusPanel.Controls.Add(this.showWriteBoxCheck);
            this.loadModbusPanel.Controls.Add(this.connectBtn);
            this.loadModbusPanel.Controls.Add(this.labelWriteNetTextBox);
            this.loadModbusPanel.Controls.Add(this.label137);
            this.loadModbusPanel.Controls.Add(this.comboReadType);
            this.loadModbusPanel.Controls.Add(this.connectCheck);
            this.loadModbusPanel.Controls.Add(this.formNetButton);
            this.loadModbusPanel.Controls.Add(this.writeNetButton);
            this.loadModbusPanel.Controls.Add(this.writeNetTextBox);
            this.loadModbusPanel.Controls.Add(this.dataNetTextBox);
            this.loadModbusPanel.Controls.Add(this.readNetBtn);
            this.loadModbusPanel.Controls.Add(this.connectLabel);
            this.loadModbusPanel.Controls.Add(this.netPortBox);
            this.loadModbusPanel.Controls.Add(this.label60);
            this.loadModbusPanel.Controls.Add(this.ipAddressBox);
            this.loadModbusPanel.Controls.Add(this.label59);
            this.loadModbusPanel.Controls.Add(this.label1);
            this.loadModbusPanel.Controls.Add(this.backOptionsButton);
            this.loadModbusPanel.Location = new System.Drawing.Point(15, 191);
            this.loadModbusPanel.Name = "loadModbusPanel";
            this.loadModbusPanel.Size = new System.Drawing.Size(749, 37);
            this.loadModbusPanel.TabIndex = 12;
            this.loadModbusPanel.Visible = false;
            // 
            // saveBinFileButton
            // 
            this.saveBinFileButton.BackColor = System.Drawing.Color.DarkGreen;
            this.saveBinFileButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveBinFileButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveBinFileButton.ForeColor = System.Drawing.Color.White;
            this.saveBinFileButton.Location = new System.Drawing.Point(22, 286);
            this.saveBinFileButton.Name = "saveBinFileButton";
            this.saveBinFileButton.Size = new System.Drawing.Size(209, 33);
            this.saveBinFileButton.TabIndex = 64;
            this.saveBinFileButton.Text = "Скачать бинарный файл";
            this.saveBinFileButton.UseVisualStyleBackColor = false;
            this.saveBinFileButton.Click += new System.EventHandler(this.SaveBinFileButton_Click);
            // 
            // showWriteBoxCheck
            // 
            this.showWriteBoxCheck.AutoSize = true;
            this.showWriteBoxCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.showWriteBoxCheck.Location = new System.Drawing.Point(24, 251);
            this.showWriteBoxCheck.Name = "showWriteBoxCheck";
            this.showWriteBoxCheck.Size = new System.Drawing.Size(213, 18);
            this.showWriteBoxCheck.TabIndex = 63;
            this.showWriteBoxCheck.Text = "Показать данные для записи";
            this.showWriteBoxCheck.UseVisualStyleBackColor = true;
            this.showWriteBoxCheck.CheckedChanged += new System.EventHandler(this.ShowWriteBoxCheck_CheckedChanged);
            // 
            // connectBtn
            // 
            this.connectBtn.BackColor = System.Drawing.Color.DarkGreen;
            this.connectBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.connectBtn.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectBtn.ForeColor = System.Drawing.Color.White;
            this.connectBtn.Location = new System.Drawing.Point(24, 120);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(195, 27);
            this.connectBtn.TabIndex = 62;
            this.connectBtn.Text = "ПОДКЛЮЧИТЬСЯ К ПЛК";
            this.connectBtn.UseVisualStyleBackColor = false;
            this.connectBtn.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // labelWriteNetTextBox
            // 
            this.labelWriteNetTextBox.AutoSize = true;
            this.labelWriteNetTextBox.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWriteNetTextBox.Location = new System.Drawing.Point(394, 231);
            this.labelWriteNetTextBox.Name = "labelWriteNetTextBox";
            this.labelWriteNetTextBox.Size = new System.Drawing.Size(131, 14);
            this.labelWriteNetTextBox.TabIndex = 61;
            this.labelWriteNetTextBox.Text = "Данные для записи";
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label137.Location = new System.Drawing.Point(394, 5);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(106, 14);
            this.label137.TabIndex = 60;
            this.label137.Text = "Состояние ПЛК";
            // 
            // comboReadType
            // 
            this.comboReadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboReadType.Enabled = false;
            this.comboReadType.FormattingEnabled = true;
            this.comboReadType.Items.AddRange(new object[] {
            "Все сигналы",
            "Командные слова",
            "Сигналы DI",
            "Сигналы AI",
            "Сигналы DO",
            "Сигналы AO",
            "Пожарная сигнализация"});
            this.comboReadType.Location = new System.Drawing.Point(24, 161);
            this.comboReadType.Name = "comboReadType";
            this.comboReadType.Size = new System.Drawing.Size(193, 21);
            this.comboReadType.TabIndex = 59;
            this.comboReadType.SelectedIndexChanged += new System.EventHandler(this.СomboReadType_SelectedIndexChanged);
            // 
            // connectCheck
            // 
            this.connectCheck.AutoSize = true;
            this.connectCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectCheck.Location = new System.Drawing.Point(24, 397);
            this.connectCheck.Name = "connectCheck";
            this.connectCheck.Size = new System.Drawing.Size(160, 18);
            this.connectCheck.TabIndex = 58;
            this.connectCheck.Text = "Подключиться к ПЛК";
            this.connectCheck.UseVisualStyleBackColor = true;
            this.connectCheck.Visible = false;
            this.connectCheck.CheckedChanged += new System.EventHandler(this.ConnectCheck_CheckedChanged);
            // 
            // formNetButton
            // 
            this.formNetButton.BackColor = System.Drawing.Color.DarkGreen;
            this.formNetButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.formNetButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.formNetButton.ForeColor = System.Drawing.Color.White;
            this.formNetButton.Location = new System.Drawing.Point(126, 200);
            this.formNetButton.Name = "formNetButton";
            this.formNetButton.Size = new System.Drawing.Size(93, 27);
            this.formNetButton.TabIndex = 57;
            this.formNetButton.Text = "СОБРАТЬ";
            this.formNetButton.UseVisualStyleBackColor = false;
            this.formNetButton.Visible = false;
            this.formNetButton.Click += new System.EventHandler(this.FormNetButton_Click);
            // 
            // writeNetButton
            // 
            this.writeNetButton.BackColor = System.Drawing.Color.DarkGreen;
            this.writeNetButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.writeNetButton.Enabled = false;
            this.writeNetButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.writeNetButton.ForeColor = System.Drawing.Color.White;
            this.writeNetButton.Location = new System.Drawing.Point(241, 200);
            this.writeNetButton.Name = "writeNetButton";
            this.writeNetButton.Size = new System.Drawing.Size(118, 27);
            this.writeNetButton.TabIndex = 56;
            this.writeNetButton.Text = "ЗАПИСАТЬ";
            this.writeNetButton.UseVisualStyleBackColor = false;
            this.writeNetButton.Click += new System.EventHandler(this.WriteNetButton_Click);
            // 
            // writeNetTextBox
            // 
            this.writeNetTextBox.Location = new System.Drawing.Point(397, 248);
            this.writeNetTextBox.Multiline = true;
            this.writeNetTextBox.Name = "writeNetTextBox";
            this.writeNetTextBox.ReadOnly = true;
            this.writeNetTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.writeNetTextBox.Size = new System.Drawing.Size(315, 200);
            this.writeNetTextBox.TabIndex = 55;
            // 
            // dataNetTextBox
            // 
            this.dataNetTextBox.Location = new System.Drawing.Point(397, 23);
            this.dataNetTextBox.Multiline = true;
            this.dataNetTextBox.Name = "dataNetTextBox";
            this.dataNetTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataNetTextBox.Size = new System.Drawing.Size(315, 200);
            this.dataNetTextBox.TabIndex = 54;
            // 
            // readNetBtn
            // 
            this.readNetBtn.BackColor = System.Drawing.Color.DarkGreen;
            this.readNetBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.readNetBtn.Enabled = false;
            this.readNetBtn.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.readNetBtn.ForeColor = System.Drawing.Color.White;
            this.readNetBtn.Location = new System.Drawing.Point(241, 157);
            this.readNetBtn.Name = "readNetBtn";
            this.readNetBtn.Size = new System.Drawing.Size(118, 27);
            this.readNetBtn.TabIndex = 53;
            this.readNetBtn.Text = "ЧИТАТЬ";
            this.readNetBtn.UseVisualStyleBackColor = false;
            this.readNetBtn.Click += new System.EventHandler(this.ReadNetBtn_Click);
            // 
            // connectLabel
            // 
            this.connectLabel.AutoSize = true;
            this.connectLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectLabel.ForeColor = System.Drawing.Color.Red;
            this.connectLabel.Location = new System.Drawing.Point(189, 19);
            this.connectLabel.Name = "connectLabel";
            this.connectLabel.Size = new System.Drawing.Size(112, 14);
            this.connectLabel.TabIndex = 51;
            this.connectLabel.Text = "Нет соединения";
            // 
            // netPortBox
            // 
            this.netPortBox.Location = new System.Drawing.Point(120, 87);
            this.netPortBox.Name = "netPortBox";
            this.netPortBox.Size = new System.Drawing.Size(122, 21);
            this.netPortBox.TabIndex = 49;
            this.netPortBox.Text = "502";
            this.netPortBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label60.Location = new System.Drawing.Point(28, 89);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(39, 14);
            this.label60.TabIndex = 48;
            this.label60.Text = "Порт";
            // 
            // ipAddressBox
            // 
            this.ipAddressBox.Location = new System.Drawing.Point(120, 51);
            this.ipAddressBox.Name = "ipAddressBox";
            this.ipAddressBox.Size = new System.Drawing.Size(122, 21);
            this.ipAddressBox.TabIndex = 47;
            this.ipAddressBox.Text = "192.168.0.101";
            this.ipAddressBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label59.Location = new System.Drawing.Point(28, 53);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(62, 14);
            this.label59.TabIndex = 46;
            this.label59.Text = "IP Адрес";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(28, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 16);
            this.label1.TabIndex = 45;
            this.label1.Text = "НАСТРОЙКА СЕТИ";
            // 
            // backOptionsButton
            // 
            this.backOptionsButton.BackColor = System.Drawing.Color.DarkGreen;
            this.backOptionsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backOptionsButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.backOptionsButton.ForeColor = System.Drawing.Color.White;
            this.backOptionsButton.Location = new System.Drawing.Point(24, 200);
            this.backOptionsButton.Name = "backOptionsButton";
            this.backOptionsButton.Size = new System.Drawing.Size(94, 27);
            this.backOptionsButton.TabIndex = 15;
            this.backOptionsButton.Text = "НАЗАД";
            this.backOptionsButton.UseVisualStyleBackColor = false;
            this.backOptionsButton.Click += new System.EventHandler(this.BackOptionsButton_Click);
            // 
            // formSignalsButton
            // 
            this.formSignalsButton.BackColor = System.Drawing.Color.DarkGreen;
            this.formSignalsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.formSignalsButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.formSignalsButton.ForeColor = System.Drawing.Color.White;
            this.formSignalsButton.Location = new System.Drawing.Point(427, 42);
            this.formSignalsButton.Name = "formSignalsButton";
            this.formSignalsButton.Size = new System.Drawing.Size(191, 27);
            this.formSignalsButton.TabIndex = 15;
            this.formSignalsButton.Text = "ТАБЛИЦА I/O И ЗАГРУЗКА";
            this.formSignalsButton.UseVisualStyleBackColor = false;
            this.formSignalsButton.Click += new System.EventHandler(this.FormSignalsButton_Click);
            // 
            // signalsPanel
            // 
            this.signalsPanel.Controls.Add(this.resetButtonSignals);
            this.signalsPanel.Controls.Add(this.loadToExl);
            this.signalsPanel.Controls.Add(this.loadPLC_SignalsButton);
            this.signalsPanel.Controls.Add(this.signalsReadyLabel);
            this.signalsPanel.Controls.Add(this.tabControlSignals);
            this.signalsPanel.Controls.Add(this.backSignalsButton);
            this.signalsPanel.Location = new System.Drawing.Point(15, 121);
            this.signalsPanel.Name = "signalsPanel";
            this.signalsPanel.Size = new System.Drawing.Size(740, 58);
            this.signalsPanel.TabIndex = 16;
            this.signalsPanel.Visible = false;
            // 
            // resetButtonSignals
            // 
            this.resetButtonSignals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resetButtonSignals.BackColor = System.Drawing.Color.DarkRed;
            this.resetButtonSignals.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resetButtonSignals.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resetButtonSignals.ForeColor = System.Drawing.Color.White;
            this.resetButtonSignals.Location = new System.Drawing.Point(741, 26);
            this.resetButtonSignals.Name = "resetButtonSignals";
            this.resetButtonSignals.Size = new System.Drawing.Size(97, 27);
            this.resetButtonSignals.TabIndex = 62;
            this.resetButtonSignals.Text = "СБРОС";
            this.resetButtonSignals.UseVisualStyleBackColor = false;
            this.resetButtonSignals.Click += new System.EventHandler(this.ResetButtonSignals_Click);
            // 
            // loadToExl
            // 
            this.loadToExl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.loadToExl.BackColor = System.Drawing.Color.DarkGreen;
            this.loadToExl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loadToExl.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadToExl.ForeColor = System.Drawing.Color.White;
            this.loadToExl.Location = new System.Drawing.Point(561, 26);
            this.loadToExl.Name = "loadToExl";
            this.loadToExl.Size = new System.Drawing.Size(173, 27);
            this.loadToExl.TabIndex = 61;
            this.loadToExl.Text = "ВЫГРУЗИТЬ В EXCEL";
            this.loadToExl.UseVisualStyleBackColor = false;
            this.loadToExl.Click += new System.EventHandler(this.LoadToExl_Click);
            // 
            // loadPLC_SignalsButton
            // 
            this.loadPLC_SignalsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.loadPLC_SignalsButton.BackColor = System.Drawing.Color.DarkGreen;
            this.loadPLC_SignalsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loadPLC_SignalsButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadPLC_SignalsButton.ForeColor = System.Drawing.Color.White;
            this.loadPLC_SignalsButton.Location = new System.Drawing.Point(408, 26);
            this.loadPLC_SignalsButton.Name = "loadPLC_SignalsButton";
            this.loadPLC_SignalsButton.Size = new System.Drawing.Size(147, 27);
            this.loadPLC_SignalsButton.TabIndex = 60;
            this.loadPLC_SignalsButton.Text = "ЗАГРУЗИТЬ В ПЛК";
            this.loadPLC_SignalsButton.UseVisualStyleBackColor = false;
            this.loadPLC_SignalsButton.Click += new System.EventHandler(this.LoadPLC_SignalsButton_Click);
            // 
            // signalsReadyLabel
            // 
            this.signalsReadyLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.signalsReadyLabel.AutoSize = true;
            this.signalsReadyLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.signalsReadyLabel.ForeColor = System.Drawing.Color.Green;
            this.signalsReadyLabel.Location = new System.Drawing.Point(123, 32);
            this.signalsReadyLabel.Name = "signalsReadyLabel";
            this.signalsReadyLabel.Size = new System.Drawing.Size(272, 14);
            this.signalsReadyLabel.TabIndex = 23;
            this.signalsReadyLabel.Text = "Карта входов/выходов сформирована";
            // 
            // tabControlSignals
            // 
            this.tabControlSignals.Controls.Add(this.tabUI);
            this.tabControlSignals.Controls.Add(this.tabDO);
            this.tabControlSignals.Controls.Add(this.tabAO);
            this.tabControlSignals.Controls.Add(this.tabCmdWord);
            this.tabControlSignals.Location = new System.Drawing.Point(10, 2);
            this.tabControlSignals.Name = "tabControlSignals";
            this.tabControlSignals.SelectedIndex = 0;
            this.tabControlSignals.Size = new System.Drawing.Size(729, 788);
            this.tabControlSignals.TabIndex = 59;
            this.tabControlSignals.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControlSignals_Selected);
            // 
            // tabUI
            // 
            this.tabUI.AutoScroll = true;
            this.tabUI.BackColor = System.Drawing.SystemColors.Control;
            this.tabUI.Controls.Add(this.block3_UIpanel);
            this.tabUI.Controls.Add(this.block2_UIpanel);
            this.tabUI.Controls.Add(this.block1_UIpanel);
            this.tabUI.Controls.Add(this.plk_UIpanel);
            this.tabUI.Location = new System.Drawing.Point(4, 22);
            this.tabUI.Name = "tabUI";
            this.tabUI.Padding = new System.Windows.Forms.Padding(3);
            this.tabUI.Size = new System.Drawing.Size(721, 762);
            this.tabUI.TabIndex = 4;
            this.tabUI.Text = "Входные сигналы";
            // 
            // block3_UIpanel
            // 
            this.block3_UIpanel.Controls.Add(this.UI16_bl3Label);
            this.block3_UIpanel.Controls.Add(this.UI16bl3_combo);
            this.block3_UIpanel.Controls.Add(this.UI16bl3_typeCombo);
            this.block3_UIpanel.Controls.Add(this.UI16bl3_lab);
            this.block3_UIpanel.Controls.Add(this.UI15_bl3Label);
            this.block3_UIpanel.Controls.Add(this.UI15bl3_combo);
            this.block3_UIpanel.Controls.Add(this.UI15bl3_typeCombo);
            this.block3_UIpanel.Controls.Add(this.UI15bl3_lab);
            this.block3_UIpanel.Controls.Add(this.UI14_bl3Label);
            this.block3_UIpanel.Controls.Add(this.UI14bl3_combo);
            this.block3_UIpanel.Controls.Add(this.UI14bl3_typeCombo);
            this.block3_UIpanel.Controls.Add(this.UI14bl3_lab);
            this.block3_UIpanel.Controls.Add(this.UI13_bl3Label);
            this.block3_UIpanel.Controls.Add(this.UI13bl3_combo);
            this.block3_UIpanel.Controls.Add(this.UI13bl3_typeCombo);
            this.block3_UIpanel.Controls.Add(this.UI13bl3_lab);
            this.block3_UIpanel.Controls.Add(this.UI12_bl3Label);
            this.block3_UIpanel.Controls.Add(this.UI12bl3_combo);
            this.block3_UIpanel.Controls.Add(this.UI12bl3_typeCombo);
            this.block3_UIpanel.Controls.Add(this.UI12bl3_lab);
            this.block3_UIpanel.Controls.Add(this.UI11_bl3Label);
            this.block3_UIpanel.Controls.Add(this.UI11bl3_combo);
            this.block3_UIpanel.Controls.Add(this.UI11bl3_typeCombo);
            this.block3_UIpanel.Controls.Add(this.UI11bl3_lab);
            this.block3_UIpanel.Controls.Add(this.UI10_bl3Label);
            this.block3_UIpanel.Controls.Add(this.UI10bl3_combo);
            this.block3_UIpanel.Controls.Add(this.UI10bl3_typeCombo);
            this.block3_UIpanel.Controls.Add(this.UI10bl3_lab);
            this.block3_UIpanel.Controls.Add(this.UI9_bl3Label);
            this.block3_UIpanel.Controls.Add(this.UI9bl3_combo);
            this.block3_UIpanel.Controls.Add(this.UI9bl3_typeCombo);
            this.block3_UIpanel.Controls.Add(this.UI9bl3_lab);
            this.block3_UIpanel.Controls.Add(this.UI8_bl3Label);
            this.block3_UIpanel.Controls.Add(this.UI8bl3_combo);
            this.block3_UIpanel.Controls.Add(this.UI8bl3_typeCombo);
            this.block3_UIpanel.Controls.Add(this.UI8bl3_lab);
            this.block3_UIpanel.Controls.Add(this.UI7_bl3Label);
            this.block3_UIpanel.Controls.Add(this.UI7bl3_combo);
            this.block3_UIpanel.Controls.Add(this.UI7bl3_typeCombo);
            this.block3_UIpanel.Controls.Add(this.UI7bl3_lab);
            this.block3_UIpanel.Controls.Add(this.UI6_bl3Label);
            this.block3_UIpanel.Controls.Add(this.UI6bl3_combo);
            this.block3_UIpanel.Controls.Add(this.UI6bl3_typeCombo);
            this.block3_UIpanel.Controls.Add(this.UI6bl3_lab);
            this.block3_UIpanel.Controls.Add(this.UI5_bl3Label);
            this.block3_UIpanel.Controls.Add(this.UI5bl3_combo);
            this.block3_UIpanel.Controls.Add(this.UI5bl3_typeCombo);
            this.block3_UIpanel.Controls.Add(this.UI5bl3_lab);
            this.block3_UIpanel.Controls.Add(this.UI4_bl3Label);
            this.block3_UIpanel.Controls.Add(this.UI4bl3_combo);
            this.block3_UIpanel.Controls.Add(this.UI4bl3_typeCombo);
            this.block3_UIpanel.Controls.Add(this.UI4bl3_lab);
            this.block3_UIpanel.Controls.Add(this.UI3_bl3Label);
            this.block3_UIpanel.Controls.Add(this.UI3bl3_combo);
            this.block3_UIpanel.Controls.Add(this.UI3bl3_typeCombo);
            this.block3_UIpanel.Controls.Add(this.UI3bl3_lab);
            this.block3_UIpanel.Controls.Add(this.UI2_bl3Label);
            this.block3_UIpanel.Controls.Add(this.UI2bl3_combo);
            this.block3_UIpanel.Controls.Add(this.UI2bl3_typeCombo);
            this.block3_UIpanel.Controls.Add(this.UI2bl3_lab);
            this.block3_UIpanel.Controls.Add(this.UI1_bl3Label);
            this.block3_UIpanel.Controls.Add(this.UI1bl3_combo);
            this.block3_UIpanel.Controls.Add(this.UIblock3_header);
            this.block3_UIpanel.Controls.Add(this.UI1bl3_typeCombo);
            this.block3_UIpanel.Controls.Add(this.UI1bl3_lab);
            this.block3_UIpanel.Enabled = false;
            this.block3_UIpanel.Location = new System.Drawing.Point(6, 1470);
            this.block3_UIpanel.Name = "block3_UIpanel";
            this.block3_UIpanel.Size = new System.Drawing.Size(637, 537);
            this.block3_UIpanel.TabIndex = 147;
            this.block3_UIpanel.Visible = false;
            // 
            // UI16_bl3Label
            // 
            this.UI16_bl3Label.AutoSize = true;
            this.UI16_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI16_bl3Label.Location = new System.Drawing.Point(10, 500);
            this.UI16_bl3Label.Name = "UI16_bl3Label";
            this.UI16_bl3Label.Size = new System.Drawing.Size(41, 14);
            this.UI16_bl3Label.TabIndex = 142;
            this.UI16_bl3Label.Text = "UI 16";
            this.UI16_bl3Label.Visible = false;
            // 
            // UI16bl3_combo
            // 
            this.UI16bl3_combo.DisplayMember = "Не выбрано";
            this.UI16bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI16bl3_combo.Enabled = false;
            this.UI16bl3_combo.FormattingEnabled = true;
            this.UI16bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI16bl3_combo.Location = new System.Drawing.Point(53, 498);
            this.UI16bl3_combo.Name = "UI16bl3_combo";
            this.UI16bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI16bl3_combo.TabIndex = 143;
            this.UI16bl3_combo.Visible = false;
            this.UI16bl3_combo.SelectedIndexChanged += new System.EventHandler(this.UI16bl3_combo_SelectedIndexChanged);
            // 
            // UI16bl3_typeCombo
            // 
            this.UI16bl3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI16bl3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI16bl3_typeCombo.Enabled = false;
            this.UI16bl3_typeCombo.FormattingEnabled = true;
            this.UI16bl3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI16bl3_typeCombo.Location = new System.Drawing.Point(439, 498);
            this.UI16bl3_typeCombo.Name = "UI16bl3_typeCombo";
            this.UI16bl3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI16bl3_typeCombo.TabIndex = 144;
            this.UI16bl3_typeCombo.Visible = false;
            this.UI16bl3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI16bl3_typeCombo_SelectedIndexChanged);
            // 
            // UI16bl3_lab
            // 
            this.UI16bl3_lab.AutoSize = true;
            this.UI16bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI16bl3_lab.Location = new System.Drawing.Point(568, 500);
            this.UI16bl3_lab.Name = "UI16bl3_lab";
            this.UI16bl3_lab.Size = new System.Drawing.Size(41, 14);
            this.UI16bl3_lab.TabIndex = 145;
            this.UI16bl3_lab.Text = "UI 16";
            // 
            // UI15_bl3Label
            // 
            this.UI15_bl3Label.AutoSize = true;
            this.UI15_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI15_bl3Label.Location = new System.Drawing.Point(10, 469);
            this.UI15_bl3Label.Name = "UI15_bl3Label";
            this.UI15_bl3Label.Size = new System.Drawing.Size(41, 14);
            this.UI15_bl3Label.TabIndex = 138;
            this.UI15_bl3Label.Text = "UI 15";
            this.UI15_bl3Label.Visible = false;
            // 
            // UI15bl3_combo
            // 
            this.UI15bl3_combo.DisplayMember = "Не выбрано";
            this.UI15bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI15bl3_combo.Enabled = false;
            this.UI15bl3_combo.FormattingEnabled = true;
            this.UI15bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI15bl3_combo.Location = new System.Drawing.Point(53, 467);
            this.UI15bl3_combo.Name = "UI15bl3_combo";
            this.UI15bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI15bl3_combo.TabIndex = 139;
            this.UI15bl3_combo.Visible = false;
            this.UI15bl3_combo.SelectedIndexChanged += new System.EventHandler(this.UI15bl3_combo_SelectedIndexChanged);
            // 
            // UI15bl3_typeCombo
            // 
            this.UI15bl3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI15bl3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI15bl3_typeCombo.Enabled = false;
            this.UI15bl3_typeCombo.FormattingEnabled = true;
            this.UI15bl3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI15bl3_typeCombo.Location = new System.Drawing.Point(439, 467);
            this.UI15bl3_typeCombo.Name = "UI15bl3_typeCombo";
            this.UI15bl3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI15bl3_typeCombo.TabIndex = 140;
            this.UI15bl3_typeCombo.Visible = false;
            this.UI15bl3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI15bl3_typeCombo_SelectedIndexChanged);
            // 
            // UI15bl3_lab
            // 
            this.UI15bl3_lab.AutoSize = true;
            this.UI15bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI15bl3_lab.Location = new System.Drawing.Point(568, 469);
            this.UI15bl3_lab.Name = "UI15bl3_lab";
            this.UI15bl3_lab.Size = new System.Drawing.Size(41, 14);
            this.UI15bl3_lab.TabIndex = 141;
            this.UI15bl3_lab.Text = "UI 15";
            // 
            // UI14_bl3Label
            // 
            this.UI14_bl3Label.AutoSize = true;
            this.UI14_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI14_bl3Label.Location = new System.Drawing.Point(10, 438);
            this.UI14_bl3Label.Name = "UI14_bl3Label";
            this.UI14_bl3Label.Size = new System.Drawing.Size(41, 14);
            this.UI14_bl3Label.TabIndex = 134;
            this.UI14_bl3Label.Text = "UI 14";
            this.UI14_bl3Label.Visible = false;
            // 
            // UI14bl3_combo
            // 
            this.UI14bl3_combo.DisplayMember = "Не выбрано";
            this.UI14bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI14bl3_combo.Enabled = false;
            this.UI14bl3_combo.FormattingEnabled = true;
            this.UI14bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI14bl3_combo.Location = new System.Drawing.Point(53, 436);
            this.UI14bl3_combo.Name = "UI14bl3_combo";
            this.UI14bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI14bl3_combo.TabIndex = 135;
            this.UI14bl3_combo.Visible = false;
            this.UI14bl3_combo.SelectedIndexChanged += new System.EventHandler(this.UI14bl3_combo_SelectedIndexChanged);
            // 
            // UI14bl3_typeCombo
            // 
            this.UI14bl3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI14bl3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI14bl3_typeCombo.Enabled = false;
            this.UI14bl3_typeCombo.FormattingEnabled = true;
            this.UI14bl3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI14bl3_typeCombo.Location = new System.Drawing.Point(439, 436);
            this.UI14bl3_typeCombo.Name = "UI14bl3_typeCombo";
            this.UI14bl3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI14bl3_typeCombo.TabIndex = 136;
            this.UI14bl3_typeCombo.Visible = false;
            this.UI14bl3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI14bl3_typeCombo_SelectedIndexChanged);
            // 
            // UI14bl3_lab
            // 
            this.UI14bl3_lab.AutoSize = true;
            this.UI14bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI14bl3_lab.Location = new System.Drawing.Point(568, 438);
            this.UI14bl3_lab.Name = "UI14bl3_lab";
            this.UI14bl3_lab.Size = new System.Drawing.Size(41, 14);
            this.UI14bl3_lab.TabIndex = 137;
            this.UI14bl3_lab.Text = "UI 14";
            // 
            // UI13_bl3Label
            // 
            this.UI13_bl3Label.AutoSize = true;
            this.UI13_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI13_bl3Label.Location = new System.Drawing.Point(10, 407);
            this.UI13_bl3Label.Name = "UI13_bl3Label";
            this.UI13_bl3Label.Size = new System.Drawing.Size(41, 14);
            this.UI13_bl3Label.TabIndex = 130;
            this.UI13_bl3Label.Text = "UI 13";
            this.UI13_bl3Label.Visible = false;
            // 
            // UI13bl3_combo
            // 
            this.UI13bl3_combo.DisplayMember = "Не выбрано";
            this.UI13bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI13bl3_combo.Enabled = false;
            this.UI13bl3_combo.FormattingEnabled = true;
            this.UI13bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI13bl3_combo.Location = new System.Drawing.Point(53, 405);
            this.UI13bl3_combo.Name = "UI13bl3_combo";
            this.UI13bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI13bl3_combo.TabIndex = 131;
            this.UI13bl3_combo.Visible = false;
            this.UI13bl3_combo.SelectedIndexChanged += new System.EventHandler(this.UI13bl3_combo_SelectedIndexChanged);
            // 
            // UI13bl3_typeCombo
            // 
            this.UI13bl3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI13bl3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI13bl3_typeCombo.Enabled = false;
            this.UI13bl3_typeCombo.FormattingEnabled = true;
            this.UI13bl3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI13bl3_typeCombo.Location = new System.Drawing.Point(439, 405);
            this.UI13bl3_typeCombo.Name = "UI13bl3_typeCombo";
            this.UI13bl3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI13bl3_typeCombo.TabIndex = 132;
            this.UI13bl3_typeCombo.Visible = false;
            this.UI13bl3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI13bl3_typeCombo_SelectedIndexChanged);
            // 
            // UI13bl3_lab
            // 
            this.UI13bl3_lab.AutoSize = true;
            this.UI13bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI13bl3_lab.Location = new System.Drawing.Point(568, 407);
            this.UI13bl3_lab.Name = "UI13bl3_lab";
            this.UI13bl3_lab.Size = new System.Drawing.Size(41, 14);
            this.UI13bl3_lab.TabIndex = 133;
            this.UI13bl3_lab.Text = "UI 13";
            // 
            // UI12_bl3Label
            // 
            this.UI12_bl3Label.AutoSize = true;
            this.UI12_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI12_bl3Label.Location = new System.Drawing.Point(10, 376);
            this.UI12_bl3Label.Name = "UI12_bl3Label";
            this.UI12_bl3Label.Size = new System.Drawing.Size(41, 14);
            this.UI12_bl3Label.TabIndex = 126;
            this.UI12_bl3Label.Text = "UI 12";
            this.UI12_bl3Label.Visible = false;
            // 
            // UI12bl3_combo
            // 
            this.UI12bl3_combo.DisplayMember = "Не выбрано";
            this.UI12bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI12bl3_combo.Enabled = false;
            this.UI12bl3_combo.FormattingEnabled = true;
            this.UI12bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI12bl3_combo.Location = new System.Drawing.Point(53, 374);
            this.UI12bl3_combo.Name = "UI12bl3_combo";
            this.UI12bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI12bl3_combo.TabIndex = 127;
            this.UI12bl3_combo.Visible = false;
            this.UI12bl3_combo.SelectedIndexChanged += new System.EventHandler(this.UI12bl3_combo_SelectedIndexChanged);
            // 
            // UI12bl3_typeCombo
            // 
            this.UI12bl3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI12bl3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI12bl3_typeCombo.Enabled = false;
            this.UI12bl3_typeCombo.FormattingEnabled = true;
            this.UI12bl3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI12bl3_typeCombo.Location = new System.Drawing.Point(439, 374);
            this.UI12bl3_typeCombo.Name = "UI12bl3_typeCombo";
            this.UI12bl3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI12bl3_typeCombo.TabIndex = 128;
            this.UI12bl3_typeCombo.Visible = false;
            this.UI12bl3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI12bl3_typeCombo_SelectedIndexChanged);
            // 
            // UI12bl3_lab
            // 
            this.UI12bl3_lab.AutoSize = true;
            this.UI12bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI12bl3_lab.Location = new System.Drawing.Point(568, 376);
            this.UI12bl3_lab.Name = "UI12bl3_lab";
            this.UI12bl3_lab.Size = new System.Drawing.Size(41, 14);
            this.UI12bl3_lab.TabIndex = 129;
            this.UI12bl3_lab.Text = "UI 12";
            // 
            // UI11_bl3Label
            // 
            this.UI11_bl3Label.AutoSize = true;
            this.UI11_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI11_bl3Label.Location = new System.Drawing.Point(10, 345);
            this.UI11_bl3Label.Name = "UI11_bl3Label";
            this.UI11_bl3Label.Size = new System.Drawing.Size(41, 14);
            this.UI11_bl3Label.TabIndex = 122;
            this.UI11_bl3Label.Text = "UI 11";
            this.UI11_bl3Label.Visible = false;
            // 
            // UI11bl3_combo
            // 
            this.UI11bl3_combo.DisplayMember = "Не выбрано";
            this.UI11bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI11bl3_combo.Enabled = false;
            this.UI11bl3_combo.FormattingEnabled = true;
            this.UI11bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI11bl3_combo.Location = new System.Drawing.Point(53, 343);
            this.UI11bl3_combo.Name = "UI11bl3_combo";
            this.UI11bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI11bl3_combo.TabIndex = 123;
            this.UI11bl3_combo.Visible = false;
            this.UI11bl3_combo.SelectedIndexChanged += new System.EventHandler(this.UI11bl3_combo_SelectedIndexChanged);
            // 
            // UI11bl3_typeCombo
            // 
            this.UI11bl3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI11bl3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI11bl3_typeCombo.Enabled = false;
            this.UI11bl3_typeCombo.FormattingEnabled = true;
            this.UI11bl3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI11bl3_typeCombo.Location = new System.Drawing.Point(439, 343);
            this.UI11bl3_typeCombo.Name = "UI11bl3_typeCombo";
            this.UI11bl3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI11bl3_typeCombo.TabIndex = 124;
            this.UI11bl3_typeCombo.Visible = false;
            this.UI11bl3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI11bl3_typeCombo_SelectedIndexChanged);
            // 
            // UI11bl3_lab
            // 
            this.UI11bl3_lab.AutoSize = true;
            this.UI11bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI11bl3_lab.Location = new System.Drawing.Point(568, 345);
            this.UI11bl3_lab.Name = "UI11bl3_lab";
            this.UI11bl3_lab.Size = new System.Drawing.Size(41, 14);
            this.UI11bl3_lab.TabIndex = 125;
            this.UI11bl3_lab.Text = "UI 11";
            // 
            // UI10_bl3Label
            // 
            this.UI10_bl3Label.AutoSize = true;
            this.UI10_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI10_bl3Label.Location = new System.Drawing.Point(10, 314);
            this.UI10_bl3Label.Name = "UI10_bl3Label";
            this.UI10_bl3Label.Size = new System.Drawing.Size(41, 14);
            this.UI10_bl3Label.TabIndex = 118;
            this.UI10_bl3Label.Text = "UI 10";
            this.UI10_bl3Label.Visible = false;
            // 
            // UI10bl3_combo
            // 
            this.UI10bl3_combo.DisplayMember = "Не выбрано";
            this.UI10bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI10bl3_combo.Enabled = false;
            this.UI10bl3_combo.FormattingEnabled = true;
            this.UI10bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI10bl3_combo.Location = new System.Drawing.Point(53, 312);
            this.UI10bl3_combo.Name = "UI10bl3_combo";
            this.UI10bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI10bl3_combo.TabIndex = 119;
            this.UI10bl3_combo.Visible = false;
            this.UI10bl3_combo.SelectedIndexChanged += new System.EventHandler(this.UI10bl3_combo_SelectedIndexChanged);
            // 
            // UI10bl3_typeCombo
            // 
            this.UI10bl3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI10bl3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI10bl3_typeCombo.Enabled = false;
            this.UI10bl3_typeCombo.FormattingEnabled = true;
            this.UI10bl3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI10bl3_typeCombo.Location = new System.Drawing.Point(439, 312);
            this.UI10bl3_typeCombo.Name = "UI10bl3_typeCombo";
            this.UI10bl3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI10bl3_typeCombo.TabIndex = 120;
            this.UI10bl3_typeCombo.Visible = false;
            this.UI10bl3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI10bl3_typeCombo_SelectedIndexChanged);
            // 
            // UI10bl3_lab
            // 
            this.UI10bl3_lab.AutoSize = true;
            this.UI10bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI10bl3_lab.Location = new System.Drawing.Point(568, 314);
            this.UI10bl3_lab.Name = "UI10bl3_lab";
            this.UI10bl3_lab.Size = new System.Drawing.Size(41, 14);
            this.UI10bl3_lab.TabIndex = 121;
            this.UI10bl3_lab.Text = "UI 10";
            // 
            // UI9_bl3Label
            // 
            this.UI9_bl3Label.AutoSize = true;
            this.UI9_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI9_bl3Label.Location = new System.Drawing.Point(10, 283);
            this.UI9_bl3Label.Name = "UI9_bl3Label";
            this.UI9_bl3Label.Size = new System.Drawing.Size(33, 14);
            this.UI9_bl3Label.TabIndex = 114;
            this.UI9_bl3Label.Text = "UI 9";
            this.UI9_bl3Label.Visible = false;
            // 
            // UI9bl3_combo
            // 
            this.UI9bl3_combo.DisplayMember = "Не выбрано";
            this.UI9bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI9bl3_combo.Enabled = false;
            this.UI9bl3_combo.FormattingEnabled = true;
            this.UI9bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI9bl3_combo.Location = new System.Drawing.Point(53, 281);
            this.UI9bl3_combo.Name = "UI9bl3_combo";
            this.UI9bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI9bl3_combo.TabIndex = 115;
            this.UI9bl3_combo.Visible = false;
            this.UI9bl3_combo.SelectedIndexChanged += new System.EventHandler(this.UI9bl3_combo_SelectedIndexChanged);
            // 
            // UI9bl3_typeCombo
            // 
            this.UI9bl3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI9bl3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI9bl3_typeCombo.Enabled = false;
            this.UI9bl3_typeCombo.FormattingEnabled = true;
            this.UI9bl3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI9bl3_typeCombo.Location = new System.Drawing.Point(439, 281);
            this.UI9bl3_typeCombo.Name = "UI9bl3_typeCombo";
            this.UI9bl3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI9bl3_typeCombo.TabIndex = 116;
            this.UI9bl3_typeCombo.Visible = false;
            this.UI9bl3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI9bl3_typeCombo_SelectedIndexChanged);
            // 
            // UI9bl3_lab
            // 
            this.UI9bl3_lab.AutoSize = true;
            this.UI9bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI9bl3_lab.Location = new System.Drawing.Point(568, 283);
            this.UI9bl3_lab.Name = "UI9bl3_lab";
            this.UI9bl3_lab.Size = new System.Drawing.Size(33, 14);
            this.UI9bl3_lab.TabIndex = 117;
            this.UI9bl3_lab.Text = "UI 9";
            // 
            // UI8_bl3Label
            // 
            this.UI8_bl3Label.AutoSize = true;
            this.UI8_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI8_bl3Label.Location = new System.Drawing.Point(10, 252);
            this.UI8_bl3Label.Name = "UI8_bl3Label";
            this.UI8_bl3Label.Size = new System.Drawing.Size(33, 14);
            this.UI8_bl3Label.TabIndex = 110;
            this.UI8_bl3Label.Text = "UI 8";
            this.UI8_bl3Label.Visible = false;
            // 
            // UI8bl3_combo
            // 
            this.UI8bl3_combo.DisplayMember = "Не выбрано";
            this.UI8bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI8bl3_combo.Enabled = false;
            this.UI8bl3_combo.FormattingEnabled = true;
            this.UI8bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI8bl3_combo.Location = new System.Drawing.Point(53, 250);
            this.UI8bl3_combo.Name = "UI8bl3_combo";
            this.UI8bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI8bl3_combo.TabIndex = 111;
            this.UI8bl3_combo.Visible = false;
            this.UI8bl3_combo.SelectedIndexChanged += new System.EventHandler(this.UI8bl3_combo_SelectedIndexChanged);
            // 
            // UI8bl3_typeCombo
            // 
            this.UI8bl3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI8bl3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI8bl3_typeCombo.Enabled = false;
            this.UI8bl3_typeCombo.FormattingEnabled = true;
            this.UI8bl3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI8bl3_typeCombo.Location = new System.Drawing.Point(439, 250);
            this.UI8bl3_typeCombo.Name = "UI8bl3_typeCombo";
            this.UI8bl3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI8bl3_typeCombo.TabIndex = 112;
            this.UI8bl3_typeCombo.Visible = false;
            this.UI8bl3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI8bl3_typeCombo_SelectedIndexChanged);
            // 
            // UI8bl3_lab
            // 
            this.UI8bl3_lab.AutoSize = true;
            this.UI8bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI8bl3_lab.Location = new System.Drawing.Point(568, 252);
            this.UI8bl3_lab.Name = "UI8bl3_lab";
            this.UI8bl3_lab.Size = new System.Drawing.Size(33, 14);
            this.UI8bl3_lab.TabIndex = 113;
            this.UI8bl3_lab.Text = "UI 8";
            // 
            // UI7_bl3Label
            // 
            this.UI7_bl3Label.AutoSize = true;
            this.UI7_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI7_bl3Label.Location = new System.Drawing.Point(10, 221);
            this.UI7_bl3Label.Name = "UI7_bl3Label";
            this.UI7_bl3Label.Size = new System.Drawing.Size(33, 14);
            this.UI7_bl3Label.TabIndex = 106;
            this.UI7_bl3Label.Text = "UI 7";
            this.UI7_bl3Label.Visible = false;
            // 
            // UI7bl3_combo
            // 
            this.UI7bl3_combo.DisplayMember = "Не выбрано";
            this.UI7bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI7bl3_combo.Enabled = false;
            this.UI7bl3_combo.FormattingEnabled = true;
            this.UI7bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI7bl3_combo.Location = new System.Drawing.Point(53, 219);
            this.UI7bl3_combo.Name = "UI7bl3_combo";
            this.UI7bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI7bl3_combo.TabIndex = 107;
            this.UI7bl3_combo.Visible = false;
            this.UI7bl3_combo.SelectedIndexChanged += new System.EventHandler(this.UI7bl3_combo_SelectedIndexChanged);
            // 
            // UI7bl3_typeCombo
            // 
            this.UI7bl3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI7bl3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI7bl3_typeCombo.Enabled = false;
            this.UI7bl3_typeCombo.FormattingEnabled = true;
            this.UI7bl3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI7bl3_typeCombo.Location = new System.Drawing.Point(439, 219);
            this.UI7bl3_typeCombo.Name = "UI7bl3_typeCombo";
            this.UI7bl3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI7bl3_typeCombo.TabIndex = 108;
            this.UI7bl3_typeCombo.Visible = false;
            this.UI7bl3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI7bl3_typeCombo_SelectedIndexChanged);
            // 
            // UI7bl3_lab
            // 
            this.UI7bl3_lab.AutoSize = true;
            this.UI7bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI7bl3_lab.Location = new System.Drawing.Point(568, 221);
            this.UI7bl3_lab.Name = "UI7bl3_lab";
            this.UI7bl3_lab.Size = new System.Drawing.Size(33, 14);
            this.UI7bl3_lab.TabIndex = 109;
            this.UI7bl3_lab.Text = "UI 7";
            // 
            // UI6_bl3Label
            // 
            this.UI6_bl3Label.AutoSize = true;
            this.UI6_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI6_bl3Label.Location = new System.Drawing.Point(10, 190);
            this.UI6_bl3Label.Name = "UI6_bl3Label";
            this.UI6_bl3Label.Size = new System.Drawing.Size(33, 14);
            this.UI6_bl3Label.TabIndex = 102;
            this.UI6_bl3Label.Text = "UI 6";
            this.UI6_bl3Label.Visible = false;
            // 
            // UI6bl3_combo
            // 
            this.UI6bl3_combo.DisplayMember = "Не выбрано";
            this.UI6bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI6bl3_combo.Enabled = false;
            this.UI6bl3_combo.FormattingEnabled = true;
            this.UI6bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI6bl3_combo.Location = new System.Drawing.Point(53, 188);
            this.UI6bl3_combo.Name = "UI6bl3_combo";
            this.UI6bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI6bl3_combo.TabIndex = 103;
            this.UI6bl3_combo.Visible = false;
            this.UI6bl3_combo.SelectedIndexChanged += new System.EventHandler(this.UI6bl3_combo_SelectedIndexChanged);
            // 
            // UI6bl3_typeCombo
            // 
            this.UI6bl3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI6bl3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI6bl3_typeCombo.Enabled = false;
            this.UI6bl3_typeCombo.FormattingEnabled = true;
            this.UI6bl3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI6bl3_typeCombo.Location = new System.Drawing.Point(439, 188);
            this.UI6bl3_typeCombo.Name = "UI6bl3_typeCombo";
            this.UI6bl3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI6bl3_typeCombo.TabIndex = 104;
            this.UI6bl3_typeCombo.Visible = false;
            this.UI6bl3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI6bl3_typeCombo_SelectedIndexChanged);
            // 
            // UI6bl3_lab
            // 
            this.UI6bl3_lab.AutoSize = true;
            this.UI6bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI6bl3_lab.Location = new System.Drawing.Point(568, 190);
            this.UI6bl3_lab.Name = "UI6bl3_lab";
            this.UI6bl3_lab.Size = new System.Drawing.Size(33, 14);
            this.UI6bl3_lab.TabIndex = 105;
            this.UI6bl3_lab.Text = "UI 6";
            // 
            // UI5_bl3Label
            // 
            this.UI5_bl3Label.AutoSize = true;
            this.UI5_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI5_bl3Label.Location = new System.Drawing.Point(10, 159);
            this.UI5_bl3Label.Name = "UI5_bl3Label";
            this.UI5_bl3Label.Size = new System.Drawing.Size(33, 14);
            this.UI5_bl3Label.TabIndex = 98;
            this.UI5_bl3Label.Text = "UI 5";
            this.UI5_bl3Label.Visible = false;
            // 
            // UI5bl3_combo
            // 
            this.UI5bl3_combo.DisplayMember = "Не выбрано";
            this.UI5bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI5bl3_combo.Enabled = false;
            this.UI5bl3_combo.FormattingEnabled = true;
            this.UI5bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI5bl3_combo.Location = new System.Drawing.Point(53, 157);
            this.UI5bl3_combo.Name = "UI5bl3_combo";
            this.UI5bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI5bl3_combo.TabIndex = 99;
            this.UI5bl3_combo.Visible = false;
            this.UI5bl3_combo.SelectedIndexChanged += new System.EventHandler(this.UI5bl3_combo_SelectedIndexChanged);
            // 
            // UI5bl3_typeCombo
            // 
            this.UI5bl3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI5bl3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI5bl3_typeCombo.Enabled = false;
            this.UI5bl3_typeCombo.FormattingEnabled = true;
            this.UI5bl3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI5bl3_typeCombo.Location = new System.Drawing.Point(439, 157);
            this.UI5bl3_typeCombo.Name = "UI5bl3_typeCombo";
            this.UI5bl3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI5bl3_typeCombo.TabIndex = 100;
            this.UI5bl3_typeCombo.Visible = false;
            this.UI5bl3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI5bl3_typeCombo_SelectedIndexChanged);
            // 
            // UI5bl3_lab
            // 
            this.UI5bl3_lab.AutoSize = true;
            this.UI5bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI5bl3_lab.Location = new System.Drawing.Point(568, 159);
            this.UI5bl3_lab.Name = "UI5bl3_lab";
            this.UI5bl3_lab.Size = new System.Drawing.Size(33, 14);
            this.UI5bl3_lab.TabIndex = 101;
            this.UI5bl3_lab.Text = "UI 5";
            // 
            // UI4_bl3Label
            // 
            this.UI4_bl3Label.AutoSize = true;
            this.UI4_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI4_bl3Label.Location = new System.Drawing.Point(10, 128);
            this.UI4_bl3Label.Name = "UI4_bl3Label";
            this.UI4_bl3Label.Size = new System.Drawing.Size(33, 14);
            this.UI4_bl3Label.TabIndex = 94;
            this.UI4_bl3Label.Text = "UI 4";
            this.UI4_bl3Label.Visible = false;
            // 
            // UI4bl3_combo
            // 
            this.UI4bl3_combo.DisplayMember = "Не выбрано";
            this.UI4bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI4bl3_combo.Enabled = false;
            this.UI4bl3_combo.FormattingEnabled = true;
            this.UI4bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI4bl3_combo.Location = new System.Drawing.Point(53, 126);
            this.UI4bl3_combo.Name = "UI4bl3_combo";
            this.UI4bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI4bl3_combo.TabIndex = 95;
            this.UI4bl3_combo.Visible = false;
            this.UI4bl3_combo.SelectedIndexChanged += new System.EventHandler(this.UI4bl3_combo_SelectedIndexChanged);
            // 
            // UI4bl3_typeCombo
            // 
            this.UI4bl3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI4bl3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI4bl3_typeCombo.Enabled = false;
            this.UI4bl3_typeCombo.FormattingEnabled = true;
            this.UI4bl3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI4bl3_typeCombo.Location = new System.Drawing.Point(439, 126);
            this.UI4bl3_typeCombo.Name = "UI4bl3_typeCombo";
            this.UI4bl3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI4bl3_typeCombo.TabIndex = 96;
            this.UI4bl3_typeCombo.Visible = false;
            this.UI4bl3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI4bl3_typeCombo_SelectedIndexChanged);
            // 
            // UI4bl3_lab
            // 
            this.UI4bl3_lab.AutoSize = true;
            this.UI4bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI4bl3_lab.Location = new System.Drawing.Point(568, 128);
            this.UI4bl3_lab.Name = "UI4bl3_lab";
            this.UI4bl3_lab.Size = new System.Drawing.Size(33, 14);
            this.UI4bl3_lab.TabIndex = 97;
            this.UI4bl3_lab.Text = "UI 4";
            // 
            // UI3_bl3Label
            // 
            this.UI3_bl3Label.AutoSize = true;
            this.UI3_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI3_bl3Label.Location = new System.Drawing.Point(10, 97);
            this.UI3_bl3Label.Name = "UI3_bl3Label";
            this.UI3_bl3Label.Size = new System.Drawing.Size(33, 14);
            this.UI3_bl3Label.TabIndex = 90;
            this.UI3_bl3Label.Text = "UI 3";
            this.UI3_bl3Label.Visible = false;
            // 
            // UI3bl3_combo
            // 
            this.UI3bl3_combo.DisplayMember = "Не выбрано";
            this.UI3bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI3bl3_combo.Enabled = false;
            this.UI3bl3_combo.FormattingEnabled = true;
            this.UI3bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI3bl3_combo.Location = new System.Drawing.Point(53, 95);
            this.UI3bl3_combo.Name = "UI3bl3_combo";
            this.UI3bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI3bl3_combo.TabIndex = 91;
            this.UI3bl3_combo.Visible = false;
            this.UI3bl3_combo.SelectedIndexChanged += new System.EventHandler(this.UI3bl3_combo_SelectedIndexChanged);
            // 
            // UI3bl3_typeCombo
            // 
            this.UI3bl3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI3bl3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI3bl3_typeCombo.Enabled = false;
            this.UI3bl3_typeCombo.FormattingEnabled = true;
            this.UI3bl3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI3bl3_typeCombo.Location = new System.Drawing.Point(439, 95);
            this.UI3bl3_typeCombo.Name = "UI3bl3_typeCombo";
            this.UI3bl3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI3bl3_typeCombo.TabIndex = 92;
            this.UI3bl3_typeCombo.Visible = false;
            this.UI3bl3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI3bl3_typeCombo_SelectedIndexChanged);
            // 
            // UI3bl3_lab
            // 
            this.UI3bl3_lab.AutoSize = true;
            this.UI3bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI3bl3_lab.Location = new System.Drawing.Point(568, 97);
            this.UI3bl3_lab.Name = "UI3bl3_lab";
            this.UI3bl3_lab.Size = new System.Drawing.Size(33, 14);
            this.UI3bl3_lab.TabIndex = 93;
            this.UI3bl3_lab.Text = "UI 3";
            // 
            // UI2_bl3Label
            // 
            this.UI2_bl3Label.AutoSize = true;
            this.UI2_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI2_bl3Label.Location = new System.Drawing.Point(10, 66);
            this.UI2_bl3Label.Name = "UI2_bl3Label";
            this.UI2_bl3Label.Size = new System.Drawing.Size(33, 14);
            this.UI2_bl3Label.TabIndex = 86;
            this.UI2_bl3Label.Text = "UI 2";
            this.UI2_bl3Label.Visible = false;
            // 
            // UI2bl3_combo
            // 
            this.UI2bl3_combo.DisplayMember = "Не выбрано";
            this.UI2bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI2bl3_combo.Enabled = false;
            this.UI2bl3_combo.FormattingEnabled = true;
            this.UI2bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI2bl3_combo.Location = new System.Drawing.Point(53, 64);
            this.UI2bl3_combo.Name = "UI2bl3_combo";
            this.UI2bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI2bl3_combo.TabIndex = 87;
            this.UI2bl3_combo.Visible = false;
            this.UI2bl3_combo.SelectedIndexChanged += new System.EventHandler(this.UI2bl3_combo_SelectedIndexChanged);
            // 
            // UI2bl3_typeCombo
            // 
            this.UI2bl3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI2bl3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI2bl3_typeCombo.Enabled = false;
            this.UI2bl3_typeCombo.FormattingEnabled = true;
            this.UI2bl3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI2bl3_typeCombo.Location = new System.Drawing.Point(439, 64);
            this.UI2bl3_typeCombo.Name = "UI2bl3_typeCombo";
            this.UI2bl3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI2bl3_typeCombo.TabIndex = 88;
            this.UI2bl3_typeCombo.Visible = false;
            this.UI2bl3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI2bl3_typeCombo_SelectedIndexChanged);
            // 
            // UI2bl3_lab
            // 
            this.UI2bl3_lab.AutoSize = true;
            this.UI2bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI2bl3_lab.Location = new System.Drawing.Point(568, 66);
            this.UI2bl3_lab.Name = "UI2bl3_lab";
            this.UI2bl3_lab.Size = new System.Drawing.Size(33, 14);
            this.UI2bl3_lab.TabIndex = 89;
            this.UI2bl3_lab.Text = "UI 2";
            // 
            // UI1_bl3Label
            // 
            this.UI1_bl3Label.AutoSize = true;
            this.UI1_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI1_bl3Label.Location = new System.Drawing.Point(10, 35);
            this.UI1_bl3Label.Name = "UI1_bl3Label";
            this.UI1_bl3Label.Size = new System.Drawing.Size(33, 14);
            this.UI1_bl3Label.TabIndex = 82;
            this.UI1_bl3Label.Text = "UI 1";
            this.UI1_bl3Label.Visible = false;
            // 
            // UI1bl3_combo
            // 
            this.UI1bl3_combo.DisplayMember = "Не выбрано";
            this.UI1bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI1bl3_combo.Enabled = false;
            this.UI1bl3_combo.FormattingEnabled = true;
            this.UI1bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI1bl3_combo.Location = new System.Drawing.Point(53, 33);
            this.UI1bl3_combo.Name = "UI1bl3_combo";
            this.UI1bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI1bl3_combo.TabIndex = 83;
            this.UI1bl3_combo.Visible = false;
            this.UI1bl3_combo.SelectedIndexChanged += new System.EventHandler(this.UI1bl3_combo_SelectedIndexChanged);
            // 
            // UIblock3_header
            // 
            this.UIblock3_header.AutoSize = true;
            this.UIblock3_header.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UIblock3_header.Location = new System.Drawing.Point(8, 4);
            this.UIblock3_header.Name = "UIblock3_header";
            this.UIblock3_header.Size = new System.Drawing.Size(142, 14);
            this.UIblock3_header.TabIndex = 82;
            this.UIblock3_header.Text = "Блок расширения 3";
            // 
            // UI1bl3_typeCombo
            // 
            this.UI1bl3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI1bl3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI1bl3_typeCombo.Enabled = false;
            this.UI1bl3_typeCombo.FormattingEnabled = true;
            this.UI1bl3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI1bl3_typeCombo.Location = new System.Drawing.Point(439, 33);
            this.UI1bl3_typeCombo.Name = "UI1bl3_typeCombo";
            this.UI1bl3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI1bl3_typeCombo.TabIndex = 84;
            this.UI1bl3_typeCombo.Visible = false;
            this.UI1bl3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI1bl3_typeCombo_SelectedIndexChanged);
            // 
            // UI1bl3_lab
            // 
            this.UI1bl3_lab.AutoSize = true;
            this.UI1bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI1bl3_lab.Location = new System.Drawing.Point(568, 35);
            this.UI1bl3_lab.Name = "UI1bl3_lab";
            this.UI1bl3_lab.Size = new System.Drawing.Size(33, 14);
            this.UI1bl3_lab.TabIndex = 85;
            this.UI1bl3_lab.Text = "UI 1";
            // 
            // block2_UIpanel
            // 
            this.block2_UIpanel.Controls.Add(this.UI16_bl2Label);
            this.block2_UIpanel.Controls.Add(this.UI16bl2_combo);
            this.block2_UIpanel.Controls.Add(this.UI16bl2_typeCombo);
            this.block2_UIpanel.Controls.Add(this.UI16bl2_lab);
            this.block2_UIpanel.Controls.Add(this.UI15_bl2Label);
            this.block2_UIpanel.Controls.Add(this.UI15bl2_combo);
            this.block2_UIpanel.Controls.Add(this.UI15bl2_typeCombo);
            this.block2_UIpanel.Controls.Add(this.UI15bl2_lab);
            this.block2_UIpanel.Controls.Add(this.UI14_bl2Label);
            this.block2_UIpanel.Controls.Add(this.UI14bl2_combo);
            this.block2_UIpanel.Controls.Add(this.UI14bl2_typeCombo);
            this.block2_UIpanel.Controls.Add(this.UI14bl2_lab);
            this.block2_UIpanel.Controls.Add(this.UI13_bl2Label);
            this.block2_UIpanel.Controls.Add(this.UI13bl2_combo);
            this.block2_UIpanel.Controls.Add(this.UI13bl2_typeCombo);
            this.block2_UIpanel.Controls.Add(this.UI13bl2_lab);
            this.block2_UIpanel.Controls.Add(this.UI12_bl2Label);
            this.block2_UIpanel.Controls.Add(this.UI12bl2_combo);
            this.block2_UIpanel.Controls.Add(this.UI12bl2_typeCombo);
            this.block2_UIpanel.Controls.Add(this.UI12bl2_lab);
            this.block2_UIpanel.Controls.Add(this.UI11_bl2Label);
            this.block2_UIpanel.Controls.Add(this.UI11bl2_combo);
            this.block2_UIpanel.Controls.Add(this.UI11bl2_typeCombo);
            this.block2_UIpanel.Controls.Add(this.UI11bl2_lab);
            this.block2_UIpanel.Controls.Add(this.UI10_bl2Label);
            this.block2_UIpanel.Controls.Add(this.UI10bl2_combo);
            this.block2_UIpanel.Controls.Add(this.UI10bl2_typeCombo);
            this.block2_UIpanel.Controls.Add(this.UI10bl2_lab);
            this.block2_UIpanel.Controls.Add(this.UI9_bl2Label);
            this.block2_UIpanel.Controls.Add(this.UI9bl2_combo);
            this.block2_UIpanel.Controls.Add(this.UI9bl2_typeCombo);
            this.block2_UIpanel.Controls.Add(this.UI9bl2_lab);
            this.block2_UIpanel.Controls.Add(this.UI8_bl2Label);
            this.block2_UIpanel.Controls.Add(this.UI8bl2_combo);
            this.block2_UIpanel.Controls.Add(this.UI8bl2_typeCombo);
            this.block2_UIpanel.Controls.Add(this.UI8bl2_lab);
            this.block2_UIpanel.Controls.Add(this.UI7_bl2Label);
            this.block2_UIpanel.Controls.Add(this.UI7bl2_combo);
            this.block2_UIpanel.Controls.Add(this.UI7bl2_typeCombo);
            this.block2_UIpanel.Controls.Add(this.UI7bl2_lab);
            this.block2_UIpanel.Controls.Add(this.UI6_bl2Label);
            this.block2_UIpanel.Controls.Add(this.UI6bl2_combo);
            this.block2_UIpanel.Controls.Add(this.UI6bl2_typeCombo);
            this.block2_UIpanel.Controls.Add(this.UI6bl2_lab);
            this.block2_UIpanel.Controls.Add(this.UI5_bl2Label);
            this.block2_UIpanel.Controls.Add(this.UI5bl2_combo);
            this.block2_UIpanel.Controls.Add(this.UI5bl2_typeCombo);
            this.block2_UIpanel.Controls.Add(this.UI5bl2_lab);
            this.block2_UIpanel.Controls.Add(this.UI4_bl2Label);
            this.block2_UIpanel.Controls.Add(this.UI4bl2_combo);
            this.block2_UIpanel.Controls.Add(this.UI4bl2_typeCombo);
            this.block2_UIpanel.Controls.Add(this.UI4bl2_lab);
            this.block2_UIpanel.Controls.Add(this.UI3_bl2Label);
            this.block2_UIpanel.Controls.Add(this.UI3bl2_combo);
            this.block2_UIpanel.Controls.Add(this.UI3bl2_typeCombo);
            this.block2_UIpanel.Controls.Add(this.UI3bl2_lab);
            this.block2_UIpanel.Controls.Add(this.UI2_bl2Label);
            this.block2_UIpanel.Controls.Add(this.UI2bl2_combo);
            this.block2_UIpanel.Controls.Add(this.UI2bl2_typeCombo);
            this.block2_UIpanel.Controls.Add(this.UI2bl2_lab);
            this.block2_UIpanel.Controls.Add(this.UI1_bl2Label);
            this.block2_UIpanel.Controls.Add(this.UI1bl2_combo);
            this.block2_UIpanel.Controls.Add(this.UIblock2_header);
            this.block2_UIpanel.Controls.Add(this.UI1bl2_typeCombo);
            this.block2_UIpanel.Controls.Add(this.UI1bl2_lab);
            this.block2_UIpanel.Enabled = false;
            this.block2_UIpanel.Location = new System.Drawing.Point(6, 932);
            this.block2_UIpanel.Name = "block2_UIpanel";
            this.block2_UIpanel.Size = new System.Drawing.Size(637, 537);
            this.block2_UIpanel.TabIndex = 146;
            this.block2_UIpanel.Visible = false;
            // 
            // UI16_bl2Label
            // 
            this.UI16_bl2Label.AutoSize = true;
            this.UI16_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI16_bl2Label.Location = new System.Drawing.Point(10, 500);
            this.UI16_bl2Label.Name = "UI16_bl2Label";
            this.UI16_bl2Label.Size = new System.Drawing.Size(41, 14);
            this.UI16_bl2Label.TabIndex = 142;
            this.UI16_bl2Label.Text = "UI 16";
            this.UI16_bl2Label.Visible = false;
            // 
            // UI16bl2_combo
            // 
            this.UI16bl2_combo.DisplayMember = "Не выбрано";
            this.UI16bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI16bl2_combo.Enabled = false;
            this.UI16bl2_combo.FormattingEnabled = true;
            this.UI16bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI16bl2_combo.Location = new System.Drawing.Point(53, 498);
            this.UI16bl2_combo.Name = "UI16bl2_combo";
            this.UI16bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI16bl2_combo.TabIndex = 143;
            this.UI16bl2_combo.Visible = false;
            this.UI16bl2_combo.SelectedIndexChanged += new System.EventHandler(this.UI16bl2_combo_SelectedIndexChanged);
            // 
            // UI16bl2_typeCombo
            // 
            this.UI16bl2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI16bl2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI16bl2_typeCombo.Enabled = false;
            this.UI16bl2_typeCombo.FormattingEnabled = true;
            this.UI16bl2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI16bl2_typeCombo.Location = new System.Drawing.Point(439, 498);
            this.UI16bl2_typeCombo.Name = "UI16bl2_typeCombo";
            this.UI16bl2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI16bl2_typeCombo.TabIndex = 144;
            this.UI16bl2_typeCombo.Visible = false;
            this.UI16bl2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI16bl2_typeCombo_SelectedIndexChanged);
            // 
            // UI16bl2_lab
            // 
            this.UI16bl2_lab.AutoSize = true;
            this.UI16bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI16bl2_lab.Location = new System.Drawing.Point(568, 500);
            this.UI16bl2_lab.Name = "UI16bl2_lab";
            this.UI16bl2_lab.Size = new System.Drawing.Size(41, 14);
            this.UI16bl2_lab.TabIndex = 145;
            this.UI16bl2_lab.Text = "UI 16";
            // 
            // UI15_bl2Label
            // 
            this.UI15_bl2Label.AutoSize = true;
            this.UI15_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI15_bl2Label.Location = new System.Drawing.Point(10, 469);
            this.UI15_bl2Label.Name = "UI15_bl2Label";
            this.UI15_bl2Label.Size = new System.Drawing.Size(41, 14);
            this.UI15_bl2Label.TabIndex = 138;
            this.UI15_bl2Label.Text = "UI 15";
            this.UI15_bl2Label.Visible = false;
            // 
            // UI15bl2_combo
            // 
            this.UI15bl2_combo.DisplayMember = "Не выбрано";
            this.UI15bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI15bl2_combo.Enabled = false;
            this.UI15bl2_combo.FormattingEnabled = true;
            this.UI15bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI15bl2_combo.Location = new System.Drawing.Point(53, 467);
            this.UI15bl2_combo.Name = "UI15bl2_combo";
            this.UI15bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI15bl2_combo.TabIndex = 139;
            this.UI15bl2_combo.Visible = false;
            this.UI15bl2_combo.SelectedIndexChanged += new System.EventHandler(this.UI15bl2_combo_SelectedIndexChanged);
            // 
            // UI15bl2_typeCombo
            // 
            this.UI15bl2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI15bl2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI15bl2_typeCombo.Enabled = false;
            this.UI15bl2_typeCombo.FormattingEnabled = true;
            this.UI15bl2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI15bl2_typeCombo.Location = new System.Drawing.Point(439, 467);
            this.UI15bl2_typeCombo.Name = "UI15bl2_typeCombo";
            this.UI15bl2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI15bl2_typeCombo.TabIndex = 140;
            this.UI15bl2_typeCombo.Visible = false;
            this.UI15bl2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI15bl2_typeCombo_SelectedIndexChanged);
            // 
            // UI15bl2_lab
            // 
            this.UI15bl2_lab.AutoSize = true;
            this.UI15bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI15bl2_lab.Location = new System.Drawing.Point(568, 469);
            this.UI15bl2_lab.Name = "UI15bl2_lab";
            this.UI15bl2_lab.Size = new System.Drawing.Size(41, 14);
            this.UI15bl2_lab.TabIndex = 141;
            this.UI15bl2_lab.Text = "UI 15";
            // 
            // UI14_bl2Label
            // 
            this.UI14_bl2Label.AutoSize = true;
            this.UI14_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI14_bl2Label.Location = new System.Drawing.Point(10, 438);
            this.UI14_bl2Label.Name = "UI14_bl2Label";
            this.UI14_bl2Label.Size = new System.Drawing.Size(41, 14);
            this.UI14_bl2Label.TabIndex = 134;
            this.UI14_bl2Label.Text = "UI 14";
            this.UI14_bl2Label.Visible = false;
            // 
            // UI14bl2_combo
            // 
            this.UI14bl2_combo.DisplayMember = "Не выбрано";
            this.UI14bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI14bl2_combo.Enabled = false;
            this.UI14bl2_combo.FormattingEnabled = true;
            this.UI14bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI14bl2_combo.Location = new System.Drawing.Point(53, 436);
            this.UI14bl2_combo.Name = "UI14bl2_combo";
            this.UI14bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI14bl2_combo.TabIndex = 135;
            this.UI14bl2_combo.Visible = false;
            this.UI14bl2_combo.SelectedIndexChanged += new System.EventHandler(this.UI14bl2_combo_SelectedIndexChanged);
            // 
            // UI14bl2_typeCombo
            // 
            this.UI14bl2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI14bl2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI14bl2_typeCombo.Enabled = false;
            this.UI14bl2_typeCombo.FormattingEnabled = true;
            this.UI14bl2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI14bl2_typeCombo.Location = new System.Drawing.Point(439, 436);
            this.UI14bl2_typeCombo.Name = "UI14bl2_typeCombo";
            this.UI14bl2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI14bl2_typeCombo.TabIndex = 136;
            this.UI14bl2_typeCombo.Visible = false;
            this.UI14bl2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI14bl2_typeCombo_SelectedIndexChanged);
            // 
            // UI14bl2_lab
            // 
            this.UI14bl2_lab.AutoSize = true;
            this.UI14bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI14bl2_lab.Location = new System.Drawing.Point(568, 438);
            this.UI14bl2_lab.Name = "UI14bl2_lab";
            this.UI14bl2_lab.Size = new System.Drawing.Size(41, 14);
            this.UI14bl2_lab.TabIndex = 137;
            this.UI14bl2_lab.Text = "UI 14";
            // 
            // UI13_bl2Label
            // 
            this.UI13_bl2Label.AutoSize = true;
            this.UI13_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI13_bl2Label.Location = new System.Drawing.Point(10, 407);
            this.UI13_bl2Label.Name = "UI13_bl2Label";
            this.UI13_bl2Label.Size = new System.Drawing.Size(41, 14);
            this.UI13_bl2Label.TabIndex = 130;
            this.UI13_bl2Label.Text = "UI 13";
            this.UI13_bl2Label.Visible = false;
            // 
            // UI13bl2_combo
            // 
            this.UI13bl2_combo.DisplayMember = "Не выбрано";
            this.UI13bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI13bl2_combo.Enabled = false;
            this.UI13bl2_combo.FormattingEnabled = true;
            this.UI13bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI13bl2_combo.Location = new System.Drawing.Point(53, 405);
            this.UI13bl2_combo.Name = "UI13bl2_combo";
            this.UI13bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI13bl2_combo.TabIndex = 131;
            this.UI13bl2_combo.Visible = false;
            this.UI13bl2_combo.SelectedIndexChanged += new System.EventHandler(this.UI13bl2_combo_SelectedIndexChanged);
            // 
            // UI13bl2_typeCombo
            // 
            this.UI13bl2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI13bl2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI13bl2_typeCombo.Enabled = false;
            this.UI13bl2_typeCombo.FormattingEnabled = true;
            this.UI13bl2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI13bl2_typeCombo.Location = new System.Drawing.Point(439, 405);
            this.UI13bl2_typeCombo.Name = "UI13bl2_typeCombo";
            this.UI13bl2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI13bl2_typeCombo.TabIndex = 132;
            this.UI13bl2_typeCombo.Visible = false;
            this.UI13bl2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI13bl2_typeCombo_SelectedIndexChanged);
            // 
            // UI13bl2_lab
            // 
            this.UI13bl2_lab.AutoSize = true;
            this.UI13bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI13bl2_lab.Location = new System.Drawing.Point(568, 407);
            this.UI13bl2_lab.Name = "UI13bl2_lab";
            this.UI13bl2_lab.Size = new System.Drawing.Size(41, 14);
            this.UI13bl2_lab.TabIndex = 133;
            this.UI13bl2_lab.Text = "UI 13";
            // 
            // UI12_bl2Label
            // 
            this.UI12_bl2Label.AutoSize = true;
            this.UI12_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI12_bl2Label.Location = new System.Drawing.Point(10, 376);
            this.UI12_bl2Label.Name = "UI12_bl2Label";
            this.UI12_bl2Label.Size = new System.Drawing.Size(41, 14);
            this.UI12_bl2Label.TabIndex = 126;
            this.UI12_bl2Label.Text = "UI 12";
            this.UI12_bl2Label.Visible = false;
            // 
            // UI12bl2_combo
            // 
            this.UI12bl2_combo.DisplayMember = "Не выбрано";
            this.UI12bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI12bl2_combo.Enabled = false;
            this.UI12bl2_combo.FormattingEnabled = true;
            this.UI12bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI12bl2_combo.Location = new System.Drawing.Point(53, 374);
            this.UI12bl2_combo.Name = "UI12bl2_combo";
            this.UI12bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI12bl2_combo.TabIndex = 127;
            this.UI12bl2_combo.Visible = false;
            this.UI12bl2_combo.SelectedIndexChanged += new System.EventHandler(this.UI12bl2_combo_SelectedIndexChanged);
            // 
            // UI12bl2_typeCombo
            // 
            this.UI12bl2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI12bl2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI12bl2_typeCombo.Enabled = false;
            this.UI12bl2_typeCombo.FormattingEnabled = true;
            this.UI12bl2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI12bl2_typeCombo.Location = new System.Drawing.Point(439, 374);
            this.UI12bl2_typeCombo.Name = "UI12bl2_typeCombo";
            this.UI12bl2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI12bl2_typeCombo.TabIndex = 128;
            this.UI12bl2_typeCombo.Visible = false;
            this.UI12bl2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI12bl2_typeCombo_SelectedIndexChanged);
            // 
            // UI12bl2_lab
            // 
            this.UI12bl2_lab.AutoSize = true;
            this.UI12bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI12bl2_lab.Location = new System.Drawing.Point(568, 376);
            this.UI12bl2_lab.Name = "UI12bl2_lab";
            this.UI12bl2_lab.Size = new System.Drawing.Size(41, 14);
            this.UI12bl2_lab.TabIndex = 129;
            this.UI12bl2_lab.Text = "UI 12";
            // 
            // UI11_bl2Label
            // 
            this.UI11_bl2Label.AutoSize = true;
            this.UI11_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI11_bl2Label.Location = new System.Drawing.Point(10, 345);
            this.UI11_bl2Label.Name = "UI11_bl2Label";
            this.UI11_bl2Label.Size = new System.Drawing.Size(41, 14);
            this.UI11_bl2Label.TabIndex = 122;
            this.UI11_bl2Label.Text = "UI 11";
            this.UI11_bl2Label.Visible = false;
            // 
            // UI11bl2_combo
            // 
            this.UI11bl2_combo.DisplayMember = "Не выбрано";
            this.UI11bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI11bl2_combo.Enabled = false;
            this.UI11bl2_combo.FormattingEnabled = true;
            this.UI11bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI11bl2_combo.Location = new System.Drawing.Point(53, 343);
            this.UI11bl2_combo.Name = "UI11bl2_combo";
            this.UI11bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI11bl2_combo.TabIndex = 123;
            this.UI11bl2_combo.Visible = false;
            this.UI11bl2_combo.SelectedIndexChanged += new System.EventHandler(this.UI11bl2_combo_SelectedIndexChanged);
            // 
            // UI11bl2_typeCombo
            // 
            this.UI11bl2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI11bl2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI11bl2_typeCombo.Enabled = false;
            this.UI11bl2_typeCombo.FormattingEnabled = true;
            this.UI11bl2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI11bl2_typeCombo.Location = new System.Drawing.Point(439, 343);
            this.UI11bl2_typeCombo.Name = "UI11bl2_typeCombo";
            this.UI11bl2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI11bl2_typeCombo.TabIndex = 124;
            this.UI11bl2_typeCombo.Visible = false;
            this.UI11bl2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI11bl2_typeCombo_SelectedIndexChanged);
            // 
            // UI11bl2_lab
            // 
            this.UI11bl2_lab.AutoSize = true;
            this.UI11bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI11bl2_lab.Location = new System.Drawing.Point(568, 345);
            this.UI11bl2_lab.Name = "UI11bl2_lab";
            this.UI11bl2_lab.Size = new System.Drawing.Size(41, 14);
            this.UI11bl2_lab.TabIndex = 125;
            this.UI11bl2_lab.Text = "UI 11";
            // 
            // UI10_bl2Label
            // 
            this.UI10_bl2Label.AutoSize = true;
            this.UI10_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI10_bl2Label.Location = new System.Drawing.Point(10, 314);
            this.UI10_bl2Label.Name = "UI10_bl2Label";
            this.UI10_bl2Label.Size = new System.Drawing.Size(41, 14);
            this.UI10_bl2Label.TabIndex = 118;
            this.UI10_bl2Label.Text = "UI 10";
            this.UI10_bl2Label.Visible = false;
            // 
            // UI10bl2_combo
            // 
            this.UI10bl2_combo.DisplayMember = "Не выбрано";
            this.UI10bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI10bl2_combo.Enabled = false;
            this.UI10bl2_combo.FormattingEnabled = true;
            this.UI10bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI10bl2_combo.Location = new System.Drawing.Point(53, 312);
            this.UI10bl2_combo.Name = "UI10bl2_combo";
            this.UI10bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI10bl2_combo.TabIndex = 119;
            this.UI10bl2_combo.Visible = false;
            this.UI10bl2_combo.SelectedIndexChanged += new System.EventHandler(this.UI10bl2_combo_SelectedIndexChanged);
            // 
            // UI10bl2_typeCombo
            // 
            this.UI10bl2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI10bl2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI10bl2_typeCombo.Enabled = false;
            this.UI10bl2_typeCombo.FormattingEnabled = true;
            this.UI10bl2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI10bl2_typeCombo.Location = new System.Drawing.Point(439, 312);
            this.UI10bl2_typeCombo.Name = "UI10bl2_typeCombo";
            this.UI10bl2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI10bl2_typeCombo.TabIndex = 120;
            this.UI10bl2_typeCombo.Visible = false;
            this.UI10bl2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI10bl2_typeCombo_SelectedIndexChanged);
            // 
            // UI10bl2_lab
            // 
            this.UI10bl2_lab.AutoSize = true;
            this.UI10bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI10bl2_lab.Location = new System.Drawing.Point(568, 314);
            this.UI10bl2_lab.Name = "UI10bl2_lab";
            this.UI10bl2_lab.Size = new System.Drawing.Size(41, 14);
            this.UI10bl2_lab.TabIndex = 121;
            this.UI10bl2_lab.Text = "UI 10";
            // 
            // UI9_bl2Label
            // 
            this.UI9_bl2Label.AutoSize = true;
            this.UI9_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI9_bl2Label.Location = new System.Drawing.Point(10, 283);
            this.UI9_bl2Label.Name = "UI9_bl2Label";
            this.UI9_bl2Label.Size = new System.Drawing.Size(33, 14);
            this.UI9_bl2Label.TabIndex = 114;
            this.UI9_bl2Label.Text = "UI 9";
            this.UI9_bl2Label.Visible = false;
            // 
            // UI9bl2_combo
            // 
            this.UI9bl2_combo.DisplayMember = "Не выбрано";
            this.UI9bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI9bl2_combo.Enabled = false;
            this.UI9bl2_combo.FormattingEnabled = true;
            this.UI9bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI9bl2_combo.Location = new System.Drawing.Point(53, 281);
            this.UI9bl2_combo.Name = "UI9bl2_combo";
            this.UI9bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI9bl2_combo.TabIndex = 115;
            this.UI9bl2_combo.Visible = false;
            this.UI9bl2_combo.SelectedIndexChanged += new System.EventHandler(this.UI9bl2_combo_SelectedIndexChanged);
            // 
            // UI9bl2_typeCombo
            // 
            this.UI9bl2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI9bl2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI9bl2_typeCombo.Enabled = false;
            this.UI9bl2_typeCombo.FormattingEnabled = true;
            this.UI9bl2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI9bl2_typeCombo.Location = new System.Drawing.Point(439, 281);
            this.UI9bl2_typeCombo.Name = "UI9bl2_typeCombo";
            this.UI9bl2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI9bl2_typeCombo.TabIndex = 116;
            this.UI9bl2_typeCombo.Visible = false;
            this.UI9bl2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI9bl2_typeCombo_SelectedIndexChanged);
            // 
            // UI9bl2_lab
            // 
            this.UI9bl2_lab.AutoSize = true;
            this.UI9bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI9bl2_lab.Location = new System.Drawing.Point(568, 283);
            this.UI9bl2_lab.Name = "UI9bl2_lab";
            this.UI9bl2_lab.Size = new System.Drawing.Size(33, 14);
            this.UI9bl2_lab.TabIndex = 117;
            this.UI9bl2_lab.Text = "UI 9";
            // 
            // UI8_bl2Label
            // 
            this.UI8_bl2Label.AutoSize = true;
            this.UI8_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI8_bl2Label.Location = new System.Drawing.Point(10, 252);
            this.UI8_bl2Label.Name = "UI8_bl2Label";
            this.UI8_bl2Label.Size = new System.Drawing.Size(33, 14);
            this.UI8_bl2Label.TabIndex = 110;
            this.UI8_bl2Label.Text = "UI 8";
            this.UI8_bl2Label.Visible = false;
            // 
            // UI8bl2_combo
            // 
            this.UI8bl2_combo.DisplayMember = "Не выбрано";
            this.UI8bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI8bl2_combo.Enabled = false;
            this.UI8bl2_combo.FormattingEnabled = true;
            this.UI8bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI8bl2_combo.Location = new System.Drawing.Point(53, 250);
            this.UI8bl2_combo.Name = "UI8bl2_combo";
            this.UI8bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI8bl2_combo.TabIndex = 111;
            this.UI8bl2_combo.Visible = false;
            this.UI8bl2_combo.SelectedIndexChanged += new System.EventHandler(this.UI8bl2_combo_SelectedIndexChanged);
            // 
            // UI8bl2_typeCombo
            // 
            this.UI8bl2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI8bl2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI8bl2_typeCombo.Enabled = false;
            this.UI8bl2_typeCombo.FormattingEnabled = true;
            this.UI8bl2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI8bl2_typeCombo.Location = new System.Drawing.Point(439, 250);
            this.UI8bl2_typeCombo.Name = "UI8bl2_typeCombo";
            this.UI8bl2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI8bl2_typeCombo.TabIndex = 112;
            this.UI8bl2_typeCombo.Visible = false;
            this.UI8bl2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI8bl2_typeCombo_SelectedIndexChanged);
            // 
            // UI8bl2_lab
            // 
            this.UI8bl2_lab.AutoSize = true;
            this.UI8bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI8bl2_lab.Location = new System.Drawing.Point(568, 252);
            this.UI8bl2_lab.Name = "UI8bl2_lab";
            this.UI8bl2_lab.Size = new System.Drawing.Size(33, 14);
            this.UI8bl2_lab.TabIndex = 113;
            this.UI8bl2_lab.Text = "UI 8";
            // 
            // UI7_bl2Label
            // 
            this.UI7_bl2Label.AutoSize = true;
            this.UI7_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI7_bl2Label.Location = new System.Drawing.Point(10, 221);
            this.UI7_bl2Label.Name = "UI7_bl2Label";
            this.UI7_bl2Label.Size = new System.Drawing.Size(33, 14);
            this.UI7_bl2Label.TabIndex = 106;
            this.UI7_bl2Label.Text = "UI 7";
            this.UI7_bl2Label.Visible = false;
            // 
            // UI7bl2_combo
            // 
            this.UI7bl2_combo.DisplayMember = "Не выбрано";
            this.UI7bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI7bl2_combo.Enabled = false;
            this.UI7bl2_combo.FormattingEnabled = true;
            this.UI7bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI7bl2_combo.Location = new System.Drawing.Point(53, 219);
            this.UI7bl2_combo.Name = "UI7bl2_combo";
            this.UI7bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI7bl2_combo.TabIndex = 107;
            this.UI7bl2_combo.Visible = false;
            this.UI7bl2_combo.SelectedIndexChanged += new System.EventHandler(this.UI7bl2_combo_SelectedIndexChanged);
            // 
            // UI7bl2_typeCombo
            // 
            this.UI7bl2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI7bl2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI7bl2_typeCombo.Enabled = false;
            this.UI7bl2_typeCombo.FormattingEnabled = true;
            this.UI7bl2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI7bl2_typeCombo.Location = new System.Drawing.Point(439, 219);
            this.UI7bl2_typeCombo.Name = "UI7bl2_typeCombo";
            this.UI7bl2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI7bl2_typeCombo.TabIndex = 108;
            this.UI7bl2_typeCombo.Visible = false;
            this.UI7bl2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI7bl2_typeCombo_SelectedIndexChanged);
            // 
            // UI7bl2_lab
            // 
            this.UI7bl2_lab.AutoSize = true;
            this.UI7bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI7bl2_lab.Location = new System.Drawing.Point(568, 221);
            this.UI7bl2_lab.Name = "UI7bl2_lab";
            this.UI7bl2_lab.Size = new System.Drawing.Size(33, 14);
            this.UI7bl2_lab.TabIndex = 109;
            this.UI7bl2_lab.Text = "UI 7";
            // 
            // UI6_bl2Label
            // 
            this.UI6_bl2Label.AutoSize = true;
            this.UI6_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI6_bl2Label.Location = new System.Drawing.Point(10, 190);
            this.UI6_bl2Label.Name = "UI6_bl2Label";
            this.UI6_bl2Label.Size = new System.Drawing.Size(33, 14);
            this.UI6_bl2Label.TabIndex = 102;
            this.UI6_bl2Label.Text = "UI 6";
            this.UI6_bl2Label.Visible = false;
            // 
            // UI6bl2_combo
            // 
            this.UI6bl2_combo.DisplayMember = "Не выбрано";
            this.UI6bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI6bl2_combo.Enabled = false;
            this.UI6bl2_combo.FormattingEnabled = true;
            this.UI6bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI6bl2_combo.Location = new System.Drawing.Point(53, 188);
            this.UI6bl2_combo.Name = "UI6bl2_combo";
            this.UI6bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI6bl2_combo.TabIndex = 103;
            this.UI6bl2_combo.Visible = false;
            this.UI6bl2_combo.SelectedIndexChanged += new System.EventHandler(this.UI6bl2_combo_SelectedIndexChanged);
            // 
            // UI6bl2_typeCombo
            // 
            this.UI6bl2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI6bl2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI6bl2_typeCombo.Enabled = false;
            this.UI6bl2_typeCombo.FormattingEnabled = true;
            this.UI6bl2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI6bl2_typeCombo.Location = new System.Drawing.Point(439, 188);
            this.UI6bl2_typeCombo.Name = "UI6bl2_typeCombo";
            this.UI6bl2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI6bl2_typeCombo.TabIndex = 104;
            this.UI6bl2_typeCombo.Visible = false;
            this.UI6bl2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI6bl2_typeCombo_SelectedIndexChanged);
            // 
            // UI6bl2_lab
            // 
            this.UI6bl2_lab.AutoSize = true;
            this.UI6bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI6bl2_lab.Location = new System.Drawing.Point(568, 190);
            this.UI6bl2_lab.Name = "UI6bl2_lab";
            this.UI6bl2_lab.Size = new System.Drawing.Size(33, 14);
            this.UI6bl2_lab.TabIndex = 105;
            this.UI6bl2_lab.Text = "UI 6";
            // 
            // UI5_bl2Label
            // 
            this.UI5_bl2Label.AutoSize = true;
            this.UI5_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI5_bl2Label.Location = new System.Drawing.Point(10, 159);
            this.UI5_bl2Label.Name = "UI5_bl2Label";
            this.UI5_bl2Label.Size = new System.Drawing.Size(33, 14);
            this.UI5_bl2Label.TabIndex = 98;
            this.UI5_bl2Label.Text = "UI 5";
            this.UI5_bl2Label.Visible = false;
            // 
            // UI5bl2_combo
            // 
            this.UI5bl2_combo.DisplayMember = "Не выбрано";
            this.UI5bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI5bl2_combo.Enabled = false;
            this.UI5bl2_combo.FormattingEnabled = true;
            this.UI5bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI5bl2_combo.Location = new System.Drawing.Point(53, 157);
            this.UI5bl2_combo.Name = "UI5bl2_combo";
            this.UI5bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI5bl2_combo.TabIndex = 99;
            this.UI5bl2_combo.Visible = false;
            this.UI5bl2_combo.SelectedIndexChanged += new System.EventHandler(this.UI5bl2_combo_SelectedIndexChanged);
            // 
            // UI5bl2_typeCombo
            // 
            this.UI5bl2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI5bl2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI5bl2_typeCombo.Enabled = false;
            this.UI5bl2_typeCombo.FormattingEnabled = true;
            this.UI5bl2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI5bl2_typeCombo.Location = new System.Drawing.Point(439, 157);
            this.UI5bl2_typeCombo.Name = "UI5bl2_typeCombo";
            this.UI5bl2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI5bl2_typeCombo.TabIndex = 100;
            this.UI5bl2_typeCombo.Visible = false;
            this.UI5bl2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI5bl2_typeCombo_SelectedIndexChanged);
            // 
            // UI5bl2_lab
            // 
            this.UI5bl2_lab.AutoSize = true;
            this.UI5bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI5bl2_lab.Location = new System.Drawing.Point(568, 159);
            this.UI5bl2_lab.Name = "UI5bl2_lab";
            this.UI5bl2_lab.Size = new System.Drawing.Size(33, 14);
            this.UI5bl2_lab.TabIndex = 101;
            this.UI5bl2_lab.Text = "UI 5";
            // 
            // UI4_bl2Label
            // 
            this.UI4_bl2Label.AutoSize = true;
            this.UI4_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI4_bl2Label.Location = new System.Drawing.Point(10, 128);
            this.UI4_bl2Label.Name = "UI4_bl2Label";
            this.UI4_bl2Label.Size = new System.Drawing.Size(33, 14);
            this.UI4_bl2Label.TabIndex = 94;
            this.UI4_bl2Label.Text = "UI 4";
            this.UI4_bl2Label.Visible = false;
            // 
            // UI4bl2_combo
            // 
            this.UI4bl2_combo.DisplayMember = "Не выбрано";
            this.UI4bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI4bl2_combo.Enabled = false;
            this.UI4bl2_combo.FormattingEnabled = true;
            this.UI4bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI4bl2_combo.Location = new System.Drawing.Point(53, 126);
            this.UI4bl2_combo.Name = "UI4bl2_combo";
            this.UI4bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI4bl2_combo.TabIndex = 95;
            this.UI4bl2_combo.Visible = false;
            this.UI4bl2_combo.SelectedIndexChanged += new System.EventHandler(this.UI4bl2_combo_SelectedIndexChanged);
            // 
            // UI4bl2_typeCombo
            // 
            this.UI4bl2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI4bl2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI4bl2_typeCombo.Enabled = false;
            this.UI4bl2_typeCombo.FormattingEnabled = true;
            this.UI4bl2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI4bl2_typeCombo.Location = new System.Drawing.Point(439, 126);
            this.UI4bl2_typeCombo.Name = "UI4bl2_typeCombo";
            this.UI4bl2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI4bl2_typeCombo.TabIndex = 96;
            this.UI4bl2_typeCombo.Visible = false;
            this.UI4bl2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI4bl2_typeCombo_SelectedIndexChanged);
            // 
            // UI4bl2_lab
            // 
            this.UI4bl2_lab.AutoSize = true;
            this.UI4bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI4bl2_lab.Location = new System.Drawing.Point(568, 128);
            this.UI4bl2_lab.Name = "UI4bl2_lab";
            this.UI4bl2_lab.Size = new System.Drawing.Size(33, 14);
            this.UI4bl2_lab.TabIndex = 97;
            this.UI4bl2_lab.Text = "UI 4";
            // 
            // UI3_bl2Label
            // 
            this.UI3_bl2Label.AutoSize = true;
            this.UI3_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI3_bl2Label.Location = new System.Drawing.Point(10, 97);
            this.UI3_bl2Label.Name = "UI3_bl2Label";
            this.UI3_bl2Label.Size = new System.Drawing.Size(33, 14);
            this.UI3_bl2Label.TabIndex = 90;
            this.UI3_bl2Label.Text = "UI 3";
            this.UI3_bl2Label.Visible = false;
            // 
            // UI3bl2_combo
            // 
            this.UI3bl2_combo.DisplayMember = "Не выбрано";
            this.UI3bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI3bl2_combo.Enabled = false;
            this.UI3bl2_combo.FormattingEnabled = true;
            this.UI3bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI3bl2_combo.Location = new System.Drawing.Point(53, 95);
            this.UI3bl2_combo.Name = "UI3bl2_combo";
            this.UI3bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI3bl2_combo.TabIndex = 91;
            this.UI3bl2_combo.Visible = false;
            this.UI3bl2_combo.SelectedIndexChanged += new System.EventHandler(this.UI3bl2_combo_SelectedIndexChanged);
            // 
            // UI3bl2_typeCombo
            // 
            this.UI3bl2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI3bl2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI3bl2_typeCombo.Enabled = false;
            this.UI3bl2_typeCombo.FormattingEnabled = true;
            this.UI3bl2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI3bl2_typeCombo.Location = new System.Drawing.Point(439, 95);
            this.UI3bl2_typeCombo.Name = "UI3bl2_typeCombo";
            this.UI3bl2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI3bl2_typeCombo.TabIndex = 92;
            this.UI3bl2_typeCombo.Visible = false;
            this.UI3bl2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI3bl2_typeCombo_SelectedIndexChanged);
            // 
            // UI3bl2_lab
            // 
            this.UI3bl2_lab.AutoSize = true;
            this.UI3bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI3bl2_lab.Location = new System.Drawing.Point(568, 97);
            this.UI3bl2_lab.Name = "UI3bl2_lab";
            this.UI3bl2_lab.Size = new System.Drawing.Size(33, 14);
            this.UI3bl2_lab.TabIndex = 93;
            this.UI3bl2_lab.Text = "UI 3";
            // 
            // UI2_bl2Label
            // 
            this.UI2_bl2Label.AutoSize = true;
            this.UI2_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI2_bl2Label.Location = new System.Drawing.Point(10, 66);
            this.UI2_bl2Label.Name = "UI2_bl2Label";
            this.UI2_bl2Label.Size = new System.Drawing.Size(33, 14);
            this.UI2_bl2Label.TabIndex = 86;
            this.UI2_bl2Label.Text = "UI 2";
            this.UI2_bl2Label.Visible = false;
            // 
            // UI2bl2_combo
            // 
            this.UI2bl2_combo.DisplayMember = "Не выбрано";
            this.UI2bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI2bl2_combo.Enabled = false;
            this.UI2bl2_combo.FormattingEnabled = true;
            this.UI2bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI2bl2_combo.Location = new System.Drawing.Point(53, 64);
            this.UI2bl2_combo.Name = "UI2bl2_combo";
            this.UI2bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI2bl2_combo.TabIndex = 87;
            this.UI2bl2_combo.Visible = false;
            this.UI2bl2_combo.SelectedIndexChanged += new System.EventHandler(this.UI2bl2_combo_SelectedIndexChanged);
            // 
            // UI2bl2_typeCombo
            // 
            this.UI2bl2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI2bl2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI2bl2_typeCombo.Enabled = false;
            this.UI2bl2_typeCombo.FormattingEnabled = true;
            this.UI2bl2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI2bl2_typeCombo.Location = new System.Drawing.Point(439, 64);
            this.UI2bl2_typeCombo.Name = "UI2bl2_typeCombo";
            this.UI2bl2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI2bl2_typeCombo.TabIndex = 88;
            this.UI2bl2_typeCombo.Visible = false;
            this.UI2bl2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI2bl2_typeCombo_SelectedIndexChanged);
            // 
            // UI2bl2_lab
            // 
            this.UI2bl2_lab.AutoSize = true;
            this.UI2bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI2bl2_lab.Location = new System.Drawing.Point(568, 66);
            this.UI2bl2_lab.Name = "UI2bl2_lab";
            this.UI2bl2_lab.Size = new System.Drawing.Size(33, 14);
            this.UI2bl2_lab.TabIndex = 89;
            this.UI2bl2_lab.Text = "UI 2";
            // 
            // UI1_bl2Label
            // 
            this.UI1_bl2Label.AutoSize = true;
            this.UI1_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI1_bl2Label.Location = new System.Drawing.Point(10, 35);
            this.UI1_bl2Label.Name = "UI1_bl2Label";
            this.UI1_bl2Label.Size = new System.Drawing.Size(33, 14);
            this.UI1_bl2Label.TabIndex = 82;
            this.UI1_bl2Label.Text = "UI 1";
            this.UI1_bl2Label.Visible = false;
            // 
            // UI1bl2_combo
            // 
            this.UI1bl2_combo.DisplayMember = "Не выбрано";
            this.UI1bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI1bl2_combo.Enabled = false;
            this.UI1bl2_combo.FormattingEnabled = true;
            this.UI1bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI1bl2_combo.Location = new System.Drawing.Point(53, 33);
            this.UI1bl2_combo.Name = "UI1bl2_combo";
            this.UI1bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI1bl2_combo.TabIndex = 83;
            this.UI1bl2_combo.Visible = false;
            this.UI1bl2_combo.SelectedIndexChanged += new System.EventHandler(this.UI1bl2_combo_SelectedIndexChanged);
            // 
            // UIblock2_header
            // 
            this.UIblock2_header.AutoSize = true;
            this.UIblock2_header.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UIblock2_header.Location = new System.Drawing.Point(8, 4);
            this.UIblock2_header.Name = "UIblock2_header";
            this.UIblock2_header.Size = new System.Drawing.Size(142, 14);
            this.UIblock2_header.TabIndex = 82;
            this.UIblock2_header.Text = "Блок расширения 2";
            // 
            // UI1bl2_typeCombo
            // 
            this.UI1bl2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI1bl2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI1bl2_typeCombo.Enabled = false;
            this.UI1bl2_typeCombo.FormattingEnabled = true;
            this.UI1bl2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI1bl2_typeCombo.Location = new System.Drawing.Point(439, 33);
            this.UI1bl2_typeCombo.Name = "UI1bl2_typeCombo";
            this.UI1bl2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI1bl2_typeCombo.TabIndex = 84;
            this.UI1bl2_typeCombo.Visible = false;
            this.UI1bl2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI1bl2_typeCombo_SelectedIndexChanged);
            // 
            // UI1bl2_lab
            // 
            this.UI1bl2_lab.AutoSize = true;
            this.UI1bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI1bl2_lab.Location = new System.Drawing.Point(568, 35);
            this.UI1bl2_lab.Name = "UI1bl2_lab";
            this.UI1bl2_lab.Size = new System.Drawing.Size(33, 14);
            this.UI1bl2_lab.TabIndex = 85;
            this.UI1bl2_lab.Text = "UI 1";
            // 
            // block1_UIpanel
            // 
            this.block1_UIpanel.Controls.Add(this.UI16_bl1Label);
            this.block1_UIpanel.Controls.Add(this.UI16bl1_combo);
            this.block1_UIpanel.Controls.Add(this.UI16bl1_typeCombo);
            this.block1_UIpanel.Controls.Add(this.UI16bl1_lab);
            this.block1_UIpanel.Controls.Add(this.UI15_bl1Label);
            this.block1_UIpanel.Controls.Add(this.UI15bl1_combo);
            this.block1_UIpanel.Controls.Add(this.UI15bl1_typeCombo);
            this.block1_UIpanel.Controls.Add(this.UI15bl1_lab);
            this.block1_UIpanel.Controls.Add(this.UI14_bl1Label);
            this.block1_UIpanel.Controls.Add(this.UI14bl1_combo);
            this.block1_UIpanel.Controls.Add(this.UI14bl1_typeCombo);
            this.block1_UIpanel.Controls.Add(this.UI14bl1_lab);
            this.block1_UIpanel.Controls.Add(this.UI13_bl1Label);
            this.block1_UIpanel.Controls.Add(this.UI13bl1_combo);
            this.block1_UIpanel.Controls.Add(this.UI13bl1_typeCombo);
            this.block1_UIpanel.Controls.Add(this.UI13bl1_lab);
            this.block1_UIpanel.Controls.Add(this.UI12_bl1Label);
            this.block1_UIpanel.Controls.Add(this.UI12bl1_combo);
            this.block1_UIpanel.Controls.Add(this.UI12bl1_typeCombo);
            this.block1_UIpanel.Controls.Add(this.UI12bl1_lab);
            this.block1_UIpanel.Controls.Add(this.UI11_bl1Label);
            this.block1_UIpanel.Controls.Add(this.UI11bl1_combo);
            this.block1_UIpanel.Controls.Add(this.UI11bl1_typeCombo);
            this.block1_UIpanel.Controls.Add(this.UI11bl1_lab);
            this.block1_UIpanel.Controls.Add(this.UI10_bl1Label);
            this.block1_UIpanel.Controls.Add(this.UI10bl1_combo);
            this.block1_UIpanel.Controls.Add(this.UI10bl1_typeCombo);
            this.block1_UIpanel.Controls.Add(this.UI10bl1_lab);
            this.block1_UIpanel.Controls.Add(this.UI9_bl1Label);
            this.block1_UIpanel.Controls.Add(this.UI9bl1_combo);
            this.block1_UIpanel.Controls.Add(this.UI9bl1_typeCombo);
            this.block1_UIpanel.Controls.Add(this.UI9bl1_lab);
            this.block1_UIpanel.Controls.Add(this.UI8_bl1Label);
            this.block1_UIpanel.Controls.Add(this.UI8bl1_combo);
            this.block1_UIpanel.Controls.Add(this.UI8bl1_typeCombo);
            this.block1_UIpanel.Controls.Add(this.UI8bl1_lab);
            this.block1_UIpanel.Controls.Add(this.UI7_bl1Label);
            this.block1_UIpanel.Controls.Add(this.UI7bl1_combo);
            this.block1_UIpanel.Controls.Add(this.UI7bl1_typeCombo);
            this.block1_UIpanel.Controls.Add(this.UI7bl1_lab);
            this.block1_UIpanel.Controls.Add(this.UI6_bl1Label);
            this.block1_UIpanel.Controls.Add(this.UI6bl1_combo);
            this.block1_UIpanel.Controls.Add(this.UI6bl1_typeCombo);
            this.block1_UIpanel.Controls.Add(this.UI6bl1_lab);
            this.block1_UIpanel.Controls.Add(this.UI5_bl1Label);
            this.block1_UIpanel.Controls.Add(this.UI5bl1_combo);
            this.block1_UIpanel.Controls.Add(this.UI5bl1_typeCombo);
            this.block1_UIpanel.Controls.Add(this.UI5bl1_lab);
            this.block1_UIpanel.Controls.Add(this.UI4_bl1Label);
            this.block1_UIpanel.Controls.Add(this.UI4bl1_combo);
            this.block1_UIpanel.Controls.Add(this.UI4bl1_typeCombo);
            this.block1_UIpanel.Controls.Add(this.UI4bl1_lab);
            this.block1_UIpanel.Controls.Add(this.UI3_bl1Label);
            this.block1_UIpanel.Controls.Add(this.UI3bl1_combo);
            this.block1_UIpanel.Controls.Add(this.UI3bl1_typeCombo);
            this.block1_UIpanel.Controls.Add(this.UI3bl1_lab);
            this.block1_UIpanel.Controls.Add(this.UI2_bl1Label);
            this.block1_UIpanel.Controls.Add(this.UI2bl1_combo);
            this.block1_UIpanel.Controls.Add(this.UI2bl1_typeCombo);
            this.block1_UIpanel.Controls.Add(this.UI2bl1_lab);
            this.block1_UIpanel.Controls.Add(this.UI1_bl1Label);
            this.block1_UIpanel.Controls.Add(this.UI1bl1_combo);
            this.block1_UIpanel.Controls.Add(this.UIblock1_header);
            this.block1_UIpanel.Controls.Add(this.UI1bl1_typeCombo);
            this.block1_UIpanel.Controls.Add(this.UI1bl1_lab);
            this.block1_UIpanel.Enabled = false;
            this.block1_UIpanel.Location = new System.Drawing.Point(6, 394);
            this.block1_UIpanel.Name = "block1_UIpanel";
            this.block1_UIpanel.Size = new System.Drawing.Size(637, 537);
            this.block1_UIpanel.TabIndex = 83;
            this.block1_UIpanel.Visible = false;
            // 
            // UI16_bl1Label
            // 
            this.UI16_bl1Label.AutoSize = true;
            this.UI16_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI16_bl1Label.Location = new System.Drawing.Point(10, 500);
            this.UI16_bl1Label.Name = "UI16_bl1Label";
            this.UI16_bl1Label.Size = new System.Drawing.Size(41, 14);
            this.UI16_bl1Label.TabIndex = 142;
            this.UI16_bl1Label.Text = "UI 16";
            this.UI16_bl1Label.Visible = false;
            // 
            // UI16bl1_combo
            // 
            this.UI16bl1_combo.DisplayMember = "Не выбрано";
            this.UI16bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI16bl1_combo.Enabled = false;
            this.UI16bl1_combo.FormattingEnabled = true;
            this.UI16bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI16bl1_combo.Location = new System.Drawing.Point(53, 498);
            this.UI16bl1_combo.Name = "UI16bl1_combo";
            this.UI16bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI16bl1_combo.TabIndex = 143;
            this.UI16bl1_combo.Visible = false;
            this.UI16bl1_combo.SelectedIndexChanged += new System.EventHandler(this.UI16bl1_combo_SelectedIndexChanged);
            // 
            // UI16bl1_typeCombo
            // 
            this.UI16bl1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI16bl1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI16bl1_typeCombo.Enabled = false;
            this.UI16bl1_typeCombo.FormattingEnabled = true;
            this.UI16bl1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI16bl1_typeCombo.Location = new System.Drawing.Point(439, 498);
            this.UI16bl1_typeCombo.Name = "UI16bl1_typeCombo";
            this.UI16bl1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI16bl1_typeCombo.TabIndex = 144;
            this.UI16bl1_typeCombo.Visible = false;
            this.UI16bl1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI16bl1_typeCombo_SelectedIndexChanged);
            // 
            // UI16bl1_lab
            // 
            this.UI16bl1_lab.AutoSize = true;
            this.UI16bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI16bl1_lab.Location = new System.Drawing.Point(568, 500);
            this.UI16bl1_lab.Name = "UI16bl1_lab";
            this.UI16bl1_lab.Size = new System.Drawing.Size(41, 14);
            this.UI16bl1_lab.TabIndex = 145;
            this.UI16bl1_lab.Text = "UI 16";
            // 
            // UI15_bl1Label
            // 
            this.UI15_bl1Label.AutoSize = true;
            this.UI15_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI15_bl1Label.Location = new System.Drawing.Point(10, 469);
            this.UI15_bl1Label.Name = "UI15_bl1Label";
            this.UI15_bl1Label.Size = new System.Drawing.Size(41, 14);
            this.UI15_bl1Label.TabIndex = 138;
            this.UI15_bl1Label.Text = "UI 15";
            this.UI15_bl1Label.Visible = false;
            // 
            // UI15bl1_combo
            // 
            this.UI15bl1_combo.DisplayMember = "Не выбрано";
            this.UI15bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI15bl1_combo.Enabled = false;
            this.UI15bl1_combo.FormattingEnabled = true;
            this.UI15bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI15bl1_combo.Location = new System.Drawing.Point(53, 467);
            this.UI15bl1_combo.Name = "UI15bl1_combo";
            this.UI15bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI15bl1_combo.TabIndex = 139;
            this.UI15bl1_combo.Visible = false;
            this.UI15bl1_combo.SelectedIndexChanged += new System.EventHandler(this.UI15bl1_combo_SelectedIndexChanged);
            // 
            // UI15bl1_typeCombo
            // 
            this.UI15bl1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI15bl1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI15bl1_typeCombo.Enabled = false;
            this.UI15bl1_typeCombo.FormattingEnabled = true;
            this.UI15bl1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI15bl1_typeCombo.Location = new System.Drawing.Point(439, 467);
            this.UI15bl1_typeCombo.Name = "UI15bl1_typeCombo";
            this.UI15bl1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI15bl1_typeCombo.TabIndex = 140;
            this.UI15bl1_typeCombo.Visible = false;
            this.UI15bl1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI15bl1_typeCombo_SelectedIndexChanged);
            // 
            // UI15bl1_lab
            // 
            this.UI15bl1_lab.AutoSize = true;
            this.UI15bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI15bl1_lab.Location = new System.Drawing.Point(568, 469);
            this.UI15bl1_lab.Name = "UI15bl1_lab";
            this.UI15bl1_lab.Size = new System.Drawing.Size(41, 14);
            this.UI15bl1_lab.TabIndex = 141;
            this.UI15bl1_lab.Text = "UI 15";
            // 
            // UI14_bl1Label
            // 
            this.UI14_bl1Label.AutoSize = true;
            this.UI14_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI14_bl1Label.Location = new System.Drawing.Point(10, 438);
            this.UI14_bl1Label.Name = "UI14_bl1Label";
            this.UI14_bl1Label.Size = new System.Drawing.Size(41, 14);
            this.UI14_bl1Label.TabIndex = 134;
            this.UI14_bl1Label.Text = "UI 14";
            this.UI14_bl1Label.Visible = false;
            // 
            // UI14bl1_combo
            // 
            this.UI14bl1_combo.DisplayMember = "Не выбрано";
            this.UI14bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI14bl1_combo.Enabled = false;
            this.UI14bl1_combo.FormattingEnabled = true;
            this.UI14bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI14bl1_combo.Location = new System.Drawing.Point(53, 436);
            this.UI14bl1_combo.Name = "UI14bl1_combo";
            this.UI14bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI14bl1_combo.TabIndex = 135;
            this.UI14bl1_combo.Visible = false;
            this.UI14bl1_combo.SelectedIndexChanged += new System.EventHandler(this.UI14bl1_combo_SelectedIndexChanged);
            // 
            // UI14bl1_typeCombo
            // 
            this.UI14bl1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI14bl1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI14bl1_typeCombo.Enabled = false;
            this.UI14bl1_typeCombo.FormattingEnabled = true;
            this.UI14bl1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI14bl1_typeCombo.Location = new System.Drawing.Point(439, 436);
            this.UI14bl1_typeCombo.Name = "UI14bl1_typeCombo";
            this.UI14bl1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI14bl1_typeCombo.TabIndex = 136;
            this.UI14bl1_typeCombo.Visible = false;
            this.UI14bl1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI14bl1_typeCombo_SelectedIndexChanged);
            // 
            // UI14bl1_lab
            // 
            this.UI14bl1_lab.AutoSize = true;
            this.UI14bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI14bl1_lab.Location = new System.Drawing.Point(568, 438);
            this.UI14bl1_lab.Name = "UI14bl1_lab";
            this.UI14bl1_lab.Size = new System.Drawing.Size(41, 14);
            this.UI14bl1_lab.TabIndex = 137;
            this.UI14bl1_lab.Text = "UI 14";
            // 
            // UI13_bl1Label
            // 
            this.UI13_bl1Label.AutoSize = true;
            this.UI13_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI13_bl1Label.Location = new System.Drawing.Point(10, 407);
            this.UI13_bl1Label.Name = "UI13_bl1Label";
            this.UI13_bl1Label.Size = new System.Drawing.Size(41, 14);
            this.UI13_bl1Label.TabIndex = 130;
            this.UI13_bl1Label.Text = "UI 13";
            this.UI13_bl1Label.Visible = false;
            // 
            // UI13bl1_combo
            // 
            this.UI13bl1_combo.DisplayMember = "Не выбрано";
            this.UI13bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI13bl1_combo.Enabled = false;
            this.UI13bl1_combo.FormattingEnabled = true;
            this.UI13bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI13bl1_combo.Location = new System.Drawing.Point(53, 405);
            this.UI13bl1_combo.Name = "UI13bl1_combo";
            this.UI13bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI13bl1_combo.TabIndex = 131;
            this.UI13bl1_combo.Visible = false;
            this.UI13bl1_combo.SelectedIndexChanged += new System.EventHandler(this.UI13bl1_combo_SelectedIndexChanged);
            // 
            // UI13bl1_typeCombo
            // 
            this.UI13bl1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI13bl1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI13bl1_typeCombo.Enabled = false;
            this.UI13bl1_typeCombo.FormattingEnabled = true;
            this.UI13bl1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI13bl1_typeCombo.Location = new System.Drawing.Point(439, 405);
            this.UI13bl1_typeCombo.Name = "UI13bl1_typeCombo";
            this.UI13bl1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI13bl1_typeCombo.TabIndex = 132;
            this.UI13bl1_typeCombo.Visible = false;
            this.UI13bl1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI13bl1_typeCombo_SelectedIndexChanged);
            // 
            // UI13bl1_lab
            // 
            this.UI13bl1_lab.AutoSize = true;
            this.UI13bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI13bl1_lab.Location = new System.Drawing.Point(568, 407);
            this.UI13bl1_lab.Name = "UI13bl1_lab";
            this.UI13bl1_lab.Size = new System.Drawing.Size(41, 14);
            this.UI13bl1_lab.TabIndex = 133;
            this.UI13bl1_lab.Text = "UI 13";
            // 
            // UI12_bl1Label
            // 
            this.UI12_bl1Label.AutoSize = true;
            this.UI12_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI12_bl1Label.Location = new System.Drawing.Point(10, 376);
            this.UI12_bl1Label.Name = "UI12_bl1Label";
            this.UI12_bl1Label.Size = new System.Drawing.Size(41, 14);
            this.UI12_bl1Label.TabIndex = 126;
            this.UI12_bl1Label.Text = "UI 12";
            this.UI12_bl1Label.Visible = false;
            // 
            // UI12bl1_combo
            // 
            this.UI12bl1_combo.DisplayMember = "Не выбрано";
            this.UI12bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI12bl1_combo.Enabled = false;
            this.UI12bl1_combo.FormattingEnabled = true;
            this.UI12bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI12bl1_combo.Location = new System.Drawing.Point(53, 374);
            this.UI12bl1_combo.Name = "UI12bl1_combo";
            this.UI12bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI12bl1_combo.TabIndex = 127;
            this.UI12bl1_combo.Visible = false;
            this.UI12bl1_combo.SelectedIndexChanged += new System.EventHandler(this.UI12bl1_combo_SelectedIndexChanged);
            // 
            // UI12bl1_typeCombo
            // 
            this.UI12bl1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI12bl1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI12bl1_typeCombo.Enabled = false;
            this.UI12bl1_typeCombo.FormattingEnabled = true;
            this.UI12bl1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI12bl1_typeCombo.Location = new System.Drawing.Point(439, 374);
            this.UI12bl1_typeCombo.Name = "UI12bl1_typeCombo";
            this.UI12bl1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI12bl1_typeCombo.TabIndex = 128;
            this.UI12bl1_typeCombo.Visible = false;
            this.UI12bl1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI12bl1_typeCombo_SelectedIndexChanged);
            // 
            // UI12bl1_lab
            // 
            this.UI12bl1_lab.AutoSize = true;
            this.UI12bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI12bl1_lab.Location = new System.Drawing.Point(568, 376);
            this.UI12bl1_lab.Name = "UI12bl1_lab";
            this.UI12bl1_lab.Size = new System.Drawing.Size(41, 14);
            this.UI12bl1_lab.TabIndex = 129;
            this.UI12bl1_lab.Text = "UI 12";
            // 
            // UI11_bl1Label
            // 
            this.UI11_bl1Label.AutoSize = true;
            this.UI11_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI11_bl1Label.Location = new System.Drawing.Point(10, 345);
            this.UI11_bl1Label.Name = "UI11_bl1Label";
            this.UI11_bl1Label.Size = new System.Drawing.Size(41, 14);
            this.UI11_bl1Label.TabIndex = 122;
            this.UI11_bl1Label.Text = "UI 11";
            this.UI11_bl1Label.Visible = false;
            // 
            // UI11bl1_combo
            // 
            this.UI11bl1_combo.DisplayMember = "Не выбрано";
            this.UI11bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI11bl1_combo.Enabled = false;
            this.UI11bl1_combo.FormattingEnabled = true;
            this.UI11bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI11bl1_combo.Location = new System.Drawing.Point(53, 343);
            this.UI11bl1_combo.Name = "UI11bl1_combo";
            this.UI11bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI11bl1_combo.TabIndex = 123;
            this.UI11bl1_combo.Visible = false;
            this.UI11bl1_combo.SelectedIndexChanged += new System.EventHandler(this.UI11bl1_combo_SelectedIndexChanged);
            // 
            // UI11bl1_typeCombo
            // 
            this.UI11bl1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI11bl1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI11bl1_typeCombo.Enabled = false;
            this.UI11bl1_typeCombo.FormattingEnabled = true;
            this.UI11bl1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI11bl1_typeCombo.Location = new System.Drawing.Point(439, 343);
            this.UI11bl1_typeCombo.Name = "UI11bl1_typeCombo";
            this.UI11bl1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI11bl1_typeCombo.TabIndex = 124;
            this.UI11bl1_typeCombo.Visible = false;
            this.UI11bl1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI11bl1_typeCombo_SelectedIndexChanged);
            // 
            // UI11bl1_lab
            // 
            this.UI11bl1_lab.AutoSize = true;
            this.UI11bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI11bl1_lab.Location = new System.Drawing.Point(568, 345);
            this.UI11bl1_lab.Name = "UI11bl1_lab";
            this.UI11bl1_lab.Size = new System.Drawing.Size(41, 14);
            this.UI11bl1_lab.TabIndex = 125;
            this.UI11bl1_lab.Text = "UI 11";
            // 
            // UI10_bl1Label
            // 
            this.UI10_bl1Label.AutoSize = true;
            this.UI10_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI10_bl1Label.Location = new System.Drawing.Point(10, 314);
            this.UI10_bl1Label.Name = "UI10_bl1Label";
            this.UI10_bl1Label.Size = new System.Drawing.Size(41, 14);
            this.UI10_bl1Label.TabIndex = 118;
            this.UI10_bl1Label.Text = "UI 10";
            this.UI10_bl1Label.Visible = false;
            // 
            // UI10bl1_combo
            // 
            this.UI10bl1_combo.DisplayMember = "Не выбрано";
            this.UI10bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI10bl1_combo.Enabled = false;
            this.UI10bl1_combo.FormattingEnabled = true;
            this.UI10bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI10bl1_combo.Location = new System.Drawing.Point(53, 312);
            this.UI10bl1_combo.Name = "UI10bl1_combo";
            this.UI10bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI10bl1_combo.TabIndex = 119;
            this.UI10bl1_combo.Visible = false;
            this.UI10bl1_combo.SelectedIndexChanged += new System.EventHandler(this.UI10bl1_combo_SelectedIndexChanged);
            // 
            // UI10bl1_typeCombo
            // 
            this.UI10bl1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI10bl1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI10bl1_typeCombo.Enabled = false;
            this.UI10bl1_typeCombo.FormattingEnabled = true;
            this.UI10bl1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI10bl1_typeCombo.Location = new System.Drawing.Point(439, 312);
            this.UI10bl1_typeCombo.Name = "UI10bl1_typeCombo";
            this.UI10bl1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI10bl1_typeCombo.TabIndex = 120;
            this.UI10bl1_typeCombo.Visible = false;
            this.UI10bl1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI10bl1_typeCombo_SelectedIndexChanged);
            // 
            // UI10bl1_lab
            // 
            this.UI10bl1_lab.AutoSize = true;
            this.UI10bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI10bl1_lab.Location = new System.Drawing.Point(568, 314);
            this.UI10bl1_lab.Name = "UI10bl1_lab";
            this.UI10bl1_lab.Size = new System.Drawing.Size(41, 14);
            this.UI10bl1_lab.TabIndex = 121;
            this.UI10bl1_lab.Text = "UI 10";
            // 
            // UI9_bl1Label
            // 
            this.UI9_bl1Label.AutoSize = true;
            this.UI9_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI9_bl1Label.Location = new System.Drawing.Point(10, 283);
            this.UI9_bl1Label.Name = "UI9_bl1Label";
            this.UI9_bl1Label.Size = new System.Drawing.Size(33, 14);
            this.UI9_bl1Label.TabIndex = 114;
            this.UI9_bl1Label.Text = "UI 9";
            this.UI9_bl1Label.Visible = false;
            // 
            // UI9bl1_combo
            // 
            this.UI9bl1_combo.DisplayMember = "Не выбрано";
            this.UI9bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI9bl1_combo.Enabled = false;
            this.UI9bl1_combo.FormattingEnabled = true;
            this.UI9bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI9bl1_combo.Location = new System.Drawing.Point(53, 281);
            this.UI9bl1_combo.Name = "UI9bl1_combo";
            this.UI9bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI9bl1_combo.TabIndex = 115;
            this.UI9bl1_combo.Visible = false;
            this.UI9bl1_combo.SelectedIndexChanged += new System.EventHandler(this.UI9bl1_combo_SelectedIndexChanged);
            // 
            // UI9bl1_typeCombo
            // 
            this.UI9bl1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI9bl1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI9bl1_typeCombo.Enabled = false;
            this.UI9bl1_typeCombo.FormattingEnabled = true;
            this.UI9bl1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI9bl1_typeCombo.Location = new System.Drawing.Point(439, 281);
            this.UI9bl1_typeCombo.Name = "UI9bl1_typeCombo";
            this.UI9bl1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI9bl1_typeCombo.TabIndex = 116;
            this.UI9bl1_typeCombo.Visible = false;
            this.UI9bl1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI9bl1_typeCombo_SelectedIndexChanged);
            // 
            // UI9bl1_lab
            // 
            this.UI9bl1_lab.AutoSize = true;
            this.UI9bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI9bl1_lab.Location = new System.Drawing.Point(568, 283);
            this.UI9bl1_lab.Name = "UI9bl1_lab";
            this.UI9bl1_lab.Size = new System.Drawing.Size(33, 14);
            this.UI9bl1_lab.TabIndex = 117;
            this.UI9bl1_lab.Text = "UI 9";
            // 
            // UI8_bl1Label
            // 
            this.UI8_bl1Label.AutoSize = true;
            this.UI8_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI8_bl1Label.Location = new System.Drawing.Point(10, 252);
            this.UI8_bl1Label.Name = "UI8_bl1Label";
            this.UI8_bl1Label.Size = new System.Drawing.Size(33, 14);
            this.UI8_bl1Label.TabIndex = 110;
            this.UI8_bl1Label.Text = "UI 8";
            this.UI8_bl1Label.Visible = false;
            // 
            // UI8bl1_combo
            // 
            this.UI8bl1_combo.DisplayMember = "Не выбрано";
            this.UI8bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI8bl1_combo.Enabled = false;
            this.UI8bl1_combo.FormattingEnabled = true;
            this.UI8bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI8bl1_combo.Location = new System.Drawing.Point(53, 250);
            this.UI8bl1_combo.Name = "UI8bl1_combo";
            this.UI8bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI8bl1_combo.TabIndex = 111;
            this.UI8bl1_combo.Visible = false;
            this.UI8bl1_combo.SelectedIndexChanged += new System.EventHandler(this.UI8bl1_combo_SelectedIndexChanged);
            // 
            // UI8bl1_typeCombo
            // 
            this.UI8bl1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI8bl1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI8bl1_typeCombo.Enabled = false;
            this.UI8bl1_typeCombo.FormattingEnabled = true;
            this.UI8bl1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI8bl1_typeCombo.Location = new System.Drawing.Point(439, 250);
            this.UI8bl1_typeCombo.Name = "UI8bl1_typeCombo";
            this.UI8bl1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI8bl1_typeCombo.TabIndex = 112;
            this.UI8bl1_typeCombo.Visible = false;
            this.UI8bl1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI8bl1_typeCombo_SelectedIndexChanged);
            // 
            // UI8bl1_lab
            // 
            this.UI8bl1_lab.AutoSize = true;
            this.UI8bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI8bl1_lab.Location = new System.Drawing.Point(568, 252);
            this.UI8bl1_lab.Name = "UI8bl1_lab";
            this.UI8bl1_lab.Size = new System.Drawing.Size(33, 14);
            this.UI8bl1_lab.TabIndex = 113;
            this.UI8bl1_lab.Text = "UI 8";
            // 
            // UI7_bl1Label
            // 
            this.UI7_bl1Label.AutoSize = true;
            this.UI7_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI7_bl1Label.Location = new System.Drawing.Point(10, 221);
            this.UI7_bl1Label.Name = "UI7_bl1Label";
            this.UI7_bl1Label.Size = new System.Drawing.Size(33, 14);
            this.UI7_bl1Label.TabIndex = 106;
            this.UI7_bl1Label.Text = "UI 7";
            this.UI7_bl1Label.Visible = false;
            // 
            // UI7bl1_combo
            // 
            this.UI7bl1_combo.DisplayMember = "Не выбрано";
            this.UI7bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI7bl1_combo.Enabled = false;
            this.UI7bl1_combo.FormattingEnabled = true;
            this.UI7bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI7bl1_combo.Location = new System.Drawing.Point(53, 219);
            this.UI7bl1_combo.Name = "UI7bl1_combo";
            this.UI7bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI7bl1_combo.TabIndex = 107;
            this.UI7bl1_combo.Visible = false;
            this.UI7bl1_combo.SelectedIndexChanged += new System.EventHandler(this.UI7bl1_combo_SelectedIndexChanged);
            // 
            // UI7bl1_typeCombo
            // 
            this.UI7bl1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI7bl1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI7bl1_typeCombo.Enabled = false;
            this.UI7bl1_typeCombo.FormattingEnabled = true;
            this.UI7bl1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI7bl1_typeCombo.Location = new System.Drawing.Point(439, 219);
            this.UI7bl1_typeCombo.Name = "UI7bl1_typeCombo";
            this.UI7bl1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI7bl1_typeCombo.TabIndex = 108;
            this.UI7bl1_typeCombo.Visible = false;
            this.UI7bl1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI7bl1_typeCombo_SelectedIndexChanged);
            // 
            // UI7bl1_lab
            // 
            this.UI7bl1_lab.AutoSize = true;
            this.UI7bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI7bl1_lab.Location = new System.Drawing.Point(568, 221);
            this.UI7bl1_lab.Name = "UI7bl1_lab";
            this.UI7bl1_lab.Size = new System.Drawing.Size(33, 14);
            this.UI7bl1_lab.TabIndex = 109;
            this.UI7bl1_lab.Text = "UI 7";
            // 
            // UI6_bl1Label
            // 
            this.UI6_bl1Label.AutoSize = true;
            this.UI6_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI6_bl1Label.Location = new System.Drawing.Point(10, 190);
            this.UI6_bl1Label.Name = "UI6_bl1Label";
            this.UI6_bl1Label.Size = new System.Drawing.Size(33, 14);
            this.UI6_bl1Label.TabIndex = 102;
            this.UI6_bl1Label.Text = "UI 6";
            this.UI6_bl1Label.Visible = false;
            // 
            // UI6bl1_combo
            // 
            this.UI6bl1_combo.DisplayMember = "Не выбрано";
            this.UI6bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI6bl1_combo.Enabled = false;
            this.UI6bl1_combo.FormattingEnabled = true;
            this.UI6bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI6bl1_combo.Location = new System.Drawing.Point(53, 188);
            this.UI6bl1_combo.Name = "UI6bl1_combo";
            this.UI6bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI6bl1_combo.TabIndex = 103;
            this.UI6bl1_combo.Visible = false;
            this.UI6bl1_combo.SelectedIndexChanged += new System.EventHandler(this.UI6bl1_combo_SelectedIndexChanged);
            // 
            // UI6bl1_typeCombo
            // 
            this.UI6bl1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI6bl1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI6bl1_typeCombo.Enabled = false;
            this.UI6bl1_typeCombo.FormattingEnabled = true;
            this.UI6bl1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI6bl1_typeCombo.Location = new System.Drawing.Point(439, 188);
            this.UI6bl1_typeCombo.Name = "UI6bl1_typeCombo";
            this.UI6bl1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI6bl1_typeCombo.TabIndex = 104;
            this.UI6bl1_typeCombo.Visible = false;
            this.UI6bl1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI6bl1_typeCombo_SelectedIndexChanged);
            // 
            // UI6bl1_lab
            // 
            this.UI6bl1_lab.AutoSize = true;
            this.UI6bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI6bl1_lab.Location = new System.Drawing.Point(568, 190);
            this.UI6bl1_lab.Name = "UI6bl1_lab";
            this.UI6bl1_lab.Size = new System.Drawing.Size(33, 14);
            this.UI6bl1_lab.TabIndex = 105;
            this.UI6bl1_lab.Text = "UI 6";
            // 
            // UI5_bl1Label
            // 
            this.UI5_bl1Label.AutoSize = true;
            this.UI5_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI5_bl1Label.Location = new System.Drawing.Point(10, 159);
            this.UI5_bl1Label.Name = "UI5_bl1Label";
            this.UI5_bl1Label.Size = new System.Drawing.Size(33, 14);
            this.UI5_bl1Label.TabIndex = 98;
            this.UI5_bl1Label.Text = "UI 5";
            this.UI5_bl1Label.Visible = false;
            // 
            // UI5bl1_combo
            // 
            this.UI5bl1_combo.DisplayMember = "Не выбрано";
            this.UI5bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI5bl1_combo.Enabled = false;
            this.UI5bl1_combo.FormattingEnabled = true;
            this.UI5bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI5bl1_combo.Location = new System.Drawing.Point(53, 157);
            this.UI5bl1_combo.Name = "UI5bl1_combo";
            this.UI5bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI5bl1_combo.TabIndex = 99;
            this.UI5bl1_combo.Visible = false;
            this.UI5bl1_combo.SelectedIndexChanged += new System.EventHandler(this.UI5bl1_combo_SelectedIndexChanged);
            // 
            // UI5bl1_typeCombo
            // 
            this.UI5bl1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI5bl1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI5bl1_typeCombo.Enabled = false;
            this.UI5bl1_typeCombo.FormattingEnabled = true;
            this.UI5bl1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI5bl1_typeCombo.Location = new System.Drawing.Point(439, 157);
            this.UI5bl1_typeCombo.Name = "UI5bl1_typeCombo";
            this.UI5bl1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI5bl1_typeCombo.TabIndex = 100;
            this.UI5bl1_typeCombo.Visible = false;
            this.UI5bl1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI5bl1_typeCombo_SelectedIndexChanged);
            // 
            // UI5bl1_lab
            // 
            this.UI5bl1_lab.AutoSize = true;
            this.UI5bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI5bl1_lab.Location = new System.Drawing.Point(568, 159);
            this.UI5bl1_lab.Name = "UI5bl1_lab";
            this.UI5bl1_lab.Size = new System.Drawing.Size(33, 14);
            this.UI5bl1_lab.TabIndex = 101;
            this.UI5bl1_lab.Text = "UI 5";
            // 
            // UI4_bl1Label
            // 
            this.UI4_bl1Label.AutoSize = true;
            this.UI4_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI4_bl1Label.Location = new System.Drawing.Point(10, 128);
            this.UI4_bl1Label.Name = "UI4_bl1Label";
            this.UI4_bl1Label.Size = new System.Drawing.Size(33, 14);
            this.UI4_bl1Label.TabIndex = 94;
            this.UI4_bl1Label.Text = "UI 4";
            this.UI4_bl1Label.Visible = false;
            // 
            // UI4bl1_combo
            // 
            this.UI4bl1_combo.DisplayMember = "Не выбрано";
            this.UI4bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI4bl1_combo.Enabled = false;
            this.UI4bl1_combo.FormattingEnabled = true;
            this.UI4bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI4bl1_combo.Location = new System.Drawing.Point(53, 126);
            this.UI4bl1_combo.Name = "UI4bl1_combo";
            this.UI4bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI4bl1_combo.TabIndex = 95;
            this.UI4bl1_combo.Visible = false;
            this.UI4bl1_combo.SelectedIndexChanged += new System.EventHandler(this.UI4bl1_combo_SelectedIndexChanged);
            // 
            // UI4bl1_typeCombo
            // 
            this.UI4bl1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI4bl1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI4bl1_typeCombo.Enabled = false;
            this.UI4bl1_typeCombo.FormattingEnabled = true;
            this.UI4bl1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI4bl1_typeCombo.Location = new System.Drawing.Point(439, 126);
            this.UI4bl1_typeCombo.Name = "UI4bl1_typeCombo";
            this.UI4bl1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI4bl1_typeCombo.TabIndex = 96;
            this.UI4bl1_typeCombo.Visible = false;
            this.UI4bl1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI4bl1_typeCombo_SelectedIndexChanged);
            // 
            // UI4bl1_lab
            // 
            this.UI4bl1_lab.AutoSize = true;
            this.UI4bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI4bl1_lab.Location = new System.Drawing.Point(568, 128);
            this.UI4bl1_lab.Name = "UI4bl1_lab";
            this.UI4bl1_lab.Size = new System.Drawing.Size(33, 14);
            this.UI4bl1_lab.TabIndex = 97;
            this.UI4bl1_lab.Text = "UI 4";
            // 
            // UI3_bl1Label
            // 
            this.UI3_bl1Label.AutoSize = true;
            this.UI3_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI3_bl1Label.Location = new System.Drawing.Point(10, 97);
            this.UI3_bl1Label.Name = "UI3_bl1Label";
            this.UI3_bl1Label.Size = new System.Drawing.Size(33, 14);
            this.UI3_bl1Label.TabIndex = 90;
            this.UI3_bl1Label.Text = "UI 3";
            this.UI3_bl1Label.Visible = false;
            // 
            // UI3bl1_combo
            // 
            this.UI3bl1_combo.DisplayMember = "Не выбрано";
            this.UI3bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI3bl1_combo.Enabled = false;
            this.UI3bl1_combo.FormattingEnabled = true;
            this.UI3bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI3bl1_combo.Location = new System.Drawing.Point(53, 95);
            this.UI3bl1_combo.Name = "UI3bl1_combo";
            this.UI3bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI3bl1_combo.TabIndex = 91;
            this.UI3bl1_combo.Visible = false;
            this.UI3bl1_combo.SelectedIndexChanged += new System.EventHandler(this.UI3bl1_combo_SelectedIndexChanged);
            // 
            // UI3bl1_typeCombo
            // 
            this.UI3bl1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI3bl1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI3bl1_typeCombo.Enabled = false;
            this.UI3bl1_typeCombo.FormattingEnabled = true;
            this.UI3bl1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI3bl1_typeCombo.Location = new System.Drawing.Point(439, 95);
            this.UI3bl1_typeCombo.Name = "UI3bl1_typeCombo";
            this.UI3bl1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI3bl1_typeCombo.TabIndex = 92;
            this.UI3bl1_typeCombo.Visible = false;
            this.UI3bl1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI3bl1_typeCombo_SelectedIndexChanged);
            // 
            // UI3bl1_lab
            // 
            this.UI3bl1_lab.AutoSize = true;
            this.UI3bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI3bl1_lab.Location = new System.Drawing.Point(568, 97);
            this.UI3bl1_lab.Name = "UI3bl1_lab";
            this.UI3bl1_lab.Size = new System.Drawing.Size(33, 14);
            this.UI3bl1_lab.TabIndex = 93;
            this.UI3bl1_lab.Text = "UI 3";
            // 
            // UI2_bl1Label
            // 
            this.UI2_bl1Label.AutoSize = true;
            this.UI2_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI2_bl1Label.Location = new System.Drawing.Point(10, 66);
            this.UI2_bl1Label.Name = "UI2_bl1Label";
            this.UI2_bl1Label.Size = new System.Drawing.Size(33, 14);
            this.UI2_bl1Label.TabIndex = 86;
            this.UI2_bl1Label.Text = "UI 2";
            this.UI2_bl1Label.Visible = false;
            // 
            // UI2bl1_combo
            // 
            this.UI2bl1_combo.DisplayMember = "Не выбрано";
            this.UI2bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI2bl1_combo.Enabled = false;
            this.UI2bl1_combo.FormattingEnabled = true;
            this.UI2bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI2bl1_combo.Location = new System.Drawing.Point(53, 64);
            this.UI2bl1_combo.Name = "UI2bl1_combo";
            this.UI2bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI2bl1_combo.TabIndex = 87;
            this.UI2bl1_combo.Visible = false;
            this.UI2bl1_combo.SelectedIndexChanged += new System.EventHandler(this.UI2bl1_combo_SelectedIndexChanged);
            // 
            // UI2bl1_typeCombo
            // 
            this.UI2bl1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI2bl1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI2bl1_typeCombo.Enabled = false;
            this.UI2bl1_typeCombo.FormattingEnabled = true;
            this.UI2bl1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI2bl1_typeCombo.Location = new System.Drawing.Point(439, 64);
            this.UI2bl1_typeCombo.Name = "UI2bl1_typeCombo";
            this.UI2bl1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI2bl1_typeCombo.TabIndex = 88;
            this.UI2bl1_typeCombo.Visible = false;
            this.UI2bl1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI2bl1_typeCombo_SelectedIndexChanged);
            // 
            // UI2bl1_lab
            // 
            this.UI2bl1_lab.AutoSize = true;
            this.UI2bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI2bl1_lab.Location = new System.Drawing.Point(568, 66);
            this.UI2bl1_lab.Name = "UI2bl1_lab";
            this.UI2bl1_lab.Size = new System.Drawing.Size(33, 14);
            this.UI2bl1_lab.TabIndex = 89;
            this.UI2bl1_lab.Text = "UI 2";
            // 
            // UI1_bl1Label
            // 
            this.UI1_bl1Label.AutoSize = true;
            this.UI1_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI1_bl1Label.Location = new System.Drawing.Point(10, 35);
            this.UI1_bl1Label.Name = "UI1_bl1Label";
            this.UI1_bl1Label.Size = new System.Drawing.Size(33, 14);
            this.UI1_bl1Label.TabIndex = 82;
            this.UI1_bl1Label.Text = "UI 1";
            this.UI1_bl1Label.Visible = false;
            // 
            // UI1bl1_combo
            // 
            this.UI1bl1_combo.DisplayMember = "Не выбрано";
            this.UI1bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI1bl1_combo.Enabled = false;
            this.UI1bl1_combo.FormattingEnabled = true;
            this.UI1bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI1bl1_combo.Location = new System.Drawing.Point(53, 33);
            this.UI1bl1_combo.Name = "UI1bl1_combo";
            this.UI1bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI1bl1_combo.TabIndex = 83;
            this.UI1bl1_combo.Visible = false;
            this.UI1bl1_combo.SelectedIndexChanged += new System.EventHandler(this.UI1bl1_combo_SelectedIndexChanged);
            // 
            // UIblock1_header
            // 
            this.UIblock1_header.AutoSize = true;
            this.UIblock1_header.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UIblock1_header.Location = new System.Drawing.Point(8, 4);
            this.UIblock1_header.Name = "UIblock1_header";
            this.UIblock1_header.Size = new System.Drawing.Size(142, 14);
            this.UIblock1_header.TabIndex = 82;
            this.UIblock1_header.Text = "Блок расширения 1";
            // 
            // UI1bl1_typeCombo
            // 
            this.UI1bl1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI1bl1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI1bl1_typeCombo.Enabled = false;
            this.UI1bl1_typeCombo.FormattingEnabled = true;
            this.UI1bl1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI1bl1_typeCombo.Location = new System.Drawing.Point(439, 33);
            this.UI1bl1_typeCombo.Name = "UI1bl1_typeCombo";
            this.UI1bl1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI1bl1_typeCombo.TabIndex = 84;
            this.UI1bl1_typeCombo.Visible = false;
            this.UI1bl1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI1bl1_typeCombo_SelectedIndexChanged);
            // 
            // UI1bl1_lab
            // 
            this.UI1bl1_lab.AutoSize = true;
            this.UI1bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI1bl1_lab.Location = new System.Drawing.Point(568, 35);
            this.UI1bl1_lab.Name = "UI1bl1_lab";
            this.UI1bl1_lab.Size = new System.Drawing.Size(33, 14);
            this.UI1bl1_lab.TabIndex = 85;
            this.UI1bl1_lab.Text = "UI 1";
            // 
            // plk_UIpanel
            // 
            this.plk_UIpanel.Controls.Add(this.UIplk_header);
            this.plk_UIpanel.Controls.Add(this.UI11_lab);
            this.plk_UIpanel.Controls.Add(this.UI1_plkLabel);
            this.plk_UIpanel.Controls.Add(this.UI11_typeCombo);
            this.plk_UIpanel.Controls.Add(this.UI1_combo);
            this.plk_UIpanel.Controls.Add(this.UI11_combo);
            this.plk_UIpanel.Controls.Add(this.UI1_typeCombo);
            this.plk_UIpanel.Controls.Add(this.UI11_plkLabel);
            this.plk_UIpanel.Controls.Add(this.UI1_lab);
            this.plk_UIpanel.Controls.Add(this.UI10_lab);
            this.plk_UIpanel.Controls.Add(this.UI2_plkLabel);
            this.plk_UIpanel.Controls.Add(this.UI10_typeCombo);
            this.plk_UIpanel.Controls.Add(this.UI2_combo);
            this.plk_UIpanel.Controls.Add(this.UI10_combo);
            this.plk_UIpanel.Controls.Add(this.UI2_typeCombo);
            this.plk_UIpanel.Controls.Add(this.UI10_plkLabel);
            this.plk_UIpanel.Controls.Add(this.UI2_lab);
            this.plk_UIpanel.Controls.Add(this.UI9_lab);
            this.plk_UIpanel.Controls.Add(this.UI3_plkLabel);
            this.plk_UIpanel.Controls.Add(this.UI9_typeCombo);
            this.plk_UIpanel.Controls.Add(this.UI3_combo);
            this.plk_UIpanel.Controls.Add(this.UI9_combo);
            this.plk_UIpanel.Controls.Add(this.UI3_typeCombo);
            this.plk_UIpanel.Controls.Add(this.UI9_plkLabel);
            this.plk_UIpanel.Controls.Add(this.UI3_lab);
            this.plk_UIpanel.Controls.Add(this.UI8_lab);
            this.plk_UIpanel.Controls.Add(this.UI4_plkLabel);
            this.plk_UIpanel.Controls.Add(this.UI8_typeCombo);
            this.plk_UIpanel.Controls.Add(this.UI4_combo);
            this.plk_UIpanel.Controls.Add(this.UI8_combo);
            this.plk_UIpanel.Controls.Add(this.UI4_typeCombo);
            this.plk_UIpanel.Controls.Add(this.UI8_plkLabel);
            this.plk_UIpanel.Controls.Add(this.UI4_lab);
            this.plk_UIpanel.Controls.Add(this.UI7_lab);
            this.plk_UIpanel.Controls.Add(this.UI5_plkLabel);
            this.plk_UIpanel.Controls.Add(this.UI7_typeCombo);
            this.plk_UIpanel.Controls.Add(this.UI5_combo);
            this.plk_UIpanel.Controls.Add(this.UI7_combo);
            this.plk_UIpanel.Controls.Add(this.UI5_typeCombo);
            this.plk_UIpanel.Controls.Add(this.UI7_plkLabel);
            this.plk_UIpanel.Controls.Add(this.UI5_lab);
            this.plk_UIpanel.Controls.Add(this.UI6_lab);
            this.plk_UIpanel.Controls.Add(this.UI6_plkLabel);
            this.plk_UIpanel.Controls.Add(this.UI6_typeCombo);
            this.plk_UIpanel.Controls.Add(this.UI6_combo);
            this.plk_UIpanel.Location = new System.Drawing.Point(6, 11);
            this.plk_UIpanel.Name = "plk_UIpanel";
            this.plk_UIpanel.Size = new System.Drawing.Size(637, 382);
            this.plk_UIpanel.TabIndex = 82;
            // 
            // UIplk_header
            // 
            this.UIplk_header.AutoSize = true;
            this.UIplk_header.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UIplk_header.Location = new System.Drawing.Point(8, 4);
            this.UIplk_header.Name = "UIplk_header";
            this.UIplk_header.Size = new System.Drawing.Size(88, 14);
            this.UIplk_header.TabIndex = 1;
            this.UIplk_header.Text = "Контроллер";
            // 
            // UI11_lab
            // 
            this.UI11_lab.AutoSize = true;
            this.UI11_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI11_lab.Location = new System.Drawing.Point(568, 345);
            this.UI11_lab.Name = "UI11_lab";
            this.UI11_lab.Size = new System.Drawing.Size(41, 14);
            this.UI11_lab.TabIndex = 81;
            this.UI11_lab.Text = "UI 11";
            // 
            // UI1_plkLabel
            // 
            this.UI1_plkLabel.AutoSize = true;
            this.UI1_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI1_plkLabel.Location = new System.Drawing.Point(10, 35);
            this.UI1_plkLabel.Name = "UI1_plkLabel";
            this.UI1_plkLabel.Size = new System.Drawing.Size(33, 14);
            this.UI1_plkLabel.TabIndex = 2;
            this.UI1_plkLabel.Text = "UI 1";
            // 
            // UI11_typeCombo
            // 
            this.UI11_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI11_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI11_typeCombo.FormattingEnabled = true;
            this.UI11_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI11_typeCombo.Location = new System.Drawing.Point(439, 343);
            this.UI11_typeCombo.Name = "UI11_typeCombo";
            this.UI11_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI11_typeCombo.TabIndex = 80;
            this.UI11_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI11_typeCombo_SelectedIndexChanged);
            // 
            // UI1_combo
            // 
            this.UI1_combo.DisplayMember = "Не выбрано";
            this.UI1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI1_combo.FormattingEnabled = true;
            this.UI1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI1_combo.Location = new System.Drawing.Point(53, 33);
            this.UI1_combo.Name = "UI1_combo";
            this.UI1_combo.Size = new System.Drawing.Size(380, 21);
            this.UI1_combo.TabIndex = 5;
            this.UI1_combo.SelectedIndexChanged += new System.EventHandler(this.UI1_combo_SelectedIndexChanged);
            // 
            // UI11_combo
            // 
            this.UI11_combo.DisplayMember = "Не выбрано";
            this.UI11_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI11_combo.FormattingEnabled = true;
            this.UI11_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI11_combo.Location = new System.Drawing.Point(53, 343);
            this.UI11_combo.Name = "UI11_combo";
            this.UI11_combo.Size = new System.Drawing.Size(380, 21);
            this.UI11_combo.TabIndex = 79;
            this.UI11_combo.SelectedIndexChanged += new System.EventHandler(this.UI11_combo_SelectedIndexChanged);
            // 
            // UI1_typeCombo
            // 
            this.UI1_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI1_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI1_typeCombo.FormattingEnabled = true;
            this.UI1_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI1_typeCombo.Location = new System.Drawing.Point(439, 33);
            this.UI1_typeCombo.Name = "UI1_typeCombo";
            this.UI1_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI1_typeCombo.TabIndex = 14;
            this.UI1_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI1_typeCombo_SelectedIndexChanged);
            // 
            // UI11_plkLabel
            // 
            this.UI11_plkLabel.AutoSize = true;
            this.UI11_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI11_plkLabel.Location = new System.Drawing.Point(10, 345);
            this.UI11_plkLabel.Name = "UI11_plkLabel";
            this.UI11_plkLabel.Size = new System.Drawing.Size(41, 14);
            this.UI11_plkLabel.TabIndex = 78;
            this.UI11_plkLabel.Text = "UI 11";
            // 
            // UI1_lab
            // 
            this.UI1_lab.AutoSize = true;
            this.UI1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI1_lab.Location = new System.Drawing.Point(568, 35);
            this.UI1_lab.Name = "UI1_lab";
            this.UI1_lab.Size = new System.Drawing.Size(33, 14);
            this.UI1_lab.TabIndex = 41;
            this.UI1_lab.Text = "UI 1";
            // 
            // UI10_lab
            // 
            this.UI10_lab.AutoSize = true;
            this.UI10_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI10_lab.Location = new System.Drawing.Point(568, 314);
            this.UI10_lab.Name = "UI10_lab";
            this.UI10_lab.Size = new System.Drawing.Size(41, 14);
            this.UI10_lab.TabIndex = 77;
            this.UI10_lab.Text = "UI 10";
            // 
            // UI2_plkLabel
            // 
            this.UI2_plkLabel.AutoSize = true;
            this.UI2_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI2_plkLabel.Location = new System.Drawing.Point(10, 66);
            this.UI2_plkLabel.Name = "UI2_plkLabel";
            this.UI2_plkLabel.Size = new System.Drawing.Size(33, 14);
            this.UI2_plkLabel.TabIndex = 42;
            this.UI2_plkLabel.Text = "UI 2";
            // 
            // UI10_typeCombo
            // 
            this.UI10_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI10_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI10_typeCombo.FormattingEnabled = true;
            this.UI10_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI10_typeCombo.Location = new System.Drawing.Point(439, 312);
            this.UI10_typeCombo.Name = "UI10_typeCombo";
            this.UI10_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI10_typeCombo.TabIndex = 76;
            this.UI10_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI10_typeCombo_SelectedIndexChanged);
            // 
            // UI2_combo
            // 
            this.UI2_combo.DisplayMember = "Не выбрано";
            this.UI2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI2_combo.FormattingEnabled = true;
            this.UI2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI2_combo.Location = new System.Drawing.Point(53, 64);
            this.UI2_combo.Name = "UI2_combo";
            this.UI2_combo.Size = new System.Drawing.Size(380, 21);
            this.UI2_combo.TabIndex = 43;
            this.UI2_combo.SelectedIndexChanged += new System.EventHandler(this.UI2_combo_SelectedIndexChanged);
            // 
            // UI10_combo
            // 
            this.UI10_combo.DisplayMember = "Не выбрано";
            this.UI10_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI10_combo.FormattingEnabled = true;
            this.UI10_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI10_combo.Location = new System.Drawing.Point(53, 312);
            this.UI10_combo.Name = "UI10_combo";
            this.UI10_combo.Size = new System.Drawing.Size(380, 21);
            this.UI10_combo.TabIndex = 75;
            this.UI10_combo.SelectedIndexChanged += new System.EventHandler(this.UI10_combo_SelectedIndexChanged);
            // 
            // UI2_typeCombo
            // 
            this.UI2_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI2_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI2_typeCombo.FormattingEnabled = true;
            this.UI2_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI2_typeCombo.Location = new System.Drawing.Point(439, 64);
            this.UI2_typeCombo.Name = "UI2_typeCombo";
            this.UI2_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI2_typeCombo.TabIndex = 44;
            this.UI2_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI2_typeCombo_SelectedIndexChanged);
            // 
            // UI10_plkLabel
            // 
            this.UI10_plkLabel.AutoSize = true;
            this.UI10_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI10_plkLabel.Location = new System.Drawing.Point(10, 314);
            this.UI10_plkLabel.Name = "UI10_plkLabel";
            this.UI10_plkLabel.Size = new System.Drawing.Size(41, 14);
            this.UI10_plkLabel.TabIndex = 74;
            this.UI10_plkLabel.Text = "UI 10";
            // 
            // UI2_lab
            // 
            this.UI2_lab.AutoSize = true;
            this.UI2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI2_lab.Location = new System.Drawing.Point(568, 66);
            this.UI2_lab.Name = "UI2_lab";
            this.UI2_lab.Size = new System.Drawing.Size(33, 14);
            this.UI2_lab.TabIndex = 45;
            this.UI2_lab.Text = "UI 2";
            // 
            // UI9_lab
            // 
            this.UI9_lab.AutoSize = true;
            this.UI9_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI9_lab.Location = new System.Drawing.Point(568, 283);
            this.UI9_lab.Name = "UI9_lab";
            this.UI9_lab.Size = new System.Drawing.Size(33, 14);
            this.UI9_lab.TabIndex = 73;
            this.UI9_lab.Text = "UI 9";
            // 
            // UI3_plkLabel
            // 
            this.UI3_plkLabel.AutoSize = true;
            this.UI3_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI3_plkLabel.Location = new System.Drawing.Point(10, 97);
            this.UI3_plkLabel.Name = "UI3_plkLabel";
            this.UI3_plkLabel.Size = new System.Drawing.Size(33, 14);
            this.UI3_plkLabel.TabIndex = 46;
            this.UI3_plkLabel.Text = "UI 3";
            // 
            // UI9_typeCombo
            // 
            this.UI9_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI9_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI9_typeCombo.FormattingEnabled = true;
            this.UI9_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI9_typeCombo.Location = new System.Drawing.Point(439, 281);
            this.UI9_typeCombo.Name = "UI9_typeCombo";
            this.UI9_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI9_typeCombo.TabIndex = 72;
            this.UI9_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI9_typeCombo_SelectedIndexChanged);
            // 
            // UI3_combo
            // 
            this.UI3_combo.DisplayMember = "Не выбрано";
            this.UI3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI3_combo.FormattingEnabled = true;
            this.UI3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI3_combo.Location = new System.Drawing.Point(53, 95);
            this.UI3_combo.Name = "UI3_combo";
            this.UI3_combo.Size = new System.Drawing.Size(380, 21);
            this.UI3_combo.TabIndex = 47;
            this.UI3_combo.SelectedIndexChanged += new System.EventHandler(this.UI3_combo_SelectedIndexChanged);
            // 
            // UI9_combo
            // 
            this.UI9_combo.DisplayMember = "Не выбрано";
            this.UI9_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI9_combo.FormattingEnabled = true;
            this.UI9_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI9_combo.Location = new System.Drawing.Point(53, 281);
            this.UI9_combo.Name = "UI9_combo";
            this.UI9_combo.Size = new System.Drawing.Size(380, 21);
            this.UI9_combo.TabIndex = 71;
            this.UI9_combo.SelectedIndexChanged += new System.EventHandler(this.UI9_combo_SelectedIndexChanged);
            // 
            // UI3_typeCombo
            // 
            this.UI3_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI3_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI3_typeCombo.FormattingEnabled = true;
            this.UI3_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI3_typeCombo.Location = new System.Drawing.Point(439, 95);
            this.UI3_typeCombo.Name = "UI3_typeCombo";
            this.UI3_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI3_typeCombo.TabIndex = 48;
            this.UI3_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI3_typeCombo_SelectedIndexChanged);
            // 
            // UI9_plkLabel
            // 
            this.UI9_plkLabel.AutoSize = true;
            this.UI9_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI9_plkLabel.Location = new System.Drawing.Point(10, 283);
            this.UI9_plkLabel.Name = "UI9_plkLabel";
            this.UI9_plkLabel.Size = new System.Drawing.Size(33, 14);
            this.UI9_plkLabel.TabIndex = 70;
            this.UI9_plkLabel.Text = "UI 9";
            // 
            // UI3_lab
            // 
            this.UI3_lab.AutoSize = true;
            this.UI3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI3_lab.Location = new System.Drawing.Point(568, 97);
            this.UI3_lab.Name = "UI3_lab";
            this.UI3_lab.Size = new System.Drawing.Size(33, 14);
            this.UI3_lab.TabIndex = 49;
            this.UI3_lab.Text = "UI 3";
            // 
            // UI8_lab
            // 
            this.UI8_lab.AutoSize = true;
            this.UI8_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI8_lab.Location = new System.Drawing.Point(568, 252);
            this.UI8_lab.Name = "UI8_lab";
            this.UI8_lab.Size = new System.Drawing.Size(33, 14);
            this.UI8_lab.TabIndex = 69;
            this.UI8_lab.Text = "UI 8";
            // 
            // UI4_plkLabel
            // 
            this.UI4_plkLabel.AutoSize = true;
            this.UI4_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI4_plkLabel.Location = new System.Drawing.Point(10, 128);
            this.UI4_plkLabel.Name = "UI4_plkLabel";
            this.UI4_plkLabel.Size = new System.Drawing.Size(33, 14);
            this.UI4_plkLabel.TabIndex = 50;
            this.UI4_plkLabel.Text = "UI 4";
            // 
            // UI8_typeCombo
            // 
            this.UI8_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI8_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI8_typeCombo.FormattingEnabled = true;
            this.UI8_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI8_typeCombo.Location = new System.Drawing.Point(439, 250);
            this.UI8_typeCombo.Name = "UI8_typeCombo";
            this.UI8_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI8_typeCombo.TabIndex = 68;
            this.UI8_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI8_typeCombo_SelectedIndexChanged);
            // 
            // UI4_combo
            // 
            this.UI4_combo.DisplayMember = "Не выбрано";
            this.UI4_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI4_combo.FormattingEnabled = true;
            this.UI4_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI4_combo.Location = new System.Drawing.Point(53, 126);
            this.UI4_combo.Name = "UI4_combo";
            this.UI4_combo.Size = new System.Drawing.Size(380, 21);
            this.UI4_combo.TabIndex = 51;
            this.UI4_combo.SelectedIndexChanged += new System.EventHandler(this.UI4_combo_SelectedIndexChanged);
            // 
            // UI8_combo
            // 
            this.UI8_combo.DisplayMember = "Не выбрано";
            this.UI8_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI8_combo.FormattingEnabled = true;
            this.UI8_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI8_combo.Location = new System.Drawing.Point(53, 250);
            this.UI8_combo.Name = "UI8_combo";
            this.UI8_combo.Size = new System.Drawing.Size(380, 21);
            this.UI8_combo.TabIndex = 67;
            this.UI8_combo.SelectedIndexChanged += new System.EventHandler(this.UI8_combo_SelectedIndexChanged);
            // 
            // UI4_typeCombo
            // 
            this.UI4_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI4_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI4_typeCombo.FormattingEnabled = true;
            this.UI4_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI4_typeCombo.Location = new System.Drawing.Point(439, 126);
            this.UI4_typeCombo.Name = "UI4_typeCombo";
            this.UI4_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI4_typeCombo.TabIndex = 52;
            this.UI4_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI4_typeCombo_SelectedIndexChanged);
            // 
            // UI8_plkLabel
            // 
            this.UI8_plkLabel.AutoSize = true;
            this.UI8_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI8_plkLabel.Location = new System.Drawing.Point(10, 252);
            this.UI8_plkLabel.Name = "UI8_plkLabel";
            this.UI8_plkLabel.Size = new System.Drawing.Size(33, 14);
            this.UI8_plkLabel.TabIndex = 66;
            this.UI8_plkLabel.Text = "UI 8";
            // 
            // UI4_lab
            // 
            this.UI4_lab.AutoSize = true;
            this.UI4_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI4_lab.Location = new System.Drawing.Point(568, 128);
            this.UI4_lab.Name = "UI4_lab";
            this.UI4_lab.Size = new System.Drawing.Size(33, 14);
            this.UI4_lab.TabIndex = 53;
            this.UI4_lab.Text = "UI 4";
            // 
            // UI7_lab
            // 
            this.UI7_lab.AutoSize = true;
            this.UI7_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI7_lab.Location = new System.Drawing.Point(568, 221);
            this.UI7_lab.Name = "UI7_lab";
            this.UI7_lab.Size = new System.Drawing.Size(33, 14);
            this.UI7_lab.TabIndex = 65;
            this.UI7_lab.Text = "UI 7";
            // 
            // UI5_plkLabel
            // 
            this.UI5_plkLabel.AutoSize = true;
            this.UI5_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI5_plkLabel.Location = new System.Drawing.Point(10, 159);
            this.UI5_plkLabel.Name = "UI5_plkLabel";
            this.UI5_plkLabel.Size = new System.Drawing.Size(33, 14);
            this.UI5_plkLabel.TabIndex = 54;
            this.UI5_plkLabel.Text = "UI 5";
            // 
            // UI7_typeCombo
            // 
            this.UI7_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI7_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI7_typeCombo.FormattingEnabled = true;
            this.UI7_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI7_typeCombo.Location = new System.Drawing.Point(439, 219);
            this.UI7_typeCombo.Name = "UI7_typeCombo";
            this.UI7_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI7_typeCombo.TabIndex = 64;
            this.UI7_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI7_typeCombo_SelectedIndexChanged);
            // 
            // UI5_combo
            // 
            this.UI5_combo.DisplayMember = "Не выбрано";
            this.UI5_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI5_combo.FormattingEnabled = true;
            this.UI5_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI5_combo.Location = new System.Drawing.Point(53, 157);
            this.UI5_combo.Name = "UI5_combo";
            this.UI5_combo.Size = new System.Drawing.Size(380, 21);
            this.UI5_combo.TabIndex = 55;
            this.UI5_combo.SelectedIndexChanged += new System.EventHandler(this.UI5_combo_SelectedIndexChanged);
            // 
            // UI7_combo
            // 
            this.UI7_combo.DisplayMember = "Не выбрано";
            this.UI7_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI7_combo.FormattingEnabled = true;
            this.UI7_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI7_combo.Location = new System.Drawing.Point(53, 219);
            this.UI7_combo.Name = "UI7_combo";
            this.UI7_combo.Size = new System.Drawing.Size(380, 21);
            this.UI7_combo.TabIndex = 63;
            this.UI7_combo.SelectedIndexChanged += new System.EventHandler(this.UI7_combo_SelectedIndexChanged);
            // 
            // UI5_typeCombo
            // 
            this.UI5_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI5_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI5_typeCombo.FormattingEnabled = true;
            this.UI5_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI5_typeCombo.Location = new System.Drawing.Point(439, 157);
            this.UI5_typeCombo.Name = "UI5_typeCombo";
            this.UI5_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI5_typeCombo.TabIndex = 56;
            this.UI5_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI5_typeCombo_SelectedIndexChanged);
            // 
            // UI7_plkLabel
            // 
            this.UI7_plkLabel.AutoSize = true;
            this.UI7_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI7_plkLabel.Location = new System.Drawing.Point(10, 221);
            this.UI7_plkLabel.Name = "UI7_plkLabel";
            this.UI7_plkLabel.Size = new System.Drawing.Size(33, 14);
            this.UI7_plkLabel.TabIndex = 62;
            this.UI7_plkLabel.Text = "UI 7";
            // 
            // UI5_lab
            // 
            this.UI5_lab.AutoSize = true;
            this.UI5_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI5_lab.Location = new System.Drawing.Point(568, 159);
            this.UI5_lab.Name = "UI5_lab";
            this.UI5_lab.Size = new System.Drawing.Size(33, 14);
            this.UI5_lab.TabIndex = 57;
            this.UI5_lab.Text = "UI 5";
            // 
            // UI6_lab
            // 
            this.UI6_lab.AutoSize = true;
            this.UI6_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI6_lab.Location = new System.Drawing.Point(568, 190);
            this.UI6_lab.Name = "UI6_lab";
            this.UI6_lab.Size = new System.Drawing.Size(33, 14);
            this.UI6_lab.TabIndex = 61;
            this.UI6_lab.Text = "UI 6";
            // 
            // UI6_plkLabel
            // 
            this.UI6_plkLabel.AutoSize = true;
            this.UI6_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UI6_plkLabel.Location = new System.Drawing.Point(10, 190);
            this.UI6_plkLabel.Name = "UI6_plkLabel";
            this.UI6_plkLabel.Size = new System.Drawing.Size(33, 14);
            this.UI6_plkLabel.TabIndex = 58;
            this.UI6_plkLabel.Text = "UI 6";
            // 
            // UI6_typeCombo
            // 
            this.UI6_typeCombo.AutoCompleteCustomSource.AddRange(new string[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI6_typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI6_typeCombo.FormattingEnabled = true;
            this.UI6_typeCombo.Items.AddRange(new object[] {
            "NTC",
            "4-20 мА",
            "DI"});
            this.UI6_typeCombo.Location = new System.Drawing.Point(439, 188);
            this.UI6_typeCombo.Name = "UI6_typeCombo";
            this.UI6_typeCombo.Size = new System.Drawing.Size(123, 21);
            this.UI6_typeCombo.TabIndex = 60;
            this.UI6_typeCombo.SelectedIndexChanged += new System.EventHandler(this.UI6_typeCombo_SelectedIndexChanged);
            // 
            // UI6_combo
            // 
            this.UI6_combo.DisplayMember = "Не выбрано";
            this.UI6_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UI6_combo.FormattingEnabled = true;
            this.UI6_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.UI6_combo.Location = new System.Drawing.Point(53, 188);
            this.UI6_combo.Name = "UI6_combo";
            this.UI6_combo.Size = new System.Drawing.Size(380, 21);
            this.UI6_combo.TabIndex = 59;
            this.UI6_combo.SelectedIndexChanged += new System.EventHandler(this.UI6_combo_SelectedIndexChanged);
            // 
            // tabDO
            // 
            this.tabDO.AutoScroll = true;
            this.tabDO.Controls.Add(this.plk_DOpanel);
            this.tabDO.Controls.Add(this.block3_DOpanel);
            this.tabDO.Controls.Add(this.block2_DOpanel);
            this.tabDO.Controls.Add(this.block1_DOpanel);
            this.tabDO.Location = new System.Drawing.Point(4, 22);
            this.tabDO.Name = "tabDO";
            this.tabDO.Size = new System.Drawing.Size(721, 762);
            this.tabDO.TabIndex = 3;
            this.tabDO.Text = "DO сигналы";
            // 
            // plk_DOpanel
            // 
            this.plk_DOpanel.Controls.Add(this.DOplk_header);
            this.plk_DOpanel.Controls.Add(this.DO1_combo);
            this.plk_DOpanel.Controls.Add(this.DO6_lab);
            this.plk_DOpanel.Controls.Add(this.DO1_plkLabel);
            this.plk_DOpanel.Controls.Add(this.DO5_lab);
            this.plk_DOpanel.Controls.Add(this.DO1_lab);
            this.plk_DOpanel.Controls.Add(this.DO4_lab);
            this.plk_DOpanel.Controls.Add(this.DO2_combo);
            this.plk_DOpanel.Controls.Add(this.DO3_lab);
            this.plk_DOpanel.Controls.Add(this.DO2_plkLabel);
            this.plk_DOpanel.Controls.Add(this.DO2_lab);
            this.plk_DOpanel.Controls.Add(this.DO3_plkLabel);
            this.plk_DOpanel.Controls.Add(this.DO6_combo);
            this.plk_DOpanel.Controls.Add(this.DO4_plkLabel);
            this.plk_DOpanel.Controls.Add(this.DO5_combo);
            this.plk_DOpanel.Controls.Add(this.DO5_plkLabel);
            this.plk_DOpanel.Controls.Add(this.DO4_combo);
            this.plk_DOpanel.Controls.Add(this.DO6_plkLabel);
            this.plk_DOpanel.Controls.Add(this.DO3_combo);
            this.plk_DOpanel.Location = new System.Drawing.Point(6, 11);
            this.plk_DOpanel.Name = "plk_DOpanel";
            this.plk_DOpanel.Size = new System.Drawing.Size(514, 227);
            this.plk_DOpanel.TabIndex = 47;
            // 
            // DOplk_header
            // 
            this.DOplk_header.AutoSize = true;
            this.DOplk_header.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DOplk_header.Location = new System.Drawing.Point(8, 4);
            this.DOplk_header.Name = "DOplk_header";
            this.DOplk_header.Size = new System.Drawing.Size(88, 14);
            this.DOplk_header.TabIndex = 2;
            this.DOplk_header.Text = "Контроллер";
            // 
            // DO1_combo
            // 
            this.DO1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO1_combo.FormattingEnabled = true;
            this.DO1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO1_combo.Location = new System.Drawing.Point(47, 33);
            this.DO1_combo.Name = "DO1_combo";
            this.DO1_combo.Size = new System.Drawing.Size(380, 21);
            this.DO1_combo.TabIndex = 15;
            this.DO1_combo.SelectedIndexChanged += new System.EventHandler(this.DO1_combo_SelectedIndexChanged);
            // 
            // DO6_lab
            // 
            this.DO6_lab.AutoSize = true;
            this.DO6_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO6_lab.Location = new System.Drawing.Point(433, 190);
            this.DO6_lab.Name = "DO6_lab";
            this.DO6_lab.Size = new System.Drawing.Size(38, 14);
            this.DO6_lab.TabIndex = 44;
            this.DO6_lab.Text = "DO 6";
            // 
            // DO1_plkLabel
            // 
            this.DO1_plkLabel.AutoSize = true;
            this.DO1_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO1_plkLabel.Location = new System.Drawing.Point(5, 35);
            this.DO1_plkLabel.Name = "DO1_plkLabel";
            this.DO1_plkLabel.Size = new System.Drawing.Size(38, 14);
            this.DO1_plkLabel.TabIndex = 9;
            this.DO1_plkLabel.Text = "DO 1";
            // 
            // DO5_lab
            // 
            this.DO5_lab.AutoSize = true;
            this.DO5_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO5_lab.Location = new System.Drawing.Point(433, 159);
            this.DO5_lab.Name = "DO5_lab";
            this.DO5_lab.Size = new System.Drawing.Size(38, 14);
            this.DO5_lab.TabIndex = 43;
            this.DO5_lab.Text = "DO 5";
            // 
            // DO1_lab
            // 
            this.DO1_lab.AutoSize = true;
            this.DO1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO1_lab.Location = new System.Drawing.Point(433, 35);
            this.DO1_lab.Name = "DO1_lab";
            this.DO1_lab.Size = new System.Drawing.Size(38, 14);
            this.DO1_lab.TabIndex = 39;
            this.DO1_lab.Text = "DO 1";
            // 
            // DO4_lab
            // 
            this.DO4_lab.AutoSize = true;
            this.DO4_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO4_lab.Location = new System.Drawing.Point(433, 128);
            this.DO4_lab.Name = "DO4_lab";
            this.DO4_lab.Size = new System.Drawing.Size(38, 14);
            this.DO4_lab.TabIndex = 42;
            this.DO4_lab.Text = "DO 4";
            // 
            // DO2_combo
            // 
            this.DO2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO2_combo.FormattingEnabled = true;
            this.DO2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO2_combo.Location = new System.Drawing.Point(46, 64);
            this.DO2_combo.Name = "DO2_combo";
            this.DO2_combo.Size = new System.Drawing.Size(380, 21);
            this.DO2_combo.TabIndex = 16;
            this.DO2_combo.SelectedIndexChanged += new System.EventHandler(this.DO2_combo_SelectedIndexChanged);
            // 
            // DO3_lab
            // 
            this.DO3_lab.AutoSize = true;
            this.DO3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO3_lab.Location = new System.Drawing.Point(433, 97);
            this.DO3_lab.Name = "DO3_lab";
            this.DO3_lab.Size = new System.Drawing.Size(38, 14);
            this.DO3_lab.TabIndex = 41;
            this.DO3_lab.Text = "DO 3";
            // 
            // DO2_plkLabel
            // 
            this.DO2_plkLabel.AutoSize = true;
            this.DO2_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO2_plkLabel.Location = new System.Drawing.Point(5, 66);
            this.DO2_plkLabel.Name = "DO2_plkLabel";
            this.DO2_plkLabel.Size = new System.Drawing.Size(38, 14);
            this.DO2_plkLabel.TabIndex = 10;
            this.DO2_plkLabel.Text = "DO 2";
            // 
            // DO2_lab
            // 
            this.DO2_lab.AutoSize = true;
            this.DO2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO2_lab.Location = new System.Drawing.Point(433, 66);
            this.DO2_lab.Name = "DO2_lab";
            this.DO2_lab.Size = new System.Drawing.Size(38, 14);
            this.DO2_lab.TabIndex = 40;
            this.DO2_lab.Text = "DO 2";
            // 
            // DO3_plkLabel
            // 
            this.DO3_plkLabel.AutoSize = true;
            this.DO3_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO3_plkLabel.Location = new System.Drawing.Point(5, 97);
            this.DO3_plkLabel.Name = "DO3_plkLabel";
            this.DO3_plkLabel.Size = new System.Drawing.Size(38, 14);
            this.DO3_plkLabel.TabIndex = 11;
            this.DO3_plkLabel.Text = "DO 3";
            // 
            // DO6_combo
            // 
            this.DO6_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO6_combo.FormattingEnabled = true;
            this.DO6_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO6_combo.Location = new System.Drawing.Point(46, 188);
            this.DO6_combo.Name = "DO6_combo";
            this.DO6_combo.Size = new System.Drawing.Size(380, 21);
            this.DO6_combo.TabIndex = 20;
            this.DO6_combo.SelectedIndexChanged += new System.EventHandler(this.DO6_combo_SelectedIndexChanged);
            // 
            // DO4_plkLabel
            // 
            this.DO4_plkLabel.AutoSize = true;
            this.DO4_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO4_plkLabel.Location = new System.Drawing.Point(5, 128);
            this.DO4_plkLabel.Name = "DO4_plkLabel";
            this.DO4_plkLabel.Size = new System.Drawing.Size(38, 14);
            this.DO4_plkLabel.TabIndex = 12;
            this.DO4_plkLabel.Text = "DO 4";
            // 
            // DO5_combo
            // 
            this.DO5_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO5_combo.FormattingEnabled = true;
            this.DO5_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO5_combo.Location = new System.Drawing.Point(46, 157);
            this.DO5_combo.Name = "DO5_combo";
            this.DO5_combo.Size = new System.Drawing.Size(380, 21);
            this.DO5_combo.TabIndex = 19;
            this.DO5_combo.SelectedIndexChanged += new System.EventHandler(this.DO5_combo_SelectedIndexChanged);
            // 
            // DO5_plkLabel
            // 
            this.DO5_plkLabel.AutoSize = true;
            this.DO5_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO5_plkLabel.Location = new System.Drawing.Point(5, 159);
            this.DO5_plkLabel.Name = "DO5_plkLabel";
            this.DO5_plkLabel.Size = new System.Drawing.Size(38, 14);
            this.DO5_plkLabel.TabIndex = 13;
            this.DO5_plkLabel.Text = "DO 5";
            // 
            // DO4_combo
            // 
            this.DO4_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO4_combo.FormattingEnabled = true;
            this.DO4_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO4_combo.Location = new System.Drawing.Point(46, 126);
            this.DO4_combo.Name = "DO4_combo";
            this.DO4_combo.Size = new System.Drawing.Size(380, 21);
            this.DO4_combo.TabIndex = 18;
            this.DO4_combo.SelectedIndexChanged += new System.EventHandler(this.DO4_combo_SelectedIndexChanged);
            // 
            // DO6_plkLabel
            // 
            this.DO6_plkLabel.AutoSize = true;
            this.DO6_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO6_plkLabel.Location = new System.Drawing.Point(5, 190);
            this.DO6_plkLabel.Name = "DO6_plkLabel";
            this.DO6_plkLabel.Size = new System.Drawing.Size(38, 14);
            this.DO6_plkLabel.TabIndex = 14;
            this.DO6_plkLabel.Text = "DO 6";
            // 
            // DO3_combo
            // 
            this.DO3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO3_combo.FormattingEnabled = true;
            this.DO3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO3_combo.Location = new System.Drawing.Point(46, 95);
            this.DO3_combo.Name = "DO3_combo";
            this.DO3_combo.Size = new System.Drawing.Size(380, 21);
            this.DO3_combo.TabIndex = 17;
            this.DO3_combo.SelectedIndexChanged += new System.EventHandler(this.DO3_combo_SelectedIndexChanged);
            // 
            // block3_DOpanel
            // 
            this.block3_DOpanel.Controls.Add(this.DO8bl3_lab);
            this.block3_DOpanel.Controls.Add(this.DO8bl3_combo);
            this.block3_DOpanel.Controls.Add(this.DO8_bl3Label);
            this.block3_DOpanel.Controls.Add(this.DO7bl3_lab);
            this.block3_DOpanel.Controls.Add(this.DO7bl3_combo);
            this.block3_DOpanel.Controls.Add(this.DO7_bl3Label);
            this.block3_DOpanel.Controls.Add(this.DO6bl3_lab);
            this.block3_DOpanel.Controls.Add(this.DO6bl3_combo);
            this.block3_DOpanel.Controls.Add(this.DO6_bl3Label);
            this.block3_DOpanel.Controls.Add(this.DO5bl3_lab);
            this.block3_DOpanel.Controls.Add(this.DO5bl3_combo);
            this.block3_DOpanel.Controls.Add(this.DO5_bl3Label);
            this.block3_DOpanel.Controls.Add(this.DO4bl3_lab);
            this.block3_DOpanel.Controls.Add(this.DO4bl3_combo);
            this.block3_DOpanel.Controls.Add(this.DO4_bl3Label);
            this.block3_DOpanel.Controls.Add(this.DO3bl3_lab);
            this.block3_DOpanel.Controls.Add(this.DO3bl3_combo);
            this.block3_DOpanel.Controls.Add(this.DO3_bl3Label);
            this.block3_DOpanel.Controls.Add(this.DO2bl3_lab);
            this.block3_DOpanel.Controls.Add(this.DO2bl3_combo);
            this.block3_DOpanel.Controls.Add(this.DO2_bl3Label);
            this.block3_DOpanel.Controls.Add(this.DO1bl3_lab);
            this.block3_DOpanel.Controls.Add(this.DOblock3_header);
            this.block3_DOpanel.Controls.Add(this.DO1bl3_combo);
            this.block3_DOpanel.Controls.Add(this.DO1_bl3Label);
            this.block3_DOpanel.Enabled = false;
            this.block3_DOpanel.Location = new System.Drawing.Point(3, 820);
            this.block3_DOpanel.Name = "block3_DOpanel";
            this.block3_DOpanel.Size = new System.Drawing.Size(518, 289);
            this.block3_DOpanel.TabIndex = 46;
            this.block3_DOpanel.Visible = false;
            // 
            // DO8bl3_lab
            // 
            this.DO8bl3_lab.AutoSize = true;
            this.DO8bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO8bl3_lab.Location = new System.Drawing.Point(433, 252);
            this.DO8bl3_lab.Name = "DO8bl3_lab";
            this.DO8bl3_lab.Size = new System.Drawing.Size(38, 14);
            this.DO8bl3_lab.TabIndex = 68;
            this.DO8bl3_lab.Text = "DO 8";
            // 
            // DO8bl3_combo
            // 
            this.DO8bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO8bl3_combo.FormattingEnabled = true;
            this.DO8bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO8bl3_combo.Location = new System.Drawing.Point(47, 250);
            this.DO8bl3_combo.Name = "DO8bl3_combo";
            this.DO8bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.DO8bl3_combo.TabIndex = 67;
            this.DO8bl3_combo.SelectedIndexChanged += new System.EventHandler(this.DO8bl3_combo_SelectedIndexChanged);
            // 
            // DO8_bl3Label
            // 
            this.DO8_bl3Label.AutoSize = true;
            this.DO8_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO8_bl3Label.Location = new System.Drawing.Point(5, 252);
            this.DO8_bl3Label.Name = "DO8_bl3Label";
            this.DO8_bl3Label.Size = new System.Drawing.Size(38, 14);
            this.DO8_bl3Label.TabIndex = 66;
            this.DO8_bl3Label.Text = "DO 8";
            // 
            // DO7bl3_lab
            // 
            this.DO7bl3_lab.AutoSize = true;
            this.DO7bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO7bl3_lab.Location = new System.Drawing.Point(433, 221);
            this.DO7bl3_lab.Name = "DO7bl3_lab";
            this.DO7bl3_lab.Size = new System.Drawing.Size(38, 14);
            this.DO7bl3_lab.TabIndex = 65;
            this.DO7bl3_lab.Text = "DO 7";
            // 
            // DO7bl3_combo
            // 
            this.DO7bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO7bl3_combo.FormattingEnabled = true;
            this.DO7bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO7bl3_combo.Location = new System.Drawing.Point(47, 219);
            this.DO7bl3_combo.Name = "DO7bl3_combo";
            this.DO7bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.DO7bl3_combo.TabIndex = 64;
            this.DO7bl3_combo.SelectedIndexChanged += new System.EventHandler(this.DO7bl3_combo_SelectedIndexChanged);
            // 
            // DO7_bl3Label
            // 
            this.DO7_bl3Label.AutoSize = true;
            this.DO7_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO7_bl3Label.Location = new System.Drawing.Point(5, 221);
            this.DO7_bl3Label.Name = "DO7_bl3Label";
            this.DO7_bl3Label.Size = new System.Drawing.Size(38, 14);
            this.DO7_bl3Label.TabIndex = 63;
            this.DO7_bl3Label.Text = "DO 7";
            // 
            // DO6bl3_lab
            // 
            this.DO6bl3_lab.AutoSize = true;
            this.DO6bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO6bl3_lab.Location = new System.Drawing.Point(433, 190);
            this.DO6bl3_lab.Name = "DO6bl3_lab";
            this.DO6bl3_lab.Size = new System.Drawing.Size(38, 14);
            this.DO6bl3_lab.TabIndex = 62;
            this.DO6bl3_lab.Text = "DO 6";
            // 
            // DO6bl3_combo
            // 
            this.DO6bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO6bl3_combo.FormattingEnabled = true;
            this.DO6bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO6bl3_combo.Location = new System.Drawing.Point(47, 188);
            this.DO6bl3_combo.Name = "DO6bl3_combo";
            this.DO6bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.DO6bl3_combo.TabIndex = 61;
            this.DO6bl3_combo.SelectedIndexChanged += new System.EventHandler(this.DO6bl3_combo_SelectedIndexChanged);
            // 
            // DO6_bl3Label
            // 
            this.DO6_bl3Label.AutoSize = true;
            this.DO6_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO6_bl3Label.Location = new System.Drawing.Point(5, 190);
            this.DO6_bl3Label.Name = "DO6_bl3Label";
            this.DO6_bl3Label.Size = new System.Drawing.Size(38, 14);
            this.DO6_bl3Label.TabIndex = 60;
            this.DO6_bl3Label.Text = "DO 6";
            // 
            // DO5bl3_lab
            // 
            this.DO5bl3_lab.AutoSize = true;
            this.DO5bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO5bl3_lab.Location = new System.Drawing.Point(433, 159);
            this.DO5bl3_lab.Name = "DO5bl3_lab";
            this.DO5bl3_lab.Size = new System.Drawing.Size(38, 14);
            this.DO5bl3_lab.TabIndex = 59;
            this.DO5bl3_lab.Text = "DO 5";
            // 
            // DO5bl3_combo
            // 
            this.DO5bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO5bl3_combo.FormattingEnabled = true;
            this.DO5bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO5bl3_combo.Location = new System.Drawing.Point(47, 157);
            this.DO5bl3_combo.Name = "DO5bl3_combo";
            this.DO5bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.DO5bl3_combo.TabIndex = 58;
            this.DO5bl3_combo.SelectedIndexChanged += new System.EventHandler(this.DO5bl3_combo_SelectedIndexChanged);
            // 
            // DO5_bl3Label
            // 
            this.DO5_bl3Label.AutoSize = true;
            this.DO5_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO5_bl3Label.Location = new System.Drawing.Point(5, 159);
            this.DO5_bl3Label.Name = "DO5_bl3Label";
            this.DO5_bl3Label.Size = new System.Drawing.Size(38, 14);
            this.DO5_bl3Label.TabIndex = 57;
            this.DO5_bl3Label.Text = "DO 5";
            // 
            // DO4bl3_lab
            // 
            this.DO4bl3_lab.AutoSize = true;
            this.DO4bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO4bl3_lab.Location = new System.Drawing.Point(433, 128);
            this.DO4bl3_lab.Name = "DO4bl3_lab";
            this.DO4bl3_lab.Size = new System.Drawing.Size(38, 14);
            this.DO4bl3_lab.TabIndex = 56;
            this.DO4bl3_lab.Text = "DO 4";
            // 
            // DO4bl3_combo
            // 
            this.DO4bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO4bl3_combo.FormattingEnabled = true;
            this.DO4bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO4bl3_combo.Location = new System.Drawing.Point(47, 126);
            this.DO4bl3_combo.Name = "DO4bl3_combo";
            this.DO4bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.DO4bl3_combo.TabIndex = 55;
            this.DO4bl3_combo.SelectedIndexChanged += new System.EventHandler(this.DO4bl3_combo_SelectedIndexChanged);
            // 
            // DO4_bl3Label
            // 
            this.DO4_bl3Label.AutoSize = true;
            this.DO4_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO4_bl3Label.Location = new System.Drawing.Point(5, 128);
            this.DO4_bl3Label.Name = "DO4_bl3Label";
            this.DO4_bl3Label.Size = new System.Drawing.Size(38, 14);
            this.DO4_bl3Label.TabIndex = 54;
            this.DO4_bl3Label.Text = "DO 4";
            // 
            // DO3bl3_lab
            // 
            this.DO3bl3_lab.AutoSize = true;
            this.DO3bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO3bl3_lab.Location = new System.Drawing.Point(433, 97);
            this.DO3bl3_lab.Name = "DO3bl3_lab";
            this.DO3bl3_lab.Size = new System.Drawing.Size(38, 14);
            this.DO3bl3_lab.TabIndex = 53;
            this.DO3bl3_lab.Text = "DO 3";
            // 
            // DO3bl3_combo
            // 
            this.DO3bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO3bl3_combo.FormattingEnabled = true;
            this.DO3bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO3bl3_combo.Location = new System.Drawing.Point(47, 95);
            this.DO3bl3_combo.Name = "DO3bl3_combo";
            this.DO3bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.DO3bl3_combo.TabIndex = 52;
            this.DO3bl3_combo.SelectedIndexChanged += new System.EventHandler(this.DO3bl3_combo_SelectedIndexChanged);
            // 
            // DO3_bl3Label
            // 
            this.DO3_bl3Label.AutoSize = true;
            this.DO3_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO3_bl3Label.Location = new System.Drawing.Point(5, 97);
            this.DO3_bl3Label.Name = "DO3_bl3Label";
            this.DO3_bl3Label.Size = new System.Drawing.Size(38, 14);
            this.DO3_bl3Label.TabIndex = 51;
            this.DO3_bl3Label.Text = "DO 3";
            // 
            // DO2bl3_lab
            // 
            this.DO2bl3_lab.AutoSize = true;
            this.DO2bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO2bl3_lab.Location = new System.Drawing.Point(433, 66);
            this.DO2bl3_lab.Name = "DO2bl3_lab";
            this.DO2bl3_lab.Size = new System.Drawing.Size(38, 14);
            this.DO2bl3_lab.TabIndex = 50;
            this.DO2bl3_lab.Text = "DO 2";
            // 
            // DO2bl3_combo
            // 
            this.DO2bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO2bl3_combo.FormattingEnabled = true;
            this.DO2bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO2bl3_combo.Location = new System.Drawing.Point(47, 64);
            this.DO2bl3_combo.Name = "DO2bl3_combo";
            this.DO2bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.DO2bl3_combo.TabIndex = 49;
            this.DO2bl3_combo.SelectedIndexChanged += new System.EventHandler(this.DO2bl3_combo_SelectedIndexChanged);
            // 
            // DO2_bl3Label
            // 
            this.DO2_bl3Label.AutoSize = true;
            this.DO2_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO2_bl3Label.Location = new System.Drawing.Point(5, 66);
            this.DO2_bl3Label.Name = "DO2_bl3Label";
            this.DO2_bl3Label.Size = new System.Drawing.Size(38, 14);
            this.DO2_bl3Label.TabIndex = 48;
            this.DO2_bl3Label.Text = "DO 2";
            // 
            // DO1bl3_lab
            // 
            this.DO1bl3_lab.AutoSize = true;
            this.DO1bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO1bl3_lab.Location = new System.Drawing.Point(433, 35);
            this.DO1bl3_lab.Name = "DO1bl3_lab";
            this.DO1bl3_lab.Size = new System.Drawing.Size(38, 14);
            this.DO1bl3_lab.TabIndex = 47;
            this.DO1bl3_lab.Text = "DO 1";
            // 
            // DOblock3_header
            // 
            this.DOblock3_header.AutoSize = true;
            this.DOblock3_header.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DOblock3_header.Location = new System.Drawing.Point(8, 4);
            this.DOblock3_header.Name = "DOblock3_header";
            this.DOblock3_header.Size = new System.Drawing.Size(142, 14);
            this.DOblock3_header.TabIndex = 45;
            this.DOblock3_header.Text = "Блок расширения 3";
            // 
            // DO1bl3_combo
            // 
            this.DO1bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO1bl3_combo.FormattingEnabled = true;
            this.DO1bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO1bl3_combo.Location = new System.Drawing.Point(47, 33);
            this.DO1bl3_combo.Name = "DO1bl3_combo";
            this.DO1bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.DO1bl3_combo.TabIndex = 46;
            this.DO1bl3_combo.SelectedIndexChanged += new System.EventHandler(this.DO1bl3_combo_SelectedIndexChanged);
            // 
            // DO1_bl3Label
            // 
            this.DO1_bl3Label.AutoSize = true;
            this.DO1_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO1_bl3Label.Location = new System.Drawing.Point(5, 35);
            this.DO1_bl3Label.Name = "DO1_bl3Label";
            this.DO1_bl3Label.Size = new System.Drawing.Size(38, 14);
            this.DO1_bl3Label.TabIndex = 45;
            this.DO1_bl3Label.Text = "DO 1";
            // 
            // block2_DOpanel
            // 
            this.block2_DOpanel.Controls.Add(this.DO8bl2_lab);
            this.block2_DOpanel.Controls.Add(this.DO8bl2_combo);
            this.block2_DOpanel.Controls.Add(this.DO8_bl2Label);
            this.block2_DOpanel.Controls.Add(this.DO7bl2_lab);
            this.block2_DOpanel.Controls.Add(this.DO1bl2_lab);
            this.block2_DOpanel.Controls.Add(this.DO2bl2_lab);
            this.block2_DOpanel.Controls.Add(this.DO3bl2_lab);
            this.block2_DOpanel.Controls.Add(this.DO4bl2_lab);
            this.block2_DOpanel.Controls.Add(this.DO5bl2_lab);
            this.block2_DOpanel.Controls.Add(this.DO6bl2_lab);
            this.block2_DOpanel.Controls.Add(this.DO7bl2_combo);
            this.block2_DOpanel.Controls.Add(this.DOblock2_header);
            this.block2_DOpanel.Controls.Add(this.DO7_bl2Label);
            this.block2_DOpanel.Controls.Add(this.DO1bl2_combo);
            this.block2_DOpanel.Controls.Add(this.DO6bl2_combo);
            this.block2_DOpanel.Controls.Add(this.DO1_bl2Label);
            this.block2_DOpanel.Controls.Add(this.DO5bl2_combo);
            this.block2_DOpanel.Controls.Add(this.DO2_bl2Label);
            this.block2_DOpanel.Controls.Add(this.DO4bl2_combo);
            this.block2_DOpanel.Controls.Add(this.DO3_bl2Label);
            this.block2_DOpanel.Controls.Add(this.DO3bl2_combo);
            this.block2_DOpanel.Controls.Add(this.DO4_bl2Label);
            this.block2_DOpanel.Controls.Add(this.DO2bl2_combo);
            this.block2_DOpanel.Controls.Add(this.DO5_bl2Label);
            this.block2_DOpanel.Controls.Add(this.DO6_bl2Label);
            this.block2_DOpanel.Enabled = false;
            this.block2_DOpanel.Location = new System.Drawing.Point(6, 529);
            this.block2_DOpanel.Name = "block2_DOpanel";
            this.block2_DOpanel.Size = new System.Drawing.Size(514, 289);
            this.block2_DOpanel.TabIndex = 38;
            this.block2_DOpanel.Visible = false;
            // 
            // DO8bl2_lab
            // 
            this.DO8bl2_lab.AutoSize = true;
            this.DO8bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO8bl2_lab.Location = new System.Drawing.Point(433, 252);
            this.DO8bl2_lab.Name = "DO8bl2_lab";
            this.DO8bl2_lab.Size = new System.Drawing.Size(38, 14);
            this.DO8bl2_lab.TabIndex = 47;
            this.DO8bl2_lab.Text = "DO 8";
            // 
            // DO8bl2_combo
            // 
            this.DO8bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO8bl2_combo.FormattingEnabled = true;
            this.DO8bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO8bl2_combo.Location = new System.Drawing.Point(46, 250);
            this.DO8bl2_combo.Name = "DO8bl2_combo";
            this.DO8bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.DO8bl2_combo.TabIndex = 46;
            this.DO8bl2_combo.SelectedIndexChanged += new System.EventHandler(this.DO8bl2_combo_SelectedIndexChanged);
            // 
            // DO8_bl2Label
            // 
            this.DO8_bl2Label.AutoSize = true;
            this.DO8_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO8_bl2Label.Location = new System.Drawing.Point(5, 252);
            this.DO8_bl2Label.Name = "DO8_bl2Label";
            this.DO8_bl2Label.Size = new System.Drawing.Size(38, 14);
            this.DO8_bl2Label.TabIndex = 45;
            this.DO8_bl2Label.Text = "DO 8";
            // 
            // DO7bl2_lab
            // 
            this.DO7bl2_lab.AutoSize = true;
            this.DO7bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO7bl2_lab.Location = new System.Drawing.Point(433, 221);
            this.DO7bl2_lab.Name = "DO7bl2_lab";
            this.DO7bl2_lab.Size = new System.Drawing.Size(38, 14);
            this.DO7bl2_lab.TabIndex = 44;
            this.DO7bl2_lab.Text = "DO 7";
            // 
            // DO1bl2_lab
            // 
            this.DO1bl2_lab.AutoSize = true;
            this.DO1bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO1bl2_lab.Location = new System.Drawing.Point(433, 35);
            this.DO1bl2_lab.Name = "DO1bl2_lab";
            this.DO1bl2_lab.Size = new System.Drawing.Size(38, 14);
            this.DO1bl2_lab.TabIndex = 38;
            this.DO1bl2_lab.Text = "DO 1";
            // 
            // DO2bl2_lab
            // 
            this.DO2bl2_lab.AutoSize = true;
            this.DO2bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO2bl2_lab.Location = new System.Drawing.Point(433, 66);
            this.DO2bl2_lab.Name = "DO2bl2_lab";
            this.DO2bl2_lab.Size = new System.Drawing.Size(38, 14);
            this.DO2bl2_lab.TabIndex = 39;
            this.DO2bl2_lab.Text = "DO 2";
            // 
            // DO3bl2_lab
            // 
            this.DO3bl2_lab.AutoSize = true;
            this.DO3bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO3bl2_lab.Location = new System.Drawing.Point(433, 97);
            this.DO3bl2_lab.Name = "DO3bl2_lab";
            this.DO3bl2_lab.Size = new System.Drawing.Size(38, 14);
            this.DO3bl2_lab.TabIndex = 40;
            this.DO3bl2_lab.Text = "DO 3";
            // 
            // DO4bl2_lab
            // 
            this.DO4bl2_lab.AutoSize = true;
            this.DO4bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO4bl2_lab.Location = new System.Drawing.Point(433, 128);
            this.DO4bl2_lab.Name = "DO4bl2_lab";
            this.DO4bl2_lab.Size = new System.Drawing.Size(38, 14);
            this.DO4bl2_lab.TabIndex = 41;
            this.DO4bl2_lab.Text = "DO 4";
            // 
            // DO5bl2_lab
            // 
            this.DO5bl2_lab.AutoSize = true;
            this.DO5bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO5bl2_lab.Location = new System.Drawing.Point(433, 159);
            this.DO5bl2_lab.Name = "DO5bl2_lab";
            this.DO5bl2_lab.Size = new System.Drawing.Size(38, 14);
            this.DO5bl2_lab.TabIndex = 42;
            this.DO5bl2_lab.Text = "DO 5";
            // 
            // DO6bl2_lab
            // 
            this.DO6bl2_lab.AutoSize = true;
            this.DO6bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO6bl2_lab.Location = new System.Drawing.Point(433, 190);
            this.DO6bl2_lab.Name = "DO6bl2_lab";
            this.DO6bl2_lab.Size = new System.Drawing.Size(38, 14);
            this.DO6bl2_lab.TabIndex = 43;
            this.DO6bl2_lab.Text = "DO 6";
            // 
            // DO7bl2_combo
            // 
            this.DO7bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO7bl2_combo.FormattingEnabled = true;
            this.DO7bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO7bl2_combo.Location = new System.Drawing.Point(46, 219);
            this.DO7bl2_combo.Name = "DO7bl2_combo";
            this.DO7bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.DO7bl2_combo.TabIndex = 37;
            this.DO7bl2_combo.SelectedIndexChanged += new System.EventHandler(this.DO7bl2_combo_SelectedIndexChanged);
            // 
            // DOblock2_header
            // 
            this.DOblock2_header.AutoSize = true;
            this.DOblock2_header.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DOblock2_header.Location = new System.Drawing.Point(8, 4);
            this.DOblock2_header.Name = "DOblock2_header";
            this.DOblock2_header.Size = new System.Drawing.Size(142, 14);
            this.DOblock2_header.TabIndex = 24;
            this.DOblock2_header.Text = "Блок расширения 2";
            // 
            // DO7_bl2Label
            // 
            this.DO7_bl2Label.AutoSize = true;
            this.DO7_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO7_bl2Label.Location = new System.Drawing.Point(5, 221);
            this.DO7_bl2Label.Name = "DO7_bl2Label";
            this.DO7_bl2Label.Size = new System.Drawing.Size(38, 14);
            this.DO7_bl2Label.TabIndex = 36;
            this.DO7_bl2Label.Text = "DO 7";
            // 
            // DO1bl2_combo
            // 
            this.DO1bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO1bl2_combo.FormattingEnabled = true;
            this.DO1bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO1bl2_combo.Location = new System.Drawing.Point(47, 33);
            this.DO1bl2_combo.Name = "DO1bl2_combo";
            this.DO1bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.DO1bl2_combo.TabIndex = 30;
            this.DO1bl2_combo.SelectedIndexChanged += new System.EventHandler(this.DO1bl2_combo_SelectedIndexChanged);
            // 
            // DO6bl2_combo
            // 
            this.DO6bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO6bl2_combo.FormattingEnabled = true;
            this.DO6bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO6bl2_combo.Location = new System.Drawing.Point(46, 188);
            this.DO6bl2_combo.Name = "DO6bl2_combo";
            this.DO6bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.DO6bl2_combo.TabIndex = 35;
            this.DO6bl2_combo.SelectedIndexChanged += new System.EventHandler(this.DO6bl2_combo_SelectedIndexChanged);
            // 
            // DO1_bl2Label
            // 
            this.DO1_bl2Label.AutoSize = true;
            this.DO1_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO1_bl2Label.Location = new System.Drawing.Point(5, 35);
            this.DO1_bl2Label.Name = "DO1_bl2Label";
            this.DO1_bl2Label.Size = new System.Drawing.Size(38, 14);
            this.DO1_bl2Label.TabIndex = 24;
            this.DO1_bl2Label.Text = "DO 1";
            // 
            // DO5bl2_combo
            // 
            this.DO5bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO5bl2_combo.FormattingEnabled = true;
            this.DO5bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO5bl2_combo.Location = new System.Drawing.Point(46, 157);
            this.DO5bl2_combo.Name = "DO5bl2_combo";
            this.DO5bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.DO5bl2_combo.TabIndex = 34;
            this.DO5bl2_combo.SelectedIndexChanged += new System.EventHandler(this.DO5bl2_combo_SelectedIndexChanged);
            // 
            // DO2_bl2Label
            // 
            this.DO2_bl2Label.AutoSize = true;
            this.DO2_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO2_bl2Label.Location = new System.Drawing.Point(5, 66);
            this.DO2_bl2Label.Name = "DO2_bl2Label";
            this.DO2_bl2Label.Size = new System.Drawing.Size(38, 14);
            this.DO2_bl2Label.TabIndex = 25;
            this.DO2_bl2Label.Text = "DO 2";
            // 
            // DO4bl2_combo
            // 
            this.DO4bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO4bl2_combo.FormattingEnabled = true;
            this.DO4bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO4bl2_combo.Location = new System.Drawing.Point(46, 126);
            this.DO4bl2_combo.Name = "DO4bl2_combo";
            this.DO4bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.DO4bl2_combo.TabIndex = 33;
            this.DO4bl2_combo.SelectedIndexChanged += new System.EventHandler(this.DO4bl2_combo_SelectedIndexChanged);
            // 
            // DO3_bl2Label
            // 
            this.DO3_bl2Label.AutoSize = true;
            this.DO3_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO3_bl2Label.Location = new System.Drawing.Point(5, 97);
            this.DO3_bl2Label.Name = "DO3_bl2Label";
            this.DO3_bl2Label.Size = new System.Drawing.Size(38, 14);
            this.DO3_bl2Label.TabIndex = 26;
            this.DO3_bl2Label.Text = "DO 3";
            // 
            // DO3bl2_combo
            // 
            this.DO3bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO3bl2_combo.FormattingEnabled = true;
            this.DO3bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO3bl2_combo.Location = new System.Drawing.Point(46, 95);
            this.DO3bl2_combo.Name = "DO3bl2_combo";
            this.DO3bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.DO3bl2_combo.TabIndex = 32;
            this.DO3bl2_combo.SelectedIndexChanged += new System.EventHandler(this.DO3bl2_combo_SelectedIndexChanged);
            // 
            // DO4_bl2Label
            // 
            this.DO4_bl2Label.AutoSize = true;
            this.DO4_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO4_bl2Label.Location = new System.Drawing.Point(5, 128);
            this.DO4_bl2Label.Name = "DO4_bl2Label";
            this.DO4_bl2Label.Size = new System.Drawing.Size(38, 14);
            this.DO4_bl2Label.TabIndex = 27;
            this.DO4_bl2Label.Text = "DO 4";
            // 
            // DO2bl2_combo
            // 
            this.DO2bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO2bl2_combo.FormattingEnabled = true;
            this.DO2bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO2bl2_combo.Location = new System.Drawing.Point(46, 64);
            this.DO2bl2_combo.Name = "DO2bl2_combo";
            this.DO2bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.DO2bl2_combo.TabIndex = 31;
            this.DO2bl2_combo.SelectedIndexChanged += new System.EventHandler(this.DO2bl2_combo_SelectedIndexChanged);
            // 
            // DO5_bl2Label
            // 
            this.DO5_bl2Label.AutoSize = true;
            this.DO5_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO5_bl2Label.Location = new System.Drawing.Point(5, 159);
            this.DO5_bl2Label.Name = "DO5_bl2Label";
            this.DO5_bl2Label.Size = new System.Drawing.Size(38, 14);
            this.DO5_bl2Label.TabIndex = 28;
            this.DO5_bl2Label.Text = "DO 5";
            // 
            // DO6_bl2Label
            // 
            this.DO6_bl2Label.AutoSize = true;
            this.DO6_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO6_bl2Label.Location = new System.Drawing.Point(5, 190);
            this.DO6_bl2Label.Name = "DO6_bl2Label";
            this.DO6_bl2Label.Size = new System.Drawing.Size(38, 14);
            this.DO6_bl2Label.TabIndex = 29;
            this.DO6_bl2Label.Text = "DO 6";
            // 
            // block1_DOpanel
            // 
            this.block1_DOpanel.Controls.Add(this.DO8bl1_lab);
            this.block1_DOpanel.Controls.Add(this.DO8bl1_combo);
            this.block1_DOpanel.Controls.Add(this.DO8_bl1Label);
            this.block1_DOpanel.Controls.Add(this.DO7bl1_lab);
            this.block1_DOpanel.Controls.Add(this.DO1bl1_lab);
            this.block1_DOpanel.Controls.Add(this.DO2bl1_lab);
            this.block1_DOpanel.Controls.Add(this.DO3bl1_lab);
            this.block1_DOpanel.Controls.Add(this.DO4bl1_lab);
            this.block1_DOpanel.Controls.Add(this.DO5bl1_lab);
            this.block1_DOpanel.Controls.Add(this.DO6bl1_lab);
            this.block1_DOpanel.Controls.Add(this.DO7bl1_combo);
            this.block1_DOpanel.Controls.Add(this.DOblock1_header);
            this.block1_DOpanel.Controls.Add(this.DO7_bl1Label);
            this.block1_DOpanel.Controls.Add(this.DO1bl1_combo);
            this.block1_DOpanel.Controls.Add(this.DO6bl1_combo);
            this.block1_DOpanel.Controls.Add(this.DO1_bl1Label);
            this.block1_DOpanel.Controls.Add(this.DO5bl1_combo);
            this.block1_DOpanel.Controls.Add(this.DO2_bl1Label);
            this.block1_DOpanel.Controls.Add(this.DO4bl1_combo);
            this.block1_DOpanel.Controls.Add(this.DO3_bl1Label);
            this.block1_DOpanel.Controls.Add(this.DO3bl1_combo);
            this.block1_DOpanel.Controls.Add(this.DO4_bl1Label);
            this.block1_DOpanel.Controls.Add(this.DO2bl1_combo);
            this.block1_DOpanel.Controls.Add(this.DO5_bl1Label);
            this.block1_DOpanel.Controls.Add(this.DO6_bl1Label);
            this.block1_DOpanel.Enabled = false;
            this.block1_DOpanel.Location = new System.Drawing.Point(6, 239);
            this.block1_DOpanel.Name = "block1_DOpanel";
            this.block1_DOpanel.Size = new System.Drawing.Size(514, 289);
            this.block1_DOpanel.TabIndex = 23;
            this.block1_DOpanel.Visible = false;
            // 
            // DO8bl1_lab
            // 
            this.DO8bl1_lab.AutoSize = true;
            this.DO8bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO8bl1_lab.Location = new System.Drawing.Point(433, 252);
            this.DO8bl1_lab.Name = "DO8bl1_lab";
            this.DO8bl1_lab.Size = new System.Drawing.Size(38, 14);
            this.DO8bl1_lab.TabIndex = 47;
            this.DO8bl1_lab.Text = "DO 8";
            // 
            // DO8bl1_combo
            // 
            this.DO8bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO8bl1_combo.Enabled = false;
            this.DO8bl1_combo.FormattingEnabled = true;
            this.DO8bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO8bl1_combo.Location = new System.Drawing.Point(46, 250);
            this.DO8bl1_combo.Name = "DO8bl1_combo";
            this.DO8bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.DO8bl1_combo.TabIndex = 46;
            this.DO8bl1_combo.Visible = false;
            this.DO8bl1_combo.SelectedIndexChanged += new System.EventHandler(this.DO8bl1_combo_SelectedIndexChanged);
            // 
            // DO8_bl1Label
            // 
            this.DO8_bl1Label.AutoSize = true;
            this.DO8_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO8_bl1Label.Location = new System.Drawing.Point(5, 252);
            this.DO8_bl1Label.Name = "DO8_bl1Label";
            this.DO8_bl1Label.Size = new System.Drawing.Size(38, 14);
            this.DO8_bl1Label.TabIndex = 45;
            this.DO8_bl1Label.Text = "DO 8";
            this.DO8_bl1Label.Visible = false;
            // 
            // DO7bl1_lab
            // 
            this.DO7bl1_lab.AutoSize = true;
            this.DO7bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO7bl1_lab.Location = new System.Drawing.Point(433, 221);
            this.DO7bl1_lab.Name = "DO7bl1_lab";
            this.DO7bl1_lab.Size = new System.Drawing.Size(38, 14);
            this.DO7bl1_lab.TabIndex = 44;
            this.DO7bl1_lab.Text = "DO 7";
            // 
            // DO1bl1_lab
            // 
            this.DO1bl1_lab.AutoSize = true;
            this.DO1bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO1bl1_lab.Location = new System.Drawing.Point(433, 35);
            this.DO1bl1_lab.Name = "DO1bl1_lab";
            this.DO1bl1_lab.Size = new System.Drawing.Size(38, 14);
            this.DO1bl1_lab.TabIndex = 38;
            this.DO1bl1_lab.Text = "DO 1";
            // 
            // DO2bl1_lab
            // 
            this.DO2bl1_lab.AutoSize = true;
            this.DO2bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO2bl1_lab.Location = new System.Drawing.Point(433, 66);
            this.DO2bl1_lab.Name = "DO2bl1_lab";
            this.DO2bl1_lab.Size = new System.Drawing.Size(38, 14);
            this.DO2bl1_lab.TabIndex = 39;
            this.DO2bl1_lab.Text = "DO 2";
            // 
            // DO3bl1_lab
            // 
            this.DO3bl1_lab.AutoSize = true;
            this.DO3bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO3bl1_lab.Location = new System.Drawing.Point(433, 97);
            this.DO3bl1_lab.Name = "DO3bl1_lab";
            this.DO3bl1_lab.Size = new System.Drawing.Size(38, 14);
            this.DO3bl1_lab.TabIndex = 40;
            this.DO3bl1_lab.Text = "DO 3";
            // 
            // DO4bl1_lab
            // 
            this.DO4bl1_lab.AutoSize = true;
            this.DO4bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO4bl1_lab.Location = new System.Drawing.Point(433, 128);
            this.DO4bl1_lab.Name = "DO4bl1_lab";
            this.DO4bl1_lab.Size = new System.Drawing.Size(38, 14);
            this.DO4bl1_lab.TabIndex = 41;
            this.DO4bl1_lab.Text = "DO 4";
            // 
            // DO5bl1_lab
            // 
            this.DO5bl1_lab.AutoSize = true;
            this.DO5bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO5bl1_lab.Location = new System.Drawing.Point(433, 159);
            this.DO5bl1_lab.Name = "DO5bl1_lab";
            this.DO5bl1_lab.Size = new System.Drawing.Size(38, 14);
            this.DO5bl1_lab.TabIndex = 42;
            this.DO5bl1_lab.Text = "DO 5";
            // 
            // DO6bl1_lab
            // 
            this.DO6bl1_lab.AutoSize = true;
            this.DO6bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO6bl1_lab.Location = new System.Drawing.Point(433, 190);
            this.DO6bl1_lab.Name = "DO6bl1_lab";
            this.DO6bl1_lab.Size = new System.Drawing.Size(38, 14);
            this.DO6bl1_lab.TabIndex = 43;
            this.DO6bl1_lab.Text = "DO 6";
            // 
            // DO7bl1_combo
            // 
            this.DO7bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO7bl1_combo.Enabled = false;
            this.DO7bl1_combo.FormattingEnabled = true;
            this.DO7bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO7bl1_combo.Location = new System.Drawing.Point(46, 219);
            this.DO7bl1_combo.Name = "DO7bl1_combo";
            this.DO7bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.DO7bl1_combo.TabIndex = 37;
            this.DO7bl1_combo.Visible = false;
            this.DO7bl1_combo.SelectedIndexChanged += new System.EventHandler(this.DO7bl1_combo_SelectedIndexChanged);
            // 
            // DOblock1_header
            // 
            this.DOblock1_header.AutoSize = true;
            this.DOblock1_header.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DOblock1_header.Location = new System.Drawing.Point(8, 4);
            this.DOblock1_header.Name = "DOblock1_header";
            this.DOblock1_header.Size = new System.Drawing.Size(142, 14);
            this.DOblock1_header.TabIndex = 24;
            this.DOblock1_header.Text = "Блок расширения 1";
            // 
            // DO7_bl1Label
            // 
            this.DO7_bl1Label.AutoSize = true;
            this.DO7_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO7_bl1Label.Location = new System.Drawing.Point(5, 221);
            this.DO7_bl1Label.Name = "DO7_bl1Label";
            this.DO7_bl1Label.Size = new System.Drawing.Size(38, 14);
            this.DO7_bl1Label.TabIndex = 36;
            this.DO7_bl1Label.Text = "DO 7";
            this.DO7_bl1Label.Visible = false;
            // 
            // DO1bl1_combo
            // 
            this.DO1bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO1bl1_combo.Enabled = false;
            this.DO1bl1_combo.FormattingEnabled = true;
            this.DO1bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO1bl1_combo.Location = new System.Drawing.Point(47, 33);
            this.DO1bl1_combo.Name = "DO1bl1_combo";
            this.DO1bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.DO1bl1_combo.TabIndex = 30;
            this.DO1bl1_combo.Visible = false;
            this.DO1bl1_combo.SelectedIndexChanged += new System.EventHandler(this.DO1bl1_combo_SelectedIndexChanged);
            // 
            // DO6bl1_combo
            // 
            this.DO6bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO6bl1_combo.Enabled = false;
            this.DO6bl1_combo.FormattingEnabled = true;
            this.DO6bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO6bl1_combo.Location = new System.Drawing.Point(46, 188);
            this.DO6bl1_combo.Name = "DO6bl1_combo";
            this.DO6bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.DO6bl1_combo.TabIndex = 35;
            this.DO6bl1_combo.Visible = false;
            this.DO6bl1_combo.SelectedIndexChanged += new System.EventHandler(this.DO6bl1_combo_SelectedIndexChanged);
            // 
            // DO1_bl1Label
            // 
            this.DO1_bl1Label.AutoSize = true;
            this.DO1_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO1_bl1Label.Location = new System.Drawing.Point(5, 35);
            this.DO1_bl1Label.Name = "DO1_bl1Label";
            this.DO1_bl1Label.Size = new System.Drawing.Size(38, 14);
            this.DO1_bl1Label.TabIndex = 24;
            this.DO1_bl1Label.Text = "DO 1";
            this.DO1_bl1Label.Visible = false;
            // 
            // DO5bl1_combo
            // 
            this.DO5bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO5bl1_combo.Enabled = false;
            this.DO5bl1_combo.FormattingEnabled = true;
            this.DO5bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO5bl1_combo.Location = new System.Drawing.Point(46, 157);
            this.DO5bl1_combo.Name = "DO5bl1_combo";
            this.DO5bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.DO5bl1_combo.TabIndex = 34;
            this.DO5bl1_combo.Visible = false;
            this.DO5bl1_combo.SelectedIndexChanged += new System.EventHandler(this.DO5bl1_combo_SelectedIndexChanged);
            // 
            // DO2_bl1Label
            // 
            this.DO2_bl1Label.AutoSize = true;
            this.DO2_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO2_bl1Label.Location = new System.Drawing.Point(5, 66);
            this.DO2_bl1Label.Name = "DO2_bl1Label";
            this.DO2_bl1Label.Size = new System.Drawing.Size(38, 14);
            this.DO2_bl1Label.TabIndex = 25;
            this.DO2_bl1Label.Text = "DO 2";
            this.DO2_bl1Label.Visible = false;
            // 
            // DO4bl1_combo
            // 
            this.DO4bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO4bl1_combo.Enabled = false;
            this.DO4bl1_combo.FormattingEnabled = true;
            this.DO4bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO4bl1_combo.Location = new System.Drawing.Point(46, 126);
            this.DO4bl1_combo.Name = "DO4bl1_combo";
            this.DO4bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.DO4bl1_combo.TabIndex = 33;
            this.DO4bl1_combo.Visible = false;
            this.DO4bl1_combo.SelectedIndexChanged += new System.EventHandler(this.DO4bl1_combo_SelectedIndexChanged);
            // 
            // DO3_bl1Label
            // 
            this.DO3_bl1Label.AutoSize = true;
            this.DO3_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO3_bl1Label.Location = new System.Drawing.Point(5, 97);
            this.DO3_bl1Label.Name = "DO3_bl1Label";
            this.DO3_bl1Label.Size = new System.Drawing.Size(38, 14);
            this.DO3_bl1Label.TabIndex = 26;
            this.DO3_bl1Label.Text = "DO 3";
            this.DO3_bl1Label.Visible = false;
            // 
            // DO3bl1_combo
            // 
            this.DO3bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO3bl1_combo.Enabled = false;
            this.DO3bl1_combo.FormattingEnabled = true;
            this.DO3bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO3bl1_combo.Location = new System.Drawing.Point(46, 95);
            this.DO3bl1_combo.Name = "DO3bl1_combo";
            this.DO3bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.DO3bl1_combo.TabIndex = 32;
            this.DO3bl1_combo.Visible = false;
            this.DO3bl1_combo.SelectedIndexChanged += new System.EventHandler(this.DO3bl1_combo_SelectedIndexChanged);
            // 
            // DO4_bl1Label
            // 
            this.DO4_bl1Label.AutoSize = true;
            this.DO4_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO4_bl1Label.Location = new System.Drawing.Point(5, 128);
            this.DO4_bl1Label.Name = "DO4_bl1Label";
            this.DO4_bl1Label.Size = new System.Drawing.Size(38, 14);
            this.DO4_bl1Label.TabIndex = 27;
            this.DO4_bl1Label.Text = "DO 4";
            this.DO4_bl1Label.Visible = false;
            // 
            // DO2bl1_combo
            // 
            this.DO2bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DO2bl1_combo.Enabled = false;
            this.DO2bl1_combo.FormattingEnabled = true;
            this.DO2bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.DO2bl1_combo.Location = new System.Drawing.Point(46, 64);
            this.DO2bl1_combo.Name = "DO2bl1_combo";
            this.DO2bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.DO2bl1_combo.TabIndex = 31;
            this.DO2bl1_combo.Visible = false;
            this.DO2bl1_combo.SelectedIndexChanged += new System.EventHandler(this.DO2bl1_combo_SelectedIndexChanged);
            // 
            // DO5_bl1Label
            // 
            this.DO5_bl1Label.AutoSize = true;
            this.DO5_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO5_bl1Label.Location = new System.Drawing.Point(5, 159);
            this.DO5_bl1Label.Name = "DO5_bl1Label";
            this.DO5_bl1Label.Size = new System.Drawing.Size(38, 14);
            this.DO5_bl1Label.TabIndex = 28;
            this.DO5_bl1Label.Text = "DO 5";
            this.DO5_bl1Label.Visible = false;
            // 
            // DO6_bl1Label
            // 
            this.DO6_bl1Label.AutoSize = true;
            this.DO6_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DO6_bl1Label.Location = new System.Drawing.Point(5, 190);
            this.DO6_bl1Label.Name = "DO6_bl1Label";
            this.DO6_bl1Label.Size = new System.Drawing.Size(38, 14);
            this.DO6_bl1Label.TabIndex = 29;
            this.DO6_bl1Label.Text = "DO 6";
            this.DO6_bl1Label.Visible = false;
            // 
            // tabAO
            // 
            this.tabAO.AutoScroll = true;
            this.tabAO.Controls.Add(this.plk_AOpanel);
            this.tabAO.Controls.Add(this.block3_AOpanel);
            this.tabAO.Controls.Add(this.block2_AOpanel);
            this.tabAO.Controls.Add(this.block1_AOpanel);
            this.tabAO.Location = new System.Drawing.Point(4, 22);
            this.tabAO.Name = "tabAO";
            this.tabAO.Size = new System.Drawing.Size(721, 762);
            this.tabAO.TabIndex = 2;
            this.tabAO.Text = "AO сигналы";
            // 
            // plk_AOpanel
            // 
            this.plk_AOpanel.Controls.Add(this.AOplk_header);
            this.plk_AOpanel.Controls.Add(this.AO1_combo);
            this.plk_AOpanel.Controls.Add(this.AO3_lab);
            this.plk_AOpanel.Controls.Add(this.AO1_plkLabel);
            this.plk_AOpanel.Controls.Add(this.AO2_lab);
            this.plk_AOpanel.Controls.Add(this.AO1_lab);
            this.plk_AOpanel.Controls.Add(this.AO3_combo);
            this.plk_AOpanel.Controls.Add(this.AO2_plkLabel);
            this.plk_AOpanel.Controls.Add(this.AO3_plkLabel);
            this.plk_AOpanel.Controls.Add(this.AO2_combo);
            this.plk_AOpanel.Location = new System.Drawing.Point(6, 11);
            this.plk_AOpanel.Name = "plk_AOpanel";
            this.plk_AOpanel.Size = new System.Drawing.Size(511, 134);
            this.plk_AOpanel.TabIndex = 33;
            // 
            // AOplk_header
            // 
            this.AOplk_header.AutoSize = true;
            this.AOplk_header.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AOplk_header.Location = new System.Drawing.Point(8, 4);
            this.AOplk_header.Name = "AOplk_header";
            this.AOplk_header.Size = new System.Drawing.Size(88, 14);
            this.AOplk_header.TabIndex = 2;
            this.AOplk_header.Text = "Контроллер";
            // 
            // AO1_combo
            // 
            this.AO1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AO1_combo.FormattingEnabled = true;
            this.AO1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.AO1_combo.Location = new System.Drawing.Point(50, 33);
            this.AO1_combo.Name = "AO1_combo";
            this.AO1_combo.Size = new System.Drawing.Size(380, 21);
            this.AO1_combo.TabIndex = 17;
            this.AO1_combo.SelectedIndexChanged += new System.EventHandler(this.AO1_combo_SelectedIndexChanged);
            // 
            // AO3_lab
            // 
            this.AO3_lab.AutoSize = true;
            this.AO3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO3_lab.Location = new System.Drawing.Point(436, 97);
            this.AO3_lab.Name = "AO3_lab";
            this.AO3_lab.Size = new System.Drawing.Size(37, 14);
            this.AO3_lab.TabIndex = 31;
            this.AO3_lab.Text = "AO 3";
            // 
            // AO1_plkLabel
            // 
            this.AO1_plkLabel.AutoSize = true;
            this.AO1_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO1_plkLabel.Location = new System.Drawing.Point(8, 35);
            this.AO1_plkLabel.Name = "AO1_plkLabel";
            this.AO1_plkLabel.Size = new System.Drawing.Size(37, 14);
            this.AO1_plkLabel.TabIndex = 12;
            this.AO1_plkLabel.Text = "AO 1";
            // 
            // AO2_lab
            // 
            this.AO2_lab.AutoSize = true;
            this.AO2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO2_lab.Location = new System.Drawing.Point(436, 66);
            this.AO2_lab.Name = "AO2_lab";
            this.AO2_lab.Size = new System.Drawing.Size(37, 14);
            this.AO2_lab.TabIndex = 30;
            this.AO2_lab.Text = "AO 2";
            // 
            // AO1_lab
            // 
            this.AO1_lab.AutoSize = true;
            this.AO1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO1_lab.Location = new System.Drawing.Point(436, 35);
            this.AO1_lab.Name = "AO1_lab";
            this.AO1_lab.Size = new System.Drawing.Size(37, 14);
            this.AO1_lab.TabIndex = 29;
            this.AO1_lab.Text = "AO 1";
            // 
            // AO3_combo
            // 
            this.AO3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AO3_combo.FormattingEnabled = true;
            this.AO3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.AO3_combo.Location = new System.Drawing.Point(49, 95);
            this.AO3_combo.Name = "AO3_combo";
            this.AO3_combo.Size = new System.Drawing.Size(380, 21);
            this.AO3_combo.TabIndex = 19;
            this.AO3_combo.SelectedIndexChanged += new System.EventHandler(this.AO3_combo_SelectedIndexChanged);
            // 
            // AO2_plkLabel
            // 
            this.AO2_plkLabel.AutoSize = true;
            this.AO2_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO2_plkLabel.Location = new System.Drawing.Point(8, 66);
            this.AO2_plkLabel.Name = "AO2_plkLabel";
            this.AO2_plkLabel.Size = new System.Drawing.Size(37, 14);
            this.AO2_plkLabel.TabIndex = 13;
            this.AO2_plkLabel.Text = "AO 2";
            // 
            // AO3_plkLabel
            // 
            this.AO3_plkLabel.AutoSize = true;
            this.AO3_plkLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO3_plkLabel.Location = new System.Drawing.Point(8, 97);
            this.AO3_plkLabel.Name = "AO3_plkLabel";
            this.AO3_plkLabel.Size = new System.Drawing.Size(37, 14);
            this.AO3_plkLabel.TabIndex = 14;
            this.AO3_plkLabel.Text = "AO 3";
            // 
            // AO2_combo
            // 
            this.AO2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AO2_combo.FormattingEnabled = true;
            this.AO2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.AO2_combo.Location = new System.Drawing.Point(49, 64);
            this.AO2_combo.Name = "AO2_combo";
            this.AO2_combo.Size = new System.Drawing.Size(380, 21);
            this.AO2_combo.TabIndex = 18;
            this.AO2_combo.SelectedIndexChanged += new System.EventHandler(this.AO2_combo_SelectedIndexChanged);
            // 
            // block3_AOpanel
            // 
            this.block3_AOpanel.Controls.Add(this.AO2bl3_lab);
            this.block3_AOpanel.Controls.Add(this.AO2bl3_combo);
            this.block3_AOpanel.Controls.Add(this.AO2_bl3Label);
            this.block3_AOpanel.Controls.Add(this.AO1bl3_lab);
            this.block3_AOpanel.Controls.Add(this.AOblock3_header);
            this.block3_AOpanel.Controls.Add(this.AO1bl3_combo);
            this.block3_AOpanel.Controls.Add(this.AO1_bl3Label);
            this.block3_AOpanel.Enabled = false;
            this.block3_AOpanel.Location = new System.Drawing.Point(6, 346);
            this.block3_AOpanel.Name = "block3_AOpanel";
            this.block3_AOpanel.Size = new System.Drawing.Size(511, 99);
            this.block3_AOpanel.TabIndex = 32;
            this.block3_AOpanel.Visible = false;
            // 
            // AO2bl3_lab
            // 
            this.AO2bl3_lab.AutoSize = true;
            this.AO2bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO2bl3_lab.Location = new System.Drawing.Point(436, 66);
            this.AO2bl3_lab.Name = "AO2bl3_lab";
            this.AO2bl3_lab.Size = new System.Drawing.Size(37, 14);
            this.AO2bl3_lab.TabIndex = 36;
            this.AO2bl3_lab.Text = "AO 2";
            // 
            // AO2bl3_combo
            // 
            this.AO2bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AO2bl3_combo.FormattingEnabled = true;
            this.AO2bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.AO2bl3_combo.Location = new System.Drawing.Point(50, 64);
            this.AO2bl3_combo.Name = "AO2bl3_combo";
            this.AO2bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.AO2bl3_combo.TabIndex = 35;
            this.AO2bl3_combo.SelectedIndexChanged += new System.EventHandler(this.AO2bl3_combo_SelectedIndexChanged);
            // 
            // AO2_bl3Label
            // 
            this.AO2_bl3Label.AutoSize = true;
            this.AO2_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO2_bl3Label.Location = new System.Drawing.Point(8, 66);
            this.AO2_bl3Label.Name = "AO2_bl3Label";
            this.AO2_bl3Label.Size = new System.Drawing.Size(37, 14);
            this.AO2_bl3Label.TabIndex = 34;
            this.AO2_bl3Label.Text = "AO 2";
            // 
            // AO1bl3_lab
            // 
            this.AO1bl3_lab.AutoSize = true;
            this.AO1bl3_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO1bl3_lab.Location = new System.Drawing.Point(436, 35);
            this.AO1bl3_lab.Name = "AO1bl3_lab";
            this.AO1bl3_lab.Size = new System.Drawing.Size(37, 14);
            this.AO1bl3_lab.TabIndex = 33;
            this.AO1bl3_lab.Text = "AO 1";
            // 
            // AOblock3_header
            // 
            this.AOblock3_header.AutoSize = true;
            this.AOblock3_header.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AOblock3_header.Location = new System.Drawing.Point(8, 4);
            this.AOblock3_header.Name = "AOblock3_header";
            this.AOblock3_header.Size = new System.Drawing.Size(229, 14);
            this.AOblock3_header.TabIndex = 31;
            this.AOblock3_header.Text = "Блок расширения 3 - M72E12RB";
            // 
            // AO1bl3_combo
            // 
            this.AO1bl3_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AO1bl3_combo.FormattingEnabled = true;
            this.AO1bl3_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.AO1bl3_combo.Location = new System.Drawing.Point(50, 33);
            this.AO1bl3_combo.Name = "AO1bl3_combo";
            this.AO1bl3_combo.Size = new System.Drawing.Size(380, 21);
            this.AO1bl3_combo.TabIndex = 32;
            this.AO1bl3_combo.SelectedIndexChanged += new System.EventHandler(this.AO1bl3_combo_SelectedIndexChanged);
            // 
            // AO1_bl3Label
            // 
            this.AO1_bl3Label.AutoSize = true;
            this.AO1_bl3Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO1_bl3Label.Location = new System.Drawing.Point(8, 35);
            this.AO1_bl3Label.Name = "AO1_bl3Label";
            this.AO1_bl3Label.Size = new System.Drawing.Size(37, 14);
            this.AO1_bl3Label.TabIndex = 31;
            this.AO1_bl3Label.Text = "AO 1";
            // 
            // block2_AOpanel
            // 
            this.block2_AOpanel.Controls.Add(this.AO1bl2_lab);
            this.block2_AOpanel.Controls.Add(this.AO2bl2_lab);
            this.block2_AOpanel.Controls.Add(this.AO1bl2_combo);
            this.block2_AOpanel.Controls.Add(this.AO2bl2_combo);
            this.block2_AOpanel.Controls.Add(this.AOblock2_header);
            this.block2_AOpanel.Controls.Add(this.AO1_bl2Label);
            this.block2_AOpanel.Controls.Add(this.AO2_bl2Label);
            this.block2_AOpanel.Enabled = false;
            this.block2_AOpanel.Location = new System.Drawing.Point(6, 246);
            this.block2_AOpanel.Name = "block2_AOpanel";
            this.block2_AOpanel.Size = new System.Drawing.Size(511, 99);
            this.block2_AOpanel.TabIndex = 28;
            this.block2_AOpanel.Visible = false;
            // 
            // AO1bl2_lab
            // 
            this.AO1bl2_lab.AutoSize = true;
            this.AO1bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO1bl2_lab.Location = new System.Drawing.Point(436, 35);
            this.AO1bl2_lab.Name = "AO1bl2_lab";
            this.AO1bl2_lab.Size = new System.Drawing.Size(37, 14);
            this.AO1bl2_lab.TabIndex = 28;
            this.AO1bl2_lab.Text = "AO 1";
            // 
            // AO2bl2_lab
            // 
            this.AO2bl2_lab.AutoSize = true;
            this.AO2bl2_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO2bl2_lab.Location = new System.Drawing.Point(436, 66);
            this.AO2bl2_lab.Name = "AO2bl2_lab";
            this.AO2bl2_lab.Size = new System.Drawing.Size(37, 14);
            this.AO2bl2_lab.TabIndex = 29;
            this.AO2bl2_lab.Text = "AO 2";
            // 
            // AO1bl2_combo
            // 
            this.AO1bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AO1bl2_combo.FormattingEnabled = true;
            this.AO1bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.AO1bl2_combo.Location = new System.Drawing.Point(50, 33);
            this.AO1bl2_combo.Name = "AO1bl2_combo";
            this.AO1bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.AO1bl2_combo.TabIndex = 25;
            this.AO1bl2_combo.SelectedIndexChanged += new System.EventHandler(this.AO1bl2_combo_SelectedIndexChanged);
            // 
            // AO2bl2_combo
            // 
            this.AO2bl2_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AO2bl2_combo.FormattingEnabled = true;
            this.AO2bl2_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.AO2bl2_combo.Location = new System.Drawing.Point(49, 64);
            this.AO2bl2_combo.Name = "AO2bl2_combo";
            this.AO2bl2_combo.Size = new System.Drawing.Size(380, 21);
            this.AO2bl2_combo.TabIndex = 26;
            this.AO2bl2_combo.SelectedIndexChanged += new System.EventHandler(this.AO2bl2_combo_SelectedIndexChanged);
            // 
            // AOblock2_header
            // 
            this.AOblock2_header.AutoSize = true;
            this.AOblock2_header.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AOblock2_header.Location = new System.Drawing.Point(8, 4);
            this.AOblock2_header.Name = "AOblock2_header";
            this.AOblock2_header.Size = new System.Drawing.Size(229, 14);
            this.AOblock2_header.TabIndex = 21;
            this.AOblock2_header.Text = "Блок расширения 2 - M72E12RB";
            // 
            // AO1_bl2Label
            // 
            this.AO1_bl2Label.AutoSize = true;
            this.AO1_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO1_bl2Label.Location = new System.Drawing.Point(8, 35);
            this.AO1_bl2Label.Name = "AO1_bl2Label";
            this.AO1_bl2Label.Size = new System.Drawing.Size(37, 14);
            this.AO1_bl2Label.TabIndex = 22;
            this.AO1_bl2Label.Text = "AO 1";
            // 
            // AO2_bl2Label
            // 
            this.AO2_bl2Label.AutoSize = true;
            this.AO2_bl2Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO2_bl2Label.Location = new System.Drawing.Point(8, 66);
            this.AO2_bl2Label.Name = "AO2_bl2Label";
            this.AO2_bl2Label.Size = new System.Drawing.Size(37, 14);
            this.AO2_bl2Label.TabIndex = 23;
            this.AO2_bl2Label.Text = "AO 2";
            // 
            // block1_AOpanel
            // 
            this.block1_AOpanel.Controls.Add(this.AO1bl1_lab);
            this.block1_AOpanel.Controls.Add(this.AO2bl1_lab);
            this.block1_AOpanel.Controls.Add(this.AO1bl1_combo);
            this.block1_AOpanel.Controls.Add(this.AO2bl1_combo);
            this.block1_AOpanel.Controls.Add(this.AOblock1_header);
            this.block1_AOpanel.Controls.Add(this.AO1_bl1Label);
            this.block1_AOpanel.Controls.Add(this.AO2_bl1Label);
            this.block1_AOpanel.Enabled = false;
            this.block1_AOpanel.Location = new System.Drawing.Point(6, 146);
            this.block1_AOpanel.Name = "block1_AOpanel";
            this.block1_AOpanel.Size = new System.Drawing.Size(511, 99);
            this.block1_AOpanel.TabIndex = 20;
            this.block1_AOpanel.Visible = false;
            // 
            // AO1bl1_lab
            // 
            this.AO1bl1_lab.AutoSize = true;
            this.AO1bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO1bl1_lab.Location = new System.Drawing.Point(436, 35);
            this.AO1bl1_lab.Name = "AO1bl1_lab";
            this.AO1bl1_lab.Size = new System.Drawing.Size(37, 14);
            this.AO1bl1_lab.TabIndex = 28;
            this.AO1bl1_lab.Text = "AO 1";
            // 
            // AO2bl1_lab
            // 
            this.AO2bl1_lab.AutoSize = true;
            this.AO2bl1_lab.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO2bl1_lab.Location = new System.Drawing.Point(436, 66);
            this.AO2bl1_lab.Name = "AO2bl1_lab";
            this.AO2bl1_lab.Size = new System.Drawing.Size(37, 14);
            this.AO2bl1_lab.TabIndex = 29;
            this.AO2bl1_lab.Text = "AO 2";
            // 
            // AO1bl1_combo
            // 
            this.AO1bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AO1bl1_combo.FormattingEnabled = true;
            this.AO1bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.AO1bl1_combo.Location = new System.Drawing.Point(50, 33);
            this.AO1bl1_combo.Name = "AO1bl1_combo";
            this.AO1bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.AO1bl1_combo.TabIndex = 25;
            this.AO1bl1_combo.SelectedIndexChanged += new System.EventHandler(this.AO1bl1_combo_SelectedIndexChanged);
            // 
            // AO2bl1_combo
            // 
            this.AO2bl1_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AO2bl1_combo.FormattingEnabled = true;
            this.AO2bl1_combo.Items.AddRange(new object[] {
            "Не выбрано"});
            this.AO2bl1_combo.Location = new System.Drawing.Point(49, 64);
            this.AO2bl1_combo.Name = "AO2bl1_combo";
            this.AO2bl1_combo.Size = new System.Drawing.Size(380, 21);
            this.AO2bl1_combo.TabIndex = 26;
            this.AO2bl1_combo.SelectedIndexChanged += new System.EventHandler(this.AO2bl1_combo_SelectedIndexChanged);
            // 
            // AOblock1_header
            // 
            this.AOblock1_header.AutoSize = true;
            this.AOblock1_header.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AOblock1_header.Location = new System.Drawing.Point(8, 4);
            this.AOblock1_header.Name = "AOblock1_header";
            this.AOblock1_header.Size = new System.Drawing.Size(229, 14);
            this.AOblock1_header.TabIndex = 21;
            this.AOblock1_header.Text = "Блок расширения 1 - M72E12RB";
            // 
            // AO1_bl1Label
            // 
            this.AO1_bl1Label.AutoSize = true;
            this.AO1_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO1_bl1Label.Location = new System.Drawing.Point(8, 35);
            this.AO1_bl1Label.Name = "AO1_bl1Label";
            this.AO1_bl1Label.Size = new System.Drawing.Size(37, 14);
            this.AO1_bl1Label.TabIndex = 22;
            this.AO1_bl1Label.Text = "AO 1";
            // 
            // AO2_bl1Label
            // 
            this.AO2_bl1Label.AutoSize = true;
            this.AO2_bl1Label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AO2_bl1Label.Location = new System.Drawing.Point(8, 66);
            this.AO2_bl1Label.Name = "AO2_bl1Label";
            this.AO2_bl1Label.Size = new System.Drawing.Size(37, 14);
            this.AO2_bl1Label.TabIndex = 23;
            this.AO2_bl1Label.Text = "AO 2";
            // 
            // tabCmdWord
            // 
            this.tabCmdWord.BackColor = System.Drawing.SystemColors.Control;
            this.tabCmdWord.Controls.Add(this.cmdWordsTextBox);
            this.tabCmdWord.Location = new System.Drawing.Point(4, 22);
            this.tabCmdWord.Name = "tabCmdWord";
            this.tabCmdWord.Size = new System.Drawing.Size(721, 762);
            this.tabCmdWord.TabIndex = 5;
            this.tabCmdWord.Text = "Командные слова";
            // 
            // cmdWordsTextBox
            // 
            this.cmdWordsTextBox.Location = new System.Drawing.Point(7, 5);
            this.cmdWordsTextBox.Name = "cmdWordsTextBox";
            this.cmdWordsTextBox.ReadOnly = true;
            this.cmdWordsTextBox.Size = new System.Drawing.Size(443, 454);
            this.cmdWordsTextBox.TabIndex = 0;
            this.cmdWordsTextBox.Text = "";
            // 
            // backSignalsButton
            // 
            this.backSignalsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.backSignalsButton.BackColor = System.Drawing.Color.DarkGreen;
            this.backSignalsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backSignalsButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.backSignalsButton.ForeColor = System.Drawing.Color.White;
            this.backSignalsButton.Location = new System.Drawing.Point(10, 26);
            this.backSignalsButton.Name = "backSignalsButton";
            this.backSignalsButton.Size = new System.Drawing.Size(94, 27);
            this.backSignalsButton.TabIndex = 58;
            this.backSignalsButton.Text = "НАЗАД";
            this.backSignalsButton.UseVisualStyleBackColor = false;
            this.backSignalsButton.Click += new System.EventHandler(this.BackSignalsButton_Click);
            // 
            // helpPanel
            // 
            this.helpPanel.Controls.Add(this.showHintCheck);
            this.helpPanel.Controls.Add(this.PDF_manual);
            this.helpPanel.Controls.Add(this.label140);
            this.helpPanel.Controls.Add(this.linkModeronWeb);
            this.helpPanel.Controls.Add(this.backHelpButton);
            this.helpPanel.Location = new System.Drawing.Point(3, 978);
            this.helpPanel.Name = "helpPanel";
            this.helpPanel.Size = new System.Drawing.Size(746, 28);
            this.helpPanel.TabIndex = 17;
            this.helpPanel.Visible = false;
            // 
            // showHintCheck
            // 
            this.showHintCheck.AutoSize = true;
            this.showHintCheck.Checked = true;
            this.showHintCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showHintCheck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.showHintCheck.Location = new System.Drawing.Point(28, 45);
            this.showHintCheck.Name = "showHintCheck";
            this.showHintCheck.Size = new System.Drawing.Size(267, 18);
            this.showHintCheck.TabIndex = 67;
            this.showHintCheck.Text = "Показывать всплывающие подсказки";
            this.showHintCheck.UseVisualStyleBackColor = true;
            this.showHintCheck.CheckedChanged += new System.EventHandler(this.ShowHintCheck_CheckedChanged);
            // 
            // PDF_manual
            // 
            this.PDF_manual.Enabled = true;
            this.PDF_manual.Location = new System.Drawing.Point(28, 76);
            this.PDF_manual.Name = "PDF_manual";
            this.PDF_manual.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("PDF_manual.OcxState")));
            this.PDF_manual.Size = new System.Drawing.Size(710, 346);
            this.PDF_manual.TabIndex = 66;
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label140.Location = new System.Drawing.Point(25, 18);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(359, 16);
            this.label140.TabIndex = 64;
            this.label140.Text = "РУКОВОДСТВО ПО ПРОГРАММЕ MODERON HVAC";
            // 
            // linkModeronWeb
            // 
            this.linkModeronWeb.AutoSize = true;
            this.linkModeronWeb.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkModeronWeb.Location = new System.Drawing.Point(418, 18);
            this.linkModeronWeb.Name = "linkModeronWeb";
            this.linkModeronWeb.Size = new System.Drawing.Size(165, 14);
            this.linkModeronWeb.TabIndex = 63;
            this.linkModeronWeb.TabStop = true;
            this.linkModeronWeb.Text = "Сайт компании Moderon";
            this.linkModeronWeb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkModeronWeb_LinkClicked);
            // 
            // backHelpButton
            // 
            this.backHelpButton.BackColor = System.Drawing.Color.DarkGreen;
            this.backHelpButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backHelpButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.backHelpButton.ForeColor = System.Drawing.Color.White;
            this.backHelpButton.Location = new System.Drawing.Point(638, 9);
            this.backHelpButton.Name = "backHelpButton";
            this.backHelpButton.Size = new System.Drawing.Size(94, 27);
            this.backHelpButton.TabIndex = 62;
            this.backHelpButton.Text = "НАЗАД";
            this.backHelpButton.UseVisualStyleBackColor = false;
            this.backHelpButton.Click += new System.EventHandler(this.BackHelpButton_Click);
            // 
            // label_comboSysType
            // 
            this.label_comboSysType.AutoSize = true;
            this.label_comboSysType.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_comboSysType.Location = new System.Drawing.Point(19, 47);
            this.label_comboSysType.Name = "label_comboSysType";
            this.label_comboSysType.Size = new System.Drawing.Size(112, 16);
            this.label_comboSysType.TabIndex = 19;
            this.label_comboSysType.Text = "ТИП СИСТЕМЫ";
            // 
            // loadCanPanel
            // 
            this.loadCanPanel.Controls.Add(this.progressBarWrite);
            this.loadCanPanel.Controls.Add(this.processWriteLabel);
            this.loadCanPanel.Controls.Add(this.backConnectLabel);
            this.loadCanPanel.Controls.Add(this.dataMatchPLC_label);
            this.loadCanPanel.Controls.Add(this.readCanButton);
            this.loadCanPanel.Controls.Add(this.loadCanButton);
            this.loadCanPanel.Controls.Add(this.refreshCanPorts);
            this.loadCanPanel.Controls.Add(this.canSelectBox);
            this.loadCanPanel.Controls.Add(this.connectCanLabel);
            this.loadCanPanel.Controls.Add(this.label181);
            this.loadCanPanel.Controls.Add(this.backCanPanelButton);
            this.loadCanPanel.Controls.Add(this.writeCanTextBox);
            this.loadCanPanel.Controls.Add(this.label180);
            this.loadCanPanel.Controls.Add(this.dataCanTextBox);
            this.loadCanPanel.Controls.Add(this.label179);
            this.loadCanPanel.Controls.Add(this.connectPlkBtn);
            this.loadCanPanel.Controls.Add(this.parityCanCombo);
            this.loadCanPanel.Controls.Add(this.label178);
            this.loadCanPanel.Controls.Add(this.label177);
            this.loadCanPanel.Controls.Add(this.speedCanCombo);
            this.loadCanPanel.Controls.Add(this.label176);
            this.loadCanPanel.Controls.Add(this.canAddressBox);
            this.loadCanPanel.Controls.Add(this.label174);
            this.loadCanPanel.Controls.Add(this.label173);
            this.loadCanPanel.Controls.Add(this.netOptionLabel);
            this.loadCanPanel.Location = new System.Drawing.Point(12, 239);
            this.loadCanPanel.Name = "loadCanPanel";
            this.loadCanPanel.Size = new System.Drawing.Size(749, 568);
            this.loadCanPanel.TabIndex = 45;
            this.loadCanPanel.Visible = false;
            // 
            // processWriteLabel
            // 
            this.processWriteLabel.AutoSize = true;
            this.processWriteLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.processWriteLabel.Location = new System.Drawing.Point(33, 384);
            this.processWriteLabel.Name = "processWriteLabel";
            this.processWriteLabel.Size = new System.Drawing.Size(202, 16);
            this.processWriteLabel.TabIndex = 78;
            this.processWriteLabel.Text = "Идёт запись в контроллер...";
            this.processWriteLabel.Visible = false;
            // 
            // progressBarWrite
            // 
            this.progressBarWrite.ForeColor = System.Drawing.Color.DarkGreen;
            this.progressBarWrite.Location = new System.Drawing.Point(31, 409);
            this.progressBarWrite.Name = "progressBarWrite";
            this.progressBarWrite.Size = new System.Drawing.Size(254, 23);
            this.progressBarWrite.TabIndex = 77;
            this.progressBarWrite.Visible = false;
            // 
            // backConnectLabel
            // 
            this.backConnectLabel.AutoSize = true;
            this.backConnectLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.backConnectLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.backConnectLabel.Location = new System.Drawing.Point(28, 478);
            this.backConnectLabel.Name = "backConnectLabel";
            this.backConnectLabel.Size = new System.Drawing.Size(312, 13);
            this.backConnectLabel.TabIndex = 79;
            this.backConnectLabel.Text = "Требуется закрыть соединение для возврата";
            this.backConnectLabel.Visible = false;
            // 
            // dataMatchPLC_label
            // 
            this.dataMatchPLC_label.AutoSize = true;
            this.dataMatchPLC_label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataMatchPLC_label.ForeColor = System.Drawing.Color.Red;
            this.dataMatchPLC_label.Location = new System.Drawing.Point(510, 528);
            this.dataMatchPLC_label.Name = "dataMatchPLC_label";
            this.dataMatchPLC_label.Size = new System.Drawing.Size(204, 14);
            this.dataMatchPLC_label.TabIndex = 76;
            this.dataMatchPLC_label.Text = "Данные в ПЛК не совпадают";
            // 
            // readCanButton
            // 
            this.readCanButton.BackColor = System.Drawing.Color.DarkGreen;
            this.readCanButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.readCanButton.Enabled = false;
            this.readCanButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.readCanButton.ForeColor = System.Drawing.Color.White;
            this.readCanButton.Location = new System.Drawing.Point(30, 335);
            this.readCanButton.Name = "readCanButton";
            this.readCanButton.Size = new System.Drawing.Size(209, 33);
            this.readCanButton.TabIndex = 75;
            this.readCanButton.Text = "ЧИТАТЬ ДАННЫЕ ИЗ ПЛК";
            this.readCanButton.UseVisualStyleBackColor = false;
            this.readCanButton.Click += new System.EventHandler(this.ReadCanButton_Click);
            // 
            // loadCanButton
            // 
            this.loadCanButton.BackColor = System.Drawing.Color.DarkGreen;
            this.loadCanButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loadCanButton.Enabled = false;
            this.loadCanButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadCanButton.ForeColor = System.Drawing.Color.White;
            this.loadCanButton.Location = new System.Drawing.Point(30, 295);
            this.loadCanButton.Name = "loadCanButton";
            this.loadCanButton.Size = new System.Drawing.Size(209, 33);
            this.loadCanButton.TabIndex = 74;
            this.loadCanButton.Text = "ЗАГРУЗИТЬ ДАННЫЕ В ПЛК";
            this.loadCanButton.UseVisualStyleBackColor = false;
            this.loadCanButton.Click += new System.EventHandler(this.LoadCanButton_Click);
            // 
            // refreshCanPorts
            // 
            this.refreshCanPorts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshCanPorts.Image = global::Moderon.Properties.Resources.refresh;
            this.refreshCanPorts.Location = new System.Drawing.Point(254, 55);
            this.refreshCanPorts.Name = "refreshCanPorts";
            this.refreshCanPorts.Size = new System.Drawing.Size(30, 30);
            this.refreshCanPorts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.refreshCanPorts.TabIndex = 64;
            this.refreshCanPorts.TabStop = false;
            this.refreshCanPorts.Click += new System.EventHandler(this.RefreshCanPorts_Click);
            // 
            // canSelectBox
            // 
            this.canSelectBox.BackColor = System.Drawing.Color.White;
            this.canSelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.canSelectBox.FormattingEnabled = true;
            this.canSelectBox.Location = new System.Drawing.Point(120, 60);
            this.canSelectBox.Name = "canSelectBox";
            this.canSelectBox.Size = new System.Drawing.Size(121, 21);
            this.canSelectBox.TabIndex = 73;
            // 
            // connectCanLabel
            // 
            this.connectCanLabel.AutoSize = true;
            this.connectCanLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectCanLabel.ForeColor = System.Drawing.Color.Red;
            this.connectCanLabel.Location = new System.Drawing.Point(121, 258);
            this.connectCanLabel.Name = "connectCanLabel";
            this.connectCanLabel.Size = new System.Drawing.Size(127, 16);
            this.connectCanLabel.TabIndex = 65;
            this.connectCanLabel.Text = "Нет соединения";
            // 
            // label181
            // 
            this.label181.AutoSize = true;
            this.label181.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label181.Location = new System.Drawing.Point(400, 528);
            this.label181.Name = "label181";
            this.label181.Size = new System.Drawing.Size(108, 14);
            this.label181.TabIndex = 71;
            this.label181.Text = "Статус данных:";
            // 
            // backCanPanelButton
            // 
            this.backCanPanelButton.BackColor = System.Drawing.Color.DarkGreen;
            this.backCanPanelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backCanPanelButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.backCanPanelButton.ForeColor = System.Drawing.Color.White;
            this.backCanPanelButton.Location = new System.Drawing.Point(28, 500);
            this.backCanPanelButton.Name = "backCanPanelButton";
            this.backCanPanelButton.Size = new System.Drawing.Size(94, 27);
            this.backCanPanelButton.TabIndex = 70;
            this.backCanPanelButton.Text = "НАЗАД";
            this.backCanPanelButton.UseVisualStyleBackColor = false;
            this.backCanPanelButton.Click += new System.EventHandler(this.BackCanPanelButton_Click);
            // 
            // writeCanTextBox
            // 
            this.writeCanTextBox.Location = new System.Drawing.Point(404, 291);
            this.writeCanTextBox.Multiline = true;
            this.writeCanTextBox.Name = "writeCanTextBox";
            this.writeCanTextBox.ReadOnly = true;
            this.writeCanTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.writeCanTextBox.Size = new System.Drawing.Size(315, 226);
            this.writeCanTextBox.TabIndex = 69;
            // 
            // label180
            // 
            this.label180.AutoSize = true;
            this.label180.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label180.Location = new System.Drawing.Point(401, 266);
            this.label180.Name = "label180";
            this.label180.Size = new System.Drawing.Size(184, 14);
            this.label180.TabIndex = 68;
            this.label180.Text = "Данные для загрузки в ПЛК";
            // 
            // dataCanTextBox
            // 
            this.dataCanTextBox.Location = new System.Drawing.Point(404, 28);
            this.dataCanTextBox.Multiline = true;
            this.dataCanTextBox.Name = "dataCanTextBox";
            this.dataCanTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataCanTextBox.Size = new System.Drawing.Size(315, 226);
            this.dataCanTextBox.TabIndex = 67;
            // 
            // label179
            // 
            this.label179.AutoSize = true;
            this.label179.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label179.Location = new System.Drawing.Point(401, 7);
            this.label179.Name = "label179";
            this.label179.Size = new System.Drawing.Size(98, 14);
            this.label179.TabIndex = 65;
            this.label179.Text = "Данные в ПЛК";
            // 
            // connectPlkBtn
            // 
            this.connectPlkBtn.BackColor = System.Drawing.Color.DarkGreen;
            this.connectPlkBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.connectPlkBtn.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectPlkBtn.ForeColor = System.Drawing.Color.White;
            this.connectPlkBtn.Location = new System.Drawing.Point(30, 207);
            this.connectPlkBtn.Name = "connectPlkBtn";
            this.connectPlkBtn.Size = new System.Drawing.Size(209, 33);
            this.connectPlkBtn.TabIndex = 65;
            this.connectPlkBtn.Text = "УСТАНОВИТЬ СОЕДИНЕНИЕ";
            this.connectPlkBtn.UseVisualStyleBackColor = false;
            this.connectPlkBtn.Click += new System.EventHandler(this.ConnectPlkBtn_Click);
            // 
            // parityCanCombo
            // 
            this.parityCanCombo.BackColor = System.Drawing.Color.White;
            this.parityCanCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.parityCanCombo.FormattingEnabled = true;
            this.parityCanCombo.Items.AddRange(new object[] {
            "Even",
            "Odd",
            "None"});
            this.parityCanCombo.Location = new System.Drawing.Point(120, 168);
            this.parityCanCombo.Name = "parityCanCombo";
            this.parityCanCombo.Size = new System.Drawing.Size(121, 21);
            this.parityCanCombo.TabIndex = 55;
            // 
            // label178
            // 
            this.label178.AutoSize = true;
            this.label178.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label178.Location = new System.Drawing.Point(28, 170);
            this.label178.Name = "label178";
            this.label178.Size = new System.Drawing.Size(67, 14);
            this.label178.TabIndex = 54;
            this.label178.Text = "Четность";
            // 
            // label177
            // 
            this.label177.AutoSize = true;
            this.label177.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label177.Location = new System.Drawing.Point(28, 259);
            this.label177.Name = "label177";
            this.label177.Size = new System.Drawing.Size(86, 14);
            this.label177.TabIndex = 53;
            this.label177.Text = "Статус ПЛК:";
            // 
            // speedCanCombo
            // 
            this.speedCanCombo.BackColor = System.Drawing.Color.White;
            this.speedCanCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.speedCanCombo.FormattingEnabled = true;
            this.speedCanCombo.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400"});
            this.speedCanCombo.Location = new System.Drawing.Point(120, 132);
            this.speedCanCombo.Name = "speedCanCombo";
            this.speedCanCombo.Size = new System.Drawing.Size(121, 21);
            this.speedCanCombo.TabIndex = 52;
            // 
            // label176
            // 
            this.label176.AutoSize = true;
            this.label176.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label176.Location = new System.Drawing.Point(28, 134);
            this.label176.Name = "label176";
            this.label176.Size = new System.Drawing.Size(67, 14);
            this.label176.TabIndex = 51;
            this.label176.Text = "Скорость";
            // 
            // canAddressBox
            // 
            this.canAddressBox.Location = new System.Drawing.Point(120, 96);
            this.canAddressBox.MaxLength = 2;
            this.canAddressBox.Name = "canAddressBox";
            this.canAddressBox.Size = new System.Drawing.Size(122, 21);
            this.canAddressBox.TabIndex = 50;
            this.canAddressBox.Text = "1";
            this.canAddressBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.canAddressBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CanAddressBox_KeyPress);
            // 
            // label174
            // 
            this.label174.AutoSize = true;
            this.label174.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label174.Location = new System.Drawing.Point(28, 98);
            this.label174.Name = "label174";
            this.label174.Size = new System.Drawing.Size(45, 14);
            this.label174.TabIndex = 49;
            this.label174.Text = "Адрес";
            // 
            // label173
            // 
            this.label173.AutoSize = true;
            this.label173.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label173.Location = new System.Drawing.Point(28, 62);
            this.label173.Name = "label173";
            this.label173.Size = new System.Drawing.Size(71, 14);
            this.label173.TabIndex = 47;
            this.label173.Text = "COM порт";
            // 
            // netOptionLabel
            // 
            this.netOptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.netOptionLabel.AutoSize = true;
            this.netOptionLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.netOptionLabel.Location = new System.Drawing.Point(28, 17);
            this.netOptionLabel.Name = "netOptionLabel";
            this.netOptionLabel.Size = new System.Drawing.Size(200, 16);
            this.netOptionLabel.TabIndex = 46;
            this.netOptionLabel.Text = "НАСТРОЙКА СОЕДИНЕНИЯ";
            // 
            // comboPlkType
            // 
            this.comboPlkType.BackColor = System.Drawing.Color.DarkGreen;
            this.comboPlkType.DisplayMember = "Moderon M72 Mini";
            this.comboPlkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPlkType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboPlkType.ForeColor = System.Drawing.Color.White;
            this.comboPlkType.FormattingEnabled = true;
            this.comboPlkType.Items.AddRange(new object[] {
            "Moderon M72 Mini",
            "Moderon M72 Optimized"});
            this.comboPlkType.Location = new System.Drawing.Point(624, 46);
            this.comboPlkType.Name = "comboPlkType";
            this.comboPlkType.Size = new System.Drawing.Size(213, 21);
            this.comboPlkType.TabIndex = 46;
            this.comboPlkType.Visible = false;
            this.comboPlkType.SelectedIndexChanged += new System.EventHandler(this.ComboPlkType_SelectedIndexChanged);
            // 
            // panelBlocks
            // 
            this.panelBlocks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBlocks.Controls.Add(this.M72E16NA_label);
            this.panelBlocks.Controls.Add(this.M72E12RA_label);
            this.panelBlocks.Controls.Add(this.M72E08RA_label);
            this.panelBlocks.Controls.Add(this.M72E12RB_label);
            this.panelBlocks.Controls.Add(this.label61);
            this.panelBlocks.Location = new System.Drawing.Point(785, 450);
            this.panelBlocks.Name = "panelBlocks";
            this.panelBlocks.Size = new System.Drawing.Size(182, 160);
            this.panelBlocks.TabIndex = 47;
            this.panelBlocks.Visible = false;
            // 
            // M72E16NA_label
            // 
            this.M72E16NA_label.AutoSize = true;
            this.M72E16NA_label.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.M72E16NA_label.Location = new System.Drawing.Point(13, 115);
            this.M72E16NA_label.Name = "M72E16NA_label";
            this.M72E16NA_label.Size = new System.Drawing.Size(67, 13);
            this.M72E16NA_label.TabIndex = 19;
            this.M72E16NA_label.Text = "M72E12RB";
            this.M72E16NA_label.Visible = false;
            // 
            // M72E12RA_label
            // 
            this.M72E12RA_label.AutoSize = true;
            this.M72E12RA_label.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.M72E12RA_label.Location = new System.Drawing.Point(13, 65);
            this.M72E12RA_label.Name = "M72E12RA_label";
            this.M72E12RA_label.Size = new System.Drawing.Size(67, 13);
            this.M72E12RA_label.TabIndex = 18;
            this.M72E12RA_label.Text = "M72E12RA";
            this.M72E12RA_label.Visible = false;
            // 
            // M72E08RA_label
            // 
            this.M72E08RA_label.AutoSize = true;
            this.M72E08RA_label.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.M72E08RA_label.Location = new System.Drawing.Point(13, 40);
            this.M72E08RA_label.Name = "M72E08RA_label";
            this.M72E08RA_label.Size = new System.Drawing.Size(67, 13);
            this.M72E08RA_label.TabIndex = 17;
            this.M72E08RA_label.Text = "M72E08RA";
            this.M72E08RA_label.Visible = false;
            // 
            // M72E12RB_label
            // 
            this.M72E12RB_label.AutoSize = true;
            this.M72E12RB_label.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.M72E12RB_label.Location = new System.Drawing.Point(13, 90);
            this.M72E12RB_label.Name = "M72E12RB_label";
            this.M72E12RB_label.Size = new System.Drawing.Size(67, 13);
            this.M72E12RB_label.TabIndex = 16;
            this.M72E12RB_label.Text = "M72E12RB";
            this.M72E12RB_label.Visible = false;
            // 
            // label61
            // 
            this.label61.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label61.Location = new System.Drawing.Point(8, 7);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(163, 16);
            this.label61.TabIndex = 15;
            this.label61.Text = "БЛОКИ РАСШИРЕНИЯ";
            // 
            // pic_signalsReady
            // 
            this.pic_signalsReady.Image = global::Moderon.Properties.Resources.green_check;
            this.pic_signalsReady.Location = new System.Drawing.Point(785, 75);
            this.pic_signalsReady.Name = "pic_signalsReady";
            this.pic_signalsReady.Size = new System.Drawing.Size(40, 40);
            this.pic_signalsReady.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_signalsReady.TabIndex = 63;
            this.pic_signalsReady.TabStop = false;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLogo.Image = global::Moderon.Properties.Resources.logo_moderon;
            this.pictureBoxLogo.InitialImage = global::Moderon.Properties.Resources.logo_moderon;
            this.pictureBoxLogo.Location = new System.Drawing.Point(843, 25);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(136, 42);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 2;
            this.pictureBoxLogo.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 1061);
            this.Controls.Add(this.pic_signalsReady);
            this.Controls.Add(this.panelBlocks);
            this.Controls.Add(this.comboPlkType);
            this.Controls.Add(this.loadCanPanel);
            this.Controls.Add(this.label_comboSysType);
            this.Controls.Add(this.helpPanel);
            this.Controls.Add(this.signalsPanel);
            this.Controls.Add(this.formSignalsButton);
            this.Controls.Add(this.loadModbusPanel);
            this.Controls.Add(this.panelElements);
            this.Controls.Add(this.comboSysType);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.mainPage);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(845, 620);
            this.Name = "Form1";
            this.Text = "MODERON HVAC";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainPage.ResumeLayout(false);
            this.sensorsPage.ResumeLayout(false);
            this.sensorsPanel.ResumeLayout(false);
            this.sensorsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sensorPicture)).EndInit();
            this.fanPage.ResumeLayout(false);
            this.outFanPanel.ResumeLayout(false);
            this.outFanPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fanPicture2)).EndInit();
            this.prFanPanel.ResumeLayout(false);
            this.prFanPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fanPicture1)).EndInit();
            this.filterPage.ResumeLayout(false);
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filterPicture)).EndInit();
            this.outFilterPanel.ResumeLayout(false);
            this.outFilterPanel.PerformLayout();
            this.dampPage.ResumeLayout(false);
            this.dampPanel.ResumeLayout(false);
            this.dampPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dampPicture)).EndInit();
            this.outDampPanel.ResumeLayout(false);
            this.outDampPanel.PerformLayout();
            this.heatPage.ResumeLayout(false);
            this.heatPanel.ResumeLayout(false);
            this.heatPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heatPicture)).EndInit();
            this.elHeatPanel.ResumeLayout(false);
            this.elHeatPanel.PerformLayout();
            this.watHeatPanel.ResumeLayout(false);
            this.watHeatPanel.PerformLayout();
            this.coolPage.ResumeLayout(false);
            this.coolPanel.ResumeLayout(false);
            this.coolPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coolPicture)).EndInit();
            this.watCoolPanel.ResumeLayout(false);
            this.watCoolPanel.PerformLayout();
            this.frCoolPanel.ResumeLayout(false);
            this.frCoolPanel.PerformLayout();
            this.humidPage.ResumeLayout(false);
            this.humidPanel.ResumeLayout(false);
            this.humidPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.humidPicture)).EndInit();
            this.cellHumidPanel.ResumeLayout(false);
            this.cellHumidPanel.PerformLayout();
            this.steamHumidPanel.ResumeLayout(false);
            this.steamHumidPanel.PerformLayout();
            this.recircPage.ResumeLayout(false);
            this.recircPanel.ResumeLayout(false);
            this.recircPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recircPicture)).EndInit();
            this.recupPage.ResumeLayout(false);
            this.recupPanel.ResumeLayout(false);
            this.recupPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recupPicture)).EndInit();
            this.defRecupSensPanel.ResumeLayout(false);
            this.defRecupSensPanel.PerformLayout();
            this.plastRecupPanel.ResumeLayout(false);
            this.plastRecupPanel.PerformLayout();
            this.glikRecupPanel.ResumeLayout(false);
            this.glikRecupPanel.PerformLayout();
            this.rotorRecupPanel.ResumeLayout(false);
            this.rotorRecupPanel.PerformLayout();
            this.addHeatPage.ResumeLayout(false);
            this.secHeatPanel.ResumeLayout(false);
            this.secHeatPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heatAddPicture)).EndInit();
            this.elAddHeatPanel.ResumeLayout(false);
            this.elAddHeatPanel.PerformLayout();
            this.watAddHeatPanel.ResumeLayout(false);
            this.watAddHeatPanel.PerformLayout();
            this.panelElements.ResumeLayout(false);
            this.panelElements.PerformLayout();
            this.loadModbusPanel.ResumeLayout(false);
            this.loadModbusPanel.PerformLayout();
            this.signalsPanel.ResumeLayout(false);
            this.signalsPanel.PerformLayout();
            this.tabControlSignals.ResumeLayout(false);
            this.tabUI.ResumeLayout(false);
            this.block3_UIpanel.ResumeLayout(false);
            this.block3_UIpanel.PerformLayout();
            this.block2_UIpanel.ResumeLayout(false);
            this.block2_UIpanel.PerformLayout();
            this.block1_UIpanel.ResumeLayout(false);
            this.block1_UIpanel.PerformLayout();
            this.plk_UIpanel.ResumeLayout(false);
            this.plk_UIpanel.PerformLayout();
            this.tabDO.ResumeLayout(false);
            this.plk_DOpanel.ResumeLayout(false);
            this.plk_DOpanel.PerformLayout();
            this.block3_DOpanel.ResumeLayout(false);
            this.block3_DOpanel.PerformLayout();
            this.block2_DOpanel.ResumeLayout(false);
            this.block2_DOpanel.PerformLayout();
            this.block1_DOpanel.ResumeLayout(false);
            this.block1_DOpanel.PerformLayout();
            this.tabAO.ResumeLayout(false);
            this.plk_AOpanel.ResumeLayout(false);
            this.plk_AOpanel.PerformLayout();
            this.block3_AOpanel.ResumeLayout(false);
            this.block3_AOpanel.PerformLayout();
            this.block2_AOpanel.ResumeLayout(false);
            this.block2_AOpanel.PerformLayout();
            this.block1_AOpanel.ResumeLayout(false);
            this.block1_AOpanel.PerformLayout();
            this.tabCmdWord.ResumeLayout(false);
            this.helpPanel.ResumeLayout(false);
            this.helpPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PDF_manual)).EndInit();
            this.loadCanPanel.ResumeLayout(false);
            this.loadCanPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refreshCanPorts)).EndInit();
            this.panelBlocks.ResumeLayout(false);
            this.panelBlocks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_signalsReady)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_exit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_help;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabControl mainPage;
        private System.Windows.Forms.TabPage fanPage;
        private System.Windows.Forms.TabPage heatPage;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.PictureBox fanPicture1;
        private System.Windows.Forms.ComboBox comboSysType;
        private System.Windows.Forms.Panel prFanPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox heaterCheck;
        private System.Windows.Forms.CheckBox coolerCheck;
        private System.Windows.Forms.TabPage coolPage;
        private System.Windows.Forms.TabPage humidPage;
        private System.Windows.Forms.TabPage recircPage;
        private System.Windows.Forms.TabPage recupPage;
        private System.Windows.Forms.CheckBox humidCheck;
        private System.Windows.Forms.CheckBox recircCheck;
        private System.Windows.Forms.CheckBox recupCheck;
        private System.Windows.Forms.Panel panelElements;  
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Panel heatPanel;
        private System.Windows.Forms.PictureBox heatPicture;
        private System.Windows.Forms.Panel coolPanel;
        private System.Windows.Forms.PictureBox coolPicture;
        private System.Windows.Forms.CheckBox filterCheck;
        private System.Windows.Forms.CheckBox dampCheck;
        private System.Windows.Forms.TabPage filterPage;
        private System.Windows.Forms.TabPage dampPage;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.PictureBox filterPicture;
        private System.Windows.Forms.Panel dampPanel;
        private System.Windows.Forms.PictureBox dampPicture;
        private System.Windows.Forms.Panel humidPanel;
        private System.Windows.Forms.PictureBox humidPicture;
        private System.Windows.Forms.Panel recircPanel;
        private System.Windows.Forms.PictureBox recircPicture;
        private System.Windows.Forms.Panel recupPanel;
        private System.Windows.Forms.PictureBox recupPicture;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel outFanPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox fanPicture2;
        private System.Windows.Forms.ComboBox filterPrCombo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel outFilterPanel;
        private System.Windows.Forms.ComboBox filterOutCombo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox prFanPowCombo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox prFanControlCombo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox prFanFC_check;
        private System.Windows.Forms.CheckBox prFanPSCheck;
        private System.Windows.Forms.ComboBox outFanControlCombo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox outFanFC_check;
        private System.Windows.Forms.CheckBox outFanPSCheck;
        private System.Windows.Forms.ComboBox outFanPowCombo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox prDampPowCombo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox heatPrDampCheck;
        private System.Windows.Forms.CheckBox confPrDampCheck;
        private System.Windows.Forms.Panel outDampPanel;
        private System.Windows.Forms.CheckBox heatOutDampCheck;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox confOutDampCheck;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox outDampPowCombo;
        private System.Windows.Forms.CheckBox outDampCheck;
        private System.Windows.Forms.ComboBox heatTypeCombo;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel watHeatPanel;
        private System.Windows.Forms.CheckBox confHeatPumpCheck;
        private System.Windows.Forms.ComboBox powPumpCombo;
        private System.Windows.Forms.CheckBox TF_heaterCheck;
        private System.Windows.Forms.Panel elHeatPanel;
        private System.Windows.Forms.ComboBox elHeatStagesCombo;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox elHeatPowBox;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox thermSwitchCombo;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel frCoolPanel;
        private System.Windows.Forms.ComboBox coolTypeCombo;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox frCoolStagesCombo;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel watCoolPanel;
        private System.Windows.Forms.CheckBox analogCoolCheck;
        private System.Windows.Forms.ComboBox powWatCoolCombo;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.CheckBox alarmFrCoolCheck;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox powOutFanBox;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox powPrFanBox;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ComboBox humidTypeCombo;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Panel cellHumidPanel;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Panel steamHumidPanel;
        private System.Windows.Forms.CheckBox alarmHumidCheck;
        private System.Windows.Forms.CheckBox analogHumCheck;
        private System.Windows.Forms.CheckBox startHumidCheck;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.CheckBox powPumpHumidCheck;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.ComboBox recircPowCombo;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.ComboBox recupTypeCombo;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Panel rotorRecupPanel;
        private System.Windows.Forms.ComboBox rotorPowCombo;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox powRotRecBox;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Panel plastRecupPanel;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label labelResPrFan_2;
        private System.Windows.Forms.TextBox powPrResFanBox;
        private System.Windows.Forms.Label labelResPrFan;
        private System.Windows.Forms.CheckBox checkResPrFan;
        private System.Windows.Forms.Label labelResOutFan_2;
        private System.Windows.Forms.TextBox powOutResFanBox;
        private System.Windows.Forms.Label labelResOutFan;
        private System.Windows.Forms.CheckBox checkResOutFan;
        private System.Windows.Forms.Panel glikRecupPanel;
        private System.Windows.Forms.CheckBox analogGlikRecCheck;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.TabPage addHeatPage;
        private System.Windows.Forms.Panel secHeatPanel;
        private System.Windows.Forms.Panel elAddHeatPanel;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox elAddHeatPowBox;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.ComboBox thermAddSwitchCombo;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.ComboBox elHeatAddStagesCombo;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Panel watAddHeatPanel;
        private System.Windows.Forms.CheckBox confAddHeatPumpCheck;
        private System.Windows.Forms.ComboBox powPumpAddCombo;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.CheckBox TF_addHeaterCheck;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.ComboBox heatAddTypeCombo;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.PictureBox heatAddPicture;
        private System.Windows.Forms.CheckBox pumpGlicRecCheck;
        private System.Windows.Forms.CheckBox recircAOSigCheck;
        private System.Windows.Forms.CheckBox analogSigHeatCheck;
        private System.Windows.Forms.CheckBox checkBox27;
        private System.Windows.Forms.TabPage sensorsPage;
        private System.Windows.Forms.Panel sensorsPanel;
        private System.Windows.Forms.CheckBox outChanSensCheck;
        private System.Windows.Forms.CheckBox roomHumSensCheck;
        private System.Windows.Forms.CheckBox chanHumSensCheck;
        private System.Windows.Forms.CheckBox roomTempSensCheck;
        private System.Windows.Forms.CheckBox prChanSensCheck;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.ComboBox bypassPlastCombo;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.CheckBox analogRotRecCheck;
        private System.Windows.Forms.CheckBox startRotRecCheck;
        private System.Windows.Forms.CheckBox outSigAlarmRotRecCheck;
        private System.Windows.Forms.ComboBox firstStHeatCombo;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.ComboBox firstStAddHeatCombo;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.CheckBox outFanThermoCheck;
        private System.Windows.Forms.CheckBox prFanThermoCheck;
        private System.Windows.Forms.CheckBox curDefOutFanCheck;
        private System.Windows.Forms.CheckBox curDefPrFanCheck;
        private System.Windows.Forms.Panel defRecupSensPanel;
        private System.Windows.Forms.CheckBox recDefPsCheck;
        private System.Windows.Forms.CheckBox recDefTempCheck;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.CheckBox watSensHeatCheck;
        private System.Windows.Forms.CheckBox sensWatAddHeatCheck;
        private System.Windows.Forms.CheckBox dehumModeCheck;
        private System.Windows.Forms.CheckBox pumpAddHeatCheck;
        private System.Windows.Forms.CheckBox addHeatCheck;
        private System.Windows.Forms.PictureBox sensorPicture;
        private System.Windows.Forms.Panel loadModbusPanel;
        private System.Windows.Forms.Button backOptionsButton;
        private System.Windows.Forms.TextBox netPortBox;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.TextBox ipAddressBox;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label connectLabel;
        private System.Windows.Forms.Button readNetBtn;
        private System.Windows.Forms.TextBox dataNetTextBox;
        private System.Windows.Forms.Button writeNetButton;
        private System.Windows.Forms.TextBox writeNetTextBox;
        private System.Windows.Forms.Button formNetButton;
        private System.Windows.Forms.CheckBox outdoorChanSensCheck;
        private System.Windows.Forms.CheckBox thermoCoolerCheck;
        private System.Windows.Forms.Button formSignalsButton;
        private System.Windows.Forms.Panel signalsPanel;
        private System.Windows.Forms.Button backSignalsButton;
        private System.Windows.Forms.TabControl tabControlSignals;
        private System.Windows.Forms.TabPage tabAO;
        private System.Windows.Forms.TabPage tabDO;
        private System.Windows.Forms.Label AOplk_header;
        private System.Windows.Forms.Label DOplk_header;
        private System.Windows.Forms.ComboBox AO3_combo;
        private System.Windows.Forms.ComboBox AO2_combo;
        private System.Windows.Forms.ComboBox AO1_combo;
        private System.Windows.Forms.Label AO3_plkLabel;
        private System.Windows.Forms.Label AO2_plkLabel;
        private System.Windows.Forms.Label AO1_plkLabel;
        private System.Windows.Forms.ComboBox DO6_combo;
        private System.Windows.Forms.ComboBox DO5_combo;
        private System.Windows.Forms.ComboBox DO4_combo;
        private System.Windows.Forms.ComboBox DO3_combo;
        private System.Windows.Forms.ComboBox DO2_combo;
        private System.Windows.Forms.ComboBox DO1_combo;
        private System.Windows.Forms.Label DO6_plkLabel;
        private System.Windows.Forms.Label DO5_plkLabel;
        private System.Windows.Forms.Label DO4_plkLabel;
        private System.Windows.Forms.Label DO3_plkLabel;
        private System.Windows.Forms.Label DO2_plkLabel;
        private System.Windows.Forms.Label DO1_plkLabel;
        private System.Windows.Forms.Label signalsReadyLabel;
        private System.Windows.Forms.Panel block1_DOpanel;
        private System.Windows.Forms.ComboBox DO7bl1_combo;
        private System.Windows.Forms.Label DOblock1_header;
        private System.Windows.Forms.Label DO7_bl1Label;
        private System.Windows.Forms.ComboBox DO1bl1_combo;
        private System.Windows.Forms.ComboBox DO6bl1_combo;
        private System.Windows.Forms.Label DO1_bl1Label;
        private System.Windows.Forms.ComboBox DO5bl1_combo;
        private System.Windows.Forms.Label DO2_bl1Label;
        private System.Windows.Forms.ComboBox DO4bl1_combo;
        private System.Windows.Forms.Label DO3_bl1Label;
        private System.Windows.Forms.ComboBox DO3bl1_combo;
        private System.Windows.Forms.Label DO4_bl1Label;
        private System.Windows.Forms.ComboBox DO2bl1_combo;
        private System.Windows.Forms.Label DO5_bl1Label;
        private System.Windows.Forms.Label DO6_bl1Label;
        private System.Windows.Forms.Panel block2_DOpanel;
        private System.Windows.Forms.ComboBox DO7bl2_combo;
        private System.Windows.Forms.Label DOblock2_header;
        private System.Windows.Forms.Label DO7_bl2Label;
        private System.Windows.Forms.ComboBox DO1bl2_combo;
        private System.Windows.Forms.ComboBox DO6bl2_combo;
        private System.Windows.Forms.Label DO1_bl2Label;
        private System.Windows.Forms.ComboBox DO5bl2_combo;
        private System.Windows.Forms.Label DO2_bl2Label;
        private System.Windows.Forms.ComboBox DO4bl2_combo;
        private System.Windows.Forms.Label DO3_bl2Label;
        private System.Windows.Forms.ComboBox DO3bl2_combo;
        private System.Windows.Forms.Label DO4_bl2Label;
        private System.Windows.Forms.ComboBox DO2bl2_combo;
        private System.Windows.Forms.Label DO5_bl2Label;
        private System.Windows.Forms.Label DO6_bl2Label;
        private System.Windows.Forms.Panel block1_AOpanel;
        private System.Windows.Forms.ComboBox AO1bl1_combo;
        private System.Windows.Forms.ComboBox AO2bl1_combo;
        private System.Windows.Forms.Label AOblock1_header;
        private System.Windows.Forms.Label AO1_bl1Label;
        private System.Windows.Forms.Label AO2_bl1Label;
        private System.Windows.Forms.Panel block2_AOpanel;
        private System.Windows.Forms.ComboBox AO1bl2_combo;
        private System.Windows.Forms.ComboBox AO2bl2_combo;
        private System.Windows.Forms.Label AOblock2_header;
        private System.Windows.Forms.Label AO1_bl2Label;
        private System.Windows.Forms.Label AO2_bl2Label;
        private System.Windows.Forms.CheckBox sigFilAlarmCheck;
        private System.Windows.Forms.CheckBox sigAlarmCheck;
        private System.Windows.Forms.CheckBox sigWorkCheck;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.Label DO6_lab;
        private System.Windows.Forms.Label DO5_lab;
        private System.Windows.Forms.Label DO4_lab;
        private System.Windows.Forms.Label DO3_lab;
        private System.Windows.Forms.Label DO2_lab;
        private System.Windows.Forms.Label DO1_lab;
        private System.Windows.Forms.Label DO7bl2_lab;
        private System.Windows.Forms.Label DO1bl2_lab;
        private System.Windows.Forms.Label DO2bl2_lab;
        private System.Windows.Forms.Label DO3bl2_lab;
        private System.Windows.Forms.Label DO4bl2_lab;
        private System.Windows.Forms.Label DO5bl2_lab;
        private System.Windows.Forms.Label DO6bl2_lab;
        private System.Windows.Forms.Label DO7bl1_lab;
        private System.Windows.Forms.Label DO1bl1_lab;
        private System.Windows.Forms.Label DO2bl1_lab;
        private System.Windows.Forms.Label DO3bl1_lab;
        private System.Windows.Forms.Label DO4bl1_lab;
        private System.Windows.Forms.Label DO5bl1_lab;
        private System.Windows.Forms.Label DO6bl1_lab;
        private System.Windows.Forms.Label AO3_lab;
        private System.Windows.Forms.Label AO2_lab;
        private System.Windows.Forms.Label AO1_lab;
        private System.Windows.Forms.Label AO1bl2_lab;
        private System.Windows.Forms.Label AO2bl2_lab;
        private System.Windows.Forms.Label AO1bl1_lab;
        private System.Windows.Forms.Label AO2bl1_lab;
        private System.Windows.Forms.CheckBox connectCheck;
        private System.Windows.Forms.ComboBox comboReadType;
        private System.Windows.Forms.Button loadPLC_SignalsButton;
        private System.Windows.Forms.Label labelWriteNetTextBox;
        private System.Windows.Forms.Label label137;
        private System.Windows.Forms.Panel helpPanel;
        private System.Windows.Forms.Button backHelpButton;
        private System.Windows.Forms.LinkLabel linkModeronWeb;
        private System.Windows.Forms.Label label140;
        private AxAcroPDFLib.AxAcroPDF PDF_manual;
        private System.Windows.Forms.Button loadToExl;
        private System.Windows.Forms.Panel block3_AOpanel;
        private System.Windows.Forms.Label AO2bl3_lab;
        private System.Windows.Forms.ComboBox AO2bl3_combo;
        private System.Windows.Forms.Label AO2_bl3Label;
        private System.Windows.Forms.Label AO1bl3_lab;
        private System.Windows.Forms.Label AOblock3_header;
        private System.Windows.Forms.ComboBox AO1bl3_combo;
        private System.Windows.Forms.Label AO1_bl3Label;
        private System.Windows.Forms.Panel block3_DOpanel;
        private System.Windows.Forms.Label DO2bl3_lab;
        private System.Windows.Forms.ComboBox DO2bl3_combo;
        private System.Windows.Forms.Label DO2_bl3Label;
        private System.Windows.Forms.Label DO1bl3_lab;
        private System.Windows.Forms.Label DOblock3_header;
        private System.Windows.Forms.ComboBox DO1bl3_combo;
        private System.Windows.Forms.Label DO1_bl3Label;
        private System.Windows.Forms.Label DO7bl3_lab;
        private System.Windows.Forms.ComboBox DO7bl3_combo;
        private System.Windows.Forms.Label DO6bl3_lab;
        private System.Windows.Forms.ComboBox DO6bl3_combo;
        private System.Windows.Forms.Label DO6_bl3Label;
        private System.Windows.Forms.Label DO5bl3_lab;
        private System.Windows.Forms.ComboBox DO5bl3_combo;
        private System.Windows.Forms.Label DO5_bl3Label;
        private System.Windows.Forms.Label DO4bl3_lab;
        private System.Windows.Forms.ComboBox DO4bl3_combo;
        private System.Windows.Forms.Label DO4_bl3Label;
        private System.Windows.Forms.Label DO3bl3_lab;
        private System.Windows.Forms.ComboBox DO3bl3_combo;
        private System.Windows.Forms.Label DO3_bl3Label;
        private System.Windows.Forms.ToolStripMenuItem saveSpecToolStripMenuItem;
        private System.Windows.Forms.TextBox h_prDampBox;
        private System.Windows.Forms.TextBox b_prDampBox;
        private System.Windows.Forms.Label label166;
        private System.Windows.Forms.Label label158;
        private System.Windows.Forms.Label label168;
        private System.Windows.Forms.Label label167;
        private System.Windows.Forms.CheckBox springRetPrDampCheck;
        private System.Windows.Forms.Label prDampSLabel;
        private System.Windows.Forms.Label outDampSLabel;
        private System.Windows.Forms.CheckBox springRetOutDampCheck;
        private System.Windows.Forms.Label cmhOutDampLabel;
        private System.Windows.Forms.Label cmbOutDampLabel;
        private System.Windows.Forms.TextBox h_outDampBox;
        private System.Windows.Forms.TextBox b_outDampBox;
        private System.Windows.Forms.Label hOutDampLabel;
        private System.Windows.Forms.Label bOutDampLabel;
        private System.Windows.Forms.Label recircSLabel;
        private System.Windows.Forms.Label label170;
        private System.Windows.Forms.Label label171;
        private System.Windows.Forms.TextBox h_recircBox;
        private System.Windows.Forms.TextBox b_recircBox;
        private System.Windows.Forms.Label label172;
        private System.Windows.Forms.Label label175;
        private System.Windows.Forms.CheckBox springRetRecircCheck;
        private System.Windows.Forms.Label prDampTorqLabel;
        private System.Windows.Forms.Label outDampTorqLabel;
        private System.Windows.Forms.Label recircTorqLabel;
        private System.Windows.Forms.Panel markPrDampPanel;
        private System.Windows.Forms.Panel markOutDampPanel;
        private System.Windows.Forms.Panel markRecircPanel;
        private System.Windows.Forms.CheckBox stopStartCheck;
        private System.Windows.Forms.CheckBox prFanAlarmCheck;
        private System.Windows.Forms.CheckBox outFanAlarmCheck;
        private System.Windows.Forms.CheckBox outFanSpeedCheck;
        private System.Windows.Forms.CheckBox outFanStStopCheck;
        private System.Windows.Forms.CheckBox prFanStStopCheck;
        private System.Windows.Forms.CheckBox prFanSpeedCheck;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.CheckBox showWriteBoxCheck;
        private System.Windows.Forms.CheckBox analogFreonCheck;
        private System.Windows.Forms.ComboBox fireTypeCombo;
        private System.Windows.Forms.Label label169;
        private System.Windows.Forms.Label label138;
        private System.Windows.Forms.CheckBox fireCheck;
        private System.Windows.Forms.Button saveBinFileButton;
        private System.Windows.Forms.Label label_comboSysType;
        private System.Windows.Forms.Panel loadCanPanel;
        private System.Windows.Forms.Label label173;
        private System.Windows.Forms.Label netOptionLabel;
        private System.Windows.Forms.TextBox canAddressBox;
        private System.Windows.Forms.Label label174;
        private System.Windows.Forms.ComboBox speedCanCombo;
        private System.Windows.Forms.Label label176;
        private System.Windows.Forms.CheckBox showHintCheck;
        private System.Windows.Forms.Label label178;
        private System.Windows.Forms.Label label177;
        private System.Windows.Forms.ComboBox parityCanCombo;
        private System.Windows.Forms.Button connectPlkBtn;
        private System.Windows.Forms.Label label179;
        private System.Windows.Forms.TextBox dataCanTextBox;
        private System.Windows.Forms.Label label180;
        private System.Windows.Forms.Button backCanPanelButton;
        private System.Windows.Forms.TextBox writeCanTextBox;
        private System.Windows.Forms.Label label181;
        private System.Windows.Forms.CheckBox recircPrDampAOCheck;
        private System.Windows.Forms.CheckBox prDampFanCheck;
        private System.Windows.Forms.CheckBox prDampConfirmFanCheck;
        private System.Windows.Forms.CheckBox outDampConfirmFanCheck;
        private System.Windows.Forms.CheckBox outDampFanCheck;
        private System.Windows.Forms.CheckBox pumpCurProtect;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox pumpCurResProtect;
        private System.Windows.Forms.CheckBox reservPumpHeater;
        private System.Windows.Forms.CheckBox reservPumpAddHeater;
        private System.Windows.Forms.CheckBox pumpCurAddProtect;
        private System.Windows.Forms.CheckBox pumpCurResAddProtect;
        private System.Windows.Forms.CheckBox confHeatResPumpCheck;
        private System.Windows.Forms.CheckBox confAddHeatResPumpCheck;
        private System.Windows.Forms.ComboBox comboPlkType;
        private System.Windows.Forms.TabPage tabUI;
        private System.Windows.Forms.Label UIplk_header;
        private System.Windows.Forms.Label UI1_plkLabel;
        private System.Windows.Forms.ComboBox UI1_combo;
        private System.Windows.Forms.Label UI2_lab;
        private System.Windows.Forms.ComboBox UI2_typeCombo;
        private System.Windows.Forms.ComboBox UI2_combo;
        private System.Windows.Forms.Label UI2_plkLabel;
        private System.Windows.Forms.Label UI1_lab;
        private System.Windows.Forms.ComboBox UI1_typeCombo;
        private System.Windows.Forms.Label UI4_lab;
        private System.Windows.Forms.ComboBox UI4_typeCombo;
        private System.Windows.Forms.ComboBox UI4_combo;
        private System.Windows.Forms.Label UI4_plkLabel;
        private System.Windows.Forms.Label UI3_lab;
        private System.Windows.Forms.ComboBox UI3_typeCombo;
        private System.Windows.Forms.ComboBox UI3_combo;
        private System.Windows.Forms.Label UI3_plkLabel;
        private System.Windows.Forms.Label UI6_lab;
        private System.Windows.Forms.ComboBox UI6_typeCombo;
        private System.Windows.Forms.ComboBox UI6_combo;
        private System.Windows.Forms.Label UI6_plkLabel;
        private System.Windows.Forms.Label UI5_lab;
        private System.Windows.Forms.ComboBox UI5_typeCombo;
        private System.Windows.Forms.ComboBox UI5_combo;
        private System.Windows.Forms.Label UI5_plkLabel;
        private System.Windows.Forms.Label UI8_lab;
        private System.Windows.Forms.ComboBox UI8_typeCombo;
        private System.Windows.Forms.ComboBox UI8_combo;
        private System.Windows.Forms.Label UI8_plkLabel;
        private System.Windows.Forms.Label UI7_lab;
        private System.Windows.Forms.ComboBox UI7_typeCombo;
        private System.Windows.Forms.ComboBox UI7_combo;
        private System.Windows.Forms.Label UI7_plkLabel;
        private System.Windows.Forms.Label UI9_lab;
        private System.Windows.Forms.ComboBox UI9_typeCombo;
        private System.Windows.Forms.ComboBox UI9_combo;
        private System.Windows.Forms.Label UI9_plkLabel;
        private System.Windows.Forms.Label UI10_lab;
        private System.Windows.Forms.ComboBox UI10_typeCombo;
        private System.Windows.Forms.ComboBox UI10_combo;
        private System.Windows.Forms.Label UI10_plkLabel;
        private System.Windows.Forms.Label UI11_lab;
        private System.Windows.Forms.ComboBox UI11_typeCombo;
        private System.Windows.Forms.ComboBox UI11_combo;
        private System.Windows.Forms.Label UI11_plkLabel;
        private System.Windows.Forms.Panel plk_AOpanel;
        private System.Windows.Forms.Panel plk_DOpanel;
        private System.Windows.Forms.Panel plk_UIpanel;
        private System.Windows.Forms.Label DO8bl1_lab;
        private System.Windows.Forms.ComboBox DO8bl1_combo;
        private System.Windows.Forms.Label DO8_bl1Label;
        private System.Windows.Forms.Label DO8bl2_lab;
        private System.Windows.Forms.ComboBox DO8bl2_combo;
        private System.Windows.Forms.Label DO8_bl2Label;
        private System.Windows.Forms.Label DO8bl3_lab;
        private System.Windows.Forms.ComboBox DO8bl3_combo;
        private System.Windows.Forms.Label DO8_bl3Label;
        private System.Windows.Forms.Label DO7_bl3Label;
        private System.Windows.Forms.Panel block1_UIpanel;
        private System.Windows.Forms.Label UIblock1_header;
        private System.Windows.Forms.Label UI1_bl1Label;
        private System.Windows.Forms.ComboBox UI1bl1_combo;
        private System.Windows.Forms.ComboBox UI1bl1_typeCombo;
        private System.Windows.Forms.Label UI1bl1_lab;
        private System.Windows.Forms.Label UI11_bl1Label;
        private System.Windows.Forms.ComboBox UI11bl1_combo;
        private System.Windows.Forms.ComboBox UI11bl1_typeCombo;
        private System.Windows.Forms.Label UI11bl1_lab;
        private System.Windows.Forms.Label UI10_bl1Label;
        private System.Windows.Forms.ComboBox UI10bl1_combo;
        private System.Windows.Forms.ComboBox UI10bl1_typeCombo;
        private System.Windows.Forms.Label UI10bl1_lab;
        private System.Windows.Forms.Label UI9_bl1Label;
        private System.Windows.Forms.ComboBox UI9bl1_combo;
        private System.Windows.Forms.ComboBox UI9bl1_typeCombo;
        private System.Windows.Forms.Label UI9bl1_lab;
        private System.Windows.Forms.Label UI8_bl1Label;
        private System.Windows.Forms.ComboBox UI8bl1_combo;
        private System.Windows.Forms.ComboBox UI8bl1_typeCombo;
        private System.Windows.Forms.Label UI8bl1_lab;
        private System.Windows.Forms.Label UI7_bl1Label;
        private System.Windows.Forms.ComboBox UI7bl1_combo;
        private System.Windows.Forms.ComboBox UI7bl1_typeCombo;
        private System.Windows.Forms.Label UI7bl1_lab;
        private System.Windows.Forms.Label UI6_bl1Label;
        private System.Windows.Forms.ComboBox UI6bl1_combo;
        private System.Windows.Forms.ComboBox UI6bl1_typeCombo;
        private System.Windows.Forms.Label UI6bl1_lab;
        private System.Windows.Forms.Label UI5_bl1Label;
        private System.Windows.Forms.ComboBox UI5bl1_combo;
        private System.Windows.Forms.ComboBox UI5bl1_typeCombo;
        private System.Windows.Forms.Label UI5bl1_lab;
        private System.Windows.Forms.Label UI4_bl1Label;
        private System.Windows.Forms.ComboBox UI4bl1_combo;
        private System.Windows.Forms.ComboBox UI4bl1_typeCombo;
        private System.Windows.Forms.Label UI4bl1_lab;
        private System.Windows.Forms.Label UI3_bl1Label;
        private System.Windows.Forms.ComboBox UI3bl1_combo;
        private System.Windows.Forms.ComboBox UI3bl1_typeCombo;
        private System.Windows.Forms.Label UI3bl1_lab;
        private System.Windows.Forms.Label UI2_bl1Label;
        private System.Windows.Forms.ComboBox UI2bl1_combo;
        private System.Windows.Forms.ComboBox UI2bl1_typeCombo;
        private System.Windows.Forms.Label UI2bl1_lab;
        private System.Windows.Forms.Label UI16_bl1Label;
        private System.Windows.Forms.ComboBox UI16bl1_combo;
        private System.Windows.Forms.ComboBox UI16bl1_typeCombo;
        private System.Windows.Forms.Label UI16bl1_lab;
        private System.Windows.Forms.Label UI15_bl1Label;
        private System.Windows.Forms.ComboBox UI15bl1_combo;
        private System.Windows.Forms.ComboBox UI15bl1_typeCombo;
        private System.Windows.Forms.Label UI15bl1_lab;
        private System.Windows.Forms.Label UI14_bl1Label;
        private System.Windows.Forms.ComboBox UI14bl1_combo;
        private System.Windows.Forms.ComboBox UI14bl1_typeCombo;
        private System.Windows.Forms.Label UI14bl1_lab;
        private System.Windows.Forms.Label UI13_bl1Label;
        private System.Windows.Forms.ComboBox UI13bl1_combo;
        private System.Windows.Forms.ComboBox UI13bl1_typeCombo;
        private System.Windows.Forms.Label UI13bl1_lab;
        private System.Windows.Forms.Label UI12_bl1Label;
        private System.Windows.Forms.ComboBox UI12bl1_combo;
        private System.Windows.Forms.ComboBox UI12bl1_typeCombo;
        private System.Windows.Forms.Label UI12bl1_lab;
        private System.Windows.Forms.Panel block2_UIpanel;
        private System.Windows.Forms.Label UI16_bl2Label;
        private System.Windows.Forms.ComboBox UI16bl2_combo;
        private System.Windows.Forms.ComboBox UI16bl2_typeCombo;
        private System.Windows.Forms.Label UI16bl2_lab;
        private System.Windows.Forms.Label UI15_bl2Label;
        private System.Windows.Forms.ComboBox UI15bl2_combo;
        private System.Windows.Forms.ComboBox UI15bl2_typeCombo;
        private System.Windows.Forms.Label UI15bl2_lab;
        private System.Windows.Forms.Label UI14_bl2Label;
        private System.Windows.Forms.ComboBox UI14bl2_combo;
        private System.Windows.Forms.ComboBox UI14bl2_typeCombo;
        private System.Windows.Forms.Label UI14bl2_lab;
        private System.Windows.Forms.Label UI13_bl2Label;
        private System.Windows.Forms.ComboBox UI13bl2_combo;
        private System.Windows.Forms.ComboBox UI13bl2_typeCombo;
        private System.Windows.Forms.Label UI13bl2_lab;
        private System.Windows.Forms.Label UI12_bl2Label;
        private System.Windows.Forms.ComboBox UI12bl2_combo;
        private System.Windows.Forms.ComboBox UI12bl2_typeCombo;
        private System.Windows.Forms.Label UI12bl2_lab;
        private System.Windows.Forms.Label UI11_bl2Label;
        private System.Windows.Forms.ComboBox UI11bl2_combo;
        private System.Windows.Forms.ComboBox UI11bl2_typeCombo;
        private System.Windows.Forms.Label UI11bl2_lab;
        private System.Windows.Forms.Label UI10_bl2Label;
        private System.Windows.Forms.ComboBox UI10bl2_combo;
        private System.Windows.Forms.ComboBox UI10bl2_typeCombo;
        private System.Windows.Forms.Label UI10bl2_lab;
        private System.Windows.Forms.Label UI9_bl2Label;
        private System.Windows.Forms.ComboBox UI9bl2_combo;
        private System.Windows.Forms.ComboBox UI9bl2_typeCombo;
        private System.Windows.Forms.Label UI9bl2_lab;
        private System.Windows.Forms.Label UI8_bl2Label;
        private System.Windows.Forms.ComboBox UI8bl2_combo;
        private System.Windows.Forms.ComboBox UI8bl2_typeCombo;
        private System.Windows.Forms.Label UI8bl2_lab;
        private System.Windows.Forms.Label UI7_bl2Label;
        private System.Windows.Forms.ComboBox UI7bl2_combo;
        private System.Windows.Forms.ComboBox UI7bl2_typeCombo;
        private System.Windows.Forms.Label UI7bl2_lab;
        private System.Windows.Forms.Label UI6_bl2Label;
        private System.Windows.Forms.ComboBox UI6bl2_combo;
        private System.Windows.Forms.ComboBox UI6bl2_typeCombo;
        private System.Windows.Forms.Label UI6bl2_lab;
        private System.Windows.Forms.Label UI5_bl2Label;
        private System.Windows.Forms.ComboBox UI5bl2_combo;
        private System.Windows.Forms.ComboBox UI5bl2_typeCombo;
        private System.Windows.Forms.Label UI5bl2_lab;
        private System.Windows.Forms.Label UI4_bl2Label;
        private System.Windows.Forms.ComboBox UI4bl2_combo;
        private System.Windows.Forms.ComboBox UI4bl2_typeCombo;
        private System.Windows.Forms.Label UI4bl2_lab;
        private System.Windows.Forms.Label UI3_bl2Label;
        private System.Windows.Forms.ComboBox UI3bl2_combo;
        private System.Windows.Forms.ComboBox UI3bl2_typeCombo;
        private System.Windows.Forms.Label UI3bl2_lab;
        private System.Windows.Forms.Label UI2_bl2Label;
        private System.Windows.Forms.ComboBox UI2bl2_typeCombo;
        private System.Windows.Forms.Label UI2bl2_lab;
        private System.Windows.Forms.Label UI1_bl2Label;
        private System.Windows.Forms.ComboBox UI1bl2_combo;
        private System.Windows.Forms.Label UIblock2_header;
        private System.Windows.Forms.ComboBox UI1bl2_typeCombo;
        private System.Windows.Forms.Label UI1bl2_lab;
        private System.Windows.Forms.Panel block3_UIpanel;
        private System.Windows.Forms.Label UI16_bl3Label;
        private System.Windows.Forms.ComboBox UI16bl3_combo;
        private System.Windows.Forms.ComboBox UI16bl3_typeCombo;
        private System.Windows.Forms.Label UI16bl3_lab;
        private System.Windows.Forms.Label UI15_bl3Label;
        private System.Windows.Forms.ComboBox UI15bl3_combo;
        private System.Windows.Forms.ComboBox UI15bl3_typeCombo;
        private System.Windows.Forms.Label UI15bl3_lab;
        private System.Windows.Forms.Label UI14_bl3Label;
        private System.Windows.Forms.ComboBox UI14bl3_combo;
        private System.Windows.Forms.ComboBox UI14bl3_typeCombo;
        private System.Windows.Forms.Label UI14bl3_lab;
        private System.Windows.Forms.Label UI13_bl3Label;
        private System.Windows.Forms.ComboBox UI13bl3_combo;
        private System.Windows.Forms.ComboBox UI13bl3_typeCombo;
        private System.Windows.Forms.Label UI13bl3_lab;
        private System.Windows.Forms.Label UI12_bl3Label;
        private System.Windows.Forms.ComboBox UI12bl3_combo;
        private System.Windows.Forms.ComboBox UI12bl3_typeCombo;
        private System.Windows.Forms.Label UI12bl3_lab;
        private System.Windows.Forms.Label UI11_bl3Label;
        private System.Windows.Forms.ComboBox UI11bl3_combo;
        private System.Windows.Forms.ComboBox UI11bl3_typeCombo;
        private System.Windows.Forms.Label UI11bl3_lab;
        private System.Windows.Forms.Label UI10_bl3Label;
        private System.Windows.Forms.ComboBox UI10bl3_combo;
        private System.Windows.Forms.ComboBox UI10bl3_typeCombo;
        private System.Windows.Forms.Label UI10bl3_lab;
        private System.Windows.Forms.Label UI9_bl3Label;
        private System.Windows.Forms.ComboBox UI9bl3_combo;
        private System.Windows.Forms.ComboBox UI9bl3_typeCombo;
        private System.Windows.Forms.Label UI9bl3_lab;
        private System.Windows.Forms.Label UI8_bl3Label;
        private System.Windows.Forms.ComboBox UI8bl3_combo;
        private System.Windows.Forms.ComboBox UI8bl3_typeCombo;
        private System.Windows.Forms.Label UI8bl3_lab;
        private System.Windows.Forms.Label UI7_bl3Label;
        private System.Windows.Forms.ComboBox UI7bl3_combo;
        private System.Windows.Forms.ComboBox UI7bl3_typeCombo;
        private System.Windows.Forms.Label UI7bl3_lab;
        private System.Windows.Forms.Label UI6_bl3Label;
        private System.Windows.Forms.ComboBox UI6bl3_combo;
        private System.Windows.Forms.ComboBox UI6bl3_typeCombo;
        private System.Windows.Forms.Label UI6bl3_lab;
        private System.Windows.Forms.Label UI5_bl3Label;
        private System.Windows.Forms.ComboBox UI5bl3_combo;
        private System.Windows.Forms.ComboBox UI5bl3_typeCombo;
        private System.Windows.Forms.Label UI5bl3_lab;
        private System.Windows.Forms.Label UI4_bl3Label;
        private System.Windows.Forms.ComboBox UI4bl3_combo;
        private System.Windows.Forms.ComboBox UI4bl3_typeCombo;
        private System.Windows.Forms.Label UI4bl3_lab;
        private System.Windows.Forms.Label UI3_bl3Label;
        private System.Windows.Forms.ComboBox UI3bl3_combo;
        private System.Windows.Forms.ComboBox UI3bl3_typeCombo;
        private System.Windows.Forms.Label UI3bl3_lab;
        private System.Windows.Forms.Label UI2_bl3Label;
        private System.Windows.Forms.ComboBox UI2bl3_combo;
        private System.Windows.Forms.ComboBox UI2bl3_typeCombo;
        private System.Windows.Forms.Label UI2bl3_lab;
        private System.Windows.Forms.Label UI1_bl3Label;
        private System.Windows.Forms.ComboBox UI1bl3_combo;
        private System.Windows.Forms.Label UIblock3_header;
        private System.Windows.Forms.ComboBox UI1bl3_typeCombo;
        private System.Windows.Forms.Label UI1bl3_lab;
        private System.Windows.Forms.ComboBox UI2bl2_combo;
        private System.Windows.Forms.TabPage tabCmdWord;
        private System.Windows.Forms.RichTextBox cmdWordsTextBox;
        private System.Windows.Forms.Panel panelBlocks;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label M72E12RB_label;
        private System.Windows.Forms.Label M72E12RA_label;
        private System.Windows.Forms.Label M72E08RA_label;
        private System.Windows.Forms.Label M72E16NA_label;
        private System.Windows.Forms.PictureBox pic_signalsReady;
        private System.Windows.Forms.Button resetButtonSignals;
        private System.Windows.Forms.CheckBox pumpGlikConfCheck;
        private System.Windows.Forms.CheckBox pumpGlikCurProtect;
        private System.Windows.Forms.ComboBox prFanFcTypeCombo;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.ComboBox outFanFcTypeCombo;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.CheckBox pumpGlikResCurProtect;
        private System.Windows.Forms.CheckBox confGlikResPumpCheck;
        private System.Windows.Forms.CheckBox reservPumpGlik;
        private Label connectCanLabel;
        private ComboBox canSelectBox;
        private PictureBox refreshCanPorts;
        private Button loadCanButton;
        private Button readCanButton;
        private Label dataMatchPLC_label;
        private ProgressBar progressBarWrite;
        private Label processWriteLabel;
        private Label backConnectLabel;
    }
}

