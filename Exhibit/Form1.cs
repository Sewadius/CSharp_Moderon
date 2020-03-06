using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exhibit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            blockTabControlInitial(); // Скрытие вкладок элементов
            SelectComboBoxes(); // Выбор для comboBox
        }

        private void SelectComboBoxes()
        {
            comboSysType.SelectedItem = "П-система"; // Выбор системы изначально
            filterPrCombo.SelectedItem = "1";
            filterOutCombo.SelectedItem = "0";
            prFanPowCombo.SelectedItem = "380 В";
            prFanControlCombo.SelectedItem = "Подача питания";
            outFanPowCombo.SelectedItem = "380 В";
            outFanControlCombo.SelectedItem = "Подача питания";
            prDampPowCombo.SelectedItem = "24 В";
            outDampPowCombo.SelectedItem = "24 В";
            heatTypeCombo.SelectedItem = "Водяной";
            elHeatStagesCombo.SelectedItem = "1";
        }

        // Выход из программы, "Выход" в меню
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); // Выход из приложения
        }
        
        private void blockTabControlInitial()
        {
            filterPage.Parent = null;
            dampPage.Parent = null;
            heatPage.Parent = null;
            coolPage.Parent = null;
            humidPage.Parent = null;
            recircPage.Parent = null;
            recupPage.Parent = null;
        }

        private void CheckOptions()
        {
            bool a = filterCheck.Checked;
            bool b = dampCheck.Checked;
            bool c = heaterCheck.Checked;
            bool d = coolerCheck.Checked;
            bool e = humidCheck.Checked;
            bool f = recircCheck.Checked;
            bool g = recupCheck.Checked;
            if (!a && !b && !c && !d && !e && !f && !g)
                comboSysType.Enabled = true;
        }

        private void heaterCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (heaterCheck.Checked)
            {
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                heatPage.Parent = tabControl1;
            }
            else
            {
                heatPage.Parent = null;
                CheckOptions();
            }
        }

        private void coolerCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (coolerCheck.Checked)
            {
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                coolPage.Parent = tabControl1;
            }
            else
            {
                coolPage.Parent = null;
                CheckOptions();
            }
                
        }

        private void humidCheck_CheckedChanged(object sender, EventArgs e)
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
                
        }

        private void recircCheck_CheckedChanged(object sender, EventArgs e)
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
                
        }

        private void recupCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (recupCheck.Checked)
            {
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                recupPage.Parent = tabControl1;
            }
            else
            {
                recupPage.Parent = null;
                CheckOptions();
            }
                
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            comboSysType.Enabled = true; // Разблокировка выбора типа системы
            comboSysType.SelectedIndex = 0; // Выбор приточной системы
            filterCheck.Checked = false;
            dampCheck.Checked = false;
            heaterCheck.Checked = false;
            coolerCheck.Checked = false;
            humidCheck.Checked = false;
            recircCheck.Checked = false;
            recupCheck.Checked = false;
            // Блокировка для рециркуляции и рекуператора
            recircCheck.Enabled = false;
            recupCheck.Enabled = false;
            outFanPanel.Visible = false;
        }

        private void comboSysType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSysType.SelectedIndex == 0) // Приточная система
            {
                recircCheck.Enabled = false;
                recupCheck.Enabled = false;
                outFanPanel.Visible = false;
                outFilterPanel.Visible = false;
                outDampPanel.Visible = false;
            }
            else // Приточно-вытяжная система
            {
                recircCheck.Enabled = true;
                recupCheck.Enabled = true;
                outFanPanel.Visible = true;
                outFilterPanel.Visible = true;
                outDampPanel.Visible = true;
            }
        }

        private void filterCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (filterCheck.Checked)
            {
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                filterPage.Parent = tabControl1;
            }
            else
            {
                filterPage.Parent = null;
                CheckOptions();
            }
        }

        private void dampCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (dampCheck.Checked)
            {
                if (comboSysType.Enabled) comboSysType.Enabled = false;
                dampPage.Parent = tabControl1;
            }
            else
            {
                dampPage.Parent = null;
                CheckOptions();
            }
        }
    }
}
