namespace Moderon
{
    partial class FormInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInfo));
            this.closeInfoButton = new System.Windows.Forms.Button();
            this.versionLabel = new System.Windows.Forms.Label();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // closeInfoButton
            // 
            this.closeInfoButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.closeInfoButton.BackColor = System.Drawing.Color.MidnightBlue;
            this.closeInfoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeInfoButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.closeInfoButton.ForeColor = System.Drawing.Color.White;
            this.closeInfoButton.Location = new System.Drawing.Point(221, 343);
            this.closeInfoButton.Name = "closeInfoButton";
            this.closeInfoButton.Size = new System.Drawing.Size(94, 27);
            this.closeInfoButton.TabIndex = 63;
            this.closeInfoButton.Text = "ОК";
            this.closeInfoButton.UseVisualStyleBackColor = false;
            this.closeInfoButton.Click += new System.EventHandler(this.CloseInfoButton_Click);
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.versionLabel.Location = new System.Drawing.Point(475, 349);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(49, 14);
            this.versionLabel.TabIndex = 65;
            this.versionLabel.Text = "v.1.4.3";
            // 
            // panelInfo
            // 
            this.panelInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelInfo.BackgroundImage")));
            this.panelInfo.Location = new System.Drawing.Point(12, 12);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(513, 321);
            this.panelInfo.TabIndex = 64;
            // 
            // FormInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 380);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.closeInfoButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormInfo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "О программе";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeInfoButton;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label versionLabel;
    }
}