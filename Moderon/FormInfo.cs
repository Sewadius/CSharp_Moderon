using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moderon
{
    public partial class FormInfo : Form
    {
        public FormInfo()
        {
            InitializeComponent();
        }

        // Закрытие информационного окна
        private void CloseInfoButton_Click(object sender, EventArgs e)
        {
            Close(); // Закрыть это окно
        }
    }
}
