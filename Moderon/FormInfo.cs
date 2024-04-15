using System;
using System.Windows.Forms;

namespace Moderon
{
    public partial class FormInfo : Form
    {
        public FormInfo()
        {
            InitializeComponent();

            // Задание версии приложения
            label_aboutVersion.Text = "Версия программы - " + 
                Form1.VERSION.Substring(0, Form1.VERSION.Length - 2);

            label_EditionNumber.Text = "Номер редакции: " +
                Form1.VERSION.Substring(Form1.VERSION.Length - 1, 1);
        }

        // Закрытие информационного окна
        private void CloseInfoButton_Click(object sender, EventArgs e)
        {
            Close(); // Закрыть это окно
        }
    }
}
