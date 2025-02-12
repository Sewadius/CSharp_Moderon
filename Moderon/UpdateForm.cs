using System;
using System.ComponentModel;
using System.Deployment.Application;
using System.Windows.Forms;

namespace Moderon
{
    public partial class UpdateForm : Form
    {
        private readonly ApplicationDeployment applicationDeployment;

        public UpdateForm(ApplicationDeployment ap)
        {
            InitializeComponent();
            applicationDeployment = ap;
            applicationDeployment.UpdateProgressChanged += ApplicationDeployment_UpdateProgressChanged;
            applicationDeployment.UpdateCompleted += ApplicationDeployment_UpdateCompleted;
            applicationDeployment.UpdateAsync();
        }

        private void ApplicationDeployment_UpdateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Что-то пошло не так\n"+e.Error);
                DialogResult = DialogResult.Abort;
            }
            if (e.Cancelled)
            {
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }

            Close();
        }

        private void ApplicationDeployment_UpdateProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void ButtonUpdateCancel_Click(object sender, EventArgs e)
        {
            applicationDeployment.UpdateAsyncCancel();
        }
    }
}
