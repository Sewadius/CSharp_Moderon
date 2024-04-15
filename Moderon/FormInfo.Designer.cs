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
            this.closeInfoButton = new System.Windows.Forms.Button();
            this.pictureBoxInfo = new System.Windows.Forms.PictureBox();
            this.label_aboutVersion = new System.Windows.Forms.Label();
            this.label_EditionNumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // closeInfoButton
            // 
            this.closeInfoButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.closeInfoButton.BackColor = System.Drawing.Color.DarkGreen;
            this.closeInfoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeInfoButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.closeInfoButton.ForeColor = System.Drawing.Color.White;
            this.closeInfoButton.Location = new System.Drawing.Point(215, 303);
            this.closeInfoButton.Name = "closeInfoButton";
            this.closeInfoButton.Size = new System.Drawing.Size(81, 27);
            this.closeInfoButton.TabIndex = 63;
            this.closeInfoButton.Text = "ОК";
            this.closeInfoButton.UseVisualStyleBackColor = false;
            this.closeInfoButton.Click += new System.EventHandler(this.CloseInfoButton_Click);
            // 
            // pictureBoxInfo
            // 
            this.pictureBoxInfo.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxInfo.Image = global::Moderon.Properties.Resources.info_screen;
            this.pictureBoxInfo.InitialImage = global::Moderon.Properties.Resources.info_screen;
            this.pictureBoxInfo.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxInfo.Name = "pictureBoxInfo";
            this.pictureBoxInfo.Size = new System.Drawing.Size(502, 276);
            this.pictureBoxInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxInfo.TabIndex = 64;
            this.pictureBoxInfo.TabStop = false;
            // 
            // label_aboutVersion
            // 
            this.label_aboutVersion.AutoSize = true;
            this.label_aboutVersion.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_aboutVersion.Location = new System.Drawing.Point(12, 309);
            this.label_aboutVersion.Name = "label_aboutVersion";
            this.label_aboutVersion.Size = new System.Drawing.Size(181, 14);
            this.label_aboutVersion.TabIndex = 65;
            this.label_aboutVersion.Text = "Версия программы - v.1.1.2";
            // 
            // label_EditionNumber
            // 
            this.label_EditionNumber.AutoSize = true;
            this.label_EditionNumber.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_EditionNumber.Location = new System.Drawing.Point(355, 309);
            this.label_EditionNumber.Name = "label_EditionNumber";
            this.label_EditionNumber.Size = new System.Drawing.Size(128, 14);
            this.label_EditionNumber.TabIndex = 66;
            this.label_EditionNumber.Text = "Номер редакции: 1";
            // 
            // FormInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 342);
            this.Controls.Add(this.label_EditionNumber);
            this.Controls.Add(this.label_aboutVersion);
            this.Controls.Add(this.pictureBoxInfo);
            this.Controls.Add(this.closeInfoButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormInfo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "О программе";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeInfoButton;
        private System.Windows.Forms.PictureBox pictureBoxInfo;
        private System.Windows.Forms.Label label_aboutVersion;
        private System.Windows.Forms.Label label_EditionNumber;
    }
}